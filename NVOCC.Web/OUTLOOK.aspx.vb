Imports System.Runtime.InteropServices
Imports System.Net.Mail
Imports Attachment = System.Net.Mail.Attachment

Public Class OUTLOOK
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else

            If Not Page.IsPostBack Then
                Dim Nome As String = ""
                Dim ID_COTACAO As String = Request.QueryString("c")


                ds = Con.ExecutarQuery("SELECT NR_COTACAO FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO)
                txtCotacao.Text = ID_COTACAO
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_COTACAO")) Then
                        lblCotacao.Text = ds.Tables(0).Rows(0).Item("NR_COTACAO")
                        txtCotacao.Text = ds.Tables(0).Rows(0).Item("NR_COTACAO").Substring(0, ds.Tables(0).Rows(0).Item("NR_COTACAO").IndexOf("/"))
                    End If
                End If


                ds = Con.ExecutarQuery("select isnull(lower(EMAIL_CONTATO),'')EMAIL_CONTATO,isnull(NM_CONTATO,'')NM_CONTATO from TB_CONTATO where ID_CONTATO = (select ID_CONTATO from TB_COTACAO where ID_COTACAO = " & ID_COTACAO & ")")

                If ds.Tables(0).Rows.Count > 0 Then
                    txtDestinatario.Text = "juliane@microled.com.br" ' ds.Tables(0).Rows(0).Item("EMAIL_CONTATO").ToString()
                    Nome = ds.Tables(0).Rows(0).Item("NM_CONTATO").ToString()
                End If
                txtMsg.Text = "Prezado(a) " & Nome & ", segue sua proposta visando uma oportunidade de embarque."
                txtAssunto.Text = "COTAÇÃO"
                lblAnexo.Text = "CotacaoPDF_" & txtCotacao.Text & ".pdf"
                ds = Con.ExecutarQuery("SELECT isnull(lower(EMAIL),'')EMAIL FROM TB_USUARIO where ID_USUARIO = " & Session("ID_USUARIO"))

                If ds.Tables(0).Rows.Count > 0 Then
                    txtCC.Text = ds.Tables(0).Rows(0).Item("EMAIL").ToString()
                End If
            End If
        End If

        Con.Fechar()
    End Sub

    Private Sub btnSair_Click(sender As Object, e As EventArgs) Handles btnSair.Click
        Response.Redirect("CotacaoComercial.aspx")
    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click

        If SeparaEmail(txtCC.Text) = False Then
            lblerro.Text = "Email de CC é invalido!"
            lblerro.Visible = True
        ElseIf SeparaEmail(txtDestinatario.Text) = False Then
            lblerro.Text = "Email de destinatário é invalido!"
            lblerro.Visible = True
        Else

            If processaFila() = False Then
                divSuccess.Visible = False
                divErro.Visible = True
            Else
                divErro.Visible = False
                divSuccess.Visible = True
                lblSuccess.Text = "Email enviado com sucesso!"
            End If
        End If
    End Sub


    Function SeparaEmail(ByVal email As String) As Boolean
        divSuccess.Visible = False
        divErro.Visible = True

        Dim erro As Boolean
        'quebrar a string
        Dim palavras As String() = email.Split(New String() _
          {";"}, StringSplitOptions.RemoveEmptyEntries)

        'exibe o resultado
        For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
            If ValidaEmail.Validar(palavras(i)) = False Then
                erro = False
            Else
                erro = True
            End If
        Next
        Return erro
    End Function


    Function processaFila() As Boolean
        Dim sSql As String
        Dim anexos As Attachment()
        Dim envia As Boolean = False
        Dim critica As String = ""
        Dim rsParam As DataSet = Nothing
        Dim rsEmail As DataTable = Nothing
        Dim rsAnexos As DataTable = Nothing
        Dim enderecos As String = ""
        Dim dirEmail As String = ""
        Dim arqExc As List(Of String) = Nothing
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


                dirEmail = Server.MapPath("/Content/cotacoes/CotacaoPDF_" & txtCotacao.Text & ".pdf")



                Mail = New MailMessage
                Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                Try
                    Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                Catch ex As Exception
                    critica = "Endereço de envio dos e-mails inválido [" & rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString & "] "
                    lblerro.Text = critica
                    lblerro.Visible = True
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
                    lblerro.Text = critica
                    lblerro.Visible = True
                    Return False

                End Try

                'ASSUNTO
                If txtAssunto.Text <> "" Then
                    Mail.Subject = txtAssunto.Text
                Else
                    Mail.Subject = ""
                End If

                'CORPO
                If txtMsg.Text <> "" Then
                    Mail.Body = txtMsg.Text
                Else
                    Mail.Body = ""
                End If

                'Mail.IsBodyHtml = True

                enderecos = txtDestinatario.Text
                Mail.To.Add(enderecos)

                nomeArq = Server.MapPath("/Content/cotacoes/CotacaoPDF_" & txtCotacao.Text & ".pdf")

                Dim anexo As New Attachment(nomeArq)
                Mail.Attachments.Add(anexo)

                Try

                    smtp.Send(Mail)

                    smtp.Dispose()

                Catch ex As Exception
                    critica = "Ocorreu um erro ao enviar o e-mail! Erro:  " & Err.Description
                    lblerro.Text = critica
                    lblerro.Visible = True
                    Err.Clear()
                    Return False

                End Try

            Else
                critica = "Não foi possível acessar As configurações para envio de e-mails, contate o suporte!"
                lblerro.Text = critica
                lblerro.Visible = True
                Return False

            End If
        Catch ex As Exception
            critica = "Ocorreu um erro ao realizar o envio de e-mails, contate o suporte!" & vbCrLf & "Erro:  " & Err.Description
            lblerro.Text = critica
            lblerro.Visible = True
            Return False

        End Try

        Return True

    End Function

End Class