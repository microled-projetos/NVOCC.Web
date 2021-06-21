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

                ds = Con.ExecutarQuery("SELECT B.ID_BL,A.ID_ACCOUNT_INVOICE,A.NR_INVOICE,A.ID_ACCOUNT_TIPO_EMISSOR,A.ID_ACCOUNT_TIPO_FATURA,A.DT_INVOICE,B.NR_PROCESSO,B.NR_BL,GRAU,A.ID_PARCEIRO_AGENTE,FL_CONFERIDO,A.ID_ACCOUNT_TIPO_INVOICE,A.ID_MOEDA,A.DT_FECHAMENTO,A.DT_VENCIMENTO,A.DS_OBSERVACAO,(SELECT SUM(ISNULL(VL_TAXA_BR,0)) FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE = A.ID_ACCOUNT_INVOICE)VALOR_TOTAL FROM (SELECT * FROM FN_ACCOUNT_INVOICE('" & txtVencimentoInicial.Text & "','" & txtVencimentoFinal.Text & "')) AS A INNER JOIN TB_BL B ON B.ID_BL = A.ID_BL_INVOICE WHERE A.ID_ACCOUNT_INVOICE = " & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtIDInvoice.Text = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_INVOICE").ToString()
                    txtID_BL.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                    txtGrau.Text = ds.Tables(0).Rows(0).Item("GRAU").ToString()
                    ddlAgente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE").ToString()
                    ddlEmissor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_EMISSOR").ToString()
                    ddlTipoInvoice.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE").ToString()
                    txtProc_ou_BL.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO").ToString()
                    txtVencimento.Text = ds.Tables(0).Rows(0).Item("DT_VENCIMENTO").ToString()
                    ddlMoeda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA").ToString()
                    ddlTipoFatura.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_FATURA").ToString()
                    txtNumeroInvoice.Text = ds.Tables(0).Rows(0).Item("NR_INVOICE").ToString()
                    txtDataInvoice.Text = ds.Tables(0).Rows(0).Item("DT_INVOICE").ToString()
                    txtObsInvoice.Text = ds.Tables(0).Rows(0).Item("DS_OBSERVACAO").ToString()
                    ckbConferido.Checked = ds.Tables(0).Rows(0).Item("FL_CONFERIDO")
                End If
                ModalPopupExtender2.Show()

                Con.Fechar()

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
                    ds = Con.ExecutarQuery("SELECT NR_INVOICE FROM TB_NUMERACAO")
                    If ds.Tables(0).Rows.Count > 0 Then

                        numero = ds.Tables(0).Rows(0).Item("NR_INVOICE")
                        numero = numero + 1
                        numeroFinal = numero.ToString.PadLeft(6, "0")
                        Invoice = ddlAgente.SelectedValue & numeroFinal
                    End If
                    txtNumeroInvoice.Text = Invoice
                End If

                If ddlEmissor.SelectedValue = 0 Or ddlAgente.SelectedValue = 0 Or ddlTipoInvoice.SelectedValue = 0 Or txtProc_ou_BL.Text = "" Or txtVencimento.Text = "" Or ddlMoeda.SelectedValue = 0 Or ddlTipoFatura.SelectedValue = 0 Or txtDataInvoice.Text = "" Or txtNumeroInvoice.Text = "" Then
                    divErroInvoice.Visible = True
                    lblErroInvoice.Text = "Preencha todos os campos obrigatórios."
                    ModalPopupExtender2.Show()
                Else
                    'insert
                    ds = Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE (ID_PARCEIRO_AGENTE,ID_ACCOUNT_TIPO_INVOICE,ID_ACCOUNT_TIPO_EMISSOR,ID_ACCOUNT_TIPO_FATURA,ID_BL,ID_MOEDA,NR_INVOICE,DT_INVOICE,DT_VENCIMENTO,FL_CONFERIDO,DS_OBSERVACAO,ID_USUARIO_LANCAMENTO) VALUES (" & ddlAgente.SelectedValue & "," & ddlTipoInvoice.SelectedValue & ", " & ddlEmissor.SelectedValue & ", " & ddlTipoFatura.SelectedValue & ", " & txtID_BL.Text & " ," & ddlMoeda.SelectedIndex & ",'" & txtNumeroInvoice.Text & "', CONVERT(DATE,'" & txtDataInvoice.Text & "',103),CONVERT(DATE,'" & txtVencimento.Text & "',103),'" & ckbConferido.Checked & "','" & txtObsInvoice.Text & "', " & Session("ID_USUARIO") & ") ; Select SCOPE_IDENTITY() as ID_ACCOUNT_INVOICE ")
                    txtIDInvoice.Text = ds.Tables(0).Rows(0).Item("ID_ACCOUNT_INVOICE")
                    lblSuccessInvoice.Text = "Registro cadastrado com sucesso!"
                    divSuccessInvoice.Visible = True
                    'Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_INVOICE = '" & numeroFinal & "' WHERE ID_NUMERACAO = 5")

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
                Con.ExecutarQuery("UPDATE TB_ACCOUNT_INVOICE SET ID_PARCEIRO_AGENTE = " & ddlAgente.SelectedValue & ",ID_ACCOUNT_TIPO_INVOICE =" & ddlTipoInvoice.SelectedValue & ",ID_ACCOUNT_TIPO_EMISSOR = " & ddlEmissor.SelectedValue & ",ID_ACCOUNT_TIPO_FATURA = " & ddlTipoFatura.SelectedValue & ",ID_BL = " & txtID_BL.Text & ",ID_MOEDA = " & ddlMoeda.SelectedIndex & ",NR_INVOICE = '" & txtNumeroInvoice.Text & "',DT_INVOICE = CONVERT(DATE,'" & txtDataInvoice.Text & "',103),DT_VENCIMENTO = CONVERT(DATE,'" & txtVencimento.Text & "',103),FL_CONFERIDO = '" & ckbConferido.Checked & "',DS_OBSERVACAO = '" & txtObsInvoice.Text & "',ID_USUARIO_ALTERACAO = " & Session("ID_USUARIO") & " WHERE ID_ACCOUNT_INVOICE = " & txtIDInvoice.Text)
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

                'delete
                Con.ExecutarQuery("DELETE FROM TB_ACCOUNT_INVOICE WHERE ID_ACCOUNT_INVOICE " & txtID.Text)
                Con.ExecutarQuery("DELETE FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE " & txtID.Text)
                lblSuccess.Text = "Registro deletado com sucesso!"
                divSuccess.Visible = True
            End If
        End If
    End Sub

    Private Sub btnPesquisa_Click(sender As Object, e As EventArgs) Handles btnPesquisa.Click
        RegistrosGrid()
    End Sub

    Sub limpaFormulario()
        txtID.Text = ""
        txtIDInvoice.Text = ""
        txtID_BL.Text = ""
        txtGrau.Text = ""
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
        ckbConferido.Checked = False
        divErroInvoice.Visible = False
        divSuccessInvoice.Visible = False
    End Sub
    Private Sub dgvInvoice_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvInvoice.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        If e.CommandName = "Selecionar" Then
            If txtlinha.Text <> "" Then
                dgvInvoice.Rows(txtlinha.Text).CssClass = "Normal"

            End If
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")


            For i As Integer = 0 To dgvInvoice.Rows.Count - 1
                dgvInvoice.Rows(txtlinha.Text).CssClass = "Normal"

            Next

            dgvInvoice.Rows(txtlinha.Text).CssClass = "selected1"


        End If

    End Sub

    Private Sub btnFecharNovaInvoice_Click(sender As Object, e As EventArgs) Handles btnFecharNovaInvoice.Click
        limpaFormulario()
        RegistrosGrid()
        ModalPopupExtender2.Hide()
    End Sub

    Sub RegistrosGrid()
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
        End If



        dsInvoice.SelectCommand = "SELECT A.ID_ACCOUNT_INVOICE,A.NR_INVOICE,A.NM_ACCOUNT_TIPO_EMISSOR,A.NM_ACCOUNT_TIPO_FATURA,A.DT_INVOICE,B.NR_PROCESSO,B.NR_BL,A.NM_AGENTE,FL_CONFERIDO,A.NM_ACCOUNT_TIPO_INVOICE,A.SIGLA_MOEDA,A.DT_FECHAMENTO,A.DS_OBSERVACAO,(SELECT SUM(ISNULL(VL_TAXA_BR,0)) FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE = A.ID_ACCOUNT_INVOICE)VALOR_TOTAL FROM (SELECT * FROM FN_ACCOUNT_INVOICE('" & txtVencimentoInicial.Text & "','" & txtVencimentoFinal.Text & "')) AS A 
INNER JOIN TB_BL B ON B.ID_BL = A.ID_BL_INVOICE " & filtro

        dgvInvoice.DataBind()
        dgvInvoice.Visible = True
    End Sub


    Private Sub txtProc_ou_BL_TextChanged(sender As Object, e As EventArgs) Handles txtProc_ou_BL.TextChanged
        divErroInvoice.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT ID_BL,GRAU FROM TB_BL WHERE NR_PROCESSO = '" & txtProc_ou_BL.Text & "' OR NR_BL = '" & txtProc_ou_BL.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            txtID_BL.Text = ds.Tables(0).Rows(0).Item("ID_BL")
            txtGrau.Text = ds.Tables(0).Rows(0).Item("GRAU")

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

    Private Sub btnTaxasExterior_Click(sender As Object, e As EventArgs) Handles btnTaxasExterior.Click
        dsTaxasExterior.SelectCommand = "SELECT ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_TAXA,NM_ITEM_DESPESA,DT_RECEBIMENTO,CD_ORIGEM FROM FN_ACCOUNT_TAXAS_EXTERIOR (" & txtID_BL.Text & ", '" & txtGrau.Text & "')  WHERE ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS) AND ID_MOEDA =" & ddlMoeda.SelectedValue

        dgvTaxasExterior.DataBind()
        dgvTaxasExterior.Visible = True
        atualizaTotalExterior()
        ModalPopupExtender4.Show()
        ModalPopupExtender2.Show()
    End Sub
    Private Sub btnTaxasDeclaradas_Click(sender As Object, e As EventArgs) Handles btnTaxasDeclaradas.Click
        dsTaxasDeclaradas.SelectCommand = "SELECT ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_TAXA,NM_ITEM_DESPESA,DT_RECEBIMENTO,CD_DECLARADO FROM FN_ACCOUNT_TAXAS_DECLARADAS (" & txtID_BL.Text & ", '" & txtGrau.Text & "')  WHERE ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS) AND ID_MOEDA =" & ddlMoeda.SelectedValue

        dgvTaxasDeclaradas.DataBind()
        dgvTaxasDeclaradas.Visible = True
        atualizaTotalDeclaradas()
        ModalPopupExtender5.Show()
        ModalPopupExtender2.Show()

        'ModalPopupExtender4.Hide()
        'ModalPopupExtender3.Hide()
        'ModalPopupExtender1.Hide()
        'ModalPopupExtender6.Hide()
        'ModalPopupExtender7.Hide()
    End Sub

    Private Sub btnDevolucaoFrete_Click(sender As Object, e As EventArgs) Handles btnDevolucaoFrete.Click
        dsDevolucao.SelectCommand = "SELECT ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_COMPRA,VL_VENDA,DT_RECEBIMENTO FROM FN_ACCOUNT_DEVOLUCAO_FRETE (" & txtID_BL.Text & ", '" & txtGrau.Text & "') A WHERE ID_MOEDA =" & ddlMoeda.SelectedValue & " AND A.ID_BL NOT IN(SELECT ID_BL FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)"

        dgvDevolucao.DataBind()
        dgvDevolucao.Visible = True
        atualizaTotalFrete()
        ModalPopupExtender3.Show()
        ModalPopupExtender2.Show()
    End Sub


    Private Sub btnComissoes_Click(sender As Object, e As EventArgs) Handles btnComissoes.Click
        dsComissoes.SelectCommand = "SELECT  ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,SIGLA_MOEDA,VL_TAXA FROM  FN_ACCOUNT_DEVOLUCAO_COMISSAO (" & txtID_BL.Text & ", '" & txtGrau.Text & "') A WHERE  ID_MOEDA =" & ddlMoeda.SelectedValue & " AND A.ID_BL NOT IN(SELECT ID_BL FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)"

        dgvComissoes.DataBind()
        ModalPopupExtender6.Show()
        ModalPopupExtender2.Show()
    End Sub

    Private Sub btnOutrasTaxas_Click(sender As Object, e As EventArgs) Handles btnOutrasTaxas.Click
        dsOutrasTaxas.SelectCommand = "SELECT  ID_BL_TAXA,ID_MOEDA,ID_BL,NR_PROCESSO,NM_ITEM_DESPESA,SIGLA_MOEDA,VL_TAXA,CD_DECLARADO,DT_RECEBIMENTO FROM  FN_ACCOUNT_OUTRAS_TAXAS(" & txtID_BL.Text & ", '" & txtGrau.Text & "')  WHERE ID_BL_TAXA NOT IN(SELECT ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS  WHERE ID_BL_TAXA IS NOT NULL) AND ID_MOEDA =" & ddlMoeda.SelectedValue

        dgvOutrasTaxas.DataBind()
        ModalPopupExtender7.Show()
        ModalPopupExtender2.Show()
    End Sub



    Private Sub btnIncluirDevolucaoFrete_Click(sender As Object, e As EventArgs) Handles btnIncluirDevolucaoFrete.Click
        For Each linha As GridViewRow In dgvDevolucao.Rows
            Dim ID_BL As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim ValorCompra As Decimal = CType(linha.FindControl("lblValorCompra"), Label).Text
            Dim ValorVenda As Decimal = CType(linha.FindControl("lblValorVenda"), Label).Text

            If check.Checked Then
                Dim Con As New Conexao_sql
                Dim VALOR As Decimal = ValorVenda + ValorCompra
                Dim VALOR_STRING As String = VALOR.ToString
                VALOR_STRING = VALOR_STRING.ToString.Replace(",", ".")
                Con.Conectar()
                Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) VALUES
