Imports System.IO
Public Class CadastrarEmbarqueHouse
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else

            If Request.QueryString("tipo") = "e" Then
                lblTipoModulo.Text = " EMBARQUE"
                lkProximo.Visible = False
                lkAnterior.Visible = False
                btnVisualizarMBL_Aereo.Text = "Gerar Master"
                btnVisualizarMBL_Maritimo.Text = "Gerar Master"
                ddlTransportador_BasicoAereo.Enabled = True
                txtNomeTransportador_Aereo.Enabled = True
                ddlTransportador_BasicoMaritimo.Enabled = True
                txtNomeTransportador_Maritimo.Enabled = True
                divDocConferidoAereo.Visible = False
                divDocConferidoMaritimo.Visible = False
            ElseIf Request.QueryString("tipo") = "h" Then
                lblTipoModulo.Text = " HOUSE"
                btnVisualizarMBL_Aereo.Text = "Visualizar MBL"
                btnVisualizarMBL_Maritimo.Text = "Visualizar MBL"
                divDocConferidoAereo.Visible = True
                divDocConferidoMaritimo.Visible = True
            End If
            If Request.QueryString("id") <> "" And Not Page.IsPostBack Then
                CarregaCampos()

            Else

                lkProximo.Visible = False
                lkAnterior.Visible = False

            End If

        End If


        Con.Fechar()


    End Sub

    Sub CarregaCampos()
        Session("ID_BL_MASTER") = 0
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT A.ID_BL,ID_SERVICO,FL_TRAKING_AUTOMATICO,ID_BL_MASTER,ID_COTACAO,ISNULL(NR_BL,0)NR_BL,NR_PROCESSO,ID_PARCEIRO_TRANSPORTADOR,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_INDICADOR ,YEAR(DT_ABERTURA)ANO_ABERTURA,
(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_CLIENTE)NM_RAZAO_CLIENTE,
ID_PARCEIRO_IMPORTADOR, ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_PORTO_ORIGEM,ID_PORTO_DESTINO, ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_COMISSARIA,ID_PARCEIRO_AGENTE,ID_INCOTERM,FL_FREE_HAND,ID_TIPO_PAGAMENTO,ID_TIPO_CARGA,ID_TIPO_ESTUFAGEM,NR_CE,CONVERT(varchar,DT_CE, 103)DT_CE,OB_REFERENCIA_COMERCIAL,OB_REFERENCIA_AUXILIAR,NM_RESUMO_MERCADORIA,OB_CLIENTE,OB_AGENTE_INTERNACIONAL,OB_COMERCIAL,OB_OPERACIONAL_INTERNA,CD_RASTREAMENTO_HBL,CD_RASTREAMENTO_MBL,ID_PARCEIRO_ARMAZEM_DESEMBARACO,ID_PARCEIRO_RODOVIARIO,(SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)BL_MASTER,(SELECT DT_CHEGADA FROM TB_BL WHERE TB_BL.ID_BL = A.ID_BL_MASTER)DT_CHEGADA_MASTER,VL_PROFIT_DIVISAO,VL_PROFIT_DIVISAO_CALCULADO,ID_PROFIT_DIVISAO,ISNULL((SELECT B.ID_STATUS_COTACAO FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO),0)ID_STATUS_COTACAO,
(SELECT B.OB_CLIENTE FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO)OB_CLIENTE_COTACAO,
(SELECT B.OB_OPERACIONAL FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO)OB_OPERACIONAL_COTACAO, 
(SELECT NM_TIPO_BL FROM TB_TIPO_BL WHERE ID_TIPO_BL = (SELECT ID_TIPO_BL FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO))NM_TIPO_BL,(SELECT NM_CLIENTE_FINAL FROM TB_CLIENTE_FINAL WHERE ID_CLIENTE_FINAL = (SELECT ID_CLIENTE_FINAL FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO))NM_CLIENTE_FINAL, VL_CARGA, ISNULL(ID_PARCEIRO_RODOVIARIO,0)ID_PARCEIRO_RODOVIARIO,ISNULL(FINAL_DESTINATION,0)FINAL_DESTINATION,ISNULL(FL_EMAIL_COTACAO,0)FL_EMAIL_COTACAO, EMAIL_COTACAO,NR_CONTRATO_ARMADOR, ISNULL(FL_TC4,0)FL_TC4,ISNULL(FL_TC6,0)FL_TC6, ISNULL(A.ID_TIPO_AERONAVE,0)ID_TIPO_AERONAVE,ISNULL(FL_DOC_CONFERIDO,0)FL_DOC_CONFERIDO, 
ISNULL((SELECT B.FL_DTA_HUB FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO),0)FL_DTA_HUB,
ISNULL((SELECT B.FL_LTL FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO),0)FL_LTL,
ISNULL((SELECT B.FL_TRANSP_DEDICADO FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO),0)FL_TRANSP_DEDICADO, ISNULL(ID_STATUS_FRETE_AGENTE,0)ID_STATUS_FRETE_AGENTE FROM TB_BL A
OUTER APPLY FN_DOC_CONFERIDO(A.ID_BL)
WHERE A.ID_BL = " & Request.QueryString("id"))

        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then



                If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
                    'AGENCIAMENTO DE EXPORTACAO MARITIMA
                    'AGENCIAMENTO DE IMPORTACAO MARITIMA

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DTA_HUB")) Then
                        ckbDtaHub_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DTA_HUB")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_LTL")) Then
                        ckbLTL_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_LTL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_TRANSP_DEDICADO")) Then
                        ckbTranspDedicado_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_TRANSP_DEDICADO")
                    End If

                    If ds.Tables(0).Rows(0).Item("ANO_ABERTURA") >= 2022 And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) And ds.Tables(0).Rows(0).Item("NR_BL") <> "0" Then
                        Dim Rastreio As New RastreioService
                        Rastreio.trackingbl(Request.QueryString("id"))
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) Then
                        txtIDMaster_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_COTACAO")) Then
                        Session("ID_COTACAO") = ds.Tables(0).Rows(0).Item("ID_COTACAO")
                        txtID_CotacaoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO")
                    Else
                        txtID_CotacaoMaritimo.Text = 0
                        Session("ID_COTACAO") = 0
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO")) Then
                        If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 10 Then
                            btnVisualizarMBL_Maritimo.Enabled = False
                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("BL_MASTER")) Then
                        txtMBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) Then
                        Session("ID_BL_MASTER") = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        txtHBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                        txtProcesso_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                        lblHouse_Titulo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        txtCodTransportador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                        ddlTransportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CONTRATO_ARMADOR")) Then
                        txtContratoArmador_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_CONTRATO_ARMADOR").ToString()
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")) Then
                        txtCodTranspRodoviario_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                        ddlTranspRodoviario_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                        ddlServico_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FINAL_DESTINATION")) Then
                        ddlFinalDestination_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("FINAL_DESTINATION")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")) Then
                        txtCodCliente_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                        ddlCliente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")) Then
                        txtCodIndicador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")
                        ddlIndicador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                        ddlOrigem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                        ddlDestino_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")) Then
                        txtCodExportador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")
                        ddlExportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")) Then
                        txtCodComissaria_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")
                        ddlComissaria_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")) Then
                        txtCodImportador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")
                        ddlImportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")) Then
                        txtCodTranspRodoviario_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                        ddlTranspRodoviario_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")) Then
                        txtCodAgente_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                        ddlAgente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_INCOTERM")) Then
                        ddlIncoterm_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_INCOTERM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_FREE_HAND")) Then
                        ckbFreeHand_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_FREE_HAND")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_TRAKING_AUTOMATICO")) Then
                        ckTrakingAutomaticoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_TRAKING_AUTOMATICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO")) Then
                        ckDocConferidosMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                        ddlTipoCarga_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                        ddlEstufagem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                            divMercadoriaCNTR_Maritimo.Attributes.CssStyle.Add("display", "block")
                            divMercadoriaBL_Maritimo.Attributes.CssStyle.Add("display", "none")

                        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                            divMercadoriaCNTR_Maritimo.Attributes.CssStyle.Add("display", "block")
                            divMercadoriaBL_Maritimo.Attributes.CssStyle.Add("display", "block")

                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CE")) Then
                        txtCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) Then
                        txtDataCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")) Then
                        txtRefComercial_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")) Then
                        txtResumoMercadoria_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO")) Then
                        ddlDivisaoProfit_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")) Then
                        txtValorDivisaoProfit_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO_CALCULADO")) Then
                        txtProfitCalculado_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO_CALCULADO")
                    End If

                    ckbEmailCotacao_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_EMAIL_COTACAO")
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("EMAIL_COTACAO")) Then
                        txtEmailCotacao_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("EMAIL_COTACAO").ToString()
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_CLIENTE")) Then
                        txtObsCliente_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")) Then
                        txtObsAgente_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_COMERCIAL")) Then
                        txtObsComercial_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")) Then
                        txtObsoperacional_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_CLIENTE_COTACAO")) Then
                        txtObsCliente_CotacaoMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE_COTACAO")
                        txtObsCliente_CotacaoMaritimo.Text = txtObsCliente_CotacaoMaritimo.Text.Replace("<br/>", vbNewLine)
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_COTACAO")) Then
                        txtObsOper_CotacaoMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_COTACAO")
                        txtObsOper_CotacaoMaritimo.Text = txtObsOper_CotacaoMaritimo.Text.Replace("<br/>", vbNewLine)
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TIPO_BL")) Then
                        txtTipoBLMaritimo.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_CLIENTE_FINAL")) Then
                        txtClienteFinalMaritimo.Text = ds.Tables(0).Rows(0).Item("NM_CLIENTE_FINAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_CARGA")) Then
                        txtValorCarga_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_CARGA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")) Then
                        ddlStatusFreteAgente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CHEGADA_MASTER")) Then

                        btnGravar_BasicoMaritimo.Visible = False
                        btnLimpar_BasicoMaritimo.Visible = False
                        btnNovaTaxaMaritimo.Visible = False
                        btnSalvar_TaxaMaritimo.Visible = False
                        btnNovaCargaMaritimo.Visible = False
                        btnSalvar_CargaMaritimo.Visible = False

                        PermissoesEspeciais()

                    End If

                    btnGravar_BasicoAereo.Visible = False
                    btnLimpar_BasicoAereo.Visible = False
                    btnNovaTaxaAereo.Visible = False
                    btnNovaCargaAereo.Visible = False
                    btnGravar_RefAereo.Visible = False
                    btnGravar_ObsAereo.Visible = False
                    btnLimpar_ObsAereo.Visible = False

                    btnCapaAereo.Visible = False
                    btnCapaMaritimo.Visible = True

                ElseIf ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                    'AGENCIAMENTO DE EXPORTAÇÃO AEREO
                    'AGENCIAMENTO DE IMPORTACAO AEREO

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DTA_HUB")) Then
                        ckbDtaHub_BasicoAereo.Checked = ds.Tables(0).Rows(0).Item("FL_DTA_HUB")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_LTL")) Then
                        ckbLTL_BasicoAereo.Checked = ds.Tables(0).Rows(0).Item("FL_LTL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_TRANSP_DEDICADO")) Then
                        ckbTranspDedicado_BasicoAereo.Checked = ds.Tables(0).Rows(0).Item("FL_TRANSP_DEDICADO")
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO")) Then
                        If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 10 Then
                            btnVisualizarMBL_Aereo.Enabled = False
                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                        txtProcesso_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                        lblHouse_Titulo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) Then
                        txtIDMaster_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_COTACAO")) Then
                        Session("ID_COTACAO") = ds.Tables(0).Rows(0).Item("ID_COTACAO")
                        txtID_CotacaoAereo.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO")
                    Else
                        txtID_CotacaoAereo.Text = 0
                        Session("ID_COTACAO") = 0
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("BL_MASTER")) Then
                        txtMBL_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("BL_MASTER")
                        Session("ID_BL_MASTER") = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        txtHBL_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                        ddlServico_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FINAL_DESTINATION")) Then
                        ddlFinalDestination_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("FINAL_DESTINATION")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_AERONAVE")) Then
                        ddlTipoAeronave_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_AERONAVE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        txtCodTransportador_Aereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                        ddlTransportador_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")) Then
                        txtCodTranspRodoviario_Aereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                        ddlTranspRodoviario_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_DESEMBARACO")) Then
                        ddlArmazem_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_DESEMBARACO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")) Then
                        txtCodCliente_Aereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                        ddlCliente_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")) Then
                        txtCodImportador_Aereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")
                        ddlImportador_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                        ddlOrigem_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                        ddlDestino_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")) Then
                        txtCodExportador_Aereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")
                        ddlExportador_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")) Then
                        txtCodIndicador_Aereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")
                        ddlIndicador_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")) Then
                        txtCodComissaria_Aereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")
                        ddlComissaria_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")) Then
                        txtCodAgente_Aereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                        ddlAgente_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_INCOTERM")) Then
                        ddlIncoterm_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_INCOTERM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_FREE_HAND")) Then
                        ckbFreeHand_BasicoAereo.Checked = ds.Tables(0).Rows(0).Item("FL_FREE_HAND")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_TC4")) Then
                        ckbTC4_BasicoAereo.Checked = ds.Tables(0).Rows(0).Item("FL_TC4")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_TC6")) Then
                        ckbTC6_BasicoAereo.Checked = ds.Tables(0).Rows(0).Item("FL_TC6")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_TRAKING_AUTOMATICO")) Then
                        ckTrakingAutomaticoAereo.Checked = ds.Tables(0).Rows(0).Item("FL_TRAKING_AUTOMATICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO")) Then
                        ckDocConferidosAereo.Checked = ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If



                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                        ddlEstufagem_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")

                        If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                            divMercadoriaCNTR_Aereo.Attributes.CssStyle.Add("display", "block")
                            divMercadoriaBL_Aereo.Attributes.CssStyle.Add("display", "none")

                        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                            divMercadoriaCNTR_Aereo.Attributes.CssStyle.Add("display", "BLOCK")
                            divMercadoriaBL_Aereo.Attributes.CssStyle.Add("display", "block")

                        End If
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CE")) Then
                        txtNumeroCE_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) Then
                        txtDataCE_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")) Then
                        txtRefComercial_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_REFERENCIA_AUXILIAR")) Then
                        txtRefAuxiliar_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("OB_REFERENCIA_AUXILIAR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO")) Then
                        ddlDivisaoProfit_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")) Then
                        txtValorDivisaoProfit_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                    End If

                    ckbEmailCotacao_BasicoAereo.Checked = ds.Tables(0).Rows(0).Item("FL_EMAIL_COTACAO")
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("EMAIL_COTACAO")) Then
                        txtEmailCotacao_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("EMAIL_COTACAO").ToString()
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")) Then
                        txtResumoMercadoria_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO_CALCULADO")) Then
                        txtProfitCalculado_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO_CALCULADO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_CLIENTE")) Then
                        txtObsCliente_ObsAereo.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")) Then
                        txtObsAgente_ObsAereo.Text = ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_COMERCIAL")) Then
                        txtObsComercial_ObsAereo.Text = ds.Tables(0).Rows(0).Item("OB_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")) Then
                        txtObsOperacional_ObsAereo.Text = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_CLIENTE_COTACAO")) Then
                        txtObsCliente_CotacaoAereo.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE_COTACAO")
                        txtObsCliente_CotacaoAereo.Text = txtObsCliente_CotacaoAereo.Text.Replace("<br/>", vbNewLine)
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_COTACAO")) Then
                        txtObsOper_CotacaoAereo.Text = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_COTACAO")
                        txtObsOper_CotacaoAereo.Text = txtObsOper_CotacaoAereo.Text.Replace("<br/>", vbNewLine)
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TIPO_BL")) Then
                        txtTipoBLAereo.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_CLIENTE_FINAL")) Then
                        txtClienteFinalAereo.Text = ds.Tables(0).Rows(0).Item("NM_CLIENTE_FINAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_CARGA")) Then
                        txtValorCarga_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("VL_CARGA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")) Then
                        ddlStatusFreteAgente_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CHEGADA_MASTER")) Then

                        btnGravar_BasicoAereo.Visible = False
                        btnLimpar_BasicoAereo.Visible = False
                        btnNovaTaxaAereo.Visible = False
                        btnSalvar_TaxaAereo.Visible = False
                        btnNovaCargaAereo.Visible = False
                        btnSalvar_CargaAereo.Visible = False

                        PermissoesEspeciais()

                    End If


                    btnGravar_BasicoMaritimo.Visible = False
                    btnLimpar_BasicoMaritimo.Visible = False
                    btnNovaTaxaMaritimo.Visible = False
                    btnNovaCargaMaritimo.Visible = False
                    btnGravar_RefMaritimo.Visible = False
                    btnGravar_ObsMaritimo.Visible = False
                    btnLimpar_ObsMaritimo.Visible = False

                    btnCapaMaritimo.Visible = False
                    btnCapaAereo.Visible = True


                Else

                    'OUTROS


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DTA_HUB")) Then
                        ckbDtaHub_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DTA_HUB")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_LTL")) Then
                        ckbLTL_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_LTL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_TRANSP_DEDICADO")) Then
                        ckbTranspDedicado_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_TRANSP_DEDICADO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) Then
                        txtIDMaster_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_COTACAO")) Then
                        Session("ID_COTACAO") = ds.Tables(0).Rows(0).Item("ID_COTACAO")
                        txtID_CotacaoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO")

                    Else
                        Session("ID_COTACAO") = 0
                        txtID_CotacaoMaritimo.Text = 0
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO")) Then
                        If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 10 Then
                            btnVisualizarMBL_Maritimo.Enabled = False
                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("BL_MASTER")) Then
                        txtMBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) Then
                        Session("ID_BL_MASTER") = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        txtHBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                        txtProcesso_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                        lblHouse_Titulo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        txtCodTransportador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                        ddlTransportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")) Then
                        txtCodTranspRodoviario_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                        ddlTranspRodoviario_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                        ddlServico_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")) Then
                        txtCodCliente_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                        ddlCliente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")) Then
                        txtCodIndicador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")
                        ddlIndicador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                        ddlOrigem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                        ddlDestino_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FINAL_DESTINATION")) Then
                        ddlFinalDestination_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("FINAL_DESTINATION")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")) Then
                        txtCodExportador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")
                        ddlExportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")) Then
                        txtCodComissaria_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")
                        ddlComissaria_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")) Then
                        txtCodImportador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")
                        ddlImportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")) Then
                        txtCodTranspRodoviario_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                        ddlTranspRodoviario_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")) Then
                        txtCodAgente_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                        ddlAgente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_INCOTERM")) Then
                        ddlIncoterm_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_INCOTERM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_FREE_HAND")) Then
                        ckbFreeHand_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_FREE_HAND")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO")) Then
                        ckDocConferidosMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                        ddlTipoCarga_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                        ddlEstufagem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                            divMercadoriaCNTR_Maritimo.Attributes.CssStyle.Add("display", "block")
                            divMercadoriaBL_Maritimo.Attributes.CssStyle.Add("display", "none")

                        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                            divMercadoriaCNTR_Maritimo.Attributes.CssStyle.Add("display", "block")
                            divMercadoriaBL_Maritimo.Attributes.CssStyle.Add("display", "block")

                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CE")) Then
                        txtCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) Then
                        txtDataCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")) Then
                        txtRefComercial_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_REFERENCIA_AUXILIAR")) Then
                        txtRefAuxiliar_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_REFERENCIA_AUXILIAR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")) Then
                        txtResumoMercadoria_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO")) Then
                        ddlDivisaoProfit_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")) Then
                        txtValorDivisaoProfit_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO_CALCULADO")) Then
                        txtProfitCalculado_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO_CALCULADO")
                    End If

                    ckbEmailCotacao_BasicoMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_EMAIL_COTACAO")
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("EMAIL_COTACAO")) Then
                        txtEmailCotacao_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("EMAIL_COTACAO").ToString()
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_CLIENTE")) Then
                        txtObsCliente_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")) Then
                        txtObsAgente_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_COMERCIAL")) Then
                        txtObsComercial_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")) Then
                        txtObsoperacional_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_CLIENTE_COTACAO")) Then
                        txtObsCliente_CotacaoMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE_COTACAO")
                        txtObsCliente_CotacaoMaritimo.Text = txtObsCliente_CotacaoMaritimo.Text.Replace("<br/>", vbNewLine)
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_COTACAO")) Then
                        txtObsOper_CotacaoMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_COTACAO")
                        txtObsOper_CotacaoMaritimo.Text = txtObsOper_CotacaoMaritimo.Text.Replace("<br/>", vbNewLine)
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TIPO_BL")) Then
                        txtTipoBLMaritimo.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_CLIENTE_FINAL")) Then
                        txtClienteFinalMaritimo.Text = ds.Tables(0).Rows(0).Item("NM_CLIENTE_FINAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_CARGA")) Then
                        txtValorCarga_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_CARGA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")) Then
                        ddlStatusFreteAgente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CHEGADA_MASTER")) Then

                        btnGravar_BasicoMaritimo.Visible = False
                        btnLimpar_BasicoMaritimo.Visible = False
                        btnNovaTaxaMaritimo.Visible = False
                        btnSalvar_TaxaMaritimo.Visible = False
                        btnNovaCargaMaritimo.Visible = False
                        btnSalvar_CargaMaritimo.Visible = False

                        PermissoesEspeciais()

                    End If

                    btnGravar_BasicoAereo.Visible = False
                    btnLimpar_BasicoAereo.Visible = False
                    btnNovaTaxaAereo.Visible = False
                    btnNovaCargaAereo.Visible = False
                    btnGravar_RefAereo.Visible = False
                    btnGravar_ObsAereo.Visible = False
                    btnLimpar_ObsAereo.Visible = False

                    btnCapaAereo.Visible = False
                    btnCapaMaritimo.Visible = True
                End If


            End If

        End If
    End Sub

    Private Sub dgvCargaMaritimo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvCargaMaritimo.RowCommand
        divSuccess_CargaMaritimo1.Visible = False
        divErro_CargaMaritimo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErro_CargaMaritimo1.Text = "Usuário não tem permissão para realizar exclusões"
                divErro_CargaMaritimo1.Visible = True
            Else
                ds = Con.ExecutarQuery("SELECT ID_CNTR_BL FROM TB_CARGA_BL Where ID_CARGA_BL = " & ID)

                Con.ExecutarQuery("DELETE From TB_CARGA_BL Where ID_CARGA_BL = " & ID)

                If ds.Tables(0).Rows.Count > 0 Then
                    Con.ExecutarQuery("DELETE From TB_AMR_CNTR_BL Where ID_CNTR_BL = " & ds.Tables(0).Rows(0).Item("ID_CNTR_BL") & "AND ID_CNTR_BL NOT IN (SELECT ID_CNTR_BL FROM TB_CARGA_BL WHERE ID_BL = " & txtID_BasicoMaritimo.Text & ") AND ID_BL = " & txtID_BasicoMaritimo.Text)
                End If

                Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)

                Dim Calcula As New CalculaBL
                Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_BL_TAXA FROM TB_BL_TAXA A WHERE  ID_BASE_CALCULO_TAXA IS NOT NULL AND VL_TAXA IS NOT NULL AND VL_TAXA <> 0 AND ID_BASE_CALCULO_TAXA <> 1 AND ID_MOEDA <> 0 AND ISNULL(ID_BL_TAXA_MASTER,0) = 0 AND ISNULL(ID_BL_MASTER,0) = 0  AND ID_BL = " & txtID_BasicoMaritimo.Text & " And ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER= A.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL  AND ID_BL_TAXA IS NOT NULL)")
                If dsTaxa.Tables(0).Rows.Count > 0 Then
                    For Each linha As DataRow In dsTaxa.Tables(0).Rows
                        Dim retorno As String = Calcula.Calcular(linha.Item("ID_BL_TAXA").ToString())
                    Next
                End If

                Calcula.CalculoProfit(txtID_BasicoMaritimo.Text)
                dgvTaxaMaritimoCompras.DataBind()
                dgvTaxaMaritimoVendas.DataBind()

                lblSuccess_CargaMaritimo1.Text = "Registro deletado!"
                divSuccess_CargaMaritimo1.Visible = True
                dgvCargaMaritimo.DataBind()
                dgvTaxaMaritimoCompras.DataBind()
                dgvTaxaMaritimoVendas.DataBind()

            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument
            dsNCM_CargaMaritimo.DataBind()
            ds = Con.ExecutarQuery("select ID_CARGA_BL,ID_TIPO_CARGA,ID_MERCADORIA,ID_NCM,(select CD_NCM +' - '+ NM_NCM from TB_NCM WHERE ID_NCM = A.ID_NCM)NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,isnull(ID_CNTR_BL,0)ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,ID_TIPO_CNTR from TB_CARGA_BL A
WHERE ID_CARGA_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CARGA_BL")) Then
                    txtID_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_MERCADORIA")) Then
                    txtQtdVolumes_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                    ddlMercadoria_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NCM")) Then

                    If ds.Tables(0).Rows(0).Item("ID_NCM") > 0 Then
                        txtIDNCM_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_NCM")
                        dsNCM_CargaMaritimo.DataBind()
                        ddlNCM_CargaMaritimo.DataBind()
                        ddlNCM_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_NCM")
                    Else
                        txtIDNCM_CargaMaritimo.Text = 0
                        dsNCM_CargaMaritimo.DataBind()
                        ddlNCM_CargaMaritimo.DataBind()
                    End If
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                    txtPesoBruto_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_M3")) Then
                    txtPesoVolumetrico_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_M3")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_EMBALAGEM")) Then
                    ddlEmbalagem_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_EMBALAGEM").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DS_GRUPO_NCM")) Then
                    txtGrupoNCM_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("DS_GRUPO_NCM")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DS_MERCADORIA")) Then
                    txtDescMercadoriaCNTR_Maritimo.Text = ds.Tables(0).Rows(0).Item("DS_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CNTR")) Then
                    ddlTipoContainer_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CNTR").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CNTR_BL")) Then
                    Dim Sql As String = "SELECT ID_CNTR_BL, NR_CNTR FROM TB_CNTR_BL WHERE ID_CNTR_BL = " & ds.Tables(0).Rows(0).Item("ID_CNTR_BL") & " OR ID_BL_MASTER = " & Session("ID_BL_MASTER") & "
union Select 0, 'Selecione' FROM [dbo].[TB_CNTR_BL] ORDER BY ID_CNTR_BL"
                    Dim ds1 As DataSet = Con.ExecutarQuery(Sql)
                    If ds1.Tables(0).Rows.Count > 0 Then
                        dsCNTR.SelectCommand = Sql
                        ddlNumeroCNTR_CargaMaritimo.DataBind()
                    End If

                    ddlNumeroCNTR_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CNTR_BL").ToString()

                    ds1 = Con.ExecutarQuery("SELECT ISNULL(VL_PESO_TARA,0)VL_PESO_TARA,ISNULL(NR_LACRE,0)NR_LACRE,ID_TIPO_CNTR FROM TB_CNTR_BL WHERE ID_CNTR_BL = " & ds.Tables(0).Rows(0).Item("ID_CNTR_BL").ToString())
                    If ds1.Tables(0).Rows.Count > 0 Then
                        txtNumeroLacre_CargaMaritimo.Text = ds1.Tables(0).Rows(0).Item("NR_LACRE")
                        txtValorTara_CargaMaritimo.Text = ds1.Tables(0).Rows(0).Item("VL_PESO_TARA")
                        ddlTipoContainer_CargaMaritimo.SelectedValue = ds1.Tables(0).Rows(0).Item("ID_TIPO_CNTR")
                    End If
                    Con.Fechar()
                End If

                mpeCargaMaritimo.Show()


            End If


        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_BL,ID_MERCADORIA,ID_TIPO_CARGA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA,ID_TIPO_CNTR )  select ID_BL,ID_MERCADORIA,ID_TIPO_CARGA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA,ID_TIPO_CNTR from TB_CARGA_BL WHERE ID_CARGA_BL = " & ID)
            lblSuccess_CargaMaritimo1.Text = "Registro duplicado!"
            divSuccess_CargaMaritimo1.Visible = True
            dgvCargaMaritimo.DataBind()


            Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)


            Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_BL_TAXA FROM TB_BL_TAXA A WHERE  ID_BASE_CALCULO_TAXA IS NOT NULL AND VL_TAXA IS NOT NULL AND VL_TAXA <> 0 AND ID_BASE_CALCULO_TAXA <> 1 AND ID_MOEDA <> 0 AND ISNULL(ID_BL_TAXA_MASTER,0) = 0 AND ISNULL(ID_BL_MASTER,0) = 0  AND ID_BL = " & txtID_BasicoMaritimo.Text & " And ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER= A.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL  AND ID_BL_TAXA IS NOT NULL)")
            If dsTaxa.Tables(0).Rows.Count > 0 Then
                For Each linha As DataRow In dsTaxa.Tables(0).Rows
                    Dim Calcula As New CalculaBL
                    Dim retorno As String = Calcula.Calcular(linha.Item("ID_BL_TAXA").ToString())
                Next

            End If


        End If
        Con.Fechar()

    End Sub

    Private Sub dgvTaxaMaritimoCompras_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxaMaritimoCompras.RowCommand

        Try
            divSuccess_TaxaMaritimo1.Visible = False
            divErro_TaxaMaritimo1.Visible = False
            Dim Con As New Conexao_sql
            Dim ds As DataSet
            Con.Conectar()
            If e.CommandName = "Excluir" Then


                Dim ID As String = e.CommandArgument

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErro_TaxaMaritimo1.Text = "Usuário não tem permissão para realizar exclusões"
                    divErro_TaxaMaritimo1.Visible = True

                Else

                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxaMaritimo1.Visible = True
                        lblErro_TaxaMaritimo1.Text = "Não foi possível excluir o registro:taxa já enviada para contas a pagar/receber!"
                    Else

                        Dim finaliza As New FinalizaCotacao
                        If finaliza.TaxaBloqueada(ID, "BL") = True Then
                            divErro_TaxaMaritimo1.Visible = True
                            lblErro_TaxaMaritimo1.Text = "Não foi possível excluir o registro: taxa já enviada para contas a pagar/receber ou invoice!"
                        Else

                            Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                            lblSuccess_TaxaMaritimo1.Text = "Registro deletado!"
                            divSuccess_TaxaMaritimo1.Visible = True
                            dgvTaxaMaritimoCompras.DataBind()
                        End If


                    End If
                End If

            ElseIf e.CommandName = "visualizar" Then
                Dim ID As String = e.CommandArgument

                ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO,A.CD_PR, ID_BL_TAXA_MASTER, ID_BL_MASTER, CD_ORIGEM_INF,QTD_BASE_CALCULO
from TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 

WHERE A.ID_BL_TAXA = " & ID)
                If ds.Tables(0).Rows.Count > 0 Then

                    'Taxas
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA")) Then
                        txtID_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL_TAXA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                        ddlDespesa_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DECLARADO")) Then
                        ckbDeclarado_TaxaMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DECLARADO")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")) Then
                        ckbProfit_TaxaMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                        ddlOrigemPagamento_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                        ddlDestinatarioCob_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                        'ddlDestinatarioCob_TaxaMaritimo.Enabled = False
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                        ddlBaseCalculo_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")

                        If ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 38 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 40 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 41 Then
                            txtQtdBaseCalculo_TaxaMaritimo.Enabled = True
                        Else
                            txtQtdBaseCalculo_TaxaMaritimo.Enabled = False
                        End If

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")) Then
                        txtQtdBaseCalculo_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                        ddlMoedaCompra_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                        txtValorCompra_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                        txtValorVenda_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")) Then
                        txtMinCompra_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                        txtMinVenda_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")) Then
                        ddlStatusPagamento_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                        txtObs_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_TAXAS")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")) Then
                        ddlEmpresa_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_PR")) Then
                        Session("CD_PR") = ds.Tables(0).Rows(0).Item("CD_PR")
                        If ds.Tables(0).Rows(0).Item("CD_PR") = "P" Then
                            divVendaMaritimo.Visible = False

                            divCompraMaritimo.Visible = True

                        ElseIf ds.Tables(0).Rows(0).Item("CD_PR") = "R" Then
                            divVendaMaritimo.Visible = True

                            divCompraMaritimo.Visible = False
                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")) Then
                        If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                            btnSalvar_TaxaMaritimo.Visible = False
                        Else
                            btnSalvar_TaxaMaritimo.Visible = True
                        End If
                    Else
                        btnSalvar_TaxaMaritimo.Visible = True
                    End If


                    Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD
from TB_BL_TAXA A 
INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
INNER JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 
WHERE  DT_CANCELAMENTO IS NULL and ID_BL_TAXA_MASTER in (select ID_BL_TAXA
from TB_BL_TAXA 
WHERE ID_BL=" & Session("ID_BL_MASTER") & ")")
                    If ds2.Tables(0).Rows(0).Item("QTD") > 0 And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA_MASTER")) Then
                        btnSalvar_TaxaMaritimo.Visible = False
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_ORIGEM_INF")) Then
                        If ds.Tables(0).Rows(0).Item("CD_ORIGEM_INF") = "COTA" Then
                            btnSalvar_TaxaMaritimo.Visible = False
                        End If
                    End If

                    Dim finaliza As New FinalizaCotacao
                    If finaliza.TaxaBloqueada(ID, "BL") = True Then
                        btnSalvar_TaxaMaritimo.Visible = False
                    End If

                    lblTipoEmpresa_Maritimo.Text = "Fornecedor:"
                    mpeTaxaMaritimo.Show()

                End If

            ElseIf e.CommandName = "Duplicar" Then
                Dim ID As String = e.CommandArgument
                Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


                If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                    divErro_TaxaMaritimo1.Visible = True
                    lblErro_TaxaMaritimo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
                Else

                    Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF,QTD_BASE_CALCULO,DT_CRIACAO,ID_USUARIO_CRIACAO)  select ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,'OPER',QTD_BASE_CALCULO,GETDATE()," & Session("ID_USUARIO") & " from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                    lblSuccess_TaxaMaritimo1.Text = "Registro duplicado!"
                    divSuccess_TaxaMaritimo1.Visible = True
                    dgvTaxaMaritimoCompras.DataBind()
                End If
            ElseIf e.CommandName = "Historico" Then

                dsHistorico.SelectParameters("ID_BL_TAXA").DefaultValue = e.CommandArgument
                dgvHistoricoMaritimo.DataBind()
                mpeHistoricoMaritimo.Show()

            End If
            Con.Fechar()
            GridTaxaMaritimoCompras()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub dgvTaxaMaritimoVendas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxaMaritimoVendas.RowCommand

        Try
            divSuccess_TaxaMaritimo1.Visible = False
            divErro_TaxaMaritimo1.Visible = False
            Dim Con As New Conexao_sql
            Dim ds As DataSet
            Con.Conectar()
            If e.CommandName = "Excluir" Then


                Dim ID As String = e.CommandArgument

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErro_TaxaMaritimo1.Text = "Usuário não tem permissão para realizar exclusões"
                    divErro_TaxaMaritimo1.Visible = True

                Else

                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxaMaritimo1.Visible = True
                        lblErro_TaxaMaritimo1.Text = "Não foi possível excluir o registro:taxa já enviada para contas a pagar/receber!"
                    Else

                        Dim finaliza As New FinalizaCotacao
                        If finaliza.TaxaBloqueada(ID, "BL") = True Then
                            divErro_TaxaMaritimo1.Visible = True
                            lblErro_TaxaMaritimo1.Text = "Não foi possível excluir o registro:taxa já enviada para contas a pagar/receber ou invoice!"
                        Else
                            Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                            lblSuccess_TaxaMaritimo1.Text = "Registro deletado!"
                            divSuccess_TaxaMaritimo1.Visible = True
                            dgvTaxaMaritimoVendas.DataBind()

                        End If


                    End If
                End If

            ElseIf e.CommandName = "visualizar" Then
                Dim ID As String = e.CommandArgument

                ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO,A.CD_PR,ID_BL_TAXA_MASTER, ID_BL_MASTER,CD_ORIGEM_INF,QTD_BASE_CALCULO
from TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 

WHERE A.ID_BL_TAXA = " & ID)
                If ds.Tables(0).Rows.Count > 0 Then

                    'Taxas
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA")) Then
                        txtID_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL_TAXA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                        ddlDespesa_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DECLARADO")) Then
                        ckbDeclarado_TaxaMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DECLARADO")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")) Then
                        ckbProfit_TaxaMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                        ddlOrigemPagamento_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                        ddlDestinatarioCob_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                        ddlBaseCalculo_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")

                        If ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 38 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 40 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 41 Then
                            txtQtdBaseCalculo_TaxaMaritimo.Enabled = True
                        Else
                            txtQtdBaseCalculo_TaxaMaritimo.Enabled = False
                        End If

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")) Then
                        txtQtdBaseCalculo_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                        ddlMoedaVenda_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                        txtValorCompra_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                        txtValorVenda_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")) Then
                        txtMinCompra_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                        txtMinVenda_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")) Then
                        ddlStatusPagamento_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                        txtObs_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_TAXAS")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")) Then
                        ddlEmpresa_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_PR")) Then
                        Session("CD_PR") = ds.Tables(0).Rows(0).Item("CD_PR")
                        If ds.Tables(0).Rows(0).Item("CD_PR") = "P" Then
                            divVendaMaritimo.Visible = False

                            divCompraMaritimo.Visible = True

                        ElseIf ds.Tables(0).Rows(0).Item("CD_PR") = "R" Then
                            divVendaMaritimo.Visible = True

                            divCompraMaritimo.Visible = False
                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")) Then
                        If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                            btnSalvar_TaxaMaritimo.Visible = False
                        Else
                            btnSalvar_TaxaMaritimo.Visible = True
                        End If
                    Else
                        btnSalvar_TaxaMaritimo.Visible = True
                    End If



                    Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD
from TB_BL_TAXA A 
INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
INNER JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 
WHERE  DT_CANCELAMENTO IS NULL and ID_BL_TAXA_MASTER in (select ID_BL_TAXA
from TB_BL_TAXA 
WHERE ID_BL=" & Session("ID_BL_MASTER") & ")")
                    If ds2.Tables(0).Rows(0).Item("QTD") > 0 And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA_MASTER")) Then
                        btnSalvar_TaxaMaritimo.Visible = False
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_ORIGEM_INF")) Then
                        If ds.Tables(0).Rows(0).Item("CD_ORIGEM_INF") = "COTA" Then
                            btnSalvar_TaxaMaritimo.Visible = False
                        End If
                    End If


                    lblTipoEmpresa_Maritimo.Text = "Parceiro:"

                    Dim finaliza As New FinalizaCotacao
                    If finaliza.TaxaBloqueada(ID, "BL") = True Then
                        btnSalvar_TaxaMaritimo.Visible = False
                    End If


                    mpeTaxaMaritimo.Show()

                End If

            ElseIf e.CommandName = "Duplicar" Then
                Dim ID As String = e.CommandArgument
                Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


                If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                    divErro_TaxaMaritimo1.Visible = True
                    lblErro_TaxaMaritimo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
                Else

                    Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF,QTD_BASE_CALCULO,DT_CRIACAO,ID_USUARIO_CRIACAO)  select ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,'OPER',QTD_BASE_CALCULO,GETDATE()," & Session("ID_USUARIO") & " from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                    lblSuccess_TaxaMaritimo1.Text = "Registro duplicado!"
                    divSuccess_TaxaMaritimo1.Visible = True
                    dgvTaxaMaritimoVendas.DataBind()
                End If
            End If
            Con.Fechar()
            GridTaxaMaritimoVendas()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub dgvTaxaAereoVendas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxaAereoVendas.RowCommand
        divSuccess_TaxaAereo1.Visible = False
        divErro_TaxaAereo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErro_TaxaAereo1.Text = "Usuário não tem permissão para realizar exclusões"
                divErro_TaxaAereo1.Visible = True

            Else

                Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


                If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                    divErro_TaxaAereo1.Visible = True
                    lblErro_TaxaAereo1.Text = "Não foi possível excluir o registro: taxa já enviada para contas a pagar/receber!"
                Else
                    Dim finaliza As New FinalizaCotacao
                    If finaliza.TaxaBloqueada(ID, "BL") = True Then
                        divErro_TaxaAereo1.Visible = True
                        lblErro_TaxaAereo1.Text = "Não foi possível excluir o registro: taxa já enviada para contas a pagar/receber ou invoice!"
                    Else

                        Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                        lblSuccess_TaxaAereo1.Text = "Registro deletado!"
                        divSuccess_TaxaAereo1.Visible = True
                        dgvTaxaAereoVendas.DataBind()

                    End If

                End If
            End If


        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO,A.CD_PR,ID_BL_TAXA_MASTER, ID_BL_MASTER,CD_ORIGEM_INF,QTD_BASE_CALCULO
from TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 

WHERE A.ID_BL_TAXA =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA")) Then
                    txtID_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                    ddlDespesa_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DECLARADO")) Then
                    ckbDeclarado_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_DECLARADO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")) Then
                    ckbProfit_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    ddlTipoPagamento_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                    ddlOrigemPagamento_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                    ddlDestinatarioCob_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculo_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")

                    If ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 38 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 40 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 41 Then
                        txtQtdBaseCalculo_TaxaAereo.Enabled = True
                    Else
                        txtQtdBaseCalculo_TaxaAereo.Enabled = False
                    End If

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")) Then
                    txtQtdBaseCalculo_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                    ddlMoedaCompra_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtValorCompra_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                    txtValorVenda_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")) Then
                    txtMinCompra_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                    txtMinVenda_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                    txtObs_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("OB_TAXAS")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")) Then
                    ddlEmpresa_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_PR")) Then
                    Session("CD_PR") = ds.Tables(0).Rows(0).Item("CD_PR")
                    If ds.Tables(0).Rows(0).Item("CD_PR") = "P" Then
                        divVendaAereo.Visible = False

                        divCompraAereo.Visible = True
                    ElseIf ds.Tables(0).Rows(0).Item("CD_PR") = "R" Then
                        divVendaAereo.Visible = True

                        divCompraAereo.Visible = False
                    End If
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")) Then
                    If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        btnSalvar_TaxaAereo.Visible = False
                    Else
                        btnSalvar_TaxaAereo.Visible = True
                    End If
                Else
                    btnSalvar_TaxaAereo.Visible = True
                End If


                Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD
from TB_BL_TAXA A 
INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
INNER JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 
WHERE  DT_CANCELAMENTO IS NULL and ID_BL_TAXA_MASTER in (select ID_BL_TAXA
from TB_BL_TAXA 
WHERE ID_BL=" & Session("ID_BL_MASTER") & ")")
                If ds2.Tables(0).Rows(0).Item("QTD") > 0 And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA_MASTER")) Then
                    btnSalvar_TaxaAereo.Visible = False
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_ORIGEM_INF")) Then
                    If ds.Tables(0).Rows(0).Item("CD_ORIGEM_INF") = "COTA" Then
                        btnSalvar_TaxaAereo.Visible = False
                    End If
                End If

                Dim finaliza As New FinalizaCotacao
                If finaliza.TaxaBloqueada(ID, "BL") = True Then
                    btnSalvar_TaxaAereo.Visible = False
                End If

                lblTipoEmpresa_Aereo.Text = "Parceiro:"
                mpeTaxaAereo.Show()

            End If
        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument

            Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


            If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                divErro_TaxaAereo1.Visible = True
                lblErro_TaxaAereo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
            Else

                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF,QTD_BASE_CALCULO,DT_CRIACAO,ID_USUARIO_CRIACAO)  select ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,'OPER',QTD_BASE_CALCULO,GETDATE()," & Session("ID_USUARIO") & " from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                lblSuccess_TaxaAereo1.Text = "Registro duplicado!"
                divSuccess_TaxaAereo1.Visible = True
                dgvTaxaAereoVendas.DataBind()
            End If
        End If
        GridTaxaAereoVendas()

        Con.Fechar()
    End Sub

    Private Sub dgvTaxaAereoCompras_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxaAereoCompras.RowCommand
        divSuccess_TaxaAereo1.Visible = False
        divErro_TaxaAereo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErro_TaxaAereo1.Text = "Usuário não tem permissão para realizar exclusões"
                divErro_TaxaAereo1.Visible = True

            Else

                Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


                If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                    divErro_TaxaAereo1.Visible = True
                    lblErro_TaxaAereo1.Text = "Não foi possível excluir o registro: taxa já enviada para contas a pagar/receber!"
                Else

                    Dim finaliza As New FinalizaCotacao
                    If finaliza.TaxaBloqueada(ID, "BL") = True Then
                        divErro_TaxaAereo1.Visible = True
                        lblErro_TaxaAereo1.Text = "Não foi possível excluir o registro: taxa já enviada para contas a pagar/receber ou invoice!"
                    Else


                        Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                        lblSuccess_TaxaAereo1.Text = "Registro deletado!"
                        divSuccess_TaxaAereo1.Visible = True
                        dgvTaxaAereoCompras.DataBind()
                    End If



                End If
            End If


        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO,A.CD_PR, ID_BL_TAXA_MASTER, ID_BL_MASTER,CD_ORIGEM_INF,QTD_BASE_CALCULO
from TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 

WHERE A.ID_BL_TAXA =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA")) Then
                    txtID_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                    ddlDespesa_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DECLARADO")) Then
                    ckbDeclarado_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_DECLARADO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")) Then
                    ckbProfit_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    ddlTipoPagamento_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                    ddlOrigemPagamento_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                    ddlDestinatarioCob_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                    ' ddlDestinatarioCob_TaxaAereo.Enabled = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculo_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
                    If ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 38 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 40 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 41 Then
                        txtQtdBaseCalculo_TaxaAereo.Enabled = True
                    Else
                        txtQtdBaseCalculo_TaxaAereo.Enabled = False
                    End If

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")) Then
                    txtQtdBaseCalculo_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                    ddlMoedaCompra_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtValorCompra_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                    txtValorVenda_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")) Then
                    txtMinCompra_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                    txtMinVenda_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                    txtObs_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("OB_TAXAS")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")) Then
                    ddlEmpresa_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_PR")) Then
                    Session("CD_PR") = ds.Tables(0).Rows(0).Item("CD_PR")
                    If ds.Tables(0).Rows(0).Item("CD_PR") = "P" Then
                        divVendaAereo.Visible = False

                        divCompraAereo.Visible = True
                    ElseIf ds.Tables(0).Rows(0).Item("CD_PR") = "R" Then
                        divVendaAereo.Visible = True

                        divCompraAereo.Visible = False
                    End If
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")) Then
                    If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        btnSalvar_TaxaAereo.Visible = False
                    Else
                        btnSalvar_TaxaAereo.Visible = True
                    End If
                Else
                    btnSalvar_TaxaAereo.Visible = True
                End If



                Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD
from TB_BL_TAXA A 
INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
INNER JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 
WHERE  DT_CANCELAMENTO IS NULL and ID_BL_TAXA_MASTER in (select ID_BL_TAXA
from TB_BL_TAXA 
WHERE ID_BL=" & Session("ID_BL_MASTER") & ")")
                If ds2.Tables(0).Rows(0).Item("QTD") > 0 And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA_MASTER")) Then
                    btnSalvar_TaxaAereo.Visible = False
                End If


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_ORIGEM_INF")) Then
                    If ds.Tables(0).Rows(0).Item("CD_ORIGEM_INF") = "COTA" Then
                        btnSalvar_TaxaAereo.Visible = False
                    End If
                End If



                Dim finaliza As New FinalizaCotacao
                If finaliza.TaxaBloqueada(ID, "BL") = True Then
                    btnSalvar_TaxaAereo.Visible = False
                End If

                mpeTaxaAereo.Show()

            End If
        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument

            Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


            If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                divErro_TaxaAereo1.Visible = True
                lblErro_TaxaAereo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
            Else

                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF,QTD_BASE_CALCULO,DT_CRIACAO,ID_USUARIO_CRIACAO)  select ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,'OPER',QTD_BASE_CALCULO,GETDATE()," & Session("ID_USUARIO") & " from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                lblSuccess_TaxaAereo1.Text = "Registro duplicado!"
                divSuccess_TaxaAereo1.Visible = True
                dgvTaxaAereoCompras.DataBind()
            End If


        ElseIf e.CommandName = "Historico" Then

            dsHistorico.SelectParameters("ID_BL_TAXA").DefaultValue = e.CommandArgument
            dgvHistoricoAereo.DataBind()
            mpeHistoricoAereo.Show()
        End If


        GridTaxaAereoCompras()

        Con.Fechar()
    End Sub

    Private Sub dgvCargaAereo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvCargaAereo.RowCommand
        divSuccess_CargaAereo1.Visible = False
        divErro_CargaAereo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErro_CargaAereo1.Text = "Usuário não tem permissão para realizar exclusões"
                divErro_CargaAereo1.Visible = True
            Else
                Con.ExecutarQuery("DELETE From TB_CARGA_BL Where ID_CARGA_BL = " & ID)
                Con.ExecutarQuery("DELETE From TB_CARGA_BL_DIMENSAO Where ID_CARGA_BL = " & ID)
                sumMedidasAereo(ID)

                Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text)

                Dim Calcula As New CalculaBL
                Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_BL_TAXA FROM TB_BL_TAXA A WHERE  ID_BASE_CALCULO_TAXA IS NOT NULL AND VL_TAXA IS NOT NULL AND VL_TAXA <> 0 AND ID_BASE_CALCULO_TAXA <> 1 AND ID_MOEDA <> 0 AND ISNULL(ID_BL_TAXA_MASTER,0) = 0 AND ISNULL(ID_BL_MASTER,0) = 0  AND ID_BL = " & txtID_BasicoAereo.Text & " And ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER= A.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL  AND ID_BL_TAXA IS NOT NULL)")
                If dsTaxa.Tables(0).Rows.Count > 0 Then
                    For Each linha As DataRow In dsTaxa.Tables(0).Rows
                        Dim retorno As String = Calcula.Calcular(linha.Item("ID_BL_TAXA").ToString())
                    Next
                End If

                Calcula.CalculoProfit(txtID_BasicoAereo.Text)
                dgvTaxaAereoCompras.DataBind()
                dgvTaxaAereoVendas.DataBind()

                lblSuccess_CargaAereo1.Text = "Registro deletado!"
                divSuccess_CargaAereo1.Visible = True
                dgvCargaAereo.DataBind()
                dgvTaxaAereoCompras.DataBind()
                dgvTaxaAereoVendas.DataBind()

            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select ID_CARGA_BL,ID_TIPO_CARGA,ID_MERCADORIA,ID_NCM,(select CD_NCM +' - '+ NM_NCM from TB_NCM WHERE ID_NCM = A.ID_NCM)NCM,VL_PESO_BRUTO,VL_M3,(SELECT B.VL_PESO_TAXADO FROM TB_BL B WHERE ID_BL = A.ID_BL)VL_PESO_TAXADO,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,DS_MERCADORIA,QT_MERCADORIA,DS_GRUPO_NCM from TB_CARGA_BL A
