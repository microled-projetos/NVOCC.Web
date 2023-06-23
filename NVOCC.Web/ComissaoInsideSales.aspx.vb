
Imports System.Windows.Input

Public Class ComissaoInsideSales
    Inherits System.Web.UI.Page
    Dim filtro As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2068 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()

        If Not Page.IsPostBack Then
            CarregaEquipes()
        End If

    End Sub

    Sub CarregaEquipes()
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim tabela As String = "<select data-live-search='True' ID='ddlEquipes' data-live-search-style='startsWith' class='form-group selectpicker' multiple='multiple' onchange='IDEquipe()'  title='Selecione'>"
        Dim dsdados As DataSet
        Dim sql As String = "SELECT ID_EQUIPE, NM_EQUIPE FROM TB_INSIDE_EQUIPE ORDER BY NM_EQUIPE "

        'ORIGEM
        dsdados = Con.ExecutarQuery(sql)
        If dsdados.Tables(0).Rows.Count > 0 Then

            For Each linhadados As DataRow In dsdados.Tables(0).Rows
                tabela &= "<option value='" & linhadados("ID_EQUIPE") & "'>" & linhadados("NM_EQUIPE") & "</option>"
            Next

        End If
        tabela &= "</select>"
        divEquipes.InnerHtml = tabela

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

            'lkAjustarComissao.Visible = True
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT A.ID_CABECALHO_COMISSAO_INSIDE ,B.ID_DETALHE_COMISSAO_INSIDE ,B.NR_PROCESSO,B.NR_NOTAS_FISCAL,B.DT_NOTA_FISCAL,B.ID_SERVICO,B.ID_PARCEIRO_CLIENTE,B.ID_PARCEIRO_VENDEDOR,B.ID_TIPO_ESTUFAGEM,B.VL_COMISSAO_BASE,B.QT_BL,B.QT_CNTR,B.VL_PERCENTUAL,B.VL_COMISSAO_TOTAL,B.DT_LIQUIDACAO,B.DS_OBSERVACAO,A.DT_EXPORTACAO
FROM            dbo.TB_CABECALHO_COMISSAO_INSIDE AS A LEFT OUTER JOIN
                         dbo.TB_DETALHE_COMISSAO_INSIDE AS B ON B.ID_CABECALHO_COMISSAO_INSIDE = A.ID_CABECALHO_COMISSAO_INSIDE
						 WHERE B.ID_DETALHE_COMISSAO_INSIDE = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DETALHE_COMISSAO_INSIDE")) Then
                    txtIDAjuste.Text = ds.Tables(0).Rows(0).Item("ID_DETALHE_COMISSAO_INSIDE")
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
                    ' lkAjustarComissao.Visible = True

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
        '  lkAjustarComissao.Visible = True

        txtID.Text = ""
        txtlinha.Text = ""
        divErro.Visible = False

        If txtCompetencia.Text = "" Then
            lblmsgErro.Text = "É necessario informar a competência."
            divErro.Visible = True
        Else

            If ddlFiltro.SelectedValue = 1 Then '' EQUIPE
                filtro = " AND NM_EQUIPE LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 4 Then '' NOME
                filtro = " AND ID_ANALISTA_COTACAO LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 5 Then  '' PROCESSO
                filtro = " AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 0 Then
                filtro = ""
            End If

            dsComissao.SelectCommand = "SELECT * FROM [dbo].[View_Comissao_Inside] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' " & filtro & " ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO"
            dgvComissoes.DataBind()

            DivGrid2.Visible = True
            lblCompetenciaCCProcesso.Text = txtCompetencia.Text
        End If
    End Sub
    Private Sub dgvTabelaComissao_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTabelaComissao.RowCommand
        DivExcluir.Visible = False
        divInfo.Visible = False
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2068 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroExcluir.Text = "Usuário não tem permissão para realizar exclusões"
                DivExcluir.Visible = True
            Else
                Con.ExecutarQuery("DELETE FROM [dbo].[TB_INSIDE_TAXA_COMISSAO] WHERE ID_INSIDE_TAXA_COMISSAO =" & ID)
                dgvTabelaComissao.DataBind()
                divInfo.Visible = True
                lblInfo.Text = "Taxa excluída com sucesso"
                mpeGestao.Show()
            End If

        ElseIf e.CommandName = "Editar" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_INSIDE_TAXA_COMISSAO,DT_VALIDADE_INICIAL,VL_TAXA_LCL,VL_TAXA_FCL, ID_USUARIO_GESTOR, ID_EQUIPE FROM TB_INSIDE_TAXA_COMISSAO WHERE ID_INSIDE_TAXA_COMISSAO = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_INSIDE_TAXA_COMISSAO")) Then
                    txtIDTabelaTaxa.Text = ds.Tables(0).Rows(0).Item("ID_INSIDE_TAXA_COMISSAO").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")) Then
                    txtValidade.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_LCL")) Then
                    txtLCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_LCL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_FCL")) Then
                    txtFCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_FCL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_USUARIO_GESTOR")) Then
                    ddlGestor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_USUARIO_GESTOR").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_EQUIPE")) Then
                    ddlEquipes.SelectedValue = ds.Tables(0).Rows(0).Item("ID_EQUIPE").ToString()
                End If
                ddlEquipes.Attributes.CssStyle.Add("display", "block")
                divEquipes.Attributes.CssStyle.Add("display", "none")
            End If

            mpeGestao.Show()
        End If
        Con.Fechar()
    End Sub

    Private Sub btnGravaTaxaTabela_Click(sender As Object, e As EventArgs) Handles btnGravaTaxaTabela.Click
        divInfo.Visible = False

        txtLCL.Text = txtLCL.Text.Replace(".", "")
        txtLCL.Text = txtLCL.Text.Replace(",", ".")

        txtFCL.Text = txtFCL.Text.Replace(".", "")
        txtFCL.Text = txtFCL.Text.Replace(",", ".")


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtIDTabelaTaxa.Text = "" Then
            SeparaEquipes(txtEquipeSelecionadas.Text)
            'Con.ExecutarQuery("INSERT INTO TB_INSIDE_TAXA_COMISSAO (DT_VALIDADE_INICIAL,VL_TAXA_LCL,VL_TAXA_FCL,ID_USUARIO_GESTOR,ID_EQUIPE) VALUES (CONVERT(DATE,'" & txtValidade.Text & "',103)," & txtLCL.Text & ", " & txtFCL.Text & "," & ddlGestor.SelectedValue & "," & txtEquipeSelecionadas.Text & " ) ")
            divInfo.Visible = True
            lblInfo.Text = "Taxa inserida com sucesso"
            dgvTabelaComissao.DataBind()
            mpeGestao.Show()
            ddlGestor.SelectedValue = 0
            txtValidade.Text = ""
            txtLCL.Text = ""
            txtFCL.Text = ""

        Else
            Con.ExecutarQuery("UPDATE TB_INSIDE_TAXA_COMISSAO SET DT_VALIDADE_INICIAL = CONVERT(DATE,'" & txtValidade.Text & "',103),VL_TAXA_LCL = " & txtLCL.Text & ",VL_TAXA_FCL = " & txtFCL.Text & ", ID_USUARIO_GESTOR = " & ddlGestor.SelectedValue & " , ID_EQUIPE = " & ddlEquipes.SelectedValue & " WHERE ID_INSIDE_TAXA_COMISSAO = " & txtIDTabelaTaxa.Text)
            divInfo.Visible = True
            lblInfo.Text = "Taxa alterada com sucesso"
            dgvTabelaComissao.DataBind()
            mpeGestao.Show()

        End If
    End Sub

    Sub SeparaEquipes(ByVal Equipes As String)
        Dim Con As New Conexao_sql
        Con.Conectar()
        'quebrar a string
        Dim palavras As String() = Equipes.Split(New String() _
          {","}, StringSplitOptions.RemoveEmptyEntries)

        'exibe o resultado
        For i As Integer = 0 To palavras.GetUpperBound(0) Step 1

            Con.ExecutarQuery("INSERT INTO TB_INSIDE_TAXA_COMISSAO (DT_VALIDADE_INICIAL,VL_TAXA_LCL,VL_TAXA_FCL,ID_USUARIO_GESTOR,ID_EQUIPE) VALUES (CONVERT(DATE,'" & txtValidade.Text & "',103)," & txtLCL.Text & ", " & txtFCL.Text & "," & ddlGestor.SelectedValue & "," & palavras(i) & " ) ")

        Next
    End Sub
    Private Sub btnFecharTabela_Click(sender As Object, e As EventArgs) Handles btnFecharTabela.Click
        txtIDTabelaTaxa.Text = ""
        txtValidade.Text = ""
        txtLCL.Text = ""
        txtFCL.Text = ""
        ddlGestor.SelectedValue = 0
        divInfo.Visible = False
        DivExcluir.Visible = False
        ddlEquipes.Attributes.CssStyle.Add("display", "none")
        divEquipes.Attributes.CssStyle.Add("display", "block")
    End Sub

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


            Dim SQL1 As String = "SELECT COMPETENCIA,NR_PROCESSO,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,PARCEIRO_VENDEDOR,ANALISTA_COTACAO,USUARIO_LIDER, NM_EQUIPE, NM_TIME,BENEFICIADO,PARCEIRO_CLIENTE,TP_SERVICO,TP_VIA,TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,DS_OBSERVACAO FROM [dbo].[View_Comissao_Inside] WHERE COMPETENCIA ='" & txtCompetencia.Text & "'
