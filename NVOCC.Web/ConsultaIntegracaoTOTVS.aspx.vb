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

            Dim sql As String = "SELECT * FROM [dbo].[FN_INTEGRACAO_TOTVS]('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') ORDER BY DATA_EMISSAO DESC"

            dsConsulta.SelectCommand = sql
            dgvConsulta.DataBind()
            divDados.Visible = True

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [FN_INTEGRACAO_TOTVS] ('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') WHERE NUMERO_DOC <> 'TOTAL' ")
            lblTotalNF.Text = ds.Tables(0).Rows(0).Item("QTD")

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [FN_INTEGRACAO_TOTVS] ('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') WHERE NUMERO_DOC <> 'TOTAL' AND CANCELADA <> 0 ")
            lblNFCanceladas.Text = ds.Tables(0).Rows(0).Item("QTD")

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [FN_INTEGRACAO_TOTVS] ('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') WHERE NUMERO_DOC <> 'TOTAL' AND DATA_INTEG_REC IS NOT NULL ")
            lblNFIntegradas.Text = ds.Tables(0).Rows(0).Item("QTD")

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [FN_INTEGRACAO_TOTVS] ('" & ddlFiltroTipo.SelectedValue & "','" & txtDataInicioBusca.Text & "','" & txtDataFimBusca.Text & "') WHERE NUMERO_DOC <> 'TOTAL' AND DATA_INTEG_REC IS NULL ")
            lblNFNaoIntegradas.Text = ds.Tables(0).Rows(0).Item("QTD")

        End If
    End Sub
    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        Pesquisa()
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("ConsultaIntegracaoTOTVS.aspx")
    End Sub
End Class