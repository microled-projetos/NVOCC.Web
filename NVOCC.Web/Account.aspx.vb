Public Class Account
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
        Else
            If Not Page.IsPostBack Then
                Dim primeirodia As Date
                ' If Month(Now.AddMonths(-1)) <= 9 Then
                If Month(Now.Date) <= 9 Then
                    primeirodia = "01/0" & Month(Now.Date) & "/" & Year(Now.Date)
                Else
                    primeirodia = "01/" & Month(Now.Date) & "/" & Year(Now.Date)
                End If
                txtVencimentoInicial.Text = primeirodia

                Dim ultimodia As Date = DateAdd("m", 1, DateSerial(Year(Now.Date), Month(Now.Date), 1))
                ultimodia = DateAdd("d", -1, ultimodia)
                txtVencimentoFinal.Text = ultimodia
            End If
        End If

        Con.Fechar()
    End Sub

    Private Sub lkAlterarInvoice_Click(sender As Object, e As EventArgs) Handles lkAlterarInvoice.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione o registro que deseja editar!"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão"
            Else

                'realiza select e preenche os campos do painel de inserção              

                ds = Con.ExecutarQuery("SELECT B.ID_BL,B.ID_PROFIT_DIVISAO,A.ID_ACCOUNT_INVOICE,A.NR_INVOICE,A.ID_ACCOUNT_TIPO_EMISSOR,A.ID_ACCOUNT_TIPO_FATURA,A.DT_INVOICE,B.NR_PROCESSO,B.NR_BL,GRAU,B.ID_STATUS_FRETE_AGENTE,A.ID_PARCEIRO_AGENTE,FL_CONFERIDO,A.ID_ACCOUNT_TIPO_INVOICE,ISNULL(A.ID_MOEDA,0)ID_MOEDA,A.DT_FECHAMENTO,A.DT_VENCIMENTO,A.DS_OBSERVACAO,(SELECT SUM(ISNULL(VL_TAXA,0)) FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE = A.ID_ACCOUNT_INVOICE)VALOR_TOTAL FROM (SELECT * FROM FN_ACCOUNT_INVOICE('" & txtVencimentoInicial.Text & "','" & txtVencimentoFinal.Text & "')) AS A INNER JOIN TB_BL B ON B.ID_BL = A.ID_BL_INVOICE WHERE A.ID_ACCOUNT_INVOICE = " & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtIDInvoice.Text = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_INVOICE").ToString()
                    txtID_BL.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                    txtGrau.Text = ds.Tables(0).Rows(0).Item("GRAU").ToString()
                    txtIDPARCEIROAGENTE.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE").ToString()
                    dsAgente.DataBind()
                    ddlAgente.DataBind()
                    ddlAgente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE").ToString()
                    ddlEmissor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_EMISSOR").ToString()
                    ddlTipoInvoice.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE").ToString()
                    If ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE").ToString() = 1 Then
                        txtProc_ou_BL.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                    ElseIf ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE").ToString() = 2 Then
                        txtProc_ou_BL.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO").ToString()
                    End If
                    txtVencimento.Text = ds.Tables(0).Rows(0).Item("DT_VENCIMENTO").ToString()
                    ddlMoeda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA").ToString()
                    ddlTipoFatura.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_FATURA").ToString()
                    txtNumeroInvoice.Text = ds.Tables(0).Rows(0).Item("NR_INVOICE").ToString()
                    txtDataInvoice.Text = ds.Tables(0).Rows(0).Item("DT_INVOICE").ToString()
                    txtObsInvoice.Text = ds.Tables(0).Rows(0).Item("DS_OBSERVACAO").ToString()
                    ckbConferido.Checked = ds.Tables(0).Rows(0).Item("FL_CONFERIDO")
                    ddlTipoDevolucao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")
                    ddlDivisaoProfit.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO")

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_FECHAMENTO")) Then
                        'btnSalvarNovaInvoice.Visible = False
                        btnComissoes.Visible = False
                        btnOutrasTaxas.Visible = False
                        btnTaxasExteriorDeclaradas.Visible = False
                        btnDevolucaoFrete.Visible = False
                        dgvItensInvoice.Columns(5).Visible = False
                        btnGravarCabecalho.Visible = False
                    Else
                        ' btnSalvarNovaInvoice.Visible = True
                        btnComissoes.Visible = True
                        btnOutrasTaxas.Visible = True
                        btnTaxasExteriorDeclaradas.Visible = True
                        btnDevolucaoFrete.Visible = True
                        dgvItensInvoice.Columns(5).Visible = True
                        btnGravarCabecalho.Visible = True
                    End If
                End If
                ModalPopupExtender2.Show()
                ddlEmissor.Enabled = False
                ddlTipoInvoice.Enabled = False
                ddlTipoFatura.Enabled = False

                Con.Fechar()
                atualizaTotalInvoice()
            End If

        End If
    End Sub

    Private Sub btnGravarCabecalho_Click(sender As Object, e As EventArgs) Handles btnGravarCabecalho.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divSuccessInvoice.Visible = False
        divErroInvoice.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        If txtIDInvoice.Text = "" Then
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão."
            Else
                Dim numero As Integer
                Dim numeroFinal As String = ""
                Dim Invoice As String = ""
                If ddlEmissor.SelectedValue = 2 Then
                    '  ds = Con.ExecutarQuery("SELECT isnull(NR_INVOICE,0)NR_INVOICE FROM TB_NUMERACAO")
                    ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Invoice NR_INVOICE")
                    If ds.Tables(0).Rows.Count > 0 Then

                        numero = ds.Tables(0).Rows(0).Item("NR_INVOICE")
                        numeroFinal = numero.ToString.PadLeft(6, "0")
                        Invoice = numeroFinal
                        Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_INVOICE = '" & Invoice & "' ")
                    End If
                    txtNumeroInvoice.Text = Invoice

                End If

                If ddlEmissor.SelectedValue = 0 Or ddlAgente.SelectedValue = 0 Or ddlTipoInvoice.SelectedValue = 0 Or txtProc_ou_BL.Text = "" Or txtVencimento.Text = "" Or ddlMoeda.SelectedValue = 0 Or ddlTipoFatura.SelectedValue = 0 Or txtDataInvoice.Text = "" Or txtNumeroInvoice.Text = "" Then
                    divErroInvoice.Visible = True
                    lblErroInvoice.Text = "Preencha todos os campos obrigatórios."
                    ModalPopupExtender2.Show()
                Else
                    'insert
                    ds = Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE (ID_PARCEIRO_AGENTE,ID_ACCOUNT_TIPO_INVOICE,ID_ACCOUNT_TIPO_EMISSOR,ID_ACCOUNT_TIPO_FATURA,ID_BL,ID_MOEDA,NR_INVOICE,DT_INVOICE,DT_VENCIMENTO,FL_CONFERIDO,DS_OBSERVACAO,ID_USUARIO_LANCAMENTO) VALUES (" & ddlAgente.SelectedValue & "," & ddlTipoInvoice.SelectedValue & ", " & ddlEmissor.SelectedValue & ", " & ddlTipoFatura.SelectedValue & ", " & txtID_BL.Text & " ," & ddlMoeda.SelectedValue & ",'" & txtNumeroInvoice.Text & "', CONVERT(DATE,'" & txtDataInvoice.Text & "',103),CONVERT(DATE,'" & txtVencimento.Text & "',103),'" & ckbConferido.Checked & "','" & txtObsInvoice.Text & "', " & Session("ID_USUARIO") & ") ; Select SCOPE_IDENTITY() as ID_ACCOUNT_INVOICE ")
                    txtIDInvoice.Text = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_INVOICE")
                    lblSuccessInvoice.Text = "Registro cadastrado com sucesso!"
                    divSuccessInvoice.Visible = True


                    ModalPopupExtender2.Show()
                End If


            End If
        Else
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão"
            Else

                'update 
                Con.ExecutarQuery("UPDATE TB_ACCOUNT_INVOICE SET ID_PARCEIRO_AGENTE = " & ddlAgente.SelectedValue & ",ID_ACCOUNT_TIPO_INVOICE =" & ddlTipoInvoice.SelectedValue & ",ID_ACCOUNT_TIPO_EMISSOR = " & ddlEmissor.SelectedValue & ",ID_ACCOUNT_TIPO_FATURA = " & ddlTipoFatura.SelectedValue & ",ID_BL = " & txtID_BL.Text & ",ID_MOEDA = " & ddlMoeda.SelectedValue & ",NR_INVOICE = '" & txtNumeroInvoice.Text & "',DT_INVOICE = CONVERT(DATE,'" & txtDataInvoice.Text & "',103),DT_VENCIMENTO = CONVERT(DATE,'" & txtVencimento.Text & "',103),FL_CONFERIDO = '" & ckbConferido.Checked & "',DS_OBSERVACAO = '" & txtObsInvoice.Text & "',ID_USUARIO_ALTERACAO = " & Session("ID_USUARIO") & " WHERE ID_ACCOUNT_INVOICE = " & txtIDInvoice.Text)
                lblSuccess.Text = "Registro alterado com sucesso!"
                divSuccess.Visible = True
                ModalPopupExtender2.Show()
            End If

        End If
    End Sub

    Private Sub lkExcluirInvoice_Click(sender As Object, e As EventArgs) Handles lkExcluirInvoice.Click
        divErro.Visible = False
        divSuccess.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione o registro que deseja excluir!"
        Else
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão"
            Else
                ds = Con.ExecutarQuery("SELECT COUNT(ID_ACCOUNT_INVOICE)QTD FROM TB_ACCOUNT_INVOICE WHERE ID_ACCOUNT_INVOICE = " & txtID.Text & " And ID_ACCOUNT_INVOICE NOT IN (SELECT ID_ACCOUNT_INVOICE FROM TB_ACCOUNT_FECHAMENTO_ITENS FI INNER JOIN TB_ACCOUNT_FECHAMENTO F ON F.ID_ACCOUNT_FECHAMENTO = FI.ID_ACCOUNT_FECHAMENTO  WHERE DT_CANCELAMENTO IS NULL)")
                'delete
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação:Invoice inclusa em Fechamento!"
                Else
                    Con.ExecutarQuery("DELETE FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE = " & txtID.Text)
                    Con.ExecutarQuery("DELETE FROM TB_ACCOUNT_INVOICE WHERE ID_ACCOUNT_INVOICE = " & txtID.Text)
                    lblSuccess.Text = "Registro deletado com sucesso!"
                    divSuccess.Visible = True

                End If

            End If
        End If
        RegistrosGrid()
    End Sub

    Private Sub btnPesquisa_Click(sender As Object, e As EventArgs) Handles btnPesquisa.Click
        RegistrosGrid()
        txtVencimentoInicialSOA.Text = txtVencimentoInicial.Text
        txtVencimentoFinalSOA.Text = txtVencimentoFinal.Text

        txtEmbarqueInicial.Text = txtVencimentoInicial.Text
        txtEmbarqueFinal.Text = txtVencimentoFinal.Text

    End Sub

    Function VerificaPositivoNegativo() As String
        If ddlEmissor.SelectedValue = 1 And ddlTipoFatura.SelectedValue = 2 Then
            'Quando o Agente for o emissor e o Tipo de Fatura for Credit Note os valores serão gravados negativos.
            Return "-"
        ElseIf ddlEmissor.SelectedValue = 2 And ddlTipoFatura.SelectedValue = 1 Then
            ' Quando o emissor for a FCA e o Tipo de Fatura for de Debit Note os valores serão gravados negativos.
            Return "-"
        Else
            Return "+"
        End If
    End Function

    Sub limpaFormulario()
        txtID.Text = ""
        txtIDInvoice.Text = ""
        txtID_BL.Text = ""
        txtGrau.Text = ""
        txtIDPARCEIROAGENTE.Text = 0
        ddlAgente.SelectedValue = 0
        ddlEmissor.SelectedValue = 0
        ddlTipoInvoice.SelectedValue = 0
        txtProc_ou_BL.Text = ""
        txtVencimento.Text = ""
        ddlMoeda.SelectedValue = 0
        ddlTipoFatura.SelectedValue = 0
        txtNumeroInvoice.Text = ""
        txtDataInvoice.Text = ""
        txtObsInvoice.Text = ""
        lblTotalInvoice.Text = 0
        ddlEmissor.Enabled = True
        ddlTipoInvoice.Enabled = True
        ddlTipoFatura.Enabled = True
        ckbConferido.Checked = True
        divErroInvoice.Visible = False
        divSuccessInvoice.Visible = False
    End Sub
    Private Sub dgvInvoice_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvInvoice.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        If e.CommandName = "Selecionar" Then

            For i As Integer = 0 To dgvInvoice.Rows.Count - 1
                dgvInvoice.Rows(i).CssClass = "Normal"

            Next
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")


            dgvInvoice.Rows(txtlinha.Text).CssClass = "selected1"


        End If

    End Sub

    Private Sub btnFecharNovaInvoice_Click(sender As Object, e As EventArgs) Handles btnFecharNovaInvoice.Click
        limpaFormulario()
        RegistrosGrid()
        ModalPopupExtender2.Hide()
    End Sub

    Sub RegistrosGrid()


        divErro.Visible = False

        If txtVencimentoInicial.Text = "" Or txtVencimentoFinal.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "É necessário informar vencimento incial e final para concluir a pesquisa"
            dgvInvoice.Visible = False

        Else

            Dim filtro As String = ""
            If rdStatus.SelectedValue = 1 Then
                filtro &= " WHERE A.FL_CONFERIDO = 1"
            ElseIf rdStatus.SelectedValue = 0 Then
                filtro &= " WHERE A.FL_CONFERIDO = 0"
            End If

            If ddlFiltro.SelectedValue = 1 Then
                filtro = " AND A.NR_INVOICE LIKE '%" & txtPesquisa.Text & "%'"

            ElseIf ddlFiltro.SelectedValue = 2 Then
                filtro = " AND B.NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 3 Then
                filtro = " AND B.NR_BL LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                filtro = " AND A.NM_AGENTE LIKE '%" & txtPesquisa.Text & "%'"
            ElseIf ddlFiltro.SelectedValue = 5 Then
                filtro = " AND A.NM_ACCOUNT_TIPO_EMISSOR LIKE '%" & txtPesquisa.Text & "%'"
            End If



            dsInvoice.SelectCommand = "SELECT A.ID_ACCOUNT_INVOICE,A.NR_INVOICE,A.NM_ACCOUNT_TIPO_EMISSOR,A.NM_ACCOUNT_TIPO_FATURA,CONVERT(VARCHAR,A.DT_VENCIMENTO,103)DT_VENCIMENTO,CONVERT(VARCHAR,A.DT_INVOICE,103)DT_INVOICE,
case when A.ID_ACCOUNT_TIPO_INVOICE = 2 then
B.NR_PROCESSO else '' end NR_PROCESSO,B.NR_BL,SUBSTRING(A.NM_AGENTE,0,15)NM_AGENTE,A.ID_PARCEIRO_AGENTE,FL_CONFERIDO,A.NM_ACCOUNT_TIPO_INVOICE,A.SIGLA_MOEDA,CONVERT(VARCHAR,A.DT_FECHAMENTO,103)DT_FECHAMENTO,SUBSTRING(A.DS_OBSERVACAO,0,50)DS_OBSERVACAO,(SELECT SUM(ISNULL(VL_TAXA,0)) FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE = A.ID_ACCOUNT_INVOICE)VALOR_TOTAL FROM (SELECT * FROM FN_ACCOUNT_INVOICE('" & txtVencimentoInicial.Text & "','" & txtVencimentoFinal.Text & "')) AS A 
INNER JOIN TB_BL B ON B.ID_BL = A.ID_BL_INVOICE " & filtro & " group by  A.ID_ACCOUNT_INVOICE,A.NR_INVOICE,A.NM_ACCOUNT_TIPO_EMISSOR,A.NM_ACCOUNT_TIPO_FATURA,A.DT_INVOICE,B.NR_PROCESSO,B.NR_BL,A.NM_AGENTE,FL_CONFERIDO,A.NM_ACCOUNT_TIPO_INVOICE,A.SIGLA_MOEDA,A.DT_FECHAMENTO,A.DS_OBSERVACAO,A.DT_VENCIMENTO,A.ID_ACCOUNT_TIPO_INVOICE,A.ID_PARCEIRO_AGENTE"

            dgvInvoice.DataBind()
            dgvInvoice.Visible = True
        End If

    End Sub


    Private Sub txtProc_ou_BL_TextChanged(sender As Object, e As EventArgs) Handles txtProc_ou_BL.TextChanged
        divErroInvoice.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim grau As String

        If ddlTipoInvoice.SelectedValue = 1 Then
            grau = "M"
        ElseIf ddlTipoInvoice.SelectedValue = 2 Then
            grau = "C"
        End If


        ds = Con.ExecutarQuery("SELECT ID_BL,GRAU,ISNULL(ID_STATUS_FRETE_AGENTE,0)ID_STATUS_FRETE_AGENTE,ISNULL(ID_PROFIT_DIVISAO,0)ID_PROFIT_DIVISAO FROM TB_BL WHERE GRAU = '" & grau & "' AND ((NR_PROCESSO = '" & txtProc_ou_BL.Text & "' AND ID_BL_MASTER IS NOT NULL) OR (NR_BL = '" & txtProc_ou_BL.Text & "'))")
        If ds.Tables(0).Rows.Count > 0 Then
            txtID_BL.Text = ds.Tables(0).Rows(0).Item("ID_BL")
            txtGrau.Text = ds.Tables(0).Rows(0).Item("GRAU")
            ddlTipoDevolucao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")
            ddlDivisaoProfit.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO")
            btnGravarCabecalho.Enabled = True
        Else
            divErroInvoice.Visible = True
            lblErroInvoice.Text = "PROCESSO/BL NÃO ENCONTRADO"
            btnGravarCabecalho.Enabled = False
        End If
        ModalPopupExtender2.Show()
    End Sub

    Private Sub ddlEmissor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmissor.SelectedIndexChanged
        If ddlEmissor.SelectedValue = 1 Then
            txtNumeroInvoice.Enabled = True
        ElseIf ddlEmissor.SelectedValue = 2 Then
            txtNumeroInvoice.Enabled = False
        End If
        ModalPopupExtender2.Show()
    End Sub

    Private Sub lkNovaInvoice_Click(sender As Object, e As EventArgs) Handles lkNovaInvoice.Click
        limpaFormulario()
        ModalPopupExtender2.Show()
    End Sub


    Private Sub btnDevolucaoFrete_Click(sender As Object, e As EventArgs) Handles btnDevolucaoFrete.Click
        divSuccessInvoice.Visible = False
        divErroInvoice.Visible = False

        dsDevolucao.SelectCommand = "SELECT ID_MOEDA,ID_BL_TAXA,ID_BL,NR_PROCESSO,NM_ITEM_DESPESA,SIGLA_MOEDA,ISNULL(VL_COMPRA,0)VL_COMPRA,ISNULL(VL_VENDA,0)VL_VENDA,ISNULL(DIFERENCA,0)DIFERENCA,DT_RECEBIMENTO FROM FN_ACCOUNT_DEVOLUCAO_FRETE (" & txtID_BL.Text & ", '" & txtGrau.Text & "') A WHERE ID_MOEDA =" & ddlMoeda.SelectedValue & " AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS  WHERE ID_BL_TAXA IS NOT NULL) AND ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS D
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = D.ID_CONTA_PAGAR_RECEBER 
WHERE D.ID_BL_TAXA = ID_BL_TAXA AND C.DT_CANCELAMENTO IS NULL  AND ISNULL(C.TP_EXPORTACAO,'') = 'ACC')"

        dgvDevolucao.DataBind()
        dgvDevolucao.Visible = True

        For i As Integer = 0 To Me.dgvDevolucao.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvDevolucao.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
        Next


        If ddlTipoInvoice.SelectedValue = 2 And ddlTipoFatura.SelectedValue = 1 And ddlEmissor.SelectedValue = 2 Then
            ddlTipoDevolucao.Enabled = False
        Else
            ddlTipoDevolucao.Enabled = True
        End If

        atualizaTotalFrete()
        ModalPopupExtender3.Show()
        ModalPopupExtender2.Show()
        dgvComissoes.Visible = False
        dgvOutrasTaxas.Visible = False
        dgvTaxasExteriorDeclaradas.Visible = False

    End Sub


    Private Sub btnComissoes_Click(sender As Object, e As EventArgs) Handles btnComissoes.Click
        divSuccessInvoice.Visible = False
        divErroInvoice.Visible = False

        dsComissoes.SelectCommand = "SELECT  ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,ISNULL(VL_TAXA,0)VL_TAXA,ISNULL(VL_TAXA_CALCULADO,0)VL_TAXA_CALCULADO  FROM  FN_ACCOUNT_DEVOLUCAO_COMISSAO (" & txtID_BL.Text & ", '" & txtGrau.Text & "') A WHERE VL_TAXA <> 0 AND ID_MOEDA =" & ddlMoeda.SelectedValue & " AND A.ID_BL NOT IN(SELECT ID_BL FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)"

        dgvComissoes.DataBind()
        dgvComissoes.Visible = True
        dgvTaxasExteriorDeclaradas.Visible = False
        dgvOutrasTaxas.Visible = False
        dgvDevolucao.Visible = False

        For i As Integer = 0 To Me.dgvComissoes.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvComissoes.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
        Next

        atualizaTotalComissoes()
        ModalPopupExtender6.Show()
        ModalPopupExtender2.Show()
    End Sub

    Private Sub btnOutrasTaxas_Click(sender As Object, e As EventArgs) Handles btnOutrasTaxas.Click
        divSuccessInvoice.Visible = False
        divErroInvoice.Visible = False

        dsOutrasTaxas.SelectCommand = "SELECT  ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,NM_ITEM_DESPESA,SIGLA_MOEDA,ISNULL(VL_TAXA,0)VL_TAXA,CD_DECLARADO,DT_RECEBIMENTO FROM  FN_ACCOUNT_OUTRAS_TAXAS_NOVA(" & txtID_BL.Text & ", '" & txtGrau.Text & "'," & ddlEmissor.SelectedValue & ", " & ddlTipoFatura.SelectedValue & ")  WHERE ID_PARCEIRO_EMPRESA = " & ddlAgente.SelectedValue & " AND VL_TAXA <> 0 AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS  WHERE ID_BL_TAXA IS NOT NULL) AND ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS D
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = D.ID_CONTA_PAGAR_RECEBER 
WHERE D.ID_BL_TAXA = ID_BL_TAXA AND C.DT_CANCELAMENTO IS NULL  AND ISNULL(C.TP_EXPORTACAO,'') = 'ACC')
 AND ID_MOEDA =" & ddlMoeda.SelectedValue

        dgvOutrasTaxas.Visible = True
        dgvOutrasTaxas.DataBind()
        dgvComissoes.Visible = False
        dgvTaxasExteriorDeclaradas.Visible = False
        dgvDevolucao.Visible = False
        atualizaTotalOutrasTaxas()
        ModalPopupExtender7.Show()
        ModalPopupExtender2.Show()
    End Sub

    Private Sub btnIncluirDevolucaoFrete_Click(sender As Object, e As EventArgs) Handles btnIncluirDevolucaoFrete.Click
        divinfo.Visible = False
        btnIncluirDevolucaoFrete.Visible = False
        Dim operador As String = VerificaPositivoNegativo()
        If lblValorFreteDevolucao.Text = "" Then
            lblValorFreteDevolucao.Text = 0
        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ID_BL As String
        Dim Devolucao As Decimal = 0
        Dim VALOR_STRING As String = 0


        If ddlTipoInvoice.SelectedValue = 2 And ddlTipoFatura.SelectedValue = 1 And ddlEmissor.SelectedValue = 2 Then
            'CASO NAO SEJA SELECIONADO O TIPO DE DEVOLUÇÃO MAS SEJA INVOICE HOUSE CREDIT NOTE FCA - OS 040459322 CHAMADO 4185
            For Each linha As GridViewRow In dgvDevolucao.Rows
                ID_BL = CType(linha.FindControl("lblID"), Label).Text
                Dim ID_BL_TAXA As String = CType(linha.FindControl("lblTaxa"), Label).Text
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                Dim ValorVenda As Decimal = CType(linha.FindControl("lblValorVenda"), Label).Text
                If check.Checked Then

                    'DEVOLUÇÃO DO FRETE DE VENDA
                    Devolucao = ValorVenda

                    VALOR_STRING = Devolucao.ToString
                    VALOR_STRING = VALOR_STRING.ToString.Replace(",", ".")

                    If VALOR_STRING <> 0 Then
                        Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) VALUES
