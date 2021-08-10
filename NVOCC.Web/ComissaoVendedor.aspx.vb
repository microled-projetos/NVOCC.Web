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
        Else
            'If Not Page.IsPostBack Then

            '    If Month(Now.AddMonths(-1)) <= 9 Then
            '        txtCompetencia.Text = "0" & Month(Now.AddMonths(-1)) & "/" & Now.Year
            '        lblCompetenciaCCProcesso.Text = txtCompetencia.Text
            '        txtNovaCompetencia.Text = "0" & Now.Month & "/" & Now.Year
            '    Else
            '        txtCompetencia.Text = Month(Now.AddMonths(-1)) & "/" & Now.Year
            '        lblCompetenciaCCProcesso.Text = txtCompetencia.Text
            '        txtNovaCompetencia.Text = Now.Month & "/" & Now.Year
            '    End If
            'End If

        End If
        Con.Fechar()
    End Sub

    Private Sub dgvComissoes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvComissoes.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False


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
                filtro = " AND FL_CALC_INSIDE = 0 AND FL_CALC_SUB = 0"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                filtro = " AND FL_CALC_SUB = 1"
            ElseIf ddlFiltro.SelectedValue = 5 Then
                filtro = " AND FL_CALC_INSIDE = 1"
            End If

            dsComissao.SelectCommand = "SELECT * FROM [dbo].[View_Comissao_Vendedor] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' " & filtro & " ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO"
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

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroExcluir.Text = "Usuário não tem permissão para realizar exclusões"
                DivExcluir.Visible = True
            Else
                Con.ExecutarQuery("DELETE FROM [dbo].[TB_TAXA_COMISSAO_VENDEDOR] WHERE ID_TAXA_COMISSAO_VENDEDORES =" & ID)
                dgvTabelaComissao.DataBind()
                divInfo.Visible = True
                lblInfo.Text = "Taxa excluída com sucesso"
                ModalPopupExtender1.Show()
            End If

        ElseIf e.CommandName = "Editar" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_COMISSAO_VENDEDORES,DT_VALIDADE_INICIAL,VL_TAXA_LCL,VL_TAXA_FCL,VL_TAXA_INSIDE_SALES FROM TB_TAXA_COMISSAO_VENDEDOR WHERE ID_TAXA_COMISSAO_VENDEDORES = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TAXA_COMISSAO_VENDEDORES")) Then
                    txtIDTabelaTaxa.Text = ds.Tables(0).Rows(0).Item("ID_TAXA_COMISSAO_VENDEDORES").ToString()
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_INSIDE_SALES")) Then
                    txtInsides.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_INSIDE_SALES").ToString()
                End If


            End If

            ModalPopupExtender1.Show()
        End If
        Con.Fechar()
    End Sub

    Private Sub btnGravaTaxaTabela_Click(sender As Object, e As EventArgs) Handles btnGravaTaxaTabela.Click
        divInfo.Visible = False

        txtLCL.Text = txtLCL.Text.Replace(".", "")
        txtLCL.Text = txtLCL.Text.Replace(",", ".")

        txtFCL.Text = txtFCL.Text.Replace(".", "")
        txtFCL.Text = txtFCL.Text.Replace(",", ".")

        txtInsides.Text = txtInsides.Text.Replace(".", "")
        txtInsides.Text = txtInsides.Text.Replace(",", ".")

        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtIDTabelaTaxa.Text = "" Then
            Con.ExecutarQuery("INSERT INTO TB_TAXA_COMISSAO_VENDEDOR (DT_VALIDADE_INICIAL,VL_TAXA_LCL,VL_TAXA_FCL,VL_TAXA_INSIDE_SALES) VALUES (CONVERT(DATE,'" & txtValidade.Text & "',103)," & txtLCL.Text & ", " & txtFCL.Text & "," & txtInsides.Text & " ) ")
            divInfo.Visible = True
            lblInfo.Text = "Taxa inserida com sucesso"
            dgvTabelaComissao.DataBind()
            ModalPopupExtender1.Show()

        Else
            Con.ExecutarQuery("UPDATE TB_TAXA_COMISSAO_VENDEDOR SET DT_VALIDADE_INICIAL = CONVERT(DATE,'" & txtValidade.Text & "',103),VL_TAXA_LCL = " & txtLCL.Text & ",VL_TAXA_FCL = " & txtFCL.Text & ",VL_TAXA_INSIDE_SALES = " & txtInsides.Text & " WHERE ID_TAXA_COMISSAO_VENDEDORES = " & txtIDTabelaTaxa.Text)
            divInfo.Visible = True
            lblInfo.Text = "Taxa alterada com sucesso"
            dgvTabelaComissao.DataBind()
            ModalPopupExtender1.Show()

        End If
    End Sub

    Private Sub btnFecharTabela_Click(sender As Object, e As EventArgs) Handles btnFecharTabela.Click
        txtIDTabelaTaxa.Text = ""
        txtValidade.Text = ""
        txtLCL.Text = ""
        txtFCL.Text = ""
        txtInsides.Text = ""
        divInfo.Visible = False
        DivExcluir.Visible = False
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
                filtro = " AND FL_CALC_INSIDE = 0 AND FL_CALC_SUB = 0"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                filtro = " AND FL_CALC_SUB = 1"
            ElseIf ddlFiltro.SelectedValue = 5 Then
                filtro = " AND FL_CALC_INSIDE = 1"
            End If

            Dim SQL As String = "SELECT COMPETENCIA,NR_PROCESSO,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,PARCEIRO_VENDEDOR,PARCEIRO_CLIENTE,TP_SERVICO,TP_VIA,TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_PERCENTUAL,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,DS_OBSERVACAO FROM [dbo].[View_Comissao_Vendedor] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' " & filtro & " ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO"

            Classes.Excel.exportaExcel(SQL, "NVOCC", "Comissao")
        End If

    End Sub


    Private Sub txtLiquidacaoInicial_TextChanged(sender As Object, e As EventArgs) Handles txtLiquidacaoInicial.TextChanged
        divErroGerarComissao.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT TOP 1 ID_TAXA_COMISSAO_VENDEDORES,VL_TAXA_FCL,VL_TAXA_LCL,VL_TAXA_INSIDE_SALES FROM TB_TAXA_COMISSAO_VENDEDOR WHERE CONVERT(DATETIME,DT_VALIDADE_INICIAL,103) <= CONVERT(DATETIME,'" & txtLiquidacaoInicial.Text & "',103)")
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_FCL")) Then
                lblFCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_FCL")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_LCL")) Then
                lblLCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_LCL")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_INSIDE_SALES")) Then
                lblInside.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_INSIDE_SALES")
            End If
        Else
            lblErroGerarComissao.Text = "Não há tabela de taxa cadastrada!"
            divErroGerarComissao.Visible = True
        End If
        VerificaCompetencia()

        ModalPopupExtender3.Show()
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
                lblErroExcluir.Text = "Usuário não tem permissão!"
                DivExcluir.Visible = True
            Else
                Dim dsQtd As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND FL_VENDEDOR_DIRETO =1 ")
                If dsQtd.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroExcluir.Text = "Não há processos liquidados nesse período!"
                    DivExcluir.Visible = True
                Else

                    Dim NOVA_COMPETECIA As String = txtNovaCompetencia.Text
                    NOVA_COMPETECIA = NOVA_COMPETECIA.Replace("/", "")
                    Dim dsInsert As DataSet
                    Dim cabecalho As String

                    If lblCompetenciaSobrepor.Text <> 0 Then
                        Con.ExecutarQuery("DELETE FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & lblCompetenciaSobrepor.Text)
                        Con.ExecutarQuery("DELETE FROM TB_CABECALHO_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & lblCompetenciaSobrepor.Text)
                    End If

                    If lblContasReceber.Text <> 0 Then
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
                    End If

                    dsInsert = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_VENDEDOR (DT_COMPETENCIA,ID_USUARIO_GERACAO,DT_GERACAO,DT_LIQUIDACAO_INICIAL ,DT_LIQUIDACAO_FINAL ) VALUES('" & NOVA_COMPETECIA & "'," & Session("ID_USUARIO") & ", getdate(),CONVERT(DATE,'" & txtLiquidacaoInicial.Text & "',103),CONVERT(DATE,'" & txtLiquidacaoFinal.Text & "',103)) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_VENDEDOR  ")
                    cabecalho = dsInsert.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_VENDEDOR")

                    Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO )
SELECT " & cabecalho & ", NR_NOTA_FISCAL, DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,
                                Case 
                WHEN ID_TIPO_ESTUFAGEM  = 1 THEN
                VL_COMISSAO_BASE * QT_CNTR

                WHEN ID_TIPO_ESTUFAGEM  = 2 THEN
                VL_COMISSAO_BASE * 1
                End COMISSAO_TOTAL,

				DT_LIQUIDACAO

FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND FL_VENDEDOR_DIRETO = 1 ")

                    SubVendedor(cabecalho)
                    SubInside(cabecalho)
                    CarregaGrid()
                    divSuccessGerarComissao.Visible = True
                    lblSuccessGerarComissao.Text = "Comissão gerada com sucesso!"

                End If
            End If

        End If



        ModalPopupExtender3.Show()
    End Sub

    Private Sub txtNovaCompetencia_TextChanged(sender As Object, e As EventArgs) Handles txtNovaCompetencia.TextChanged
        VerificaCompetencia()
    End Sub

    Sub SubVendedor(cabecalho As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO_SUB_VENDEDOR,ID_PARCEIRO_VENDEDOR,VL_TAXA_FIXA,VL_PERCENTUAL,ID_BASE_CALCULO FROM TB_SUB_VENDEDOR WHERE CONVERT(DATE,DT_VALIDADE_INICIAL,103) BETWEEN CONVERT(DATE,'" & txtLiquidacaoInicial.Text & "',103) AND CONVERT(DATE,'" & txtLiquidacaoFinal.Text & "',103) ")
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
                            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB ) 
SELECT ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR," & TaxaFixa & ",QT_CNTR*" & TaxaFixa & ",DT_LIQUIDACAO,1 FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR =  " & linha.Item("ID_PARCEIRO_VENDEDOR") & " AND ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho)

                        ElseIf Base_Calculo = 32 Then
                            'POR HBL - Muplica por um
                            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB ) 
