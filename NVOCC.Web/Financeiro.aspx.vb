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
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Response.Redirect("MontagemPagamento.aspx?id=" & txtID.Text)
        End If

    End Sub
    Private Sub lkBaixaCancel_Pagar_Click(sender As Object, e As EventArgs) Handles lkBaixaCancel_Pagar.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Response.Redirect("BaixaCancelamentos.aspx?id=" & txtID.Text)
        End If

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
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Response.Redirect("BaixaCancelamentos.aspx?id=" & txtID.Text)
        End If

    End Sub

    Private Sub lkFaturar_Click(sender As Object, e As EventArgs) Handles lkFaturar.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Response.Redirect("Faturamento.aspx?id=" & txtID.Text)
        End If

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
End Class