(" & txtIDInvoice.Text & "," & ID_BL & ",(SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & "), NULL,(SELECT  ID_ITEM_FRETE_ACCOUNT FROM TB_PARAMETROS)," & VALOR_STRING & ", 'DF')")
            End If
        Next

        dsDevolucao.DataBind()
        dsItensInvoice.DataBind()
    End Sub

    Private Sub btnIncluirTaxasExterior_Click(sender As Object, e As EventArgs) Handles btnIncluirTaxasExterior.Click
        For Each linha As GridViewRow In dgvTaxasExterior.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")

            If check.Checked Then
                Dim Con As New Conexao_sql
                Con.Conectar()
                Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) SELECT " & txtIDInvoice.Text & ",A.ID_BL,(SELECT ID_BL_MASTER FROM TB_BL B WHERE A.ID_BL = B.ID_BL),A.ID_BL_TAXA,A.ID_ITEM_DESPESA,VL_TAXA,'TE' FROM TB_BL_TAXA A WHERE A.ID_BL_TAXA =" & ID)
            End If
        Next
        dsTaxasExterior.DataBind()
        dsItensInvoice.DataBind()
    End Sub

    Private Sub btnIncluirTaxasDeclaradas_Click(sender As Object, e As EventArgs) Handles btnIncluirTaxasDeclaradas.Click
        For Each linha As GridViewRow In dgvTaxasDeclaradas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")

            If check.Checked Then
                Dim Con As New Conexao_sql
                Con.Conectar()
                Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) SELECT " & txtIDInvoice.Text & ",A.ID_BL,(SELECT ID_BL_MASTER FROM TB_BL B WHERE A.ID_BL = B.ID_BL),A.ID_BL_TAXA,A.ID_ITEM_DESPESA,VL_TAXA,'TD' FROM TB_BL_TAXA A WHERE A.ID_BL_TAXA =" & ID)
            End If
        Next

        dsTaxasDeclaradas.DataBind()
        dsItensInvoice.DataBind()
    End Sub

    Private Sub btnIncluirComissoes_Click(sender As Object, e As EventArgs) Handles btnIncluirComissoes.Click
        For Each linha As GridViewRow In dgvComissoes.Rows
            Dim ID_BL As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text

            If check.Checked Then
                Dim Con As New Conexao_sql
                Dim VALOR_STRING As String = VALOR.ToString
                VALOR_STRING = VALOR_STRING.ToString.Replace(",", ".")
                Con.Conectar()
                Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) VALUES
