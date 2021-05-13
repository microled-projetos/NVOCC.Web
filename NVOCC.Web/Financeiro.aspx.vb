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
        Response.Redirect("MontagemPagamento.aspx?id=" & txtID.Text)
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
        Response.Redirect("EmissaoND.aspx")
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

        Dim filtro As String = ""

        Dim sql As String = ""

        If ddlFiltro.SelectedValue = 1 Then

            filtro &= "AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 2 Then
            filtro &= "AND NR_BL_MASTER LIKE '%" & txtPesquisa.Text & "%'"


        ElseIf ddlFiltro.SelectedValue = 3 Then
            filtro &= "AND NM_PARCEIRO_CLIENTE LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 4 Then
            filtro &= "AND REFERENCIA_CLIENTE LIKE '%" & txtPesquisa.Text & "%'"

        End If

        If ckStatus.Items.FindByValue(1).Selected Then
            filtro &= "AND (TOTAL_A_PAGAR_ABERTAS > 0 Or TOTAL_A_RECEBER_ABERTAS > 0)"

        End If
        If ckStatus.Items.FindByValue(2).Selected Then
            filtro &= "AND (TOTAL_A_PAGAR_QUITADAS > 0 Or TOTAL_A_RECEBER_QUITADAS > 0)"

        End If
        If ckStatus.Items.FindByValue(3).Selected Then
            filtro &= "AND (TOTAL_A_PAGAR_CANCELADAS > 0 Or TOTAL_A_RECEBER_CANCELADAS > 0)"

        End If
    End Sub
End Class