(" & txtIDInvoice.Text & "," & ID_BL & ",(SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & "), " & ID_BL_TAXA & ",(SELECT  ID_ITEM_FRETE_ACCOUNT FROM TB_PARAMETROS)," & operador & VALOR_STRING & ", 'DF')")
                    End If

                End If

            Next

            Con.Fechar()


            dsDevolucao.DataBind()
            dgvItensInvoice.DataBind()
            dgvDevolucao.Visible = False
            ModalPopupExtender3.Hide()
            lblSuccessInvoice.Text = "Inclusão realizada com sucesso!"
            divSuccessInvoice.Visible = True
            ModalPopupExtender2.Show()
            atualizaTotalInvoice()

        Else

            If ddlTipoDevolucao.SelectedValue = 0 Then

                divinfo.Visible = True
                lblinfo.Text = "Selecione um tipo de devolução!"
            Else

                If lblValorFreteDevolucao.Text <> 0 Then
                    Devolucao = 0
                    VALOR_STRING = 0
                    ID_BL = txtID_BL.Text

                    If ddlTipoDevolucao.SelectedValue = 4 Then

                        'DEVOLUÇÃO DA DIFERENÇA DE FRETE
                        Devolucao = lblValorFreteDevolucao.Text
                        VALOR_STRING = Devolucao.ToString
                        VALOR_STRING = VALOR_STRING.ToString.Replace(",", ".")

                        Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) VALUES
