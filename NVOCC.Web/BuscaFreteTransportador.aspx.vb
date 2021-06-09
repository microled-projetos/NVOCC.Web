Public Class BuscaFreteTransportador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
    End Sub

    Private Sub ddlConsultas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConsultas.SelectedIndexChanged
        If ddlConsultas.SelectedValue = 1 Then
            ocean.Visible = True
            locais.Visible = False
            txtDataInicial.Text = Now.Date.ToString("dd-MM-yyyy")


        ElseIf ddlConsultas.SelectedValue = 2 Then
            locais.Visible = True
            ocean.Visible = False

        Else
            ocean.Visible = False
            locais.Visible = False

        End If
    End Sub

    Private Sub bntPesquisarLocais_Click(sender As Object, e As EventArgs) Handles bntPesquisarLocais.Click
        diverro.Visible = False

        If ddlDestinoLocais.SelectedValue = 0 Or ddlTransportadorLocais.SelectedValue = 0 Then
            lblmsgErro.Text = "Preencha os filtros para pesquisar"
            diverro.Visible = True

        Else
            Dim sql As String = " SELECT * FROM [dbo].[View_Taxas_locais_Armador]
WHERE ID_TRANSPORTADOR = " & ddlTransportadorLocais.SelectedValue & " AND ID_PORTO = " & ddlDestinoLocais.SelectedValue & " order by VL_TAXA_LOCAL_COMPRA"
            dsTaxas.SelectCommand = sql
            dgvTaxas.DataBind()

            DivGrid.Visible = True
        End If


    End Sub
    Private Sub bntPesquisarOcean_Click(sender As Object, e As EventArgs) Handles bntPesquisarOcean.Click
        Dim v As New VerificaData
        diverro.Visible = False

        If v.ValidaData(txtDataFinal.Text) = False Or v.ValidaData(txtDataInicial.Text) = False Then
            divErro.Visible = True
            lblmsgErro.Text = "Data Inválida."
        ElseIf ddlTransportadorOcean.SelectedValue = 0 Or ddlDestinoOcean.SelectedValue = 0 Then 'Or ddlContainer.SelectedValue = 0
            lblmsgErro.Text = "Preencha os filtros para pesquisar"
            diverro.Visible = True

        Else
            Dim sql As String = "SELECT * FROM [dbo].[View_Taxas_locais_Armador]
WHERE ID_TRANSPORTADOR = " & ddlTransportadorOcean.SelectedValue & " AND ID_PORTO = " & ddlDestinoOcean.SelectedValue & " AND DT_VALIDADE_INICIAL BETWEEN  convert(date,'" & txtDataInicial.Text & "',103) AND  convert(date,'" & txtDataFinal.Text & "',103) order by VL_TAXA_LOCAL_COMPRA"
            dsTaxas.SelectCommand = sql
            dgvTaxas.DataBind()
            DivGrid.Visible = True

            'container LIKE '%" & ddlContainer.SelectedItem.Text & "%'
        End If


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
    Protected Sub dgvTaxas_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        divsuccess.Visible = False
        diverro.Visible = False
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvTaxas.DataSource = Session("TaskTable")
            dgvTaxas.DataBind()
            dgvTaxas.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
        DivGrid.Page.SetFocus(dgvTaxas)

    End Sub

End Class