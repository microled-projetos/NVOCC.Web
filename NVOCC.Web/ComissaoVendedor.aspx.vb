
Imports System.Windows.Input

Public Class ComissaoVendedor
    Inherits System.Web.UI.Page
    Dim filtro As String = ""
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

    Protected Sub dgvComissoes_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        divSuccess.Visible = False
        divErro.Visible = False
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvComissoes.DataSource = Session("TaskTable")
            CarregaGrid()
            dgvComissoes.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Private Sub dgvComissoes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvComissoes.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        CarregaGrid()


        If e.CommandName = "Selecionar" Then
            If txtlinha.Text <> "" Then
                dgvComissoes.Rows(txtlinha.Text).CssClass = "Normal"

            End If
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")

            dgvComissoes.Rows(txtlinha.Text).CssClass = "selected1"

            lkAjustarComissao.Visible = True
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT A.ID_CABECALHO_COMISSAO_VENDEDOR ,B.ID_DETALHE_COMISSAO_VENDEDOR ,B.NR_PROCESSO,B.NR_NOTAS_FISCAL,B.DT_NOTA_FISCAL,B.ID_SERVICO,B.ID_PARCEIRO_CLIENTE,B.ID_PARCEIRO_VENDEDOR,B.ID_TIPO_ESTUFAGEM,B.VL_COMISSAO_BASE,B.QT_BL,B.QT_CNTR,B.VL_PERCENTUAL,B.VL_COMISSAO_TOTAL,B.DT_LIQUIDACAO,B.DS_OBSERVACAO,A.DT_EXPORTACAO
FROM            dbo.TB_CABECALHO_COMISSAO_VENDEDOR AS A LEFT OUTER JOIN
                         dbo.TB_DETALHE_COMISSAO_VENDEDOR AS B ON B.ID_CABECALHO_COMISSAO_VENDEDOR = A.ID_CABECALHO_COMISSAO_VENDEDOR
						 WHERE B.ID_DETALHE_COMISSAO_VENDEDOR = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DETALHE_COMISSAO_VENDEDOR")) Then
                    txtIDAjuste.Text = ds.Tables(0).Rows(0).Item("ID_DETALHE_COMISSAO_VENDEDOR")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                    txtAjusteProcesso.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTAS_FISCAL")) Then
                    txtAjusteNotaFiscal.Text = ds.Tables(0).Rows(0).Item("NR_NOTAS_FISCAL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_NOTA_FISCAL")) Then
                    txtAjusteDataNota.Text = ds.Tables(0).Rows(0).Item("DT_NOTA_FISCAL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                    ddlAjusteServico.SelectedValue = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_VENDEDOR")) Then
                    ddlAjusteVendedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_VENDEDOR")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")) Then
                    ddlAjusteCliente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                    ddlAjusteEstufagem.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMISSAO_BASE")) Then
                    txtAjusteBase.Text = ds.Tables(0).Rows(0).Item("VL_COMISSAO_BASE")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_BL")) Then
                    txtAjusteQtdBl.Text = ds.Tables(0).Rows(0).Item("QT_BL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_CNTR")) Then
                    txtAjusteQtdCNTR.Text = ds.Tables(0).Rows(0).Item("QT_CNTR")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PERCENTUAL")) Then
                    txtAjustePorcentagem.Text = ds.Tables(0).Rows(0).Item("VL_PERCENTUAL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMISSAO_TOTAL")) Then
                    txtAjusteTotal.Text = ds.Tables(0).Rows(0).Item("VL_COMISSAO_TOTAL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) Then
                    txtAjusteLiquidacao.Text = ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DS_OBSERVACAO")) Then
                    txtAjusteObs.Text = ds.Tables(0).Rows(0).Item("DS_OBSERVACAO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EXPORTACAO")) Then
                    lkAjustarComissao.Visible = False
                Else
                    lkAjustarComissao.Visible = True

                End If
            End If
            Con.Fechar()

        End If
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        CarregaGrid()
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

    Sub CarregaGrid()
        divSuccess.Visible = False
        divErro.Visible = False
        lkAjustarComissao.Visible = True

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

            dsComissao.SelectCommand = "SELECT * FROM [dbo].[View_Comissao_Vendedor] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' " & filtro & " ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO"
            dgvComissoes.DataBind()

            DivGrid2.Visible = True
            lblCompetenciaCCProcesso.Text = txtCompetencia.Text
        End If
    End Sub
    'Private Sub dgvTabelaComissao_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTabelaComissao.RowCommand
    '    DivExcluir.Visible = False
    '    divInfo.Visible = False
    '    Dim ID As String = e.CommandArgument
    '    Dim Con As New Conexao_sql
    '    Con.Conectar()
    '    If e.CommandName = "Excluir" Then

    '        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
    '        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
    '            lblErroExcluir.Text = "Usuário não tem permissão para realizar exclusões"
    '            DivExcluir.Visible = True
    '        Else
    '            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_TAXA_COMISSAO] WHERE ID_VENDEDOR_TAXA_COMISSAO =" & ID)
    '            dgvTabelaComissao.DataBind()
    '            divInfo.Visible = True
    '            lblInfo.Text = "Taxa excluída com sucesso"
    '            mpeTabelas.Show()
    '        End If

    '    ElseIf e.CommandName = "Editar" Then

    '        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_VENDEDOR_TAXA_COMISSAO,DT_VALIDADE_INICIAL,ID_TIPO_ESTUFAGEM,ID_VIATRANSPORTE,ID_TIPO_CALCULO,ID_BASE_CALCULO_TAXA ,VL_TAXA
    '  ,VL_PROFIT_INICIO,VL_PROFIT_FIM,VL_COMISSAO FROM TB_VENDEDOR_TAXA_COMISSAO WHERE ID_VENDEDOR_TAXA_COMISSAO = " & ID)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VENDEDOR_TAXA_COMISSAO")) Then
    '                txtIDTabelaTaxa.Text = ds.Tables(0).Rows(0).Item("ID_VENDEDOR_TAXA_COMISSAO").ToString()
    '            End If
    '            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")) Then
    '                txtValidade.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL").ToString()
    '            End If
    '            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
    '                txtLCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA").ToString()
    '            End If
    '            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_FCL")) Then
    '                txtFCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_FCL").ToString()
    '            End If


    '        End If

    '        mpeTabelas.Show()
    '    End If
    '    Con.Fechar()
    'End Sub


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


    Private Sub txtLiquidacaoInicial_TextChanged(sender As Object, e As EventArgs) Handles txtLiquidacaoInicial.TextChanged
        'divErroGerarComissao.Visible = False

        'Dim Con As New Conexao_sql
        'Con.Conectar()
        'Dim ds As DataSet = Con.ExecutarQuery("SELECT TOP 1 ID_TAXA_COMISSAO_VENDEDORES,VL_TAXA_FCL,VL_TAXA_LCL FROM TB_TAXA_COMISSAO_VENDEDOR WHERE CONVERT(DATETIME,DT_VALIDADE_INICIAL,103) <= CONVERT(DATETIME,'" & txtLiquidacaoInicial.Text & "',103)")
        'If ds.Tables(0).Rows.Count > 0 Then
        '    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_FCL")) Then
        '        lblFCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_FCL")
        '    End If

        '    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_LCL")) Then
        '        lblLCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_LCL")
        '    End If

        'Else
        '    lblErroGerarComissao.Text = "Não há tabela de taxa cadastrada!"
        '    divErroGerarComissao.Visible = True
        'End If
        'VerificaCompetencia()

        'ModalPopupExtender3.Show()
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

    Private Sub btnGerarComissao_Click(sender As Object, e As EventArgs) Handles btnGerarComissao.Click
        btnGerarComissao.Visible = False
        divSuccess.Visible = False
        divErro.Visible = False
        divAtencaoGerarComissao.Visible = False
        divSuccessGerarComissao.Visible = False
        divErroGerarComissao.Visible = False
        divInfoGerarComissao.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        If lblCompetenciaSobrepor.Text = "" Then
            lblCompetenciaSobrepor.Text = 0
        End If

        If lblContasReceber.Text = "" Then
            lblContasReceber.Text = 0
        End If

        If txtNovaCompetencia.Text = "" Or txtLiquidacaoInicial.Text = "" Or txtLiquidacaoFinal.Text = "" Then
            lblErroGerarComissao.Text = "Preencha os campos obrigatórios."
            divErroGerarComissao.Visible = True

        Else
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroGerarComissao.Text = "Usuário não tem permissão!"
                divErroGerarComissao.Visible = True
            Else
                Dim dsQtd As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND FL_VENDEDOR_DIRETO =1 ")
                If dsQtd.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroGerarComissao.Text = "Não há processos em aberto para comissão nesse período!"
                    divErroGerarComissao.Visible = True
                Else

                    Dim competencia As String = txtNovaCompetencia.Text

                    Dim dsInsert As DataSet = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_VENDEDOR (DT_COMPETENCIA,ID_USUARIO_GERACAO,DT_GERACAO,DT_LIQUIDACAO_INICIAL ,DT_LIQUIDACAO_FINAL ) VALUES('" & competencia.Replace("/", "") & "'," & Session("ID_USUARIO") & ", getdate(),CONVERT(DATE,'" & txtLiquidacaoInicial.Text & "',103),CONVERT(DATE,'" & txtLiquidacaoFinal.Text & "',103)) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_VENDEDOR  ")
                    Dim cabecalho As String = dsInsert.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_VENDEDOR")


                    ds = Con.ExecutarQuery("SELECT A.ID_BL, A.ID_TIPO_ESTUFAGEM, A.QT_CNTR,A.ID_SERVICO,B.ID_VIATRANSPORTE,NULL AS DT_PAGAMENTO_EXP FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') A INNER JOIN TB_SERVICO B ON B.ID_SERVICO = A.ID_SERVICO WHERE A.DT_PAGAMENTO_EXP IS NULL AND A.FL_VENDEDOR_DIRETO = 1 ")
                    If ds.Tables(0).Rows.Count > 0 Then

                        For Each linha As DataRow In ds.Tables(0).Rows

                            If Not IsDBNull(linha.Item("ID_BL")) Then

                                Dim consulta As DataSet

                                '1)
                                Dim DespesaDestinoAberta As String = 0
                                consulta = Con.ExecutarQuery(" SELECT ISNULL(SUM(VL_TAXA_CALCULADO),0)VL_TAXA_CALCULADO FROM TB_BL_TAXA WHERE ID_BL =" & linha.Item("ID_BL") & " AND CD_PR ='R' AND ID_BL_TAXA NOT IN (SELECT ISNULL(ID_BL_TAXA,0) FROM View_Taxa_Bloqueada)")
                                If consulta.Tables(0).Rows.Count > 0 Then
                                    DespesaDestinoAberta = consulta.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")
                                End If

                                '2)
                                Dim FreteVenda As String = 0
                                consulta = Con.ExecutarQuery("SELECT  ISNULL(VL_TAXA_CALCULADO,0)VL_TAXA_CALCULADO FROM TB_BL_TAXA WHERE ID_BL =" & linha.Item("ID_BL") & " AND CD_PR ='R' AND ID_ITEM_DESPESA = 14")
                                If consulta.Tables(0).Rows.Count > 0 Then
                                    FreteVenda = consulta.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")
                                End If

                                Dim TaxasCompra As String = 0
                                consulta = Con.ExecutarQuery("SELECT  ISNULL(SUM(VL_TAXA_CALCULADO),0)VL_TAXA_CALCULADO FROM TB_BL_TAXA WHERE ID_BL =" & linha.Item("ID_BL") & " AND CD_PR ='P' AND ID_ITEM_DESPESA IN ( 14,134,510,1733)")
                                If consulta.Tables(0).Rows.Count > 0 Then
                                    TaxasCompra = consulta.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")
                                End If

                                Dim ProfitBL As String = 0
                                consulta = Con.ExecutarQuery("SELECT  ISNULL(VL_PROFIT_DIVISAO_CALCULADO,0)VL_PROFIT_DIVISAO_CALCULADO FROM TB_BL WHERE ID_BL =" & linha.Item("ID_BL") & " ")
                                If consulta.Tables(0).Rows.Count > 0 Then
                                    ProfitBL = consulta.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO_CALCULADO")
                                End If



                                Dim ProfitBrasil As Decimal = Convert.ToDecimal(FreteVenda) - (Convert.ToDecimal(TaxasCompra) + Convert.ToDecimal(ProfitBL))

                                '3)
                                Dim DespesaOrigemAberta As String = 0
                                consulta = Con.ExecutarQuery("SELECT  ISNULL(SUM(VL_TAXA_CALCULADO),0)VL_TAXA_CALCULADO FROM TB_BL_TAXA WHERE ID_BL =" & linha.Item("ID_BL") & " AND CD_PR ='R' AND ID_BL_TAXA NOT IN (SELECT ISNULL(ID_BL_TAXA,0) FROM View_Taxa_Bloqueada)")
                                If consulta.Tables(0).Rows.Count > 0 Then
                                    DespesaOrigemAberta = consulta.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")
                                End If


                                '4)
                                Dim RealProfitBrasil As Decimal = Convert.ToDecimal(ProfitBrasil) - Convert.ToDecimal(DespesaDestinoAberta) - Convert.ToDecimal(DespesaOrigemAberta)


                                '5)
                                If linha.Item("ID_TIPO_ESTUFAGEM") = "1" Then
                                    RealProfitBrasil = RealProfitBrasil / linha.Item("QT_CNTR")
                                End If

                                Dim Valor As String = RealProfitBrasil


                                Dim VL_COMISSAO As String = ""

                                Dim VL_COMISSAO_TOTAL As Decimal = 0

                                If RealProfitBrasil < 0 Then

                                    VL_COMISSAO = 20

                                    VL_COMISSAO_TOTAL = Convert.ToDecimal(VL_COMISSAO) * linha.Item("QT_CNTR")

                                    Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,ID_ANALISTA_COTACAO, VL_PROFIT_REAL_BRASIL)
