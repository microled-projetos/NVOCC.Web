Public Class MontagemPagamento
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
            If Not Page.IsPostBack Then
                If Request.QueryString("f") <> 0 Then
                    txtVencimentoBusca.Text = Session("VENCIMENTO")
                    txtVencimento.Text = Session("VENCIMENTO")
                    dsFornecedor.SelectCommand = "SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN (SELECT ID_PARCEIRO_EMPRESA FROM dbo.TB_BL_TAXA WHERE CD_PR = 'P' AND DT_SOLICITACAO_PAGAMENTO = CONVERT(DATE,'" & txtVencimentoBusca.Text & "',103) )
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"
                    dsFornecedor.DataBind()
                    ddlFornecedor.SelectedValue = Request.QueryString("f")

                Else

                    txtVencimentoBusca.Text = Now.Date.ToString("dd-MM-yyyy")
                    txtVencimento.Text = Now.Date.ToString("dd-MM-yyyy")
                    txtDataFatura.Text = Now.Date.ToString("dd-MM-yyyy")
                    dsFornecedor.SelectCommand = "SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN (SELECT ID_PARCEIRO_EMPRESA FROM dbo.TB_BL_TAXA WHERE CD_PR = 'P' AND DT_SOLICITACAO_PAGAMENTO = CONVERT(DATE,'" & txtVencimentoBusca.Text & "',103) )
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"
                    dsFornecedor.DataBind()
                End If

            End If

        End If
        Con.Fechar()

    End Sub

    Private Sub btnDesmarcar_Click(sender As Object, e As EventArgs) Handles btnDesmarcar.Click
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvTaxas.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = False
            txtValor.Text = 0
        Next
        txtValor.Text = 0
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        txtValor.Text = 0
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvTaxas.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
            Dim valor As Decimal = CType(Me.dgvTaxas.Rows(i).FindControl("lblValor"), Label).Text
            Dim valor2 As Decimal = valor + valor2
            txtValor.Text = valor2
        Next
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If ddlFornecedor.SelectedValue <> 0 And txtVencimentoBusca.Text <> "" Then
            Dim filtro As String = ""

            If txtMaster.Text <> "" Then
                filtro &= " AND NR_BL_MASTER LIKE '%" & txtMaster.Text & "%'"

            End If


            If txtHouse.Text <> "" Then
                filtro &= " AND NR_BL LIKE '%" & txtHouse.Text & "%' AND GRAU = 'C' AND ID_BL_MASTER IS NOT NULL"

            End If


            If txtProcesso.Text <> "" Then
                filtro &= " AND NR_PROCESSO LIKE '%" & txtProcesso.Text & "%'"

            End If


            dsTaxas.SelectCommand = "SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE CD_PR= 'P' AND ID_PARCEIRO_EMPRESA = " & ddlFornecedor.SelectedValue & "AND DT_SOLICITACAO_PAGAMENTO = CONVERT(DATE, '" & txtVencimentoBusca.Text & "', 103) " & filtro


            dgvTaxas.DataBind()
            ' divgrid.Visible = True


            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("select convert(decimal(18,2),ISNULL(VL_ALIQUOTA_ISS,0))VL_ALIQUOTA_ISS FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue)
            If ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS") > 0 Then
                lbl_ISS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS")
                lbl_ISS.Text = lbl_ISS.Text.Replace(".", "")
                lbl_ISS.Text = lbl_ISS.Text.Replace(",", ".")
            Else
                lbl_ISS.Text = 3
            End If

        Else
            lblErro.Text = "É necessário informar o fornecedor e data de vencimento"
            divErro.Visible = True
        End If
    End Sub
    Private Sub dgvTaxas_Load(sender As Object, e As EventArgs) Handles dgvTaxas.Load
        Dim Con As New Conexao_sql
        txtValor.Text = 0
        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim valor As Double = CType(linha.FindControl("lblValor"), Label).Text
            Dim valor2 As Double = txtValor.Text
            Dim checkISS As CheckBox = linha.FindControl("ckbISS")
            If check.Checked Then
                'If checkISS.Checked Then
                '    Dim iss As Double = valor / 100
                '    iss = iss * lbl_ISS.Text


                '    valor = valor - iss

                '    valor = valor * 100
                '    valor = Math.Truncate(valor)
                '    valor = valor / 100
                'End If

                'txtValor.Text = valor2 + valor
                'txtValor.Text = FormatNumber(txtValor.Text, 2)

                If checkISS.Checked Then
                    Dim iss As Decimal = valor / 100
                    iss = iss * lbl_ISS.Text
                    valor = valor - iss

                End If

                txtValor.Text = valor2 + valor
                txtValor.Text = FormatCurrency(txtValor.Text)

            End If
        Next

    End Sub

    Private Sub btnMontar_Click(sender As Object, e As EventArgs) Handles btnMontar.Click
        divErro.Visible = False
        divSuccess.Visible = False

        If txtVencimento.Text = "" Then
            lblErro.Text = "É necessário informar a Data de Vencimento!"
            divErro.Visible = True
            Exit Sub
        ElseIf txtNumeroFatura.Text = "" Then
            lblErro.Text = "É necessário informar número da fatura!"
            divErro.Visible = True
            Exit Sub
        ElseIf txtValor.Text = "" Or txtValor.Text = 0 Then
            lblErro.Text = "É necessário ter um valor para montagem de pagamento"
            divErro.Visible = True
            Exit Sub

        Else
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet
            If ckbBaixaAutomatica.Checked = True Then
                ds = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,DT_LIQUIDACAO,DT_FATURA_FORNECEDOR,ID_CONTA_BANCARIA,ID_USUARIO_LANCAMENTO,CD_PR,NR_FATURA_FORNECEDOR) VALUES (GETDATE(),CONVERT(DATE, '" & txtVencimento.Text & "',103),CONVERT(DATE, '" & txtVencimento.Text & "',103),CONVERT(DATE, '" & txtDataFatura.Text & "',103)," & ddlContaBancaria.SelectedValue & "," & Session("ID_USUARIO") & ",'P','" & txtNumeroFatura.Text & "') Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER ")

            Else
                ds = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,DT_FATURA_FORNECEDOR,ID_CONTA_BANCARIA,ID_USUARIO_LANCAMENTO,CD_PR,NR_FATURA_FORNECEDOR) VALUES (GETDATE(),CONVERT(DATE, '" & txtVencimento.Text & "',103),CONVERT(DATE, '" & txtDataFatura.Text & "',103)," & ddlContaBancaria.SelectedValue & "," & Session("ID_USUARIO") & ",'P','" & txtNumeroFatura.Text & "')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER  ")
            End If
            Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")

            For Each linha As GridViewRow In dgvTaxas.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                Dim checkISS As CheckBox = linha.FindControl("ckbISS")
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL_TAXA)QTD FROM [TB_CONTA_PAGAR_RECEBER_ITENS] A
INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER
WHERE DT_CANCELAMENTO IS NULL AND ID_BL_TAXA =" & ID)
                    If ds1.Tables(0).Rows(0).Item("QTD") > 0 Then
                        lblErro.Text = "Há taxas já cadastradas em contas a pagar"
                        divErro.Visible = True
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & ID_CONTA_PAGAR_RECEBER)
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER =" & ID_CONTA_PAGAR_RECEBER)
                        Exit Sub
                    Else

                        If checkISS.Checked Then
                            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,VL_ISS,FL_ABATER_ISS )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO,VL_CAMBIO,VL_TAXA_BR ,VL_TAXA_BR - ((VL_TAXA_BR/100)* " & lbl_ISS.Text & ") ,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,(VL_TAXA_BR/100)* " & lbl_ISS.Text & ",1 FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                        Else
                            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,VL_ISS )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO,VL_CAMBIO,VL_TAXA_BR ,VL_TAXA_BR ,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,(VL_TAXA_BR/100)* " & lbl_ISS.Text & " FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                        End If

                    End If
                End If
            Next
            Con.Fechar()
            lblSuccess.Text = "Montagem realizada com sucesso!"
            divSuccess.Visible = True
            txtValor.Text = 0
            txtNumeroFatura.Text = ""
            txtVencimento.Text = ""
            dgvTaxas.DataBind()

            'Dim finaliza As New FinalizaCotacao
            'finaliza.Finalizar()
        End If

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("Financeiro.aspx")
    End Sub

    Private Sub txtVencimentoBusca_TextChanged(sender As Object, e As EventArgs) Handles txtVencimentoBusca.TextChanged
        dsFornecedor.SelectCommand = "SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN (SELECT ID_PARCEIRO_EMPRESA FROM dbo.TB_BL_TAXA WHERE CD_PR = 'P' AND DT_SOLICITACAO_PAGAMENTO = CONVERT(DATE,'" & txtVencimentoBusca.Text & "',103) ) union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO"
        dsFornecedor.DataBind()
    End Sub




End Class