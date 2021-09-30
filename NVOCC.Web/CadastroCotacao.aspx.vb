Public Class CadastroCotacao
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            Session("estufagem") = 0
            ddlTipoPagamentoTaxa.SelectedValue = 1
            ddlDestinatarioCobrancaTaxa.SelectedValue = 1

            CarregaCampos()
            VerificaEstufagem()
            VerificaTransporte()

        Else
            If txtID.Text = "" And Not Page.IsPostBack Then
                ' txtNumeroCotacao.Text = NumeroCotacao()
                txtCotacaoTaxa.Text = txtNumeroCotacao.Text
                txtCotacaoMercadoria.Text = txtNumeroCotacao.Text
                ddlUsuarioStatus.SelectedValue = Session("ID_USUARIO")
                ddlAnalista.SelectedValue = Session("ID_USUARIO")
                txtAbertura.Text = Now.Date.ToString("dd-MM-yyyy")
                btnNovoFrete.Attributes.CssStyle.Add("display", "none")
                ddlStatusCotacao.SelectedValue = 1
                'ddlStatusCotacao.Enabled = False
                Session("ID_CLIENTE") = 0

            End If
        End If

        'GridHistoricoCotacao()
        'GridHistoricoFrete()


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

        ds = Con.ExecutarQuery("SELECT A.ID_COTACAO,ID_FRETE_TRANSPORTADOR,A.NR_COTACAO,ID_PORTO_DESTINO,ID_PORTO_ORIGEM,ID_TRANSPORTADOR,ID_PARCEIRO_INDICADOR,
CONVERT(varchar,A.DT_ABERTURA,103)DT_ABERTURA,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR,
A.ID_STATUS_COTACAO,
CONVERT(varchar,A.DT_STATUS_COTACAO,103)DT_STATUS_COTACAO,
CONVERT(varchar,A.DT_VALIDADE_COTACAO,103)DT_VALIDADE_COTACAO,
CONVERT(varchar,A.DT_ENVIO_COTACAO,103)DT_ENVIO_COTACAO,
A.ID_ANALISTA_COTACAO,A.ID_AGENTE_INTERNACIONAL,A.ID_TIPO_BL,A.ID_INCOTERM,A.ID_TIPO_ESTUFAGEM,A.ID_DESTINATARIO_COMERCIAL,A.ID_CLIENTE,A.ID_CLIENTE_FINAL,A.ID_CONTATO,A.ID_SERVICO,A.ID_VENDEDOR,A.OB_CLIENTE,A.OB_MOTIVO_CANCELAMENTO,A.OB_OPERACIONAL,A.ID_MOTIVO_CANCELAMENTO,
CONVERT(varchar,A.DT_CALCULO_COTACAO,103)DT_CALCULO_COTACAO,ID_TIPO_CARGA, 
NR_PROCESSO_GERADO,ID_PROCESSO, ID_USUARIO_STATUS,(SELECT FL_COTACAO_APROVADA FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO = A.ID_STATUS_COTACAO )FL_COTACAO_APROVADA, (SELECT FL_ENCERRA_COTACAO FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO = A.ID_STATUS_COTACAO )FL_ENCERRA_COTACAO
FROM  TB_COTACAO A
    WHERE A.ID_COTACAO = " & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then

            'Informaçoes basicas
            txtID.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
            txtNumeroCotacao.Text = ds.Tables(0).Rows(0).Item("NR_COTACAO").ToString()
            ckbFreeHand.Checked = ds.Tables(0).Rows(0).Item("FL_FREE_HAND")


            txtAbertura.Text = ds.Tables(0).Rows(0).Item("DT_ABERTURA").ToString()
            ddlStatusFreteAgente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString()

            If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 10 Then
                Dim sql As String = "SELECT ID_STATUS_COTACAO, NM_STATUS_COTACAO FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO IN(7,8,9, " & ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO").ToString() & ")
union SELECT  0, 'Selecione' ORDER BY ID_STATUS_COTACAO"
                Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                If ds1.Tables(0).Rows.Count > 0 Then
                    dsStatusCotacao.SelectCommand = sql
                    ddlStatusCotacao.DataBind()
                End If
            End If
            ddlStatusCotacao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO").ToString()

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_STATUS_COTACAO")) Then
                txtDataStatus.Text = ds.Tables(0).Rows(0).Item("DT_STATUS_COTACAO").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_COTACAO")) Then
                txtValidade.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_COTACAO").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_ENVIO_COTACAO")) Then
                txtEnvio.Text = ds.Tables(0).Rows(0).Item("DT_ENVIO_COTACAO").ToString()
            End If

            txtCodIndicador.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString()
            ddlIndicador.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString()
            ddlDestinoFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString()
            ddlOrigemFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString()
            txtCodTransportador.Text = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
            Session("ID_TRANSPORTADOR") = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()

            ddlTransportadorFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
            ddlFornecedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
            ddlDestinatarioComercial.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COMERCIAL").ToString()
            txtCodAgente.Text = ds.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString()
            ddlAgente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString()
            ddlAnalista.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ANALISTA_COTACAO").ToString()
            ddlIncoterm.SelectedValue = ds.Tables(0).Rows(0).Item("ID_INCOTERM").ToString()
            txtCodCliente.Text = ds.Tables(0).Rows(0).Item("ID_CLIENTE").ToString()
            ddlCliente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CLIENTE").ToString()
            txtCodExportador.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString()
            ddlExportador.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString()
            txtCodImportador.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString()
            ddlImportador.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString()

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
            If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                txtViaTransporte.Text = 4
            Else
                txtViaTransporte.Text = 1

            End If

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

            If ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL") = 0 Then
                divClienteFinal.Attributes.CssStyle.Add("display", "none")
            Else
                dsClienteFinal.SelectCommand = "SELECT ID_CLIENTE_FINAL,NM_CLIENTE_FINAL FROM TB_CLIENTE_FINAL WHERE ID_CLIENTE_FINAL = " & ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL") & "
