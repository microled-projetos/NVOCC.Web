﻿Imports System.IO
Public Class CadastroCotacao
    Inherits System.Web.UI.Page
    Public imagemBase64Retorno As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        If Request.QueryString("id") <> "" Then

            If Not Page.IsPostBack Then
                Session("estufagem") = 0

                ddlTipoPagamentoTaxa.SelectedValue = 1
                dsDestinatarioComercial.DataBind()
                CarregaCampos()
            End If

        Else
            If txtID.Text = "" And Not Page.IsPostBack Then
                txtCotacaoTaxa.Text = txtNumeroCotacao.Text
                txtCotacaoMercadoria.Text = txtNumeroCotacao.Text
                ddlUsuarioStatus.SelectedValue = Session("ID_USUARIO")
                ddlAnalista.SelectedValue = Session("ID_USUARIO")
                txtAbertura.Text = Now.Date.ToString("dd/MM/yyyy")
                btnNovoFrete.Attributes.CssStyle.Add("display", "none")
                ddlStatusCotacao.SelectedValue = 16
                Session("ID_CLIENTE") = 0

            End If
        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub

    Sub CarregaCampos()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT A.ID_COTACAO,ISNULL(ID_FRETE_TRANSPORTADOR,0)ID_FRETE_TRANSPORTADOR,A.NR_COTACAO,ISNULL(ID_PORTO_DESTINO,0)ID_PORTO_DESTINO,ISNULL(ID_PORTO_ORIGEM,0)ID_PORTO_ORIGEM,ISNULL(ID_TRANSPORTADOR,0)ID_TRANSPORTADOR,ISNULL(A.ID_PARCEIRO_INDICADOR,0)ID_PARCEIRO_INDICADOR,DT_FOLLOWUP,
CONVERT(varchar,A.DT_ABERTURA,103)DT_ABERTURA,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR,
A.ID_STATUS_COTACAO,
CONVERT(varchar,A.DT_STATUS_COTACAO,103)DT_STATUS_COTACAO,
CONVERT(varchar,A.DT_VALIDADE_COTACAO,103)DT_VALIDADE_COTACAO,
CONVERT(varchar,A.DT_ENVIO_COTACAO,103)DT_ENVIO_COTACAO,
A.ID_ANALISTA_COTACAO,ISNULL(A.ID_AGENTE_INTERNACIONAL,0)ID_AGENTE_INTERNACIONAL,A.ID_TIPO_BL,A.ID_INCOTERM,A.ID_TIPO_ESTUFAGEM,ISNULL(A.ID_DESTINATARIO_COMERCIAL,0)ID_DESTINATARIO_COMERCIAL,ISNULL(A.ID_CLIENTE,0)ID_CLIENTE,ISNULL(A.ID_CLIENTE_FINAL,0)ID_CLIENTE_FINAL,A.ID_CONTATO,A.ID_SERVICO,A.ID_VENDEDOR,A.OB_CLIENTE,A.OB_MOTIVO_CANCELAMENTO,A.OB_OPERACIONAL,ISNULL(A.ID_MOTIVO_CANCELAMENTO,0)ID_MOTIVO_CANCELAMENTO,
CONVERT(varchar,A.DT_CALCULO_COTACAO,103)DT_CALCULO_COTACAO,ID_TIPO_CARGA, 
NR_PROCESSO_GERADO,ID_PROCESSO, ID_USUARIO_STATUS,(SELECT FL_COTACAO_APROVADA FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO = A.ID_STATUS_COTACAO )FL_COTACAO_APROVADA, (SELECT FL_ENCERRA_COTACAO FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO = A.ID_STATUS_COTACAO )FL_ENCERRA_COTACAO, ISNULL(FL_LTL,0)FL_LTL,ISNULL(FL_DTA_HUB,0)FL_DTA_HUB,ISNULL(FL_TRANSP_DEDICADO,0)FL_TRANSP_DEDICADO, VL_TOTAL_PESO_BRUTO,VL_TOTAL_M3, ISNULL(ID_PARCEIRO_RODOVIARIO,0)ID_PARCEIRO_RODOVIARIO, ISNULL(FL_EMAIL_COTACAO,0)FL_EMAIL_COTACAO,EMAIL_COTACAO,ISNULL([dbo].[FN_ANALISTA_COTACAO_PRICING](A.ID_COTACAO),0) AS ID_ANALISTA_COTACAO_PRICING,ISNULL(ID_TIPO_AERONAVE,0)ID_TIPO_AERONAVE, ISNULL(FL_TC4,0)FL_TC4,ISNULL(FL_TC6,0)FL_TC6 , ISNULL(VL_PESO_TAXADO,0)VL_PESO_TAXADO,ISNULL((SELECT ID_BL FROM TB_BL WHERE ID_COTACAO = A.ID_COTACAO AND GRAU = 'C' AND NR_PROCESSO = A.NR_PROCESSO_GERADO AND ISNULL(FL_CANCELADO,0) = 0 ),0)ID_BL 
FROM  TB_COTACAO A
    WHERE A.ID_COTACAO = " & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then

            'Informaçoes basicas
            txtID.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
            txtID_BL.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
            txtNumeroCotacao.Text = ds.Tables(0).Rows(0).Item("NR_COTACAO").ToString()
            ckbFreeHand.Checked = ds.Tables(0).Rows(0).Item("FL_FREE_HAND")
            ckbLTL.Checked = ds.Tables(0).Rows(0).Item("FL_LTL")
            ckbDtaHub.Checked = ds.Tables(0).Rows(0).Item("FL_DTA_HUB")
            ckbTC4.Checked = ds.Tables(0).Rows(0).Item("FL_TC4")
            ckbTC6.Checked = ds.Tables(0).Rows(0).Item("FL_TC6")
            ckbTranspDedicado.Checked = ds.Tables(0).Rows(0).Item("FL_TRANSP_DEDICADO")
            txtAbertura.Text = ds.Tables(0).Rows(0).Item("DT_ABERTURA").ToString()

            If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 10 Then
                Dim sql As String = "SELECT ID_STATUS_COTACAO, NM_STATUS_COTACAO FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO IN(7,8,15, " & ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO").ToString() & ")
union SELECT  0, 'Selecione' ORDER BY ID_STATUS_COTACAO"
                Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                If ds1.Tables(0).Rows.Count > 0 Then
                    dsStatusCotacao.SelectCommand = sql
                    ddlStatusCotacao.DataBind()
                End If
            End If

            If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 12 Or ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 9 Or ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 15 Then
                btnGravar.Enabled = False
                btnSalvarFrete.Visible = False
                btnSalvarTaxa.Visible = False
                btnSalvarMercadoria.Visible = False
                btnNovaMercadoria.Enabled = False
                btnNovaTaxa.Enabled = False
                btnDeletarTaxas.Enabled = False
                btnSelecionarTudo.Enabled = False
                btnImportar.Visible = False
                dgvMercadoria.Columns(8).Visible = False
                dgvTaxas.Columns(13).Visible = False
                btnGravarReferencia.Visible = False
                dgvReferencia.Columns(3).Visible = False
                dgvReferencia.Columns(4).Visible = False

            Else
                btnGravar.Enabled = True
                btnSalvarFrete.Visible = True
                btnSalvarTaxa.Visible = True
                btnSalvarMercadoria.Visible = True
                btnNovaMercadoria.Enabled = True
                btnNovaTaxa.Enabled = True
                btnDeletarTaxas.Enabled = True
                btnSelecionarTudo.Enabled = True
                btnImportar.Visible = True
                dgvMercadoria.Columns(8).Visible = True
                dgvTaxas.Columns(13).Visible = True
                btnGravarReferencia.Visible = True
                dgvReferencia.Columns(3).Visible = True
                dgvReferencia.Columns(4).Visible = True
            End If
            ddlStatusCotacao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO").ToString()

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_STATUS_COTACAO")) Then
                txtDataStatus.Text = ds.Tables(0).Rows(0).Item("DT_STATUS_COTACAO").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_FOLLOWUP")) Then
                txtDataFollowUp.Text = ds.Tables(0).Rows(0).Item("DT_FOLLOWUP").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_COTACAO")) Then
                txtValidade.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_COTACAO").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_ENVIO_COTACAO")) Then
                txtEnvio.Text = ds.Tables(0).Rows(0).Item("DT_ENVIO_COTACAO").ToString()
            End If

            txtCodIndicador.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString()
            ddlIndicador.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString()

            txtCodTransportador.Text = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
            Session("ID_TRANSPORTADOR") = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
            ddlTransportadorFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
            ddlFornecedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
            ddlDestinatarioComercial.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COMERCIAL").ToString()


            If ds.Tables(0).Rows(0).Item("ID_SERVICO").ToString() > 2 Then
                'EXPO
                ddlDestinatarioCobrancaTaxa.SelectedValue = 0
            Else
                'IMPO
                If ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString() <> 0 Then
                    ddlDestinatarioCobrancaTaxa.SelectedValue = 4

                Else
                    ddlDestinatarioCobrancaTaxa.SelectedValue = 1

                End If
            End If

            ckbEmailCotacao.Checked = ds.Tables(0).Rows(0).Item("FL_EMAIL_COTACAO")
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("EMAIL_COTACAO")) Then
                txtEmailCotacao.Text = ds.Tables(0).Rows(0).Item("EMAIL_COTACAO").ToString()
            End If

            txtCodAgente.Text = ds.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString()
            ddlAgente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString()
            ddlAnalista.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ANALISTA_COTACAO").ToString()
            ddlAnalistaPricing.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ANALISTA_COTACAO_PRICING").ToString()
            ddlIncoterm.SelectedValue = ds.Tables(0).Rows(0).Item("ID_INCOTERM").ToString()
            txtCodCliente.Text = ds.Tables(0).Rows(0).Item("ID_CLIENTE").ToString()
            ddlCliente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CLIENTE").ToString()
            txtCodExportador.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString()
            ddlExportador.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString()
            txtCodImportador.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString()
            ddlImportador.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString()
            txtCodTranspRodoviario.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO").ToString()
            ddlTranspRodoviario.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO").ToString()

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CLIENTE")) And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTATO")) Then

                Dim sql As String = "SELECT ID_CONTATO, NM_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & ds.Tables(0).Rows(0).Item("ID_CLIENTE") & " or ID_CONTATO = " & ds.Tables(0).Rows(0).Item("ID_CONTATO") & "
union SELECT  0, 'Selecione' ORDER BY ID_CONTATO"
                Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                If ds1.Tables(0).Rows.Count > 0 Then
                    dsContato.SelectCommand = sql
                    ddlContato.DataBind()
                End If
                Con.Fechar()

                ddlContato.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CONTATO")
            End If

            ddlServico.SelectedValue = ds.Tables(0).Rows(0).Item("ID_SERVICO").ToString()

            Session("ID_STATUS") = ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO").ToString()
            Session("estufagem") = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString()
            Session("servico") = ds.Tables(0).Rows(0).Item("ID_SERVICO").ToString()
            Session("ID_CLIENTE") = ds.Tables(0).Rows(0).Item("ID_CLIENTE").ToString()
            Session("RefTaxado") = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO").ToString()
            MaritimoXAereo()


            ddlDestinoFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString()
            ddlOrigemFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString()
            ddlTipoAeronave.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_AERONAVE").ToString()
            txtID_Vendedor.Text = ds.Tables(0).Rows(0).Item("ID_VENDEDOR").ToString
            dsVendedor.SelectParameters("ID_PARCEIRO").DefaultValue = ds.Tables(0).Rows(0).Item("ID_VENDEDOR").ToString
            ddlVendedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VENDEDOR").ToString

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CALCULO_COTACAO")) Then

                txtDataCalculo.Text = ds.Tables(0).Rows(0).Item("DT_CALCULO_COTACAO").ToString()
            End If

            ddlMotivoCancelamento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOTIVO_CANCELAMENTO").ToString()
            txtObsCancelamento.Text = ds.Tables(0).Rows(0).Item("OB_MOTIVO_CANCELAMENTO").ToString()
            txtObsCliente.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE").ToString()
            txtObsCliente.Text = txtObsCliente.Text.Replace("<br/>", vbNewLine)

            txtObsOperacional.Text = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL").ToString()
            ddlEstufagem.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString()

            If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                RedMoedaCompra.Visible = False
                RedValorTaxaCompra.Visible = False
            End If


            ddlUsuarioStatus.SelectedValue = ds.Tables(0).Rows(0).Item("ID_USUARIO_STATUS").ToString()
            txtProcessoCotacao.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO").ToString()


            dsClienteFinal.SelectCommand = "SELECT ID_CLIENTE_FINAL,SUBSTRING(NM_CLIENTE_FINAL,0,50) +' ('+ NR_CNPJ +')' NM_CLIENTE_FINAL  FROM TB_CLIENTE_FINAL WHERE (ID_CLIENTE_FINAL = " & ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL") & ") OR (ID_PARCEIRO = " & ds.Tables(0).Rows(0).Item("ID_CLIENTE") & " ) union SELECT  0, '    Selecione' ORDER BY NM_CLIENTE_FINAL"
            ddlClienteFinal.DataBind()
            divClienteFinal.Attributes.CssStyle.Add("display", "block")
            ddlClienteFinal.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL").ToString()


            ddlTipoBL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_BL").ToString()
            txtCotacaoTaxa.Text = txtNumeroCotacao.Text
            txtCotacaoMercadoria.Text = txtNumeroCotacao.Text

            Dim Linhas As Integer = dgvFrete.Rows.Count
            If Linhas > 0 Then
                btnNovoFrete.Attributes.CssStyle.Add("display", "none")
            End If

            If ds.Tables(0).Rows(0).Item("FL_ENCERRA_COTACAO") = True Then
                ddlStatusCotacao.Enabled = False
            End If


            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")) Then
                Dim sql As String = ""
                If ds.Tables(0).Rows(0).Item("ID_SERVICO").ToString() <= 2 And ds.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString() <> 0 Then
                    sql = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE  ( ID_FRETE_TRANSPORTADOR = " & ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString() & ") or (ID_PORTO_ORIGEM =  " & ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString() & " AND ID_PORTO_DESTINO = " & ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString() & "  AND ID_AGENTE = " & ds.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString() & " ) AND CONVERT(DATE,DT_VALIDADE_FINAL,103) >= CONVERT(DATE, GETDATE(),103) union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "
                Else
                    sql = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE  ( ID_FRETE_TRANSPORTADOR = " & ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString() & ") or (ID_PORTO_ORIGEM =  " & ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString() & " AND ID_PORTO_DESTINO = " & ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString() & " ) AND CONVERT(DATE,DT_VALIDADE_FINAL,103) >= CONVERT(DATE, GETDATE(),103) union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "

                End If

                Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                If ds1.Tables(0).Rows.Count > 0 Then
                    dsFreteTransportador.SelectCommand = sql
                    ddlFreteTransportador_Frete.DataBind()
                End If
                Con.Fechar()

                ddlFreteTransportador_Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")
                Session("ID_FRETE_TRANSPORTADOR") = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()
            Else
                Session("ID_FRETE_TRANSPORTADOR") = 0
            End If



            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_PESO_BRUTO")) Then
                Session("RefPesoSum") = ds.Tables(0).Rows(0).Item("VL_TOTAL_PESO_BRUTO").ToString()
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_M3")) Then
                Session("RefVolumeSum") = ds.Tables(0).Rows(0).Item("VL_TOTAL_M3").ToString()
            End If
            GridHistoricoCotacao()
            '  GridHistoricoFrete()

        End If
    End Sub


    Sub MaritimoXAereo()
        If Session("servico") = 2 Or Session("servico") = 5 Then
            'AEREO
            txtViaTransporte.Text = 4
            divPesoTaxadoCBM.Attributes.CssStyle.Add("display", "block")
            divCheckFrete.Attributes.CssStyle.Add("display", "block")
            divTTAereo.Attributes.CssStyle.Add("display", "block")
            divAereo.Attributes.CssStyle.Add("display", "block")
            divFlagAereo.Attributes.CssStyle.Add("display", "block")
            divFlagMaritimo.Attributes.CssStyle.Add("display", "none")
            divContratoMaritimo.Attributes.CssStyle.Add("display", "none")
            divCntr.Attributes.CssStyle.Add("display", "none")
            divMedidasMaritimo.Attributes.CssStyle.Add("display", "none")
            divCamposMaritimos.Attributes.CssStyle.Add("display", "none")
            divQtdMercadoria.Attributes.CssStyle.Add("display", "block")
            DivFreetime.Attributes.CssStyle.Add("display", "none")
            RedM3.Visible = False
            divMinimosFCL.Visible = False
            divTarifaSpot.Visible = False
            ddlEstufagem.SelectedValue = 2
            ddlTipoBL.SelectedValue = 2
            lblM3.Text = "Peso Cubado"
            modalFrete.InnerText = "ROTAS"
            lblAbaFrete.Text = "Rotas"
            modalMercaoria.InnerText = "EMBALAGEM/FRETE"
            lblAbaEmbalagem.Text = "Embalagem/Frete"
            lblTipoBL.Text = "Tipo AWB:"
            lblTaxaIncluded.Text = "Obs:"
            lblorigem.Text = "Aeroporto de Origem"
            lbldestino.Text = "Aeroporto de Destino"
            dsPorto.SelectCommand = "SELECT ID_PORTO, CONVERT(VARCHAR,CD_PORTO) + ' - ' + NM_PORTO AS NM_PORTO FROM [dbo].[TB_PORTO] WHERE ISNULL(FL_ATIVO,0)=1 AND NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = 4 union SELECT  0, '      Selecione' ORDER BY NM_PORTO "
            ddlOrigemFrete.DataBind()
            ddlDestinoFrete.DataBind()
            dsBaseCalculo.SelectCommand = "SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA] WHERE ID_VIATRANSPORTE <> 1 union SELECT  0, '   Selecione' ORDER BY NM_BASE_CALCULO_TAXA"


        Else
            'MARITIMO      
            lblTipoBL.Text = "Tipo BL:"
            lblTaxaIncluded.Text = "Taxas Included:"
            txtViaTransporte.Text = 1
            divCheckFrete.Attributes.CssStyle.Add("display", "none")
            divTTAereo.Attributes.CssStyle.Add("display", "none")
            divAereo.Attributes.CssStyle.Add("display", "none")
            divFlagAereo.Attributes.CssStyle.Add("display", "none")
            divMedidasAereo.Attributes.CssStyle.Add("display", "none")
            divPesoTaxadoCBM.Attributes.CssStyle.Add("display", "none")
            divCamposMaritimos.Attributes.CssStyle.Add("display", "block")
            divMedidasMaritimo.Attributes.CssStyle.Add("display", "block")
            divFlagMaritimo.Attributes.CssStyle.Add("display", "block")
            lblM3.Text = "Valor M3"
            modalFrete.InnerText = "FRETE"
            lblAbaFrete.Text = "Frete"
            modalMercaoria.InnerText = "EMBALAGEM"
            lblAbaEmbalagem.Text = "Embalagem"
            lblorigem.Text = "Porto de Origem"
            lbldestino.Text = "Porto de Destino"
            dsPorto.SelectCommand = "SELECT ID_PORTO, NM_PORTO + ' - ' +  CONVERT(VARCHAR,CD_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO] WHERE ISNULL(FL_ATIVO,0)=1 AND NM_PORTO IS NOT NULL AND ID_VIATRANSPORTE = 1 union SELECT  0, '      Selecione' ORDER BY NM_PORTO "
            ddlOrigemFrete.DataBind()
            ddlDestinoFrete.DataBind()
            dsBaseCalculo.SelectCommand = "SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA] WHERE  ID_VIATRANSPORTE <> 4 union SELECT  0, '   Selecione' ORDER BY NM_BASE_CALCULO_TAXA"

            If Session("estufagem") = 1 Then
                'FCL
                divCntr.Attributes.CssStyle.Add("display", "block")
                DivFreetime.Attributes.CssStyle.Add("display", "block")
                divContratoMaritimo.Attributes.CssStyle.Add("display", "block")
                divQtdMercadoria.Attributes.CssStyle.Add("display", "none")
                RedQTDContainer.Visible = True
                RedContainer.Visible = True

                RedQTDMercadoria.Visible = False
                RedPesoBruto.Visible = False
                RedM3.Visible = False
                divTarifaSpot.Visible = True
                divMinimosFCL.Visible = True
                divCompraMinimaLCL.Visible = False
                divVendaMinimaLCL.Visible = False


            ElseIf Session("estufagem") = 2 Then
                'LCL
                divCntr.Attributes.CssStyle.Add("display", "none")
                DivFreetime.Attributes.CssStyle.Add("display", "none")
                divContratoMaritimo.Attributes.CssStyle.Add("display", "none")
                divQtdMercadoria.Attributes.CssStyle.Add("display", "block")
                RedQTDMercadoria.Visible = True
                RedPesoBruto.Visible = True
                RedM3.Visible = True
                divTarifaSpot.Visible = False
                RedQTDContainer.Visible = False
                RedContainer.Visible = False
                RedFree.Visible = False

                divMinimosFCL.Visible = False
                divCompraMinimaLCL.Visible = True
                divVendaMinimaLCL.Visible = True

            End If
        End If


        If (Session("servico") <> 4) And (Session("servico") <> 5) Then
            'SE NAO FOR EXP
            dsDestinatarioCobranca.SelectCommand = "select ID_DESTINATARIO_COBRANCA,NM_DESTINATARIO_COBRANCA from TB_DESTINATARIO_COBRANCA WHERE ISNULL(TP_SERVICO,'') <> 'EXP' 
union SELECT  0, 'Selecione'  ORDER BY ID_DESTINATARIO_COBRANCA"
            ddlDestinatarioCobrancaTaxa.DataBind()
        Else
            'SE FOR EXP PODE EXIBIR TODOS OS TIPOS
            dsDestinatarioCobranca.DataBind()
            ddlDestinatarioCobrancaTaxa.DataBind()
        End If

    End Sub
    Protected Sub dgvHistoricoCotacao_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvHistoricoCotacao.DataSource = Session("TaskTable")
            dgvHistoricoCotacao.DataBind()
            dgvHistoricoCotacao.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    'Protected Sub dgvHistoricoFrete_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
    '    Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

    '    If dt IsNot Nothing Then
    '        dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
    '        Session("TaskTable") = dt
    '        dgvHistoricoFrete.DataSource = Session("TaskTable")
    '        dgvHistoricoFrete.DataBind()
    '        dgvHistoricoFrete.HeaderRow.TableSection = TableRowSection.TableHeader
    '    End If
    'End Sub

    Protected Sub dgvFrete_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvFrete.DataSource = Session("TaskTable")
            dgvFrete.DataBind()
            dgvFrete.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Protected Sub dgvMercadoria_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvMercadoria.DataSource = Session("TaskTable")
            dgvMercadoria.DataBind()
            dgvMercadoria.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Protected Sub dgvTaxas_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvTaxas.DataSource = Session("TaskTable")
            dgvTaxas.DataBind()
            dgvTaxas.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Private Sub btnFecharFrete_Click(sender As Object, e As EventArgs) Handles btnFecharFrete.Click
        divErroFrete.Visible = False
        divSuccessFrete.Visible = False
        dgvTaxas.DataBind()
        dgvFrete.DataBind()

        mpeNovoFrete.Hide()
    End Sub
    Private Sub btnFecharMercadoria_Click(sender As Object, e As EventArgs) Handles btnFecharMercadoria.Click
        divErroMercadoria.Visible = False
        divSuccessMercadoria.Visible = False
        ddlMercadoria.SelectedValue = 0
        ddlMoedaCarga.SelectedValue = 0
        ddlTipoContainerMercadoria.SelectedValue = 0
        txtQtdContainerMercadoria.Text = ""
        txtFreteCompraMercadoriaCalc.Text = ""
        txtFreteVendaMercadoriaCalc.Text = ""
        txtPesoBrutoMercadoria.Text = ""
        txtM3Mercadoria.Text = ""
        txtComprimentoMercadoria.Text = ""
        txtComprimentoMercadoria.Text = ""
        txtLarguraMercadoria.Text = ""
        txtAlturaMercadoria.Text = ""
        txtValorCargaMercadoria.Text = ""
        txtFreeTimeMercadoria.Text = ""
        txtQtdMercadoria.Text = ""
        txtDsMercadoria.Text = ""
        txtOutrasOBS_Mercadoria.Text = ""
        txtOBS_Endereco.Text = ""
        txtIDMercadoria.Text = ""
        txtFreteVendaMinima.Text = ""
        txtFreteCompraMinima.Text = ""
        txtFreteCompraMercadoriaUnitario.Text = ""
        txtFreteVendaMercadoriaUnitario.Text = ""
        btnAdicionarMedidasAereo.Visible = False
        txtAlturaMercadoriaAereo.Text = ""
        txtLarguraMercadoriaAereo.Text = ""
        txtComprimentoMercadoriaAereo.Text = ""
        txtQtdCaixasAereo.Text = ""
        divMedidasAereo.Attributes.CssStyle.Add("display", "none")
        txtPesoTaxadoMercadoria.Text = ""
        ddlProfitMercadoria.SelectedValue = 0
        ddlMoedaFreteMercadoria.SelectedValue = 0
        txtValorProfitMercadoria.Text = ""

        mpeNovoMercadoria.Hide()
    End Sub

    Sub LimparDadosTaxa()
        txtIDTaxa.Text = ""
        ddlItemDespesaTaxa.SelectedValue = 0
        ddlOrigemPagamentoTaxa.SelectedValue = 0
        ddlBaseCalculoTaxa.SelectedValue = 0
        ddlMoedaCompraTaxa.SelectedValue = 0
        ddlMoedaVendaTaxa.SelectedValue = 0
        ddlTipoPagamentoTaxa.SelectedValue = 1
        txtValorTaxaVenda.Enabled = True
        txtValorTaxaVendaMin.Enabled = True
        ddlMoedaVendaTaxa.Enabled = True
        ddlDestinatarioCobrancaTaxa.Enabled = True
        If ddlServico.SelectedValue > 2 Then
            'EXPO
            ddlDestinatarioCobrancaTaxa.SelectedValue = 0
        Else
            'IMPO
            If ddlImportador.SelectedValue <> 0 Then
                ddlDestinatarioCobrancaTaxa.SelectedValue = 4
            Else
                ddlDestinatarioCobrancaTaxa.SelectedValue = 1
            End If
        End If

        dsFornecedor.DataBind()
        ddlFornecedor.DataBind()
        ddlFornecedor.SelectedValue = ddlTransportadorFrete.SelectedValue

        ckbProfitTaxa.Checked = False
        ckbDeclaradoTaxa.Checked = False

        txtValorTaxaCompra.Text = ""
        txtValorTaxaVenda.Text = ""
        txtValorTaxaVendaMin.Text = ""
        txtValorTaxaCompraMin.Text = ""
        txtValorTaxaCompraCalc.Text = ""
        txtValorTaxaVendaCalc.Text = ""
        divErroTaxa.Visible = False
        divSuccessTaxa.Visible = False
        txtObsTaxa.Text = ""
        txtQtdBaseCalculo.Text = ""
        btnSalvarTaxa.Visible = True
        ddlMoedaVendaTaxa.Enabled = True
        txtValorTaxaVenda.Enabled = True
        txtValorTaxaVendaMin.Enabled = True
        ddlMoedaCompraTaxa.Enabled = True
        txtValorTaxaCompra.Enabled = True
        txtValorTaxaCompraMin.Enabled = True
        ddlDestinatarioCobrancaTaxa.Enabled = True
        ddlFornecedor.Enabled = True

    End Sub
    Private Sub btnFecharTaxa_Click(sender As Object, e As EventArgs) Handles btnFecharTaxa.Click
        LimparDadosTaxa()
        lkAnterior.Visible = False
        lkProximo.Visible = False
        mpeNovoTaxa.Hide()
    End Sub
    Private Function GetSortDirection(ByVal column As String) As String
        Dim sortDirection As String = "ASC"
        Dim sortExpression As String = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then

            If sortExpression = column Then
                Dim lastDirection As String = TryCast(ViewState("SortDirection"), String)

                If (lastDirection IsNot Nothing) AndAlso (lastDirection = "ASC") Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column
        Return sortDirection
    End Function
    Sub GridHistoricoCotacao()
        dsHistoricoCotacao.SelectCommand = "SELECT top " & txtQtd.Text & "