SELECT ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR," & TaxaFixa & "," & TaxaFixa & ",DT_LIQUIDACAO,1 FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR =  " & linha.Item("ID_PARCEIRO_VENDEDOR") & " AND ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho)

                        End If

                    Else

                        '' comissoes de sub sem vendedor pai cadastrado
                        If Base_Calculo = 34 Then
                            'POR CNTR
                            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB )
SELECT " & cabecalho & ",NR_NOTA_FISCAL, DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR, " & TaxaFixa & ",
                  Case 
                WHEN ID_TIPO_ESTUFAGEM  = 1 THEN
               " & TaxaFixa & " * QT_CNTR

                WHEN ID_TIPO_ESTUFAGEM  = 2 THEN
                " & TaxaFixa & " * 1
                End COMISSAO_TOTAL,

				DT_LIQUIDACAO,1

FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "')
WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR =1 AND FL_VENDEDOR_DIRETO = 0 AND FL_ATIVO = 1)  AND
ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_SUB_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR Not IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & "))")

                        ElseIf Base_Calculo = 32 Then
                            'POR HBL - Muplica por um

                            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB )
SELECT " & cabecalho & ",NR_NOTA_FISCAL, DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR, " & TaxaFixa & ",  " & TaxaFixa & ", DT_LIQUIDACAO,1
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
                        Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_PERCENTUAL,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB ) 