(" & txtIDInvoice.Text & "," & ID_BL & ",(SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & "), NULL ,(SELECT  ID_ITEM_FRETE_ACCOUNT FROM TB_PARAMETROS)," & operador & VALOR_STRING & ", 'DF')")

                    Else

                        For Each linha As GridViewRow In dgvDevolucao.Rows
                            ID_BL = CType(linha.FindControl("lblID"), Label).Text
                            Dim ID_BL_TAXA As String = CType(linha.FindControl("lblTaxa"), Label).Text
                            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                            Dim ValorCompra As Decimal = CType(linha.FindControl("lblValorCompra"), Label).Text
                            Dim ValorVenda As Decimal = CType(linha.FindControl("lblValorVenda"), Label).Text
                            If check.Checked Then

                                If ddlTipoDevolucao.SelectedValue = 2 Then
                                    'DEVOLUÇÃO DO FRETE DE COMPRA
                                    Devolucao = ValorCompra
                                ElseIf ddlTipoDevolucao.SelectedValue = 3 Then
                                    'DEVOLUÇÃO DO FRETE DE VENDA
                                    Devolucao = ValorVenda
                                End If

                                VALOR_STRING = Devolucao.ToString
                                VALOR_STRING = VALOR_STRING.ToString.Replace(",", ".")

                                If VALOR_STRING <> 0 Then
                                    Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) VALUES