ID_COTACAO, 
NR_COTACAO, 
DT_ABERTURA,
ID_STATUS_COTACAO,
(SELECT NM_STATUS_COTACAO FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO = A.ID_STATUS_COTACAO) NM_STATUS_COTACAO,
ID_CLIENTE,
                    (SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_CLIENTE )AS CLIENTE,

ID_PORTO_ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM) Origem,
ID_PORTO_DESTINO, 
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) Destino
FROM TB_COTACAO A where ID_CLIENTE = " & Session("ID_CLIENTE") & " AND ID_TIPO_ESTUFAGEM = " & Session("estufagem")
        dgvHistoricoCotacao.DataBind()


        dsHistoricoCotacao.SelectParameters("ID_CLIENTE").DefaultValue = Session("ID_CLIENTE")
        dsHistoricoCotacao.SelectParameters("ID_TIPO_ESTUFAGEM").DefaultValue = Session("estufagem")

        dgvHistoricoCotacao.DataBind()
    End Sub

    'Sub GridHistoricoFrete()
    '    Try

    '        Dim Con As New Conexao_sql
    '        Con.Conectar()
    '        Dim ds As DataSet = Con.ExecutarQuery("SELECT CNPJ FROM TB_PARCEIRO  WHERE ID_PARCEIRO = " & Session("ID_CLIENTE"))

    '        If ds.Tables(0).Rows.Count > 0 Then
    '            txtcnpj.Text = ds.Tables(0).Rows(0).Item("CNPJ").ToString()
    '        Else
    '            txtcnpj.Text = 0

    '        End If

    '        dsHistoricoFrete.SelectCommand = "SELECT * FROM VW_VALOR_FRETE_LOTE where rownum <= " & txtQtd.Text & " and cnpj = '" & txtcnpj.Text & "' "
    '        dgvHistoricoFrete.DataBind()

    '        dsHistoricoFrete.SelectParameters("cnpj").DefaultValue = txtcnpj.Text
    '        dgvHistoricoFrete.DataBind()

    '    Catch ex As Exception
    '        Session("erro") = ex.ToString
    '        Response.Redirect("Erro.aspx")
    '    End Try
    'End Sub

    Private Sub dgvFrete_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFrete.RowCommand
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()

        If e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument
            divErroFrete.Visible = False
            divSuccessFrete.Visible = False
            divInfoFrete.Visible = False
            ds = Con.ExecutarQuery("
SELECT ID_COTACAO,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TIPO_PAGAMENTO,ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN ,VL_TOTAL_FRETE_COMPRA_MIN,TRANSITTIME_TRUCKING_AEREO ,FINAL_DESTINATION,ISNULL(FL_FRETE_DECLARADO,0)FL_FRETE_DECLARADO,ISNULL(FL_FRETE_PROFIT,0)FL_FRETE_PROFIT,NR_CONTRATO_ARMADOR,ID_TIPO_AERONAVE, ISNULL(ID_AGENTE_INTERNACIONAL,0)ID_AGENTE_INTERNACIONAL FROM TB_COTACAO WHERE ID_COTACAO = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Frete
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                    ddlOrigemFrete.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                End If

                ckFreteDeclarado.Checked = ds.Tables(0).Rows(0).Item("FL_FRETE_DECLARADO")
                ckFreteProfit.Checked = ds.Tables(0).Rows(0).Item("FL_FRETE_PROFIT")

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                    ddlDestinoFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_AERONAVE")) Then
                    ddlTipoAeronave.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_AERONAVE")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA1")) Then
                    ddlEscala1Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA1")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA2")) Then
                    ddlEscala2Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA2")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA3")) Then
                    ddlEscala3Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA3")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_FREQUENCIA")) Then
                    ddlFrequenciaFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FREQUENCIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FREQUENCIA")) Then
                    txtValorFrequenciaFrete.Text = ds.Tables(0).Rows(0).Item("VL_FREQUENCIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE")) Then
                    ddlMoedaFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
                    txtPesoTaxadoFrete.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_FINAL")) Then
                    txtTTimeFreteFinal.Text = ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_FINAL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_INICIAL")) Then
                    txtTTimeFreteInicial.Text = ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_INICIAL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_INICIAL")) And Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_FINAL")) Then
                    Dim INICIAL As Integer = ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_INICIAL")
                    Dim FINAL As Integer = ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_FINAL")
                    Dim MEDIA As Integer = INICIAL + FINAL
                    MEDIA = MEDIA / 2
                    txtTTimeFreteMedia.Text = MEDIA
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TRANSITTIME_TRUCKING_AEREO")) Then
                    txtTTimeFreteTruckingAereo.Text = ds.Tables(0).Rows(0).Item("TRANSITTIME_TRUCKING_AEREO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_BL")) Then
                    ddlTipoBL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_BL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TAXAS_INCLUDED")) Then
                    txtIncludedFrete.Text = ds.Tables(0).Rows(0).Item("NM_TAXAS_INCLUDED")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CONTRATO_ARMADOR")) Then
                    txtContratoArmador.Text = ds.Tables(0).Rows(0).Item("NR_CONTRATO_ARMADOR").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")) Then



                    Dim sql As String = ""

                    If ddlServico.SelectedValue <= 2 And ddlAgente.SelectedValue <> 0 Then
                        sql = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE  ( ID_FRETE_TRANSPORTADOR = " & ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString() & ") or (ID_PORTO_ORIGEM =  " & ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString() & " AND ID_PORTO_DESTINO = " & ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString() & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & "  AND ID_AGENTE = " & ddlAgente.SelectedValue & ") AND CONVERT(DATE,DT_VALIDADE_FINAL,103) >= CONVERT(DATE, GETDATE(),103) union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "

                    Else
                        sql = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE  ( ID_FRETE_TRANSPORTADOR = " & ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString() & ") or (ID_PORTO_ORIGEM =  " & ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString() & " AND ID_PORTO_DESTINO = " & ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString() & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & " ) AND CONVERT(DATE,DT_VALIDADE_FINAL,103) >= CONVERT(DATE, GETDATE(),103) union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "

                    End If


                    Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                    If ds1.Tables(0).Rows.Count > 0 Then
                        dsFreteTransportador.SelectCommand = sql
                        ddlFreteTransportador_Frete.DataBind()
                    End If
                    Con.Fechar()

                    ddlFreteTransportador_Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")
                    Session("ID_FRETE_TRANSPORTADOR") = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")

                    If ddlFreteTransportador_Frete.SelectedValue <> 0 Then
                        ddlMoedaFrete.Enabled = "False"
                        txtTTimeFreteInicial.Enabled = "False"
                        txtTTimeFreteFinal.Enabled = "False"
                    Else
                        ddlMoedaFrete.Enabled = "True"
                        txtTTimeFreteInicial.Enabled = "True"
                        txtTTimeFreteFinal.Enabled = "True"
                    End If
                End If



                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA")) Then
                    txtFreteCompra.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA")) Then
                    txtFreteVenda.Enabled = False
                    txtFreteVenda.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA")
                    'Else
                    '    txtFreteVenda.Enabled = True

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE")) Then
                    ddlDivisaoProfit.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_DIVISAO_FRETE")) Then
                    txtValorDivisaoProfit.Text = ds.Tables(0).Rows(0).Item("VL_DIVISAO_FRETE")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR")) Then
                    txtCodTransportador.Text = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
                    ddlTransportadorFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                    ddlTipoCargaFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VIA_ROTA")) Then
                    ddlRotaFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VIA_ROTA")
                    If ds.Tables(0).Rows(0).Item("ID_VIA_ROTA") = 1 Then
                        divEscala.Attributes.CssStyle.Add("display", "none")
                    ElseIf ds.Tables(0).Rows(0).Item("ID_VIA_ROTA") = 2 Then
                        divEscala.Attributes.CssStyle.Add("display", "block")
                    End If
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                    ddlEstufagemFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    ddlTipoPagamento_Frete.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FINAL_DESTINATION")) Then
                    ddlFinalDestination.Text = ds.Tables(0).Rows(0).Item("FINAL_DESTINATION")
                End If

                mpeNovoFrete.Show()

            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub dgvTaxas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxas.RowCommand
        divDeleteTaxas.Visible = False
        divDeleteErroTaxas.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument


            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblDeleteErroTaxas.Text = "Usuário não tem permissão para realizar exclusões"
                divDeleteErroTaxas.Visible = True
            Else


                If ddlStatusCotacao.SelectedValue = 12 Or ddlStatusCotacao.SelectedValue = 15 Or ddlStatusCotacao.SelectedValue = 9 Then
                    lblDeleteErroTaxas.Text = "Status da cotação não permite realizar exclusões!"
                    divDeleteErroTaxas.Visible = True
                Else
                    Dim finaliza As New FinalizaCotacao
                    If finaliza.TaxaBloqueada(ID, "COTACAO") = True Then
                        lblDeleteErroTaxas.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber ou invoice!"
                        divDeleteErroTaxas.Visible = True
                    Else
                        ds = Con.ExecutarQuery("SELECT ID_STATUS_COTACAO,NR_PROCESSO_GERADO FROM TB_COTACAO WHERE ID_COTACAO =" & txtID.Text)
                        If ds.Tables(0).Rows.Count > 0 Then
                            Con.ExecutarQuery("DELETE From TB_COTACAO_TAXA Where ID_COTACAO_TAXA = " & ID)
                            lblDeleteTaxas.Text = "Registro deletado!"
                            divDeleteTaxas.Visible = True
                            dgvTaxas.DataBind()
                            If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 10 And Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO")) Then
                                Dim RotinaUpdate As New RotinaUpdate
                                RotinaUpdate.DeletaTaxas(txtID.Text, ID, txtProcessoCotacao.Text)
                            End If
                        End If
                    End If
                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument
            dsFornecedor.DataBind()
            ddlFornecedor.DataBind()

            ds = Con.ExecutarQuery("SELECT A.ID_COTACAO_TAXA,A.ID_FORNECEDOR,
A.ID_COTACAO,
A.ID_ITEM_DESPESA,
A.ID_TIPO_PAGAMENTO,
A.ID_ORIGEM_PAGAMENTO,
A.FL_DECLARADO,
A.FL_DIVISAO_PROFIT,
A.ID_DESTINATARIO_COBRANCA,
A.ID_MOEDA_COMPRA,
A.VL_TAXA_COMPRA_CALCULADO,
A.ID_MOEDA_VENDA,
A.VL_TAXA_VENDA_CALCULADO,
A.ID_BASE_CALCULO_TAXA,
A.OB_TAXAS,
A.VL_TAXA_VENDA_MIN,
A.VL_TAXA_COMPRA,
A.VL_TAXA_VENDA,
A.VL_TAXA_COMPRA_MIN,
A.QTD_BASE_CALCULO,
ISNULL(I.FL_PREMIACAO,0)FL_PREMIACAO 
FROM TB_COTACAO_TAXA A
LEFT JOIN TB_ITEM_DESPESA I ON A.ID_ITEM_DESPESA = I.ID_ITEM_DESPESA 
WHERE A.ID_COTACAO_TAXA = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                lkAnterior.Visible = True
                lkProximo.Visible = True

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")) Then
                    txtIDTaxa.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_FORNECEDOR")) Then
                    ddlFornecedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_FORNECEDOR")
                End If

                ckbDeclaradoTaxa.Checked = ds.Tables(0).Rows(0).Item("FL_DECLARADO")
                ckbProfitTaxa.Checked = ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    ddlTipoPagamentoTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                    ddlOrigemPagamentoTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                    ddlDestinatarioCobrancaTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculoTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")

                    If ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 38 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 40 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 41 Then
                        txtQtdBaseCalculo.Enabled = True
                    Else
                        txtQtdBaseCalculo.Enabled = False
                    End If

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")) Then
                    txtQtdBaseCalculo.Text = ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")) Then
                    ddlMoedaCompraTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")) Then
                    txtValorTaxaCompra.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")) Then
                    txtValorTaxaCompraMin.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")) Then
                    txtValorTaxaCompraCalc.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")) Then
                    ddlMoedaVendaTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA")) Then
                    txtValorTaxaVenda.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")) Then
                    txtValorTaxaVendaMin.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")) Then
                    txtValorTaxaVendaCalc.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                    txtObsTaxa.Text = ds.Tables(0).Rows(0).Item("OB_TAXAS")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                    ddlItemDespesaTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                End If

                Dim finaliza As New FinalizaCotacao
                If finaliza.TaxaBloqueada(ID, "COTACAO", "R") = True Then
                    ddlMoedaVendaTaxa.Enabled = False
                    txtValorTaxaVenda.Enabled = False
                    txtValorTaxaVendaMin.Enabled = False
                    txtValorTaxaVendaCalc.Enabled = False
                    ddlDestinatarioCobrancaTaxa.Enabled = False
                End If

                If finaliza.TaxaBloqueada(ID, "COTACAO", "P") = True Then
                    ddlMoedaCompraTaxa.Enabled = False
                    txtValorTaxaCompra.Enabled = False
                    txtValorTaxaCompraMin.Enabled = False
                    txtValorTaxaCompraCalc.Enabled = False
                    ddlFornecedor.Enabled = False
                End If

                If ds.Tables(0).Rows(0).Item("FL_PREMIACAO") = True Then
                    ddlMoedaVendaTaxa.Enabled = False
                    txtValorTaxaVenda.Enabled = False
                    txtValorTaxaVendaMin.Enabled = False
                    txtValorTaxaVendaCalc.Enabled = False
                    ddlDestinatarioCobrancaTaxa.Enabled = False
                End If

                If ddlStatusCotacao.SelectedValue = 12 Or ddlStatusCotacao.SelectedValue = 15 Or ddlStatusCotacao.SelectedValue = 9 Then
                    btnSalvarTaxa.Visible = False
                Else
                    btnSalvarTaxa.Visible = True
                End If



                mpeNovoTaxa.Show()

            End If
        End If
        Con.Fechar()

    End Sub

    Private Sub dgvMercadoria_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvMercadoria.RowCommand
        divDeleteErroMercadoria.Visible = False
        divDeleteMercadoria.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ID As String = e.CommandArgument
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblDeleteErroMercadoria.Text = "Usuário não tem permissão para realizar exclusões"
                divDeleteErroMercadoria.Visible = True
            Else
                Con.ExecutarQuery("DELETE From TB_COTACAO_MERCADORIA Where ID_COTACAO_MERCADORIA = " & ID)
                Con.ExecutarQuery("DELETE From TB_COTACAO_MERCADORIA_DIMENSAO Where ID_COTACAO_MERCADORIA = " & ID)
                sumMedidasAereo(ID)
                lblDeleteMercadoria.Text = "Registro deletado!"
                divDeleteMercadoria.Visible = True
                dgvMercadoria.DataBind()
                AtualizaFreteMercadoria()
                ds = Con.ExecutarQuery("SELECT ID_STATUS_COTACAO,NR_PROCESSO_GERADO FROM TB_COTACAO WHERE ID_COTACAO =" & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 10 And Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO")) Then
                        Dim RotinaUpdate As New RotinaUpdate
                        RotinaUpdate.UpdateCarga(txtID.Text, ID, txtProcessoCotacao.Text, Session("RefPeso"), Session("RefVolume"), Session("RefPesoSum"), Session("RefVolumeSum"))
                        RotinaUpdate.DeletaCarga(txtID.Text, ID, txtProcessoCotacao.Text)
                    End If
                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT 
A.ID_COTACAO_MERCADORIA,A.ID_COTACAO,A.ID_MERCADORIA,A.ID_TIPO_CONTAINER,A.QT_CONTAINER,A.VL_FRETE_COMPRA,VL_FRETE_COMPRA_UNITARIO,VL_FRETE_VENDA_UNITARIO,
A.VL_FRETE_VENDA,A.VL_PESO_BRUTO,A.VL_M3,A.OUTRAS_OBS,A.DS_MERCADORIA,A.VL_COMPRIMENTO,A.VL_LARGURA,A.VL_ALTURA,A.VL_CARGA,A.QT_DIAS_FREETIME,A.QT_MERCADORIA,A.VL_FRETE_COMPRA_MIN,A.VL_FRETE_VENDA_MIN,OBS_ENDERECO, ISNULL(ID_MOEDA_CARGA,0)ID_MOEDA_CARGA, B.VL_PESO_TAXADO,B.ID_MOEDA_FRETE,B.ID_TIPO_DIVISAO_FRETE, B.VL_DIVISAO_FRETE, B.FL_FRETE_DECLARADO ,B.FL_FRETE_PROFIT,VL_CBM, ISNULL(B.FL_TARIFA_SPOT,0)FL_TARIFA_SPOT 
FROM TB_COTACAO_MERCADORIA A
INNER JOIN TB_COTACAO B ON A.ID_COTACAO = B.ID_COTACAO
WHERE A.ID_COTACAO_MERCADORIA = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_TARIFA_SPOT")) Then
                    ckTarifaSpot.Checked = ds.Tables(0).Rows(0).Item("FL_TARIFA_SPOT")
                End If

                If Session("servico") = 2 Or Session("servico") = 5 Then
                    btnAdicionarMedidasAereo.Visible = True
                    divMedidasAereo.Attributes.CssStyle.Add("display", "block")

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
                        txtPesoTaxadoMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE")) Then
                        ddlMoedaFreteMercadoria.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE")) Then
                        ddlProfitMercadoria.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_DIVISAO_FRETE")) Then
                        txtValorProfitMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_DIVISAO_FRETE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_FRETE_DECLARADO")) Then
                        ckFreteDeclarado.Checked = ds.Tables(0).Rows(0).Item("FL_FRETE_DECLARADO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_FRETE_PROFIT")) Then
                        ckFreteProfit.Checked = ds.Tables(0).Rows(0).Item("FL_FRETE_PROFIT")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_CBM")) Then
                        txtCBMAereo.Text = ds.Tables(0).Rows(0).Item("VL_CBM")
                    End If

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_COTACAO_MERCADORIA")) Then
                    txtIDMercadoria.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MERCADORIA")) Then
                    ddlMercadoria.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER")) Then
                    ddlTipoContainerMercadoria.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_CONTAINER")) Then
                    txtQtdContainerMercadoria.Text = ds.Tables(0).Rows(0).Item("QT_CONTAINER")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA_CARGA")) Then
                    ddlMoedaCarga.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_CARGA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FRETE_VENDA_UNITARIO")) Then
                    txtFreteVendaMercadoriaUnitario.Text = ds.Tables(0).Rows(0).Item("VL_FRETE_VENDA_UNITARIO")
                End If



                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FRETE_COMPRA")) Then
                    txtFreteCompraMercadoriaCalc.Text = ds.Tables(0).Rows(0).Item("VL_FRETE_COMPRA")

                    If ds.Tables(0).Rows(0).Item("VL_FRETE_COMPRA") = 0 Then

                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT QT_DIAS_FREETIME, isnull(VL_COMPRA,0)VL_COMPRA from TB_TARIFARIO_FRETE_TRANSPORTADOR where ID_FRETE_TRANSPORTADOR = (SELECT ID_FRETE_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") AND ID_TIPO_CONTAINER = " & ds.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER") & " AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and  convert(date,DT_VALIDADE_FINAL,103)")

                        If ds1.Tables(0).Rows.Count > 0 Then

                            If Not IsDBNull(ds1.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")) Then
                                txtFreeTimeMercadoria.Text = ds1.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")
                            End If

                            If Not IsDBNull(ds1.Tables(0).Rows(0).Item("VL_COMPRA")) Then
                                txtFreteCompraMercadoriaUnitario.Text = ds1.Tables(0).Rows(0).Item("VL_COMPRA")
                                If txtQtdContainerMercadoria.Text > 0 Then

                                    txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text
                                Else
                                    txtFreteCompraMercadoriaCalc.Text = ds1.Tables(0).Rows(0).Item("VL_COMPRA")
                                End If

                            End If
                        Else

                            txtFreteCompraMercadoriaCalc.Text = ""
                            txtFreteCompraMercadoriaUnitario.Text = ""
                            txtFreeTimeMercadoria.Text = ""
                        End If


                    End If

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FRETE_COMPRA_UNITARIO")) Then
                    txtFreteCompraMercadoriaUnitario.Text = ds.Tables(0).Rows(0).Item("VL_FRETE_COMPRA_UNITARIO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")) Then
                    If ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME") <> 0 Then
                        txtFreeTimeMercadoria.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")
                    End If
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FRETE_VENDA")) Then
                    txtFreteVendaMercadoriaCalc.Text = ds.Tables(0).Rows(0).Item("VL_FRETE_VENDA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                    txtPesoBrutoMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                    Session("RefPeso") = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_M3")) Then
                    txtM3Mercadoria.Text = ds.Tables(0).Rows(0).Item("VL_M3")
                    Session("RefVolume") = ds.Tables(0).Rows(0).Item("VL_M3")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMPRIMENTO")) Then
                    txtComprimentoMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_COMPRIMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_ALTURA")) Then
                    txtAlturaMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_ALTURA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_LARGURA")) Then
                    txtLarguraMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_LARGURA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_CARGA")) Then
                    txtValorCargaMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_CARGA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DS_MERCADORIA")) Then
                    txtDsMercadoria.Text = ds.Tables(0).Rows(0).Item("DS_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("OUTRAS_OBS")) Then
                    txtOutrasOBS_Mercadoria.Text = ds.Tables(0).Rows(0).Item("OUTRAS_OBS")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_MERCADORIA")) Then
                    txtQtdMercadoria.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FRETE_VENDA_MIN")) Then
                    txtFreteVendaMinima.Text = ds.Tables(0).Rows(0).Item("VL_FRETE_VENDA_MIN")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FRETE_COMPRA_MIN")) Then
                    txtFreteCompraMinima.Text = ds.Tables(0).Rows(0).Item("VL_FRETE_COMPRA_MIN")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("OBS_ENDERECO")) Then
                    txtOBS_Endereco.Text = ds.Tables(0).Rows(0).Item("OBS_ENDERECO")
                End If

                mpeNovoMercadoria.Show()

            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("CadastroCotacao.aspx")
    End Sub
    Function NumeroCotacao() As String

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Cotacao_" & Now.Year.ToString & " NRSEQUENCIALCOTACAO")
        Dim NR_COTACAO As String = ds.Tables(0).Rows(0).Item("NRSEQUENCIALCOTACAO")
        Dim ano_atual = Now.Year.ToString.Substring(2)

        Session("NR_COTACAO") = NR_COTACAO
        NR_COTACAO = NR_COTACAO.PadLeft(7, "0") & "/" & ano_atual

        Con.Fechar()


        Return NR_COTACAO
    End Function
    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        If ddlStatusCotacao.SelectedValue = 9 Or ddlStatusCotacao.SelectedValue = 15 Then
            mpeEnvioSI.Show()
        Else
            Gravar()
        End If
    End Sub

    Sub Gravar()
        lblmsgSuccess.Text = "Registro cadastrado/atualizado com sucesso!"
        diverro.Visible = False
        divsuccess.Visible = False
        divinfo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim PROCESSO As String = ""
        Dim v As New VerificaData

        ' txtAbertura.Text = txtAbertura.Text.Replace("-", "/")

        If txtAbertura.Text = "" Or ddlStatusCotacao.SelectedValue = 0 Or ddlUsuarioStatus.SelectedValue = 0 Or txtValidade.Text = "" Or ddlDestinatarioComercial.SelectedValue = 0 Or ddlAnalista.SelectedValue = 0 Or ddlCliente.SelectedValue = 0 Or ddlIncoterm.SelectedValue = 0 Or ddlEstufagem.SelectedValue = 0 Or ddlTipoBL.SelectedValue = 0 Or ddlServico.SelectedValue = 0 Or ddlVendedor.SelectedValue = 0 Then
            ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
            lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Informações Básicas."
            diverro.Visible = True

        ElseIf v.ValidaData(txtAbertura.Text) = False Or IsDate(txtAbertura.Text) = False Then
            ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
            diverro.Visible = True
            lblmsgErro.Text = "A data de abertura é inválida."

        ElseIf v.ValidaData(txtValidade.Text) = False Or IsDate(txtValidade.Text) = False Then
            ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
            diverro.Visible = True
            lblmsgErro.Text = "A data de validade é inválida."

        ElseIf txtProcessoCotacao.Text = "" And ddlStatusCotacao.SelectedValue = 10 Then
            ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
            diverro.Visible = True
            lblmsgErro.Text = "Apenas cotações com número de processo gerado podem ser colocadas em update!"


        ElseIf ddlAgente.SelectedValue = 0 And (ddlStatusCotacao.SelectedValue = 9 Or ddlStatusCotacao.SelectedValue = 15) Then
            ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
            diverro.Visible = True
            lblmsgErro.Text = "Apenas cotações com agente preechido podem ser aprovadas!"

        ElseIf (ddlStatusCotacao.SelectedValue = 9 Or ddlStatusCotacao.SelectedValue = 15) And txtDataCalculo.Text = "" Then
            ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
            diverro.Visible = True
            lblmsgErro.Text = "Necessário calcular cotação!"
            Exit Sub



        Else



            If txtDataFollowUp.Text <> "" Then
                If v.ValidaData(txtDataFollowUp.Text) = False Or IsDate(txtDataFollowUp.Text) = False Then
                    ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
                    lblmsgErro.Text = "A data de FollowUp é inválida."
                    diverro.Visible = True
                    Exit Sub
                Else
                    txtDataFollowUp.Text = "CONVERT(date,'" & txtDataFollowUp.Text & "',103)"
                End If
            Else
                txtDataFollowUp.Text = "NULL"
            End If

            Dim dsContatos As DataSet = Con.ExecutarQuery("SELECT ID_CONTATO, NM_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue & "
union SELECT  0, 'Selecione' ORDER BY ID_CONTATO")
            If dsContatos.Tables(0).Rows.Count > 1 And ddlContato.SelectedValue = 0 Then
                ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
                lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Informações Básicas."
                diverro.Visible = True
                Exit Sub
            End If

            Dim ObsCliente As String = ""
            Dim ObsCancelamento As String = ""
            Dim ObsOperacional As String = ""
            Dim EmailCotacao As String = ""

            If txtObsCliente.Text = "" Then
                ObsCliente = "NULL"
            Else
                ObsCliente = txtObsCliente.Text
                ObsCliente = ObsCliente.Replace("'", "''")
                ObsCliente = ObsCliente.Replace(vbNewLine, "<br/>")
                ObsCliente = "'" & ObsCliente & "'"
            End If

            If txtObsCancelamento.Text = "" Then
                ObsCancelamento = "NULL"
            Else
                ObsCancelamento = txtObsCancelamento.Text
                ObsCancelamento = ObsCancelamento.Replace("'", "''")
                ObsCancelamento = "'" & ObsCancelamento & "'"
            End If

            If txtObsOperacional.Text = "" Then
                ObsOperacional = "NULL"
            Else
                ObsOperacional = txtObsOperacional.Text
                ObsOperacional = ObsOperacional.Replace("'", "''")
                ObsOperacional = "'" & ObsOperacional & "'"
            End If

            If txtEmailCotacao.Text = "" Then
                EmailCotacao = "NULL"
            Else
                EmailCotacao = txtEmailCotacao.Text
                EmailCotacao = EmailCotacao.Replace("'", "''")
                EmailCotacao = "'" & EmailCotacao & "'"
            End If


            If txtID.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
                    diverro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else

                    If ddlTipoPagamento_Frete.SelectedValue = 0 And (ddlStatusCotacao.SelectedValue = 9 Or ddlStatusCotacao.SelectedValue = 15) Then
                        ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
                        lblmsgErro.Text = "Apenas cotações com tipo de frete preechido podem ser aprovadas!"
                        diverro.Visible = True
                        Exit Sub
                    End If

                    Session("estufagem") = ddlEstufagem.SelectedValue
                    Session("servico") = ddlServico.SelectedValue



                    txtEnvio.Text = Now.Date.ToString("dd-MM-yyyy")
                    txtDataStatus.Text = Now.Date.ToString("dd-MM-yyyy")

                    'INSERE              
                    txtNumeroCotacao.Text = NumeroCotacao()

                    If txtNumeroCotacao.Text = "" Then
                        ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
                        lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Informações Básicas."
                        diverro.Visible = True
                        Exit Sub

                    End If


                    ds = Con.ExecutarQuery("INSERT INTO TB_COTACAO (NR_COTACAO,ID_TIPO_BL, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_TIPO_ESTUFAGEM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, ID_USUARIO_STATUS, DT_VALIDADE_COTACAO,FL_FREE_HAND,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR,FL_LTL,FL_DTA_HUB,FL_TRANSP_DEDICADO,DT_FOLLOWUP,ID_PARCEIRO_RODOVIARIO,FL_EMAIL_COTACAO,EMAIL_COTACAO,FL_TC4,FL_TC6 ) VALUES ('" & txtNumeroCotacao.Text & "'," & ddlTipoBL.SelectedValue & ", getdate(), " & ddlStatusCotacao.SelectedValue & ", Convert(datetime, '" & txtDataStatus.Text & "', 103)," & ddlAnalista.SelectedValue & ", " & ddlAgente.SelectedValue & "," & ddlIncoterm.SelectedValue & "," & ddlEstufagem.SelectedValue & ", " & ddlDestinatarioComercial.SelectedValue & "," & ddlCliente.SelectedValue & "," & ddlClienteFinal.SelectedValue & ", " & ddlContato.SelectedValue & " , " & ddlServico.SelectedValue & " , " & ddlVendedor.SelectedValue & "," & ObsCliente & "," & ObsCancelamento & "," & ObsOperacional & "," & ddlMotivoCancelamento.SelectedValue & "," & ddlUsuarioStatus.SelectedValue & ",Convert(datetime, '" & txtValidade.Text & "', 103),'" & ckbFreeHand.Checked & "', " & ddlIndicador.SelectedValue & "," & ddlExportador.SelectedValue & "," & ddlImportador.SelectedValue & ",'" & ckbLTL.Checked & "','" & ckbDtaHub.Checked & "','" & ckbTranspDedicado.Checked & "'," & txtDataFollowUp.Text & "," & ddlTranspRodoviario.SelectedValue & ",'" & ckbEmailCotacao.Checked & "'," & EmailCotacao & ",'" & ckbTC4.Checked & "','" & ckbTC6.Checked & "' ) Select SCOPE_IDENTITY() as ID_COTACAO ")

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALCOTACAO =  " & Session("NR_COTACAO") & ", ANOSEQUENCIALCOTACAO = YEAR(GETDATE())")


                    'PREENCHE SESSÃO E CAMPO DE ID
                    Session("ID_COTACAO") = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                    txtID.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                    Session("ID_CLIENTE") = ddlCliente.SelectedValue
                    Session("ID_STATUS") = ddlStatusCotacao.SelectedValue
                    Session("ID_FRETE_TRANSPORTADOR") = 0

                    txtDataFollowUp.Text = txtDataFollowUp.Text.Replace("NULL", "")
                    txtDataFollowUp.Text = txtDataFollowUp.Text.Replace("CONVERT(varchar,'", "")
                    txtDataFollowUp.Text = txtDataFollowUp.Text.Replace("',103)", "")

                    divsuccess.Visible = True
                    dgvFrete.DataBind()
                    ' dgvHistoricoFrete.DataBind()
                    dgvHistoricoCotacao.DataBind()

                    VerificaRepetida()
                    AtualizaPortoAgenteSI()

                    Con.Fechar()

                End If



            Else

                If (ddlStatusCotacao.SelectedValue = 9 Or ddlStatusCotacao.SelectedValue = 15) And ValorMinimoPendente(txtID.Text) = True Then
                    ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
                    diverro.Visible = True
                    lblmsgErro.Text = "Cotação contém taxa(s) com valor minimo vazio!"
                    Exit Sub
                End If


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
                    diverro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else

                    Dim d1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_TIPO_PAGAMENTO,0)ID_TIPO_PAGAMENTO,DT_VALIDADE_COTACAO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)
                    If d1.Tables(0).Rows.Count > 0 Then

                        If d1.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO") = 0 And (ddlStatusCotacao.SelectedValue = 9 Or ddlStatusCotacao.SelectedValue = 15) Then
                            ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
                            lblmsgErro.Text = "Apenas cotações com tipo de frete preechido podem ser aprovadas!"
                            diverro.Visible = True

                            txtDataCalculo.Text = txtDataCalculo.Text.Replace("'", "")
                            txtDataCalculo.Text = txtDataCalculo.Text.Replace("NULL", "")

                            Exit Sub
                        End If
                        If txtValidade.Text < Now.Date And (ddlStatusCotacao.SelectedValue = 9) Then
                            If txtProcessoCotacao.Text = "" Then
                                ddlStatusCotacao.SelectedValue = Session("ID_STATUS")
                                diverro.Visible = True
                                lblmsgErro.Text = "Cotação com data de validade inferior a data atual!"


                                txtDataCalculo.Text = txtDataCalculo.Text.Replace("'", "")
                                txtDataCalculo.Text = txtDataCalculo.Text.Replace("NULL", "")

                                Exit Sub
                            End If

                        End If

                    End If


                    'REALIZA UPDATE  
                    Con.ExecutarQuery("UPDATE TB_COTACAO SET ID_STATUS_COTACAO = " & ddlStatusCotacao.SelectedValue & ", DT_VALIDADE_COTACAO = Convert(datetime, '" & txtValidade.Text & "', 103), ID_AGENTE_INTERNACIONAL = " & ddlAgente.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm.SelectedValue & ", ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & ", ID_DESTINATARIO_COMERCIAL = " & ddlDestinatarioComercial.SelectedValue & ", ID_CLIENTE = " & ddlCliente.SelectedValue & ", ID_CLIENTE_FINAL = " & ddlClienteFinal.SelectedValue & ", ID_CONTATO = " & ddlContato.SelectedValue & ", ID_SERVICO = " & ddlServico.SelectedValue & ", ID_VENDEDOR = " & ddlVendedor.SelectedValue & ", OB_CLIENTE = " & ObsCliente & ", OB_MOTIVO_CANCELAMENTO = " & ObsCancelamento & ", OB_OPERACIONAL = " & ObsOperacional & ", ID_MOTIVO_CANCELAMENTO = " & ddlMotivoCancelamento.SelectedValue & ", ID_TIPO_BL = " & ddlTipoBL.SelectedValue & ", FL_FREE_HAND = '" & ckbFreeHand.Checked & "',ID_PARCEIRO_INDICADOR = " & ddlIndicador.SelectedValue & ", ID_PARCEIRO_EXPORTADOR  = " & ddlExportador.SelectedValue & ", ID_PARCEIRO_IMPORTADOR = " & ddlImportador.SelectedValue & ",FL_LTL = '" & ckbLTL.Checked & "',FL_DTA_HUB = '" & ckbDtaHub.Checked & "',FL_TRANSP_DEDICADO  = '" & ckbTranspDedicado.Checked & "', DT_FOLLOWUP = " & txtDataFollowUp.Text & ", ID_PARCEIRO_RODOVIARIO = " & ddlTranspRodoviario.SelectedValue & ",FL_EMAIL_COTACAO = '" & ckbEmailCotacao.Checked & "', EMAIL_COTACAO = " & EmailCotacao & ", FL_TC4 = '" & ckbTC4.Checked & "',FL_TC6 = '" & ckbTC6.Checked & "' WHERE ID_COTACAO = " & txtID.Text)


                    If ddlStatusCotacao.SelectedValue <> Session("ID_STATUS") Then
                        'SEPARA E ENVIA EMAIL CASO COTAÇÃO ESTEJA APROVADA
                        If ddlStatusCotacao.SelectedValue = 9 Or ddlStatusCotacao.SelectedValue = 15 Then

                            ddlStatusCotacao.Enabled = False

                            If Session("ID_STATUS") <> 10 And txtProcessoCotacao.Text = "" Then
                                Dim AprovaCotacao As New AprovaCotacao
                                txtProcessoCotacao.Text = AprovaCotacao.AprovaCotacao(txtID.Text, ddlServico.SelectedValue, ddlEstufagem.SelectedValue, ddlDivisaoProfit.SelectedValue, Session("ID_USUARIO"))
                            Else

                                Dim RotinaUpdate As New RotinaUpdate
                                RotinaUpdate.UpdateInfoBasicas(txtID.Text, txtProcessoCotacao.Text, Session("RefTaxado"))
                                RotinaUpdate.UpdateFrete(txtID.Text, txtProcessoCotacao.Text)
                                RotinaUpdate.UpdateFreteTaxa(txtID.Text, txtProcessoCotacao.Text)
                                Dim ds2 As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
                                If ds2.Tables(0).Rows.Count > 0 Then
                                    For Each linha As DataRow In ds2.Tables(0).Rows
                                        RotinaUpdate.UpdateTaxas(txtID.Text, linha.Item("ID_COTACAO_TAXA"), txtProcessoCotacao.Text)
                                    Next
                                End If

                            End If


                            Con.ExecutarQuery("UPDATE TB_COTACAO SET DT_ENVIO_COTACAO = getdate() where ID_COTACAO = " & txtID.Text)

                            ds = Con.ExecutarQuery("SELECT EMAIL_FECHAMENTO_COTACAO FROM TB_PARAMETROS WHERE EMAIL_FECHAMENTO_COTACAO IS NOT NULL")
                            If ds.Tables(0).Rows.Count > 0 Then
                                Dim DESTINATARIO As String = ds.Tables(0).Rows(0).Item("EMAIL_FECHAMENTO_COTACAO").ToString()
                                SeparaEmail(DESTINATARIO)

                            End If

                        End If

                        Con.ExecutarQuery("UPDATE TB_COTACAO SET ID_USUARIO_STATUS = " & Session("ID_USUARIO") & ", DT_STATUS_COTACAO = getdate() where ID_COTACAO = " & txtID.Text)
                        ddlUsuarioStatus.SelectedValue = Session("ID_USUARIO")

                    End If



                    If ddlServico.SelectedValue < 3 Then
                        Dim ID_DESTINATARIO_COBRANCA As Integer = 1

                        If ddlImportador.SelectedValue <> 0 Then
                            ID_DESTINATARIO_COBRANCA = 4
                            ddlDestinatarioCobrancaTaxa.SelectedValue = 4
                        End If

                        Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET ID_DESTINATARIO_COBRANCA = " & ID_DESTINATARIO_COBRANCA & " WHERE ISNULL(VL_TAXA_VENDA,0) <> 0 AND ID_COTACAO = " & txtID.Text & " AND ID_COTACAO_TAXA NOT IN (SELECT ID_COTACAO_TAXA FROM View_Taxa_Bloqueada WHERE CD_PR= 'R')")
                    End If

                    Session("estufagem") = ddlEstufagem.SelectedValue
                    Session("transporte") = ddlServico.SelectedValue
                    Session("ID_CLIENTE") = ddlCliente.SelectedValue
                    Session("ID_STATUS") = ddlStatusCotacao.SelectedValue

                    If ddlStatusCotacao.SelectedValue = 10 And txtProcessoCotacao.Text <> "" Then
                        Dim RotinaUpdate As New RotinaUpdate
                        RotinaUpdate.UpdateInfoBasicas(txtID.Text, txtProcessoCotacao.Text, Session("RefTaxado"))
                        RotinaUpdate.UpdateFrete(txtID.Text, txtProcessoCotacao.Text)
                        RotinaUpdate.UpdateFreteTaxa(txtID.Text, txtProcessoCotacao.Text)
                        Dim ds2 As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
                        If ds2.Tables(0).Rows.Count > 0 Then
                            For Each linha As DataRow In ds2.Tables(0).Rows
                                RotinaUpdate.UpdateTaxas(txtID.Text, linha.Item("ID_COTACAO_TAXA"), txtProcessoCotacao.Text)
                            Next
                        End If
                    End If

                    If ddlStatusCotacao.SelectedValue = 7 Then
                        'CANCELAR
                        Con.ExecutarQuery("UPDATE TB_COTACAO SET ID_STATUS_COTACAO = 7, DT_STATUS_COTACAO = GETDATE() , ID_USUARIO_STATUS = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)

                        Con.ExecutarQuery("UPDATE TB_BL SET DT_CANCELAMENTO = GETDATE() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)

                        ddlStatusCotacao.Enabled = False
                    End If

                    txtDataCalculo.Text = txtDataCalculo.Text.Replace("'", "")
                    txtDataCalculo.Text = txtDataCalculo.Text.Replace("NULL", "")

                    txtDataFollowUp.Text = txtDataFollowUp.Text.Replace("NULL", "")
                    txtDataFollowUp.Text = txtDataFollowUp.Text.Replace("CONVERT(varchar,'", "")
                    txtDataFollowUp.Text = txtDataFollowUp.Text.Replace("',103)", "")

                    divsuccess.Visible = True

                    dgvFrete.DataBind()
                    '   dgvHistoricoFrete.DataBind()
                    dgvHistoricoCotacao.DataBind()

                    VerificaRepetida()
                    AtualizaPortoAgenteSI()

                    Con.Fechar()

                End If

            End If
            txtObsCliente.Text = txtObsCliente.Text.Replace("<br/>", vbNewLine)

            dsFornecedor.DataBind()
            ddlFornecedor.DataBind()
        End If

    End Sub

    Function ValorMinimoPendente(ID_COTACAO As Integer) As Boolean
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim finaliza As New FinalizaCotacao
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,
VL_TAXA_COMPRA,
ISNULL(VL_TAXA_COMPRA_MIN, 0)VL_TAXA_COMPRA_MIN,
VL_TAXA_VENDA,
ISNULL(VL_TAXA_VENDA_MIN, 0) VL_TAXA_VENDA_MIN
From TB_COTACAO_TAXA
WHERE ID_COTACAO = " & ID_COTACAO & " And ID_BASE_CALCULO_TAXA In (6,7,13,14)")
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows
                If finaliza.TaxaBloqueada(linha.Item("ID_COTACAO_TAXA"), "COTACAO") = False Then

                    If linha.Item("VL_TAXA_COMPRA") <> 0 And linha.Item("VL_TAXA_COMPRA_MIN") = 0 Then
                        Return True
                    End If

                    If linha.Item("VL_TAXA_VENDA") <> 0 And linha.Item("VL_TAXA_VENDA_MIN") = 0 Then
                        Return True
                    End If

                End If
            Next
        End If

        ds = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,
VL_TAXA_COMPRA,
ISNULL(VL_TAXA_COMPRA_MIN, 0)VL_TAXA_COMPRA_MIN,
VL_TAXA_VENDA,
ISNULL(VL_TAXA_VENDA_MIN, 0) VL_TAXA_VENDA_MIN
From TB_COTACAO_TAXA
WHERE ID_COTACAO = " & ID_COTACAO & " And ID_BASE_CALCULO_TAXA = 37 ")
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows
                If finaliza.TaxaBloqueada(linha.Item("ID_COTACAO_TAXA"), "COTACAO") = False Then

                    If linha.Item("VL_TAXA_VENDA") <> 0 And linha.Item("VL_TAXA_VENDA_MIN") = 0 Then
                        Return True
                    End If

                End If
            Next

        End If


        Return False
    End Function

    Private Sub btnSalvarFrete_Click(sender As Object, e As EventArgs) Handles btnSalvarFrete.Click
        divErroFrete.Visible = False
        divSuccessFrete.Visible = False
        divInfoFrete.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        If txtID.Text = "" Then

            lblErroFrete.Text = "Antes de inserir o Frete é necessário cadastrar as Informações Basicas"
            divErroFrete.Visible = True

        ElseIf ddlOrigemFrete.SelectedValue = 0 Or ddlDestinoFrete.SelectedValue = 0 Then

            lblErroFrete.Text = "Preencha todos os campos obrigatórios"
            divErroFrete.Visible = True


        Else
            If txtValorDivisaoProfit.Text = "" Then
                txtValorDivisaoProfit.Text = "0"
            End If

            If txtTTimeFreteInicial.Text = "" Then
                txtTTimeFreteInicial.Text = "0"
            End If

            If txtTTimeFreteFinal.Text = "" Then
                txtTTimeFreteFinal.Text = "0"
            End If

            If txtTTimeFreteMedia.Text = "" Then
                txtTTimeFreteMedia.Text = "0"
            End If

            If txtTTimeFreteTruckingAereo.Text = "" Then
                txtTTimeFreteTruckingAereo.Text = "0"
            End If

            Dim TTInicial As Integer = txtTTimeFreteInicial.Text
            Dim TTFinal As Integer = txtTTimeFreteFinal.Text
            Dim TTMedia As Integer = (TTFinal + TTInicial)
            TTMedia = TTMedia / 2
            txtTTimeFreteMedia.Text = TTMedia


            Dim TaxasIncluded As String
            If txtIncludedFrete.Text = "" Then
                TaxasIncluded = " NULL "
            Else
                TaxasIncluded = txtIncludedFrete.Text
                TaxasIncluded = TaxasIncluded.Replace("'", "''")
                TaxasIncluded = " '" & TaxasIncluded & "' "
            End If

            Dim Contrato As String
            If txtContratoArmador.Text = "" Then
                Contrato = " NULL "
            Else
                Contrato = txtContratoArmador.Text
                Contrato = Contrato.Replace("'", "''")
                Contrato = " '" & Contrato & "' "
            End If


            txtPesoTaxadoFrete.Text = txtPesoTaxadoFrete.Text.Replace(".", "")
            txtPesoTaxadoFrete.Text = txtPesoTaxadoFrete.Text.Replace(",", ".")

            txtFreteCompra.Text = txtFreteCompra.Text.Replace(".", "")
            txtFreteCompra.Text = txtFreteCompra.Text.Replace(",", ".")

            txtFreteVenda.Text = txtFreteVenda.Text.Replace(".", "")
            txtFreteVenda.Text = txtFreteVenda.Text.Replace(",", ".")

            txtValorDivisaoProfit.Text = txtValorDivisaoProfit.Text.Replace(".", "")
            txtValorDivisaoProfit.Text = txtValorDivisaoProfit.Text.Replace(",", ".")

            txtVendaMinimaFCL.Text = txtVendaMinimaFCL.Text.Replace(".", "")
            txtVendaMinimaFCL.Text = txtVendaMinimaFCL.Text.Replace(",", ".")

            txtCompraMinimaFCL.Text = txtCompraMinimaFCL.Text.Replace(".", "")
            txtCompraMinimaFCL.Text = txtCompraMinimaFCL.Text.Replace(",", ".")

            If txtCompraMinimaFCL.Text = "" Then
                txtCompraMinimaFCL.Text = "0"
            End If

            If txtVendaMinimaFCL.Text = "" Then
                txtVendaMinimaFCL.Text = "0"
            End If

            If txtValorFrequenciaFrete.Text = "" Then
                txtValorFrequenciaFrete.Text = "0"
            End If



            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErroFrete.Visible = True
                lblErroFrete.Text = "Usuário não possui permissão."

            Else

                If ddlServico.SelectedValue = 1 Or ddlServico.SelectedValue = 4 Then
                    'AGENCIAMENTO DE IMPORTACAO MARITIMA + AGENCIAMENTO DE EXPORTACAO MARITIMA
                    'ALTERA FRETE
                    ds = Con.ExecutarQuery("UPDATE TB_COTACAO SET 
ID_PORTO_ORIGEM = " & ddlOrigemFrete.SelectedValue & ",
ID_PORTO_DESTINO = " & ddlDestinoFrete.SelectedValue & ", 
ID_PORTO_ESCALA1 = " & ddlEscala1Frete.SelectedValue & ",
ID_PORTO_ESCALA2 = " & ddlEscala2Frete.SelectedValue & ",
ID_PORTO_ESCALA3 = " & ddlEscala3Frete.SelectedValue & ",
QT_TRANSITTIME_FINAL  = " & txtTTimeFreteFinal.Text & ",
QT_TRANSITTIME_INICIAL = " & txtTTimeFreteInicial.Text & ",
QT_TRANSITTIME_MEDIA = " & txtTTimeFreteMedia.Text & ",
ID_TIPO_FREQUENCIA = " & ddlFrequenciaFrete.SelectedValue & ",
VL_FREQUENCIA = '" & txtValorFrequenciaFrete.Text & "', 
NM_TAXAS_INCLUDED =  " & TaxasIncluded & ", 
ID_FRETE_TRANSPORTADOR = " & ddlFreteTransportador_Frete.SelectedValue & ",
ID_TIPO_BL = " & ddlTipoBL.SelectedValue & ",
ID_MOEDA_FRETE = " & ddlMoedaFrete.SelectedValue & ",
VL_TOTAL_FRETE_VENDA_MIN = " & txtVendaMinimaFCL.Text & ", 
VL_TOTAL_FRETE_COMPRA_MIN = " & txtCompraMinimaFCL.Text & ",
ID_TIPO_DIVISAO_FRETE = " & ddlDivisaoProfit.SelectedValue & ",
VL_DIVISAO_FRETE = " & txtValorDivisaoProfit.Text & ", 
ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & ",
ID_TIPO_CARGA = " & ddlTipoCargaFrete.SelectedValue & ",
ID_VIA_ROTA = " & ddlRotaFrete.SelectedValue & ", 
ID_TIPO_ESTUFAGEM = " & ddlEstufagemFrete.SelectedValue & " ,
ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_Frete.SelectedValue & " ,
TRANSITTIME_TRUCKING_AEREO = " & txtTTimeFreteTruckingAereo.Text & " ,
FINAL_DESTINATION = " & ddlFinalDestination.SelectedValue & ",
NR_CONTRATO_ARMADOR = " & Contrato & ", 
FL_FRETE_DECLARADO = '" & ckFreteDeclarado.Checked & "',
FL_FRETE_PROFIT = '" & ckFreteProfit.Checked & "' 
WHERE ID_COTACAO = " & txtID.Text)

                ElseIf ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then
                    'AGENCIAMENTO DE IMPORTACAO AEREO + AGENCIAMENTO DE EXPORTAÇÃO AEREO

                    'ALTERA FRETE
                    ds = Con.ExecutarQuery("UPDATE TB_COTACAO SET 
ID_PORTO_ORIGEM = " & ddlOrigemFrete.SelectedValue & ",
ID_PORTO_DESTINO = " & ddlDestinoFrete.SelectedValue & ", 
ID_PORTO_ESCALA1 = " & ddlEscala1Frete.SelectedValue & ",
ID_PORTO_ESCALA2 = " & ddlEscala2Frete.SelectedValue & ",
ID_PORTO_ESCALA3 = " & ddlEscala3Frete.SelectedValue & ",
QT_TRANSITTIME_FINAL  = " & txtTTimeFreteFinal.Text & ",
QT_TRANSITTIME_INICIAL = " & txtTTimeFreteInicial.Text & ",
QT_TRANSITTIME_MEDIA = " & txtTTimeFreteMedia.Text & ",
ID_TIPO_FREQUENCIA = " & ddlFrequenciaFrete.SelectedValue & ",
VL_FREQUENCIA = '" & txtValorFrequenciaFrete.Text & "', 
NM_TAXAS_INCLUDED =  " & TaxasIncluded & ", 
ID_FRETE_TRANSPORTADOR = " & ddlFreteTransportador_Frete.SelectedValue & ",
ID_TIPO_BL = " & ddlTipoBL.SelectedValue & ",
ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & ",
ID_TIPO_CARGA = " & ddlTipoCargaFrete.SelectedValue & ",
ID_VIA_ROTA = " & ddlRotaFrete.SelectedValue & ", 
ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_Frete.SelectedValue & " ,
ID_TIPO_AERONAVE = " & ddlTipoAeronave.SelectedValue & " ,
TRANSITTIME_TRUCKING_AEREO = " & txtTTimeFreteTruckingAereo.Text & " ,
FINAL_DESTINATION = " & ddlFinalDestination.SelectedValue & " 
WHERE ID_COTACAO = " & txtID.Text)

                End If

                txtVendaMinimaFCL.Text = txtVendaMinimaFCL.Text.Replace(".", ",")
                txtCompraMinimaFCL.Text = txtCompraMinimaFCL.Text.Replace(".", ",")
                txtFreteVenda.Text = txtFreteVenda.Text.Replace(".", ",")
                txtFreteCompra.Text = txtFreteCompra.Text.Replace(".", ",")
                txtValorDivisaoProfit.Text = txtValorDivisaoProfit.Text.Replace(".", ",")

                divSuccessFrete.Visible = True
                Con.Fechar()

                If divDeleteTaxas.Visible = True And lblDeleteTaxas.Text = "Ação realizada com sucesso!" Then
                    divInfoFrete.Visible = True
                    lblInfoFrete.Text = "Registros importados automaticamente, favor revisar as taxas da cotação!"
                End If
                dgvTaxas.DataBind()
                dgvFrete.DataBind()

                AtualizaFreteMercadoria()

                If ddlServico.SelectedValue <= 2 And ddlFreteTransportador_Frete.SelectedValue <> 0 Then
                    If Session("ID_FRETE_TRANSPORTADOR") <> ddlFreteTransportador_Frete.SelectedValue Then
                        AtualizaTaxaAgente()
                    End If
                End If

                If Session("ID_STATUS") = 10 Then
                    Dim RotinaUpdate As New RotinaUpdate
                    RotinaUpdate.UpdateFrete(txtID.Text, txtProcessoCotacao.Text)
                    RotinaUpdate.UpdateFreteTaxa(txtID.Text, txtProcessoCotacao.Text)
                End If

                Session("ID_FRETE_TRANSPORTADOR") = ddlFreteTransportador_Frete.SelectedValue
            End If


        End If

        If ddlFreteTransportador_Frete.SelectedValue <> 0 Then
            ddlMoedaFrete.Enabled = "False"
            txtTTimeFreteInicial.Enabled = "False"
            txtTTimeFreteFinal.Enabled = "False"
        Else
            ddlMoedaFrete.Enabled = "True"
            txtTTimeFreteInicial.Enabled = "True"
            txtTTimeFreteFinal.Enabled = "True"
        End If
        mpeNovoFrete.Show()


    End Sub

    Sub AlteraMercadoriaAereo()

        'ALTERA FRETE
        Dim Con As New Conexao_sql
        Con.Conectar()
        Con.ExecutarQuery("UPDATE TB_COTACAO SET 
VL_PESO_TAXADO = " & txtPesoTaxadoMercadoria.Text.Replace(",", ".") & ", 
ID_MOEDA_FRETE = " & ddlMoedaFreteMercadoria.SelectedValue & ",
ID_TIPO_DIVISAO_FRETE = " & ddlProfitMercadoria.SelectedValue & ",
VL_DIVISAO_FRETE = " & txtValorProfitMercadoria.Text.Replace(",", ".") & ", 
FL_FRETE_DECLARADO = '" & ckFreteDeclarado.Checked & "',
FL_FRETE_PROFIT = '" & ckFreteProfit.Checked & "' 
WHERE ID_COTACAO = " & txtID.Text)

    End Sub

    Sub AtualizaTaxaAgente()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        If ddlFreteTransportador_Frete.SelectedValue <> 0 Then
            ds = Con.ExecutarQuery("select ISNULL(ID_AGENTE,0)ID_AGENTE from TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & ddlFreteTransportador_Frete.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim ID_AGENTE As Integer = ds.Tables(0).Rows(0).Item("ID_AGENTE").ToString()
                Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET ID_FORNECEDOR = " & ID_AGENTE & " WHERE FL_DECLARADO = 1 AND ID_COTACAO = " & txtID.Text)
                dgvTaxas.DataBind()
            End If
        End If
    End Sub

    Sub AtualizaFreteMercadoria()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        If Session("estufagem") = 1 And ddlFreteTransportador_Frete.SelectedValue <> 0 Then
            ds = Con.ExecutarQuery("Select ID_COTACAO_MERCADORIA FROM TB_COTACAO_MERCADORIA A INNER Join TB_COTACAO B On B.ID_COTACAO = A.ID_COTACAO
WHERE a.ID_COTACAO = " & txtID.Text & " And a.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER from TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103)) ")
            If ds.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In ds.Tables(0).Rows

                    Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET VL_FRETE_COMPRA =  CASE WHEN VL_FRETE_COMPRA <
                                    (SELECT (SELECT VL_COMPRA FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103))* QT_CONTAINER FROM TB_COTACAO_MERCADORIA A WHERE A.ID_COTACAO_MERCADORIA = " & linha.Item("ID_COTACAO_MERCADORIA") & " ) THEN 
                                    (SELECT (SELECT VL_COMPRA FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103))* QT_CONTAINER FROM TB_COTACAO_MERCADORIA A WHERE A.ID_COTACAO_MERCADORIA = " & linha.Item("ID_COTACAO_MERCADORIA") & " ) ELSE VL_FRETE_COMPRA END WHERE ID_COTACAO_MERCADORIA =  " & linha.Item("ID_COTACAO_MERCADORIA"))

                    Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET VL_FRETE_COMPRA_UNITARIO = CASE WHEN VL_FRETE_COMPRA_UNITARIO < (SELECT (SELECT VL_COMPRA FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103)) FROM TB_COTACAO_MERCADORIA A WHERE A.ID_COTACAO_MERCADORIA = " & linha.Item("ID_COTACAO_MERCADORIA") & " )
                   THEN (SELECT (SELECT VL_COMPRA FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103)) FROM TB_COTACAO_MERCADORIA A WHERE A.ID_COTACAO_MERCADORIA = " & linha.Item("ID_COTACAO_MERCADORIA") & " )
                    ELSE VL_FRETE_COMPRA_UNITARIO END WHERE  ID_COTACAO_MERCADORIA =  " & linha.Item("ID_COTACAO_MERCADORIA"))

                    Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET QT_DIAS_FREETIME = CASE WHEN QT_DIAS_FREETIME < (SELECT (SELECT QT_DIAS_FREETIME FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103)) FROM TB_COTACAO_MERCADORIA A WHERE A.ID_COTACAO_MERCADORIA = " & linha.Item("ID_COTACAO_MERCADORIA") & " )
                                   THEN (SELECT (SELECT QT_DIAS_FREETIME FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103)) FROM TB_COTACAO_MERCADORIA A WHERE A.ID_COTACAO_MERCADORIA = " & linha.Item("ID_COTACAO_MERCADORIA") & " )
                    ELSE QT_DIAS_FREETIME END WHERE ID_COTACAO_MERCADORIA =  " & linha.Item("ID_COTACAO_MERCADORIA"))

                Next
            End If
        End If

        Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_PESO_BRUTO=
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

        Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

        Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_COMPRA_MIN =
