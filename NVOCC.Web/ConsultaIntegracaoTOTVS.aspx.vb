Public Class ConsultaIntegracaoTOTVS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Sub Pesquisa()
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim FILTRO As String = ""

        If txtDataInicioBusca.Text = "" Or txtDataFimBusca.Text = "" Or ddlFiltroTipo.SelectedValue = "0" Then

            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Filtros obrigatórios!');</script>", False)

        Else
            Dim ds As DataSet
            Dim sql As String = "SELECT * FROM [dbo].[FN_INTEGRACAO_TOTVS]('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') ORDER BY DATA_EMISSAO DESC"



            ' dgvConsulta.DataSource = ds.Tables(0)
            dsConsulta.SelectCommand = sql

            dgvConsulta.DataBind()
            divDados.Visible = True

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [FN_INTEGRACAO_TOTVS] ('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') WHERE NUMERO_DOC <> '   TOTAL' ")
            lblTotalNF.Text = ds.Tables(0).Rows(0).Item("QTD")

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [FN_INTEGRACAO_TOTVS] ('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') WHERE NUMERO_DOC <> '   TOTAL' AND CANCELADA <> 0 ")
            lblNFCanceladas.Text = ds.Tables(0).Rows(0).Item("QTD")

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [FN_INTEGRACAO_TOTVS] ('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') WHERE NUMERO_DOC <> '   TOTAL' AND DATA_INTEG_REC IS NOT NULL ")
            lblNFIntegradas.Text = ds.Tables(0).Rows(0).Item("QTD")

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [FN_INTEGRACAO_TOTVS] ('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') WHERE NUMERO_DOC <> '   TOTAL' AND DATA_INTEG_REC IS NULL ")
            lblNFNaoIntegradas.Text = ds.Tables(0).Rows(0).Item("QTD")

        End If
    End Sub
    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        Pesquisa()
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("ConsultaIntegracaoTOTVS.aspx")
    End Sub

    Private Sub dgvConsulta_Sorting(sender As Object, e As GridViewSortEventArgs) Handles dgvConsulta.Sorting
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvConsulta.DataSource = Session("TaskTable")
            dgvConsulta.DataBind()
            dgvConsulta.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
        Pesquisa()
    End Sub

    Private Function GetSortDirection(ByVal column As String) As String
        Dim sortDirection As String = "ASC"
        Dim sortExpression As String = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then

            If sortExpression = column Then
                Dim lastDirection As String = TryCast(ViewState("SortDirection"), String)

                If (lastDirection IsNot Nothing) AndAlso (lastDirection = "ASC") Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column
        Return sortDirection
    End Function

    Private Sub dsConsulta_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles dsConsulta.Selecting
        e.Command.CommandTimeout = 0
    End Sub
End Class