union SELECT  0, ' Selecione' ORDER BY NM_CLIENTE_FINAL"
                ddlClienteFinal.DataBind()
                divClienteFinal.Attributes.CssStyle.Add("display", "block")
                ddlClienteFinal.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL").ToString()

            End If

            ddlTipoBL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_BL").ToString()
            'ddlMercadoria.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString()
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
                'ddlFreteTransportador_Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()
                Dim sql As String = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE  ( ID_FRETE_TRANSPORTADOR = " & ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString() & ") or (ID_PORTO_ORIGEM =  " & ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString() & " AND ID_PORTO_DESTINO = " & ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString() & " ) union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "

                Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                If ds1.Tables(0).Rows.Count > 0 Then
                    dsFreteTransportador.SelectCommand = sql
                    ddlFreteTransportador_Frete.DataBind()
                End If
                Con.Fechar()

                ddlFreteTransportador_Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")
                Session("ID_FRETE_TRANSPORTADOR") = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()

            End If

            Session("ID_STATUS") = ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO").ToString()
            Session("estufagem") = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString()
            Session("transporte") = ds.Tables(0).Rows(0).Item("ID_SERVICO").ToString()
            Session("ID_CLIENTE") = ds.Tables(0).Rows(0).Item("ID_CLIENTE").ToString()

            GridHistoricoCotacao()
            GridHistoricoFrete()



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

    Protected Sub dgvHistoricoFrete_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvHistoricoFrete.DataSource = Session("TaskTable")
            dgvHistoricoFrete.DataBind()
            dgvHistoricoFrete.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

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
        ddlTipoContainerMercadoria.SelectedValue = 0
        txtQtdContainerMercadoria.Text = ""
        txtFreteCompraMercadoriaCalc.Text = ""
        txtFreteVendaMercadoriaCalc.Text = ""
        txtPesoBrutoMercadoria.Text = ""
        txtM3Mercadoria.Text = ""
        txtComprimentoMercadoria.Text = ""
        txtLarguraMercadoria.Text = ""
        txtAlturaMercadoria.Text = ""
        txtValorCargaMercadoria.Text = ""
        txtFreeTimeMercadoria.Text = ""
        txtQtdMercadoria.Text = ""
        txtDsMercadoria.Text = ""
        txtOutrasOBS_Mercadoria.Text = ""
        txtIDMercadoria.Text = ""
        txtFreteVendaMinima.Text = ""
        txtFreteCompraMinima.Text = ""
        txtFreteCompraMercadoriaUnitario.Text = ""
        txtFreteVendaMercadoriaUnitario.Text = ""

        mpeNovoMercadoria.Hide()
    End Sub

    Private Sub btnFecharTaxa_Click(sender As Object, e As EventArgs) Handles btnFecharTaxa.Click
        txtIDTaxa.Text = ""
        ddlItemDespesaTaxa.SelectedValue = 0
        ddlOrigemPagamentoTaxa.SelectedValue = 0
        ddlBaseCalculoTaxa.SelectedValue = 0
        ddlMoedaCompraTaxa.SelectedValue = 0
        ddlMoedaVendaTaxa.SelectedValue = 0
        ddlTipoPagamentoTaxa.SelectedValue = 1
        ddlDestinatarioCobrancaTaxa.SelectedValue = 1
        ddlFornecedor.SelectedValue = ddlTransportadorFrete.SelectedValue
        txtValorTaxaCompra.Text = ""
        txtValorTaxaVenda.Text = ""
        txtValorTaxaVendaMin.Text = ""
        txtValorTaxaCompraMin.Text = ""
        txtValorTaxaCompraCalc.Text = ""
        txtValorTaxaVendaCalc.Text = ""
        divErroTaxa.Visible = False
        divSuccessTaxa.Visible = False
        txtObsTaxa.Text = ""

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
    Sub GridHistoricoFrete()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT CNPJ FROM TB_PARCEIRO  WHERE ID_PARCEIRO = " & Session("ID_CLIENTE"))
        If ds.Tables(0).Rows.Count > 0 Then
            txtcnpj.Text = ds.Tables(0).Rows(0).Item("CNPJ").ToString()
        Else
            txtcnpj.Text = 0
        End If

        dsHistoricoFrete.SelectCommand = "SELECT * FROM VW_VALOR_FRETE_LOTE where rownum <= " & txtQtd.Text & " and cnpj = '" & txtcnpj.Text & "' "
        dgvHistoricoFrete.DataBind()


        dsHistoricoFrete.SelectParameters("cnpj").DefaultValue = txtcnpj.Text
        dgvHistoricoFrete.DataBind()

    End Sub
    Private Sub dgvFrete_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFrete.RowCommand
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()

        If e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("
SELECT ID_COTACAO,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TIPO_PAGAMENTO,ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN ,VL_TOTAL_FRETE_COMPRA_MIN,TRANSITTIME_TRUCKING_AEREO  FROM TB_COTACAO WHERE ID_COTACAO = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Frete
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                    ddlOrigemFrete.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                    ddlDestinoFrete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")) Then

                    Dim sql As String = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE  ( ID_FRETE_TRANSPORTADOR = " & ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString() & ") or (ID_PORTO_ORIGEM =  " & ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString() & " AND ID_PORTO_DESTINO = " & ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString() & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & " ) union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "

                    Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                    If ds1.Tables(0).Rows.Count > 0 Then
                        dsFreteTransportador.SelectCommand = sql
                        ddlFreteTransportador_Frete.DataBind()
                    End If
                    Con.Fechar()

                    ddlFreteTransportador_Frete.SelectedValue = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")


                    If ddlFreteTransportador_Frete.SelectedValue <> 0 Then
                        ddlMoedaFrete.Enabled = "False"
                        txtTTimeFreteInicial.Enabled = "False"
                        txtFreteCompra.Enabled = "False"
                        txtTTimeFreteFinal.Enabled = "False"
                    Else
                        ddlMoedaFrete.Enabled = "True"
                        txtTTimeFreteInicial.Enabled = "True"
                        txtFreteCompra.Enabled = "True"
                        txtTTimeFreteFinal.Enabled = "True"
                    End If
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
                    txtPesoTaxadoFrete.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA")) Then
                    txtFreteCompra.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA")) Then
                    txtFreteVenda.Enabled = False
                    txtFreteVenda.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA")
                Else
                    txtFreteVenda.Enabled = True

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
                Con.ExecutarQuery("DELETE From TB_COTACAO_TAXA Where ID_COTACAO_TAXA = " & ID)
                lblDeleteTaxas.Text = "Registro deletado!"
                divDeleteTaxas.Visible = True
                dgvTaxas.DataBind()
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument
            dsFornecedor.DataBind()
            ddlFornecedor.DataBind()

            ds = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,ID_FORNECEDOR,
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
VL_TAXA_COMPRA_MIN
FROM TB_COTACAO_TAXA A
WHERE A.ID_COTACAO_TAXA = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")) Then
                    txtIDTaxa.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_FORNECEDOR")) Then
                    ddlFornecedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_FORNECEDOR")
                End If

                ckbDeclaradoTaxa.Checked = ds.Tables(0).Rows(0).Item("FL_DECLARADO")
                ckbProfitTaxa.Checked = ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                    ddlItemDespesaTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                End If
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
                lblDeleteMercadoria.Text = "Registro deletado!"
                divDeleteMercadoria.Visible = True
                dgvMercadoria.DataBind()
                AtualizaFreteMercadoria()
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT 
A.ID_COTACAO_MERCADORIA,A.ID_COTACAO,A.ID_MERCADORIA,A.ID_TIPO_CONTAINER,A.QT_CONTAINER,A.VL_FRETE_COMPRA,VL_FRETE_COMPRA_UNITARIO,VL_FRETE_VENDA_UNITARIO,
A.VL_FRETE_VENDA,A.VL_PESO_BRUTO,A.VL_M3,A.OUTRAS_OBS,A.DS_MERCADORIA,A.VL_COMPRIMENTO,A.VL_LARGURA,A.VL_ALTURA,A.VL_CARGA,A.QT_DIAS_FREETIME,A.QT_MERCADORIA,A.VL_FRETE_COMPRA_MIN,A.VL_FRETE_VENDA_MIN,OBS_ENDERECO FROM TB_COTACAO_MERCADORIA A
WHERE ID_COTACAO_MERCADORIA = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
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
                                'Dim valor As Double = ds1.Tables(0).Rows(0).Item("VL_COMPRA")
                                If txtQtdContainerMercadoria.Text > 0 Then
                                    'Dim qtd As Integer = txtQtdContainerMercadoria.Text
                                    'Dim X As Double = valor * qtd
                                    'Dim TOTAL As String = X.ToString("#,###.00")
                                    'txtFreteCompraMercadoriaCalc.Text = TOTAL,                        
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
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_M3")) Then
                    txtM3Mercadoria.Text = ds.Tables(0).Rows(0).Item("VL_M3")
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
        diverro.Visible = False
        divsuccess.Visible = False
        divinfo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim PROCESSO As String = ""
        Dim v As New VerificaData

        txtAbertura.Text = txtAbertura.Text.Replace("-", "/")

        If txtAbertura.Text = "" Or ddlStatusCotacao.SelectedValue = 0 Or ddlUsuarioStatus.SelectedValue = 0 Or txtValidade.Text = "" Or ddlDestinatarioComercial.SelectedValue = 0 Or ddlAnalista.SelectedValue = 0 Or ddlCliente.SelectedValue = 0 Or ddlIncoterm.SelectedValue = 0 Or ddlEstufagem.SelectedValue = 0 Or ddlTipoBL.SelectedValue = 0 Or ddlServico.SelectedValue = 0 Or ddlVendedor.SelectedValue = 0 Then
            lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Informações Básicas."
            diverro.Visible = True

        ElseIf v.ValidaData(txtAbertura.Text) = False Then
            diverro.Visible = True
            lblmsgErro.Text = "A data de abertura é inválida."

        ElseIf txtProcessoCotacao.Text = "" And ddlStatusCotacao.SelectedValue = 10 Then
            diverro.Visible = True
            lblmsgErro.Text = "Apenas cotações com número de processo gerado podem ser colocadas em update"

        Else

            Dim dsContatos As DataSet = Con.ExecutarQuery("SELECT ID_CONTATO, NM_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue & "
union SELECT  0, 'Selecione' ORDER BY ID_CONTATO")
            If dsContatos.Tables(0).Rows.Count > 1 And ddlContato.SelectedValue = 0 Then
                lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Informações Básicas."
                diverro.Visible = True
                Exit Sub
            End If

            If txtObsCliente.Text = "" Then
                txtObsCliente.Text = "NULL"
            Else
                txtObsCliente.Text = "'" & txtObsCliente.Text & "'"
            End If

            If txtObsCancelamento.Text = "" Then
                txtObsCancelamento.Text = "NULL"
            Else
                txtObsCancelamento.Text = "'" & txtObsCancelamento.Text & "'"
            End If

            If txtObsOperacional.Text = "" Then
                txtObsOperacional.Text = "NULL"
            Else
                txtObsOperacional.Text = "'" & txtObsOperacional.Text & "'"
            End If

            'If txtProcessoCotacao.Text = "" Then
            '    txtProcessoCotacao.Text = "NULL"
            'Else
            '    PROCESSO = txtProcessoCotacao.Text
            '    txtProcessoCotacao.Text = "'" & txtProcessoCotacao.Text & "'"
            'End If

            txtObsCliente.Text = txtObsCliente.Text.Replace(vbNewLine, "<br/>")

            If txtID.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    diverro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else

                    Session("estufagem") = ddlEstufagem.SelectedValue
                    Session("transporte") = ddlServico.SelectedValue

                    VerificaEstufagem()
                    VerificaTransporte()

                    txtEnvio.Text = Now.Date.ToString("dd-MM-yyyy")
                    txtDataStatus.Text = Now.Date.ToString("dd-MM-yyyy")

                    'INSERE              
                    txtNumeroCotacao.Text = NumeroCotacao()

                    If txtNumeroCotacao.Text = "" Then
                        lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Informações Básicas."
                        diverro.Visible = True
                        Exit Sub

                    End If

                    ds = Con.ExecutarQuery("INSERT INTO TB_COTACAO (NR_COTACAO,ID_TIPO_BL, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_TIPO_ESTUFAGEM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, ID_USUARIO_STATUS, DT_VALIDADE_COTACAO,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR) VALUES ('" & txtNumeroCotacao.Text & "'," & ddlTipoBL.SelectedValue & ", getdate(), " & ddlStatusCotacao.SelectedValue & ", Convert(datetime, '" & txtDataStatus.Text & "', 103)," & ddlAnalista.SelectedValue & ", " & ddlAgente.SelectedValue & "," & ddlIncoterm.SelectedValue & "," & ddlEstufagem.SelectedValue & ", " & ddlDestinatarioComercial.SelectedValue & "," & ddlCliente.SelectedValue & "," & ddlClienteFinal.SelectedValue & ", " & ddlContato.SelectedValue & " , " & ddlServico.SelectedValue & " , " & ddlVendedor.SelectedValue & "," & txtObsCliente.Text & "," & txtObsCancelamento.Text & "," & txtObsOperacional.Text & "," & ddlMotivoCancelamento.SelectedValue & "," & ddlUsuarioStatus.SelectedValue & ",Convert(datetime, '" & txtValidade.Text & "', 103),'" & ckbFreeHand.Checked & "'," & ddlStatusFreteAgente.SelectedValue & ", " & ddlIndicador.SelectedValue & "," & ddlExportador.SelectedValue & "," & ddlImportador.SelectedValue & ") Select SCOPE_IDENTITY() as ID_COTACAO ")

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALCOTACAO =  " & Session("NR_COTACAO") & ", ANOSEQUENCIALCOTACAO = YEAR(GETDATE())")


                        'PREENCHE SESSÃO E CAMPO DE ID
                        Session("ID_COTACAO") = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                        txtID.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                        Session("ID_CLIENTE") = ddlCliente.SelectedValue

                        txtObsCliente.Text = txtObsCliente.Text.Replace("'", "")
                        txtObsCancelamento.Text = txtObsCancelamento.Text.Replace("'", "")
                        txtObsOperacional.Text = txtObsOperacional.Text.Replace("'", "")

                        txtObsCliente.Text = txtObsCliente.Text.Replace("NULL", "")
                        txtObsCancelamento.Text = txtObsCancelamento.Text.Replace("NULL", "")
                        txtObsOperacional.Text = txtObsOperacional.Text.Replace("NULL", "")

                        Con.Fechar()
                        divsuccess.Visible = True
                        dgvFrete.DataBind()
                        dgvHistoricoFrete.DataBind()
                        dgvHistoricoCotacao.DataBind()
                    End If



                    Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    diverro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else
                    If ddlStatusCotacao.SelectedValue <> Session("ID_STATUS") Then

                        'SEPARA E ENVIA EMAIL CASO COTAÇÃO ESTEJA APROVADA
                        If ddlStatusCotacao.SelectedValue = 9 Then
                            Dim reaprovamento As Boolean = False
                            ddlStatusCotacao.Enabled = False

                            If Session("ID_STATUS") = 10 Then
                                reaprovamento = True
                            Else
                                reaprovamento = False
                            End If


                            If NumeroProcesso(reaprovamento) = False Then
                                txtObsCliente.Text = txtObsCliente.Text.Replace("'", "")
                                txtObsCancelamento.Text = txtObsCancelamento.Text.Replace("'", "")
                                txtObsOperacional.Text = txtObsOperacional.Text.Replace("'", "")
                                txtDataCalculo.Text = txtDataCalculo.Text.Replace("'", "")

                                txtObsCliente.Text = txtObsCliente.Text.Replace("NULL", "")
                                txtObsCancelamento.Text = txtObsCancelamento.Text.Replace("NULL", "")
                                txtObsOperacional.Text = txtObsOperacional.Text.Replace("NULL", "")
                                txtDataCalculo.Text = txtDataCalculo.Text.Replace("NULL", "")

                                ddlStatusCotacao.Enabled = True

                                diverro.Visible = True
                                lblmsgErro.Text = "Já existe processo para esta cotação!"
                                Exit Sub
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


                    'REALIZA UPDATE  
                    Con.ExecutarQuery("UPDATE TB_COTACAO SET ID_STATUS_COTACAO = " & ddlStatusCotacao.SelectedValue & ", DT_VALIDADE_COTACAO = Convert(datetime, '" & txtValidade.Text & "', 103), ID_AGENTE_INTERNACIONAL = " & ddlAgente.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm.SelectedValue & ", ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & ", ID_DESTINATARIO_COMERCIAL = " & ddlDestinatarioComercial.SelectedValue & ", ID_CLIENTE = " & ddlCliente.SelectedValue & ", ID_CLIENTE_FINAL = " & ddlClienteFinal.SelectedValue & ", ID_CONTATO = " & ddlContato.SelectedValue & ", ID_SERVICO = " & ddlServico.SelectedValue & ", ID_VENDEDOR = " & ddlVendedor.SelectedValue & ", OB_CLIENTE = " & txtObsCliente.Text & ", OB_MOTIVO_CANCELAMENTO = " & txtObsCancelamento.Text & ", OB_OPERACIONAL = " & txtObsOperacional.Text & ", ID_MOTIVO_CANCELAMENTO = " & ddlMotivoCancelamento.SelectedValue & ", ID_TIPO_BL = " & ddlTipoBL.SelectedValue & ", FL_FREE_HAND = '" & ckbFreeHand.Checked & "', ID_STATUS_FRETE_AGENTE = " & ddlStatusFreteAgente.SelectedValue & ",ID_PARCEIRO_INDICADOR = " & ddlIndicador.SelectedValue & ", ID_PARCEIRO_EXPORTADOR  = " & ddlExportador.SelectedValue & ", ID_PARCEIRO_IMPORTADOR = " & ddlImportador.SelectedValue & " WHERE ID_COTACAO = " & txtID.Text)



                    Session("estufagem") = ddlEstufagem.SelectedValue
                    Session("transporte") = ddlServico.SelectedValue
                    Session("ID_CLIENTE") = ddlCliente.SelectedValue
                    Session("ID_STATUS") = ddlStatusCotacao.SelectedValue
                    VerificaEstufagem()
                    VerificaTransporte()

                    txtObsCliente.Text = txtObsCliente.Text.Replace("'", "")
                    txtObsCancelamento.Text = txtObsCancelamento.Text.Replace("'", "")
                    txtObsOperacional.Text = txtObsOperacional.Text.Replace("'", "")
                    txtDataCalculo.Text = txtDataCalculo.Text.Replace("'", "")

                    txtObsCliente.Text = txtObsCliente.Text.Replace("NULL", "")
                    txtObsCancelamento.Text = txtObsCancelamento.Text.Replace("NULL", "")
                    txtObsOperacional.Text = txtObsOperacional.Text.Replace("NULL", "")
                    txtDataCalculo.Text = txtDataCalculo.Text.Replace("NULL", "")

                    divsuccess.Visible = True
                    Con.Fechar()
                    dgvFrete.DataBind()

                    dgvHistoricoFrete.DataBind()
                    dgvHistoricoCotacao.DataBind()



                End If

            End If
            txtObsCliente.Text = txtObsCliente.Text.Replace("<br/>", vbNewLine)

            dsFornecedor.DataBind()
            ddlFornecedor.DataBind()
        End If
    End Sub

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

            'ElseIf ddlEstufagemFrete.SelectedValue = 1 And (ddlDivisaoProfit.SelectedValue = 0 Or txtValorDivisaoProfit.Text = "") Then
            '    lblErroFrete.Text = "Preencha os campos <strong>Tipo Divisão Profit</strong> e <strong>Valor Divisão Profit</strong>"
            '    divErroFrete.Visible = True
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
                TaxasIncluded = " '" & txtIncludedFrete.Text & "' "
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
TRANSITTIME_TRUCKING_AEREO = " & txtTTimeFreteTruckingAereo.Text & " 
WHERE ID_COTACAO = " & txtID.Text)

                'If Session("ID_FRETE_TRANSPORTADOR") <> ddlFreteTransportador_Frete.SelectedValue Then
                '    lblInfoFrete.Text = "É necessário excluir taxas da tabela de frete anterior e importar as da nova!"
                '    divInfoFrete.Visible = True
                '    Session("ID_FRETE_TRANSPORTADOR") = ddlFreteTransportador_Frete.SelectedValue
                'End If

                'If Session("ID_TRANSPORTADOR") <> ddlTransportadorFrete.SelectedValue Then
                '    lblInfoFrete.Text = "É necessário excluir taxas do transportador anterior e importar as do novo!"
                '    divInfoFrete.Visible = True
                '    Session("ID_TRANSPORTADOR") = ddlTransportadorFrete.SelectedValue
                'End If

                divSuccessFrete.Visible = True
                Con.Fechar()
                ImportaTaxas()
                dgvTaxas.DataBind()
                dgvFrete.DataBind()

                AtualizaFreteMercadoria()
            End If


        End If

        If ddlFreteTransportador_Frete.SelectedValue <> 0 Then
            ddlMoedaFrete.Enabled = "False"
            txtTTimeFreteInicial.Enabled = "False"
            txtFreteCompra.Enabled = "False"
            txtTTimeFreteFinal.Enabled = "False"
        Else
            ddlMoedaFrete.Enabled = "True"
            txtTTimeFreteInicial.Enabled = "True"
            txtFreteCompra.Enabled = "True"
            txtTTimeFreteFinal.Enabled = "True"
        End If
        mpeNovoFrete.Show()


    End Sub

    Sub AtualizaFreteMercadoria()
        Dim Con As New Conexao_sql
        Con.Conectar()
        If Session("estufagem") = 1 And ddlFreteTransportador_Frete.SelectedValue <> 0 Then
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_MERCADORIA FROM TB_COTACAO_MERCADORIA A INNER Join TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO
WHERE a.ID_COTACAO = " & txtID.Text & " And a.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER from TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103)) ")
            If ds.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In ds.Tables(0).Rows
                    Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET VL_FRETE_COMPRA = 
                (SELECT (SELECT VL_COMPRA FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103))* QT_CONTAINER FROM TB_COTACAO_MERCADORIA A WHERE A.ID_COTACAO_MERCADORIA = " & linha.Item("ID_COTACAO_MERCADORIA") & " ) 
                , VL_FRETE_COMPRA_UNITARIO = (SELECT (SELECT VL_COMPRA FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103)) FROM TB_COTACAO_MERCADORIA A WHERE A.ID_COTACAO_MERCADORIA = " & linha.Item("ID_COTACAO_MERCADORIA") & " ) ,
               QT_DIAS_FREETIME =     (SELECT (SELECT QT_DIAS_FREETIME FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER AND convert(date,getdate(),103) between convert(date,DT_VALIDADE_INICIAL,103) and convert(date,DT_VALIDADE_FINAL,103)) FROM TB_COTACAO_MERCADORIA A WHERE A.ID_COTACAO_MERCADORIA = " & linha.Item("ID_COTACAO_MERCADORIA") & " )
                WHERE ID_COTACAO_MERCADORIA =  " & linha.Item("ID_COTACAO_MERCADORIA"))
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


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        If txtID.Text = "" Then

            lblErroMercadoria.Text = "Antes de inserir Mercadoria é necessário cadastrar as Informações Basicas"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()


        ElseIf Session("estufagem") = 1 And txtQtdContainerMercadoria.Text = "" Then
            RedQTDContainer.Visible = True
            RedContainer.Visible = True
            ' RedFree.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 1 And ddlTipoContainerMercadoria.SelectedValue = 0 Then
            RedQTDContainer.Visible = True
            RedContainer.Visible = True
            '  RedFree.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 1 And ddlMercadoria.SelectedValue = 0 Then
            RedQTDContainer.Visible = True
            RedContainer.Visible = True
            ' RedFree.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
            'ElseIf Session("estufagem") = 1 And txtFreeTimeMercadoria.Text = "" Then
            '    RedQTDContainer.Visible = True
            '    RedContainer.Visible = True
            '    RedFree.Visible = True

            '    lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            '    divErroMercadoria.Visible = True
            '    mpeNovoMercadoria.Show()

        ElseIf Session("estufagem") = 2 And ddlMercadoria.SelectedValue = 0 Then
            RedQTDMercadoria.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 2 And txtQtdMercadoria.Text = "" Then
            RedQTDMercadoria.Visible = True
            RedPesoBruto.Visible = True
            RedM3.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 2 And txtPesoBrutoMercadoria.Text = "" Then
            RedQTDMercadoria.Visible = True
            RedPesoBruto.Visible = True
            RedM3.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 2 And txtM3Mercadoria.Text = "" Then
            RedQTDMercadoria.Visible = True
            RedPesoBruto.Visible = True
            RedM3.Visible = True
            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()

        ElseIf Session("transporte") = 2 And txtPesoBrutoMercadoria.Text = "" Then
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

            txtFreteVendaMercadoriaUnitario.Text = txtFreteVendaMercadoriaUnitario.Text.Replace(".", "")
            txtFreteVendaMercadoriaUnitario.Text = txtFreteVendaMercadoriaUnitario.Text.Replace(",", ".")

            txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaCalc.Text.Replace(".", "")
            txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaCalc.Text.Replace(",", ".")

            txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaCalc.Text.Replace(".", "")
            txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaCalc.Text.Replace(",", ".")

            txtPesoBrutoMercadoria.Text = txtPesoBrutoMercadoria.Text.Replace(".", "")
            txtPesoBrutoMercadoria.Text = txtPesoBrutoMercadoria.Text.Replace(",", ".")

            txtM3Mercadoria.Text = txtM3Mercadoria.Text.Replace(".", "")
            txtM3Mercadoria.Text = txtM3Mercadoria.Text.Replace(",", ".")

            txtComprimentoMercadoria.Text = txtComprimentoMercadoria.Text.Replace(".", "")
            txtComprimentoMercadoria.Text = txtComprimentoMercadoria.Text.Replace(",", ".")

            txtLarguraMercadoria.Text = txtLarguraMercadoria.Text.Replace(".", "")
            txtLarguraMercadoria.Text = txtLarguraMercadoria.Text.Replace(",", ".")

            txtAlturaMercadoria.Text = txtAlturaMercadoria.Text.Replace(".", "")
            txtAlturaMercadoria.Text = txtAlturaMercadoria.Text.Replace(",", ".")

            txtValorCargaMercadoria.Text = txtValorCargaMercadoria.Text.Replace(".", "")
            txtValorCargaMercadoria.Text = txtValorCargaMercadoria.Text.Replace(",", ".")

            txtFreteVendaMinima.Text = txtFreteVendaMinima.Text.Replace(".", "")
            txtFreteVendaMinima.Text = txtFreteVendaMinima.Text.Replace(",", ".")

            txtFreteCompraMinima.Text = txtFreteCompraMinima.Text.Replace(".", "")
            txtFreteCompraMinima.Text = txtFreteCompraMinima.Text.Replace(",", ".")

            If txtFreeTimeMercadoria.Text = "" Then
                txtFreeTimeMercadoria.Text = "0"
            End If

            If txtQtdMercadoria.Text = "" Then
                txtQtdMercadoria.Text = "0"
            End If

            If txtQtdContainerMercadoria.Text = "" Then
                txtQtdContainerMercadoria.Text = "0"
            End If

            If txtFreteCompraMercadoriaCalc.Text = "" Then
                txtFreteCompraMercadoriaCalc.Text = "0"
            End If

            If txtFreteVendaMercadoriaUnitario.Text = "" Then
                txtFreteVendaMercadoriaUnitario.Text = "0"
            End If
            If txtFreteVendaMercadoriaCalc.Text = "" Then
                txtFreteVendaMercadoriaCalc.Text = "0"
            End If

            If txtFreteCompraMercadoriaUnitario.Text = "" Then
                txtFreteCompraMercadoriaUnitario.Text = "0"
            End If

            If txtPesoBrutoMercadoria.Text = "" Then
                txtPesoBrutoMercadoria.Text = "0"
            End If

            If txtM3Mercadoria.Text = "" Then
                txtM3Mercadoria.Text = "0"
            End If

            If txtComprimentoMercadoria.Text = "" Then
                txtComprimentoMercadoria.Text = "0"
            End If

            If txtAlturaMercadoria.Text = "" Then
                txtAlturaMercadoria.Text = "0"
            End If

            If txtLarguraMercadoria.Text = "" Then
                txtLarguraMercadoria.Text = "0"
            End If

            If txtValorCargaMercadoria.Text = "" Then
                txtValorCargaMercadoria.Text = "0"
            End If

            If txtDsMercadoria.Text = "" Then
                txtDsMercadoria.Text = "NULL"
            Else
                txtDsMercadoria.Text = "'" & txtDsMercadoria.Text & "'"
            End If

            If txtOutrasOBS_Mercadoria.Text = "" Then
                txtOutrasOBS_Mercadoria.Text = "NULL"
            Else
                txtOutrasOBS_Mercadoria.Text = "'" & txtOutrasOBS_Mercadoria.Text & "'"
            End If

            If txtOBS_Endereco.Text = "" Then
                txtOBS_Endereco.Text = "NULL"
            Else
                txtOBS_Endereco.Text = "'" & txtOBS_Endereco.Text & "'"
            End If

            If txtFreteCompraMinima.Text = "" Then
                txtFreteCompraMinima.Text = "0"
            End If

            If txtFreteVendaMinima.Text = "" Then
                txtFreteVendaMinima.Text = "0"
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
                    Con.ExecutarQuery("INSERT INTO TB_COTACAO_MERCADORIA ( ID_COTACAO,
ID_MERCADORIA,ID_TIPO_CONTAINER,QT_CONTAINER,VL_FRETE_COMPRA,VL_FRETE_VENDA,VL_PESO_BRUTO,VL_M3,DS_MERCADORIA,VL_COMPRIMENTO,VL_LARGURA,VL_ALTURA,VL_CARGA,QT_DIAS_FREETIME,QT_MERCADORIA,VL_FRETE_COMPRA_MIN,VL_FRETE_VENDA_MIN,OBS_ENDERECO,VL_FRETE_COMPRA_UNITARIO,VL_FRETE_VENDA_UNITARIO,OUTRAS_OBS) VALUES (" & txtID.Text & "," & ddlMercadoria.SelectedValue & " ," & ddlTipoContainerMercadoria.SelectedValue & "," & txtQtdContainerMercadoria.Text & "," & txtFreteCompraMercadoriaCalc.Text & "," & txtFreteVendaMercadoriaCalc.Text & "," & txtPesoBrutoMercadoria.Text & "," & txtM3Mercadoria.Text & ", " & txtDsMercadoria.Text & " ," & txtComprimentoMercadoria.Text & ", " & txtLarguraMercadoria.Text & "," & txtAlturaMercadoria.Text & ", " & txtValorCargaMercadoria.Text & "," & txtFreeTimeMercadoria.Text & "," & txtQtdMercadoria.Text & "," & txtFreteCompraMinima.Text & "," & txtFreteVendaMinima.Text & ", " & txtOBS_Endereco.Text & "," & txtFreteCompraMercadoriaUnitario.Text & "," & txtFreteVendaMercadoriaUnitario.Text & "," & txtOutrasOBS_Mercadoria.Text & ")")


                    ddlMercadoria.SelectedValue = 0
                    ddlTipoContainerMercadoria.SelectedValue = 0
                    txtQtdContainerMercadoria.Text = ""
                    txtFreteCompraMercadoriaCalc.Text = ""
                    txtFreteVendaMercadoriaCalc.Text = ""
                    txtPesoBrutoMercadoria.Text = ""
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

                    Con.Fechar()
                    dgvMercadoria.DataBind()
                    divSuccessMercadoria.Visible = True
                    mpeNovoMercadoria.Show()
                    AtualizaFreteMercadoria()



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
ID_MERCADORIA = " & ddlMercadoria.SelectedValue & ", ID_TIPO_CONTAINER = " & ddlTipoContainerMercadoria.SelectedValue & ",QT_CONTAINER = " & txtQtdContainerMercadoria.Text & ",VL_FRETE_COMPRA =  " & txtFreteCompraMercadoriaCalc.Text & ",VL_FRETE_VENDA = " & txtFreteVendaMercadoriaCalc.Text & ",VL_PESO_BRUTO = " & txtPesoBrutoMercadoria.Text & ",VL_M3 = " & txtM3Mercadoria.Text & ",DS_MERCADORIA = " & txtDsMercadoria.Text & ",VL_COMPRIMENTO = " & txtComprimentoMercadoria.Text & ",VL_LARGURA = " & txtLarguraMercadoria.Text & ",VL_ALTURA = " & txtAlturaMercadoria.Text & ",VL_CARGA = " & txtValorCargaMercadoria.Text & " ,QT_DIAS_FREETIME = " & txtFreeTimeMercadoria.Text & " ,VL_FRETE_COMPRA_MIN = " & txtFreteCompraMinima.Text & ",VL_FRETE_VENDA_MIN = " & txtFreteVendaMinima.Text & ",OBS_ENDERECO = " & txtOBS_Endereco.Text & ",OUTRAS_OBS = " & txtOutrasOBS_Mercadoria.Text & ",VL_FRETE_COMPRA_UNITARIO = " & txtFreteCompraMercadoriaUnitario.Text & ",VL_FRETE_VENDA_UNITARIO = " & txtFreteVendaMercadoriaUnitario.Text & ", QT_MERCADORIA = " & txtQtdMercadoria.Text & " WHERE ID_COTACAO_MERCADORIA = " & txtIDMercadoria.Text)


                    txtDsMercadoria.Text = txtDsMercadoria.Text.Replace("NULL", "")
                    txtDsMercadoria.Text = txtDsMercadoria.Text.Replace("'", "")

                    txtOBS_Endereco.Text = txtOBS_Endereco.Text.Replace("NULL", "")
                    txtOBS_Endereco.Text = txtOBS_Endereco.Text.Replace("'", "")

                    txtOutrasOBS_Mercadoria.Text = txtOutrasOBS_Mercadoria.Text.Replace("NULL", "")
                    txtOutrasOBS_Mercadoria.Text = txtOutrasOBS_Mercadoria.Text.Replace("'", "")

                    txtFreteCompraMercadoriaUnitario.Text = txtFreteCompraMercadoriaUnitario.Text.Replace(".", ",")
                    txtFreteCompraMinima.Text = txtFreteCompraMinima.Text.Replace(".", ",")
                    txtFreteCompraMercadoriaCalc.Text = txtFreteCompraMercadoriaCalc.Text.Replace(".", ",")

                    txtFreteVendaMercadoriaUnitario.Text = txtFreteVendaMercadoriaUnitario.Text.Replace(".", ",")
                    txtFreteVendaMinima.Text = txtFreteVendaMinima.Text.Replace(".", ",")
                    txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaCalc.Text.Replace(".", ",")

                    txtValorCargaMercadoria.Text = txtValorCargaMercadoria.Text.Replace(".", ",")
                    txtM3Mercadoria.Text = txtM3Mercadoria.Text.Replace(".", ",")
                    txtPesoBrutoMercadoria.Text = txtPesoBrutoMercadoria.Text.Replace(".", ",")

                    txtComprimentoMercadoria.Text = txtComprimentoMercadoria.Text.Replace(".", ",")
                    txtLarguraMercadoria.Text = txtLarguraMercadoria.Text.Replace(".", ",")
                    txtAlturaMercadoria.Text = txtAlturaMercadoria.Text.Replace(".", ",")

                    divSuccessMercadoria.Visible = True
                    Con.Fechar()
                    dgvMercadoria.DataBind()
                    mpeNovoMercadoria.Show()

                    AtualizaFreteMercadoria()


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


        If txtID.Text = "" Then

            lblErroTaxa.Text = "Antes de inserir Taxa é necessario cadastrar as Informações Basicas"
            divErroTaxa.Visible = True

        ElseIf ddlEstufagem.SelectedValue = 1 And (ddlItemDespesaTaxa.SelectedValue = 0 Or ddlOrigemPagamentoTaxa.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0 Or ddlMoedaCompraTaxa.SelectedValue = 0 Or txtValorTaxaCompra.Text = "" Or ddlMoedaVendaTaxa.SelectedValue = 0 Or txtValorTaxaVenda.Text = "" Or ddlTipoPagamentoTaxa.SelectedValue = 0 Or ddlDestinatarioCobrancaTaxa.SelectedValue = 0) Then

            lblErroTaxa.Text = "Preencha todos os campos obrigatórios"
            divErroTaxa.Visible = True

        ElseIf ddlEstufagem.SelectedValue = 2 And (ddlItemDespesaTaxa.SelectedValue = 0 Or ddlOrigemPagamentoTaxa.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0 Or ddlMoedaVendaTaxa.SelectedValue = 0 Or txtValorTaxaVenda.Text = "" Or ddlTipoPagamentoTaxa.SelectedValue = 0 Or ddlDestinatarioCobrancaTaxa.SelectedValue = 0) Then

            lblErroTaxa.Text = "Preencha todos os campos obrigatórios"
            divErroTaxa.Visible = True


        Else


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



            If txtValorTaxaCompraMin.Text = "" Then
                txtValorTaxaCompraMin.Text = "0"
            End If

            If txtValorTaxaCompra.Text = "" Then
                txtValorTaxaCompra.Text = "0"
            End If

            If txtValorTaxaVendaMin.Text = "" Then
                txtValorTaxaVendaMin.Text = "0"
            End If

            If txtObsTaxa.Text = "" Then
                txtObsTaxa.Text = "NULL"
            Else
                txtObsTaxa.Text = "'" & txtObsTaxa.Text & "'"
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
                    Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (
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
OB_TAXAS) VALUES (" & txtID.Text & "," & ddlFornecedor.SelectedValue & "," & ddlItemDespesaTaxa.SelectedValue & "," & ddlTipoPagamentoTaxa.SelectedValue & "," & ddlOrigemPagamentoTaxa.SelectedValue & ",'" & ckbDeclaradoTaxa.Checked & "','" & ckbProfitTaxa.Checked & "'," & ddlDestinatarioCobrancaTaxa.SelectedValue & "," & ddlBaseCalculoTaxa.SelectedValue & "," & ddlMoedaCompraTaxa.SelectedValue & "," & OPERADOR & txtValorTaxaCompra.Text & "," & OPERADOR & txtValorTaxaCompraMin.Text & "," & ddlMoedaVendaTaxa.SelectedValue & "," & OPERADOR & txtValorTaxaVenda.Text & "," & OPERADOR & txtValorTaxaVendaMin.Text & "," & txtObsTaxa.Text & ")")

                    'Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET FL_TAXA_TRANSPORTADOR = 1 WHERE ID_FORNECEDOR = (SELECT ID_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " )  AND ID_COTACAO = " & txtID.Text)
                    'Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET FL_TAXA_TRANSPORTADOR = 0 WHERE ID_FORNECEDOR <> (SELECT ID_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " )  AND ID_COTACAO = " & txtID.Text)

                    txtIDTaxa.Text = ""
                    ddlItemDespesaTaxa.SelectedValue = 0
                    ddlOrigemPagamentoTaxa.SelectedValue = 0
                    ddlBaseCalculoTaxa.SelectedValue = 0
                    ddlMoedaCompraTaxa.SelectedValue = 0
                    ddlMoedaVendaTaxa.SelectedValue = 0
                    ddlTipoPagamentoTaxa.SelectedValue = 1
                    ddlDestinatarioCobrancaTaxa.SelectedValue = 1
                    txtValorTaxaCompra.Text = ""
                    txtValorTaxaVenda.Text = ""
                    txtValorTaxaVendaMin.Text = ""
                    txtValorTaxaCompraMin.Text = ""
                    txtValorTaxaVendaCalc.Text = ""
                    txtValorTaxaCompraCalc.Text = ""
                    ckbDeclaradoTaxa.Checked = False

                    txtObsTaxa.Text = txtObsTaxa.Text.Replace("NULL", "")
                    txtObsTaxa.Text = txtObsTaxa.Text.Replace("'", "")

                    Con.Fechar()
                    dgvTaxas.DataBind()
                    divSuccessTaxa.Visible = True

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
OB_TAXAS = " & txtObsTaxa.Text & " 
WHERE ID_COTACAO_TAXA = " & txtIDTaxa.Text)

                    txtValorTaxaCompra.Text = txtValorTaxaCompra.Text.Replace(".", ",")
                    txtValorTaxaCompraMin.Text = txtValorTaxaCompraMin.Text.Replace(".", ",")
                    txtValorTaxaVenda.Text = txtValorTaxaVenda.Text.Replace(".", ",")
                    txtValorTaxaVendaMin.Text = txtValorTaxaVendaMin.Text.Replace(".", ",")

                    txtObsTaxa.Text = txtObsTaxa.Text.Replace("NULL", "")
                    txtObsTaxa.Text = txtObsTaxa.Text.Replace("'", "")

                    divSuccessTaxa.Visible = True
                    Con.Fechar()
                    dgvTaxas.DataBind()

                End If


            End If


        End If

        mpeNovoTaxa.Show()


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
        If ddlDestinoFrete.SelectedValue <> 0 And ddlOrigemFrete.SelectedValue <> 0 And ddlTransportadorFrete.SelectedValue <> 0 Then

            Dim sql As String = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE convert(date,DT_VALIDADE_FINAL,103) >= convert(date, getdate(),103) AND ID_PORTO_ORIGEM = " & ddlOrigemFrete.SelectedValue & " AND ID_PORTO_DESTINO = " & ddlDestinoFrete.SelectedValue & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & " union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "
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
                ' txtFreteCompra.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")
                '  txtFreteVenda.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")
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
            ' Response.Redirect("CotacaoPDF_PT.aspx?c=" & txtID.Text)
            url = "GeraPDF.aspx?c=" & ID & "&l=p&f=i"
            Response.Write("<script>")
            Response.Write("window.open('" & url & "','_blank')")
            Response.Write("</script>")
        End If
    End Sub

    Private Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCliente.SelectedIndexChanged
        If ddlCliente.SelectedValue <> 0 Then
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim sql As String = "SELECT ID_CONTATO, NM_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue & " union SELECT  0, 'Selecione' ORDER BY ID_CONTATO"
            Dim ds As DataSet = Con.ExecutarQuery(sql)
            If ds.Tables(0).Rows.Count > 1 Then
                dsContato.SelectCommand = sql
                ddlContato.DataBind()
                sql = "SELECT ID_CONTATO FROM [dbo].[TB_CONTATO] WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue
                ds = Con.ExecutarQuery(sql)
                ddlContato.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CONTATO")
            Else
                ddlContato.SelectedValue = 0
            End If

            sql = "SELECT ID_CLIENTE_FINAL,NM_CLIENTE_FINAL FROM TB_CLIENTE_FINAL WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue & "
