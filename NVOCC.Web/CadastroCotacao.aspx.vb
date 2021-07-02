Public Class CadastroCotacao
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            Session("estufagem") = 0
            CarregaCampos()
            VerificaEstufagem()
            VerificaTransporte()

        Else
            If txtID.Text = "" Then
                txtNumeroCotacao.Text = NumeroCotacao()
                txtCotacaoTaxa.Text = txtNumeroCotacao.Text
                txtCotacaoMercadoria.Text = txtNumeroCotacao.Text
                ddlUsuarioStatus.SelectedValue = Session("ID_USUARIO")
                ddlAnalista.SelectedValue = Session("ID_USUARIO")
                txtAbertura.Text = Now.Date.ToString("dd-MM-yyyy")
                btnNovoFrete.Attributes.CssStyle.Add("display", "none")
                ddlStatusCotacao.SelectedValue = 2
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

        ds = Con.ExecutarQuery("SELECT A.ID_COTACAO,ID_FRETE_TRANSPORTADOR,A.NR_COTACAO,ID_PORTO_DESTINO,ID_PORTO_ORIGEM,ID_TRANSPORTADOR,
CONVERT(varchar,A.DT_ABERTURA,103)DT_ABERTURA,
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


            txtAbertura.Text = ds.Tables(0).Rows(0).Item("DT_ABERTURA").ToString()

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

            ddlDestinatarioComercial.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COMERCIAL").ToString()
            ddlAgente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString()
            ddlAnalista.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ANALISTA_COTACAO").ToString()
            ddlIncoterm.SelectedValue = ds.Tables(0).Rows(0).Item("ID_INCOTERM").ToString()
            ddlCliente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CLIENTE").ToString()

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CLIENTE")) And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTATO")) Then

                Dim sql As String = "SELECT ID_CONTATO, NM_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & ds.Tables(0).Rows(0).Item("ID_CLIENTE") & " or ID_CONTATO = " & ds.Tables(0).Rows(0).Item("ID_CONTATO") & "
