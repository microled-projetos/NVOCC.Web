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
        If Not IsPostBack Then
            Filtro()
        End If
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

    Sub Filtro()

        divSuccess.Visible = False
        divErro.Visible = False

        Dim FILTRO As String = ""

        If ddlFiltro.SelectedValue <> 0 Then

            If ddlFiltro.SelectedValue = 1 Then
                'NR_PROCESSO
                FILTRO = " AND NR_PROCESSO LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 2 Then
                'ITEM DESPESA
                FILTRO = " AND NM_ITEM_DESPESA LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 3 Then
                'PARCEIRO VINCULADO
                FILTRO = " AND NM_PARCEIRO_EMPRESA LIKE '%" & txtFiltro.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                'VALOR TAXA
                Dim VL_TAXA As String = txtFiltro.Text.Replace(".", "").Replace(",", ".")
                FILTRO = " AND VL_TAXA LIKE '%" & VL_TAXA & "%'"
            ElseIf ddlFiltro.SelectedValue = 5 Then
                'VALOR TAXA CALCULADA
                Dim VL_TAXA As String = txtFiltro.Text.Replace(".", "").Replace(",", ".")
                FILTRO = " AND VL_TAXA_CALCULADO LIKE '%" & VL_TAXA & "%'"
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
            ElseIf ddlFiltro.SelectedValue = 11 Then
                'NR_BL
                FILTRO = " AND NR_BL LIKE '%" & txtFiltro.Text & "%'"
            End If
        End If


        If txtDtInicial.Text <> "" Then
            FILTRO &= " AND CONVERT(DATE,DT_ABERTURA,103) >= CONVERT(DATE,'" & txtDtInicial.Text & "',103)"
        End If
        If txtDtFinal.Text <> "" Then
            FILTRO &= " AND CONVERT(DATE,DT_ABERTURA,103) <= CONVERT(DATE,'" & txtDtFinal.Text & "',103)"
        End If

        If ckInativo.Checked = True And ckAtivo.Checked = False Then
            FILTRO &= " AND ISNULL(FL_TAXA_INATIVA,0) = 1"
        ElseIf ckInativo.Checked = False And ckAtivo.Checked = True Then
            FILTRO &= " AND ISNULL(FL_TAXA_INATIVA,0) = 0"
        End If

        Dim sql As String = "SELECT