(" & txtIDInvoice.Text & "," & ID_BL & ",(SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & "), " & ID_BL_TAXA & ",(SELECT  ID_ITEM_FRETE_ACCOUNT FROM TB_PARAMETROS)," & operador & VALOR_STRING & ", 'DF')")
                                End If

                            End If

                        Next
                    End If
                End If

                If ddlTipoInvoice.SelectedValue = 1 And ddlTipoDevolucao.SelectedValue <> 0 Then
                    'MASTER
                    Con.ExecutarQuery("UPDATE TB_BL SET ID_STATUS_FRETE_AGENTE =  " & ddlTipoDevolucao.SelectedValue & " WHERE ID_BL = " & txtID_BL.Text)
                ElseIf ddlTipoInvoice.SelectedValue = 2 And ddlTipoDevolucao.SelectedValue <> 0 Then
                    'HOUSE
                    Con.ExecutarQuery("UPDATE TB_BL SET ID_STATUS_FRETE_AGENTE =  " & ddlTipoDevolucao.SelectedValue & " WHERE ID_BL = (SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & txtID_BL.Text & ")")
                End If

                Con.Fechar()


                dsDevolucao.DataBind()
                dgvItensInvoice.DataBind()
                dgvDevolucao.Visible = False
                ModalPopupExtender3.Hide()
                lblSuccessInvoice.Text = "Inclusão realizada com sucesso!"
                divSuccessInvoice.Visible = True
                ModalPopupExtender2.Show()
                atualizaTotalInvoice()
            End If

        End If
        btnIncluirDevolucaoFrete.Visible = True

    End Sub

    Private Sub btnIncluirComissoes_Click(sender As Object, e As EventArgs) Handles btnIncluirComissoes.Click
        Dim operador As String = VerificaPositivoNegativo()
        btnIncluirComissoes.Visible = False

        For Each linha As GridViewRow In dgvComissoes.Rows
            Dim ID_BL As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text

            If check.Checked Then
                Dim Con As New Conexao_sql
                Dim VALOR_STRING As String = Valor.ToString
                VALOR_STRING = VALOR_STRING.ToString.Replace(",", ".")
                Con.Conectar()

                If Valor < 0 Then
                    Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) VALUES