union SELECT  0, 'Selecione' FROM TB_CONTATO ORDER BY ID_CONTATO"
                Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                If ds1.Tables(0).Rows.Count > 0 Then
                    dsContato.SelectCommand = sql
                    ddlContato.DataBind()
                End If
                Con.Fechar()

                ddlContato.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CONTATO")
            End If

            ddlServico.SelectedValue = ds.Tables(0).Rows(0).Item("ID_SERVICO").ToString()

            ddlVendedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VENDEDOR").ToString

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CALCULO_COTACAO")) Then

                txtDataCalculo.Text = ds.Tables(0).Rows(0).Item("DT_CALCULO_COTACAO").ToString()
            End If

            ddlMotivoCancelamento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOTIVO_CANCELAMENTO").ToString()
            txtObsCancelamento.Text = ds.Tables(0).Rows(0).Item("OB_MOTIVO_CANCELAMENTO").ToString()
            txtObsCliente.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE").ToString()
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
                ddlClienteFinal.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL").ToString()
            End If

            ddlTipoBL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_BL").ToString()
            ddlMercadoria.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString()
            txtCotacaoTaxa.Text = txtNumeroCotacao.Text
            txtCotacaoMercadoria.Text = txtNumeroCotacao.Text

            Dim Linhas As Integer = dgvFrete.Rows.Count
            If Linhas > 0 Then
                btnNovoFrete.Attributes.CssStyle.Add("display", "none")
            End If

            If ds.Tables(0).Rows(0).Item("FL_ENCERRA_COTACAO") = True Then
                ddlStatusCotacao.Enabled = False
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
        txtFreteCompraMercadoria.Text = ""
        txtFreteVendaMercadoria.Text = ""
        txtPesoBrutoMercadoria.Text = ""
        txtM3Mercadoria.Text = ""
        txtDsMercadoria.Text = ""
        txtComprimentoMercadoria.Text = ""
        txtLarguraMercadoria.Text = ""
        txtAlturaMercadoria.Text = ""
        txtValorCargaMercadoria.Text = ""
        txtFreeTimeMercadoria.Text = ""
        txtQtdMercadoria.Text = ""
        txtDsMercadoria.Text = ""
        txtIDMercadoria.Text = ""
        txtFreteVendaMinima.Text = ""
        txtFreteCompraMinima.Text = ""

        mpeNovoMercadoria.Hide()
    End Sub

    Private Sub btnFecharTaxa_Click(sender As Object, e As EventArgs) Handles btnFecharTaxa.Click
        txtIDTaxa.Text = ""
        ddlItemDespesaTaxa.SelectedValue = 0
        ddlOrigemPagamentoTaxa.SelectedValue = 0
        ddlBaseCalculoTaxa.SelectedValue = 0
        ddlMoedaCompraTaxa.SelectedValue = 0
        ddlMoedaVendaTaxa.SelectedValue = 0
        ddlTipoPagamentoTaxa.SelectedValue = 0
        ddlDestinatarioCobrancaTaxa.SelectedValue = 0
        txtValorTaxaCompra.Text = ""
        txtValorTaxaVenda.Text = ""
        txtValorTaxaVendaMin.Text = ""
        txtValorTaxaCompraMin.Text = ""
        divErroTaxa.Visible = False
        divSuccessTaxa.Visible = False
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
SELECT ID_COTACAO,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TIPO_PAGAMENTO,ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN ,VL_TOTAL_FRETE_COMPRA_MIN
FROM  TB_COTACAO WHERE ID_COTACAO = " & ID)
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_BL")) Then
                    ddlTipoBL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_BL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TAXAS_INCLUDED")) Then
                    txtIncludedFrete.Text = ds.Tables(0).Rows(0).Item("NM_TAXAS_INCLUDED")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")) Then

                    Dim sql As String = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE   ID_FRETE_TRANSPORTADOR = " & ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString() & " union SELECT  0, 'Selecione' FROM TB_FRETE_TRANSPORTADOR ORDER BY ID_FRETE_TRANSPORTADOR "

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

            ds = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,
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
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT 
A.ID_COTACAO_MERCADORIA,A.ID_COTACAO,A.ID_MERCADORIA,A.ID_TIPO_CONTAINER,A.QT_CONTAINER,A.VL_FRETE_COMPRA,
A.VL_FRETE_VENDA,A.VL_PESO_BRUTO,A.VL_M3,A.DS_MERCADORIA,A.VL_COMPRIMENTO,A.VL_LARGURA,A.VL_ALTURA,A.VL_CARGA,A.QT_DIAS_FREETIME,A.QT_MERCADORIA,A.VL_FRETE_COMPRA_MIN,A.VL_FRETE_VENDA_MIN,OBS_ENDERECO FROM TB_COTACAO_MERCADORIA A
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FRETE_COMPRA")) Then
                    txtFreteCompraMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_FRETE_COMPRA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FRETE_VENDA")) Then
                    txtFreteVendaMercadoria.Text = ds.Tables(0).Rows(0).Item("VL_FRETE_VENDA")
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")) Then
                    txtFreeTimeMercadoria.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")
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


        ds = Con.ExecutarQuery("SELECT MAX(NR_COTACAO)NR_COTACAO FROM [TB_COTACAO]")


        Dim numero_antecedente As String = ds.Tables(0).Rows(0).Item("NR_COTACAO").Substring(0, 7)

        Dim ano_antecedente As String = ds.Tables(0).Rows(0).Item("NR_COTACAO").Substring(8)

        Dim ano_atual = Now.Year.ToString.Substring(2)

        Dim var_aux As Integer
        Dim NR_COTACAO As String

        If ano_antecedente = ano_atual Then
            var_aux = numero_antecedente + 1
            NR_COTACAO = var_aux
            NR_COTACAO = NR_COTACAO.PadLeft(7, "0") & "/" & ano_atual
        Else
            NR_COTACAO = 1

            NR_COTACAO = NR_COTACAO.PadLeft(7, "0") & "/" & ano_atual
        End If

        Con.Fechar()

        Return NR_COTACAO
    End Function

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        diverro.Visible = False
        divsuccess.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim PROCESSO As String = ""
        Dim v As New VerificaData

        txtAbertura.Text = txtAbertura.Text.Replace("-", "/")

        If txtNumeroCotacao.Text = "" Or txtAbertura.Text = "" Or ddlStatusCotacao.SelectedValue = 0 Or ddlUsuarioStatus.SelectedValue = 0 Or txtValidade.Text = "" Or ddlDestinatarioComercial.SelectedValue = 0 Or ddlAnalista.SelectedValue = 0 Or ddlCliente.SelectedValue = 0 Or ddlAgente.SelectedValue = 0 Or ddlIncoterm.SelectedValue = 0 Or ddlEstufagem.SelectedValue = 0 Or ddlTipoBL.SelectedValue = 0 Or ddlServico.SelectedValue = 0 Or ddlVendedor.SelectedValue = 0 Then
            lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Informações Básicas."
            diverro.Visible = True

        ElseIf v.ValidaData(txtAbertura.Text) = False Then
            diverro.Visible = True
            lblmsgErro.Text = "A data de abertura é inválida."

        Else

            Dim dsContatos As DataSet = Con.ExecutarQuery("SELECT ID_CONTATO, NM_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue & "
union SELECT  0, 'Selecione' FROM TB_CONTATO ORDER BY ID_CONTATO")
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
                    ds = Con.ExecutarQuery("INSERT INTO TB_COTACAO (NR_COTACAO,ID_TIPO_BL, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_TIPO_ESTUFAGEM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, ID_USUARIO_STATUS, DT_VALIDADE_COTACAO) VALUES ('" & txtNumeroCotacao.Text & "'," & ddlTipoBL.SelectedValue & ", getdate(), " & ddlStatusCotacao.SelectedValue & ", Convert(datetime, '" & txtDataStatus.Text & "', 103)," & ddlAnalista.SelectedValue & ", " & ddlAgente.SelectedValue & "," & ddlIncoterm.SelectedValue & "," & ddlEstufagem.SelectedValue & ", " & ddlDestinatarioComercial.SelectedValue & "," & ddlCliente.SelectedValue & "," & ddlClienteFinal.SelectedValue & ", " & ddlContato.SelectedValue & " , " & ddlServico.SelectedValue & " , " & ddlVendedor.SelectedValue & "," & txtObsCliente.Text & "," & txtObsCancelamento.Text & "," & txtObsOperacional.Text & "," & ddlMotivoCancelamento.SelectedValue & "," & ddlUsuarioStatus.SelectedValue & ",Convert(datetime, '" & txtValidade.Text & "', 103) ) Select SCOPE_IDENTITY() as ID_COTACAO ")

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
                            NumeroProcesso()

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
                    Con.ExecutarQuery("UPDATE TB_COTACAO SET ID_STATUS_COTACAO = " & ddlStatusCotacao.SelectedValue & ", DT_VALIDADE_COTACAO = Convert(datetime, '" & txtValidade.Text & "', 103), ID_AGENTE_INTERNACIONAL = " & ddlAgente.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm.SelectedValue & ", ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & ", ID_DESTINATARIO_COMERCIAL = " & ddlDestinatarioComercial.SelectedValue & ", ID_CLIENTE = " & ddlCliente.SelectedValue & ", ID_CLIENTE_FINAL = " & ddlClienteFinal.SelectedValue & ", ID_CONTATO = " & ddlContato.SelectedValue & ", ID_SERVICO = " & ddlServico.SelectedValue & ", ID_VENDEDOR = " & ddlVendedor.SelectedValue & ", OB_CLIENTE = " & txtObsCliente.Text & ", OB_MOTIVO_CANCELAMENTO = " & txtObsCancelamento.Text & ", OB_OPERACIONAL = " & txtObsOperacional.Text & ", ID_MOTIVO_CANCELAMENTO = " & ddlMotivoCancelamento.SelectedValue & ", ID_TIPO_BL = " & ddlTipoBL.SelectedValue & "  WHERE ID_COTACAO = " & txtID.Text)


                    Session("estufagem") = ddlEstufagem.SelectedValue
                    Session("transporte") = ddlServico.SelectedValue
                    Session("ID_CLIENTE") = ddlCliente.SelectedValue

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


        End If
    End Sub

    Private Sub btnSalvarFrete_Click(sender As Object, e As EventArgs) Handles btnSalvarFrete.Click
        divErroFrete.Visible = False
        divSuccessFrete.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        If txtID.Text = "" Then

            lblErroFrete.Text = "Antes de inserir o Frete é necessário cadastrar as Informações Basicas"
            divErroFrete.Visible = True

        ElseIf ddlTransportadorFrete.SelectedValue = 0 Or ddlOrigemFrete.SelectedValue = 0 Or ddlDestinoFrete.SelectedValue = 0 Or txtTTimeFreteInicial.Text = "" Or txtTTimeFreteFinal.Text = "" Or ddlTipoCargaFrete.SelectedValue = 0 Or ddlRotaFrete.SelectedValue = 0 Or ddlFrequenciaFrete.SelectedValue = 0 Or ddlMoedaFrete.SelectedValue = 0 Or ddlEstufagemFrete.SelectedValue = 0 Or ddlTipoPagamento_Frete.SelectedValue = 0 Then
            lblErroFrete.Text = "Preencha todos os campos obrigatórios"
            divErroFrete.Visible = True

        ElseIf ddlEstufagemFrete.SelectedValue = 1 And (ddlDivisaoProfit.SelectedValue = 0 Or txtValorDivisaoProfit.Text = "") Then
            lblErroFrete.Text = "Preencha os campos <strong>Tipo Divisão Profit</strong> e <strong>Valor Divisão Profit</strong>"
            divErroFrete.Visible = True
        Else

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

            If txtValorDivisaoProfit.Text = "" Then
                txtValorDivisaoProfit.Text = "0"
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
ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_Frete.SelectedValue & " 
WHERE ID_COTACAO = " & txtID.Text)

                divSuccessFrete.Visible = True
                Con.Fechar()
                ImportaTaxas()
                dgvTaxas.DataBind()
                dgvFrete.DataBind()
            End If


        End If

        mpeNovoFrete.Show()


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
            RedFree.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 1 And ddlTipoContainerMercadoria.SelectedValue = 0 Then
            RedQTDContainer.Visible = True
            RedContainer.Visible = True
            RedFree.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 1 And ddlMercadoria.SelectedValue = 0 Then
            RedQTDContainer.Visible = True
            RedContainer.Visible = True
            RedFree.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()
        ElseIf Session("estufagem") = 1 And txtFreeTimeMercadoria.Text = "" Then
            RedQTDContainer.Visible = True
            RedContainer.Visible = True
            RedFree.Visible = True

            lblErroMercadoria.Text = "Preencha todos os campos obrigatórios"
            divErroMercadoria.Visible = True
            mpeNovoMercadoria.Show()

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


            txtFreteCompraMercadoria.Text = txtFreteCompraMercadoria.Text.Replace(".", "")
            txtFreteCompraMercadoria.Text = txtFreteCompraMercadoria.Text.Replace(",", ".")

            txtFreteVendaMercadoria.Text = txtFreteVendaMercadoria.Text.Replace(".", "")
            txtFreteVendaMercadoria.Text = txtFreteVendaMercadoria.Text.Replace(",", ".")

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

            If txtFreteCompraMercadoria.Text = "" Then
                txtFreteCompraMercadoria.Text = "0"
            End If

            If txtFreteVendaMercadoria.Text = "" Then
                txtFreteVendaMercadoria.Text = "0"
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


            If txtIDMercadoria.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroMercadoria.Visible = True
                    lblErroMercadoria.Text = "Usuário não possui permissão para cadastrar."
                    mpeNovoMercadoria.Show()

                Else

                    'INSERE MERCADORIA
                    Con.ExecutarQuery("INSERT INTO TB_COTACAO_MERCADORIA ( ID_COTACAO,
ID_MERCADORIA,ID_TIPO_CONTAINER,QT_CONTAINER,VL_FRETE_COMPRA,VL_FRETE_VENDA,VL_PESO_BRUTO,VL_M3,DS_MERCADORIA,VL_COMPRIMENTO,VL_LARGURA,VL_ALTURA,VL_CARGA,QT_DIAS_FREETIME,QT_MERCADORIA,VL_FRETE_COMPRA_MIN,VL_FRETE_VENDA_MIN,OBS_ENDERECO) VALUES (" & txtID.Text & "," & ddlMercadoria.SelectedValue & " ," & ddlTipoContainerMercadoria.SelectedValue & "," & txtQtdContainerMercadoria.Text & "," & txtFreteCompraMercadoria.Text & "," & txtFreteVendaMercadoria.Text & "," & txtPesoBrutoMercadoria.Text & "," & txtM3Mercadoria.Text & ", " & txtDsMercadoria.Text & " ," & txtComprimentoMercadoria.Text & ", " & txtLarguraMercadoria.Text & "," & txtAlturaMercadoria.Text & ", " & txtValorCargaMercadoria.Text & "," & txtFreeTimeMercadoria.Text & "," & txtQtdMercadoria.Text & "," & txtFreteCompraMinima.Text & "," & txtFreteVendaMinima.Text & ", " & txtOBS_Endereco.Text & ")")


                    ddlMercadoria.SelectedValue = 0
                    ddlTipoContainerMercadoria.SelectedValue = 0
                    txtQtdContainerMercadoria.Text = ""
                    txtFreteCompraMercadoria.Text = ""
                    txtFreteVendaMercadoria.Text = ""
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

                    Con.Fechar()
                    dgvMercadoria.DataBind()
                    divSuccessMercadoria.Visible = True
                    mpeNovoMercadoria.Show()


                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_COMPRA =
(SELECT SUM(ISNULL(VL_FRETE_COMPRA,0))VL_FRETE_COMPRA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_VENDA =
(SELECT SUM(ISNULL(VL_FRETE_VENDA,0))VL_FRETE_VENDA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ") WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_PESO_BRUTO=
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_COMPRA_MIN =
(SELECT SUM(ISNULL(VL_FRETE_COMPRA_MIN,0))VL_FRETE_COMPRA_MIN FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_VENDA_MIN =
(SELECT SUM(ISNULL(VL_FRETE_VENDA_MIN,0))VL_FRETE_VENDA_MIN FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)


                End If



            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroMercadoria.Visible = True
                    lblErroMercadoria.Text = "Usuário não possui permissão para alterar."
                    mpeNovoMercadoria.Show()

                Else


                    'ALTERA MERCADORIA
                    Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET ID_COTACAO = " & txtID.Text & ",
ID_MERCADORIA = " & ddlMercadoria.SelectedValue & ", ID_TIPO_CONTAINER = " & ddlTipoContainerMercadoria.SelectedValue & ",QT_CONTAINER = " & txtQtdContainerMercadoria.Text & ",VL_FRETE_COMPRA =  " & txtFreteCompraMercadoria.Text & ",VL_FRETE_VENDA = " & txtFreteVendaMercadoria.Text & ",VL_PESO_BRUTO = " & txtPesoBrutoMercadoria.Text & ",VL_M3 = " & txtM3Mercadoria.Text & ",DS_MERCADORIA = " & txtDsMercadoria.Text & ",VL_COMPRIMENTO = " & txtComprimentoMercadoria.Text & ",VL_LARGURA = " & txtLarguraMercadoria.Text & ",VL_ALTURA = " & txtAlturaMercadoria.Text & ",VL_CARGA = " & txtValorCargaMercadoria.Text & " ,QT_DIAS_FREETIME = " & txtFreeTimeMercadoria.Text & " ,VL_FRETE_COMPRA_MIN = " & txtFreteCompraMinima.Text & ",VL_FRETE_VENDA_MIN = " & txtFreteVendaMinima.Text & ",OBS_ENDERECO = " & txtOBS_Endereco.Text & " WHERE ID_COTACAO_MERCADORIA = " & txtIDMercadoria.Text)

                    ddlMercadoria.SelectedValue = 0
                    ddlTipoContainerMercadoria.SelectedValue = 0
                    txtQtdContainerMercadoria.Text = ""
                    txtFreteCompraMercadoria.Text = ""
                    txtFreteVendaMercadoria.Text = ""
                    txtPesoBrutoMercadoria.Text = ""
                    txtM3Mercadoria.Text = ""
                    txtDsMercadoria.Text = ""
                    txtComprimentoMercadoria.Text = ""
                    txtLarguraMercadoria.Text = ""
                    txtAlturaMercadoria.Text = ""
                    txtValorCargaMercadoria.Text = ""
                    txtFreeTimeMercadoria.Text = ""
                    txtQtdMercadoria.Text = ""
                    txtDsMercadoria.Text = ""
                    txtFreteCompraMinima.Text = ""
                    txtFreteVendaMinima.Text = ""
                    txtOBS_Endereco.Text = ""

                    divSuccessMercadoria.Visible = True
                    Con.Fechar()
                    dgvMercadoria.DataBind()
                    mpeNovoMercadoria.Show()


                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_COMPRA =
(SELECT SUM(ISNULL(VL_FRETE_COMPRA,0))VL_FRETE_COMPRA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_VENDA =
(SELECT SUM(ISNULL(VL_FRETE_VENDA,0))VL_FRETE_VENDA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ") WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_PESO_BRUTO=
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_COMPRA_MIN =
(SELECT SUM(ISNULL(VL_FRETE_COMPRA_MIN,0))VL_FRETE_COMPRA_MIN FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_FRETE_VENDA_MIN =
(SELECT SUM(ISNULL(VL_FRETE_VENDA_MIN,0))VL_FRETE_VENDA_MIN FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & txtID.Text & ")WHERE ID_COTACAO =  " & txtID.Text)

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


            If txtValorTaxaCompraMin.Text = "" Then
                txtValorTaxaCompraMin.Text = "0"
            End If

            If txtValorTaxaCompra.Text = "" Then
                txtValorTaxaCompra.Text = "0"
            End If

            If txtValorTaxaVendaMin.Text = "" Then
                txtValorTaxaVendaMin.Text = "0"
            End If

            If txtIDTaxa.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroTaxa.Visible = True
                    lblErroTaxa.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else



                    'INSERE TAXAS
                    ds = Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA ( 
ID_COTACAO,
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
OB_TAXAS) VALUES (" & txtID.Text & "," & ddlItemDespesaTaxa.SelectedValue & "," & ddlTipoPagamentoTaxa.SelectedValue & "," & ddlOrigemPagamentoTaxa.SelectedValue & ",'" & ckbDeclaradoTaxa.Checked & "','" & ckbProfitTaxa.Checked & "'," & ddlDestinatarioCobrancaTaxa.SelectedValue & "," & ddlBaseCalculoTaxa.SelectedValue & "," & ddlMoedaCompraTaxa.SelectedValue & ",'" & txtValorTaxaCompra.Text & "','" & txtValorTaxaCompraMin.Text & "'," & ddlMoedaVendaTaxa.SelectedValue & ",'" & txtValorTaxaVenda.Text & "','" & txtValorTaxaVendaMin.Text & "','" & txtObsTaxa.Text & "')")


                    txtIDTaxa.Text = ""
                    ddlItemDespesaTaxa.SelectedValue = 0
                    ddlOrigemPagamentoTaxa.SelectedValue = 0
                    ddlBaseCalculoTaxa.SelectedValue = 0
                    ddlMoedaCompraTaxa.SelectedValue = 0
                    ddlMoedaVendaTaxa.SelectedValue = 0
                    ddlTipoPagamentoTaxa.SelectedValue = 0
                    ddlDestinatarioCobrancaTaxa.SelectedValue = 0
                    txtValorTaxaCompra.Text = ""
                    txtValorTaxaVenda.Text = ""
                    txtValorTaxaVendaMin.Text = ""
                    txtValorTaxaCompraMin.Text = ""

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


                    'ALTERA TAXAS
                    ds = Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET 
ID_ITEM_DESPESA = " & ddlItemDespesaTaxa.SelectedValue & ",
ID_TIPO_PAGAMENTO = " & ddlTipoPagamentoTaxa.SelectedValue & " ,
ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamentoTaxa.SelectedValue & ",
FL_DECLARADO = '" & ckbDeclaradoTaxa.Checked & "',
FL_DIVISAO_PROFIT = '" & ckbProfitTaxa.Checked & "',
ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCobrancaTaxa.SelectedValue & ",
ID_BASE_CALCULO_TAXA = " & ddlBaseCalculoTaxa.SelectedValue & ",
ID_MOEDA_COMPRA = " & ddlMoedaCompraTaxa.SelectedValue & ",
VL_TAXA_COMPRA = '" & txtValorTaxaCompra.Text & "',
VL_TAXA_COMPRA_MIN = '" & txtValorTaxaCompraMin.Text & "',
ID_MOEDA_VENDA = " & ddlMoedaVendaTaxa.SelectedValue & ",
VL_TAXA_VENDA = '" & txtValorTaxaVenda.Text & "',
VL_TAXA_VENDA_MIN = '" & txtValorTaxaVendaMin.Text & "',
OB_TAXAS = '" & txtObsTaxa.Text & "'
WHERE ID_COTACAO_TAXA = " & txtIDTaxa.Text)

                    txtIDTaxa.Text = ""
                    ddlItemDespesaTaxa.SelectedValue = 0
                    ddlOrigemPagamentoTaxa.SelectedValue = 0
                    ddlBaseCalculoTaxa.SelectedValue = 0
                    ddlMoedaCompraTaxa.SelectedValue = 0
                    ddlMoedaVendaTaxa.SelectedValue = 0
                    ddlTipoPagamentoTaxa.SelectedValue = 0
                    ddlDestinatarioCobrancaTaxa.SelectedValue = 0
                    txtValorTaxaCompra.Text = ""
                    txtValorTaxaVenda.Text = ""
                    txtValorTaxaVendaMin.Text = ""
                    txtValorTaxaCompraMin.Text = ""
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
        Dim sql As String = "SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR As varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE convert(date,DT_VALIDADE_FINAL,103) >= convert(date, getdate(),103) AND ID_PORTO_ORIGEM = " & ddlOrigemFrete.SelectedValue & " AND ID_PORTO_DESTINO = " & ddlDestinoFrete.SelectedValue & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & " union SELECT  0, 'Selecione' FROM TB_FRETE_TRANSPORTADOR ORDER BY ID_FRETE_TRANSPORTADOR "
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsFreteTransportador.SelectCommand = sql
            ddlFreteTransportador_Frete.DataBind()
        End If
        Con.Fechar()
    End Sub

    Private Sub ddlFreteTransportador_Frete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFreteTransportador_Frete.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT QT_DIAS_TRANSITTIME_INICIAL, QT_DIAS_TRANSITTIME_FINAL,QT_DIAS_TRANSITTIME_MEDIA, ID_TIPO_FREQUENCIA,ID_TIPO_CARGA,ID_VIA_ROTA,VL_FREQUENCIA,ID_MOEDA_FRETE,NM_TAXAS_INCLUDED,ID_PORTO_ESCALA,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,(select Sum(B.VL_COMPRA) from TB_TARIFARIO_FRETE_TRANSPORTADOR b where A.ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR )VL_COMPRA  FROM TB_FRETE_TRANSPORTADOR A WHERE A.ID_FRETE_TRANSPORTADOR = " & ddlFreteTransportador_Frete.SelectedValue)
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
                txtFreteCompra.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")
                txtFreteVenda.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")

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
            url = "GeraPDF.aspx?c=" & ID & "&l=p"
            Response.Write("<script>")
            Response.Write("window.open('" & url & "','_blank')")
            Response.Write("</script>")
        End If
    End Sub

    Private Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCliente.SelectedIndexChanged
        If ddlCliente.SelectedValue <> 0 Then
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim sql As String = "SELECT ID_CONTATO, NM_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue & " union SELECT  0, 'Selecione' FROM TB_CONTATO ORDER BY ID_CONTATO"
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
union SELECT  0, 'Selecione' FROM TB_CLIENTE_FINAL ORDER BY ID_CLIENTE_FINAL"
            ds = Con.ExecutarQuery(sql)
            If ds.Tables(0).Rows.Count = 1 Then
                dsClienteFinal.SelectCommand = sql
                ddlClienteFinal.DataBind()
                divClienteFinal.Attributes.CssStyle.Add("display", "none")
            Else

                divClienteFinal.Attributes.CssStyle.Add("display", "block")
                ddlClienteFinal.DataBind()

                sql = "SELECT ID_CLIENTE_FINAL FROM [dbo].[TB_CLIENTE_FINAL] WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue
                ds = Con.ExecutarQuery(sql)
                ddlClienteFinal.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL")
            End If


            sql = "SELECT ID_VENDEDOR FROM TB_PARCEIRO WHERE ID_PARCEIRO = " & ddlCliente.SelectedValue
            Dim ds1 As DataSet = Con.ExecutarQuery(sql)

            sql = "SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE FL_VENDEDOR = 1 AND ID_PARCEIRO = " & ds1.Tables(0).Rows(0).Item("ID_VENDEDOR") & " union SELECT  0, 'Selecione' FROM TB_PARCEIRO ORDER BY ID_PARCEIRO"
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
            RedFree.Visible = True

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

    Sub NumeroProcesso()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        ds = Con.ExecutarQuery("SELECT NRSEQUENCIALPROCESSO, AnoSequencialProcesso FROM TB_PARAMETROS")

        Dim PROCESSO_FINAL As String
        Dim ID_BL As String
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


        If ds.Tables(0).Rows(0).Item("AnoSequencialProcesso") = ano_atual Then
            ''CASO A ANO SEJA IGUAL

            ds = Con.ExecutarQuery("Select A.ID_SERVICO,isnull(B.VL_M3,0)VL_M3, isnull(B.VL_PESO_BRUTO,0)VL_PESO_BRUTO,
                            (SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO
                            from TB_COTACAO A 
                            left JOIN TB_COTACAO_MERCADORIA B ON B.ID_COTACAO = A.ID_COTACAO
                            Where A.ID_COTACAO = " & txtID.Text)

            SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")

            NRSEQUENCIALPROCESSO = NRSEQUENCIALPROCESSO + 1
            PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

            Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "'")

            Con.ExecutarQuery("UPDATE TB_COTACAO SET NR_PROCESSO_GERADO = '" & PROCESSO_FINAL & "' WHERE ID_COTACAO = " & txtID.Text)
            txtProcessoCotacao.Text = PROCESSO_FINAL

            Dim dsBL As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,GRAU,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_PARCEIRO_TRANSPORTADOR,ID_COTACAO,DT_ABERTURA,VL_PROFIT_DIVISAO,ID_PROFIT_DIVISAO,VL_FRETE,ID_MOEDA_FRETE,ID_PARCEIRO_VENDEDOR  ) 
SELECT '" & PROCESSO_FINAL & "','C', " & ddlServico.SelectedValue & ",ID_CLIENTE,ID_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,GETDATE(),VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,VL_TOTAL_FRETE_VENDA,ID_MOEDA_FRETE,ID_VENDEDOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_BL ")
            ID_BL = dsBL.Tables(0).Rows(0).Item("ID_BL").ToString()

            'TAXAS COMPRAS
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA) 
SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS," & ID_BL & ",1,'P',(SELECT ID_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA > 0 AND ID_COTACAO = " & txtID.Text)

            'TAXAS VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA) 
SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_CALCULADO,VL_TAXA_VENDA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'R', (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & "),1 FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA > 0 AND  ID_COTACAO = " & txtID.Text)


            Dim ID_BASE_CALCULO As Integer
            If ddlEstufagem.SelectedValue = 1 Then
                ID_BASE_CALCULO = 5
            ElseIf ddlEstufagem.SelectedValue = 2 Then
                ID_BASE_CALCULO = 13
            Else
                ID_BASE_CALCULO = 0
            End If

            'FRETE COMPRA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA_MIN," & ID_BL & ",'P',ID_TIPO_PAGAMENTO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)

            'FRETE VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)


            Dim dsCarga As DataSet = Con.ExecutarQuery("SELECT QT_CONTAINER FROM TB_COTACAO_MERCADORIA
 WHERE QT_CONTAINER is not null and QT_CONTAINER <> 0  and ID_COTACAO =  " & txtID.Text)
            If dsCarga.Tables(0).Rows.Count > 0 Then
                Dim QT_CONTAINER = dsCarga.Tables(0).Rows(0).Item("QT_CONTAINER")

                For i As Integer = 1 To QT_CONTAINER Step 1
                    Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,isnull(VL_PESO_BRUTO,0)/isnull(QT_CONTAINER,0)VL_PESO_BRUTO,isnull(VL_M3,0)/isnull(QT_CONTAINER,0)VL_M3," & ID_BL & "  FROM TB_COTACAO_MERCADORIA
 WHERE ID_COTACAO =  " & txtID.Text)
                Next

            Else
                Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3," & ID_BL & " FROM TB_COTACAO_MERCADORIA
 WHERE ID_COTACAO =  " & txtID.Text)
            End If

        Else
            ''CASO A ANO SEJA DIFERENTE

            Con.ExecutarQuery("UPDATE TB_PARAMETROS SET AnoSequencialProcesso = '" & ano_atual & "'")

            ds = Con.ExecutarQuery("Select ID_SERVICO,
(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO
From TB_COTACAO A Where ID_COTACAO = " & txtID.Text)
            SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")

            NRSEQUENCIALPROCESSO = NRSEQUENCIALPROCESSO + 1

            PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual


            Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "'")

            Con.ExecutarQuery("UPDATE TB_COTACAO SET NR_PROCESSO_GERADO = '" & PROCESSO_FINAL & "' WHERE ID_COTACAO = " & txtID.Text)
            txtProcessoCotacao.Text = PROCESSO_FINAL

            Dim dsBL As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,GRAU,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_PARCEIRO_TRANSPORTADOR,ID_COTACAO,DT_ABERTURA,VL_PROFIT_DIVISAO,ID_PROFIT_DIVISAO,VL_FRETE,ID_MOEDA_FRETE,ID_PARCEIRO_VENDEDOR  ) SELECT '" & PROCESSO_FINAL & "','C', " & ddlServico.SelectedValue & ",ID_CLIENTE,ID_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,GETDATE(),VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,VL_TOTAL_FRETE_VENDA,ID_MOEDA_FRETE,ID_VENDEDOR  FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_BL ")
            ID_BL = dsBL.Tables(0).Rows(0).Item("ID_BL").ToString()

            'TAXAS COMPRAS
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA) SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'P',(SELECT ID_TRANSPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") FROM TB_COTACAO_TAXA
 WHERE  VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_VENDA > 0 AND  ID_COTACAO = " & txtID.Text)


            'TAXAS VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA, ID_DESTINATARIO_COBRANCA) SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_CALCULADO,VL_TAXA_VENDA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'R',(SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & "),1 FROM TB_COTACAO_TAXA
 WHERE  VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA > 0 AND  ID_COTACAO = " & txtID.Text)


            Dim ID_BASE_CALCULO As Integer
            If ddlEstufagem.SelectedValue = 1 Then
                ID_BASE_CALCULO = 5
            ElseIf ddlEstufagem.SelectedValue = 2 Then
                ID_BASE_CALCULO = 13
            Else
                ID_BASE_CALCULO = 0
            End If

            'FRETE COMPRA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA_MIN," & ID_BL & ",'P',ID_TIPO_PAGAMENTO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)

            'FRETE VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)

            Dim dsCarga As DataSet = Con.ExecutarQuery("SELECT QT_CONTAINER FROM TB_COTACAO_MERCADORIA
 WHERE QT_CONTAINER is not null and QT_CONTAINER <> 0  and ID_COTACAO =  " & txtID.Text)
            If dsCarga.Tables(0).Rows.Count > 0 Then
                Dim QT_CONTAINER = dsCarga.Tables(0).Rows(0).Item("QT_CONTAINER")

                For i As Integer = 1 To QT_CONTAINER Step 1
                    Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL ) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,isnull(VL_PESO_BRUTO,0)/isnull(QT_CONTAINER,0)VL_PESO_BRUTO,isnull(VL_M3,0)/isnull(QT_CONTAINER,0)VL_M3," & ID_BL & "  FROM TB_COTACAO_MERCADORIA
 WHERE ID_COTACAO =  " & txtID.Text)
                Next

            Else
                Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3," & ID_BL & " FROM TB_COTACAO_MERCADORIA
 WHERE ID_COTACAO =  " & txtID.Text)
            End If

        End If
    End Sub

    Sub ImportaTaxas()

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT count(*)qtd FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
        '  If ds.Tables(0).Rows.Count > 0 Then
        If ds.Tables(0).Rows(0).Item("qtd") = 0 Then
            Dim comex As Integer = 0
            Dim FILTROCOMEX As String = ""

            If ddlServico.SelectedValue = 1 Or ddlServico.SelectedValue = 4 Then
                comex = 1
            ElseIf ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then
                comex = 2
            End If

            If comex > 0 Then
                FILTROCOMEX = " AND ID_TIPO_COMEX = " & comex
            End If

            Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO)
select " & txtID.Text & " , ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_COMPRA)ID_MOEDA_COMPRA,VL_TAXA_COMPRA,(SELECT ID_MOEDA FROM TB_MOEDA WHERE CD_MOEDA = ID_MOEDA_VENDA)ID_MOEDA_VENDA,VL_TAXA_VENDA,FL_DIVISAO_PROFIT,OB_TAXAS,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO from TB_TAXA_CLIENTE where ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & " AND ID_PARCEIRO = " & ddlCliente.SelectedValue)

            If ddlFreteTransportador_Frete.SelectedValue = 0 Then

                Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,VL_TAXA_COMPRA,ID_MOEDA_COMPRA,ID_BASE_CALCULO_TAXA,FL_TAXA_TRANSPORTADOR)   
SELECT " & txtID.Text & " , ID_ITEM_DESPESA, VL_TAXA_LOCAL_COMPRA, ID_MOEDA,ID_BASE_CALCULO,1  FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ddlDestinoFrete.SelectedValue & " AND ID_TRANSPORTADOR = " & ddlTransportadorFrete.SelectedValue & " AND ID_VIATRANSPORTE = (select ID_VIATRANSPORTE from TB_SERVICO b where ID_SERVICO = " & ddlServico.SelectedValue & ")" & FILTROCOMEX)
            Else
                Con.ExecutarQuery("INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA_MIN,FL_TAXA_TRANSPORTADOR)
                    SELECT " & txtID.Text & ",ID_ITEM_DESPESA,CASE WHEN ID_ORIGEM_PAGAMENTO = 1 THEN 1 WHEN ID_ORIGEM_PAGAMENTO =2 THEN 2 END ID_TIPO_PAGAMENTO ,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_MIN,VL_TAXA_COMPRA_MIN,1 FROM TB_TABELA_FRETE_TAXA WHERE ID_FRETE_TRANSPORTADOR =  " & ddlFreteTransportador_Frete.SelectedValue)
            End If
        End If
        '  End If
    End Sub

    Private Sub ddlRotaFrete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRotaFrete.SelectedIndexChanged
        If ddlRotaFrete.SelectedValue = 1 Then
            divEscala.Attributes.CssStyle.Add("display", "none")
        ElseIf ddlRotaFrete.SelectedValue = 2 Then
            divEscala.Attributes.CssStyle.Add("display", "block")
        End If
    End Sub

    Private Sub txtQtdContainerMercadoria_TextChanged(sender As Object, e As EventArgs) Handles txtQtdContainerMercadoria.TextChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT QT_DIAS_FREETIME,VL_COMPRA from TB_TARIFARIO_FRETE_TRANSPORTADOR where ID_FRETE_TRANSPORTADOR = " & ddlFreteTransportador_Frete.SelectedValue & " AND ID_TIPO_CONTAINER = " & ddlTipoContainerMercadoria.SelectedValue)

        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")) Then
                txtFreeTimeMercadoria.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMPRA")) Then
                Dim valor As Double = ds.Tables(0).Rows(0).Item("VL_COMPRA")
                Dim qtd As Integer = txtQtdContainerMercadoria.Text
                Dim X As Double = valor * qtd
                Dim TOTAL As String = X.ToString("#,###.00")
                'txtFreteCompraMercadoria.Text = FormatCurrency(valor * qtd)


                txtFreteCompraMercadoria.Text = TOTAL
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

    Private Sub txtFreteVenda_TextChanged(sender As Object, e As EventArgs) Handles txtFreteVenda.TextChanged
        If txtFreteVenda.Text <> "" And txtFreteCompra.Text <> "" Then

            Dim VENDA As Double = txtFreteVenda.Text
            Dim COMPRA As Double = txtFreteCompra.Text
            If VENDA < COMPRA Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "Func()", True)
            End If
        End If
    End Sub

End Class