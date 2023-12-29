Public Class Erro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("erro") Is Nothing Then
            lblerro.Text = Session("erro")
        End If
    End Sub

End Class