(" & txtIDInvoice.Text & "," & ID_BL & ",(SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & "), NULL,(SELECT  ID_ITEM_FRETE_ACCOUNT FROM TB_PARAMETROS)," & VALOR_STRING & ", 'CO')")
            End If


        Next
        dsComissoes.DataBind()
        dsItensInvoice.DataBind()
    End Sub

    Private Sub lkAvisoEmbarque_Click(sender As Object, e As EventArgs) Handles lkAvisoEmbarque.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione o registro que deseja editar!"
        Else
            Session("Vencimento_Inicial") = ""
            Session("Vencimento_Final") = ""

            Session("Vencimento_Inicial") = txtVencimentoInicial.Text
            Session("Vencimento_Final") = txtVencimentoFinal.Text

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "AvisoEmbarque()", True)
        End If

    End Sub


    Sub atualizaTotalFrete()
        Dim Con As New Conexao_sql
        lblValorFreteDevolucao.Text = 0
        lblValorFreteCompra.Text = 0
        lblValorFreteVenda.Text = 0
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

                lblValorFreteDevolucao.Text = ValorVenda + ValorCompra
            End If
            ModalPopupExtender3.Show()
            ModalPopupExtender2.Show()

        Next

    End Sub

    Sub atualizaTotalExterior()
        lblTotalExterior.Text = 0

        For Each linha As GridViewRow In dgvTaxasExterior.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text

            Dim Valor2 As Decimal = lblTotalExterior.Text

            If check.Checked Then
                lblTotalExterior.Text = Valor + Valor2
            End If
            ModalPopupExtender4.Show()
            ModalPopupExtender2.Show()
        Next
    End Sub
    Sub atualizaTotalDeclaradas()
        lblTotalDeclaradas.Text = 0

        For Each linha As GridViewRow In dgvTaxasDeclaradas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text

            Dim Valor2 As Decimal = lblTotalDeclaradas.Text

            If check.Checked Then
                lblTotalDeclaradas.Text = Valor + Valor2
            End If
            ModalPopupExtender5.Show()
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
        atualizaTotalFrete()
    End Sub

    Private Sub dgvTaxasExterior_Load(sender As Object, e As EventArgs) Handles dgvTaxasExterior.Load
        atualizaTotalExterior()
    End Sub

    Private Sub dgvComissoes_Load(sender As Object, e As EventArgs) Handles dgvComissoes.Load
        atualizaTotalComissoes()

    End Sub

    Private Sub dgvTaxasDeclaradas_Load(sender As Object, e As EventArgs) Handles dgvTaxasDeclaradas.Load
        atualizaTotalDeclaradas()

    End Sub

    Private Sub dgvOutrasTaxas_Load(sender As Object, e As EventArgs) Handles dgvOutrasTaxas.Load
        atualizaTotalOutrasTaxas()

    End Sub

    Private Sub btnIncluirOutrasTaxas_Click(sender As Object, e As EventArgs) Handles btnIncluirOutrasTaxas.Click
        For Each linha As GridViewRow In dgvOutrasTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")

            If check.Checked Then
                Dim Con As New Conexao_sql
                Con.Conectar()
                Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS(ID_ACCOUNT_INVOICE,ID_BL,ID_BL_MASTER,ID_BL_TAXA,ID_ITEM_DESPESA,VL_TAXA,CD_TIPO_DEVOLUCAO) SELECT " & txtIDInvoice.Text & ",A.ID_BL,(SELECT ID_BL_MASTER FROM TB_BL B WHERE A.ID_BL = B.ID_BL),A.ID_BL_TAXA,A.ID_ITEM_DESPESA,VL_TAXA,'OT' FROM TB_BL_TAXA A WHERE A.ID_BL_TAXA =" & ID)
            End If
        Next

        dsOutrasTaxas.DataBind()
        dsItensInvoice.DataBind()
    End Sub

    'Protected Sub dgvItensInvoice_DataBound(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim Total As Decimal = 0

    '    For Each row As GridViewRow In dgvItensInvoice.Rows
    '        Dim preco As Decimal = Decimal.Parse(row.Cells(2).Text)
    '        Dim qtde As Integer = Int32.Parse(row.Cells(3).Text)
    '        Total += preco * qtde
    '    Next

    '    Dim footer As GridViewRow = dgvItensInvoice.FooterRow
    '    footer.Cells(0).ColumnSpan = 3
    '    footer.Cells(0).HorizontalAlign = HorizontalAlign.Center
    '    footer.Cells.RemoveAt(2)
    '    footer.Cells.RemoveAt(1)
    '    footer.Cells(0).Text = String.Format("Total: {0:n2}", myStock)
    'End Sub
End Class