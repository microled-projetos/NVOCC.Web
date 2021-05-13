Public Class SolicitacaoPagamento
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
            If Request.QueryString("id") <> "" Then
                txtID_BL.Text = Request.QueryString("id")
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ID_BL,NR_BL,GRAU,ID_BL_MASTER, (SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)NR_BL_MASTER FROM TB_BL A WHERE ID_BL = " & txtID_BL.Text)
                If ds1.Tables(0).Rows.Count > 0 Then

                    If Not IsDBNull(ds1.Tables(0).Rows(0).Item("GRAU")) Then

                        If ds1.Tables(0).Rows(0).Item("GRAU") = "M" Then
                            lblMBL.Text = ds1.Tables(0).Rows(0).Item("NR_BL")
                        ElseIf ds1.Tables(0).Rows(0).Item("GRAU") = "C" Then
                            lblMBL.Text = ds1.Tables(0).Rows(0).Item("NR_BL_MASTER")
                        End If

                    End If
                End If
            End If
        End If
        Con.Fechar()


    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("Financeiro.aspx")
    End Sub

    Private Sub ddlFornecedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFornecedor.SelectedIndexChanged
        If ddlFornecedor.SelectedValue <> 0 Then
            dsTaxas.SelectCommand = "SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE  (ID_BL = " & txtID_BL.Text & " OR ID_BL_MASTER = " & txtID_BL.Text & ") AND CD_PR = 'P' AND ID_PARCEIRO_EMPRESA = " & ddlFornecedor.SelectedValue
            dgvTaxas.DataBind()


            dsTaxas.SelectParameters("ID_BL").DefaultValue = txtID_BL.Text
            dgvTaxas.DataBind()
        Else
            dgvTaxas.DataBind()
        End If

    End Sub

    Private Sub dgvTaxas_Load(sender As Object, e As EventArgs) Handles dgvTaxas.Load
        Dim Con As New Conexao_sql
        lblTotal.Text = 0
        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim valor As String = CType(linha.FindControl("lblValor"), Label).Text
            Dim valor2 As Double = lblTotal.Text

            If check.Checked Then
                lblTotal.Text = valor2 + valor
            End If
        Next

    End Sub

    Private Sub btnSolicitar_Click(sender As Object, e As EventArgs) Handles btnSolicitar.Click
        divErro.Visible = False
        divSuccess.Visible = False

        If txtVencimento.Text = "" Then
            lblErro.Text = "É necessário informar a Data de Vencimento!"
            divErro.Visible = True
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            For Each linha As GridViewRow In dgvTaxas.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA] SET [DT_SOLICITACAO_PAGAMENTO] = CONVERT(DATE,'" & txtVencimento.Text & "',103) WHERE ID_BL_TAXA =" & ID)
                End If
            Next
            Con.Fechar()
            lblSuccess.Text = "Solicitação realizada com sucesso!"
            divSuccess.Visible = True
            lblTotal.Text = 0
            txtVencimento.Text = ""
            dgvTaxas.DataBind()

        End If


    End Sub

    Private Sub btnAtualizaValor_Click(sender As Object, e As EventArgs) Handles btnAtualizaValor.Click
        divErro.Visible = False
        divSuccess.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()

        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            If check.Checked Then
                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                Dim moeda As String = CType(linha.FindControl("lblMoeda"), Label).Text
                Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_MOEDA_FRETE_ARMADOR FROM TB_MOEDA_FRETE_ARMADOR WHERE DT_CAMBIO =(SELECT MAX(DT_CAMBIO) FROM TB_MOEDA_FRETE_ARMADOR WHERE ID_MOEDA = " & moeda & ") AND ID_MOEDA = " & moeda)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim ID_MOEDA_FRETE_ARMADOR As String = ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE_ARMADOR")
                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA]  SET [VL_TAXA_BR] = VL_TAXA_CALCULADO * (SELECT VL_TXOFICIAL FROM TB_MOEDA_FRETE_ARMADOR WHERE ID_MOEDA_FRETE_ARMADOR = " & ID_MOEDA_FRETE_ARMADOR & "),DT_ATUALIZACAO_CAMBIO = GETDATE() WHERE ID_BL_TAXA =" & ID)
                End If

            End If
        Next
        Con.Fechar()
        dgvTaxas.DataBind()
        lblSuccess.Text = "Valores atualizados com sucesso!"
        divSuccess.Visible = True
    End Sub

End Class