SELECT ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_CLIENTE," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR," & percentual & "," & percentual & ",
(SELECT (SELECT sum(isnull(VL_LIQUIDO,0))VL_LIQUIDO FROM VW_PROCESSO_RECEBIDO B
inner join TB_CONTA_PAGAR_RECEBER_ITENS C on C.ID_CONTA_PAGAR_RECEBER =B.ID_CONTA_PAGAR_RECEBER
WHERE B.ID_BL = A.ID_BL)/100) * " & percentual & ",DT_LIQUIDACAO,1 FROM TB_DETALHE_COMISSAO_VENDEDOR  A WHERE ID_PARCEIRO_VENDEDOR =  " & linha.Item("ID_PARCEIRO_VENDEDOR") & " AND ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho)

                    Else

                        '' comissoes de sub sem vendedor pai cadastrado
                        Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO,FL_CALC_SUB ) 
SELECT " & cabecalho & ",NR_NOTA_FISCAL, DT_NOTA_FISCAL,NR_PROCESSO,ID_SERVICO,ID_BL," & linha.Item("ID_PARCEIRO_SUB_VENDEDOR") & ",ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_BL,QT_CNTR, " & percentual & ", 
(SELECT (SELECT sum(isnull(VL_LIQUIDO,0))VL_LIQUIDO FROM VW_PROCESSO_RECEBIDO B
inner join TB_CONTA_PAGAR_RECEBER_ITENS C on C.ID_CONTA_PAGAR_RECEBER =B.ID_CONTA_PAGAR_RECEBER
WHERE B.ID_BL = A.ID_BL)/100) * " & percentual & " ,

				DT_LIQUIDACAO,1

FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') A
WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR =1 AND FL_VENDEDOR_DIRETO = 0 AND FL_ATIVO = 1)  AND
ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_SUB_VENDEDOR WHERE ID_PARCEIRO_VENDEDOR Not IN (SELECT ID_PARCEIRO_VENDEDOR FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR = " & cabecalho & "))")

                    End If


                End If

            Next

        End If

    End Sub
    Sub SubInside(cabecalho As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim TaxaInside As Decimal = lblInside.Text

        'gravar premiacao
        ds = Con.ExecutarQuery("SELECT ID_BL,(SELECT ID_PARCEIRO_EQUIPE_INSIDE FROM TB_PARAMETROS)ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,NR_PROCESSO,ID_SERVICO,ID_TIPO_ESTUFAGEM FROM FN_VENDEDOR('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL AND ID_PARCEIRO_VENDEDOR IN (SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO =1 AND FL_ATIVO = 1)")

        If ds.Tables(0).Rows.Count > 0 Then

            Dim TaxaInside_final As String = TaxaInside.ToString.Replace(".", "")
            TaxaInside_final = TaxaInside_final.ToString.Replace(",", ".")

            For Each linha As DataRow In ds.Tables(0).Rows

                Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (ID_CABECALHO_COMISSAO_VENDEDOR,NR_PROCESSO,ID_BL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_VENDEDOR,ID_TIPO_ESTUFAGEM,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,FL_CALC_INSIDE ) VALUES(" & cabecalho & ",
'" & linha.Item("NR_PROCESSO") & "',
" & linha.Item("ID_BL") & ",
" & linha.Item("ID_SERVICO") & ",
" & linha.Item("ID_PARCEIRO_CLIENTE") & ",
" & linha.Item("ID_PARCEIRO_VENDEDOR") & ",
" & linha.Item("ID_TIPO_ESTUFAGEM") & ",
" & TaxaInside_final & ",
" & TaxaInside_final & ",
1)")

            Next

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

    Private Sub btnLimpaTaxaTabela_Click(sender As Object, e As EventArgs) Handles btnLimpaTaxaTabela.Click
        txtIDTabelaTaxa.Text = ""
        txtValidade.Text = ""
        txtLCL.Text = ""
        txtFCL.Text = ""
        txtInsides.Text = ""
        DivExcluir.Visible = False
        divInfo.Visible = False
        ModalPopupExtender1.Show()
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
        Else

            If lblContasReceber.Text <> 0 Then
                Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
                Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
            End If

            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (CD_PR, DT_COMPETENCIA, DT_LANCAMENTO,DT_LIQUIDACAO ,DT_VENCIMENTO,ID_TIPO_LANCAMENTO_CAIXA  ,
ID_USUARIO_LANCAMENTO ,ID_USUARIO_LIQUIDACAO,TP_EXPORTACAO) VALUES('P','" & txtCompetencia.Text & "',GETDATE(),CONVERT(DATE,'" & txtLiquidacaoCCProcesso.Text & "',103),CONVERT(DATE,'" & txtLiquidacaoCCProcesso.Text & "',103),7, " & Session("ID_USUARIO") & ", " & Session("ID_USUARIO") & ",'CVEND')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER_ITENS")
            Dim ID_CONTAS_PAGAR_RECEBER_ITENS As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")

            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_PARCEIRO_EMPRESA,DS_HISTORICO_LANCAMENTO,ID_CONTA_PAGAR_RECEBER, VL_LANCAMENTO ,VL_LIQUIDO,ID_ITEM_DESPESA)
               SELECT ID_PARCEIRO_VENDEDOR,'COMISSÃO VENDEDOR – " & txtCompetencia.Text & "'," & ID_CONTAS_PAGAR_RECEBER_ITENS & ",VL_COMISSAO_TOTAL, VL_COMISSAO_TOTAL, (SELECT ID_ITEM_VENDEDOR FROM TB_PARAMETROS)ID_ITEM_VENDEDOR  FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_CABECALHO_COMISSAO_VENDEDOR in (SELECT distinct ID_CABECALHO_COMISSAO_VENDEDOR FROM View_Comissao_Vendedor WHERE COMPETENCIA = '" & txtCompetencia.Text & "')")


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
        VerificaCCPRocesso()
    End Sub

    Private Sub btnFecharCCProcesso_Click(sender As Object, e As EventArgs) Handles btnFecharCCProcesso.Click
        txtLiquidacaoCCProcesso.Text = ""
        divInfoCCProcesso.Visible = False
    End Sub



    'Private Sub dsComissao_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles dsComissao.Selected
    '    Response.Write(Classes.debug.imprimeValoresParametros(e.Command, True))
    '    Response.End()
    'End Sub
End Class