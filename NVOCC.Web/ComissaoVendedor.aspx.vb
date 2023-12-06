
Imports System.Windows.Input

Public Class ComissaoVendedor
    Inherits System.Web.UI.Page
    Dim filtro As String = ""

    'GERAL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()


        txtIDComissoesProspeccao.Enabled = False

    End Sub
    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        CarregaGridComissaoVendedor()
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

    'RELATORIOS
    Private Sub lkCSV_Click(sender As Object, e As EventArgs) Handles lkCSV.Click
        txtID.Text = ""
        txtlinha.Text = ""
        divErro.Visible = False

        If txtCompetencia.Text = "" Then
            lblmsgErro.Text = "É necessario informar a competência."
            divErro.Visible = True
        Else
            If ddlFiltro.SelectedValue = 1 Then
                filtro = " AND PARCEIRO_VENDEDOR LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 2 Then
                filtro = " AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 3 Then
                filtro = " AND FL_CALC_EQUIPE = 0 AND FL_CALC_SUB = 0"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                filtro = " AND FL_CALC_SUB = 1"
            ElseIf ddlFiltro.SelectedValue = 5 Then
                filtro = " AND FL_CALC_EQUIPE = 1"
            ElseIf ddlFiltro.SelectedValue = 0 Then
                filtro = ""
            End If

            Dim SQL As String = "SELECT COMPETENCIA,NR_PROCESSO,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,PARCEIRO_VENDEDOR,ANALISTA_COTACAO,USUARIO_LIDER,PARCEIRO_CLIENTE,TP_SERVICO,TP_VIA,TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_PERCENTUAL,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,DS_OBSERVACAO FROM [dbo].[View_Comissao_Vendedor] WHERE COMPETENCIA ='" & txtCompetencia.Text & "' " & filtro & " union  SELECT NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL union SELECT NULL,NULL,NULL,NULL,PARCEIRO_VENDEDOR,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,sum (VL_COMISSAO_TOTAL)VL_COMISSAO_TOTAL,NULL,NULL FROM [dbo].[View_Comissao_Vendedor] WHERE COMPETENCIA ='" & txtCompetencia.Text & "' " & filtro & " group BY PARCEIRO_VENDEDOR UNION  SELECT NULL,NULL,NULL,NULL,ANALISTA_COTACAO,NULL,NULL,NULL,NULL,NULL,NULL,QT_BL,QT_CNTR,NULL,NULL, VL_COMISSAO_TOTAL as 'VALOR', NULL,NULL FROM [dbo].[FN_EQUIPES]('" & txtCompetencia.Text & "')  ORDER BY COMPETENCIA DESC, PARCEIRO_VENDEDOR,NR_PROCESSO ASC"

            Classes.Excel.exportaExcel(SQL, "Comissao")
        End If

    End Sub
    Private Sub lkRelPorVendedor_Click(sender As Object, e As EventArgs) Handles lkRelPorVendedor.Click
        divErro.Visible = False
        divSuccess.Visible = False

        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "RelPorVendedor()", True)

        End If

    End Sub
    Private Sub lkRelEquipe_Click(sender As Object, e As EventArgs) Handles lkRelEquipe.Click
        divErro.Visible = False

        If txtCompetencia.Text = "" Then
            lblmsgErro.Text = "É necessario informar a competência."
            divErro.Visible = True
        Else

            Dim SQL As String = "SELECT COMPETENCIA,USUARIO, VL_COMISSAO as 'VALOR',LIDER,NM_EQUIPE AS EQUIPE FROM [dbo].[View_Equipes] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' ORDER BY NM_EQUIPE,LIDER,USUARIO"

            Classes.Excel.exportaExcel(SQL, "Equipes")
        End If

    End Sub

    'VERIFICACOES E CC DO PROCESSO
    'Sub VerificaCCPRocesso()
    '    Dim Con As New Conexao_sql
    '    Con.Conectar()

    '    'Verifica se a competencia já existe
    '    Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CVEND' AND DT_COMPETENCIA = '" & txtCompetencia.Text & "'")
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        divInfoCCProcesso.Visible = True
    '        lblInfoCCProcesso.Text = "COMPETENCIA JÁ EXPORTADA!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
    '        lblContasReceber.Text = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
    '    Else
    '        lblContasReceber.Text = 0
    '        divInfoCCProcesso.Visible = False
    '    End If
    '    mpeCCProcesso.Show()
    'End Sub

    'Sub VerificaCompetencia()
    '    Dim Con As New Conexao_sql
    '    Con.Conectar()

    '    'Verifica se a competencia já existe
    '    Dim ds As DataSet = Con.ExecutarQuery("Select ID_CABECALHO_COMISSAO_VENDEDOR,DT_EXPORTACAO FROM View_Comissao_Vendedor WHERE COMPETENCIA = '" & txtNovaCompetencia.Text & "'")
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        divAtencaoGerarComissao.Visible = True
    '        lblAtencaoGerarComissao.Text = "COMPETENCIA JÁ EXISTE!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
    '        lblCompetenciaSobrepor.Text = ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_VENDEDOR")
    '        If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EXPORTACAO")) Then
    '            Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CVEND' AND DT_COMPETENCIA = '" & txtNovaCompetencia.Text & "'")
    '            If dsAuxiliar.Tables(0).Rows.Count > 0 Then
    '                lblContasReceber.Text = dsAuxiliar.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
    '            Else
    '                lblContasReceber.Text = 0
    '            End If
    '        End If
    '    Else
    '        lblCompetenciaSobrepor.Text = 0
    '        lblContasReceber.Text = 0
    '        divAtencaoGerarComissao.Visible = False
    '    End If


    '    'Verifica se a competencia anterior foi exportada
    '    Dim Competencia_Nova As String = "01/" & txtNovaCompetencia.Text
    '    Dim Variavel_Auxiliar As Date = Competencia_Nova
    '    Variavel_Auxiliar = Variavel_Auxiliar.AddMonths(-1)
    '    Dim Competencia_Anterior As String = Variavel_Auxiliar.ToString()
    '    Competencia_Anterior = Competencia_Anterior.Substring(3, 7)
    '    ds = Con.ExecutarQuery("SELECT ID_CABECALHO_COMISSAO_VENDEDOR FROM View_Comissao_Vendedor WHERE COMPETENCIA = '" & Competencia_Anterior.ToString() & "' AND DT_EXPORTACAO IS NULL")
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        divInfoComissaoVendas.Visible = True
    '        lblInfoComissaoVendas.Text = "Competência imediatamente anterior não exportada para a conta corrente do processo."
    '    Else
    '        divInfoComissaoVendas.Visible = False
    '    End If

    '    mpeGerarComissoes.Show()

    'End Sub

    'TABELA DE VENDA
    Private Sub btnSalvarTabelaVenda_Click(sender As Object, e As EventArgs) Handles btnSalvarTabelaVenda.Click
        divComissoesVendaSucesso.Visible = False
        divComissoesVendaErro.Visible = False

        Dim ValorComissoesVenda As String = txtValorComissoesVenda.Text.Replace(".", "")
        ValorComissoesVenda = ValorComissoesVenda.Replace(",", ".")

        Dim InicioComissoesVenda As String = txtProfitInicialComissoesVenda.Text.Replace(".", "")
        InicioComissoesVenda = InicioComissoesVenda.Replace(",", ".")


        Dim FinalComissoesVenda As String = txtProfitFinalComissoesVenda.Text.Replace(".", "")
        FinalComissoesVenda = FinalComissoesVenda.Replace(",", ".")

        Dim CalculadoComissoesVenda As String = txtCalculadoComissoesVenda.Text.Replace(".", "")
        CalculadoComissoesVenda = CalculadoComissoesVenda.Replace(",", ".")


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim sql As String = ""

        If txtIDComissoesVenda.Text = "" Then

            sql = " INSERT INTO TB_VENDEDOR_TAXA_COMISSAO  "
            sql = sql & " (DT_VALIDADE_INICIAL,ID_TIPO_ESTUFAGEM,ID_VIATRANSPORTE,ID_TIPO_CALCULO,ID_BASE_CALCULO_TAXA,VL_TAXA,VL_PROFIT_INICIO,VL_PROFIT_FIM,VL_COMISSAO) "
            sql = sql & " VALUES ( CONVERT(DATE,'" & txtValidadaInicialComissoesVenda.Text & "',103) ," & ddlEstufagemComissoesVenda.SelectedValue & " , "
            sql = sql & ddlViaComissoesVenda.SelectedValue & " , " & ddlTipoCalculoComissoesVenda.SelectedValue & " , " & ddlBaseCalculoComissoesVenda.SelectedValue & ", "
            sql = sql & ValorComissoesVenda & "," & InicioComissoesVenda & "," & FinalComissoesVenda & " , " & CalculadoComissoesVenda & ") "
            Con.ExecutarQuery(sql)
            divComissoesVendaSucesso.Visible = True
            lblComissoesVendaSucesso.Text = "Taxa inserida com sucesso"
            dgvTabelaVenda.DataBind()
            mpeTabelas.Show()
            LimparTabelaVenda()
        Else
            sql = " UPDATE TB_VENDEDOR_TAXA_COMISSAO Set DT_VALIDADE_INICIAL = CONVERT(Date,'" & txtValidadaInicialComissoesVenda.Text & "',103) , "
            sql = sql & " ID_TIPO_ESTUFAGEM = " & ddlEstufagemComissoesVenda.SelectedValue & " ,ID_VIATRANSPORTE = " & ddlViaComissoesVenda.SelectedValue & " , "
            sql = sql & " ID_TIPO_CALCULO = " & ddlTipoCalculoComissoesVenda.SelectedValue & " ,ID_BASE_CALCULO_TAXA = " & ddlBaseCalculoComissoesVenda.SelectedValue & ", "
            sql = sql & " VL_TAXA = " & ValorComissoesVenda & " ,VL_PROFIT_INICIO = " & InicioComissoesVenda & ", "
            sql = sql & " VL_PROFIT_FIM = " & FinalComissoesVenda & " ,VL_COMISSAO = " & CalculadoComissoesVenda
            sql = sql & " WHERE ID_VENDEDOR_TAXA_COMISSAO = " & txtIDComissoesVenda.Text
            Con.ExecutarQuery(sql)
            divComissoesVendaSucesso.Visible = True
            lblComissoesVendaSucesso.Text = "Taxa alterada com sucesso"
            dgvTabelaVenda.DataBind()
            mpeTabelas.Show()

        End If

    End Sub
    Private Sub dgvTabelaVenda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTabelaVenda.RowCommand
        divComissoesVendaSucesso.Visible = False
        divComissoesVendaErro.Visible = False
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblComissoesVendaErro.Text = "Usuário não tem permissão para realizar exclusões"
                divComissoesVendaErro.Visible = True
            Else
                Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_TAXA_COMISSAO] WHERE ID_VENDEDOR_TAXA_COMISSAO =" & ID)
                dgvTabelaVenda.DataBind()
                lblComissoesVendaSucesso.Text = "Taxa excluída com sucesso"
                divComissoesVendaSucesso.Visible = True
                mpeTabelas.Show()
            End If

        ElseIf e.CommandName = "Editar" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_VENDEDOR_TAXA_COMISSAO,convert(date,DT_VALIDADE_INICIAL,103)DT_VALIDADE_INICIAL,ID_TIPO_ESTUFAGEM,ID_VIATRANSPORTE,ID_TIPO_CALCULO,ID_BASE_CALCULO_TAXA,VL_TAXA,VL_PROFIT_INICIO,VL_PROFIT_FIM,VL_COMISSAO FROM TB_VENDEDOR_TAXA_COMISSAO WHERE ID_VENDEDOR_TAXA_COMISSAO = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VENDEDOR_TAXA_COMISSAO")) Then
                    txtIDComissoesVenda.Text = ds.Tables(0).Rows(0).Item("ID_VENDEDOR_TAXA_COMISSAO").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")) Then
                    txtValidadaInicialComissoesVenda.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                    ddlEstufagemComissoesVenda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")) Then
                    ddlViaComissoesVenda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CALCULO")) Then
                    ddlTipoCalculoComissoesVenda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CALCULO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculoComissoesVenda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtValorComissoesVenda.Text = ds.Tables(0).Rows(0).Item("VL_TAXA").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PROFIT_INICIO")) Then
                    txtProfitInicialComissoesVenda.Text = ds.Tables(0).Rows(0).Item("VL_PROFIT_INICIO").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PROFIT_FIM")) Then
                    txtProfitFinalComissoesVenda.Text = ds.Tables(0).Rows(0).Item("VL_PROFIT_FIM").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMISSAO")) Then
                    txtCalculadoComissoesVenda.Text = ds.Tables(0).Rows(0).Item("VL_COMISSAO").ToString()
                End If

            End If

            mpeTabelas.Show()
        End If
        Con.Fechar()
    End Sub
    Public Sub LimparTabelaVenda()
        txtIDComissoesVenda.Text = ""
        txtValidadaInicialComissoesVenda.Text = ""
        ddlEstufagemComissoesVenda.SelectedValue = 0
        ddlViaComissoesVenda.SelectedValue = 0
        ddlTipoCalculoComissoesVenda.SelectedValue = 0
        ddlBaseCalculoComissoesVenda.SelectedValue = 0
        txtValorComissoesVenda.Text = 0
        txtProfitInicialComissoesVenda.Text = ""
        txtProfitFinalComissoesVenda.Text = ""
        txtCalculadoComissoesVenda.Text = ""
    End Sub
    Private Sub btnFecharTabelaVenda_Click(sender As Object, e As EventArgs) Handles btnFecharTabelaVenda.Click
        LimparTabelaVenda()
        mpeTabelas.Hide()
    End Sub


    'COMISSOES DE VENDA
    Sub CarregaGridComissaoVendedor()
        divSuccess.Visible = False
        divErro.Visible = False

        txtID.Text = ""
        txtlinha.Text = ""
        divErro.Visible = False

        If txtCompetencia.Text = "" Then
            lblmsgErro.Text = "É necessario informar a competência."
            divErro.Visible = True
        Else

            If ddlFiltro.SelectedValue = 1 Then
                filtro = " AND PARCEIRO_VENDEDOR LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 2 Then
                filtro = " AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 3 Then
                filtro = " AND FL_CALC_EQUIPE = 0 AND FL_CALC_SUB = 0"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                filtro = " AND FL_CALC_SUB = 1"
            ElseIf ddlFiltro.SelectedValue = 5 Then
                filtro = " AND FL_CALC_EQUIPE = 1"
            ElseIf ddlFiltro.SelectedValue = 0 Then
                filtro = ""
            End If

            dsComissaoVendedor.SelectCommand = "SELECT * FROM [dbo].[View_Comissao_Vendedor] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' " & filtro & " ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO"
            dgvComissaoVendedor.DataBind()

        End If
    End Sub

    Sub SubVendedor(cabecalho As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT A.ID_PARCEIRO_SUB_VENDEDOR,A.ID_PARCEIRO_VENDEDOR,A.VL_TAXA_FIXA,A.VL_PERCENTUAL,A.ID_BASE_CALCULO
FROM TB_SUB_VENDEDOR A
INNER JOIN (
SELECT ID_PARCEIRO_VENDEDOR, ID_PARCEIRO_SUB_VENDEDOR, MAX(DT_VALIDADE_INICIAL) AS DT_VALIDADE_INICIAL
FROM TB_SUB_VENDEDOR
WHERE CONVERT(DATE,DT_VALIDADE_INICIAL,103) <= CONVERT(DATE,'" & txtDtInicioRelComissaoVendas.Text & "',103)
GROUP BY ID_PARCEIRO_VENDEDOR, ID_PARCEIRO_SUB_VENDEDOR) B
ON A.ID_PARCEIRO_VENDEDOR = B.ID_PARCEIRO_VENDEDOR 
AND A.ID_PARCEIRO_SUB_VENDEDOR = B.ID_PARCEIRO_SUB_VENDEDOR
AND A.DT_VALIDADE_INICIAL = B.DT_VALIDADE_INICIAL")
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows

                If Not IsDBNull(linha.Item("VL_TAXA_FIXA")) Then

                    Dim TaxaFixa As String = linha.Item("VL_TAXA_FIXA").ToString
                    Dim Base_Calculo As String = linha.Item("ID_BASE_CALCULO").ToString

                    TaxaFixa = TaxaFixa.Replace(".", "")
                    TaxaFixa = TaxaFixa.Replace(",", ".")
                    Dim dsContador As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_PARCEIRO_VENDEDOR)QTD FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR =" & cabecalho & " AND ID_PARCEIRO_VENDEDOR = " & linha.Item("ID_PARCEIRO_VENDEDOR").ToString)
                    If dsContador.Tables(0).Rows(0).Item("QTD") > 0 Then
                        '' comissoes de sub com vendedor pai cadastrados
                        If Base_Calculo = 34 Then
                            'POR CNTR
                            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB,ID_ANALISTA_COTACAO ) 
