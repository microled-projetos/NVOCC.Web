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
                Session("FORNECEDOR") = ddlFornecedor.SelectedValue
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_CIDADE")) Then
                    lblCidade.Text = ds.Tables(0).Rows(0).Item("NM_CIDADE")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")) Then
                    lblTipoFaturamento.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO")) Then
                    lblDiasFaturamento.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_COFINS")) Then
                    lbl_COFINS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_COFINS")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_PIS")) Then
                    lbl_PIS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_PIS")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS")) Then
                    lbl_ISS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS")
                End If

                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ID_BL,ID_BL_MASTER,GRAU, CASE WHEN GRAU = 'C' THEN (SELECT CASE WHEN DT_CHEGADA < GETDATE() THEN GETDATE() ELSE DT_CHEGADA END FROM TB_BL B WHERE B.ID_BL = A.ID_BL_MASTER) WHEN GRAU = 'M' AND DT_CHEGADA < GETDATE() THEN GETDATE() WHEN GRAU = 'M' THEN DT_CHEGADA END DT_CHEGADA
FROM [TB_BL] A WHERE A.ID_BL = " & txtID_BL.Text)
                Dim DATA As Date
                If ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 1 Then
                    DATA = Now.Date.ToString("dd-MM-yyyy")
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
                            If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_FCL")) Then
                                lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                divErro.Visible = True
                                Exit Sub
                            Else
                                lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_FCL")
                                lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_FCL")
                                If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 2 Then

                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,ISNULL(VL_TXABERTURA,0)VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False

                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 4 Then
                                    dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFreteArmador.DataBind()
                                    dgvMoedaFreteArmador.Visible = True

                                    dgvMoedaFrete.Visible = False
                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 5 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False
                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 11 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False
                                End If


                            End If

                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 1 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                            If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_LCL")) Then
                                lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                divErro.Visible = True
                                Exit Sub
                            Else
                                lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_LCL")
                                lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_LCL")
                                If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 2 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False

                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 4 Then

                                    dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFreteArmador.DataBind()
                                    dgvMoedaFreteArmador.Visible = True

                                    dgvMoedaFrete.Visible = False

                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 5 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA, DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False
                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 11 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False
                                End If

                            End If

                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 4 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                            If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_FCL")) Then
                                lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                divErro.Visible = True
                                Exit Sub
                            Else
                                lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_FCL")
                                lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_FCL")

                                If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 2 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False

                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 4 Then

                                    dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFreteArmador.DataBind()
                                    dgvMoedaFreteArmador.Visible = True

                                    dgvMoedaFrete.Visible = False
                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 5 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False
                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 11 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False
                                End If
                            End If





                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 4 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then

                            If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_LCL")) Then
                                lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                divErro.Visible = True
                                Exit Sub
                            Else
                                lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_LCL")
                                lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_LCL")

                                If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 2 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False

                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 4 Then

                                    dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM  TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFreteArmador.DataBind()
                                    dgvMoedaFreteArmador.Visible = True

                                    dgvMoedaFrete.Visible = False
                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 5 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False
                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 11 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False
                                End If

                            End If



                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Then

                            If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")) Then
                                lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                divErro.Visible = True
                                Exit Sub
                            Else
                                lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")
                                lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_IMPO")

                                If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 2 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False

                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 4 Then
                                    dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFreteArmador.DataBind()
                                    dgvMoedaFreteArmador.Visible = True

                                    dgvMoedaFrete.Visible = False
                                End If

                            End If



                        ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                            If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")) Then
                                lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                divErro.Visible = True
                                Exit Sub
                            Else
                                lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")
                                lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_EXPO")

                                If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 2 Then
                                    dsMoedaFrete.SelectCommand = "SELECT ID_MOEDA_FRETE,VL_TXOFICIAL,VL_TXABERTURA ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFrete.DataBind()
                                    dgvMoedaFrete.Visible = True

                                    dgvMoedaFreteArmador.Visible = False


                                ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 4 Then
                                    dsMoedaFreteArmador.SelectCommand = "SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE_ARMADOR A WHERE A.ID_MOEDA <> 124 AND DT_CAMBIO = CONVERT(DATE,GETDATE(),103)"
                                    dgvMoedaFreteArmador.DataBind()
                                    dgvMoedaFreteArmador.Visible = True

                                    dgvMoedaFrete.Visible = False
                                End If

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

        If txtVencimento.Text = "" Then
            lblErro.Text = "É necessário informar uma data de vencimento."
            divErro.Visible = True
            Exit Sub
        Else
            If lblDiasFaturamento.Text = "" Then
                lblDiasFaturamento.Text = 0
            End If
            AtualizaValorReal()
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet
            ds = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA,ID_USUARIO_LANCAMENTO,CD_PR,ID_TIPO_FATURAMENTO,QT_DIAS_FATURAMENTO) VALUES (GETDATE(),CONVERT(DATE, '" & txtVencimento.Text & "',103),1," & Session("ID_USUARIO") & ",'R',(SELECT ID_TIPO_FATURAMENTO FROM TB_PARCEIRO WHERE ID_PARCEIRO= " & Session("FORNECEDOR") & ")," & lblDiasFaturamento.Text & ")  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER  ")
            Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
            lblID_CONTA_PAGAR_RECEBER.Text = ID_CONTA_PAGAR_RECEBER
            For Each linha As GridViewRow In dgvTaxas.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Dim ItemDespesa As String = CType(linha.FindControl("lblItemDespesa"), Label).Text
                    Dim valor As Decimal = CType(linha.FindControl("lblValor"), Label).Text

                    Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL_TAXA)QTD FROM [TB_CONTA_PAGAR_RECEBER_ITENS] A
INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER
WHERE DT_CANCELAMENTO IS NULL AND ID_BL_TAXA =" & ID)
                    If ds1.Tables(0).Rows(0).Item("QTD") > 0 Then
                        lblErro.Text = "Há taxas já cadastradas em contas a receber"
                        divErro.Visible = True
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " & ID_CONTA_PAGAR_RECEBER)
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER =" & ID_CONTA_PAGAR_RECEBER)
                    Else

                        Dim ISS As Decimal
                        Dim PIS As Decimal
                        Dim COFINS As Decimal


                        If lbl_ISS.Text > 0 Then
                            ISS = lbl_ISS.Text
                        Else
                            ISS = Session("VL_ALIQUOTA_ISS")
                        End If
                        ISS = (valor / 100) * ISS

                        If lbl_PIS.Text > 0 Then
                            PIS = lbl_PIS.Text
                        Else
                            PIS = Session("VL_ALIQUOTA_PIS")
                        End If
                        PIS = (valor / 100) * PIS


                        If lbl_COFINS.Text > 0 Then
                            COFINS = lbl_COFINS.Text
                        Else
                            COFINS = Session("VL_ALIQUOTA_COFINS")
                        End If
                        COFINS = (valor / 100) * COFINS


                        Dim Desconto As Decimal = COFINS + ISS + PIS

                        Dim desconto_final As String = Desconto.ToString
                        desconto_final = desconto_final.Replace(".", "")
                        desconto_final = desconto_final.Replace(",", ".")

                        Dim COFINS_final As String = COFINS.ToString
                        COFINS_final = COFINS_final.Replace(".", "")
                        COFINS_final = COFINS_final.Replace(",", ".")


                        Dim PIS_final As String = PIS.ToString
                        PIS_final = PIS_final.Replace(".", "")
                        PIS_final = PIS_final.Replace(",", ".")


                        Dim ISS_final As String = ISS.ToString
                        ISS_final = ISS_final.Replace(".", "")
                        ISS_final = ISS_final.Replace(",", ".")

                        Dim valor_final As String = valor.ToString
                        valor_final = valor_final.Replace(".", "")
                        valor_final = valor_final.Replace(",", ".")


                        If lblCidade.Text = "SANTOS" Then
                            Dim dsDespesa As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_TIPO_ITEM_DESPESA)QTD FROM TB_TIPO_ITEM_DESPESA WHERE ID_TIPO_ITEM_DESPESA = (SELECT ID_TIPO_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA =  " & ItemDespesa & ") AND CD_TIPO_ITEM_DESPESA = 'R'")
                            If dsDespesa.Tables(0).Rows(0).Item("QTD") = 0 Then
                                Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,VL_ISS,VL_PIS,VL_COFINS )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO,VL_CAMBIO," & valor_final & ",VL_TAXA_BR,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA, " & ISS_final & "," & PIS_final & " ," & COFINS_final & " FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                            Else

                                Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,VL_ISS,VL_PIS,VL_COFINS )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO,VL_CAMBIO," & valor_final & ",VL_TAXA_BR  - (" & desconto_final & "), ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA, " & ISS_final & "," & PIS_final & " ," & COFINS_final & " FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                            End If
                        Else
                            Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,VL_ISS,VL_PIS,VL_COFINS )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO,VL_CAMBIO," & valor_final & ",VL_TAXA_BR,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA, " & ISS_final & "," & PIS_final & " ," & COFINS_final & " FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                        End If



                    End If
                End If
            Next
            Con.Fechar()
            lblSuccess.Text = "Cálculo realizado com sucesso!"
            divSuccess.Visible = True
            txtVencimento.Text = ""
            ddlFornecedor.SelectedValue = 0
            dgvTaxas.DataBind()
            mpeND.Show()
        End If



    End Sub

    Private Sub dgvTaxas_Load(sender As Object, e As EventArgs) Handles dgvTaxas.Load
        VerificaTaxas()
    End Sub
    Sub VerificaTaxas()
        divErro.Visible = False
        divSuccess.Visible = False

        Dim Con As New Conexao_sql
        'Dim i As Integer = 0
        txtValor.Text = 0

        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Calculado As String = CType(linha.FindControl("lblCalculado"), Label).Text
            Dim valor As String = CType(linha.FindControl("lblValorBR"), Label).Text
            Dim valor2 As Double = txtValor.Text

            If check.Checked Then
                'i = i + 1
                txtValor.Text = valor2 + valor

                If Calculado = False Then
                    lblErro.Text = "O PROCESSO NECESSITA DE CÁLCULO"
                    divErro.Visible = True
                    check.Checked = False
                    check.Enabled = False
                    Exit Sub


                End If

            End If
        Next

        'If i > 0 Then
        '    btnCalcularRecebimento.Enabled = True

        'Else
        '    btnCalcularRecebimento.Enabled = False

        'End If
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

            VerificaTaxas()
        Next
    End Sub

    Private Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Response.Redirect("EmissaoND.aspx?id=" & txtID_BL.Text)

    End Sub

    Sub AtualizaValorReal()
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
                            For Each linhaMoedas As GridViewRow In dgvMoedaFreteArmador.Rows
                                Dim MoedaFrete As String = CType(linhaMoedas.FindControl("lblMoedaFrete"), Label).Text

                                If MoedaFrete = moeda Then
                                    ValorCambio = CType(linhaMoedas.FindControl("txtValorCambio"), TextBox).Text
                                    If lblSpread.Text <> "" And lblSpread.Text > 0 Then
                                        If lblAcordo.Text = "CAMBIO DO ARMADOR + SPREAD" Then
                                            Dim spread As Decimal = (ValorCambio / 100) * lblSpread.Text
                                            ValorCambio = ValorCambio + spread
                                        End If

                                    End If
                                    ValorCambio = ValorCambio.Replace(".", "")
                                    ValorCambio = ValorCambio.Replace(",", ".")


                                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA]  SET [VL_TAXA_BR] = VL_TAXA_CALCULADO * " & ValorCambio & ",DT_ATUALIZACAO_CAMBIO = GETDATE(),VL_CAMBIO = " & ValorCambio & " WHERE ID_BL_TAXA =" & ID)
                                End If

                            Next

                        Else


                            For Each linhaMoedas As GridViewRow In dgvMoedaFrete.Rows
                                Dim MoedaFrete As String = CType(linhaMoedas.FindControl("lblMoedaFrete"), Label).Text

                                If MoedaFrete = moeda Then
                                    ValorCambio = CType(linhaMoedas.FindControl("txtValorCambio"), TextBox).Text
                                    If lblSpread.Text <> "" And lblSpread.Text > 0 Then
                                        If lblAcordo.Text = "CAMBIO PTAX + SPREAD" Then
                                            Dim spread As Decimal = (ValorCambio / 100) * lblSpread.Text
                                            ValorCambio = ValorCambio + spread
                                        End If

                                        If lblAcordo.Text = "TAXA ABERTURA PTAX + SPREAD" Then
                                            ValorCambio = CType(linhaMoedas.FindControl("txtValorAbertuda"), TextBox).Text
                                            Dim spread As Decimal = (ValorCambio / 100) * lblSpread.Text
                                            ValorCambio = ValorCambio + spread
                                        End If
                                        If lblAcordo.Text = "TAXA ABERTURA PTAX" Then
                                            ValorCambio = CType(linhaMoedas.FindControl("txtValorAbertuda"), TextBox).Text
                                        End If
                                    End If
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



    End Sub

End Class