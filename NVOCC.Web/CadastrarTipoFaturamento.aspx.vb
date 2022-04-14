Public Class CadastrarTipoFaturamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        If Request.QueryString("p") = "" Then

            Response.Redirect("Default.aspx")
        Else

            If Not IsPostBack Then
                txtIDParceiro.Text = Request.QueryString("p")
                Dim Con As New Conexao_sql
                Dim ds As DataSet
                Con.Conectar()
                ds = Con.ExecutarQuery("SELECT NM_FANTASIA FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO = " & txtIDParceiro.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    lblRazaoSocial.Text = ds.Tables(0).Rows(0).Item("NM_FANTASIA").ToString()
                End If
                If Request.QueryString("id") <> "" Then

                End If
            End If

        End If
    End Sub

End Class