(" & txtIDInvoice.Text & "," & ID_BL & ",(SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & "), NULL,(SELECT  ID_ITEM_COMISSAO_ACCOUNT FROM TB_PARAMETROS)," & VALOR_STRING & ", 'CO')")
                Else
                    Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) VALUES
(" & txtIDInvoice.Text & "," & ID_BL & ",(SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & "), NULL,(SELECT  ID_ITEM_COMISSAO_ACCOUNT FROM TB_PARAMETROS)," & operador & VALOR_STRING & ", 'CO')")
                End If


            End If


        Next
        dsComissoes.DataBind()
        dgvItensInvoice.DataBind()
        dgvComissoes.Visible = False
        ModalPopupExtender6.Hide()
        lblSuccessInvoice.Text = "Inclusão realizada com sucesso!"
        divSuccessInvoice.Visible = True
        ModalPopupExtender2.Show()
        atualizaTotalInvoice()
        btnIncluirComissoes.Visible = True
    End Sub
    Private Sub btnIncluirOutrasTaxas_Click(sender As Object, e As EventArgs) Handles btnIncluirOutrasTaxas.Click
        Dim operador As String = VerificaPositivoNegativo()
        btnIncluirOutrasTaxas.Visible = False
        For Each linha As GridViewRow In dgvOutrasTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")

            If check.Checked Then
                Dim Con As New Conexao_sql
                Con.Conectar()
                Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) SELECT " & txtIDInvoice.Text & ",A.ID_BL,(SELECT ID_BL_MASTER FROM TB_BL B WHERE A.ID_BL = B.ID_BL),A.ID_BL_TAXA,A.ID_ITEM_DESPESA," & operador & " VL_TAXA_CALCULADO,'OT' FROM TB_BL_TAXA A WHERE A.ID_BL_TAXA =" & ID)
            End If
        Next

        dsOutrasTaxas.DataBind()
        dgvItensInvoice.DataBind()
        dgvOutrasTaxas.Visible = False
        ModalPopupExtender7.Hide()
        lblSuccessInvoice.Text = "Inclusão realizada com sucesso!"
        divSuccessInvoice.Visible = True
        ModalPopupExtender2.Show()
        atualizaTotalInvoice()
        btnIncluirOutrasTaxas.Visible = True

    End Sub
    Private Sub lkAvisoEmbarque_Click(sender As Object, e As EventArgs) Handles lkAvisoEmbarque.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione um registro!"
        Else
            Session("DataInicial") = ""
            Session("DataFinal") = ""

            Session("DataInicial") = txtVencimentoInicial.Text
            Session("DataFinal") = txtVencimentoFinal.Text

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "AvisoEmbarque()", True)
        End If

    End Sub


    Sub atualizaTotalFrete()

        Dim Con As New Conexao_sql
        lblValorFreteDevolucao.Text = 0
        lblValorFreteCompra.Text = 0
        lblValorFreteVenda.Text = 0

        If ddlTipoDevolucao.SelectedValue = 2 Then
            'DEVOLUÇÃO DO FRETE DE COMPRA
            For Each linha As GridViewRow In dgvDevolucao.Rows
                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                Dim ValorCompra As Decimal = CType(linha.FindControl("lblValorCompra"), Label).Text
                Dim ValorVenda As Decimal = CType(linha.FindControl("lblValorVenda"), Label).Text

                Dim ValorCompra2 As Decimal = lblValorFreteCompra.Text
                Dim ValorVenda2 As Decimal = lblValorFreteVenda.Text

                If check.Checked Then
                    lblValorFreteCompra.Text = ValorCompra + ValorCompra2
                    lblValorFreteVenda.Text = ValorVenda + ValorVenda2

                    lblValorFreteDevolucao.Text = lblValorFreteCompra.Text
                End If

            Next
        ElseIf ddlTipoDevolucao.SelectedValue = 3 Then
            'DEVOLUÇÃO DO FRETE DE VENDA
            For Each linha As GridViewRow In dgvDevolucao.Rows
                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                Dim ValorCompra As Decimal = CType(linha.FindControl("lblValorCompra"), Label).Text
                Dim ValorVenda As Decimal = CType(linha.FindControl("lblValorVenda"), Label).Text

                Dim ValorCompra2 As Decimal = lblValorFreteCompra.Text
                Dim ValorVenda2 As Decimal = lblValorFreteVenda.Text

                If check.Checked Then
                    lblValorFreteCompra.Text = ValorCompra + ValorCompra2
                    lblValorFreteVenda.Text = ValorVenda + ValorVenda2

                    lblValorFreteDevolucao.Text = lblValorFreteVenda.Text
                End If


            Next
        ElseIf ddlTipoDevolucao.SelectedValue = 4 Then
            'DEVOLUÇÃO DA DIFERENÇA DE FRETE
            For Each linha As GridViewRow In dgvDevolucao.Rows
                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                Dim ValorCompra As Decimal = CType(linha.FindControl("lblValorCompra"), Label).Text
                Dim ValorVenda As Decimal = CType(linha.FindControl("lblValorVenda"), Label).Text

                Dim ValorCompra2 As Decimal = lblValorFreteCompra.Text
                Dim ValorVenda2 As Decimal = lblValorFreteVenda.Text

                If check.Checked Then
                    lblValorFreteCompra.Text = ValorCompra + ValorCompra2
                    lblValorFreteVenda.Text = ValorVenda + ValorVenda2

                    lblValorFreteDevolucao.Text = lblValorFreteVenda.Text - lblValorFreteCompra.Text
                End If


            Next

        End If

        If lblValorFreteDevolucao.Text < 0 Then
            divinfo.Visible = True
            lblinfo.Text = "O valor total a ser incluso na invoice é negativo!"
        Else
            divinfo.Visible = False
        End If
        ModalPopupExtender3.Show()
        ModalPopupExtender2.Show()



    End Sub

    Sub atualizaTotalInvoice()
        lblTotalInvoice.Text = 0
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT SUM(ISNULL(VL_TAXA,0))QTD FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE = " & txtIDInvoice.Text)
        If Not IsDBNull(ds.Tables(0).Rows(0).Item("QTD")) Then
            lblTotalInvoice.Text = ds.Tables(0).Rows(0).Item("QTD")
        End If
        ModalPopupExtender2.Show()
        Con.Fechar()
    End Sub

    Sub atualizaTotalExteriorDeclaradas()
        lblTotalExteriorDeclaradas.Text = 0

        For Each linha As GridViewRow In dgvTaxasExteriorDeclaradas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text

            Dim Valor2 As Decimal = lblTotalExteriorDeclaradas.Text

            If check.Checked Then
                lblTotalExteriorDeclaradas.Text = Valor + Valor2
            End If
            ModalPopupExtender10.Show()
            ModalPopupExtender2.Show()
        Next
    End Sub

    Sub atualizaTotalComissoes()
        lblTotalComissoes.Text = 0

        For Each linha As GridViewRow In dgvComissoes.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text

            Dim Valor2 As Decimal = lblTotalComissoes.Text

            If check.Checked Then
                lblTotalComissoes.Text = Valor + Valor2
            End If
            ModalPopupExtender6.Show()
            ModalPopupExtender2.Show()
        Next
    End Sub

    Sub atualizaTotalOutrasTaxas()
        lblTotalOutrasTaxas.Text = 0

        For Each linha As GridViewRow In dgvOutrasTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text

            Dim Valor2 As Decimal = lblTotalOutrasTaxas.Text

            If check.Checked Then
                lblTotalOutrasTaxas.Text = Valor + Valor2

            End If
            ModalPopupExtender7.Show()
            ModalPopupExtender2.Show()

        Next


    End Sub

    Private Sub dgvDevolucao_Load(sender As Object, e As EventArgs) Handles dgvDevolucao.Load
        If dgvDevolucao.Visible = True Then
            atualizaTotalFrete()
        End If
    End Sub


    Private Sub dgvComissoes_Load(sender As Object, e As EventArgs) Handles dgvComissoes.Load
        If dgvComissoes.Visible = True Then
            atualizaTotalComissoes()
        End If
    End Sub
    Private Sub dgvOutrasTaxas_Load(sender As Object, e As EventArgs) Handles dgvOutrasTaxas.Load
        If dgvOutrasTaxas.Visible = True Then
            atualizaTotalOutrasTaxas()
        End If
    End Sub



    Private Sub dgvItensInvoice_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvItensInvoice.RowCommand
        divErroInvoice.Visible = False
        divSuccessInvoice.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErroInvoice.Visible = True
                lblErroInvoice.Text = "Preencha todos os campos obrigatórios."
                Exit Sub

            Else
                Con.ExecutarQuery("DELETE From TB_ACCOUNT_INVOICE_ITENS Where ID_ACCOUNT_INVOICE_ITENS = " & ID)
                lblSuccessInvoice.Text = "Registro deletado!"
                divSuccessInvoice.Visible = True
                dgvItensInvoice.DataBind()
            End If
            ModalPopupExtender2.Show()
        End If
        Con.Fechar()
    End Sub

    Private Sub lkGeraCSV_Click(sender As Object, e As EventArgs) Handles lkGeraCSV.Click
        If txtVencimentoInicial.Text = "" Or txtVencimentoFinal.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "É necessário informar vencimento incial e final!"
            dgvInvoice.Visible = False

        Else
            Dim filtro As String = ""
            If rdStatus.SelectedValue = 1 Then
                filtro &= " WHERE A.FL_CONFERIDO = 1"
            ElseIf rdStatus.SelectedValue = 2 Then
                filtro &= " WHERE A.FL_CONFERIDO = 0"
            End If

            If ddlFiltro.SelectedValue = 1 Then
                filtro = " AND A.NR_INVOICE LIKE '%" & txtPesquisa.Text & " %'"

            ElseIf ddlFiltro.SelectedValue = 2 Then
                filtro = " AND B.NR_PROCESSO LIKE '%" & txtPesquisa.Text & " %'"
            ElseIf ddlFiltro.SelectedValue = 3 Then
                filtro = " AND B.NR_BL LIKE '%" & txtPesquisa.Text & " %'"
            ElseIf ddlFiltro.SelectedValue = 4 Then
                filtro = " AND A.NM_AGENTE LIKE '%" & txtPesquisa.Text & " %'"
            ElseIf ddlFiltro.SelectedValue = 5 Then
                filtro = " AND A.NM_ACCOUNT_TIPO_EMISSOR LIKE '%" & txtPesquisa.Text & "%'"
            End If



            Dim SQL As String = "SELECT A.NR_INVOICE INVOICE,A.NM_ACCOUNT_TIPO_EMISSOR TIPO_EMISSOR,A.NM_ACCOUNT_TIPO_FATURA TIPO_FATURA,CONVERT(VARCHAR,A.DT_INVOICE,103) DATA,B.NR_PROCESSO,B.NR_BL 'No BL',A.NM_AGENTE AGENTE,