union SELECT  0, '  Selecione' ORDER BY NM_CLIENTE_FINAL"
            ds = Con.ExecutarQuery(sql)
            If ds.Tables(0).Rows.Count = 1 Then
                dsClienteFinal.SelectCommand = sql
                ddlClienteFinal.DataBind()
                divClienteFinal.Attributes.CssStyle.Add("display", "none")

            ElseIf ds.Tables(0).Rows.Count > 1 Then
                dsClienteFinal.SelectCommand = sql
                ddlClienteFinal.DataBind()
                divClienteFinal.Attributes.CssStyle.Add("display", "block")

            Else

                divClienteFinal.Attributes.CssStyle.Add("display", "block")
                ddlClienteFinal.DataBind()

                sql = "SELECT ID_CLIENTE_FINAL FROM [dbo].[TB_CLIENTE_FINAL] WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue
                ds = Con.ExecutarQuery(sql)
                ddlClienteFinal.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL")
            End If


            sql = "SELECT ID_VENDEDOR FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue
            Dim ds1 As DataSet = Con.ExecutarQuery(sql)

            sql = "SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE FL_VENDEDOR = 1 AND ID_PARCEIRO = " & ds1.Tables(0).Rows(0).Item("ID_VENDEDOR") & " union SELECT  0, 'Selecione' ORDER BY ID_PARCEIRO"
            ds = Con.ExecutarQuery(sql)
            If ds.Tables(0).Rows.Count > 1 Then
                dsVendedor.SelectCommand = sql
                ddlVendedor.DataBind()

                ddlVendedor.SelectedValue = ds1.Tables(0).Rows(0).Item("ID_VENDEDOR")
            Else
                ddlVendedor.DataBind()

            End If
            Session("ID_CLIENTE") = ddlCliente.SelectedValue

            GridHistoricoCotacao()
            GridHistoricoFrete()
        Else
            ddlClienteFinal.DataBind()
            ddlContato.DataBind()
            ddlVendedor.DataBind()
        End If


    End Sub
    Sub VerificaEstufagem()
        If Session("estufagem") = 1 Then
            DivFreetime.Attributes.CssStyle.Add("display", "block")
            divQtdMercadoria.Attributes.CssStyle.Add("display", "none")
            RedQTDContainer.Visible = True
            RedContainer.Visible = True
            'RedFree.Visible = True

            RedQTDMercadoria.Visible = False
            RedPesoBruto.Visible = False
            RedM3.Visible = False

            divMinimosFCL.Visible = True
            divCompraMinimaLCL.Visible = False
            divVendaMinimaLCL.Visible = False

        ElseIf Session("estufagem") = 2 Then
            DivFreetime.Attributes.CssStyle.Add("display", "none")
            divQtdMercadoria.Attributes.CssStyle.Add("display", "block")
            RedQTDMercadoria.Visible = True
            RedPesoBruto.Visible = True
            RedM3.Visible = True

            RedQTDContainer.Visible = False
            RedContainer.Visible = False
            RedFree.Visible = False

            divMinimosFCL.Visible = False
            divCompraMinimaLCL.Visible = True
            divVendaMinimaLCL.Visible = True

        Else
            DivFreetime.Attributes.CssStyle.Add("display", "none")
            divQtdMercadoria.Attributes.CssStyle.Add("display", "none")
        End If
    End Sub

    Sub VerificaTransporte()
        If Session("transporte") = 2 Then
            RedPesoBruto.Visible = True
            RedM3.Visible = True
        End If

        If Session("transporte") = 5 Then
            RedPesoBruto.Visible = True
            RedM3.Visible = True
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


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim dsTaxas As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
        If dsTaxas.Tables(0).Rows(0).Item("QTD") = 0 Then
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_CLIENTE,ID_ITEM_DESPESA FROM TB_TAXA_CLIENTE A WHERE ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & " AND ID_PARCEIRO = " & ddlCliente.SelectedValue & FILTROCOMEX & FILTROVIA)
            If ds.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In ds.Tables(0).Rows
                    Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR)