SELECT " & cabecalho & ", NR_NOTA_FISCAL, DT_NOTA_FISCAL, NR_PROCESSO, ID_SERVICO, ID_BL, ID_PARCEIRO_VENDEDOR, ID_PARCEIRO_CLIENTE, ID_TIPO_ESTUFAGEM, QT_BL, QT_CNTR, " & VL_COMISSAO.Replace(",", ".") & " , " & VL_COMISSAO_TOTAL.ToString.Replace(",", ".") & " , DT_LIQUIDACAO, ISNULL(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO," & Valor.Replace(",", ".") & " FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE ID_BL = " & linha.Item("ID_BL"))

                                Else

                                    Dim BaseCaculo As DataSet = Con.ExecutarQuery("SELECT VL_COMISSAO, ID_TIPO_CALCULO, ID_BASE_CALCULO_TAXA FROM TB_VENDEDOR_TAXA_COMISSAO WHERE ID_TIPO_ESTUFAGEM = " & linha.Item("ID_TIPO_ESTUFAGEM") & " AND ID_VIATRANSPORTE = " & linha.Item("ID_VIATRANSPORTE") & " AND DT_VALIDADE_INICIAL <= GETDATE() AND (( " & Valor.Replace(",", ".") & " BETWEEN VL_PROFIT_INICIO AND VL_PROFIT_FIM ) OR (" & Valor.Replace(",", ".") & " > VL_PROFIT_INICIO AND VL_PROFIT_FIM IS NULL)) ")
                                    If BaseCaculo.Tables(0).Rows.Count > 0 Then

                                        VL_COMISSAO = BaseCaculo.Tables(0).Rows(0).Item("VL_COMISSAO")

                                        VL_COMISSAO_TOTAL = Convert.ToDecimal(VL_COMISSAO) * linha.Item("QT_CNTR")


                                        Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,ID_ANALISTA_COTACAO,VL_PROFIT_REAL_BRASIL)
