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
        divInfo.Visible = False
        divErro.Visible = False
        divSuccess.Visible = False

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
ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL,
VL_ALIQUOTA_ISS, VL_ALIQUOTA_PIS, VL_ALIQUOTA_COFINS
FROM [TB_PARCEIRO] A WHERE ID_PARCEIRO =" & ddlFornecedor.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                lblCidade.Text = ds.Tables(0).Rows(0).Item("NM_CIDADE")
                lblTipoFaturamento.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")
                lblDiasFaturamento.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO")
                lbl_COFINS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_COFINS")
                lbl_PIS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_PIS")
                lbl_ISS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS")

                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ID_BL,ID_BL_MASTER,GRAU, CASE WHEN GRAU = 'C' THEN (SELECT CASE WHEN DT_CHEGADA < GETDATE() THEN GETDATE() ELSE DT_CHEGADA END FROM TB_BL B WHERE B.ID_BL = A.ID_BL_MASTER) WHEN GRAU = 'M' AND DT_CHEGADA < GETDATE() THEN GETDATE() WHEN GRAU = 'M' THEN DT_CHEGADA END DT_CHEGADA
FROM [TB_BL] A WHERE A.ID_BL = " & txtID_BL.Text)
                Dim DATA As Date
                If ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 1 Then
                    txtVencimento.Text = Now.Date.ToString("dd-MM-yyyy")
                ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 2 Then
                    DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                    DATA = DATA.AddDays(lblDiasFaturamento.Text)
                ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 5 Then
                    DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                    DATA = DATA.AddMonths(1)

                    DATA = "11/" & DATA.Month & "/" & DATA.Year
                    DATA = FinalSemana(DATA)

                End If
                txtVencimento.Text = DATA


                ds1 = Con.ExecutarQuery("SELECT ID_SERVICO, ID_TIPO_ESTUFAGEM FROM TB_BL WHERE ID_BL = " & txtID_BL.Text)
                If ds1.Tables(0).Rows.Count > 0 Then

                    If Not IsDBNull(ds1.Tables(0).Rows(0).Item("ID_SERVICO")) And Not IsDBNull(ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then

                        If ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 1 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then

                            lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_FCL")
                            lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_FCL")
                            If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 2 Then

                                dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_MARITIMO_IMPO_FCL FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFrete.DataBind()
                                dgvMoedaFrete.Visible = True

                                dgvMoedaFreteArmador.Visible = False

                            ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 4 Then
                                dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_MARITIMO_IMPO_FCL FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFreteArmador.DataBind()
                                dgvMoedaFreteArmador.Visible = True

                                dgvMoedaFrete.Visible = False

                            End If

                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 1 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then

                            lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_LCL")
                            lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_LCL")
                            If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 2 Then
                                dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_MARITIMO_IMPO_LCL FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFrete.DataBind()
                                dgvMoedaFrete.Visible = True

                                dgvMoedaFreteArmador.Visible = False

                            ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 4 Then

                                dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_MARITIMO_IMPO_LCL FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFreteArmador.DataBind()
                                dgvMoedaFreteArmador.Visible = True

                                dgvMoedaFrete.Visible = False
                            End If

                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 4 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                            lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_FCL")
                            lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_FCL")

                            If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 2 Then
                                dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_MARITIMO_EXPO_FCL FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFrete.DataBind()
                                dgvMoedaFrete.Visible = True

                                dgvMoedaFreteArmador.Visible = False

                            ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 4 Then

                                dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_MARITIMO_EXPO_FCL FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFreteArmador.DataBind()
                                dgvMoedaFreteArmador.Visible = True

                                dgvMoedaFrete.Visible = False
                            End If

                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 4 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then

                            lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_LCL")
                            lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_LCL")

                            If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 2 Then
                                dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_MARITIMO_EXPO_LCL FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFrete.DataBind()
                                dgvMoedaFrete.Visible = True

                                dgvMoedaFreteArmador.Visible = False

                            ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 4 Then

                                dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_MARITIMO_EXPO_LCL FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFreteArmador.DataBind()
                                dgvMoedaFreteArmador.Visible = True

                                dgvMoedaFrete.Visible = False
                            End If

                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Then

                            lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")
                            lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_IMPO")

                            If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 2 Then
                                dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_AEREO_IMPO FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFrete.DataBind()
                                dgvMoedaFrete.Visible = True

                                dgvMoedaFreteArmador.Visible = False

                            ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 4 Then
                                dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_AEREO_IMPO FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFreteArmador.DataBind()
                                dgvMoedaFreteArmador.Visible = True

                                dgvMoedaFrete.Visible = False
                            End If

                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then

                            lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")
                            lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_EXPO")

                            If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 2 Then
                                dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_AEREO_EXPO FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFrete.DataBind()
                                dgvMoedaFrete.Visible = True

                                dgvMoedaFreteArmador.Visible = False


                            ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 4 Then
                                dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA,
VL_TXOFICIAL + (SELECT SPREAD_AEREO_EXPO FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlFornecedor.SelectedValue & ")'CAMBIO + SPREAD'
FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                dgvMoedaFreteArmador.DataBind()
                                dgvMoedaFreteArmador.Visible = True

                                dgvMoedaFrete.Visible = False
                            End If


                        End If

                    Else
                        divInfo.Visible = True

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
                Dim ItemDespesa As String = CType(linha.FindControl("lblItemDespesa"), Label).Text
                Dim valor As String = CType(linha.FindControl("lblValor"), Label).Text

                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL_TAXA)QTD FROM [TB_CONTA_PAGAR_RECEBER_ITENS] WHERE ID_BL_TAXA =" & ID)
                If ds1.Tables(0).Rows(0).Item("QTD") > 0 Then
                    lblErro.Text = "Há taxas já cadastradas em contas a receber"
                    divErro.Visible = True
                    Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & ID_CONTA_PAGAR_RECEBER)
                    Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER =" & ID_CONTA_PAGAR_RECEBER)
                Else
                    Dim dsDespesa As DataSet = Con.ExecutarQuery("SELECT ID_TIPO_ITEM_DESPESA,CD_TIPO_ITEM_DESPESA FROM TB_TIPO_ITEM_DESPESA WHERE ID_TIPO_ITEM_DESPESA = (SELECT ID_TIPO_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ItemDespesa & ") AND CD_TIPO_ITEM_DESPESA = 'R'")

                    If dsDespesa.Tables(0).Rows.Count > 0 Then
                        Dim ISS As String
                        Dim PIS As String
                        Dim COFINS As String


                        If lbl_ISS.Text > 0 Then
                            ISS = lbl_ISS.Text
                        Else
                            ISS = Session("VL_ALIQUOTA_ISS")
                        End If
                        ISS = valor * ISS
                        ISS = ISS.Replace(".", "")
                        ISS = ISS.Replace(",", ".")

                        If lbl_PIS.Text > 0 Then
                            PIS = lbl_PIS.Text
                        Else
                            PIS = Session("VL_ALIQUOTA_PIS")
                        End If
                        PIS = valor * PIS
                        PIS = PIS.Replace(".", "")
                        PIS = PIS.Replace(",", ".")

                        If lbl_COFINS.Text > 0 Then
                            COFINS = lbl_COFINS.Text
                        Else
                            COFINS = Session("VL_ALIQUOTA_COFINS")
                        End If
                        COFINS = valor * COFINS
                        COFINS = COFINS.Replace(".", "")
                        COFINS = COFINS.Replace(",", ".")


                        valor = valor.Replace(".", "")
                        valor = valor.Replace(",", ".")

                        Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,VL_ISS,VL_PIS,VL_COFINS )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO," & valor & "," & valor & ",ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA, " & ISS & "," & PIS & " ," & COFINS & " FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                    Else
                        valor = valor.Replace(".", "")
                        valor = valor.Replace(",", ".")
                        Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA  )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO," & valor & ", " & valor & ",ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                    End If

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
        divErro.Visible = False
        divSuccess.Visible = False

        Dim Con As New Conexao_sql
        Dim i As Integer = 0

        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Calculado As String = CType(linha.FindControl("lblCalculado"), Label).Text

            If check.Checked Then
                i = i + 1
                If Calculado = False Then
                    lblErro.Text = "O PROCESSO NECESSITA DE CÁLCULO"
                    divErro.Visible = True
                    btnCalcularRecebimento.Enabled = False
                    Exit Sub

                End If

            End If
        Next

        If i > 0 Then
            btnCalcularRecebimento.Enabled = True

        Else
            btnCalcularRecebimento.Enabled = False

        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("Financeiro.aspx")
    End Sub

    Public Function FinalSemana(ByVal data As Date)
        If data.DayOfWeek = DayOfWeek.Saturday Then
            data = DateAdd(DateInterval.Day, 2, data)
        ElseIf data.DayOfWeek = DayOfWeek.Sunday Then
            data = DateAdd(DateInterval.Day, 1, data)
        End If
        Return data.ToString("dd/MM/yyyy")
    End Function


End Class