SELECT ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR," & TaxaFixa & ",QT_CNTR*" & TaxaFixa & ",DT_LIQUIDACAO,1,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR =  " & linha.Item("ID_PARCEIRO_VENDEDOR") & " AND ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho)

                        ElseIf Base_Calculo = 32 Then
                            'POR HBL - Muplica por um
                            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB,ID_ANALISTA_COTACAO ) 
SELECT ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR," & TaxaFixa & "," & TaxaFixa & ",DT_LIQUIDACAO,1,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR =  " & linha.Item("ID_PARCEIRO_VENDEDOR") & " AND ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho)

                        End If

                    Else

                        '' comissoes de sub sem vendedor pai cadastrado
                        If Base_Calculo = 34 Then
                            'POR CNTR
                            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB,ID_ANALISTA_COTACAO )
SELECT " & cabecalho & ",NR_NOTA_FISCAL, DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR, " & TaxaFixa & ",
                  Case 
                WHEN ID_TIPO_ESTUFAGEM  = 1 THEN
               " & TaxaFixa & " * QT_CNTR

                WHEN ID_TIPO_ESTUFAGEM  = 2 THEN
                " & TaxaFixa & " * 1
                End COMISSAO_TOTAL,

				DT_LIQUIDACAO,1,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO

FROM FN_VENDEDOR('" & txtDtInicioRelComissaoVendas.Text & "','" & txtDtTerminoRelComissaoVendas.Text & "')
WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR =1 AND FL_VENDEDOR_DIRETO = 0 AND FL_ATIVO = 1)  AND
ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_SUB_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR Not IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & "))")

                        ElseIf Base_Calculo = 32 Then
                            'POR HBL - Muplica por um

                            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB,ID_ANALISTA_COTACAO )
