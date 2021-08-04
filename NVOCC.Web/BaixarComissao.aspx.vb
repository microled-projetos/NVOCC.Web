Public Class BaixarComissao
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        txtID.Text = ""
        txtLinha.Text = ""
        divErro.Visible = False

        If txtCompetencia.Text = "" Then
            lblErro.Text = "É necessario informar a competência."
            divErro.Visible = True
        ElseIf txtQuinzena.Text = "" Then
            lblErro.Text = "É necessario informar a quinzena."
            divErro.Visible = True
        Else
            Dim filtro As String = " AND DT_COMPETENCIA = '" & txtCompetencia.Text & "' AND NR_QUINZENA = " & txtQuinzena.Text

            If ckStatus.Items.FindByValue(1).Selected And ckStatus.Items.FindByValue(2).Selected Then
                filtro &= " AND DT_CANCELAMENTO IS NULL"
            Else
                If ckStatus.Items.FindByValue(1).Selected Then
                    filtro &= " AND DT_LIQUIDACAO IS NULL AND DT_CANCELAMENTO IS NULL"
                End If

                If ckStatus.Items.FindByValue(2).Selected Then
                    filtro &= " AND DT_LIQUIDACAO IS NOT NULL AND DT_CANCELAMENTO IS NULL"
                End If
            End If




            dsPagar.SelectCommand = "SELECT * FROM [dbo].[View_Baixas_Comissoes] WHERE CD_PR =  'P' AND TP_EXPORTACAO = 'CINT' " & filtro & " ORDER BY DT_VENCIMENTO DESC, NR_FATURA_FORNECEDOR"
            dgvTaxasPagar.DataBind()
            gridPagar.Visible = True


        End If
    End Sub

    Private Sub btnSalvarBaixa_Click(sender As Object, e As EventArgs) Handles btnSalvarBaixa.Click
        divErroBaixa.Visible = False
        divSuccessBaixa.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtIDBaixa.Text = "" Then
            lblErroBaixa.Text = "É necessário selecionar um registro para efetuar a baixa!"
            divErroBaixa.Visible = True
        ElseIf txtLiquidacao.Text = "" Then
            lblErroBaixa.Text = "É necessário informar a data para efetuar a baixa!"
            divErroBaixa.Visible = True
        ElseIf txtContrato.Text = "" Then
            lblErroBaixa.Text = "É necessário informar o numero do contrato para efetuar a baixa!"
            divErroBaixa.Visible = True
        Else

            Dim dsVerificacao As DataSet = Con.ExecutarQuery("SELECT DT_LIQUIDACAO FROM TB_CONTA_PAGAR_RECEBER WHERE DT_LIQUIDACAO IS NOT NULL AND ID_CONTA_PAGAR_RECEBER =" & txtIDBaixa.Text)

            If dsVerificacao.Tables(0).Rows.Count > 0 And lblContador.Text = "" Then

                divInfo.Visible = True
                lblmsgInfo.Text = "REGISTRO JÁ LIQUIDADO!<br/>DESEJA SOBREPOR AS INFORMAÇÕES?"
                lblContador.Text = 1
                ModalPopupExtender1.Show()
                btnSalvarBaixa.Text = "Sobrepor Informações"
                Exit Sub

            Else
                lblContador.Text = ""
                btnSalvarBaixa.Text = "Baixar"
                divInfo.Visible = False
                Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtLiquidacao.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & ", NR_DOCUMENTO = '" & txtContrato.Text & "' WHERE ID_CONTA_PAGAR_RECEBER =" & txtIDBaixa.Text)

                For Each linhaMoeda As GridViewRow In dgvMoedas.Rows
                    Dim IDMoeda As String = CType(linhaMoeda.FindControl("lblMoeda"), Label).Text
                    Dim Cambio As String = CType(linhaMoeda.FindControl("txtValorCambio"), TextBox).Text

                    Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER_ITENS FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_MOEDA = " & IDMoeda & " AND ID_CONTA_PAGAR_RECEBER = " & txtIDBaixa.Text)

                    Cambio = Cambio.Replace(".", "")
                    Cambio = Cambio.Replace(",", ".")

                    If ds.Tables(0).Rows.Count > 0 Then
                        If Cambio = "" Then
                            lblErroBaixa.Text = "É necessário informar o valor de câmbio!"
                            divErroBaixa.Visible = True
                            ModalPopupExtender1.Show()
                            Exit Sub
                        Else
                            For Each linhads As DataRow In ds.Tables(0).Rows
                                Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER_ITENS] SET [DT_CAMBIO] = CONVERT(DATE,'" & txtLiquidacao.Text & "',103),VL_CAMBIO = " & Cambio & " ,VL_LANCAMENTO =  VL_TAXA_CALCULADO * " & Cambio & ",VL_LIQUIDO =  VL_TAXA_CALCULADO * " & Cambio & " WHERE ID_CONTA_PAGAR_RECEBER_ITENS =" & linhads.Item("ID_CONTA_PAGAR_RECEBER_ITENS").ToString())
                            Next
                        End If
                    End If
                Next

                dgvTaxasPagar.DataBind()


                Con.Fechar()
                lblSuccessBaixa.Text = "Baixa realizada com sucesso!"
                divSuccessBaixa.Visible = True
                txtCompetencia.Text = ""
            End If

        End If
        ModalPopupExtender1.Show()
    End Sub

    Private Sub dgvTaxasPagar_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxasPagar.RowCommand
        lblQuinzena.Text = ""
        lblCompetencia.Text = ""
        txtIDBaixa.Text = ""
        If e.CommandName = "Selecionar" Then
            If txtLinha.Text <> "" Then
                dgvTaxasPagar.Rows(txtLinha.Text).CssClass = "Normal"

            End If
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtLinha.Text = ID.Substring(ID.IndexOf("|"))
            txtLinha.Text = txtLinha.Text.Replace("|", "")

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CONTA_PAGAR_RECEBER,DT_COMPETENCIA,NR_QUINZENA FROM View_Baixas_Comissoes WHERE ID_CONTA_PAGAR_RECEBER = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")) Then
                    txtIDBaixa.Text &= ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_COMPETENCIA")) Then
                    lblCompetencia.Text &= "Competencia: " & ds.Tables(0).Rows(0).Item("DT_COMPETENCIA") & "<br/>"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_QUINZENA")) Then
                    lblQuinzena.Text &= "Quinzena: " & ds.Tables(0).Rows(0).Item("NR_QUINZENA") & "<br/>"
                End If
                ModalPopupExtender1.Show()
            End If
            Con.Fechar()

        End If

    End Sub
End Class