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
            Dim filtro As String = " AND COMPETENCIA = '" & txtCompetencia.Text & "' AND NR_QUINZENA = " & txtQuinzena.Text

            'If ckStatus.Items.FindByValue(1).Selected And ckStatus.Items.FindByValue(2).Selected Then
            '    filtro &= " AND DT_CANCELAMENTO IS NULL"
            'Else
            '    If ckStatus.Items.FindByValue(1).Selected Then
            '        filtro &= " AND DT_LIQUIDACAO IS NULL AND DT_CANCELAMENTO IS NULL"
            '    End If

            '    If ckStatus.Items.FindByValue(2).Selected Then
            '        filtro &= " AND DT_LIQUIDACAO IS NOT NULL AND DT_CANCELAMENTO IS NULL"
            '    End If
            'End If




            dsPagar.SelectCommand = "SELECT * FROM [dbo].[View_Comissao_Internacional] WHERE DT_EXPORTACAO IS NULL " & filtro
            dgvTaxasPagar.DataBind()
            gridPagar.Visible = True


        End If
    End Sub

    Private Sub btnSalvarBaixa_Click(sender As Object, e As EventArgs) Handles btnSalvarBaixa.Click
        divErroBaixa.Visible = False
        divSuccessBaixa.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtIDBaixa.Text = "" Then
            lblErroBaixa.Text = "É necessário selecionar um registro para efetuar a baixa!"
            divErroBaixa.Visible = True
            ModalPopupExtender1.Show()

        ElseIf txtLiquidacao.Text = "" Then
            lblErroBaixa.Text = "É necessário informar a data de liquidação para efetuar a baixa!"
            divErroBaixa.Visible = True
            ModalPopupExtender1.Show()

        ElseIf txtContrato.Text = "" Then
            lblErroBaixa.Text = "É necessário informar o numero do contrato para efetuar a baixa!"
            divErroBaixa.Visible = True
            ModalPopupExtender1.Show()

        ElseIf ddlContaBancaria.SelectedValue = 0 Then
            lblErroBaixa.Text = "É necessário informar a conta bancária para efetuar a baixa!"
            divErroBaixa.Visible = True
            ModalPopupExtender1.Show()

        Else

            GravaCCProcesso()

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
                lblSuccess.Text = "Baixa realizada com sucesso!"
                divSuccess.Visible = True
                txtCompetencia.Text = ""
                txtQuinzena.Text = ""

                ModalPopupExtender1.Hide()

            End If

        End If
    End Sub

    Sub GravaCCProcesso()

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (CD_PR, DT_COMPETENCIA,NR_QUINZENA, DT_LANCAMENTO ,DT_VENCIMENTO,ID_CONTA_BANCARIA,ID_TIPO_LANCAMENTO_CAIXA  ,
        ID_USUARIO_LANCAMENTO ,TP_EXPORTACAO) VALUES('P','" & txtCompetencia.Text & "','" & txtQuinzena.Text & "',GETDATE(),GETDATE()," & ddlContaBancaria.SelectedValue & ",7, " & Session("ID_USUARIO") & ", 'CINT')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER")
        Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")

        Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_BL,ID_MOEDA,ID_PARCEIRO_EMPRESA,DS_HISTORICO_LANCAMENTO,ID_CONTA_PAGAR_RECEBER, VL_LANCAMENTO ,VL_LIQUIDO,VL_TAXA_CALCULADO,ID_ITEM_DESPESA )
                        SELECT ID_BL,ID_MOEDA,ID_PARCEIRO_VENDEDOR,'COMISSÃO INDICADOR INTERNACIONAL – " & txtCompetencia.Text & "-" & txtQuinzena.Text & "'," & ID_CONTA_PAGAR_RECEBER & ",0,0, VL_COMISSAO,(SELECT ID_ITEM_INDICADOR_INTERNACIONAL FROM TB_PARAMETROS)ID_ITEM_INDICADOR_INTERNACIONAL FROM TB_DETALHE_COMISSAO_INTERNACIONAL WHERE ID_CABECALHO_COMISSAO_INTERNACIONAL IN (SELECT ID_CABECALHO_COMISSAO_INTERNACIONAL FROM View_Comissao_Internacional WHERE COMPETENCIA = '" & txtCompetencia.Text & "' AND NR_QUINZENA = '" & txtQuinzena.Text & "' AND ID_CABECALHO_COMISSAO_INTERNACIONAL = " & txtIDBaixa.Text & " ) ")

        Con.ExecutarQuery("UPDATE TB_CABECALHO_COMISSAO_INTERNACIONAL SET DT_EXPORTACAO = GETDATE(),ID_USUARIO_EXPORTACAO = " & Session("ID_USUARIO") & " WHERE DT_COMPETENCIA = '" & txtCompetencia.Text.Substring(0, 2) & txtCompetencia.Text.Substring(3, 4) & "' AND NR_QUINZENA = '" & txtQuinzena.Text & "' AND ID_CABECALHO_COMISSAO_INTERNACIONAL = " & txtIDBaixa.Text)

        txtIDBaixa.Text = ID_CONTA_PAGAR_RECEBER

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
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CABECALHO_COMISSAO_INTERNACIONAL,COMPETENCIA,NR_QUINZENA FROM View_Comissao_Internacional WHERE ID_CABECALHO_COMISSAO_INTERNACIONAL = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INTERNACIONAL")) Then
                    txtIDBaixa.Text &= ds.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_INTERNACIONAL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("COMPETENCIA")) Then
                    lblCompetencia.Text &= "Competencia: " & ds.Tables(0).Rows(0).Item("COMPETENCIA") & "<br/>"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_QUINZENA")) Then
                    lblQuinzena.Text &= "Quinzena: " & ds.Tables(0).Rows(0).Item("NR_QUINZENA") & "<br/>"
                End If
                ModalPopupExtender1.Show()
            End If
            Con.Fechar()

        End If

    End Sub

    Private Sub btnFecharBaixa_Click(sender As Object, e As EventArgs) Handles btnFecharBaixa.Click
        txtCompetencia.Text = ""
        txtQuinzena.Text = ""
        txtIDBaixa.Text = ""
        txtContrato.Text = ""
        txtLiquidacao.Text = ""
        ddlContaBancaria.SelectedValue = 0

        ModalPopupExtender1.Hide()
    End Sub
End Class