CASE WHEN FL_CONFERIDO = 1 THEN 'SIM' ELSE 'NÃO' END CONFERIDO,A.NM_ACCOUNT_TIPO_INVOICE TIPO_INVOICE,CONVERT(VARCHAR,A.DT_VENCIMENTO,103) VENCIMENTO,CONVERT(VARCHAR,A.DT_FECHAMENTO,103) FECHAMENTO,A.DS_OBSERVACAO OBS,A.SIGLA_MOEDA MOEDA,(SELECT SUM(ISNULL(VL_TAXA,0)) FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE = A.ID_ACCOUNT_INVOICE)TOTAL FROM (SELECT * FROM FN_ACCOUNT_INVOICE('" & txtVencimentoInicial.Text & "','" & txtVencimentoFinal.Text & "')) AS A 
INNER JOIN TB_BL B ON B.ID_BL = A.ID_BL_INVOICE " & filtro & " group by A.ID_ACCOUNT_INVOICE,A.ID_ACCOUNT_INVOICE,A.NR_INVOICE,A.NM_ACCOUNT_TIPO_EMISSOR,A.NM_ACCOUNT_TIPO_FATURA,A.DT_INVOICE,B.NR_PROCESSO,B.NR_BL,A.NM_AGENTE,FL_CONFERIDO,A.NM_ACCOUNT_TIPO_INVOICE,A.SIGLA_MOEDA,A.DT_FECHAMENTO,A.DS_OBSERVACAO,A.DT_VENCIMENTO"

            Classes.Excel.exportaExcel(SQL, "NVOCC", "Invoices")
        End If


    End Sub


    Private Sub btnCSVProcessoPeriodo_Click(sender As Object, e As EventArgs) Handles btnCSVProcessoPeriodo.Click

        If txtEmbarqueInicial.Text = "" Then
            divErroRelatorio.Visible = True
            lblErroRelatorio.Text = "É necessário informar data incial para concluir a pesquisa"
        ElseIf txtEmbarqueFinal.Text = "" Then
            divErroRelatorio.Visible = True
            lblErroRelatorio.Text = "É necessário informar data final para concluir a pesquisa"
        Else

            Dim filtro As String = ""

            If ddlAgenteRelatorio.SelectedValue <> 0 Then
                filtro &= " AND ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgenteRelatorio.SelectedValue
            End If
            If txtProcessoRelatorio.Text <> "" Then
                filtro &= " AND NR_PROCESSO = '" & txtProcessoRelatorio.Text & "'"
            End If

            Dim SQL As String = "SELECT NR_PROCESSO,BL_MASTER,PAGAMENTO_BL_MASTER AS 'TIPO FRETE MASTER'
,NR_BL AS 'BL_HOUSE',TIPO_PAGAMENTO AS 'TIPO DO FRETE HOUSE',TIPO_ESTUFAGEM,
CASE WHEN (SELECT ISNULL(CD_SIGLA,'') FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_ORIGEM) = '' THEN ORIGEM ELSE

(SELECT CD_SIGLA FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_ORIGEM)
END ORIGEM,CASE WHEN (SELECT ISNULL(CD_SIGLA,'') FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_DESTINO) = '' THEN DESTINO ELSE