SELECT " & cabecalho & ",NR_NOTA_FISCAL, DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR, " & TaxaFixa & ",  " & TaxaFixa & ", DT_LIQUIDACAO,1, isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO 
FROM FN_VENDEDOR('" & txtDtInicioRelComissaoVendas.Text & "','" & txtDtTerminoRelComissaoVendas.Text & "')
WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR =1 AND FL_VENDEDOR_DIRETO = 0 AND FL_ATIVO = 1)  AND
ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_SUB_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR Not IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & "))")

                        End If
                    End If


                ElseIf Not IsDBNull(linha.Item("VL_PERCENTUAL")) Then

                    Dim percentual As String = linha.Item("VL_PERCENTUAL").ToString
                    percentual = percentual.Replace(".", "")
                    percentual = percentual.Replace(",", ".")


                    Dim dsContador As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_PARCEIRO_VENDEDOR)QTD FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR =" & cabecalho & " AND ID_PARCEIRO_VENDEDOR = " & linha.Item("ID_PARCEIRO_VENDEDOR").ToString)
                    If dsContador.Tables(0).Rows(0).Item("QTD") > 0 Then
                        '' comissoes de sub com vendedor pai cadastrado
                        Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_PERCENTUAL,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB,ID_ANALISTA_COTACAO ) 
SELECT ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR," & percentual & "," & percentual & ",
(SELECT (SELECT sum(isnull(VL_LIQUIDO,0))VL_LIQUIDO FROM VW_PROCESSO_RECEBIDO B
inner join TB_CONTA_PAGAR_RECEBER_ITENS C on C.ID_CONTA_PAGAR_RECEBER =B.ID_CONTA_PAGAR_RECEBER
WHERE B.ID_BL = A.ID_BL)/100) * " & percentual & ",DT_LIQUIDACAO,1,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO FROM TB_DETALHE_COMISSAO_VENDEDOR  A WHERE ID_PARCEIRO_VENDEDOR =  " & linha.Item("ID_PARCEIRO_VENDEDOR") & " AND ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho)

                    Else

                        '' comissoes de sub sem vendedor pai cadastrado
                        Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB,ID_ANALISTA_COTACAO ) 
SELECT " & cabecalho & ",NR_NOTA_FISCAL, DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR, " & percentual & ", 
(SELECT (SELECT sum(isnull(VL_LIQUIDO,0))VL_LIQUIDO FROM VW_PROCESSO_RECEBIDO B
inner join TB_CONTA_PAGAR_RECEBER_ITENS C on C.ID_CONTA_PAGAR_RECEBER =B.ID_CONTA_PAGAR_RECEBER
WHERE B.ID_BL = A.ID_BL)/100) * " & percentual & " ,

				DT_LIQUIDACAO,1,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO 