SELECT " & cabecalho & ", NR_NOTA_FISCAL, DT_NOTA_FISCAL, NR_PROCESSO, ID_SERVICO, ID_BL, ID_PARCEIRO_VENDEDOR, ID_PARCEIRO_CLIENTE, ID_TIPO_ESTUFAGEM, QT_BL, QT_CNTR, " & VL_COMISSAO.Replace(",", ".") & " , " & VL_COMISSAO_TOTAL.ToString.Replace(",", ".") & " , DT_LIQUIDACAO, ISNULL(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO," & Valor.Replace(",", ".") & " FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE ID_BL = " & linha.Item("ID_BL"))

                                    Else

                                    End If

                                End If

                            End If

                        Next
                    End If

                    '  CalcularMetas(cabecalho)


                    divErro.Visible = False
                    divSuccessGerarComissao.Visible = True
                    lblSuccessGerarComissao.Text = "Comissão gerada com sucesso!"
                End If
            End If

        End If


        btnGerarComissao.Visible = True
        ModalPopupExtender3.Show()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefault()", True)

    End Sub

    Private Sub txtNovaCompetencia_TextChanged(sender As Object, e As EventArgs) Handles txtNovaCompetencia.TextChanged
        ' VerificaCompetencia()
    End Sub

    '    Sub CalcularMetas(cabecalho As Integer)
    '        Dim Con As New Conexao_sql
    '        Con.Conectar()
    '        Dim ds As DataSet = Con.ExecutarQuery("SELECT A.ID_PARCEIRO_VENDEDOR, A.ID_TIPO_ESTUFAGEM, B.ID_VIATRANSPORTE, A.ID_SERVICO, COUNT(*)QTD FROM TB_DETALHE_COMISSAO_VENDEDOR A
    'INNER JOIN TB_SERVICO B ON A.ID_SERVICO = B.ID_SERVICO 
    'GROUP BY A.ID_PARCEIRO_VENDEDOR, A.ID_TIPO_ESTUFAGEM, B.ID_VIATRANSPORTE, A.ID_SERVICO 
    'ORDER BY QTD DESC")
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            For Each linha As DataRow In ds.Tables(0).Rows

    '                If Not IsDBNull(linha.Item("ID_PARCEIRO_VENDEDOR")) Then
    '                    Dim dsConsulta As DataSet = Con.ExecutarQuery("SELECT VL_META FROM TB_VENDEDOR_CADASTRO_METAS WHERE DT_VALIDADE_INICIAL <= GETDATE() AND ID_TIPO_ESTUFAGEM = " & linha.Item("ID_TIPO_ESTUFAGEM") & " AND ID_VIATRANSPORTE = " & linha.Item("ID_VIATRANSPORTE") & " AND " & linha.Item("QTD") & " BETWEEN META_MIN AND META_MAX ")

    '                    If dsConsulta.Tables(0).Rows.Count > 0 Then
    '                        If Not IsDBNull(dsConsulta.Tables(0).Rows(0).Item("VL_META")) Then
    '                            Con.ExecutarQuery("UPDATE A SET 
    'A.VL_META = " & dsConsulta.Tables(0).Rows(0).Item("VL_META").ToString.Replace(",", ".") & " , 
    'A.VL_COMISSAO_TOTAL = A.VL_COMISSAO_TOTAL + " & dsConsulta.Tables(0).Rows(0).Item("VL_META").ToString.Replace(",", ".") & "
    'FROM TB_DETALHE_COMISSAO_VENDEDOR AS A
    'INNER JOIN TB_SERVICO AS B ON A.ID_SERVICO = B.ID_SERVICO 
    'WHERE A.ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & " AND A.ID_TIPO_ESTUFAGEM = " & linha.Item("ID_TIPO_ESTUFAGEM") & " AND B.ID_VIATRANSPORTE = " & linha.Item("ID_VIATRANSPORTE") & " AND A.ID_PARCEIRO_VENDEDOR = " & linha.Item("ID_PARCEIRO_VENDEDOR"))

    '                        End If
    '                    End If
    '                End If
    '            Next
    '        End If
    '    End Sub

    Sub SubVendedor(cabecalho As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT A.ID_PARCEIRO_SUB_VENDEDOR,A.ID_PARCEIRO_VENDEDOR,A.VL_TAXA_FIXA,A.VL_PERCENTUAL,A.ID_BASE_CALCULO
FROM TB_SUB_VENDEDOR A
INNER JOIN (
SELECT ID_PARCEIRO_VENDEDOR, ID_PARCEIRO_SUB_VENDEDOR, MAX(DT_VALIDADE_INICIAL) AS DT_VALIDADE_INICIAL
FROM TB_SUB_VENDEDOR
WHERE CONVERT(DATE,DT_VALIDADE_INICIAL,103) <= CONVERT(DATE,'" & txtLiquidacaoInicial.Text & "',103)
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

FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "')
WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR =1 AND FL_VENDEDOR_DIRETO = 0 AND FL_ATIVO = 1)  AND
ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_SUB_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR Not IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & "))")

                        ElseIf Base_Calculo = 32 Then
                            'POR HBL - Muplica por um

                            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB,ID_ANALISTA_COTACAO )
