Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization
Imports Boleto2Net
Imports Newtonsoft.Json
Imports System.Diagnostics
Imports System.Text
Public Class Faturamento
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2028 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
            ds = Con.ExecutarQuery("SELECT FL_ROTINA_IR_ATIVA,VL_ISENTO_IR_NF,VL_PERC_IR_NF FROM TB_PARAMETROS")
            txtCNPJFCA.Text = "00.639.367/0003-11"
            If Not Page.IsPostBack Then

                Session("FL_ROTINA_IR_ATIVA") = ds.Tables(0).Rows(0).Item("FL_ROTINA_IR_ATIVA")
                Session("VL_ISENTO_IR_NF") = ds.Tables(0).Rows(0).Item("VL_ISENTO_IR_NF")
                Session("VL_PERC_IR_NF") = ds.Tables(0).Rows(0).Item("VL_PERC_IR_NF")

                txtDataCheckFim.Text = Now.Date
                txtDataCheckInicial.Text = Now.Date.AddDays(-1)
                ' txtDataCheckInicial.Text = FinalSemanaSubtrai(txtDataCheckInicial.Text)
                Dim primeirodia As Date
                If Month(Now.Date) <= 9 Then
                    primeirodia = "01/0" & Month(Now.Date) & "/" & Year(Now.Date)
                Else
                    primeirodia = "01/" & Month(Now.Date) & "/" & Year(Now.Date)
                End If
                txtDataCheckInicial.Text = primeirodia
                AtualizaGrid()
                If Not Page.IsPostBack Then
                    ddlBanco.SelectedValue = "1"
                    'Session("filtros") = ""
                    'Session("RelFat") = ""
                End If
            End If
            txtData.Text = Now.Date.ToString("dd/MM/yyyy")
            lkNotasFiscais.Visible = True
            lkConsultaNotas.Visible = True
        End If
        Con.Fechar()
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divInfo.Visible = False
        AtualizaGrid()
    End Sub
    Sub AtualizaGrid()
        lkRelatorioFaturamento.Text = "Relatório de Faturamento"
        txtID.Text = ""
        txtlinha.Text = ""
        Session("RelFat") = ""
        Session("filtros") = ""
        Dim FiltroCheck As String = ""

        If txtDataCheckInicial.Text <> "" And txtDataCheckFim.Text = "" Then
            lblmsgErro.Text = "É necessário informar a data inicio e fim!"
            divErro.Visible = True
            Exit Sub
        ElseIf txtDataCheckFim.Text <> "" And txtDataCheckInicial.Text = "" Then
            lblmsgErro.Text = "É necessário informar a data inicio e fim!"
            divErro.Visible = True
            Exit Sub
        End If
        Session("filtros") &= "De " & txtDataCheckInicial.Text & " até " & txtDataCheckFim.Text

        If ckStatus.Items.FindByValue(1).Selected And ckStatus.Items.FindByValue(2).Selected Then
            'A Faturar e Faturados
            FiltroCheck &= " "
            Session("filtros") &= " A Faturar, Faturados"
        Else

            If ckStatus.Items.FindByValue(1).Selected Then
                'A Faturar
                ' FiltroCheck &= " AND DT_LIQUIDACAO IS NULL"
                FiltroCheck &= " AND NR_NOTA_DEBITO IS NULL"
                Session("filtros") &= ", A Faturar"
            End If

            If ckStatus.Items.FindByValue(2).Selected Then
                'Faturados
                ' FiltroCheck &= " AND DT_LIQUIDACAO IS NOT NULL"
                FiltroCheck &= " AND NR_NOTA_DEBITO IS NOT NULL"
                Session("filtros") &= ", Faturados"
            End If
        End If

        If ckStatus.Items.FindByValue(3).Selected And ckStatus.Items.FindByValue(4).Selected Then
            'À Vista e À Prazo
            FiltroCheck &= " AND ID_TIPO_FATURAMENTO IN (1,2)"
            Session("filtros") &= ", À Vista, À Prazo"
        Else

            If ckStatus.Items.FindByValue(3).Selected Then
                'À vista
                FiltroCheck &= " AND ID_TIPO_FATURAMENTO = 1"
                Session("filtros") &= ", À Vista"
            End If

            If ckStatus.Items.FindByValue(4).Selected Then
                'A Prazo          
                FiltroCheck &= " AND ID_TIPO_FATURAMENTO = 2"
                Session("filtros") &= ", À Prazo"
            End If


        End If




        If ckStatus.Items.FindByValue(5).Selected Then
            'Cancelados
            FiltroCheck &= " AND DT_CANCELAMENTO IS NOT NULL"
            Session("filtros") &= ", Cancelados"
        Else
            FiltroCheck &= " AND DT_CANCELAMENTO IS NULL"
        End If

        Dim FiltroDrop As String = ""

        If ddlFiltro.SelectedValue = 1 Then
            'Data Vencimento
            If txtDataInicioBusca.Text <> "" And txtDataFimBusca.Text = "" Then
                lblmsgErro.Text = "É necessário informar a data inicio e fim!"
                divErro.Visible = True
                Exit Sub
            ElseIf txtDataFimBusca.Text <> "" And txtDataInicioBusca.Text = "" Then
                lblmsgErro.Text = "É necessário informar a data inicio e fim!"
                divErro.Visible = True
                Exit Sub
            Else
                FiltroDrop &= " AND CONVERT(DATE,DT_VENCIMENTO,103) BETWEEN CONVERT(DATE,'" & txtDataInicioBusca.Text & "',103) AND CONVERT(DATE,'" & txtDataFimBusca.Text & "',103)"

                Session("filtros") &= "<br/> Vencimento de " & txtDataInicioBusca.Text & " até " & txtDataFimBusca.Text
            End If


        ElseIf ddlFiltro.SelectedValue = 2 Then
            'Número do processo
            FiltroDrop &= " AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 3 Then
            'Nome do Cliente
            FiltroDrop &= " AND PARCEIRO_EMPRESA LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 4 Then
            'Referência  do Cliente
            FiltroDrop &= " AND REFERENCIA_CLIENTE LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 5 Then
            'Nº Nota Débito
            FiltroDrop &= " AND NR_NOTA_DEBITO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 6 Then
            'Nº RPS
            FiltroDrop &= " AND NR_RPS LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 7 Then
            'Nº Nota Fiscal
            FiltroDrop &= " AND NR_NOTA_FISCAL LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 8 Then
            'Nº Recibo
            FiltroDrop &= " AND NR_RECIBO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 9 Then
            'Data Liquidação
            If txtDataInicioBusca.Text <> "" And txtDataFimBusca.Text = "" Then
                lblmsgErro.Text = "É necessário informar a data inicio e fim!"
                divErro.Visible = True
                Exit Sub
            ElseIf txtDataFimBusca.Text <> "" And txtDataInicioBusca.Text = "" Then
                lblmsgErro.Text = "É necessário informar a data inicio e fim!"
                divErro.Visible = True
                Exit Sub
            Else
                FiltroDrop &= " AND CONVERT(DATE,DT_LIQUIDACAO,103) BETWEEN CONVERT(DATE,'" & txtDataInicioBusca.Text & "',103) AND CONVERT(DATE,'" & txtDataFimBusca.Text & "',103)"
                Session("filtros") &= "<br/> Liquidação de " & txtDataInicioBusca.Text & " até " & txtDataFimBusca.Text
            End If

        End If


        dsFaturamento.SelectCommand = "SELECT ID_FATURAMENTO,DT_VENCIMENTO,NR_PROCESSO,NM_CLIENTE_REDUZIDO,REFERENCIA_CLIENTE,REFERENCIA_AUXILIAR,REFERENCIA_SHIPPER,VL_NOTA_DEBITO,NR_NOTA_DEBITO,DT_NOTA_DEBITO,NR_RPS,DT_RPS,
