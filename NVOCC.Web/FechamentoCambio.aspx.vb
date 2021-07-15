Public Class FechamentoCambio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub

    Private Sub btnPesquisa_Click(sender As Object, e As EventArgs) Handles btnPesquisa.Click
        Dim filtro As String = ""
        If rdStatus.SelectedValue = 1 Then
            filtro &= " WHERE DT_LIQUIDACAO IS NULL"
        ElseIf rdStatus.SelectedValue = 2 Then
            filtro &= " WHERE DT_LIQUIDACAO IS NOT NULL"
        End If

        If ddlFiltro.SelectedValue = 1 Then
            filtro &= " AND NR_CONTRATO LIKE '%" & txtPesquisa.Text & "%'"
        ElseIf ddlFiltro.SelectedValue = 2 Then
            filtro &= " AND DT_FECHAMENTO = CONVERT(VARCHAR,'" & txtPesquisa.Text & "',103)"

        ElseIf ddlFiltro.SelectedValue = 3 Then
            filtro &= " AND NM_AGENTE LIKE '%" & txtPesquisa.Text & "%'"
        End If



        dsFechamento.SelectCommand = "SELECT distinct ID_ACCOUNT_FECHAMENTO,NR_CONTRATO,NM_CORRETOR,NM_AGENTE,SIGLA_MOEDA,VL_CONTRATO,VL_CONTRATO_BR,CONVERT(VARCHAR,DT_FECHAMENTO,103)DT_FECHAMENTO,CONVERT(VARCHAR,DT_TAXA_CAMBIO,103)DT_TAXA_CAMBIO,VL_TAXA_CAMBIO,CONVERT(VARCHAR,DT_LIQUIDACAO,103)DT_LIQUIDACAO,CONVERT(VARCHAR,DT_CANCELAMENTO,103)DT_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO FROM [dbo].[View_Fechamento]" & filtro

        dgvFechamento.DataBind()
        divTotalInvoices.Visible = True
        dgvFechamento.Visible = True
    End Sub
    Private Sub dgvFechamento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFechamento.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        If e.CommandName = "Selecionar" Then
            If txtlinha.Text <> "" Then
                dgvFechamento.Rows(txtlinha.Text).CssClass = "Normal"

            End If
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")


            For i As Integer = 0 To dgvFechamento.Rows.Count - 1
                dgvFechamento.Rows(txtlinha.Text).CssClass = "Normal"

            Next

            dgvFechamento.Rows(txtlinha.Text).CssClass = "selected1"


            'lkBaixarFechamento.Visible = True
            'lkCancelarFechamento.Visible = True
            'lkexcluirFechamento.Visible = True


        End If

    End Sub

    Private Sub lkBaixarFechamento_Click(sender As Object, e As EventArgs) Handles lkBaixarFechamento.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione o registro que deseja baixar!"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão"
            Else


                ds = Con.ExecutarQuery("SELECT ID_ACCOUNT_FECHAMENTO,NM_CORRETOR,NM_AGENTE,NR_CONTRATO,VL_CONTRATO,SIGLA_MOEDA,VL_CONTRATO_BR,DT_LIQUIDACAO,DT_CANCELAMENTO FROM FN_ACCOUNT_FECHAMENTO(" & txtID.Text & ")")
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) Then
                        divErro.Visible = True
                        lblErro.Text = "Não foi possivel completar a ação: Registro já liquidado!"

                    ElseIf Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        divErro.Visible = True
                        lblErro.Text = "Não foi possivel completar a ação: Registro cancelado!"

                    Else
                        lblAgenteBaixa.Text = ds.Tables(0).Rows(0).Item("NM_AGENTE").ToString()
                        lblMoedaEstrangeiroBaixa.Text = ds.Tables(0).Rows(0).Item("SIGLA_MOEDA").ToString()
                        lblValorEstrangeiroBaixa.Text = ds.Tables(0).Rows(0).Item("VL_CONTRATO").ToString()
                        lblValorRealBaixa.Text = ds.Tables(0).Rows(0).Item("VL_CONTRATO_BR").ToString()
                        lblContratoBaixa.Text = ds.Tables(0).Rows(0).Item("NR_CONTRATO").ToString()
                        lblInstituicaoBaixa.Text = ds.Tables(0).Rows(0).Item("NM_CORRETOR").ToString()

                        ModalPopupExtender4.Show()
                    End If



                End If


                Con.Fechar()

            End If

        End If
    End Sub

    Private Sub lkCancelarFechamento_Click(sender As Object, e As EventArgs) Handles lkCancelarFechamento.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione o registro que deseja cancelar!"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão"
            Else


                ds = Con.ExecutarQuery("SELECT ID_ACCOUNT_FECHAMENTO,NM_CORRETOR,NM_AGENTE,NR_CONTRATO,VL_CONTRATO,SIGLA_MOEDA,VL_CONTRATO_BR FROM FN_ACCOUNT_FECHAMENTO(" & txtID.Text & ")")
                If ds.Tables(0).Rows.Count > 0 Then
                    lblAgenteCancel.Text = ds.Tables(0).Rows(0).Item("NM_AGENTE").ToString()
                    lblMoedaEstrangeiroCancel.Text = ds.Tables(0).Rows(0).Item("SIGLA_MOEDA").ToString()
                    lblValorEstrangeiroCancel.Text = ds.Tables(0).Rows(0).Item("VL_CONTRATO").ToString()
                    lblValorRealCancel.Text = ds.Tables(0).Rows(0).Item("VL_CONTRATO_BR").ToString()
                    lblContratoCancel.Text = ds.Tables(0).Rows(0).Item("NR_CONTRATO").ToString()
                    lblInstituicaoCancel.Text = ds.Tables(0).Rows(0).Item("NM_CORRETOR").ToString()
                End If
                ModalPopupExtender5.Show()

                Con.Fechar()

            End If

        End If
    End Sub

    Sub limpaFormulario()
        txtMotivoCancel.Text = ""
        lblAgenteCancel.Text = ""
        lblMoedaEstrangeiroCancel.Text = ""
        lblValorEstrangeiroCancel.Text = ""
        lblValorRealCancel.Text = ""
        lblContratoCancel.Text = ""
        lblInstituicaoCancel.Text = ""

        lblAgenteBaixa.Text = ""
        lblMoedaEstrangeiroBaixa.Text = ""
        lblValorEstrangeiroBaixa.Text = ""
        lblValorRealBaixa.Text = ""
        lblContratoBaixa.Text = ""
        lblInstituicaoBaixa.Text = ""

        ddlAgenteNovo.SelectedValue = 0
        ddlMoedaNovo.SelectedValue = 0
        ddlCorretorNovo.SelectedValue = 0
        txtContratoNovo.Text = ""
        txtDataFechamentoNovo.Text = ""
        txtTarifaNovo.Text = ""
        txtIOFNovo.Text = ""
        txtValorNovo.Text = ""
        txtCambioNovo.Text = ""
        txtDataCambioNovo.Text = ""
        txtValorBRNovo.Text = ""

        divErro.Visible = False
        divSuccess.Visible = False
    End Sub

    Private Sub btnFechaCancel_Click(sender As Object, e As EventArgs) Handles btnFechaCancel.Click
        limpaFormulario()
        ModalPopupExtender5.Hide()
    End Sub

    Private Sub btnFecharBaixa_Click(sender As Object, e As EventArgs) Handles btnFecharBaixa.Click
        limpaFormulario()
        ModalPopupExtender4.Hide()
    End Sub

    Private Sub btnSalvaCancel_Click(sender As Object, e As EventArgs) Handles btnSalvaCancel.Click
        Dim Con As New Conexao_sql
        Con.Conectar()

        divErro.Visible = False
        divSuccess.Visible = False
        If txtMotivoCancel.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Informe o motivo de cancelamento!"
        Else

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_ACCOUNT_FECHAMENTO,DT_LIQUIDACAO,DT_CANCELAMENTO FROM FN_ACCOUNT_FECHAMENTO(" & txtID.Text & ")")
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: Registro cancelado!"

                ElseIf IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) Then
                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: Registro não liquidado!"

                Else

                    'update de cancelamento
                    Con.ExecutarQuery("UPDATE TB_ACCOUNT_FECHAMENTO SET  DS_MOTIVO_CANCELAMENTO = '" & txtMotivoCancel.Text & "', DT_CANCELAMENTO= GETDATE() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & "  WHERE ID_ACCOUNT_FECHAMENTO = " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_CONTA_PAGAR_RECEBER SET DT_CANCELAMENTO= GETDATE() , DS_MOTIVO_CANCELAMENTO = '" & txtMotivoCancel.Text & "', ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER = (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_ACCOUNT_FECHAMENTO WHERE ID_ACCOUNT_FECHAMENTO = " & txtID.Text & " )")

                    Con.Fechar()
                    ModalPopupExtender5.Hide()
                    lblSuccess.Text = "Cancelamento realizado com sucesso!"
                    divSuccess.Visible = True
                    dgvFechamento.DataBind()
                End If
                limpaFormulario()

            End If

        End If
    End Sub

    Private Sub btnSalvarBaixa_Click(sender As Object, e As EventArgs) Handles btnSalvarBaixa.Click
        Dim Con As New Conexao_sql
        Con.Conectar()

        divErro.Visible = False
        divSuccess.Visible = False
        If txtDataLiquidacaoBaixa.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Informe a data de liquidação!"
        Else
            'update de liquidacao



            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (ID_CONTA_BANCARIA,
DT_LANCAMENTO,DT_VENCIMENTO,ID_USUARIO_LANCAMENTO,NR_DOCUMENTO,DS_DOCUMENTO_TRANSACAO, DT_LIQUIDACAO,ID_USUARIO_LIQUIDACAO,CD_PR,TP_EXPORTACAO ) SELECT ID_PARCEIRO_CORRETOR , GETDATE(),CONVERT(DATE,'" & txtDataLiquidacaoBaixa.Text & "',103)," & Session("ID_USUARIO") & ",NR_CONTRATO,'FECHAMENTO DE CÂMBIO',CONVERT(DATE,'" & txtDataLiquidacaoBaixa.Text & "',103)," & Session("ID_USUARIO") & ",'P','ACC' FROM TB_ACCOUNT_FECHAMENTO WHERE ID_ACCOUNT_FECHAMENTO = " & txtID.Text & "  ;  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER  ")

            Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
            ds = Con.ExecutarQuery("SELECT ID_ACCOUNT_INVOICE FROM TB_ACCOUNT_FECHAMENTO_ITENS WHERE ID_ACCOUNT_FECHAMENTO =  " & txtID.Text)
            For Each linha As DataRow In ds.Tables(0).Rows

                Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL,ID_BL_TAXA,ID_MOEDA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,DS_HISTORICO_LANCAMENTO,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,VL_TAXA_CALCULADO,TP_CLASSIFICACAO,FL_INTEGRA_PA,NR_DOCUMENTO,TP_DOCUMENTO) 
SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL,ID_BL_TAXA,
(SELECT ID_MOEDA FROM TB_ACCOUNT_INVOICE WHERE ID_ACCOUNT_INVOICE = " & linha.Item("ID_ACCOUNT_INVOICE") & "),
(SELECT DT_TAXA_CAMBIO FROM TB_ACCOUNT_FECHAMENTO WHERE ID_ACCOUNT_FECHAMENTO = " & txtID.Text & "),
(SELECT VL_TAXA_CAMBIO FROM TB_ACCOUNT_FECHAMENTO WHERE ID_ACCOUNT_FECHAMENTO = " & txtID.Text & "),VL_TAXA_BR,VL_TAXA_BR,'ACCOUNT – FECHAMENTO DE CÂMBIO ' + (SELECT NR_CONTRATO FROM TB_ACCOUNT_FECHAMENTO WHERE ID_ACCOUNT_FECHAMENTO = " & txtID.Text & "),ID_ITEM_DESPESA,(SELECT ID_PARCEIRO_AGENTE FROM TB_ACCOUNT_INVOICE WHERE ID_ACCOUNT_INVOICE =  " & linha.Item("ID_ACCOUNT_INVOICE") & "),VL_TAXA,CD_TIPO_DEVOLUCAO,1,ID_ACCOUNT_INVOICE,'INVOICE' FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE =" & linha.Item("ID_ACCOUNT_INVOICE"))

            Next

            Con.ExecutarQuery("UPDATE TB_ACCOUNT_FECHAMENTO SET DT_LIQUIDACAO = CONVERT(DATE,'" & txtDataLiquidacaoBaixa.Text & "',103), ID_USUARIO_LIQUIDACAO =" & Session("ID_USUARIO") & ",ID_CONTA_PAGAR_RECEBER = " & ID_CONTA_PAGAR_RECEBER & "  WHERE ID_ACCOUNT_FECHAMENTO = " & txtID.Text)
            Con.Fechar()
            limpaFormulario()
            ModalPopupExtender4.Hide()
            lblSuccess.Text = "Liquidação realizada com sucesso!"
            divSuccess.Visible = True
            dgvFechamento.DataBind()
        End If
    End Sub

    Private Sub btnSalvarFechamento_Click(sender As Object, e As EventArgs) Handles btnSalvarFechamento.Click
        Dim Con As New Conexao_sql
        Con.Conectar()
        divErroNovoFechamento.Visible = False
        divErro.Visible = False
        divSuccess.Visible = False
        If ddlAgenteNovo.SelectedValue = 0 Or ddlMoedaNovo.SelectedValue = 0 Or ddlCorretorNovo.SelectedValue = 0 Or txtContratoNovo.Text = "" Or
            txtDataFechamentoNovo.Text = "" Or txtTarifaNovo.Text = "" Or txtIOFNovo.Text = "" Or txtValorNovo.Text = "" Or
            txtCambioNovo.Text = "" Or txtDataCambioNovo.Text = "" Or txtValorBRNovo.Text = "" Then
            divErroNovoFechamento.Visible = True
            lblErroNovoFechamento.Text = "Preencha os campos obrigatórios!"
            ModalPopupExtender3.Show()
            Exit Sub
        ElseIf txtValorNovo.Text <> lblValorTotalInvoices.Text Then
            divErroNovoFechamento.Visible = True
            lblErroNovoFechamento.Text = "O valor do contrato informado não corresponde ao valor total de invoices selecionadas!"
            ModalPopupExtender3.Show()
            Exit Sub
        ElseIf VerificaValorReal = False Then
            divErroNovoFechamento.Visible = True
            lblErroNovoFechamento.Text = "O valor em real não corresponde ao produto de Valor x Taxa Câmbio!"
            ModalPopupExtender3.Show()
            Exit Sub
        Else

            txtTarifaNovo.Text = txtTarifaNovo.Text.Replace(".", "")
            txtTarifaNovo.Text = txtTarifaNovo.Text.Replace(",", ".")

            txtIOFNovo.Text = txtIOFNovo.Text.Replace(".", "")
            txtIOFNovo.Text = txtIOFNovo.Text.Replace(",", ".")

            txtValorBRNovo.Text = txtValorBRNovo.Text.Replace(".", "")
            txtValorBRNovo.Text = txtValorBRNovo.Text.Replace(",", ".")

            txtValorNovo.Text = txtValorNovo.Text.Replace(".", "")
            txtValorNovo.Text = txtValorNovo.Text.Replace(",", ".")

            txtCambioNovo.Text = txtCambioNovo.Text.Replace(".", "")
            txtCambioNovo.Text = txtCambioNovo.Text.Replace(",", ".")

            ' insert cabeçalho
            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_FECHAMENTO (ID_PARCEIRO_AGENTE ,ID_PARCEIRO_CORRETOR ,ID_USUARIO_LANCAMENTO
            ,ID_MOEDA ,NR_CONTRATO,DT_FECHAMENTO ,DT_APROVACAO  ,DT_TAXA_CAMBIO ,VL_TAXA_CAMBIO,VL_TARIFA_CORRETOR ,VL_IOF ,VL_CONTRATO ,VL_CONTRATO_BR ) VALUES(" & ddlAgenteNovo.SelectedValue & "," & ddlCorretorNovo.SelectedValue & "," & Session("ID_USUARIO") & "," & ddlMoedaNovo.SelectedValue & ",'" & txtContratoNovo.Text & "',CONVERT(DATE,'" & txtDataFechamentoNovo.Text & "',103),CONVERT(DATE,'" & txtDataFechamentoNovo.Text & "',103),CONVERT(DATE,'" & txtDataCambioNovo.Text & "',103)," & txtCambioNovo.Text & ", " & txtTarifaNovo.Text & "," & txtIOFNovo.Text & "," & txtValorNovo.Text & "," & txtValorBRNovo.Text & ") Select SCOPE_IDENTITY() as ID_ACCOUNT_FECHAMENTO  ")
            Dim ID_ACCOUNT_FECHAMENTO As String = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_FECHAMENTO")

            'insere itens
            For Each linha As GridViewRow In dgvInvoice.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                If check.Checked Then
                    Dim ID_ACCOUNT_INVOICE As String = CType(linha.FindControl("lblID"), Label).Text
                    Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_FECHAMENTO_ITENS (ID_ACCOUNT_FECHAMENTO,ID_ACCOUNT_INVOICE) VALUES(" & ID_ACCOUNT_FECHAMENTO & "," & ID_ACCOUNT_INVOICE & ")")




                    Dim dstaxas As DataSet = Con.ExecutarQuery("SELECT ID_ACCOUNT_INVOICE_ITENS,VL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE =" & ID_ACCOUNT_INVOICE)
                    If dstaxas.Tables(0).Rows.Count > 0 Then
                        Dim VL_TAXA_CAMBIO As Double = txtCambioNovo.Text.ToString.Replace(".", ",")
                        Dim VL_TAXA_AC As Decimal = 0
                        Dim VL_TAXA_BR_AC As Decimal = 0

                        For Each linhads As DataRow In dstaxas.Tables(0).Rows
                            Dim VL_TAXA As Decimal = linhads.Item("VL_TAXA").ToString()
                            VL_TAXA_AC = VL_TAXA_AC + VL_TAXA
                            Dim VL_TAXA_BR As Decimal = (VL_TAXA_AC * VL_TAXA_CAMBIO)
                            VL_TAXA_BR = VL_TAXA_BR - VL_TAXA_BR_AC
                            Dim VL_BR_FINAL As String = VL_TAXA_BR.ToString.Replace(",", ".")
                            Con.ExecutarQuery("UPDATE [dbo].[TB_ACCOUNT_INVOICE_ITENS] SET VL_TAXA_BR = " & VL_BR_FINAL & " WHERE ID_ACCOUNT_INVOICE_ITENS =" & linhads.Item("ID_ACCOUNT_INVOICE_ITENS").ToString())
                            VL_TAXA_BR_AC = VL_TAXA_BR_AC + VL_TAXA_BR

                            VL_TAXA_BR = txtValorBRNovo.Text - VL_TAXA_BR_AC


                        Next


                    End If
                End If
            Next

            Con.Fechar()
            limpaFormulario()
            ModalPopupExtender3.Hide()
            dgvFechamento.DataBind()

            dgvInvoice.Visible = False
            lblSuccess.Text = "Fechamento cadastrado com sucesso!"
            divSuccess.Visible = True
        End If

    End Sub

    Private Sub ddlMoedaNovo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMoedaNovo.SelectedIndexChanged
        dsInvoice.SelectCommand = "SELECT A.ID_ACCOUNT_INVOICE, F.NM_ACCOUNT_TIPO_INVOICE, 
 G.NM_ACCOUNT_TIPO_EMISSOR, A.NR_INVOICE, CONVERT(VARCHAR,A.DT_INVOICE,103)DT_INVOICE, ISNULL((SELECT SUM(ISNULL(VL_TAXA,0))FROM TB_ACCOUNT_INVOICE_ITENS B WHERE A.ID_ACCOUNT_INVOICE=B.ID_ACCOUNT_INVOICE),0)VALOR_TOTAL
