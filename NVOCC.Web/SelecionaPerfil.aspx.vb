Public Class SelecionaPerfil
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
    End Sub

    Private Sub btnAcessar_Click(sender As Object, e As EventArgs) Handles btnAcessar.Click
        Session("ID_TIPO_USUARIO") = rdTipoUsuario.SelectedValue
        Response.Redirect("Default.aspx")
    End Sub
End Class