SELECT " & txtID.Text & " , ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_COMPRA)ID_MOEDA_COMPRA,VL_TAXA_COMPRA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_VENDA)ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,1,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,1,getdate(),FL_TAXA_TRANSPORTADOR,
CASE 
WHEN ISNULL(FL_TAXA_TRANSPORTADOR,0) = 1 
then (SELECT ID_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & " )
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then (SELECT ID_PARCEIRO_INDICADOR FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & " )
ELSE 0 END ID_FORNECEDOR 
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
SELECT ID_ITEM_DESPESA, MAX(DT_VALIDADE_INICIAL) AS DT_VALIDADE_INICIAL
FROM TB_TAXA_LOCAL_TRANSPORTADOR
WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROVIA & FILTROCOMEX & "

AND ID_BASE_CALCULO IN (SELECT ID_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA C WHERE C.ID_TIPO_CONTAINER IS NULL OR C.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text & "))


AND CONVERT(DATE,DT_VALIDADE_INICIAL,103) <= (SELECT CONVERT(DATE,DT_VALIDADE_COTACAO,103) FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & ")
GROUP BY  ID_ITEM_DESPESA) B

ON  A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA
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
                            Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,VL_TAXA_COMPRA,ID_MOEDA_COMPRA,ID_BASE_CALCULO_TAXA,FL_TAXA_TRANSPORTADOR,ID_DESTINATARIO_COBRANCA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO)   
SELECT " & txtID.Text & " , ID_ITEM_DESPESA, VL_TAXA_LOCAL_COMPRA, ID_MOEDA,ID_BASE_CALCULO,1,1,1,ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,1,getdate() FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR"))
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
                            Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA_MIN,FL_TAXA_TRANSPORTADOR,ID_DESTINATARIO_COBRANCA,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO)
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

 
 ,VL_TAXA_COMPRA_MIN,1,1, (SELECT ID_TRANSPORTADOR FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR  = A.ID_FRETE_TRANSPORTADOR) ID_TRANSPORTADOR,1,getdate()  FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA"))
                        End If

                    Next


                    divDeleteTaxas.Visible = True
                    lblDeleteTaxas.Text = "Ação realizada com sucesso!"

                End If

            End If
        Else









            ''CASO A TABELA ESTEJA JA TENHA REGISTROS

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_CLIENTE,ID_ITEM_DESPESA FROM TB_TAXA_CLIENTE A WHERE ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & " AND ID_PARCEIRO = " & ddlCliente.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_ITEM_DESPESA NOT IN(SELECT ID_ITEM_DESPESA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text & " AND VL_TAXA_COMPRA = A.VL_TAXA_COMPRA AND VL_TAXA_VENDA = A.VL_TAXA_VENDA)")
            If ds.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In ds.Tables(0).Rows
                    Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR)
