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
            Dim retorno As String = email.EnviarEmail(txtCpfEmail.Text, "Senha Temporaria - NVOCC", Mensagem)
            If retorno = "" Then
                divErro.Visible = True
            Else
                divsucesso.Visible = True
                lblSucessoMsg.Text = retorno
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
End Class