FROM FN_VENDEDOR('" & txtDtInicioRelComissaoVendas.Text & "','" & txtDtTerminoRelComissaoVendas.Text & "') A
WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR =1 AND FL_VENDEDOR_DIRETO = 0 AND FL_ATIVO = 1)  AND
ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_SUB_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR Not IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & "))")

                    End If


                End If

            Next

        End If

    End Sub

    Sub SubEquipe(cabecalho As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        'gravar premiacao
        ds = Con.ExecutarQuery("SELECT ID_BL,(SELECT ID_PARCEIRO_EQUIPE_INSIDE FROM TB_PARAMETROS)ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,NR_PROCESSO,ID_SERVICO,ID_TIPO_ESTUFAGEM,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO,ISNULL(QT_BL,0)QT_BL,ISNULL(QT_CNTR,0)QT_CNTR FROM FN_VENDEDOR('" & txtDtInicioRelComissaoVendas.Text & "','" & txtDtTerminoRelComissaoVendas.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO =1 AND FL_ATIVO = 1) AND ID_ANALISTA_COTACAO IN (SELECT ID_USUARIO_MEMBRO_EQUIPE FROM TB_EQUIPE_MEMBROS)")

        If ds.Tables(0).Rows.Count > 0 Then

            Dim TaxaEquipe_final As String = "0"

            For Each linha As DataRow In ds.Tables(0).Rows

                Dim dsTaxa As DataSet = Con.ExecutarQuery("select B.TAXA_LIDER,B.TAXA_EQUIPE,A.ID_USUARIO_LIDER from TB_EQUIPE_MEMBROS A 
INNER JOIN TB_EQUIPE_LIDER B ON A.ID_USUARIO_LIDER = B.ID_USUARIO_LIDER
WHERE A.ID_USUARIO_MEMBRO_EQUIPE = " & linha.Item("ID_ANALISTA_COTACAO"))

                If dsTaxa.Tables(0).Rows.Count > 0 Then
                    TaxaEquipe_final = dsTaxa.Tables(0).Rows(0).Item("TAXA_EQUIPE")
                    TaxaEquipe_final = TaxaEquipe_final.ToString.Replace(".", "")
                    TaxaEquipe_final = TaxaEquipe_final.ToString.Replace(",", ".")
                End If

                Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,FL_CALC_EQUIPE,ID_ANALISTA_COTACAO,ID_USUARIO_LIDER,QT_BL,QT_CNTR) VALUES(" & cabecalho & ",
'" & linha.Item("NR_PROCESSO") & "',
" & linha.Item("ID_BL") & ",
" & linha.Item("ID_SERVICO") & ",
" & linha.Item("ID_PARCEIRO_CLIENTE") & ",
" & linha.Item("ID_PARCEIRO_VENDEDOR") & ",
" & linha.Item("ID_TIPO_ESTUFAGEM") & ",
" & TaxaEquipe_final & ",
" & TaxaEquipe_final & ",
1,
" & linha.Item("ID_ANALISTA_COTACAO") & ",
" & dsTaxa.Tables(0).Rows(0).Item("ID_USUARIO_LIDER") & "," & linha.Item("QT_BL") & "," & linha.Item("QT_CNTR") & ")")

            Next

            TabelaEquipe(cabecalho)
        End If

    End Sub

    Private Sub btnRelGerarCompetenciaComissaoVendas_Click(sender As Object, e As EventArgs) Handles btnRelGerarCompetenciaComissaoVendas.Click
        divSuccess.Visible = False
        divErro.Visible = False
        lblSuccessComissaoVendas.Visible = False
        divErroComissaoVendas.Visible = False
        divInfoComissaoVendas.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        If lblCompetenciaSobrepor.Text = "" Then
            lblCompetenciaSobrepor.Text = 0
        End If

        If lblContasReceber.Text = "" Then
            lblContasReceber.Text = 0
        End If

        If txtDtInicioRelComissaoVendas.Text = "" Or txtDtTerminoRelComissaoVendas.Text = "" Then
            lblErroComissaoVendas.Text = "Preencha os campos obrigatórios."
            divErroComissaoVendas.Visible = True

        Else
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroComissaoVendas.Text = "Usuário não tem permissão!"
                divErroComissaoVendas.Visible = True
            Else
                Dim dsQtd As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM FN_VENDEDOR('" & txtDtInicioRelComissaoVendas.Text & "','" & txtDtTerminoRelComissaoVendas.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND FL_VENDEDOR_DIRETO =1 ")
                If dsQtd.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroComissaoVendas.Text = "Não há processos em aberto para comissão nesse período!"
                    divErroComissaoVendas.Visible = True
                Else

                    Dim NOVA_COMPETECIA As String = ""
                    NOVA_COMPETECIA = NOVA_COMPETECIA.Replace("/", "")
                    Dim dsInsert As DataSet
                    Dim cabecalho As String

                    Con.ExecutarQuery("DELETE FROM TB_DETALHE_COMISSAO_VENDEDOR  WHERE ID_CABECALHO_COMISSAO_VENDEDOR 
IN (SELECT ID_CABECALHO_COMISSAO_VENDEDOR FROM TB_CABECALHO_COMISSAO_VENDEDOR WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "')")

                    Con.ExecutarQuery("DELETE FROM TB_CABECALHO_COMISSAO_VENDEDOR WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "'")


                    If lblContasReceber.Text <> 0 Then
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)

                        divInfoComissaoVendas.Visible = True
                        lblInfoComissaoVendas.Text = "Necessário exportar competência para a conta corrente do processo!"
                    End If

                    dsInsert = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_VENDEDOR (DT_COMPETENCIA,ID_USUARIO_GERACAO,DT_GERACAO,DT_LIQUIDACAO_INICIAL ,DT_LIQUIDACAO_FINAL ) VALUES('" & NOVA_COMPETECIA & "'," & Session("ID_USUARIO") & ", getdate(),CONVERT(DATE,'" & txtDtInicioRelComissaoVendas.Text & "',103),CONVERT(DATE,'" & txtDtTerminoRelComissaoVendas.Text & "',103)) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_VENDEDOR  ")
                    cabecalho = dsInsert.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_VENDEDOR")

                    Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,ID_ANALISTA_COTACAO )
SELECT " & cabecalho & ", NR_NOTA_FISCAL, DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,
                                Case 
                WHEN ID_TIPO_ESTUFAGEM  = 1 THEN
                VL_COMISSAO_BASE * QT_CNTR

                WHEN ID_TIPO_ESTUFAGEM  = 2 THEN
                VL_COMISSAO_BASE * 1
                End COMISSAO_TOTAL,

				DT_LIQUIDACAO,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO

FROM FN_VENDEDOR('" & txtDtInicioRelComissaoVendas.Text & "','" & txtDtTerminoRelComissaoVendas.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND FL_VENDEDOR_DIRETO = 1 ")

                    SubVendedor(cabecalho)
                    CarregaGridComissaoVendedor()

                    Con.ExecutarQuery("UPDATE TB_DETALHE_COMISSAO_VENDEDOR SET VL_COMISSAO_TOTAL = 0 WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & " AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR=1 AND FL_COMISSAO_ZERADA = 1) ")

                    divErro.Visible = False
                    lblSuccessComissaoVendas.Visible = True
                    lblSuccessComissaoVendas.Text = "Comissão gerada com sucesso!"
                End If
            End If

        End If


        mpeRelComissaoVenda.Show()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefault()", True)
    End Sub

    Sub TabelaEquipe(cabecalho As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_ANALISTA_COTACAO,SUM (VL_COMISSAO_TOTAL)VL_COMISSAO_TOTAL FROM TB_DETALHE_COMISSAO_VENDEDOR
WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & " AND FL_CALC_EQUIPE = 1 GROUP BY ID_ANALISTA_COTACAO ")
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows

                Con.ExecutarQuery("INSERT INTO TB_EQUIPE_COMISSAO (ID_CABECALHO_COMISSAO_VENDEDOR,ID_USUARIO,VL_COMISSAO,FL_LIDER ) VALUES(" & cabecalho & "," & linha.Item("ID_ANALISTA_COTACAO") & ", " & linha.Item("VL_COMISSAO_TOTAL").ToString.Replace(".", "").Replace(",", ".") & ",0)")

            Next

        End If


        ds = Con.ExecutarQuery("SELECT B.ID_USUARIO_LIDER, COUNT(ID_ANALISTA_COTACAO) * TAXA_LIDER as VALOR, TAXA_LIDER 
FROM TB_DETALHE_COMISSAO_VENDEDOR A
INNER JOIN TB_EQUIPE_MEMBROS B ON A.ID_ANALISTA_COTACAO = B.ID_USUARIO_MEMBRO_EQUIPE
INNER JOIN TB_EQUIPE_LIDER C ON B.ID_USUARIO_LIDER = C.ID_USUARIO_LIDER
WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & " AND FL_CALC_EQUIPE = 1
GROUP BY B.ID_USUARIO_LIDER,TAXA_LIDER")

        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows

                Dim TaxaLider As String = linha.Item("TAXA_LIDER")
                Dim TaxaLider_final As String = TaxaLider.ToString.Replace(".", "")
                TaxaLider_final = TaxaLider_final.ToString.Replace(",", ".")


                Con.ExecutarQuery("INSERT INTO TB_EQUIPE_COMISSAO (ID_CABECALHO_COMISSAO_VENDEDOR,ID_USUARIO,VL_COMISSAO,FL_LIDER ) VALUES(" & cabecalho & "," & linha.Item("ID_USUARIO_LIDER") & ", " & linha.Item("VALOR").ToString.Replace(",", ".") & ",1)")

                Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,ID_USUARIO_LIDER,ID_PARCEIRO_VENDEDOR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,FL_CALC_EQUIPE ) VALUES(" & cabecalho & "," & linha.Item("ID_USUARIO_LIDER") & ",(SELECT ID_PARCEIRO_EQUIPE_INSIDE FROM TB_PARAMETROS)," & TaxaLider_final & ", " & linha.Item("VALOR").ToString.Replace(",", ".") & ",1)")



            Next

        End If
    End Sub

    Private Sub btnRelGravarCCProcessoComissaoVendas_Click(sender As Object, e As EventArgs) Handles btnRelGravarCCProcessoComissaoVendas.Click
        divErroComissaoVendas.Visible = False
        lblSuccessComissaoVendas.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtDtInicioRelComissaoVendas.Text = "" Or txtDtTerminoRelComissaoVendas.Text = "" Then
            lblErroComissaoVendas.Text = "Preencha os campos obrigatórios."
            divErroComissaoVendas.Visible = True

        Else

            Dim COMPETECIA As String = txtDtInicioRelComissaoVendas.Text
            COMPETECIA = COMPETECIA.Substring(3)

            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (CD_PR, DT_COMPETENCIA, DT_LANCAMENTO,DT_LIQUIDACAO ,DT_VENCIMENTO,ID_TIPO_LANCAMENTO_CAIXA  ,
ID_USUARIO_LANCAMENTO ,ID_USUARIO_LIQUIDACAO,TP_EXPORTACAO) VALUES('P','" & COMPETECIA & "',GETDATE(),GETDATE(),GETDATE(),7, " & Session("ID_USUARIO") & ", " & Session("ID_USUARIO") & ",'CVEND')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER")
            Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")

            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_PARCEIRO_EMPRESA,DS_HISTORICO_LANCAMENTO,ID_CONTA_PAGAR_RECEBER, VL_LANCAMENTO ,VL_LIQUIDO,ID_ITEM_DESPESA,VL_TAXA_CALCULADO,ID_MOEDA, ID_BL )
               SELECT ID_PARCEIRO_VENDEDOR,'COMISSÃO VENDEDOR – " & COMPETECIA & "'," & ID_CONTA_PAGAR_RECEBER & ",VL_COMISSAO_TOTAL, VL_COMISSAO_TOTAL, (SELECT ID_ITEM_VENDEDOR FROM TB_PARAMETROS)ID_ITEM_VENDEDOR,VL_COMISSAO_TOTAL,124,ID_BL  FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR in (SELECT distinct ID_CABECALHO_COMISSAO_VENDEDOR FROM View_Comissao_Vendedor WHERE COMPETENCIA = '" & COMPETECIA & "')")

            Con.ExecutarQuery("UPDATE TB_CABECALHO_COMISSAO_VENDEDOR SET DT_EXPORTACAO =  GETDATE(), ID_USUARIO_EXPORTACAO = " & Session("ID_USUARIO") & "   WHERE ID_CABECALHO_COMISSAO_VENDEDOR in (SELECT distinct ID_CABECALHO_COMISSAO_VENDEDOR FROM View_Comissao_Vendedor WHERE COMPETENCIA = '" & COMPETECIA & "')")


            CarregaGridComissaoVendedor()
            lblSuccessComissaoVendas.Visible = True
            lblSuccessComissaoVendas.Text = "Comissão exportada para o processo com sucesso!"

        End If

    End Sub

    Private Sub btnFiltrarRelComissaoVendas_Click(sender As Object, e As EventArgs) Handles btnFiltrarRelComissaoVendas.Click
        CarregaGridComissaoVendedor()
    End Sub

    'TABELA PROSPECCAO

    Public Sub LimparComissaoProspeccao()
        txtIDComissoesProspeccao.Text = ""
        txtValidadeInicialComissoesProspeccao.Text = ""
        ddlEstufagemComissoesProspeccao.SelectedValue = 0
        ddlViaComissoesProspeccao.SelectedValue = 0
        ddlTipoCalculoComissoesProspeccao.SelectedValue = 0
        ddlBaseCalculoComissoesProspeccao.SelectedValue = 0
        txtValorComissoesProspecccao.Text = ""
        ddlEquipeComissoesProspeccao.SelectedValue = 0
    End Sub
    Private Sub btnSalvarComissoesProspeccao_Click(sender As Object, e As EventArgs) Handles btnSalvarComissoesProspeccao.Click
        divComissoesProspeccaoErro.Visible = False
        divComissoesProspeccaoSucesso.Visible = False

        Dim Valor As String = txtValorComissoesProspecccao.Text.Replace(".", "")
        Valor = Valor.Replace(",", ".")


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim sql As String = ""
        If txtIDComissoesProspeccao.Text = "" Then

            sql = " INSERT INTO TB_VENDEDOR_PROSPECCAO  "
            sql = sql & " (DT_VALIDADE_INICIAL,ID_TIPO_ESTUFAGEM,ID_VIATRANSPORTE,ID_TIPO_CALCULO,ID_BASE_CALCULO_TAXA,VL_TAXA,ID_EQUIPE,FL_PAGAMENTO_RECORRENTE) "
            sql = sql & " VALUES ( CONVERT(DATE,'" & txtValidadeInicialComissoesProspeccao.Text & "',103) ," & ddlEstufagemComissoesProspeccao.SelectedValue & " , "
            sql = sql & ddlViaComissoesProspeccao.SelectedValue & " , " & ddlTipoCalculoComissoesProspeccao.SelectedValue & " , " & ddlBaseCalculoComissoesProspeccao.SelectedValue & ", "
            sql = sql & Valor & "," & ddlEquipeComissoesProspeccao.SelectedValue & "," & rdPagamentoComissoesProspeccao.SelectedValue & ") "
            Con.ExecutarQuery(sql)
            divComissoesProspeccaoSucesso.Visible = True
            lblComissoesProspeccaoSucesso.Text = "Taxa inserida com sucesso"
            dgvTabelaComissaoProspeccao.DataBind()
            mpeTabelas.Show()
            LimparComissaoProspeccao()
        Else
            sql = " UPDATE TB_VENDEDOR_PROSPECCAO SET DT_VALIDADE_INICIAL = CONVERT(Date,'" & txtValidadeInicialComissoesProspeccao.Text & "',103) , "
            sql = sql & " ID_TIPO_ESTUFAGEM = " & ddlEstufagemComissoesProspeccao.SelectedValue & " ,ID_VIATRANSPORTE = " & ddlViaComissoesProspeccao.SelectedValue & " , "
            sql = sql & " ID_TIPO_CALCULO = " & ddlTipoCalculoComissoesProspeccao.SelectedValue & " ,ID_BASE_CALCULO_TAXA = " & ddlBaseCalculoComissoesProspeccao.SelectedValue & ", "
            sql = sql & " VL_TAXA = " & Valor & " ,ID_EQUIPE = " & ddlEquipeComissoesProspeccao.SelectedValue & ",  FL_PAGAMENTO_RECORRENTE = " & rdPagamentoComissoesProspeccao.SelectedValue
            sql = sql & " WHERE ID_VENDEDOR_PROSPECCAO = " & txtIDComissoesProspeccao.Text
            Con.ExecutarQuery(sql)
            divComissoesProspeccaoSucesso.Visible = True
            lblComissoesProspeccaoSucesso.Text = "Taxa alterada com sucesso"
            dgvTabelaComissaoProspeccao.DataBind()
            mpeTabelas.Show()

        End If
    End Sub

    Private Sub dgvTabelaComissaoProspeccao_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTabelaComissaoProspeccao.RowCommand
        divComissoesProspeccaoSucesso.Visible = False
        divComissoesProspeccaoErro.Visible = False

        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblComissoesProspeccaoErro.Text = "Usuário não tem permissão para realizar exclusões"
                divComissoesProspeccaoErro.Visible = True
            Else
                Con.ExecutarQuery("DELETE FROM TB_VENDEDOR_PROSPECCAO WHERE ID_VENDEDOR_PROSPECCAO =" & ID)
                dgvTabelaComissaoProspeccao.DataBind()
                divComissoesProspeccaoSucesso.Visible = True
                lblComissoesProspeccaoSucesso.Text = "Taxa excluída com sucesso"
                mpeTabelas.Show()
            End If

        ElseIf e.CommandName = "Editar" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_VENDEDOR_PROSPECCAO,DT_VALIDADE_INICIAL,ID_TIPO_ESTUFAGEM,ID_VIATRANSPORTE,ID_TIPO_CALCULO
      ,ID_BASE_CALCULO_TAXA,VL_TAXA,ID_EQUIPE,FL_PAGAMENTO_RECORRENTE FROM TB_VENDEDOR_PROSPECCAO WHERE ID_VENDEDOR_PROSPECCAO = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VENDEDOR_PROSPECCAO")) Then
                    txtIDComissoesProspeccao.Text = ds.Tables(0).Rows(0).Item("ID_VENDEDOR_PROSPECCAO").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")) Then
                    txtValidadeInicialComissoesProspeccao.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                    ddlEstufagemComissoesProspeccao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")) Then
                    ddlViaComissoesProspeccao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CALCULO")) Then
                    ddlTipoCalculoComissoesProspeccao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CALCULO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculoComissoesProspeccao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtValorComissoesProspecccao.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_EQUIPE")) Then
                    ddlEquipeComissoesProspeccao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_EQUIPE")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_PAGAMENTO_RECORRENTE")) Then
                    rdPagamentoComissoesProspeccao.SelectedValue = ds.Tables(0).Rows(0).Item("FL_PAGAMENTO_RECORRENTE")
                End If

            End If

            mpeTabelas.Show()
        End If
        Con.Fechar()
    End Sub

    Private Sub btnLimparComissoesProspeccao_Click(sender As Object, e As EventArgs) Handles btnLimparComissoesProspeccao.Click
        LimparComissaoProspeccao()
    End Sub

    Private Sub btnFecharComissoesProspecao_Click(sender As Object, e As EventArgs) Handles btnFecharComissoesProspecao.Click
        LimparComissaoProspeccao()
        mpeTabelas.Hide()
    End Sub

    'PROSPECCAO

    Public Sub btnFecharRelComissaoProspecao_Click(sender As Object, e As EventArgs) Handles btnFecharRelComissaoProspecao.Click

        UpdateRelComissaoProspecao.Visible = False
        txtDtInicioComissaoProspecao.Text = String.Empty
        txtDtTerminoComissaoProspecao.Text = String.Empty

    End Sub

    Private Sub btnRelGerarCompetenciaComissaoProspecao_Click(sender As Object, e As EventArgs) Handles btnRelGerarCompetenciaComissaoProspecao.Click
        divComissaoProspeccaoSucesso.Visible = False
        divComissaoProspeccaoErro.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtDtInicioComissaoProspecao.Text = "" Or txtDtTerminoComissaoProspecao.Text = "" Then
            lblComissaoProspeccaoErro.Text = "Preencha os campos obrigatórios."
            divComissaoProspeccaoErro.Visible = True


        Else
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblComissaoProspeccaoErro.Text = "Usuário não tem permissão!"
                divComissaoProspeccaoErro.Visible = True
            Else


                Dim NOVA_COMPETECIA As String = txtDtInicioComissaoProspecao.Text
                NOVA_COMPETECIA = NOVA_COMPETECIA.Substring(2)
                NOVA_COMPETECIA = NOVA_COMPETECIA.Replace("/", "")

                Dim dsInsert As DataSet
                Dim cabecalho As String

                dsInsert = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_PROSPECCAO (DT_COMPETENCIA,ID_USUARIO_GERACAO,DT_GERACAO,DT_LIQUIDACAO_INICIAL ,DT_LIQUIDACAO_FINAL ) VALUES('" & NOVA_COMPETECIA & "'," & Session("ID_USUARIO") & ", getdate(),CONVERT(DATE,'" & txtDtInicioComissaoProspecao.Text & "',103),CONVERT(DATE,'" & txtDtTerminoComissaoProspecao.Text & "',103)) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_PROSPECCAO   ")
                cabecalho = dsInsert.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_PROSPECCAO")

                Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_PROSPECCAO (ID_CABECALHO_COMISSAO_PROSPECCAO,NR_PROCESSO,ID_BL,NR_NOTA_FISCAL,ID_PARCEIRO_PROSPECCAO,VL_COMISSAO_TOTAL,DT_LIQUIDACAO)