(SELECT SUM(ISNULL(VL_FRETE_COMPRA_MIN,0))VL_FRETE_COMPRA_MIN FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

        Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_VENDA_MIN =
(SELECT SUM(ISNULL(VL_FRETE_VENDA_MIN,0))VL_FRETE_VENDA_MIN FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)


        If Session("estufagem") = 2 Then
            Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_VENDA =
(SELECT SUM(ISNULL(VL_FRETE_VENDA_UNITARIO,0))VL_FRETE_VENDA_UNITARIO FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ") WHERE ID_COTACAO =  " & txtID.Text)

            Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_COMPRA =
(SELECT SUM(ISNULL(VL_FRETE_COMPRA_UNITARIO,0))VL_FRETE_COMPRA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

        ElseIf Session("estufagem") = 1 Then

            Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_VENDA =
(SELECT SUM(ISNULL(VL_FRETE_VENDA,0))VL_FRETE_VENDA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ") WHERE ID_COTACAO =  " & txtID.Text)

            Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_COMPRA =
(SELECT SUM(ISNULL(VL_FRETE_COMPRA,0))VL_FRETE_COMPRA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)
        End If

        dgvMercadoria.DataBind()
    End Sub
    Private Sub btnSalvarMercadoria_Click(sender As Object, e As EventArgs) Handles btnSalvarMercadoria.Click
        divErroMercadoria.Visible = False
        divSuccessMercadoria.Visible = False
        lblSuccessMercadoria.Text = "Registro cadastrado/atualizado com sucesso!"

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        If txtID.Text = "" Then

            lblErroMercadoria.Text = "Antes de inserir Mercadoria é necessário cadastrar as Informações Basicas"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()


        ElseIf (Session("servico") <> 2 And Session("servico") <> 5) And Session("estufagem") = 1 And txtQtdContainerMercadoria.Text = "" Then
            RedQTDContainer.Visible = True
            RedContainer.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf (Session("servico") <> 2 And Session("servico") <> 5) And Session("estufagem") = 1 And ddlTipoContainerMercadoria.SelectedValue = 0 Then
            RedQTDContainer.Visible = True
            RedContainer.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 1 And ddlMercadoria.SelectedValue = 0 Then
            RedQTDContainer.Visible = True
            RedContainer.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()

        ElseIf Session("estufagem") = 2 And ddlMercadoria.SelectedValue = 0 Then
            RedQTDMercadoria.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf (Session("servico") <> 2 And Session("servico") <> 5) And Session("estufagem") = 2 And txtQtdMercadoria.Text = "" Then
            RedQTDMercadoria.Visible = True
            RedPesoBruto.Visible = True
            RedM3.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf (Session("servico") <> 2 And Session("servico") <> 5) And Session("estufagem") = 2 And txtPesoBrutoMercadoria.Text = "" Then
            RedQTDMercadoria.Visible = True
            RedPesoBruto.Visible = True
            RedM3.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 2 And txtM3Mercadoria.Text = "" And (Session("servico") <> 2 And Session("servico") <> 5) Then
            RedQTDMercadoria.Visible = True
            RedPesoBruto.Visible = True
            RedM3.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()

        ElseIf Session("transporte") = 2 And txtPesoBrutoMercadoria.Text = "" And (Session("servico") <> 2 And Session("servico") <> 5) Then
            RedPesoBruto.Visible = True
            RedM3.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("transporte") = 5 And txtM3Mercadoria.Text = "" Then
            RedPesoBruto.Visible = True
            RedM3.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()

        Else
            txtFreteCompraMercadoriaUnitario.Text = txtFreteCompraMercadoriaUnitario.Text.Replace(".", "")
            txtFreteCompraMercadoriaUnitario.Text = txtFreteCompraMercadoriaUnitario.Text.Replace(",", ".")
            If txtFreteCompraMercadoriaUnitario.Text = "" Then
                txtFreteCompraMercadoriaUnitario.Text = "0"
            End If



            txtFreteVendaMercadoriaUnitario.Text = txtFreteVendaMercadoriaUnitario.Text.Replace(".", "")
            txtFreteVendaMercadoriaUnitario.Text = txtFreteVendaMercadoriaUnitario.Text.Replace(",", ".")
            If txtFreteVendaMercadoriaUnitario.Text = "" Then
                txtFreteVendaMercadoriaUnitario.Text = "0"
            End If


            txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaCalc.Text.Replace(".", "")
            txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaCalc.Text.Replace(",", ".")
            If txtFreteCompraMercadoriaCalc.Text = "" Then
                txtFreteCompraMercadoriaCalc.Text = "0"
            End If


            txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaCalc.Text.Replace(".", "")
            txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaCalc.Text.Replace(",", ".")
            If txtFreteVendaMercadoriaCalc.Text = "" Then
                txtFreteVendaMercadoriaCalc.Text = "0"
            End If


            txtPesoBrutoMercadoria.Text = txtPesoBrutoMercadoria.Text.Replace(".", "")
            txtPesoBrutoMercadoria.Text = txtPesoBrutoMercadoria.Text.Replace(",", ".")
            If txtPesoBrutoMercadoria.Text = "" Then
                txtPesoBrutoMercadoria.Text = "0"
            End If


            txtM3Mercadoria.Text = txtM3Mercadoria.Text.Replace(".", "")
            txtM3Mercadoria.Text = txtM3Mercadoria.Text.Replace(",", ".")
            If txtM3Mercadoria.Text = "" Then
                txtM3Mercadoria.Text = "0"
            End If


            txtComprimentoMercadoria.Text = txtComprimentoMercadoria.Text.Replace(".", "")
            txtComprimentoMercadoria.Text = txtComprimentoMercadoria.Text.Replace(",", ".")
            If txtComprimentoMercadoria.Text = "" Then
                txtComprimentoMercadoria.Text = "0"
            End If


            txtLarguraMercadoria.Text = txtLarguraMercadoria.Text.Replace(".", "")
            txtLarguraMercadoria.Text = txtLarguraMercadoria.Text.Replace(",", ".")
            If txtLarguraMercadoria.Text = "" Then
                txtLarguraMercadoria.Text = "0"
            End If

            txtAlturaMercadoria.Text = txtAlturaMercadoria.Text.Replace(".", "")
            txtAlturaMercadoria.Text = txtAlturaMercadoria.Text.Replace(",", ".")
            If txtAlturaMercadoria.Text = "" Then
                txtAlturaMercadoria.Text = "0"
            End If


            txtValorCargaMercadoria.Text = txtValorCargaMercadoria.Text.Replace(".", "")
            txtValorCargaMercadoria.Text = txtValorCargaMercadoria.Text.Replace(",", ".")
            If txtValorCargaMercadoria.Text = "" Then
                txtValorCargaMercadoria.Text = "0"
            End If


            txtFreteVendaMinima.Text = txtFreteVendaMinima.Text.Replace(".", "")
            txtFreteVendaMinima.Text = txtFreteVendaMinima.Text.Replace(",", ".")
            If txtFreteVendaMinima.Text = "" Then
                txtFreteVendaMinima.Text = "0"
            End If

            txtFreteCompraMinima.Text = txtFreteCompraMinima.Text.Replace(".", "")
            txtFreteCompraMinima.Text = txtFreteCompraMinima.Text.Replace(",", ".")
            If txtFreteCompraMinima.Text = "" Then
                txtFreteCompraMinima.Text = "0"
            End If

            txtCBMAereo.Text = txtCBMAereo.Text.Replace(".", "")
            txtCBMAereo.Text = txtCBMAereo.Text.Replace(",", ".")
            If txtCBMAereo.Text = "" Then
                txtCBMAereo.Text = "0"
            End If


            If txtFreeTimeMercadoria.Text = "" Then
                txtFreeTimeMercadoria.Text = "0"
            End If

            If txtQtdMercadoria.Text = "" Then
                txtQtdMercadoria.Text = "0"
            End If

            If txtQtdContainerMercadoria.Text = "" Then
                txtQtdContainerMercadoria.Text = "0"
            End If

            If txtPesoTaxadoMercadoria.Text = "" Then
                txtPesoTaxadoMercadoria.Text = "0"
            End If

            If txtValorProfitMercadoria.Text = "" Then
                txtValorProfitMercadoria.Text = "0"
            End If


            Dim DescMercadoria As String = ""
            If txtDsMercadoria.Text = "" Then
                DescMercadoria = "NULL"
            Else
                DescMercadoria = txtDsMercadoria.Text
                DescMercadoria = DescMercadoria.Replace("'", "''")
                DescMercadoria = "'" & DescMercadoria & "'"
            End If

            Dim OutrasOBS As String = ""
            If txtOutrasOBS_Mercadoria.Text = "" Then
                OutrasOBS = "NULL"
            Else
                OutrasOBS = txtOutrasOBS_Mercadoria.Text
                OutrasOBS = OutrasOBS.Replace("'", "''")
                OutrasOBS = "'" & OutrasOBS & "'"
            End If

            Dim OBSEndereco As String = ""
            If txtOBS_Endereco.Text = "" Then
                OBSEndereco = "NULL"
            Else
                OBSEndereco = txtOBS_Endereco.Text
                OBSEndereco = OBSEndereco.Replace("'", "''")
                OBSEndereco = "'" & OBSEndereco & "'"
            End If





            If Session("estufagem") = 2 Then

                If txtFreteCompraMercadoriaCalc.Text = 0 Then
                    txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaUnitario.Text

                End If

                If txtFreteVendaMercadoriaCalc.Text = 0 Then
                    txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text

                End If



                Dim venda_calc As Decimal = txtFreteVendaMercadoriaCalc.Text
                Dim venda_min As Decimal = txtFreteVendaMinima.Text

                If venda_calc < venda_min Then
                    txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMinima.Text
                End If

                Dim compra_calc As Decimal = txtFreteCompraMercadoriaCalc.Text
                Dim compra_min As Decimal = txtFreteCompraMinima.Text

                If compra_calc < compra_min Then
                    txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMinima.Text
                End If
            End If


            If txtIDMercadoria.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroMercadoria.Visible = True
                    lblErroMercadoria.Text = "Usuário não possui permissão para cadastrar."
                    mpeNovoMercadoria.Show()

                Else

                    'INSERE MERCADORIA
                    Dim dsMercadoria As DataSet = Con.ExecutarQuery("INSERT INTO TB_COTACAO_MERCADORIA ( ID_COTACAO,
ID_MERCADORIA,ID_TIPO_CONTAINER,QT_CONTAINER,VL_FRETE_COMPRA,VL_FRETE_VENDA,VL_PESO_BRUTO,VL_M3,DS_MERCADORIA,VL_COMPRIMENTO,VL_LARGURA,VL_ALTURA,VL_CARGA,QT_DIAS_FREETIME,QT_MERCADORIA,VL_FRETE_COMPRA_MIN,VL_FRETE_VENDA_MIN,OBS_ENDERECO,VL_FRETE_COMPRA_UNITARIO,VL_FRETE_VENDA_UNITARIO,OUTRAS_OBS,ID_MOEDA_CARGA,VL_CBM) VALUES (" & txtID.Text & "," & ddlMercadoria.SelectedValue & " ," & ddlTipoContainerMercadoria.SelectedValue & "," & txtQtdContainerMercadoria.Text & "," & txtFreteCompraMercadoriaCalc.Text & "," & txtFreteVendaMercadoriaCalc.Text & "," & txtPesoBrutoMercadoria.Text & "," & txtM3Mercadoria.Text & ", " & DescMercadoria & " ," & txtComprimentoMercadoria.Text & ", " & txtLarguraMercadoria.Text & "," & txtAlturaMercadoria.Text & ", " & txtValorCargaMercadoria.Text & "," & txtFreeTimeMercadoria.Text & "," & txtQtdMercadoria.Text & "," & txtFreteCompraMinima.Text & "," & txtFreteVendaMinima.Text & ", " & OBSEndereco & "," & txtFreteCompraMercadoriaUnitario.Text & "," & txtFreteVendaMercadoriaUnitario.Text & "," & OutrasOBS & "," & ddlMoedaCarga.SelectedValue & "," & txtCBMAereo.Text & ")   Select SCOPE_IDENTITY() as ID_COTACAO_MERCADORIA ")

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET FL_TARIFA_SPOT = '" & ckTarifaSpot.Checked & "' WHERE ID_COTACAO = " & txtID.Text)

                    If ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then
                        txtIDMercadoria.Text = dsMercadoria.Tables(0).Rows(0).Item("ID_COTACAO_MERCADORIA").ToString()
                        btnAdicionarMedidasAereo.Visible = True
                        AdicionarMedidasAereo()
                        AlteraMercadoriaAereo()

                        divErroMercadoria.Visible = False
                        divMedidasAereo.Attributes.CssStyle.Add("display", "block")
                        txtFreteCompraMercadoriaUnitario.Text = txtFreteCompraMercadoriaUnitario.Text.Replace(".", ",")
                        txtFreteCompraMinima.Text = txtFreteCompraMinima.Text.Replace(".", ",")
                        txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaCalc.Text.Replace(".", ",")

                        txtFreteVendaMercadoriaUnitario.Text = txtFreteVendaMercadoriaUnitario.Text.Replace(".", ",")
                        txtFreteVendaMinima.Text = txtFreteVendaMinima.Text.Replace(".", ",")
                        txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaCalc.Text.Replace(".", ",")

                        txtValorCargaMercadoria.Text = txtValorCargaMercadoria.Text.Replace(".", ",")
                        txtM3Mercadoria.Text = txtM3Mercadoria.Text.Replace(".", ",")
                        txtPesoBrutoMercadoria.Text = txtPesoBrutoMercadoria.Text.Replace(".", ",")
                        txtCBMAereo.Text = txtCBMAereo.Text.Replace(".", ",")


                        txtComprimentoMercadoria.Text = txtComprimentoMercadoria.Text.Replace(".", ",")
                        txtLarguraMercadoria.Text = txtLarguraMercadoria.Text.Replace(".", ",")
                        txtAlturaMercadoria.Text = txtAlturaMercadoria.Text.Replace(".", ",")
                        txtValorProfitMercadoria.Text = txtValorProfitMercadoria.Text.Replace(".", ",")
                    Else

                        ddlMercadoria.SelectedValue = 0
                        ddlTipoContainerMercadoria.SelectedValue = 0
                        txtQtdContainerMercadoria.Text = ""
                        txtFreteCompraMercadoriaCalc.Text = ""
                        txtFreteVendaMercadoriaCalc.Text = ""
                        txtPesoBrutoMercadoria.Text = ""
                        txtCBMAereo.Text = ""
                        txtM3Mercadoria.Text = ""
                        txtDsMercadoria.Text = ""
                        txtComprimentoMercadoria.Text = ""
                        txtLarguraMercadoria.Text = ""
                        txtAlturaMercadoria.Text = ""
                        txtValorCargaMercadoria.Text = ""
                        txtFreeTimeMercadoria.Text = ""
                        txtQtdMercadoria.Text = ""
                        txtFreteCompraMinima.Text = ""
                        txtFreteVendaMinima.Text = ""
                        txtOBS_Endereco.Text = ""
                        txtOutrasOBS_Mercadoria.Text = ""
                        txtFreteCompraMercadoriaUnitario.Text = ""
                        txtFreteVendaMercadoriaUnitario.Text = ""
                        txtIDMercadoria.Text = ""
                    End If

                    Con.Fechar()
                    dgvMercadoria.DataBind()
                    divSuccessMercadoria.Visible = True
                    mpeNovoMercadoria.Show()
                    AtualizaFreteMercadoria()

                    If Session("ID_STATUS") = 10 Then
                        Dim CalCotacao As New CalculaCotacao
                        Dim retorno As String = CalCotacao.CalculaCotacao(txtID.Text)

                        Dim RotinaUpdate As New RotinaUpdate
                        RotinaUpdate.UpdateCarga(txtID.Text, dsMercadoria.Tables(0).Rows(0).Item("ID_COTACAO_MERCADORIA").ToString(), txtProcessoCotacao.Text, Session("RefPeso"), Session("RefVolume"), Session("RefPesoSum"), Session("RefVolumeSum"))
                        RotinaUpdate.UpdateFreteTaxa(txtID.Text, txtProcessoCotacao.Text)

                        Dim ds2 As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
                        If ds2.Tables(0).Rows.Count > 0 Then
                            For Each linha As DataRow In ds2.Tables(0).Rows
                                RotinaUpdate.UpdateTaxas(txtID.Text, linha.Item("ID_COTACAO_TAXA"), txtProcessoCotacao.Text)
                            Next
                        End If
                        RotinaUpdate.UpdateInfoBasicas(txtID.Text, txtProcessoCotacao.Text, Session("RefTaxado"))

                    End If

                    VerificaRepetida()

                End If


            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroMercadoria.Visible = True
                    lblErroMercadoria.Text = "Usuário não possui permissão para alterar."
                    mpeNovoMercadoria.Show()

                Else

                    'ALTERA MERCADORIA
                    Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET 
ID_MERCADORIA = " & ddlMercadoria.SelectedValue & ", ID_TIPO_CONTAINER = " & ddlTipoContainerMercadoria.SelectedValue & ",QT_CONTAINER = " & txtQtdContainerMercadoria.Text & ",VL_FRETE_COMPRA =  " & txtFreteCompraMercadoriaCalc.Text & ",VL_FRETE_VENDA = " & txtFreteVendaMercadoriaCalc.Text & ",VL_PESO_BRUTO = " & txtPesoBrutoMercadoria.Text & ",VL_M3 = " & txtM3Mercadoria.Text & ",DS_MERCADORIA = " & DescMercadoria & ",VL_COMPRIMENTO = " & txtComprimentoMercadoria.Text & ",VL_LARGURA = " & txtLarguraMercadoria.Text & ",VL_ALTURA = " & txtAlturaMercadoria.Text & ",VL_CARGA = " & txtValorCargaMercadoria.Text & " ,QT_DIAS_FREETIME = " & txtFreeTimeMercadoria.Text & " ,VL_FRETE_COMPRA_MIN = " & txtFreteCompraMinima.Text & ",VL_FRETE_VENDA_MIN = " & txtFreteVendaMinima.Text & ",OBS_ENDERECO = " & OBSEndereco & ",OUTRAS_OBS = " & OutrasOBS & ",VL_FRETE_COMPRA_UNITARIO = " & txtFreteCompraMercadoriaUnitario.Text & ",VL_FRETE_VENDA_UNITARIO = " & txtFreteVendaMercadoriaUnitario.Text & ", QT_MERCADORIA = " & txtQtdMercadoria.Text & ", ID_MOEDA_CARGA =" & ddlMoedaCarga.SelectedValue & ",VL_CBM = " & txtCBMAereo.Text & " WHERE ID_COTACAO_MERCADORIA = " & txtIDMercadoria.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET FL_TARIFA_SPOT = '" & ckTarifaSpot.Checked & "' WHERE ID_COTACAO = " & txtID.Text)

                    If ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then
                        AdicionarMedidasAereo()
                        AlteraMercadoriaAereo()
                        divErroMercadoria.Visible = False
                    End If



                    txtFreteCompraMercadoriaUnitario.Text = txtFreteCompraMercadoriaUnitario.Text.Replace(".", ",")
                    txtFreteCompraMinima.Text = txtFreteCompraMinima.Text.Replace(".", ",")
                    txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaCalc.Text.Replace(".", ",")

                    txtFreteVendaMercadoriaUnitario.Text = txtFreteVendaMercadoriaUnitario.Text.Replace(".", ",")
                    txtFreteVendaMinima.Text = txtFreteVendaMinima.Text.Replace(".", ",")
                    txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaCalc.Text.Replace(".", ",")

                    txtValorCargaMercadoria.Text = txtValorCargaMercadoria.Text.Replace(".", ",")
                    txtM3Mercadoria.Text = txtM3Mercadoria.Text.Replace(".", ",")
                    txtPesoBrutoMercadoria.Text = txtPesoBrutoMercadoria.Text.Replace(".", ",")
                    txtCBMAereo.Text = txtCBMAereo.Text.Replace(".", ",")

                    txtComprimentoMercadoria.Text = txtComprimentoMercadoria.Text.Replace(".", ",")
                    txtLarguraMercadoria.Text = txtLarguraMercadoria.Text.Replace(".", ",")
                    txtAlturaMercadoria.Text = txtAlturaMercadoria.Text.Replace(".", ",")
                    txtValorProfitMercadoria.Text = txtValorProfitMercadoria.Text.Replace(".", ",")

                    divSuccessMercadoria.Visible = True
                    Con.Fechar()
                    dgvMercadoria.DataBind()
                    mpeNovoMercadoria.Show()


                    AtualizaFreteMercadoria()
                    Dim CalCotacao As New CalculaCotacao
                    Dim retorno As String = CalCotacao.CalculaCotacao(txtID.Text)

                    If Session("ID_STATUS") = 10 Then

                        Dim RotinaUpdate As New RotinaUpdate
                        RotinaUpdate.UpdateCarga(txtID.Text, txtIDMercadoria.Text, txtProcessoCotacao.Text, Session("RefPeso"), Session("RefVolume"), Session("RefPesoSum"), Session("RefVolumeSum"))
                        RotinaUpdate.UpdateFreteTaxa(txtID.Text, txtProcessoCotacao.Text)

                        Dim ds2 As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
                        If ds2.Tables(0).Rows.Count > 0 Then
                            For Each linha As DataRow In ds2.Tables(0).Rows
                                RotinaUpdate.UpdateTaxas(txtID.Text, linha.Item("ID_COTACAO_TAXA"), txtProcessoCotacao.Text)
                            Next
                        End If
                        RotinaUpdate.UpdateInfoBasicas(txtID.Text, txtProcessoCotacao.Text, Session("RefTaxado"))

                    End If

                    Session("RefPeso") = txtPesoBrutoMercadoria.Text

                    Session("RefVolume") = txtM3Mercadoria.Text


                    ds = Con.ExecutarQuery("SELECT VL_TOTAL_PESO_BRUTO,VL_TOTAL_M3 FROM  TB_COTACAO A WHERE A.ID_COTACAO = " & txtID.Text)
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_PESO_BRUTO")) Then
                            Session("RefPesoSum") = ds.Tables(0).Rows(0).Item("VL_TOTAL_PESO_BRUTO").ToString()
                        End If
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_M3")) Then
                            Session("RefVolumeSum") = ds.Tables(0).Rows(0).Item("VL_TOTAL_M3").ToString()
                        End If
                    End If

                    ds = Con.ExecutarQuery("SELECT isnull(VL_PESO_TAXADO,0)VL_PESO_TAXADO, isnull(VL_TOTAL_M3,0)VL_TOTAL_M3 from TB_COTACAO where ID_COTACAO =" & txtID.Text)
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
                            txtPesoTaxadoMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
                            Session("RefTaxado") = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
                        End If
                    End If

                    VerificaRepetida()

                End If

            End If

        End If

        mpeNovoMercadoria.Show()

    End Sub

    Private Sub btnSalvarTaxa_Click(sender As Object, e As EventArgs) Handles btnSalvarTaxa.Click
        divErroTaxa.Visible = False
        divSuccessTaxa.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        If txtQtdBaseCalculo.Text = "" Then
            txtQtdBaseCalculo.Text = "0"
        End If

        If txtValorTaxaCompraMin.Text = "" Then
            txtValorTaxaCompraMin.Text = "0"
        End If

        If txtValorTaxaCompra.Text = "" Then
            txtValorTaxaCompra.Text = "0"
        End If

        If txtValorTaxaVenda.Text = "" Then
            txtValorTaxaVenda.Text = "0"
        End If

        If txtValorTaxaVendaMin.Text = "" Then
            txtValorTaxaVendaMin.Text = "0"
        End If


        If txtID.Text = "" Then

            lblErroTaxa.Text = "Antes de inserir Taxa é necessario cadastrar as Informações Basicas"
            divErroTaxa.Visible = True

        ElseIf ddlItemDespesaTaxa.SelectedValue = 0 Then
            lblErroTaxa.Text = "Selecione o item de despesa"
            divErroTaxa.Visible = True

        ElseIf txtQtdBaseCalculo.Text = 0 And (ddlBaseCalculoTaxa.SelectedValue = 38 Or ddlBaseCalculoTaxa.SelectedValue = 40 Or ddlBaseCalculoTaxa.SelectedValue = 41) Then
            lblErroTaxa.Text = "Necessário informar quantidade para base de calculo selecionada!"
            divErroTaxa.Visible = True


        ElseIf ddlEstufagem.SelectedValue = 1 And (ddlOrigemPagamentoTaxa.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0 Or ddlMoedaCompraTaxa.SelectedValue = 0 Or txtValorTaxaCompra.Text = "" Or ddlTipoPagamentoTaxa.SelectedValue = 0) Then

            lblErroTaxa.Text = "Preencha todos os campos obrigatórios"
            divErroTaxa.Visible = True


        ElseIf ddlEstufagem.SelectedValue = 2 And (ddlOrigemPagamentoTaxa.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0 Or ddlTipoPagamentoTaxa.SelectedValue = 0) Then

            lblErroTaxa.Text = "Preencha todos os campos obrigatórios"
            divErroTaxa.Visible = True


        ElseIf ddlItemDespesaTaxa.SelectedValue <> 550 And (ddlEstufagem.SelectedValue = 1 And (ddlOrigemPagamentoTaxa.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0 Or ddlMoedaCompraTaxa.SelectedValue = 0 Or txtValorTaxaCompra.Text = "" Or ddlTipoPagamentoTaxa.SelectedValue = 0)) Then
            lblErroTaxa.Text = "Preencha todos os campos obrigatórios"
            divErroTaxa.Visible = True

        ElseIf ddlItemDespesaTaxa.SelectedValue = 550 And (ddlEstufagem.SelectedValue = 1 And (ddlOrigemPagamentoTaxa.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0)) Then
            lblErroTaxa.Text = "Preencha todos os campos obrigatórios"
            divErroTaxa.Visible = True

        ElseIf ddlItemDespesaTaxa.SelectedValue = 550 And ddlEstufagem.SelectedValue = 1 And (ddlOrigemPagamentoTaxa.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0 Or txtValorTaxaCompra.Text = "" Or ddlTipoPagamentoTaxa.SelectedValue = 0) Then
            lblErroTaxa.Text = "Preencha todos os campos obrigatórios"
            divErroTaxa.Visible = True

        ElseIf ddlItemDespesaTaxa.SelectedValue <> 550 And ddlEstufagem.SelectedValue = 1 And (ddlOrigemPagamentoTaxa.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0 Or ddlMoedaCompraTaxa.SelectedValue = 0 Or txtValorTaxaCompra.Text = "" Or ddlTipoPagamentoTaxa.SelectedValue = 0) Then
            lblErroTaxa.Text = "Preencha todos os campos obrigatórios"
            divErroTaxa.Visible = True

        Else


            Dim dsPremiacao As DataSet = Con.ExecutarQuery("SELECT ISNULL(FL_PREMIACAO,0)FL_PREMIACAO,ID_ITEM_DESPESA   FROM TB_ITEM_DESPESA  WHERE ID_ITEM_DESPESA = " & ddlItemDespesaTaxa.SelectedValue)
            If dsPremiacao.Tables(0).Rows.Count > 0 Then
                If dsPremiacao.Tables(0).Rows(0).Item("FL_PREMIACAO") = False And (ddlMoedaVendaTaxa.SelectedValue = 0 Or txtValorTaxaVenda.Text = "" Or ddlDestinatarioCobrancaTaxa.SelectedValue = 0) Then
                    lblErroTaxa.Text = "Preencha todos os campos obrigatórios"
                    divErroTaxa.Visible = True
                    mpeNovoTaxa.Show()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefaultTaxas()", True)
                    Exit Sub
                End If

                If dsPremiacao.Tables(0).Rows(0).Item("FL_PREMIACAO") = True And (txtValorTaxaVenda.Text <> 0 Or txtValorTaxaVendaMin.Text <> 0) Then
                    lblErroTaxa.Text = "Não é possivel cadastrar taxa de venda de premiação!"
                    divErroTaxa.Visible = True
                    mpeNovoTaxa.Show()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefaultTaxas()", True)
                    Exit Sub
                End If

                If dsPremiacao.Tables(0).Rows(0).Item("FL_PREMIACAO") = True Then
                    txtValorTaxaVenda.Text = "0"
                    txtValorTaxaVendaCalc.Text = "0"
                    txtValorTaxaVendaMin.Text = "0"
                    ddlMoedaVendaTaxa.SelectedValue = 0
                    ddlDestinatarioCobrancaTaxa.SelectedValue = 3
                End If
            End If




            txtValorTaxaCompra.Text = txtValorTaxaCompra.Text.Replace(".", "")
            txtValorTaxaCompra.Text = txtValorTaxaCompra.Text.Replace(",", ".")

            txtValorTaxaVenda.Text = txtValorTaxaVenda.Text.Replace(".", "")
            txtValorTaxaVenda.Text = txtValorTaxaVenda.Text.Replace(",", ".")

            txtValorTaxaVendaMin.Text = txtValorTaxaVendaMin.Text.Replace(".", "")
            txtValorTaxaVendaMin.Text = txtValorTaxaVendaMin.Text.Replace(",", ".")

            txtValorTaxaCompraMin.Text = txtValorTaxaCompraMin.Text.Replace(".", "")
            txtValorTaxaCompraMin.Text = txtValorTaxaCompraMin.Text.Replace(",", ".")



            txtValorTaxaCompra.Text = txtValorTaxaCompra.Text.Replace("-", "")

            txtValorTaxaVenda.Text = txtValorTaxaVenda.Text.Replace("-", "")

            txtValorTaxaVendaMin.Text = txtValorTaxaVendaMin.Text.Replace("-", "")

            txtValorTaxaCompraMin.Text = txtValorTaxaCompraMin.Text.Replace("-", "")






            Dim ObsTaxa As String = ""
            If txtObsTaxa.Text = "" Then
                ObsTaxa = "NULL"
            Else
                ObsTaxa = txtObsTaxa.Text
                ObsTaxa = ObsTaxa.Replace("'", "''")
                ObsTaxa = "'" & ObsTaxa & "'"
            End If


            If txtIDTaxa.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroTaxa.Visible = True
                    lblErroTaxa.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else
                    ds = Con.ExecutarQuery("SELECT ISNULL(ID_TIPO_ITEM_DESPESA,0)ID_TIPO_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlItemDespesaTaxa.SelectedValue)
                    Dim OPERADOR As String = "+"
                    If ds.Tables(0).Rows(0).Item("ID_TIPO_ITEM_DESPESA") = 3 Then
                        OPERADOR = "-"
                    Else
                        OPERADOR = "+"
                    End If


                    'INSERE TAXAS
                    Dim dsCotacao As DataSet = Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (
ID_COTACAO,
ID_FORNECEDOR,
ID_ITEM_DESPESA,
ID_TIPO_PAGAMENTO,
ID_ORIGEM_PAGAMENTO,
FL_DECLARADO,
FL_DIVISAO_PROFIT,
ID_DESTINATARIO_COBRANCA,
ID_BASE_CALCULO_TAXA,
ID_MOEDA_COMPRA,
VL_TAXA_COMPRA,
VL_TAXA_COMPRA_MIN,
ID_MOEDA_VENDA,
VL_TAXA_VENDA,
VL_TAXA_VENDA_MIN,
OB_TAXAS,
QTD_BASE_CALCULO
 ) VALUES (" & txtID.Text & "," & ddlFornecedor.SelectedValue & "," & ddlItemDespesaTaxa.SelectedValue & "," & ddlTipoPagamentoTaxa.SelectedValue & "," & ddlOrigemPagamentoTaxa.SelectedValue & ",'" & ckbDeclaradoTaxa.Checked & "','" & ckbProfitTaxa.Checked & "'," & ddlDestinatarioCobrancaTaxa.SelectedValue & "," & ddlBaseCalculoTaxa.SelectedValue & "," & ddlMoedaCompraTaxa.SelectedValue & "," & OPERADOR & txtValorTaxaCompra.Text & "," & OPERADOR & txtValorTaxaCompraMin.Text & "," & ddlMoedaVendaTaxa.SelectedValue & "," & OPERADOR & txtValorTaxaVenda.Text & "," & OPERADOR & txtValorTaxaVendaMin.Text & "," & ObsTaxa & "," & txtQtdBaseCalculo.Text & " )   Select SCOPE_IDENTITY() as ID_COTACAO_TAXA ")
                    Dim ID_COTACAO_TAXA = dsCotacao.Tables(0).Rows(0).Item("ID_COTACAO_TAXA").ToString()


                    txtIDTaxa.Text = ""
                    LimparDadosTaxa()


                    Con.Fechar()
                    dgvTaxas.DataBind()
                    divSuccessTaxa.Visible = True

                    If Session("ID_STATUS") = 10 Then

                        Dim CalCotacao As New CalculaCotacao
                        Dim retorno As String = CalCotacao.CalculaCotacao(txtID.Text)

                        Dim RotinaUpdate As New RotinaUpdate
                        RotinaUpdate.UpdateTaxas(txtID.Text, ID_COTACAO_TAXA, txtProcessoCotacao.Text)
                    End If

                End If


            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroTaxa.Visible = True
                    lblErroTaxa.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else
                    ds = Con.ExecutarQuery("SELECT ISNULL(ID_TIPO_ITEM_DESPESA,0)ID_TIPO_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlItemDespesaTaxa.SelectedValue)
                    Dim OPERADOR As String = "+"
                    If ds.Tables(0).Rows(0).Item("ID_TIPO_ITEM_DESPESA") = 3 Then
                        OPERADOR = "-"
                    Else
                        OPERADOR = "+"
                    End If

                    'ALTERA TAXAS
                    Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET 
ID_ITEM_DESPESA = " & ddlItemDespesaTaxa.SelectedValue & ",
ID_TIPO_PAGAMENTO = " & ddlTipoPagamentoTaxa.SelectedValue & " ,
ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamentoTaxa.SelectedValue & ",
FL_DECLARADO = '" & ckbDeclaradoTaxa.Checked & "',
FL_DIVISAO_PROFIT = '" & ckbProfitTaxa.Checked & "',
ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCobrancaTaxa.SelectedValue & ",
ID_FORNECEDOR = " & ddlFornecedor.SelectedValue & ",
ID_BASE_CALCULO_TAXA = " & ddlBaseCalculoTaxa.SelectedValue & ",
ID_MOEDA_COMPRA = " & ddlMoedaCompraTaxa.SelectedValue & ",
VL_TAXA_COMPRA =" & OPERADOR & txtValorTaxaCompra.Text & ",
VL_TAXA_COMPRA_MIN = " & OPERADOR & txtValorTaxaCompraMin.Text & ",
ID_MOEDA_VENDA = " & ddlMoedaVendaTaxa.SelectedValue & ",
VL_TAXA_VENDA = " & OPERADOR & txtValorTaxaVenda.Text & ",
VL_TAXA_VENDA_MIN = " & OPERADOR & txtValorTaxaVendaMin.Text & ",
OB_TAXAS = " & ObsTaxa & " ,
QTD_BASE_CALCULO = " & txtQtdBaseCalculo.Text & "  
 WHERE ID_COTACAO_TAXA = " & txtIDTaxa.Text)

                    txtValorTaxaCompra.Text = txtValorTaxaCompra.Text.Replace(".", ",")
                    txtValorTaxaCompraMin.Text = txtValorTaxaCompraMin.Text.Replace(".", ",")
                    txtValorTaxaVenda.Text = txtValorTaxaVenda.Text.Replace(".", ",")
                    txtValorTaxaVendaMin.Text = txtValorTaxaVendaMin.Text.Replace(".", ",")

                    divSuccessTaxa.Visible = True
                    Con.Fechar()
                    dgvTaxas.DataBind()

                    If Session("ID_STATUS") = 10 Then

                        Dim CalCotacao As New CalculaCotacao
                        Dim retorno As String = CalCotacao.CalculaCotacao(txtID.Text)

                        Dim RotinaUpdate As New RotinaUpdate
                        RotinaUpdate.UpdateTaxas(txtID.Text, txtIDTaxa.Text, txtProcessoCotacao.Text)
                    End If

                End If


            End If


        End If

        mpeNovoTaxa.Show()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefaultTaxas()", True)

    End Sub

    Private Sub txtQtd_TextChanged(sender As Object, e As EventArgs) Handles txtQtd.TextChanged
        GridHistoricoCotacao()
    End Sub
    Sub SeparaEmail(ByVal email As String)
        'quebrar a string
        Dim palavras As String() = email.Split(New String() _
          {";"}, StringSplitOptions.RemoveEmptyEntries)

        'exibe o resultado
        For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
            Dim EmailService As New EmailService
            Dim Mensagem As String = "Cotação Aprovada! <br/><br> ID:" & txtID.Text & "<br/><br/>NUMERO COTAÇÃO: " & txtNumeroCotacao.Text & "<br/><br/>."

            Dim retorno As String = EmailService.EnviarEmail(palavras(i), "COTAÇÃO APROVADA - NVOCC", Mensagem)
        Next
    End Sub
    Private Sub ddlDestinoFrete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDestinoFrete.SelectedIndexChanged
        CarregaTabelaFrete()
    End Sub

    Private Sub ddlOrigemFrete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrigemFrete.SelectedIndexChanged
        CarregaTabelaFrete()
    End Sub

    Private Sub ddlTransportadorFrete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransportadorFrete.SelectedIndexChanged
        CarregaTabelaFrete()
    End Sub

    Sub CarregaTabelaFrete()
        If ddlDestinoFrete.SelectedValue <> 0 And ddlOrigemFrete.SelectedValue <> 0 And ddlTransportadorFrete.SelectedValue <> 0 Then
            Dim sql As String = ""

            If ddlServico.SelectedValue <= 2 And ddlAgente.SelectedValue <> 0 Then
                sql = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE CONVERT(DATE,DT_VALIDADE_FINAL,103) >= CONVERT(DATE, GETDATE(),103) AND ID_PORTO_ORIGEM = " & ddlOrigemFrete.SelectedValue & " AND ID_PORTO_DESTINO = " & ddlDestinoFrete.SelectedValue & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & " AND ID_AGENTE = " & ddlAgente.SelectedValue & "  union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "
            Else
                sql = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE CONVERT(DATE,DT_VALIDADE_FINAL,103) >= CONVERT(DATE, GETDATE(),103) AND ID_PORTO_ORIGEM = " & ddlOrigemFrete.SelectedValue & " AND ID_PORTO_DESTINO = " & ddlDestinoFrete.SelectedValue & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & " union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "
            End If

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                dsFreteTransportador.SelectCommand = sql
                ddlFreteTransportador_Frete.DataBind()
            End If
            Con.Fechar()
        End If
    End Sub

    Private Sub ddlFreteTransportador_Frete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFreteTransportador_Frete.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT QT_DIAS_TRANSITTIME_INICIAL, QT_DIAS_TRANSITTIME_FINAL,QT_DIAS_TRANSITTIME_MEDIA, ID_TIPO_FREQUENCIA,ID_TIPO_CARGA,ID_VIA_ROTA,VL_FREQUENCIA,ID_MOEDA_FRETE,NM_TAXAS_INCLUDED,ID_PORTO_ESCALA,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,(select Sum(B.VL_COMPRA) from TB_TARIFARIO_FRETE_TRANSPORTADOR b where A.ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR AND convert(date,DT_VALIDADE_FINAL,103) >= convert(date, getdate(),103) )VL_COMPRA  FROM TB_FRETE_TRANSPORTADOR A WHERE A.ID_FRETE_TRANSPORTADOR = " & ddlFreteTransportador_Frete.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                ddlTipoCargaFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VIA_ROTA")) Then
                ddlRotaFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VIA_ROTA")
                If ds.Tables(0).Rows(0).Item("ID_VIA_ROTA") = 1 Then
                    divEscala.Attributes.CssStyle.Add("display", "none")
                ElseIf ds.Tables(0).Rows(0).Item("ID_VIA_ROTA") = 2 Then
                    divEscala.Attributes.CssStyle.Add("display", "block")
                End If
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FREQUENCIA")) Then
                txtValorFrequenciaFrete.Text = ds.Tables(0).Rows(0).Item("VL_FREQUENCIA")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE")) Then
                ddlMoedaFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TAXAS_INCLUDED")) Then
                txtIncludedFrete.Text = ds.Tables(0).Rows(0).Item("NM_TAXAS_INCLUDED")
            Else
                txtIncludedFrete.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_TRANSITTIME_INICIAL")) Then
                txtTTimeFreteInicial.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_TRANSITTIME_INICIAL")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_TRANSITTIME_FINAL")) Then
                txtTTimeFreteFinal.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_TRANSITTIME_FINAL")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_TRANSITTIME_MEDIA")) Then
                txtTTimeFreteMedia.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_TRANSITTIME_MEDIA")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_FREQUENCIA")) Then
                ddlFrequenciaFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FREQUENCIA")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA")) Then
                ddlEscala1Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA2")) Then
                ddlEscala2Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA2")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA3")) Then
                ddlEscala3Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA3")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMPRA")) Then
                Session("VL_COMPRA") = ds.Tables(0).Rows(0).Item("VL_COMPRA")
            End If

            If txtTTimeFreteInicial.Text <> "" And txtTTimeFreteInicial.Text <> "" Then
                Dim TTInicial As Integer = txtTTimeFreteInicial.Text
                Dim TTFinal As Integer = txtTTimeFreteFinal.Text
                Dim TTMedia As Integer = (TTFinal + TTInicial)
                TTMedia = TTMedia / 2
                txtTTimeFreteMedia.Text = TTMedia
            End If

        End If


    End Sub

    Private Sub dgvHistoricoCotacao_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvHistoricoCotacao.RowCommand
        If e.CommandName = "Selecionar" Then
            Dim ID As String = e.CommandArgument
            Dim url As String = ""
            url = "GeraPDF.aspx?c=" & ID & "&l=p&f=i"
            Response.Write("<script>")
            Response.Write("window.open('" & url & "','_blank')")
            Response.Write("</script>")
        End If
    End Sub

    Private Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCliente.SelectedIndexChanged

        If ddlCliente.SelectedValue <> 0 Then
            Session("ID_CLIENTE") = ddlCliente.SelectedValue
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim sql As String = "SELECT ID_CONTATO, NM_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & Session("ID_CLIENTE") & " union SELECT  0, '   Selecione' ORDER BY ID_CONTATO"
            Dim ds As DataSet = Con.ExecutarQuery(sql)
            If ds.Tables(0).Rows.Count > 1 Then
                dsContato.SelectCommand = sql
                ddlContato.DataBind()
                ddlContato.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CONTATO")
            Else
                dsContato.SelectCommand = "SELECT  0 as ID_CONTATO, '  Selecione' as NM_CONTATO"
                ddlContato.DataBind()
                ddlContato.SelectedValue = 0
            End If

            sql = "SELECT ID_CLIENTE_FINAL,SUBSTRING(NM_CLIENTE_FINAL,0,50) +' ('+ NR_CNPJ +')' NM_CLIENTE_FINAL FROM TB_CLIENTE_FINAL WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue & "
union SELECT  0, '    Selecione' ORDER BY NM_CLIENTE_FINAL"
            ds = Con.ExecutarQuery(sql)
            If ds.Tables(0).Rows.Count = 1 Then
                dsClienteFinal.SelectCommand = "SELECT  0 as ID_CLIENTE_FINAL, '    Selecione' as NM_CLIENTE_FINAL"
                ddlClienteFinal.DataBind()
                divClienteFinal.Attributes.CssStyle.Add("display", "none")
                ddlClienteFinal.SelectedValue = 0
            Else
                dsClienteFinal.SelectCommand = sql
                ddlClienteFinal.DataBind()
                divClienteFinal.Attributes.CssStyle.Add("display", "block")
                ddlClienteFinal.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL")
            End If


            sql = "SELECT ID_VENDEDOR FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue
            Dim ds1 As DataSet = Con.ExecutarQuery(sql)

            sql = "SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE (FL_VENDEDOR = 1  AND FL_ATIVO = 1)  OR ID_PARCEIRO = " & ds1.Tables(0).Rows(0).Item("ID_VENDEDOR") & " union SELECT  0, 'Selecione' ORDER BY ID_PARCEIRO"
            ds = Con.ExecutarQuery(sql)
            If ds.Tables(0).Rows.Count > 1 Then
                dsVendedor.SelectCommand = sql
                ddlVendedor.DataBind()
                ddlVendedor.SelectedValue = ds1.Tables(0).Rows(0).Item("ID_VENDEDOR")
            Else
                dsVendedor.SelectCommand = "SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE (FL_VENDEDOR = 1  AND FL_ATIVO = 1)  OR ID_PARCEIRO = " & ddlCliente.SelectedValue & " union SELECT  0, ' Selecione' ORDER BY NM_RAZAO" 'geral
                ddlVendedor.DataBind()
            End If

            Session("ID_CLIENTE") = ddlCliente.SelectedValue

            GridHistoricoCotacao()
            '   GridHistoricoFrete()
        Else
            ddlClienteFinal.DataBind()
            ddlContato.DataBind()
            ddlVendedor.DataBind()
        End If


    End Sub




    Sub ImportaTaxas()

        Dim comex As Integer = 0
        Dim via As Integer = 0

        Dim ID_PORTO As Integer = 0
        Dim FILTROCOMEX As String = ""
        Dim FILTROVIA As String = ""

        If ddlServico.SelectedValue = 1 Then
            'AGENCIAMENTO DE IMPORTACAO MARITIMA
            comex = 1
            ID_PORTO = ddlDestinoFrete.SelectedValue
            via = 1
        ElseIf ddlServico.SelectedValue = 4 Then
            'AGENCIAMENTO DE EXPORTACAO MARITIMA
            comex = 2
            ID_PORTO = ddlOrigemFrete.SelectedValue
            via = 1
        ElseIf ddlServico.SelectedValue = 5 Then
            'AGENCIAMENTO DE EXPORTAÇÃO AEREO
            comex = 2
            ID_PORTO = ddlOrigemFrete.SelectedValue
            via = 4
        ElseIf ddlServico.SelectedValue = 2 Then
            'AGENCIAMENTO DE IMPORTACAO AEREO
            comex = 1
            ID_PORTO = ddlDestinoFrete.SelectedValue
            via = 4
        End If

        If comex > 0 Then
            FILTROCOMEX = " AND ID_TIPO_COMEX = " & comex
        End If

        If via > 0 Then
            FILTROVIA = " AND ID_VIATRANSPORTE = " & via
        End If

        Dim ID_DESTINATARIO_COBRANCA As Integer = 1
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If ddlServico.SelectedValue > 2 Then
            'EXPO
            ID_DESTINATARIO_COBRANCA = 0

        Else
            'IMPO
            ds = Con.ExecutarQuery("SELECT CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                ID_DESTINATARIO_COBRANCA = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
            End If
        End If


        Dim dsTaxas As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
        If dsTaxas.Tables(0).Rows(0).Item("QTD") = 0 Then
            ds = Con.ExecutarQuery("SELECT ID_TAXA_CLIENTE,ID_ITEM_DESPESA FROM TB_TAXA_CLIENTE A WHERE ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & " AND ID_PARCEIRO = " & ddlCliente.SelectedValue & FILTROCOMEX & FILTROVIA)
            If ds.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In ds.Tables(0).Rows
                    Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR,VL_TAXA_COMPRA_MIN,VL_TAXA_VENDA_MIN)
SELECT " & txtID.Text & " , ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_COMPRA)ID_MOEDA_COMPRA,VL_TAXA_COMPRA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_VENDA)ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,1,ID_ORIGEM_PAGAMENTO, 

