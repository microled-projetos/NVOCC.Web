Public Class InativacaoTaxas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2066 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub

    Protected Sub btnMarcarTudo_Click(sender As Object, e As EventArgs)
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvTaxas.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            If txtCont.Text = 0 Then
                ckbSelecionar.Checked = True
            Else
                ckbSelecionar.Checked = False
            End If
        Next
        If txtCont.Text = 0 Then
            txtCont.Text = 1
        Else
            txtCont.Text = 0
        End If
    End Sub

    Sub FILTRO()
        Dim FILTRO As String = ""

        If ddlFiltro.SelectedValue <> 0 And txtFiltro.Text = "" Then

        ElseIf ddlFiltro.SelectedValue <> 0 And txtFiltro.Text <> "" Then
            If ddlFiltro.SelectedValue = 1 Then
                'NR_PROCESSO
                FILTRO = " AND NR_PROCESSO LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 2 Then
                'ITEM DESPESA
                FILTRO = " AND NM_ITEM_DESPESA LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 3 Then
                'PARCEIRO VINCULADO
                FILTRO = " AND NM_ORIGEM_PAGAMENTO LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                'VALOR TAXA
                FILTRO = " AND VL_TAXA LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 5 Then
                'VALOR TAXA CALCULADA
                FILTRO = " AND VL_TAXA_CALCULADO LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 6 Then
                'MOEDA
                FILTRO = " AND SIGLA_MOEDA LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 7 Then
                'TIPO_MOVIMENTO
                FILTRO = " AND TIPO_MOVIMENTO LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 8 Then
                'NM_ORIGEM_PAGAMENTO
                FILTRO = " AND NM_ORIGEM_PAGAMENTO LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 9 Then
                'LANCAMENTO
                FILTRO = " AND LANCAMENTO LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 10 Then
                'HISTÓRICO
                FILTRO = " AND HISTORICO LIKE '%" & txtFiltro.Text & "%'"
            End If
        End If


        If txtDtInicial.Text <> "" Then
            FILTRO = " AND CONVERT(DATETIME,DT_ABERTURA,103) >= CONVERT(DATETIME,'" & txtFiltro.Text & "',103)"
        End If
        If txtDtFinal.Text <> "" Then
            FILTRO = " AND CONVERT(DATETIME,DT_ABERTURA,103) <= CONVERT(DATETIME,'" & txtFiltro.Text & "',103)"
        End If

        Dim sql As String = "SELECT
ID_BL_TAXA,
NR_PROCESSO,
NM_PARCEIRO_EMPRESA,
NM_ITEM_DESPESA,
SIGLA_MOEDA,
NM_ORIGEM_PAGAMENTO,
VL_TAXA,
VL_TAXA_CALCULADO, 
VL_TAXA_BR,
LANCAMENTO,
TIPO_MOVIMENTO FROM [dbo].[View_Inativacao_Taxas] WHERE CD_PR='P' " & FILTRO & " ORDER BY ID_BL_TAXA DESC"


        dsTaxas.SelectCommand = sql
        dgvTaxas.DataBind()
    End Sub
    Private Sub lkExportarCSV_Click(sender As Object, e As EventArgs) Handles lkExportarCSV.Click
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim FILTRO As String = ""
        Dim X As String = dsTaxas.SelectCommand
        Dim sql As String = "SELECT 
ID_BL_TAXA,
NR_PROCESSO,
NM_PARCEIRO_EMPRESA,
NM_ITEM_DESPESA,
SIGLA_MOEDA,
NM_ORIGEM_PAGAMENTO,
VL_TAXA,
VL_TAXA_CALCULADO, 
VL_TAXA_BR,
LANCAMENTO,
TIPO_MOVIMENTO,
HISTORICO FROM [dbo].[View_Inativacao_Taxas] WHERE ISNULL(ID_BL_TAXA,0) <> 0 " & FILTRO & " ORDER BY ID_BL_TAXA DESC"


        Classes.Excel.exportaExcel(sql, "NVOCC", "INATIVACAO_TAXAS")
    End Sub

    Private Sub dgvTaxas_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvTaxas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Status As Label = CType(e.Row.FindControl("lblTemHistorico"), Label)

            Dim ImageButton As ImageButton = CType(e.Row.FindControl("ImageButton1"), ImageButton)



            If Status.Text = "0" Then

                ImageButton.Visible = False

            End If



        End If
    End Sub
End Class