SELECT DISTINCT " & cabecalho & ",  MIN(A.NR_PROCESSO)NR_PROCESSO,MIN(A.ID_BL)ID_BL,MIN(A.NR_NOTA_FISCAL)NR_NOTA_FISCAL,C.ID_EQUIPE,  VL_TAXA,MIN(A.DT_LIQUIDACAO)DT_LIQUIDACAO 
FROM FN_VENDEDOR('" & txtDtInicioComissaoProspecao.Text & "','" & txtDtTerminoComissaoProspecao.Text & "') A
INNER JOIN TB_DETALHE_COMISSAO_VENDEDOR E ON  E.ID_BL =A.ID_BL
INNER JOIN TB_CABECALHO_COMISSAO_VENDEDOR D ON D.ID_CABECALHO_COMISSAO_VENDEDOR = E.ID_CABECALHO_COMISSAO_VENDEDOR
LEFT  JOIN TB_PARCEIRO B ON B.ID_PARCEIRO=A.ID_PARCEIRO_CLIENTE
LEFT  JOIN TB_VENDEDOR_EQUIPE C  ON B.ID_VENDEDOR_PROSPECCAO=C.ID_EQUIPE
LEFT  JOIN TB_VENDEDOR_PROSPECCAO F  ON F.ID_EQUIPE=C.ID_EQUIPE
WHERE ISNULL(B.ID_VENDEDOR_PROSPECCAO,0) <> 0 AND ISNULL(B.FL_PROSPECCAO_REALIZADA,0) = 0 
GROUP BY  C.ID_EQUIPE, C.NM_EQUIPE ,B.ID_PARCEIRO ,VL_TAXA ")

                Con.ExecutarQuery("UPDATE TB_PARCEIRO SET FL_PROSPECCAO_REALIZADA = 0 WHERE ID_PARCEIRO IN ( SELECT ID_PARCEIRO_PROSPECCAO FROM TB_DETALHE_COMISSAO_PROSPECCAO) ")

                divComissaoProspeccaoSucesso.Visible = True
                lblComissaoProspeccaoSucesso.Text = "Comissão gerada com sucesso!"

                dsRelProspeccao.SelectCommand = "SELECT * FROM [dbo].[View_Prospeccao] WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "' ORDER BY NR_PROCESSO"
                dgvRelProspeccao.DataBind()

            End If

        End If

        mpeRelComissaoProspecao.Show()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefault()", True)
    End Sub

    Private Sub lkRelComissaoProspecao_Click(sender As Object, e As EventArgs) Handles lkRelComissaoProspecao.Click
        mpeRelComissaoProspecao.Show()
    End Sub

    Private Sub btnFiltrarComissaoProspecao_Click(sender As Object, e As EventArgs) Handles btnFiltrarComissaoProspecao.Click
        Dim DT_COMPETENCIA As String = txtDtInicioComissaoProspecao.Text
        DT_COMPETENCIA = DT_COMPETENCIA.Substring(2)
        DT_COMPETENCIA = DT_COMPETENCIA.Replace("/", "")
        dsRelProspeccao.SelectCommand = "SELECT * FROM [dbo].[View_Prospeccao]  ORDER BY NR_PROCESSO"
        dgvRelProspeccao.DataBind()
        mpeRelComissaoProspecao.Show()
    End Sub
    Private Sub btnRelGravarCCProcessoComissaoProspecao_Click(sender As Object, e As EventArgs) Handles btnRelGravarCCProcessoComissaoProspecao.Click
        divComissaoProspeccaoSucesso.Visible = False
        divComissaoProspeccaoErro.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtDtInicioComissaoProspecao.Text = "" Or txtDtTerminoComissaoProspecao.Text = "" Then
            lblComissaoProspeccaoErro.Text = "Preencha os campos obrigatórios."
            divComissaoProspeccaoErro.Visible = True

        Else

            Dim COMPETECIA As String = txtDtInicioComissaoProspecao.Text
            COMPETECIA = COMPETECIA.Substring(3)

            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (CD_PR, DT_COMPETENCIA, DT_LANCAMENTO,DT_LIQUIDACAO ,DT_VENCIMENTO,ID_TIPO_LANCAMENTO_CAIXA  ,
ID_USUARIO_LANCAMENTO ,ID_USUARIO_LIQUIDACAO,TP_EXPORTACAO) VALUES('P','" & COMPETECIA & "',GETDATE(),GETDATE(),GETDATE(),7, " & Session("ID_USUARIO") & ", " & Session("ID_USUARIO") & ",'CPROSP')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER")
            Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")

            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_PARCEIRO_EMPRESA,DS_HISTORICO_LANCAMENTO,ID_CONTA_PAGAR_RECEBER, VL_LANCAMENTO ,VL_LIQUIDO,ID_ITEM_DESPESA,VL_TAXA_CALCULADO,ID_MOEDA, ID_BL )
               SELECT ID_PARCEIRO_PROSPECCAO,'COMISSÃO PROSPECÇÃO – " & COMPETECIA & "'," & ID_CONTA_PAGAR_RECEBER & ",VL_COMISSAO_TOTAL, VL_COMISSAO_TOTAL, (SELECT ID_ITEM_VENDEDOR FROM TB_PARAMETROS)ID_ITEM_VENDEDOR,VL_COMISSAO_TOTAL,124,ID_BL  FROM TB_DETALHE_COMISSAO_PROSPECCAO WHERE ID_CABECALHO_COMISSAO_PROSPECCAO in (SELECT distinct ID_CABECALHO_COMISSAO_PROSPECCAO FROM View_Prospeccao WHERE COMPETENCIA = '" & COMPETECIA & "')")


            Con.ExecutarQuery("UPDATE TB_CABECALHO_COMISSAO_PROSPECCAO SET DT_EXPORTACAO =  GETDATE(), ID_USUARIO_EXPORTACAO = " & Session("ID_USUARIO") & "   WHERE ID_CABECALHO_COMISSAO_PROSPECCAO IN (SELECT DISTINCT ID_CABECALHO_COMISSAO_PROSPECCAO FROM VIEW_PROSPECCAO WHERE COMPETENCIA = '" & COMPETECIA & "')")


            divComissaoProspeccaoSucesso.Visible = True
            lblComissaoProspeccaoSucesso.Text = "Comissão exportada para o processo com sucesso!"

        End If
        mpeRelComissaoProspecao.Show()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefault()", True)
    End Sub


    'TABELA INDICACAO INTERNA
    Public Sub LimparIndicadorInterno()
        txtIDIndicadorInterno.Text = ""
        txtValidadeIndicadorInterno.Text = ""
        txtValorIndicadorInterno.Text = ""
    End Sub
    Private Sub btnLimparIndicadorInterno_Click(sender As Object, e As EventArgs) Handles btnLimparIndicadorInterno.Click
        LimparIndicadorInterno()
        mpeTabelas.Show()
    End Sub

    Private Sub btnSalvarIndicadorInterno_Click(sender As Object, e As EventArgs) Handles btnSalvarIndicadorInterno.Click
        divIndicacaoInternoSucesso.Visible = False
        divIndicacaoInternoErro.Visible = False

        Dim Valor As String = txtValorIndicadorInterno.Text.Replace(".", "")
        Valor = Valor.Replace(",", ".")


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim sql As String = ""
        If txtIDIndicadorInterno.Text = "" Then

            sql = " INSERT INTO TB_VENDEDOR_INDICADOR_INTERNO (DT_VALIDADE_INICIAL,VL_TAXA)  VALUES ( CONVERT(DATE,'" & txtValidadeIndicadorInterno.Text & "',103) ," & Valor & ") "
            Con.ExecutarQuery(sql)
            divIndicacaoInternoSucesso.Visible = True
            lblIndicacaoInternoSucesso.Text = "Taxa inserida com sucesso"
            dgvIndicadorInterno.DataBind()
            mpeTabelas.Show()
            LimparComissaoProspeccao()
        Else
            sql = " UPDATE TB_VENDEDOR_INDICADOR_INTERNO SET DT_VALIDADE_INICIAL = CONVERT(Date,'" & txtValidadeIndicadorInterno.Text & "',103) ,  VL_TAXA = " & Valor & "  WHERE ID_VENDEDOR_INDICADOR_INTERNO = " & txtIDIndicadorInterno.Text
            Con.ExecutarQuery(sql)
            divIndicacaoInternoSucesso.Visible = True
            lblIndicacaoInternoSucesso.Text = "Taxa alterada com sucesso"
            dgvIndicadorInterno.DataBind()
            mpeTabelas.Show()

        End If
    End Sub

    Private Sub dgvIndicadorInterno_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvIndicadorInterno.RowCommand
        divIndicacaoInternoSucesso.Visible = False
        divIndicacaoInternoErro.Visible = False

        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblIndicacaoInternoErro.Text = "Usuário não tem permissão para realizar exclusões"
                divIndicacaoInternoErro.Visible = True
            Else
                Con.ExecutarQuery("DELETE FROM TB_VENDEDOR_INDICADOR_INTERNO WHERE ID_VENDEDOR_INDICADOR_INTERNO =" & ID)
                dgvIndicadorInterno.DataBind()
                divIndicacaoInternoSucesso.Visible = True
                lblIndicacaoInternoSucesso.Text = "Taxa excluída com sucesso"
                mpeTabelas.Show()
            End If

        ElseIf e.CommandName = "Editar" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_VENDEDOR_INDICADOR_INTERNO,DT_VALIDADE_INICIAL,VL_TAXA FROM TB_VENDEDOR_INDICADOR_INTERNO WHERE ID_VENDEDOR_INDICADOR_INTERNO = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VENDEDOR_INDICADOR_INTERNO")) Then
                    txtIDIndicadorInterno.Text = ds.Tables(0).Rows(0).Item("ID_VENDEDOR_INDICADOR_INTERNO").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")) Then
                    txtValidadeIndicadorInterno.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtValorIndicadorInterno.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                End If


            End If

            mpeTabelas.Show()
        End If
        Con.Fechar()
    End Sub

    Private Sub btnFecharIndicadorInterno_Click(sender As Object, e As EventArgs) Handles btnFecharIndicadorInterno.Click
        LimparIndicadorInterno()
        mpeTabelas.Hide()
    End Sub

    'INDICACAO INTERNA
    Public Sub btnFecharRelComissaoIndicacaoInterna_Click(sender As Object, e As EventArgs) Handles btnFecharRelComissaoIndicacaoInterna.Click
        UpdateRelComissaoIndicacaoInterna.Visible = False
        txtDtInicioRelIndicacaoInterna.Text = String.Empty
        txtDtTerminoRelIndicacaoInterna.Text = String.Empty

    End Sub

    Private Sub btnRelGerarCompetenciaComissaoIndicacaoInterna_Click(sender As Object, e As EventArgs) Handles btnRelGerarCompetenciaComissaoIndicacaoInterna.Click
        divIndicacaoInternaSucesso.Visible = False
        divIndicacaoInternaErro.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtDtInicioRelIndicacaoInterna.Text = "" Or txtDtTerminoRelIndicacaoInterna.Text = "" Then
            lblIndicacaoInternaErro.Text = "Preencha os campos obrigatórios."
            divIndicacaoInternaErro.Visible = True


        Else
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblIndicacaoInternaErro.Text = "Usuário não tem permissão!"
                divIndicacaoInternaErro.Visible = True
            Else


                Dim NOVA_COMPETECIA As String = txtDtInicioRelIndicacaoInterna.Text
                NOVA_COMPETECIA = NOVA_COMPETECIA.Substring(2)
                NOVA_COMPETECIA = NOVA_COMPETECIA.Replace("/", "")

                Dim dsInsert As DataSet
                Dim cabecalho As String

                dsInsert = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_INDICACAO_INTERNA (DT_COMPETENCIA,ID_USUARIO_GERACAO,DT_GERACAO,DT_LIQUIDACAO_INICIAL ,DT_LIQUIDACAO_FINAL ) VALUES('" & NOVA_COMPETECIA & "'," & Session("ID_USUARIO") & ", getdate(),CONVERT(DATE,'" & txtDtInicioRelIndicacaoInterna.Text & "',103),CONVERT(DATE,'" & txtDtTerminoRelIndicacaoInterna.Text & "',103)) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_INDICACAO_INTERNA   ")
                cabecalho = dsInsert.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INDICACAO_INTERNA")

                Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_INDICACAO_INTERNA (ID_CABECALHO_COMISSAO_INDICACAO_INTERNA,NR_PROCESSO,ID_BL,NR_NOTA_FISCAL,ID_PARCEIRO_INDICACAO_INTERNA,VL_COMISSAO_TOTAL,DT_LIQUIDACAO)