CASE 
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then 3
ELSE " & ID_DESTINATARIO_COBRANCA & " END ID_DESTINATARIO_COBRANCA ,

1,getdate(),FL_TAXA_TRANSPORTADOR,

CASE 
WHEN ISNULL(FL_TAXA_TRANSPORTADOR,0) = 1 
then (SELECT ID_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & " )
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then (SELECT ID_PARCEIRO_INDICADOR FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & " )
ELSE 0 END ID_FORNECEDOR,
VL_TARIFA_MINIMA_COMPRA,
VL_TARIFA_MINIMA
FROM TB_TAXA_CLIENTE
WHERE ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_PARCEIRO = " & ddlCliente.SelectedValue & " AND ID_TAXA_CLIENTE = " & linha.Item("ID_TAXA_CLIENTE"))
                Next

                divDeleteTaxas.Visible = True
                lblDeleteTaxas.Text = "Ação realizada com sucesso!"

            End If

            If ddlFreteTransportador_Frete.SelectedValue = 0 Then

                ds = Con.ExecutarQuery("SELECT A.ID_TAXA_LOCAL_TRANSPORTADOR,A.ID_ITEM_DESPESA,A.ID_ORIGEM_PAGAMENTO,A.ID_BASE_CALCULO
FROM TB_TAXA_LOCAL_TRANSPORTADOR A

INNER JOIN (
SELECT ID_ITEM_DESPESA,ID_BASE_CALCULO, MAX(DT_VALIDADE_INICIAL) AS DT_VALIDADE_INICIAL
FROM TB_TAXA_LOCAL_TRANSPORTADOR
WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROVIA & FILTROCOMEX & "

AND ID_BASE_CALCULO IN (SELECT ID_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA C WHERE C.ID_TIPO_CONTAINER IS NULL OR C.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text & "))


AND CONVERT(DATE,DT_VALIDADE_INICIAL,103) <= (SELECT CONVERT(DATE,DT_VALIDADE_COTACAO,103) FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & ")
GROUP BY  ID_ITEM_DESPESA,ID_BASE_CALCULO) B

ON  A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA
AND A.ID_BASE_CALCULO = B.ID_BASE_CALCULO
AND A.DT_VALIDADE_INICIAL = B.DT_VALIDADE_INICIAL

WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROVIA & FILTROCOMEX & "

AND A.ID_BASE_CALCULO IN (SELECT ID_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA C WHERE C.ID_TIPO_CONTAINER IS NULL OR C.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text & "))


AND B.DT_VALIDADE_INICIAL <= (SELECT DT_VALIDADE_COTACAO FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & ")")
                If ds.Tables(0).Rows.Count > 0 Then

                    For Each linha As DataRow In ds.Tables(0).Rows

                        Dim dsVerificaExistencia As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE FL_TAXA_TRANSPORTADOR = 0 AND ID_ITEM_DESPESA = " & linha.Item("ID_ITEM_DESPESA") & " AND ID_ORIGEM_PAGAMENTO = " & linha.Item("ID_ORIGEM_PAGAMENTO") & " AND ID_BASE_CALCULO_TAXA = " & linha.Item("ID_BASE_CALCULO") & " AND ISNULL(VL_TAXA_COMPRA,0) = 0 AND ID_COTACAO = " & txtID.Text)

                        If dsVerificaExistencia.Tables(0).Rows.Count > 0 Then
                            Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET 

ID_MOEDA_COMPRA =  (SELECT ID_MOEDA FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") & "),

VL_TAXA_COMPRA =  (SELECT VL_TAXA_LOCAL_COMPRA FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") & "),

FL_TAXA_TRANSPORTADOR = 1,

ID_FORNECEDOR  =  (SELECT ID_TRANSPORTADOR FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") & ")
 
WHERE ID_COTACAO_TAXA =  " & dsVerificaExistencia.Tables(0).Rows(0).Item("ID_COTACAO_TAXA"))
                        Else
                            Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,VL_TAXA_COMPRA,ID_MOEDA_COMPRA,VL_TAXA_VENDA,ID_MOEDA_VENDA,ID_BASE_CALCULO_TAXA,FL_TAXA_TRANSPORTADOR,ID_DESTINATARIO_COBRANCA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO,QTD_BASE_CALCULO)   
SELECT " & txtID.Text & " , ID_ITEM_DESPESA, VL_TAXA_LOCAL_COMPRA, ID_MOEDA,VL_TAXA_LOCAL_COMPRA, ID_MOEDA,ID_BASE_CALCULO,1,

CASE 
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then 3
ELSE " & ID_DESTINATARIO_COBRANCA & " END ID_DESTINATARIO_COBRANCA ,



1,ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,1,getdate(),QTD_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR"))
                        End If

                    Next
                    divDeleteTaxas.Visible = True
                    lblDeleteTaxas.Text = "Ação realizada com sucesso!"
                End If

            Else
                ds = Con.ExecutarQuery("SELECT ID_TABELA_FRETE_TAXA,ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_ORIGEM_PAGAMENTO FROM TB_TABELA_FRETE_TAXA A WHERE ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & "  
   AND ID_BASE_CALCULO_TAXA IN (SELECT ID_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA C WHERE C.ID_TIPO_CONTAINER IS NULL OR C.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text & "))")

                If ds.Tables(0).Rows.Count > 0 Then
                    For Each linha As DataRow In ds.Tables(0).Rows
                        Dim dsVerificaExistencia As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE FL_TAXA_TRANSPORTADOR = 0 AND ID_ITEM_DESPESA = " & linha.Item("ID_ITEM_DESPESA") & " AND ID_ORIGEM_PAGAMENTO = " & linha.Item("ID_ORIGEM_PAGAMENTO") & " AND ID_BASE_CALCULO_TAXA = " & linha.Item("ID_BASE_CALCULO_TAXA") & " AND ISNULL(VL_TAXA_COMPRA,0) = 0 AND ID_COTACAO = " & txtID.Text)

                        If dsVerificaExistencia.Tables(0).Rows.Count > 0 Then

                            Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET 
ID_MOEDA_COMPRA = (SELECT ID_MOEDA_COMPRA FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA") & "),
VL_TAXA_COMPRA = (SELECT VL_TAXA_COMPRA FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA") & "),
VL_TAXA_COMPRA_MIN = (SELECT VL_TAXA_COMPRA_MIN FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA") & "),
FL_TAXA_TRANSPORTADOR = 1,
ID_FORNECEDOR  =  (SELECT ID_TRANSPORTADOR FROM TB_FRETE_TRANSPORTADOR A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & ")
 
WHERE ID_COTACAO_TAXA =  " & dsVerificaExistencia.Tables(0).Rows(0).Item("ID_COTACAO_TAXA"))
                        Else
                            Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA_MIN,FL_TAXA_TRANSPORTADOR,ID_DESTINATARIO_COBRANCA,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO,QTD_BASE_CALCULO)
                    SELECT " & txtID.Text & ",ID_ITEM_DESPESA,1,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,
 CASE 
 WHEN ID_MOEDA_VENDA IS NULL THEN ID_MOEDA_COMPRA 
 WHEN ID_MOEDA_VENDA = 0 THEN ID_MOEDA_COMPRA
 ELSE ID_MOEDA_VENDA END ID_MOEDA_VENDA,

 CASE 
 WHEN VL_TAXA_VENDA IS NULL THEN VL_TAXA_COMPRA 
 WHEN VL_TAXA_VENDA = 0 THEN VL_TAXA_COMPRA
 ELSE VL_TAXA_VENDA END VL_TAXA_VENDA,

 CASE 
 WHEN VL_TAXA_VENDA_MIN IS NULL THEN VL_TAXA_COMPRA_MIN 
 WHEN VL_TAXA_VENDA_MIN = 0 THEN VL_TAXA_COMPRA_MIN
 ELSE VL_TAXA_VENDA_MIN END VL_TAXA_VENDA_MIN

 
 ,VL_TAXA_COMPRA_MIN,1, 

CASE 
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then 3
ELSE " & ID_DESTINATARIO_COBRANCA & " END ID_DESTINATARIO_COBRANCA , 

(SELECT ID_TRANSPORTADOR FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR  = A.ID_FRETE_TRANSPORTADOR) ID_TRANSPORTADOR,1,getdate(),QTD_BASE_CALCULO  FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA"))
                        End If

                    Next


                    divDeleteTaxas.Visible = True
                    lblDeleteTaxas.Text = "Ação realizada com sucesso!"

                End If

            End If
        Else




            ''CASO A TABELA ESTEJA JA TENHA REGISTROS

            ds = Con.ExecutarQuery("SELECT ID_TAXA_CLIENTE,ID_ITEM_DESPESA FROM TB_TAXA_CLIENTE A WHERE ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & " AND ID_PARCEIRO = " & ddlCliente.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_ITEM_DESPESA NOT IN(SELECT ID_ITEM_DESPESA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text & " AND VL_TAXA_COMPRA = A.VL_TAXA_COMPRA AND VL_TAXA_VENDA = A.VL_TAXA_VENDA)")
            If ds.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In ds.Tables(0).Rows
                    Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR,VL_TAXA_COMPRA_MIN,VL_TAXA_VENDA_MIN)
SELECT " & txtID.Text & " , ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_COMPRA)ID_MOEDA_COMPRA,VL_TAXA_COMPRA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_VENDA)ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,1,ID_ORIGEM_PAGAMENTO,

CASE 
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then 3
ELSE " & ID_DESTINATARIO_COBRANCA & " END ID_DESTINATARIO_COBRANCA ,

1,getdate(),FL_TAXA_TRANSPORTADOR,

CASE 
WHEN ISNULL(FL_TAXA_TRANSPORTADOR,0) = 1 
then (SELECT ID_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & " )
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then (SELECT ID_PARCEIRO_INDICADOR FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & " )
ELSE 0 END ID_FORNECEDOR,
VL_TARIFA_MINIMA_COMPRA,
VL_TARIFA_MINIMA
FROM TB_TAXA_CLIENTE WHERE ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_PARCEIRO = " & ddlCliente.SelectedValue & " AND ID_TAXA_CLIENTE = " & linha.Item("ID_TAXA_CLIENTE"))
                Next

                divDeleteTaxas.Visible = True
                lblDeleteTaxas.Text = "Ação realizada com sucesso!"

            End If

            If ddlFreteTransportador_Frete.SelectedValue = 0 Then


                ds = Con.ExecutarQuery("SELECT A.ID_TAXA_LOCAL_TRANSPORTADOR,A.ID_ITEM_DESPESA,A.ID_ORIGEM_PAGAMENTO,A.ID_BASE_CALCULO
FROM TB_TAXA_LOCAL_TRANSPORTADOR A

INNER JOIN (
SELECT ID_ITEM_DESPESA,ID_BASE_CALCULO, MAX(DT_VALIDADE_INICIAL) AS DT_VALIDADE_INICIAL
FROM TB_TAXA_LOCAL_TRANSPORTADOR
WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROVIA & FILTROCOMEX & "

AND ID_BASE_CALCULO IN (SELECT ID_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA C WHERE C.ID_TIPO_CONTAINER IS NULL OR C.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text & "))


AND CONVERT(DATE,DT_VALIDADE_INICIAL,103) <= (SELECT CONVERT(DATE,DT_VALIDADE_COTACAO,103) FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & ")
GROUP BY  ID_ITEM_DESPESA,ID_BASE_CALCULO) B

ON  A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA
AND A.ID_BASE_CALCULO = B.ID_BASE_CALCULO
AND A.DT_VALIDADE_INICIAL = B.DT_VALIDADE_INICIAL

WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROVIA & FILTROCOMEX & "

AND A.ID_BASE_CALCULO IN (SELECT ID_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA C WHERE C.ID_TIPO_CONTAINER IS NULL OR C.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text & "))


AND B.DT_VALIDADE_INICIAL <= (SELECT DT_VALIDADE_COTACAO FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & ") AND A.ID_ITEM_DESPESA NOT IN (SELECT ID_ITEM_DESPESA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text & " AND VL_TAXA_COMPRA = A.VL_TAXA_LOCAL_COMPRA )")
                If ds.Tables(0).Rows.Count > 0 Then

                    For Each linha As DataRow In ds.Tables(0).Rows

                        Dim dsVerificaExistencia As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE ID_ITEM_DESPESA = " & linha.Item("ID_ITEM_DESPESA") & " AND ID_ORIGEM_PAGAMENTO = " & linha.Item("ID_ORIGEM_PAGAMENTO") & " AND ID_BASE_CALCULO_TAXA = " & linha.Item("ID_BASE_CALCULO") & " AND ISNULL(VL_TAXA_COMPRA,0) = 0 AND ID_COTACAO = " & txtID.Text)

                        If dsVerificaExistencia.Tables(0).Rows.Count > 0 Then
                            Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET 

ID_MOEDA_COMPRA =  (SELECT ID_MOEDA FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") & "),

VL_TAXA_COMPRA =  (SELECT VL_TAXA_LOCAL_COMPRA FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") & "),

FL_TAXA_TRANSPORTADOR = 1,

ID_FORNECEDOR  =  (SELECT ID_TRANSPORTADOR FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") & ")
 
WHERE ID_COTACAO_TAXA =  " & dsVerificaExistencia.Tables(0).Rows(0).Item("ID_COTACAO_TAXA"))
                        Else
                            Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,VL_TAXA_COMPRA,ID_MOEDA_COMPRA,VL_TAXA_VENDA,ID_MOEDA_VENDA,ID_BASE_CALCULO_TAXA,FL_TAXA_TRANSPORTADOR,ID_DESTINATARIO_COBRANCA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO,QTD_BASE_CALCULO)   
SELECT " & txtID.Text & " , ID_ITEM_DESPESA, VL_TAXA_LOCAL_COMPRA, ID_MOEDA,VL_TAXA_LOCAL_COMPRA, ID_MOEDA,ID_BASE_CALCULO,1,

CASE 
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then 3
ELSE " & ID_DESTINATARIO_COBRANCA & " END ID_DESTINATARIO_COBRANCA ,


1,ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,1,getdate(),QTD_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR"))
                        End If



                    Next
                    divDeleteTaxas.Visible = True
                    lblDeleteTaxas.Text = "Ação realizada com sucesso!"
                End If

            Else
                ds = Con.ExecutarQuery("SELECT ID_TABELA_FRETE_TAXA,ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_ORIGEM_PAGAMENTO FROM TB_TABELA_FRETE_TAXA A WHERE ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & "
  AND ID_ITEM_DESPESA NOT IN (SELECT ID_ITEM_DESPESA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text & " AND VL_TAXA_COMPRA = A.VL_TAXA_COMPRA AND VL_TAXA_VENDA = ISNULL(A.VL_TAXA_VENDA,A.VL_TAXA_COMPRA))
   AND ID_BASE_CALCULO_TAXA IN (SELECT ID_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA C WHERE C.ID_TIPO_CONTAINER IS NULL OR C.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text & "))")


                If ds.Tables(0).Rows.Count > 0 Then
                    For Each linha As DataRow In ds.Tables(0).Rows
                        Dim dsVerificaExistencia As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE ID_ITEM_DESPESA = " & linha.Item("ID_ITEM_DESPESA") & " AND ID_ORIGEM_PAGAMENTO = " & linha.Item("ID_ORIGEM_PAGAMENTO") & " AND ID_BASE_CALCULO_TAXA = " & linha.Item("ID_BASE_CALCULO_TAXA") & " AND ISNULL(VL_TAXA_COMPRA,0) = 0 AND ID_COTACAO = " & txtID.Text)

                        If dsVerificaExistencia.Tables(0).Rows.Count > 0 Then

                            Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET 
ID_MOEDA_COMPRA = (SELECT ID_MOEDA_COMPRA FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA") & "),
VL_TAXA_COMPRA = (SELECT VL_TAXA_COMPRA FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA") & "),
VL_TAXA_COMPRA_MIN = (SELECT VL_TAXA_COMPRA_MIN FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA") & "),
FL_TAXA_TRANSPORTADOR = 1,
ID_FORNECEDOR  =  (SELECT ID_TRANSPORTADOR FROM TB_FRETE_TRANSPORTADOR A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & ")
 
WHERE ID_COTACAO_TAXA =  " & dsVerificaExistencia.Tables(0).Rows(0).Item("ID_COTACAO_TAXA"))
                        Else
                            Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA_MIN,FL_TAXA_TRANSPORTADOR,ID_DESTINATARIO_COBRANCA,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO,QTD_BASE_CALCULO)
                    SELECT " & txtID.Text & ",ID_ITEM_DESPESA,1,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,
 CASE 
 WHEN ID_MOEDA_VENDA IS NULL THEN ID_MOEDA_COMPRA 
 WHEN ID_MOEDA_VENDA = 0 THEN ID_MOEDA_COMPRA
 ELSE ID_MOEDA_VENDA END ID_MOEDA_VENDA,

 CASE 
 WHEN VL_TAXA_VENDA IS NULL THEN VL_TAXA_COMPRA 
 WHEN VL_TAXA_VENDA = 0 THEN VL_TAXA_COMPRA
 ELSE VL_TAXA_VENDA END VL_TAXA_VENDA,

 CASE 
 WHEN VL_TAXA_VENDA_MIN IS NULL THEN VL_TAXA_COMPRA_MIN 
 WHEN VL_TAXA_VENDA_MIN = 0 THEN VL_TAXA_COMPRA_MIN
 ELSE VL_TAXA_VENDA_MIN END VL_TAXA_VENDA_MIN

 
 ,VL_TAXA_COMPRA_MIN,1,

CASE 
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then 3
ELSE " & ID_DESTINATARIO_COBRANCA & " END ID_DESTINATARIO_COBRANCA ,

(SELECT ID_TRANSPORTADOR FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR  = A.ID_FRETE_TRANSPORTADOR) ID_TRANSPORTADOR,1,getdate(),QTD_BASE_CALCULO  FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA"))
                        End If

                    Next


                    divDeleteTaxas.Visible = True
                    lblDeleteTaxas.Text = "Ação realizada com sucesso!"

                End If

            End If


        End If


        dsFornecedor.DataBind()
        ddlFornecedor.DataBind()
    End Sub

    Private Sub ddlRotaFrete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRotaFrete.SelectedIndexChanged
        If ddlRotaFrete.SelectedValue = 1 Then
            divEscala.Attributes.CssStyle.Add("display", "none")
        ElseIf ddlRotaFrete.SelectedValue = 2 Then
            divEscala.Attributes.CssStyle.Add("display", "block")
        End If
    End Sub

    Private Sub txtQtdContainerMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtQtdContainerMercadoria.TextChanged

        If txtQtdContainerMercadoria.Text = "" Then
            txtQtdContainerMercadoria.Text = 0
        End If

        If txtFreteCompraMercadoriaUnitario.Text = "" Then
            txtFreteCompraMercadoriaUnitario.Text = 0
        End If
        If txtFreteVendaMercadoriaUnitario.Text = "" Then
            txtFreteVendaMercadoriaUnitario.Text = 0
        End If

        If ddlFreteTransportador_Frete.SelectedValue <> 0 Then


            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT QT_DIAS_FREETIME,isnull(VL_COMPRA,0)VL_COMPRA from TB_TARIFARIO_FRETE_TRANSPORTADOR where ID_FRETE_TRANSPORTADOR = (SELECT ID_FRETE_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") AND ID_TIPO_CONTAINER = " & ddlTipoContainerMercadoria.SelectedValue & " AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and  convert(date,DT_VALIDADE_FINAL,103)")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")) Then
                    txtFreeTimeMercadoria.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMPRA")) Then
                    txtFreteCompraMercadoriaUnitario.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")

                    If txtQtdContainerMercadoria.Text > 0 Then

                        txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text
                        txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text
                    Else
                        txtFreteCompraMercadoriaCalc.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")
                        txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text
                    End If
                End If
            Else
                txtFreteCompraMercadoriaCalc.Text = 0
                txtFreteCompraMercadoriaUnitario.Text = 0
                txtFreeTimeMercadoria.Text = 0
            End If
        Else
            If txtQtdContainerMercadoria.Text > 0 Then
                txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text
                txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text
            Else
                txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaUnitario.Text
                txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text
            End If
        End If

    End Sub

    Private Sub txtTTimeFreteFinal_TextChanged(sender As Object, e As EventArgs) Handles txtTTimeFreteFinal.TextChanged
        If txtTTimeFreteInicial.Text <> "" And txtTTimeFreteFinal.Text <> "" Then

            Dim TTInicial As Integer = txtTTimeFreteInicial.Text
            Dim TTFinal As Integer = txtTTimeFreteFinal.Text
            Dim TTMedia As Integer = (TTFinal + TTInicial)
            TTMedia = TTMedia / 2
            txtTTimeFreteMedia.Text = TTMedia

        End If
    End Sub

    Private Sub txtTTimeFreteInicial_TextChanged(sender As Object, e As EventArgs) Handles txtTTimeFreteInicial.TextChanged
        If txtTTimeFreteInicial.Text <> "" And txtTTimeFreteFinal.Text <> "" Then

            Dim TTInicial As Integer = txtTTimeFreteInicial.Text
            Dim TTFinal As Integer = txtTTimeFreteFinal.Text
            Dim TTMedia As Integer = (TTFinal + TTInicial)
            TTMedia = TTMedia / 2
            txtTTimeFreteMedia.Text = TTMedia

        End If
    End Sub

    Private Sub txtFreteVenda_TextChanged(sender As Object, e As EventArgs) Handles txtFreteVenda.TextChanged
        If txtFreteVenda.Text <> "" And txtFreteCompra.Text <> "" Then

            Dim VENDA As Double = txtFreteVenda.Text
            Dim COMPRA As Double = txtFreteCompra.Text
            If VENDA < COMPRA Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "Func()", True)
            End If
        End If
    End Sub

    Sub NumeroProcesso()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim PROCESSO_FINAL As String = ""
        Dim ID_BL As String
        Dim ID_BL_OLD As String = 0
        Dim OB_CLIENTE As String = ""
        Dim OB_AGENTE_INTERNACIONAL As String = ""
        Dim OB_COMERCIAL As String = ""
        Dim OB_OPERACIONAL_INTERNA As String = ""
        Dim HBL As String = "0"



        ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Processo_" & Now.Year.ToString & " NRSEQUENCIALPROCESSO")
        Dim NRSEQUENCIALPROCESSO As Integer = ds.Tables(0).Rows(0).Item("NRSEQUENCIALPROCESSO")
        Dim ano_atual = Now.Year.ToString.Substring(2)
        Dim SIGLA_PROCESSO As String
        Dim mes_atual As String
        If Now.Month < 10 Then
            mes_atual = "0" & Now.Month.ToString
        Else
            mes_atual = Now.Month.ToString
        End If

        ds = Con.ExecutarQuery("Select A.ID_SERVICO,(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO
                            from TB_COTACAO A Where A.ID_COTACAO = " & txtID.Text)

        SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")


        PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

        Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "', ANOSEQUENCIALPROCESSO = year(getdate()) ")

        Con.ExecutarQuery("UPDATE TB_COTACAO SET NR_PROCESSO_GERADO = '" & PROCESSO_FINAL & "' WHERE ID_COTACAO = " & txtID.Text)


        txtProcessoCotacao.Text = PROCESSO_FINAL



        Dim dsBL As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,GRAU,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_PARCEIRO_TRANSPORTADOR,ID_COTACAO,DT_ABERTURA,VL_PROFIT_DIVISAO,ID_PROFIT_DIVISAO,VL_FRETE,ID_MOEDA_FRETE,ID_PARCEIRO_VENDEDOR,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_TIPO_PAGAMENTO,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR,VL_CARGA,FINAL_DESTINATION, ID_PARCEIRO_RODOVIARIO,VL_PESO_TAXADO,VL_M3) 
SELECT '" & PROCESSO_FINAL & "','C', " & ddlServico.SelectedValue & ",ID_CLIENTE,ID_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,GETDATE(),VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,VL_TOTAL_FRETE_VENDA,ID_MOEDA_FRETE,ID_VENDEDOR,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_TIPO_PAGAMENTO,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR, CASE WHEN ID_PARCEIRO_IMPORTADOR IS NULL THEN ID_CLIENTE WHEN ID_PARCEIRO_IMPORTADOR = 0 THEN ID_CLIENTE ELSE ID_PARCEIRO_IMPORTADOR END ID_PARCEIRO_IMPORTADOR, (SELECT (ISNULL(SUM(VL_CARGA),0))
        FROM TB_COTACAO_MERCADORIA B WHERE A.ID_COTACAO = B.ID_COTACAO ),FINAL_DESTINATION,ID_PARCEIRO_RODOVIARIO,VL_PESO_TAXADO,VL_TOTAL_M3 FROM TB_COTACAO A WHERE A.ID_COTACAO = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_BL ")

        ID_BL = dsBL.Tables(0).Rows(0).Item("ID_BL").ToString()


        'UPDATE INSERINDO ID_BL NAS REFERENCIAS DA COTAÇÃO
        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET ID_BL = " & ID_BL & " WHERE ID_COTACAO = " & txtID.Text)


        'TAXAS COMPRAS
        Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF,ID_COTACAO_TAXA,QTD_BASE_CALCULO ) 
SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'P',ID_FORNECEDOR,'COTA',ID_COTACAO_TAXA,QTD_BASE_CALCULO FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA <> 0 AND ID_COTACAO = " & txtID.Text)

        'TAXAS VENDA
        Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF,ID_COTACAO_TAXA,QTD_BASE_CALCULO) 
 SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_CALCULADO,VL_TAXA_VENDA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'R',
 
 CASE 
 WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
 THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") 
 
 WHEN ID_DESTINATARIO_COBRANCA = 2
 THEN (SELECT ID_AGENTE_INTERNACIONAL FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ")
 
 WHEN ID_DESTINATARIO_COBRANCA = 7
 THEN (SELECT ID_PARCEIRO_RODOVIARIO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ")

 WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") = 0
 THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ")
 
 WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") <> 0 then
 (SELECT ID_PARCEIRO_IMPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") 

 ELSE NULL
 END ID_PARCEIRO_EMPRESA,
 

CASE
 WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
 THEN 1
 ELSE ID_DESTINATARIO_COBRANCA
 END ID_DESTINATARIO_COBRANCA,'COTA',ID_COTACAO_TAXA,QTD_BASE_CALCULO

FROM TB_COTACAO_TAXA WHERE VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA <> 0 AND  ID_COTACAO = " & txtID.Text)

        Dim FL_PROFIT_FRETE As Integer = 0
        If ddlDivisaoProfit.SelectedValue <> 0 Then
            Dim dsProfit As DataSet = Con.ExecutarQuery("SELECT isnull(FL_PROFIT_FRETE,0)FL_PROFIT_FRETE FROM [dbo].TB_TIPO_DIVISAO_PROFIT WHERE ID_TIPO_DIVISAO_PROFIT = " & ddlDivisaoProfit.SelectedValue)
            FL_PROFIT_FRETE = dsProfit.Tables(0).Rows(0).Item("FL_PROFIT_FRETE")
        End If



        Dim ID_BASE_CALCULO As Integer

        If ddlEstufagem.SelectedValue = 1 Then

            ID_BASE_CALCULO = 5 'VALOR FIXO
            'FRETE COMPRA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,FL_TAXA_TRANSPORTADOR,CD_ORIGEM_INF)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA_MIN," & ID_BL & ",'P', " & FL_PROFIT_FRETE & ",
 
 ID_TRANSPORTADOR AS ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA,
 1,'COTA'
 
 FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)

        ElseIf ddlEstufagem.SelectedValue = 2 Then

            ID_BASE_CALCULO = 13 'POR TON / M³
        Else
            ID_BASE_CALCULO = 0
        End If


        If ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then
            ID_BASE_CALCULO = 42

            'FRETE COMPRA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,FL_TAXA_TRANSPORTADOR,CD_ORIGEM_INF,FL_DECLARADO,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA * VL_PESO_TAXADO,VL_TOTAL_FRETE_COMPRA_MIN," & ID_BL & ",'P',  ISNULL(FL_FRETE_PROFIT,0)FL_FRETE_PROFIT ,
 
 ID_TRANSPORTADOR AS ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA,
 1,'COTA',ISNULL(FL_FRETE_DECLARADO,0)FL_FRETE_DECLARADO,ID_TIPO_PAGAMENTO,
 CASE 
 WHEN ID_SERVICO in (1,2) and ID_TIPO_PAGAMENTO = 1
 THEN 1

WHEN ID_SERVICO  in (1,2) and ID_TIPO_PAGAMENTO = 2
THEN 2

 WHEN ID_SERVICO in (4,5) and ID_TIPO_PAGAMENTO = 1
 THEN 2

WHEN ID_SERVICO  in (4,5) and ID_TIPO_PAGAMENTO = 2
THEN 1

ELSE 0
end ID_ORIGEM_PAGAMENTO
 
 FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)


            'FRETE VENDA AEREO
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF,ID_ORIGEM_PAGAMENTO,FL_DECLARADO)

 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO, ISNULL(FL_FRETE_PROFIT,0)FL_FRETE_PROFIT ,

 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN ID_PARCEIRO_IMPORTADOR
 ELSE ID_CLIENTE
 END ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA ,'COTA',


 CASE 
 WHEN ID_SERVICO in (1,2) and ID_TIPO_PAGAMENTO = 1
 THEN 1

WHEN ID_SERVICO  in (1,2) and ID_TIPO_PAGAMENTO = 2
THEN 2

 WHEN ID_SERVICO in (4,5) and ID_TIPO_PAGAMENTO = 1
 THEN 2

WHEN ID_SERVICO  in (4,5) and ID_TIPO_PAGAMENTO = 2
THEN 1

ELSE 0
end ID_ORIGEM_PAGAMENTO,ISNULL(FL_FRETE_DECLARADO,0)FL_FRETE_DECLARADO
 
 FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)


        Else



            'FRETE VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF,ID_ORIGEM_PAGAMENTO)

 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO, " & FL_PROFIT_FRETE & " ,

 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN ID_PARCEIRO_IMPORTADOR
 ELSE ID_CLIENTE
 END ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA ,'COTA',
 

 CASE 
 WHEN ID_SERVICO in (1,2) and ID_TIPO_PAGAMENTO = 1
 THEN 1

WHEN ID_SERVICO  in (1,2) and ID_TIPO_PAGAMENTO = 2
THEN 2

 WHEN ID_SERVICO in (4,5) and ID_TIPO_PAGAMENTO = 1
 THEN 2

WHEN ID_SERVICO  in (4,5) and ID_TIPO_PAGAMENTO = 2
THEN 1

ELSE 0
end ID_ORIGEM_PAGAMENTO
 
 FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)

        End If

        Dim dsCarga As DataSet
        If ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then

            dsCarga = Con.ExecutarQuery("SELECT ID_COTACAO_MERCADORIA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text)
            If dsCarga.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In dsCarga.Tables(0).Rows
                    Dim dsInsertCarga As DataSet = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,VL_PESO_BRUTO,VL_M3,ID_BL,ID_COTACAO_MERCADORIA,DS_MERCADORIA) 
SELECT ID_MERCADORIA, ID_MERCADORIA, VL_PESO_BRUTO, VL_M3, " & ID_BL & " , ID_COTACAO_MERCADORIA,DS_MERCADORIA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO_MERCADORIA =" & linha.Item("ID_COTACAO_MERCADORIA") & " Select SCOPE_IDENTITY() as ID_CARGA_BL")

                    Dim ID_CARGA_BL As String = dsInsertCarga.Tables(0).Rows(0).Item("ID_CARGA_BL")

                    Con.ExecutarQuery("INSERT INTO TB_CARGA_BL_DIMENSAO (QTD_CAIXA,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,ID_COTACAO_MERCADORIA,ID_COTACAO_MERCADORIA_DIMENSAO,ID_BL,ID_CARGA_BL) 
SELECT B.QTD_CAIXA,B.VL_ALTURA,B.VL_COMPRIMENTO,B.VL_LARGURA, A.ID_COTACAO_MERCADORIA, ID , " & ID_BL & " , " & ID_CARGA_BL & "
FROM TB_COTACAO_MERCADORIA A
INNER JOIN TB_COTACAO_MERCADORIA_DIMENSAO B ON A.ID_COTACAO_MERCADORIA = B.ID_COTACAO_MERCADORIA AND A.ID_COTACAO = B.ID_COTACAO
WHERE A.ID_COTACAO_MERCADORIA =" & linha.Item("ID_COTACAO_MERCADORIA"))

                Next


            End If

        Else

            If ddlEstufagem.SelectedValue = 1 Then

                dsCarga = Con.ExecutarQuery("SELECT ID_COTACAO_MERCADORIA,QT_CONTAINER FROM TB_COTACAO_MERCADORIA
         WHERE QT_CONTAINER is not null and QT_CONTAINER <> 0 and ID_COTACAO = " & txtID.Text)
                If dsCarga.Tables(0).Rows.Count > 0 Then
                    Dim QT_CONTAINER As Integer
                    For Each linha As DataRow In dsCarga.Tables(0).Rows
                        QT_CONTAINER = linha.Item("QT_CONTAINER")

                        For i As Integer = 1 To QT_CONTAINER Step 1
                            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL,ID_TIPO_CNTR,ID_COTACAO_MERCADORIA) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,isnull(VL_PESO_BRUTO,0)/isnull(QT_CONTAINER,0)VL_PESO_BRUTO,isnull(VL_M3,0)/isnull(QT_CONTAINER,0)VL_M3," & ID_BL & ",ID_TIPO_CONTAINER, ID_COTACAO_MERCADORIA FROM TB_COTACAO_MERCADORIA
                WHERE ID_COTACAO_MERCADORIA =  " & linha.Item("ID_COTACAO_MERCADORIA"))
                        Next
                    Next
                Else
                    Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,ID_BL,ID_COTACAO_MERCADORIA) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO," & ID_BL & ",ID_COTACAO_MERCADORIA FROM TB_COTACAO_MERCADORIA
         WHERE ID_COTACAO =  " & txtID.Text)
                End If


            ElseIf ddlEstufagem.SelectedValue = 2 Then

                Dim ID_MERCADORIA As Integer = 11
                dsCarga = Con.ExecutarQuery("SELECT DISTINCT ID_MERCADORIA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text)
                If dsCarga.Tables(0).Rows.Count = 1 Then
                    ID_MERCADORIA = dsCarga.Tables(0).Rows(0).Item("ID_MERCADORIA")
                End If

                Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL,ID_MERCADORIA,ID_EMBALAGEM) 
        SELECT SUM(QT_MERCADORIA)QT_MERCADORIA,SUM(VL_PESO_BRUTO)VL_PESO_BRUTO,SUM(VL_M3)VL_M3," & ID_BL & ", " & ID_MERCADORIA & "," & ID_MERCADORIA & " FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text)

            End If
        End If




    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        divDeleteTaxas.Visible = False
        divDeleteErroTaxas.Visible = False
        divinfo.Visible = False
        ImportaTaxas()
        If ddlServico.SelectedValue <= 2 And ddlFreteTransportador_Frete.SelectedValue <> 0 Then
            AtualizaTaxaAgente()
        End If
        dgvTaxas.DataBind()
    End Sub

    Private Sub txtNomeCliente_TextChanged(sender As Object, e As EventArgs) Handles txtNomeCliente.TextChanged
        diverro.Visible = False
        divsuccess.Visible = False
        divinfo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodCliente.Text = "" Then
            txtCodCliente.Text = 0
        End If
        If txtNomeCliente.Text = "" Then
            txtNomeCliente.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE (FL_EXPORTADOR= 1 OR FL_IMPORTADOR =1 OR FL_AGENTE = 1 OR FL_AGENTE_INTERNACIONAL =1 OR FL_COMISSARIA = 1 OR FL_INDICADOR = 1) and  (NM_RAZAO like '%" & txtNomeCliente.Text & "%' or ID_PARCEIRO =  " & txtCodCliente.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE (FL_EXPORTADOR= 1 OR FL_IMPORTADOR =1 OR FL_AGENTE = 1 OR FL_AGENTE_INTERNACIONAL =1 OR FL_COMISSARIA = 1 OR FL_INDICADOR = 1) and  (NM_FANTASIA like '%" & txtNomeCliente.Text & "%' or ID_PARCEIRO =  " & txtCodCliente.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE (FL_EXPORTADOR= 1 OR FL_IMPORTADOR =1 OR FL_AGENTE = 1 OR FL_AGENTE_INTERNACIONAL =1 OR FL_COMISSARIA = 1 OR FL_INDICADOR = 1) and  (CNPJ like '%" & txtNomeCliente.Text & "%' or ID_PARCEIRO =  " & txtCodCliente.Text & ")
UNION 
SELECT  0,'', ' Selecione' FROM TB_PARCEIRO ORDER BY NM_RAZAO"

        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsCliente.SelectCommand = Sql
            dsCliente.DataBind()
            ddlCliente.DataBind()
        Else
            diverro.Visible = True
            lblmsgErro.Text = "Parceiro não encontrado!"
        End If
        txtNomeCliente.Text = txtNomeCliente.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeIndicador_TextChanged(sender As Object, e As EventArgs) Handles txtNomeIndicador.TextChanged
        diverro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodIndicador.Text = "" Then
            txtCodIndicador.Text = 0
        End If
        If txtNomeIndicador.Text = "" Then
            txtNomeIndicador.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE  FL_INDICADOR =  1  and (NM_RAZAO like '%" & txtNomeIndicador.Text & "%' or ID_PARCEIRO =  " & txtCodIndicador.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_INDICADOR =  1  and (NM_FANTASIA like '%" & txtNomeIndicador.Text & "%' or ID_PARCEIRO =  " & txtCodIndicador.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_INDICADOR =  1  and (CNPJ like '%" & txtNomeIndicador.Text & "%' or ID_PARCEIRO =  " & txtCodIndicador.Text & ") 
UNION 
SELECT  0,'', ' Selecione' FROM TB_PARCEIRO ORDER BY NM_RAZAO"


        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsIndicador.SelectCommand = Sql
            dsIndicador.DataBind()
            ddlIndicador.DataBind()
        Else
            diverro.Visible = True
            lblmsgErro.Text = "Parceiro não encontrado!"
        End If
        txtNomeIndicador.Text = txtNomeIndicador.Text.Replace("NULL", "")

    End Sub

    Private Sub txtNomeAgente_TextChanged(sender As Object, e As EventArgs) Handles txtNomeAgente.TextChanged
        diverro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodAgente.Text = "" Then
            txtCodAgente.Text = 0
        End If
        If txtNomeAgente.Text = "" Then
            txtNomeAgente.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE  FL_AGENTE_INTERNACIONAL =  1  and ((NM_RAZAO like '%" & txtNomeAgente.Text & "%' and FL_ATIVO=1 ) or ( ID_PARCEIRO =  " & txtCodAgente.Text & "))  
UNION
SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE FL_AGENTE_INTERNACIONAL =  1  and ((NM_FANTASIA like '%" & txtNomeAgente.Text & "%' and FL_ATIVO=1 ) or ( ID_PARCEIRO =  " & txtCodAgente.Text & ")) 
UNION
SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE FL_AGENTE_INTERNACIONAL =  1  and ((CNPJ like '%" & txtNomeAgente.Text & "%' and FL_ATIVO=1 ) or ( ID_PARCEIRO =  " & txtCodAgente.Text & ")) 
UNION 
SELECT  0,'', ' Selecione' FROM TB_PARCEIRO ORDER BY NM_RAZAO"

        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsAgente.SelectCommand = Sql
            dsAgente.DataBind()
            ddlAgente.DataBind()
        Else
            diverro.Visible = True
            lblmsgErro.Text = "Parceiro não encontrado!"
        End If
        txtNomeAgente.Text = txtNomeAgente.Text.Replace("NULL", "")

    End Sub

    Private Sub txtFreteVendaMercadoriaUnitario_TextChanged(sender As Object, e As EventArgs) Handles txtFreteVendaMercadoriaUnitario.TextChanged
        If txtQtdContainerMercadoria.Text <> "" And txtFreteVendaMercadoriaUnitario.Text <> "" Then

            If txtQtdContainerMercadoria.Text > 0 And txtFreteVendaMercadoriaUnitario.Text <> 0 Then
                txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text
            Else
                txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text
            End If

        End If
    End Sub
    Private Sub txtFreteCompraMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtFreteCompraMercadoriaUnitario.TextChanged
        If txtQtdContainerMercadoria.Text <> "" And txtFreteCompraMercadoriaUnitario.Text <> "" Then

            If txtQtdContainerMercadoria.Text > 0 And txtFreteCompraMercadoriaUnitario.Text <> 0 Then

                txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text

            Else
                txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaUnitario.Text
            End If

        End If
    End Sub

    Private Sub txtNomeExportador_TextChanged(sender As Object, e As EventArgs) Handles txtNomeExportador.TextChanged
        diverro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodExportador.Text = "" Then
            txtCodExportador.Text = 0
        End If
        If txtNomeExportador.Text = "" Then
            txtNomeExportador.Text = "NULL"
        End If

        Dim Sql As String = ""
        If ddlServico.SelectedValue = 1 Or ddlServico.SelectedValue = 2 Then


            Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_SHIPPER =  1  and (NM_RAZAO like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ")
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_SHIPPER =  1  and (NM_FANTASIA like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_SHIPPER =  1  and (CNPJ like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ") 
UNION 
SELECT  0,'', ' Selecione' FROM TB_PARCEIRO ORDER BY NM_RAZAO"


        ElseIf ddlServico.SelectedValue = 4 Or ddlServico.SelectedValue = 5 Then

            Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_EXPORTADOR =  1  and (NM_RAZAO like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ")
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_EXPORTADOR =  1  and (NM_FANTASIA like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_EXPORTADOR =  1  and (CNPJ like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ") 
UNION 
SELECT  0,'', ' Selecione' FROM TB_PARCEIRO ORDER BY NM_RAZAO"
        End If

        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsExportador.SelectCommand = Sql
            dsExportador.DataBind()
            ddlExportador.DataBind()
        Else
            diverro.Visible = True
            lblmsgErro.Text = "Parceiro não encontrado!"
        End If
        txtNomeExportador.Text = txtNomeExportador.Text.Replace("NULL", "")

    End Sub

    Private Sub ddlServico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlServico.SelectedIndexChanged
        Session("servico") = ddlServico.SelectedValue
        MaritimoXAereo()
    End Sub

    Private Sub ddlTipoContainerMercadoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoContainerMercadoria.SelectedIndexChanged
        If ddlTipoContainerMercadoria.SelectedValue <> 0 Then
            If txtQtdContainerMercadoria.Text = "" Then
                txtQtdContainerMercadoria.Text = 0
            End If
            If txtFreteCompraMercadoriaUnitario.Text = "" Then
                txtFreteCompraMercadoriaUnitario.Text = 0
            End If
            If txtFreteVendaMercadoriaUnitario.Text = "" Then
                txtFreteVendaMercadoriaUnitario.Text = 0
            End If

            If ddlFreteTransportador_Frete.SelectedValue <> 0 Then

                Dim Con As New Conexao_sql
                Con.Conectar()
                Dim ds As DataSet = Con.ExecutarQuery("SELECT QT_DIAS_FREETIME,isnull(VL_COMPRA,0)VL_COMPRA from TB_TARIFARIO_FRETE_TRANSPORTADOR where ID_FRETE_TRANSPORTADOR = (SELECT ID_FRETE_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") AND ID_TIPO_CONTAINER = " & ddlTipoContainerMercadoria.SelectedValue & " AND convert(date,'" & txtValidade.Text & "',103) between convert(date,DT_VALIDADE_INICIAL,103) and  convert(date,DT_VALIDADE_FINAL,103)")

                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")) Then
                        txtFreeTimeMercadoria.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMPRA")) Then
                        txtFreteCompraMercadoriaUnitario.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")

                        If txtQtdContainerMercadoria.Text > 0 Then
                            txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text
                            txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text
                        Else
                            txtFreteCompraMercadoriaCalc.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")
                            txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text
                        End If
                    End If
                Else
                    txtFreteCompraMercadoriaCalc.Text = 0
                    txtFreteCompraMercadoriaUnitario.Text = 0
                    txtFreeTimeMercadoria.Text = 0
                End If
            End If
        End If
    End Sub
    Private Sub btnDeletarTaxas_Click(sender As Object, e As EventArgs) Handles btnDeletarTaxas.Click
        divDeleteTaxas.Visible = False
        divDeleteErroTaxas.Visible = False
        divinfo.Visible = False

        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            lblDeleteErroTaxas.Text = "Usuário não tem permissão para realizar exclusões"
            divDeleteErroTaxas.Visible = True

        Else

            If ddlStatusCotacao.SelectedValue = 12 Or ddlStatusCotacao.SelectedValue = 15 Or ddlStatusCotacao.SelectedValue = 9 Then
                lblDeleteErroTaxas.Text = "Status da cotação não permite realizar exclusões!"
                divDeleteErroTaxas.Visible = True
            Else


                For Each linha As GridViewRow In dgvTaxas.Rows
                    Dim check As CheckBox = linha.FindControl("ckSelecionar")
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    If check.Checked Then
                        Dim finaliza As New FinalizaCotacao
                        If finaliza.TaxaBloqueada(ID, "COTACAO") = True Then
                            lblDeleteErroTaxas.Text = "Não foi possível deletar taxas já enviadas para contas a pagar/receber ou invoice!"
                            divDeleteErroTaxas.Visible = True
                        Else
                            ds = Con.ExecutarQuery("SELECT ID_STATUS_COTACAO,NR_PROCESSO_GERADO FROM TB_COTACAO WHERE ID_COTACAO =" & txtID.Text)
                            If ds.Tables(0).Rows.Count > 0 Then
                                Con.ExecutarQuery("DELETE From TB_COTACAO_TAXA Where ID_COTACAO_TAXA = " & ID)
                                lblDeleteTaxas.Text = "Registros deletados!"
                                divDeleteTaxas.Visible = True
                                dgvTaxas.DataBind()
                                If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 10 And Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO")) Then
                                    Dim RotinaUpdate As New RotinaUpdate
                                    RotinaUpdate.DeletaTaxas(txtID.Text, ID, txtProcessoCotacao.Text)
                                End If
                            End If
                        End If
                    End If
                Next
            End If

        End If
    End Sub

    Private Sub btnSelecionarTudo_Click(sender As Object, e As EventArgs) Handles btnSelecionarTudo.Click
        divDeleteTaxas.Visible = False
        divDeleteErroTaxas.Visible = False
        divinfo.Visible = False
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvTaxas.Rows(i).FindControl("ckSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
        Next
    End Sub

    Private Sub ddlBaseCalculoTaxa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBaseCalculoTaxa.SelectedIndexChanged
        If ddlBaseCalculoTaxa.SelectedValue = 38 Or ddlBaseCalculoTaxa.SelectedValue = 40 Or ddlBaseCalculoTaxa.SelectedValue = 41 Then
            txtQtdBaseCalculo.Enabled = True
        Else
            txtQtdBaseCalculo.Enabled = False
        End If
    End Sub

    Private Sub btnGravarReferencia_Click(sender As Object, e As EventArgs) Handles btnGravarReferencia.Click
        divSuccessReferencia.Visible = False
        divErroReferencia.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        Dim Referencia As String = ""
        If txtID.Text = "" Then
            lblErroReferencia.Text = "Necessário inserir cotação!"
            divErroReferencia.Visible = True
        ElseIf txtReferencia.Text = "" Or ddlTipoReferencia.SelectedValue = "0" Then
            lblErroReferencia.Text = "Preencha todos os campos!"
            divErroReferencia.Visible = True
        Else
            Referencia = txtReferencia.Text
            Referencia = Referencia.Replace("'", "''")
            Referencia = "'" & Referencia & "'"

            If txtID_Referencia.Text = "" Then

                'INSERT
                If txtProcessoCotacao.Text <> "" Then
                    ds = Con.ExecutarQuery("Select ID_BL FROM TB_BL WHERE GRAU='C' AND NR_PROCESSO = '" & txtProcessoCotacao.Text & "'")
                    If ds.Tables(0).Rows.Count > 0 Then

                        Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_COTACAO,NR_REFERENCIA_CLIENTE,TIPO,ID_BL) VALUES (" & txtID.Text & ", " & Referencia & ",'" & ddlTipoReferencia.SelectedValue & "'," & ds.Tables(0).Rows(0).Item("ID_BL") & ")")

                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET ID_BL =" & ds.Tables(0).Rows(0).Item("ID_BL") & " WHERE ID_COTACAO = " & txtID.Text)

                    Else

                        Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_COTACAO,NR_REFERENCIA_CLIENTE,TIPO) VALUES (" & txtID.Text & ", " & Referencia & ",'" & ddlTipoReferencia.SelectedValue & "')")
                    End If

                Else

                    Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_COTACAO,NR_REFERENCIA_CLIENTE,TIPO) VALUES (" & txtID.Text & ", " & Referencia & ",'" & ddlTipoReferencia.SelectedValue & "')")
                End If

                divSuccessReferencia.Visible = True
                dgvReferencia.DataBind()
                txtID_Referencia.Text = ""
                txtReferencia.Text = ""
                ddlTipoReferencia.SelectedValue = 0


            Else

                'UPDATE
                If txtProcessoCotacao.Text <> "" Then
                    ds = Con.ExecutarQuery("Select ID_BL FROM TB_BL WHERE GRAU='C' AND NR_PROCESSO = '" & txtProcessoCotacao.Text & "'")
                    If ds.Tables(0).Rows.Count > 0 Then

                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = " & Referencia & ", TIPO = '" & ddlTipoReferencia.SelectedValue & "', ID_BL = " & ds.Tables(0).Rows(0).Item("ID_BL") & " WHERE ID_REFERENCIA_CLIENTE = " & txtID_Referencia.Text)
                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET ID_BL =" & ds.Tables(0).Rows(0).Item("ID_BL") & " WHERE ID_COTACAO = " & txtID.Text)

                    Else

                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = " & Referencia & ", TIPO = '" & ddlTipoReferencia.SelectedValue & "'  WHERE ID_REFERENCIA_CLIENTE = " & txtID_Referencia.Text)
                    End If

                Else

                    Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = " & Referencia & ", TIPO = '" & ddlTipoReferencia.SelectedValue & "'  WHERE ID_REFERENCIA_CLIENTE = " & txtID_Referencia.Text)
                End If

                divSuccessReferencia.Visible = True
                dgvReferencia.DataBind()
                txtID_Referencia.Text = ""
                txtReferencia.Text = ""
                ddlTipoReferencia.SelectedValue = 0

            End If


        End If



    End Sub

    Private Sub dgvReferencia_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvReferencia.RowCommand
        divSuccessReferencia.Visible = False
        divErroReferencia.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroReferencia.Text = "Usuário não tem permissão para realizar exclusões"
                divErroReferencia.Visible = True
            Else
                Con.ExecutarQuery("DELETE From TB_REFERENCIA_CLIENTE Where ID_REFERENCIA_CLIENTE = " & ID)
                lblSuccessReferencia.Text = "Registro deletado!"
                divSuccessReferencia.Visible = True
                dgvReferencia.DataBind()
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select NR_REFERENCIA_CLIENTE,TIPO from TB_REFERENCIA_CLIENTE
WHERE ID_REFERENCIA_CLIENTE = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                txtID_Referencia.Text = ID

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")) Then
                    txtReferencia.Text = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE").ToString
                    ddlTipoReferencia.SelectedValue = ds.Tables(0).Rows(0).Item("TIPO").ToString
                End If

            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub btnLimparReferencia_Click(sender As Object, e As EventArgs) Handles btnLimparReferencia.Click
        divSuccessReferencia.Visible = False
        divErroReferencia.Visible = False
        txtID_Referencia.Text = ""
        txtReferencia.Text = ""
        ddlTipoReferencia.SelectedValue = 0
        dgvReferencia.DataBind()
    End Sub

    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
        divsuccess.Visible = False
        diverro.Visible = False

        Dim CalCotacao As New CalculaCotacao
        Dim retorno As String = CalCotacao.CalculaCotacao(txtID.Text)

        If retorno = "Calculo realizado com sucesso" Then
            lblmsgSuccess.Text = "Calculo realizado com sucesso"
            divsuccess.Visible = True
            txtDataCalculo.Text = Now.Date
        Else
            diverro.Visible = True
            lblmsgErro.Text = retorno
        End If
    End Sub

    Sub AdicionarMedidasAereo()
        divErroMercadoria.Visible = False
        divSuccessMercadoria.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        ' sumMedidasAereo(txtIDMercadoria.Text)

        If txtID.Text = "" Then
            lblErroMercadoria.Text = "Necessário inserir cotação na Aba de Informações Básicas."
            divErroMercadoria.Visible = True

        ElseIf txtIDMercadoria.Text = "" Then
            lblErroMercadoria.Text = "Necessário inserir mercadoria."
            divErroMercadoria.Visible = True

        ElseIf txtQtdCaixasAereo.Text = "" Or txtComprimentoMercadoriaAereo.Text = "" Or txtAlturaMercadoriaAereo.Text = "" Or txtLarguraMercadoriaAereo.Text = "" Then
            lblErroMercadoria.Text = "Preencha todos os campos de dimensoes!"
            divErroMercadoria.Visible = True

        Else

            txtAlturaMercadoriaAereo.Text = txtAlturaMercadoriaAereo.Text.Replace(".", "")
            txtAlturaMercadoriaAereo.Text = txtAlturaMercadoriaAereo.Text.Replace(",", ".")

            txtComprimentoMercadoriaAereo.Text = txtComprimentoMercadoriaAereo.Text.Replace(".", "")
            txtComprimentoMercadoriaAereo.Text = txtComprimentoMercadoriaAereo.Text.Replace(",", ".")

            txtLarguraMercadoriaAereo.Text = txtLarguraMercadoriaAereo.Text.Replace(".", "")
            txtLarguraMercadoriaAereo.Text = txtLarguraMercadoriaAereo.Text.Replace(",", ".")

            ds = Con.ExecutarQuery("INSERT INTO TB_COTACAO_MERCADORIA_DIMENSAO (ID_COTACAO,ID_COTACAO_MERCADORIA, QTD_CAIXA, VL_LARGURA, VL_ALTURA, VL_COMPRIMENTO) VALUES (" & txtID.Text & "," & txtIDMercadoria.Text & "," & txtQtdCaixasAereo.Text & "," & txtLarguraMercadoriaAereo.Text & "," & txtAlturaMercadoriaAereo.Text & "," & txtComprimentoMercadoriaAereo.Text & ") Select SCOPE_IDENTITY() as ID ")
            Dim ID As String = ds.Tables(0).Rows(0).Item("ID").ToString()
            Dim CLA As Decimal

            ds = Con.ExecutarQuery("SELECT (isnull(D.QTD_CAIXA,0) * isnull(D.VL_COMPRIMENTO,0) * isnull(D.VL_ALTURA,0) * isnull(D.VL_LARGURA,0))/5988 AS CLA
from TB_COTACAO A  
left join TB_COTACAO_MERCADORIA_DIMENSAO D ON D.ID_COTACAO = A.ID_COTACAO
Where A.ID_COTACAO = " & txtID.Text)


            If ds.Tables(0).Rows.Count > 0 Then
                For Each linha As DataRow In ds.Tables(0).Rows
                    CLA = CLA + linha.Item("CLA")
                Next
            End If
            txtM3Mercadoria.Text = CLA.ToString("0.000")

            sumMedidasAereo(txtIDMercadoria.Text)
            If ddlStatusCotacao.SelectedValue = 10 And txtProcessoCotacao.Text <> "" Then
                Dim RotinaUpdate As New RotinaUpdate
                RotinaUpdate.InsereDimensaoCarga(txtID.Text, txtIDMercadoria.Text, txtProcessoCotacao.Text, ID)
            End If


            txtPesoTaxadoMercadoria.Text = ArredondarPesoTaxado(txtID.Text)

            'Dim CalCotacao As New CalculaCotacao
            'Dim retorno As String = CalCotacao.CalculaCotacao(txtID.Text)

            'ds = Con.ExecutarQuery("SELECT isnull(VL_PESO_TAXADO,0)VL_PESO_TAXADO, isnull(VL_TOTAL_M3,0)VL_TOTAL_M3 from TB_COTACAO where ID_COTACAO =" & txtID.Text)
            'If ds.Tables(0).Rows.Count > 0 Then
            '    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
            '        txtPesoTaxadoMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
            '    End If
            '    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_M3")) Then
            '        txtM3Mercadoria.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_M3")
            '    End If
            'End If

            dgvMedidasAereo.DataBind()
            txtAlturaMercadoriaAereo.Text = ""
            txtLarguraMercadoriaAereo.Text = ""
            txtComprimentoMercadoriaAereo.Text = ""
            txtQtdCaixasAereo.Text = ""
        End If


    End Sub

    Sub sumMedidasAereo(IDMercadoria As String)
        Dim Con As New Conexao_sql
        Con.Conectar()
        If IDMercadoria <> "" Then
            Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QTD_CAIXA,0))QTD_CAIXA FROM TB_COTACAO_MERCADORIA_DIMENSAO WHERE ID_COTACAO_MERCADORIA =  " & IDMercadoria & ") WHERE ID_COTACAO_MERCADORIA =  " & IDMercadoria)

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(QT_MERCADORIA,0)QT_MERCADORIA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO_MERCADORIA =  " & IDMercadoria)
            If ds.Tables(0).Rows.Count > 0 Then
                txtQtdMercadoria.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
            End If

            Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET VL_LARGURA =
(SELECT SUM(ISNULL(VL_LARGURA,0))VL_LARGURA FROM TB_COTACAO_MERCADORIA_DIMENSAO WHERE ID_COTACAO_MERCADORIA =  " & IDMercadoria & ") WHERE ID_COTACAO_MERCADORIA =  " & IDMercadoria)

            Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET VL_ALTURA =
(SELECT SUM(ISNULL(VL_ALTURA,0))VL_ALTURA FROM TB_COTACAO_MERCADORIA_DIMENSAO WHERE ID_COTACAO_MERCADORIA =  " & IDMercadoria & ") WHERE ID_COTACAO_MERCADORIA =  " & IDMercadoria)

            Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET VL_COMPRIMENTO =
(SELECT SUM(ISNULL(VL_COMPRIMENTO,0))VL_COMPRIMENTO FROM TB_COTACAO_MERCADORIA_DIMENSAO WHERE ID_COTACAO_MERCADORIA =  " & IDMercadoria & ") WHERE ID_COTACAO_MERCADORIA =  " & IDMercadoria)

        End If
    End Sub
    Private Sub btnAdicionarMedidasAereo_Click(sender As Object, e As ImageClickEventArgs) Handles btnAdicionarMedidasAereo.Click
        AdicionarMedidasAereo()
    End Sub

    Private Sub dgvMedidasAereo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvMedidasAereo.RowCommand
        divSuccessMercadoria.Visible = False
        divErroMercadoria.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("DELETE From TB_COTACAO_MERCADORIA_DIMENSAO Where ID = " & ID)
            sumMedidasAereo(txtIDMercadoria.Text)
            lblSuccessMercadoria.Text = "Registro deletado!"
            divSuccessMercadoria.Visible = True
            dgvMedidasAereo.DataBind()
            If ddlStatusCotacao.SelectedValue = 10 And txtProcessoCotacao.Text <> "" Then
                Dim RotinaUpdate As New RotinaUpdate
                RotinaUpdate.DeletaDimensaoCarga(txtID.Text, txtIDMercadoria.Text, txtProcessoCotacao.Text, ID)
            End If


            ds = Con.ExecutarQuery("SELECT (isnull(D.QTD_CAIXA,0) * isnull(D.VL_COMPRIMENTO,0) * isnull(D.VL_ALTURA,0) * isnull(D.VL_LARGURA,0))/5988 AS CLA
from TB_COTACAO A  
left join TB_COTACAO_MERCADORIA_DIMENSAO D ON D.ID_COTACAO = A.ID_COTACAO
Where A.ID_COTACAO = " & txtID.Text)
            Dim CLA As Decimal

            If ds.Tables(0).Rows.Count > 0 Then
                For Each linha As DataRow In ds.Tables(0).Rows
                    CLA = CLA + linha.Item("CLA")
                Next
            End If
            txtM3Mercadoria.Text = CLA.ToString("0.000")
            txtPesoTaxadoMercadoria.Text = ArredondarPesoTaxado(txtID.Text)

            'Dim CalCotacao As New CalculaCotacao
            'Dim retorno As String = CalCotacao.CalculaCotacao(txtID.Text)

            'ds = Con.ExecutarQuery("SELECT isnull(VL_PESO_TAXADO,0)VL_PESO_TAXADO, isnull(VL_TOTAL_M3,0)VL_TOTAL_M3 from TB_COTACAO where ID_COTACAO =" & txtID.Text)
            'If ds.Tables(0).Rows.Count > 0 Then
            '    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
            '        txtPesoTaxadoMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
            '    End If
            '    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_M3")) Then
            '        txtM3Mercadoria.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_M3")
            '    End If
            'End If

        End If

    End Sub

    Private Sub ddlItemDespesaTaxa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlItemDespesaTaxa.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        Dim Sb = New StringBuilder()

        Dim idDespesa As Integer = Convert.ToInt32(ddlItemDespesaTaxa.SelectedValue)

        Sb.AppendLine("SELECT isnull(FL_PREMIACAO,0) as FL_PREMIACAO FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & idDespesa)
        ds = Con.ExecutarQuery(Sb.ToString)
        Try
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("FL_PREMIACAO") = True Then
                    txtValorTaxaVenda.Enabled = False
                    txtValorTaxaVenda.Text = 0
                    txtValorTaxaVendaMin.Enabled = False
                    txtValorTaxaVendaMin.Text = 0
                    ddlMoedaVendaTaxa.Enabled = False
                    ddlMoedaVendaTaxa.SelectedValue = 0
                    ddlDestinatarioCobrancaTaxa.Enabled = False
                    ddlDestinatarioCobrancaTaxa.SelectedValue = 3
                    ddlFornecedor.SelectedValue = ddlIndicador.SelectedValue
                Else
                    txtValorTaxaVenda.Enabled = True
                    txtValorTaxaVendaMin.Enabled = True
                    ddlMoedaVendaTaxa.Enabled = True
                    ddlDestinatarioCobrancaTaxa.Enabled = True
                End If
            End If

            If idDespesa = 550 Then
                ddlMoedaCompraTaxa.Enabled = False
                txtValorTaxaCompra.Enabled = False
                txtValorTaxaCompraMin.Enabled = False
            Else
                ddlMoedaCompraTaxa.Enabled = True
                txtValorTaxaCompra.Enabled = True
                txtValorTaxaCompraMin.Enabled = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ckbDeclaradoTaxa_CheckedChanged(sender As Object, e As EventArgs) Handles ckbDeclaradoTaxa.CheckedChanged
        If ddlServico.SelectedValue <= 2 And ddlFreteTransportador_Frete.SelectedValue <> 0 And ckbDeclaradoTaxa.Checked = True Then
            ddlFornecedor.SelectedValue = ddlAgente.SelectedValue
        End If
    End Sub

    Private Sub lkProximo_Click(sender As Object, e As EventArgs) Handles lkProximo.Click
        divErroTaxa.Visible = False
        divSuccessTaxa.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim LinhaAtual As Integer = 0
        Dim ProximaLinha As Integer = 0
        Dim PrimeiraTaxa As String = 0




        Dim ds As DataSet = Con.ExecutarQuery("SELECT ROW_NUMBER() OVER(ORDER BY ID_COTACAO_TAXA) AS num, ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
        If ds.Tables(0).Rows.Count > 0 Then
            PrimeiraTaxa = ds.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")
            For Each linha As DataRow In ds.Tables(0).Rows
                If linha.Item("ID_COTACAO_TAXA") = txtIDTaxa.Text Then
                    LinhaAtual = linha.Item("num")
                    ProximaLinha = linha.Item("num") + 1
                End If

                If ProximaLinha = linha.Item("num") Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,ID_FORNECEDOR,
ID_COTACAO,
ID_ITEM_DESPESA,
ID_TIPO_PAGAMENTO,
ID_ORIGEM_PAGAMENTO,
FL_DECLARADO,
FL_DIVISAO_PROFIT,
ID_DESTINATARIO_COBRANCA,
ID_MOEDA_COMPRA,
VL_TAXA_COMPRA_CALCULADO,
ID_MOEDA_VENDA,
VL_TAXA_VENDA_CALCULADO,
ID_BASE_CALCULO_TAXA,
OB_TAXAS,
VL_TAXA_VENDA_MIN,
VL_TAXA_COMPRA,
VL_TAXA_VENDA,
VL_TAXA_COMPRA_MIN,
QTD_BASE_CALCULO
FROM TB_COTACAO_TAXA A
WHERE A.ID_COTACAO_TAXA =  " & linha.Item("ID_COTACAO_TAXA"))
                    If dsTaxa.Tables(0).Rows.Count > 0 Then
                        LimparDadosTaxa()
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")) Then
                            txtIDTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_FORNECEDOR")) Then
                            ddlFornecedor.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_FORNECEDOR")
                        End If

                        ckbDeclaradoTaxa.Checked = dsTaxa.Tables(0).Rows(0).Item("FL_DECLARADO")
                        ckbProfitTaxa.Checked = dsTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                            ddlItemDespesaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                            ddlTipoPagamentoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                            ddlOrigemPagamentoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                            ddlDestinatarioCobrancaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                            ddlBaseCalculoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")

                            If dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 38 Or dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 40 Or dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 41 Then
                                txtQtdBaseCalculo.Enabled = True
                            Else
                                txtQtdBaseCalculo.Enabled = False
                            End If

                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")) Then
                            txtQtdBaseCalculo.Text = dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")) Then
                            ddlMoedaCompraTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")) Then
                            txtValorTaxaCompra.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")) Then
                            txtValorTaxaCompraMin.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")) Then
                            txtValorTaxaCompraCalc.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")) Then
                            ddlMoedaVendaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA")) Then
                            txtValorTaxaVenda.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")) Then
                            txtValorTaxaVendaMin.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")) Then
                            txtValorTaxaVendaCalc.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                            txtObsTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("OB_TAXAS")
                        End If

                        Dim finaliza As New FinalizaCotacao
                        If finaliza.TaxaBloqueada(txtIDTaxa.Text, "COTACAO", "R") = True Then
                            ddlMoedaVendaTaxa.Enabled = False
                            txtValorTaxaVenda.Enabled = False
                            txtValorTaxaVendaMin.Enabled = False
                            txtValorTaxaVendaCalc.Enabled = False
                            ddlDestinatarioCobrancaTaxa.Enabled = False
                        End If

                        If finaliza.TaxaBloqueada(txtIDTaxa.Text, "COTACAO", "P") = True Then
                            ddlMoedaCompraTaxa.Enabled = False
                            txtValorTaxaCompra.Enabled = False
                            txtValorTaxaCompraMin.Enabled = False
                            txtValorTaxaCompraCalc.Enabled = False
                            ddlFornecedor.Enabled = False
                        End If


                        If ddlStatusCotacao.SelectedValue = 12 Or ddlStatusCotacao.SelectedValue = 15 Or ddlStatusCotacao.SelectedValue = 9 Then
                            btnSalvarTaxa.Visible = False
                        Else
                            btnSalvarTaxa.Visible = True
                        End If



                        mpeNovoTaxa.Show()
                    End If

                ElseIf ProximaLinha > ds.Tables(0).Rows.Count Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,ID_FORNECEDOR,
ID_COTACAO,
ID_ITEM_DESPESA,
ID_TIPO_PAGAMENTO,
ID_ORIGEM_PAGAMENTO,
FL_DECLARADO,
FL_DIVISAO_PROFIT,
ID_DESTINATARIO_COBRANCA,
ID_MOEDA_COMPRA,
VL_TAXA_COMPRA_CALCULADO,
ID_MOEDA_VENDA,
VL_TAXA_VENDA_CALCULADO,
ID_BASE_CALCULO_TAXA,
OB_TAXAS,
VL_TAXA_VENDA_MIN,
VL_TAXA_COMPRA,
VL_TAXA_VENDA,
VL_TAXA_COMPRA_MIN,
QTD_BASE_CALCULO
FROM TB_COTACAO_TAXA A
WHERE A.ID_COTACAO_TAXA =  " & PrimeiraTaxa)
                    If dsTaxa.Tables(0).Rows.Count > 0 Then
                        LimparDadosTaxa()
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")) Then
                            txtIDTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_FORNECEDOR")) Then
                            ddlFornecedor.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_FORNECEDOR")
                        End If

                        ckbDeclaradoTaxa.Checked = dsTaxa.Tables(0).Rows(0).Item("FL_DECLARADO")
                        ckbProfitTaxa.Checked = dsTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                            ddlItemDespesaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                            ddlTipoPagamentoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                            ddlOrigemPagamentoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                            ddlDestinatarioCobrancaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                            ddlBaseCalculoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")

                            If dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 38 Or dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 40 Or dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 41 Then
                                txtQtdBaseCalculo.Enabled = True
                            Else
                                txtQtdBaseCalculo.Enabled = False
                            End If

                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")) Then
                            txtQtdBaseCalculo.Text = dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")) Then
                            ddlMoedaCompraTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")) Then
                            txtValorTaxaCompra.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")) Then
                            txtValorTaxaCompraMin.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")) Then
                            txtValorTaxaCompraCalc.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")) Then
                            ddlMoedaVendaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA")) Then
                            txtValorTaxaVenda.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")) Then
                            txtValorTaxaVendaMin.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")) Then
                            txtValorTaxaVendaCalc.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                            txtObsTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("OB_TAXAS")
                        End If

                        Dim finaliza As New FinalizaCotacao
                        If finaliza.TaxaBloqueada(txtIDTaxa.Text, "COTACAO", "R") = True Then
                            ddlMoedaVendaTaxa.Enabled = False
                            txtValorTaxaVenda.Enabled = False
                            txtValorTaxaVendaMin.Enabled = False
                            txtValorTaxaVendaCalc.Enabled = False
                            ddlDestinatarioCobrancaTaxa.Enabled = False
                        End If

                        If finaliza.TaxaBloqueada(txtIDTaxa.Text, "COTACAO", "P") = True Then
                            ddlMoedaCompraTaxa.Enabled = False
                            txtValorTaxaCompra.Enabled = False
                            txtValorTaxaCompraMin.Enabled = False
                            txtValorTaxaCompraCalc.Enabled = False
                            ddlFornecedor.Enabled = False
                        End If


                        If ddlStatusCotacao.SelectedValue = 12 Or ddlStatusCotacao.SelectedValue = 15 Or ddlStatusCotacao.SelectedValue = 9 Then
                            btnSalvarTaxa.Visible = False
                        Else
                            btnSalvarTaxa.Visible = True
                        End If



                        mpeNovoTaxa.Show()
                    End If
                End If

            Next


        End If
    End Sub
    Private Sub lkAnterior_Click(sender As Object, e As EventArgs) Handles lkAnterior.Click
        divErroTaxa.Visible = False
        divSuccessTaxa.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim LinhaAtual As Integer = 0
        Dim ProximaLinha As Integer = 0
        Dim PrimeiraTaxa As String = 0



        Dim ds As DataSet = Con.ExecutarQuery("SELECT ROW_NUMBER() OVER(ORDER BY ID_COTACAO_TAXA desc) AS num,ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
        If ds.Tables(0).Rows.Count > 0 Then
            PrimeiraTaxa = ds.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")
            For Each linha As DataRow In ds.Tables(0).Rows
                If linha.Item("ID_COTACAO_TAXA") = txtIDTaxa.Text Then
                    LinhaAtual = linha.Item("num")
                    ProximaLinha = linha.Item("num") + 1
                End If

                If ProximaLinha = linha.Item("num") Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,ID_FORNECEDOR,
ID_COTACAO,
ID_ITEM_DESPESA,
ID_TIPO_PAGAMENTO,
ID_ORIGEM_PAGAMENTO,
FL_DECLARADO,
FL_DIVISAO_PROFIT,
ID_DESTINATARIO_COBRANCA,
ID_MOEDA_COMPRA,
VL_TAXA_COMPRA_CALCULADO,
ID_MOEDA_VENDA,
VL_TAXA_VENDA_CALCULADO,
ID_BASE_CALCULO_TAXA,
OB_TAXAS,
VL_TAXA_VENDA_MIN,
VL_TAXA_COMPRA,
VL_TAXA_VENDA,
VL_TAXA_COMPRA_MIN,
QTD_BASE_CALCULO
FROM TB_COTACAO_TAXA A
WHERE A.ID_COTACAO_TAXA =  " & linha.Item("ID_COTACAO_TAXA"))
                    If dsTaxa.Tables(0).Rows.Count > 0 Then
                        LimparDadosTaxa()
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")) Then
                            txtIDTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_FORNECEDOR")) Then
                            ddlFornecedor.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_FORNECEDOR")
                        End If

                        ckbDeclaradoTaxa.Checked = dsTaxa.Tables(0).Rows(0).Item("FL_DECLARADO")
                        ckbProfitTaxa.Checked = dsTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                            ddlItemDespesaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                            ddlTipoPagamentoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                            ddlOrigemPagamentoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                            ddlDestinatarioCobrancaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                            ddlBaseCalculoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")

                            If dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 38 Or dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 40 Or dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 41 Then
                                txtQtdBaseCalculo.Enabled = True
                            Else
                                txtQtdBaseCalculo.Enabled = False
                            End If

                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")) Then
                            txtQtdBaseCalculo.Text = dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")) Then
                            ddlMoedaCompraTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")) Then
                            txtValorTaxaCompra.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")) Then
                            txtValorTaxaCompraMin.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")) Then
                            txtValorTaxaCompraCalc.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")) Then
                            ddlMoedaVendaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA")) Then
                            txtValorTaxaVenda.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")) Then
                            txtValorTaxaVendaMin.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")) Then
                            txtValorTaxaVendaCalc.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                            txtObsTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("OB_TAXAS")
                        End If

                        Dim finaliza As New FinalizaCotacao
                        If finaliza.TaxaBloqueada(txtIDTaxa.Text, "COTACAO", "R") = True Then
                            ddlMoedaVendaTaxa.Enabled = False
                            txtValorTaxaVenda.Enabled = False
                            txtValorTaxaVendaMin.Enabled = False
                            txtValorTaxaVendaCalc.Enabled = False
                            ddlDestinatarioCobrancaTaxa.Enabled = False
                        End If

                        If finaliza.TaxaBloqueada(txtIDTaxa.Text, "COTACAO", "P") = True Then
                            ddlMoedaCompraTaxa.Enabled = False
                            txtValorTaxaCompra.Enabled = False
                            txtValorTaxaCompraMin.Enabled = False
                            txtValorTaxaCompraCalc.Enabled = False
                            ddlFornecedor.Enabled = False
                        End If


                        If ddlStatusCotacao.SelectedValue = 12 Or ddlStatusCotacao.SelectedValue = 15 Or ddlStatusCotacao.SelectedValue = 9 Then
                            btnSalvarTaxa.Visible = False
                        Else
                            btnSalvarTaxa.Visible = True
                        End If



                        mpeNovoTaxa.Show()

                    End If

                ElseIf ProximaLinha > ds.Tables(0).Rows.Count Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,ID_FORNECEDOR,
ID_COTACAO,
ID_ITEM_DESPESA,
ID_TIPO_PAGAMENTO,
ID_ORIGEM_PAGAMENTO,
FL_DECLARADO,
FL_DIVISAO_PROFIT,
ID_DESTINATARIO_COBRANCA,
ID_MOEDA_COMPRA,
VL_TAXA_COMPRA_CALCULADO,
ID_MOEDA_VENDA,
VL_TAXA_VENDA_CALCULADO,
ID_BASE_CALCULO_TAXA,
OB_TAXAS,
VL_TAXA_VENDA_MIN,
VL_TAXA_COMPRA,
VL_TAXA_VENDA,
VL_TAXA_COMPRA_MIN,
QTD_BASE_CALCULO
FROM TB_COTACAO_TAXA A
WHERE A.ID_COTACAO_TAXA =  " & PrimeiraTaxa)
                    If dsTaxa.Tables(0).Rows.Count > 0 Then
                        LimparDadosTaxa()
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")) Then
                            txtIDTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_FORNECEDOR")) Then
                            ddlFornecedor.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_FORNECEDOR")
                        End If

                        ckbDeclaradoTaxa.Checked = dsTaxa.Tables(0).Rows(0).Item("FL_DECLARADO")
                        ckbProfitTaxa.Checked = dsTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                            ddlItemDespesaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                            ddlTipoPagamentoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                            ddlOrigemPagamentoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                            ddlDestinatarioCobrancaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                            ddlBaseCalculoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")

                            If dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 38 Or dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 40 Or dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 41 Then
                                txtQtdBaseCalculo.Enabled = True
                            Else
                                txtQtdBaseCalculo.Enabled = False
                            End If

                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")) Then
                            txtQtdBaseCalculo.Text = dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO")
                        End If

                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")) Then
                            ddlMoedaCompraTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")) Then
                            txtValorTaxaCompra.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")) Then
                            txtValorTaxaCompraMin.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")) Then
                            txtValorTaxaCompraCalc.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")) Then
                            ddlMoedaVendaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA")) Then
                            txtValorTaxaVenda.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")) Then
                            txtValorTaxaVendaMin.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")) Then
                            txtValorTaxaVendaCalc.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO")
                        End If
                        If Not IsDBNull(dsTaxa.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                            txtObsTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("OB_TAXAS")
                        End If

                        Dim finaliza As New FinalizaCotacao
                        If finaliza.TaxaBloqueada(txtIDTaxa.Text, "COTACAO", "R") = True Then
                            ddlMoedaVendaTaxa.Enabled = False
                            txtValorTaxaVenda.Enabled = False
                            txtValorTaxaVendaMin.Enabled = False
                            txtValorTaxaVendaCalc.Enabled = False
                            ddlDestinatarioCobrancaTaxa.Enabled = False
                        End If

                        If finaliza.TaxaBloqueada(txtIDTaxa.Text, "COTACAO", "P") = True Then
                            ddlMoedaCompraTaxa.Enabled = False
                            txtValorTaxaCompra.Enabled = False
                            txtValorTaxaCompraMin.Enabled = False
                            txtValorTaxaCompraCalc.Enabled = False
                            ddlFornecedor.Enabled = False
                        End If


                        If ddlStatusCotacao.SelectedValue = 12 Or ddlStatusCotacao.SelectedValue = 15 Or ddlStatusCotacao.SelectedValue = 9 Then
                            btnSalvarTaxa.Visible = False
                        Else
                            btnSalvarTaxa.Visible = True
                        End If



                        mpeNovoTaxa.Show()

                    End If
                End If

            Next

        End If


    End Sub


    Function ArredondarPesoTaxado(ID_COTACAO As String, Optional PESO_TAXADO As Decimal = 0) As Decimal

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim M3 As Decimal = 0
        Dim PESO_BRUTO As Decimal = 0

        If txtM3Mercadoria.Text <> "" Then
            M3 = txtM3Mercadoria.Text
        End If

        If txtPesoBrutoMercadoria.Text <> "" Then
            PESO_BRUTO = txtPesoBrutoMercadoria.Text
        End If


        If PESO_TAXADO = 0 Then
            Dim ds As DataSet = Con.ExecutarQuery("Select A.ID_TIPO_ESTUFAGEM, A.ID_SERVICO from TB_COTACAO A Where A.ID_COTACAO = " & ID_COTACAO)
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

    Private Sub txtPesoBrutoMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtPesoBrutoMercadoria.TextChanged
        txtPesoTaxadoMercadoria.Text = ArredondarPesoTaxado(txtID.Text)
    End Sub

    Private Sub txtPesoTaxadoMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtPesoTaxadoMercadoria.TextChanged
        txtPesoTaxadoMercadoria.Text = ArredondarPesoTaxado(txtID.Text, txtPesoTaxadoMercadoria.Text)
    End Sub
    Private Sub txtM3Mercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtM3Mercadoria.TextChanged
        txtPesoTaxadoMercadoria.Text = ArredondarPesoTaxado(txtID.Text)
    End Sub
    Private Sub txtCBMAereo_TextChanged(sender As Object, e As EventArgs) Handles txtCBMAereo.TextChanged

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM  TB_COTACAO_MERCADORIA_DIMENSAO WHERE ID_COTACAO =" & txtID.Text)
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            If txtPesoBrutoMercadoria.Text <> "" And txtCBMAereo.Text <> "" Then
                Dim PesoBruto As Decimal = txtPesoBrutoMercadoria.Text
                Dim CBM As Decimal = txtCBMAereo.Text
                CBM = CBM * 167
                txtM3Mercadoria.Text = CBM.ToString("0.000")
            End If
        End If

        txtPesoTaxadoMercadoria.Text = ArredondarPesoTaxado(txtID.Text)
    End Sub
    Sub CalculaM3Maritimo()
        If Session("estufagem") = 2 Then

            If txtQtdMercadoria.Text <> "" And txtComprimentoMercadoria.Text <> "" And txtLarguraMercadoria.Text <> "" And txtAlturaMercadoria.Text <> "" Then
                Dim QTD As Decimal = txtQtdMercadoria.Text
                Dim COMP As Decimal = txtComprimentoMercadoria.Text
                Dim LARG As Decimal = txtLarguraMercadoria.Text
                Dim ALT As Decimal = txtAlturaMercadoria.Text
                Dim M3 As Decimal = QTD * COMP * LARG * ALT

                txtM3Mercadoria.Text = M3.ToString("0.000")
            End If

        Else

            If txtComprimentoMercadoria.Text <> "" And txtLarguraMercadoria.Text <> "" And txtAlturaMercadoria.Text <> "" Then
                Dim COMP As Decimal = txtComprimentoMercadoria.Text
                Dim LARG As Decimal = txtLarguraMercadoria.Text
                Dim ALT As Decimal = txtAlturaMercadoria.Text
                Dim M3 As Decimal = COMP * LARG * ALT

                txtM3Mercadoria.Text = M3.ToString("0.000")
            End If
        End If
    End Sub
    Private Sub txtComprimentoMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtComprimentoMercadoria.TextChanged
        CalculaM3Maritimo()
    End Sub

    Private Sub txtLarguraMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtLarguraMercadoria.TextChanged
        CalculaM3Maritimo()
    End Sub

    Private Sub txtAlturaMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtAlturaMercadoria.TextChanged
        CalculaM3Maritimo()
    End Sub

    Private Sub txtQtdMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtQtdMercadoria.TextChanged
        CalculaM3Maritimo()
    End Sub

    Private Sub ddlEstufagem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEstufagem.SelectedIndexChanged

        Session("estufagem") = ddlEstufagem.SelectedValue
        MaritimoXAereo()

    End Sub

    Private Sub dgvArquivos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvArquivos.RowCommand
        divErroUpload.Visible = False
        divSuccessUpload.Visible = False

        If e.CommandName = "Excluir" Then

            Try
                Dim Con As New Conexao_sql
                Con.Conectar()
                Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroUpload.Text = "Usuário não tem permissão para realizar exclusões"
                    divErroUpload.Visible = True
                Else

                    Dim CommandArgument As String = e.CommandArgument

                    Dim ID_ARQUIVO As String = CommandArgument.Substring(0, CommandArgument.IndexOf("|"))

                    Dim CAMINHO_ARQUIVO As String = CommandArgument.Substring(CommandArgument.IndexOf("|"))
                    CAMINHO_ARQUIVO = CAMINHO_ARQUIVO.Replace("|", "")

                    File.Delete(CAMINHO_ARQUIVO)

                    Con.ExecutarQuery("DELETE FROM TB_UPLOADS WHERE ID_ARQUIVO = " & ID_ARQUIVO)
                    divSuccessUpload.Visible = True
                    dgvArquivos.DataBind()

                End If

            Catch ex As Exception
                lblErroUpload.Text = ex.Message
                divErroUpload.Visible = True
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
                lblErroUpload.Text = ex.Message
                divErroUpload.Visible = True
            End Try


        End If

        txtUP.Text = 1

    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        divErroUpload.Visible = False
        divSuccessUpload.Visible = False

        If txtID.Text = "" Then
            lblErroUpload.Text = "Necessário inserir cotação!"
            divErroUpload.Visible = True

        ElseIf ddlTipoArquivo.selectedvalue = 0 Then
            lblErroUpload.Text = "Necessário selecionar um tipo de arquivo!"
            divErroUpload.Visible = True


        ElseIf FileUpload1.HasFile Then


            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim nomeArquivo As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim ds As DataSet = Con.ExecutarQuery(" SELECT COUNT(*)QTD FROM TB_UPLOADS WHERE ID_COTACAO=" & txtID.Text & " AND ID_TIPO_ARQUIVO = " & ddlTipoArquivo.SelectedValue & " AND NM_ARQUIVO ='" & nomeArquivo & "'")
            If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                lblErroUpload.Text = "Arquivo já existe!"
                divErroUpload.Visible = True
            Else

                Dim diretorio_arquivos As String = System.Configuration.ConfigurationSettings.AppSettings("CaminhoUploads") & "\Uploads\Cotacao_" & txtID.Text

                If Not Directory.Exists(diretorio_arquivos) Then
                    System.IO.Directory.CreateDirectory(diretorio_arquivos)
                End If

                FileUpload1.PostedFile.SaveAs(diretorio_arquivos & "\" & nomeArquivo)

                If txtID_BL.Text = "" Then
                    txtID_BL.Text = "0"
                End If

                Con.ExecutarQuery("INSERT INTO TB_UPLOADS (NM_ARQUIVO,ID_TIPO_ARQUIVO,ID_USUARIO,DT_UPLOAD,FL_ATIVO_CLIENTES,ID_COTACAO,ID_BL,CAMINHO_ARQUIVO) VALUES ('" & nomeArquivo & "'," & ddlTipoArquivo.SelectedValue & "," & Session("ID_USUARIO") & ", getdate(), '" & ckAtivoClientes.Checked & "'," & txtID.Text & "," & txtID_BL.Text & ",'" & diretorio_arquivos & "/" & nomeArquivo & "' )")

                divSuccessUpload.Visible = True
                dgvArquivos.DataBind()

            End If
        Else

            lblErroUpload.Text = "Por favor, selecione um arquivo a enviar."
            divErroUpload.Visible = True

        End If
        ddlTipoArquivo.SelectedValue = 0
        txtUP.Text = 1
    End Sub

    Private Sub btnLimparUpload_Click(sender As Object, e As EventArgs) Handles btnLimparUpload.Click
        divErroUpload.Visible = False
        divSuccessUpload.Visible = False
        txtUP.Text = 1
        ddlTipoArquivo.SelectedValue = 0
        FileUpload1.FileContent.Flush()
    End Sub

    Public Sub ckAtivoClientes_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        divErroUpload.Visible = False
        divSuccessUpload.Visible = False
        Dim chk As CheckBox = DirectCast(sender, CheckBox)
        Dim row = DirectCast(chk.NamingContainer, GridViewRow)
        Dim ID_ARQUIVO = DirectCast(row.FindControl("lblID_ARQUIVO"), Label).Text
        Dim Con As New Conexao_sql
        Con.Conectar()
        Con.ExecutarQuery("UPDATE TB_UPLOADS SET FL_ATIVO_CLIENTES = '" & chk.Checked & "' WHERE ID_ARQUIVO = " & ID_ARQUIVO)
        divSuccessUpload.Visible = True
        dgvArquivos.DataBind()
        Con.Fechar()
        txtUP.Text = 1
    End Sub

    Private Sub dgvArquivos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvArquivos.RowDataBound
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


    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        If txtID.Text = "" Then
            diverro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja enviar!"

        Else

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "EnviarCotacao()", True)

        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtID.Text = "" Then
            diverro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja imprimir!"

        Else
            lblmsgErro.Text = ""
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("Select count(*)QTD from TB_COTACAO where DT_CALCULO_COTACAO is not null and id_cotacao = " & txtID.Text)
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                diverro.Visible = True
                lblmsgErro.Text = "Cotação necessita de cálculo!"
            Else

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ImprimirCotacao()", True)

            End If
            Con.Fechar()
        End If
    End Sub

    Sub VerificaRepetida()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT  REF_REPETIDAS ,DT_ABERTURA, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_TIPO_ESTUFAGEM, ID_INCOTERM, VL_TOTAL_PESO_BRUTO, VL_M3 FROM [dbo].[View_Cotacao_Repetidas] WHERE ID_COTACAO = " & txtID.Text)
        If dsCotacao.Tables(0).Rows.Count > 0 Then

            Dim dsCopias As DataSet = Con.ExecutarQuery("SELECT  REF_REPETIDAS ,DT_ABERTURA, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_TIPO_ESTUFAGEM, ID_INCOTERM, VL_TOTAL_PESO_BRUTO, VL_M3 FROM [dbo].[View_Cotacao_Repetidas] WHERE REF_REPETIDAS = '" & dsCotacao.Tables(0).Rows(0).Item("REF_REPETIDAS") & "' AND  ID_COTACAO <> " & txtID.Text)
            If dsCopias.Tables(0).Rows.Count > 0 Then
                If dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM") <> dsCopias.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM") Or dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_DESTINO") <> dsCopias.Tables(0).Rows(0).Item("ID_PORTO_DESTINO") Or dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") <> dsCopias.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") Or dsCotacao.Tables(0).Rows(0).Item("ID_INCOTERM") <> dsCopias.Tables(0).Rows(0).Item("ID_INCOTERM") Or dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_PESO_BRUTO") <> dsCopias.Tables(0).Rows(0).Item("VL_TOTAL_PESO_BRUTO") Or dsCotacao.Tables(0).Rows(0).Item("VL_M3") <> dsCopias.Tables(0).Rows(0).Item("VL_M3") Or dsCotacao.Tables(0).Rows(0).Item("DT_ABERTURA") <> dsCopias.Tables(0).Rows(0).Item("DT_ABERTURA") Then
                    Con.ExecutarQuery("UPDATE TB_COTACAO SET REF_REPETIDAS = NULL WHERE ID_COTACAO = " & txtID.Text)
                End If
            End If

        End If
    End Sub

    Private Sub btnConfirmaEnviarSI_Click(sender As Object, e As EventArgs) Handles btnConfirmaEnviarSI.Click
        Dim Con As New Conexao_sql
        Con.Conectar()
        Con.ExecutarQuery("UPDATE TB_COTACAO SET FL_ENVIA_SI = 1 WHERE ID_COTACAO = " & txtID.Text)
        Gravar()
        mpeEnvioSI.Hide()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefaultSI()", True)
    End Sub

    Private Sub btnCancelaEnvioSI_Click(sender As Object, e As EventArgs) Handles btnCancelaEnvioSI.Click
        Dim Con As New Conexao_sql
        Con.Conectar()
        Con.ExecutarQuery("UPDATE TB_COTACAO SET FL_ENVIA_SI = 0 WHERE ID_COTACAO = " & txtID.Text)
        Gravar()
        mpeEnvioSI.Hide()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefaultSI()", True)
    End Sub
    Sub AtualizaPortoAgenteSI()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlAgente.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            lblAgenteSI.Text = "Agente Internacional: " & ds.Tables(0).Rows(0).Item("NM_RAZAO")
        Else
            lblAgenteSI.Text = ""
        End If

        ds = Con.ExecutarQuery("SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = " & ddlOrigemFrete.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            lblPortoOrigemSI.Text = "Porto de Origem: " & ds.Tables(0).Rows(0).Item("NM_PORTO")
        Else
            lblPortoOrigemSI.Text = ""
        End If
    End Sub

    Public Sub ckEnvioSI_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        divErroUpload.Visible = False
        divSuccessUpload.Visible = False
        Dim chk As CheckBox = DirectCast(sender, CheckBox)
        Dim row = DirectCast(chk.NamingContainer, GridViewRow)
        Dim ID_ARQUIVO = DirectCast(row.FindControl("lblID_ARQUIVO"), Label).Text
        Dim Con As New Conexao_sql
        Con.Conectar()
        Con.ExecutarQuery("UPDATE TB_UPLOADS SET FL_ENVIA_SI = '" & chk.Checked & "' WHERE ID_ARQUIVO = " & ID_ARQUIVO)
        divSuccessUpload.Visible = True
        dgvArquivos.DataBind()
        Con.Fechar()
        txtUP.Text = 1
    End Sub

    Private Sub ddlDivisaoProfit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDivisaoProfit.SelectedIndexChanged
        If ddlDivisaoProfit.SelectedValue = 0 Or ddlDivisaoProfit.SelectedValue = 11 Then
            txtValorDivisaoProfit.Text = 0
            txtValorDivisaoProfit.Enabled = False
        Else
            txtValorDivisaoProfit.Enabled = True
        End If
    End Sub
End Class