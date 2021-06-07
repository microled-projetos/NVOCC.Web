Public Class FaturarRecebimento
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

    Private Sub dgvContasReceber_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvContasReceber.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Selecionar" Then
            Dim ID As String = e.CommandArgument
            ds = Con.ExecutarQuery("SELECT COUNT(ID_FATURAMENTO)QTD FROM [TB_FATURAMENTO] WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER = " & ID)
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

                Con.ExecutarQuery("UPDATE TB_CONTA_PAGAR_RECEBER SET DT_ENVIO_FATURAMENTO = GETDATE() WHERE ID_CONTA_PAGAR_RECEBER = " & ID)

                Con.ExecutarQuery("INSERT INTO TB_FATURAMENTO (ID_CONTA_PAGAR_RECEBER,VL_NOTA) SELECT ID_CONTA_PAGAR_RECEBER, (SELECT SUM(ISNULL(VL_LIQUIDO,0)) FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER ) VL_NOTA FROM TB_CONTA_PAGAR_RECEBER A WHERE ID_CONTA_PAGAR_RECEBER = " & ID)

                divSuccess.Visible = True
                lblmsgSuccess.Text = "Faturamento realizado com sucesso"
            Else
                divErro.Visible = True
                lblmsgErro.Text = "PAGAMENTO JÁ ENVIADO AO FATURAMENTO"
            End If

            dgvContasReceber.DataBind()

        End If

    End Sub

    Private Sub btnPesquisa_Click(sender As Object, e As EventArgs) Handles btnPesquisa.Click
        Dim filtro As String = ""

        If ddlFiltro.SelectedValue = 1 Then

            filtro &= "AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 2 Then
            filtro &= "AND NR_BL_MASTER LIKE '%" & txtPesquisa.Text & "%'"


        ElseIf ddlFiltro.SelectedValue = 3 Then
            filtro &= "AND PARCEIRO_EMPRESA LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 4 Then
            filtro &= "AND REFERENCIA_CLIENTE LIKE '%" & txtPesquisa.Text & "%'"

        End If

        dsContasReceber.SelectCommand = "SELECT * FROM [dbo].[View_Contas_Receber] WHERE (CD_PR = 'R')" & filtro
        dgvContasReceber.DataBind()

        ddlFiltro.SelectedValue = 0
        txtPesquisa.Text = ""
    End Sub
End Class