SELECT " & cabecalho & ",NR_NOTA_FISCAL, DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR, " & TaxaFixa & ",  " & TaxaFixa & ", DT_LIQUIDACAO,1, isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO 
FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "')
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

FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') A
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
        ds = Con.ExecutarQuery("SELECT ID_BL,(SELECT ID_PARCEIRO_EQUIPE_INSIDE FROM TB_PARAMETROS)ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,NR_PROCESSO,ID_SERVICO,ID_TIPO_ESTUFAGEM,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO,ISNULL(QT_BL,0)QT_BL,ISNULL(QT_CNTR,0)QT_CNTR FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO =1 AND FL_ATIVO = 1) AND ID_ANALISTA_COTACAO IN (SELECT ID_USUARIO_MEMBRO_EQUIPE FROM TB_EQUIPE_MEMBROS)")

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
    Sub VerificaCompetencia()
        Dim Con As New Conexao_sql
        Con.Conectar()

        'Verifica se a competencia já existe
        Dim ds As DataSet = Con.ExecutarQuery("Select ID_CABECALHO_COMISSAO_VENDEDOR,DT_EXPORTACAO FROM View_Comissao_Vendedor WHERE COMPETENCIA = '" & txtNovaCompetencia.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            divAtencaoGerarComissao.Visible = True
            lblAtencaoGerarComissao.Text = "COMPETENCIA JÁ EXISTE!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
            lblCompetenciaSobrepor.Text = ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_VENDEDOR")
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EXPORTACAO")) Then
                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CVEND' AND DT_COMPETENCIA = '" & txtNovaCompetencia.Text & "'")
                If dsAuxiliar.Tables(0).Rows.Count > 0 Then
                    lblContasReceber.Text = dsAuxiliar.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
                Else
                    lblContasReceber.Text = 0
                End If
            End If
        Else
            lblCompetenciaSobrepor.Text = 0
            lblContasReceber.Text = 0
            divAtencaoGerarComissao.Visible = False
        End If


        'Verifica se a competencia anterior foi exportada
        Dim Competencia_Nova As String = "01/" & txtNovaCompetencia.Text
        Dim Variavel_Auxiliar As Date = Competencia_Nova
        Variavel_Auxiliar = Variavel_Auxiliar.AddMonths(-1)
        Dim Competencia_Anterior As String = Variavel_Auxiliar.ToString()
        Competencia_Anterior = Competencia_Anterior.Substring(3, 7)
        ds = Con.ExecutarQuery("SELECT ID_CABECALHO_COMISSAO_VENDEDOR FROM View_Comissao_Vendedor WHERE COMPETENCIA = '" & Competencia_Anterior.ToString() & "' AND DT_EXPORTACAO IS NULL")
        If ds.Tables(0).Rows.Count > 0 Then
            divInfoGerarComissao.Visible = True
            lblInfoGerarComissao.Text = "Competência imediatamente anterior não exportada para a conta corrente do processo."
        Else
            divInfoGerarComissao.Visible = False
        End If

        ModalPopupExtender3.Show()

    End Sub

    Sub VerificaCCPRocesso()
        Dim Con As New Conexao_sql
        Con.Conectar()

        'Verifica se a competencia já existe
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CVEND' AND DT_COMPETENCIA = '" & txtCompetencia.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            divInfoCCProcesso.Visible = True
            lblInfoCCProcesso.Text = "COMPETENCIA JÁ EXPORTADA!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
            lblContasReceber.Text = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
        Else
            lblContasReceber.Text = 0
            divInfoCCProcesso.Visible = False
        End If
        ModalPopupExtender6.Show()
    End Sub


    Private Sub btnFecharGerarComissao_Click(sender As Object, e As EventArgs) Handles btnFecharGerarComissao.Click
        divAtencaoGerarComissao.Visible = False
        divSuccessGerarComissao.Visible = False
        divErroGerarComissao.Visible = False
        divInfoGerarComissao.Visible = False
        txtLiquidacaoInicial.Text = ""
        txtLiquidacaoFinal.Text = ""
        txtNovaCompetencia.Text = ""
        ModalPopupExtender3.Hide()
    End Sub

    Private Sub btnAlteraComisaao_Click(sender As Object, e As EventArgs) Handles btnAlteraComisaao.Click
        divSuccesAjuste.Visible = False
        divErroAjuste.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroAjuste.Visible = True
            lblErroAjuste.Text = "Usuário não possui permissão!"
            Exit Sub

        Else
            If txtIDAjuste.Text = "" Then

                lblErroAjuste.Text = "É necessario selecionar um registro para alterar."
                divErroAjuste.Visible = True

            ElseIf txtAjusteProcesso.Text = "" Or txtAjusteNotaFiscal.Text = "" Or txtAjusteDataNota.Text = "" Or ddlAjusteServico.SelectedValue = 0 Or ddlAjusteVendedor.SelectedValue = 0 Or ddlAjusteCliente.SelectedValue = 0 Or ddlAjusteEstufagem.SelectedValue = 0 Or txtAjusteBase.Text = "" Or txtAjusteQtdBl.Text = "" Or txtAjusteQtdCNTR.Text = "" Or txtAjustePorcentagem.Text = "" Or txtAjusteTotal.Text = "" Or txtAjusteLiquidacao.Text = "" Then

                lblErroAjuste.Text = "Preencha os campos obrigatórios."
                divErroAjuste.Visible = True
            Else
                If txtAjusteObs.Text = "" Then
                    txtAjusteObs.Text = "NULL"
                Else
                    txtAjusteObs.Text = "'" & txtAjusteObs.Text & "'"
                End If



                txtAjusteBase.Text = txtAjusteBase.Text.Replace(".", "")
                txtAjusteBase.Text = txtAjusteBase.Text.Replace(",", ".")


                txtAjustePorcentagem.Text = txtAjustePorcentagem.Text.Replace(".", "")
                txtAjustePorcentagem.Text = txtAjustePorcentagem.Text.Replace(",", ".")


                txtAjusteTotal.Text = txtAjusteTotal.Text.Replace(".", "")
                txtAjusteTotal.Text = txtAjusteTotal.Text.Replace(",", ".")




                Con.ExecutarQuery("UPDATE TB_DETALHE_COMISSAO_VENDEDOR SET NR_PROCESSO = '" & txtAjusteProcesso.Text & "',NR_NOTAS_FISCAL = '" & txtAjusteNotaFiscal.Text & "',DT_NOTA_FISCAL = CONVERT(DATE,'" & txtAjusteDataNota.Text & "',103),ID_SERVICO = " & ddlAjusteServico.SelectedValue & ",ID_PARCEIRO_CLIENTE = " & ddlAjusteCliente.SelectedValue & ",ID_PARCEIRO_VENDEDOR= " & ddlAjusteVendedor.SelectedValue & ",ID_TIPO_ESTUFAGEM = " & ddlAjusteEstufagem.SelectedValue & ",VL_COMISSAO_BASE = " & txtAjusteBase.Text & ",QT_BL = " & txtAjusteQtdBl.Text & ",QT_CNTR = " & txtAjusteQtdCNTR.Text & ",VL_PERCENTUAL = " & txtAjustePorcentagem.Text & ",VL_COMISSAO_TOTAL= " & txtAjusteTotal.Text & ",DT_LIQUIDACAO =  CONVERT(DATE,'" & txtAjusteLiquidacao.Text & "',103), DS_OBSERVACAO = " & txtAjusteObs.Text & " WHERE ID_DETALHE_COMISSAO_VENDEDOR = " & txtIDAjuste.Text)
                divSuccesAjuste.Visible = True
                lblSuccesAjuste.Text = "Comissão alterada com sucesso!"

                txtAjusteObs.Text = txtAjusteObs.Text.Replace("NULL", "")
                txtAjusteObs.Text = txtAjusteObs.Text.Replace("'", "")

            End If

        End If
        ModalPopupExtender5.Show()

    End Sub

    Private Sub btnExcluirAlteraComisaao_Click(sender As Object, e As EventArgs) Handles btnExcluirAlteraComisaao.Click
        divSuccesAjuste.Visible = False
        divErroAjuste.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroAjuste.Visible = True
            lblErroAjuste.Text = "Usuário não possui permissão!"
            ModalPopupExtender5.Show()
            Exit Sub

        Else
            If txtIDAjuste.Text = "" Then

                divErroAjuste.Visible = True
                lblErroAjuste.Text = "É necessario selecionar um registro para excluir"
                ModalPopupExtender5.Show()
            Else
                Con.ExecutarQuery("DELETE FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_DETALHE_COMISSAO_VENDEDOR = " & txtIDAjuste.Text)
                divSuccess.Visible = True
                lblmsgSuccess.Text = "Comissão deletada com sucesso!"
                dgvComissoes.DataBind()
            End If

        End If

    End Sub

    Private Sub btnFecharAlteraComisaao_Click(sender As Object, e As EventArgs) Handles btnFecharAlteraComisaao.Click
        txtAjusteProcesso.Text = ""
        txtAjusteNotaFiscal.Text = ""
        txtAjusteDataNota.Text = ""
        ddlAjusteServico.SelectedValue = 0
        ddlAjusteVendedor.SelectedValue = 0
        ddlAjusteCliente.SelectedValue = 0
        ddlAjusteEstufagem.SelectedValue = 0
        txtAjusteBase.Text = ""
        txtAjusteQtdBl.Text = ""
        txtAjusteQtdCNTR.Text = ""
        txtAjustePorcentagem.Text = ""
        txtAjusteTotal.Text = ""
        txtAjusteLiquidacao.Text = ""
        txtAjusteObs.Text = ""
        txtID.Text = ""
        txtIDAjuste.Text = ""
        lkAjustarComissao.Visible = False
        divSuccesAjuste.Visible = False
        divErroAjuste.Visible = False


        For i As Integer = 0 To dgvComissoes.Rows.Count - 1
            dgvComissoes.Rows(i).CssClass = "Normal"

        Next
    End Sub


    Private Sub lkGravarCCProcesso_Click(sender As Object, e As EventArgs) Handles lkGravarCCProcesso.Click
        divErroCCProcesso.Visible = False
        divSuccess.Visible = False
        divErro.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtLiquidacaoCCProcesso.Text = "" Then

            divErroCCProcesso.Visible = True
            lblErroCCProcesso.Text = "É necessario informar a data de liquidação!"
            ModalPopupExtender6.Show()
        ElseIf lblCompetenciaCCProcesso.Text = "" Then

            divErroCCProcesso.Visible = True
            lblErroCCProcesso.Text = "É necessario pesquisar uma competência!"
            ModalPopupExtender6.Show()

        Else

            If lblContasReceber.Text <> 0 Then
                Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
                Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
            End If

            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (CD_PR, DT_COMPETENCIA, DT_LANCAMENTO,DT_LIQUIDACAO ,DT_VENCIMENTO,ID_TIPO_LANCAMENTO_CAIXA  ,
ID_USUARIO_LANCAMENTO ,ID_USUARIO_LIQUIDACAO,TP_EXPORTACAO) VALUES('P','" & txtCompetencia.Text & "',GETDATE(),CONVERT(DATE,'" & txtLiquidacaoCCProcesso.Text & "',103),CONVERT(DATE,'" & txtLiquidacaoCCProcesso.Text & "',103),7, " & Session("ID_USUARIO") & ", " & Session("ID_USUARIO") & ",'CVEND')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER_ITENS")
            Dim ID_CONTAS_PAGAR_RECEBER_ITENS As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")

            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_PARCEIRO_EMPRESA,DS_HISTORICO_LANCAMENTO,ID_CONTA_PAGAR_RECEBER, VL_LANCAMENTO ,VL_LIQUIDO,ID_ITEM_DESPESA,VL_TAXA_CALCULADO,ID_MOEDA, ID_BL )
               SELECT ID_PARCEIRO_VENDEDOR,'COMISSÃO VENDEDOR – " & txtCompetencia.Text & "'," & ID_CONTAS_PAGAR_RECEBER_ITENS & ",VL_COMISSAO_TOTAL, VL_COMISSAO_TOTAL, (SELECT ID_ITEM_VENDEDOR FROM TB_PARAMETROS)ID_ITEM_VENDEDOR,VL_COMISSAO_TOTAL,124,ID_BL  FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR in (SELECT distinct ID_CABECALHO_COMISSAO_VENDEDOR FROM View_Comissao_Vendedor WHERE COMPETENCIA = '" & txtCompetencia.Text & "')")


            Con.ExecutarQuery("UPDATE TB_CABECALHO_COMISSAO_VENDEDOR SET DT_EXPORTACAO =  GETDATE(), ID_USUARIO_EXPORTACAO = " & Session("ID_USUARIO") & "   WHERE ID_CABECALHO_COMISSAO_VENDEDOR in (SELECT distinct ID_CABECALHO_COMISSAO_VENDEDOR FROM View_Comissao_Vendedor WHERE COMPETENCIA = '" & txtCompetencia.Text & "')")


            CarregaGrid()
            divSuccess.Visible = True
            lblmsgSuccess.Text = "Comissão exportada para o processo com sucesso!"
            ModalPopupExtender6.Hide()
            txtLiquidacaoCCProcesso.Text = ""
            divInfoCCProcesso.Visible = False

        End If


    End Sub

    Private Sub txtCompetencia_TextChanged(sender As Object, e As EventArgs) Handles txtCompetencia.TextChanged
        lblCompetenciaCCProcesso.Text = txtCompetencia.Text
    End Sub

    Private Sub txtLiquidacaoCCProcesso_TextChanged(sender As Object, e As EventArgs) Handles txtLiquidacaoCCProcesso.TextChanged
        'VerificaCCPRocesso()
    End Sub

    Private Sub btnFecharCCProcesso_Click(sender As Object, e As EventArgs) Handles btnFecharCCProcesso.Click
        txtLiquidacaoCCProcesso.Text = ""
        divInfoCCProcesso.Visible = False
        divErroCCProcesso.Visible = False

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

    'Public Sub txtDtInicioComissaoProspecao_TextChanged(sender As Object, e As EventArgs) Handles txtDtInicioComissaoProspecao.TextChanged
    '    Dim DtInicio As String = txtDtInicioComissaoProspecao.Text

    '    If Not IsDate(Convert.ToDateTime(DtInicio)) Then
    '        lblErroDataInicio.Text = "Data Inválida"
    '    End If

    'End Sub


    Public Sub btnFecharRelComissaoProspecao_Click(sender As Object, e As EventArgs) Handles btnFecharRelComissaoProspecao.Click

        UpdateRelComissaoProspecao.Visible = False
        txtDtInicioComissaoProspecao.Text = String.Empty
        txtDtTerminoComissaoProspecao.Text = String.Empty

    End Sub

    Public Sub btnFecharRelComissaoIndicacaoInterna_Click(sender As Object, e As EventArgs) Handles btnFecharRelComissaoIndicacaoInterna.Click
        UpdateRelComissaoIndicacaoInterna.Visible = False
        txtDtInicioRelIndicacaoInterna.Text = String.Empty
        txtDtTerminoRelIndicacaoInterna.Text = String.Empty

    End Sub

    Private Sub dgvTabelaComissaoVendedor_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTabelaComissaoVendedor.RowCommand
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
                dgvTabelaComissaoVendedor.DataBind()
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

    Public Sub LimparComissaoVenda()
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


    Private Sub btnSalvarComissaoVendedor_Click(sender As Object, e As EventArgs) Handles btnSalvarComissaoVendedor.Click
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
            dgvTabelaComissaoVendedor.DataBind()
            mpeTabelas.Show()
            LimparComissaoProspeccao()
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
            dgvTabelaComissaoVendedor.DataBind()
            mpeTabelas.Show()

        End If
    End Sub

    Private Sub btnFecharComissoesVenda_Click(sender As Object, e As EventArgs) Handles btnFecharComissoesVenda.Click
        LimparComissaoVenda()
        mpeTabelas.Hide()
    End Sub

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

    Private Sub btnLimparGeradorMetasAlcancadas_Click(sender As Object, e As EventArgs) Handles btnLimparGeradorMetasAlcancadas.Click

    End Sub

    Private Sub btnGerarMetasAlcancadas_Click(sender As Object, e As EventArgs) Handles btnGerarMetasAlcancadas.Click
        'EXIBE RELATORIO NA TELA PARA VALIDACAO
        divMetasAlcancadasSucesso.Visible = False
        divMetasAlcancadasErro.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtDataInicioMetasAlcancadas.Text = "" Or txtDataTerminoMetasAlcancadas.Text = "" Then
            lblErroGerarComissao.Text = "Preencha os campos obrigatórios."
            divErroGerarComissao.Visible = True
        Else

        End If
    End Sub

    Private Sub btnValidarMetasAlcancadas_Click(sender As Object, e As EventArgs) Handles btnValidarMetasAlcancadas.Click
        ''GRAVA NA TABELA
    End Sub

    Private Sub btnFecharMetasAlcancadas_Click(sender As Object, e As EventArgs) Handles btnFecharMetasAlcancadas.Click

    End Sub
End Class
