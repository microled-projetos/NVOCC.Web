Public Class CalcularRecebimento
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
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT NR_BL FROM TB_BL WHERE ID_BL = " & txtID_BL.Text)
                If ds1.Tables(0).Rows.Count > 0 Then
                    lblMBL.Text = ds1.Tables(0).Rows(0).Item("NR_BL")
                End If
                txtCambio.Text = Now.Date.ToString("dd-MM-yyyy")

            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub ddlFornecedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFornecedor.SelectedIndexChanged
        If ddlFornecedor.SelectedValue <> 0 Then
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT (SELECT NM_CIDADE FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE)NM_CIDADE,(SELECT NM_TIPO_FATURAMENTO FROM TB_TIPO_FATURAMENTO WHERE ID_TIPO_FATURAMENTO = A.ID_TIPO_FATURAMENTO)NM_TIPO_FATURAMENTO,ID_TIPO_FATURAMENTO,QT_DIAS_FATURAMENTO,SPREAD_AEREO_EXPO,SPREAD_AEREO_IMPO,SPREAD_MARITIMO_EXPO_FCL,SPREAD_MARITIMO_EXPO_LCL,SPREAD_MARITIMO_IMPO_FCL,SPREAD_MARITIMO_IMPO_LCL,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_AEREO)ACORDO_CAMBIO_AEREO,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL)ACORDO_CAMBIO_MARITIMO_EXPO_FCL,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL)ACORDO_CAMBIO_MARITIMO_EXPO_LCL,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL)ACORDO_CAMBIO_MARITIMO_IMPO_FCL,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL)ACORDO_CAMBIO_MARITIMO_IMPO_LCL,
ID_ACORDO_CAMBIO_AEREO,
ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL,
ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL,
ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL,
ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL 
FROM [TB_PARCEIRO] A WHERE ID_PARCEIRO =" & ddlFornecedor.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                lblCidade.Text = ds.Tables(0).Rows(0).Item("NM_CIDADE")
                lblTipoFaturamento.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")
                lblDiasFaturamento.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO")

                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ID_BL,ID_BL_MASTER,GRAU, CASE WHEN GRAU = 'C' THEN (SELECT CASE WHEN DT_CHEGADA < GETDATE() THEN GETDATE() ELSE DT_CHEGADA END FROM TB_BL B WHERE B.ID_BL = A.ID_BL_MASTER) WHEN GRAU = 'M' AND DT_CHEGADA < GETDATE() THEN GETDATE() WHEN GRAU = 'M' THEN DT_CHEGADA END DT_CHEGADA
FROM [TB_BL] A WHERE A.ID_BL == " & txtID_BL.Text)

                If ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 1 Then
                    txtVencimento.Text = Now.Date.ToString("dd-MM-yyyy")
                ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 2 Then
                    Dim DATA As Date = ds.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                    DATA = DATA.AddDays(lblDiasFaturamento.Text)
                ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 5 Then
                    Dim DATA As Date = ds.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                    DATA = DATA.AddDays(lblDiasFaturamento.Text)
                End If



                ds1 = Con.ExecutarQuery("SELECT ID_SERVICO, ID_TIPO_ESTUFAGEM FROM TB_BL WHERE ID_BL = " & txtID_BL.Text)
                If ds1.Tables(0).Rows.Count > 0 Then
                    If ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 1 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then

                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_FCL")
                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_FCL")
                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 2 Then
                            dgvMoedaFreteArmador.Visible = False
                            dgvMoedaFrete.Visible = True

                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 4 Then
                            dgvMoedaFreteArmador.Visible = True
                            dgvMoedaFrete.Visible = False
                        End If

                    ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 1 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then

                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_LCL")
                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_LCL")
                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 2 Then
                            dgvMoedaFreteArmador.Visible = False
                            dgvMoedaFrete.Visible = True

                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 4 Then
                            dgvMoedaFreteArmador.Visible = True
                            dgvMoedaFrete.Visible = False
                        End If

                    ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 4 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_FCL")
                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_FCL")

                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 2 Then
                            dgvMoedaFreteArmador.Visible = False
                            dgvMoedaFrete.Visible = True

                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 4 Then
                            dgvMoedaFreteArmador.Visible = True
                            dgvMoedaFrete.Visible = False
                        End If

                    ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 4 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then

                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_LCL")
                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_LCL")

                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 2 Then
                            dgvMoedaFreteArmador.Visible = False
                            dgvMoedaFrete.Visible = True

                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 4 Then
                            dgvMoedaFreteArmador.Visible = True
                            dgvMoedaFrete.Visible = False
                        End If

                    ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Then

                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")
                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_IMPO")

                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 2 Then
                            dgvMoedaFreteArmador.Visible = False
                            dgvMoedaFrete.Visible = True

                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 4 Then
                            dgvMoedaFreteArmador.Visible = True
                            dgvMoedaFrete.Visible = False
                        End If

                    ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then

                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")
                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_EXPO")

                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 2 Then
                            dgvMoedaFreteArmador.Visible = False
                            dgvMoedaFrete.Visible = True

                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 4 Then
                            dgvMoedaFreteArmador.Visible = True
                            dgvMoedaFrete.Visible = False
                        End If


                    End If
                End If


            End If


            dsTaxas.SelectCommand = "SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE (ID_BL = " & txtID_BL.Text & " OR ID_BL_MASTER = " & txtID_BL.Text & ") AND CD_PR = 'R' AND ID_PARCEIRO_EMPRESA = " & ddlFornecedor.SelectedValue
            dgvTaxas.DataBind()


            dsTaxas.SelectParameters("ID_BL").DefaultValue = txtID_BL.Text
            dsTaxas.SelectParameters("ID_PARCEIRO_EMPRESA").DefaultValue = ddlFornecedor.SelectedValue
            dgvTaxas.DataBind()

            divConteudo.Visible = True

        End If
    End Sub



    Private Sub btnCalcularRecebimento_Click(sender As Object, e As EventArgs) Handles btnCalcularRecebimento.Click
        divErro.Visible = False
        divSuccess.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        ds = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA,ID_USUARIO_LANCAMENTO,CD_PR) VALUES (GETDATE(),CONVERT(DATE, '" & txtVencimento.Text & "',103),1," & Session("ID_USUARIO") & ",'R')  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER  ")
        Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            If check.Checked Then
                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                Dim valor As String = CType(linha.FindControl("lblValor"), Label).Text
                valor = valor.Replace(".", "")
                valor = valor.Replace(",", ".")
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL_TAXA)QTD FROM [TB_CONTA_PAGAR_RECEBER_ITENS] WHERE ID_BL_TAXA =" & ID)
                If ds1.Tables(0).Rows(0).Item("QTD") > 0 Then
                    lblErro.Text = "Há taxas já cadastradas em contas a pagar"
                    divErro.Visible = True
                Else
                    Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_LANCAMENTO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA  )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO," & valor & ",ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                End If
            End If
        Next
        Con.Fechar()
        lblSuccess.Text = "Montagem realizada com sucesso!"
        divSuccess.Visible = True
        txtVencimento.Text = ""
        ddlFornecedor.SelectedValue = 0
        dgvTaxas.DataBind()

    End Sub

    Private Sub dgvTaxas_Load(sender As Object, e As EventArgs) Handles dgvTaxas.Load
        Dim Con As New Conexao_sql
        Dim i As Integer = 0

        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            If check.Checked Then
                i = i + 1
            End If
        Next

        If i > 0 Then
            btnCalcularRecebimento.Enabled = True

        Else
            btnCalcularRecebimento.Enabled = False

        End If
    End Sub

End Class