NR_RECIBO,DT_RECIBO,NR_NOTA_FISCAL,DT_NOTA_FISCAL,DT_NF_COMPETENCIA,DT_LIQUIDACAO,DT_CANCELAMENTO,NOSSONUMERO,ARQ_REM,NM_TIPO_FATURAMENTO,DT_ENVIO_FATURAMENTO,FL_FREE_HAND FROM [dbo].[View_Faturamento] WHERE CONVERT(DATE,DT_ENVIO_FATURAMENTO,103) BETWEEN CONVERT(DATE,'" & txtDataCheckInicial.Text & "',103) AND CONVERT(DATE,'" & txtDataCheckFim.Text & "',103) " & FiltroDrop & FiltroCheck
        dgvFaturamento.DataBind()

        Session("RelFat") = " WHERE CONVERT(DATE,DT_ENVIO_FATURAMENTO,103) BETWEEN CONVERT(DATE,'" & txtDataCheckInicial.Text & "',103) AND CONVERT(DATE,'" & txtDataCheckFim.Text & "',103) " & FiltroDrop & FiltroCheck
        lkFatura.Visible = True
        lkDesmosntrativos.Visible = True
        lkRPS.Visible = True
        lkNotasFiscais.Visible = True
    End Sub
    Private Sub ddlFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltro.SelectedIndexChanged
        If ddlFiltro.SelectedValue = 1 Or ddlFiltro.SelectedValue = 9 Then
            divBusca.Attributes.CssStyle.Add("display", "none")
            divDatasBusca.Attributes.CssStyle.Add("display", "block")
            'txtDataInicioBusca.Text = Now.Date.AddDays(-1)
            'txtDataInicioBusca.Text = FinalSemana(txtDataInicioBusca.Text)
            Dim primeirodia As Date
            If Month(Now.Date) <= 9 Then
                primeirodia = "01/0" & Month(Now.Date) & "/" & Year(Now.Date)
            Else
                primeirodia = "01/" & Month(Now.Date) & "/" & Year(Now.Date)
            End If
            txtDataInicioBusca.Text = primeirodia
            txtDataFimBusca.Text = Now.Date
        Else
            divBusca.Attributes.CssStyle.Add("display", "block")
            divDatasBusca.Attributes.CssStyle.Add("display", "none")
            txtPesquisa.Text = ""
        End If
    End Sub

    Private Sub btnBaixarFatura_Click(sender As Object, e As EventArgs) Handles btnBaixarFatura.Click
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            If txtData.Text = "" Then
                lblmsgErro.Text = "É necessário informar a data para efetuar a baixa!"
                divErro.Visible = True
                Exit Sub
            Else
                Dim NumeracaoDoc As New NumeracaoDoc
                Dim numero As String = NumeracaoDoc.Numerar(2)

                Dim ds As DataSet = Con.ExecutarQuery("Select A.DT_CANCELAMENTO,(Select DT_LIQUIDACAO FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER =  A.ID_CONTA_PAGAR_RECEBER)DT_LIQUIDACAO,(Select ISNULL(ID_TIPO_FATURAMENTO,0) FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER =  A.ID_CONTA_PAGAR_RECEBER)ID_TIPO_FATURAMENTO FROM TB_FATURAMENTO A WHERE A.ID_FATURAMENTO = " & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura cancelada!"
                        divErro.Visible = True
                        Exit Sub

                    ElseIf Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) Then
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura já liquidada!"
                        divErro.Visible = True
                        Exit Sub

                    ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") <> 1 Then
                        lblmsgErro.Text = "Não foi possivel completar a ação: O tipo de faturamento do registro nao permite baixas pelo modulo atual!"
                        divErro.Visible = True
                        Exit Sub


                    Else
                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_RECIBO = getdate(), NR_RECIBO = '" & numero & "' WHERE ID_FATURAMENTO =" & txtID.Text)
                        Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RECIBO = '" & numero & "' WHERE ID_NUMERACAO = 5")

                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtData.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER = (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO =" & txtID.Text & ")")
                        Con.Fechar()
                        lblmsgSuccess.Text = "Baixa realizada com sucesso!"
                        divSuccess.Visible = True
                        txtData.Text = ""
                        txtObs.Text = ""
                        dgvFaturamento.DataBind()
                    End If
                End If


            End If
        End If

    End Sub

    Private Sub btnSalvarCancelamento_Click(sender As Object, e As EventArgs) Handles btnSalvarCancelamento.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divInfo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            If txtObs.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "É necessario preencher o campo de observações!"
            Else
                Dim ds As DataSet = Con.ExecutarQuery("SELECT B.DT_LIQUIDACAO,A.NR_NOTA_FISCAL,B.DT_CANCELAMENTO,A.DT_CANCELAMENTO DT_CANCELAMENTO_FATURA  FROM [TB_FATURAMENTO] A
LEFT JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER
WHERE ID_FATURAMENTO =" & txtID.Text)
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) And Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                    'ds.Tables(0).Rows.Count > 0
                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura já liquidada!"
                Else

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO_FATURA")) Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura já cancelada!"
                        Exit Sub

                    ElseIf Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then

                        divErro.Visible = True
                        lblmsgErro.Text = "Não foi possivel completar a ação: A NOTA FISCAL JÁ FOI GERADA!"
                        Exit Sub

                    Else
                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "' WHERE ID_FATURAMENTO =" & txtID.Text)

                        Con.ExecutarQuery("UPDATE TB_CONTA_PAGAR_RECEBER SET DT_ENVIO_FATURAMENTO = NULL WHERE ID_CONTA_PAGAR_RECEBER  IN  (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO  WHERE ID_FATURAMENTO = " & txtID.Text & " )")

                        Con.Fechar()
                        lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"
                        divSuccess.Visible = True
                        txtData.Text = ""
                        txtObs.Text = ""
                        dgvFaturamento.DataBind()
                        divInfo.Visible = False
                    End If

                End If
            End If
        End If

    End Sub
    Private Sub lkReciboPagamento_Click(sender As Object, e As EventArgs) Handles lkReciboPagamento.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT B.DT_LIQUIDACAO,A.NR_RECIBO FROM [TB_FATURAMENTO] A
LEFT JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER
WHERE DT_LIQUIDACAO IS NOT NULL AND ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then

                If IsDBNull(ds.Tables(0).Rows(0).Item("NR_RECIBO")) Then
                    Dim NumeracaoDoc As New NumeracaoDoc
                    Dim numero As String = NumeracaoDoc.Numerar(2)

                    Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_RECIBO = getdate(), NR_RECIBO = '" & numero & "' WHERE ID_FATURAMENTO =" & txtID.Text)
                    Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RECIBO = '" & numero & "'")


                End If

                mpeDemonstrativos.Show()
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "FuncRecibo()", True)

            Else
                divErro.Visible = True
                lblmsgErro.Text = "Nota sem liquidação!"
            End If
            Con.Fechar()
        End If


    End Sub

    Private Sub lkReciboServico_Click(sender As Object, e As EventArgs) Handles lkReciboServico.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT OB_RPS FROM [TB_FATURAMENTO] WHERE DT_RPS IS NOT NULL AND ID_FATURAMENTO = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                txtAuxObsRps.Text = "RECIBO"
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_RPS")) Then
                    txtOBSRPS.Text = ds.Tables(0).Rows(0).Item("OB_RPS")
                End If
                mpeRPS.Hide()
                mpeObsRps.Show()

            Else
                divErro.Visible = True
                lblmsgErro.Text = "Nota sem RPS!"
            End If
        End If
    End Sub


    Private Sub lkGerarRPS_Click(sender As Object, e As EventArgs) Handles lkGerarRPS.Click
        divSuccess.Visible = False
        divErro.Visible = False

        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT OB_RPS FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                txtAuxObsRps.Text = "GERAR"
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_RPS")) Then
                    txtOBSRPS.Text = ds.Tables(0).Rows(0).Item("OB_RPS")
                End If
                mpeRPS.Hide()
                mpeObsRps.Show()

            Else
                mpeRPS.Hide()
                divErro.Visible = True
                lblmsgErro.Text = "Fatura não encontada!!"
            End If

        End If
    End Sub


    Private Sub lkReenviarRPS_Click(sender As Object, e As EventArgs) Handles lkReenviarRPS.Click
        divSuccess.Visible = False
        divErro.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_DEBITO,NR_RPS,DT_RPS FROM [TB_FATURAMENTO]
WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_DEBITO")) Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_RPS")) Then


                        Dim numero As String = ds.Tables(0).Rows(0).Item("NR_RPS")

                        Using GeraRps = New WsNVOCC.WsNvocc

                            Dim consulta = GeraRps.IntegraNFePrefeitura(numero, 1, "SQL", "NVOCC", 1)

                        End Using

                        Using GeraRps = New WsNVOCC.WsNvocc

                            Dim consulta = GeraRps.ConsultaNFePrefeitura(txtID.Text, 1, "SQL", "NVOCC")

                        End Using

                        ds = Con.ExecutarQuery("SELECT isnull(STATUS_NFE,0)STATUS_NFE FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If ds.Tables(0).Rows(0).Item("STATUS_NFE") = 2 Then
                                divSuccess.Visible = True
                                lblmsgSuccess.Text = "RPS reenviada com sucesso!"
                                dsFaturamento.SelectCommand = "Select * FROM [dbo].[View_Faturamento] where NR_RPS = '" & numero & "'"
                                dgvFaturamento.DataBind()
                            Else
                                Dim ds2 As DataSet = Con.ExecutarQuery("SELECT isnull(STATUS_NFE,0)STATUS_NFE FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
                                If ds2.Tables(0).Rows.Count > 0 Then
                                    If ds2.Tables(0).Rows(0).Item("STATUS_NFE") <> 2 Then

                                        Dim ERRO As String = ""
                                        Dim DS1 As DataSet = Con.ExecutarQuery("SELECT CRITICA FROM TB_LOG_NFSE WHERE ID_LOG = (SELECT MAX(ID_LOG)ID_LOG FROM TB_LOG_NFSE WHERE CRITICA IS NOT NULL AND ID_FATURAMENTO = " & txtID.Text & " )")
                                        If DS1.Tables(0).Rows.Count > 0 Then
                                            If Not IsDBNull(DS1.Tables(0).Rows(0).Item("CRITICA")) Then
                                                ERRO = DS1.Tables(0).Rows(0).Item("CRITICA")
                                            End If
                                        End If
                                        lblmsgErro.Text = "Não foi possivel completar a ação: " & ERRO
                                        divErro.Visible = True
                                        Exit Sub

                                    Else
                                        lblmsgErro.Text = "Não foi possivel completar a ação!"
                                        divErro.Visible = True
                                        Exit Sub
                                    End If

                                End If

                            End If

                        End If


                    End If


                Else
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota de débito!"
                    divErro.Visible = True
                End If

            End If

        End If
    End Sub


    Private Sub lkCancelarNota_Click(sender As Object, e As EventArgs) Handles lkCancelarNota.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_FISCAL,NR_RPS FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                    If PrazoCancelamento() = False Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Prazo para cancelamento expirado!"
                        Exit Sub

                    Else



                        Try

                            Dim RPSCancelada As String = ds.Tables(0).Rows(0).Item("NR_RPS")

                            Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_CANCELAMENTO = getdate(), ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = 'CANCELAMENTO DA NOTA FISCAL' , STATUS_NFE = 3 WHERE ID_FATURAMENTO =" & txtID.Text)



                            Using GeraRps = New WsNVOCC.WsNvocc

                                Dim consulta = GeraRps.CancelaNFePrefeitura(RPSCancelada, 1, "SQL", "NVOCC")

                            End Using




                            ds = Con.ExecutarQuery("SELECT isnull(STATUS_NFE,0)STATUS_NFE FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
                            If ds.Tables(0).Rows.Count > 0 Then
                                If ds.Tables(0).Rows(0).Item("STATUS_NFE") = 3 Then
                                    divSuccess.Visible = True
                                    lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"
                                    dgvFaturamento.DataBind()
                                Else

                                    lblmsgErro.Text = "Não foi possivel completar a ação!"
                                    divErro.Visible = True
                                End If

                            End If
                        Catch ex As Exception

                            divErro.Visible = True
                            lblmsgErro.Text = "Não foi possivel completar a ação: " & ex.Message

                        End Try


                    End If
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
                End If

            Else
                divErro.Visible = True
                lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
            End If
            Con.Fechar()
        End If

    End Sub

    Private Sub btnSubstituir_Click(sender As Object, e As EventArgs) Handles btnSubstituir.Click
        divErroSubstituir.Visible = False
        divSuccess.Visible = False
        Dim validar As New VerificaData

        If txtID.Text = "" Then
            divErroSubstituir.Visible = True
            lblErroSubstituir.Text = "Selecione um registro"

        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_FISCAL,NR_RPS FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                    'If PrazosSubstituicao() = False Then
                    '    divErro.Visible = True
                    '    lblmsgErro.Text = "Nota selecionada não pode ser substituída!"
                    '    ModalPopupExtender5.Hide()
                    '    Exit Sub

                    'Else
                    Try

                        Dim RPSSubstituida As String = ds.Tables(0).Rows(0).Item("NR_RPS")

                        Dim NumeracaoDoc As New NumeracaoDoc
                        Dim RPSNova As String = NumeracaoDoc.Numerar(3)


                        Dim ConOracle As New Conexao_oracle
                        ConOracle.Conectar()
                        Dim dt As DataTable = ConOracle.Consultar("select SERIE from Sgipa.TB_SERIE_RPS WHERE FLAG_ATIVO = 1 ")
                        Dim SERIE_RPS As String = ""

                        If dt.Rows.Count > 0 Then
                            SERIE_RPS = dt.Rows(0)("SERIE").ToString
                        End If

                        Dim dsInsert As DataSet = Con.ExecutarQuery("INSERT INTO TB_FATURAMENTO (ID_CONTA_PAGAR_RECEBER,NR_NOTA_DEBITO,NR_RPS,NR_RECIBO,DT_NOTA_DEBITO,DT_RPS,DT_RECIBO,FL_RPS,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,SERIE_RPS,ID_PARCEIRO_CLIENTE,COMPETENCIA,DT_COMPETENCIA) SELECT ID_CONTA_PAGAR_RECEBER,NR_NOTA_DEBITO,'" & RPSNova & "',NR_RECIBO,DT_NOTA_DEBITO,Getdate(),DT_RECIBO,FL_RPS,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,'" & SERIE_RPS & "',ID_PARCEIRO_CLIENTE,COMPETENCIA,DT_COMPETENCIA FROM TB_FATURAMENTO WHERE ID_FATURAMENTO =" & txtID.Text & "  SELECT SCOPE_IDENTITY() as ID_FATURAMENTO")
                        Dim NOVO_FATURAMENTO As String = dsInsert.Tables(0).Rows(0).Item("ID_FATURAMENTO")

                        Con.ExecutarQuery("update F 
                            set 
                            F.NM_CLIENTE = '" & txtRazaoSocial.Text & "',
                            F.COMPL_ENDERECO = '" & txtComplem.Text & "',
                            F.ENDERECO = '" & txtEndereco.Text & "',
                            F.BAIRRO ='" & txtBairro.Text & "',
                            F.NR_ENDERECO = '" & txtNumEndereco.Text & "',
                            F.CEP = '" & txtCEP.Text & "',
                            F.CNPJ = '" & txtCNPJSub.Text & "',
                            F.INSCR_ESTADUAL = '" & txtInscEstadual.Text & "',
                            F.INSCR_MUNICIPAL = '" & txtInscMunicipal.Text & "',
                            F.CIDADE = '" & txtCidade.Text & "',
                            F.ESTADO = '" & txtEstado.Text & "',
                            F.ID_PARCEIRO_CLIENTE = " & txtIDParceiro.Text & " 
                            FROM TB_FATURAMENTO F
                            WHERE ID_FATURAMENTO = " & NOVO_FATURAMENTO)

                        'ROTINA COMENTADA CHAMADO 5182 - HAMILTON
                        'Con.ExecutarQuery("update p 
                        '    set 
                        '    P.NM_RAZAO = '" & txtRazaoSocial.Text & "',
                        '    P.COMPL_ENDERECO = '" & txtComplem.Text & "',
                        '    P.ENDERECO = '" & txtEndereco.Text & "',
                        '    P.BAIRRO ='" & txtBairro.Text & "',
                        '    P.NR_ENDERECO = '" & txtNumEndereco.Text & "',
                        '    P.CEP = '" & txtCEP.Text & "',
                        '    P.CNPJ = '" & txtCNPJSub.Text & "',
                        '    P.INSCR_ESTADUAL = '" & txtInscEstadual.Text & "',
                        '    P.INSCR_MUNICIPAL = '" & txtInscMunicipal.Text & "',
                        '    P.ID_CIDADE = (SELECT ID_CIDADE FROM TB_CIDADE WHERE NM_CIDADE = '" & txtCidade.Text & "')
                        '    FROM TB_FATURAMENTO F
                        '    INNER JOIN TB_PARCEIRO P on P.ID_PARCEIRO = F.ID_PARCEIRO_CLIENTE
                        '   AND F.ID_FATURAMENTO = " & NOVO_FATURAMENTO)


                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_CANCELAMENTO = getdate(), ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = 'SUBSTITUIÇÃO DA NOTA FISCAL' , STATUS_NFE = 3 WHERE ID_FATURAMENTO =" & txtID.Text)

                        Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RPS = '" & RPSNova & "' WHERE ID_NUMERACAO = 5")


                        Using GeraRps = New WsNVOCC.WsNvocc

                            Dim consulta = GeraRps.SubstituiNFePrefeitura(RPSSubstituida, RPSNova, 1, "SQL", "NVOCC", NOVO_FATURAMENTO)

                        End Using

                        Con.ExecutarQuery("UPDATE TB_FATURAMENTO SET DT_COMPETENCIA = (SELECT DT_COMPETENCIA FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & txtID.Text & " ) WHERE ID_FATURAMENTO = " & NOVO_FATURAMENTO)

                        txtID.Text = ""
                        lblmsgSuccess.Text = "RPS enviada para substituição com sucesso!"
                        divSuccess.Visible = True
                        divErroSubstituir.Visible = False
                        dgvFaturamento.DataBind()
                        ModalPopupExtender5.Hide()

                    Catch ex As Exception

                        divErro.Visible = True
                        lblmsgErro.Text = "Não foi possivel completar a ação: " & ex.Message

                    End Try

                    ' End If

                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
                    ModalPopupExtender5.Hide()

                End If

            Else
                divErro.Visible = True
                lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
                ModalPopupExtender5.Hide()

            End If
            Con.Fechar()
        End If


    End Sub

    Private Sub lkSubstituirNota_Click(sender As Object, e As EventArgs) Handles lkSubstituirNota.Click
        divErroSubstituir.Visible = False
        divSuccess.Visible = False
        divErro.Visible = False

        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"

        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO_CLIENTE, NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP, DT_NOTA_FISCAL,COD_VER_NFSE FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                txtIDParceiro.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE").ToString
                txtRazaoSocial.Text = ds.Tables(0).Rows(0).Item("NM_CLIENTE").ToString
                txtCNPJSub.Text = ds.Tables(0).Rows(0).Item("CNPJ").ToString
                txtInscEstadual.Text = ds.Tables(0).Rows(0).Item("INSCR_ESTADUAL").ToString
                txtInscMunicipal.Text = ds.Tables(0).Rows(0).Item("INSCR_MUNICIPAL").ToString
                txtEndereco.Text = ds.Tables(0).Rows(0).Item("ENDERECO").ToString
                txtNumEndereco.Text = ds.Tables(0).Rows(0).Item("NR_ENDERECO").ToString
                txtBairro.Text = ds.Tables(0).Rows(0).Item("BAIRRO").ToString
                txtComplem.Text = ds.Tables(0).Rows(0).Item("COMPL_ENDERECO").ToString
                txtCidade.Text = ds.Tables(0).Rows(0).Item("CIDADE").ToString
                txtEstado.Text = ds.Tables(0).Rows(0).Item("ESTADO").ToString
                txtCEP.Text = ds.Tables(0).Rows(0).Item("CEP").ToString
                txtDataEmissao.Text = ds.Tables(0).Rows(0).Item("DT_NOTA_FISCAL").ToString
                txtCodVerificacao.Text = ds.Tables(0).Rows(0).Item("COD_VER_NFSE").ToString
                ModalPopupExtender5.Show()
            Else
                divErro.Visible = True
                lblmsgErro.Text = "Não foi possivel completar a ação: fatura não encontrada!"
            End If
            Con.Fechar()
        End If

    End Sub

    Private Sub lkVisualizarNota_Click(sender As Object, e As EventArgs) Handles lkVisualizarNota.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divInfo.Visible = False
        If Not String.IsNullOrEmpty(txtID.Text) Then
            If txtCOD_VER_NFSE.Text <> "" And txtNR_NOTA.Text <> "" Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ImprimirNota()", True)
            Else


                Try
                    Using GeraRps = New WsNVOCC.WsNvocc

                        Dim consulta = GeraRps.ConsultaNFePrefeitura(txtID.Text, 1, "SQL", "NVOCC")

                    End Using

                    Dim Con As New Conexao_sql
                    Con.Conectar()
                    Dim ds2 As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_FISCAL,COD_VER_NFSE FROM TB_FATURAMENTO WHERE ID_FATURAMENTO =" & txtID.Text)

                    If ds2.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(ds2.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                            txtNR_NOTA.Text = ds2.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")
                        End If
                        If Not IsDBNull(ds2.Tables(0).Rows(0).Item("COD_VER_NFSE")) Then
                            txtCOD_VER_NFSE.Text = ds2.Tables(0).Rows(0).Item("COD_VER_NFSE")
                        End If
                    End If

                    If txtCOD_VER_NFSE.Text <> "" And txtNR_NOTA.Text <> "" Then
                        divErro.Visible = False
                        AtualizaGrid()
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ImprimirNota()", True)
                    Else


                        ds2 = Con.ExecutarQuery("SELECT isnull(STATUS_NFE,0)STATUS_NFE FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
                        If ds2.Tables(0).Rows.Count > 0 Then
                            If ds2.Tables(0).Rows(0).Item("STATUS_NFE") <> 2 Then

                                Dim ERRO As String = ""
                                Dim DS1 As DataSet = Con.ExecutarQuery("SELECT CRITICA FROM TB_LOG_NFSE WHERE ID_LOG = (SELECT MAX(ID_LOG)ID_LOG FROM TB_LOG_NFSE WHERE CRITICA IS NOT NULL AND ID_FATURAMENTO = " & txtID.Text & " )")
                                If DS1.Tables(0).Rows.Count > 0 Then
                                    If Not IsDBNull(DS1.Tables(0).Rows(0).Item("CRITICA")) Then
                                        ERRO = DS1.Tables(0).Rows(0).Item("CRITICA")
                                    End If
                                End If
                                lblmsgErro.Text = "Não foi possivel completar a ação: " & ERRO
                                divErro.Visible = True
                                Exit Sub

                            Else
                                lblmsgErro.Text = "Não foi possivel completar a ação!"
                                divErro.Visible = True
                                Exit Sub
                            End If

                        End If




                    End If
                    Con.Fechar()
                Catch ex As Exception

                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel completar a ação: " & ex.Message

                End Try



            End If
        Else
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro!"
        End If

    End Sub

    Protected Sub dgvFaturamento_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvFaturamento.DataSource = Session("TaskTable")
            dgvFaturamento.DataBind()
            dgvFaturamento.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
        AtualizaGrid()
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

    Private Sub dgvFaturamento_Load(sender As Object, e As EventArgs) Handles dgvFaturamento.Load
        Dim Con As New Conexao_sql
        Dim contador As Integer = 0
        Dim IDs As String = ""
        txtID.Text = ""
        For Each linha As GridViewRow In dgvFaturamento.Rows
            Dim check As CheckBox = linha.FindControl("ckSelecionar")
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            If check.Checked Then
                contador = contador + 1
                If IDs = "" Then
                    IDs &= ID
                Else
                    IDs &= "," & ID

                End If
            End If
        Next
        Session("IDs") = IDs
        If contador = 1 Then
            lkFatura.Visible = True
            lkNotaDebito.Visible = True
            lkReciboServico.Visible = True
            lkReciboPagamento.Visible = True
            lkRPS.Visible = True
            lkNotasFiscais.Visible = True
            lkBoleto.Visible = True

            txtID.Text = IDs
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER,NR_PROCESSO,PARCEIRO_EMPRESA,CONVERT(VARCHAR,DT_NOTA_FISCAL,103)DT_NOTA_FISCAL,NR_NOTA_FISCAL,VL_NOTA_DEBITO,OB_RPS,STATUS_NFE,COD_VER_NFSE,ID_PARCEIRO_CLIENTE,ID_SERVICO,NR_RPS  FROM View_Faturamento WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                    lblProcessoCancelamento.Text = "PROCESSO: " & ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    lblProcessoBaixa.Text = "PROCESSO: " & ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    lblProcessoSubs.Text = "PROCESSO: " & ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    Session("ProcessoFaturamento") = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PARCEIRO_EMPRESA")) Then
                    lblClienteCancelamento.Text = "CLIENTE: " & ds.Tables(0).Rows(0).Item("PARCEIRO_EMPRESA")
                    lblClienteBaixa.Text = "CLIENTE: " & ds.Tables(0).Rows(0).Item("PARCEIRO_EMPRESA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")) Then
                    txtID_CLIENTE.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                    If ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL") <> "PENDENTE" And ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL") <> "SEM RECEITA" Then
                        txtNR_NOTA.Text = ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")
                    End If
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                    txtID_SERVICO.Text = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("COD_VER_NFSE")) Then
                    txtCOD_VER_NFSE.Text = ds.Tables(0).Rows(0).Item("COD_VER_NFSE")
                End If


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")) Then
                    Session("ID_CONTA_PAGAR_RECEBER") = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
                End If

                'If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_RPS")) Then
                '    txtOBSRPS.Text = ds.Tables(0).Rows(0).Item("OB_RPS")
                'End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("STATUS_NFE")) Then
                    If ds.Tables(0).Rows(0).Item("STATUS_NFE") = 1 Or ds.Tables(0).Rows(0).Item("STATUS_NFE") = 4 Or ds.Tables(0).Rows(0).Item("STATUS_NFE") = 5 Then
                        lkReenviarRPS.Visible = True
                        lkGerarRPS.Visible = False
                    Else
                        lkReenviarRPS.Visible = False
                        lkGerarRPS.Visible = True
                    End If
                Else
                    lkReenviarRPS.Visible = False
                    lkGerarRPS.Visible = True
                End If

            End If
            Con.Fechar()
        ElseIf contador > 1 Then
            lkFatura.Visible = False
            lkRPS.Visible = False
            lkNotasFiscais.Visible = False
            lkReenviarRPS.Visible = False
            lkBoleto.Visible = False
            lkNotaDebito.Visible = False
            lkReciboServico.Visible = False
            lkReciboPagamento.Visible = False
        End If


    End Sub

    Private Sub btnImprimirBoleto_Click(sender As Object, e As EventArgs) Handles btnImprimirBoleto.Click
        divErro.Visible = False
        divSuccess.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim IDs As String = 0

        If txtID.Text <> "" Then
            IDs = txtID.Text
        End If

        If IDs = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro!"

        ElseIf ddlBanco.SelectedValue = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "É necessario selecionar um banco!"
        Else
            Dim objBoletos As New Boletos
            Dim strImpressora As String = Nothing
            Dim blnImprimir As Boolean = False
            Dim intCopias As Short = 1

            Try
                Dim NR_PROCESSO As String = ""
                Dim NossoNumero As String = ""
                Dim COD_BANCO As String = ""
                Dim VL_MULTA As String = ""
                Dim VL_MORA As String = ""
                Dim OBS1 As String = ""
                Dim OBS2 As String = ""
                ' Dim SEQ_ARQUIVO As String = ""
                Dim SEQUENCIA As String = ""
                Dim ds As DataSet = Con.ExecutarQuery("Select NM_CEDENTE,NR_BANCO As COD_BANCO,convert(int,NR_BANCO)NR_BANCO,CNPJ_CPF_CEDENTE,NR_AGENCIA,DG_AGENCIA,NR_CONTA,DG_CONTA,ENDERECO_CEDENTE,CARTEIRA,CD_CEDENTE,CD_TRASMISSAO,NUMERO_END_CEDENTE, BAIRRO_END_CEDENTE, UF_END_CEDENTE, CEP_END_CEDENTE, CIDADE_END_CEDENTE, COMP_END_CEDENTE,OBS1,OBS2,SEQUENCIA, VL_MULTA,VL_MORA, (Select NR_NOTA_DEBITO FROM TB_FATURAMENTO A WHERE ID_FATURAMENTO In (" & IDs & "))NR_NOTA_DEBITO FROM TB_CONTA_BANCARIA WHERE ID_CONTA_BANCARIA = " & ddlBanco.SelectedValue)
                If ds.Tables(0).Rows.Count > 0 Then

                    If IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_DEBITO")) Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Fatura sem nota de débito!"
                        Exit Sub
                    End If



                    'Salvando informaçoes importantes
                    COD_BANCO = ds.Tables(0).Rows(0).Item("COD_BANCO")
                    VL_MULTA = ds.Tables(0).Rows(0).Item("VL_MULTA")
                    VL_MORA = ds.Tables(0).Rows(0).Item("VL_MORA")

                    OBS1 = ds.Tables(0).Rows(0).Item("OBS1")
                    OBS2 = ds.Tables(0).Rows(0).Item("OBS2")
                    ' SEQ_ARQUIVO = ds.Tables(0).Rows(0).Item("SEQ_ARQUIVO")
                    SEQUENCIA = ds.Tables(0).Rows(0).Item("SEQUENCIA")

                    ''CRIAÇÃO DA PARTE DO CEDENTE
                    'Cabeçalho do Banco
                    objBoletos.Banco = Banco.Instancia(ds.Tables(0).Rows(0).Item("NR_BANCO"))
                    objBoletos.Banco.Cedente = New Cedente
                    objBoletos.Banco.Cedente.CPFCNPJ = ds.Tables(0).Rows(0).Item("CNPJ_CPF_CEDENTE")
                    objBoletos.Banco.Cedente.Nome = ds.Tables(0).Rows(0).Item("NM_CEDENTE")
                    objBoletos.Banco.Cedente.Observacoes = ""

                    Dim conta As New ContaBancaria
                    conta.Agencia = ds.Tables(0).Rows(0).Item("NR_AGENCIA")
                    conta.DigitoAgencia = ds.Tables(0).Rows(0).Item("DG_AGENCIA")
                    conta.OperacaoConta = String.Empty
                    conta.Conta = ds.Tables(0).Rows(0).Item("NR_CONTA")
                    conta.DigitoConta = ds.Tables(0).Rows(0).Item("DG_CONTA")
                    conta.CarteiraPadrao = ds.Tables(0).Rows(0).Item("CARTEIRA")

                    conta.VariacaoCarteiraPadrao = ""
                    conta.TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples
                    conta.TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro
                    conta.TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
                    conta.TipoDocumento = TipoDocumento.Tradicional

                    Dim ender As New Endereco
                    ender.LogradouroEndereco = ds.Tables(0).Rows(0).Item("ENDERECO_CEDENTE")
                    ender.LogradouroNumero = ds.Tables(0).Rows(0).Item("NUMERO_END_CEDENTE")
                    ender.LogradouroComplemento = ds.Tables(0).Rows(0).Item("COMP_END_CEDENTE")
                    ender.Bairro = ds.Tables(0).Rows(0).Item("BAIRRO_END_CEDENTE")
                    ender.Cidade = ds.Tables(0).Rows(0).Item("CIDADE_END_CEDENTE")
                    ender.UF = ds.Tables(0).Rows(0).Item("UF_END_CEDENTE")
                    ender.CEP = ds.Tables(0).Rows(0).Item("CEP_END_CEDENTE")

                    objBoletos.Banco.Cedente.Codigo = ds.Tables(0).Rows(0).Item("CD_CEDENTE")
                    objBoletos.Banco.Cedente.CodigoDV = "6"
                    objBoletos.Banco.Cedente.CodigoTransmissao = ds.Tables(0).Rows(0).Item("CD_TRASMISSAO")
                    objBoletos.Banco.Cedente.ContaBancaria = conta
                    objBoletos.Banco.Cedente.Endereco = ender

                    objBoletos.Banco.FormataCedente()
                End If

                Dim i As Integer = 0

                ds = Con.ExecutarQuery("SELECT ID_FATURAMENTO,
CNPJ,
NM_CLIENTE,
ENDERECO,
NR_ENDERECO,
COMPL_ENDERECO,
CEP,
CIDADE,
BAIRRO ,
NR_NOTA_FISCAL,
NOSSONUMERO, 
(SELECT case when B.DT_VENCIMENTO < = getdate() then CONVERT(VARCHAR,getdate()+1,103)  else CONVERT(VARCHAR,B.DT_VENCIMENTO,103)end FROM TB_CONTA_PAGAR_RECEBER B WHERE B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER)DT_VENCIMENTO,
'ND ' + NR_NOTA_DEBITO AS NR_NOTA_DEBITO,
(SELECT REPLACE(REPLACE(NR_PROCESSO,'/',''),'-','') from View_Faturamento where ID_FATURAMENTO = A.ID_FATURAMENTO ) NR_DOCUMENTO,
(SELECT NR_PROCESSO from View_Faturamento where ID_FATURAMENTO = A.ID_FATURAMENTO )NR_PROCESSO, 
CASE WHEN ISNULL(A.VL_IR_NF,0)> 0 THEN 1 ELSE 0 END FL_IR, 
CASE WHEN A.NR_NOTA_FISCAL IS NOT NULL THEN 1 ELSE 0 END FL_NF,
(SELECT SUM(ISNULL(B.VL_LIQUIDO,0)) FROM TB_CONTA_PAGAR_RECEBER_ITENS B WHERE B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER) VL_FATURA,
ISNULL(VL_IR_NF,0)VL_IR_NF,
ISNULL(VL_ISS,0)VL_ISS 
FROM TB_FATURAMENTO A
WHERE ID_FATURAMENTO =" & IDs & " 
GROUP BY ID_FATURAMENTO,ID_CONTA_PAGAR_RECEBER,CNPJ,NM_CLIENTE,ENDERECO,BAIRRO,NR_ENDERECO,COMPL_ENDERECO,CEP,CIDADE,NR_NOTA_FISCAL,NOSSONUMERO,NR_NOTA_DEBITO,VL_IR_NF,VL_ISS")

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim VL_BOLETO As String = 0
                    NR_PROCESSO = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    For Each linhads As DataRow In ds.Tables(0).Rows
                        i = i + 1
                        ''CRIAÇÃO DO TITULO
                        Dim GeraRemessa As New GeraRemessa
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("NOSSONUMERO")) Then
                            NossoNumero = ds.Tables(0).Rows(0).Item("NOSSONUMERO")
                        Else
                            NossoNumero = GeraRemessa.obtemProximoNossoNum(SEQUENCIA)
                        End If


                        Dim Titulo As New Boleto(objBoletos.Banco)
                        Titulo.Sacado = New Sacado With {
                    .CPFCNPJ = linhads.Item("CNPJ").ToString(),
                    .Endereco = New Boleto2Net.Endereco With {
                    .Bairro = linhads.Item("BAIRRO").ToString(),
                    .CEP = linhads.Item("CEP").ToString(),
                    .Cidade = linhads.Item("CIDADE").ToString(),
                    .LogradouroComplemento = linhads.Item("COMPL_ENDERECO").ToString(),
                    .LogradouroEndereco = linhads.Item("ENDERECO").ToString(),
                    .LogradouroNumero = linhads.Item("NR_ENDERECO").ToString(),
                    .UF = "SP"},
                    .Nome = linhads.Item("NM_CLIENTE").ToString(),
                    .Observacoes = ""
                }
                        Titulo.CodigoOcorrencia = "01"
                        Titulo.DescricaoOcorrencia = "Remessa Registrar"
                        Titulo.NumeroDocumento = linhads.Item("NR_DOCUMENTO").ToString()
                        Titulo.NumeroControleParticipante = "12"
                        Titulo.NossoNumero = NossoNumero
                        Titulo.DataEmissao = Now.Date
                        Titulo.DataVencimento = linhads.Item("DT_VENCIMENTO")


                        ''VALOR DO BOLETO
                        VL_BOLETO = linhads.Item("VL_FATURA")

                        If linhads.Item("CIDADE").ToString() = "SANTOS" And linhads.Item("FL_NF").ToString = 1 Then
                            VL_BOLETO = VL_BOLETO - linhads.Item("VL_ISS")
                        End If

                        If linhads.Item("FL_IR").ToString = 1 Then
                            VL_BOLETO = VL_BOLETO - linhads.Item("VL_IR_NF")
                        End If

                        Titulo.ValorTitulo = VL_BOLETO
                        VL_BOLETO = VL_BOLETO.ToString.Replace(".", "")
                        VL_BOLETO = VL_BOLETO.Replace(",", ".")


                        Titulo.Aceite = "N"
                        Titulo.EspecieDocumento = TipoEspecieDocumento.DS
                        Titulo.DataDesconto = Now.Date.AddDays(15)
                        Titulo.ValorDesconto = 0


                        'PARTE DA MULTA
                        Titulo.DataMulta = Now.Date.AddDays(15)
                        Titulo.PercentualMulta = VL_MULTA
                        Titulo.ValorMulta = Titulo.ValorTitulo * Titulo.PercentualMulta / 100
                        Titulo.MensagemInstrucoesCaixa = OBS1


                        'PARTE JUROS DE MORA
                        Titulo.DataJuros = Now.Date.AddDays(15)
                        Titulo.PercentualJurosDia = VL_MORA
                        Titulo.ValorJurosDia = Titulo.ValorTitulo * Titulo.PercentualJurosDia / 100
                        Dim instrucoes As String = OBS2 & "<br/><br/>" & linhads.Item("NR_NOTA_DEBITO").ToString()
                        If String.IsNullOrEmpty(Titulo.MensagemInstrucoesCaixa) Then
                            Titulo.MensagemInstrucoesCaixa = instrucoes
                        Else
                            Titulo.MensagemInstrucoesCaixa += " - " & instrucoes
                        End If


                        Titulo.CodigoProtesto = TipoCodigoProtesto.NaoProtestar
                        Titulo.DiasProtesto = 0
                        Titulo.CodigoBaixaDevolucao = TipoCodigoBaixaDevolucao.NaoBaixarNaoDevolver
                        Titulo.DiasBaixaDevolucao = 0
                        Titulo.ValidarDados()
                        objBoletos.Add(Titulo)

                        Dim Dig_NossoNum As String = GeraRemessa.Calculo_DV_NN_Santander(NossoNumero)
                        Con.ExecutarQuery("UPDATE [TB_FATURAMENTO] SET VL_BOLETO = '" & VL_BOLETO & "', NOSSONUMERO = '" & NossoNumero & "', DT_VENCIMENTO_BOLETO = CONVERT(DATE,'" & Titulo.DataVencimento & "',103), DT_EMISSAO_BOLETO = GETDATE(), COD_BANCO = '" & COD_BANCO & "', DIG_NOSSONUM='" & Dig_NossoNum.Replace(" ", "") & "',FL_ENVIADO_REM = 0 WHERE ID_FATURAMENTO = " & linhads.Item("ID_FATURAMENTO").ToString())

                    Next
                End If


                'limpa diretorio de boletos
                Dim di As System.IO.DirectoryInfo = New DirectoryInfo(Server.MapPath("/Content/boletos"))
                For Each file As FileInfo In di.GetFiles()
                    file.Delete()
                Next
                For Each dir As DirectoryInfo In di.GetDirectories()
                    dir.Delete(True)
                Next


                'Gera boletos
                Dim numBoletos As Integer = 0
                For Each linha In objBoletos
                    numBoletos += 1
                    Dim NovoBoleto = New BoletoBancario
                    NovoBoleto.Boleto = linha
                    Dim pdf = NovoBoleto.MontaBytesPDF(False)
                    File.WriteAllBytes(Server.MapPath("/Content/boletos\BOLETO " & NR_PROCESSO.Replace("/", "-") & ".pdf"), pdf)
                    txtIDBoleto.Text = NR_PROCESSO.Replace("/", "-")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "FuncImprimirBoleto()", True)

                Next

                divSuccess.Visible = True
                lblmsgSuccess.Text = "Boleto gerado com sucesso!"
                mpeOpcoesBoletos.Show()


            Catch ex As Exception
                divErro.Visible = True
                lblmsgErro.Text = "ERRO: " & ex.Message

            End Try
        End If

    End Sub

    Public Function FinalSemanaSubtrai(ByVal data As Date)
        If data.DayOfWeek = DayOfWeek.Saturday Then
            data = DateAdd(DateInterval.Day, -1, data)
        ElseIf data.DayOfWeek = DayOfWeek.Sunday Then
            data = DateAdd(DateInterval.Day, -2, data)
        End If
        Return data.ToString("dd/MM/yyyy")
    End Function
    Public Function FinalSemana(ByVal data As Date)
        If data.DayOfWeek = DayOfWeek.Saturday Then
            data = DateAdd(DateInterval.Day, 2, data)
        ElseIf data.DayOfWeek = DayOfWeek.Sunday Then
            data = DateAdd(DateInterval.Day, 1, data)
        End If
        Return data.ToString("dd/MM/yyyy")
    End Function

    Public Function PrazoCancelamento() As Boolean

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT DT_RPS,CASE WHEN  MONTH(GETDATE()) > MONTH(DT_RPS) THEN 'MAIOR' WHEN  MONTH(GETDATE()) < MONTH(DT_RPS) THEN 'MENOR'  ELSE 'IGUAL' END 'STATUS_DT' ,DATEDIFF(MONTH,DT_RPS,GETDATE()) AS DIFERENCA  FROM TB_FATURAMENTO WHERE DT_RPS IS NOT NULL AND ID_FATURAMENTO = " & txtID.Text)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("STATUS_DT") = "IGUAL" Then
                Return True
            ElseIf ds.Tables(0).Rows(0).Item("STATUS_DT") = "MENOR" Then
                Return True
            ElseIf ds.Tables(0).Rows(0).Item("STATUS_DT") = "MAIOR" And ds.Tables(0).Rows(0).Item("DIFERENCA") = 1 Then
                If Now.Day <= 5 Then
                    Return True

                Else
                    Return False

                End If
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Function PrazosSubstituicao() As Boolean
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT DT_RPS,CASE WHEN  MONTH(GETDATE()) > MONTH(DT_RPS) THEN 'MAIOR' WHEN  MONTH(GETDATE()) < MONTH(DT_RPS) THEN 'MENOR'  ELSE 'IGUAL' END 'STATUS_DT' ,DATEDIFF(MONTH,DT_RPS,GETDATE()) AS DIFERENCA  FROM TB_FATURAMENTO WHERE DT_RPS IS NOT NULL AND ID_FATURAMENTO = " & txtID.Text)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("STATUS_DT") = "MAIOR" And ds.Tables(0).Rows(0).Item("DIFERENCA") = 1 Then
                If Now.Day >= 5 Then
                    Return True

                Else
                    Return False

                End If
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Private Sub btnConsultaNotas_Click(sender As Object, e As EventArgs) Handles btnConsultaNotas.Click
        Session("RelFat") = ""
        Session("filtros") = ""

        Dim filtro As String = ""

        If ddlCliente.SelectedValue <> 0 Then
            Dim nome As String = ddlCliente.SelectedItem.Text
            filtro &= " AND NM_CLIENTE = '" & nome & "' "

        End If

        If ddlStatusConsultaNotas.SelectedValue <> 0 Then
            If ddlStatusConsultaNotas.SelectedValue = 1 Then
                filtro &= " AND DT_CANCELAMENTO IS NULL "
            End If
            If ddlStatusConsultaNotas.SelectedValue = 2 Then
                filtro &= " AND DT_CANCELAMENTO IS NOT NULL "
            End If
        End If

        If txtConsultaNotaInicio.Text <> "" Then
            If txtConsultaNotaFim.Text <> "" Then
                'filtro
                filtro &= " AND NR_NOTA_FISCAL BETWEEN '" & txtConsultaNotaInicio.Text & "' AND '" & txtConsultaNotaFim.Text & "' "
            Else
                'msg erro
                divErroConsultasNotas.Visible = True
                lblErroConsultasNotas.Text = "É necessário informar inicio e fim para prosseguir com a consulta!"
                ModalPopupExtender9.Show()
                Exit Sub
            End If
        End If

        If txtConsultaRPSInicio.Text <> "" Then
            If txtConsultaRPSFim.Text <> "" Then
                'filtro
                filtro &= " AND NR_RPS BETWEEN '" & txtConsultaRPSInicio.Text & "' AND '" & txtConsultaRPSFim.Text & "' "

            Else
                'msg erro
                divErroConsultasNotas.Visible = True
                lblErroConsultasNotas.Text = "É necessário informar inicio e fim para prosseguir com a consulta!"
                ModalPopupExtender9.Show()
                Exit Sub
            End If
        End If

        If txtConsultaVencimentoInicio.Text <> "" Then
            If txtConsultaVencimentoFim.Text <> "" Then
                'filtro
                filtro &= " AND  CONVERT(DATE,DT_VENCIMENTO,103)  BETWEEN CONVERT(DATE,'" & txtConsultaVencimentoInicio.Text & "',103) AND CONVERT(DATE,'" & txtConsultaVencimentoFim.Text & "',103) "

            Else
                'msg erro
                divErroConsultasNotas.Visible = True
                lblErroConsultasNotas.Text = "É necessário informar data de inicio e fim para prosseguir com a consulta!"
                ModalPopupExtender9.Show()
                Exit Sub

            End If
        End If

        If txtConsultaPagamentoInicio.Text <> "" Then
            If txtConsultaPagamentoFim.Text <> "" Then
                'filtro
                ' filtro &= " AND CONVERT(DATE,DT_NOTA_FISCAL,103) BETWEEN CONVERT(DATE,'" & txtConsultaPagamentoInicio.Text & "',103) AND CONVERT(DATE,'" & txtConsultaPagamentoFim.Text & "',103) "
                filtro &= " AND CONVERT(DATE,DT_COMPETENCIA,103) BETWEEN CONVERT(DATE,'" & txtConsultaPagamentoInicio.Text & "',103) AND CONVERT(DATE,'" & txtConsultaPagamentoFim.Text & "',103) "

            Else
                'msg erro
                divErroConsultasNotas.Visible = True
                lblErroConsultasNotas.Text = "É necessário informar data de inicio e fim para prosseguir com a consulta!"
                ModalPopupExtender9.Show()
                Exit Sub
            End If
        End If

        If ddlTaxa.SelectedValue <> 0 Then
            filtro &= " AND ID_CONTA_PAGAR_RECEBER IN (SELECT DISTINCT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_ITEM_DESPESA = " & ddlTaxa.SelectedValue & ") "
        End If


        Session("RelFat") = " WHERE NR_NOTA_FISCAL IS NOT NULL " & filtro

        Dim sql As String = "SELECT * FROM [dbo].[View_Faturamento] WHERE NR_NOTA_FISCAL IS NOT NULL " & filtro & " ORDER BY DT_VENCIMENTO,NR_PROCESSO"
        dsFaturamento.SelectCommand = sql
        dgvFaturamento.DataBind()
        ModalPopupExtender9.Hide()
        divErroConsultasNotas.Visible = False

        ddlCliente.SelectedValue = 0
        ddlStatusConsultaNotas.SelectedValue = 0

        txtConsultaNotaInicio.Text = ""
        txtConsultaNotaFim.Text = ""

        txtConsultaRPSInicio.Text = ""
        txtConsultaRPSFim.Text = ""

        txtConsultaVencimentoInicio.Text = ""
        txtConsultaVencimentoFim.Text = ""

        txtConsultaPagamentoInicio.Text = ""
        txtConsultaPagamentoFim.Text = ""
        lkRelatorioFaturamento.Text = "Imprimir Relatório de Nota"
    End Sub

    Private Sub btnProsseguir_Click(sender As Object, e As EventArgs) Handles btnProsseguir.Click
        If txtAuxObsRps.Text = "GERAR" Then
            Dim Con As New Conexao_sql
            Con.Conectar()

            Con.ExecutarQuery("UPDATE [TB_FATURAMENTO] SET OB_RPS = '" & txtOBSRPS.Text & "' WHERE ID_FATURAMENTO = " & txtID.Text)

            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_DEBITO,NR_RPS,DT_RPS,ISNULL(COMPL_ENDERECO,'')COMPL_ENDERECO FROM [TB_FATURAMENTO]
WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_DEBITO")) Then
                    If IsDBNull(ds.Tables(0).Rows(0).Item("NR_RPS")) And IsDBNull(ds.Tables(0).Rows(0).Item("DT_RPS")) Then

                        If ds.Tables(0).Rows(0).Item("COMPL_ENDERECO").ToString.Length > 60 Then
                            divErro.Visible = True
                            lblmsgErro.Text = "Campo complemento do endereço exede o valor maximo permitido!"
                            Exit Sub
                        End If

                        Dim dsVerificaReceita As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_CONTA_PAGAR_RECEBER_ITENS)QTD,SUM(ISNULL(VL_LIQUIDO,0))VL_LIQUIDO,SUM(ISNULL(VL_ISS,0))VL_ISS FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER IN (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & txtID.Text & ") AND ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_RECEITA = 1)")

                        If dsVerificaReceita.Tables(0).Rows(0).Item("QTD") = 0 Then

                            lblmsgErro.Text = "Não foi possivel completar a ação: fatura selecionada não possui item de receita!"
                            divErro.Visible = True
                        Else
                            Dim NumeracaoDoc As New NumeracaoDoc
                            Dim numero As String = NumeracaoDoc.Numerar(3)
                            Dim ValorExtenso As New ValorExtenso
                            Dim Extenso As String = ValorExtenso.NumeroToExtenso(dsVerificaReceita.Tables(0).Rows(0).Item("VL_LIQUIDO"))
                            Dim Valor As String = dsVerificaReceita.Tables(0).Rows(0).Item("VL_LIQUIDO").ToString
                            Dim LIQUIDO As Decimal = dsVerificaReceita.Tables(0).Rows(0).Item("VL_LIQUIDO").ToString

                            Valor = Valor.Replace(".", "")
                            Valor = Valor.Replace(",", ".")


                            Dim VL_ISS As String = dsVerificaReceita.Tables(0).Rows(0).Item("VL_ISS").ToString
                            VL_ISS = VL_ISS.Replace(".", "")
                            VL_ISS = VL_ISS.Replace(",", ".")



                            Dim ConOracle As New Conexao_oracle
                            ConOracle.Conectar()
                            Dim dt As DataTable = ConOracle.Consultar("select SERIE from Sgipa.TB_SERIE_RPS WHERE FLAG_ATIVO = 1 ")
                            Dim SERIE_RPS As String = ""

                            If dt.Rows.Count > 0 Then
                                SERIE_RPS = dt.Rows(0)("SERIE").ToString
                            End If



                            Dim IR As String = 0

                            If Session("FL_ROTINA_IR_ATIVA") = True Then

                                Dim sqlIR As String = ""
                                If txtID_SERVICO.Text = 1 Or txtID_SERVICO.Text = 4 Then
                                    'MARITIMO
                                    sqlIR = "SELECT COUNT(*) QTD FROM TB_ITEM_DESPESA WHERE CD_ISS_MAR = '10.05' AND ID_ITEM_DESPESA IN (
                                SELECT ID_ITEM_DESPESA FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER IN (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & txtID.Text & " )) AND FL_RECEITA = 1 "
                                ElseIf txtID_SERVICO.Text = 2 Or txtID_SERVICO.Text = 5 Then
                                    'AEREO
                                    sqlIR = "SELECT COUNT(*) QTD FROM TB_ITEM_DESPESA WHERE CD_ISS_AER = '10.05' AND ID_ITEM_DESPESA IN (
                                SELECT ID_ITEM_DESPESA FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER IN (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & txtID.Text & " )) AND FL_RECEITA = 1 "
                                Else
                                    'OUTROS
                                    sqlIR = "SELECT COUNT(*) QTD FROM TB_ITEM_DESPESA WHERE (CD_ISS_AER = '10.05' or CD_ISS_MAR = '10.05') AND ID_ITEM_DESPESA IN (
                                SELECT ID_ITEM_DESPESA FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER IN (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & txtID.Text & " )) AND FL_RECEITA = 1 "
                                End If

                                Dim dsIR As DataSet = Con.ExecutarQuery(sqlIR)
                                If dsIR.Tables(0).Rows(0).Item("QTD") > 0 Then
                                    dsIR = Con.ExecutarQuery("select isnull(SUM(VL_NOTA),0)TOTAL_NOTAS,
                                isnull(sum(VL_IR_NF),0)IR_ANTERIOR  from tb_faturamento where year(DT_NOTA_FISCAL) = year(getdate()) and month(DT_NOTA_FISCAL) = month(getdate()) and day(DT_NOTA_FISCAL) = day(getdate()) and ID_PARCEIRO_CLIENTE = " & txtID_CLIENTE.Text)
                                    If dsIR.Tables(0).Rows.Count > 0 Then
                                        Dim TOTAL_NOTAS As Decimal = dsIR.Tables(0).Rows(0).Item("TOTAL_NOTAS")
                                        Dim IR_ANTERIOR As Decimal = dsIR.Tables(0).Rows(0).Item("IR_ANTERIOR")
                                        Dim IR_NOVO As Decimal = 0

                                        If TOTAL_NOTAS > Session("VL_ISENTO_IR_NF") Then

                                            'CALCULA IR E SALVA NA VARIAVEL PARA POSTERIORMENTE FAZER O UPDATE
                                            IR_NOVO = LIQUIDO / 100
                                            IR_NOVO = IR_NOVO * Session("VL_PERC_IR_NF")
                                            IR = IR_NOVO

                                            'TIRA IR DO VALOR TOTAL DAS NOTAS
                                            TOTAL_NOTAS = TOTAL_NOTAS - IR_NOVO

                                            'TIRA IR ANTERIOR DO VALOR TOTAL DE NOTAS COM DESCONTO DO IR NOVO
                                            TOTAL_NOTAS = TOTAL_NOTAS - IR_ANTERIOR
                                        Else

                                            LIQUIDO = LIQUIDO + TOTAL_NOTAS
                                            If LIQUIDO > Session("VL_ISENTO_IR_NF") Then
                                                'CALCULA IR E SALVA NA VARIAVEL PARA POSTERIORMENTE FAZER O UPDATE
                                                IR_NOVO = LIQUIDO / 100
                                                IR_NOVO = IR_NOVO * Session("VL_PERC_IR_NF")
                                                IR = IR_NOVO

                                                'TIRA IR DO VALOR TOTAL DAS NOTAS
                                                TOTAL_NOTAS = TOTAL_NOTAS - IR_NOVO

                                                'TIRA IR ANTERIOR DO VALOR TOTAL DE NOTAS COM DESCONTO DO IR NOVO
                                                TOTAL_NOTAS = TOTAL_NOTAS - IR_ANTERIOR
                                            End If
                                        End If

                                    End If

                                    IR = IR.Replace(".", "")
                                    IR = IR.Replace(",", ".")

                                End If

                            End If
                            Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET STATUS_NFE = 0,DT_RPS = getdate(), NR_RPS = '" & numero & "',VL_NOTA = " & Valor & ",VL_NOTA_EXTENSO = '" & Extenso & "', VL_ISS = " & VL_ISS & ", SERIE_RPS = '" & SERIE_RPS & "',VL_IR_NF= '" & IR & "' WHERE ID_FATURAMENTO =" & txtID.Text)


                            Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RPS = '" & numero & "' WHERE ID_NUMERACAO = 5")

                            Try
                                Using GeraRps = New WsNVOCC.WsNvocc

                                    Dim consulta = GeraRps.IntegraNFePrefeitura(numero, 1, "SQL", "NVOCC", 0)

                                End Using


                            Catch ex As Exception

                                divErro.Visible = True
                                lblmsgErro.Text = "Não foi possivel completar a ação: " & ex.Message
                                Exit Sub

                            End Try

                            Using GeraRps = New WsNVOCC.WsNvocc

                                Dim consulta = GeraRps.ConsultaNFePrefeitura(txtID.Text, 1, "SQL", "NVOCC")

                            End Using


                            ds = Con.ExecutarQuery("SELECT isnull(STATUS_NFE,0)STATUS_NFE FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
                            If ds.Tables(0).Rows.Count > 0 Then
                                If ds.Tables(0).Rows(0).Item("STATUS_NFE") = 2 Then
                                    divSuccess.Visible = True
                                    lblmsgSuccess.Text = "RPS gerada com sucesso!"
                                    dsFaturamento.SelectCommand = "Select * FROM [dbo].[View_Faturamento] where NR_RPS = '" & numero & "'"
                                    dgvFaturamento.DataBind()

                                ElseIf ds.Tables(0).Rows(0).Item("STATUS_NFE") = 4 Then
                                    divinf.Visible = True
                                    lblmsginf.Text = "RPS em processamento, por favor aguarde!"
                                    dsFaturamento.SelectCommand = "Select * FROM [dbo].[View_Faturamento] where NR_RPS = '" & numero & "'"
                                    dgvFaturamento.DataBind()

                                ElseIf ds.Tables(0).Rows(0).Item("STATUS_NFE") = 5 Then

                                    Dim ERRO As String = ""
                                    Dim DS1 As DataSet = Con.ExecutarQuery("SELECT CRITICA FROM TB_LOG_NFSE WHERE ID_LOG = (SELECT MAX(ID_LOG)ID_LOG FROM TB_LOG_NFSE WHERE CRITICA IS NOT NULL AND ID_FATURAMENTO = " & txtID.Text & " )")
                                    If DS1.Tables(0).Rows.Count > 0 Then
                                        If Not IsDBNull(DS1.Tables(0).Rows(0).Item("CRITICA")) Then
                                            ERRO = DS1.Tables(0).Rows(0).Item("CRITICA")
                                        End If
                                    End If
                                    lblmsgErro.Text = "Não foi possivel completar a ação!: " & ERRO
                                    divErro.Visible = True
                                    dsFaturamento.SelectCommand = "Select * FROM [dbo].[View_Faturamento] where NR_RPS = '" & numero & "'"
                                    dgvFaturamento.DataBind()

                                Else
                                    lblmsgErro.Text = "Não foi possivel completar a ação!"
                                    divErro.Visible = True
                                    dsFaturamento.SelectCommand = "Select * FROM [dbo].[View_Faturamento] where NR_RPS = '" & numero & "'"
                                    dgvFaturamento.DataBind()
                                End If

                            End If



                        End If
                    Else
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura já possui RPS!"
                        divErro.Visible = True
                    End If

                Else
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota de débito!"
                    divErro.Visible = True
                End If

            End If

        ElseIf txtAuxObsRps.Text = "RECIBO" Then

            If txtOBSRPS.Text = "" Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "FuncImprimirRPS()", True)

            Else
                Dim Con As New Conexao_sql
                Con.Conectar()

                Con.ExecutarQuery("UPDATE [TB_FATURAMENTO] SET OB_RPS = '" & txtOBSRPS.Text & "' WHERE ID_FATURAMENTO = " & txtID.Text)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "FuncImprimirRPS()", True)
            End If

        End If
        mpeRPS.Show()

    End Sub

    Private Sub btnFecharDesmosntrativos_Click(sender As Object, e As EventArgs) Handles btnFecharDesmosntrativos.Click
        mpeDemonstrativos.Hide()
        divSuccess.Visible = False
        divErro.Visible = False
        divinf.Visible = False
        Dim ID As String = txtID.Text
        AtualizaGrid()

        For Each linha As GridViewRow In dgvFaturamento.Rows
            Dim check As CheckBox = linha.FindControl("ckSelecionar")
            Dim ID_grid As String = CType(linha.FindControl("lblID"), Label).Text
            If ID_grid = ID Then
                check.Checked = True
            End If
        Next

        txtID.Text = ID
    End Sub

    Private Sub lkBoletoRemessa_Click(sender As Object, e As EventArgs) Handles lkBoletoRemessa.Click
        Response.Redirect("RemessaBoletos.aspx")
    End Sub

    Private Sub lkExcluirBoleto_Click(sender As Object, e As EventArgs) Handles lkExcluirBoleto.Click
        divSuccess.Visible = False
        divErro.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"

        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Con.ExecutarQuery("UPDATE [TB_FATURAMENTO] SET VL_BOLETO = NULL, NOSSONUMERO = NULL , DT_VENCIMENTO_BOLETO = NULL , DT_EMISSAO_BOLETO = NULL , COD_BANCO = NULL , DIG_NOSSONUM = NULL , FL_ENVIADO_REM = 0 WHERE ID_FATURAMENTO =" & txtID.Text)
            divSuccess.Visible = True
            lblmsgSuccess.Text = "Boleto deletado com sucesso!"
            AtualizaGrid()
        End If

    End Sub

    Private Sub dgvFaturamento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFaturamento.RowCommand
        If e.CommandName = "Anexo" Then
            Try

                Dim ID_FATURAMENTO As String = e.CommandArgument
                Dim diretorio_arquivos As String = Server.MapPath("~/Content/Arquivos/Faturamento") & ID_FATURAMENTO

                If Directory.Exists(diretorio_arquivos) Then
                    Dim di As System.IO.DirectoryInfo = New DirectoryInfo(diretorio_arquivos)
                    For Each file As FileInfo In di.GetFiles()
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "AbrirArquivo(" & ID_FATURAMENTO & ")", True)
                    Next
                Else
                    lblmsgErro.Text = "Faturamento selecionado não possui anexo!"
                    divErro.Visible = True
                End If

            Catch ex As Exception
                lblmsgErro.Text = ex.Message
                divErro.Visible = True
            End Try
        End If

    End Sub

    Private Sub dgvFaturamento_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvFaturamento.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ID_FATURAMENTO As Label = CType(e.Row.FindControl("lblID"), Label)
            Dim ANEXO As LinkButton = CType(e.Row.FindControl("lnkAnexo"), LinkButton)


            Dim diretorio_arquivos As String = Server.MapPath("~/Content/Arquivos/Faturamento") & ID_FATURAMENTO.Text.ToString
            ANEXO.Visible = False
            If Directory.Exists(diretorio_arquivos) Then
                Dim di As System.IO.DirectoryInfo = New DirectoryInfo(diretorio_arquivos)
                For Each file As FileInfo In di.GetFiles()
                    ANEXO.Visible = True
                Next
            Else
                ANEXO.Visible = False
            End If

        End If
    End Sub

    Private Sub txtCNPJSub_TextChanged(sender As Object, e As EventArgs) Handles txtCNPJSub.TextChanged
        divErroSubstituir.Visible = False

        If txtCNPJSub.Text <> "" Then
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO, UPPER(P.NM_RAZAO)NM_RAZAO, UPPER(P.ENDERECO)ENDERECO, P.NR_ENDERECO, UPPER(P.COMPL_ENDERECO)COMPL_ENDERECO, UPPER(P.BAIRRO)BAIRRO, P.CEP, P.CNPJ, P.INSCR_ESTADUAL, P.INSCR_MUNICIPAL, UPPER(C.NM_CIDADE)NM_CIDADE, UPPER(E.NM_ESTADO)NM_ESTADO FROM TB_PARCEIRO P LEFT JOIN TB_CIDADE C ON C.ID_CIDADE = P.ID_CIDADE LEFT JOIN TB_ESTADO E ON E.ID_ESTADO = C.ID_ESTADO WHERE CNPJ ='" & txtCNPJSub.Text & "'")
            If ds.Tables(0).Rows.Count > 0 Then
                txtIDParceiro.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO")

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_RAZAO")) Then
                    txtRazaoSocial.Text = ds.Tables(0).Rows(0).Item("NM_RAZAO")
                Else
                    txtRazaoSocial.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ENDERECO")) Then
                    txtEndereco.Text = ds.Tables(0).Rows(0).Item("ENDERECO")
                Else
                    txtEndereco.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_ENDERECO")) Then
                    txtNumEndereco.Text = ds.Tables(0).Rows(0).Item("NR_ENDERECO")
                Else
                    txtNumEndereco.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("BAIRRO")) Then
                    txtBairro.Text = ds.Tables(0).Rows(0).Item("BAIRRO")
                Else
                    txtBairro.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("COMPL_ENDERECO")) Then
                    txtComplem.Text = ds.Tables(0).Rows(0).Item("COMPL_ENDERECO")
                Else
                    txtComplem.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_CIDADE")) Then
                    txtCidade.Text = ds.Tables(0).Rows(0).Item("NM_CIDADE")
                Else
                    txtCidade.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_ESTADO")) Then
                    txtEstado.Text = ds.Tables(0).Rows(0).Item("NM_ESTADO")
                Else
                    txtEstado.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CEP")) Then
                    txtCEP.Text = ds.Tables(0).Rows(0).Item("CEP")
                Else
                    txtCEP.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("INSCR_ESTADUAL")) Then
                    txtInscEstadual.Text = ds.Tables(0).Rows(0).Item("INSCR_ESTADUAL")
                Else
                    txtInscEstadual.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("INSCR_MUNICIPAL")) Then
                    txtInscMunicipal.Text = ds.Tables(0).Rows(0).Item("INSCR_MUNICIPAL")
                Else
                    txtInscMunicipal.Text = ""
                End If

            Else
                divErroSubstituir.Visible = True
                lblErroSubstituir.Text = "CNPJ não localizado!"

            End If
        End If
        ModalPopupExtender5.Show()
    End Sub

    Private Sub lkOpcoesBoletos_Click(sender As Object, e As EventArgs) Handles lkOpcoesBoletos.Click
        divSuccess.Visible = False
        divErro.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            mpeOpcoesBoletos.Show()
            mpeDemonstrativos.Hide()

        End If
    End Sub
    Private Sub btnFecharOpcoesBoletos_Click(sender As Object, e As EventArgs) Handles btnFecharOpcoesBoletos.Click
        mpeOpcoesBoletos.Hide()
        mpeDemonstrativos.Show()
    End Sub
    Private Sub lkRPS_Click(sender As Object, e As EventArgs) Handles lkRPS.Click
        mpeDemonstrativos.Hide()
        mpeRPS.Show()
    End Sub

    Private Sub btnFecharRPS_Click(sender As Object, e As EventArgs) Handles btnFecharRPS.Click
        mpeDemonstrativos.Show()
        mpeRPS.Hide()
    End Sub

    Private Sub btnFecharBoleto_Click(sender As Object, e As EventArgs) Handles btnFecharBoleto.Click
        mpeBoleto.Hide()
        mpeOpcoesBoletos.Show()
    End Sub


    Private Sub lkFatura_Click(sender As Object, e As EventArgs) Handles lkFatura.Click
        divSuccess.Visible = False
        divErro.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            mpeOpcoesFatura.Show()
        End If

    End Sub

    Private Sub lkNotaDebito_Click(sender As Object, e As EventArgs) Handles lkNotaDebito.Click
        divSuccess.Visible = False
        divErro.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ImprimirND()", True)
        End If
    End Sub

    Private Sub lkNotasFiscais_Click(sender As Object, e As EventArgs) Handles lkNotasFiscais.Click
        divSuccess.Visible = False
        divErro.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            mpeNotasFiscais.Show()
        End If
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("Faturamento.aspx")
    End Sub

    Private Sub lkRelatorioFaturamento_Click(sender As Object, e As EventArgs) Handles lkRelatorioFaturamento.Click
        divSuccess.Visible = False
        divErro.Visible = False
        If dgvFaturamento.Rows.Count = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Não há registros para serem impressos"
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "RelatorioFaturamento()", True)
        End If
    End Sub

    Private Sub lkTesteCancelamentoNF_Click(sender As Object, e As EventArgs) Handles lkTesteCancelamentoNF.Click

        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_FISCAL,NR_RPS FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                    If PrazoCancelamento() = False Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Prazo para cancelamento expirado!"
                        Exit Sub

                    Else

                        Try

                            Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_CANCELAMENTO = getdate(), ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = 'TESTE CANCELAMENTO NF TOTVS' , STATUS_NFE = 3, CANCELA_NFE  = 1 WHERE ID_FATURAMENTO =" & txtID.Text)

                            divSuccess.Visible = True
                            lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"

                        Catch ex As Exception

                            divErro.Visible = True
                            lblmsgErro.Text = "Não foi possivel completar a ação: " & ex.Message

                        End Try


                    End If
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
                End If

            Else
                divErro.Visible = True
                lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
            End If
            Con.Fechar()
        End If

    End Sub
End Class