ID_BL_TAXA,
NR_PROCESSO,
NR_BL,
NM_PARCEIRO_EMPRESA,
NM_ITEM_DESPESA,
SIGLA_MOEDA,
NM_ORIGEM_PAGAMENTO,
VL_TAXA,
VL_TAXA_CALCULADO, 
VL_TAXA_BR,
LANCAMENTO,
TIPO_MOVIMENTO,
HISTORICO,
DT_ABERTURA 
FROM [dbo].[View_Inativacao_Taxas] WHERE ISNULL(ID_BL_TAXA,0) <> 0 " & FILTRO & " ORDER BY ID_BL_TAXA DESC"

        Session("ExportarCSV") = sql
        dsTaxas.SelectCommand = sql
        dgvTaxas.DataBind()
    End Sub

    Sub FiltroAvancado()

        divSuccess.Visible = False
        divErro.Visible = False

        Dim FILTRO As String = ""

        If txtFiltroProcesso.Text <> "" Then
            'NR_PROCESSO
            FILTRO &= " AND NR_PROCESSO LIKE '%" & txtFiltroProcesso.Text & "%'"
        End If

        If txtFiltroDespesa.Text <> "" Then
            'ITEM DESPESA
            FILTRO &= " AND NM_ITEM_DESPESA LIKE '%" & txtFiltroDespesa.Text & "%'"
        End If

        If txtFiltroParceiro.Text <> "" Then
            'PARCEIRO VINCULADO
            FILTRO &= " AND NM_PARCEIRO_EMPRESA LIKE '%" & txtFiltroParceiro.Text & "%'"
        End If

        If txtFiltroValor.Text <> "" Then
            'VALOR TAXA
            Dim VL_TAXA As String = txtFiltroValor.Text.Replace(".", "").Replace(",", ".")
            FILTRO &= " AND VL_TAXA LIKE '%" & VL_TAXA & "%'"
        End If

        If txtFiltroValorCalculada.Text <> "" Then
            'VALOR TAXA CALCULADA
            Dim VL_TAXA As String = txtFiltroValorCalculada.Text.Replace(".", "").Replace(",", ".")
            FILTRO &= " AND VL_TAXA_CALCULADO LIKE '%" & VL_TAXA & "%'"
        End If

        If txtFiltroMoeda.Text <> "" Then
            'MOEDA
            FILTRO &= " AND SIGLA_MOEDA LIKE '%" & txtFiltroMoeda.Text & "%'"
        End If

        If txtFiltroMovimento.Text <> "" Then
            'TIPO_MOVIMENTO
            FILTRO &= " AND TIPO_MOVIMENTO LIKE '%" & txtFiltroMovimento.Text & "%'"
        End If

        If txtFiltroOrigemPagamento.Text <> "" Then
            'NM_ORIGEM_PAGAMENTO
            FILTRO &= " AND NM_ORIGEM_PAGAMENTO LIKE '%" & txtFiltroOrigemPagamento.Text & "%'"
        End If

        If txtFiltroLancamento.Text <> "" Then
            'LANCAMENTO
            FILTRO &= " AND LANCAMENTO LIKE '%" & txtFiltroLancamento.Text & "%'"
        End If

        If txtBLFiltro.Text <> "" Then
            'NR_BL
            FILTRO &= " AND NR_BL LIKE '%" & txtBLFiltro.Text & "%'"
        End If

        If txtFiltroDataInicial.Text <> "" Then
            FILTRO &= " AND CONVERT(DATE,DT_ABERTURA,103) >= CONVERT(DATE,'" & txtFiltroDataInicial.Text & "',103)"
        End If

        If txtFiltroDataFinal.Text <> "" Then
            FILTRO &= " AND CONVERT(DATE,DT_ABERTURA,103) <= CONVERT(DATE,'" & txtFiltroDataFinal.Text & "',103)"
        End If

        Dim sql As String = "SELECT