SELECT " & txtID.Text & " , ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_COMPRA)ID_MOEDA_COMPRA,VL_TAXA_COMPRA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_VENDA)ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,1,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,1,getdate(),FL_TAXA_TRANSPORTADOR,
CASE 
WHEN ISNULL(FL_TAXA_TRANSPORTADOR,0) = 1 
then (SELECT ID_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & " )
WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ISNULL(FL_PREMIACAO,0) = 1 ) 
then (SELECT ID_PARCEIRO_INDICADOR FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & " )
ELSE 0 END ID_FORNECEDOR 
FROM TB_TAXA_CLIENTE WHERE ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_PARCEIRO = " & ddlCliente.SelectedValue & " AND ID_TAXA_CLIENTE = " & linha.Item("ID_TAXA_CLIENTE"))
                Next

                divDeleteTaxas.Visible = True
                lblDeleteTaxas.Text = "Ação realizada com sucesso!"

            End If

            If ddlFreteTransportador_Frete.SelectedValue = 0 Then


                ds = Con.ExecutarQuery("SELECT A.ID_TAXA_LOCAL_TRANSPORTADOR,A.ID_ITEM_DESPESA,A.ID_ORIGEM_PAGAMENTO,A.ID_BASE_CALCULO
FROM TB_TAXA_LOCAL_TRANSPORTADOR A

INNER JOIN (
SELECT ID_ITEM_DESPESA, MAX(DT_VALIDADE_INICIAL) AS DT_VALIDADE_INICIAL
FROM TB_TAXA_LOCAL_TRANSPORTADOR
WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROVIA & FILTROCOMEX & "

AND ID_BASE_CALCULO IN (SELECT ID_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA C WHERE C.ID_TIPO_CONTAINER IS NULL OR C.ID_TIPO_CONTAINER IN (SELECT ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & txtID.Text & "))


AND CONVERT(DATE,DT_VALIDADE_INICIAL,103) <= (SELECT CONVERT(DATE,DT_VALIDADE_COTACAO,103) FROM TB_COTACAO WHERE ID_COTACAO =  " & txtID.Text & ")
GROUP BY  ID_ITEM_DESPESA) B

ON  A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA
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
                            Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,VL_TAXA_COMPRA,ID_MOEDA_COMPRA,ID_BASE_CALCULO_TAXA,FL_TAXA_TRANSPORTADOR,ID_DESTINATARIO_COBRANCA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO)   
SELECT " & txtID.Text & " , ID_ITEM_DESPESA, VL_TAXA_LOCAL_COMPRA, ID_MOEDA,ID_BASE_CALCULO,1,1,1,ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,1,getdate() FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & FILTROCOMEX & FILTROVIA & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR"))
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
                            Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA_MIN,FL_TAXA_TRANSPORTADOR,ID_DESTINATARIO_COBRANCA,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA,DT_IMPORTACAO)
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

 
 ,VL_TAXA_COMPRA_MIN,1,1, (SELECT ID_TRANSPORTADOR FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR  = A.ID_FRETE_TRANSPORTADOR) ID_TRANSPORTADOR,1,getdate()  FROM TB_TABELA_FRETE_TAXA A WHERE A.ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue & " AND A.ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA"))
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
                    ' Dim valor As Double = ds.Tables(0).Rows(0).Item("VL_COMPRA")
                    If txtQtdContainerMercadoria.Text > 0 Then
                        'Dim qtd As Integer = txtQtdContainerMercadoria.Text
                        'Dim X As Double = valor * qtd
                        'Dim TOTAL As String = X.ToString '("#,###.00")
                        'txtFreteCompraMercadoriaCalc.Text = TOTAL
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
        If txtTTimeFreteInicial.Text <> "" And txtTTimeFreteInicial.Text <> "" Then

            Dim TTInicial As Integer = txtTTimeFreteInicial.Text
            Dim TTFinal As Integer = txtTTimeFreteFinal.Text
            Dim TTMedia As Integer = (TTFinal + TTInicial)
            TTMedia = TTMedia / 2
            txtTTimeFreteMedia.Text = TTMedia

        End If
    End Sub

    Private Sub txtTTimeFreteInicial_TextChanged(sender As Object, e As EventArgs) Handles txtTTimeFreteInicial.TextChanged
        If txtTTimeFreteInicial.Text <> "" And txtTTimeFreteInicial.Text <> "" Then

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

    Function NumeroProcesso(Optional reaprovamento As Boolean = False) As Boolean
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

        If reaprovamento = True Then

            ds = Con.ExecutarQuery("SELECT ISNULL(NR_PROCESSO_GERADO,'')NR_PROCESSO_GERADO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)
            PROCESSO_FINAL = ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO")
            ds = Con.ExecutarQuery("SELECT ID_BL FROM TB_BL WHERE GRAU='C' AND ID_COTACAO = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                ID_BL_OLD = ds.Tables(0).Rows(0).Item("ID_BL")


                'SALVA OBS DO CLIENTE, DO AGENTE, DO COMERCIAL E DO OPERACIONAL CADASTRADO NA BL ANTIGA PARA O NOVO BL
                ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,'0')HBL,ISNULL(OB_CLIENTE,'')OB_CLIENTE, ISNULL(OB_AGENTE_INTERNACIONAL,'')OB_AGENTE_INTERNACIONAL, ISNULL(OB_COMERCIAL,'')OB_COMERCIAL, ISNULL(OB_OPERACIONAL_INTERNA,'')OB_OPERACIONAL_INTERNA  FROM TB_BL WHERE ID_BL = " & ID_BL_OLD)
                If ds.Tables(0).Rows.Count > 0 Then
                    OB_CLIENTE = ds.Tables(0).Rows(0).Item("OB_CLIENTE")
                    OB_AGENTE_INTERNACIONAL = ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")
                    OB_COMERCIAL = ds.Tables(0).Rows(0).Item("OB_COMERCIAL")
                    OB_OPERACIONAL_INTERNA = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")
                    HBL = ds.Tables(0).Rows(0).Item("HBL")
                End If

                'DELETA BL ANTIGO
                Con.ExecutarQuery("DELETE FROM TB_BL WHERE GRAU = 'C' AND ID_BL = " & ID_BL_OLD)
            End If

        ElseIf reaprovamento = False Then

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

        End If

        txtProcessoCotacao.Text = PROCESSO_FINAL

        ds = Con.ExecutarQuery("SELECT COUNT(ID_COTACAO)QTD FROM TB_BL WHERE GRAU='C' AND DT_CANCELAMENTO IS NULL AND ID_COTACAO = " & txtID.Text)

        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Dim dsBL As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,GRAU,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_PARCEIRO_TRANSPORTADOR,ID_COTACAO,DT_ABERTURA,VL_PROFIT_DIVISAO,ID_PROFIT_DIVISAO,VL_FRETE,ID_MOEDA_FRETE,ID_PARCEIRO_VENDEDOR,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_TIPO_PAGAMENTO,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR,VL_CARGA ) 
