Public Class ComissaoIndicadorNacional
    Inherits System.Web.UI.Page
    Dim filtro As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2030 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub

    '    Private Sub dgvComissoes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvComissoes.RowCommand
    '        divSuccess.Visible = False
    '        divErro.Visible = False


    '        If e.CommandName = "Selecionar" Then
    '            If txtlinha.Text <> "" Then
    '                dgvComissoes.Rows(txtlinha.Text).CssClass = "Normal"

    '            End If
    '            Dim ID As String = e.CommandArgument


    '            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

    '            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
    '            txtlinha.Text = txtlinha.Text.Replace("|", "")


    '            dgvComissoes.Rows(txtlinha.Text).CssClass = "selected1"

    '            lkAjustarComissao.Visible = True
    '            Dim Con As New Conexao_sql
    '            Con.Conectar()
    '            Dim ds As DataSet = Con.ExecutarQuery("SELECT DT_COMPETENCIA,NR_QUINZENA,A.ID_CABECALHO_COMISSAO_NACIONAL ,B.ID_DETALHE_COMISSAO_NACIONAL ,B.NR_PROCESSO,B.ID_PARCEIRO_INDICADOR,B.VL_COMISSAO,B.VL_TAXA,B.DT_LIQUIDACAO,B.ID_MOEDA,A.DT_EXPORTACAO
    'FROM            dbo.TB_CABECALHO_COMISSAO_NACIONAL AS A LEFT OUTER JOIN
    '                         dbo.TB_DETALHE_COMISSAO_NACIONAL AS B ON B.ID_CABECALHO_COMISSAO_NACIONAL = A.ID_CABECALHO_COMISSAO_NACIONAL
    '						 WHERE B.ID_DETALHE_COMISSAO_NACIONAL = " & txtID.Text)
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DETALHE_COMISSAO_NACIONAL")) Then
    '                    txtIDAjuste.Text = ds.Tables(0).Rows(0).Item("ID_DETALHE_COMISSAO_NACIONAL")
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
    '                    txtAjusteProcesso.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")) Then
    '                    ddlAjusteVendedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
    '                    ddlMoeda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
    '                    txtAjusteBase.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
    '                End If

    '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) Then
    '                    txtAjusteLiquidacao.Text = ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")
    '                End If

    '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EXPORTACAO")) Then
    '                    lkAjustarComissao.Visible = False
    '                Else
    '                    lkAjustarComissao.Visible = True
    '                End If

    '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_COMPETENCIA")) Then
    '                    lblCompetenciaCCProcesso.Text = ds.Tables(0).Rows(0).Item("DT_COMPETENCIA")
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_QUINZENA")) Then
    '                    lblQuinzena.Text = ds.Tables(0).Rows(0).Item("NR_QUINZENA")
    '                End If
    '            End If
    '            Con.Fechar()

    '        End If
    '    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        CarregaGrid()
    End Sub

    Sub CarregaGrid()

        '  lkAjustarComissao.Visible = True

        txtID.Text = ""
        txtlinha.Text = ""
        divErro.Visible = False

        If txtQuinzena.Text = "" Then
            lblmsgErro.Text = "É necessario informar a quinzena."
            divErro.Visible = True
        ElseIf txtCompetencia.Text = "" Then
            lblmsgErro.Text = "É necessario informar a competência."
            divErro.Visible = True
        Else

            If ddlFiltro.SelectedValue = 1 Then
                filtro = " AND PARCEIRO_INDICADOR LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 2 Then
                filtro = " AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 3 Then
                filtro = " AND MBL LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                filtro = " AND HBL LIKE '%" & txtPesquisa.Text & "%'"
            End If

            dsComissao.SelectCommand = "SELECT * FROM [dbo].[View_Comissao_Nacional] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' AND NR_QUINZENA ='" & txtQuinzena.Text & "' " & filtro & " ORDER BY PARCEIRO_INDICADOR,NR_PROCESSO"
            dgvComissoes.DataBind()
            ddlFiltro.SelectedValue = 0
            txtPesquisa.Text = ""
            DivGrid2.Visible = True
            lblQuinzena.Text = txtQuinzena.Text
            lblCompetenciaCCProcesso.Text = txtCompetencia.Text

        End If
    End Sub
    Private Sub lkCSV_Click(sender As Object, e As EventArgs) Handles lkCSV.Click
        Dim SQL As String = "SELECT DT_BAIXA_NF_TOTVS AS BAIXA_TOTVS, NR_NF_TOTVS AS NF_TOTVS,COMPETENCIA,NR_QUINZENA,NR_PROCESSO,PARCEIRO_INDICADOR,HBL,MBL,PARCEIRO_CLIENTE,TIPO_ESTUFAGEM,MOEDA,VL_TAXA,VL_CAMBIO,VL_COMISSAO,DT_LIQUIDACAO,PARCEIRO_IMPORTADOR,DT_EXPORTACAO FROM [dbo].[View_Comissao_Nacional] WHERE COMPETENCIA = '" & txtCompetencia.Text & "' and NR_QUINZENA = '" & txtQuinzena.Text & "' " & filtro & " ORDER BY PARCEIRO_INDICADOR,NR_PROCESSO"

        Classes.Excel.exportaExcel(SQL, "ComissaoNacional")
    End Sub

    Private Sub btnGerarComissao_Click(sender As Object, e As EventArgs) Handles btnGerarComissao.Click
        btnGerarComissao.Visible = False
        divAtencaoGerarComissao.Visible = False
        divSuccessGerarComissao.Visible = False
        divErroGerarComissao.Visible = False
        divInfoGerarComissao.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()


        If txtNovaCompetencia.Text = "" Or txtLiquidacaoInicial.Text = "" Or txtLiquidacaoFinal.Text = "" Or txtNovaQuinzena.Text = "" Then
            lblErroGerarComissao.Text = "Preencha os campos obrigatórios."
            divErroGerarComissao.Visible = True
            ModalPopupExtender3.Show()

        Else
            'VerificaCompetencia()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2030 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroGerarComissao.Text = "Usuário não tem permissão!"
                divErroGerarComissao.Visible = True
                ModalPopupExtender3.Show()

            Else
                Dim dsQtd As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM FN_INDICADOR_NACIONAL('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL ")
                If dsQtd.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroGerarComissao.Text = "Não há processos em aberto para comissão nesse período!"
                    divErroGerarComissao.Visible = True
                    ModalPopupExtender3.Show()

                Else

                    If txtObs.Text = "" Then
                        txtObs.Text = "NULL"
                    Else
                        txtObs.Text = "'" & txtObs.Text & "'"
                    End If

                    Dim NOVA_COMPETECIA As String = txtNovaCompetencia.Text
                    NOVA_COMPETECIA = NOVA_COMPETECIA.Replace("/", "")
                    Dim dsInsert As DataSet
                    Dim cabecalho As String


                    Con.ExecutarQuery("DELETE FROM TB_DETALHE_COMISSAO_NACIONAL WHERE ID_CABECALHO_COMISSAO_NACIONAL 
IN (SELECT ID_CABECALHO_COMISSAO_NACIONAL FROM TB_CABECALHO_COMISSAO_NACIONAL WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "' AND NR_QUINZENA = '" & txtNovaQuinzena.Text & "')")
                    Con.ExecutarQuery("DELETE FROM TB_CABECALHO_COMISSAO_NACIONAL WHERE DT_COMPETENCIA = '" & NOVA_COMPETECIA & "' AND NR_QUINZENA = '" & txtNovaQuinzena.Text & "'")

                    If lblContasReceber.Text <> 0 Then
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)

                        'divInfoGerarComissao.Visible = True
                        'lblInfoGerarComissao.Text = "Necessário exportar competência para a conta corrente do processo!"
                    End If


                    dsInsert = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_NACIONAL  (DT_COMPETENCIA,NR_QUINZENA,DT_LIQUIDACAO_INICIAL,DT_LIQUIDACAO_FINAL,ID_USUARIO_GERACAO,DT_GERACAO,DS_OBSERVACAO) VALUES('" & NOVA_COMPETECIA & "','" & txtNovaQuinzena.Text & "',CONVERT(DATE,'" & txtLiquidacaoInicial.Text & "',103),CONVERT(DATE,'" & txtLiquidacaoFinal.Text & "',103)," & Session("ID_USUARIO") & ", getdate()," & txtObs.Text & " ) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_INTERNACIONAL  ")
                    cabecalho = dsInsert.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INTERNACIONAL")

                    Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_NACIONAL (ID_CABECALHO_COMISSAO_NACIONAL,ID_BL,NR_PROCESSO,ID_PARCEIRO_INDICADOR,ID_BL_TAXA,ID_MOEDA,VL_TAXA,VL_CAMBIO,DT_CAMBIO,VL_COMISSAO,DT_LIQUIDACAO)
SELECT " & cabecalho & ", ID_BL,NR_PROCESSO,ID_PARCEIRO_EMPRESA,ID_BL_TAXA,ID_MOEDA,VL_TAXA_CALCULADO,VL_CAMBIO,DT_CAMBIO,VL_TAXA_CALCULADO * VL_CAMBIO AS COMISSAO,DT_LIQUIDACAO FROM FN_INDICADOR_NACIONAL('" & txtLiquidacaoInicial.Text & "','" & txtLiquidacaoFinal.Text & "') WHERE DT_PAGAMENTO_EXP IS NULL ")



                    CarregaGrid()
                    lblmsgSuccess.Text = "Comissão gerada com sucesso!"
                    divSuccess.Visible = True
                    divInfoCCProcesso.Visible = False
                    divAtencaoGerarComissao.Visible = False
                    divSuccessGerarComissao.Visible = False
                    divErroGerarComissao.Visible = False
                    divInfoGerarComissao.Visible = False
                    txtLiquidacaoInicial.Text = ""
                    txtLiquidacaoFinal.Text = ""
                    txtNovaCompetencia.Text = ""
                    txtNovaQuinzena.Text = ""
                    txtObs.Text = ""
                    ModalPopupExtender3.Hide()
                    divErro.Visible = False
                End If
            End If

        End If
        divInfoCCProcesso.Visible = False
        btnGerarComissao.Visible = True
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefault()", True)

    End Sub

    Private Sub txtNovaQuinzena_TextChanged(sender As Object, e As EventArgs) Handles txtNovaQuinzena.TextChanged
        VerificaCompetencia()
        VerificaCCPRocesso()
        If divInfoCCProcesso.Visible = True Then
            divAtencaoGerarComissao.Visible = False
        End If
    End Sub
    Sub VerificaCompetencia()
        Dim Con As New Conexao_sql
        Con.Conectar()

        'Verifica se a competencia já existe
        Dim ds As DataSet = Con.ExecutarQuery("Select ID_CABECALHO_COMISSAO_NACIONAL,DT_EXPORTACAO FROM View_Comissao_Nacional WHERE COMPETENCIA = '" & txtNovaCompetencia.Text & "' AND NR_QUINZENA = '" & txtNovaQuinzena.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            divAtencaoGerarComissao.Visible = True
            lblAtencaoGerarComissao.Text = "COMPETENCIA JÁ EXISTE!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
            lblCompetenciaSobrepor.Text = ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_NACIONAL")

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EXPORTACAO")) Then
                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CNAC' AND DT_COMPETENCIA = '" & txtNovaCompetencia.Text & "' AND NR_QUINZENA = '" & txtNovaQuinzena.Text & "'")
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
        ds = Con.ExecutarQuery("SELECT ID_CABECALHO_COMISSAO_NACIONAL  FROM View_Comissao_Nacional  WHERE COMPETENCIA = '" & Competencia_Anterior.ToString() & "' AND DT_EXPORTACAO IS NULL")
        If ds.Tables(0).Rows.Count > 0 Then
            divInfoGerarComissao.Visible = True
            lblInfoGerarComissao.Text = "Competência imediatamente anterior não exportada para a conta corrente do processo."
        Else
            divInfoGerarComissao.Visible = False
        End If

        ModalPopupExtender3.Show()

    End Sub
    Private Sub btnFecharGerarComissao_Click(sender As Object, e As EventArgs) Handles btnFecharGerarComissao.Click
        divInfoCCProcesso.Visible = False
        divAtencaoGerarComissao.Visible = False
        divSuccessGerarComissao.Visible = False
        divErroGerarComissao.Visible = False
        divInfoGerarComissao.Visible = False
        txtLiquidacaoInicial.Text = ""
        txtLiquidacaoFinal.Text = ""
        txtNovaCompetencia.Text = ""
        txtNovaQuinzena.Text = ""
        ModalPopupExtender3.Hide()
        divSuccess.Visible = False
        divErro.Visible = False

    End Sub

    Private Sub btnAlteraComisaao_Click(sender As Object, e As EventArgs) Handles btnAlteraComisaao.Click
        divSuccesAjuste.Visible = False
        divErroAjuste.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2030 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroAjuste.Visible = True
            lblErroAjuste.Text = "Usuário não possui permissão!"
            Exit Sub

        Else
            If txtIDAjuste.Text = "" Then

                lblErroAjuste.Text = "É necessario selecionar um registro para alterar."
                divErroAjuste.Visible = True

            ElseIf txtAjusteProcesso.Text = "" Or ddlAjusteVendedor.SelectedValue = 0 Or ddlMoeda.SelectedValue = 0 Or txtAjusteBase.Text = "" Or txtAjusteLiquidacao.Text = "" Then

                lblErroAjuste.Text = "Preencha os campos obrigatórios."
                divErroAjuste.Visible = True
            Else

                txtAjusteBase.Text = txtAjusteBase.Text.Replace(".", "")
                txtAjusteBase.Text = txtAjusteBase.Text.Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_DETALHE_COMISSAO_NACIONAL  SET NR_PROCESSO = '" & txtAjusteProcesso.Text & "',ID_PARCEIRO_INDICADOR= " & ddlAjusteVendedor.SelectedValue & ",ID_MOEDA = " & ddlMoeda.SelectedValue & ",VL_TAXA = " & txtAjusteBase.Text & ",DT_LIQUIDACAO =  CONVERT(DATE,'" & txtAjusteLiquidacao.Text & "',103) WHERE ID_DETALHE_COMISSAO_NACIONAL  = " & txtIDAjuste.Text)
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
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2030 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

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
                Con.ExecutarQuery("DELETE FROM TB_DETALHE_COMISSAO_NACIONAL  WHERE ID_DETALHE_COMISSAO_NACIONAL  = " & txtIDAjuste.Text)
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
        lkAjustarComissao.Visible = False
        divSuccesAjuste.Visible = False
        divErroAjuste.Visible = False

        For i As Integer = 0 To dgvComissoes.Rows.Count - 1
            dgvComissoes.Rows(i).CssClass = "Normal"

        Next
    End Sub

    Private Sub lkGravar_CCProcesso_Click(sender As Object, e As EventArgs) Handles lkGravar_CCProcesso.Click
        divSuccesAjuste.Visible = False
        divErroAjuste.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()


        If lblContasReceber.Text = "" Then
            lblContasReceber.Text = 0
        End If


        If lblQuinzena.Text = "" Then
            lblErroCCProcesso.Text = "É necessario informar a quinzena."
            divErroCCProcesso.Visible = True
            ModalPopupExtender6.Show()

        ElseIf lblCompetenciaCCProcesso.Text = "" Then
            lblErroCCProcesso.Text = "É necessario informar a competência."
            divErroCCProcesso.Visible = True
            ModalPopupExtender6.Show()

        ElseIf txtLiquidacaoCCProcesso.Text = "" Then
            lblErroCCProcesso.Text = "É necessario informar a data de liquidação."
            divErroCCProcesso.Visible = True
            ModalPopupExtender6.Show()

        ElseIf ddlContaBancaria.SelectedValue = 0 Then
            lblErroCCProcesso.Text = "É necessario informar a conta bancária."
            divErroCCProcesso.Visible = True
            ModalPopupExtender6.Show()

        Else

            If lblContasReceber.Text <> 0 Then
                Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
                Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & lblContasReceber.Text)
            End If

            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (CD_PR, DT_COMPETENCIA,NR_QUINZENA, DT_LANCAMENTO ,DT_LIQUIDACAO,DT_VENCIMENTO,ID_CONTA_BANCARIA,ID_TIPO_LANCAMENTO_CAIXA ,
        ID_USUARIO_LANCAMENTO ,ID_USUARIO_LIQUIDACAO,TP_EXPORTACAO) 
		VALUES('P','" & txtCompetencia.Text & "','" & txtQuinzena.Text & "',GETDATE(),CONVERT(DATE,'" & txtLiquidacaoCCProcesso.Text & "',103),CONVERT(DATE,'" & txtLiquidacaoCCProcesso.Text & "',103)," & ddlContaBancaria.SelectedValue & ",7, " & Session("ID_USUARIO") & "," & Session("ID_USUARIO") & ", 'CNAC')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER")
            Dim ID_CONTAS_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")

            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_BL,ID_PARCEIRO_EMPRESA,DS_HISTORICO_LANCAMENTO,ID_BL_TAXA,ID_CONTA_PAGAR_RECEBER,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO ,VL_LIQUIDO,ID_MOEDA,VL_TAXA_CALCULADO,ID_ITEM_DESPESA)
                SELECT ID_BL,ID_PARCEIRO_INDICADOR,'COMISSÃO INDICADOR NACIONAL – " & txtCompetencia.Text & "-" & txtQuinzena.Text & "',ID_BL_TAXA," & ID_CONTAS_PAGAR_RECEBER & ",DT_CAMBIO,VL_CAMBIO,VL_COMISSAO, VL_COMISSAO,ID_MOEDA,VL_TAXA,(SELECT ID_ITEM_INDICADOR_NACIONAL FROM TB_PARAMETROS)ID_ITEM_INDICADOR_NACIONAL FROM TB_DETALHE_COMISSAO_NACIONAL  WHERE ID_CABECALHO_COMISSAO_NACIONAL IN (SELECT ID_CABECALHO_COMISSAO_NACIONAL FROM View_Comissao_Nacional WHERE COMPETENCIA = '" & txtCompetencia.Text & "' AND NR_QUINZENA = '" & txtQuinzena.Text & "') ")

            Con.ExecutarQuery("UPDATE TB_CABECALHO_COMISSAO_NACIONAL SET DT_EXPORTACAO = GETDATE(),ID_USUARIO_EXPORTACAO = " & Session("ID_USUARIO") & " WHERE DT_COMPETENCIA = '" & txtCompetencia.Text.Substring(0, 2) & txtCompetencia.Text.Substring(3, 4) & "' AND NR_QUINZENA = '" & txtQuinzena.Text & "'")

            divSuccess.Visible = True
            lblmsgSuccess.Text = "Comissão exportada para o processo com sucesso!"
            CarregaGrid()
            ModalPopupExtender6.Hide()
            ddlContaBancaria.SelectedValue = 0
        End If


    End Sub

    Private Sub ddlContaBancaria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContaBancaria.SelectedIndexChanged
        VerificaCCPRocesso()
    End Sub



    Sub VerificaCCPRocesso()
        Dim Con As New Conexao_sql
        Con.Conectar()

        'Verifica se a competencia já existe
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER WHERE TP_EXPORTACAO = 'CNAC' AND DT_COMPETENCIA = '" & txtNovaCompetencia.Text & "' AND NR_QUINZENA = '" & txtNovaQuinzena.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            divInfoCCProcesso.Visible = True
            lblInfoCCProcesso.Text = "COMPETENCIA JÁ EXPORTADA!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
            lblContasReceber.Text = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
        Else
            lblContasReceber.Text = 0
            divInfoCCProcesso.Visible = False
        End If
        ModalPopupExtender3.Show()
    End Sub

    Private Sub txtLiquidacaoFinal_TextChanged(sender As Object, e As EventArgs) Handles txtLiquidacaoFinal.TextChanged
        VerificaCompetencia()
        VerificaCCPRocesso()
        If divInfoCCProcesso.Visible = True Then
            divAtencaoGerarComissao.Visible = False
        End If
    End Sub

    Private Sub btnFecharCCProcesso_Click(sender As Object, e As EventArgs) Handles btnFecharCCProcesso.Click
        txtLiquidacaoCCProcesso.Text = ""
        ddlContaBancaria.SelectedValue = 0
        divInfoCCProcesso.Visible = False
        divErroCCProcesso.Visible = False
    End Sub

    'Private Function GetSortDirection(ByVal column As String) As String
    '    Dim sortDirection As String = "ASC"
    '    Dim sortExpression As String = TryCast(ViewState("SortExpression"), String)

    '    If sortExpression IsNot Nothing Then

    '        If sortExpression = column Then
    '            Dim lastDirection As String = TryCast(ViewState("SortDirection"), String)

    '            If (lastDirection IsNot Nothing) AndAlso (lastDirection = "ASC") Then
    '                sortDirection = "DESC"
    '            End If
    '        End If
    '    End If

    '    ViewState("SortDirection") = sortDirection
    '    ViewState("SortExpression") = column
    '    Return sortDirection
    'End Function
    'Private Sub dgvComissoes_Sorting(sender As Object, e As GridViewSortEventArgs) Handles dgvComissoes.Sorting
    '    Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)
    '    CarregaGrid()
    '    If dt IsNot Nothing Then
    '        dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
    '        Session("TaskTable") = dt
    '        dgvComissoes.DataSource = Session("TaskTable")
    '        CarregaGrid()
    '        dgvComissoes.HeaderRow.TableSection = TableRowSection.TableHeader
    '    End If
    'End Sub
End Class