ID_BL_TAXA,
NR_PROCESSO,
NR_BL,
NM_PARCEIRO_EMPRESA,
NM_ITEM_DESPESA,
SIGLA_MOEDA,
NM_ORIGEM_PAGAMENTO,
VL_TAXA,
VL_TAXA_CALCULADO, 
VL_TAXA_BR,
LANCAMENTO,
TIPO_MOVIMENTO,
HISTORICO,
DT_ABERTURA 
FROM [dbo].[View_Inativacao_Taxas] WHERE ISNULL(ID_BL_TAXA,0) <> 0 " & FILTRO & " ORDER BY ID_BL_TAXA DESC"

        Session("ExportarCSV") = sql
        dsTaxas.SelectCommand = sql
        dgvTaxas.DataBind()
    End Sub
    Private Sub lkExportarCSV_Click(sender As Object, e As EventArgs) Handles lkExportarCSV.Click
        If Session("ExportarCSV") Is Nothing Then
            Session("ExportarCSV") = dsTaxas.SelectCommand
        End If
        Classes.Excel.exportaExcel(Session("ExportarCSV"), "NVOCC", "InativacaoTaxas")
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

    Private Sub ddlMotivos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMotivos.SelectedIndexChanged
        If ddlMotivos.SelectedValue <> 0 Then

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(FL_PRECISA_DESCR,0)FL_PRECISA_DESCR FROM TB_MOTIVO_INATIVACAO WHERE ID_MOTIVO_INATIVACAO = " & ddlMotivos.SelectedValue)
            If ds.Tables(0).Rows(0).Item("FL_PRECISA_DESCR") = True Then
                divDescMotivo.Visible = True
            Else ds.Tables(0).Rows(0).Item("FL_PRECISA_DESCR") = False
                divDescMotivo.Visible = False
                txtMotivo.Text = ""
            End If
        End If
        mpeConfirmacao.Show()
    End Sub

    Private Sub btnConfirmaGravacao_Click(sender As Object, e As EventArgs) Handles btnConfirmaGravacao.Click
        divSuccess.Visible = False
        divErro.Visible = False

        If ddlMotivos.SelectedValue = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Necessário selecionar um motivo!"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(FL_PRECISA_DESCR,0)FL_PRECISA_DESCR FROM TB_MOTIVO_INATIVACAO WHERE ID_MOTIVO_INATIVACAO = " & ddlMotivos.SelectedValue)
            If ds.Tables(0).Rows(0).Item("FL_PRECISA_DESCR") = True And txtMotivo.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Necessário descrever o motivo!"
            Else

                Dim TaxaErro As String = ""

                For Each linha As GridViewRow In dgvTaxas.Rows
                    Dim ID_BL_TAXA As String = CType(linha.FindControl("lblTaxa"), Label).Text
                    Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                    If check.Checked Then
                        Dim I As New InativaAtivaTaxa
                        Dim Retorno As Boolean = I.InativarAtivar(ID_BL_TAXA, txtMotivo.Text, ddlMotivos.SelectedIndex, Session("ID_USUARIO"))
                        If Retorno = False Then
                            If TaxaErro = "" Then
                                TaxaErro = ID_BL_TAXA
                            Else
                                TaxaErro &= " , " & ID_BL_TAXA
                            End If
                        End If
                    End If
                Next

                If TaxaErro <> "" Then

                    ds = Con.ExecutarQuery("SELECT B.NR_PROCESSO +  CASE WHEN ISNULL(B.NR_BL,'0') <> '0' THEN ' - ' + B.NR_BL ELSE '' END  + ' - ' + C.NM_ITEM_DESPESA + ':' + CONVERT(VARCHAR,A.VL_TAXA)  DESCR FROM TB_BL_TAXA A
INNER JOIN TB_BL B ON A.ID_BL =B.ID_BL
INNER JOIN TB_ITEM_DESPESA C ON C.ID_ITEM_DESPESA = A.ID_ITEM_DESPESA WHERE A.ID_BL_TAXA ( " & TaxaErro & " ) ")

                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel concluir o processo para:"
                    For Each linha As DataRow In ds.Tables(0).Rows
                        lblmsgErro.Text &= "<br/>" & linha.Item("DESCR").ToString()
                    Next

                Else
                    'sucesso

                    Filtro()
                    divSuccess.Visible = True
                    lblmsgSuccess.Text = "Ação realizada com sucesso!"
                End If
            End If
        End If

        ddlMotivos.SelectedValue = 0
        txtMotivo.Text = ""

    End Sub

    Private Sub dgvTaxas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxas.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        If e.CommandName = "Historico" Then

            dsHistorico.SelectParameters("ID_BL_TAXA").DefaultValue = e.CommandArgument
            dgvHistorico.DataBind()
            mpeHistorico.Show()

        End If
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Filtro()
    End Sub

    Private Sub btnLimparCampos_Click(sender As Object, e As EventArgs) Handles btnLimparCampos.Click
        Response.Redirect("InativacaoTaxas.aspx")
    End Sub

    Sub LimpaFiltroAvancado()
        txtFiltroProcesso.Text = ""
        txtFiltroDespesa.Text = ""
        txtFiltroParceiro.Text = ""
        txtFiltroValor.Text = ""
        txtFiltroValorCalculada.Text = ""
        txtFiltroMoeda.Text = ""
        txtFiltroMovimento.Text = ""
        txtFiltroOrigemPagamento.Text = ""
        txtFiltroLancamento.Text = ""
        txtBLFiltro.Text = ""
        txtFiltroDataInicial.Text = ""
        txtFiltroDataFinal.Text = ""
    End Sub
    Private Sub btnConsultaAvancada_Click(sender As Object, e As EventArgs) Handles btnConsultaAvancada.Click
        FiltroAvancado()
        LimpaFiltroAvancado()
    End Sub
    Private Sub btnFecharFiltroAvancado_Click(sender As Object, e As EventArgs) Handles btnFecharFiltroAvancado.Click
        LimpaFiltroAvancado()
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        Filtro()
    End Sub

    Private Sub ckInativo_CheckedChanged(sender As Object, e As EventArgs) Handles ckInativo.CheckedChanged
        Filtro()
    End Sub

    Private Sub ckAtivo_CheckedChanged(sender As Object, e As EventArgs) Handles ckAtivo.CheckedChanged
        Filtro()
    End Sub
End Class