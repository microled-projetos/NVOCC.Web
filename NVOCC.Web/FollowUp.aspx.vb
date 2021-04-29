Public Class FollowUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            txtID_BL.Text = Request.QueryString("id")
            ds = Con.ExecutarQuery("SELECT NR_BL FROM [TB_BL] WHERE ID_BL = " & Request.QueryString("id"))
            If ds.Tables(0).Rows.Count > 0 Then
                NumeroBL.Text = " - " & ds.Tables(0).Rows(0).Item("NR_BL")
            End If

        End If


        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub
    Private Sub gdvFollowUp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdvFollowUp.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim img As Image = CType(e.Row.FindControl("image1"), Image)

            Select Case e.Row.Cells(5).Text

                Case "ETAPA REALIZADA"
                    img.ImageUrl = "Content/imagens/159e1b.png"
                    img.Visible = True

                Case "ETAPA PENDENTE"

                    img.ImageUrl = "Content/imagens/e72c17.png"
                    img.Visible = True

            End Select

        End If

    End Sub


End Class