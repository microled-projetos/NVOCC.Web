Imports System.Net
Imports System.Runtime.Serialization
Imports Newtonsoft.Json

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
            txtData.Text = Now.Date.ToString("dd/MM/yyyy")
            lkNotasFiscais.Visible = True
            lkConsultaNotas.Visible = True
        End If
        Con.Fechar()
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        txtID.Text = ""
        txtlinha.Text = ""

        Dim filtro As String = ""

        If ddlFiltro.SelectedValue = 1 Then
            filtro &= " WHERE DT_VENCIMENTO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 2 Then
            filtro &= " WHERE NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 3 Then
            filtro &= " WHERE PARCEIRO_EMPRESA LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 4 Then
            filtro &= " WHERE REFERENCIA_CLIENTE LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 5 Then
            filtro &= " WHERE NR_NOTA_DEBITO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 6 Then
            filtro &= " WHERE NR_RPS LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 7 Then
            filtro &= " WHERE NR_NOTA_FISCAL LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 8 Then
            filtro &= " WHERE NR_RECIBO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 9 Then
            filtro &= " WHERE DT_LIQUIDACAO >= '%" & txtPesquisa.Text & "%'"

        End If

        If ckStatus.Items.FindByValue(1).Selected Then
            If filtro = "" Then
                filtro &= " WHERE DT_LIQUIDACAO IS NULL AND DT_CANCELAMENTO IS NULL"

            Else
                filtro &= " OR DT_LIQUIDACAO IS NULL AND DT_CANCELAMENTO IS NULL"

            End If


        End If
        If ckStatus.Items.FindByValue(2).Selected Then

            If filtro = "" Then
                filtro &= " WHERE DT_LIQUIDACAO IS NOT NULL AND DT_CANCELAMENTO IS NULL"

            Else
                filtro &= " OR DT_LIQUIDACAO IS NOT NULL AND DT_CANCELAMENTO IS NULL"

            End If

        End If
        If ckStatus.Items.FindByValue(3).Selected Then
            If filtro = "" Then
                filtro &= " WHERE DT_CANCELAMENTO IS NOT NULL"

            Else
                filtro &= " OR DT_CANCELAMENTO IS NOT NULL"

            End If
        End If

        dsFaturamento.SelectCommand = "SELECT * FROM [dbo].[View_Faturamento] " & filtro
        dgvFaturamento.DataBind()

        ddlFiltro.SelectedValue = 0
        txtPesquisa.Text = ""
        lkFatura.Visible = True
        lkDesmosntrativos.Visible = True
        lkRPS.Visible = True
        lkNotasFiscais.Visible = True
    End Sub

    Private Sub ddlFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltro.SelectedIndexChanged
        If ddlFiltro.SelectedValue = 1 Or ddlFiltro.SelectedValue = 9 Then
            txtPesquisa.CssClass = "form-control data"
            txtPesquisa.Text = Now.Date.AddDays(-1)
            txtPesquisa.Text = FinalSemana(txtPesquisa.Text)

        Else
            txtPesquisa.CssClass = "form-control"
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

                Dim ds As DataSet = Con.ExecutarQuery("SELECT A.DT_CANCELAMENTO,(SELECT DT_LIQUIDACAO FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER =  A.ID_CONTA_PAGAR_RECEBER)DT_LIQUIDACAO FROM TB_FATURAMENTO A WHERE A.ID_FATURAMENTO = " & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura cancelada!"
                        divErro.Visible = True
                        Exit Sub

                    ElseIf Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) Then
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura já liquidada!"
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
                Dim ds As DataSet = Con.ExecutarQuery("SELECT B.DT_LIQUIDACAO,A.NR_NOTA_FISCAL,A.DT_CANCELAMENTO FROM [TB_FATURAMENTO] A
LEFT JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER
WHERE DT_LIQUIDACAO IS NULL AND ID_FATURAMENTO =" & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura já cancelada!"
                        Exit Sub

                    ElseIf Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                        If lblContador.Text = "" Then
                            divInfo.Visible = True
                            lblmsgInfo.Text = "A NOTA FISCAL JÁ FOI GERADA.<br/>CONFIRMA O CANCELAMENTO DA FATURA ASSIM MESMO?"
                            lblContador.Text = 1
                            ModalPopupExtender3.Show()
                            btnSalvarCancelamento.Text = "Confirmar Cancelamento"
                            Exit Sub
                        Else
                            Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "' WHERE ID_FATURAMENTO =" & txtID.Text)
                            Con.Fechar()
                            lblContador.Text = ""
                            btnSalvarCancelamento.Text = "Salvar"
                            divInfo.Visible = False
                            divSuccess.Visible = True
                            dgvFaturamento.DataBind()
                            lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"
                        End If

                    Else
                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "' WHERE ID_FATURAMENTO =" & txtID.Text)
                        Con.Fechar()
                        lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"
                        divSuccess.Visible = True
                        txtData.Text = ""
                        txtObs.Text = ""
                        dgvFaturamento.DataBind()
                        divInfo.Visible = False
                    End If
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura já liquidada!"
                End If
            End If
        End If

    End Sub

    Private Sub dgvFaturamento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFaturamento.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        divErroSubstituir.Visible = False
        divInfo.Visible = False

        If e.CommandName = "Selecionar" Then
            If txtlinha.Text <> "" Then
                dgvFaturamento.Rows(txtlinha.Text).CssClass = "Normal"

            End If
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")


            For i As Integer = 0 To dgvFaturamento.Rows.Count - 1
                dgvFaturamento.Rows(txtlinha.Text).CssClass = "Normal"

            Next

            dgvFaturamento.Rows(txtlinha.Text).CssClass = "selected1"

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER,NR_PROCESSO,PARCEIRO_EMPRESA,CONVERT(VARCHAR,DT_NOTA_FISCAL,103)DT_NOTA_FISCAL,NR_NOTA_FISCAL,VL_NOTA_DEBITO FROM View_Faturamento WHERE ID_FATURAMENTO =" & txtID.Text)
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
                    lblClienteSubs.Text = "CLIENTE: " & ds.Tables(0).Rows(0).Item("PARCEIRO_EMPRESA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                    lblNumeroNota.Text = ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")
                    lkNotasFiscais.Visible = True
                Else
                    lkNotasFiscais.Visible = False
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_NOTA_FISCAL")) Then
                    lblDataEmissao.Text = ds.Tables(0).Rows(0).Item("DT_NOTA_FISCAL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")) Then
                    Session("ID_CONTA_PAGAR_RECEBER") = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_NOTA_DEBITO")) Then
                    Dim ValorExtenso As New ValorExtenso
                    Session("ValorExtenso") = ValorExtenso.NumeroToExtenso(ds.Tables(0).Rows(0).Item("VL_NOTA_DEBITO"))
                    Dim VL As String = ds.Tables(0).Rows(0).Item("VL_NOTA_DEBITO").ToString
                    VL = VL.Replace(".", "")
                    VL = VL.Replace(",", ".")

                    Session("Valor") = VL
                End If


            End If
            Con.Fechar()

        End If
    End Sub

    Private Sub lkNotaDebito_Click(sender As Object, e As EventArgs) Handles lkNotaDebito.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else

            Response.Redirect("EmissaoNDFaturamento.aspx?id=" & txtID.Text)
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

            Dim ds As DataSet = Con.ExecutarQuery("SELECT B.DT_LIQUIDACAO FROM [TB_FATURAMENTO] A
LEFT JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER
WHERE DT_LIQUIDACAO IS NOT NULL AND ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                'Response.Redirect("ReciboPagamento.aspx?id=" & txtID.Text)
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

            Dim ds As DataSet = Con.ExecutarQuery("SELECT DT_RPS FROM [TB_FATURAMENTO] WHERE DT_RPS IS NOT NULL AND ID_FATURAMENTO = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                ' Response.Redirect("ReciboProvisorioServico.aspx?id=" & txtID.Text)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "FuncImprimirRPS()", True)
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
            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_DEBITO,NR_RPS,DT_RPS FROM [TB_FATURAMENTO]
WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_DEBITO")) Then
                    If IsDBNull(ds.Tables(0).Rows(0).Item("NR_RPS")) And IsDBNull(ds.Tables(0).Rows(0).Item("DT_RPS")) Then

                        Dim dsRPS As DataSet = Con.ExecutarQuery("SELECT NM_RAZAO,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CEP,(SELECT NM_CIDADE FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE)CIDADE,(SELECT NM_ESTADO FROM TB_ESTADO WHERE ID_ESTADO = (SELECT ID_ESTADO FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE))ESTADO,VL_ALIQUOTA_ISS FROM TB_PARCEIRO A
WHERE ID_PARCEIRO = (SELECT TOP 1 ID_PARCEIRO_EMPRESA FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER= " & Session("ID_CONTA_PAGAR_RECEBER") & ")")
                        If dsRPS.Tables(0).Rows.Count > 0 Then


                            Dim NumeracaoDoc As New NumeracaoDoc
                            Dim numero As String = NumeracaoDoc.Numerar(3)
                            Dim VL_ISS As String = dsRPS.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS").ToString
                            VL_ISS = VL_ISS.Replace(".", "")
                            VL_ISS = VL_ISS.Replace(",", ".")

                            Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET STATUS_NFE = 0,DT_RPS = getdate(), NR_RPS = '" & numero & "', NM_CLIENTE = '" & dsRPS.Tables(0).Rows(0).Item("NM_RAZAO").ToString & "',CNPJ = '" & dsRPS.Tables(0).Rows(0).Item("CNPJ").ToString & "',INSCR_ESTADUAL ='" & dsRPS.Tables(0).Rows(0).Item("INSCR_ESTADUAL").ToString & "',INSCR_MUNICIPAL ='" & dsRPS.Tables(0).Rows(0).Item("INSCR_MUNICIPAL").ToString & "',ENDERECO='" & dsRPS.Tables(0).Rows(0).Item("ENDERECO").ToString & "',NR_ENDERECO='" & dsRPS.Tables(0).Rows(0).Item("NR_ENDERECO").ToString & "',COMPL_ENDERECO='" & dsRPS.Tables(0).Rows(0).Item("COMPL_ENDERECO").ToString & "',BAIRRO='" & dsRPS.Tables(0).Rows(0).Item("BAIRRO").ToString & "',CEP ='" & dsRPS.Tables(0).Rows(0).Item("CEP").ToString & "',CIDADE ='" & dsRPS.Tables(0).Rows(0).Item("CIDADE").ToString & "',ESTADO ='" & dsRPS.Tables(0).Rows(0).Item("ESTADO").ToString & "',VL_ISS =" & VL_ISS & ",VL_NOTA = " & Session("Valor") & ",VL_NOTA_EXTENSO = '" & Session("ValorExtenso") & "' WHERE ID_FATURAMENTO =" & txtID.Text)

                            Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RPS = '" & numero & "' WHERE ID_NUMERACAO = 5")



                            'Using GeraRps = New NotaFiscal.WsNvocc

                            '    Dim consulta = GeraRps.IntegraNFePrefeitura(numero, 1, "SQL", "NVOCC", 0)

                            'End Using


                            divSuccess.Visible = True
                            lblmsgSuccess.Text = "RPS gerada com sucesso!"
                            dgvFaturamento.DataBind()

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

                        Dim RPSCancelada As String = ds.Tables(0).Rows(0).Item("NR_RPS")
                        Dim NumeracaoDoc As New NumeracaoDoc
                        Dim numero As String = NumeracaoDoc.Numerar(3)


                        Con.ExecutarQuery("INSERT INTO TB_FATURAMENTO (ID_CONTA_PAGAR_RECEBER,DT_CANCELAMENTO,ID_USUARIO_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO,NR_NOTA_DEBITO,NR_RPS,NR_NOTA_FISCAL,NR_RECIBO,DT_NOTA_DEBITO,DT_RPS,DT_NOTA_FISCAL,DT_RECIBO,FL_RPS,FL_NOTA_SUBSTITUTA,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,STATUS_NFE) SELECT ID_CONTA_PAGAR_RECEBER,DT_CANCELAMENTO,ID_USUARIO_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO,NR_NOTA_DEBITO,'" & numero & "',NR_NOTA_FISCAL,NR_RECIBO,DT_NOTA_DEBITO,Getdate(),DT_NOTA_FISCAL,DT_RECIBO,FL_RPS,FL_NOTA_SUBSTITUTA,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,3 FROM TB_FATURAMENTO WHERE ID_FATURAMENTO =" & txtID.Text)
                        Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RPS = '" & numero & "' WHERE ID_NUMERACAO = 5")
                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_CANCELAMENTO = getdate(), ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = 'CANCELAMENTO DA NOTA FISCAL' WHERE ID_FATURAMENTO =" & txtID.Text)



                        'Using GeraRps = New NotaFiscal.WsNvocc

                        '    Dim consulta = GeraRps.CancelaNFePrefeitura(RPSCancelada, 1, "SQL", "NVOCC")

                        'End Using


                        lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"
                        divSuccess.Visible = True
                        dgvFaturamento.DataBind()
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

        ElseIf txtNovoNumeroNota.Text = "" Then
            divErroSubstituir.Visible = True
            lblErroSubstituir.Text = "É necessário informar um novo número de nota para concluir a substituição!"
            ModalPopupExtender5.Show()
            Exit Sub
        ElseIf txtNovaEmissaoNota.Text = "" Then
            divErroSubstituir.Visible = True
            lblErroSubstituir.Text = "É necessário informar uma nova data de emissao de nota para concluir a substituição!"
            ModalPopupExtender5.Show()
            Exit Sub
        ElseIf validar.ValidaData(txtNovaEmissaoNota.Text) = False Then
            divErroSubstituir.Visible = True
            lblErroSubstituir.Text = "Nova data de emissão inválida!"
            ModalPopupExtender5.Show()
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_FISCAL FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                    If PrazosSubstituicao() = False Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Nota selecionada não pode ser substituída!"
                        Exit Sub

                    Else
                        Con.ExecutarQuery("INSERT INTO TB_FATURAMENTO (ID_CONTA_PAGAR_RECEBER,DT_CANCELAMENTO,ID_USUARIO_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO,NR_NOTA_DEBITO,NR_RPS,NR_NOTA_FISCAL,NR_RECIBO,DT_NOTA_DEBITO,DT_RPS,DT_NOTA_FISCAL,DT_RECIBO,FL_RPS,FL_NOTA_SUBSTITUTA,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,STATUS_NFE) SELECT ID_CONTA_PAGAR_RECEBER,DT_CANCELAMENTO,ID_USUARIO_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO,NR_NOTA_DEBITO,NR_RPS,'" & txtNovoNumeroNota.Text & "',NR_RECIBO,DT_NOTA_DEBITO,DT_RPS,CONVERT(DATE,'" & txtNovaEmissaoNota.Text & "',103),DT_RECIBO,FL_RPS,1,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,2 FROM TB_FATURAMENTO WHERE ID_FATURAMENTO =" & txtID.Text)

                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_CANCELAMENTO = getdate(), ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = 'SUBSTITUIÇÃO DA NOTA FISCAL' WHERE ID_FATURAMENTO =" & txtID.Text)
                        lblmsgSuccess.Text = "Substituição realizada com sucesso!"
                        divSuccess.Visible = True
                        txtNovoNumeroNota.Text = ""
                        txtNovaEmissaoNota.Text = ""
                        divErroSubstituir.Visible = False
                        dgvFaturamento.DataBind()
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

    Private Sub lkVisualizarNota_Click(sender As Object, e As EventArgs) Handles lkVisualizarNota.Click
        If Not String.IsNullOrEmpty(txtID.Text) Then

            'Using GeraRps = New NotaFiscal.WsNvocc

            '    Dim consulta = GeraRps.ConsultaNFePrefeitura(txtID.Text, 1, "SQL", "NVOCC")

            'End Using
        Else
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro!"
        End If
    End Sub

    Private Sub dgvFaturamento_Load(sender As Object, e As EventArgs) Handles dgvFaturamento.Load
        Dim Con As New Conexao_sql
        Dim contador As Integer = 0
        Dim IDs As String = ""

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
        If contador > 0 Then
            lkBoleto.Visible = True
        Else
            lkBoleto.Visible = False
        End If

        If contador = 1 Then
            'lkFatura.Visible = True
            'lkDesmosntrativos.Visible = True
            'lkRPS.Visible = True
            'lkNotasFiscais.Visible = True
            txtID.Text = IDs
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER,NR_PROCESSO,PARCEIRO_EMPRESA,CONVERT(VARCHAR,DT_NOTA_FISCAL,103)DT_NOTA_FISCAL,NR_NOTA_FISCAL,VL_NOTA_DEBITO FROM View_Faturamento WHERE ID_FATURAMENTO =" & txtID.Text)
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
                    lblClienteSubs.Text = "CLIENTE: " & ds.Tables(0).Rows(0).Item("PARCEIRO_EMPRESA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                    lblNumeroNota.Text = ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")
                    lkNotasFiscais.Visible = True
                Else
                    lkNotasFiscais.Visible = False
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_NOTA_FISCAL")) Then
                    lblDataEmissao.Text = ds.Tables(0).Rows(0).Item("DT_NOTA_FISCAL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")) Then
                    Session("ID_CONTA_PAGAR_RECEBER") = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_NOTA_DEBITO")) Then
                    Dim ValorExtenso As New ValorExtenso
                    Session("ValorExtenso") = ValorExtenso.NumeroToExtenso(ds.Tables(0).Rows(0).Item("VL_NOTA_DEBITO"))
                    Dim VL As String = ds.Tables(0).Rows(0).Item("VL_NOTA_DEBITO").ToString
                    VL = VL.Replace(".", "")
                    VL = VL.Replace(",", ".")

                    Session("Valor") = VL
                End If


            End If
            Con.Fechar()
        Else
            'lkFatura.Visible = False
            'lkDesmosntrativos.Visible = False
            'lkRPS.Visible = False
            'lkNotasFiscais.Visible = False
        End If

    End Sub

    Private Sub btnImprimirBoleto_Click(sender As Object, e As EventArgs) Handles btnImprimirBoleto.Click
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Dim contador As Integer = 0
        Dim IDs As String = ""
        For Each linha As GridViewRow In dgvFaturamento.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text

            Dim check As CheckBox = linha.FindControl("ckSelecionar")
            If check.Checked Then
                contador = 1
                If IDs = "" Then
                    IDs &= ID
                Else
                    IDs &= "," & ID

                End If
            End If
        Next
        If contador = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro!"
        ElseIf ddlBanco.SelectedValue = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "É necessario selecionar um banco!"
        Else
            Call jsonBoleto(IDs)

        End If

    End Sub

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

    Sub jsonBoleto(IDs As String)
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_RPS FROM TB_FATURAMENTO 
WHERE ID_FATURAMENTO IN (" & IDs & ")")
        If ds.Tables(0).Rows.Count > 0 Then
            Dim boleto As New Boleto()
            boleto.Banco = ddlBanco.SelectedValue
            boleto.Nr_rps = New List(Of String)

            For Each linhads As DataRow In ds.Tables(0).Rows
                'boleto.Nr_rps = New List(Of String) From {"000001","000002"}
                boleto.Nr_rps.Add(linhads.Item("NR_RPS").ToString())
            Next
            JsonConvert.SerializeObject(boleto)

        End If
    End Sub

    Private Sub btnConsultaNotas_Click(sender As Object, e As EventArgs) Handles btnConsultaNotas.Click
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
                filtro &= " AND DT_VENCIMENTO BETWEEN CONVERT(DATE,'" & txtConsultaVencimentoInicio.Text & "',103) AND CONVERT(DATE,'" & txtConsultaVencimentoFim.Text & "',103) "

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
                filtro &= " AND DT_LIQUIDACAO BETWEEN CONVERT(DATE,'" & txtConsultaPagamentoInicio.Text & "',103) AND CONVERT(DATE,'" & txtConsultaPagamentoFim.Text & "',103) "

            Else
                'msg erro
                divErroConsultasNotas.Visible = True
                lblErroConsultasNotas.Text = "É necessário informar data de inicio e fim para prosseguir com a consulta!"
                ModalPopupExtender9.Show()
                Exit Sub
            End If
        End If

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

    End Sub
End Class