SELECT '" & PROCESSO_FINAL & "','C', " & ddlServico.SelectedValue & ",ID_CLIENTE,ID_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,GETDATE(),VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,VL_TOTAL_FRETE_VENDA,ID_MOEDA_FRETE,ID_VENDEDOR,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_TIPO_PAGAMENTO,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR, CASE WHEN ID_PARCEIRO_IMPORTADOR IS NULL THEN ID_CLIENTE WHEN ID_PARCEIRO_IMPORTADOR = 0 THEN ID_CLIENTE ELSE ID_PARCEIRO_IMPORTADOR END ID_PARCEIRO_IMPORTADOR, (SELECT (ISNULL(SUM(VL_CARGA),0))
        FROM TB_COTACAO_MERCADORIA B WHERE A.ID_COTACAO = B.ID_COTACAO ) FROM TB_COTACAO A WHERE A.ID_COTACAO = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_BL ")
            ID_BL = dsBL.Tables(0).Rows(0).Item("ID_BL").ToString()

            'TAXAS COMPRAS
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF) 
SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'P',ID_FORNECEDOR,'COTA' FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA <> 0 AND ID_COTACAO = " & txtID.Text)

            'TAXAS VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF) 
 SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_CALCULADO,VL_TAXA_VENDA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'R',
 
 CASE 
 WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
 THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") 
 
 WHEN ID_DESTINATARIO_COBRANCA = 2
 THEN (SELECT ID_AGENTE_INTERNACIONAL FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ")
 
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
 END ID_DESTINATARIO_COBRANCA,'COTA'

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
                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,FL_TAXA_TRANSPORTADOR,CD_ORIGEM_INF)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA_MIN," & ID_BL & ",'P',ID_TIPO_PAGAMENTO, " & FL_PROFIT_FRETE & ",
 
 ID_TRANSPORTADOR AS ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6
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

            'FRETE VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF)

 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO, " & FL_PROFIT_FRETE & " ,

 CASE WHEN (ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6) AND ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN ID_PARCEIRO_IMPORTADOR
 ELSE ID_CLIENTE
 END ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA ,'COTA'
 
 FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)


            Dim dsCarga As DataSet
            If ddlEstufagem.SelectedValue = 1 Then

                dsCarga = Con.ExecutarQuery("SELECT ID_COTACAO_MERCADORIA,QT_CONTAINER FROM TB_COTACAO_MERCADORIA
 WHERE QT_CONTAINER is not null and QT_CONTAINER <> 0 and ID_COTACAO = " & txtID.Text)
                If dsCarga.Tables(0).Rows.Count > 0 Then
                    Dim QT_CONTAINER As Integer
                    For Each linha As DataRow In dsCarga.Tables(0).Rows
                        QT_CONTAINER = linha.Item("QT_CONTAINER")

                        For i As Integer = 1 To QT_CONTAINER Step 1
                            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL,ID_TIPO_CNTR) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,isnull(VL_PESO_BRUTO,0)/isnull(QT_CONTAINER,0)VL_PESO_BRUTO,isnull(VL_M3,0)/isnull(QT_CONTAINER,0)VL_M3," & ID_BL & ",ID_TIPO_CONTAINER  FROM TB_COTACAO_MERCADORIA
        WHERE ID_COTACAO_MERCADORIA =  " & linha.Item("ID_COTACAO_MERCADORIA"))
                        Next
                    Next
                Else
                    Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,ID_BL) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO," & ID_BL & " FROM TB_COTACAO_MERCADORIA
 WHERE ID_COTACAO =  " & txtID.Text)
                End If


            ElseIf ddlEstufagem.SelectedValue = 2 Then

                Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL) 
