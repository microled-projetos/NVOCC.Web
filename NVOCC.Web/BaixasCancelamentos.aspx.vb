Public Class BaixasCancelamentos
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

        Else
            If Request.QueryString("t") = "p" Then
                lblTipo.Text = "CONTAS A PAGAR"
                'btnBaixarPagamento.Visible = True
                gridPagar.Visible = True
                gridReceber.Visible = False
                btnCambio.Visible = False

            ElseIf Request.QueryString("t") = "r" Then
                lblTipo.Text = "CONTAS A RECEBER"
                'btnBaixarRecebimento.Visible = True
                gridPagar.Visible = False
                gridReceber.Visible = True
                'If rdStatus.SelectedValue = 1 Then
                '    btnCambio.Visible = True
                'Else
                '    btnCambio.Visible = False
                'End If
            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub btnSalvarCancelamento_Click(sender As Object, e As EventArgs) Handles btnSalvarCancelamento.Click
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()

        If Request.QueryString("t") = "p" Then

            For Each linha As GridViewRow In dgvTaxasPagar.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text

                    Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_CONTA_PAGAR_RECEBER)QTD FROM TB_FATURAMENTO WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER =" & ID)
                    If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                        lblErro.Text = "Não foi possivel conclir a ação: Registro já faturado!"
                        divErro.Visible = True
                        Exit Sub
                    Else

                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "'  WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
                        lblSuccess.Text = "Cancelamento realizado com sucesso!"
                        divSuccess.Visible = True
                    End If

                End If
            Next



            dgvTaxasPagar.DataBind()

        ElseIf Request.QueryString("t") = "r" Then

            For Each linha As GridViewRow In dgvTaxasReceber.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")

                If check.Checked Then

                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_CONTA_PAGAR_RECEBER)QTD FROM TB_FATURAMENTO WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER =" & ID)
                    If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                        lblErro.Text = "Não foi possivel conclir a ação: Registro já faturado!"
                        divErro.Visible = True
                        Exit Sub
                    Else
                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "' WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
                        lblSuccess.Text = "Cancelamento realizado com sucesso!"
                        divSuccess.Visible = True

                    End If

                End If
            Next

            dgvTaxasReceber.DataBind()

        End If

        Con.Fechar()

        txtData.Text = ""
        txtObs.Text = ""
        mpeObs.Hide()


    End Sub

    Private Sub btnSalvarBaixa_Click(sender As Object, e As EventArgs) Handles btnSalvarBaixa.Click
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtData.Text = "" Then
            lblErro.Text = "É necessário informar a data para efetuar a baixa!"
            divErro.Visible = True
            Exit Sub

        Else
            If Request.QueryString("t") = "p" Then

                For Each linha As GridViewRow In dgvTaxasPagar.Rows
                    Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                    If check.Checked Then
                        Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtData.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
                    End If
                Next

                dgvTaxasPagar.DataBind()

            ElseIf Request.QueryString("t") = "r" Then

                For Each linha As GridViewRow In dgvTaxasReceber.Rows
                    Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                    If check.Checked Then
                        Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtData.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER =" & ID)

                        Dim dsFaturamento As DataSet = Con.ExecutarQuery("SELECT ID_FATURAMENTO FROM TB_FATURAMENTO WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER = " & ID)
                        If dsFaturamento.Tables(0).Rows.Count > 0 Then
                            Dim NumeracaoDoc As New NumeracaoDoc
                            Dim numero As String = NumeracaoDoc.Numerar(2)

                            Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_RECIBO = getdate(), NR_RECIBO = '" & numero & "' WHERE ID_FATURAMENTO =" & dsFaturamento.Tables(0).Rows(0).Item("ID_FATURAMENTO"))
                            Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RECIBO = '" & numero & "'")
                        End If
                    End If
                Next

                dgvTaxasReceber.DataBind()

            End If


            Con.Fechar()
            lblSuccess.Text = "Baixa realizada com sucesso!"
            divSuccess.Visible = True
            txtData.Text = ""
            txtObs.Text = ""
            mpeObs.Hide()

        End If
    End Sub


    Private Sub dgvTaxasPagar_Load(sender As Object, e As EventArgs) Handles dgvTaxasPagar.Load
        Dim Con As New Conexao_sql
        lblFaturaCambio.Text = ""
        lblProcessoCambio.Text = ""
        lblClienteCambio.Text = ""

        lblFaturaCancelamento.Text = ""
        lblProcessoCancelamento.Text = ""
        lblClienteCancelamento.Text = ""

        lblFaturaBaixa.Text = ""
        lblProcessoBaixa.Text = ""
        lblClienteBaixa.Text = ""
        For Each linha As GridViewRow In dgvTaxasPagar.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim fatura As String = CType(linha.FindControl("lblFatura"), Label).Text
            Dim fornecedor As String = CType(linha.FindControl("lblFornecedor"), Label).Text

            If check.Checked Then
                lblFaturaCancelamento.Text &= "Nº Fatura: " & fatura & "<br/>"
                lblClienteCancelamento.Text &= "Fornecedor: " & fornecedor & "<br/>"


                lblFaturaBaixa.Text &= "Nº Fatura: " & fatura & "<br/>"
                lblClienteBaixa.Text &= "Fornecedor: " & fornecedor & "<br/>"

                lblFaturaCambio.Text &= "Nº Fatura: " & fatura & "<br/>"
                lblClienteCambio.Text &= "Fornecedor: " & fornecedor & "<br/>"
            End If


        Next


    End Sub


    Private Sub dgvTaxasReceber_Load(sender As Object, e As EventArgs) Handles dgvTaxasReceber.Load
        Using Status = New NotaFiscal.WsNvocc

            Status.StatusBloqueio(dsReceber.SelectCommand)

        End Using

        CarregaGridReceber()
    End Sub

    Sub CarregaGridReceber()

        Dim Con As New Conexao_sql
        lblFaturaCambio.Text = ""
        lblProcessoCambio.Text = ""
        lblClienteCambio.Text = ""

        lblFaturaCancelamento.Text = ""
        lblProcessoCancelamento.Text = ""
        lblClienteCancelamento.Text = ""

        lblFaturaBaixa.Text = ""
        lblProcessoBaixa.Text = ""
        lblClienteBaixa.Text = ""
        For Each linha As GridViewRow In dgvTaxasReceber.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim processo As String = CType(linha.FindControl("lblProcesso"), Label).Text
            Dim fornecedor As String = CType(linha.FindControl("lblFornecedor"), Label).Text
            Dim ID_PARCEIRO_ARMAZEM_DESCARGA As String = CType(linha.FindControl("lblID_PARCEIRO_ARMAZEM_DESCARGA"), Label).Text

            'Dim FL_BLOQUEIO_FCA As String = CType(linha.FindControl("lblFL_BLOQUEIO_FCA"), Label).Text
            'Dim btnDesbloquearFCA As ImageButton = CType(linha.FindControl("btnDesbloquearFCA"), ImageButton)
            'Dim btnBloquearFCA As ImageButton = CType(linha.FindControl("btnBloquearFCA"), ImageButton)

            Dim FL_BLOQUEIO_FINANCEIRO As String = CType(linha.FindControl("lblFL_BLOQUEIO_FINANCEIRO"), Label).Text
            Dim btnDesbloquearFinanceiro As ImageButton = CType(linha.FindControl("btnDesbloquearFinanceiro"), ImageButton)
            Dim btnBloquearFinanceiro As ImageButton = CType(linha.FindControl("btnBloquearFinanceiro"), ImageButton)

            Dim FL_BLOQUEIO_DOCUMENTAL As String = CType(linha.FindControl("lblFL_BLOQUEIO_DOCUMENTAL"), Label).Text
            Dim btnDesbloquearDocumental As ImageButton = CType(linha.FindControl("btnDesbloquearDocumental"), ImageButton)
            Dim btnBloquearDocumental As ImageButton = CType(linha.FindControl("btnBloquearDocumental"), ImageButton)

            If check.Checked Then
                lblProcessoCancelamento.Text &= "Nº Processo: " & processo & "<br/>"
                lblClienteCancelamento.Text &= "Fornecedor: " & fornecedor & "<br/>"


                lblProcessoBaixa.Text &= "Nº Processo: " & processo & "<br/>"
                lblClienteBaixa.Text &= "Fornecedor: " & fornecedor & "<br/>"

                lblProcessoCambio.Text &= "Nº Processo: " & processo & "<br/>"
                lblClienteCambio.Text &= "Fornecedor: " & fornecedor & "<br/>"
            End If

            If ID_PARCEIRO_ARMAZEM_DESCARGA = 74 Then
                'If FL_BLOQUEIO_FCA = "SIM" Then
                '    btnBloquearFCA.Visible = False
                '    btnDesbloquearFCA.Visible = True

                'ElseIf FL_BLOQUEIO_FCA = "NÃO" Then
                '    btnDesbloquearFCA.Visible = False
                '    btnBloquearFCA.Visible = True

                'End If


                If FL_BLOQUEIO_DOCUMENTAL = "SIM" Then
                    btnBloquearDocumental.Visible = False
                    btnDesbloquearDocumental.Visible = True

                ElseIf FL_BLOQUEIO_DOCUMENTAL = "NÃO" Then
                    btnDesbloquearDocumental.Visible = False
                    btnBloquearDocumental.Visible = True

                End If



                If FL_BLOQUEIO_FINANCEIRO = "SIM" Then
                    btnBloquearFinanceiro.Visible = False
                    btnDesbloquearFinanceiro.Visible = True

                ElseIf FL_BLOQUEIO_FINANCEIRO = "NÃO" Then
                    btnDesbloquearFinanceiro.Visible = False
                    btnBloquearFinanceiro.Visible = True

                End If

            Else

                'btnBloquearFCA.Visible = False
                'btnDesbloquearFCA.Visible = False
                btnBloquearFinanceiro.Visible = False
                btnDesbloquearFinanceiro.Visible = False
                btnBloquearDocumental.Visible = False
                btnDesbloquearDocumental.Visible = False

            End If
        Next

    End Sub
    Sub Filtro()
        divErro.Visible = False
        divSuccess.Visible = False
        Dim FILTRO As String = ""
        If rdStatus.SelectedValue = 2 Then

            If txtVencimentoInicial.Text = "" Or txtVencimentoFinal.Text = "" Then
                lblErro.Text = "É necessário informar a data de vencimento inicial e final para busca de faturas fechadas!"
                divErro.Visible = True
                Exit Sub
            Else
                FILTRO &= " AND DT_LIQUIDACAO IS NOT NULL and CONVERT(DATE,DT_VENCIMENTO,103) Between CONVERT(DATE,'" & txtVencimentoInicial.Text & "',103) and CONVERT(DATE,'" & txtVencimentoFinal.Text & "',103)"

            End If

            btnAtualizaCambio.Visible = False

        ElseIf rdStatus.SelectedValue = 1 Then


            If txtVencimentoInicial.Text = "" Or txtVencimentoFinal.Text = "" Then
                FILTRO &= " AND DT_LIQUIDACAO IS NULL"

            Else
                FILTRO &= " AND DT_LIQUIDACAO IS NULL and CONVERT(DATE,DT_VENCIMENTO,103) Between CONVERT(DATE,'" & txtVencimentoInicial.Text & "',103) and CONVERT(DATE,'" & txtVencimentoFinal.Text & "',103)"

            End If

            btnAtualizaCambio.Visible = True
        End If
        Dim sql As String = "SELECT * FROM [View_Baixas_Cancelamentos]  WHERE CD_PR =  'R' " & FILTRO & " ORDER BY DT_VENCIMENTO DESC"
        Using Status = New NotaFiscal.WsNvocc

            Status.StatusBloqueio(sql)

        End Using

        dsReceber.SelectCommand = sql
        Session("CSVReceber") = sql
        dgvTaxasReceber.DataBind()
        CarregaGridReceber()


        sql = "SELECT * FROM [View_Baixas_Cancelamentos]  WHERE CD_PR =  'P' " & FILTRO & " ORDER BY DT_VENCIMENTO DESC"
        dsPagar.SelectCommand = sql
        Session("CSVPagar") = sql

        dgvTaxasPagar.DataBind()
    End Sub
    Private Sub btnpesquisar_Click(sender As Object, e As EventArgs) Handles btnpesquisar.Click
        Filtro()
    End Sub

    Private Sub dgvTaxasReceber_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxasReceber.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        Dim ds As DataSet
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim resultado As String
        Dim lblLogin As Label = TryCast(Page.Master.FindControl("lbllogin"), Label)
        Dim Usuario As String = lblLogin.Text


        If e.CommandName = "BloquearFCA" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "B", 39, 0, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try


            End If

        ElseIf e.CommandName = "DesbloquearFCA" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "L", 39, 45, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try



            End If
        ElseIf e.CommandName = "BloquearFinanceiro" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "B", 40, 0, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try


            End If

        ElseIf e.CommandName = "DesbloquearFinanceiro" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "L", 40, 45, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try



            End If
        ElseIf e.CommandName = "BloquearDocumental" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "B", 44, 0, Usuario)

                    End Using


                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação" & ex.Message
                    Exit Sub

                End Try


            End If

        ElseIf e.CommandName = "DesbloquearDocumental" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "L", 44, 45, Usuario)

                    End Using


                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try



            End If
        End If

        If resultado = "BL não localizado!" Then
            divErro.Visible = True
            lblErro.Text = resultado
        Else
            divSuccess.Visible = True
            lblSuccess.Text = "Ação realizada com sucesso!"
        End If
        Filtro()
    End Sub

    Private Sub btnAtualizaCambio_Click(sender As Object, e As EventArgs) Handles btnAtualizaCambio.Click
        lblErro.Text = ""
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtDataCambio.Text = "" Then
            lblErro.Text = "É necessário informar a data para prosseguir!"
            divErro.Visible = True
            Exit Sub
        ElseIf txtValorCambio.Text = "" Then
            lblErro.Text = "É necessário informar o valor para prosseguir!"
            divErro.Visible = True
            Exit Sub
        Else
            If Request.QueryString("t") = "r" Then

                For Each linha As GridViewRow In dgvTaxasReceber.Rows
                    Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                    If check.Checked Then
                        Dim SPREAD As Decimal = 0
                        Dim ID_SERVICO As Integer = 0
                        Dim ID_TIPO_ESTUFAGEM As Integer = 0
                        Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                        Dim liquidacao As String = CType(linha.FindControl("lblLiquidacao"), Label).Text
                        Dim Processo As String = CType(linha.FindControl("lblProcesso"), Label).Text
                        Dim ID_BL As String = CType(linha.FindControl("lblID_BL"), Label).Text
                        Dim ID_PARCEIRO_EMPRESA As String = CType(linha.FindControl("lblID_PARCEIRO_EMPRESA"), Label).Text
                        Dim ValorCambio As Decimal = txtValorCambio.Text
                        If liquidacao = "" Then
                            Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT ID_SERVICO,ID_TIPO_ESTUFAGEM FROM TB_BL WHERE ID_BL =" & ID_BL)
                            If dsProcesso.Tables(0).Rows.Count > 0 Then
                                If Not IsDBNull(dsProcesso.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                                    ID_SERVICO = dsProcesso.Tables(0).Rows(0).Item("ID_SERVICO")
                                End If
                                If Not IsDBNull(dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                                    ID_TIPO_ESTUFAGEM = dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                                End If
                            End If




                            Dim dsAcordo As DataSet = Con.ExecutarQuery("SELECT 
ISNULL(ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL,0)ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL,
ISNULL(ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL,0)ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL,
ISNULL(ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL,0)ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL,
ISNULL(ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL,0)ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL,
ISNULL(ID_ACORDO_CAMBIO_AEREO_IMPO,0)ID_ACORDO_CAMBIO_AEREO_IMPO,
ISNULL(ID_ACORDO_CAMBIO_AEREO_EXPO,0)ID_ACORDO_CAMBIO_AEREO_EXPO,


ISNULL(SPREAD_AEREO_EXPO,0)SPREAD_AEREO_EXPO,
ISNULL(SPREAD_AEREO_IMPO,0)SPREAD_AEREO_IMPO,
ISNULL(SPREAD_MARITIMO_EXPO_FCL,0)SPREAD_MARITIMO_EXPO_FCL,
ISNULL(SPREAD_MARITIMO_EXPO_LCL,0)SPREAD_MARITIMO_EXPO_LCL,
ISNULL(SPREAD_MARITIMO_IMPO_FCL,0)SPREAD_MARITIMO_IMPO_FCL,
ISNULL(SPREAD_MARITIMO_IMPO_LCL,0)SPREAD_MARITIMO_IMPO_LCL,
ISNULL(VL_ALIQUOTA_ISS,0)VL_ALIQUOTA_ISS, ISNULL(VL_ALIQUOTA_PIS,0)VL_ALIQUOTA_PIS, ISNULL(VL_ALIQUOTA_COFINS,0)VL_ALIQUOTA_COFINS

FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ID_PARCEIRO_EMPRESA)
                            Dim ID_ACORDO As Integer = 0
                            If ID_SERVICO = 1 And ID_TIPO_ESTUFAGEM = 1 Then
                                ' AGENCIAMENTO DE IMPORTACAO MARITIMA + FCL
                                ID_ACORDO = dsAcordo.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL")
                                SPREAD = dsAcordo.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_FCL")

                            ElseIf ID_SERVICO = 1 And ID_TIPO_ESTUFAGEM = 2 Then
                                ' AGENCIAMENTO DE IMPORTACAO MARITIMA + LCL
                                ID_ACORDO = dsAcordo.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL")
                                SPREAD = dsAcordo.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_LCL")

                            ElseIf ID_SERVICO = 4 And ID_TIPO_ESTUFAGEM = 1 Then
                                ' AGENCIAMENTO DE EXPORTACAO MARITIMA + FCL
                                ID_ACORDO = dsAcordo.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL")
                                SPREAD = dsAcordo.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_FCL")

                            ElseIf ID_SERVICO = 4 And ID_TIPO_ESTUFAGEM = 2 Then
                                ' AGENCIAMENTO DE EXPORTACAO MARITIMA + LCL
                                ID_ACORDO = dsAcordo.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL")
                                SPREAD = dsAcordo.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_LCL")

                            ElseIf ID_SERVICO = 2 Then
                                'AEREO IMPO
                                ID_ACORDO = dsAcordo.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO_IMPO")
                                SPREAD = dsAcordo.Tables(0).Rows(0).Item("SPREAD_AEREO_IMPO")

                            ElseIf ID_SERVICO = 5 Then
                                'AEREO EXPO
                                ID_ACORDO = dsAcordo.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO_EXPO")
                                SPREAD = dsAcordo.Tables(0).Rows(0).Item("SPREAD_AEREO_EXPO")

                            End If


                            If ID_ACORDO = 1 Or ID_ACORDO = 3 Or ID_ACORDO = 5 Then

                                SPREAD = 0

                            End If



                            If SPREAD > 0 Then
                                If ID_ACORDO = 2 Or ID_ACORDO = 4 Or ID_ACORDO = 11 Then
                                    Dim CalculoSPREAD As Decimal = (ValorCambio / 100) * SPREAD
                                    ValorCambio = ValorCambio + CalculoSPREAD
                                End If
                            End If



                            Dim valorCambioFinal As String = ValorCambio
                            valorCambioFinal = valorCambioFinal.Replace(".", "")
                            valorCambioFinal = valorCambioFinal.Replace(",", ".")


                            Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER_ITENS] Set DT_CAMBIO = CONVERT(Date,'" & txtDataCambio.Text & "',103), VL_LANCAMENTO = VL_TAXA_CALCULADO * " & valorCambioFinal & " , VL_LIQUIDO = VL_TAXA_CALCULADO * " & valorCambioFinal & ", VL_CAMBIO = " & valorCambioFinal & " WHERE ID_MOEDA = " & ddlMoeda.SelectedValue & " AND ID_CONTA_PAGAR_RECEBER =" & ID)


                            Dim dsConsulta As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER_ITENS,VL_LIQUIDO FROM [TB_CONTA_PAGAR_RECEBER_ITENS] WHERE ID_MOEDA = " & ddlMoeda.SelectedValue & " AND ID_CONTA_PAGAR_RECEBER =" & ID)

                            If dsConsulta.Tables(0).Rows.Count > 0 Then

                                For Each linha2 As DataRow In dsConsulta.Tables(0).Rows

                                    'IMPOSTOS
                                    Dim ISS As Decimal
                                    Dim PIS As Decimal
                                    Dim COFINS As Decimal
                                    Dim valor As Decimal = linha2.Item("VL_LIQUIDO")


                                    If dsAcordo.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS").ToString() > 0 Then
                                        ISS = dsAcordo.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS").ToString()
                                    Else
                                        ISS = Session("VL_ALIQUOTA_ISS")
                                    End If
                                    ISS = (valor / 100) * ISS

                                    If dsAcordo.Tables(0).Rows(0).Item("VL_ALIQUOTA_PIS").ToString() > 0 Then
                                        PIS = dsAcordo.Tables(0).Rows(0).Item("VL_ALIQUOTA_PIS").ToString()
                                    Else
                                        PIS = Session("VL_ALIQUOTA_PIS")
                                    End If
                                    PIS = (valor / 100) * PIS


                                    If dsAcordo.Tables(0).Rows(0).Item("VL_ALIQUOTA_COFINS").ToString() > 0 Then
                                        COFINS = dsAcordo.Tables(0).Rows(0).Item("VL_ALIQUOTA_COFINS").ToString()
                                    Else
                                        COFINS = Session("VL_ALIQUOTA_COFINS")
                                    End If
                                    COFINS = (valor / 100) * COFINS


                                    Dim Desconto As Decimal = COFINS + ISS + PIS

                                    Dim desconto_final As String = Desconto.ToString
                                    desconto_final = desconto_final.Replace(".", "")
                                    desconto_final = desconto_final.Replace(",", ".")

                                    Dim COFINS_final As String = COFINS.ToString
                                    COFINS_final = COFINS_final.Replace(".", "")
                                    COFINS_final = COFINS_final.Replace(",", ".")


                                    Dim PIS_final As String = PIS.ToString
                                    PIS_final = PIS_final.Replace(".", "")
                                    PIS_final = PIS_final.Replace(",", ".")


                                    Dim ISS_final As String = ISS.ToString
                                    ISS_final = ISS_final.Replace(".", "")
                                    ISS_final = ISS_final.Replace(",", ".")


                                    Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER_ITENS] Set VL_ISS = " & ISS_final & ", VL_PIS = " & PIS_final & ", VL_COFINS = " & COFINS_final & " WHERE ID_MOEDA = " & ddlMoeda.SelectedValue & " AND ID_CONTA_PAGAR_RECEBER =" & ID & " AND ID_CONTA_PAGAR_RECEBER_ITENS =  " & linha2.Item("ID_CONTA_PAGAR_RECEBER_ITENS").ToString())
                                Next
                            End If

                            Filtro()
                            lblSuccess.Text = "Atualização de câmbio realizada com sucesso!"
                            divSuccess.Visible = True

                        Else
                            Filtro()
                            'ERRO
                            lblErro.Text &= "PROCESSO " & Processo & " JÁ LIQUIDADO! <br/>"
                            divErro.Visible = True
                        End If

                    End If
                Next



            End If


            Con.Fechar()

            txtDataCambio.Text = ""
            txtValorCambio.Text = ""
            ddlMoeda.SelectedValue = 0
            ModalPopupExtender2.Hide()

        End If
    End Sub

    Private Sub btnCSV_Click(sender As Object, e As EventArgs) Handles btnCSV.Click
        Dim sql As String = ""
        If Request.QueryString("t") = "p" Then

            If Session("CSVPagar") <> "" Then
                sql = Session("CSVPagar")

            Else
                sql = dsReceber.SelectCommand

            End If

        ElseIf Request.QueryString("t") = "r" Then
            If Session("CSVReceber") <> "" Then
                sql = Session("CSVReceber")

            Else
                sql = dsReceber.SelectCommand

            End If

        End If

        If sql <> "" Then
            Classes.Excel.exportaExcel(sql, "NVOCC", "BaixasCancelamentos")
        End If
    End Sub
End Class