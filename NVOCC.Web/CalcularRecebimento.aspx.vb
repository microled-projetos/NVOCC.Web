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
                Dim ds1 As DataSet
                If Not Page.IsPostBack Then

                    txtID_BL.Text = Request.QueryString("id")
                    ds1 = Con.ExecutarQuery("SELECT isnull(NR_PROCESSO,'')NR_PROCESSO FROM TB_BL WHERE ID_BL = " & txtID_BL.Text)
                    If ds1.Tables(0).Rows.Count > 0 Then
                        lblProceso.Text = ds1.Tables(0).Rows(0).Item("NR_PROCESSO")
                    End If
                    txtCambio.Text = Now.Date.ToString("dd-MM-yyyy")
                End If



            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub ddlFornecedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFornecedor.SelectedIndexChanged
        divInfo.Visible = False
        divErro.Visible = False
        divSuccess.Visible = False
        divConteudo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds0 As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM [dbo].[View_BL_TAXAS]
WHERE (ID_BL = " & txtID_BL.Text & " OR ID_BL_MASTER = " & txtID_BL.Text & ") AND CD_PR = 'R' AND ISNULL(ID_PARCEIRO_EMPRESA,0) = 0 AND ID_DESTINATARIO_COBRANCA <> 3")
        If ds0.Tables(0).Rows(0).Item("QTD") > 0 Then
            ddlFornecedor.Enabled = False
            lblErro.Text = "EXISTE TAXA SEM IDENTIFICAÇÃO DO DESTINATÁRIO DE COBRANÇA!"
            divErro.Visible = True
        Else

            If ddlFornecedor.SelectedValue <> 0 Then

                Dim ds As DataSet = Con.ExecutarQuery("SELECT (SELECT NM_CIDADE FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE)NM_CIDADE,(SELECT NM_TIPO_FATURAMENTO FROM TB_TIPO_FATURAMENTO WHERE ID_TIPO_FATURAMENTO = A.ID_TIPO_FATURAMENTO)NM_TIPO_FATURAMENTO,ID_TIPO_FATURAMENTO,QT_DIAS_FATURAMENTO,SPREAD_AEREO_EXPO,SPREAD_AEREO_IMPO,SPREAD_MARITIMO_EXPO_FCL,SPREAD_MARITIMO_EXPO_LCL,SPREAD_MARITIMO_IMPO_FCL,SPREAD_MARITIMO_IMPO_LCL,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_AEREO_EXPO)ACORDO_CAMBIO_AEREO,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_AEREO_IMPO)ACORDO_CAMBIO_AEREO_IMPO,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL)ACORDO_CAMBIO_MARITIMO_EXPO_FCL,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL)ACORDO_CAMBIO_MARITIMO_EXPO_LCL,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL)ACORDO_CAMBIO_MARITIMO_IMPO_FCL,
(SELECT NM_ACORDO_CAMBIO FROM TB_ACORDO_CAMBIO WHERE ID_ACORDO_CAMBIO =  ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL)ACORDO_CAMBIO_MARITIMO_IMPO_LCL,
ID_ACORDO_CAMBIO_AEREO_EXPO AS ID_ACORDO_CAMBIO_AEREO,
ID_ACORDO_CAMBIO_AEREO_IMPO,
ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL,
ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL,
ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL,
ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL,
ISNULL(VL_ALIQUOTA_ISS,3)VL_ALIQUOTA_ISS, ISNULL(VL_ALIQUOTA_PIS,7.6)VL_ALIQUOTA_PIS, ISNULL(VL_ALIQUOTA_COFINS,1.65)VL_ALIQUOTA_COFINS FROM [TB_PARCEIRO] A WHERE ID_PARCEIRO =" & ddlFornecedor.SelectedValue)
                If ds.Tables(0).Rows.Count > 0 Then
                    Session("FORNECEDOR") = ddlFornecedor.SelectedValue
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_CIDADE")) Then
                        lblCidade.Text = ds.Tables(0).Rows(0).Item("NM_CIDADE")
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

                    Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ID_BL,ID_BL_MASTER,GRAU,ISNULL(ID_SERVICO,0)ID_SERVICO,(SELECT TP_SERVICO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)TP_SERVICO, CASE WHEN GRAU = 'C' THEN (SELECT CASE WHEN DT_CHEGADA < GETDATE() THEN GETDATE() ELSE DT_CHEGADA END FROM TB_BL B WHERE B.ID_BL = A.ID_BL_MASTER) WHEN GRAU = 'M' AND DT_CHEGADA < GETDATE() THEN GETDATE() WHEN GRAU = 'M' THEN DT_CHEGADA END DT_CHEGADA, ISNULL(ID_INCOTERM,0)ID_INCOTERM, ISNULL(FL_FREE_HAND,0)FL_FREE_HAND 
FROM [TB_BL] A WHERE A.ID_BL = " & txtID_BL.Text)
                    Dim DATA As Date
                    If IsDBNull(ds1.Tables(0).Rows(0).Item("DT_CHEGADA")) And ds1.Tables(0).Rows(0).Item("ID_SERVICO") <= 2 Then
                        lblErro.Text = "PROCESSO SEM DATA DE CHEGADA CADASTRADA"
                        divErro.Visible = True
                        Exit Sub
                    End If

                    If IsDBNull(ds1.Tables(0).Rows(0).Item("TP_SERVICO")) Then
                        lblTpServico.Text = ds1.Tables(0).Rows(0).Item("TP_SERVICO")
                    End If


                    'PROCURA COMBINAÇÃO COMPLETA
                    Dim dsFaturamento As DataSet = Con.ExecutarQuery("SELECT ID_TIPO_FATURAMENTO, (SELECT NM_TIPO_FATURAMENTO FROM TB_TIPO_FATURAMENTO WHERE ID_TIPO_FATURAMENTO = A.ID_TIPO_FATURAMENTO)NM_TIPO_FATURAMENTO FROM TB_PARCEIRO_TIPO_FATURAMENTO A WHERE ID_INCOTERM = " & ds1.Tables(0).Rows(0).Item("ID_INCOTERM") & " AND ID_SERVICO = " & ds1.Tables(0).Rows(0).Item("ID_SERVICO") & " AND FL_FREE_HAND = '" & ds1.Tables(0).Rows(0).Item("FL_FREE_HAND") & "' AND ID_PARCEIRO = " & ddlFornecedor.SelectedValue)

                    'CASO ENCONTRE
                    If dsFaturamento.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(dsFaturamento.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")) Then
                            lblTipoFaturamento.Text = dsFaturamento.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")
                        End If

                        If Not IsDBNull(dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")) Then
                            lblIDTipoFaturamento.Text = dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")
                        End If

                        If ds1.Tables(0).Rows(0).Item("ID_SERVICO") <= 2 Then
                            If dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 1 Then
                                DATA = Now.Date.ToString("dd-MM-yyyy")

                            ElseIf dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 2 Then
                                DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                                DATA = DATA.AddDays(lblDiasFaturamento.Text)
                            ElseIf dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 5 Then
                                DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                                DATA = DATA.AddMonths(1)

                                DATA = "11/" & DATA.Month & "/" & DATA.Year
                                DATA = FinalSemana(DATA)

                            End If
                            txtVencimento.Text = DATA
                        End If


                    Else

                        'CASO NAO ENCONTRE PROCURA COMBINAÇÃO COM INCOTERM
                        dsFaturamento = Con.ExecutarQuery("SELECT ID_TIPO_FATURAMENTO, (SELECT NM_TIPO_FATURAMENTO FROM TB_TIPO_FATURAMENTO WHERE ID_TIPO_FATURAMENTO = A.ID_TIPO_FATURAMENTO)NM_TIPO_FATURAMENTO FROM TB_PARCEIRO_TIPO_FATURAMENTO A WHERE FL_FREE_HAND = 0 AND ID_INCOTERM = " & ds1.Tables(0).Rows(0).Item("ID_INCOTERM") & " AND ID_SERVICO = " & ds1.Tables(0).Rows(0).Item("ID_SERVICO") & " AND ID_PARCEIRO = " & ddlFornecedor.SelectedValue)

                        If dsFaturamento.Tables(0).Rows.Count > 0 Then

                            If Not IsDBNull(dsFaturamento.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")) Then
                                lblTipoFaturamento.Text = dsFaturamento.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")
                            End If

                            If Not IsDBNull(dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")) Then
                                lblIDTipoFaturamento.Text = dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")
                            End If

                            If ds1.Tables(0).Rows(0).Item("ID_SERVICO") <= 2 Then
                                If dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 1 Then
                                    DATA = Now.Date.ToString("dd-MM-yyyy")

                                ElseIf dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 2 Then
                                    DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                                    DATA = DATA.AddDays(lblDiasFaturamento.Text)
                                ElseIf dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 5 Then
                                    DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                                    DATA = DATA.AddMonths(1)

                                    DATA = "11/" & DATA.Month & "/" & DATA.Year
                                    DATA = FinalSemana(DATA)

                                End If
                                txtVencimento.Text = DATA
                            End If


                        Else

                            'CASO NAO ENCONTRE PROCURA COMBINAÇÃO COM FREE HAND
                            dsFaturamento = Con.ExecutarQuery("SELECT ID_TIPO_FATURAMENTO, (SELECT NM_TIPO_FATURAMENTO FROM TB_TIPO_FATURAMENTO WHERE ID_TIPO_FATURAMENTO = A.ID_TIPO_FATURAMENTO)NM_TIPO_FATURAMENTO FROM TB_PARCEIRO_TIPO_FATURAMENTO A WHERE ID_INCOTERM = 0 AND FL_FREE_HAND = '" & ds1.Tables(0).Rows(0).Item("FL_FREE_HAND") & "' AND ID_SERVICO = " & ds1.Tables(0).Rows(0).Item("ID_SERVICO") & " AND ID_PARCEIRO = " & ddlFornecedor.SelectedValue)

                            If dsFaturamento.Tables(0).Rows.Count > 0 Then

                                If Not IsDBNull(dsFaturamento.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")) Then
                                    lblTipoFaturamento.Text = dsFaturamento.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")
                                End If

                                If Not IsDBNull(dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")) Then
                                    lblIDTipoFaturamento.Text = dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")
                                End If

                                If ds1.Tables(0).Rows(0).Item("ID_SERVICO") <= 2 Then
                                    If dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 1 Then
                                        DATA = Now.Date.ToString("dd-MM-yyyy")

                                    ElseIf dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 2 Then
                                        DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                                        DATA = DATA.AddDays(lblDiasFaturamento.Text)
                                    ElseIf dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 5 Then
                                        DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                                        DATA = DATA.AddMonths(1)

                                        DATA = "11/" & DATA.Month & "/" & DATA.Year
                                        DATA = FinalSemana(DATA)

                                    End If
                                    txtVencimento.Text = DATA
                                End If

                            Else

                                'CASO NAO ENCONTRE PROCURA COMBINAÇÃO APENAS COM SERVICO

                                dsFaturamento = Con.ExecutarQuery("SELECT ID_TIPO_FATURAMENTO, (SELECT NM_TIPO_FATURAMENTO FROM TB_TIPO_FATURAMENTO WHERE ID_TIPO_FATURAMENTO = A.ID_TIPO_FATURAMENTO)NM_TIPO_FATURAMENTO FROM TB_PARCEIRO_TIPO_FATURAMENTO A WHERE ID_SERVICO = " & ds1.Tables(0).Rows(0).Item("ID_SERVICO") & " AND ID_INCOTERM = 0  AND FL_FREE_HAND = 0 AND ID_PARCEIRO = " & ddlFornecedor.SelectedValue)
                                If dsFaturamento.Tables(0).Rows.Count > 0 Then

                                    If Not IsDBNull(dsFaturamento.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")) Then
                                        lblTipoFaturamento.Text = dsFaturamento.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")
                                    End If

                                    If Not IsDBNull(dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")) Then
                                        lblIDTipoFaturamento.Text = dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")
                                    End If

                                    If ds1.Tables(0).Rows(0).Item("ID_SERVICO") <= 2 Then
                                        If dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 1 Then
                                            DATA = Now.Date.ToString("dd-MM-yyyy")

                                        ElseIf dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 2 Then
                                            DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                                            DATA = DATA.AddDays(lblDiasFaturamento.Text)
                                        ElseIf dsFaturamento.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 5 Then
                                            DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                                            DATA = DATA.AddMonths(1)

                                            DATA = "11/" & DATA.Month & "/" & DATA.Year
                                            DATA = FinalSemana(DATA)

                                        End If
                                        txtVencimento.Text = DATA
                                    End If

                                Else

                                    'CASO NAO ENCONTRE USA O PADRÃO DA TABELA DE PARCEROS

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")) Then
                                        lblTipoFaturamento.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")) Then
                                        lblIDTipoFaturamento.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")
                                    End If

                                    If ds1.Tables(0).Rows(0).Item("ID_SERVICO") <= 2 Then
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
                                    End If
                                End If
                            End If
                        End If







                        'Else
                        '    'CASO NAO ENCONTRE 

                        '    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")) Then
                        '        lblTipoFaturamento.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_FATURAMENTO")
                        '    End If

                        '    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")) Then
                        '        lblIDTipoFaturamento.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")
                        '    End If

                        '    If ds1.Tables(0).Rows(0).Item("ID_SERVICO") <= 2 Then
                        '        If ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 1 Then
                        '            DATA = Now.Date.ToString("dd-MM-yyyy")

                        '        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 2 Then
                        '            DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                        '            DATA = DATA.AddDays(lblDiasFaturamento.Text)
                        '        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO") = 5 Then
                        '            DATA = ds1.Tables(0).Rows(0).Item("DT_CHEGADA").ToString

                        '            DATA = DATA.AddMonths(1)

                        '            DATA = "11/" & DATA.Month & "/" & DATA.Year
                        '            DATA = FinalSemana(DATA)

                        '        End If
                        '        txtVencimento.Text = DATA
                        '    End If

                    End If






                    ds1 = Con.ExecutarQuery("SELECT ID_SERVICO, ID_TIPO_ESTUFAGEM FROM TB_BL WHERE ID_BL = " & txtID_BL.Text)
                        If ds1.Tables(0).Rows.Count > 0 Then

                            If Not IsDBNull(ds1.Tables(0).Rows(0).Item("ID_SERVICO")) And Not IsDBNull(ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then

                                If ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 1 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                                    If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_FCL")) Then
                                        lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                    divErro.Visible = True
                                    lblAcordo.Text = ""
                                    lblSpread.Text = ""
                                    Exit Sub
                                    Else
                                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_FCL")
                                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_FCL")
                                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 2 Then


                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False

                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 4 Then

                                            dgvMoedaFreteArmador.Visible = True

                                            dgvMoedaFrete.Visible = False
                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 5 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False
                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL") = 11 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False
                                        End If


                                    End If

                                ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 1 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                                    If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_LCL")) Then
                                        lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                    divErro.Visible = True
                                    lblAcordo.Text = ""
                                    lblSpread.Text = ""
                                    Exit Sub
                                    Else
                                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_IMPO_LCL")
                                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_LCL")
                                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 2 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False

                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 4 Then
                                            dgvMoedaFreteArmador.Visible = True

                                            dgvMoedaFrete.Visible = False

                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 5 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False
                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL") = 11 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False
                                        End If

                                    End If

                                ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 4 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                                    If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_FCL")) Then
                                        lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                    divErro.Visible = True
                                    lblAcordo.Text = ""
                                    lblSpread.Text = ""
                                    Exit Sub
                                    Else
                                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_FCL")
                                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_FCL")

                                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 2 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False

                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 4 Then
                                            dgvMoedaFreteArmador.Visible = True

                                            dgvMoedaFrete.Visible = False
                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 5 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False
                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL") = 11 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False
                                        End If
                                    End If





                                ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 4 And ds1.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then

                                    If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_LCL")) Then
                                        lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                    divErro.Visible = True
                                    lblAcordo.Text = ""
                                    lblSpread.Text = ""
                                    Exit Sub
                                    Else
                                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_MARITIMO_EXPO_LCL")
                                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_LCL")

                                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 2 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False

                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 4 Then


                                            dgvMoedaFreteArmador.Visible = True

                                            dgvMoedaFrete.Visible = False
                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 5 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False
                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL") = 11 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False
                                        End If

                                    End If



                                ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Then

                                    If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")) Then
                                        lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                    divErro.Visible = True
                                    lblAcordo.Text = ""
                                    lblSpread.Text = ""
                                    Exit Sub
                                    Else
                                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")
                                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_IMPO")

                                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 2 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False

                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 4 Then

                                            dgvMoedaFreteArmador.Visible = True

                                            dgvMoedaFrete.Visible = False
                                        End If

                                    End If



                                ElseIf ds1.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                                    If IsDBNull(ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")) Then
                                        lblErro.Text = "PARCEIRO SEM ACORDO DE CAMBIO CADASTRADO"
                                    divErro.Visible = True
                                    lblAcordo.Text = ""
                                    lblSpread.Text = ""
                                    Exit Sub
                                    Else
                                        lblAcordo.Text = ds.Tables(0).Rows(0).Item("ACORDO_CAMBIO_AEREO")
                                        lblSpread.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_EXPO")

                                        If ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 1 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 2 Then

                                            dgvMoedaFrete.Visible = True

                                            dgvMoedaFreteArmador.Visible = False


                                        ElseIf ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 3 Or ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO") = 4 Then

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

                divConteudo.Visible = True
                grid()

            End If
        End If


    End Sub


    Sub grid()
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim sqlGrid As String = "SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE (ID_BL = " & txtID_BL.Text & " OR ID_BL_MASTER = " & txtID_BL.Text & ") AND CD_PR = 'R' AND ID_DESTINATARIO_COBRANCA <> 3 AND ID_PARCEIRO_EMPRESA = " & ddlFornecedor.SelectedValue

        Using Status = New NotaFiscal.WsNvocc

            Status.StatusBloqueio(sqlGrid)

        End Using

        dsTaxas.SelectCommand = sqlGrid
        dgvTaxas.DataBind()

        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim ID_PARCEIRO_ARMAZEM_DESCARGA As String = CType(linha.FindControl("lblID_PARCEIRO_ARMAZEM_DESCARGA"), Label).Text


            Dim FL_BLOQUEIO_FINANCEIRO As String = CType(linha.FindControl("lblFL_BLOQUEIO_FINANCEIRO"), Label).Text
            Dim btnDesbloquearFinanceiro As ImageButton = CType(linha.FindControl("btnDesbloquearFinanceiro"), ImageButton)
            Dim btnBloquearFinanceiro As ImageButton = CType(linha.FindControl("btnBloquearFinanceiro"), ImageButton)

            Dim FL_BLOQUEIO_DOCUMENTAL As String = CType(linha.FindControl("lblFL_BLOQUEIO_DOCUMENTAL"), Label).Text
            Dim btnDesbloquearDocumental As ImageButton = CType(linha.FindControl("btnDesbloquearDocumental"), ImageButton)
            Dim btnBloquearDocumental As ImageButton = CType(linha.FindControl("btnBloquearDocumental"), ImageButton)

            If ID_PARCEIRO_ARMAZEM_DESCARGA = 74 And linha.RowIndex = 0 Then

                If FL_BLOQUEIO_DOCUMENTAL = "SIM" Then
                    btnBloquearDocumental.Visible = False
                    btnDesbloquearDocumental.Visible = True

                ElseIf FL_BLOQUEIO_DOCUMENTAL = "NÃO" Then
                    btnDesbloquearDocumental.Visible = False
                    btnBloquearDocumental.Visible = True

                End If



                If FL_BLOQUEIO_FINANCEIRO = "SIM" Then
                    btnBloquearFinanceiro.Visible = False
                    btnDesbloquearFinanceiro.Visible = True

                ElseIf FL_BLOQUEIO_FINANCEIRO = "NÃO" Then
                    btnDesbloquearFinanceiro.Visible = False
                    btnBloquearFinanceiro.Visible = True

                End If

            Else

                btnBloquearFinanceiro.Visible = False
                btnDesbloquearFinanceiro.Visible = False
                btnBloquearDocumental.Visible = False
                btnDesbloquearDocumental.Visible = False

            End If

            btnBloquearFinanceiro.Visible = False
            btnDesbloquearFinanceiro.Visible = False
            btnBloquearDocumental.Visible = False
            btnDesbloquearDocumental.Visible = False
        Next

        divConteudo.Visible = True

        Dim ds0 As DataSet = Con.ExecutarQuery(sqlGrid)
        If ds0.Tables(0).Rows.Count = 0 Then
            ds0 = Con.ExecutarQuery("select count(*)qtd from tb_bl A
INNER JOIN TB_COTACAO B ON A.ID_COTACAO = B.ID_COTACAO
WHERE B.ID_STATUS_COTACAO  = 10 AND ID_BL = " & txtID_BL.Text)
            If ds0.Tables(0).Rows(0).Item("QTD") > 0 Then
                lblErro.Text = "PROCESSO COM COTAÇÃO EM UPDATE!"
                divErro.Visible = True
            End If

        End If

    End Sub
    Private Sub btnCalcularRecebimento_Click(sender As Object, e As EventArgs) Handles btnCalcularRecebimento.Click
        divErro.Visible = False
        divSuccess.Visible = False
        Dim v As New VerificaData

        If txtVencimento.Text = "" Then
            lblErro.Text = "É necessário informar uma data de vencimento."
            divErro.Visible = True
            Exit Sub

        ElseIf v.ValidaData(txtVencimento.Text) = False Then
            lblErro.Text = "Data de vencimento invalida!"
            divErro.Visible = True
            Exit Sub

        ElseIf txtValor.Text = 0 Then
            lblErro.Text = "Não é possivel completar a ação: o valor não pode se zerado!"
            divErro.Visible = True
            Exit Sub

        ElseIf lblAcordo.Text = "" And lblTpServico.Text <> "" Then
            lblErro.Text = "É necessário preencher o acordo de câmbio no cadastro do parceiro!"
            divErro.Visible = True
            Exit Sub
        Else
            If lblDiasFaturamento.Text = "" Then
                lblDiasFaturamento.Text = 0
            End If
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet

            Dim ISS_final As String = Session("VL_ALIQUOTA_ISS")
            ISS_final = ISS_final.Replace(".", "")
            ISS_final = ISS_final.Replace(",", ".")

            VerificaTaxas()


<<<<<<< HEAD
            ds = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA,ID_USUARIO_LANCAMENTO,CD_PR,ID_TIPO_FATURAMENTO,QT_DIAS_FATURAMENTO,VL_ALIQUOTA_ISS) VALUES (GETDATE(),CONVERT(DATE, '" & txtVencimento.Text & "',103),1," & Session("ID_USUARIO") & ",'R',(SELECT ID_TIPO_FATURAMENTO FROM TB_PARCEIRO WHERE ID_PARCEIRO= " & ddlFornecedor.SelectedValue & ")," & lblDiasFaturamento.Text & ", " & ISS_final & ")  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER  ")
=======
            ds = Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA,ID_USUARIO_LANCAMENTO,CD_PR,ID_TIPO_FATURAMENTO,QT_DIAS_FATURAMENTO,VL_ALIQUOTA_ISS) VALUES (GETDATE(),CONVERT(DATE, '" & txtVencimento.Text & "',103),1," & Session("ID_USUARIO") & ",'R'," & lblIDTipoFaturamento.Text & "," & lblDiasFaturamento.Text & ", " & ISS_final & ")  Select SCOPE_IDENTITY() as ID_CONTA_PAGAR_RECEBER  ")
>>>>>>> devjuliane
            Dim ID_CONTA_PAGAR_RECEBER As String = ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER")
            lblID_CONTA_PAGAR_RECEBER.Text = ID_CONTA_PAGAR_RECEBER

            If lblProceso.Text <> "" Then
                If lblProceso.Text.Substring(0, 1) = "L" Then
                    Con.ExecutarQuery("UPDATE TB_CONTA_PAGAR_RECEBER SET DT_LIQUIDACAO = CONVERT(DATE,'" & txtVencimento.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER= " & ID_CONTA_PAGAR_RECEBER)
                End If
            End If

            For Each linha As GridViewRow In dgvTaxas.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Dim ItemDespesa As String = CType(linha.FindControl("lblItemDespesa"), Label).Text
                    Dim valor As Decimal = CType(linha.FindControl("lblValorBR"), Label).Text

                    Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL_TAXA)QTD FROM [TB_CONTA_PAGAR_RECEBER_ITENS] A
INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER
WHERE DT_CANCELAMENTO IS NULL AND ISNULL(B.TP_EXPORTACAO,'') = '' AND ID_BL_TAXA =" & ID)
                    If ds1.Tables(0).Rows(0).Item("QTD") > 0 Then
                        lblErro.Text = "Há taxas já cadastradas em contas a receber"
                        divErro.Visible = True
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " & ID_CONTA_PAGAR_RECEBER)
                        Con.ExecutarQuery("DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER =" & ID_CONTA_PAGAR_RECEBER)
                        Exit Sub
                    Else

                        Dim ISS As Decimal = 0
                        Dim PIS As Decimal = 0
                        Dim COFINS As Decimal = 0

                        If lbl_ISS.Text <> "" Then
                            If lbl_ISS.Text > 0 Then
                                ISS = lbl_ISS.Text
                            Else
                                ISS = Session("VL_ALIQUOTA_ISS")
                            End If
                        End If
                        ISS = (valor / 100) * ISS

                        If lbl_PIS.Text <> "" Then
                            If lbl_PIS.Text > 0 Then
                                PIS = lbl_PIS.Text
                            Else
                                PIS = Session("VL_ALIQUOTA_PIS")
                            End If
                        End If
                        PIS = (valor / 100) * PIS

                        If lbl_COFINS.Text <> "" Then
                            If lbl_COFINS.Text > 0 Then
                                COFINS = lbl_COFINS.Text
                            Else
                                COFINS = Session("VL_ALIQUOTA_COFINS")
                            End If
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


                            ISS_final = ISS.ToString
                            ISS_final = ISS_final.Replace(".", "")
                            ISS_final = ISS_final.Replace(",", ".")

                            'Dim valor_final As String = valor.ToString
                            'valor_final = valor_final.Replace(".", "")
                            'valor_final = valor_final.Replace(",", ".")


                            If lblCidade.Text = "SANTOS" Then
                                Dim dsDespesa As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_TIPO_ITEM_DESPESA)QTD FROM TB_TIPO_ITEM_DESPESA WHERE ID_TIPO_ITEM_DESPESA = (SELECT ID_TIPO_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA =  " & ItemDespesa & ") AND CD_TIPO_ITEM_DESPESA = 'R'")
                                If dsDespesa.Tables(0).Rows(0).Item("QTD") = 0 Then
                                    Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,VL_ISS,VL_PIS,VL_COFINS)SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO,VL_CAMBIO,VL_TAXA_BR,VL_TAXA_BR,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA, " & ISS_final & "," & PIS_final & " ," & COFINS_final & " FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                                Else

                                    Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,VL_ISS,VL_PIS,VL_COFINS )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO,VL_CAMBIO,VL_TAXA_BR,VL_TAXA_BR, ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA, " & ISS_final & "," & PIS_final & " ," & COFINS_final & " FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
                                End If
                            Else
                                Con.ExecutarQuery("INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER,ID_BL_TAXA,DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_LIQUIDO,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA,VL_ISS,VL_PIS,VL_COFINS )SELECT " & ID_CONTA_PAGAR_RECEBER & ",ID_BL_TAXA,DT_ATUALIZACAO_CAMBIO,VL_CAMBIO,VL_TAXA_BR,VL_TAXA_BR,ID_BL,ID_ITEM_DESPESA,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_MOEDA,VL_TAXA_CALCULADO,FL_INTEGRA_PA, " & ISS_final & "," & PIS_final & " ," & COFINS_final & " FROM TB_BL_TAXA WHERE ID_BL_TAXA =" & ID)
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
            Dim finaliza As New FinalizaCotacao
            finaliza.Finalizar()
        End If



    End Sub

    Private Sub dgvTaxas_Load(sender As Object, e As EventArgs) Handles dgvTaxas.Load
        VerificaTaxas()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT NR_PROCESSO,NM_ITEM_DESPESA FROM [dbo].[View_BL_TAXAS]
                WHERE (ID_BL = " & txtID_BL.Text & " OR ID_BL_MASTER = " & txtID_BL.Text & ") AND CD_PR = 'R' AND ISNULL(ID_PARCEIRO_EMPRESA,0) = 0 AND ID_DESTINATARIO_COBRANCA <> 3")
        If ds1.Tables(0).Rows.Count > 0 Then

            divErro.Visible = True
            lblErro.Text = "EXISTE TAXA SEM IDENTIFICAÇÃO DO DESTINATÁRIO DE COBRANÇA!"
            ddlFornecedor.Enabled = False
            For Each linha As DataRow In ds1.Tables(0).Rows
                lblErro.Text &= "<br/>PROCESSO: " & linha.Item("NR_PROCESSO").ToString() & " - TAXA: " & linha.Item("NM_ITEM_DESPESA").ToString()
            Next
        End If
        Con.Fechar()

    End Sub
    Sub VerificaTaxas()
        btnCalcularRecebimento.Enabled = True
        divErro.Visible = False
        divSuccess.Visible = False

        Dim Con As New Conexao_sql
        'Dim i As Integer = 0
        txtValor.Text = 0

        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Calculado As String = CType(linha.FindControl("lblCalculado"), Label).Text
            Dim Valor_Calculado As Decimal = CType(linha.FindControl("lblValor"), Label).Text
            Dim valor As Decimal = CType(linha.FindControl("lblValorBR"), Label).Text
            Dim valor2 As Decimal = txtValor.Text

            If check.Checked = True Then
                'i = i + 1
                txtValor.Text = valor2 + valor

                If Calculado = False Then
                    lblErro.Text = "O PROCESSO NECESSITA DE CÁLCULO"
                    divErro.Visible = True
                    check.Checked = False
                    check.Enabled = False
                    Exit Sub


                End If

                If Valor_Calculado = 0 Then
                    lblErro.Text = "O PROCESSO NECESSITA DE CÁLCULO"
                    divErro.Visible = True
                    check.Checked = False
                    check.Enabled = False
                    Exit Sub
                End If

                If valor = 0 Then
                    btnCalcularRecebimento.Enabled = False
                End If

            End If
        Next

        'If i > 0 Then
        '    btnCalcularRecebimento.Enabled = True

        'Else
        '    btnCalcularRecebimento.Enabled = False

        'End If
        'txtValor.Text = FormatCurrency(txtValor.Text)

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
            Dim valor As Double = CType(Me.dgvTaxas.Rows(i).FindControl("lblValorBR"), Label).Text
            txtValor.Text = txtValor.Text + valor

            VerificaTaxas()
        Next
    End Sub

    Private Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Response.Redirect("EmissaoND.aspx?id=" & txtID_BL.Text)

    End Sub

    Private Sub btnAtualizaValor_Click(sender As Object, e As EventArgs) Handles btnAtualizaValor.Click
        divErro.Visible = False
        divSuccess.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim OPERADOR As String
        For Each linha As GridViewRow In dgvTaxas.Rows
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            If check.Checked Then
                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                Dim moeda As String = CType(linha.FindControl("lblMoeda"), Label).Text
                Dim ValorCambio As Decimal

                If moeda = 124 Then
                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA]  SET [VL_TAXA_BR] = VL_TAXA_CALCULADO, DT_ATUALIZACAO_CAMBIO = GETDATE() WHERE ID_BL_TAXA =" & ID)
                Else

                    If dgvMoedaFreteArmador.Visible = True Then
                        For Each linhaMoedas As GridViewRow In dgvMoedaFreteArmador.Rows
                            Dim MoedaFrete As String = CType(linhaMoedas.FindControl("lblMoedaFrete"), Label).Text

                            If MoedaFrete = moeda Then
                                ValorCambio = CType(linhaMoedas.FindControl("txtValorCambio"), TextBox).Text

                                If ValorCambio = 0 Then
                                    lblErro.Text = "Informe o valor de câmbio!"
                                    divErro.Visible = True
                                    Exit Sub
                                Else





                                    If lblSpread.Text <> "" And lblSpread.Text > 0 Then
                                        If lblAcordo.Text = "CAMBIO DO ARMADOR + SPREAD" Then
                                            Dim spread As Decimal = (ValorCambio / 100) * lblSpread.Text
                                            ValorCambio = ValorCambio + spread
                                        End If

                                    End If
                                    Dim valorCambioFinal As String = ValorCambio
                                    valorCambioFinal = valorCambioFinal.Replace(".", "")
                                    valorCambioFinal = valorCambioFinal.Replace(",", ".")


                                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA]  SET [VL_TAXA_BR] = VL_TAXA_CALCULADO * " & valorCambioFinal & ",DT_ATUALIZACAO_CAMBIO = GETDATE(),VL_CAMBIO = " & valorCambioFinal & " WHERE ID_BL_TAXA =" & ID)


                                    'Dim dsOperador As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_TIPO_ITEM_DESPESA,0)ID_TIPO_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlDespesa_TaxaMaritimo.SelectedValue)
                                    'Dim OPERADOR As String = "+"
                                    'If dsOperador.Tables(0).Rows(0).Item("ID_TIPO_ITEM_DESPESA") = 3 Then
                                    '    OPERADOR = "-"
                                    'Else
                                    '    OPERADOR = "+"
                                    'End If
                                    'Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA]  SET [VL_TAXA_BR] = VL_TAXA_CALCULADO * " & valorCambioFinal & ",DT_ATUALIZACAO_CAMBIO = GETDATE(),VL_CAMBIO = " & valorCambioFinal & " WHERE ID_BL_TAXA =" & ID)

                                End If
                            End If

                        Next

                    Else


                        For Each linhaMoedas As GridViewRow In dgvMoedaFrete.Rows
                            Dim MoedaFrete As String = CType(linhaMoedas.FindControl("lblMoedaFrete"), Label).Text

                            If MoedaFrete = moeda Then
                                ValorCambio = CType(linhaMoedas.FindControl("txtValorCambio"), TextBox).Text

                                If ValorCambio = 0 Then
                                    lblErro.Text = "Informe o valor de câmbio!"
                                    divErro.Visible = True
                                    Exit Sub
                                Else
                                    If lblSpread.Text <> "" And lblSpread.Text > 0 Then

                                        If lblAcordo.Text = "CAMBIO PTAX + SPREAD" Then
                                            Dim spread As Decimal = (ValorCambio / 100) * lblSpread.Text
                                            ValorCambio = ValorCambio + spread
                                        End If

                                        If lblAcordo.Text = "TAXA ABERTURA + SPREAD" Then
                                            ValorCambio = CType(linhaMoedas.FindControl("txtValorAbertuda"), TextBox).Text
                                            Dim spread As Decimal = (ValorCambio / 100) * lblSpread.Text
                                            ValorCambio = ValorCambio + spread
                                        End If

                                        If lblAcordo.Text = "TAXA ABERTURA" Then
                                            ValorCambio = CType(linhaMoedas.FindControl("txtValorAbertuda"), TextBox).Text
                                        End If

                                    End If
                                    Dim valorCambioFinal As String = ValorCambio
                                    valorCambioFinal = valorCambioFinal.Replace(".", "")
                                    valorCambioFinal = valorCambioFinal.Replace(",", ".")

                                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL_TAXA]  SET [VL_TAXA_BR] = VL_TAXA_CALCULADO * " & valorCambioFinal & ",DT_ATUALIZACAO_CAMBIO = GETDATE(), VL_CAMBIO = " & valorCambioFinal & " WHERE ID_BL_TAXA =" & ID)
                                End If

                            End If

                        Next
                    End If


                End If
            End If

        Next


        grid()
        VerificaTaxas()
        lblSuccess.Text = "Atualização de valor realizada com sucesso!"
        divSuccess.Visible = True


    End Sub

    Private Sub dgvTaxas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxas.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        Dim ds As DataSet
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim lblLogin As Label = TryCast(Page.Master.FindControl("lbllogin"), Label)
        Dim Usuario As String = lblLogin.Text
        Dim resultado As String

        If e.CommandName = "BloquearFCA" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "B", 39, 0, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try


            End If

        ElseIf e.CommandName = "DesbloquearFCA" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "L", 39, 45, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try



            End If
        ElseIf e.CommandName = "BloquearFinanceiro" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "B", 40, 0, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try


            End If

        ElseIf e.CommandName = "DesbloquearFinanceiro" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "L", 40, 45, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try



            End If
        ElseIf e.CommandName = "BloquearDocumental" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "B", 44, 0, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação" & ex.Message
                    Exit Sub

                End Try


            End If

        ElseIf e.CommandName = "DesbloquearDocumental" Then
            ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,0)NR_BL FROM [dbo].[TB_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    Using Status = New NotaFiscal.WsNvocc

                        resultado = Status.DesBloqueio(ds.Tables(0).Rows(0).Item("NR_BL"), "L", 44, 45, Usuario)

                    End Using

                Catch ex As Exception

                    divErro.Visible = True
                    lblErro.Text = "Não foi possivel completar a ação: " & ex.Message
                    Exit Sub

                End Try



            End If
        End If

        If resultado = "BL não localizado!" Then
            divErro.Visible = True
            lblErro.Text = resultado
        Else
            divSuccess.Visible = True
            lblSuccess.Text = "Ação realizada com sucesso!"
        End If
        grid()


    End Sub
End Class