SELECT SUM(QT_MERCADORIA)QT_MERCADORIA,SUM(VL_PESO_BRUTO)VL_PESO_BRUTO,SUM(VL_M3)VL_M3," & ID_BL & " FROM TB_COTACAO_MERCADORIA
 WHERE ID_COTACAO =  " & txtID.Text)

            End If


        Else

            Return False

        End If

        If reaprovamento = True Then

            'PASSA OBS DO CLIENTE, DO AGENTE, DO COMERCIAL E DO OPERACIONAL CADASTRADO NA BL ANTIGA PARA O NOVO BL
            If OB_CLIENTE <> "" Then
                Con.ExecutarQuery("UPDATE TB_BL SET OB_CLIENTE = '" & OB_CLIENTE & "' WHERE  ID_BL = " & ID_BL)
            End If
            If OB_AGENTE_INTERNACIONAL <> "" Then
                Con.ExecutarQuery("UPDATE TB_BL SET OB_AGENTE_INTERNACIONAL = '" & OB_AGENTE_INTERNACIONAL & "' WHERE  ID_BL = " & ID_BL)
            End If
            If OB_COMERCIAL <> "" Then
                Con.ExecutarQuery("UPDATE TB_BL SET OB_COMERCIAL = '" & OB_COMERCIAL & "' WHERE ID_BL = " & ID_BL)
            End If
            If OB_OPERACIONAL_INTERNA <> "" Then
                Con.ExecutarQuery("UPDATE TB_BL SET OB_OPERACIONAL_INTERNA = '" & OB_OPERACIONAL_INTERNA & "' WHERE ID_BL = " & ID_BL)
            End If

            'INSERE NR_BL ANTIGO NO REGISTRO NOVO
            Con.ExecutarQuery("UPDATE TB_BL SET NR_BL = '" & HBL & "' WHERE ID_BL = " & ID_BL)

            'DELETA TAXAS ANTIGAS QUE VIERAM DA COTAÇÃO
            Con.ExecutarQuery("DELETE FROM TB_BL_TAXA WHERE ID_BL = " & ID_BL_OLD & " AND CD_ORIGEM_INF = 'COTA' ")

            'ALTERA ID_BL DAS TAXAS ANTIGAS QUE NAO VIERAM DA COTACAO PARA O NOVO O ID_BL
            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL = " & ID_BL & " WHERE ID_BL = " & ID_BL_OLD & " AND CD_ORIGEM_INF = 'OPER' ")

            'ALTERA ID_BL DA REFERENCIA DO CLIENTE PARA O NOVO BL
            Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET ID_BL = " & ID_BL & " WHERE ID_BL = " & ID_BL_OLD)

            If ddlEstufagem.SelectedValue = 1 Then

                'DELETA CARGAS ANTIGAS QUE VIERAM DA COTAÇÃO
                Con.ExecutarQuery("DELETE FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL_OLD & " AND ID_MERCADORIA in (SELECT OLD.ID_MERCADORIA FROM TB_CARGA_BL OLD
INNER JOIN TB_CARGA_BL NEW ON NEW.ID_MERCADORIA = OLD.ID_MERCADORIA 
WHERE OLD.ID_BL = " & ID_BL_OLD & " AND NEW.ID_BL = " & ID_BL & ")")

                'ALTERA CARGAS ANTIGAS QUE NAO VIERAM DA COTACAO PARA O NOVO O ID_BL
                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_BL = " & ID_BL & " WHERE ID_BL = " & ID_BL_OLD)

            ElseIf ddlEstufagem.SelectedValue = 2 Then

                'DELETA CARGAS 
                Con.ExecutarQuery("DELETE FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL_OLD)

            End If

        End If

        Return True

    End Function

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        ImportaTaxas()
        dgvTaxas.DataBind()
    End Sub

    Private Sub txtNomeCliente_TextChanged(sender As Object, e As EventArgs) Handles txtNomeCliente.TextChanged
        diverro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodCliente.Text = "" Then
            txtCodCliente.Text = 0
        End If
        If txtNomeCliente.Text = "" Then
            txtNomeCliente.Text = "NULL"
        End If

        'Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE (FL_EXPORTADOR= 1 OR FL_IMPORTADOR =1 OR FL_AGENTE = 1 OR FL_AGENTE_INTERNACIONAL =1 OR FL_COMISSARIA = 1 OR FL_INDICADOR = 1) and  (NM_RAZAO like '%" & txtNomeCliente.Text & "%' or ID_PARCEIRO =  " & txtCodCliente.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"

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

        ' Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE FL_INDICADOR =  1  and  (NM_RAZAO like '%" & txtNomeIndicador.Text & "%' or ID_PARCEIRO =  " & txtCodIndicador.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"

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

        ' Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE FL_AGENTE_INTERNACIONAL = 1 and (NM_RAZAO like '%" & txtNomeAgente.Text & "%' or ID_PARCEIRO =  " & txtCodAgente.Text & ") union SELECT  0,'',' Selecione' ORDER BY NM_RAZAO"
        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE  FL_AGENTE_INTERNACIONAL =  1  and (NM_RAZAO like '%" & txtNomeAgente.Text & "%' or ID_PARCEIRO =  " & txtCodAgente.Text & ")  
UNION
SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE FL_AGENTE_INTERNACIONAL =  1  and (NM_FANTASIA like '%" & txtNomeAgente.Text & "%' or ID_PARCEIRO =  " & txtCodAgente.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO, NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' as Descricao FROM TB_PARCEIRO WHERE FL_AGENTE_INTERNACIONAL =  1  and (CNPJ like '%" & txtNomeAgente.Text & "%' or ID_PARCEIRO =  " & txtCodAgente.Text & ") 
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
                'Dim valor As String = txtFreteVendaMercadoriaUnitario.Text
                'Dim qtd As Integer = txtQtdContainerMercadoria.Text
                'Dim X As Double = valor * qtd
                'Dim TOTAL As String = X.ToString '("#,###.00")
                txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text * txtQtdContainerMercadoria.Text
            Else
                txtFreteVendaMercadoriaCalc.Text = txtFreteVendaMercadoriaUnitario.Text
            End If

        End If
    End Sub
    Private Sub txtFreteCompraMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtFreteCompraMercadoriaUnitario.TextChanged
        If txtQtdContainerMercadoria.Text <> "" And txtFreteCompraMercadoriaUnitario.Text <> "" Then

            If txtQtdContainerMercadoria.Text > 0 And txtFreteCompraMercadoriaUnitario.Text <> 0 Then
                'Dim valor As String = txtFreteCompraMercadoriaUnitario.Text
                'Dim qtd As Integer = txtQtdContainerMercadoria.Text
                'Dim X As Double = valor * qtd
                'Dim TOTAL As String = X.ToString '("#,###.00")
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
            'Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  ( FL_SHIPPER = 1 ) and (NM_RAZAO like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"


            Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_SHIPPER =  1  and (NM_RAZAO like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ")
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_SHIPPER =  1  and (NM_FANTASIA like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE FL_SHIPPER =  1  and (CNPJ like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ") 
UNION 
SELECT  0,'', ' Selecione' FROM TB_PARCEIRO ORDER BY NM_RAZAO"


        ElseIf ddlServico.SelectedValue = 4 Or ddlServico.SelectedValue = 5 Then
            ' Sql = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO end as Descricao FROM TB_PARCEIRO WHERE  ( FL_EXPORTADOR =1 ) and (NM_RAZAO like '%" & txtNomeExportador.Text & "%' or ID_PARCEIRO =  " & txtCodExportador.Text & ") union SELECT  0,'', ' Selecione' ORDER BY NM_RAZAO"

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
        If ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then
            txtViaTransporte.Text = 4
        Else
            txtViaTransporte.Text = 1

        End If
    End Sub

    Private Sub ddlOrigemFrete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrigemFrete.SelectedIndexChanged
        If ddlDestinoFrete.SelectedValue <> 0 And ddlOrigemFrete.SelectedValue <> 0 And ddlTransportadorFrete.SelectedValue <> 0 Then

            Dim sql As String = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE convert(date,DT_VALIDADE_FINAL,103) >= convert(date, getdate(),103) AND ID_PORTO_ORIGEM = " & ddlOrigemFrete.SelectedValue & " AND ID_PORTO_DESTINO = " & ddlDestinoFrete.SelectedValue & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & " union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "
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

    Private Sub ddlTransportadorFrete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransportadorFrete.SelectedIndexChanged
        If ddlDestinoFrete.SelectedValue <> 0 And ddlOrigemFrete.SelectedValue <> 0 And ddlTransportadorFrete.SelectedValue <> 0 Then

            Dim sql As String = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE convert(date,DT_VALIDADE_FINAL,103) >= convert(date, getdate(),103) AND ID_PORTO_ORIGEM = " & ddlOrigemFrete.SelectedValue & " AND ID_PORTO_DESTINO = " & ddlDestinoFrete.SelectedValue & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & " union SELECT  0, 'Selecione' ORDER BY ID_FRETE_TRANSPORTADOR "
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
                        ' Dim valor As Double = ds.Tables(0).Rows(0).Item("VL_COMPRA")
                        If txtQtdContainerMercadoria.Text > 0 Then
                            'Dim qtd As Integer = txtQtdContainerMercadoria.Text
                            'Dim X As Double = valor * qtd
                            'Dim TOTAL As String = X.ToString '("#,###.00")
                            'txtFreteCompraMercadoriaCalc.Text = TOTAL
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
End Class