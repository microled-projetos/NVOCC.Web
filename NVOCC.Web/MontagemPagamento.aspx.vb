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
            txtVencimento.Text = Now.Date.ToString("dd-MM-yyyy")
            txtDataFatura.Text = Now.Date.ToString("dd-MM-yyyy")
            ckbBaixaAutomatica.Checked = True
        End If
        Con.Fechar()

    End Sub

    Private Sub btnDesmarcar_Click(sender As Object, e As EventArgs) Handles btnDesmarcar.Click
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvTaxas.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = False
            txtValor.Text = 0
        Next
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        txtValor.Text = 0
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvTaxas.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
            Dim valor As Double = CType(Me.dgvTaxas.Rows(i).FindControl("lblValor"), Label).Text
            txtValor.Text = txtValor.Text + valor
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


            'dsTaxas.SelectParameters("DATA").DefaultValue = txtVencimentoBusca.Text
            'dsTaxas.SelectParameters("ID_PARCEIRO_EMPRESA").DefaultValue = ddlFornecedor.SelectedValue






            'dgvTaxas.DataBind()
            divgrid.Visible = True
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
            Dim valor As String = CType(linha.FindControl("lblValor"), Label).Text
            Dim valor2 As Double = txtValor.Text

            If check.Checked Then
                txtValor.Text = valor2 + valor
            End If
        Next

    End Sub

    Private Sub btnMontar_Click(sender As Object, e As EventArgs) Handles btnMontar.Click
        divErro.Visible = False
        divSuccess.Visible = False

        If txtVencimento.Text = "" Then
            lblErro.Text = "É necessário informar a Data de Vencimento!"
            divErro.Visible = True
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
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Dim valor As String = CType(linha.FindControl("lblValor"), Label).Text
                    valor = valor.Replace(".", "")
                    valor = valor.Replace(",", ".")
                    Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL_TAXA)QTD FROM [TB_CONTA_PAGAR_RECEBER_ITENS] A
INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER
WHERE DT_CANCELAMENTO IS NULL AND ID_BL_TAXA =" & ID)
                    If ds1.Tables(0).Rows(0).Item("QTD") > 0 Then
                        lblErro.Text = "Há taxas já cadastradas em contas a pagar"
                        divErro.Visible = True
                    Else
                        Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA  )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO,VL_CAMBIO," & valor & ",ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                    End If
                End If
            Next
            Con.Fechar()
            lblSuccess.Text = "Montagem realizada com sucesso!"
            divSuccess.Visible = True
            txtValor.Text = 0
            txtVencimento.Text = ""
            dgvTaxas.DataBind()


        End If

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("Financeiro.aspx")
    End Sub
End Class