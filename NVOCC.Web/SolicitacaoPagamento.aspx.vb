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
                    txtVencimento.Text = Now.Date.ToString("dd/MM/yyyy")
                    If Not IsDBNull(ds1.Tables(0).Rows(0).Item("GRAU")) Then

                        If ds1.Tables(0).Rows(0).Item("GRAU") = "M" Then
                            lblMBL.Text = ds1.Tables(0).Rows(0).Item("NR_BL")
                        ElseIf ds1.Tables(0).Rows(0).Item("GRAU") = "C" Then
                            lblMBL.Text = ds1.Tables(0).Rows(0).Item("NR_BL_MASTER")
                            lblID_MBL.Text = ds1.Tables(0).Rows(0).Item("ID_BL_MASTER")

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
WHERE  (ID_BL_MASTER = " & lblID_MBL.Text & ") AND CD_PR = 'P' AND ID_PARCEIRO_EMPRESA = " & ddlFornecedor.SelectedValue
            dgvTaxas.DataBind()


            dsTaxas.SelectParameters("ID_BL").DefaultValue = lblID_MBL.Text
            dgvTaxas.DataBind()
            divgrids.Visible = True

            For Each linha As GridViewRow In dgvTaxas.Rows
                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                Dim valor As String = CType(linha.FindControl("lblValor"), Label).Text
                Dim valor2 As Decimal = lblTotal.Text
                Dim Calculado As String = CType(linha.FindControl("lblCalculado"), Label).Text

                If check.Checked Then
                    lblTotal.Text = valor2 + valor
                    If Calculado = False Then
                        lblErro.Text = "TAXA NECESSITA DE CÁLCULO"
                        divErro.Visible = True
                        check.Checked = False
                        check.Enabled = False

                    End If
                End If


            Next
            Dim Con As New Conexao_sql
            Con.Conectar()


            Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_TRANSPORTADOR FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("FL_TRANSPORTADOR") = True Then
                    dgvMoedasArmador.Visible = True
                    dgvMoedas.Visible = False
                Else
                    dgvMoedasArmador.Visible = False
                    dgvMoedas.Visible = True

                End If
            End If
        End If

    End Sub

    Private Sub dgvTaxas_Load(sender As Object, e As EventArgs) Handles dgvTaxas.Load
        AtualizaTotal()
    End Sub

    Sub AtualizaTotal()
        lblTotal.Text = 0
        Dim i As Integer = 0

        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text
            Dim valor2 As Decimal = lblTotal.Text
            Dim Calculado As String = CType(linha.FindControl("lblCalculado"), Label).Text

            If check.Checked Then
                lblTotal.Text = valor2 + valor
                If Calculado = False Then
                    lblErro.Text = "TAXA NECESSITA DE CÁLCULO"
                    divErro.Visible = True
                    check.Checked = False
                    check.Enabled = False

                End If
            End If

        Next
        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "SalvaPosicao()", True)

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
            'lblTotal.Text = 0
            'txtVencimento.Text = ""
            'dgvTaxas.DataBind()
            mpeMontagem.Show()

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
                Dim ValorCambio As String

                If moeda = 124 Then
                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA]  SET [VL_TAXA_BR] = VL_TAXA_CALCULADO,DT_ATUALIZACAO_CAMBIO = GETDATE() WHERE ID_BL_TAXA =" & ID)
                Else
                    Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_TRANSPORTADOR FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue)
                    If ds.Tables(0).Rows.Count > 0 Then
                        If ds.Tables(0).Rows(0).Item("FL_TRANSPORTADOR") = True Then
                            For Each linhaMoedas As GridViewRow In dgvMoedasArmador.Rows
                                Dim MoedaFrete As String = CType(linhaMoedas.FindControl("lblMoedaFrete"), Label).Text

                                If MoedaFrete = moeda Then
                                    ValorCambio = CType(linhaMoedas.FindControl("txtValorCambio"), TextBox).Text
                                    ValorCambio = ValorCambio.Replace(".", "")
                                    ValorCambio = ValorCambio.Replace(",", ".")


                                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA]  SET [VL_TAXA_BR] = VL_TAXA_CALCULADO * " & ValorCambio & ",DT_ATUALIZACAO_CAMBIO = GETDATE(),VL_CAMBIO = " & ValorCambio & " WHERE ID_BL_TAXA =" & ID)
                                End If

                            Next

                        Else


                            For Each linhaMoedas As GridViewRow In dgvMoedas.Rows
                                Dim MoedaFrete As String = CType(linhaMoedas.FindControl("lblMoedaFrete"), Label).Text

                                If MoedaFrete = moeda Then
                                    ValorCambio = CType(linhaMoedas.FindControl("txtValorCambio"), TextBox).Text
                                    ValorCambio = ValorCambio.Replace(".", "")
                                    ValorCambio = ValorCambio.Replace(",", ".")


                                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA]  SET [VL_TAXA_BR] = VL_TAXA_CALCULADO * " & ValorCambio & ",DT_ATUALIZACAO_CAMBIO = GETDATE(), VL_CAMBIO = " & ValorCambio & " WHERE ID_BL_TAXA =" & ID)
                                End If

                            Next
                        End If


                    End If
                End If

            End If
        Next
        Con.Fechar()
        dsTaxas.SelectParameters("ID_BL").DefaultValue = txtID_BL.Text
        dsTaxas.SelectParameters("ID_EMPRESA").DefaultValue = ddlFornecedor.SelectedValue

        dgvTaxas.DataBind()
        AtualizaTotal()
        lblSuccess.Text = "Valores atualizados com sucesso!"
        divSuccess.Visible = True
    End Sub


    Private Sub btnDesmarcar_Click(sender As Object, e As EventArgs) Handles btnDesmarcar.Click
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvTaxas.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = False
        Next
        AtualizaTotal()
        divErro.Visible = False
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvTaxas.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
            Dim valor As Double = CType(Me.dgvTaxas.Rows(i).FindControl("lblValor"), Label).Text
        Next
        AtualizaTotal()

    End Sub

    Private Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Session("VENCIMENTO") = txtVencimento.Text
        Response.Redirect("MontagemPagamento.aspx?f=" & ddlFornecedor.SelectedValue)

    End Sub

    Private Sub btnNao_Click(sender As Object, e As EventArgs) Handles btnNao.Click
        Response.Redirect("Financeiro.aspx")
    End Sub
End Class