SELECT DISTINCT " & cabecalho & ",  MIN(A.NR_PROCESSO)NR_PROCESSO,MIN(A.ID_BL)ID_BL,MIN(A.NR_NOTA_FISCAL)NR_NOTA_FISCAL,C.ID_PARCEIRO,   (SELECT VL_TAXA FROM TB_VENDEDOR_INDICADOR_INTERNO
WHERE DT_VALIDADE_INICIAL <= GETDATE()
)VL_TAXA,MIN(A.DT_LIQUIDACAO)DT_LIQUIDACAO
FROM FN_VENDEDOR('" & txtDtInicioRelIndicacaoInterna.Text & "','" & txtDtTerminoRelIndicacaoInterna.Text & "') A
 INNER JOIN TB_DETALHE_COMISSAO_VENDEDOR E ON  E.ID_BL =A.ID_BL
 INNER JOIN TB_CABECALHO_COMISSAO_VENDEDOR D ON D.ID_CABECALHO_COMISSAO_VENDEDOR = E.ID_CABECALHO_COMISSAO_VENDEDOR
LEFT  JOIN TB_PARCEIRO B ON B.ID_PARCEIRO=A.ID_PARCEIRO_CLIENTE
LEFT  JOIN TB_PARCEIRO C  ON B.ID_PARCEIRO_INDICACAO_INTERNA=C.ID_PARCEIRO  
WHERE ISNULL(B.ID_PARCEIRO_INDICACAO_INTERNA,0) <> 0 AND ISNULL(C.FL_INDICACAO_REALIZADA,0) = 0 GROUP BY  C.NM_RAZAO ,C.ID_PARCEIRO ")



                Con.ExecutarQuery("UPDATE TB_PARCEIRO SET FL_INDICACAO_REALIZADA = 0 WHERE ID_PARCEIRO IN ( SELECT ID_PARCEIRO_INDICACAO_INTERNA FROM TB_DETALHE_COMISSAO_INDICACAO_INTERNA) ")

                divIndicacaoInternaSucesso.Visible = True
                lblIndicacaoInternaSucesso.Text = "Comissão gerada com sucesso!"

                dsRelIndicacaoInterna.SelectCommand = "SELECT * FROM [dbo].[View_Indicacao_Interna] WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "' ORDER BY NR_PROCESSO"
                dgvRelIndicacaoInterna.DataBind()

            End If

        End If

        mpeRelComissaoIndicacaoInterna.Show()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefault()", True)
    End Sub
    Private Sub btnFiltrarRelIndicacaoInterna_Click(sender As Object, e As EventArgs) Handles btnFiltrarRelIndicacaoInterna.Click

        Dim DT_COMPETENCIA As String = txtDtInicioRelIndicacaoInterna.Text
        DT_COMPETENCIA = DT_COMPETENCIA.Substring(2)
        DT_COMPETENCIA = DT_COMPETENCIA.Replace("/", "")
        dsRelIndicacaoInterna.SelectCommand = "SELECT * FROM [dbo].[View_Indicacao_Interna] WHERE DT_COMPETENCIA = '" & DT_COMPETENCIA & "' ORDER BY NR_PROCESSO"
        dgvRelIndicacaoInterna.DataBind()

    End Sub

    Private Sub lkRelComissaoIndicacaoInterna_Click(sender As Object, e As EventArgs) Handles lkRelComissaoIndicacaoInterna.Click
        mpeRelComissaoIndicacaoInterna.Show()
    End Sub

    Private Sub btnRelGravarCCProcessoComissaoIndicacaoInterna_Click(sender As Object, e As EventArgs) Handles btnRelGravarCCProcessoComissaoIndicacaoInterna.Click
        divIndicacaoInternaSucesso.Visible = False
        divIndicacaoInternaErro.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtDtInicioRelIndicacaoInterna.Text = "" Or txtDtTerminoRelIndicacaoInterna.Text = "" Then
            lblIndicacaoInternaErro.Text = "Preencha os campos obrigatórios."
            divIndicacaoInternaErro.Visible = True


        Else
            Dim COMPETECIA As String = txtDtInicioRelIndicacaoInterna.Text
            COMPETECIA = COMPETECIA.Substring(3)

            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (CD_PR, DT_COMPETENCIA, DT_LANCAMENTO,DT_LIQUIDACAO ,DT_VENCIMENTO,ID_TIPO_LANCAMENTO_CAIXA  ,
ID_USUARIO_LANCAMENTO ,ID_USUARIO_LIQUIDACAO,TP_EXPORTACAO,ID_CONTA_BANCARIA) VALUES('P','" & COMPETECIA & "',GETDATE(),GETDATE(),GETDATE(),7, " & Session("ID_USUARIO") & ", " & Session("ID_USUARIO") & ",'CINDINT',1)  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER")
            Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")

            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_PARCEIRO_EMPRESA,DS_HISTORICO_LANCAMENTO,ID_CONTA_PAGAR_RECEBER, VL_LANCAMENTO ,VL_LIQUIDO,ID_ITEM_DESPESA,VL_TAXA_CALCULADO,ID_MOEDA, ID_BL )
               SELECT ID_PARCEIRO_INDICACAO_INTERNA,'COMISSÃO INDICADOR INTERNO - " & COMPETECIA & "'," & ID_CONTA_PAGAR_RECEBER & ",VL_COMISSAO_TOTAL, VL_COMISSAO_TOTAL, (SELECT ID_ITEM_VENDEDOR FROM TB_PARAMETROS)ID_ITEM_VENDEDOR,VL_COMISSAO_TOTAL,124,ID_BL  FROM [TB_DETALHE_COMISSAO_INDICACAO_INTERNA] WHERE ID_CABECALHO_COMISSAO_INDICACAO_INTERNA in (SELECT DISTINCT ID_CABECALHO_COMISSAO_INDICACAO_INTERNA FROM [dbo].[View_Indicacao_Interna] WHERE COMPETENCIA = '" & COMPETECIA & "')")


            Con.ExecutarQuery("UPDATE [TB_CABECALHO_COMISSAO_INDICACAO_INTERNA] SET DT_EXPORTACAO =  GETDATE(), ID_USUARIO_EXPORTACAO = " & Session("ID_USUARIO") & " WHERE ID_CABECALHO_COMISSAO_INDICACAO_INTERNA IN (SELECT DISTINCT ID_CABECALHO_COMISSAO_INDICACAO_INTERNA FROM VIEW_INDICACAO_INTERNA WHERE COMPETENCIA = '" & COMPETECIA & "')")

            divIndicacaoInternaSucesso.Visible = True
            lblIndicacaoInternaSucesso.Text = "Comissão exportada para o CC com sucesso!"

        End If
        mpeRelComissaoIndicacaoInterna.Show()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefault()", True)
    End Sub

    'TABELA DE METAS
    Public Sub LimparCadastroMeta()
        txtIDCadastroMeta.Text = ""
        txtValidadeCadastroMeta.Text = ""
        txtMetaMinimaCadastroMeta.Text = ""
        txtMetaMaximaCadastroMeta.Text = ""
        txtValorCadastroMeta.Text = ""
        ddlViaCadastroMeta.SelectedValue = 0
        ddlEstufagemCadastroMeta.SelectedValue = 0
    End Sub

    Private Sub btnLimparCadastroMeta_Click(sender As Object, e As EventArgs) Handles btnLimparCadastroMeta.Click
        LimparCadastroMeta()
    End Sub

    Private Sub btnSalvarCadastroMeta_Click(sender As Object, e As EventArgs) Handles btnSalvarCadastroMeta.Click
        divCadastroMetaSucesso.Visible = False
        divCadastroMetaErro.Visible = False

        Dim Meta As String = txtValorCadastroMeta.Text.Replace(".", "")
        Meta = Meta.Replace(",", ".")

        Dim MetaMin As String = txtMetaMinimaCadastroMeta.Text.Replace(".", "")
        MetaMin = MetaMin.Replace(",", ".")

        Dim MetaMax As String = txtMetaMaximaCadastroMeta.Text.Replace(".", "")
        MetaMax = MetaMax.Replace(",", ".")

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim sql As String = ""
        If txtIDCadastroMeta.Text = "" Then

            sql = " INSERT INTO TB_VENDEDOR_CADASTRO_METAS (DT_VALIDADE_INICIAL,ID_TIPO_ESTUFAGEM,ID_VIATRANSPORTE,VL_META,VL_META_MIN,META_MAX)"
            sql = sql & " VALUES ( CONVERT(DATE,'" & txtValidadeCadastroMeta.Text & "',103) , " & ddlEstufagemCadastroMeta.SelectedValue & " , " & ddlViaCadastroMeta.SelectedValue & " , "
            sql = sql & " " & Meta & " ,  " & MetaMin & " , " & "  " & MetaMax & " ) "
            Con.ExecutarQuery(sql)
            divCadastroMetaSucesso.Visible = True
            lblCadastroMetaSucesso.Text = "Taxa inserida com sucesso"
            dgvCadastroMeta.DataBind()
            mpeCadastroMetas.Show()
            LimparCadastroMeta()
        Else
            sql = " UPDATE TB_VENDEDOR_CADASTRO_METAS Set DT_VALIDADE_INICIAL = CONVERT(Date,'" & txtValidadeCadastroMeta.Text & "',103) ,"
            sql = sql & " ID_TIPO_ESTUFAGEM = " & ddlEstufagemCadastroMeta.SelectedValue & " ,ID_VIATRANSPORTE = " & ddlViaCadastroMeta.SelectedValue & " , "
            sql = sql & " VL_META = " & Meta & " , VL_META_MIN = " & MetaMin & " , " & " , META_MAX = " & MetaMax
            sql = sql & " WHERE ID_VENDEDOR_METAS = " & txtIDCadastroMeta.Text
            Con.ExecutarQuery(sql)
            divCadastroMetaSucesso.Visible = True
            lblCadastroMetaSucesso.Text = "Taxa alterada com sucesso"
            dgvCadastroMeta.DataBind()
            mpeCadastroMetas.Show()

        End If
    End Sub

    Private Sub btnFecharCadastrarMetas_Click(sender As Object, e As EventArgs) Handles btnFecharCadastrarMetas.Click
        LimparCadastroMeta()
        mpeCadastroMetas.Hide()
    End Sub

    Private Sub dgvCadastroMeta_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvCadastroMeta.RowCommand
        divCadastroMetaSucesso.Visible = False
        divCadastroMetaErro.Visible = False

        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblCadastroMetaErro.Text = "Usuário não tem permissão para realizar exclusões"
                divCadastroMetaErro.Visible = True
                mpeCadastroMetas.Show()
            Else
                Con.ExecutarQuery("DELETE FROM TB_VENDEDOR_CADASTRO_METAS WHERE ID_VENDEDOR_METAS =" & ID)
                dgvIndicadorInterno.DataBind()
                divCadastroMetaSucesso.Visible = True
                lblCadastroMetaSucesso.Text = "Taxa excluída com sucesso"
                mpeCadastroMetas.Show()
            End If

        ElseIf e.CommandName = "Editar" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_VENDEDOR_METAS,DT_VALIDADE_INICIAL,ID_TIPO_ESTUFAGEM,ID_VIATRANSPORTE,VL_META,VL_META_MIN,META_MAX FROM TB_VENDEDOR_CADASTRO_METAS WHERE ID_VENDEDOR_METAS = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VENDEDOR_METAS")) Then
                    txtIDCadastroMeta.Text = ds.Tables(0).Rows(0).Item("ID_VENDEDOR_METAS").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")) Then
                    txtValidadeCadastroMeta.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_META")) Then
                    txtValorCadastroMeta.Text = ds.Tables(0).Rows(0).Item("VL_META").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_META_MIN")) Then
                    txtMetaMinimaCadastroMeta.Text = ds.Tables(0).Rows(0).Item("VL_META_MIN")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("META_MAX")) Then
                    txtMetaMaximaCadastroMeta.Text = ds.Tables(0).Rows(0).Item("META_MAX").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                    ddlEstufagemCadastroMeta.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")) Then
                    ddlViaCadastroMeta.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")
                End If
            End If

            mpeCadastroMetas.Show()
        End If
        Con.Fechar()
    End Sub

    'METAS

    Private Sub btnLimparGeradorMetasAlcancadas_Click(sender As Object, e As EventArgs) Handles btnLimparGeradorMetasAlcancadas.Click
        divMetasAlcancadasSucesso.Visible = False
        divMetasAlcancadasErro.Visible = False
        divConteudoDinamico.InnerHtml = ""
        txtDataInicioMetasAlcancadas.Text = ""
        txtDataTerminoMetasAlcancadas.Text = ""
        txtIDMetasAlcancadas.Text = ""
        mpeGerarMetasAlcancadas.Show()
    End Sub

    Private Sub btnGerarMetasAlcancadas_Click(sender As Object, e As EventArgs) Handles btnGerarMetasAlcancadas.Click
        'EXIBE RELATORIO NA TELA PARA VALIDACAO
        divMetasAlcancadasSucesso.Visible = False
        divMetasAlcancadasErro.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtDataInicioMetasAlcancadas.Text = "" Or txtDataTerminoMetasAlcancadas.Text = "" Then
            lblMetasAlcancadasErro.Text = "Preencha os campos obrigatórios."
            divMetasAlcancadasErro.Visible = True
        Else
            CalcularMetas()
            mpeGerarMetasAlcancadas.Show()
        End If
    End Sub

    Sub CalcularMetas()
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_VENDEDOR_META (ID_USUARIO_GERACAO,DT_GERACAO,DT_LIQUIDACAO_INICIAL ,DT_LIQUIDACAO_FINAL ) VALUES( " & Session("ID_USUARIO") & ", getdate(),CONVERT(DATE,'" & txtDataInicioMetasAlcancadas.Text & "',103),CONVERT(DATE,'" & txtDataTerminoMetasAlcancadas.Text & "',103)) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_VENDEDOR_META  ")
        Dim cabecalho As String = ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_VENDEDOR_META")
        txtIDMetasAlcancadas.Text = cabecalho

        ds = Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR_META (ID_CABECALHO_COMISSAO_VENDEDOR_META  , ID_BL , ID_SERVICO , ID_VIATRANSPORTE , ID_PARCEIRO_VENDEDOR  , ID_PARCEIRO_CLIENTE , ID_TIPO_ESTUFAGEM , QT_BL , QT_CNTR, VL_ADICIONAL) SELECT " & cabecalho & ", ID_BL , ID_SERVICO , ID_VIATRANSPORTE , ID_PARCEIRO_VENDEDOR , ID_PARCEIRO_CLIENTE  , ID_TIPO_ESTUFAGEM  , QT_PROCESSO , QT_CNTR , 0  FROM [dbo].[FN_VENDEDOR_RELATORIO_METAS] ('" & txtDataInicioMetasAlcancadas.Text & "','" & txtDataTerminoMetasAlcancadas.Text & "') ORDER BY ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,ID_BL")


        ds = Con.ExecutarQuery("SELECT
 ROW_NUMBER() OVER( PARTITION BY ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM, ID_VIATRANSPORTE ORDER BY ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,ID_BL )NUM,
ID_DETALHE_COMISSAO_VENDEDOR_META ,ID_VIATRANSPORTE , ID_PARCEIRO_VENDEDOR , ID_TIPO_ESTUFAGEM  FROM TB_DETALHE_COMISSAO_VENDEDOR_META WHERE ID_CABECALHO_COMISSAO_VENDEDOR_META = " & cabecalho & " ORDER BY ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,ID_BL")
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows

                Dim dsMeta As DataSet = Con.ExecutarQuery("SELECT VL_META, META_MIN, META_MAX FROM [FN_VENDEDOR_CALCULA_METAS]('" & txtDataInicioMetasAlcancadas.Text & "','" & txtDataTerminoMetasAlcancadas.Text & "') WHERE ID_PARCEIRO_VENDEDOR = '" & linha("ID_PARCEIRO_VENDEDOR") & "' AND ID_VIATRANSPORTE = " & linha("ID_VIATRANSPORTE") & " AND ID_TIPO_ESTUFAGEM = " & linha("ID_TIPO_ESTUFAGEM"))
                If dsMeta.Tables(0).Rows.Count > 0 Then

                    If linha("NUM") > dsMeta.Tables(0).Rows(0).Item("META_MIN") And linha("NUM") < dsMeta.Tables(0).Rows(0).Item("META_MAX") Then
                        Con.ExecutarQuery("UPDATE TB_DETALHE_COMISSAO_VENDEDOR_META SET VL_ADICIONAL = " & dsMeta.Tables(0).Rows(0).Item("VL_META").ToString.Replace(",", ".") & " WHERE ID_DETALHE_COMISSAO_VENDEDOR_META = " & linha("ID_DETALHE_COMISSAO_VENDEDOR_META"))

                    End If

                End If
            Next

        End If
        dsMetasAlcancadas.DataBind()
        dgvMetasAlcancadas.DataBind()
    End Sub
    Private Sub btnValidarMetasAlcancadas_Click(sender As Object, e As EventArgs) Handles btnValidarMetasAlcancadas.Click
        ''GRAVA NA TABELA DE COMISSOES VENDEDOR
        divMetasAlcancadasSucesso.Visible = False
        divMetasAlcancadasErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()

        Con.ExecutarQuery("UPDATE TB_CABECALHO_COMISSAO_VENDEDOR_META SET ID_USUARIO_VALIDACAO = " & Session("ID_USUARIO") & ", DT_VALIDACAO =  GETDATE() WHERE ID_CABECALHO_COMISSAO_VENDEDOR_META = " & txtIDMetasAlcancadas.Text)
        Con.ExecutarQuery("UPDATE A SET A.VL_META  =  B.VL_ADICIONAL FROM [NVOCCHOM].[dbo].[TB_DETALHE_COMISSAO_VENDEDOR] A INNER JOIN TB_DETALHE_COMISSAO_VENDEDOR_META B ON A.ID_BL = B.ID_BL WHERE ID_CABECALHO_COMISSAO_VENDEDOR_META = " & txtIDMetasAlcancadas.Text & " AND VL_ADICIONAL <> 0 ")
        lblMetasAlcancadasSucesso.Text = "Dados validados com sucesso!"
        divMetasAlcancadasSucesso.Visible = True
        mpeGerarMetasAlcancadas.Show()
    End Sub

    Private Sub btnFecharMetasAlcancadas_Click(sender As Object, e As EventArgs) Handles btnFecharMetasAlcancadas.Click
        divMetasAlcancadasSucesso.Visible = False
        divMetasAlcancadasErro.Visible = False
        divConteudoDinamico.InnerHtml = ""
        txtDataInicioMetasAlcancadas.Text = ""
        txtDataTerminoMetasAlcancadas.Text = ""
        txtIDMetasAlcancadas.Text = ""
        mpeGerarMetasAlcancadas.Hide()
    End Sub


End Class