(SELECT CD_SIGLA FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_DESTINO)
END DESTINO,(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_CLIENTE)CLIENTE,
(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_AGENTE_INTERNACIONAL)AGENTE_INTERNACIONAL,
(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_TRANSPORTADOR)TRANSPORTADOR,convert(varchar,DT_PREVISAO_EMBARQUE_MASTER,103)DT_PREVISAO_EMBARQUE_MASTER,convert(varchar,DT_EMBARQUE_MASTER,103)DT_EMBARQUE_MASTER,convert(varchar,DT_PREVISAO_CHEGADA_MASTER,103)DT_PREVISAO_CHEGADA_MASTER,convert(varchar,DT_CHEGADA_MASTER,103)DT_CHEGADA_MASTER ,B.VL_CAMBIO,B.DT_LIQUIDACAO
            FROM [dbo].[View_House] A
LEFT JOIN [VW_PROCESSO_RECEBIDO] B ON A.ID_BL = B.ID_BL  
 WHERE CONVERT(DATE,DT_EMBARQUE_MASTER,103) BETWEEN CONVERT(DATE,'" & txtEmbarqueInicial.Text & "',103) AND CONVERT(DATE,'" & txtEmbarqueFinal.Text & "',103) 
" & filtro & "
ORDER BY NR_PROCESSO"

            Classes.Excel.exportaExcel(SQL, "NVOCC", "ProcessosPeriodo")
        End If

    End Sub

    Private Sub btnRelacaoAgentes_Click(sender As Object, e As EventArgs) Handles btnRelacaoAgentes.Click
        Dim sql As String = "SELECT DISTINCT PARCEIRO_AGENTE_INTERNACIONAL FROM [View_House] WHERE CONVERT(DATE, DT_EMBARQUE_MASTER,103) BETWEEN CONVERT(DATE,'" & txtEmbarqueInicial.Text & "',103) AND CONVERT(DATE,'" & txtEmbarqueFinal.Text & "',103)"
        Classes.Excel.exportaExcel(sql, "NVOCC", "RelacaoAgentes")

    End Sub

    Private Sub lkInvoiceFCA_Click(sender As Object, e As EventArgs) Handles lkInvoiceFCA.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione um registro!"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_ACCOUNT_TIPO_EMISSOR,ID_ACCOUNT_TIPO_FATURA FROM TB_ACCOUNT_INVOICE WHERE ID_ACCOUNT_INVOICE = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_EMISSOR") = 2 Then
                    Session("DataInicial") = ""
                    Session("DataFinal") = ""

                    Session("DataInicial") = txtVencimentoInicial.Text
                    Session("DataFinal") = txtVencimentoFinal.Text

                    If ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_FATURA") = 1 Then
                        'DEBIT NOTE
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "InvoiceFCA()", True)

                    ElseIf ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_FATURA") = 2 Then
                        'CREDIT NOTE
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "InvoiceCredit()", True)

                    End If


                Else
                    divErro.Visible = True
                    lblErro.Text = "Relatório disponivel apenas para invoices emitidas pela FCA"
                End If
            End If

        End If

    End Sub

    Private Sub lkImprimirSOA1_Click(sender As Object, e As EventArgs) Handles lkImprimirSOA1.Click
        If txtVencimentoInicialSOA.Text = "" Or txtVencimentoFinalSOA.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "É necessário informar vencimento inicial e final!"
        Else
            Session("DataInicial") = ""
            Session("DataFinal") = ""

            Session("DataInicial") = txtVencimentoInicialSOA.Text
            Session("DataFinal") = txtVencimentoFinalSOA.Text

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "SOA1()", True)
        End If
    End Sub

    Private Sub lkImprimirSOA2_Click(sender As Object, e As EventArgs) Handles lkImprimirSOA2.Click
        If txtVencimentoInicialSOA.Text = "" Or txtVencimentoFinalSOA.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "É necessário informar vencimento inicial e final!"

        ElseIf DateDiff(DateInterval.Day, Convert.ToDateTime(txtVencimentoInicialSOA.Text), Convert.ToDateTime(txtVencimentoFinalSOA.Text)) > 62 Then
            divErro.Visible = True
            lblErro.Text = "O periodo maximo para consulta desse relatorio é de 60 dias!"

        Else

            Session("DataInicial") = ""
            Session("DataFinal") = ""

            Session("DataInicial") = txtVencimentoInicialSOA.Text
            Session("DataFinal") = txtVencimentoFinalSOA.Text

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "SOA2()", True)
        End If
    End Sub

    Private Sub btnFecharDevolucaoFrete_Click(sender As Object, e As EventArgs) Handles btnFecharDevolucaoFrete.Click
        dgvDevolucao.Visible = False
        ModalPopupExtender3.Hide()
        ModalPopupExtender2.Show()

    End Sub
    Private Sub btnFecharComissoes_Click(sender As Object, e As EventArgs) Handles btnFecharComissoes.Click
        dgvComissoes.Visible = False
        ModalPopupExtender6.Hide()
        ModalPopupExtender2.Show()

    End Sub

    Private Sub btnFecharOutrasTaxas_Click(sender As Object, e As EventArgs) Handles btnFecharOutrasTaxas.Click
        dgvOutrasTaxas.Visible = False
        ModalPopupExtender7.Hide()
        ModalPopupExtender2.Show()

    End Sub

    Private Sub ddlTipoDevolucao_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoDevolucao.SelectedIndexChanged
        atualizaTotalFrete()
    End Sub

    Protected Sub dgvInvoice_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvInvoice.DataSource = Session("TaskTable")
            dgvInvoice.DataBind()
            dgvInvoice.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
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

    Private Sub btnBuscarRelatorio_Click(sender As Object, e As EventArgs) Handles btnBuscarRelatorio.Click
        divErroRelatorio.Visible = False
        Dim v As New VerificaData

        If txtEmbarqueInicial.Text = "" Then
            divErroRelatorio.Visible = True
            lblErroRelatorio.Text = "É necessário informar data incial para concluir a pesquisa"
        ElseIf txtEmbarqueFinal.Text = "" Then
            divErroRelatorio.Visible = True
            lblErroRelatorio.Text = "É necessário informar data final para concluir a pesquisa"
        ElseIf v.ValidaData(txtEmbarqueInicial.Text) = False Or IsDate(txtEmbarqueInicial.Text) = False Then
            divErroRelatorio.Visible = True
            lblErroRelatorio.Text = "Data inicial é inválida."
        ElseIf v.ValidaData(txtEmbarqueFinal.Text) = False Or IsDate(txtEmbarqueFinal.Text) = False Then
            divErroRelatorio.Visible = True
            lblErroRelatorio.Text = "Data final é inválida."
        Else

            Dim filtro As String = ""

            If ddlAgenteRelatorio.SelectedValue <> 0 Then
                filtro &= " AND ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgenteRelatorio.SelectedValue
            End If
            If txtProcessoRelatorio.Text <> "" Then
                filtro &= " AND NR_PROCESSO = '" & txtProcessoRelatorio.Text & "'"
            End If

            Dim sql As String = "SELECT A.ID_BL,NR_PROCESSO,BL_MASTER,PAGAMENTO_BL_MASTER AS 'TIPO FRETE MASTER'