FROM FN_ACCOUNT_INVOICE('" & txtVencimentoInicial.Text & "','" & txtVencimentoFinal.Text & "') A
LEFT JOIN TB_ACCOUNT_TIPO_INVOICE F ON A.ID_ACCOUNT_TIPO_INVOICE=F.ID_ACCOUNT_TIPO_INVOICE
LEFT JOIN TB_ACCOUNT_TIPO_EMISSOR G ON A.ID_ACCOUNT_TIPO_EMISSOR=G.ID_ACCOUNT_TIPO_EMISSOR
WHERE (A.DT_FECHAMENTO IS NULL) OR ( A.DT_FECHAMENTO IS NOT NULL AND DT_CANCELAMENTO_FECHAMENTO IS NOT NULL)  AND ID_MOEDA = " & ddlMoedaNovo.SelectedValue & " AND ID_PARCEIRO_AGENTE = " & ddlAgenteNovo.SelectedValue & " group by A.ID_ACCOUNT_INVOICE, F.NM_ACCOUNT_TIPO_INVOICE, 
 G.NM_ACCOUNT_TIPO_EMISSOR, A.NR_INVOICE, A.DT_INVOICE,DT_CANCELAMENTO_FECHAMENTO,DT_FECHAMENTO"
        dsInvoice.DataBind()
        dgvInvoice.DataBind()
        dgvInvoice.Visible = True
        ModalPopupExtender3.Show()
        divTotalInvoices.Visible = True
        AtualizaTotal()


    End Sub

    Sub AtualizaTotal()
        Dim Con As New Conexao_sql
        lblValorTotalInvoices.Text = 0
        For Each linha As GridViewRow In dgvInvoice.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text
            Dim valor2 As Decimal = lblValorTotalInvoices.Text

            If check.Checked Then
                lblValorTotalInvoices.Text = valor2 + valor
            End If
        Next
        ModalPopupExtender3.Show()
        Con.Fechar()
    End Sub
    Private Sub dgvInvoice_Load(sender As Object, e As EventArgs) Handles dgvInvoice.Load
        If dgvInvoice.Visible = True Then
            AtualizaTotal()
        End If

    End Sub

    Function VerificaValorReal() As Boolean
        Dim ValorCambio As Decimal = txtValorNovo.Text * txtCambioNovo.Text

        If txtValorBRNovo.Text = ValorCambio Then
            Return True
        Else
            Dim diferenca As Decimal = txtValorBRNovo.Text - ValorCambio

            If diferenca = 0.03 Then
                Return True
            Else
                If diferenca > 0.03 Then
                    Return False

                ElseIf diferenca < -0.03 Then
                    Return False
                Else
                    Return True
                End If

            End If
        End If

    End Function

    Private Sub lkExcluirFechamento_Click(sender As Object, e As EventArgs) Handles lkExcluirFechamento.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione o registro que deseja excluir!"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão"
            Else


                ds = Con.ExecutarQuery("SELECT ID_ACCOUNT_FECHAMENTO,DT_LIQUIDACAO,DT_CANCELAMENTO FROM FN_ACCOUNT_FECHAMENTO(" & txtID.Text & ")")
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) Then
                        divErro.Visible = True
                        lblErro.Text = "Não foi possivel completar a ação: Registro já liquidado!"

                    ElseIf Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        divErro.Visible = True
                        lblErro.Text = "Não foi possivel completar a ação: Registro cancelado!"

                    Else
                        'delete
                        Con.ExecutarQuery("DELETE FROM TB_ACCOUNT_FECHAMENTO WHERE ID_ACCOUNT_FECHAMENTO = " & txtID.Text)
                        Con.ExecutarQuery("DELETE FROM TB_ACCOUNT_FECHAMENTO_ITENS WHERE ID_ACCOUNT_FECHAMENTO =" & txtID.Text)

                        lblSuccess.Text = "Fechamento deletado com sucesso!"
                        divSuccess.Visible = True
                        dgvFechamento.DataBind()

                    End If



                End If

                Con.Fechar()

            End If

        End If
    End Sub

    Private Sub btnPesquisarContratos_Click(sender As Object, e As EventArgs) Handles btnPesquisarContratos.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtFechamentoInicial.Text = "" Or txtFechamentoFinal.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "É necessário informar a data de fechamento inicial e final!"
        Else
            Session("DataInicial") = ""
            Session("DataFinal") = ""

            Session("DataInicial") = txtFechamentoInicial.Text
            Session("DataFinal") = txtFechamentoFinal.Text

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ContratosFirmados()", True)
        End If

    End Sub
End Class