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
            ElseIf Request.QueryString("tipo") = "h" Then
                lblTipoModulo.Text = " HOUSE"
                btnVisualizarMBL_Aereo.Text = "Visualizar MBL"
                btnVisualizarMBL_Maritimo.Text = "Visualizar MBL"
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
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_BL,ID_SERVICO,ID_BL_MASTER,NR_BL,NR_PROCESSO,ID_PARCEIRO_TRANSPORTADOR,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_INDICADOR ,
(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_CLIENTE)NM_RAZAO_CLIENTE,
ID_PARCEIRO_IMPORTADOR, ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_PORTO_ORIGEM,ID_PORTO_DESTINO, ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_COMISSARIA,ID_PARCEIRO_AGENTE,ID_INCOTERM,FL_FREE_HAND,ID_TIPO_PAGAMENTO,ID_TIPO_CARGA,ID_TIPO_ESTUFAGEM,NR_CE,CONVERT(varchar,DT_CE, 103)DT_CE,OB_REFERENCIA_COMERCIAL,OB_REFERENCIA_AUXILIAR,NM_RESUMO_MERCADORIA,OB_CLIENTE,OB_AGENTE_INTERNACIONAL,OB_COMERCIAL,OB_OPERACIONAL_INTERNA,CD_RASTREAMENTO_HBL,CD_RASTREAMENTO_MBL,ID_PARCEIRO_ARMAZEM_DESEMBARACO,ID_PARCEIRO_RODOVIARIO,(SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)BL_MASTER,(SELECT DT_CHEGADA FROM TB_BL WHERE TB_BL.ID_BL = A.ID_BL_MASTER)DT_CHEGADA_MASTER,VL_PROFIT_DIVISAO,ID_PROFIT_DIVISAO,ISNULL((SELECT B.ID_STATUS_COTACAO FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO),0)ID_STATUS_COTACAO,
(SELECT B.OB_CLIENTE FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO)OB_CLIENTE_COTACAO,
(SELECT B.OB_OPERACIONAL FROM TB_COTACAO B WHERE B.ID_COTACAO = A.ID_COTACAO)OB_OPERACIONAL_COTACAO
FROM TB_BL A where ID_BL =" & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then

                If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
                    'AGENCIAMENTO DE EXPORTACAO MARITIMA
                    'AGENCIAMENTO DE IMPORTACAO MARITIMA

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) Then
                        txtIDMaster_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
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
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        txtCodTransportador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                        ddlTransportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
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
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_MASTER")) Then
                        txtIDMaster_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
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

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        txtCodTransportador_Aereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                        ddlTransportador_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")) Then
                        ddlTranspRod_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
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

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                        ddlTipoCarga_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")
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

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")) Then
                        txtResumoMercadoria_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")
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
                    Con.ExecutarQuery("DELETE From TB_AMR_CNTR_BL Where ID_CNTR_BL = " & ds.Tables(0).Rows(0).Item("ID_CNTR_BL") & " AND ID_BL = " & txtID_BasicoMaritimo.Text)
                End If

                lblSuccess_CargaMaritimo1.Text = "Registro deletado!"
                divSuccess_CargaMaritimo1.Visible = True
                dgvCargaMaritimo.DataBind()
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

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
                        ddlNCM_CargaMaritimo.Items.Insert(1, ds.Tables(0).Rows(0).Item("NCM"))
                        ddlNCM_CargaMaritimo.SelectedIndex = 1
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


                        Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                        lblSuccess_TaxaMaritimo1.Text = "Registro deletado!"
                        divSuccess_TaxaMaritimo1.Visible = True
                        dgvTaxaMaritimoCompras.DataBind()
                    End If
                End If

            ElseIf e.CommandName = "visualizar" Then
                Dim ID As String = e.CommandArgument

                ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO,A.CD_PR
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
                        ddlDestinatarioCob_TaxaMaritimo.Enabled = False
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                        ddlBaseCalculo_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
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

                    'If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")) Then
                    '    ' = ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")
                    'End If

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

                    Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF)  select ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,'OPER' from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                    lblSuccess_TaxaMaritimo1.Text = "Registro duplicado!"
                    divSuccess_TaxaMaritimo1.Visible = True
                    dgvTaxaMaritimoCompras.DataBind()
                End If
            End If
            Con.Fechar()
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


                        Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                        lblSuccess_TaxaMaritimo1.Text = "Registro deletado!"
                        divSuccess_TaxaMaritimo1.Visible = True
                        dgvTaxaMaritimoVendas.DataBind()
                    End If
                End If

            ElseIf e.CommandName = "visualizar" Then
                Dim ID As String = e.CommandArgument

                ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO,A.CD_PR
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
                        ddlDestinatarioCob_TaxaMaritimo.Enabled = True
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                        ddlBaseCalculo_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
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

                    'If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")) Then
                    '    ' = ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")
                    'End If

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

                    lblTipoEmpresa_Maritimo.Text = "Parceiro:"

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

                    Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF)  select ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,'OPER' from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                    lblSuccess_TaxaMaritimo1.Text = "Registro duplicado!"
                    divSuccess_TaxaMaritimo1.Visible = True
                    dgvTaxaMaritimoVendas.DataBind()
                End If
            End If
            Con.Fechar()
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


                    Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                    lblSuccess_TaxaAereo1.Text = "Registro deletado!"
                    divSuccess_TaxaAereo1.Visible = True
                    dgvTaxaAereoVendas.DataBind()
                End If
            End If


        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO,A.CD_PR
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
                    ddlDestinatarioCob_TaxaAereo.Enabled = True
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculo_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
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

                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF)  select ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,'OPER' from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                lblSuccess_TaxaAereo1.Text = "Registro duplicado!"
                divSuccess_TaxaAereo1.Visible = True
                dgvTaxaAereoVendas.DataBind()
            End If
        End If
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


                    Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                    lblSuccess_TaxaAereo1.Text = "Registro deletado!"
                    divSuccess_TaxaAereo1.Visible = True
                    dgvTaxaAereoCompras.DataBind()
                End If
            End If


        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO,A.CD_PR
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
                    ddlDestinatarioCob_TaxaAereo.Enabled = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculo_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
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

                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF)  select ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,'OPER' from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                lblSuccess_TaxaAereo1.Text = "Registro duplicado!"
                divSuccess_TaxaAereo1.Visible = True
                dgvTaxaAereoCompras.DataBind()
            End If
        End If
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
                lblSuccess_CargaAereo1.Text = "Registro deletado!"
                divSuccess_CargaAereo1.Visible = True
                dgvCargaAereo.DataBind()
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select ID_CARGA_BL,ID_TIPO_CARGA,ID_MERCADORIA,ID_NCM,(select CD_NCM +' - '+ NM_NCM from TB_NCM WHERE ID_NCM = A.ID_NCM)NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,DS_MERCADORIA,QT_MERCADORIA from TB_CARGA_BL A
WHERE ID_CARGA_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CARGA_BL")) Then
                    txtID_CargaAereo.Text = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_MERCADORIA")) Then
                    txtQtdVolume_CargaAereo.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                    ddlMercadoria_CargaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")
                End If
                'If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NCM")) Then
                '    ddlNCM_CargaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_NCM")
                'End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NCM")) Then
                    If ds.Tables(0).Rows(0).Item("ID_NCM") > 0 Then
                        ddlNCM_CargaAereo.Items.Insert(1, ds.Tables(0).Rows(0).Item("NCM"))
                        ddlNCM_CargaAereo.SelectedIndex = 1
                    End If
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                    txtPesoBruto_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_M3")) Then
                    txtPesoVolumetrico_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_M3")
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
        End If
        Con.Fechar()
    End Sub

    Private Sub btnFechar_CargaAereo_Click(sender As Object, e As EventArgs) Handles btnFechar_CargaAereo.Click
        divErro_CargaAereo2.Visible = False
        divSuccess_CargaAereo2.Visible = False
        ddlMercadoria_CargaAereo.SelectedValue = 0
        ddlNCM_CargaAereo.SelectedValue = 0
        txtLargura_CargaAereo.Text = ""
        txtAltura_CargaAereo.Text = ""
        txtComprimento_CargaAereo.Text = ""
        txtDescMercadoria_CargaAereo.Text = ""
        txtPesoBruto_CargaAereo.Text = ""
        txtPesoVolumetrico_CargaAereo.Text = ""
        txtID_CargaAereo.Text = ""

        mpeCargaAereo.Hide()
    End Sub

    Private Sub btnFechar_CargaMaritimo_Click(sender As Object, e As EventArgs) Handles btnFechar_CargaMaritimo.Click
        divErro_CargaMaritimo2.Visible = False
        divSuccess_CargaMaritimo2.Visible = False
        ddlMercadoria_CargaMaritimo.SelectedValue = 0
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
        divErro_TaxaAereo2.Visible = False
        divSuccess_TaxaAereo2.Visible = False
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
        Session("CD_PR") = 0
        divVendaAereo.Visible = True
        divCompraAereo.Visible = True
        lblTipoEmpresa_Aereo.Text = "Fornecedor:"
        ddlDestinatarioCob_TaxaAereo.Enabled = True
        mpeTaxaAereo.Hide()
    End Sub

    Private Sub btnFechar_TaxaMaritimo_Click(sender As Object, e As EventArgs) Handles btnFechar_TaxaMaritimo.Click
        divErro_TaxaMaritimo2.Visible = False
        divSuccess_TaxaMaritimo2.Visible = False
        txtID_TaxaMaritimo.Text = ""
        ddlStatusPagamento_TaxaMaritimo.SelectedValue = 0
        ddlDespesa_TaxaMaritimo.SelectedValue = 0
        ' ddlTipoPagamento_TaxaMaritimo.SelectedValue = 0
        ddlOrigemPagamento_TaxaMaritimo.SelectedValue = 0
        ' ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 0
        ddlBaseCalculo_TaxaMaritimo.SelectedValue = 0
        ddlMoedaCompra_TaxaMaritimo.SelectedValue = 0
        ddlMoedaVenda_TaxaMaritimo.SelectedValue = 0
        ddlEmpresa_TaxaMaritimo.SelectedValue = 0
        txtValorCompra_TaxaMaritimo.Text = ""
        txtValorVenda_TaxaMaritimo.Text = ""
        'txtBaseCompra_TaxaMaritimo.Text = ""
        txtObs_TaxaMaritimo.Text = ""
        txtMinCompra_TaxaMaritimo.Text = ""
        txtMinVenda_TaxaMaritimo.Text = ""
        Session("CD_PR") = 0
        divVendaMaritimo.Visible = True
        divCompraMaritimo.Visible = True
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
                If txtObsAgente_ObsAereo.Text = "" Then
                    txtObsAgente_ObsAereo.Text = "NULL"
                Else
                    txtObsAgente_ObsAereo.Text = "'" & txtObsAgente_ObsAereo.Text & "'"
                End If

                If txtObsCliente_ObsAereo.Text = "" Then
                    txtObsCliente_ObsAereo.Text = "NULL"
                Else
                    txtObsCliente_ObsAereo.Text = "'" & txtObsCliente_ObsAereo.Text & "'"
                End If

                If txtObsOperacional_ObsAereo.Text = "" Then
                    txtObsOperacional_ObsAereo.Text = "NULL"
                Else
                    txtObsOperacional_ObsAereo.Text = "'" & txtObsOperacional_ObsAereo.Text & "'"
                End If

                If txtObsComercial_ObsAereo.Text = "" Then
                    txtObsComercial_ObsAereo.Text = "NULL"
                Else
                    txtObsComercial_ObsAereo.Text = "'" & txtObsComercial_ObsAereo.Text & "'"
                End If

                Con.ExecutarQuery("UPDATE TB_BL SET OB_CLIENTE = " & txtObsCliente_ObsAereo.Text & ", OB_AGENTE_INTERNACIONAL = " & txtObsAgente_ObsAereo.Text & " , OB_COMERCIAL = " & txtObsComercial_ObsAereo.Text & ",OB_OPERACIONAL_INTERNA = " & txtObsOperacional_ObsAereo.Text & " WHERE ID_BL = " & txtID_BasicoAereo.Text)
                divSuccess_ObsAereo.Visible = True

                txtObsCliente_ObsAereo.Text = txtObsCliente_ObsAereo.Text.Replace("NULL", "")
                txtObsCliente_ObsAereo.Text = txtObsCliente_ObsAereo.Text.Replace("'", "")

                txtObsAgente_ObsAereo.Text = txtObsAgente_ObsAereo.Text.Replace("NULL", "")
                txtObsAgente_ObsAereo.Text = txtObsAgente_ObsAereo.Text.Replace("'", "")

                txtObsComercial_ObsAereo.Text = txtObsComercial_ObsAereo.Text.Replace("NULL", "")
                txtObsComercial_ObsAereo.Text = txtObsComercial_ObsAereo.Text.Replace("'", "")

                txtObsOperacional_ObsAereo.Text = txtObsOperacional_ObsAereo.Text.Replace("NULL", "")
                txtObsOperacional_ObsAereo.Text = txtObsOperacional_ObsAereo.Text.Replace("'", "")

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
                If txtObsAgente_ObsMaritimo.Text = "" Then
                    txtObsAgente_ObsMaritimo.Text = "NULL"
                Else
                    txtObsAgente_ObsMaritimo.Text = "'" & txtObsAgente_ObsMaritimo.Text & "'"
                End If

                If txtObsCliente_ObsMaritimo.Text = "" Then
                    txtObsCliente_ObsMaritimo.Text = "NULL"
                Else
                    txtObsCliente_ObsMaritimo.Text = "'" & txtObsCliente_ObsMaritimo.Text & "'"
                End If

                If txtObsoperacional_ObsMaritimo.Text = "" Then
                    txtObsoperacional_ObsMaritimo.Text = "NULL"
                Else
                    txtObsoperacional_ObsMaritimo.Text = "'" & txtObsoperacional_ObsMaritimo.Text & "'"
                End If

                If txtObsComercial_ObsMaritimo.Text = "" Then
                    txtObsComercial_ObsMaritimo.Text = "NULL"
                Else
                    txtObsComercial_ObsMaritimo.Text = "'" & txtObsComercial_ObsMaritimo.Text & "'"
                End If

                Con.ExecutarQuery("UPDATE TB_BL SET OB_CLIENTE = " & txtObsCliente_ObsMaritimo.Text & ", OB_AGENTE_INTERNACIONAL = " & txtObsAgente_ObsMaritimo.Text & " , OB_COMERCIAL = " & txtObsComercial_ObsMaritimo.Text & ",OB_OPERACIONAL_INTERNA = " & txtObsoperacional_ObsMaritimo.Text & " WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
                divSuccess_ObsMaritimo.Visible = True

                txtObsCliente_ObsMaritimo.Text = txtObsCliente_ObsMaritimo.Text.Replace("NULL", "")
                txtObsCliente_ObsMaritimo.Text = txtObsCliente_ObsMaritimo.Text.Replace("'", "")

                txtObsAgente_ObsMaritimo.Text = txtObsAgente_ObsMaritimo.Text.Replace("NULL", "")
                txtObsAgente_ObsMaritimo.Text = txtObsAgente_ObsMaritimo.Text.Replace("'", "")

                txtObsComercial_ObsMaritimo.Text = txtObsComercial_ObsMaritimo.Text.Replace("NULL", "")
                txtObsComercial_ObsMaritimo.Text = txtObsComercial_ObsMaritimo.Text.Replace("'", "")

                txtObsoperacional_ObsMaritimo.Text = txtObsoperacional_ObsMaritimo.Text.Replace("NULL", "")
                txtObsoperacional_ObsMaritimo.Text = txtObsoperacional_ObsMaritimo.Text.Replace("'", "")
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

                    Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_BL,NR_REFERENCIA_CLIENTE) VALUES (" & txtID_BasicoAereo.Text & ", '" & txtRefAereo.Text & "')")
                    divSuccess_RefAereo.Visible = True

                    txtRefAereo.Text = ""

                End If
            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_RefAereo.Visible = True
                    lblErro_RefAereo.Text = "Usuário não possui permissão."

                Else

                    Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = '" & txtRefAereo.Text & "' WHERE ID_REFERENCIA_CLIENTE = " & txtID_RefAereo.Text)
                    divSuccess_RefAereo.Visible = True

                    txtRefAereo.Text = ""
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

                    Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_BL,NR_REFERENCIA_CLIENTE) VALUES (" & txtID_BasicoMaritimo.Text & ", '" & txtRefMaritimo.Text & "')")
                    divSuccess_RefMaritimo.Visible = True

                    txtRefMaritimo.Text = ""
                    dgvRefMaritimo.DataBind()
                End If
            Else
                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_RefMaritimo.Visible = True
                    lblErro_RefMaritimo.Text = "Usuário não possui permissão."

                Else

                    Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = '" & txtRefMaritimo.Text & "' WHERE ID_REFERENCIA_CLIENTE = " & txtID_RefMaritimo.Text)
                    divSuccess_RefMaritimo.Visible = True

                    txtRefMaritimo.Text = ""
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

    End Sub

    Private Sub btnCancelar_RefAereo_Click(sender As Object, e As EventArgs) Handles btnCancelar_RefAereo.Click
        divSuccess_RefAereo.Visible = False
        divErro_RefAereo.Visible = False
        txtRefAereo.Text = ""
        txtID_RefAereo.Text = ""

    End Sub

    Sub CalculoProfit(ID As String)
        Dim Profit As String = ""
        Dim x As Double
        Dim y As Double
        Dim z As Double
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim dsProfit As DataSet = Con.ExecutarQuery("Select ISNULL(ID_PROFIT_DIVISAO,0)ID_PROFIT_DIVISAO,ISNULL(VL_PROFIT_DIVISAO,0)VL_PROFIT_DIVISAO FROM TB_BL WHERE ID_BL = " & ID)
        If dsProfit.Tables(0).Rows.Count > 0 Then
            If dsProfit.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO") = 1 Then
                'VALOR FIXO A RECEBER
                z = dsProfit.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                Profit = z.ToString
                Profit = Profit.Replace(".", String.Empty).Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO_CALCULADO = '" & Profit & "'  WHERE ID_BL = " & ID)

            ElseIf dsProfit.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO") = 2 Then
                'VALOR FIXO A PAGAR
                z = dsProfit.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                Profit = z.ToString
                Profit = Profit.Replace(".", String.Empty).Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO_CALCULADO = '" & Profit & "'  WHERE ID_BL = " & ID)

            ElseIf dsProfit.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO") = 3 Then
                'PERCENTUAL A RECEBER

                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT ISNULL((SELECT SUM(VL_TAXA) FROM TB_BL_TAXA WHERE CD_PR = 'R' AND FL_DIVISAO_PROFIT = 1 AND ID_BL = " & ID & ") - (SELECT SUM(VL_TAXA) FROM TB_BL_TAXA WHERE CD_PR = 'P' AND FL_DIVISAO_PROFIT = 1 AND ID_BL = " & ID & "),0) AS LUCRO")

                x = dsProfit.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                y = dsAuxiliar.Tables(0).Rows(0).Item("LUCRO")
                y = y / 100
                z = y * x
                Profit = z.ToString
                Profit = Profit.Replace(".", String.Empty).Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO_CALCULADO = '" & Profit & "'  WHERE ID_BL = " & ID)

            ElseIf dsProfit.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO") = 4 Then
                'PERCENTUAL A PAGAR
                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT ISNULL((SELECT SUM(VL_TAXA) FROM TB_BL_TAXA WHERE CD_PR = 'R' AND FL_DIVISAO_PROFIT = 1 AND ID_BL = " & ID & ") - (SELECT SUM(VL_TAXA) FROM TB_BL_TAXA WHERE CD_PR = 'P' AND FL_DIVISAO_PROFIT = 1 AND ID_BL = " & ID & "),0) AS LUCRO")

                x = dsProfit.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                y = dsAuxiliar.Tables(0).Rows(0).Item("LUCRO")
                y = y / 100
                z = y * x
                Profit = z.ToString
                Profit = Profit.Replace(".", String.Empty).Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO_CALCULADO = '" & Profit & "'  WHERE ID_BL = " & ID)

            ElseIf dsProfit.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO") = 5 Then
                'POR TEU A RECEBER
                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT SUM(TEU)QTD FROM TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER IN (Select ID_TIPO_CNTR FROM TB_AMR_CNTR_BL A
INNER JOIN TB_CNTR_BL B ON B.ID_CNTR_BL=A.ID_CNTR_BL
        WHERE A.ID_BL = " & ID & ")")

                x = dsProfit.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                y = dsAuxiliar.Tables(0).Rows(0).Item("QTD")
                z = y * x
                Profit = z.ToString
                Profit = Profit.Replace(".", String.Empty).Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO_CALCULADO = '" & Profit & "'  WHERE ID_BL = " & ID)

            ElseIf dsProfit.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO") = 6 Then
                'POR TEU A PAGAR
                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT SUM(TEU)QTD FROM TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER IN (Select ID_TIPO_CNTR FROM TB_AMR_CNTR_BL A
INNER JOIN TB_CNTR_BL B ON B.ID_CNTR_BL=A.ID_CNTR_BL
        WHERE A.ID_BL = " & ID & ")")

                x = dsProfit.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                y = dsAuxiliar.Tables(0).Rows(0).Item("QTD")
                z = y * x
                Profit = z.ToString
                Profit = Profit.Replace(".", String.Empty).Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO_CALCULADO = '" & Profit & "'  WHERE ID_BL = " & ID)
            ElseIf dsProfit.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO") = 7 Then
                'POR CONTEINER A RECEBER
                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_AMR_CNTR_BL)QTD FROM TB_AMR_CNTR_BL WHERE ID_BL = " & ID)

                x = dsProfit.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                y = dsAuxiliar.Tables(0).Rows(0).Item("QTD")
                z = y * x
                Profit = z.ToString
                Profit = Profit.Replace(".", String.Empty).Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO_CALCULADO = '" & Profit & "'  WHERE ID_BL = " & ID)

            ElseIf dsProfit.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO") = 8 Then
                'POR CONTEINER A PAGAR
                Dim dsAuxiliar As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_AMR_CNTR_BL)QTD FROM TB_AMR_CNTR_BL WHERE ID_BL = " & ID)

                x = dsProfit.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO")
                y = dsAuxiliar.Tables(0).Rows(0).Item("QTD")
                z = y * x
                Profit = z.ToString
                Profit = Profit.Replace(".", String.Empty).Replace(",", ".")

                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO_CALCULADO = '" & Profit & "'  WHERE ID_BL = " & ID)
            End If
        End If
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

            If txtValorDivisaoProfit_BasicoMaritimo.Text = "" Then
                txtValorDivisaoProfit_BasicoMaritimo.Text = 0
            End If

            txtValorDivisaoProfit_BasicoMaritimo.Text = txtValorDivisaoProfit_BasicoMaritimo.Text.Replace(".", "")
            txtValorDivisaoProfit_BasicoMaritimo.Text = txtValorDivisaoProfit_BasicoMaritimo.Text.Replace(",", ".")

            If txtResumoMercadoria_BasicoMaritimo.Text = "" Then
                txtResumoMercadoria_BasicoMaritimo.Text = "NULL"
            Else
                txtResumoMercadoria_BasicoMaritimo.Text = "'" & txtResumoMercadoria_BasicoMaritimo.Text & "'"
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


            If txtCE_BasicoMaritimo.Text = "" Then
                txtCE_BasicoMaritimo.Text = "NULL"
                txtDataCE_BasicoMaritimo.Text = "NULL"

            Else
                txtCE_BasicoMaritimo.Text = "'" & txtCE_BasicoMaritimo.Text & "'"
                txtDataCE_BasicoMaritimo.Text = " getdate() "
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
                            'INSERE 
                            ds = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,NR_BL,ID_PARCEIRO_TRANSPORTADOR,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_EXPORTADOR, ID_PARCEIRO_COMISSARIA,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,FL_FREE_HAND,ID_TIPO_ESTUFAGEM,ID_TIPO_PAGAMENTO,ID_TIPO_CARGA,NR_CE,DT_CE,OB_REFERENCIA_AUXILIAR,OB_REFERENCIA_COMERCIAL,NM_RESUMO_MERCADORIA, ID_SERVICO,GRAU,ID_PARCEIRO_IMPORTADOR,DT_ABERTURA,ID_PARCEIRO_INDICADOR,ID_PROFIT_DIVISAO,VL_PROFIT_DIVISAO ) VALUES (" & txtProcesso_BasicoMaritimo.Text & "," & txtHBL_BasicoMaritimo.Text & "," & ddlTransportador_BasicoMaritimo.SelectedValue & ", " & ddlOrigem_BasicoMaritimo.SelectedValue & ", " & ddlDestino_BasicoMaritimo.SelectedValue & "," & ddlCliente_BasicoMaritimo.SelectedValue & ", " & ddlExportador_BasicoMaritimo.SelectedValue & "," & ddlComissaria_BasicoMaritimo.SelectedValue & "," & ddlAgente_BasicoMaritimo.SelectedValue & "," & ddlIncoterm_BasicoMaritimo.SelectedValue & ",'" & ckbFreeHand_BasicoMaritimo.Checked & "'," & ddlEstufagem_BasicoMaritimo.SelectedValue & "," & ddlTipoPagamento_BasicoMaritimo.SelectedValue & "," & ddlTipoCarga_BasicoMaritimo.SelectedValue & "," & txtCE_BasicoMaritimo.Text & "," & txtDataCE_BasicoMaritimo.Text & "," & txtRefAuxiliar_BasicoMaritimo.Text & "," & txtRefComercial_BasicoMaritimo.Text & ", " & txtResumoMercadoria_BasicoMaritimo.Text & ", " & ddlServico_BasicoMaritimo.SelectedValue & ", 'C'," & ddlImportador_BasicoMaritimo.SelectedValue & ",GETDATE()," & ddlIndicador_BasicoMaritimo.SelectedValue & "," & ddlDivisaoProfit_BasicoMaritimo.SelectedValue & ", " & txtValorDivisaoProfit_BasicoMaritimo.Text & ") Select SCOPE_IDENTITY() as ID_BL ")

                            'PREENCHE SESSÃO E CAMPO DE ID
                            Session("ID_BL") = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                            txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()

                            NumeroProcesso()

                            CalculoProfit(txtID_BasicoMaritimo.Text)

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
                    Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = " & txtProcesso_BasicoMaritimo.Text & " , NR_BL = " & txtHBL_BasicoMaritimo.Text & ", ID_PARCEIRO_TRANSPORTADOR = " & ddlTransportador_BasicoMaritimo.SelectedValue & ", ID_PORTO_ORIGEM = " & ddlOrigem_BasicoMaritimo.SelectedValue & ", ID_PORTO_DESTINO = " & ddlDestino_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_CLIENTE = " & ddlCliente_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_EXPORTADOR = " & ddlExportador_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_COMISSARIA = " & ddlComissaria_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgente_BasicoMaritimo.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm_BasicoMaritimo.SelectedValue & ", ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoMaritimo.SelectedValue & ", ID_TIPO_CARGA = " & ddlTipoCarga_BasicoMaritimo.SelectedValue & ", NR_CE = " & txtCE_BasicoMaritimo.Text & ", DT_CE = " & txtDataCE_BasicoMaritimo.Text & ",  OB_REFERENCIA_AUXILIAR =" & txtRefAuxiliar_BasicoMaritimo.Text & ", OB_REFERENCIA_COMERCIAL = " & txtRefComercial_BasicoMaritimo.Text & ", NM_RESUMO_MERCADORIA = " & txtResumoMercadoria_BasicoMaritimo.Text & ",FL_FREE_HAND = '" & ckbFreeHand_BasicoMaritimo.Checked & "', ID_TIPO_ESTUFAGEM = " & ddlEstufagem_BasicoMaritimo.SelectedValue & ",ID_SERVICO =" & ddlServico_BasicoMaritimo.SelectedValue & ", GRAU = 'C', ID_PARCEIRO_IMPORTADOR = " & ddlImportador_BasicoMaritimo.SelectedValue & ",ID_PARCEIRO_INDICADOR = " & ddlIndicador_BasicoMaritimo.SelectedValue & ",ID_PROFIT_DIVISAO = " & ddlDivisaoProfit_BasicoMaritimo.SelectedValue & ", VL_PROFIT_DIVISAO = " & txtValorDivisaoProfit_BasicoMaritimo.Text & " WHERE ID_BL = " & txtID_BasicoMaritimo.Text)

                    CalculoProfit(txtID_BasicoMaritimo.Text)

                    LimpaNulo()



                    divSuccess_BasicoMaritimo.Visible = True
                    Con.Fechar()


                End If

            End If


        End If
    End Sub

    Sub LimpaNulo()
        txtResumoMercadoria_BasicoMaritimo.Text = txtResumoMercadoria_BasicoMaritimo.Text.Replace("'", "")
        txtResumoMercadoria_BasicoMaritimo.Text = txtResumoMercadoria_BasicoMaritimo.Text.Replace("NULL", "")

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

        txtResumoMercadoria_BasicoAereo.Text = txtResumoMercadoria_BasicoAereo.Text.Replace("'", "")
        txtResumoMercadoria_BasicoAereo.Text = txtResumoMercadoria_BasicoAereo.Text.Replace("NULL", "")

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
            If txtValorDivisaoProfit_BasicoAereo.Text = "" Then
                txtValorDivisaoProfit_BasicoAereo.Text = 0
            End If

            txtValorDivisaoProfit_BasicoAereo.Text = txtValorDivisaoProfit_BasicoAereo.Text.Replace(".", "")
            txtValorDivisaoProfit_BasicoAereo.Text = txtValorDivisaoProfit_BasicoAereo.Text.Replace(",", ".")

            If txtResumoMercadoria_BasicoAereo.Text = "" Then
                txtResumoMercadoria_BasicoAereo.Text = "NULL"
            Else
                txtResumoMercadoria_BasicoAereo.Text = "'" & txtResumoMercadoria_BasicoAereo.Text & "'"
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

            If txtNumeroCE_BasicoAereo.Text = "" Then
                txtNumeroCE_BasicoAereo.Text = "NULL"
                txtDataCE_BasicoAereo.Text = "NULL"

            Else
                txtNumeroCE_BasicoAereo.Text = "'" & txtNumeroCE_BasicoAereo.Text & "'"
                txtDataCE_BasicoAereo.Text = " getdate() "
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
                            'INSERE 
                            ds = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,NR_BL, ID_PARCEIRO_TRANSPORTADOR, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_PARCEIRO_CLIENTE, ID_PARCEIRO_EXPORTADOR, ID_PARCEIRO_COMISSARIA, ID_PARCEIRO_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_PARCEIRO_ARMAZEM_DESEMBARACO, ID_TIPO_PAGAMENTO, ID_TIPO_CARGA, NR_CE, DT_CE, OB_REFERENCIA_AUXILIAR, OB_REFERENCIA_COMERCIAL, NM_RESUMO_MERCADORIA,ID_PARCEIRO_RODOVIARIO,ID_SERVICO,GRAU,ID_PARCEIRO_IMPORTADOR,DT_ABERTURA,FL_FREE_HAND,ID_PARCEIRO_INDICADOR,ID_PROFIT_DIVISAO,VL_PROFIT_DIVISAO) VALUES (" & txtProcesso_BasicoAereo.Text & ", " & txtHBL_BasicoAereo.Text & "," & ddlTransportador_BasicoAereo.SelectedValue & ", " & ddlOrigem_BasicoAereo.SelectedValue & ", " & ddlDestino_BasicoAereo.SelectedValue & "," & ddlCliente_BasicoAereo.SelectedValue & ", " & ddlExportador_BasicoAereo.SelectedValue & "," & ddlComissaria_BasicoAereo.SelectedValue & "," & ddlAgente_BasicoAereo.SelectedValue & "," & ddlIncoterm_BasicoAereo.SelectedValue & "," & ddlArmazem_BasicoAereo.SelectedValue & "," & ddlTipoPagamento_BasicoAereo.SelectedValue & "," & ddlTipoCarga_BasicoAereo.SelectedValue & "," & txtNumeroCE_BasicoAereo.Text & ", " & txtDataCE_BasicoAereo.Text & "," & txtRefAuxiliar_BasicoAereo.Text & "," & txtRefComercial_BasicoAereo.Text & ", " & txtResumoMercadoria_BasicoAereo.Text & ", " & ddlTranspRod_BasicoAereo.SelectedValue & "," & ddlServico_BasicoAereo.SelectedValue & ",'C'," & ddlImportador_BasicoAereo.SelectedValue & ",GETDATE(),'" & ckbFreeHand_BasicoAereo.Checked & "'," & ddlIndicador_BasicoAereo.SelectedValue & "," & ddlDivisaoProfit_BasicoAereo.SelectedValue & ", " & txtValorDivisaoProfit_BasicoAereo.Text & ") Select SCOPE_IDENTITY() as ID_BL ")


                            'PREENCHE SESSÃO E CAMPO DE ID
                            Session("ID_BL") = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                            txtID_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()

                            NumeroProcesso()

                            CalculoProfit(txtID_BasicoAereo.Text)

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
                    Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = " & txtProcesso_BasicoAereo.Text & " , NR_BL = " & txtHBL_BasicoAereo.Text & ",ID_PARCEIRO_TRANSPORTADOR = " & ddlTransportador_BasicoAereo.SelectedValue & ", ID_PORTO_ORIGEM = " & ddlOrigem_BasicoAereo.SelectedValue & ", ID_PORTO_DESTINO = " & ddlDestino_BasicoAereo.SelectedValue & ", ID_PARCEIRO_CLIENTE = " & ddlCliente_BasicoAereo.SelectedValue & ", ID_PARCEIRO_EXPORTADOR = " & ddlExportador_BasicoAereo.SelectedValue & ", ID_PARCEIRO_COMISSARIA = " & ddlComissaria_BasicoAereo.SelectedValue & ", ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgente_BasicoAereo.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm_BasicoAereo.SelectedValue & ", ID_PARCEIRO_ARMAZEM_DESEMBARACO = " & ddlArmazem_BasicoAereo.SelectedValue & ", ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoAereo.SelectedValue & ", ID_TIPO_CARGA = " & ddlTipoCarga_BasicoAereo.SelectedValue & ", NR_CE = " & txtNumeroCE_BasicoAereo.Text & ", DT_CE = " & txtDataCE_BasicoAereo.Text & ",  OB_REFERENCIA_AUXILIAR =" & txtRefAuxiliar_BasicoAereo.Text & ", OB_REFERENCIA_COMERCIAL = " & txtRefComercial_BasicoAereo.Text & ", NM_RESUMO_MERCADORIA = " & txtResumoMercadoria_BasicoAereo.Text & ",ID_PARCEIRO_RODOVIARIO = " & ddlTranspRod_BasicoAereo.SelectedValue & ",ID_SERVICO = " & ddlServico_BasicoAereo.SelectedValue & ", ID_PARCEIRO_IMPORTADOR = " & ddlImportador_BasicoAereo.SelectedValue & ",FL_FREE_HAND = '" & ckbFreeHand_BasicoAereo.Checked & "' ,ID_PARCEIRO_INDICADOR = " & ddlIndicador_BasicoAereo.SelectedValue & ",ID_PROFIT_DIVISAO = " & ddlDivisaoProfit_BasicoAereo.SelectedValue & ",VL_PROFIT_DIVISAO = " & txtValorDivisaoProfit_BasicoAereo.Text & " WHERE ID_BL = " & txtID_BasicoAereo.Text)

                    CalculoProfit(txtID_BasicoAereo.Text)

                    LimpaNulo()

                    divSuccess_BasicoAereo.Visible = True
                    Con.Fechar()


                End If




            End If


        End If
    End Sub

    Private Sub btnSalvar_CargaAereo_Click(sender As Object, e As EventArgs) Handles btnSalvar_CargaAereo.Click

        divSuccess_CargaAereo2.Visible = False
        divErro_CargaAereo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

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

        If txtDescMercadoria_CargaAereo.Text = "" Then
            txtDescMercadoria_CargaAereo.Text = "NULL"
        Else
            txtDescMercadoria_CargaAereo.Text = "'" & txtDescMercadoria_CargaAereo.Text & "'"
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
            ID_NCM = SeparaNCM(ddlNCM_CargaAereo.SelectedValue)
        End If


        If txtPesoVolumetrico_CargaAereo.Text = "" Then
            divErro_CargaAereo2.Visible = True
            lblErro_CargaAereo2.Text = "É necessário informar o Peso Volumetrico da carga."

        ElseIf txtPesoVolumetrico_CargaAereo.Text <= 0 Then
            divErro_CargaAereo2.Visible = True
            lblErro_CargaAereo2.Text = "Peso Volumetrico da carga deve ser maior que zero."

        ElseIf txtID_CargaAereo.Text = "" Then

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro_CargaAereo2.Visible = True
                lblErro_CargaAereo2.Text = "Usuário não possui permissão para cadastrar."
                ' Exit Sub

            Else

                'INSERE 
                ds = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_BL,ID_TIPO_CARGA,ID_NCM,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,DS_MERCADORIA,QT_MERCADORIA) VALUES (" & txtID_BasicoAereo.Text & "," & ddlMercadoria_CargaAereo.SelectedValue & ", " & ID_NCM & ", " & txtPesoBruto_CargaAereo.Text & "," & txtPesoVolumetrico_CargaAereo.Text & ", " & txtAltura_CargaAereo.Text & "," & txtLargura_CargaAereo.Text & "," & txtComprimento_CargaAereo.Text & "," & txtDescMercadoria_CargaAereo.Text & "," & txtQtdVolume_CargaAereo.Text & ") Select SCOPE_IDENTITY() as ID_CARGA_BL ")
                Dim ID_CARGA_BL As String = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")


                Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text)


                Con.Fechar()
                divSuccess_CargaAereo2.Visible = True
                dgvCargaAereo.DataBind()
            End If

        Else

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro_CargaAereo2.Visible = True
                lblErro_CargaAereo2.Text = "Usuário não possui permissão para alterar."
                Exit Sub

            Else


                'REALIZA UPDATE 
                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_BL = " & txtID_BasicoAereo.Text & ",ID_TIPO_CARGA = " & ddlMercadoria_CargaAereo.SelectedValue & ",ID_NCM = " & ID_NCM & ",VL_PESO_BRUTO = " & txtPesoBruto_CargaAereo.Text & ",VL_M3 = " & txtPesoVolumetrico_CargaAereo.Text & ",VL_ALTURA =" & txtAltura_CargaAereo.Text & ",VL_LARGURA = " & txtLargura_CargaAereo.Text & ",VL_COMPRIMENTO = " & txtComprimento_CargaAereo.Text & ",DS_MERCADORIA = " & txtDescMercadoria_CargaAereo.Text & ", QT_MERCADORIA = " & txtQtdVolume_CargaAereo.Text & " WHERE ID_CARGA_BL = " & txtID_CargaAereo.Text)



                Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text)

                divSuccess_CargaAereo2.Visible = True
                Con.Fechar()
                dgvCargaAereo.DataBind()


            End If


        End If
        txtDescMercadoria_CargaAereo.Text = txtDescMercadoria_CargaAereo.Text.Replace("'", "")
        txtDescMercadoria_CargaAereo.Text = txtDescMercadoria_CargaAereo.Text.Replace("NULL", "")

    End Sub

    Private Sub btnSalvar_CargaMaritimo_Click(sender As Object, e As EventArgs) Handles btnSalvar_CargaMaritimo.Click
        divSuccess_CargaMaritimo2.Visible = False
        divErro_CargaMaritimo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData


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


        txtPesoBruto_CargaMaritimo.Text = txtPesoBruto_CargaMaritimo.Text.Replace(".", "")
        txtPesoBruto_CargaMaritimo.Text = txtPesoBruto_CargaMaritimo.Text.Replace(",", ".")

        txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(".", "")
        txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(",", ".")

        Dim ID_NCM As String = 0

        If ddlNCM_CargaMaritimo.SelectedIndex <> 0 Then
            ID_NCM = SeparaNCM(ddlNCM_CargaMaritimo.SelectedValue)
        End If

        If txtDescMercadoriaCNTR_Maritimo.Text = "" Then
            txtDescMercadoriaCNTR_Maritimo.Text = "NULL"
        Else
            txtDescMercadoriaCNTR_Maritimo.Text = "'" & txtDescMercadoriaCNTR_Maritimo.Text & "'"
        End If

        If txtPesoVolumetrico_CargaMaritimo.Text = "" Then
            divErro_CargaMaritimo2.Visible = True
            lblErro_CargaMaritimo2.Text = "É necessário informar o Peso Volumetrico da carga."

        ElseIf txtPesoVolumetrico_CargaMaritimo.Text <= 0 Then
            divErro_CargaMaritimo2.Visible = True
            lblErro_CargaMaritimo2.Text = "Peso Volumetrico da carga deve ser maior que zero."

        ElseIf txtID_CargaMaritimo.Text = "" Then


            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro_CargaMaritimo2.Visible = True
                lblErro_CargaMaritimo2.Text = "Usuário não possui permissão para cadastrar."

            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_CARGA_BL)QTD FROM TB_CARGA_BL where ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.SelectedValue & "  AND ID_BL = " & txtID_BasicoMaritimo.Text)
                If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                    divErro_CargaMaritimo2.Visible = True
                    lblErro_CargaMaritimo2.Text = "Container vinculado em outra carga desta mesma BL."

                Else

                    'INSERE 
                    ds = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_CNTR_BL, ID_EMBALAGEM, DS_GRUPO_NCM,ID_BL,ID_TIPO_CARGA,ID_NCM,VL_PESO_BRUTO,VL_M3,QT_MERCADORIA,DS_MERCADORIA,ID_TIPO_CNTR) VALUES (" & ddlNumeroCNTR_CargaMaritimo.SelectedValue & "," & ddlEmbalagem_CargaMaritimo.SelectedValue & "," & txtGrupoNCM_CargaMaritimo.Text & "," & txtID_BasicoMaritimo.Text & "," & ddlMercadoria_CargaMaritimo.SelectedValue & ", " & ID_NCM & ", " & txtPesoBruto_CargaMaritimo.Text & "," & txtPesoVolumetrico_CargaMaritimo.Text & "," & txtQtdVolumes_CargaMaritimo.Text & "," & txtDescMercadoriaCNTR_Maritimo.Text & "," & ddlTipoContainer_CargaMaritimo.SelectedValue & ") Select SCOPE_IDENTITY() as ID_CARGA_BL ")
                    Dim ID_CARGA_BL As String = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")


                    If ddlNumeroCNTR_CargaMaritimo.SelectedValue <> 0 Then
                        Call AMR_CNTR_INSERT(txtID_BasicoMaritimo.Text, ID_CARGA_BL)
                    End If

                    Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)



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
                If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                    divErro_CargaMaritimo2.Visible = True
                    lblErro_CargaMaritimo2.Text = "Container vinculado em outra carga desta mesma BL."

                Else

                    If ddlNumeroCNTR_CargaMaritimo.SelectedValue <> 0 Then
                        Call AMR_CNTR_UPDATE(txtID_BasicoMaritimo.Text, txtID_CargaMaritimo.Text)
                    End If


                    'REALIZA UPDATE 
                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_BL = " & txtID_BasicoMaritimo.Text & ",ID_TIPO_CARGA = " & ddlMercadoria_CargaMaritimo.SelectedValue & ",ID_NCM = " & ID_NCM & ",VL_PESO_BRUTO = " & txtPesoBruto_CargaMaritimo.Text & ",VL_M3 = " & txtPesoVolumetrico_CargaMaritimo.Text & ",ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.SelectedValue & ", ID_EMBALAGEM = " & ddlEmbalagem_CargaMaritimo.SelectedValue & ", DS_GRUPO_NCM = " & txtGrupoNCM_CargaMaritimo.Text & ",DS_MERCADORIA = " & txtDescMercadoriaCNTR_Maritimo.Text & ", QT_MERCADORIA = " & txtQtdVolumes_CargaMaritimo.Text & ", ID_TIPO_CNTR = " & ddlTipoContainer_CargaMaritimo.SelectedValue & " WHERE ID_CARGA_BL = " & txtID_CargaMaritimo.Text)

                    Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)

                    divSuccess_CargaMaritimo2.Visible = True
                    Con.Fechar()

                End If

            End If


        End If

        txtDescMercadoriaCNTR_Maritimo.Text = txtDescMercadoriaCNTR_Maritimo.Text.Replace("'", "")
        txtDescMercadoriaCNTR_Maritimo.Text = txtDescMercadoriaCNTR_Maritimo.Text.Replace("NULL", "")

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



        If txtObs_TaxaAereo.Text = "" Then
            txtObs_TaxaAereo.Text = "NULL"
        Else
            txtObs_TaxaAereo.Text = "'" & txtObs_TaxaAereo.Text & "'"
        End If

        'If txtBaseCompra_TaxaAereo.Text = "" Then
        '    txtBaseCompra_TaxaAereo.Text = 0
        'End If

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

        'txtBaseCompra_TaxaAereo.Text = txtBaseCompra_TaxaAereo.Text.Replace(".", "")
        'txtBaseCompra_TaxaAereo.Text = txtBaseCompra_TaxaAereo.Text.Replace(",", ".")

        txtMinCompra_TaxaAereo.Text = txtMinCompra_TaxaAereo.Text.Replace(".", "")
        txtMinCompra_TaxaAereo.Text = txtMinCompra_TaxaAereo.Text.Replace(",", ".")

        txtValorCompra_TaxaAereo.Text = txtValorCompra_TaxaAereo.Text.Replace(".", "")
        txtValorCompra_TaxaAereo.Text = txtValorCompra_TaxaAereo.Text.Replace(",", ".")

        txtMinVenda_TaxaAereo.Text = txtMinVenda_TaxaAereo.Text.Replace(".", "")
        txtMinVenda_TaxaAereo.Text = txtMinVenda_TaxaAereo.Text.Replace(",", ".")

        txtValorVenda_TaxaAereo.Text = txtValorVenda_TaxaAereo.Text.Replace(".", "")
        txtValorVenda_TaxaAereo.Text = txtValorVenda_TaxaAereo.Text.Replace(",", ".")

        If txtValorCompra_TaxaAereo.Text = 0 And txtValorVenda_TaxaAereo.Text = 0 Then
            divErro_TaxaAereo2.Visible = True
            lblErro_TaxaAereo2.Text = "Não é possivel cadastrar taxas com valores zerados."
            mpeTaxaAereo.Show()
            txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("'", "")
            txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("NULL", "")
            Exit Sub
        Else
            If txtID_TaxaAereo.Text = "" Then


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxaAereo2.Visible = True
                    lblErro_TaxaAereo2.Text = "Usuário não possui permissão para cadastrar."
                    mpeTaxaAereo.Show()
                    txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("'", "")
                    txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("NULL", "")
                    Exit Sub

                Else
                    Dim dstaxa As DataSet
                    Dim ID_BL_TAXA As String
                    Dim Calcula As New CalculaBL
                    Dim retorno As String

                    If txtValorCompra_TaxaAereo.Text > 0 Then
                        'INSERE TAXA DE COMPRA
                        dstaxa = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DIVISAO_PROFIT,FL_DECLARADO,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF) VALUES (" & txtID_BasicoAereo.Text & "," & ddlDespesa_TaxaAereo.SelectedValue & "," & ddlTipoPagamento_TaxaAereo.SelectedValue & "," & ddlOrigemPagamento_TaxaAereo.SelectedValue & "," & ddlDestinatarioCob_TaxaAereo.SelectedValue & "," & ddlBaseCalculo_TaxaAereo.SelectedValue & "," & ddlMoedaCompra_TaxaAereo.SelectedValue & "," & txtValorCompra_TaxaAereo.Text & "," & txtMinCompra_TaxaAereo.Text & "," & txtObs_TaxaAereo.Text & ",'" & ckbDeclarado_TaxaAereo.Checked & "','" & ckbProfit_TaxaAereo.Checked & "'," & ddlEmpresa_TaxaAereo.SelectedValue & ",'" & ckbPremiacao_TaxaAereo.Checked & "','P','OPER') Select SCOPE_IDENTITY() as ID_BL_TAXA ")
                        ID_BL_TAXA = dstaxa.Tables(0).Rows(0).Item("ID_BL_TAXA")
                        retorno = Calcula.Calcular(ID_BL_TAXA)
                    End If


                    If txtValorVenda_TaxaAereo.Text > 0 Then
                        Dim empresa As Integer = 0
                        If ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 1 Then
                            'Cliente
                            empresa = ddlCliente_BasicoMaritimo.SelectedValue
                        ElseIf ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 2 Then
                            'Agente
                            empresa = ddlAgente_BasicoMaritimo.SelectedValue

                        End If

                        'INSERE TAXA DE VENDA
                        dstaxa = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DIVISAO_PROFIT,FL_DECLARADO,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF) VALUES (" & txtID_BasicoAereo.Text & "," & ddlDespesa_TaxaAereo.SelectedValue & "," & ddlTipoPagamento_TaxaAereo.SelectedValue & "," & ddlOrigemPagamento_TaxaAereo.SelectedValue & "," & ddlDestinatarioCob_TaxaAereo.SelectedValue & "," & ddlBaseCalculo_TaxaAereo.SelectedValue & "," & ddlMoedaVenda_TaxaAereo.SelectedValue & "," & txtValorVenda_TaxaAereo.Text & "," & txtMinVenda_TaxaAereo.Text & "," & txtObs_TaxaAereo.Text & ",'" & ckbDeclarado_TaxaAereo.Checked & "','" & ckbProfit_TaxaAereo.Checked & "'," & empresa & ",'" & ckbPremiacao_TaxaAereo.Checked & "','R','OPER') Select SCOPE_IDENTITY() as ID_BL_TAXA ")

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
                    txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("'", "")
                    txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("NULL", "")
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
                        txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("'", "")
                        txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("NULL", "")
                        Exit Sub

                    Else



                        If Session("CD_PR") = "P" Then

                            'REALIZA UPDATE TAXA COMPRA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoAereo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoAereo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaAereo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaAereo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaAereo.SelectedValue & ",ID_MOEDA =" & ddlMoedaCompra_TaxaAereo.SelectedValue & ",VL_TAXA = " & txtValorCompra_TaxaAereo.Text & ",VL_TAXA_MIN = " & txtMinCompra_TaxaAereo.Text & ",OB_TAXAS = " & txtObs_TaxaAereo.Text & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaAereo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaAereo.Checked & "', ID_PARCEIRO_EMPRESA =  " & ddlEmpresa_TaxaAereo.SelectedValue & ",FL_CALCULADO = 0,FL_PREMIACAO ='" & ckbPremiacao_TaxaAereo.Checked & "' WHERE ID_BL_TAXA = " & txtID_TaxaAereo.Text)

                        ElseIf Session("CD_PR") = "R" Then

                            'REALIZA UPDATE TAXA VENDA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoAereo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoAereo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaAereo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaAereo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaAereo.SelectedValue & ",ID_MOEDA =" & ddlMoedaVenda_TaxaAereo.SelectedValue & ",VL_TAXA = " & txtValorVenda_TaxaAereo.Text & ",VL_TAXA_MIN = " & txtMinVenda_TaxaAereo.Text & ",OB_TAXAS = " & txtObs_TaxaAereo.Text & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaAereo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaAereo.Checked & "', ID_PARCEIRO_EMPRESA =  " & ddlEmpresa_TaxaAereo.SelectedValue & ",FL_CALCULADO = 0,FL_PREMIACAO ='" & ckbPremiacao_TaxaAereo.Checked & "' WHERE ID_BL_TAXA = " & txtID_TaxaAereo.Text)

                        End If

                        Dim Calcula As New CalculaBL
                        Dim retorno As String = Calcula.Calcular(txtID_TaxaAereo.Text)


                        txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("'", "")
                        txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("NULL", "")

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
                'CASO EXISTA
                If dsAMRNovo.Tables(0).Rows.Count = 0 Then
                    'CASO SIM, CHAMA ROTINA DE INSERÇÃO
                    Call AMR_CNTR_INSERT(ID_BL, ID_CARGA_BL)


                End If


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

        If txtObs_TaxaMaritimo.Text = "" Then
            txtObs_TaxaMaritimo.Text = "NULL"
        Else
            txtObs_TaxaMaritimo.Text = "'" & txtObs_TaxaMaritimo.Text & "'"
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

        If txtValorCompra_TaxaMaritimo.Text = 0 And txtValorVenda_TaxaMaritimo.Text = 0 Then
            divErro_TaxaMaritimo2.Visible = True
            lblErro_TaxaMaritimo2.Text = "Não é possivel cadastrar taxas com valores zerados."
            mpeTaxaMaritimo.Show()
            txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("'", "")
            txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("NULL", "")
            Exit Sub
        Else
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
                    txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("'", "")
                    txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("NULL", "")
                    mpeTaxaMaritimo.Show()

                    Exit Sub

                Else
                    Dim dstaxa As DataSet
                    Dim ID_BL_TAXA As String
                    Dim Calcula As New CalculaBL
                    Dim retorno As String

                    If txtValorCompra_TaxaMaritimo.Text > 0 Then
                        'INSERE TAXA DE COMPRA

                        dstaxa = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DIVISAO_PROFIT,FL_DECLARADO,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF) VALUES (" & txtID_BasicoMaritimo.Text & "," & ddlDespesa_TaxaMaritimo.SelectedValue & "," & ddlTipoPagamento_TaxaMaritimo.SelectedValue & "," & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & "," & ddlStatusPagamento_TaxaMaritimo.SelectedValue & "," & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & "," & ddlBaseCalculo_TaxaMaritimo.SelectedValue & "," & ddlMoedaCompra_TaxaMaritimo.SelectedValue & "," & OPERADOR & txtValorCompra_TaxaMaritimo.Text & "," & OPERADOR & txtMinCompra_TaxaMaritimo.Text & "," & txtObs_TaxaMaritimo.Text & ",'" & ckbDeclarado_TaxaMaritimo.Checked & "','" & ckbProfit_TaxaMaritimo.Checked & "'," & ddlEmpresa_TaxaMaritimo.SelectedValue & ",'" & ckbPremiacao_TaxaMaritimo.Checked & "','P','OPER') Select SCOPE_IDENTITY() as ID_BL_TAXA ")

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

                        End If

                        'INSERE TAXA DE VENDA
                        dstaxa = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DIVISAO_PROFIT,FL_DECLARADO,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,CD_ORIGEM_INF) VALUES (" & txtID_BasicoMaritimo.Text & "," & ddlDespesa_TaxaMaritimo.SelectedValue & "," & ddlTipoPagamento_TaxaMaritimo.SelectedValue & "," & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & "," & ddlStatusPagamento_TaxaMaritimo.SelectedValue & "," & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & "," & ddlBaseCalculo_TaxaMaritimo.SelectedValue & "," & ddlMoedaVenda_TaxaMaritimo.SelectedValue & "," & OPERADOR & txtValorVenda_TaxaMaritimo.Text & "," & OPERADOR & txtMinVenda_TaxaMaritimo.Text & "," & txtObs_TaxaMaritimo.Text & ",'" & ckbDeclarado_TaxaMaritimo.Checked & "','" & ckbProfit_TaxaMaritimo.Checked & "'," & empresa & ",'" & ckbPremiacao_TaxaMaritimo.Checked & "','R','OPER') Select SCOPE_IDENTITY() as ID_BL_TAXA ")

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




                    Con.Fechar()
                End If




            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxaMaritimo2.Visible = True
                    lblErro_TaxaMaritimo2.Text = "Usuário não possui permissão para alterar."
                    txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("'", "")
                    txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("NULL", "")
                    mpeTaxaMaritimo.Show()

                    Exit Sub

                Else
                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & txtID_TaxaMaritimo.Text & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxaMaritimo2.Visible = True
                        lblErro_TaxaMaritimo2.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
                        txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("'", "")
                        txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("NULL", "")
                        mpeTaxaMaritimo.Show()

                        Exit Sub
                    Else


                        'REALIZA UPDATE 


                        If Session("CD_PR") = "P" Then

                            'REALIZA UPDATE TAXA COMPRA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoMaritimo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaMaritimo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_TaxaMaritimo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaMaritimo.SelectedValue & ",ID_MOEDA =" & ddlMoedaCompra_TaxaMaritimo.SelectedValue & ",VL_TAXA = " & OPERADOR & txtValorCompra_TaxaMaritimo.Text & ",VL_TAXA_MIN = " & OPERADOR & txtMinCompra_TaxaMaritimo.Text & ",OB_TAXAS = " & txtObs_TaxaMaritimo.Text & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaMaritimo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaMaritimo.Checked & "',ID_PARCEIRO_EMPRESA = " & ddlEmpresa_TaxaMaritimo.SelectedValue & ",FL_CALCULADO = 0, FL_PREMIACAO  = '" & ckbPremiacao_TaxaMaritimo.Checked & "' WHERE ID_BL_TAXA = " & txtID_TaxaMaritimo.Text)
                        ElseIf Session("CD_PR") = "R" Then

                            'REALIZA UPDATE TAXA VENDA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoMaritimo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaMaritimo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_TaxaMaritimo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaMaritimo.SelectedValue & ",ID_MOEDA =" & ddlMoedaVenda_TaxaMaritimo.SelectedValue & ",VL_TAXA = " & OPERADOR & txtValorVenda_TaxaMaritimo.Text & ",VL_TAXA_MIN = " & OPERADOR & txtMinVenda_TaxaMaritimo.Text & ",OB_TAXAS = " & txtObs_TaxaMaritimo.Text & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaMaritimo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaMaritimo.Checked & "',ID_PARCEIRO_EMPRESA = " & ddlEmpresa_TaxaMaritimo.SelectedValue & ",FL_CALCULADO = 0, FL_PREMIACAO  = '" & ckbPremiacao_TaxaMaritimo.Checked & "' WHERE ID_BL_TAXA = " & txtID_TaxaMaritimo.Text)

                        End If

                        Dim Calcula As New CalculaBL
                        Dim retorno As String = Calcula.Calcular(txtID_TaxaMaritimo.Text)

                        dgvTaxaMaritimoCompras.DataBind()
                        dgvTaxaMaritimoVendas.DataBind()
                        divSuccess_TaxaMaritimo2.Visible = True


                        txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("'", "")
                        txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("NULL", "")


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

            ds = Con.ExecutarQuery("select NR_REFERENCIA_CLIENTE from TB_REFERENCIA_CLIENTE
WHERE ID_REFERENCIA_CLIENTE = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                txtID_RefMaritimo.Text = ID

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")) Then
                    txtRefMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")
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

            ds = Con.ExecutarQuery("select NR_REFERENCIA_CLIENTE from TB_REFERENCIA_CLIENTE
WHERE ID_REFERENCIA_CLIENTE = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                txtID_RefAereo.Text = ID

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")) Then
                    txtRefAereo.Text = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")
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

    End Sub

    Private Sub ddlDespesa_TaxaAereo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDespesa_TaxaAereo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT FL_PREMIACAO FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            ckbPremiacao_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_PREMIACAO")
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
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(VL_PESO_TARA,0)VL_PESO_TARA,ISNULL(NR_LACRE,0)NR_LACRE FROM TB_CNTR_BL WHERE ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            txtNumeroLacre_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_LACRE")
            txtValorTara_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TARA")
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

    Private Sub btnSalvarNCM_CargaMaritimo_Click(sender As Object, e As EventArgs) Handles btnSalvarNCM_CargaMaritimo.Click
        Dim id As String = rdNCM_CargaMaritimo.SelectedValue
        Dim nome As String = rdNCM_CargaMaritimo.SelectedItem.Text

        ddlNCM_CargaMaritimo.Items.Insert(1, nome)
        ddlNCM_CargaMaritimo.SelectedIndex = 1
        mpeNCM_CargaMaritimo.Hide()
    End Sub

    Private Sub btnSalvarNCM_CargaAereo_Click(sender As Object, e As EventArgs) Handles btnSalvarNCM_CargaAereo.Click
        Dim id As String = rdNCM_CargaAereo.SelectedValue
        Dim nome As String = rdNCM_CargaAereo.SelectedItem.Text

        ddlNCM_CargaAereo.Items.Insert(1, nome)
        ddlNCM_CargaAereo.SelectedIndex = 1
        mpeNCM_CargaAereo.Hide()

    End Sub

    Private Sub txtNCMFiltro_CargaMaritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNCMFiltro_CargaMaritimo.TextChanged
        mpeNCM_CargaMaritimo.Show()

    End Sub


    Private Sub txtNCMFiltro_CargaAereo_TextChanged(sender As Object, e As EventArgs) Handles txtNCMFiltro_CargaAereo.TextChanged
        mpeNCM_CargaAereo.Show()

    End Sub


    Function SeparaNCM(TEXTO As String) As String
        Dim ID As String = ""

        If TEXTO <> "" Then
            TEXTO = TEXTO.Substring(0, TEXTO.IndexOf("-"))

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet

            ds = Con.ExecutarQuery("SELECT ID_NCM FROM TB_NCM WHERE CD_NCM = '" & TEXTO & "'")
            If ds.Tables(0).Rows.Count > 0 Then
                ID = ds.Tables(0).Rows(0).Item("ID_NCM")
            Else
                ID = 0
            End If
        End If


        Return ID
    End Function


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

        If ddlServico_BasicoMaritimo.SelectedValue = 2 Then
            Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (FL_SHIPPER = 1 ) and (NM_RAZAO like '%" & txtNomeExportador_Aereo.Text & "%' or ID_PARCEIRO =  " & txtCodExportador_Aereo.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"
        ElseIf ddlServico_BasicoMaritimo.SelectedValue = 5 Then
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
            Session("ddlTipoPagamento_BasicoMaritimo") = ddlTipoPagamento_BasicoMaritimo.SelectedValue
            Session("ddlEstufagem_BasicoMaritimo") = ddlEstufagem_BasicoMaritimo.SelectedValue

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_STATUS_FRETE_AGENTE,ID_COTACAO FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                Session("ID_COTACAO") = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                Session("ID_STATUS_FRETE_AGENTE") = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString()
            End If

            Response.Redirect("CadastrarMaster.aspx?s=m")

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
            Session("ddlTipoPagamento_BasicoAereo") = ddlTipoPagamento_BasicoAereo.SelectedValue
            Session("ddlEstufagem_BasicoAereo") = ddlEstufagem_BasicoAereo.SelectedValue

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_STATUS_FRETE_AGENTE,ID_COTACAO FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                Session("ID_COTACAO") = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                Session("ID_STATUS_FRETE_AGENTE") = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString()
            End If


            Response.Redirect("CadastrarMaster.aspx?s=a")


        ElseIf Request.QueryString("tipo") = "h" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MBLAereo()", True)
        End If
    End Sub

    Sub NumeroProcessoMaster()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT NRSEQUENCIALPROCESSO, AnoSequencialProcesso FROM TB_PARAMETROS")

        Dim PROCESSO_FINAL As String

        Dim NRSEQUENCIALPROCESSO As Integer = ds.Tables(0).Rows(0).Item("NRSEQUENCIALPROCESSO")
        Dim AnoSequencialProcesso = ds.Tables(0).Rows(0).Item("AnoSequencialProcesso")
        Dim ano_atual = Now.Year.ToString.Substring(2)
        Dim SIGLA_PROCESSO As String
        Dim mes_atual As String
        If Now.Month < 10 Then
            mes_atual = "0" & Now.Month.ToString
        Else
            mes_atual = Now.Month.ToString
        End If


        If txtIDMaster_BasicoMaritimo.Text <> "" Then
            'MARITIMO

            ds = Con.ExecutarQuery("Select A.ID_SERVICO,(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO from TB_BL A Where ID_SERVICO <> 0 AND A.ID_BL = " & txtIDMaster_BasicoMaritimo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")

                If AnoSequencialProcesso = ano_atual Then

                    NRSEQUENCIALPROCESSO = NRSEQUENCIALPROCESSO + 1

                Else

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET AnoSequencialProcesso = '" & ano_atual & "' ")

                    NRSEQUENCIALPROCESSO = 1

                End If

                PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

                Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "'")

                Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = '" & PROCESSO_FINAL & "' WHERE ID_BL = " & txtIDMaster_BasicoMaritimo.Text)

            End If


        ElseIf txtIDMaster_BasicoAereo.Text <> "" Then
            'AEREO

            ds = Con.ExecutarQuery("Select A.ID_SERVICO,(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO from TB_BL A Where ID_SERVICO <> 0 AND A.ID_BL = " & txtIDMaster_BasicoAereo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")

                If AnoSequencialProcesso = ano_atual Then

                    NRSEQUENCIALPROCESSO = NRSEQUENCIALPROCESSO + 1

                Else

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET AnoSequencialProcesso = '" & ano_atual & "'")

                    NRSEQUENCIALPROCESSO = 1

                End If

                PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

                Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "'")

                Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = '" & PROCESSO_FINAL & "' WHERE ID_BL = " & txtIDMaster_BasicoAereo.Text)

            End If

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
End Class