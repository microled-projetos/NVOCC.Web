Imports System.Net.Mail
Imports Attachment = System.Net.Mail.Attachment
Public Class RecuperarSenha
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnRecuperarSenha_Click(sender As Object, e As EventArgs) Handles btnRecuperarSenha.Click
        divsucesso.Visible = False
        divInfo.Visible = False
        divErro.Visible = False

        'Verifica se campo necessario foi preenchido
        If txtCpfEmail.Text = "" Then
            divInfo.Visible = True
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim Criptografar As New Criptografia
            'Gera senha temporária
            Dim senhaTemporaria As String = GetRandomPassword(8)
            'Criptografa senha temporaria e realiza alteração no banco
            Dim Senha As String = Criptografar.CriptografarSenha(senhaTemporaria)
            Con.ExecutarQuery("UPDATE [dbo].[TB_USUARIO] SET SENHA ='" & Senha & "' WHERE EMAIL = '" & txtCpfEmail.Text & "'")
            Con.Fechar()
            'Chama serviço de mail e envia senha temporária para o email informado pelo usuario
            Dim email As New EmailService
            Dim Mensagem As String = "Caro, Sr(a)!<br/><br/> Sua senha foi redefinida. <br/><br/> Nova senha: " & senhaTemporaria & "<br/><br/>Realize a troca em seu primeiro acesso. <br/><br/>Em caso de dúvidas entre em contato com o suporte."
            Dim retorno As String = processaFila(txtCpfEmail.Text, "Senha Temporaria - NVOCC", Mensagem)
            If retorno = False Then
                divErro.Visible = True
            Else
                divsucesso.Visible = True
            End If
        End If

    End Sub
    Function GetRandomPassword(ByVal length As Integer) As String
        'Gera senha temporária

        Static rand As New Random

        Dim password As New System.Text.StringBuilder(length)

        For i As Integer = 1 To length

            Dim charIndex As Integer
            Do

                charIndex = rand.Next(48, 123)

            Loop Until (charIndex >= 48 AndAlso charIndex <= 57)

            password.Append(Convert.ToChar(charIndex))

        Next

        Return password.ToString()

    End Function

    Function processaFila(email As String, assunto As String, msg As String) As Boolean
        Dim sSql As String
        Dim anexos As Attachment()
        Dim critica As String = ""
        Dim enderecos As String = ""
        Dim rsParam As DataSet = Nothing
        Dim indExc As Long
        Dim nomeArq As String
        Dim validaEnd As String
        Dim ends() As String
        Dim Mail As New MailMessage
        Dim smtp As New SmtpClient()

        Try
            Dim Con As New Conexao_sql
            Con.Conectar()

            sSql = "SELECT EMAIL_REMETENTE, END_SMTP, SENHA_REMETENTE, DOMINIO_REMETENTE, EXIGE_SSL, PORTA_SMTP, DIR_EMAIL_GER AS DIR_EMAIL "
            sSql = sSql & " FROM TB_PARAMETROS "
            rsParam = Con.ExecutarQuery(sSql)

            If rsParam.Tables(0).Rows.Count > 0 Then


                Mail = New MailMessage
                Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                Try
                    Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                Catch ex As Exception
                    critica = "Endereço de envio dos e-mails inválido [" & rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString & "] "
                    lblErroMsg.Text = critica
                    Return False
                End Try


                Try
                    smtp = New SmtpClient(rsParam.Tables(0).Rows(0)("END_SMTP").ToString)
                    If rsParam.Tables(0).Rows(0)("EXIGE_SSL").ToString = "1" Then
                        smtp.EnableSsl = True
                    Else
                        smtp.EnableSsl = False
                    End If
                    smtp.Credentials = New System.Net.NetworkCredential(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString, rsParam.Tables(0).Rows(0)("SENHA_REMETENTE").ToString, rsParam.Tables(0).Rows(0)("DOMINIO_REMETENTE").ToString)
                    smtp.Port = rsParam.Tables(0).Rows(0)("PORTA_SMTP").ToString
                Catch ex As Exception
                    critica = "Configurações de envio de e-mail inválidas, contate o suporte!" & Err.Description
                    lblErroMsg.Text = critica
                    Return False

                End Try


                'ASSUNTO
                Mail.Subject = assunto


                'CORPO
                Mail.Body = msg
                Mail.IsBodyHtml = True


                'DESTINATARIO
                enderecos = email
                Dim palavras As String() = enderecos.Split(New String() _
          {";"}, StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
                    Mail.To.Add(palavras(i).ToString)

                Next

                Try

                    smtp.Send(Mail)

                    smtp.Dispose()

                Catch ex As Exception
                    critica = "Ocorreu um erro ao enviar o e-mail! Erro:  " & Err.Description
                    lblErroMsg.Text = critica
                    Err.Clear()
                    Return False

                End Try

            Else
                critica = "Não foi possível acessar As configurações para envio de e-mails, contate o suporte!"
                lblErroMsg.Text = critica
                Return False

            End If
        Catch ex As Exception
            critica = "Ocorreu um erro ao realizar o envio de e-mails, contate o suporte!" & vbCrLf & "Erro:  " & Err.Description
            lblErroMsg.Text = critica
            Return False

        End Try

        Return True

    End Function

End Class