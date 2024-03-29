﻿Public Class ComissaoIndicadorInternacional
    Inherits System.Web.UI.Page
    Dim filtro As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2031 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")


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
            Dim ds As DataSet = Con.ExecutarQuery("SELECT A.ID_CABECALHO_COMISSAO_INTERNACIONAL ,B.ID_DETALHE_COMISSAO_INTERNACIONAL ,B.NR_PROCESSO,B.ID_PARCEIRO_VENDEDOR,B.QT_CNTR,B.VL_TAXA,B.DT_LIQUIDACAO,B.ID_MOEDA,QT_CNTR,DT_EXPORTACAO,DT_COMPETENCIA,NR_QUINZENA
FROM            dbo.TB_CABECALHO_COMISSAO_INTERNACIONAL AS A LEFT OUTER JOIN
                         dbo.TB_DETALHE_COMISSAO_INTERNACIONAL AS B ON B.ID_CABECALHO_COMISSAO_INTERNACIONAL = A.ID_CABECALHO_COMISSAO_INTERNACIONAL
						 WHERE B.ID_DETALHE_COMISSAO_INTERNACIONAL = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DETALHE_COMISSAO_INTERNACIONAL")) Then
                    txtIDAjuste.Text = ds.Tables(0).Rows(0).Item("ID_DETALHE_COMISSAO_INTERNACIONAL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                    txtAjusteProcesso.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_VENDEDOR")) Then
                    ddlAjusteVendedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_VENDEDOR")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                    ddlMoeda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtAjusteBase.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_CNTR")) Then
                    txtAjusteQtdCNTR.Text = ds.Tables(0).Rows(0).Item("QT_CNTR")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) Then
                    txtAjusteLiquidacao.Text = ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EXPORTACAO")) Then
                    lkAjustarComissao.Visible = False
                Else
                    lkAjustarComissao.Visible = True
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INTERNACIONAL")) Then
                    txtIDBaixa.Text = ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INTERNACIONAL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_COMPETENCIA")) Then
                    lblCompetencia.Text = "Competencia: " & ds.Tables(0).Rows(0).Item("DT_COMPETENCIA") & "<br/>"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_QUINZENA")) Then
                    lblQuinzena.Text = "Quinzena: " & ds.Tables(0).Rows(0).Item("NR_QUINZENA") & "<br/>"
                End If


            End If
            Con.Fechar()

        End If
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        CarregaGrid()
    End Sub

    Sub CarregaGrid()
        txtID.Text = ""
        txtlinha.Text = ""
        divErro.Visible = False
        lkAjustarComissao.Visible = True

        If txtQuinzena.Text = "" Then
            lblmsgErro.Text = "É necessario informar a quinzena."
            divErro.Visible = True
        ElseIf txtCompetencia.Text = "" Then
            lblmsgErro.Text = "É necessario informar a competência."
            divErro.Visible = True
        Else


            If ddlFiltro.SelectedValue = 1 Then
                filtro = " AND PARCEIRO_VENDEDOR LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 2 Then
                filtro = " AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 3 Then
                filtro = " AND MBL LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                filtro = " AND HBL LIKE '%" & txtPesquisa.Text & "%'"
            End If


            Dim Sql As String = "SELECT * FROM [dbo].[View_Comissao_Internacional] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' AND NR_QUINZENA ='" & txtQuinzena.Text & "' " & filtro & " ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO"
            dsComissao.SelectCommand = Sql
            dgvComissoes.DataBind()
            ddlFiltro.SelectedValue = 0
            txtPesquisa.Text = ""
            DivGrid2.Visible = True
            lblCompetencia.Text = txtCompetencia.Text
            lblQuinzena.Text = txtQuinzena.Text
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery(Sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INTERNACIONAL")) Then
                    txtIDBaixa.Text = ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INTERNACIONAL")
                Else
                    txtIDBaixa.Text = ""
                End If
            Else
                txtIDBaixa.Text = ""
            End If
            Con.Fechar()

        End If
    End Sub

    Private Sub dgvTabelaComissao_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTabelaComissao.RowCommand
        DivExcluir.Visible = False
        divInfo.Visible = False
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2031 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroExcluir.Text = "Usuário não tem permissão para realizar exclusões"
                DivExcluir.Visible = True
            Else
                Con.ExecutarQuery("DELETE FROM [dbo].[TB_TAXA_COMISSAO_INDICADOR ] WHERE ID_TAXA_COMISSAO_INDICADOR =" & ID)
                dgvTabelaComissao.DataBind()
                divInfo.Visible = True
                lblInfo.Text = "Taxa excluída com sucesso"
                ModalPopupExtender1.Show()
            End If

        ElseIf e.CommandName = "Editar" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_COMISSAO_INDICADOR,DT_VALIDADE_INICIAL,VL_TAXA,ID_PARCEIRO_VENDEDOR,ID_MOEDA FROM TB_TAXA_COMISSAO_INDICADOR  WHERE ID_TAXA_COMISSAO_INDICADOR = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TAXA_COMISSAO_INDICADOR")) Then
                    txtIDTabelaTaxa.Text = ds.Tables(0).Rows(0).Item("ID_TAXA_COMISSAO_INDICADOR").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")) Then
                    txtValidadeTabela.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtTaxaTabela.Text = ds.Tables(0).Rows(0).Item("VL_TAXA").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_VENDEDOR")) Then
                    ddlVendedorTabela.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_VENDEDOR").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                    ddlMoedaTabela.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA").ToString()
                End If
            End If

            ModalPopupExtender1.Show()
        End If
        Con.Fechar()
    End Sub

    Private Sub btnGravaTaxaTabela_Click(sender As Object, e As EventArgs) Handles btnGravaTaxaTabela.Click
        divInfo.Visible = False

        txtTaxaTabela.Text = txtTaxaTabela.Text.Replace(".", "")
        txtTaxaTabela.Text = txtTaxaTabela.Text.Replace(",", ".")
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtIDTabelaTaxa.Text = "" Then
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2031 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroExcluir.Text = "Usuário não tem permissão!"
                DivExcluir.Visible = True
            Else

                Con.ExecutarQuery("INSERT INTO TB_TAXA_COMISSAO_INDICADOR (DT_VALIDADE_INICIAL,VL_TAXA,ID_PARCEIRO_VENDEDOR,ID_MOEDA) VALUES (CONVERT(DATE,'" & txtValidadeTabela.Text & "',103)," & txtTaxaTabela.Text & "," & ddlVendedorTabela.SelectedValue & ", " & ddlMoedaTabela.SelectedValue & ") ")
                divInfo.Visible = True
                lblInfo.Text = "Taxa cadastrada com sucesso!"
                txtIDTabelaTaxa.Text = ""
                txtValidadeTabela.Text = ""
                ddlMoedaTabela.SelectedValue = 0
                ddlVendedorTabela.SelectedValue = 0
                txtTaxaTabela.Text = ""
                dgvTabelaComissao.DataBind()
                ModalPopupExtender1.Show()

            End If

        Else
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2031 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroExcluir.Text = "Usuário não tem permissão!"
                DivExcluir.Visible = True
            Else
                Con.ExecutarQuery("UPDATE TB_TAXA_COMISSAO_INDICADOR SET DT_VALIDADE_INICIAL = CONVERT(DATE,'" & txtValidadeTabela.Text & "',103),VL_TAXA = " & txtTaxaTabela.Text & " ,ID_PARCEIRO_VENDEDOR = " & ddlVendedorTabela.SelectedValue & ",ID_MOEDA = " & ddlMoedaTabela.SelectedValue & " WHERE ID_TAXA_COMISSAO_INDICADOR = " & txtIDTabelaTaxa.Text)
                divInfo.Visible = True
                lblInfo.Text = "Taxa alterada com sucesso"
                txtIDTabelaTaxa.Text = ""
                txtValidadeTabela.Text = ""
                ddlMoedaTabela.SelectedValue = 0
                ddlVendedorTabela.SelectedValue = 0
                txtTaxaTabela.Text = ""
                dgvTabelaComissao.DataBind()
                ModalPopupExtender1.Show()
            End If
        End If
    End Sub

    Private Sub btnFecharTabela_Click(sender As Object, e As EventArgs) Handles btnFecharTabela.Click
        txtIDTabelaTaxa.Text = ""
        txtValidadeTabela.Text = ""
        ddlMoedaTabela.SelectedValue = 0
        ddlVendedorTabela.SelectedValue = 0
        txtTaxaTabela.Text = ""
        divInfo.Visible = False
        DivExcluir.Visible = False
    End Sub

    Private Sub lkCSV_Click(sender As Object, e As EventArgs) Handles lkCSV.Click
        Dim SQL As String = "SELECT COMPETENCIA,NR_QUINZENA,NR_PROCESSO,PARCEIRO_VENDEDOR AS PARCEIRO_INDICADOR  ,PARCEIRO_CLIENTE,MBL,HBL,TIPO_ESTUFAGEM,QT_CNTR,MOEDA,VL_TAXA,VL_COMISSAO,PARCEIRO_AGENTE_INTERNACIONAL,[DT_EMBARQUE(ETD)],[DT_CHEGADA(ETA)],DT_LIQUIDACAO,DT_EXPORTACAO FROM [dbo].[View_Comissao_Internacional] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' AND NR_QUINZENA ='" & txtQuinzena.Text & "'  " & filtro & " ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO"

        Classes.Excel.exportaExcel(SQL, "ComissaoInternacional")
    End Sub

    Private Sub btnGerarComissao_Click(sender As Object, e As EventArgs) Handles btnGerarComissao.Click
        btnGerarComissao.Visible = False
        divAtencaoGerarComissao.Visible = False
        divSuccessGerarComissao.Visible = False
        divErroGerarComissao.Visible = False
        divInfoGerarComissao.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim CONTADOR As Integer = 0

        If lblContasReceber.Text = "" Then
            lblContasReceber.Text = 0
        End If

        If lblCompetenciaSobrepor.Text = "" Then
            lblCompetenciaSobrepor.Text = 0
        End If


        If txtNovaCompetencia.Text = "" Or txtLiquidacaoInicial.Text = "" Or txtLiquidacaoFinal.Text = "" Or txtNovaQuinzena.Text = "" Then
            lblErroGerarComissao.Text = "Preencha os campos obrigatórios."
            divErroGerarComissao.Visible = True

        Else
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2031 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroExcluir.Text = "Usuário não tem permissão!"
                DivExcluir.Visible = True
            Else
                Dim dsQtd As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM FN_INDICADOR_INTERNACIONAL('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL ")
                If dsQtd.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroGerarComissao.Text = "Não há processos em aberto para comissão nesse período!"
                    divErroGerarComissao.Visible = True

                Else

                    dsQtd = Con.ExecutarQuery("SELECT ID_PARCEIRO_VENDEDOR,(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_PARCEIRO_VENDEDOR)NM_RAZAO,
CASE WHEN (SELECT ID_TAXA_COMISSAO_INDICADOR FROM [FN_TAXA_COMISSAO_INDICADOR](A.ID_PARCEIRO_VENDEDOR,A.DT_LIQUIDACAO) )IS NULL THEN '0' ELSE 1 END TAXA
FROM FN_INDICADOR_INTERNACIONAL('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') A ")
                    If dsQtd.Tables(0).Rows.Count = 0 Then
                        lblErroGerarComissao.Text = "Não há taxa cadastrada para os indicadores!"
                        divErroGerarComissao.Visible = True
                        ModalPopupExtender3.Show()
                        Exit Sub
                    Else

                        lblErroGerarComissao.Text = ""
                        For Each linha As DataRow In dsQtd.Tables(0).Rows
                            If linha("TAXA") = 0 Then
                                CONTADOR = CONTADOR + 1
                                lblErroGerarComissao.Text &= "Não há taxa cadastrada para: " & linha("NM_RAZAO") & "<br/>"
                            End If

                        Next
                        If CONTADOR <> 0 Then
                            divErroGerarComissao.Visible = True
                            ModalPopupExtender3.Show()

                            Exit Sub
                        End If

                    End If

                    If txtObs.Text = "" Then
                        txtObs.Text = "NULL"
                    Else
                        txtObs.Text = "'" & txtObs.Text & "'"
                    End If



                    Dim NOVA_COMPETECIA As String = txtNovaCompetencia.Text
                    NOVA_COMPETECIA = NOVA_COMPETECIA.Replace("/", "")
                    Dim dsInsert As DataSet
                    Dim cabecalho As String


                    Con.ExecutarQuery("DELETE FROM TB_DETALHE_COMISSAO_INTERNACIONAL WHERE ID_CABECALHO_COMISSAO_INTERNACIONAL 
IN (SELECT ID_CABECALHO_COMISSAO_INTERNACIONAL FROM TB_CABECALHO_COMISSAO_INTERNACIONAL WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "' AND NR_QUINZENA = '" & txtNovaQuinzena.Text & "')")

                    Con.ExecutarQuery("DELETE FROM TB_CABECALHO_COMISSAO_INTERNACIONAL WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "' AND NR_QUINZENA = '" & txtNovaQuinzena.Text & "' ")


                    If lblContasReceber.Text <> 0 Then
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)

                        divInfoGerarComissao.Visible = True
                        lblInfoGerarComissao.Text = "Necessário exportar competência para a conta corrente do processo!"
                    End If

                    dsInsert = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_INTERNACIONAL  (DT_COMPETENCIA,NR_QUINZENA,DT_LIQUIDACAO_INICIAL,DT_LIQUIDACAO_FINAL,ID_USUARIO_GERACAO,DT_GERACAO,DS_OBSERVACAO) VALUES('" & NOVA_COMPETECIA & "','" & txtNovaQuinzena.Text & "',CONVERT(DATE,'" & txtLiquidacaoInicial.Text & "',103),CONVERT(DATE,'" & txtLiquidacaoFinal.Text & "',103)," & Session("ID_USUARIO") & ", getdate()," & txtObs.Text & " ) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_INTERNACIONAL  ")
                    cabecalho = dsInsert.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INTERNACIONAL")

                    Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_INTERNACIONAL  (ID_CABECALHO_COMISSAO_INTERNACIONAL,ID_BL,NR_PROCESSO,ID_PARCEIRO_VENDEDOR,QT_CNTR,ID_MOEDA,VL_TAXA,VL_COMISSAO,DT_LIQUIDACAO) 
SELECT " & cabecalho & ",A.ID_BL,A.NR_PROCESSO,A.ID_PARCEIRO_VENDEDOR,QT_CNTR,C.ID_MOEDA,C.VL_TAXA,
QT_CNTR* C.VL_TAXA AS VL_COMISSAO, DT_LIQUIDACAO 
FROM FN_INDICADOR_INTERNACIONAL('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') A
OUTER APPLY [FN_TAXA_COMISSAO_INDICADOR](A.ID_PARCEIRO_VENDEDOR,A.DT_LIQUIDACAO) C  
WHERE DT_PAGAMENTO_EXP IS NULL AND A.VL_TAXA > 0")

                    divSuccessGerarComissao.Visible = True
                    lblSuccessGerarComissao.Text = "Comissão gerada com sucesso!"
                    txtObs.Text = txtObs.Text.Replace("NULL", "")
                    txtObs.Text = txtObs.Text.Replace("'", "")
                    CarregaGrid()
                End If

            End If

        End If


        btnGerarComissao.Visible = True
        ModalPopupExtender3.Show()
    End Sub

    Private Sub txtNovaCompetencia_TextChanged(sender As Object, e As EventArgs) Handles txtNovaCompetencia.TextChanged
        VerificaCompetencia()
    End Sub

    Sub VerificaCompetencia()
        Dim Con As New Conexao_sql
        Con.Conectar()

        'Verifica se a competencia já existe
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CABECALHO_COMISSAO_INTERNACIONAL,DT_EXPORTACAO FROM View_Comissao_Internacional WHERE COMPETENCIA = '" & txtNovaCompetencia.Text & "' AND NR_QUINZENA = '" & txtNovaQuinzena.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            divAtencaoGerarComissao.Visible = True
            lblAtencaoGerarComissao.Text = "COMPETENCIA JÁ EXISTE!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
            lblCompetenciaSobrepor.Text = ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INTERNACIONAL")
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EXPORTACAO")) Then
                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CINT' AND DT_COMPETENCIA = '" & txtNovaCompetencia.Text & "' AND NR_QUINZENA = '" & txtNovaQuinzena.Text & "'")
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
        ds = Con.ExecutarQuery("SELECT ID_CABECALHO_COMISSAO_INTERNACIONAL  FROM View_Comissao_Internacional  WHERE COMPETENCIA = '" & Competencia_Anterior.ToString() & "' AND DT_EXPORTACAO IS NULL")
        If ds.Tables(0).Rows.Count > 0 Then
            divInfoGerarComissao.Visible = True
            lblInfoGerarComissao.Text = "Competência imediatamente anterior não exportada para a conta corrente do processo."
        Else
            divInfoGerarComissao.Visible = False
        End If

        ModalPopupExtender3.Show()

    End Sub

    Private Sub btnFecharGerarComissao_Click(sender As Object, e As EventArgs) Handles btnFecharGerarComissao.Click
        divAtencaoGerarComissao.Visible = False
        divSuccessGerarComissao.Visible = False
        divErroGerarComissao.Visible = False
        divInfoGerarComissao.Visible = False
        txtLiquidacaoInicial.Text = ""
        txtLiquidacaoFinal.Text = ""
        ModalPopupExtender3.Hide()
        txtNovaCompetencia.Text = ""
        txtNovaQuinzena.Text = ""
    End Sub

    Private Sub btnAlteraComisaao_Click(sender As Object, e As EventArgs) Handles btnAlteraComisaao.Click
        divSuccesAjuste.Visible = False
        divErroAjuste.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2031 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroAjuste.Visible = True
            lblErroAjuste.Text = "Usuário não possui permissão!"
            Exit Sub

        Else
            If txtIDAjuste.Text = "" Then

                lblErroAjuste.Text = "É necessario selecionar um registro para alterar."
                divErroAjuste.Visible = True

            ElseIf txtAjusteProcesso.Text = "" Or ddlAjusteVendedor.SelectedValue = 0 Or txtAjusteQtdCNTR.Text = "" Or ddlMoeda.SelectedValue = 0 Or txtAjusteBase.Text = "" Or txtAjusteLiquidacao.Text = "" Then

                lblErroAjuste.Text = "Preencha os campos obrigatórios."
                divErroAjuste.Visible = True
            Else

                txtAjusteBase.Text = txtAjusteBase.Text.Replace(".", "")
                txtAjusteBase.Text = txtAjusteBase.Text.Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_DETALHE_COMISSAO_INTERNACIONAL SET NR_PROCESSO = '" & txtAjusteProcesso.Text & "',ID_PARCEIRO_VENDEDOR= " & ddlAjusteVendedor.SelectedValue & ",ID_MOEDA = " & ddlMoeda.SelectedValue & ",VL_TAXA = " & txtAjusteBase.Text & ",DT_LIQUIDACAO =  CONVERT(DATE,'" & txtAjusteLiquidacao.Text & "',103), QT_CNTR = " & txtAjusteQtdCNTR.Text & " WHERE ID_DETALHE_COMISSAO_INTERNACIONAL  = " & txtIDAjuste.Text)
                divSuccesAjuste.Visible = True
                lblSuccesAjuste.Text = "Comissão alterada com sucesso!"

            End If

        End If
        ModalPopupExtender5.Show()

    End Sub

    Private Sub btnExcluirAlteraComisaao_Click(sender As Object, e As EventArgs) Handles btnExcluirAlteraComisaao.Click
        divSuccesAjuste.Visible = False
        divErroAjuste.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2031 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

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
                Con.ExecutarQuery("DELETE FROM TB_DETALHE_COMISSAO_INTERNACIONAL  WHERE ID_DETALHE_COMISSAO_INTERNACIONAL  = " & txtIDAjuste.Text)
                divSuccess.Visible = True
                lblmsgSuccess.Text = "Comissão deletada com sucesso!"
                dgvComissoes.DataBind()
            End If

        End If

    End Sub

    Private Sub btnFecharAlteraComisaao_Click(sender As Object, e As EventArgs) Handles btnFecharAlteraComisaao.Click
        txtAjusteProcesso.Text = ""
        ddlMoeda.SelectedValue = 0
        ddlAjusteVendedor.SelectedValue = 0
        txtAjusteBase.Text = ""
        txtAjusteLiquidacao.Text = ""
        txtID.Text = ""
        txtIDAjuste.Text = ""
        txtAjusteQtdCNTR.Text = ""
        lkAjustarComissao.Visible = False
        divSuccesAjuste.Visible = False
        divErroAjuste.Visible = False

        For i As Integer = 0 To dgvComissoes.Rows.Count - 1
            dgvComissoes.Rows(i).CssClass = "Normal"

        Next
    End Sub

    Private Sub btnLimpaTaxaTabela_Click(sender As Object, e As EventArgs) Handles btnLimpaTaxaTabela.Click
        txtIDTabelaTaxa.Text = ""
        txtValidadeTabela.Text = ""
        ddlMoedaTabela.SelectedValue = 0
        ddlVendedorTabela.SelectedValue = 0
        txtTaxaTabela.Text = ""
        divInfo.Visible = False
        DivExcluir.Visible = False
        ModalPopupExtender1.Show()
    End Sub

    Private Sub txtNovaQuinzena_TextChanged(sender As Object, e As EventArgs) Handles txtNovaQuinzena.TextChanged
        VerificaCompetencia()
    End Sub

    Private Sub btnFecharBaixa_Click(sender As Object, e As EventArgs) Handles btnFecharBaixa.Click
        CarregaGrid()
        txtIDBaixa.Text = ""
        txtContrato.Text = ""
        txtLiquidacao.Text = ""
        ddlContaBancaria.SelectedValue = 0
        txtIDCC.Text = ""
        ModalPopupExtender1.Hide()

    End Sub


    Private Sub btnSalvarBaixa_Click(sender As Object, e As EventArgs) Handles btnSalvarBaixa.Click
        divErroBaixa.Visible = False
        divSuccessBaixa.Visible = False
        divSuccess.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtIDBaixa.Text = "" Then
            lblErroBaixa.Text = "É necessário selecionar um registro para efetuar a baixa!"
            divErroBaixa.Visible = True
            ModalPopupExtender2.Show()

        ElseIf txtLiquidacao.Text = "" Then
            lblErroBaixa.Text = "É necessário informar a data de liquidação para efetuar a baixa!"
            divErroBaixa.Visible = True
            ModalPopupExtender2.Show()

        ElseIf txtContrato.Text = "" Then
            lblErroBaixa.Text = "É necessário informar o numero do contrato para efetuar a baixa!"
            divErroBaixa.Visible = True
            ModalPopupExtender2.Show()

        ElseIf ddlContaBancaria.SelectedValue = 0 Then
            lblErroBaixa.Text = "É necessário informar a conta bancária para efetuar a baixa!"
            divErroBaixa.Visible = True
            ModalPopupExtender2.Show()

        Else

            If txtIDCC.Text = "" Then
                GravaCCProcesso()
            End If


            Dim dsVerificacao As DataSet = Con.ExecutarQuery("SELECT DT_LIQUIDACAO FROM TB_CONTA_PAGAR_RECEBER WHERE DT_LIQUIDACAO IS NOT NULL AND ID_CONTA_PAGAR_RECEBER =" & txtIDCC.Text)

            If dsVerificacao.Tables(0).Rows.Count > 0 And lblContador.Text = "" Then

                divInfoBaixa.Visible = True
                lblInfoBaixa.Text = "REGISTRO JÁ LIQUIDADO!<br/>DESEJA SOBREPOR AS INFORMAÇÕES?"
                lblContador.Text = 1
                ModalPopupExtender2.Show()
                btnSalvarBaixa.Text = "Sobrepor Informações"
                Exit Sub

            Else
                lblContador.Text = ""
                btnSalvarBaixa.Text = "Baixar"
                divInfoBaixa.Visible = False
                Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtLiquidacao.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & ", NR_DOCUMENTO = '" & txtContrato.Text & "' WHERE ID_CONTA_PAGAR_RECEBER =" & txtIDCC.Text)

                For Each linhaMoeda As GridViewRow In dgvMoedas.Rows
                    Dim IDMoeda As String = CType(linhaMoeda.FindControl("lblMoeda"), Label).Text
                    Dim Cambio As String = CType(linhaMoeda.FindControl("txtValorCambio"), TextBox).Text

                    Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER_ITENS FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_MOEDA = " & IDMoeda & " AND ID_CONTA_PAGAR_RECEBER = " & txtIDCC.Text)

                    Cambio = Cambio.Replace(".", "")
                    Cambio = Cambio.Replace(",", ".")

                    If ds.Tables(0).Rows.Count > 0 Then
                        If Cambio = "" Then
                            lblErroBaixa.Text = "É necessário informar o valor de câmbio!"
                            divErroBaixa.Visible = True
                            ModalPopupExtender2.Show()
                            Exit Sub
                        Else
                            For Each linhads As DataRow In ds.Tables(0).Rows
                                Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER_ITENS] SET [DT_CAMBIO] = CONVERT(DATE,'" & txtLiquidacao.Text & "',103),VL_CAMBIO = " & Cambio & " ,VL_LANCAMENTO =  VL_TAXA_CALCULADO * " & Cambio & ",VL_LIQUIDO =  VL_TAXA_CALCULADO * " & Cambio & " WHERE ID_CONTA_PAGAR_RECEBER_ITENS =" & linhads.Item("ID_CONTA_PAGAR_RECEBER_ITENS").ToString())
                            Next
                        End If
                    End If
                Next

                dgvComissoes.DataBind()


                Con.Fechar()
                lblmsgSuccess.Text = "Gravação realizada com sucesso!"
                divSuccess.Visible = True
                lblCompetencia.Text = ""
                lblQuinzena.Text = ""
                txtIDBaixa.Text = ""
                txtIDCC.Text = ""
                txtContrato.Text = ""
                txtLiquidacao.Text = ""
                ddlContaBancaria.SelectedValue = 0
                ModalPopupExtender2.Hide()

            End If

        End If
    End Sub

    Sub GravaCCProcesso()
        If lblContasReceber.Text = "" Then
            lblContasReceber.Text = 0
        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        If lblContasReceber.Text <> 0 Then
            Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
            Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
        End If

        Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (CD_PR, DT_COMPETENCIA,NR_QUINZENA, DT_LANCAMENTO ,DT_VENCIMENTO,ID_CONTA_BANCARIA,ID_TIPO_LANCAMENTO_CAIXA  ,
        ID_USUARIO_LANCAMENTO ,TP_EXPORTACAO) VALUES('P','" & txtCompetencia.Text & "','" & txtQuinzena.Text & "',GETDATE(),GETDATE()," & ddlContaBancaria.SelectedValue & ",7, " & Session("ID_USUARIO") & ", 'CINT')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER")
        Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")

        Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_BL,ID_MOEDA,ID_PARCEIRO_EMPRESA,DS_HISTORICO_LANCAMENTO,ID_CONTA_PAGAR_RECEBER, VL_LANCAMENTO ,VL_LIQUIDO,VL_TAXA_CALCULADO,ID_ITEM_DESPESA )
                        SELECT ID_BL,ID_MOEDA,ID_PARCEIRO_VENDEDOR,'COMISSÃO INDICADOR INTERNACIONAL – " & txtCompetencia.Text & "-" & txtQuinzena.Text & "'," & ID_CONTA_PAGAR_RECEBER & ",0,0, VL_COMISSAO,(SELECT ID_ITEM_INDICADOR_INTERNACIONAL FROM TB_PARAMETROS)ID_ITEM_INDICADOR_INTERNACIONAL FROM TB_DETALHE_COMISSAO_INTERNACIONAL WHERE ID_CABECALHO_COMISSAO_INTERNACIONAL IN (SELECT ID_CABECALHO_COMISSAO_INTERNACIONAL FROM View_Comissao_Internacional WHERE COMPETENCIA = '" & txtCompetencia.Text & "' AND NR_QUINZENA = '" & txtQuinzena.Text & "' AND ID_CABECALHO_COMISSAO_INTERNACIONAL = " & txtIDBaixa.Text & " ) ")

        Con.ExecutarQuery("UPDATE TB_CABECALHO_COMISSAO_INTERNACIONAL SET DT_EXPORTACAO = GETDATE(),ID_USUARIO_EXPORTACAO = " & Session("ID_USUARIO") & " WHERE DT_COMPETENCIA = '" & txtCompetencia.Text.Substring(0, 2) & txtCompetencia.Text.Substring(3, 4) & "' AND NR_QUINZENA = '" & txtQuinzena.Text & "' AND ID_CABECALHO_COMISSAO_INTERNACIONAL = " & txtIDBaixa.Text)

        txtIDCC.Text = ID_CONTA_PAGAR_RECEBER

    End Sub

    Sub VerificaCCPRocesso()
        Dim Con As New Conexao_sql
        Con.Conectar()

        'Verifica se a competencia já existe
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CINT' AND DT_COMPETENCIA = '" & txtCompetencia.Text & "' AND NR_QUINZENA = '" & txtQuinzena.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            divInfoBaixa.Visible = True
            lblInfoBaixa.Text = "COMPETENCIA JÁ EXPORTADA!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
            lblContasReceber.Text = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
        Else
            lblContasReceber.Text = 0
            divInfoBaixa.Visible = False
        End If
        ModalPopupExtender2.Show()
    End Sub

    Private Sub ddlContaBancaria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContaBancaria.SelectedIndexChanged
        VerificaCCPRocesso()
    End Sub

    Private Sub txtLiquidacaoFinal_TextChanged(sender As Object, e As EventArgs) Handles txtLiquidacaoFinal.TextChanged
        VerificaCompetencia()
    End Sub
End Class