WHERE ID_CARGA_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                dsNCM_CargaAereo.DataBind()

                btnAdicionarMedidasAereo.Visible = True
                divMedidasAereo.Attributes.CssStyle.Add("display", "block")

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CARGA_BL")) Then
                    txtID_CargaAereo.Text = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_MERCADORIA")) Then
                    txtQtdVolume_CargaAereo.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DS_GRUPO_NCM")) Then
                    txtGrupoNCM_CargaAereo.Text = ds.Tables(0).Rows(0).Item("DS_GRUPO_NCM")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                    ddlMercadoria_CargaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_EMBALAGEM")) Then
                    ddlEmbalagem_CargaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_EMBALAGEM").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NCM")) Then
                    If ds.Tables(0).Rows(0).Item("ID_NCM") > 0 Then
                        txtIDNCM_CargaAereo.Text = ds.Tables(0).Rows(0).Item("ID_NCM")
                        dsNCM_CargaAereo.DataBind()
                        ddlNCM_CargaAereo.DataBind()
                        ddlNCM_CargaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_NCM")
                    Else
                        txtIDNCM_CargaAereo.Text = 0
                        dsNCM_CargaAereo.DataBind()
                        ddlNCM_CargaAereo.DataBind()
                    End If
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                    txtPesoBruto_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_M3")) Then
                    txtPesoVolumetrico_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_M3")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
                    txtPesoTaxado_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMPRIMENTO")) Then
                    txtComprimento_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_COMPRIMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_LARGURA")) Then
                    txtLargura_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_LARGURA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_ALTURA")) Then
                    txtAltura_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_ALTURA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DS_MERCADORIA")) Then
                    txtDescMercadoria_CargaAereo.Text = ds.Tables(0).Rows(0).Item("DS_MERCADORIA")
                End If
                mpeCargaAereo.Show()

            End If

        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_BL,ID_MERCADORIA,ID_TIPO_CARGA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA,ID_TIPO_CNTR )  select ID_BL,ID_MERCADORIA,ID_TIPO_CARGA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA,ID_TIPO_CNTR from TB_CARGA_BL WHERE ID_CARGA_BL = " & ID)
            lblSuccess_CargaAereo1.Text = "Registro duplicado!"
            divSuccess_CargaAereo1.Visible = True
            dgvCargaAereo.DataBind()

            Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text)


            Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_BL_TAXA FROM TB_BL_TAXA A WHERE  ID_BASE_CALCULO_TAXA IS NOT NULL AND VL_TAXA IS NOT NULL AND VL_TAXA <> 0 AND ID_BASE_CALCULO_TAXA <> 1 AND ID_MOEDA <> 0 AND ISNULL(ID_BL_TAXA_MASTER,0) = 0 AND ISNULL(ID_BL_MASTER,0) = 0  AND ID_BL = " & txtID_BasicoAereo.Text & " And ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER= A.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL  AND ID_BL_TAXA IS NOT NULL)")
            If dsTaxa.Tables(0).Rows.Count > 0 Then
                For Each linha As DataRow In dsTaxa.Tables(0).Rows
                    Dim Calcula As New CalculaBL
                    Dim retorno As String = Calcula.Calcular(linha.Item("ID_BL_TAXA").ToString())
                Next

            End If
        End If

        Con.Fechar()
    End Sub

    Private Sub btnFechar_CargaAereo_Click(sender As Object, e As EventArgs) Handles btnFechar_CargaAereo.Click
        divErro_CargaAereo2.Visible = False
        divSuccess_CargaAereo2.Visible = False
        ddlMercadoria_CargaAereo.SelectedValue = 0
        ddlNCM_CargaAereo.SelectedValue = 0
        txtNCMFiltro_CargaAereo.Text = ""
        txtIDNCM_CargaAereo.Text = ""
        txtLargura_CargaAereo.Text = ""
        txtAltura_CargaAereo.Text = ""
        txtComprimento_CargaAereo.Text = ""
        txtDescMercadoria_CargaAereo.Text = ""
        txtPesoBruto_CargaAereo.Text = ""
        txtPesoVolumetrico_CargaAereo.Text = ""
        txtID_CargaAereo.Text = ""
        txtGrupoNCM_CargaAereo.Text = ""
        txtQtdVolume_CargaAereo.Text = ""
        'btnAdicionarMedidasAereo.Visible = False
        txtAlturaMercadoriaAereo.Text = ""
        txtLarguraMercadoriaAereo.Text = ""
        txtComprimentoMercadoriaAereo.Text = ""
        txtQtdCaixasAereo.Text = ""
        txtPesoTaxado_CargaAereo.Text = ""
        divMedidasAereo.Attributes.CssStyle.Add("display", "none")
        ddlEmbalagem_CargaAereo.SelectedValue = 0
        mpeCargaAereo.Hide()
    End Sub

    Private Sub btnFechar_CargaMaritimo_Click(sender As Object, e As EventArgs) Handles btnFechar_CargaMaritimo.Click
        divErro_CargaMaritimo2.Visible = False
        divSuccess_CargaMaritimo2.Visible = False
        ddlMercadoria_CargaMaritimo.SelectedValue = 0
        txtNCMFiltro_CargaMaritimo.Text = ""
        txtIDNCM_CargaMaritimo.Text = ""
        ddlNCM_CargaMaritimo.SelectedValue = 0
        txtNumeroLacre_CargaMaritimo.Text = ""
        txtValorTara_CargaMaritimo.Text = ""
        txtNumeroContainer_CargaMaritimo.Text = ""
        ddlTipoContainer_CargaMaritimo.SelectedValue = 0
        txtPesoBruto_CargaMaritimo.Text = ""
        txtPesoVolumetrico_CargaMaritimo.Text = ""
        txtID_CargaMaritimo.Text = ""
        txtGrupoNCM_CargaMaritimo.Text = ""
        ddlEmbalagem_CargaMaritimo.SelectedValue = 0
        ddlNumeroCNTR_CargaMaritimo.SelectedValue = 0
        txtQtdVolumes_CargaMaritimo.Text = ""
        txtDescMercadoriaCNTR_Maritimo.Text = ""

        dgvCargaMaritimo.DataBind()
        mpeCargaMaritimo.Hide()
    End Sub

    Private Sub btnFechar_TaxaAereo_Click(sender As Object, e As EventArgs) Handles btnFechar_TaxaAereo.Click
        txtValorVenda_TaxaAereo.Enabled = True
        txtMinVenda_TaxaAereo.Enabled = True
        ddlMoedaVenda_TaxaAereo.Enabled = True
        ddlDestinatarioCob_TaxaAereo.Enabled = True
        divErro_TaxaAereo2.Visible = False
        divSuccess_TaxaAereo2.Visible = False
        ddlDespesa_TaxaAereo.SelectedValue = 0
        ddlOrigemPagamento_TaxaAereo.SelectedValue = 0
        ddlBaseCalculo_TaxaAereo.SelectedValue = 0
        ddlMoedaCompra_TaxaAereo.SelectedValue = 0
        ddlMoedaVenda_TaxaAereo.SelectedValue = 0
        ddlEmpresa_TaxaAereo.SelectedValue = 0
        txtValorCompra_TaxaAereo.Text = ""
        txtQtdBaseCalculo_TaxaMaritimo.Text = ""
        txtValorVenda_TaxaAereo.Text = ""
        txtObs_TaxaAereo.Text = ""
        txtMinCompra_TaxaAereo.Text = ""
        txtMinVenda_TaxaAereo.Text = ""
        txtID_TaxaAereo.Text = ""
        Session("CD_PR") = 0
        divVendaAereo.Visible = True
        divCompraAereo.Visible = True
        btnSalvar_TaxaAereo.Visible = True
        lblTipoEmpresa_Aereo.Text = "Fornecedor:"
        ddlDestinatarioCob_TaxaAereo.Enabled = True
        mpeTaxaAereo.Hide()
    End Sub

    Private Sub btnFechar_TaxaMaritimo_Click(sender As Object, e As EventArgs) Handles btnFechar_TaxaMaritimo.Click
        txtValorVenda_TaxaMaritimo.Enabled = True
        txtMinVenda_TaxaMaritimo.Enabled = True
        ddlMoedaVenda_TaxaMaritimo.Enabled = True
        ddlDestinatarioCob_TaxaMaritimo.Enabled = True
        divErro_TaxaMaritimo2.Visible = False
        divSuccess_TaxaMaritimo2.Visible = False
        txtID_TaxaMaritimo.Text = ""
        txtQtdBaseCalculo_TaxaMaritimo.Text = ""
        ddlStatusPagamento_TaxaMaritimo.SelectedValue = 0
        ddlDespesa_TaxaMaritimo.SelectedValue = 0
        ddlOrigemPagamento_TaxaMaritimo.SelectedValue = 0
        ddlBaseCalculo_TaxaMaritimo.SelectedValue = 0
        ddlMoedaCompra_TaxaMaritimo.SelectedValue = 0
        ddlMoedaVenda_TaxaMaritimo.SelectedValue = 0
        ddlEmpresa_TaxaMaritimo.SelectedValue = 0
        txtValorCompra_TaxaMaritimo.Text = ""
        txtValorVenda_TaxaMaritimo.Text = ""
        txtObs_TaxaMaritimo.Text = ""
        txtMinCompra_TaxaMaritimo.Text = ""
        txtMinVenda_TaxaMaritimo.Text = ""
        Session("CD_PR") = 0
        divVendaMaritimo.Visible = True
        divCompraMaritimo.Visible = True
        btnSalvar_TaxaMaritimo.Visible = True
        lblTipoEmpresa_Maritimo.Text = "Fornecedor:"
        ddlDestinatarioCob_TaxaMaritimo.Enabled = True
        mpeTaxaMaritimo.Hide()

    End Sub

    Private Sub btnGravar_ObsAereo_Click(sender As Object, e As EventArgs) Handles btnGravar_ObsAereo.Click
        divSuccess_ObsAereo.Visible = False
        divErro_ObsAereo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtID_BasicoAereo.Text = "" Then
            lblErro_ObsAereo.Text = "Antes de inserir observações é necessário cadastrar as Informações Basicas"
            divErro_ObsAereo.Visible = True

        Else

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro_ObsAereo.Visible = True
                lblErro_ObsAereo.Text = "Usuário não possui permissão."

            Else

                Dim ObsAgente As String = ""
                If txtObsAgente_ObsAereo.Text = "" Then
                    ObsAgente = "NULL"
                Else
                    ObsAgente = txtObsAgente_ObsAereo.Text
                    ObsAgente = ObsAgente.Replace("'", "''")
                    ObsAgente = "'" & ObsAgente & "'"
                End If

                Dim ObsCliente As String = ""
                If txtObsCliente_ObsAereo.Text = "" Then
                    ObsCliente = "NULL"
                Else
                    ObsCliente = txtObsCliente_ObsAereo.Text
                    ObsCliente = ObsCliente.Replace("'", "''")
                    ObsCliente = "'" & ObsCliente & "'"
                End If

                Dim ObsOperacional As String = ""
                If txtObsOperacional_ObsAereo.Text = "" Then
                    ObsOperacional = "NULL"
                Else
                    ObsOperacional = txtObsOperacional_ObsAereo.Text
                    ObsOperacional = ObsOperacional.Replace("'", "''")
                    ObsOperacional = "'" & ObsOperacional & "'"
                End If

                Dim ObsComercial As String = ""
                If txtObsComercial_ObsAereo.Text = "" Then
                    ObsComercial = "NULL"
                Else
                    ObsComercial = txtObsComercial_ObsAereo.Text
                    ObsComercial = ObsComercial.Replace("'", "''")
                    ObsComercial = "'" & ObsComercial & "'"
                End If

                Con.ExecutarQuery("UPDATE TB_BL SET OB_CLIENTE = " & ObsCliente & ", OB_AGENTE_INTERNACIONAL = " & ObsAgente & " , OB_COMERCIAL = " & ObsComercial & ",OB_OPERACIONAL_INTERNA = " & ObsOperacional & " WHERE ID_BL = " & txtID_BasicoAereo.Text)
                divSuccess_ObsAereo.Visible = True

                'txtObsCliente_ObsAereo.Text = txtObsCliente_ObsAereo.Text.Replace("NULL", "")
                'txtObsCliente_ObsAereo.Text = txtObsCliente_ObsAereo.Text.Replace("'", "")

                'txtObsAgente_ObsAereo.Text = txtObsAgente_ObsAereo.Text.Replace("NULL", "")
                'txtObsAgente_ObsAereo.Text = txtObsAgente_ObsAereo.Text.Replace("'", "")

                'txtObsComercial_ObsAereo.Text = txtObsComercial_ObsAereo.Text.Replace("NULL", "")
                'txtObsComercial_ObsAereo.Text = txtObsComercial_ObsAereo.Text.Replace("'", "")

                'txtObsOperacional_ObsAereo.Text = txtObsOperacional_ObsAereo.Text.Replace("NULL", "")
                'txtObsOperacional_ObsAereo.Text = txtObsOperacional_ObsAereo.Text.Replace("'", "")

            End If


        End If
    End Sub

    Private Sub btnGravar_ObsMaritimo_Click(sender As Object, e As EventArgs) Handles btnGravar_ObsMaritimo.Click
        divSuccess_ObsMaritimo.Visible = False
        divErro_ObsMaritimo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtID_BasicoMaritimo.Text = "" Then
            lblErro_ObsMaritimo.Text = "Antes de inserir observações é necessário cadastrar as Informações Basicas"
            divErro_ObsMaritimo.Visible = True

        Else

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro_ObsMaritimo.Visible = True
                lblErro_ObsMaritimo.Text = "Usuário não possui permissão."

            Else
                Dim ObsAgente As String = ""
                If txtObsAgente_ObsMaritimo.Text = "" Then
                    ObsAgente = "NULL"
                Else
                    ObsAgente = txtObsAgente_ObsMaritimo.Text
                    ObsAgente = ObsAgente.Replace("'", "''")
                    ObsAgente = "'" & ObsAgente & "'"
                End If

                Dim ObsCliente As String = ""
                If txtObsCliente_ObsMaritimo.Text = "" Then
                    ObsCliente = "NULL"
                Else
                    ObsCliente = txtObsCliente_ObsMaritimo.Text
                    ObsCliente = ObsCliente.Replace("'", "''")
                    ObsCliente = "'" & ObsCliente & "'"
                End If

                Dim ObsOperacional As String = ""
                If txtObsoperacional_ObsMaritimo.Text = "" Then
                    ObsOperacional = "NULL"
                Else
                    ObsOperacional = txtObsoperacional_ObsMaritimo.Text
                    ObsOperacional = ObsOperacional.Replace("'", "''")
                    ObsOperacional = "'" & ObsOperacional & "'"
                End If

                Dim ObsComercial As String = ""
                If txtObsComercial_ObsMaritimo.Text = "" Then
                    ObsComercial = "NULL"
                Else
                    ObsComercial = txtObsComercial_ObsMaritimo.Text
                    ObsComercial = ObsComercial.Replace("'", "''")
                    ObsComercial = "'" & ObsComercial & "'"
                End If

                Con.ExecutarQuery("UPDATE TB_BL SET OB_CLIENTE = " & ObsCliente & ", OB_AGENTE_INTERNACIONAL = " & ObsAgente & " , OB_COMERCIAL = " & ObsComercial & ",OB_OPERACIONAL_INTERNA = " & ObsOperacional & " WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
                divSuccess_ObsMaritimo.Visible = True

                'txtObsCliente_ObsMaritimo.Text = txtObsCliente_ObsMaritimo.Text.Replace("NULL", "")
                'txtObsCliente_ObsMaritimo.Text = txtObsCliente_ObsMaritimo.Text.Replace("'", "")

                'txtObsAgente_ObsMaritimo.Text = txtObsAgente_ObsMaritimo.Text.Replace("NULL", "")
                'txtObsAgente_ObsMaritimo.Text = txtObsAgente_ObsMaritimo.Text.Replace("'", "")

                'txtObsComercial_ObsMaritimo.Text = txtObsComercial_ObsMaritimo.Text.Replace("NULL", "")
                'txtObsComercial_ObsMaritimo.Text = txtObsComercial_ObsMaritimo.Text.Replace("'", "")

                'txtObsoperacional_ObsMaritimo.Text = txtObsoperacional_ObsMaritimo.Text.Replace("NULL", "")
                'txtObsoperacional_ObsMaritimo.Text = txtObsoperacional_ObsMaritimo.Text.Replace("'", "")
            End If

        End If
    End Sub

    Private Sub btnLimpar_ObsAereo_Click(sender As Object, e As EventArgs) Handles btnLimpar_ObsAereo.Click
        txtObsCliente_ObsAereo.Text = ""
        txtObsAgente_ObsAereo.Text = ""
        txtObsComercial_ObsAereo.Text = ""
        txtObsOperacional_ObsAereo.Text = ""
    End Sub

    Private Sub btnLimpar_ObsMaritimo_Click(sender As Object, e As EventArgs) Handles btnLimpar_ObsMaritimo.Click
        txtObsCliente_ObsMaritimo.Text = ""
        txtObsAgente_ObsMaritimo.Text = ""
        txtObsComercial_ObsMaritimo.Text = ""
        txtObsoperacional_ObsMaritimo.Text = ""
    End Sub

    Private Sub btnLimpar_BasicoAereo_Click(sender As Object, e As EventArgs) Handles btnLimpar_BasicoAereo.Click
        If Request.QueryString("tipo") = "e" Then
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=e")
        ElseIf Request.QueryString("tipo") = "h" Then
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=h")
        End If
    End Sub

    Private Sub btnLimpar_BasicoMaritimo_Click(sender As Object, e As EventArgs) Handles btnLimpar_BasicoMaritimo.Click
        If Request.QueryString("tipo") = "e" Then
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=e")
        ElseIf Request.QueryString("tipo") = "h" Then
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=h")
        End If
    End Sub

    Private Sub btnGravar_RefAereo_Click(sender As Object, e As EventArgs) Handles btnGravar_RefAereo.Click
        divSuccess_RefAereo.Visible = False
        divErro_RefAereo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtRefAereo.Text = "" Then
            lblErro_RefAereo.Text = "Preencha o campo de referencia!"
            divErro_RefAereo.Visible = True

        ElseIf txtID_BasicoAereo.Text = "" Then
            lblErro_RefAereo.Text = "Antes de inserir referencia é necessário cadastrar as Informações Basicas"
            divErro_RefAereo.Visible = True

        Else

            If txtID_RefAereo.Text = "" Then
                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_RefAereo.Visible = True
                    lblErro_RefAereo.Text = "Usuário não possui permissão."

                Else

                    ds = Con.ExecutarQuery("SELECT ISNULL(ID_COTACAO,0)ID_COTACAO FROM TB_BL WHERE ID_BL =" & txtID_BasicoAereo.Text)
                    If ds.Tables(0).Rows(0).Item("ID_COTACAO") = 0 Then
                        Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_BL,NR_REFERENCIA_CLIENTE,TIPO) VALUES (" & txtID_BasicoAereo.Text & ", '" & txtRefAereo.Text & "', '" & ddlTipoRefAereo.SelectedValue & "')")
                    Else

                        Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_BL,NR_REFERENCIA_CLIENTE,TIPO,ID_COTACAO) VALUES (" & txtID_BasicoAereo.Text & ", '" & txtRefAereo.Text & "', '" & ddlTipoRefAereo.SelectedValue & "'," & ds.Tables(0).Rows(0).Item("ID_COTACAO") & ")")
                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET ID_COTACAO =" & ds.Tables(0).Rows(0).Item("ID_COTACAO") & " WHERE ID_BL = " & txtID_BasicoAereo.Text)
                    End If

                    divSuccess_RefAereo.Visible = True
                    lblSuccess_RefAereo.Text = "Registro cadastrado/atualizado com sucesso!"

                    txtID_RefAereo.Text = ""
                    txtRefAereo.Text = ""
                    ddlTipoRefAereo.SelectedValue = 0
                    dgvRefAereo.DataBind()
                End If
            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_RefAereo.Visible = True
                    lblErro_RefAereo.Text = "Usuário não possui permissão."

                Else

                    ds = Con.ExecutarQuery("SELECT ISNULL(ID_COTACAO,0)ID_COTACAO FROM TB_BL WHERE ID_BL =" & txtID_BasicoAereo.Text)
                    If ds.Tables(0).Rows(0).Item("ID_COTACAO") = 0 Then
                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = '" & txtRefAereo.Text & "', TIPO = '" & ddlTipoRefAereo.SelectedValue & "' WHERE ID_REFERENCIA_CLIENTE = " & txtID_RefAereo.Text)
                    Else

                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = '" & txtRefAereo.Text & "', TIPO = '" & ddlTipoRefAereo.SelectedValue & "', ID_COTACAO = " & ds.Tables(0).Rows(0).Item("ID_COTACAO") & " WHERE ID_REFERENCIA_CLIENTE = " & txtID_RefAereo.Text)
                    End If
                    lblSuccess_RefAereo.Text = "Registro cadastrado/atualizado com sucesso!"

                    divSuccess_RefAereo.Visible = True
                    txtID_RefAereo.Text = ""
                    txtRefAereo.Text = ""
                    ddlTipoRefAereo.SelectedValue = 0
                    dgvRefAereo.DataBind()

                End If
            End If

        End If

    End Sub

    Private Sub btnGravar_RefMaritimo_Click(sender As Object, e As EventArgs) Handles btnGravar_RefMaritimo.Click
        divSuccess_RefMaritimo.Visible = False
        divErro_RefMaritimo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtRefMaritimo.Text = "" Then
            lblErro_RefMaritimo.Text = "Preencha o campo de referencia!"
            divErro_RefMaritimo.Visible = True

        ElseIf txtID_BasicoMaritimo.Text = "" Then
            lblErro_RefMaritimo.Text = "Antes de inserir referencia é necessário cadastrar as Informações Basicas"
            divErro_RefMaritimo.Visible = True

        Else

            If txtID_RefMaritimo.Text = "" Then
                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_RefMaritimo.Visible = True
                    lblErro_RefMaritimo.Text = "Usuário não possui permissão."

                Else
                    ds = Con.ExecutarQuery("SELECT ISNULL(ID_COTACAO,0)ID_COTACAO FROM TB_BL WHERE ID_BL =" & txtID_BasicoMaritimo.Text)
                    If ds.Tables(0).Rows(0).Item("ID_COTACAO") = 0 Then
                        Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_BL,NR_REFERENCIA_CLIENTE,TIPO) VALUES (" & txtID_BasicoMaritimo.Text & ", '" & txtRefMaritimo.Text & "', '" & ddlTipoRefMaritimo.SelectedValue & "')")
                    Else

                        Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_BL,NR_REFERENCIA_CLIENTE,TIPO,ID_COTACAO) VALUES (" & txtID_BasicoMaritimo.Text & ", '" & txtRefMaritimo.Text & "', '" & ddlTipoRefMaritimo.SelectedValue & "'," & ds.Tables(0).Rows(0).Item("ID_COTACAO") & ")")
                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET ID_COTACAO =" & ds.Tables(0).Rows(0).Item("ID_COTACAO") & " WHERE ID_BL = " & txtID_BasicoMaritimo.Text)

                    End If

                    divSuccess_RefMaritimo.Visible = True
                    ddlTipoRefMaritimo.SelectedValue = 0
                    txtRefMaritimo.Text = ""
                    txtID_RefMaritimo.Text = ""
                    lblSuccess_RefMaritimo.Text = "Registro cadastrado/atualizado com sucesso!"
                    dgvRefMaritimo.DataBind()
                End If
            Else
                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_RefMaritimo.Visible = True
                    lblErro_RefMaritimo.Text = "Usuário não possui permissão."

                Else

                    ds = Con.ExecutarQuery("SELECT ISNULL(ID_COTACAO,0)ID_COTACAO FROM TB_BL WHERE ID_BL =" & txtID_BasicoMaritimo.Text)
                    If ds.Tables(0).Rows(0).Item("ID_COTACAO") = 0 Then
                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = '" & txtRefMaritimo.Text & "', TIPO = '" & ddlTipoRefMaritimo.SelectedValue & "' WHERE ID_REFERENCIA_CLIENTE = " & txtID_RefMaritimo.Text)
                    Else

                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = '" & txtRefMaritimo.Text & "', TIPO = '" & ddlTipoRefMaritimo.SelectedValue & "', ID_COTACAO = " & ds.Tables(0).Rows(0).Item("ID_COTACAO") & " WHERE ID_REFERENCIA_CLIENTE = " & txtID_RefMaritimo.Text)

                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET ID_COTACAO =" & ds.Tables(0).Rows(0).Item("ID_COTACAO") & " WHERE ID_BL = " & txtID_BasicoMaritimo.Text)

                    End If

                    divSuccess_RefMaritimo.Visible = True
                    lblSuccess_RefMaritimo.Text = "Registro cadastrado/atualizado com sucesso!"

                    txtID_RefMaritimo.Text = ""
                    txtRefMaritimo.Text = ""
                    ddlTipoRefMaritimo.SelectedValue = 0
                    dgvRefMaritimo.DataBind()
                End If
            End If

        End If
    End Sub

    Private Sub btnCancelar_RefMaritimo_Click(sender As Object, e As EventArgs) Handles btnCancelar_RefMaritimo.Click
        divSuccess_RefMaritimo.Visible = False
        divErro_RefMaritimo.Visible = False
        txtRefMaritimo.Text = ""
        txtID_RefMaritimo.Text = ""
        ddlTipoRefMaritimo.SelectedValue = 0

    End Sub

    Private Sub btnCancelar_RefAereo_Click(sender As Object, e As EventArgs) Handles btnCancelar_RefAereo.Click
        divSuccess_RefAereo.Visible = False
        divErro_RefAereo.Visible = False
        txtRefAereo.Text = ""
        txtID_RefAereo.Text = ""
        ddlTipoRefAereo.SelectedValue = 0

    End Sub

    Sub LimpaNulo()
        ' txtResumoMercadoria_BasicoMaritimo.Text = txtResumoMercadoria_BasicoMaritimo.Text.Replace("'", "")
        ' txtResumoMercadoria_BasicoMaritimo.Text = txtResumoMercadoria_BasicoMaritimo.Text.Replace("NULL", "")

        txtRefComercial_BasicoMaritimo.Text = txtRefComercial_BasicoMaritimo.Text.Replace("'", "")
        txtRefComercial_BasicoMaritimo.Text = txtRefComercial_BasicoMaritimo.Text.Replace("NULL", "")

        txtRefAuxiliar_BasicoMaritimo.Text = txtRefAuxiliar_BasicoMaritimo.Text.Replace("'", "")
        txtRefAuxiliar_BasicoMaritimo.Text = txtRefAuxiliar_BasicoMaritimo.Text.Replace("NULL", "")

        txtProcesso_BasicoMaritimo.Text = txtProcesso_BasicoMaritimo.Text.Replace("'", "")
        txtProcesso_BasicoMaritimo.Text = txtProcesso_BasicoMaritimo.Text.Replace("NULL", "")

        txtHBL_BasicoMaritimo.Text = txtHBL_BasicoMaritimo.Text.Replace("'", "")
        txtHBL_BasicoMaritimo.Text = txtHBL_BasicoMaritimo.Text.Replace("NULL", "")

        txtCE_BasicoMaritimo.Text = txtCE_BasicoMaritimo.Text.Replace("'", "")
        txtCE_BasicoMaritimo.Text = txtCE_BasicoMaritimo.Text.Replace("NULL", "")

        txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("NULL", "")
        txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
        txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("',103)", "")

        ' txtResumoMercadoria_BasicoAereo.Text = txtResumoMercadoria_BasicoAereo.Text.Replace("'", "")
        ' txtResumoMercadoria_BasicoAereo.Text = txtResumoMercadoria_BasicoAereo.Text.Replace("NULL", "")

        txtRefComercial_BasicoAereo.Text = txtRefComercial_BasicoAereo.Text.Replace("'", "")
        txtRefComercial_BasicoAereo.Text = txtRefComercial_BasicoAereo.Text.Replace("NULL", "")

        txtRefAuxiliar_BasicoAereo.Text = txtRefAuxiliar_BasicoAereo.Text.Replace("'", "")
        txtRefAuxiliar_BasicoAereo.Text = txtRefAuxiliar_BasicoAereo.Text.Replace("NULL", "")

        txtProcesso_BasicoAereo.Text = txtProcesso_BasicoAereo.Text.Replace("'", "")
        txtProcesso_BasicoAereo.Text = txtProcesso_BasicoAereo.Text.Replace("NULL", "")

        txtHBL_BasicoAereo.Text = txtHBL_BasicoAereo.Text.Replace("'", "")
        txtHBL_BasicoAereo.Text = txtHBL_BasicoAereo.Text.Replace("NULL", "")

        txtDataCE_BasicoAereo.Text = txtDataCE_BasicoAereo.Text.Replace("NULL", "")
        txtDataCE_BasicoAereo.Text = txtDataCE_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
        txtDataCE_BasicoAereo.Text = txtDataCE_BasicoAereo.Text.Replace("',103)", "")

        txtNumeroCE_BasicoAereo.Text = txtNumeroCE_BasicoAereo.Text.Replace("'", "")
        txtNumeroCE_BasicoAereo.Text = txtNumeroCE_BasicoAereo.Text.Replace("NULL", "")
    End Sub

    Private Sub btnGravar_BasicoMaritimo_Click(sender As Object, e As EventArgs) Handles btnGravar_BasicoMaritimo.Click

        divSuccess_BasicoMaritimo.Visible = False
        divErro_BasicoMaritimo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        If ddlServico_BasicoMaritimo.SelectedValue = 0 Then
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "É necesário informar o tipo de serviço."

        Else
            txtHBL_BasicoMaritimo.Text = txtHBL_BasicoMaritimo.Text.Replace(" ", "")


            If txtValorDivisaoProfit_BasicoMaritimo.Text = "" Then
                txtValorDivisaoProfit_BasicoMaritimo.Text = 0
            End If

            txtValorDivisaoProfit_BasicoMaritimo.Text = txtValorDivisaoProfit_BasicoMaritimo.Text.Replace(".", "")
            txtValorDivisaoProfit_BasicoMaritimo.Text = txtValorDivisaoProfit_BasicoMaritimo.Text.Replace(",", ".")


            If txtValorCarga_BasicoMaritimo.Text = "" Then
                txtValorCarga_BasicoMaritimo.Text = 0
            End If

            txtValorCarga_BasicoMaritimo.Text = txtValorCarga_BasicoMaritimo.Text.Replace(".", "")
            txtValorCarga_BasicoMaritimo.Text = txtValorCarga_BasicoMaritimo.Text.Replace(",", ".")


            'If txtResumoMercadoria_BasicoMaritimo.Text = "" Then
            '    txtResumoMercadoria_BasicoMaritimo.Text = "NULL"
            'Else
            '    txtResumoMercadoria_BasicoMaritimo.Text = "'" & txtResumoMercadoria_BasicoMaritimo.Text & "'"
            'End If

            Dim ResumoMercadoria As String = txtResumoMercadoria_BasicoMaritimo.Text
            If ResumoMercadoria = "" Then
                ResumoMercadoria = "NULL"
            Else
                ResumoMercadoria = ResumoMercadoria.Replace("'", "''")
                ResumoMercadoria = "'" & ResumoMercadoria & "'"
            End If

            If txtRefAuxiliar_BasicoMaritimo.Text = "" Then
                txtRefAuxiliar_BasicoMaritimo.Text = "NULL"
            Else
                txtRefAuxiliar_BasicoMaritimo.Text = "'" & txtRefAuxiliar_BasicoMaritimo.Text & "'"
            End If

            If txtRefComercial_BasicoMaritimo.Text = "" Then
                txtRefComercial_BasicoMaritimo.Text = "NULL"
            Else
                txtRefComercial_BasicoMaritimo.Text = "'" & txtRefComercial_BasicoMaritimo.Text & "'"
            End If

            If txtProcesso_BasicoMaritimo.Text = "" Then
                txtProcesso_BasicoMaritimo.Text = "NULL"
            Else
                txtProcesso_BasicoMaritimo.Text = "'" & txtProcesso_BasicoMaritimo.Text & "'"
            End If

            If txtHBL_BasicoMaritimo.Text = "" Then
                txtHBL_BasicoMaritimo.Text = "NULL"
            Else
                txtHBL_BasicoMaritimo.Text = "'" & txtHBL_BasicoMaritimo.Text & "'"
            End If

            Dim EmailCotacao As String = ""

            If txtEmailCotacao_BasicoMaritimo.Text = "" Then
                EmailCotacao = "NULL"
            Else
                EmailCotacao = txtEmailCotacao_BasicoMaritimo.Text
                EmailCotacao = EmailCotacao.Replace("'", "''")
                EmailCotacao = "'" & EmailCotacao & "'"
            End If


            Dim ContratoArmador As String = ""

            If txtContratoArmador_BasicoMaritimo.Text = "" Then
                ContratoArmador = "NULL"
            Else
                ContratoArmador = txtContratoArmador_BasicoMaritimo.Text
                ContratoArmador = ContratoArmador.Replace("'", "''")
                ContratoArmador = "'" & ContratoArmador & "'"
            End If


            If txtID_BasicoMaritimo.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else


                    If Request.QueryString("tipo") = "e" And ddlServico_BasicoMaritimo.SelectedValue = 0 Then
                        divErro_BasicoMaritimo.Visible = True
                        lblErro_BasicoMaritimo.Text = "É necessário preencher o tipo de serviço."
                        LimpaNulo()
                        Exit Sub
                    Else

                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL)QTD FROM TB_BL WHERE FL_CANCELADO = 0 AND NR_BL = " & txtHBL_BasicoMaritimo.Text & "")
                        If ds1.Tables(0).Rows(0).Item("QTD") = 0 Then

                            If txtCE_BasicoMaritimo.Text = "" Then
                                txtCE_BasicoMaritimo.Text = "NULL"
                                txtDataCE_BasicoMaritimo.Text = "NULL"

                            Else
                                txtCE_BasicoMaritimo.Text = "'" & txtCE_BasicoMaritimo.Text & "'"
                                txtDataCE_BasicoMaritimo.Text = " getdate() "
                            End If

                            'INSERE 
                            ds = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,NR_BL,ID_PARCEIRO_TRANSPORTADOR,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_EXPORTADOR, ID_PARCEIRO_COMISSARIA,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,FL_FREE_HAND,ID_TIPO_ESTUFAGEM,ID_TIPO_PAGAMENTO,ID_TIPO_CARGA,NR_CE,DT_CE,OB_REFERENCIA_AUXILIAR,OB_REFERENCIA_COMERCIAL,NM_RESUMO_MERCADORIA, ID_SERVICO,GRAU,ID_PARCEIRO_IMPORTADOR,DT_ABERTURA,ID_PARCEIRO_INDICADOR,ID_PROFIT_DIVISAO,VL_PROFIT_DIVISAO,VL_CARGA,ID_PARCEIRO_RODOVIARIO,FINAL_DESTINATION,FL_TRAKING_AUTOMATICO,FL_EMAIL_COTACAO, EMAIL_COTACAO, NR_CONTRATO_ARMADOR , ID_USUARIO_ABERTURA) VALUES (" & txtProcesso_BasicoMaritimo.Text & "," & txtHBL_BasicoMaritimo.Text & "," & ddlTransportador_BasicoMaritimo.SelectedValue & ", " & ddlOrigem_BasicoMaritimo.SelectedValue & ", " & ddlDestino_BasicoMaritimo.SelectedValue & "," & ddlCliente_BasicoMaritimo.SelectedValue & ", " & ddlExportador_BasicoMaritimo.SelectedValue & "," & ddlComissaria_BasicoMaritimo.SelectedValue & "," & ddlAgente_BasicoMaritimo.SelectedValue & "," & ddlIncoterm_BasicoMaritimo.SelectedValue & ",'" & ckbFreeHand_BasicoMaritimo.Checked & "'," & ddlEstufagem_BasicoMaritimo.SelectedValue & "," & ddlTipoPagamento_BasicoMaritimo.SelectedValue & "," & ddlTipoCarga_BasicoMaritimo.SelectedValue & "," & txtCE_BasicoMaritimo.Text & "," & txtDataCE_BasicoMaritimo.Text & "," & txtRefAuxiliar_BasicoMaritimo.Text & "," & txtRefComercial_BasicoMaritimo.Text & ", " & ResumoMercadoria & ", " & ddlServico_BasicoMaritimo.SelectedValue & ", 'C'," & ddlImportador_BasicoMaritimo.SelectedValue & ",GETDATE()," & ddlIndicador_BasicoMaritimo.SelectedValue & "," & ddlDivisaoProfit_BasicoMaritimo.SelectedValue & ", " & txtValorDivisaoProfit_BasicoMaritimo.Text & "," & txtValorCarga_BasicoMaritimo.Text & "," & ddlTranspRodoviario_BasicoMaritimo.SelectedValue & "," & ddlFinalDestination_BasicoMaritimo.SelectedValue & ",'" & ckTrakingAutomaticoMaritimo.Checked & "','" & ckbEmailCotacao_BasicoMaritimo.Checked & "'," & EmailCotacao & ", " & ContratoArmador & " , " & Session("ID_USUARIO") & " ) Select SCOPE_IDENTITY() as ID_BL ")

                            'PREENCHE SESSÃO E CAMPO DE ID
                            Session("ID_BL") = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                            txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()

                            NumeroProcesso()

                            Dim Calcula As New CalculaBL
                            txtProfitCalculado_BasicoMaritimo.Text = Calcula.CalculoProfit(txtID_BasicoMaritimo.Text)
                            If ddlDivisaoProfit_BasicoMaritimo.SelectedValue = 11 Or ddlDivisaoProfit_BasicoMaritimo.SelectedValue = 0 Then
                                txtValorDivisaoProfit_BasicoMaritimo.Text = txtProfitCalculado_BasicoMaritimo.Text
                            End If
                            dgvTaxaMaritimoCompras.DataBind()
                            dgvTaxaMaritimoVendas.DataBind()

                            LimpaNulo()


                            DocConferido(txtID_BasicoMaritimo.Text, "M")

                            LimpaNulo()
                            Con.Fechar()
                            divSuccess_BasicoMaritimo.Visible = True

                        Else
                            divErro_BasicoMaritimo.Visible = True
                            lblErro_BasicoMaritimo.Text = "Já existe BL cadastrada com este número."
                            Exit Sub
                        End If

                    End If

                End If



            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else


                    'REALIZA UPDATE 
                    Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = " & txtProcesso_BasicoMaritimo.Text & " , NR_BL = " & txtHBL_BasicoMaritimo.Text & ", ID_PARCEIRO_TRANSPORTADOR = " & ddlTransportador_BasicoMaritimo.SelectedValue & ", ID_PORTO_ORIGEM = " & ddlOrigem_BasicoMaritimo.SelectedValue & ", ID_PORTO_DESTINO = " & ddlDestino_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_CLIENTE = " & ddlCliente_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_EXPORTADOR = " & ddlExportador_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_COMISSARIA = " & ddlComissaria_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgente_BasicoMaritimo.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm_BasicoMaritimo.SelectedValue & ", ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoMaritimo.SelectedValue & ", ID_TIPO_CARGA = " & ddlTipoCarga_BasicoMaritimo.SelectedValue & ", OB_REFERENCIA_AUXILIAR =" & txtRefAuxiliar_BasicoMaritimo.Text & ", OB_REFERENCIA_COMERCIAL = " & txtRefComercial_BasicoMaritimo.Text & ", NM_RESUMO_MERCADORIA = " & ResumoMercadoria & ",FL_FREE_HAND = '" & ckbFreeHand_BasicoMaritimo.Checked & "', ID_TIPO_ESTUFAGEM = " & ddlEstufagem_BasicoMaritimo.SelectedValue & ",ID_SERVICO =" & ddlServico_BasicoMaritimo.SelectedValue & ", GRAU = 'C', ID_PARCEIRO_IMPORTADOR = " & ddlImportador_BasicoMaritimo.SelectedValue & ",ID_PARCEIRO_INDICADOR = " & ddlIndicador_BasicoMaritimo.SelectedValue & ",ID_PROFIT_DIVISAO = " & ddlDivisaoProfit_BasicoMaritimo.SelectedValue & ", VL_PROFIT_DIVISAO = " & txtValorDivisaoProfit_BasicoMaritimo.Text & " , VL_CARGA = " & txtValorCarga_BasicoMaritimo.Text & ", ID_PARCEIRO_RODOVIARIO = " & ddlTranspRodoviario_BasicoMaritimo.SelectedValue & " ,FINAL_DESTINATION = " & ddlFinalDestination_BasicoMaritimo.SelectedValue & ", FL_TRAKING_AUTOMATICO = '" & ckTrakingAutomaticoMaritimo.Checked & "', FL_EMAIL_COTACAO = '" & ckbEmailCotacao_BasicoMaritimo.Checked & "', EMAIL_COTACAO = " & EmailCotacao & " , NR_CONTRATO_ARMADOR = " & ContratoArmador & " , ID_USUARIO_ULTIMA_ALTERACAO = " & Session("ID_USUARIO") & "  WHERE ID_BL = " & txtID_BasicoMaritimo.Text)


                    If txtCE_BasicoMaritimo.Text <> "" Then
                        ds = Con.ExecutarQuery("SELECT ISNULL(NR_CE,'')NR_CE, DT_CE FROM TB_BL WHERE ID_BL = " & txtID_BasicoMaritimo.Text & "")
                        If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) And txtCE_BasicoMaritimo.Text <> "" Then
                            Con.ExecutarQuery("UPDATE TB_BL SET DT_CE = GETDATE(), NR_CE = '" & txtCE_BasicoMaritimo.Text & "' WHERE DT_CE IS NULL AND ID_BL = " & txtID_BasicoMaritimo.Text & "")
                        ElseIf ds.Tables(0).Rows(0).Item("NR_CE").ToString <> txtCE_BasicoMaritimo.Text Then
                            Con.ExecutarQuery("UPDATE TB_BL SET DT_CE = GETDATE(), NR_CE = '" & txtCE_BasicoMaritimo.Text & "' WHERE ID_BL = " & txtID_BasicoMaritimo.Text & "")
                        End If
                    End If

                    ds = Con.ExecutarQuery("SELECT YEAR(DT_ABERTURA)ANO_ABERTURA,ID_BL_MASTER,ISNULL(NR_BL,0)NR_BL FROM [TB_BL] WHERE ID_BL = " & txtID_BasicoMaritimo.Text & "")
                    If ds.Tables(0).Rows(0).Item("ANO_ABERTURA") >= 2022 And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) And ds.Tables(0).Rows(0).Item("NR_BL") <> "0" Then
                        Dim Rastreio As New RastreioService
                        Rastreio.trackingbl(txtID_BasicoMaritimo.Text)
                    End If

                    Dim Calcula As New CalculaBL
                    txtProfitCalculado_BasicoMaritimo.Text = Calcula.CalculoProfit(txtID_BasicoMaritimo.Text)
                    If ddlDivisaoProfit_BasicoMaritimo.SelectedValue = 11 Or ddlDivisaoProfit_BasicoMaritimo.SelectedValue = 0 Then
                        txtValorDivisaoProfit_BasicoMaritimo.Text = txtProfitCalculado_BasicoMaritimo.Text
                    End If
                    dgvTaxaMaritimoCompras.DataBind()
                    dgvTaxaMaritimoVendas.DataBind()

                    LimpaNulo()

                    DocConferido(txtID_BasicoMaritimo.Text, "M")

                    LimpaNulo()

                    divSuccess_BasicoMaritimo.Visible = True
                    Con.Fechar()


                End If

            End If


        End If
        txtValorDivisaoProfit_BasicoMaritimo.Text = txtValorDivisaoProfit_BasicoMaritimo.Text.Replace(".", ",")
        txtValorCarga_BasicoMaritimo.Text = txtValorCarga_BasicoMaritimo.Text.Replace(".", ",")
    End Sub


    Private Sub btnGravar_BasicoAereo_Click(sender As Object, e As EventArgs) Handles btnGravar_BasicoAereo.Click

        divSuccess_BasicoAereo.Visible = False
        divErro_BasicoAereo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        If ddlServico_BasicoAereo.SelectedValue = 0 Then
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "É necesário informar o tipo de serviço."
        Else

            txtHBL_BasicoAereo.Text = txtHBL_BasicoAereo.Text.Replace(" ", "")


            If txtValorDivisaoProfit_BasicoAereo.Text = "" Then
                txtValorDivisaoProfit_BasicoAereo.Text = 0
            End If

            txtValorDivisaoProfit_BasicoAereo.Text = txtValorDivisaoProfit_BasicoAereo.Text.Replace(".", "")
            txtValorDivisaoProfit_BasicoAereo.Text = txtValorDivisaoProfit_BasicoAereo.Text.Replace(",", ".")

            If txtValorCarga_BasicoAereo.Text = "" Then
                txtValorCarga_BasicoAereo.Text = 0
            End If

            txtValorCarga_BasicoAereo.Text = txtValorCarga_BasicoAereo.Text.Replace(".", "")
            txtValorCarga_BasicoAereo.Text = txtValorCarga_BasicoAereo.Text.Replace(",", ".")


            'If txtResumoMercadoria_BasicoAereo.Text = "" Then
            '    txtResumoMercadoria_BasicoAereo.Text = "NULL"
            'Else
            '    txtResumoMercadoria_BasicoAereo.Text = "'" & txtResumoMercadoria_BasicoAereo.Text & "'"
            'End If

            Dim ResumoMercadoria As String = txtResumoMercadoria_BasicoAereo.Text
            If ResumoMercadoria = "" Then
                ResumoMercadoria = "NULL"
            Else
                ResumoMercadoria = ResumoMercadoria.Replace("'", "''")
                ResumoMercadoria = "'" & ResumoMercadoria & "'"
            End If

            If txtProcesso_BasicoAereo.Text = "" Then
                txtProcesso_BasicoAereo.Text = "NULL"
            Else
                txtProcesso_BasicoAereo.Text = "'" & txtProcesso_BasicoAereo.Text & "'"
            End If

            If txtRefAuxiliar_BasicoAereo.Text = "" Then
                txtRefAuxiliar_BasicoAereo.Text = "NULL"
            Else
                txtRefAuxiliar_BasicoAereo.Text = "'" & txtRefAuxiliar_BasicoAereo.Text & "'"
            End If

            If txtRefComercial_BasicoAereo.Text = "" Then
                txtRefComercial_BasicoAereo.Text = "NULL"
            Else
                txtRefComercial_BasicoAereo.Text = "'" & txtRefComercial_BasicoAereo.Text & "'"
            End If

            If txtHBL_BasicoAereo.Text = "" Then
                txtHBL_BasicoAereo.Text = "NULL"
            Else
                txtHBL_BasicoAereo.Text = "'" & txtHBL_BasicoAereo.Text & "'"
            End If

            Dim EmailCotacao As String = ""

            If txtEmailCotacao_BasicoAereo.Text = "" Then
                EmailCotacao = "NULL"
            Else
                EmailCotacao = txtEmailCotacao_BasicoAereo.Text
                EmailCotacao = EmailCotacao.Replace("'", "''")
                EmailCotacao = "'" & EmailCotacao & "'"
            End If

            If txtID_BasicoAereo.Text = "" Then


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else

                    If Request.QueryString("tipo") = "e" And ddlServico_BasicoAereo.SelectedValue = 0 Then
                        divErro_BasicoAereo.Visible = True
                        lblErro_BasicoAereo.Text = "É necessário preencher o tipo de serviço."
                        LimpaNulo()

                        Exit Sub
                    Else
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL)QTD FROM TB_BL WHERE NR_BL = " & txtHBL_BasicoAereo.Text & "")
                        If ds1.Tables(0).Rows(0).Item("QTD") = 0 Then

                            If txtNumeroCE_BasicoAereo.Text = "" Then
                                txtNumeroCE_BasicoAereo.Text = "NULL"
                                txtDataCE_BasicoAereo.Text = "NULL"

                            Else
                                txtNumeroCE_BasicoAereo.Text = "'" & txtNumeroCE_BasicoAereo.Text & "'"
                                txtDataCE_BasicoAereo.Text = " getdate() "
                            End If

                            'INSERE 
                            ds = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,NR_BL, ID_PARCEIRO_TRANSPORTADOR, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_PARCEIRO_CLIENTE, ID_PARCEIRO_EXPORTADOR, ID_PARCEIRO_COMISSARIA, ID_PARCEIRO_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_PARCEIRO_ARMAZEM_DESEMBARACO, ID_TIPO_PAGAMENTO, NR_CE, DT_CE, OB_REFERENCIA_AUXILIAR, OB_REFERENCIA_COMERCIAL, NM_RESUMO_MERCADORIA,ID_SERVICO,GRAU,ID_PARCEIRO_IMPORTADOR,DT_ABERTURA,FL_FREE_HAND,ID_PARCEIRO_INDICADOR,ID_PROFIT_DIVISAO,VL_PROFIT_DIVISAO,VL_CARGA,ID_PARCEIRO_RODOVIARIO,FL_TRAKING_AUTOMATICO,FL_EMAIL_COTACAO, EMAIL_COTACAO,FL_TC4,FL_TC6,ID_TIPO_AERONAVE, ID_USUARIO_ABERTURA) VALUES (" & txtProcesso_BasicoAereo.Text & ", " & txtHBL_BasicoAereo.Text & "," & ddlTransportador_BasicoAereo.SelectedValue & ", " & ddlOrigem_BasicoAereo.SelectedValue & ", " & ddlDestino_BasicoAereo.SelectedValue & "," & ddlCliente_BasicoAereo.SelectedValue & ", " & ddlExportador_BasicoAereo.SelectedValue & "," & ddlComissaria_BasicoAereo.SelectedValue & "," & ddlAgente_BasicoAereo.SelectedValue & "," & ddlIncoterm_BasicoAereo.SelectedValue & "," & ddlArmazem_BasicoAereo.SelectedValue & "," & ddlTipoPagamento_BasicoAereo.SelectedValue & "," & txtNumeroCE_BasicoAereo.Text & ", " & txtDataCE_BasicoAereo.Text & "," & txtRefAuxiliar_BasicoAereo.Text & "," & txtRefComercial_BasicoAereo.Text & ", " & ResumoMercadoria & "," & ddlServico_BasicoAereo.SelectedValue & ",'C'," & ddlImportador_BasicoAereo.SelectedValue & ",GETDATE(),'" & ckbFreeHand_BasicoAereo.Checked & "'," & ddlIndicador_BasicoAereo.SelectedValue & "," & ddlDivisaoProfit_BasicoAereo.SelectedValue & ", " & txtValorDivisaoProfit_BasicoAereo.Text & ", " & txtValorCarga_BasicoAereo.Text & "," & ddlTranspRodoviario_BasicoMaritimo.SelectedValue & ",'" & ckTrakingAutomaticoAereo.Checked & "', '" & ckbEmailCotacao_BasicoAereo.Checked & "'," & EmailCotacao & " , '" & ckbTC4_BasicoAereo.Checked & "','" & ckbTC6_BasicoAereo.Checked & "'," & ddlTipoAeronave_BasicoAereo.SelectedValue & " , " & Session("ID_USUARIO") & " ) Select SCOPE_IDENTITY() as ID_BL ")

                            'PREENCHE SESSÃO E CAMPO DE ID
                            Session("ID_BL") = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                            txtID_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()

                            NumeroProcesso()

                            Dim Calcula As New CalculaBL
                            txtProfitCalculado_BasicoAereo.Text = Calcula.CalculoProfit(txtID_BasicoAereo.Text)
                            If ddlDivisaoProfit_BasicoAereo.SelectedValue = 11 Or ddlDivisaoProfit_BasicoAereo.SelectedValue = 0 Then
                                txtValorDivisaoProfit_BasicoAereo.Text = txtProfitCalculado_BasicoAereo.Text
                            End If
                            dgvTaxaAereoCompras.DataBind()
                            dgvTaxaAereoVendas.DataBind()

                            DocConferido(txtID_BasicoAereo.Text, "A")

                            LimpaNulo()

                            Con.Fechar()
                            divSuccess_BasicoAereo.Visible = True
                        Else
                            divErro_BasicoAereo.Visible = True
                            lblErro_BasicoAereo.Text = "Já existe BL cadastrada com este número."
                            Exit Sub
                        End If


                    End If

                End If



            Else
                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else


                    'REALIZA UPDATE 
                    Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = " & txtProcesso_BasicoAereo.Text & " , NR_BL = " & txtHBL_BasicoAereo.Text & ",ID_PARCEIRO_TRANSPORTADOR = " & ddlTransportador_BasicoAereo.SelectedValue & ", ID_PORTO_ORIGEM = " & ddlOrigem_BasicoAereo.SelectedValue & ", ID_PORTO_DESTINO = " & ddlDestino_BasicoAereo.SelectedValue & ", ID_PARCEIRO_CLIENTE = " & ddlCliente_BasicoAereo.SelectedValue & ", ID_PARCEIRO_EXPORTADOR = " & ddlExportador_BasicoAereo.SelectedValue & ", ID_PARCEIRO_COMISSARIA = " & ddlComissaria_BasicoAereo.SelectedValue & ", ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgente_BasicoAereo.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm_BasicoAereo.SelectedValue & ", ID_PARCEIRO_ARMAZEM_DESEMBARACO = " & ddlArmazem_BasicoAereo.SelectedValue & ", ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoAereo.SelectedValue & " ,  OB_REFERENCIA_AUXILIAR =" & txtRefAuxiliar_BasicoAereo.Text & ", OB_REFERENCIA_COMERCIAL = " & txtRefComercial_BasicoAereo.Text & ", NM_RESUMO_MERCADORIA = " & ResumoMercadoria & ",ID_SERVICO = " & ddlServico_BasicoAereo.SelectedValue & ", ID_PARCEIRO_IMPORTADOR = " & ddlImportador_BasicoAereo.SelectedValue & ",FL_FREE_HAND = '" & ckbFreeHand_BasicoAereo.Checked & "' ,ID_PARCEIRO_INDICADOR = " & ddlIndicador_BasicoAereo.SelectedValue & ",ID_PROFIT_DIVISAO = " & ddlDivisaoProfit_BasicoAereo.SelectedValue & ",VL_PROFIT_DIVISAO = " & txtValorDivisaoProfit_BasicoAereo.Text & ", VL_CARGA = " & txtValorCarga_BasicoAereo.Text & ",ID_PARCEIRO_RODOVIARIO = " & ddlTranspRodoviario_BasicoAereo.SelectedValue & ", FL_TRAKING_AUTOMATICO = '" & ckTrakingAutomaticoAereo.Checked & "', FL_EMAIL_COTACAO = '" & ckbEmailCotacao_BasicoAereo.Checked & "', EMAIL_COTACAO = " & EmailCotacao & " ,FL_TC4 = '" & ckbTC4_BasicoAereo.Checked & "' ,FL_TC6 = '" & ckbTC6_BasicoAereo.Checked & "',ID_TIPO_AERONAVE = " & ddlTipoAeronave_BasicoAereo.SelectedValue & ", ID_USUARIO_ULTIMA_ALTERACAO = " & Session("ID_USUARIO") & "  WHERE ID_BL = " & txtID_BasicoAereo.Text)

                    If txtNumeroCE_BasicoAereo.Text <> "" Then
                        ds = Con.ExecutarQuery("SELECT ISNULL(NR_CE,'')NR_CE, DT_CE FROM TB_BL WHERE ID_BL = " & txtID_BasicoAereo.Text & "")
                        If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) And txtNumeroCE_BasicoAereo.Text <> "" Then
                            Con.ExecutarQuery("UPDATE TB_BL SET DT_CE = GETDATE(), NR_CE = '" & txtNumeroCE_BasicoAereo.Text & "' WHERE DT_CE IS NULL AND ID_BL = " & txtID_BasicoAereo.Text & "")
                        ElseIf ds.Tables(0).Rows(0).Item("NR_CE").ToString <> txtNumeroCE_BasicoAereo.Text Then
                            Con.ExecutarQuery("UPDATE TB_BL SET DT_CE = GETDATE(), NR_CE = '" & txtNumeroCE_BasicoAereo.Text & "' WHERE ID_BL = " & txtID_BasicoAereo.Text & "")
                        End If
                    End If

                    'ds = Con.ExecutarQuery("SELECT YEAR(DT_ABERTURA)ANO_ABERTURA,ID_BL_MASTER,ISNULL(NR_BL,0)NR_BL FROM [TB_BL] WHERE ID_BL = " & txtID_BasicoAereo.Text & "")
                    'If ds.Tables(0).Rows(0).Item("ANO_ABERTURA") >= 2022 And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) And ds.Tables(0).Rows(0).Item("NR_BL") <> "0" Then
                    '    Dim Rastreio As New RastreioService
                    '    Rastreio.trackingbl(txtID_BasicoAereo.Text)
                    'End If

                    Dim Calcula As New CalculaBL
                    txtProfitCalculado_BasicoAereo.Text = Calcula.CalculoProfit(txtID_BasicoAereo.Text)
                    If ddlDivisaoProfit_BasicoAereo.SelectedValue = 11 Or ddlDivisaoProfit_BasicoAereo.SelectedValue = 0 Then
                        txtValorDivisaoProfit_BasicoAereo.Text = txtProfitCalculado_BasicoAereo.Text
                    End If
                    dgvTaxaAereoCompras.DataBind()
                    dgvTaxaAereoVendas.DataBind()

                    DocConferido(txtID_BasicoAereo.Text, "A")

                    LimpaNulo()

                    divSuccess_BasicoAereo.Visible = True
                    Con.Fechar()


                End If




            End If


        End If

        txtValorDivisaoProfit_BasicoAereo.Text = txtValorDivisaoProfit_BasicoAereo.Text.Replace(".", ",")
        txtValorCarga_BasicoAereo.Text = txtValorCarga_BasicoAereo.Text.Replace(".", ",")

    End Sub

    Private Sub btnSalvar_CargaAereo_Click(sender As Object, e As EventArgs) Handles btnSalvar_CargaAereo.Click
        lblSuccess_CargaAereo2.Text = "Registro cadastrado/atualizado com sucesso!"

        divSuccess_CargaAereo2.Visible = False
        divErro_CargaAereo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        If txtGrupoNCM_CargaAereo.Text = "" Then
            txtGrupoNCM_CargaAereo.Text = "NULL"
        Else
            txtGrupoNCM_CargaAereo.Text = "'" & txtGrupoNCM_CargaAereo.Text & "'"
        End If

        If txtPesoBruto_CargaAereo.Text = "" Then
            txtPesoBruto_CargaAereo.Text = 0
        End If

        If txtQtdVolume_CargaAereo.Text = "" Then
            txtQtdVolume_CargaAereo.Text = 0
        End If

        If txtPesoVolumetrico_CargaAereo.Text = "" Then
            txtPesoVolumetrico_CargaAereo.Text = 0
        End If

        If txtAltura_CargaAereo.Text = "" Then
            txtAltura_CargaAereo.Text = 0
        End If

        If txtLargura_CargaAereo.Text = "" Then
            txtLargura_CargaAereo.Text = 0
        End If

        If txtComprimento_CargaAereo.Text = "" Then
            txtComprimento_CargaAereo.Text = 0
        End If

        If Session("ID_BL_MASTER") Is Nothing Then
            Session("ID_BL_MASTER") = 0
        End If

        'If txtDescMercadoria_CargaAereo.Text = "" Then
        '    txtDescMercadoria_CargaAereo.Text = "NULL"
        'Else
        '    txtDescMercadoria_CargaAereo.Text = "'" & txtDescMercadoria_CargaAereo.Text & "'"
        'End If

        Dim DescMercadoria As String = txtDescMercadoria_CargaAereo.Text
        If DescMercadoria = "" Then
            DescMercadoria = "NULL"
        Else
            DescMercadoria = DescMercadoria.Replace("'", "''")
            DescMercadoria = "'" & DescMercadoria & "'"
        End If


        txtPesoBruto_CargaAereo.Text = txtPesoBruto_CargaAereo.Text.Replace(".", "")
        txtPesoBruto_CargaAereo.Text = txtPesoBruto_CargaAereo.Text.Replace(",", ".")

        txtPesoVolumetrico_CargaAereo.Text = txtPesoVolumetrico_CargaAereo.Text.Replace(".", "")
        txtPesoVolumetrico_CargaAereo.Text = txtPesoVolumetrico_CargaAereo.Text.Replace(",", ".")

        txtComprimento_CargaAereo.Text = txtComprimento_CargaAereo.Text.Replace(".", "")
        txtComprimento_CargaAereo.Text = txtComprimento_CargaAereo.Text.Replace(",", ".")

        txtLargura_CargaAereo.Text = txtLargura_CargaAereo.Text.Replace(".", "")
        txtLargura_CargaAereo.Text = txtLargura_CargaAereo.Text.Replace(",", ".")

        txtAltura_CargaAereo.Text = txtAltura_CargaAereo.Text.Replace(".", "")
        txtAltura_CargaAereo.Text = txtAltura_CargaAereo.Text.Replace(",", ".")

        Dim ID_NCM As String = 0

        If ddlNCM_CargaAereo.SelectedIndex <> 0 Then
            ID_NCM = ddlNCM_CargaAereo.SelectedValue 'SeparaNCM(ddlNCM_CargaAereo.SelectedValue)
        End If


        If txtPesoVolumetrico_CargaAereo.Text = "" Then
            txtPesoVolumetrico_CargaAereo.Text = 0

        End If

        If txtID_CargaAereo.Text = "" Then

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro_CargaAereo2.Visible = True
                lblErro_CargaAereo2.Text = "Usuário não possui permissão para cadastrar."
                ' Exit Sub

            Else

                'INSERE 
                ds = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_BL,ID_TIPO_CARGA,ID_NCM,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,DS_MERCADORIA,QT_MERCADORIA,DS_GRUPO_NCM,ID_EMBALAGEM) VALUES (" & txtID_BasicoAereo.Text & "," & ddlMercadoria_CargaAereo.SelectedValue & ", " & ID_NCM & ", " & txtPesoBruto_CargaAereo.Text & "," & txtPesoVolumetrico_CargaAereo.Text & ", " & txtAltura_CargaAereo.Text & "," & txtLargura_CargaAereo.Text & "," & txtComprimento_CargaAereo.Text & "," & DescMercadoria & "," & txtQtdVolume_CargaAereo.Text & "," & txtGrupoNCM_CargaAereo.Text & ", " & ddlEmbalagem_CargaAereo.SelectedValue & ") Select SCOPE_IDENTITY() as ID_CARGA_BL ")
                Dim ID_CARGA_BL As String = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")
                txtID_CargaAereo.Text = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")

                AdicionarMedidasAereo()
                divErro_CargaAereo2.Visible = False
                btnAdicionarMedidasAereo.Visible = True
                divMedidasAereo.Attributes.CssStyle.Add("display", "block")

                Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text)


                Dim Calcula As New CalculaBL
                Dim dsTaxa As DataSet = Con.ExecutarQuery("Select CONVERT(VARCHAR,ID_BL_TAXA)ID_BL_TAXA,ID_BL FROM [FN_TAXAS_BL_CALCULO](" & txtID_BasicoAereo.Text & ") where id_bl= " & txtID_BasicoAereo.Text)
                If dsTaxa.Tables(0).Rows.Count > 0 Then
                    For Each linha As DataRow In dsTaxa.Tables(0).Rows
                        Dim retorno As String = Calcula.Calcular(linha.Item("ID_BL_TAXA").ToString())
                    Next

                End If

                Calcula.CalculoProfit(txtID_BasicoAereo.Text)
                dgvTaxaAereoCompras.DataBind()
                dgvTaxaAereoVendas.DataBind()




                Con.Fechar()
                divSuccess_CargaAereo2.Visible = True
                dgvCargaAereo.DataBind()
                dgvTaxaAereoCompras.DataBind()
                dgvTaxaAereoVendas.DataBind()

            End If

        Else

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro_CargaAereo2.Visible = True
                lblErro_CargaAereo2.Text = "Usuário não possui permissão para alterar."
                Exit Sub

            Else


                'REALIZA UPDATE 
                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_BL = " & txtID_BasicoAereo.Text & ",ID_TIPO_CARGA = " & ddlMercadoria_CargaAereo.SelectedValue & ",ID_NCM = " & ID_NCM & ",VL_PESO_BRUTO = " & txtPesoBruto_CargaAereo.Text & ",VL_M3 = " & txtPesoVolumetrico_CargaAereo.Text & ",VL_ALTURA =" & txtAltura_CargaAereo.Text & ",VL_LARGURA = " & txtLargura_CargaAereo.Text & ",VL_COMPRIMENTO = " & txtComprimento_CargaAereo.Text & ",DS_MERCADORIA = " & DescMercadoria & ", QT_MERCADORIA = " & txtQtdVolume_CargaAereo.Text & ",DS_GRUPO_NCM = " & txtGrupoNCM_CargaAereo.Text & ", ID_EMBALAGEM = " & ddlEmbalagem_CargaAereo.SelectedValue & " WHERE ID_CARGA_BL = " & txtID_CargaAereo.Text)



                Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text)

                divSuccess_CargaAereo2.Visible = True
                Con.Fechar()
                dgvCargaAereo.DataBind()

                Dim Calcula As New CalculaBL
                Dim dsTaxa As DataSet = Con.ExecutarQuery("Select CONVERT(VARCHAR,ID_BL_TAXA)ID_BL_TAXA,ID_BL FROM [FN_TAXAS_BL_CALCULO](" & txtID_BasicoAereo.Text & ") where id_bl= " & txtID_BasicoAereo.Text)
                If dsTaxa.Tables(0).Rows.Count > 0 Then
                    For Each linha As DataRow In dsTaxa.Tables(0).Rows
                        Dim retorno As String = Calcula.Calcular(linha.Item("ID_BL_TAXA").ToString())
                    Next

                End If

                Calcula.CalculoProfit(txtID_BasicoAereo.Text)
                dgvTaxaAereoCompras.DataBind()
                dgvTaxaAereoVendas.DataBind()

                CalculaCLA()

                dgvTaxaAereoCompras.DataBind()
                dgvTaxaAereoVendas.DataBind()

            End If


        End If
        ' txtDescMercadoria_CargaAereo.Text = txtDescMercadoria_CargaAereo.Text.Replace("'", "")
        ' txtDescMercadoria_CargaAereo.Text = txtDescMercadoria_CargaAereo.Text.Replace("NULL", "")

        txtGrupoNCM_CargaAereo.Text = txtGrupoNCM_CargaAereo.Text.Replace("'", "")
        txtGrupoNCM_CargaAereo.Text = txtGrupoNCM_CargaAereo.Text.Replace("NULL", "")


        txtPesoVolumetrico_CargaAereo.Text = txtPesoVolumetrico_CargaAereo.Text.Replace(".", ",")

        txtPesoBruto_CargaAereo.Text = txtPesoBruto_CargaAereo.Text.Replace(".", ",")
    End Sub

    Private Sub btnSalvar_CargaMaritimo_Click(sender As Object, e As EventArgs) Handles btnSalvar_CargaMaritimo.Click
        divSuccess_CargaMaritimo2.Visible = False
        divErro_CargaMaritimo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        If ddlExportador_BasicoAereo.SelectedValue = 71 Then
            txtValorVenda_TaxaAereo.Text = "0"
            txtMinVenda_TaxaAereo.Text = "0"
            ddlDestinatarioCob_TaxaAereo.SelectedValue = 3
        End If

        If txtQtdVolumes_CargaMaritimo.Text = "" Then
            txtQtdVolumes_CargaMaritimo.Text = 0
        End If

        If txtGrupoNCM_CargaMaritimo.Text = "" Then
            txtGrupoNCM_CargaMaritimo.Text = "NULL"
        Else
            txtGrupoNCM_CargaMaritimo.Text = "'" & txtGrupoNCM_CargaMaritimo.Text & "'"
        End If

        If txtPesoVolumetrico_CargaMaritimo.Text = "" Then
            txtPesoVolumetrico_CargaMaritimo.Text = 0
        End If

        If txtPesoBruto_CargaMaritimo.Text = "" Then
            txtPesoBruto_CargaMaritimo.Text = 0
        End If

        If Session("ID_BL_MASTER") Is Nothing Then
            Session("ID_BL_MASTER") = 0
        End If

        txtPesoBruto_CargaMaritimo.Text = txtPesoBruto_CargaMaritimo.Text.Replace(".", "")
        txtPesoBruto_CargaMaritimo.Text = txtPesoBruto_CargaMaritimo.Text.Replace(",", ".")



        Dim ID_NCM As String = 0

        If ddlNCM_CargaMaritimo.SelectedIndex <> 0 Then
            ID_NCM = ddlNCM_CargaMaritimo.SelectedValue 'SeparaNCM(ddlNCM_CargaMaritimo.SelectedValue)
        End If


        Dim DescMercadoria As String = txtDescMercadoriaCNTR_Maritimo.Text
        If DescMercadoria = "" Then
            DescMercadoria = "NULL"
        Else
            DescMercadoria = DescMercadoria.Replace("'", "''")
            DescMercadoria = "'" & DescMercadoria & "'"
        End If

        If txtPesoVolumetrico_CargaMaritimo.Text = "" Then
            divErro_CargaMaritimo2.Visible = True
            lblErro_CargaMaritimo2.Text = "É necessário informar o Peso Volumetrico da carga."

        ElseIf txtPesoVolumetrico_CargaMaritimo.Text <= 0 Then
            divErro_CargaMaritimo2.Visible = True
            lblErro_CargaMaritimo2.Text = "Peso Volumetrico da carga deve ser maior que zero."


        ElseIf Session("ID_BL_MASTER") <> 0 Then

            ds = Con.ExecutarQuery("SELECT COUNT(ID_CNTR_BL)QTD FROM TB_CNTR_BL WHERE ID_BL_MASTER = " & Session("ID_BL_MASTER"))
            If ds.Tables(0).Rows(0).Item("QTD") <> 0 Then
                Dim VL_M3_TOTAL As Decimal = 0
                Dim VL_MAXIMO As Decimal = 0
                Dim QTD_CNTR As Integer = 0
                Dim VL_M3 As Decimal = 0
                Dim ID_Carga As Integer = 0


                If txtID_CargaMaritimo.Text <> "" Then
                    ID_Carga = txtID_CargaMaritimo.Text
                End If


                ds = Con.ExecutarQuery("SELECT SUM(VL_M3)VL_M3_TOTAL FROM TB_CARGA_BL where id_bl IN (SELECT ID_BL FROM TB_BL WHERE ID_BL_MASTER = " & Session("ID_BL_MASTER") & " ) AND ID_CARGA_BL <> " & ID_Carga)
                If ds.Tables(0).Rows.Count > 0 And Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_M3_TOTAL")) Then
                    VL_M3_TOTAL = ds.Tables(0).Rows(0).Item("VL_M3_TOTAL")
                End If

                ds = Con.ExecutarQuery("SELECT count(ID_CNTR_BL)QTD_CNTR,(count(ID_CNTR_BL) * 80)VL_MAXIMO FROM TB_CNTR_BL WHERE ID_BL_MASTER = " & Session("ID_BL_MASTER"))
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("QTD_CNTR")) Then
                        QTD_CNTR = ds.Tables(0).Rows(0).Item("QTD_CNTR")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_MAXIMO")) Then
                        VL_MAXIMO = ds.Tables(0).Rows(0).Item("VL_MAXIMO")
                    End If
                End If


                VL_M3 = VL_M3_TOTAL + txtPesoVolumetrico_CargaMaritimo.Text
                If VL_M3 > VL_MAXIMO Then
                    divErro_CargaMaritimo2.Visible = True
                    lblErro_CargaMaritimo2.Text = "Valor M3 superior ao permitido para quantidade containers!"

                    txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(".", ",")

                    txtPesoBruto_CargaMaritimo.Text = txtPesoBruto_CargaMaritimo.Text.Replace(".", ",")

                    ' txtDescMercadoriaCNTR_Maritimo.Text = txtDescMercadoriaCNTR_Maritimo.Text.Replace("'", "")
                    ' txtDescMercadoriaCNTR_Maritimo.Text = txtDescMercadoriaCNTR_Maritimo.Text.Replace("NULL", "")

                    txtGrupoNCM_CargaMaritimo.Text = txtGrupoNCM_CargaMaritimo.Text.Replace("'", "")
                    txtGrupoNCM_CargaMaritimo.Text = txtGrupoNCM_CargaMaritimo.Text.Replace("NULL", "")

                    mpeCargaMaritimo.Show()
                    Exit Sub

                End If
            End If
        End If


        If txtID_CargaMaritimo.Text = "" Then


            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro_CargaMaritimo2.Visible = True
                lblErro_CargaMaritimo2.Text = "Usuário não possui permissão para cadastrar."

            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_CARGA_BL)QTD FROM TB_CARGA_BL where ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.SelectedValue & "  AND ID_BL = " & txtID_BasicoMaritimo.Text)
                If ds.Tables(0).Rows(0).Item("QTD") > 0 And ddlNumeroCNTR_CargaMaritimo.SelectedValue <> 0 Then
                    divErro_CargaMaritimo2.Visible = True
                    lblErro_CargaMaritimo2.Text = "Container vinculado em outra carga desta mesma BL."

                Else



                    txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(".", "")
                    txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(",", ".")


                    'INSERE 
                    ds = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_CNTR_BL, ID_EMBALAGEM, DS_GRUPO_NCM,ID_BL,ID_TIPO_CARGA,ID_NCM,VL_PESO_BRUTO,VL_M3,QT_MERCADORIA,DS_MERCADORIA,ID_TIPO_CNTR) VALUES (" & ddlNumeroCNTR_CargaMaritimo.SelectedValue & "," & ddlEmbalagem_CargaMaritimo.SelectedValue & "," & txtGrupoNCM_CargaMaritimo.Text & "," & txtID_BasicoMaritimo.Text & "," & ddlMercadoria_CargaMaritimo.SelectedValue & ", " & ID_NCM & ", " & txtPesoBruto_CargaMaritimo.Text & "," & txtPesoVolumetrico_CargaMaritimo.Text & "," & txtQtdVolumes_CargaMaritimo.Text & "," & DescMercadoria & "," & ddlTipoContainer_CargaMaritimo.SelectedValue & ") Select SCOPE_IDENTITY() as ID_CARGA_BL ")
                    Dim ID_CARGA_BL As String = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")


                    If ddlNumeroCNTR_CargaMaritimo.SelectedValue <> 0 Then
                        Call AMR_CNTR_INSERT(txtID_BasicoMaritimo.Text, ID_CARGA_BL)
                    End If

                    Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)

                    Dim Calcula As New CalculaBL
                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_BL_TAXA FROM TB_BL_TAXA A WHERE  ID_BASE_CALCULO_TAXA IS NOT NULL AND VL_TAXA IS NOT NULL AND VL_TAXA <> 0 AND ID_BASE_CALCULO_TAXA <> 1 AND ID_MOEDA <> 0 AND ISNULL(ID_BL_TAXA_MASTER,0) = 0 AND ISNULL(ID_BL_MASTER,0) = 0  AND ID_BL = " & txtID_BasicoMaritimo.Text & " And ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER= A.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL  AND ID_BL_TAXA IS NOT NULL)")
                    If dsTaxa.Tables(0).Rows.Count > 0 Then
                        For Each linha As DataRow In dsTaxa.Tables(0).Rows
                            Dim retorno As String = Calcula.Calcular(linha.Item("ID_BL_TAXA").ToString())
                        Next

                    End If

                    Calcula.CalculoProfit(txtID_BasicoMaritimo.Text)
                    dgvTaxaMaritimoCompras.DataBind()
                    dgvTaxaMaritimoVendas.DataBind()

                    dgvCargaMaritimo.DataBind()

                    Con.Fechar()
                    divSuccess_CargaMaritimo2.Visible = True
                End If
            End If


        Else

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro_CargaMaritimo2.Visible = True
                lblErro_CargaMaritimo2.Text = "Usuário não possui permissão para alterar."


            Else
                ds = Con.ExecutarQuery("SELECT COUNT(ID_CARGA_BL)QTD FROM TB_CARGA_BL where ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.SelectedValue & "  AND ID_BL = " & txtID_BasicoMaritimo.Text & " AND ID_CARGA_BL <> " & txtID_CargaMaritimo.Text)
                If ds.Tables(0).Rows(0).Item("QTD") > 0 And ddlNumeroCNTR_CargaMaritimo.SelectedValue <> 0 Then
                    divErro_CargaMaritimo2.Visible = True
                    lblErro_CargaMaritimo2.Text = "Container vinculado em outra carga desta mesma BL."

                Else

                    If ddlNumeroCNTR_CargaMaritimo.SelectedValue <> 0 Then
                        Call AMR_CNTR_UPDATE(txtID_BasicoMaritimo.Text, txtID_CargaMaritimo.Text)
                    End If


                    txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(".", "")
                    txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(",", ".")

                    'REALIZA UPDATE 
                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_BL = " & txtID_BasicoMaritimo.Text & ",ID_TIPO_CARGA = " & ddlMercadoria_CargaMaritimo.SelectedValue & ",ID_NCM = " & ID_NCM & ",VL_PESO_BRUTO = " & txtPesoBruto_CargaMaritimo.Text & ",VL_M3 = " & txtPesoVolumetrico_CargaMaritimo.Text & ",ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.SelectedValue & ", ID_EMBALAGEM = " & ddlEmbalagem_CargaMaritimo.SelectedValue & ", DS_GRUPO_NCM = " & txtGrupoNCM_CargaMaritimo.Text & ",DS_MERCADORIA = " & DescMercadoria & ", QT_MERCADORIA = " & txtQtdVolumes_CargaMaritimo.Text & ", ID_TIPO_CNTR = " & ddlTipoContainer_CargaMaritimo.SelectedValue & " WHERE ID_CARGA_BL = " & txtID_CargaMaritimo.Text)

                    Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)

                    Dim Calcula As New CalculaBL
                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_BL_TAXA FROM TB_BL_TAXA A WHERE  ID_BASE_CALCULO_TAXA IS NOT NULL AND VL_TAXA IS NOT NULL AND VL_TAXA <> 0 AND  ID_BASE_CALCULO_TAXA <> 1 AND ID_MOEDA <> 0 AND ISNULL(ID_BL_TAXA_MASTER,0) = 0 AND ISNULL(ID_BL_MASTER,0) = 0  AND ID_BL = " & txtID_BasicoMaritimo.Text & " And ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER= A.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL  AND ID_BL_TAXA IS NOT NULL)")
                    If dsTaxa.Tables(0).Rows.Count > 0 Then
                        For Each linha As DataRow In dsTaxa.Tables(0).Rows
                            Dim retorno As String = Calcula.Calcular(linha.Item("ID_BL_TAXA").ToString())
                        Next
                    End If

                    Calcula.CalculoProfit(txtID_BasicoMaritimo.Text)
                    dgvTaxaMaritimoCompras.DataBind()
                    dgvTaxaMaritimoVendas.DataBind()

                    divSuccess_CargaMaritimo2.Visible = True
                    Con.Fechar()

                End If

            End If


        End If

        txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(".", ",")

        txtPesoBruto_CargaMaritimo.Text = txtPesoBruto_CargaMaritimo.Text.Replace(".", ",")

        '  txtDescMercadoriaCNTR_Maritimo.Text = txtDescMercadoriaCNTR_Maritimo.Text.Replace("'", "")
        'txtDescMercadoriaCNTR_Maritimo.Text = txtDescMercadoriaCNTR_Maritimo.Text.Replace("NULL", "")

        txtGrupoNCM_CargaMaritimo.Text = txtGrupoNCM_CargaMaritimo.Text.Replace("'", "")
        txtGrupoNCM_CargaMaritimo.Text = txtGrupoNCM_CargaMaritimo.Text.Replace("NULL", "")

        mpeCargaMaritimo.Show()

    End Sub


    Private Sub btnSalvar_TaxaAereo_Click(sender As Object, e As EventArgs) Handles btnSalvar_TaxaAereo.Click

        divSuccess_TaxaAereo2.Visible = False
        divErro_TaxaAereo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData


        If txtMinCompra_TaxaAereo.Text = "" Then
            txtMinCompra_TaxaAereo.Text = 0
        End If

        If txtValorCompra_TaxaAereo.Text = "" Then
            txtValorCompra_TaxaAereo.Text = 0
        End If

        If txtMinVenda_TaxaAereo.Text = "" Then
            txtMinVenda_TaxaAereo.Text = 0
        End If

        If txtValorVenda_TaxaAereo.Text = "" Then
            txtValorVenda_TaxaAereo.Text = 0
        End If


        If txtQtdBaseCalculo_TaxaAereo.Text = "" Then
            txtQtdBaseCalculo_TaxaAereo.Text = 0
        End If




        If txtValorCompra_TaxaAereo.Text = 0 And txtValorVenda_TaxaAereo.Text = 0 Then
            divErro_TaxaAereo2.Visible = True
            lblErro_TaxaAereo2.Text = "Não é possivel cadastrar taxas com valores zerados."
            mpeTaxaAereo.Show()
            Exit Sub

        ElseIf ddlOrigemPagamento_TaxaAereo.SelectedValue = 0 Or ddlTipoPagamento_TaxaAereo.SelectedValue = 0 Then
            divErro_TaxaAereo2.Visible = True
            lblErro_TaxaAereo2.Text = "Preencha os campos obrigatórios!"
            mpeTaxaAereo.Show()
            Exit Sub

        ElseIf (ddlBaseCalculo_TaxaAereo.SelectedValue = 38 Or ddlBaseCalculo_TaxaAereo.SelectedValue = 40 Or ddlBaseCalculo_TaxaAereo.SelectedValue = 41) And txtQtdBaseCalculo_TaxaAereo.Text = 0 Then
            divErro_TaxaAereo2.Visible = True
            lblErro_TaxaAereo2.Text = "Necessário preencher a quantidade para base de cálculo selecionada!"
            mpeTaxaAereo.Show()
            Exit Sub

        ElseIf txtValorVenda_TaxaAereo.Text <> 0 And ddlDestinatarioCob_TaxaAereo.SelectedValue = 0 Then
            divErro_TaxaAereo2.Visible = True
            lblErro_TaxaAereo2.Text = "Necessário preencher destinatario cobrança!"
            mpeTaxaAereo.Show()
            Exit Sub

        ElseIf ddlDespesa_TaxaAereo.SelectedValue = 71 And (txtValorVenda_TaxaAereo.Text <> 0 Or txtMinVenda_TaxaAereo.Text <> 0) Then
            divErro_TaxaAereo2.Visible = True
            lblErro_TaxaAereo2.Text = "Não possivel cadastrar taxa de premiação de venda!"
            mpeTaxaAereo.Show()
            Exit Sub
        Else

            If ddlDespesa_TaxaAereo.SelectedValue = 71 Then
                txtValorVenda_TaxaAereo.Text = "0"
                txtMinVenda_TaxaAereo.Text = "0"
                ddlDestinatarioCob_TaxaAereo.SelectedValue = 3
            End If


            Dim ObsTaxa As String = ""
            If txtObs_TaxaAereo.Text = "" Then
                ObsTaxa = "NULL"
            Else
                ObsTaxa = txtObs_TaxaAereo.Text
                ObsTaxa = ObsTaxa.Replace("'", "''")
                ObsTaxa = "'" & ObsTaxa & "'"
            End If


            txtMinCompra_TaxaAereo.Text = txtMinCompra_TaxaAereo.Text.Replace(".", "")
            txtMinCompra_TaxaAereo.Text = txtMinCompra_TaxaAereo.Text.Replace(",", ".")

            txtValorCompra_TaxaAereo.Text = txtValorCompra_TaxaAereo.Text.Replace(".", "")
            txtValorCompra_TaxaAereo.Text = txtValorCompra_TaxaAereo.Text.Replace(",", ".")

            txtMinVenda_TaxaAereo.Text = txtMinVenda_TaxaAereo.Text.Replace(".", "")
            txtMinVenda_TaxaAereo.Text = txtMinVenda_TaxaAereo.Text.Replace(",", ".")

            txtValorVenda_TaxaAereo.Text = txtValorVenda_TaxaAereo.Text.Replace(".", "")
            txtValorVenda_TaxaAereo.Text = txtValorVenda_TaxaAereo.Text.Replace(",", ".")


            If txtID_TaxaAereo.Text = "" Then


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxaAereo2.Visible = True
                    lblErro_TaxaAereo2.Text = "Usuário não possui permissão para cadastrar."
                    mpeTaxaAereo.Show()

                    Exit Sub

                Else
                    Dim dstaxa As DataSet
                    Dim ID_BL_TAXA As String
                    Dim Calcula As New CalculaBL
                    Dim retorno As String

                    If txtValorCompra_TaxaAereo.Text > 0 Then
                        'INSERE TAXA DE COMPRA
                        dstaxa = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF,DT_CRIACAO,ID_USUARIO_CRIACAO) VALUES (" & txtID_BasicoAereo.Text & "," & ddlDespesa_TaxaAereo.SelectedValue & "," & ddlTipoPagamento_TaxaAereo.SelectedValue & "," & ddlOrigemPagamento_TaxaAereo.SelectedValue & "," & ddlDestinatarioCob_TaxaAereo.SelectedValue & "," & ddlBaseCalculo_TaxaAereo.SelectedValue & "," & ddlMoedaCompra_TaxaAereo.SelectedValue & "," & txtValorCompra_TaxaAereo.Text & "," & txtMinCompra_TaxaAereo.Text & "," & ObsTaxa & ",'" & ckbDeclarado_TaxaAereo.Checked & "','" & ckbProfit_TaxaAereo.Checked & "'," & ddlEmpresa_TaxaAereo.SelectedValue & ",'" & ckbPremiacao_TaxaAereo.Checked & "','P','OPER',GETDATE()," & Session("ID_USUARIO") & ") Select SCOPE_IDENTITY() as ID_BL_TAXA ")
                        ID_BL_TAXA = dstaxa.Tables(0).Rows(0).Item("ID_BL_TAXA")
                        retorno = Calcula.Calcular(ID_BL_TAXA)
                    End If


                    If txtValorVenda_TaxaAereo.Text > 0 Then
                        Dim empresa As Integer = 0
                        If ddlDestinatarioCob_TaxaAereo.SelectedValue = 1 Then
                            'Cliente
                            empresa = ddlCliente_BasicoAereo.SelectedValue

                        ElseIf ddlDestinatarioCob_TaxaAereo.SelectedValue = 2 Then
                            'Agente
                            empresa = ddlAgente_BasicoAereo.SelectedValue

                        ElseIf ddlDestinatarioCob_TaxaAereo.SelectedValue = 4 Then
                            'Importador
                            empresa = ddlImportador_BasicoAereo.SelectedValue
                        ElseIf ddlDestinatarioCob_TaxaAereo.SelectedValue = 7 Then
                            'Transp Rodoviario
                            empresa = ddlTranspRodoviario_BasicoAereo.SelectedValue

                        ElseIf ddlDestinatarioCob_TaxaAereo.SelectedValue = 8 Then
                            'Exportador
                            empresa = ddlExportador_BasicoAereo.SelectedValue
                        End If

                        'INSERE TAXA DE VENDA
                        dstaxa = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF,DT_CRIACAO,ID_USUARIO_CRIACAO) VALUES (" & txtID_BasicoAereo.Text & "," & ddlDespesa_TaxaAereo.SelectedValue & "," & ddlTipoPagamento_TaxaAereo.SelectedValue & "," & ddlOrigemPagamento_TaxaAereo.SelectedValue & "," & ddlDestinatarioCob_TaxaAereo.SelectedValue & "," & ddlBaseCalculo_TaxaAereo.SelectedValue & "," & ddlMoedaVenda_TaxaAereo.SelectedValue & "," & txtValorVenda_TaxaAereo.Text & "," & txtMinVenda_TaxaAereo.Text & "," & ObsTaxa & ",'" & ckbDeclarado_TaxaAereo.Checked & "','" & ckbProfit_TaxaAereo.Checked & "'," & empresa & ",'" & ckbPremiacao_TaxaAereo.Checked & "','R','OPER',GETDATE()," & Session("ID_USUARIO") & ") Select SCOPE_IDENTITY() as ID_BL_TAXA ")

                        ID_BL_TAXA = dstaxa.Tables(0).Rows(0).Item("ID_BL_TAXA")
                        retorno = Calcula.Calcular(ID_BL_TAXA)
                    End If

                    ddlDespesa_TaxaAereo.SelectedValue = 0
                    'ddlTipoPagamento_TaxaAereo.SelectedValue = 0
                    ddlOrigemPagamento_TaxaAereo.SelectedValue = 0
                    'ddlDestinatarioCob_TaxaAereo.SelectedValue = 0
                    ddlBaseCalculo_TaxaAereo.SelectedValue = 0
                    ddlMoedaCompra_TaxaAereo.SelectedValue = 0
                    ddlMoedaVenda_TaxaAereo.SelectedValue = 0
                    ddlEmpresa_TaxaAereo.SelectedValue = 0
                    txtValorCompra_TaxaAereo.Text = ""
                    txtValorVenda_TaxaAereo.Text = ""
                    'txtBaseCompra_TaxaAereo.Text = ""
                    txtObs_TaxaAereo.Text = ""
                    txtMinCompra_TaxaAereo.Text = ""
                    txtMinVenda_TaxaAereo.Text = ""
                    txtID_TaxaAereo.Text = ""

                    dgvTaxaAereoVendas.DataBind()
                    dgvTaxaAereoCompras.DataBind()

                    Con.Fechar()
                    divSuccess_TaxaAereo2.Visible = True

                End If

            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxaAereo2.Visible = True
                    lblErro_TaxaAereo2.Text = "Usuário não possui permissão para alterar."

                    txtValorCompra_TaxaAereo.Text = txtValorCompra_TaxaAereo.Text.Replace(".", ",")
                    txtValorVenda_TaxaAereo.Text = txtValorVenda_TaxaAereo.Text.Replace(".", ",")
                    txtMinCompra_TaxaAereo.Text = txtMinCompra_TaxaAereo.Text.Replace(".", ",")
                    txtMinVenda_TaxaAereo.Text = txtMinVenda_TaxaAereo.Text.Replace(".", ",")
                    mpeTaxaAereo.Show()
                    Exit Sub

                Else
                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & txtID_TaxaAereo.Text & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxaAereo2.Visible = True
                        lblErro_TaxaAereo2.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
                        mpeTaxaAereo.Show()

                        txtValorCompra_TaxaAereo.Text = txtValorCompra_TaxaAereo.Text.Replace(".", ",")
                        txtValorVenda_TaxaAereo.Text = txtValorVenda_TaxaAereo.Text.Replace(".", ",")
                        txtMinCompra_TaxaAereo.Text = txtMinCompra_TaxaAereo.Text.Replace(".", ",")
                        txtMinVenda_TaxaAereo.Text = txtMinVenda_TaxaAereo.Text.Replace(".", ",")
                        Exit Sub

                    Else



                        If Session("CD_PR") = "P" Then

                            'REALIZA UPDATE TAXA COMPRA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoAereo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_TaxaAereo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaAereo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaAereo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaAereo.SelectedValue & ",ID_MOEDA =" & ddlMoedaCompra_TaxaAereo.SelectedValue & ",VL_TAXA = " & txtValorCompra_TaxaAereo.Text & ",VL_TAXA_MIN = " & txtMinCompra_TaxaAereo.Text & ",OB_TAXAS = " & ObsTaxa & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaAereo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaAereo.Checked & "', ID_PARCEIRO_EMPRESA =  " & ddlEmpresa_TaxaAereo.SelectedValue & ",FL_PREMIACAO ='" & ckbPremiacao_TaxaAereo.Checked & "',DT_ULTIMA_EDICAO = GETDATE(),ID_USUARIO_ULTIMA_EDICAO = " & Session("ID_USUARIO") & " WHERE ID_BL_TAXA = " & txtID_TaxaAereo.Text)

                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_CALCULADO = 0 WHERE ID_BL_TAXA_MASTER IS NULL AND ID_BL_TAXA = " & txtID_TaxaAereo.Text)

                        ElseIf Session("CD_PR") = "R" Then

                            'REALIZA UPDATE TAXA VENDA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoAereo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_TaxaAereo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaAereo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaAereo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaAereo.SelectedValue & ",ID_MOEDA =" & ddlMoedaVenda_TaxaAereo.SelectedValue & ",VL_TAXA = " & txtValorVenda_TaxaAereo.Text & ",VL_TAXA_MIN = " & txtMinVenda_TaxaAereo.Text & ",OB_TAXAS = " & ObsTaxa & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaAereo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaAereo.Checked & "', ID_PARCEIRO_EMPRESA =  " & ddlEmpresa_TaxaAereo.SelectedValue & ",FL_CALCULADO = 0,FL_PREMIACAO ='" & ckbPremiacao_TaxaAereo.Checked & "',DT_ULTIMA_EDICAO = GETDATE(),ID_USUARIO_ULTIMA_EDICAO = " & Session("ID_USUARIO") & " WHERE ID_BL_TAXA = " & txtID_TaxaAereo.Text)

                        End If

                        Dim Calcula As New CalculaBL
                        Dim retorno As String = Calcula.Calcular(txtID_TaxaAereo.Text)

                        txtValorCompra_TaxaAereo.Text = txtValorCompra_TaxaAereo.Text.Replace(".", ",")
                        txtValorVenda_TaxaAereo.Text = txtValorVenda_TaxaAereo.Text.Replace(".", ",")
                        txtMinCompra_TaxaAereo.Text = txtMinCompra_TaxaAereo.Text.Replace(".", ",")
                        txtMinVenda_TaxaAereo.Text = txtMinVenda_TaxaAereo.Text.Replace(".", ",")

                        divSuccess_TaxaAereo2.Visible = True
                        Con.Fechar()
                        dgvTaxaAereoVendas.DataBind()
                        dgvTaxaAereoCompras.DataBind()

                    End If
                End If

            End If
            mpeTaxaAereo.Show()

        End If


    End Sub

    Sub AMR_CNTR_UPDATE(ID_BL As Integer, ID_CARGA_BL As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim cntr As Integer

        'COMPARARA CNTR ATUAL(ANTES DO UPDATE) COM O CNTR DO FORMULARIO
        Dim dsCNTR As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_CNTR_BL,0)ID_CNTR_BL FROM TB_CARGA_BL WHERE ID_CARGA_BL =  " & ID_CARGA_BL)
        If dsCNTR.Tables(0).Rows.Count > 0 Then
            cntr = dsCNTR.Tables(0).Rows(0).Item("ID_CNTR_BL")
        End If
        'CASO SEJAM DIFERENTES
        If cntr <> ddlNumeroCNTR_CargaMaritimo.SelectedValue Then

            'VERIFICA SE HÁ AMARRAÇÃO DE CNTR ATUAL(ANTES DO UPDATE)
            Dim dsAMRAtual As DataSet = Con.ExecutarQuery("SELECT ID_AMR_CNTR_BL FROM TB_AMR_CNTR_BL WHERE ID_BL = " & ID_BL & "  AND ID_CNTR_BL =(SELECT ID_CNTR_BL FROM TB_CARGA_BL WHERE ID_CARGA_BL =  " & ID_CARGA_BL & ")")


            'CASO EXISTA
            If dsAMRAtual.Tables(0).Rows.Count > 0 Then
                'VERIFICA SE HÁ AMARRAÇÃO DE CNTR SELECIONADO NO FORMULARIO
                Dim dsAMRNovo As DataSet = Con.ExecutarQuery("SELECT ID_AMR_CNTR_BL FROM TB_AMR_CNTR_BL WHERE ID_BL = " & ID_BL & "  AND ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.Text)
                'CASO EXISTA
                If dsAMRNovo.Tables(0).Rows.Count > 0 Then

                    'VERIFICA SE ALGUMA OUTRA CARGA DESSE BL UTILIZA O CNTR ATUAL(ANTES DO UPDATE)
                    Dim dsAMROutro As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_CARGA_BL)QTD FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL & "  AND ID_CNTR_BL = (SELECT ID_CNTR_BL FROM TB_CARGA_BL WHERE ID_CARGA_BL =  " & ID_CARGA_BL & ") AND ID_CARGA_BL <> " & ID_CARGA_BL)

                    'CASO NÃO REALIZA O DELETE DO CNTR ATUAL(ANTES DO UPDATE)
                    If dsAMROutro.Tables(0).Rows(0).Item("QTD") = 0 Then
                        Con.ExecutarQuery("DELETE FROM TB_AMR_CNTR_BL WHERE ID_AMR_CNTR_BL = " & dsAMRNovo.Tables(0).Rows(0).Item("ID_AMR_CNTR_BL"))

                    End If
                Else

                    'VERIFICA SE ALGUMA OUTRA CARGA DESSE BL UTILIZA O CNTR ATUAL(ANTES DO UPDATE)
                    Dim dsAMROutro As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_CARGA_BL)QTD FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL & "  AND ID_CNTR_BL = (SELECT ID_CNTR_BL FROM TB_CARGA_BL WHERE ID_CARGA_BL =  " & ID_CARGA_BL & ") AND ID_CARGA_BL <> " & ID_CARGA_BL)

                    'CASO NÃO REALIZA, O DELETE DO CNTR ATUAL(ANTES DO UPDATE)
                    If dsAMROutro.Tables(0).Rows(0).Item("QTD") = 0 Then
                        'CASO NÃO REALIZA O UPDATE DO CNTR ATUAL(ANTES DO UPDATE) PARA O CNTR SELECIONADO NO FORMULARIO
                        Con.ExecutarQuery("UPDATE TB_AMR_CNTR_BL SET ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.Text & " WHERE ID_AMR_CNTR_BL = " & dsAMRAtual.Tables(0).Rows(0).Item("ID_AMR_CNTR_BL"))
                    Else
                        'CASO SIM, CHAMA ROTINA DE INSERÇÃO
                        Call AMR_CNTR_INSERT(ID_BL, ID_CARGA_BL)

                    End If
                End If
            Else
                'VERIFICA SE HÁ AMARRAÇÃO DE CNTR SELECIONADO NO FORMULARIO
                Dim dsAMRNovo As DataSet = Con.ExecutarQuery("SELECT ID_AMR_CNTR_BL FROM TB_AMR_CNTR_BL WHERE ID_BL = " & ID_BL & "  AND ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.Text)

                If dsAMRNovo.Tables(0).Rows.Count = 0 Then
                    'CASO NAO EXISTA, CHAMA ROTINA DE INSERÇÃO
                    Call AMR_CNTR_INSERT(ID_BL, ID_CARGA_BL)


                End If


            End If
        Else

            'VERIFICA SE HÁ AMARRAÇÃO DE CNTR SELECIONADO NO FORMULARIO
            Dim dsAMRNovo As DataSet = Con.ExecutarQuery("SELECT ID_AMR_CNTR_BL FROM TB_AMR_CNTR_BL WHERE ID_BL = " & ID_BL & "  AND ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.Text)

            If dsAMRNovo.Tables(0).Rows.Count = 0 Then
                'CASO NAO EXISTA, CHAMA ROTINA DE INSERÇÃO
                Call AMR_CNTR_INSERT(ID_BL, ID_CARGA_BL)


            End If

        End If
    End Sub


    Sub AMR_CNTR_INSERT(ID_BL As Integer, ID_CARGA_BL As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim dsAMR As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_AMR_CNTR_BL)QTD FROM TB_AMR_CNTR_BL WHERE ID_BL =" & ID_BL & "  AND ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.Text)

        If dsAMR.Tables(0).Rows(0).Item("QTD") = 0 Then
            Con.ExecutarQuery("INSERT INTO TB_AMR_CNTR_BL (ID_BL,ID_CNTR_BL) VALUES(" & ID_BL & "," & ddlNumeroCNTR_CargaMaritimo.Text & ")")
        End If

    End Sub

    Private Sub btnSalvar_TaxaMaritimo_Click(sender As Object, e As EventArgs) Handles btnSalvar_TaxaMaritimo.Click
        divSuccess_TaxaMaritimo2.Visible = False
        divErro_TaxaMaritimo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData




        If txtMinCompra_TaxaMaritimo.Text = "" Then
            txtMinCompra_TaxaMaritimo.Text = 0
        End If

        If txtValorCompra_TaxaMaritimo.Text = "" Then
            txtValorCompra_TaxaMaritimo.Text = 0
        End If

        If txtMinVenda_TaxaMaritimo.Text = "" Then
            txtMinVenda_TaxaMaritimo.Text = 0
        End If

        If txtValorVenda_TaxaMaritimo.Text = "" Then
            txtValorVenda_TaxaMaritimo.Text = 0
        End If

        If txtQtdBaseCalculo_TaxaMaritimo.Text = "" Then
            txtQtdBaseCalculo_TaxaMaritimo.Text = 0
        End If

        If txtValorCompra_TaxaMaritimo.Text = 0 And txtValorVenda_TaxaMaritimo.Text = 0 Then
            divErro_TaxaMaritimo2.Visible = True
            lblErro_TaxaMaritimo2.Text = "Não é possivel cadastrar taxas com valores zerados."
            mpeTaxaMaritimo.Show()
            Exit Sub

        ElseIf ddlOrigemPagamento_TaxaMaritimo.SelectedValue = 0 Or ddlTipoPagamento_TaxaMaritimo.SelectedValue = 0 Then
            divErro_TaxaMaritimo2.Visible = True
            lblErro_TaxaMaritimo2.Text = "Preencha os campos obrigatórios!"
            mpeTaxaMaritimo.Show()
            Exit Sub

        ElseIf (ddlBaseCalculo_TaxaMaritimo.SelectedValue = 38 Or ddlBaseCalculo_TaxaMaritimo.SelectedValue = 40 Or ddlBaseCalculo_TaxaMaritimo.SelectedValue = 41) And txtQtdBaseCalculo_TaxaMaritimo.Text = 0 Then
            divErro_TaxaMaritimo2.Visible = True
            lblErro_TaxaMaritimo2.Text = "Necessário preencher a quantidade para base de cálculo selecionada!"
            mpeTaxaMaritimo.Show()
            Exit Sub

        ElseIf txtValorVenda_TaxaMaritimo.Text <> 0 And ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 0 Then
            divErro_TaxaMaritimo2.Visible = True
            lblErro_TaxaMaritimo2.Text = "Necessário preencher destinatario cobrança!"
            mpeTaxaMaritimo.Show()
            Exit Sub

        ElseIf ddlDespesa_TaxaMaritimo.SelectedValue = 71 And ((txtValorVenda_TaxaMaritimo.Text <> 0) Or (txtMinVenda_TaxaMaritimo.Text <> 0)) Then
            divErro_TaxaMaritimo2.Visible = True
            lblErro_TaxaMaritimo2.Text = "Não possivel cadastrar taxa de premiação de venda!"
            mpeTaxaMaritimo.Show()
            Exit Sub
        Else

            If ddlDespesa_TaxaMaritimo.SelectedValue = 71 Then
                txtValorVenda_TaxaMaritimo.Text = "0"
                txtMinVenda_TaxaMaritimo.Text = "0"
                ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 3
            End If

            Dim ObsTaxa As String = ""
            If txtObs_TaxaMaritimo.Text = "" Then
                ObsTaxa = "NULL"
            Else
                ObsTaxa = txtObs_TaxaMaritimo.Text
                ObsTaxa = ObsTaxa.Replace("'", "''")
                ObsTaxa = "'" & ObsTaxa & "'"
            End If

            txtMinCompra_TaxaMaritimo.Text = txtMinCompra_TaxaMaritimo.Text.Replace("-", "")

            txtValorCompra_TaxaMaritimo.Text = txtValorCompra_TaxaMaritimo.Text.Replace("-", "")

            txtMinVenda_TaxaMaritimo.Text = txtMinVenda_TaxaMaritimo.Text.Replace("-", "")

            txtValorVenda_TaxaMaritimo.Text = txtValorVenda_TaxaMaritimo.Text.Replace("-", "")


            txtMinCompra_TaxaMaritimo.Text = txtMinCompra_TaxaMaritimo.Text.Replace(".", "")
            txtMinCompra_TaxaMaritimo.Text = txtMinCompra_TaxaMaritimo.Text.Replace(",", ".")

            txtValorCompra_TaxaMaritimo.Text = txtValorCompra_TaxaMaritimo.Text.Replace(".", "")
            txtValorCompra_TaxaMaritimo.Text = txtValorCompra_TaxaMaritimo.Text.Replace(",", ".")

            txtMinVenda_TaxaMaritimo.Text = txtMinVenda_TaxaMaritimo.Text.Replace(".", "")
            txtMinVenda_TaxaMaritimo.Text = txtMinVenda_TaxaMaritimo.Text.Replace(",", ".")

            txtValorVenda_TaxaMaritimo.Text = txtValorVenda_TaxaMaritimo.Text.Replace(".", "")
            txtValorVenda_TaxaMaritimo.Text = txtValorVenda_TaxaMaritimo.Text.Replace(",", ".")



            ds = Con.ExecutarQuery("SELECT ISNULL(ID_TIPO_ITEM_DESPESA,0)ID_TIPO_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlDespesa_TaxaMaritimo.SelectedValue)
            Dim OPERADOR As String = "+"
            If ds.Tables(0).Rows(0).Item("ID_TIPO_ITEM_DESPESA") = 3 Then
                OPERADOR = "-"
            Else
                OPERADOR = "+"
            End If

            If txtID_TaxaMaritimo.Text = "" Then



                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxaMaritimo2.Visible = True
                    lblErro_TaxaMaritimo2.Text = "Usuário não possui permissão para cadastrar."

                    mpeTaxaMaritimo.Show()
                    txtValorCompra_TaxaMaritimo.Text = txtValorCompra_TaxaMaritimo.Text.Replace(".", ",")
                    txtValorVenda_TaxaMaritimo.Text = txtValorVenda_TaxaMaritimo.Text.Replace(".", ",")
                    txtMinCompra_TaxaMaritimo.Text = txtMinCompra_TaxaMaritimo.Text.Replace(".", ",")
                    txtMinVenda_TaxaMaritimo.Text = txtMinVenda_TaxaMaritimo.Text.Replace(".", ",")
                    Exit Sub

                Else
                    Dim dstaxa As DataSet
                    Dim ID_BL_TAXA As String
                    Dim Calcula As New CalculaBL
                    Dim retorno As String

                    If txtValorCompra_TaxaMaritimo.Text > 0 Then
                        'INSERE TAXA DE COMPRA

                        dstaxa = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF,QTD_BASE_CALCULO,DT_CRIACAO,ID_USUARIO_CRIACAO) VALUES (" & txtID_BasicoMaritimo.Text & "," & ddlDespesa_TaxaMaritimo.SelectedValue & "," & ddlTipoPagamento_TaxaMaritimo.SelectedValue & "," & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & "," & ddlStatusPagamento_TaxaMaritimo.SelectedValue & "," & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & "," & ddlBaseCalculo_TaxaMaritimo.SelectedValue & "," & ddlMoedaCompra_TaxaMaritimo.SelectedValue & "," & OPERADOR & txtValorCompra_TaxaMaritimo.Text & "," & OPERADOR & txtMinCompra_TaxaMaritimo.Text & "," & ObsTaxa & ",'" & ckbDeclarado_TaxaMaritimo.Checked & "','" & ckbProfit_TaxaMaritimo.Checked & "'," & ddlEmpresa_TaxaMaritimo.SelectedValue & ",'" & ckbPremiacao_TaxaMaritimo.Checked & "','P','OPER'," & txtQtdBaseCalculo_TaxaMaritimo.Text & ",GETDATE()," & Session("ID_USUARIO") & ") Select SCOPE_IDENTITY() as ID_BL_TAXA ")

                        ID_BL_TAXA = dstaxa.Tables(0).Rows(0).Item("ID_BL_TAXA")

                        retorno = Calcula.Calcular(ID_BL_TAXA)
                        divSuccess_TaxaMaritimo2.Visible = True

                    End If

                    If txtValorVenda_TaxaMaritimo.Text > 0 Then
                        Dim empresa As Integer = 0
                        If ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 1 Then
                            'Cliente
                            empresa = ddlCliente_BasicoMaritimo.SelectedValue
                        ElseIf ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 2 Then
                            'Agente
                            empresa = ddlAgente_BasicoMaritimo.SelectedValue
                        ElseIf ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 4 Then
                            'Importador
                            empresa = ddlImportador_BasicoMaritimo.SelectedValue
                        ElseIf ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 7 Then
                            'Transp Rodoviario
                            empresa = ddlTranspRodoviario_BasicoMaritimo.SelectedValue
                        ElseIf ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 8 Then
                            'Exportador
                            empresa = ddlExportador_BasicoMaritimo.SelectedValue

                        End If

                        'INSERE TAXA DE VENDA
                        dstaxa = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF,QTD_BASE_CALCULO,DT_CRIACAO,ID_USUARIO_CRIACAO) VALUES (" & txtID_BasicoMaritimo.Text & "," & ddlDespesa_TaxaMaritimo.SelectedValue & "," & ddlTipoPagamento_TaxaMaritimo.SelectedValue & "," & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & "," & ddlStatusPagamento_TaxaMaritimo.SelectedValue & "," & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & "," & ddlBaseCalculo_TaxaMaritimo.SelectedValue & "," & ddlMoedaVenda_TaxaMaritimo.SelectedValue & "," & OPERADOR & txtValorVenda_TaxaMaritimo.Text & "," & OPERADOR & txtMinVenda_TaxaMaritimo.Text & "," & ObsTaxa & ",'" & ckbDeclarado_TaxaMaritimo.Checked & "','" & ckbProfit_TaxaMaritimo.Checked & "'," & empresa & ",'" & ckbPremiacao_TaxaMaritimo.Checked & "','R','OPER'," & txtQtdBaseCalculo_TaxaMaritimo.Text & ",GETDATE()," & Session("ID_USUARIO") & ") Select SCOPE_IDENTITY() as ID_BL_TAXA ")

                        ID_BL_TAXA = dstaxa.Tables(0).Rows(0).Item("ID_BL_TAXA")
                        retorno = Calcula.Calcular(ID_BL_TAXA)
                        divSuccess_TaxaMaritimo2.Visible = True

                    End If

                    dgvTaxaMaritimoCompras.DataBind()
                    dgvTaxaMaritimoVendas.DataBind()

                    txtID_TaxaMaritimo.Text = ""
                    ddlStatusPagamento_TaxaMaritimo.SelectedValue = 0
                    ddlDespesa_TaxaMaritimo.SelectedValue = 0
                    ddlOrigemPagamento_TaxaMaritimo.SelectedValue = 0
                    ddlBaseCalculo_TaxaMaritimo.SelectedValue = 0
                    ddlMoedaCompra_TaxaMaritimo.SelectedValue = 0
                    ddlMoedaVenda_TaxaMaritimo.SelectedValue = 0
                    ddlEmpresa_TaxaMaritimo.SelectedValue = 0
                    txtValorCompra_TaxaMaritimo.Text = ""
                    txtValorVenda_TaxaMaritimo.Text = ""
                    txtObs_TaxaMaritimo.Text = ""
                    txtMinCompra_TaxaMaritimo.Text = ""
                    txtMinVenda_TaxaMaritimo.Text = ""
                    txtQtdBaseCalculo_TaxaMaritimo.Text = ""



                    Con.Fechar()
                End If




            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxaMaritimo2.Visible = True
                    lblErro_TaxaMaritimo2.Text = "Usuário não possui permissão para alterar."

                    mpeTaxaMaritimo.Show()
                    txtValorCompra_TaxaMaritimo.Text = txtValorCompra_TaxaMaritimo.Text.Replace(".", ",")
                    txtValorVenda_TaxaMaritimo.Text = txtValorVenda_TaxaMaritimo.Text.Replace(".", ",")
                    txtMinCompra_TaxaMaritimo.Text = txtMinCompra_TaxaMaritimo.Text.Replace(".", ",")
                    txtMinVenda_TaxaMaritimo.Text = txtMinVenda_TaxaMaritimo.Text.Replace(".", ",")
                    Exit Sub

                Else
                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & txtID_TaxaMaritimo.Text & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxaMaritimo2.Visible = True
                        lblErro_TaxaMaritimo2.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"

                        mpeTaxaMaritimo.Show()
                        txtValorCompra_TaxaMaritimo.Text = txtValorCompra_TaxaMaritimo.Text.Replace(".", ",")
                        txtValorVenda_TaxaMaritimo.Text = txtValorVenda_TaxaMaritimo.Text.Replace(".", ",")
                        txtMinCompra_TaxaMaritimo.Text = txtMinCompra_TaxaMaritimo.Text.Replace(".", ",")
                        txtMinVenda_TaxaMaritimo.Text = txtMinVenda_TaxaMaritimo.Text.Replace(".", ",")
                        Exit Sub
                    Else


                        'REALIZA UPDATE 


                        If Session("CD_PR") = "P" Then

                            'REALIZA UPDATE TAXA COMPRA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoMaritimo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaMaritimo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_TaxaMaritimo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaMaritimo.SelectedValue & ",ID_MOEDA =" & ddlMoedaCompra_TaxaMaritimo.SelectedValue & ",VL_TAXA = " & OPERADOR & txtValorCompra_TaxaMaritimo.Text & ",VL_TAXA_MIN = " & OPERADOR & txtMinCompra_TaxaMaritimo.Text & ",OB_TAXAS = " & ObsTaxa & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaMaritimo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaMaritimo.Checked & "',ID_PARCEIRO_EMPRESA = " & ddlEmpresa_TaxaMaritimo.SelectedValue & ", FL_PREMIACAO  = '" & ckbPremiacao_TaxaMaritimo.Checked & "', QTD_BASE_CALCULO  = " & txtQtdBaseCalculo_TaxaMaritimo.Text & ",DT_ULTIMA_EDICAO = GETDATE(),ID_USUARIO_ULTIMA_EDICAO = " & Session("ID_USUARIO") & " WHERE ID_BL_TAXA = " & txtID_TaxaMaritimo.Text)

                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_CALCULADO = 0 WHERE ID_BL_TAXA_MASTER IS NULL AND ID_BL_TAXA = " & txtID_TaxaMaritimo.Text)

                        ElseIf Session("CD_PR") = "R" Then

                            'REALIZA UPDATE TAXA VENDA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoMaritimo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaMaritimo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_TaxaMaritimo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaMaritimo.SelectedValue & ",ID_MOEDA =" & ddlMoedaVenda_TaxaMaritimo.SelectedValue & ",VL_TAXA = " & OPERADOR & txtValorVenda_TaxaMaritimo.Text & ",VL_TAXA_MIN = " & OPERADOR & txtMinVenda_TaxaMaritimo.Text & ",OB_TAXAS = " & ObsTaxa & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaMaritimo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaMaritimo.Checked & "',ID_PARCEIRO_EMPRESA = " & ddlEmpresa_TaxaMaritimo.SelectedValue & ",FL_CALCULADO = 0, FL_PREMIACAO  = '" & ckbPremiacao_TaxaMaritimo.Checked & "', QTD_BASE_CALCULO  = " & txtQtdBaseCalculo_TaxaMaritimo.Text & ",DT_ULTIMA_EDICAO = GETDATE(),ID_USUARIO_ULTIMA_EDICAO = " & Session("ID_USUARIO") & "  WHERE ID_BL_TAXA = " & txtID_TaxaMaritimo.Text)

                        End If

                        Dim Calcula As New CalculaBL
                        Dim retorno As String = Calcula.Calcular(txtID_TaxaMaritimo.Text)

                        dgvTaxaMaritimoCompras.DataBind()
                        dgvTaxaMaritimoVendas.DataBind()
                        divSuccess_TaxaMaritimo2.Visible = True


                        txtValorCompra_TaxaMaritimo.Text = txtValorCompra_TaxaMaritimo.Text.Replace(".", ",")
                        txtValorVenda_TaxaMaritimo.Text = txtValorVenda_TaxaMaritimo.Text.Replace(".", ",")
                        txtMinCompra_TaxaMaritimo.Text = txtMinCompra_TaxaMaritimo.Text.Replace(".", ",")
                        txtMinVenda_TaxaMaritimo.Text = txtMinVenda_TaxaMaritimo.Text.Replace(".", ",")

                        Con.Fechar()
                    End If
                End If

            End If
        End If

        mpeTaxaMaritimo.Show()

    End Sub

    Private Sub dgvRefMaritimo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvRefMaritimo.RowCommand
        divSuccess_RefMaritimo.Visible = False
        divErro_RefMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErro_RefMaritimo.Text = "Usuário não tem permissão para realizar exclusões"
                divErro_RefMaritimo.Visible = True
            Else
                Con.ExecutarQuery("DELETE From TB_REFERENCIA_CLIENTE Where ID_REFERENCIA_CLIENTE = " & ID)
                lblSuccess_RefMaritimo.Text = "Registro deletado!"
                divSuccess_RefMaritimo.Visible = True
                dgvRefMaritimo.DataBind()
            End If


        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select NR_REFERENCIA_CLIENTE,TIPO from TB_REFERENCIA_CLIENTE
    WHERE ID_REFERENCIA_CLIENTE = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                txtID_RefMaritimo.Text = ID

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")) Then
                    txtRefMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")
                    ddlTipoRefMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("TIPO").ToString
                End If

            End If
        End If
        Con.Fechar()
    End Sub
    Private Sub dgvRefAereo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvRefAereo.RowCommand
        divSuccess_RefAereo.Visible = False
        divErro_RefAereo.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErro_RefAereo.Text = "Usuário não tem permissão para realizar exclusões"
                divErro_RefAereo.Visible = True
            Else
                Con.ExecutarQuery("DELETE From TB_REFERENCIA_CLIENTE Where ID_REFERENCIA_CLIENTE = " & ID)
                lblSuccess_RefAereo.Text = "Registro deletado!"
                divSuccess_RefAereo.Visible = True
                dgvRefAereo.DataBind()
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select NR_REFERENCIA_CLIENTE,TIPO from TB_REFERENCIA_CLIENTE
    WHERE ID_REFERENCIA_CLIENTE = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                txtID_RefAereo.Text = ID

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")) Then
                    txtRefAereo.Text = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")
                    ddlTipoRefAereo.SelectedValue = ds.Tables(0).Rows(0).Item("TIPO").ToString
                End If

            End If
        End If
        Con.Fechar()
    End Sub

    Sub NumeroProcesso()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Processo_" & Now.Year.ToString & " NRSEQUENCIALPROCESSO")

        Dim PROCESSO_FINAL As String

        Dim NRSEQUENCIALPROCESSO As Integer = ds.Tables(0).Rows(0).Item("NRSEQUENCIALPROCESSO")
        Dim ano_atual = Now.Year.ToString.Substring(2)
        Dim SIGLA_PROCESSO As String
        Dim mes_atual As String
        If Now.Month < 10 Then
            mes_atual = "0" & Now.Month.ToString
        Else
            mes_atual = Now.Month.ToString
        End If


        If txtID_BasicoMaritimo.Text <> "" Then
            ds = Con.ExecutarQuery("Select A.ID_SERVICO,(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO from TB_BL A Where ID_SERVICO <> 0 AND A.ID_BL = " & txtID_BasicoMaritimo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")

                PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

                Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "', ANOSEQUENCIALPROCESSO = year(getdate()) ")

                Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = '" & PROCESSO_FINAL & "' WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
                txtProcesso_BasicoMaritimo.Text = PROCESSO_FINAL


            End If


        ElseIf txtID_BasicoAereo.Text <> "" Then

            ds = Con.ExecutarQuery("Select A.ID_SERVICO,(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO from TB_BL A Where ID_SERVICO <> 0 AND A.ID_BL = " & txtID_BasicoAereo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")

                PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

                Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "', ANOSEQUENCIALPROCESSO = year(getdate()) ")

                Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = '" & PROCESSO_FINAL & "' WHERE ID_BL = " & txtID_BasicoAereo.Text)
                txtProcesso_BasicoAereo.Text = PROCESSO_FINAL

            End If


        End If
    End Sub

    Sub PermissoesEspeciais()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_GRAVAR_MASTER_BASICO,FL_GRAVAR_MASTER_CONTAINER,FL_GRAVAR_MASTER_TAXAS,FL_GRAVAR_MASTER_VINCULAR,FL_GRAVAR_HOUSE_BASICO,FL_GRAVAR_HOUSE_CARGA,FL_GRAVAR_HOUSE_TAXAS
FROM TB_USUARIO where ID_USUARIO =" & Session("ID_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_HOUSE_BASICO") = True Then
                btnGravar_BasicoMaritimo.Visible = True
                btnLimpar_BasicoMaritimo.Visible = True
                btnGravar_BasicoAereo.Visible = True
                btnLimpar_BasicoAereo.Visible = True
            End If

            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_HOUSE_CARGA") = True Then

                btnNovaCargaMaritimo.Visible = True
                btnSalvar_CargaMaritimo.Visible = True
                btnNovaCargaAereo.Visible = True
                btnSalvar_CargaAereo.Visible = True
            End If

            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_HOUSE_TAXAS") = True Then
                btnNovaTaxaMaritimo.Visible = True
                btnSalvar_TaxaMaritimo.Visible = True
                btnNovaTaxaAereo.Visible = True
                btnSalvar_TaxaAereo.Visible = True
            End If

        End If



    End Sub

    Private Sub ddlDespesa_TaxaMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDespesa_TaxaMaritimo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT FL_PREMIACAO FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlDespesa_TaxaMaritimo.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            ckbPremiacao_TaxaMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_PREMIACAO")
        End If
        If ddlDespesa_TaxaMaritimo.SelectedValue = 71 Then
            txtValorVenda_TaxaMaritimo.Enabled = False
            txtValorVenda_TaxaMaritimo.Text = 0
            txtMinVenda_TaxaMaritimo.Enabled = False
            txtMinVenda_TaxaMaritimo.Text = 0
            ddlMoedaVenda_TaxaMaritimo.Enabled = False
            ddlMoedaVenda_TaxaMaritimo.SelectedValue = 0
            ddlDestinatarioCob_TaxaMaritimo.Enabled = False
            ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 3
        Else
            txtValorVenda_TaxaMaritimo.Enabled = True
            txtMinVenda_TaxaMaritimo.Enabled = True
            ddlMoedaVenda_TaxaMaritimo.Enabled = True
            ddlDestinatarioCob_TaxaMaritimo.Enabled = True
        End If

    End Sub

    Private Sub ddlDespesa_TaxaAereo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDespesa_TaxaAereo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT FL_PREMIACAO FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            ckbPremiacao_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_PREMIACAO")
        End If

        If ddlDespesa_TaxaAereo.SelectedValue = 71 Then
            txtValorVenda_TaxaAereo.Enabled = False
            txtValorVenda_TaxaAereo.Text = 0
            txtMinVenda_TaxaAereo.Enabled = False
            txtMinVenda_TaxaAereo.Text = 0
            ddlMoedaVenda_TaxaAereo.Enabled = False
            ddlMoedaVenda_TaxaAereo.SelectedValue = 0
            ddlDestinatarioCob_TaxaAereo.Enabled = False
            ddlDestinatarioCob_TaxaAereo.SelectedValue = 3
        Else
            txtValorVenda_TaxaAereo.Enabled = True
            txtMinVenda_TaxaAereo.Enabled = True
            ddlMoedaVenda_TaxaAereo.Enabled = True
            ddlDestinatarioCob_TaxaAereo.Enabled = True
        End If

    End Sub

    Private Sub ddlTipoContainer_CargaMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoContainer_CargaMaritimo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim sql As String = "SELECT ID_CNTR_BL, NR_CNTR FROM TB_CNTR_BL WHERE ID_TIPO_CNTR = " & ddlTipoContainer_CargaMaritimo.SelectedValue & " AND ID_BL_MASTER = " & Session("ID_BL_MASTER") & "
union SELECT 0, 'Selecione' FROM [dbo].[TB_CNTR_BL] ORDER BY ID_CNTR_BL"
        Dim ds As DataSet = Con.ExecutarQuery(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsCNTR.SelectCommand = sql
            ddlNumeroCNTR_CargaMaritimo.DataBind()
        End If
    End Sub

    Private Sub ddlNumeroCNTR_CargaMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNumeroCNTR_CargaMaritimo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(VL_PESO_TARA,0)VL_PESO_TARA,ISNULL(NR_LACRE,0)NR_LACRE, ISNULL(ID_TIPO_CNTR,0)ID_TIPO_CNTR FROM TB_CNTR_BL WHERE ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            txtNumeroLacre_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_LACRE")
            txtValorTara_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TARA")
            ddlTipoContainer_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CNTR")
        End If
    End Sub

    Private Sub txtValorVenda_TaxaMaritimo_TextChanged(sender As Object, e As EventArgs) Handles txtValorVenda_TaxaMaritimo.TextChanged
        If txtID_TaxaMaritimo.Text = "" And txtValorCompra_TaxaMaritimo.Text <> "" And txtValorVenda_TaxaMaritimo.Text <> "" Then

            Dim VENDA As Double = txtValorVenda_TaxaMaritimo.Text
            Dim COMPRA As Double = txtValorCompra_TaxaMaritimo.Text
            If VENDA < COMPRA Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "Func()", True)
            End If
        End If

    End Sub

    Private Sub txtValorVenda_TaxaAereo_TextChanged(sender As Object, e As EventArgs) Handles txtValorVenda_TaxaAereo.TextChanged
        If txtID_TaxaAereo.Text = "" And txtValorVenda_TaxaAereo.Text <> "" And txtValorCompra_TaxaAereo.Text <> "" Then

            Dim VENDA As Double = txtValorVenda_TaxaAereo.Text
            Dim COMPRA As Double = txtValorCompra_TaxaAereo.Text
            If VENDA < COMPRA Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "Func()", True)
            End If
        End If
    End Sub

    Private Sub ddlEstufagem_BasicoMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEstufagem_BasicoMaritimo.SelectedIndexChanged
        If ddlEstufagem_BasicoMaritimo.SelectedValue = 1 Then
            divMercadoriaCNTR_Maritimo.Attributes.CssStyle.Add("display", "block")
            divMercadoriaBL_Maritimo.Attributes.CssStyle.Add("display", "none")

        ElseIf ddlEstufagem_BasicoMaritimo.SelectedValue = 2 Then
            divMercadoriaCNTR_Maritimo.Attributes.CssStyle.Add("display", "block")
            divMercadoriaBL_Maritimo.Attributes.CssStyle.Add("display", "block")

        End If
    End Sub

    Private Sub ddlEstufagem_BasicoAereo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEstufagem_BasicoAereo.SelectedIndexChanged
        If ddlEstufagem_BasicoAereo.SelectedValue = 1 Then
            divMercadoriaCNTR_Aereo.Attributes.CssStyle.Add("display", "block")
            divMercadoriaBL_Aereo.Attributes.CssStyle.Add("display", "none")

        ElseIf ddlEstufagem_BasicoAereo.SelectedValue = 2 Then
            divMercadoriaCNTR_Aereo.Attributes.CssStyle.Add("display", "BLOCK")
            divMercadoriaBL_Aereo.Attributes.CssStyle.Add("display", "block")

        End If
    End Sub

    Private Sub txtNomeCliente_Maritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeCliente_Maritimo.TextChanged
        divErro_BasicoMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodCliente_Maritimo.Text = "" Then
            txtCodCliente_Maritimo.Text = 0
        End If
        If txtNomeCliente_Maritimo.Text = "" Then
            txtNomeCliente_Maritimo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (FL_EXPORTADOR= 1 OR FL_IMPORTADOR =1 OR FL_AGENTE = 1 OR FL_AGENTE_INTERNACIONAL =1 OR FL_COMISSARIA = 1 OR FL_INDICADOR = 1) and (NM_RAZAO like '%" & txtNomeCliente_Maritimo.Text & "%' or ID_PARCEIRO =  " & txtCodCliente_Maritimo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsCliente_Maritimo.SelectCommand = Sql
            dsCliente_Maritimo.DataBind()
            ddlCliente_BasicoMaritimo.DataBind()
        Else
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "Parceiro não encontrado!"
        End If
        txtNomeCliente_Maritimo.Text = txtNomeCliente_Maritimo.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeImportador_Maritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeImportador_Maritimo.TextChanged
        divErro_BasicoMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodImportador_Maritimo.Text = "" Then
            txtCodImportador_Maritimo.Text = 0
        End If
        If txtNomeImportador_Maritimo.Text = "" Then
            txtNomeImportador_Maritimo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_IMPORTADOR =1 and  (NM_RAZAO like '%" & txtNomeImportador_Maritimo.Text & "%' or ID_PARCEIRO =  " & txtCodImportador_Maritimo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsImportador_Maritimo.SelectCommand = Sql
            dsImportador_Maritimo.DataBind()
            ddlImportador_BasicoMaritimo.DataBind()
        Else
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "Parceiro não encontrado!"
        End If
        txtNomeImportador_Maritimo.Text = txtNomeImportador_Maritimo.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeCliente_Aereo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeCliente_Aereo.TextChanged
        divErro_BasicoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodCliente_Aereo.Text = "" Then
            txtCodCliente_Aereo.Text = 0
        End If
        If txtNomeCliente_Aereo.Text = "" Then
            txtNomeCliente_Aereo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (FL_EXPORTADOR= 1 OR FL_IMPORTADOR =1 OR FL_AGENTE = 1 OR FL_AGENTE_INTERNACIONAL =1 OR FL_COMISSARIA = 1 OR FL_INDICADOR = 1) and  (NM_RAZAO like '%" & txtNomeCliente_Aereo.Text & "%' or ID_PARCEIRO =  " & txtCodCliente_Aereo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsCliente_Aereo.SelectCommand = Sql
            dsCliente_Aereo.DataBind()
            ddlCliente_BasicoAereo.DataBind()
        Else
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "Parceiro não encontrado!"
        End If
        txtNomeCliente_Aereo.Text = txtNomeCliente_Aereo.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeImportador_Aereo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeImportador_Aereo.TextChanged
        divErro_BasicoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodImportador_Aereo.Text = "" Then
            txtCodImportador_Aereo.Text = 0
        End If
        If txtNomeImportador_Aereo.Text = "" Then
            txtNomeImportador_Aereo.Text = "NULL"
        End If
        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_IMPORTADOR =1 and  (NM_RAZAO like '%" & txtNomeImportador_Aereo.Text & "%' or ID_PARCEIRO =  " & txtCodImportador_Aereo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsImportador_Aereo.SelectCommand = Sql
            dsImportador_Aereo.DataBind()
            ddlImportador_BasicoAereo.DataBind()
        Else
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "Parceiro não encontrado!"
        End If
        txtNomeImportador_Aereo.Text = txtNomeImportador_Aereo.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeComissaria_Aereo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeComissaria_Aereo.TextChanged
        divErro_BasicoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodComissaria_Aereo.Text = "" Then
            txtCodComissaria_Aereo.Text = 0
        End If
        If txtNomeComissaria_Aereo.Text = "" Then
            txtNomeComissaria_Aereo.Text = "NULL"
        End If
        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_COMISSARIA =1 and (NM_RAZAO like '%" & txtNomeComissaria_Aereo.Text & "%' or ID_PARCEIRO =  " & txtCodComissaria_Aereo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsComissaria_Aereo.SelectCommand = Sql
            dsComissaria_Aereo.DataBind()
            ddlComissaria_BasicoAereo.DataBind()
        Else
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "Parceiro não encontrado!"
        End If
        txtNomeComissaria_Aereo.Text = txtNomeComissaria_Aereo.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeComissaria_Maritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeComissaria_Maritimo.TextChanged
        divErro_BasicoMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodComissaria_Maritimo.Text = "" Then
            txtCodComissaria_Maritimo.Text = 0
        End If
        If txtNomeComissaria_Maritimo.Text = "" Then
            txtNomeComissaria_Maritimo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_COMISSARIA =1 and (NM_RAZAO like '%" & txtNomeComissaria_Maritimo.Text & "%' or ID_PARCEIRO =  " & txtCodComissaria_Maritimo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsComissaria_Maritimo.SelectCommand = Sql
            dsComissaria_Maritimo.DataBind()
            ddlComissaria_BasicoMaritimo.DataBind()
        Else
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "Parceiro não encontrado!"
        End If
        txtNomeComissaria_Maritimo.Text = txtNomeComissaria_Maritimo.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeExportador_Maritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeExportador_Maritimo.TextChanged
        divErro_BasicoMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodExportador_Maritimo.Text = "" Then
            txtCodExportador_Maritimo.Text = 0
        End If
        If txtNomeExportador_Maritimo.Text = "" Then
            txtNomeExportador_Maritimo.Text = "NULL"
        End If

        Dim Sql As String = ""
        If ddlServico_BasicoMaritimo.SelectedValue = 1 Then
            Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  ( FL_SHIPPER = 1 ) and (NM_RAZAO like '%" & txtNomeExportador_Maritimo.Text & "%' or ID_PARCEIRO =  " & txtCodExportador_Maritimo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        ElseIf ddlServico_BasicoMaritimo.SelectedValue = 4 Then
            Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  ( FL_EXPORTADOR =1 ) and (NM_RAZAO like '%" & txtNomeExportador_Maritimo.Text & "%' or ID_PARCEIRO =  " & txtCodExportador_Maritimo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        End If

        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsExportador_Maritimo.SelectCommand = Sql
            dsExportador_Maritimo.DataBind()
            ddlExportador_BasicoMaritimo.DataBind()
        Else
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "Parceiro não encontrado!"
        End If
        txtNomeExportador_Maritimo.Text = txtNomeExportador_Maritimo.Text.Replace("NULL", "")

    End Sub
    Private Sub txtNomeExportador_Aereo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeExportador_Aereo.TextChanged
        divErro_BasicoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodExportador_Aereo.Text = "" Then
            txtCodExportador_Aereo.Text = 0
        End If
        If txtNomeExportador_Aereo.Text = "" Then
            txtNomeExportador_Aereo.Text = "NULL"
        End If
        Dim Sql As String = ""

        If ddlServico_BasicoAereo.SelectedValue = 2 Then
            Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (FL_SHIPPER = 1 ) and (NM_RAZAO like '%" & txtNomeExportador_Aereo.Text & "%' or ID_PARCEIRO =  " & txtCodExportador_Aereo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        ElseIf ddlServico_BasicoAereo.SelectedValue = 5 Then
            Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (FL_EXPORTADOR =1 ) and (NM_RAZAO like '%" & txtNomeExportador_Aereo.Text & "%' or ID_PARCEIRO =  " & txtCodExportador_Aereo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        End If
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsExportador_Aereo.SelectCommand = Sql
            dsExportador_Aereo.DataBind()
            ddlExportador_BasicoAereo.DataBind()
        Else
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "Parceiro não encontrado!"
        End If
        txtNomeExportador_Aereo.Text = txtNomeExportador_Aereo.Text.Replace("NULL", "")

    End Sub

    Private Sub lkProximo_Click(sender As Object, e As EventArgs) Handles lkProximo.Click
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim LinhaAtual As Integer = 0
        Dim ProximaLinha As Integer = 0
        Dim PrimeiroBL As String = 0
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_BL,ISNULL(ID_BL_MASTER,0) ID_BL_MASTER, ROW_NUMBER() OVER(ORDER BY ID_BL) AS num
  From TB_BL where ID_BL_MASTER =  " & Session("ID_BL_MASTER"))
        If ds.Tables(0).Rows.Count > 0 Then
            PrimeiroBL = ds.Tables(0).Rows(0).Item("ID_BL")
            For Each linha As DataRow In ds.Tables(0).Rows
                If linha.Item("ID_BL") = Request.QueryString("id") Then
                    LinhaAtual = linha.Item("num")
                    ProximaLinha = linha.Item("num") + 1
                End If

                If ProximaLinha = linha.Item("num") Then
                    Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
                    url = String.Format(url, linha.Item("ID_BL"))
                    Response.Redirect(url)
                ElseIf ProximaLinha > ds.Tables(0).Rows.Count Then
                    Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
                    url = String.Format(url, PrimeiroBL)
                    Response.Redirect(url)
                End If

            Next

            'Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
            'url = String.Format(url, ds.Tables(0).Rows(0).Item("ID_BL"))
            'Response.Redirect(url)
        End If
    End Sub

    Private Sub lkAnterior_Click(sender As Object, e As EventArgs) Handles lkAnterior.Click
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim LinhaAtual As Integer = 0
        Dim ProximaLinha As Integer = 0
        Dim PrimeiroBL As String = 0
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_BL,ISNULL(ID_BL_MASTER,0) ID_BL_MASTER, ROW_NUMBER() OVER(ORDER BY ID_BL desc) AS num
  From TB_BL where ID_BL_MASTER =  " & Session("ID_BL_MASTER") & "")
        If ds.Tables(0).Rows.Count > 0 Then
            PrimeiroBL = ds.Tables(0).Rows(0).Item("ID_BL")
            For Each linha As DataRow In ds.Tables(0).Rows
                If linha.Item("ID_BL") = Request.QueryString("id") Then
                    LinhaAtual = linha.Item("num")
                    ProximaLinha = linha.Item("num") + 1
                End If

                If ProximaLinha = linha.Item("num") Then
                    Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
                    url = String.Format(url, linha.Item("ID_BL"))
                    Response.Redirect(url)
                ElseIf ProximaLinha > ds.Tables(0).Rows.Count Then
                    Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
                    url = String.Format(url, PrimeiroBL)
                    Response.Redirect(url)
                End If

            Next

            'Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
            'url = String.Format(url, ds.Tables(0).Rows(0).Item("ID_BL"))
            'Response.Redirect(url)
        End If
    End Sub
    Private Sub btnVisualizarMBL_Maritimo_Click(sender As Object, e As EventArgs) Handles btnVisualizarMBL_Maritimo.Click

        If Request.QueryString("tipo") = "e" Then

            Dim Con As New Conexao_sql
            Con.Conectar()

            Session("ddlServico_BasicoMaritimo") = ddlServico_BasicoMaritimo.Text
            Session("ddlTransportador_BasicoMaritimo") = ddlTransportador_BasicoMaritimo.SelectedValue
            Session("ddlOrigem_BasicoMaritimo") = ddlOrigem_BasicoMaritimo.SelectedValue
            Session("ddlDestino_BasicoMaritimo") = ddlDestino_BasicoMaritimo.SelectedValue
            Session("ddlAgente_BasicoMaritimo") = ddlAgente_BasicoMaritimo.SelectedValue
            Session("ddlTipoPagamento_BasicoMaritimo") = 0 'ddlTipoPagamento_BasicoMaritimo.SelectedValue
            Session("ddlEstufagem_BasicoMaritimo") = ddlEstufagem_BasicoMaritimo.SelectedValue

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_STATUS_FRETE_AGENTE,ID_COTACAO FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                Session("ID_COTACAO") = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                Session("ID_STATUS_FRETE_AGENTE") = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString()
            End If

            Response.Redirect("CadastrarMaster.aspx?g=M&s=M")

        ElseIf Request.QueryString("tipo") = "h" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MBLMaritimo()", True)
        End If

    End Sub

    Private Sub btnVisualizarMBL_Aereo_Click(sender As Object, e As EventArgs) Handles btnVisualizarMBL_Aereo.Click
        If Request.QueryString("tipo") = "e" Then

            Dim Con As New Conexao_sql
            Con.Conectar()



            Session("ddlServico_BasicoAereo") = ddlServico_BasicoAereo.Text
            Session("ddlTransportador_BasicoAereo") = ddlTransportador_BasicoAereo.SelectedValue
            Session("ddlOrigem_BasicoAereo") = ddlOrigem_BasicoAereo.SelectedValue
            Session("ddlDestino_BasicoAereo") = ddlDestino_BasicoAereo.SelectedValue
            Session("ddlAgente_BasicoAereo") = ddlAgente_BasicoAereo.SelectedValue
            Session("ddlTipoPagamento_BasicoAereo") = 0 'ddlTipoPagamento_BasicoAereo.SelectedValue
            Session("ddlEstufagem_BasicoAereo") = ddlEstufagem_BasicoAereo.SelectedValue

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_STATUS_FRETE_AGENTE,ID_COTACAO FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                Session("ID_COTACAO") = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                Session("ID_STATUS_FRETE_AGENTE") = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString()
            End If


            Response.Redirect("CadastrarMaster.aspx?g=A&s=A")


        ElseIf Request.QueryString("tipo") = "h" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MBLAereo()", True)
        End If
    End Sub


    Private Sub txtNomeIndicador_Maritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeIndicador_Maritimo.TextChanged
        divErro_BasicoMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodIndicador_Maritimo.Text = "" Then
            txtCodIndicador_Maritimo.Text = 0
        End If
        If txtNomeIndicador_Maritimo.Text = "" Then
            txtNomeIndicador_Maritimo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_INDICADOR =  1  and  (NM_RAZAO like '%" & txtNomeIndicador_Maritimo.Text & "%' or ID_PARCEIRO =  " & txtCodIndicador_Maritimo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsIndicador_Maritimo.SelectCommand = Sql
            dsIndicador_Maritimo.DataBind()
            ddlIndicador_BasicoMaritimo.DataBind()
        Else
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "Parceiro não encontrado!"
        End If
        txtNomeIndicador_Maritimo.Text = txtNomeIndicador_Maritimo.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeTransportador_Maritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeTransportador_Maritimo.TextChanged
        divErro_BasicoMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodTransportador_Maritimo.Text = "" Then
            txtCodTransportador_Maritimo.Text = 0
        End If
        If txtNomeTransportador_Maritimo.Text = "" Then
            txtNomeTransportador_Maritimo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_TRANSPORTADOR =  1  and  (NM_RAZAO like '%" & txtNomeTransportador_Maritimo.Text & "%' or ID_PARCEIRO =  " & txtCodTransportador_Maritimo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsTransportador_Maritimo.SelectCommand = Sql
            dsTransportador_Maritimo.DataBind()
            ddlTransportador_BasicoMaritimo.DataBind()
        Else
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "Parceiro não encontrado!"
        End If
        txtNomeTransportador_Maritimo.Text = txtNomeTransportador_Maritimo.Text.Replace("NULL", "")
    End Sub

    Private Sub txtNomeAgente_Maritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeAgente_Maritimo.TextChanged
        divErro_BasicoMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodAgente_Maritimo.Text = "" Then
            txtCodAgente_Maritimo.Text = 0
        End If
        If txtNomeAgente_Maritimo.Text = "" Then
            txtNomeAgente_Maritimo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE FL_AGENTE_INTERNACIONAL = 1 and (NM_RAZAO like '%" & txtNomeAgente_Maritimo.Text & "%' or ID_PARCEIRO =  " & txtCodAgente_Maritimo.Text & ") union SELECT  0,'',' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsAgente_Maritimo.SelectCommand = Sql
            dsAgente_Maritimo.DataBind()
            ddlAgente_BasicoMaritimo.DataBind()
        Else
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "Parceiro não encontrado!"
        End If
        txtNomeAgente_Maritimo.Text = txtNomeAgente_Maritimo.Text.Replace("NULL", "")

    End Sub


    Private Sub txtNomeIndicador_Aereo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeIndicador_Aereo.TextChanged
        divErro_BasicoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodIndicador_Aereo.Text = "" Then
            txtCodIndicador_Aereo.Text = 0
        End If
        If txtNomeIndicador_Aereo.Text = "" Then
            txtNomeIndicador_Aereo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_INDICADOR =  1  and  (NM_RAZAO like '%" & txtNomeIndicador_Aereo.Text & "%' or ID_PARCEIRO =  " & txtCodIndicador_Aereo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsIndicador_Aereo.SelectCommand = Sql
            dsIndicador_Aereo.DataBind()
            ddlIndicador_BasicoAereo.DataBind()
        Else
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "Parceiro não encontrado!"
        End If
        txtNomeIndicador_Aereo.Text = txtNomeIndicador_Aereo.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeTransportador_Aereo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeTransportador_Aereo.TextChanged
        divErro_BasicoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodTransportador_Aereo.Text = "" Then
            txtCodTransportador_Aereo.Text = 0
        End If
        If txtNomeTransportador_Aereo.Text = "" Then
            txtNomeTransportador_Aereo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_TRANSPORTADOR =  1  and  (NM_RAZAO like '%" & txtNomeTransportador_Aereo.Text & "%' or ID_PARCEIRO =  " & txtCodTransportador_Aereo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsTransportador_Aereo.SelectCommand = Sql
            dsTransportador_Aereo.DataBind()
            ddlTransportador_BasicoAereo.DataBind()
        Else
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "Parceiro não encontrado!"
        End If
        txtNomeTransportador_Aereo.Text = txtNomeTransportador_Aereo.Text.Replace("NULL", "")
    End Sub

    Private Sub txtNomeAgente_Aereo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeAgente_Aereo.TextChanged
        divErro_BasicoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodAgente_Aereo.Text = "" Then
            txtCodAgente_Aereo.Text = 0
        End If
        If txtNomeAgente_Aereo.Text = "" Then
            txtNomeAgente_Aereo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE FL_AGENTE_INTERNACIONAL = 1 and (NM_RAZAO like '%" & txtNomeAgente_Aereo.Text & "%' or ID_PARCEIRO =  " & txtCodAgente_Aereo.Text & ") union SELECT  0,'',' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsAgente_Aereo.SelectCommand = Sql
            dsAgente_Aereo.DataBind()
            ddlAgente_BasicoAereo.DataBind()
        Else
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "Parceiro não encontrado!"
        End If
        txtNomeAgente_Aereo.Text = txtNomeAgente_Aereo.Text.Replace("NULL", "")

    End Sub

    Function VerificaDiferencaCotacao(ID As Integer, Tipo As String, ID_COTACAO As Integer, Via As String) As Boolean

        If Tipo = "TAXA" Then
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim dsBL As DataSet = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF FROM TB_BL_TAXA WHERE CD_ORIGEM_INF = 'COTA' AND ID_ITEM_DESPESA <> 14 AND ID_BL_TAXA = " & ID)
            If dsBL.Tables(0).Rows.Count > 0 Then
                Dim dsCotacao As DataSet
                Dim valor As String
                If dsBL.Tables(0).Rows(0).Item("CD_PR") = "P" Then
                    valor = dsBL.Tables(0).Rows(0).Item("VL_TAXA").ToString.Replace(",", ".")

                    dsCotacao = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR FROM TB_COTACAO_TAXA WHERE ID_ITEM_DESPESA = " & dsBL.Tables(0).Rows(0).Item("ID_ITEM_DESPESA") & " AND VL_TAXA_COMPRA = " & valor & " AND ID_COTACAO = " & ID_COTACAO)

                    If Via = "M" Then

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA").ToString <> txtValorCompra_TaxaMaritimo.Text Then
                            Return True
                        Else
                            Return False
                        End If

                    ElseIf Via = "A" Then

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA") <> txtValorCompra_TaxaAereo.Text Then
                            Return True
                        Else
                            Return False
                        End If

                    End If


                ElseIf dsBL.Tables(0).Rows(0).Item("CD_PR") = "R" Then

                    valor = dsBL.Tables(0).Rows(0).Item("VL_TAXA").ToString.Replace(",", ".")

                    dsCotacao = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_VENDA,VL_TAXA_VENDA_CALCULADO,VL_TAXA_VENDA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR FROM TB_COTACAO_TAXA WHERE ID_ITEM_DESPESA = " & dsBL.Tables(0).Rows(0).Item("ID_ITEM_DESPESA") & " AND VL_TAXA_VENDA = " & valor & " AND ID_COTACAO = " & ID_COTACAO)

                    If Via = "M" Then

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA").ToString <> txtValorVenda_TaxaMaritimo.Text Then
                            Return True
                        Else
                            Return False
                        End If

                    ElseIf Via = "A" Then

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA") <> txtValorVenda_TaxaAereo.Text Then
                            Return True
                        Else
                            Return False
                        End If

                    End If

                End If



            Else
                Return False
            End If




        ElseIf Tipo = "CARGA" Then

        End If

    End Function

    Sub GridTaxaMaritimoCompras()
        For Each linha As GridViewRow In dgvTaxaMaritimoCompras.Rows
            Dim ORIGEM As String = CType(linha.FindControl("lblORIGEM"), Label).Text
            Dim ID_BL_TAXA As String = CType(linha.FindControl("lblID_BL_TAXA"), Label).Text
            Dim btnVisualizar As LinkButton = CType(linha.FindControl("btnVisualizar"), LinkButton)
            Dim btnDuplicar As LinkButton = CType(linha.FindControl("btnDuplicar"), LinkButton)
            Dim btnExcluir As LinkButton = CType(linha.FindControl("btnExcluir"), LinkButton)
            Dim Ativa As Label = CType(linha.FindControl("lblAtiva"), Label)
            Dim TemHistorico As Label = CType(linha.FindControl("lblTemHistorico"), Label)

            Dim ImageButton As ImageButton = CType(linha.FindControl("ImageButton1"), ImageButton)

            If ORIGEM = "COTAÇÃO" Then
                btnExcluir.Visible = False
            ElseIf ID_BL_TAXA = "0" Then
                btnExcluir.Visible = False
                btnVisualizar.Visible = False
                btnDuplicar.Visible = False
                dgvTaxaMaritimoCompras.Rows(linha.RowIndex).CssClass = "compra"
            Else
                btnExcluir.Visible = True

            End If

            If TemHistorico.Text = "0" Then
                ImageButton.Visible = False
            End If

            If Ativa.Text = "NÃO" Then
                dgvTaxaMaritimoCompras.Rows(linha.RowIndex).CssClass = "inativa"
            End If

        Next


    End Sub
    Private Sub dgvTaxaMaritimoCompras_Load(sender As Object, e As EventArgs) Handles dgvTaxaMaritimoCompras.Load
        GridTaxaMaritimoCompras()

        If txtID_BasicoMaritimo.Text <> "" Then
            DiferencaTaxas(txtID_BasicoMaritimo.Text, "M")
        End If

    End Sub

    Sub GridTaxaMaritimoVendas()
        For Each linha As GridViewRow In dgvTaxaMaritimoVendas.Rows
            Dim ORIGEM As String = CType(linha.FindControl("lblORIGEM"), Label).Text
            Dim ID_BL_TAXA As String = CType(linha.FindControl("lblID_BL_TAXA"), Label).Text
            Dim btnExcluir As LinkButton = CType(linha.FindControl("btnExcluir"), LinkButton)
            Dim btnVisualizar As LinkButton = CType(linha.FindControl("btnVisualizar"), LinkButton)
            Dim btnDuplicar As LinkButton = CType(linha.FindControl("btnDuplicar"), LinkButton)
            Dim Ativa As Label = CType(linha.FindControl("lblAtiva"), Label)

            If ORIGEM = "COTAÇÃO" Then
                btnExcluir.Visible = False
            ElseIf ID_BL_TAXA = "0" Then
                btnExcluir.Visible = False
                btnVisualizar.Visible = False
                btnDuplicar.Visible = False
                dgvTaxaMaritimoVendas.Rows(linha.RowIndex).CssClass = "venda"
            Else
                btnExcluir.Visible = True
            End If

            If Ativa.Text = "NÃO" Then
                dgvTaxaMaritimoVendas.Rows(linha.RowIndex).CssClass = "inativa"
            End If
        Next

    End Sub


    Private Sub dgvTaxaMaritimoVendas_Load(sender As Object, e As EventArgs) Handles dgvTaxaMaritimoVendas.Load
        GridTaxaMaritimoVendas()

        If txtID_BasicoMaritimo.Text <> "" Then
            DiferencaTaxas(txtID_BasicoMaritimo.Text, "M")
        End If

    End Sub
    Sub GridTaxaAereoCompras()
        For Each linha As GridViewRow In dgvTaxaAereoCompras.Rows
            Dim ORIGEM As String = CType(linha.FindControl("lblORIGEM"), Label).Text
            Dim ID_BL_TAXA As String = CType(linha.FindControl("lblID_BL_TAXA"), Label).Text
            Dim btnVisualizar As LinkButton = CType(linha.FindControl("btnVisualizar"), LinkButton)
            Dim btnDuplicar As LinkButton = CType(linha.FindControl("btnDuplicar"), LinkButton)
            Dim btnExcluir As LinkButton = CType(linha.FindControl("btnExcluir"), LinkButton)
            Dim TemHistorico As Label = CType(linha.FindControl("lblTemHistorico"), Label)
            Dim Ativa As Label = CType(linha.FindControl("lblAtiva"), Label)
            Dim ImageButton As ImageButton = CType(linha.FindControl("ImageButton1"), ImageButton)

            If ORIGEM = "COTAÇÃO" Then
                btnExcluir.Visible = False
            ElseIf ID_BL_TAXA = "0" Then
                btnExcluir.Visible = False
                btnVisualizar.Visible = False
                btnDuplicar.Visible = False
                dgvTaxaAereoCompras.Rows(linha.RowIndex).CssClass = "compra"
            Else
                btnExcluir.Visible = True

            End If

            If TemHistorico.Text = "0" Then
                ImageButton.Visible = False
            End If

            If Ativa.Text = "NÃO" Then
                dgvTaxaAereoCompras.Rows(linha.RowIndex).CssClass = "inativa"
            End If


        Next


    End Sub
    Private Sub dgvTaxaAereoCompras_Load(sender As Object, e As EventArgs) Handles dgvTaxaAereoCompras.Load
        GridTaxaAereoCompras()

        If txtID_BasicoAereo.Text <> "" Then
            DiferencaTaxas(txtID_BasicoAereo.Text, "A")
        End If
    End Sub

    Sub GridTaxaAereoVendas()
        For Each linha As GridViewRow In dgvTaxaAereoVendas.Rows
            Dim ORIGEM As String = CType(linha.FindControl("lblORIGEM"), Label).Text
            Dim ID_BL_TAXA As String = CType(linha.FindControl("lblID_BL_TAXA"), Label).Text
            Dim btnVisualizar As LinkButton = CType(linha.FindControl("btnVisualizar"), LinkButton)
            Dim btnDuplicar As LinkButton = CType(linha.FindControl("btnDuplicar"), LinkButton)
            Dim btnExcluir As LinkButton = CType(linha.FindControl("btnExcluir"), LinkButton)

            If ORIGEM = "COTAÇÃO" Then
                btnExcluir.Visible = False
            ElseIf ID_BL_TAXA = "0" Then
                btnExcluir.Visible = False
                btnVisualizar.Visible = False
                btnDuplicar.Visible = False
                dgvTaxaAereoVendas.Rows(linha.RowIndex).CssClass = "venda"
            Else
                btnExcluir.Visible = True

            End If
        Next

    End Sub
    Private Sub dgvTaxaAereoVendas_Load(sender As Object, e As EventArgs) Handles dgvTaxaAereoVendas.Load
        GridTaxaAereoVendas()

        If txtID_BasicoAereo.Text <> "" Then
            DiferencaTaxas(txtID_BasicoAereo.Text, "A")
        End If
    End Sub

    Sub DiferencaTaxas(ID_BL As String, TIPO As String)
        Dim Con As New Conexao_sql
        Dim tabela As String = ""

        Con.Conectar()
        Dim Sql As String = "SELECT B.NM_MOEDA, SUM (
CASE WHEN CD_PR = 'P' THEN  - VL_TAXA ELSE VL_TAXA END )VL_TAXA_TOTAL,
SUM (
CASE WHEN CD_PR = 'P' THEN  - VL_TAXA_CALCULADO ELSE VL_TAXA_CALCULADO END )VL_TAXA_CALCULADO_TOTAL
FROM TB_BL_TAXA A
INNER JOIN TB_MOEDA B ON A.ID_MOEDA= B.ID_MOEDA
WHERE ID_BL = " & ID_BL & " GROUP BY B.NM_MOEDA "
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            tabela = "<table style='color:blue;font-size:12px;'>"

            For Each linha As DataRow In ds.Tables(0).Rows
                tabela &= "<tr>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("NM_MOEDA") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>DIFERENÇA VALOR TAXA:<strong> " & linha("VL_TAXA_TOTAL") & "</strong></td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>DIFERENÇA VALOR TAXA CALCULADO:<strong> " & linha("VL_TAXA_CALCULADO_TOTAL") & "</strong></td>"
                tabela &= "</tr>"
            Next

            tabela &= "</table>"
        Else

        End If

        If TIPO = "M" Then
            lblDiferencaMaritimo.Text = tabela
            lblDiferencaAereo.Text = ""
        ElseIf TIPO = "A" Then
            lblDiferencaAereo.Text = tabela
            lblDiferencaMaritimo.Text = ""
        End If

    End Sub
    Private Sub txtNomeTranspRodoviario_BasicoMaritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeTranspRodoviario_BasicoMaritimo.TextChanged
        divErro_BasicoMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodTranspRodoviario_Maritimo.Text = "" Then
            txtCodTranspRodoviario_Maritimo.Text = 0
        End If
        If txtNomeTranspRodoviario_BasicoMaritimo.Text = "" Then
            txtNomeTranspRodoviario_BasicoMaritimo.Text = "NULL"
        End If

        Dim Sql As String = "Select ID_PARCEIRO,NM_RAZAO,Case When TP_PESSOA = 1 Then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_RODOVIARIO =1 and ((NM_RAZAO like '%" & txtNomeTranspRodoviario_BasicoMaritimo.Text & "%' AND FL_ATIVO = 1) or (ID_PARCEIRO =  " & txtCodTranspRodoviario_Maritimo.Text & ")) union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsTranspRodoviario_Maritimo.SelectCommand = Sql
            dsTranspRodoviario_Maritimo.DataBind()
            ddlTranspRodoviario_BasicoMaritimo.DataBind()
        Else
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "Parceiro não encontrado!"
        End If
        txtNomeTranspRodoviario_BasicoMaritimo.Text = txtNomeTranspRodoviario_BasicoMaritimo.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeTranspRodoviario_BasicoAereo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeTranspRodoviario_BasicoAereo.TextChanged
        divErro_BasicoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodTranspRodoviario_Aereo.Text = "" Then
            txtCodTranspRodoviario_Aereo.Text = 0
        End If
        If txtNomeTranspRodoviario_BasicoAereo.Text = "" Then
            txtNomeTranspRodoviario_BasicoAereo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_RODOVIARIO =1 and ((NM_RAZAO like '%" & txtNomeTranspRodoviario_BasicoAereo.Text & "%' AND FL_ATIVO = 1) or (ID_PARCEIRO =  " & txtCodTranspRodoviario_Aereo.Text & ")) union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsTranspRodoviario_Aereo.SelectCommand = Sql
            dsTranspRodoviario_Aereo.DataBind()
            ddlTranspRodoviario_BasicoAereo.DataBind()
        Else
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "Parceiro não encontrado!"
        End If
        txtNomeTranspRodoviario_BasicoAereo.Text = txtNomeTranspRodoviario_BasicoAereo.Text.Replace("NULL", "")

    End Sub

    Private Sub ddlBaseCalculo_TaxaAereo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBaseCalculo_TaxaAereo.SelectedIndexChanged
        If ddlBaseCalculo_TaxaAereo.SelectedValue = 38 Or ddlBaseCalculo_TaxaAereo.SelectedValue = 40 Or ddlBaseCalculo_TaxaAereo.SelectedValue = 41 Then
            txtQtdBaseCalculo_TaxaAereo.Enabled = True
        Else
            txtQtdBaseCalculo_TaxaAereo.Enabled = False
            txtQtdBaseCalculo_TaxaAereo.Text = 0
        End If
    End Sub

    Private Sub ddlBaseCalculo_TaxaMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBaseCalculo_TaxaMaritimo.SelectedIndexChanged
        If ddlBaseCalculo_TaxaMaritimo.SelectedValue = 38 Or ddlBaseCalculo_TaxaMaritimo.SelectedValue = 40 Or ddlBaseCalculo_TaxaMaritimo.SelectedValue = 41 Then
            txtQtdBaseCalculo_TaxaMaritimo.Enabled = True
        Else
            txtQtdBaseCalculo_TaxaMaritimo.Text = 0
            txtQtdBaseCalculo_TaxaMaritimo.Enabled = False
        End If
    End Sub

    Function ArredondarPesoTaxado(ID_BL As String, Optional PESO_TAXADO As Decimal = 0) As Decimal

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim M3 As Decimal = 0
        Dim PESO_BRUTO As Decimal = 0

        If txtPesoVolumetrico_CargaAereo.Text <> "" Then
            M3 = txtPesoVolumetrico_CargaAereo.Text
        End If

        If txtPesoBruto_CargaAereo.Text <> "" Then
            PESO_BRUTO = txtPesoBruto_CargaAereo.Text
        End If


        If PESO_TAXADO = 0 Then
            Dim ds As DataSet = Con.ExecutarQuery("Select A.ID_TIPO_ESTUFAGEM, A.ID_SERVICO from TB_BL A Where A.ID_BL = " & ID_BL)
            If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                'AEREO


                If M3 >= PESO_BRUTO Then
                    PESO_TAXADO = M3
                Else
                    PESO_TAXADO = PESO_BRUTO
                End If

            End If
        End If

        If PESO_TAXADO <> 0 Then
            Dim PrimeiraCasa As String = PESO_TAXADO.ToString
            If PrimeiraCasa.IndexOf(",") > 0 Then
                PrimeiraCasa = PrimeiraCasa.Substring(PrimeiraCasa.IndexOf(","), 2)
                PrimeiraCasa = PrimeiraCasa.Replace(",", "")
                If PrimeiraCasa = 5 Then
                    Dim SegundaCasa As String = PESO_TAXADO.ToString
                    Dim tamanho = SegundaCasa.Substring(SegundaCasa.IndexOf("," & PrimeiraCasa))
                    If tamanho.Length > 2 Then
                        SegundaCasa = SegundaCasa.Substring(SegundaCasa.IndexOf("," & PrimeiraCasa), 3)
                        SegundaCasa = SegundaCasa.Replace("," & PrimeiraCasa, "")
                        If SegundaCasa > 0 Then
                            PESO_TAXADO = Math.Ceiling(PESO_TAXADO)
                        End If
                    End If
                ElseIf PrimeiraCasa = 0 Then

                    Dim SegundaCasa As String = PESO_TAXADO.ToString
                    Dim tamanho = SegundaCasa.Substring(SegundaCasa.IndexOf("," & PrimeiraCasa))
                    If tamanho.Length > 2 Then
                        SegundaCasa = SegundaCasa.Substring(SegundaCasa.IndexOf("," & PrimeiraCasa), 3)
                        SegundaCasa = SegundaCasa.Replace("," & PrimeiraCasa, "")
                        If SegundaCasa > 0 Then
                            Dim PESO_TAXADO_INTEIRO As Decimal = Math.Truncate(PESO_TAXADO)
                            PESO_TAXADO = PESO_TAXADO_INTEIRO + 0.5
                        Else
                            Dim TerceiraCasa As String = PESO_TAXADO.ToString
                            tamanho = TerceiraCasa.Substring(TerceiraCasa.IndexOf("," & SegundaCasa))
                            If tamanho.Length > 3 Then
                                TerceiraCasa = TerceiraCasa.Substring(TerceiraCasa.IndexOf("," & SegundaCasa), 4)
                                TerceiraCasa = TerceiraCasa.Replace("," & PrimeiraCasa & SegundaCasa, "")
                                If TerceiraCasa > 0 Then
                                    Dim PESO_TAXADO_INTEIRO As Decimal = Math.Truncate(PESO_TAXADO)
                                    PESO_TAXADO = PESO_TAXADO_INTEIRO + 0.5
                                End If
                            End If
                        End If
                    End If

                ElseIf PrimeiraCasa > 5 Then
                    PESO_TAXADO = Math.Ceiling(PESO_TAXADO)

                ElseIf PrimeiraCasa < 5 Then

                    Dim PESO_TAXADO_INTEIRO As Decimal = Math.Truncate(PESO_TAXADO)
                    PESO_TAXADO = PESO_TAXADO_INTEIRO + 0.5
                End If

            End If
        End If
        Return PESO_TAXADO.ToString("0.000")

    End Function
    Sub AdicionarMedidasAereo()
        divErro_CargaAereo2.Visible = False
        divSuccess_CargaAereo2.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        sumMedidasAereo(txtID_CargaAereo.Text)

        If txtID_BasicoAereo.Text = "" Then
            lblErro_CargaAereo2.Text = "Necessário inserir dados basicos do processo."
            divErro_CargaAereo2.Visible = True

        ElseIf txtID_CargaAereo.Text = "" Then
            lblErro_CargaAereo2.Text = "Necessário inserir carga."
            divErro_CargaAereo2.Visible = True

        ElseIf txtQtdCaixasAereo.Text = "" Or txtComprimentoMercadoriaAereo.Text = "" Or txtAlturaMercadoriaAereo.Text = "" Or txtLarguraMercadoriaAereo.Text = "" Then
            lblErro_CargaAereo2.Text = "Preencha todos os campos de dimensoes!"
            divErro_CargaAereo2.Visible = True

        Else

            txtAlturaMercadoriaAereo.Text = txtAlturaMercadoriaAereo.Text.Replace(".", "")
            txtAlturaMercadoriaAereo.Text = txtAlturaMercadoriaAereo.Text.Replace(",", ".")

            txtComprimentoMercadoriaAereo.Text = txtComprimentoMercadoriaAereo.Text.Replace(".", "")
            txtComprimentoMercadoriaAereo.Text = txtComprimentoMercadoriaAereo.Text.Replace(",", ".")

            txtLarguraMercadoriaAereo.Text = txtLarguraMercadoriaAereo.Text.Replace(".", "")
            txtLarguraMercadoriaAereo.Text = txtLarguraMercadoriaAereo.Text.Replace(",", ".")

            ds = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL_DIMENSAO (ID_BL,ID_CARGA_BL, QTD_CAIXA, VL_LARGURA, VL_ALTURA, VL_COMPRIMENTO) VALUES (" & txtID_BasicoAereo.Text & "," & txtID_CargaAereo.Text & "," & txtQtdCaixasAereo.Text & "," & txtLarguraMercadoriaAereo.Text & "," & txtAlturaMercadoriaAereo.Text & "," & txtComprimentoMercadoriaAereo.Text & ")")
            CalculaCLA()
            sumMedidasAereo(txtID_CargaAereo.Text)

            txtPesoTaxado_CargaAereo.Text = ArredondarPesoTaxado(txtID_BasicoAereo.Text)
            dgvMedidasAereo.DataBind()
            txtAlturaMercadoriaAereo.Text = ""
            txtLarguraMercadoriaAereo.Text = ""
            txtComprimentoMercadoriaAereo.Text = ""
            txtQtdCaixasAereo.Text = ""
        End If


    End Sub
    Sub CalculaCLA()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim LCA As Decimal
        Dim PESO_BRUTO As Decimal
        Dim PESO_TAXADO As Decimal
        Dim VL_M3 As Decimal
        Dim ds As DataSet = Con.ExecutarQuery("SELECT isnull(VL_PESO_TAXADO,0)VL_PESO_TAXADO, isnull(VL_M3,0)VL_M3, isnull(A.VL_PESO_BRUTO,0)VL_PESO_BRUTO,
(isnull(D.QTD_CAIXA,0) * isnull(D.VL_COMPRIMENTO,0) * isnull(D.VL_ALTURA,0) * isnull(D.VL_LARGURA,0))/5988 AS LCA
from TB_BL A 
left join TB_CARGA_BL_DIMENSAO D ON D.ID_BL = A.ID_BL
Where A.ID_BL = " & txtID_BasicoAereo.Text)
        PESO_BRUTO = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
        VL_M3 = ds.Tables(0).Rows(0).Item("VL_M3")

        For Each linha As DataRow In ds.Tables(0).Rows
            LCA = LCA + linha.Item("LCA")
        Next

        If LCA = 0 Then
            LCA = VL_M3
        End If

        If LCA >= PESO_BRUTO Then
            PESO_TAXADO = LCA
        Else
            PESO_TAXADO = PESO_BRUTO
        End If



        txtPesoTaxado_CargaAereo.Text = FormatNumber(PESO_TAXADO, 3)
        txtPesoVolumetrico_CargaAereo.Text = FormatNumber(LCA, 3)
        Dim LCAFinal As String = LCA.ToString
        LCAFinal = LCAFinal.ToString.Replace(".", "")
        LCAFinal = LCAFinal.ToString.Replace(",", ".")

        Dim PESO_TAXADO_Final As String = PESO_TAXADO.ToString
        PESO_TAXADO_Final = PESO_TAXADO_Final.ToString.Replace(".", "")
        PESO_TAXADO_Final = PESO_TAXADO_Final.ToString.Replace(",", ".")
        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_M3 = " & LCAFinal & " WHERE ID_BL = " & txtID_BasicoAereo.Text)
        Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 = " & LCAFinal & " WHERE ID_BL = " & txtID_BasicoAereo.Text)
        Con.ExecutarQuery("UPDATE TB_BL SET VL_PESO_TAXADO = " & PESO_TAXADO_Final & " WHERE ID_BL = " & txtID_BasicoAereo.Text)
    End Sub

    Sub sumMedidasAereo(ID_CargaAereo As String)
        Dim Con As New Conexao_sql
        Con.Conectar()
        If ID_CargaAereo <> "" Then
            Con.ExecutarQuery("UPDATE TB_CARGA_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QTD_CAIXA,0))QTD_CAIXA FROM TB_CARGA_BL_DIMENSAO WHERE ID_CARGA_BL =  " & ID_CargaAereo & ") WHERE ID_CARGA_BL =  " & ID_CargaAereo)

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(QT_MERCADORIA,0)QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_CARGA_BL =  " & ID_CargaAereo)
            If ds.Tables(0).Rows.Count > 0 Then
                txtQtdVolume_CargaAereo.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
            End If

            Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_LARGURA =
(SELECT SUM(ISNULL(VL_LARGURA,0))VL_LARGURA FROM TB_CARGA_BL_DIMENSAO WHERE ID_CARGA_BL =  " & ID_CargaAereo & ") WHERE ID_CARGA_BL =  " & ID_CargaAereo)

            Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_ALTURA =
(SELECT SUM(ISNULL(VL_ALTURA,0))VL_ALTURA FROM TB_CARGA_BL_DIMENSAO WHERE ID_CARGA_BL =  " & ID_CargaAereo & ") WHERE ID_CARGA_BL =  " & ID_CargaAereo)

            Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_COMPRIMENTO =
(SELECT SUM(ISNULL(VL_COMPRIMENTO,0))VL_COMPRIMENTO FROM TB_CARGA_BL_DIMENSAO WHERE ID_CARGA_BL =  " & ID_CargaAereo & ") WHERE ID_CARGA_BL =  " & ID_CargaAereo)
        End If
    End Sub

    Private Sub btnAdicionarMedidasAereo_Click(sender As Object, e As ImageClickEventArgs) Handles btnAdicionarMedidasAereo.Click
        AdicionarMedidasAereo()
    End Sub

    Private Sub dgvMedidasAereo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvMedidasAereo.RowCommand
        divErro_CargaAereo2.Visible = False
        divSuccess_CargaAereo2.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("DELETE From TB_CARGA_BL_DIMENSAO Where ID = " & ID)
            CalculaCLA()
            sumMedidasAereo(txtID_CargaAereo.Text)
            lblSuccess_CargaAereo2.Text = "Registro CLA deletado!"
            divSuccess_CargaAereo2.Visible = True
            dgvMedidasAereo.DataBind()
            txtPesoTaxado_CargaAereo.Text = ArredondarPesoTaxado(txtID_BasicoAereo.Text)
        End If

    End Sub

    Sub DocConferido(ID_BL As String, TIPO As String)
        Dim Con As New Conexao_sql
        Con.Conectar()
        If ID_BL <> "" Then
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(FL_DOC_CONFERIDO, 0)FL_DOC_CONFERIDO FROM FN_DOC_CONFERIDO(" & ID_BL & ")")

            If TIPO = "A" Then
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO") = False And ckDocConferidosAereo.Checked = True Then
                        Con.ExecutarQuery("INSERT INTO TB_BL_HIST_DOC (ID_BL,FL_DOC_CONFERIDO,ID_USUARIO,DATA) VALUES (" & ID_BL & ", '" & ckDocConferidosAereo.Checked & "'," & Session("ID_USUARIO") & ",GETDATE()) ")
                    ElseIf ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO") = True And ckDocConferidosAereo.Checked = False Then
                        Con.ExecutarQuery("INSERT INTO TB_BL_HIST_DOC (ID_BL,FL_DOC_CONFERIDO,ID_USUARIO,DATA) VALUES (" & ID_BL & ", '" & ckDocConferidosAereo.Checked & "'," & Session("ID_USUARIO") & ",GETDATE()) ")
                    End If
                Else
                    If ckDocConferidosAereo.Checked = True Then
                        Con.ExecutarQuery("INSERT INTO TB_BL_HIST_DOC (ID_BL,FL_DOC_CONFERIDO,ID_USUARIO,DATA) VALUES (" & ID_BL & ", '" & ckDocConferidosAereo.Checked & "'," & Session("ID_USUARIO") & ",GETDATE()) ")
                    End If
                End If
            ElseIf TIPO = "M" Then
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO") = False And ckDocConferidosMaritimo.Checked = True Then
                        Con.ExecutarQuery("INSERT INTO TB_BL_HIST_DOC (ID_BL,FL_DOC_CONFERIDO,ID_USUARIO,DATA) VALUES (" & ID_BL & ", '" & ckDocConferidosMaritimo.Checked & "'," & Session("ID_USUARIO") & ",GETDATE()) ")
                    ElseIf ds.Tables(0).Rows(0).Item("FL_DOC_CONFERIDO") = True And ckDocConferidosMaritimo.Checked = False Then
                        Con.ExecutarQuery("INSERT INTO TB_BL_HIST_DOC (ID_BL,FL_DOC_CONFERIDO,ID_USUARIO,DATA) VALUES (" & ID_BL & ", '" & ckDocConferidosMaritimo.Checked & "'," & Session("ID_USUARIO") & ",GETDATE()) ")
                    End If
                Else
                    If ckDocConferidosMaritimo.Checked = True Then
                        Con.ExecutarQuery("INSERT INTO TB_BL_HIST_DOC (ID_BL,FL_DOC_CONFERIDO,ID_USUARIO,DATA) VALUES (" & ID_BL & ", '" & ckDocConferidosMaritimo.Checked & "'," & Session("ID_USUARIO") & ",GETDATE()) ")
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub btnUploadAereo_Click(sender As Object, e As EventArgs) Handles btnUploadAereo.Click
        divErroUploadAereo.Visible = False
        divSuccessUploadAereo.Visible = False

        If txtID_BasicoAereo.Text = "" Then
            lblErroUploadAereo.Text = "Necessário inserir processo!"
            divErroUploadAereo.Visible = True

        ElseIf ddlTipoArquivoAereo.selectedvalue = 0 Then
            lblErroUploadAereo.Text = "Necessário selecionar um tipo de arquivo!"
            divErroUploadAereo.Visible = True

        ElseIf FileUploadAereo.HasFile Then

            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim nomeArquivo As String = Path.GetFileName(FileUploadAereo.PostedFile.FileName)
            Dim ds As DataSet = Con.ExecutarQuery(" SELECT COUNT(*)QTD FROM TB_UPLOADS WHERE ID_TIPO_ARQUIVO = " & ddlTipoArquivoAereo.SelectedValue & " AND NM_ARQUIVO ='" & nomeArquivo & "' AND ((ID_COTACAO=" & txtID_CotacaoAereo.Text & ") OR ( ID_BL = " & txtID_BasicoAereo.Text & ")) ")
            If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                lblErroUploadAereo.Text = "Arquivo já existe!"
                divErroUploadAereo.Visible = True
            Else

                Dim diretorio_arquivos As String = ""

                If txtID_CotacaoAereo.Text <> 0 Then
                    diretorio_arquivos = System.Configuration.ConfigurationSettings.AppSettings("CaminhoUploads") & "\Uploads\Cotacao_" & txtID_CotacaoAereo.Text
                Else
                    diretorio_arquivos = System.Configuration.ConfigurationSettings.AppSettings("CaminhoUploads") & "\Uploads\BL_" & txtID_BasicoAereo.Text
                End If

                If Not Directory.Exists(diretorio_arquivos) Then
                    System.IO.Directory.CreateDirectory(diretorio_arquivos)
                End If



                FileUploadAereo.PostedFile.SaveAs(diretorio_arquivos & "\" & nomeArquivo)


                Con.ExecutarQuery("INSERT INTO TB_UPLOADS (NM_ARQUIVO,ID_TIPO_ARQUIVO,ID_USUARIO,DT_UPLOAD,FL_ATIVO_CLIENTES,ID_COTACAO,ID_BL,CAMINHO_ARQUIVO) VALUES ('" & nomeArquivo & "'," & ddlTipoArquivoAereo.SelectedValue & "," & Session("ID_USUARIO") & ", getdate(),0," & txtID_CotacaoAereo.Text & "," & txtID_BasicoAereo.Text & ",'" & diretorio_arquivos & "/" & nomeArquivo & "' )")

                divSuccessUploadAereo.Visible = True
                dgvArquivosAereo.DataBind()
            End If
        Else

            lblErroUploadAereo.Text = "Por favor, selecione um arquivo a enviar."
            divErroUploadAereo.Visible = True

        End If

        ddlTipoArquivoAereo.SelectedValue = 0
        txtUPAereo.Text = 1

    End Sub

    Private Sub btnUploadMaritimo_Click(sender As Object, e As EventArgs) Handles btnUploadMaritimo.Click
        divErroUploadMaritimo.Visible = False
        divSuccessUploadMaritimo.Visible = False

        If txtID_BasicoMaritimo.Text = "" Then
            lblErroUploadMaritimo.Text = "Necessário inserir processo!"
            divErroUploadMaritimo.Visible = True

        ElseIf ddlTipoArquivoMaritimo.selectedvalue = 0 Then
            lblErroUploadMaritimo.Text = "Necessário selecionar um tipo de arquivo!"
            divErroUploadMaritimo.Visible = True

        ElseIf FileUploadMaritimo.HasFile Then

            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim nomeArquivo As String = Path.GetFileName(FileUploadMaritimo.PostedFile.FileName)
            Dim ds As DataSet = Con.ExecutarQuery(" SELECT COUNT(*)QTD FROM TB_UPLOADS WHERE ID_TIPO_ARQUIVO = " & ddlTipoArquivoMaritimo.SelectedValue & " AND NM_ARQUIVO ='" & nomeArquivo & "' AND ((ID_COTACAO=" & txtID_CotacaoMaritimo.Text & ") OR ( ID_BL = " & txtID_BasicoMaritimo.Text & ")) ")
            If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                lblErroUploadMaritimo.Text = "Arquivo já existe!"
                divErroUploadMaritimo.Visible = True
            Else

                Dim diretorio_arquivos As String = ""

                If txtID_CotacaoMaritimo.Text <> 0 Then
                    diretorio_arquivos = System.Configuration.ConfigurationSettings.AppSettings("CaminhoUploads") & "\Uploads\Cotacao_" & txtID_CotacaoMaritimo.Text
                Else
                    diretorio_arquivos = System.Configuration.ConfigurationSettings.AppSettings("CaminhoUploads") & "\Uploads\BL_" & txtID_BasicoMaritimo.Text
                End If

                If Not Directory.Exists(diretorio_arquivos) Then
                    System.IO.Directory.CreateDirectory(diretorio_arquivos)
                End If



                FileUploadMaritimo.PostedFile.SaveAs(diretorio_arquivos & "\" & nomeArquivo)


                Con.ExecutarQuery("INSERT INTO TB_UPLOADS (NM_ARQUIVO,ID_TIPO_ARQUIVO,ID_USUARIO,DT_UPLOAD,FL_ATIVO_CLIENTES,ID_COTACAO,ID_BL,CAMINHO_ARQUIVO) VALUES ('" & nomeArquivo & "'," & ddlTipoArquivoMaritimo.SelectedValue & "," & Session("ID_USUARIO") & ", getdate(), 0," & txtID_CotacaoMaritimo.Text & "," & txtID_BasicoMaritimo.Text & ",'" & diretorio_arquivos & "/" & nomeArquivo & "' )")

                divSuccessUploadMaritimo.Visible = True
                dgvArquivosMaritimo.DataBind()
            End If

        Else

            lblErroUploadMaritimo.Text = "Por favor, selecione um arquivo a enviar."
            divErroUploadMaritimo.Visible = True

        End If
        ddlTipoArquivoMaritimo.SelectedValue = 0
        txtUPMaritimo.Text = 1
    End Sub

    Private Sub dgvArquivosAereo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvArquivosAereo.RowCommand
        divSuccessUploadAereo.Visible = False

        If e.CommandName = "Excluir" Then

            Try
                Dim Con As New Conexao_sql
                Con.Conectar()
                Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroUploadAereo.Text = "Usuário não tem permissão para realizar exclusões"
                    divErroUploadAereo.Visible = True
                Else

                    Dim CommandArgument As String = e.CommandArgument

                    Dim ID_ARQUIVO As String = CommandArgument.Substring(0, CommandArgument.IndexOf("|"))

                    Dim CAMINHO_ARQUIVO As String = CommandArgument.Substring(CommandArgument.IndexOf("|"))
                    CAMINHO_ARQUIVO = CAMINHO_ARQUIVO.Replace("|", "")

                    File.Delete(CAMINHO_ARQUIVO)

                    Con.ExecutarQuery("DELETE FROM TB_UPLOADS WHERE ID_ARQUIVO = " & ID_ARQUIVO)
                    divSuccessUploadAereo.Visible = True
                    dgvArquivosAereo.DataBind()

                End If
                Con.Fechar()

            Catch ex As Exception
                lblErroUploadAereo.Text = ex.Message
                divErroUploadAereo.Visible = True
            End Try

        ElseIf e.CommandName = "Download" Then

            Try
                Dim CAMINHO_ARQUIVO As String = e.CommandArgument
                Response.ContentType = ContentType
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(CAMINHO_ARQUIVO))
                Response.WriteFile(CAMINHO_ARQUIVO)
                Response.Flush()
                Response.Close()

            Catch ex As Exception
                lblErroUploadAereo.Text = ex.Message
                divErroUploadAereo.Visible = True
            End Try

        End If

        txtUPAereo.Text = 1
    End Sub

    Private Sub dgvArquivosMaritimo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvArquivosMaritimo.RowCommand
        divErroUploadMaritimo.Visible = False
        divSuccessUploadMaritimo.Visible = False


        If e.CommandName = "Excluir" Then

            Try
                Dim Con As New Conexao_sql
                Con.Conectar()
                Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroUploadMaritimo.Text = "Usuário não tem permissão para realizar exclusões"
                    divErroUploadMaritimo.Visible = True
                Else

                    Dim CommandArgument As String = e.CommandArgument

                    Dim ID_ARQUIVO As String = CommandArgument.Substring(0, CommandArgument.IndexOf("|"))

                    Dim CAMINHO_ARQUIVO As String = CommandArgument.Substring(CommandArgument.IndexOf("|"))
                    CAMINHO_ARQUIVO = CAMINHO_ARQUIVO.Replace("|", "")

                    File.Delete(CAMINHO_ARQUIVO)

                    Con.ExecutarQuery("DELETE FROM TB_UPLOADS WHERE ID_ARQUIVO = " & ID_ARQUIVO)
                    divSuccessUploadMaritimo.Visible = True
                    dgvArquivosMaritimo.DataBind()
                End If

            Catch ex As Exception
                lblErroUploadMaritimo.Text = ex.Message
                divErroUploadMaritimo.Visible = True
            End Try



        ElseIf e.CommandName = "Download" Then

            Try
                Dim CAMINHO_ARQUIVO As String = e.CommandArgument
                Response.ContentType = ContentType
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(CAMINHO_ARQUIVO))
                Response.WriteFile(CAMINHO_ARQUIVO)
                Response.Flush()
                Response.Close()

            Catch ex As Exception
                lblErroUploadMaritimo.Text = ex.Message
                divErroUploadMaritimo.Visible = True
            End Try

        End If

        txtUPMaritimo.Text = 1
    End Sub

    Private Sub btnLimparUploadAereo_Click(sender As Object, e As EventArgs) Handles btnLimparUploadAereo.Click
        divErroUploadAereo.Visible = False
        divSuccessUploadAereo.Visible = False
        txtUPAereo.Text = 1
        ddlTipoArquivoAereo.SelectedValue = 0
        FileUploadAereo.FileContent.Flush()
    End Sub

    Private Sub btnLimparUploadMaritimo_Click(sender As Object, e As EventArgs) Handles btnLimparUploadMaritimo.Click
        divErroUploadMaritimo.Visible = False
        divSuccessUploadMaritimo.Visible = False
        txtUPMaritimo.Text = 1
        ddlTipoArquivoMaritimo.SelectedValue = 0
        FileUploadMaritimo.FileContent.Flush()
    End Sub

    Public Sub ckAtivoClientes_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox = DirectCast(sender, CheckBox)
        Dim row = DirectCast(chk.NamingContainer, GridViewRow)
        Dim ID_ARQUIVO = DirectCast(row.FindControl("lblID_ARQUIVO"), Label).Text
        Dim Con As New Conexao_sql
        Con.Conectar()
        Con.ExecutarQuery("UPDATE TB_UPLOADS SET FL_ATIVO_CLIENTES = '" & chk.Checked & "' WHERE ID_ARQUIVO = " & ID_ARQUIVO)
        If Request.QueryString("s") = "A" Then
            divSuccessUploadAereo.Visible = True
            dgvArquivosAereo.DataBind()
            Con.Fechar()
            txtUPAereo.Text = 1
        ElseIf Request.QueryString("s") = "M" Then
            divSuccessUploadMaritimo.Visible = True
            dgvArquivosMaritimo.DataBind()
            Con.Fechar()
            txtUPMaritimo.Text = 1
        End If

    End Sub

    Public Sub ckEnvioSI_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox = DirectCast(sender, CheckBox)
        Dim row = DirectCast(chk.NamingContainer, GridViewRow)
        Dim ID_ARQUIVO = DirectCast(row.FindControl("lblID_ARQUIVO"), Label).Text
        Dim Con As New Conexao_sql
        Con.Conectar()
        Con.ExecutarQuery("UPDATE TB_UPLOADS SET FL_ENVIO_SI = '" & chk.Checked & "' WHERE ID_ARQUIVO = " & ID_ARQUIVO)
        If Request.QueryString("s") = "A" Then
            divSuccessUploadAereo.Visible = True
            dgvArquivosAereo.DataBind()
            Con.Fechar()
            txtUPAereo.Text = 1
        ElseIf Request.QueryString("s") = "M" Then
            divSuccessUploadMaritimo.Visible = True
            dgvArquivosMaritimo.DataBind()
            Con.Fechar()
            txtUPMaritimo.Text = 1
        End If

    End Sub

    Private Sub dgvArquivosMaritimo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvArquivosMaritimo.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim NM_ARQUIVO As Label = CType(e.Row.FindControl("lblNM_ARQUIVO"), Label)

            Dim lblBotaoVisualizar As Label = CType(e.Row.FindControl("lblBotaoVisualizar"), Label)

            If (NM_ARQUIVO.Text.ToLower.IndexOf(".pdf") > 1) Or (NM_ARQUIVO.Text.ToLower.IndexOf(".mp4") > 1) Or (NM_ARQUIVO.Text.ToLower.IndexOf(".jpeg") > 1) Or (NM_ARQUIVO.Text.ToLower.IndexOf(".jpg") > 1) Or (NM_ARQUIVO.Text.ToLower.IndexOf(".png") > 1) Then
                lblBotaoVisualizar.Visible = True
            Else
                lblBotaoVisualizar.Visible = False
            End If

        End If
    End Sub

    Private Sub dgvArquivosAereo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvArquivosAereo.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim NM_ARQUIVO As Label = CType(e.Row.FindControl("lblNM_ARQUIVO"), Label)

            Dim lblBotaoVisualizar As Label = CType(e.Row.FindControl("lblBotaoVisualizar"), Label)

            If (NM_ARQUIVO.Text.ToLower.IndexOf(".pdf") > 1) Or (NM_ARQUIVO.Text.ToLower.IndexOf(".mp4") > 1) Or (NM_ARQUIVO.Text.ToLower.IndexOf(".jpeg") > 1) Or (NM_ARQUIVO.Text.ToLower.IndexOf(".jpg") > 1) Or (NM_ARQUIVO.Text.ToLower.IndexOf(".png") > 1) Then
                lblBotaoVisualizar.Visible = True
            Else
                lblBotaoVisualizar.Visible = False
            End If

        End If
    End Sub

    Private Sub txtPesoBruto_CargaAereo_TextChanged(sender As Object, e As EventArgs) Handles txtPesoBruto_CargaAereo.TextChanged
        txtPesoTaxado_CargaAereo.Text = ArredondarPesoTaxado(txtID_BasicoAereo.Text)
    End Sub

    Private Sub txtPesoVolumetrico_CargaAereo_TextChanged(sender As Object, e As EventArgs) Handles txtPesoVolumetrico_CargaAereo.TextChanged
        txtPesoTaxado_CargaAereo.Text = ArredondarPesoTaxado(txtID_BasicoAereo.Text)

    End Sub

    Private Sub ddlTipoPagamento_BasicoMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoPagamento_BasicoMaritimo.SelectedIndexChanged
        Dim AtualizaStatus As New AtualizaStatusFreteAgente
        ddlStatusFreteAgente_BasicoMaritimo.SelectedValue = AtualizaStatus.AtualizaStatusFreteAgenteHouse(txtID_BasicoMaritimo.Text, ddlTipoPagamento_BasicoMaritimo.SelectedValue, ckbFreeHand_BasicoMaritimo.Checked)
    End Sub

    Private Sub ddlTipoPagamento_BasicoAereo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoPagamento_BasicoAereo.SelectedIndexChanged
        Dim AtualizaStatus As New AtualizaStatusFreteAgente
        ddlStatusFreteAgente_BasicoAereo.SelectedValue = AtualizaStatus.AtualizaStatusFreteAgenteHouse(txtID_BasicoAereo.Text, ddlTipoPagamento_BasicoAereo.SelectedValue, ckbFreeHand_BasicoAereo.Checked)
    End Sub

    Private Sub ckbFreeHand_BasicoAereo_CheckedChanged(sender As Object, e As EventArgs) Handles ckbFreeHand_BasicoAereo.CheckedChanged
        Dim AtualizaStatus As New AtualizaStatusFreteAgente
        ddlStatusFreteAgente_BasicoAereo.SelectedValue = AtualizaStatus.AtualizaStatusFreteAgenteHouse(txtID_BasicoAereo.Text, ddlTipoPagamento_BasicoAereo.SelectedValue, ckbFreeHand_BasicoAereo.Checked)
    End Sub

    Private Sub ckbFreeHand_BasicoMaritimo_CheckedChanged(sender As Object, e As EventArgs) Handles ckbFreeHand_BasicoMaritimo.CheckedChanged
        Dim AtualizaStatus As New AtualizaStatusFreteAgente
        ddlStatusFreteAgente_BasicoMaritimo.SelectedValue = AtualizaStatus.AtualizaStatusFreteAgenteHouse(txtID_BasicoMaritimo.Text, ddlTipoPagamento_BasicoMaritimo.SelectedValue, ckbFreeHand_BasicoMaritimo.Checked)
    End Sub
End Class