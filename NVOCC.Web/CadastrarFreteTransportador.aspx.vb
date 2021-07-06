Public Class CadastrarFreteTransportador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            CarregaCampos()
        Else
            ckbAtivo.Checked = True
            ckbCargaEspecial.Checked = False


        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub
    Sub CarregaCampos()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT A.ID_FRETE_TRANSPORTADOR,A.ID_TRANSPORTADOR,A.ID_AGENTE,A.ID_PORTO_ORIGEM,A.ID_PORTO_DESTINO,A.ID_PORTO_ESCALA,A.ID_MOEDA_FRETE,A.ID_TIPO_CARGA,A.ID_VIA_ROTA,A.ID_TIPO_COMEX,A.QT_DIAS_TRANSITTIME_INICIAL,A.QT_DIAS_TRANSITTIME_FINAL,A.QT_DIAS_TRANSITTIME_MEDIA,A.ID_TIPO_FREQUENCIA,A.VL_FREQUENCIA,A.NM_TAXAS_INCLUDED,A.FL_ATIVO,CONVERT(varchar,A.DT_VALIDADE_FINAL,103)DT_VALIDADE_FINAL, A.ID_PORTO_ESCALA2,A.ID_PORTO_ESCALA3,ID_VIATRANSPORTE,ID_ORIGEM_PAGAMENTO

    FROM TB_FRETE_TRANSPORTADOR A
    WHERE A.ID_FRETE_TRANSPORTADOR = " & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then

            'Informaçoes basicas
            txtID_FreteTransportador.Text = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()
            ddlTransportador.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
            ddlOrigem_Pagamento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString()
            ckbAtivo.Checked = ds.Tables(0).Rows(0).Item("FL_ATIVO")
            ddlAgente.SelectedValue = ds.Tables(0).Rows(0).Item("ID_AGENTE").ToString()
            ddlOrigem.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString()
            ddlDestino.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString()
            ddlEscala1.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA").ToString()
            ddlMoeda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString()
            ddlTipoCarga.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString()
            ddlRota.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VIA_ROTA").ToString()
            ddlComex.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_COMEX").ToString()
            txtTransittimeInicial.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_TRANSITTIME_INICIAL").ToString()
            txtTransittimeFinal.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_TRANSITTIME_FINAL").ToString()
            ddlFrequencia.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FREQUENCIA").ToString()
            txtTaxas.Text = ds.Tables(0).Rows(0).Item("NM_TAXAS_INCLUDED").ToString()

            txtValidadeFinal.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_FINAL").ToString()


            ddlViaTransporte.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VIATRANSPORTE").ToString()

            If ds.Tables(0).Rows(0).Item("ID_VIA_ROTA").ToString() = 2 Then
                divEscala.Attributes.CssStyle.Add("display", "block")
            Else
                divEscala.Attributes.CssStyle.Add("display", "none")
            End If




            'tarifario
            txtFreteTransportadorTarifario.Text = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()


            If ds.Tables(0).Rows(0).Item("ID_TIPO_COMEX").ToString() = 2 Then
                divMercadoriaServico.Attributes.CssStyle.Add("display", "block")
            Else
                divMercadoriaServico.Attributes.CssStyle.Add("display", "none")
            End If




            'Taxas
            txtFreteTransportadorTaxa.Text = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")


        End If
    End Sub

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        diverro.Visible = False
        divsuccess.Visible = False
        Dim v As New VerificaData

        Dim ESCALA1 As Integer = ddlEscala1.SelectedValue
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        If ddlTransportador.SelectedValue = 0 Or ddlAgente.SelectedValue = 0 Or ddlOrigem.SelectedValue = 0 Or ddlDestino.SelectedValue = 0 Or ddlMoeda.SelectedValue = 0 Or ddlTipoCarga.SelectedValue = 0 Or ddlRota.SelectedValue = 0 Or ddlComex.SelectedValue = 0 Or txtTransittimeInicial.Text = "" Or txtTransittimeFinal.Text = "" Or ddlFrequencia.SelectedIndex = 0 Then
            lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Informações Básicas."
            diverro.Visible = True


        ElseIf ddlRota.SelectedValue = 2 And ESCALA1 = 0 Then
            diverro.Visible = True
            lblmsgErro.Text = "Preencha ao menos um porto para escala."

        Else

            Dim TTInicial As Integer = txtTransittimeInicial.Text
            Dim TTFinal As Integer = txtTransittimeFinal.Text

            Dim TTMedia As Integer = (TTFinal + TTInicial)
            TTMedia = TTMedia / 2

            Dim TaxasIncluded As String

            If txtTaxas.Text = "" Then
                TaxasIncluded = " NULL "
            Else
                TaxasIncluded = " '" & txtTaxas.Text & "' "
            End If

            If txtID_FreteTransportador.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    diverro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else
                    'INSERE FRETE TRANSPORTADOR
                    ds = Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR ( ID_TRANSPORTADOR, ID_AGENTE, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_PORTO_ESCALA,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3, ID_MOEDA_FRETE, ID_TIPO_CARGA, ID_VIA_ROTA, ID_TIPO_COMEX, QT_DIAS_TRANSITTIME_INICIAL, QT_DIAS_TRANSITTIME_FINAL, QT_DIAS_TRANSITTIME_MEDIA, ID_TIPO_FREQUENCIA,NM_TAXAS_INCLUDED, FL_ATIVO,ID_VIATRANSPORTE,ID_ORIGEM_PAGAMENTO) VALUES (" & ddlTransportador.SelectedValue & "," & ddlAgente.SelectedValue & "," & ddlOrigem.SelectedValue & " ," & ddlDestino.SelectedValue & ", " & ddlEscala1.SelectedValue & ", " & ddlEscala2.SelectedValue & ", " & ddlEscala3.SelectedValue & "," & ddlMoeda.SelectedValue & ", " & ddlTipoCarga.SelectedValue & ", " & ddlRota.SelectedValue & ", " & ddlComex.SelectedValue & ",'" & txtTransittimeInicial.Text & "','" & txtTransittimeFinal.Text & "','" & TTMedia & "'," & ddlFrequencia.SelectedIndex & "," & TaxasIncluded & ",'" & ckbAtivo.Checked & "'," & ddlViaTransporte.SelectedValue & "," & ddlOrigem_Pagamento.SelectedValue & "  ) Select SCOPE_IDENTITY() as ID_FRETE_TRANSPORTADOR ")

                    'PREENCHE SESSÃO E CAMPOS DO TARIFARIO E DA TAXA COM O ID DO NOVO FRETE TRANSPORTADOR
                    Session("ID_FRETE_TRANSPORTADOR") = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()
                    txtID_FreteTransportador.Text = Session("ID_FRETE_TRANSPORTADOR")
                    txtFreteTransportadorTarifario.Text = Session("ID_FRETE_TRANSPORTADOR")
                    txtFreteTransportadorTaxa.Text = Session("ID_FRETE_TRANSPORTADOR")

                    Con.Fechar()

                    'IMPORTA TAXAS LOCAIS ARMADOR
                    ImportaTaxas()
                    dgvTaxas.DataBind()

                    If txtValidadeFinal.Enabled = True And txtValidadeFinal.Text <> "" Then
                        Con.ExecutarQuery("UPDATE TB_FRETE_TRANSPORTADOR SET DT_VALIDADE_FINAL = " & txtValidadeFinal.Text & " WHERE ID_FRETE_TRANSPORTADOR  = " & Session("ID_FRETE_TRANSPORTADOR"))
                    End If
                    divsuccess.Visible = True

                End If



            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    diverro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else

                    'REALIZA UPDATE DO FRETE TRANSPORTADOR
                    Con.ExecutarQuery("UPDATE TB_FRETE_TRANSPORTADOR  SET  ID_TRANSPORTADOR = " & ddlTransportador.SelectedValue & ", ID_AGENTE = " & ddlAgente.SelectedValue & ", ID_PORTO_ORIGEM = " & ddlOrigem.SelectedValue & " , ID_PORTO_DESTINO = " & ddlDestino.SelectedValue & ", ID_PORTO_ESCALA = " & ddlEscala1.SelectedValue & ", ID_PORTO_ESCALA2 = " & ddlEscala2.SelectedValue & ",ID_PORTO_ESCALA3 = " & ddlEscala3.SelectedValue & ", ID_MOEDA_FRETE =" & ddlMoeda.SelectedValue & ", ID_TIPO_CARGA = " & ddlTipoCarga.SelectedValue & ", ID_VIA_ROTA =  " & ddlRota.SelectedValue & ", ID_TIPO_COMEX = " & ddlComex.SelectedValue & ", QT_DIAS_TRANSITTIME_INICIAL =  " & txtTransittimeInicial.Text & ", QT_DIAS_TRANSITTIME_FINAL = " & txtTransittimeFinal.Text & ", QT_DIAS_TRANSITTIME_MEDIA = '" & TTMedia & "', ID_TIPO_FREQUENCIA = " & ddlFrequencia.SelectedValue & ", NM_TAXAS_INCLUDED =  " & TaxasIncluded & ", FL_ATIVO = '" & ckbAtivo.Checked & "',ID_VIATRANSPORTE = " & ddlViaTransporte.SelectedValue & ", ID_ORIGEM_PAGAMENTO = " & ddlOrigem_Pagamento.SelectedValue & " WHERE ID_FRETE_TRANSPORTADOR = " & txtID_FreteTransportador.Text)


                    If txtValidadeFinal.Enabled = True And txtValidadeFinal.Text <> "" Then
                        Con.ExecutarQuery("UPDATE TB_FRETE_TRANSPORTADOR SET DT_VALIDADE_FINAL = CONVERT(DATE,'" & txtValidadeFinal.Text & "',103) WHERE ID_FRETE_TRANSPORTADOR  = " & txtID_FreteTransportador.Text)
                    End If
                    divsuccess.Visible = True
                    Con.Fechar()


                End If



            End If


        End If

    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("CadastrarFreteTransportador.aspx")
    End Sub

    Private Sub ddlRota_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRota.SelectedIndexChanged
        If ddlRota.SelectedValue = 2 Then
            ' divEscala.Visible = True
            divEscala.Attributes.CssStyle.Add("display", "block")
        Else
            ' divEscala.Visible = False
            divEscala.Attributes.CssStyle.Add("display", "none")

        End If
    End Sub

    Private Sub ddlComex_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlComex.SelectedIndexChanged
        If ddlComex.SelectedValue = 2 Then
            divMercadoriaServico.Attributes.CssStyle.Add("display", "block")
        Else
            divMercadoriaServico.Attributes.CssStyle.Add("display", "none")
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
    Protected Sub dgvFreteTarifario_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvFreteTarifario.DataSource = Session("TaskTable")
            dgvFreteTarifario.DataBind()
            dgvFreteTarifario.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
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

    Private Sub dgvFreteTarifario_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFreteTarifario.RowCommand
        divMsgExcluir.Visible = False
        divInfoTarifario.Visible = False

        divMsgErro.Visible = False
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblmsgErro.Text = "Usuário não tem permissão para realizar exclusões"
                divMsgErro.Visible = True

            Else
                Con.ExecutarQuery("DELETE From TB_TARIFARIO_FRETE_TRANSPORTADOR Where ID_TARIFARIO_FRETE_TRANSPORTADOR = " & ID)
                lblMsgExcluir.Text = "Registro deletado!"
                divMsgExcluir.Visible = True
                dgvFreteTarifario.DataBind()
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT B.ID_TARIFARIO_FRETE_TRANSPORTADOR,B.ID_TIPO_CONTAINER,B.ID_TIPO_ESTUFAGEM,CONVERT(varchar,B.DT_VALIDADE_INICIAL,103)DT_VALIDADE_INICIAL,CONVERT(varchar,B.DT_VALIDADE_FINAL,103)DT_VALIDADE_FINAL,B.VL_COMPRA,B.VL_MINIMO,B.QT_DIAS_FREETIME,B.FL_IMO,B.FL_CARGA_ESPECIAL,B.VL_M3_INICIAL,B.VL_M3_FINAL,B.ID_MERCADORIA,B.SERVICO,B.ID_FRETE_TRANSPORTADOR,A.ID_TIPO_COMEX
 FROM TB_FRETE_TRANSPORTADOR A
                    Left Join TB_TARIFARIO_FRETE_TRANSPORTADOR B ON B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR
