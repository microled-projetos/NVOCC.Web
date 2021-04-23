Public Class TrocarSenha
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Verifica se usuario está logado
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
    End Sub

    Private Sub btnTrocarSenha_Click(sender As Object, e As EventArgs) Handles btnTrocarSenha.Click
        divmsg.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        'Criptografa senha e realiza alteração no banco de dados
        Dim Criptografar As New Criptografia
        Dim Senha As String = Criptografar.CriptografarSenha(txtNovaSenha.Text)
        Con.ExecutarQuery("UPDATE [dbo].[TB_USUARIO] SET SENHA ='" & Senha & "'  WHERE ID_USUARIO = " & Session("ID_USUARIO"))
        txtNovaSenha.Text = ""
        divmsg.Visible = True
        Con.Fechar()
    End Sub
End Class