Public Class ConsultaIntegracaoTOTVS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Sub Pesquisa()
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim FILTRO As String = ""

        If txtDataInicioBusca.Text = "" And txtDataFimBusca.Text = "" And ddlFiltroTipo.SelectedValue = "0" Then

            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe um filtro!');</script>", False)

        Else


            If ddlFiltroTipo.SelectedValue <> "0" Then
                If FILTRO = "" Then
                    FILTRO = " WHERE TIPO = '" & ddlFiltroTipo.SelectedValue & "'"
                Else
                    FILTRO = FILTRO & " AND TIPO = '" & ddlFiltroTipo.SelectedValue & "'"
                End If
            End If

            If txtDataInicioBusca.Text <> "" Then

                If FILTRO = "" Then
                    FILTRO = " WHERE CONVERT(DATE,DATA_EMISSAO,103) >= CONVERT(DATE,'" & txtDataInicioBusca.Text & "',103) "
                Else
                    FILTRO = FILTRO & " AND CONVERT(DATE,DATA_EMISSAO,103) >= CONVERT(DATE,'" & txtDataInicioBusca.Text & "',103)"
                End If


            End If

            If txtDataFimBusca.Text <> "" Then
                If FILTRO = "" Then
                    FILTRO = " WHERE CONVERT(DATE,DATA_EMISSAO,103) <= CONVERT(DATE,'" & txtDataFimBusca.Text & "',103)"
                Else
                    FILTRO = FILTRO & " AND CONVERT(DATE,DATA_EMISSAO,103) <= CONVERT(DATE,'" & txtDataFimBusca.Text & "',103)"
                End If

            End If
            Dim sql As String = "SELECT * FROM [dbo].[VW_INTEGRACAO_TOTVS]  " & FILTRO & " ORDER BY DATA_EMISSAO DESC"

            dsConsulta.SelectCommand = sql
            dgvConsulta.DataBind()
            divDados.Visible = True

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [VW_INTEGRACAO_TOTVS] " & FILTRO)
            lblTotalNF.Text = ds.Tables(0).Rows(0).Item("QTD")

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [VW_INTEGRACAO_TOTVS] " & FILTRO & " AND CANCELADA <> 0 ")
            lblNFCanceladas.Text = ds.Tables(0).Rows(0).Item("QTD")

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [VW_INTEGRACAO_TOTVS] " & FILTRO & " AND DATA_INTEG_REC IS NOT NULL ")
            lblNFIntegradas.Text = ds.Tables(0).Rows(0).Item("QTD")

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [VW_INTEGRACAO_TOTVS] " & FILTRO & " AND DATA_INTEG_REC IS NULL ")
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