UNION 
SELECT NULL,NULL,NULL,NULL,NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL, NULL,NULL 
ORDER BY COMPETENCIA DESC,NR_PROCESSO DESC, USUARIO_LIDER DESC "
            Dim SQL2 As String = "SELECT NULL, NULL,NULL,NULL, NULL, NULL, NULL,NULL,NOMENCLATURA,BENEFICIADO,NULL,  NULL,NULL,  NULL,  TOTAL_PROCESSOS, NULL, COMISSAO_PROCESSO, COMISSAO_TOTAL,NULL,NULL FROM [FN_RELATORIO_COMISSAO_INSIDE]('" & txtCompetencia.Text & "') ORDER BY  ORDEM, NOMENCLATURA, BENEFICIADO"


            Classes.Excel.exportaExcelDuplo(SQL1, SQL2, "Comissao")
        End If

    End Sub


    Private Sub txtLiquidacaoInicial_TextChanged(sender As Object, e As EventArgs) Handles txtLiquidacaoInicial.TextChanged
        divErroGerarComissao.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT TOP 1 ID_INSIDE_TAXA_COMISSAO ,VL_TAXA_FCL,VL_TAXA_LCL FROM TB_INSIDE_TAXA_COMISSAO WHERE CONVERT(DATETIME,DT_VALIDADE_INICIAL,103) <= CONVERT(DATETIME,'" & txtLiquidacaoInicial.Text & "',103)")
        If ds.Tables(0).Rows.Count = 0 Then
            lblErroGerarComissao.Text = "Não há tabela de taxa cadastrada!"
            divErroGerarComissao.Visible = True
        End If

        VerificaCompetencia()

        ModalPopupExtender3.Show()
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
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2068 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroGerarComissao.Text = "Usuário não tem permissão!"
                divErroGerarComissao.Visible = True
            Else
                Dim dsQtd As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM FN_COMISSAO_INSIDE_SALES('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND FL_VENDEDOR_DIRETO =1 ")
                If dsQtd.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroGerarComissao.Text = "Não há processos em aberto para comissão nesse período!"
                    divErroGerarComissao.Visible = True
                Else

                    Dim QtdProcessos = dsQtd.Tables(0).Rows(0).Item("QTD")
                    Dim NOVA_COMPETECIA As String = txtNovaCompetencia.Text
                    NOVA_COMPETECIA = NOVA_COMPETECIA.Replace("/", "")
                    Dim dsInsert As DataSet
                    Dim cabecalho As String

                    Con.ExecutarQuery("DELETE FROM TB_DETALHE_COMISSAO_INSIDE  WHERE ID_CABECALHO_COMISSAO_INSIDE 
IN (SELECT ID_CABECALHO_COMISSAO_INSIDE FROM TB_CABECALHO_COMISSAO_INSIDE WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "')")

                    '                    Con.ExecutarQuery("DELETE FROM TB_EQUIPE_COMISSAO  WHERE ID_CABECALHO_COMISSAO_INSIDE 
                    'IN (SELECT ID_CABECALHO_COMISSAO_INSIDE FROM TB_CABECALHO_COMISSAO_INSIDE WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "')")

                    Con.ExecutarQuery("DELETE FROM TB_CABECALHO_COMISSAO_INSIDE WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "'")


                    If lblContasReceber.Text <> 0 Then
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)

                        divInfoGerarComissao.Visible = True
                        lblInfoGerarComissao.Text = "Necessário exportar competência para a conta corrente do processo!"
                    End If

                    dsInsert = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_INSIDE (DT_COMPETENCIA,ID_USUARIO_GERACAO,DT_GERACAO,DT_LIQUIDACAO_INICIAL ,DT_LIQUIDACAO_FINAL ) VALUES('" & NOVA_COMPETECIA & "'," & Session("ID_USUARIO") & ", getdate(),CONVERT(DATE,'" & txtLiquidacaoInicial.Text & "',103),CONVERT(DATE,'" & txtLiquidacaoFinal.Text & "',103)) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_INSIDE  ")
                    cabecalho = dsInsert.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INSIDE")

                    Equipe(cabecalho)
                    CarregaGrid()

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
        VerificaCompetencia()
    End Sub

    Sub Equipe(cabecalho As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim ContadorHBL As Integer = 0
        Dim ContadorMembros As Integer = 0
        Dim TaxaBase_final As String = "0"
        Dim TaxaCalculada_final As String = "0"

        'ANALISTA
        ds = Con.ExecutarQuery("SELECT ID_BL,(SELECT ID_PARCEIRO_EQUIPE_INSIDE FROM TB_PARAMETROS)ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,NR_PROCESSO,ID_SERVICO,ID_TIPO_ESTUFAGEM,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO,ISNULL(QT_BL,0)QT_BL,ISNULL(QT_CNTR,0)QT_CNTR  FROM FN_COMISSAO_INSIDE_SALES('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO =1 AND FL_ATIVO = 1) AND ID_ANALISTA_COTACAO IN (SELECT ID_USUARIO_MEMBRO_EQUIPE FROM TB_INSIDE_EQUIPE_MEMBROS where ISNULL(ID_TIME,0) = 0)")

        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows

                Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT A.ID_EQUIPE,B.TAXA_LIDER,B.TAXA_EQUIPE,B.ID_USUARIO_LIDER,  C.VL_TAXA_FCL, C.VL_TAXA_LCL, C.ID_USUARIO_GESTOR FROM TB_INSIDE_EQUIPE_MEMBROS A 
INNER JOIN TB_INSIDE_EQUIPE  B ON A.ID_EQUIPE = B.ID_EQUIPE
INNER JOIN TB_INSIDE_TAXA_COMISSAO C  ON C.ID_EQUIPE = B.ID_EQUIPE 
WHERE A.ID_USUARIO_MEMBRO_EQUIPE = " & linha.Item("ID_ANALISTA_COTACAO"))

                If dsTaxa.Tables(0).Rows.Count > 0 Then
                    TaxaCalculada_final = dsTaxa.Tables(0).Rows(0).Item("TAXA_EQUIPE")
                    TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(".", "")
                    TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(",", ".")

                    ''ANALISTA
                    Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_INSIDE (ID_CABECALHO_COMISSAO_INSIDE,ID_EQUIPE,ID_USUARIO_GESTOR,NR_PROCESSO,ID_BL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,FL_CALC_EQUIPE,ID_ANALISTA_COTACAO,ID_USUARIO_LIDER,QT_BL,QT_CNTR,ID_USUARIO_BENEFICIADO) VALUES(" & cabecalho & ",
" & dsTaxa.Tables(0).Rows(0).Item("ID_EQUIPE") & ",
" & dsTaxa.Tables(0).Rows(0).Item("ID_USUARIO_GESTOR") & ",
'" & linha.Item("NR_PROCESSO") & "',
" & linha.Item("ID_BL") & ",
" & linha.Item("ID_SERVICO") & ",
" & linha.Item("ID_PARCEIRO_CLIENTE") & ",
" & linha.Item("ID_PARCEIRO_VENDEDOR") & ",
" & linha.Item("ID_TIPO_ESTUFAGEM") & ",
" & TaxaCalculada_final & ",
" & TaxaCalculada_final & ",
 1,
" & linha.Item("ID_ANALISTA_COTACAO") & ",
" & dsTaxa.Tables(0).Rows(0).Item("ID_USUARIO_LIDER") & "," & linha.Item("QT_BL") & "," & linha.Item("QT_CNTR") & "," & linha.Item("ID_ANALISTA_COTACAO") & ")")

                End If

            Next

        End If


        'TIME
        'ds = Con.ExecutarQuery("SELECT COUNT(distinct ID_BL)BL, COUNT(distinct ID_ANALISTA_COTACAO)ANALISTA FROM FN_COMISSAO_INSIDE_SALES('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO =1 AND FL_ATIVO = 1)  AND ID_ANALISTA_COTACAO IN (SELECT ID_USUARIO_MEMBRO_TIME FROM TB_INSIDE_TIME_MEMBROS )")
        '        Dim dsTimes = Con.ExecutarQuery("SELECT B.ID_TIME, COUNT(distinct A.ID_BL)BL, COUNT(distinct A.ID_ANALISTA_COTACAO)ANALISTA FROM FN_COMISSAO_INSIDE_SALES('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') A
        'INNER JOIN TB_INSIDE_TIME_MEMBROS B ON A.ID_ANALISTA_COTACAO = B.ID_USUARIO_MEMBRO_TIME
        'WHERE A.DT_PAGAMENTO_EXP IS NULL 
        'AND A.ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO =1 AND FL_ATIVO = 1)  
        'AND A.ID_ANALISTA_COTACAO IN (SELECT ID_USUARIO_MEMBRO_TIME FROM TB_INSIDE_TIME_MEMBROS ) 
        'GROUP BY B.ID_TIME")
        '        ContadorHBL = dsTimes.Tables(0).Rows(0).Item("BL")
        '        ContadorMembros = dsTimes.Tables(0).Rows(0).Item("ANALISTA")
        '        If dsTimes.Tables(0).Rows.Count > 0 Then

        '   For Each linhaTime As DataRow In dsTimes.Tables(0).Rows
        ds = Con.ExecutarQuery("SELECT ID_BL,(SELECT ID_PARCEIRO_EQUIPE_INSIDE FROM TB_PARAMETROS)ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,NR_PROCESSO,ID_SERVICO,ID_TIPO_ESTUFAGEM,isnull(ID_ANALISTA_COTACAO,0)ID_ANALISTA_COTACAO,ISNULL(QT_BL,0)QT_BL,ISNULL(QT_CNTR,0)QT_CNTR  FROM FN_COMISSAO_INSIDE_SALES('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO =1 AND FL_ATIVO = 1)  AND ID_ANALISTA_COTACAO IN (SELECT ID_USUARIO_MEMBRO_TIME FROM TB_INSIDE_TIME_MEMBROS )")
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows

                Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT A.ID_TIME, B.TAXA_EQUIPE, A.ID_EQUIPE,B.ID_USUARIO_LIDER, C.ID_USUARIO_GESTOR, B.TAXA_EQUIPE/(SELECT COUNT(ID_USUARIO_MEMBRO_TIME) FROM TB_INSIDE_TIME_MEMBROS M WHERE A.ID_TIME = M.ID_TIME)TAXA_EQUIPE_CALCULADA FROM TB_INSIDE_EQUIPE_MEMBROS A 
INNER JOIN TB_INSIDE_EQUIPE  B ON A.ID_EQUIPE = B.ID_EQUIPE
INNER JOIN TB_INSIDE_TAXA_COMISSAO C  ON C.ID_EQUIPE = B.ID_EQUIPE 
WHERE A.ID_TIME IN (SELECT ID_TIME FROM TB_INSIDE_TIME_MEMBROS WHERE ID_USUARIO_MEMBRO_TIME = " & linha.Item("ID_ANALISTA_COTACAO") & ")")

                If dsTaxa.Tables(0).Rows.Count > 0 Then

                    TaxaBase_final = dsTaxa.Tables(0).Rows(0).Item("TAXA_EQUIPE")
                    TaxaBase_final = TaxaBase_final.ToString.Replace(".", "")
                    TaxaBase_final = TaxaBase_final.ToString.Replace(",", ".")


                    TaxaCalculada_final = dsTaxa.Tables(0).Rows(0).Item("TAXA_EQUIPE_CALCULADA")
                    ' TaxaCalculada_final = TaxaCalculada_final * ContadorHBL
                    'TaxaCalculada_final = TaxaCalculada_final / ContadorMembros
                    TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(".", "")
                    TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(",", ".")



                    ''MEMBRO TIME
                    Dim dsDetalhe As DataSet = Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_INSIDE (ID_CABECALHO_COMISSAO_INSIDE,ID_EQUIPE,ID_TIME,ID_USUARIO_GESTOR,NR_PROCESSO,ID_BL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,FL_CALC_TIME,ID_ANALISTA_COTACAO,ID_USUARIO_LIDER,QT_BL,QT_CNTR, ID_USUARIO_BENEFICIADO) VALUES(" & cabecalho & ",
" & dsTaxa.Tables(0).Rows(0).Item("ID_EQUIPE") & ",
" & dsTaxa.Tables(0).Rows(0).Item("ID_TIME") & ",
" & dsTaxa.Tables(0).Rows(0).Item("ID_USUARIO_GESTOR") & ",
'" & linha.Item("NR_PROCESSO") & "',
" & linha.Item("ID_BL") & ",
" & linha.Item("ID_SERVICO") & ",
" & linha.Item("ID_PARCEIRO_CLIENTE") & ",
" & linha.Item("ID_PARCEIRO_VENDEDOR") & ",
" & linha.Item("ID_TIPO_ESTUFAGEM") & ",
" & TaxaBase_final & ",
" & TaxaCalculada_final & ",
 1,
" & linha.Item("ID_ANALISTA_COTACAO") & ",
" & dsTaxa.Tables(0).Rows(0).Item("ID_USUARIO_LIDER") & "," & linha.Item("QT_BL") & "," & linha.Item("QT_CNTR") & "," & linha.Item("ID_ANALISTA_COTACAO") & ") Select SCOPE_IDENTITY() as ID_DETALHE_COMISSAO_INSIDE")

                    Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_INSIDE (ID_CABECALHO_COMISSAO_INSIDE,ID_EQUIPE,ID_TIME,ID_USUARIO_GESTOR,NR_PROCESSO,ID_BL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,FL_CALC_TIME,ID_ANALISTA_COTACAO,ID_USUARIO_LIDER,QT_BL,QT_CNTR,ID_USUARIO_BENEFICIADO) SELECT A.ID_CABECALHO_COMISSAO_INSIDE,A.ID_EQUIPE,A.ID_TIME,A.ID_USUARIO_GESTOR,A.NR_PROCESSO,A.ID_BL,A.ID_SERVICO,A.ID_PARCEIRO_CLIENTE,A.ID_PARCEIRO_VENDEDOR,A.ID_TIPO_ESTUFAGEM,A.VL_COMISSAO_BASE,A.VL_COMISSAO_TOTAL,A.FL_CALC_TIME,A.ID_ANALISTA_COTACAO,ID_USUARIO_LIDER,A.QT_BL,A.QT_CNTR,B.ID_USUARIO_MEMBRO_TIME FROM TB_DETALHE_COMISSAO_INSIDE A INNER JOIN TB_INSIDE_TIME_MEMBROS B ON A.ID_TIME = B.ID_TIME WHERE ID_USUARIO_MEMBRO_TIME <> " & linha.Item("ID_ANALISTA_COTACAO") & " AND ID_DETALHE_COMISSAO_INSIDE = " & dsDetalhe.Tables(0).Rows(0).Item("ID_DETALHE_COMISSAO_INSIDE"))

                End If

            Next

        End If

        '    Next
        '   End If


        'LIDER/GESTOR

        ds = Con.ExecutarQuery(" SELECT DISTINCT D.NR_PROCESSO, D.ID_BL, D.ID_SERVICO,D.ID_ANALISTA_COTACAO, D.QT_BL, D.QT_CNTR, D.ID_PARCEIRO_CLIENTE, D.ID_PARCEIRO_VENDEDOR,D.ID_TIPO_ESTUFAGEM, A.ID_EQUIPE, B.TAXA_LIDER, B.ID_USUARIO_LIDER,  C.VL_TAXA_FCL, C.VL_TAXA_LCL, C.ID_USUARIO_GESTOR FROM TB_INSIDE_EQUIPE_MEMBROS A 
INNER JOIN TB_INSIDE_EQUIPE  B ON A.ID_EQUIPE = B.ID_EQUIPE
INNER JOIN TB_INSIDE_TAXA_COMISSAO C  ON B.ID_EQUIPE = C.ID_EQUIPE 
INNER JOIN TB_DETALHE_COMISSAO_INSIDE D ON C.ID_EQUIPE = D.ID_EQUIPE 
WHERE D.ID_CABECALHO_COMISSAO_INSIDE =" & cabecalho)
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows

                'LIDER
                If linha.Item("ID_USUARIO_LIDER").ToString() <> "" Then

                    TaxaBase_final = linha.Item("TAXA_LIDER")
                    TaxaBase_final = TaxaBase_final.ToString.Replace(".", "")
                    TaxaBase_final = TaxaBase_final.ToString.Replace(",", ".")

                    TaxaCalculada_final = linha.Item("TAXA_LIDER")
                    TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(".", "")
                    TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(",", ".")


                    Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_INSIDE (ID_CABECALHO_COMISSAO_INSIDE,ID_EQUIPE,ID_USUARIO_GESTOR,NR_PROCESSO,ID_BL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,FL_CALC_LIDER,ID_ANALISTA_COTACAO,ID_USUARIO_LIDER,QT_BL,QT_CNTR,ID_USUARIO_BENEFICIADO) VALUES(" & cabecalho & ",
" & linha.Item("ID_EQUIPE") & ",
" & linha.Item("ID_USUARIO_GESTOR") & ",
'" & linha.Item("NR_PROCESSO") & "',
" & linha.Item("ID_BL") & ",
" & linha.Item("ID_SERVICO") & ",
" & linha.Item("ID_PARCEIRO_CLIENTE") & ",
" & linha.Item("ID_PARCEIRO_VENDEDOR") & ",
" & linha.Item("ID_TIPO_ESTUFAGEM") & ",
" & TaxaBase_final & ",
" & TaxaCalculada_final & ",
 1,
" & linha.Item("ID_ANALISTA_COTACAO") & ",
" & linha.Item("ID_USUARIO_LIDER") & "," & linha.Item("QT_BL") & "," & linha.Item("QT_CNTR") & "," & linha.Item("ID_USUARIO_LIDER") & ")")

                End If

                'GESTOR
                If linha.Item("ID_USUARIO_GESTOR").ToString() <> "" Then

                    If linha.Item("ID_TIPO_ESTUFAGEM").ToString() = "1" Then

                        TaxaBase_final = linha.Item("VL_TAXA_FCL")
                        TaxaBase_final = TaxaBase_final.ToString.Replace(".", "")
                        TaxaBase_final = TaxaBase_final.ToString.Replace(",", ".")

                        TaxaCalculada_final = linha.Item("VL_TAXA_FCL")
                        TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(".", "")
                        TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(",", ".")

                        Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_INSIDE (ID_CABECALHO_COMISSAO_INSIDE,ID_EQUIPE,ID_USUARIO_GESTOR,NR_PROCESSO,ID_BL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,FL_CALC_GESTOR,ID_ANALISTA_COTACAO,ID_USUARIO_LIDER,QT_BL,QT_CNTR, ID_USUARIO_BENEFICIADO) VALUES(" & cabecalho & ",
" & linha.Item("ID_EQUIPE") & ",
" & linha.Item("ID_USUARIO_GESTOR") & ",
'" & linha.Item("NR_PROCESSO") & "',
" & linha.Item("ID_BL") & ",
" & linha.Item("ID_SERVICO") & ",
" & linha.Item("ID_PARCEIRO_CLIENTE") & ",
" & linha.Item("ID_PARCEIRO_VENDEDOR") & ",
" & linha.Item("ID_TIPO_ESTUFAGEM") & ",
" & TaxaBase_final & ",
" & TaxaCalculada_final & ",
 1,
" & linha.Item("ID_ANALISTA_COTACAO") & ",
" & linha.Item("ID_USUARIO_LIDER") & "," & linha.Item("QT_BL") & "," & linha.Item("QT_CNTR") & "," & linha.Item("ID_USUARIO_GESTOR") & ")")

                    ElseIf linha.Item("ID_TIPO_ESTUFAGEM").ToString() = "2" Then

                        TaxaBase_final = linha.Item("VL_TAXA_LCL")
                        TaxaBase_final = TaxaBase_final.ToString.Replace(".", "")
                        TaxaBase_final = TaxaBase_final.ToString.Replace(",", ".")

                        TaxaCalculada_final = linha.Item("VL_TAXA_LCL")
                        TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(".", "")
                        TaxaCalculada_final = TaxaCalculada_final.ToString.Replace(",", ".")

                        Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_INSIDE (ID_CABECALHO_COMISSAO_INSIDE,ID_EQUIPE,ID_USUARIO_GESTOR,NR_PROCESSO,ID_BL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,FL_CALC_GESTOR,ID_ANALISTA_COTACAO,ID_USUARIO_LIDER,QT_BL,QT_CNTR, ID_USUARIO_BENEFICIADO) VALUES(" & cabecalho & ",
" & linha.Item("ID_EQUIPE") & ",
" & linha.Item("ID_USUARIO_GESTOR") & ",
'" & linha.Item("NR_PROCESSO") & "',
" & linha.Item("ID_BL") & ",
" & linha.Item("ID_SERVICO") & ",
" & linha.Item("ID_PARCEIRO_CLIENTE") & ",
" & linha.Item("ID_PARCEIRO_VENDEDOR") & ",
" & linha.Item("ID_TIPO_ESTUFAGEM") & ",
" & TaxaBase_final & ",
" & TaxaCalculada_final & ",
 1,
" & linha.Item("ID_ANALISTA_COTACAO") & ",
" & linha.Item("ID_USUARIO_LIDER") & "," & linha.Item("QT_BL") & "," & linha.Item("QT_CNTR") & "," & linha.Item("ID_USUARIO_GESTOR") & ")")
                    End If

                End If

            Next

        End If


    End Sub


    Sub VerificaCompetencia()
        Dim Con As New Conexao_sql
        Con.Conectar()

        'Verifica se a competencia já existe
        Dim ds As DataSet = Con.ExecutarQuery("Select ID_CABECALHO_COMISSAO_INSIDE,DT_EXPORTACAO FROM View_Comissao_Inside WHERE COMPETENCIA = '" & txtNovaCompetencia.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            divAtencaoGerarComissao.Visible = True
            lblAtencaoGerarComissao.Text = "COMPETENCIA JÁ EXISTE!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
            lblCompetenciaSobrepor.Text = ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INSIDE")
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EXPORTACAO")) Then
                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CINSD' AND DT_COMPETENCIA = '" & txtNovaCompetencia.Text & "'")
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
        ds = Con.ExecutarQuery("SELECT ID_CABECALHO_COMISSAO_INSIDE FROM View_Comissao_Inside WHERE COMPETENCIA = '" & Competencia_Anterior.ToString() & "' AND DT_EXPORTACAO IS NULL")
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
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CINSD' AND DT_COMPETENCIA = '" & txtCompetencia.Text & "'")
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
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2068 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

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




                Con.ExecutarQuery("UPDATE TB_DETALHE_COMISSAO_INSIDE SET NR_PROCESSO = '" & txtAjusteProcesso.Text & "',NR_NOTAS_FISCAL = '" & txtAjusteNotaFiscal.Text & "',DT_NOTA_FISCAL = CONVERT(DATE,'" & txtAjusteDataNota.Text & "',103),ID_SERVICO = " & ddlAjusteServico.SelectedValue & ",ID_PARCEIRO_CLIENTE = " & ddlAjusteCliente.SelectedValue & ",ID_PARCEIRO_VENDEDOR= " & ddlAjusteVendedor.SelectedValue & ",ID_TIPO_ESTUFAGEM = " & ddlAjusteEstufagem.SelectedValue & ",VL_COMISSAO_BASE = " & txtAjusteBase.Text & ",QT_BL = " & txtAjusteQtdBl.Text & ",QT_CNTR = " & txtAjusteQtdCNTR.Text & ",VL_PERCENTUAL = " & txtAjustePorcentagem.Text & ",VL_COMISSAO_TOTAL= " & txtAjusteTotal.Text & ",DT_LIQUIDACAO =  CONVERT(DATE,'" & txtAjusteLiquidacao.Text & "',103), DS_OBSERVACAO = " & txtAjusteObs.Text & " WHERE ID_DETALHE_COMISSAO_INSIDE = " & txtIDAjuste.Text)
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
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2068 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

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
                Con.ExecutarQuery("DELETE FROM TB_DETALHE_COMISSAO_INSIDE WHERE ID_DETALHE_COMISSAO_INSIDE = " & txtIDAjuste.Text)
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

    Private Sub btnLimpaTaxaTabela_Click(sender As Object, e As EventArgs) Handles btnLimpaTaxaTabela.Click
        txtIDTabelaTaxa.Text = ""
        txtValidade.Text = ""
        txtLCL.Text = ""
        txtFCL.Text = ""
        ddlGestor.SelectedValue = 0
        DivExcluir.Visible = False
        divInfo.Visible = False
        ddlEquipes.Attributes.CssStyle.Add("display", "none")
        divEquipes.Attributes.CssStyle.Add("display", "block")
        mpeGestao.Show()
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
ID_USUARIO_LANCAMENTO ,ID_USUARIO_LIQUIDACAO,TP_EXPORTACAO) VALUES('P','" & txtCompetencia.Text & "',GETDATE(),CONVERT(DATE,'" & txtLiquidacaoCCProcesso.Text & "',103),CONVERT(DATE,'" & txtLiquidacaoCCProcesso.Text & "',103),7, " & Session("ID_USUARIO") & ", " & Session("ID_USUARIO") & ",'CINSD')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER_ITENS")
            Dim ID_CONTAS_PAGAR_RECEBER_ITENS As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")

            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_PARCEIRO_EMPRESA,DS_HISTORICO_LANCAMENTO,ID_CONTA_PAGAR_RECEBER, VL_LANCAMENTO ,VL_LIQUIDO,ID_ITEM_DESPESA,VL_TAXA_CALCULADO,ID_MOEDA, ID_BL )
               SELECT ID_PARCEIRO_VENDEDOR,'COMISSÃO INSIDE – " & txtCompetencia.Text & "'," & ID_CONTAS_PAGAR_RECEBER_ITENS & ",VL_COMISSAO_TOTAL, VL_COMISSAO_TOTAL, (SELECT ID_ITEM_VENDEDOR FROM TB_PARAMETROS)ID_ITEM_VENDEDOR,VL_COMISSAO_TOTAL,124,ID_BL  FROM TB_DETALHE_COMISSAO_INSIDE WHERE ID_CABECALHO_COMISSAO_INSIDE in (SELECT distinct ID_CABECALHO_COMISSAO_INSIDE FROM View_Comissao_Inside WHERE COMPETENCIA = '" & txtCompetencia.Text & "')")


            Con.ExecutarQuery("UPDATE TB_CABECALHO_COMISSAO_INSIDE SET DT_EXPORTACAO =  GETDATE(), ID_USUARIO_EXPORTACAO = " & Session("ID_USUARIO") & "   WHERE ID_CABECALHO_COMISSAO_INSIDE in (SELECT distinct ID_CABECALHO_COMISSAO_INSIDE FROM View_Comissao_Inside WHERE COMPETENCIA = '" & txtCompetencia.Text & "')")


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
        VerificaCCPRocesso()
    End Sub

    Private Sub btnFecharCCProcesso_Click(sender As Object, e As EventArgs) Handles btnFecharCCProcesso.Click
        txtLiquidacaoCCProcesso.Text = ""
        divInfoCCProcesso.Visible = False
        divErroCCProcesso.Visible = False

    End Sub

    Private Sub lkRelEquipe_Click(sender As Object, e As EventArgs) Handles lkRelEquipe.Click
        divErro.Visible = False

        If txtCompetencia.Text = "" Then
            lblmsgErro.Text = "É necessario informar a competência."
            divErro.Visible = True
        Else

            Dim SQL As String = "SELECT COMPETENCIA,USUARIO, VL_COMISSAO_TOTAL as 'VALOR',LIDER,NM_EQUIPE AS EQUIPE FROM [dbo].[View_Equipes] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' ORDER BY NM_EQUIPE,LIDER,USUARIO"

            Classes.Excel.exportaExcel(SQL, "Equipes")
        End If

    End Sub

    Private Sub ddlGestor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGestor.SelectedIndexChanged
        CarregaEquipes()
        mpeGestao.Show()
    End Sub

    Private Sub lkResumoRelatório_Click(sender As Object, e As EventArgs) Handles lkResumoRelatório.Click
        Dim SQL As String = "SELECT NOMENCLATURA,BENEFICIADO,TOTAL_PROCESSOS,COMISSAO_PROCESSO,COMISSAO_TOTAL FROM [FN_RELATORIO_COMISSAO_INSIDE]('" & txtCompetencia.Text & "') ORDER BY ORDEM,NOMENCLATURA, BENEFICIADO "

        Classes.Excel.exportaExcel(SQL, "ResumoComissao")
    End Sub
End Class