WHERE B.ID_TARIFARIO_FRETE_TRANSPORTADOR = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'tarifario
                txtIDTarifario.Text = ds.Tables(0).Rows(0).Item("ID_TARIFARIO_FRETE_TRANSPORTADOR").ToString
                txtFreteTransportadorTarifario.Text = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()
                txtEstufagem.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString()
                txtValorCompra.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")
                ckbIMO.Checked = ds.Tables(0).Rows(0).Item("FL_IMO")
                ckbCargaEspecial.Checked = ds.Tables(0).Rows(0).Item("FL_CARGA_ESPECIAL")
                txtValidadeInicial.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL").ToString()
                txtValidadeFinal_Tarifario.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_FINAL").ToString()


                If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString() = 2 Then
                    txtM3Inicial.Text = ds.Tables(0).Rows(0).Item("VL_M3_INICIAL")
                    txtM3Final.Text = ds.Tables(0).Rows(0).Item("VL_M3_FINAL")
                    txtValorMinimo.Text = ds.Tables(0).Rows(0).Item("VL_MINIMO")
                Else
                    ddlContainer.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER")
                    txtFreetime.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")
                End If

                If ds.Tables(0).Rows(0).Item("ID_TIPO_COMEX").ToString() = 2 Then
                    divMercadoriaServico.Attributes.CssStyle.Add("display", "block")
                    ddlMercadoria.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString()
                    txtservico.Text = ds.Tables(0).Rows(0).Item("SERVICO").ToString()
                Else
                    divMercadoriaServico.Attributes.CssStyle.Add("display", "none")

                End If

                mpeNovoTarifario.Show()
            End If
        End If
        Con.Fechar()

    End Sub


    Private Sub dgvTaxas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxas.RowCommand
        divDeleteTaxas.Visible = False
        divDeleteErro.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblDeleteErro.Text = "Usuário não tem permissão para realizar exclusões"
                divDeleteErro.Visible = True
            Else
                Con.ExecutarQuery("DELETE From TB_TABELA_FRETE_TAXA Where ID_TABELA_FRETE_TAXA = " & ID)
                lblDeleteTaxas.Text = "Registro deletado!"
                divDeleteTaxas.Visible = True
                dgvTaxas.DataBind()
            End If


        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT C.ID_TABELA_FRETE_TAXA,C.ID_TIPO_ESTUFAGEM,C.ID_ITEM_DESPESA,C.ID_ORIGEM_PAGAMENTO,C.ID_BASE_CALCULO_TAXA,C.ID_MOEDA_COMPRA,C.VL_TAXA_COMPRA,C.ID_MOEDA_VENDA,C.VL_TAXA_VENDA,VL_TAXA_VENDA_MIN,ID_FRETE_TRANSPORTADOR,VL_TAXA_COMPRA_MIN

