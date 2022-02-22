Public Class EmissaoND
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2027 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
            If Request.QueryString("id") <> "" Then
                txtID_BL.Text = Request.QueryString("id")
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM TB_BL WHERE ID_BL = " & txtID_BL.Text)
                If ds1.Tables(0).Rows.Count > 0 Then
                    lblMBL.Text = ds1.Tables(0).Rows(0).Item("NR_BL")
                End If
            End If
        End If
    End Sub

    Private Sub dgvNotas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvNotas.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False

        If e.CommandName = "Selecionar" Then
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ID_CIDADE As Integer = 0
            txtID.text = e.CommandArgument
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ND()", True)
            Con.Fechar()
        End If
    End Sub
End Class