,NR_BL AS 'BL_HOUSE',TIPO_PAGAMENTO AS 'TIPO FRETE HOUSE',TIPO_ESTUFAGEM,
CASE WHEN (SELECT ISNULL(CD_SIGLA,'') FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_ORIGEM) = '' THEN ORIGEM ELSE

(SELECT CD_SIGLA FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_ORIGEM)
END ORIGEM,CASE WHEN (SELECT ISNULL(CD_SIGLA,'') FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_DESTINO) = '' THEN DESTINO ELSE

(SELECT CD_SIGLA FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_DESTINO)
END DESTINO,(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_CLIENTE)CLIENTE,
(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_AGENTE_INTERNACIONAL)AGENTE_INTERNACIONAL,
(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_TRANSPORTADOR)TRANSPORTADOR,convert(varchar,DT_PREVISAO_EMBARQUE_MASTER,103)DT_PREVISAO_EMBARQUE_MASTER,convert(varchar,DT_EMBARQUE_MASTER,103)DT_EMBARQUE_MASTER,convert(varchar,DT_PREVISAO_CHEGADA_MASTER,103)DT_PREVISAO_CHEGADA_MASTER,convert(varchar,DT_CHEGADA_MASTER,103)DT_CHEGADA_MASTER , B.VL_CAMBIO_FRETE, B.DT_LIQUIDACAO
FROM [dbo].[View_House] A
LEFT JOIN [VW_PROCESSO_RECEBIDO] B ON A.ID_BL = B.ID_BL WHERE CONVERT(DATE,DT_EMBARQUE_MASTER,103) BETWEEN CONVERT(DATE,'" & txtEmbarqueInicial.Text & "',103) AND CONVERT(DATE,'" & txtEmbarqueFinal.Text & "',103) " & filtro
            dsProcessoPeriodo.SelectCommand = sql
            dgvProcessoPeriodo.DataBind()


            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_BL,0)ID_BL FROM TB_BL WHERE NR_PROCESSO = '" & txtProcessoRelatorio.Text & "'")
            If ds.Tables(0).Rows.Count > 0 Then
                txtIDBLProcessoRelatorio.Text = ds.Tables(0).Rows(0).Item("ID_BL")
            Else
                txtIDBLProcessoRelatorio.Text = 0
            End If


            Session("DataInicial") = ""
            Session("DataFinal") = ""
            Session("DataInicial") = txtEmbarqueInicial.Text
            Session("DataFinal") = txtEmbarqueFinal.Text

        End If
        ModalPopupExtender8.Show()
    End Sub

    Private Sub btnTaxasExteriorDeclaradas_Click(sender As Object, e As EventArgs) Handles btnTaxasExteriorDeclaradas.Click

        divSuccessInvoice.Visible = False
        divErroInvoice.Visible = False


        dsTaxasExteriorDeclaradas.SelectCommand = "SELECT ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,ISNULL(VL_TAXA,0)VL_TAXA,NM_ITEM_DESPESA,DT_RECEBIMENTO FROM FN_ACCOUNT_TAXAS_DECLARADAS (" & txtID_BL.Text & ", '" & txtGrau.Text & "')  WHERE VL_TAXA <> 0 AND ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_BL_TAXA IS NOT NULL) AND ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS D
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = D.ID_CONTA_PAGAR_RECEBER 
WHERE D.ID_BL_TAXA = ID_BL_TAXA AND C.DT_CANCELAMENTO IS NULL  AND ISNULL(C.TP_EXPORTACAO,'') = 'ACC') AND ID_MOEDA =" & ddlMoeda.SelectedValue

        dgvTaxasExteriorDeclaradas.DataBind()
        dgvTaxasExteriorDeclaradas.Visible = True
        dgvComissoes.Visible = False
        dgvOutrasTaxas.Visible = False
        dgvDevolucao.Visible = False

        For i As Integer = 0 To Me.dgvTaxasExteriorDeclaradas.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvTaxasExteriorDeclaradas.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
        Next

        atualizaTotalExteriorDeclaradas()
        ModalPopupExtender10.Show()
        ModalPopupExtender2.Show()
    End Sub

    Private Sub btnIncluirTaxasExteriorDeclaradas_Click(sender As Object, e As EventArgs) Handles btnIncluirTaxasExteriorDeclaradas.Click
        Dim operador As String = VerificaPositivoNegativo()
        btnIncluirTaxasExteriorDeclaradas.Visible = False
        For Each linha As GridViewRow In dgvTaxasExteriorDeclaradas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")

            If check.Checked Then
                Dim Con As New Conexao_sql
                Con.Conectar()
                Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) SELECT " & txtIDInvoice.Text & ",A.ID_BL,(SELECT ID_BL_MASTER FROM TB_BL B WHERE A.ID_BL = B.ID_BL),A.ID_BL_TAXA,A.ID_ITEM_DESPESA," & operador & " VL_TAXA_CALCULADO,'TD' FROM TB_BL_TAXA A WHERE A.ID_BL_TAXA =" & ID)
            End If
        Next

        dsTaxasExteriorDeclaradas.DataBind()
        dgvItensInvoice.DataBind()
        dgvTaxasExteriorDeclaradas.Visible = False
        ModalPopupExtender10.Hide()
        lblSuccessInvoice.Text = "Inclusão realizada com sucesso!"
        divSuccessInvoice.Visible = True
        ModalPopupExtender2.Show()
        atualizaTotalInvoice()
        btnIncluirTaxasExteriorDeclaradas.Visible = True

    End Sub

    Private Sub btnFecharTaxasExteriorDeclaradas_Click(sender As Object, e As EventArgs) Handles btnFecharTaxasExteriorDeclaradas.Click
        dgvTaxasExteriorDeclaradas.Visible = False
        ModalPopupExtender10.Hide()
        ModalPopupExtender2.Show()

    End Sub

    Private Sub dgvTaxasExteriorDeclaradas_Load(sender As Object, e As EventArgs) Handles dgvTaxasExteriorDeclaradas.Load
        If dgvTaxasExteriorDeclaradas.Visible = True Then
            atualizaTotalExteriorDeclaradas()
        End If
    End Sub

    Private Sub btnImprimirProcessoPeriodo_Click(sender As Object, e As EventArgs) Handles btnImprimirProcessoPeriodo.Click
        divErroRelatorio.Visible = False
        If txtEmbarqueInicial.Text = "" Then
            divErroRelatorio.Visible = True
            lblErroRelatorio.Text = "É necessário informar data incial para concluir a pesquisa"
        ElseIf txtEmbarqueFinal.Text = "" Then
            divErroRelatorio.Visible = True
            lblErroRelatorio.Text = "É necessário informar data final para concluir a pesquisa"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_BL,0)ID_BL FROM TB_BL WHERE NR_PROCESSO = '" & txtProcessoRelatorio.Text & "'")
            If ds.Tables(0).Rows.Count > 0 Then
                txtIDBLProcessoRelatorio.Text = ds.Tables(0).Rows(0).Item("ID_BL")
            Else
                txtIDBLProcessoRelatorio.Text = 0
            End If

            Session("DataInicial") = ""
            Session("DataFinal") = ""
            Session("DataInicial") = txtEmbarqueInicial.Text
            Session("DataFinal") = txtEmbarqueFinal.Text
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ProcessosPeriodo()", True)
        End If
        ModalPopupExtender8.Show()
    End Sub
End Class