FROM TB_TABELA_FRETE_TAXA C
WHERE C.ID_TABELA_FRETE_TAXA = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TABELA_FRETE_TAXA")) Then
                    txtIDTaxa.Text = ds.Tables(0).Rows(0).Item("ID_TABELA_FRETE_TAXA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")) Then
                    txtFreteTransportadorTaxa.Text = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                    ddlItemDespesa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                    ddlOrigemPagamento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculoTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")) Then
                    ddlMoedaCompra.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")) Then
                    ddlMoedaVenda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")) Then
                    txtValorTaxaCompra.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA")) Then
                    txtValorTaxaVenda.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")) Then
                    txtValorTaxaVendaMin.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")) Then
                    txtValorTaxaCompraMin.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                    ddlEstufagemTaxa.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                End If

                mpeNovoTaxa.Show()

                End If
            End If
        Con.Fechar()

    End Sub

    Private Sub btnFecharTarifario_Click(sender As Object, e As EventArgs) Handles btnFecharTarifario.Click
        divSuccessTarifario.Visible = False
        divErroTarifario.Visible = False

        txtIDTarifario.Text = ""
        txtEstufagem.Text = ""
        txtValorCompra.Text = ""
        txtM3Inicial.Text = ""
        txtM3Final.Text = ""
        txtValorMinimo.Text = ""
        ddlContainer.SelectedValue = 0
        txtValidadeInicial.Text = ""
        txtValidadeFinal_Tarifario.Text = ""
        txtFreetime.Text = ""
        ddlMercadoria.SelectedValue = 0
        txtservico.Text = ""

        dgvFreteTarifario.DataBind()
        mpeNovoTarifario.Hide()
    End Sub

    Private Sub btnFecharTaxa_Click(sender As Object, e As EventArgs) Handles btnFecharTaxa.Click
        divSuccessTaxa.Visible = False
        divErroTaxa.Visible = False

        txtIDTaxa.Text = ""
        ddlItemDespesa.SelectedValue = 0
        ddlOrigemPagamento.SelectedValue = 0
        ddlBaseCalculoTaxa.SelectedValue = 0
        ddlMoedaCompra.SelectedValue = 0
        ddlMoedaVenda.SelectedValue = 0
        txtValorTaxaCompra.Text = ""
        txtValorTaxaVenda.Text = ""
        txtValorTaxaVendaMin.Text = ""
        txtValorTaxaCompraMin.Text = ""

        dgvTaxas.DataBind()
        mpeNovoTaxa.Hide()
    End Sub

    Private Sub btnSalvarTarifario_Click(sender As Object, e As EventArgs) Handles btnSalvarTarifario.Click
        divErroTarifario.Visible = False
        divSuccessTarifario.Visible = False
        divInfoTarifario.Visible = False
        Dim estufagem As Integer


        If txtEstufagem.Text = "" Then
            estufagem = 1
        Else
            estufagem = txtEstufagem.Text
        End If
        Dim v As New VerificaData

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtFreteTransportadorTarifario.Text = "" Then

            lblmsgErroTarifario.Text = "Antes de inserir Tarifario é necessário cadastrar Frete Transportador na Aba de Informações Basicas"
            divErroTarifario.Visible = True

        ElseIf txtValidadeInicial.Text = "" Then
            lblmsgErroTarifario.Text = "Preencha todos os campos obrigatórios."
            divErroTarifario.Visible = True
            Exit Sub

        ElseIf txtValidadeFinal_Tarifario.Text = "" Then
            divErroTarifario.Visible = True
            lblmsgErroTarifario.Text = "Preencha todos os campos obrigatórios."
            Exit Sub

        ElseIf v.ValidaData(txtValidadeInicial.Text) = False Then
            divErroTarifario.Visible = True
            lblmsgErroTarifario.Text = "A data de validade inicial é inválida."
            Exit Sub

        ElseIf v.ValidaData(txtValidadeFinal_Tarifario.Text) = False Then
            divErroTarifario.Visible = True
            lblmsgErroTarifario.Text = "A data de validade final é inválida."
            Exit Sub
        ElseIf txtValorCompra.Text = "" Then
            lblmsgErroTarifario.Text = "Preencha todos os campos obrigatórios."
            divErroTarifario.Visible = True
            Exit Sub

        ElseIf estufagem = 1 And ddlContainer.SelectedValue = 0 Then
            lblmsgErroTarifario.Text = "Preencha todos os campos obrigatórios"
            divErroTarifario.Visible = True
            Exit Sub

        ElseIf estufagem = 1 And txtFreetime.Text = "" Then
            lblmsgErroTarifario.Text = "Preencha todos os campos obrigatórios."
            divErroTarifario.Visible = True
            Exit Sub

        ElseIf estufagem = 2 And txtM3Final.Text = "" Then
            lblmsgErroTarifario.Text = "Preencha todos os campos obrigatórios."
            divErroTarifario.Visible = True
            Exit Sub

        ElseIf estufagem = 2 And txtM3Inicial.Text = "" Then
            lblmsgErroTarifario.Text = "Preencha todos os campos obrigatórios."
            divErroTarifario.Visible = True
            Exit Sub

        ElseIf estufagem = 2 And txtValorMinimo.Text = "" Then
            lblmsgErroTarifario.Text = "Preencha todos os campos obrigatórios."
            divErroTarifario.Visible = True

            Exit Sub


        Else



            txtValorCompra.Text = txtValorCompra.Text.Replace(".", "")
            txtValorCompra.Text = txtValorCompra.Text.Replace(",", ".")

            txtValorMinimo.Text = txtValorMinimo.Text.Replace(".", "")
            txtValorMinimo.Text = txtValorMinimo.Text.Replace(",", ".")

            txtM3Inicial.Text = txtM3Inicial.Text.Replace(".", "")
            txtM3Inicial.Text = txtM3Inicial.Text.Replace(",", ".")

            txtM3Final.Text = txtM3Final.Text.Replace(".", "")
            txtM3Final.Text = txtM3Final.Text.Replace(",", ".")


            If txtIDTarifario.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroTarifario.Visible = True
                    lblmsgErroTarifario.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else


                    'VERIFIFA SE HÁ TARIFARIO EM ABERTO
                    ds = Con.ExecutarQuery("SELECT ID_TARIFARIO_FRETE_TRANSPORTADOR FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_TIPO_CONTAINER = " & ddlContainer.SelectedValue & " AND DT_VALIDADE_FINAL >= convert(date,getdate(),103) and ID_FRETE_TRANSPORTADOR =" & txtFreteTransportadorTarifario.Text)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim data As DateTime = txtValidadeFinal_Tarifario.Text
                        data = data.AddDays(-1)
                        'ALTERA DATA DO TAFIFARIO EM ABERTO
                        For Each linha As DataRow In ds.Tables(0).Rows
                            Dim FreteTransportador As Integer = linha.Item("ID_TARIFARIO_FRETE_TRANSPORTADOR").ToString()
                            Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR SET DT_VALIDADE_FINAL = convert(date,'" & data & "',103) WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & FreteTransportador)
                        Next

                    End If

                    If txtservico.Text = "" Then
                        txtservico.Text = "NULL"
                    Else
                        txtservico.Text = "'" & txtservico.Text & "'"
                    End If

                    'VERIFICA TIPO DE ESTUFAGEM
                    If estufagem = 2 Then

                        'INSERE TARIFARIO
                        ds = Con.ExecutarQuery("INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR,ID_TIPO_ESTUFAGEM, VL_COMPRA, VL_MINIMO,FL_IMO,FL_CARGA_ESPECIAL,VL_M3_INICIAL,VL_M3_FINAL,ID_MERCADORIA,SERVICO,DT_VALIDADE_INICIAL,DT_VALIDADE_FINAL ) VALUES (" & txtFreteTransportadorTarifario.Text & "," & estufagem & ",'" & txtValorCompra.Text & "','" & txtValorMinimo.Text & "','" & ckbIMO.Checked & "','" & ckbCargaEspecial.Checked & "','" & txtM3Inicial.Text & "', '" & txtM3Final.Text & "', " & ddlMercadoria.SelectedValue & ", " & txtservico.Text & ",convert(date,'" & txtValidadeInicial.Text & "',103),convert(date,'" & txtValidadeFinal_Tarifario.Text & "',103) )")
                        txtValidadeFinal.Enabled = True

                    Else
                        'INSERE TARIFARIO
                        ds = Con.ExecutarQuery("INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR, ID_TIPO_CONTAINER,ID_TIPO_ESTUFAGEM,DT_VALIDADE_INICIAL,DT_VALIDADE_FINAL,VL_COMPRA, QT_DIAS_FREETIME,FL_IMO,FL_CARGA_ESPECIAL,ID_MERCADORIA,SERVICO ) VALUES (" & txtFreteTransportadorTarifario.Text & "," & ddlContainer.SelectedValue & "," & estufagem & " , convert(date,'" & txtValidadeInicial.Text & "',103),convert(date,'" & txtValidadeFinal_Tarifario.Text & "',103),'" & txtValorCompra.Text & "' ," & txtFreetime.Text & ",'" & ckbIMO.Checked & "','" & ckbCargaEspecial.Checked & "', " & ddlMercadoria.SelectedValue & ", " & txtservico.Text & ")")

                    End If

                    Con.ExecutarQuery("UPDATE TB_FRETE_TRANSPORTADOR SET DT_VALIDADE_FINAL = (SELECT MAX(DT_VALIDADE_FINAL) FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR  = " & txtFreteTransportadorTarifario.Text & ") WHERE ID_FRETE_TRANSPORTADOR  = " & txtFreteTransportadorTarifario.Text)


                    txtIDTarifario.Text = ""
                    txtEstufagem.Text = ""
                    txtValorCompra.Text = ""
                    txtM3Inicial.Text = ""
                    txtM3Final.Text = ""
                    txtValorMinimo.Text = ""
                    ddlContainer.SelectedValue = 0
                    txtValidadeInicial.Text = ""
                    txtValidadeFinal_Tarifario.Text = ""
                    txtFreetime.Text = ""
                    ddlMercadoria.SelectedValue = 0
                    txtservico.Text = ""
                    dgvFreteTarifario.DataBind()
                    Con.Fechar()
                    divSuccessTarifario.Visible = True

                End If


            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroTarifario.Visible = True
                    lblmsgErroTarifario.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else

                    If txtservico.Text = "" Then
                        txtservico.Text = "NULL"
                    Else
                        txtservico.Text = "'" & txtservico.Text & "'"
                    End If

                    'VERIFICA TIPO DE ESTUFAGEM
                    If estufagem = 2 Then
                        'ALTERA TARIFARIO
                        ds = Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR SET ID_FRETE_TRANSPORTADOR =  " & txtFreteTransportadorTarifario.Text & ",ID_TIPO_ESTUFAGEM = " & estufagem & ",VL_COMPRA = '" & txtValorCompra.Text & "', VL_MINIMO = '" & txtValorMinimo.Text & "',FL_IMO = '" & ckbIMO.Checked & "',FL_CARGA_ESPECIAL = '" & ckbCargaEspecial.Checked & "', VL_M3_INICIAL = '" & txtM3Inicial.Text & "', VL_M3_FINAL = '" & txtM3Final.Text & "', ID_MERCADORIA = " & ddlMercadoria.SelectedValue & ", SERVICO = " & txtservico.Text & ",DT_VALIDADE_INICIAL = convert(date,'" & txtValidadeInicial.Text & "',103),DT_VALIDADE_FINAL = convert(date,'" & txtValidadeFinal_Tarifario.Text & "',103) WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & txtIDTarifario.Text)
                    Else
                        'ALTERA TARIFARIO
                        ds = Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR SET ID_FRETE_TRANSPORTADOR =  " & txtFreteTransportadorTarifario.Text & ", ID_TIPO_CONTAINER = " & ddlContainer.SelectedValue & " ,ID_TIPO_ESTUFAGEM = " & estufagem & ",DT_VALIDADE_INICIAL = convert(date,'" & txtValidadeInicial.Text & "',103),DT_VALIDADE_FINAL = convert(date,'" & txtValidadeFinal_Tarifario.Text & "',103), VL_COMPRA = '" & txtValorCompra.Text & "', QT_DIAS_FREETIME = " & txtFreetime.Text & ",FL_IMO = '" & ckbIMO.Checked & "',FL_CARGA_ESPECIAL =  '" & ckbCargaEspecial.Checked & "', ID_MERCADORIA = " & ddlMercadoria.SelectedValue & ", SERVICO = " & txtservico.Text & " WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & txtIDTarifario.Text)


                    End If
                    Con.ExecutarQuery("UPDATE TB_FRETE_TRANSPORTADOR SET DT_VALIDADE_FINAL = (SELECT MAX(DT_VALIDADE_FINAL) FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR  = " & txtFreteTransportadorTarifario.Text & ") WHERE ID_FRETE_TRANSPORTADOR  = " & txtFreteTransportadorTarifario.Text)

                    dgvFreteTarifario.DataBind()
                    divSuccessTarifario.Visible = True
                    Con.Fechar()


                End If

            End If

        End If





    End Sub

    Private Sub btnSalvarTaxa_Click(sender As Object, e As EventArgs) Handles btnSalvarTaxa.Click

        divErroTaxa.Visible = False
        divSuccessTaxa.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        If txtFreteTransportadorTaxa.Text = "" Then

            lblErroTaxa.Text = "Antes de inserir Taxa é necessario cadastrar Frete Transportador na Aba de Informações Basicas"
            divErroTaxa.Visible = True

        ElseIf ddlItemDespesa.SelectedValue = 0 Or ddlOrigemPagamento.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0 Or ddlMoedaCompra.SelectedValue = 0 Or txtValorTaxaCompra.Text = "" Or ddlEstufagemTaxa.SelectedValue = 0 Then
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

            If ddlMoedaVenda.SelectedValue = 0 Then
                ddlMoedaVenda.SelectedValue = ddlMoedaCompra.SelectedValue
            End If

            If txtValorTaxaVenda.Text = "" Then
                txtValorTaxaVenda.Text = txtValorTaxaCompra.Text
            Else
                txtValorTaxaVenda.Text = "" & txtValorTaxaVenda.Text & ""
            End If

            If txtValorTaxaVendaMin.Text = "" Then
                txtValorTaxaVendaMin.Text = "NULL"
            Else
                txtValorTaxaVendaMin.Text = "" & txtValorTaxaVendaMin.Text & ""
            End If

            If txtValorTaxaCompraMin.Text = "" Then
                txtValorTaxaCompraMin.Text = "NULL"
            Else
                txtValorTaxaCompraMin.Text = "" & txtValorTaxaCompraMin.Text & ""
            End If


            If txtIDTaxa.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroTaxa.Visible = True
                    lblErroTaxa.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else



                    'INSERE TAXAS
                    ds = Con.ExecutarQuery("INSERT INTO TB_TABELA_FRETE_TAXA ( ID_FRETE_TRANSPORTADOR,ID_TIPO_ESTUFAGEM,ID_ITEM_DESPESA,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA_MIN) VALUES (" & txtFreteTransportadorTaxa.Text & "," & ddlEstufagemTaxa.SelectedValue & " ," & ddlItemDespesa.SelectedValue & "," & ddlOrigemPagamento.SelectedValue & "," & ddlBaseCalculoTaxa.SelectedValue & "," & ddlMoedaCompra.SelectedValue & "," & txtValorTaxaCompra.Text & "," & ddlMoedaVenda.SelectedValue & "," & txtValorTaxaVenda.Text & ", " & txtValorTaxaVendaMin.Text & "," & txtValorTaxaCompraMin.Text & " )")


                    txtIDTaxa.Text = ""
                    ddlItemDespesa.SelectedValue = 0
                    ddlOrigemPagamento.SelectedValue = 0
                    ddlBaseCalculoTaxa.SelectedValue = 0
                    ddlMoedaCompra.SelectedValue = 0
                    ddlMoedaVenda.SelectedValue = 0
                    txtValorTaxaCompra.Text = ""
                    txtValorTaxaVenda.Text = ""
                    txtValorTaxaVendaMin.Text = ""
                    txtValorTaxaCompraMin.Text = ""
                    Con.Fechar()
                    divSuccessTaxa.Visible = True

                End If


            Else


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroTaxa.Visible = True
                    lblErroTaxa.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else


                    'ALTERA TAXAS
                    ds = Con.ExecutarQuery("UPDATE TB_TABELA_FRETE_TAXA SET ID_FRETE_TRANSPORTADOR =  " & txtFreteTransportadorTaxa.Text & ", ID_TIPO_ESTUFAGEM = " & ddlEstufagemTaxa.SelectedValue & ", ID_ITEM_DESPESA =  " & ddlItemDespesa.SelectedValue & " , ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento.SelectedValue & ",ID_BASE_CALCULO_TAXA =  " & ddlBaseCalculoTaxa.SelectedValue & " ,ID_MOEDA_COMPRA =  " & ddlMoedaCompra.SelectedValue & ",VL_TAXA_COMPRA = " & txtValorTaxaCompra.Text & ",ID_MOEDA_VENDA = " & ddlMoedaVenda.SelectedValue & ",VL_TAXA_VENDA = " & txtValorTaxaVenda.Text & ", VL_TAXA_VENDA_MIN = " & txtValorTaxaVendaMin.Text & ",VL_TAXA_COMPRA_MIN = " & txtValorTaxaCompraMin.Text & " WHERE ID_TABELA_FRETE_TAXA = " & txtIDTaxa.Text)

                    divSuccessTaxa.Visible = True
                    Con.Fechar()
                    txtValorTaxaVenda.Text = txtValorTaxaVenda.Text.Replace("NULL", "")
                    txtValorTaxaVendaMin.Text = txtValorTaxaVendaMin.Text.Replace("NULL", "")

                    txtValorTaxaCompra.Text = txtValorTaxaCompra.Text.Replace("NULL", "")
                    txtValorTaxaCompraMin.Text = txtValorTaxaCompraMin.Text.Replace("NULL", "")
                End If

            End If


        End If




    End Sub
    Sub ImportaTaxas()
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR,ID_ITEM_DESPESA FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ddlDestino.SelectedValue & " AND ID_TRANSPORTADOR = " & ddlTransportador.SelectedValue & " AND ID_VIATRANSPORTE = " & ddlViaTransporte.SelectedValue & " AND ID_TIPO_COMEX = " & ddlComex.SelectedValue & " AND ID_ITEM_DESPESA NOT IN (SELECT ID_ITEM_DESPESA FROM TB_TABELA_FRETE_TAXA WHERE ID_FRETE_TRANSPORTADOR = " & txtID_FreteTransportador.Text & "  )")
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows
                Con.ExecutarQuery("INSERT INTO TB_TABELA_FRETE_TAXA (ID_FRETE_TRANSPORTADOR,ID_ITEM_DESPESA,VL_TAXA_COMPRA,ID_MOEDA_COMPRA,ID_BASE_CALCULO_TAXA)   
SELECT " & txtID_FreteTransportador.Text & ", ID_ITEM_DESPESA, VL_TAXA_LOCAL_COMPRA,ID_MOEDA,ID_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_PORTO = " & ddlDestino.SelectedValue & " AND ID_TRANSPORTADOR = " & ddlTransportador.SelectedValue & " AND ID_VIATRANSPORTE = " & ddlViaTransporte.SelectedValue & " AND ID_TIPO_COMEX = " & ddlComex.SelectedValue & " AND ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR"))

            Next
            divDeleteTaxas.Visible = True
            lblDeleteTaxas.Text = "Ação realizada com sucesso!"
        Else
            divDeleteErro.Visible = True
            lblDeleteErro.Text = "Não há taxas para importar!"
        End If

    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        ImportaTaxas()
        dgvTaxas.DataBind()

    End Sub
End Class