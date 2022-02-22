Public Class Financeiro
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

        End If
        Con.Fechar()
    End Sub

    Private Sub lkSolicitacaoPagamento_Click(sender As Object, e As EventArgs) Handles lkSolicitacaoPagamento.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Response.Redirect("SolicitacaoPagamento.aspx?id=" & txtID.Text)
        End If
    End Sub

    Private Sub lkMontagemPagamento_Click(sender As Object, e As EventArgs) Handles lkMontagemPagamento.Click
        Response.Redirect("MontagemPagamento.aspx")
    End Sub
    Private Sub lkBaixaCancel_Pagar_Click(sender As Object, e As EventArgs) Handles lkBaixaCancel_Pagar.Click
        Response.Redirect("BaixasCancelamentos.aspx?t=p")
    End Sub
    Private Sub lkCalcularRecebimento_Click(sender As Object, e As EventArgs) Handles lkCalcularRecebimento.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Response.Redirect("CalcularRecebimento.aspx?id=" & txtID.Text)
        End If
    End Sub
    Private Sub lkEmissaoND_Click(sender As Object, e As EventArgs) Handles lkEmissaoND.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Response.Redirect("EmissaoND.aspx?id=" & txtID.Text)
        End If

    End Sub
    Private Sub lkBaixaCancel_Receber_Click(sender As Object, e As EventArgs) Handles lkBaixaCancel_Receber.Click
        Response.Redirect("BaixasCancelamentos.aspx?t=r")
    End Sub

    Private Sub lkFaturar_Click(sender As Object, e As EventArgs) Handles lkFaturar.Click
        Response.Redirect("FaturarRecebimento.aspx")
    End Sub

    Private Sub dgvFinanceiro_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFinanceiro.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        AtualizaGrid()
        If e.CommandName = "Selecionar" Then
            If txtlinha.Text <> "" Then
                dgvFinanceiro.Rows(txtlinha.Text).CssClass = "Normal"

            End If
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")


            For i As Integer = 0 To dgvFinanceiro.Rows.Count - 1
                dgvFinanceiro.Rows(txtlinha.Text).CssClass = "Normal"

            Next

            dgvFinanceiro.Rows(txtlinha.Text).CssClass = "selected1"


        End If
    End Sub

    Private Sub btnPesquisa_Click(sender As Object, e As EventArgs) Handles btnPesquisa.Click
        AtualizaGrid()
    End Sub


    Sub AtualizaGrid()
        divErro.Visible = False

        Dim filtro As String = ""

        If ddlFiltro.SelectedValue < 4 And txtPesquisa.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "É necessário preencher o campo de pesquisa!"
            dgvFinanceiro.Visible = False
        Else
            If ddlFiltro.SelectedValue = 1 Then 'Número do Master
                filtro &= " WHERE NR_BL_MASTER LIKE '%" & txtPesquisa.Text & "%'"

            ElseIf ddlFiltro.SelectedValue = 2 Then 'Número do processo

                filtro &= " WHERE NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"

            ElseIf ddlFiltro.SelectedValue = 3 Then 'Número do House 
                filtro &= " WHERE NR_BL LIKE '%" & txtPesquisa.Text & "%'"

            ElseIf ddlFiltro.SelectedValue = 4 Then 'Todos em aberto

                filtro &= " WHERE (QT_TAXAS_PAGAR_ABERTA > 0 Or QT_TAXAS_RECEBER_ABERTA > 0)"

            ElseIf ddlFiltro.SelectedValue = 5 Then 'Periodo de chegada
                filtro &= " WHERE CONVERT(DATE,DT_CHEGADA_MASTER,103) BETWEEN CONVERT(DATE,'" & txtDataInicioBusca.Text & "',103) AND CONVERT(DATE,'" & txtDataFimBusca.Text & "',103)"


            ElseIf ddlFiltro.SelectedValue = 6 Then 'Tipo Faturamento
                filtro &= " WHERE ID_TIPO_FATURAMENTO = " & ddlTipoFaturamento.SelectedValue

            End If


            If rdServico.SelectedValue = 0 Then
                filtro &= " AND ID_SERVICO NOT IN (1,2,4,5) " 'OUTROS

            ElseIf rdTransporte.SelectedValue = 2 And rdServico.SelectedValue = 1 Then
                filtro &= " AND ID_SERVICO = 2" 'AGENCIAMENTO DE IMPORTACAO AEREO

            ElseIf rdTransporte.SelectedValue = 1 And rdServico.SelectedValue = 1 Then
                filtro &= " AND ID_SERVICO = 1" 'AGENCIAMENTO DE IMPORTACAO MARITIMA


            ElseIf rdTransporte.SelectedValue = 1 And rdServico.SelectedValue = 2 Then
                filtro &= " AND ID_SERVICO = 4" 'AGENCIAMENTO DE EXPORTACAO MARITIMA

            ElseIf rdTransporte.SelectedValue = 2 And rdServico.SelectedValue = 2 Then
                filtro &= " AND ID_SERVICO = 5" 'AGENCIAMENTO DE EXPORTAÇÃO AEREO

            End If




            dsFinanceiro.SelectCommand = "SELECT * FROM [View_Financeiro]  " & filtro & " ORDER BY NR_PROCESSO"
            dgvFinanceiro.DataBind()
            dgvFinanceiro.Visible = True
        End If
    End Sub

    Private Sub rdTransporte_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdTransporte.SelectedIndexChanged
        AtualizaGrid()
    End Sub

    Private Sub rdServico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdServico.SelectedIndexChanged
        AtualizaGrid()
    End Sub

    Private Sub ddlFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltro.SelectedIndexChanged
        If ddlFiltro.SelectedValue = 5 Then
            divBusca.Attributes.CssStyle.Add("display", "none")
            divDatasBusca.Attributes.CssStyle.Add("display", "block")
            divddlBusca.Attributes.CssStyle.Add("display", "none")

        ElseIf ddlFiltro.SelectedValue = 6 Then
            divBusca.Attributes.CssStyle.Add("display", "none")
            divDatasBusca.Attributes.CssStyle.Add("display", "none")
            divddlBusca.Attributes.CssStyle.Add("display", "block")
        Else
            divBusca.Attributes.CssStyle.Add("display", "block")
            divDatasBusca.Attributes.CssStyle.Add("display", "none")
            divddlBusca.Attributes.CssStyle.Add("display", "none")
        End If
    End Sub
End Class