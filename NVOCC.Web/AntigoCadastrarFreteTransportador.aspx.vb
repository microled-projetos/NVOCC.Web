Imports OfficeOpenXml
Public Class AntigoCadastrarFreteTransportador
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            CarregaCampos()
        Else
            ckbAtivo.Checked = True
            ckbCargaEspecial.Checked = True


        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ACESSAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("FL_ACESSAR") <> True Then

                Response.Redirect("Default.aspx")

            End If

        Else
            Response.Redirect("Default.aspx")
        End If
        Con.Fechar()
    End Sub

    'Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
    '    divErro.Visible = False
    '    divSuccess.Visible = False
    '    Dim Con As New Conexao_sql
    '    Con.Conectar()
    '    Dim ds As DataSet

    '    ds = Con.ExecutarQuery("SELECT FL_CADASTRAR, FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
    '    If ds.Tables(0).Rows.Count > 0 Then

    '        If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") = True Then
    '            FreteTransportador()


    '        ElseIf ds.Tables(0).Rows(0).Item("FL_CADASTRAR") = True Then
    '            FreteTransportador()


    '        Else

    '            diverro.Visible = True
    '            lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
    '        End If
    '    Else
    '        diverro.Visible = True
    '        lblmsgErro.Text = "Usuário não possui permissão."
    '    End If

    '    dgvFreteTarifario.DataBind()
    '    dgvTaxas.DataBind()
    'End Sub
    Sub CarregaCampos()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT A.ID_FRETE_TRANSPORTADOR,A.ID_TRANSPORTADOR,A.ID_AGENTE,A.ID_PORTO_ORIGEM,A.ID_PORTO_DESTINO,A.ID_PORTO_ESCALA,A.ID_MOEDA_FRETE,A.ID_TIPO_CARGA,A.ID_VIA_ROTA,A.ID_TIPO_COMEX,A.QT_DIAS_TRANSITTIME_INICIAL,A.QT_DIAS_TRANSITTIME_FINAL,A.QT_DIAS_TRANSITTIME_MEDIA,A.ID_TIPO_FREQUENCIA,A.VL_FREQUENCIA,A.NM_TAXAS_INCLUDED,A.FL_ATIVO,A.DT_VALIDADE_FINAL, A.ID_PORTO_ESCALA2,A.ID_PORTO_ESCALA3,

B.ID_TARIFARIO_FRETE_TRANSPORTADOR,B.ID_TIPO_CONTAINER,B.ID_TIPO_ESTUFAGEM,B.DT_VALIDADE_INICIAL,B.VL_COMPRA,B.VL_MINIMO,B.QT_DIAS_FREETIME,B.FL_IMO,B.FL_CARGA_ESPECIAL,B.VL_M3_INICIAL,B.VL_M3_FINAL,B.ID_MERCADORIA,B.SERVICO,

C.ID_TABELA_FRETE_TAXA,C.ID_TIPO_ESTUFAGEM,C.ID_TIPO_ITEM_DESPESA,C.ID_ORIGEM_PAGAMENTO,C.ID_BASE_CALCULO_TAXA,C.ID_MOEDA_COMPRA,C.VL_TAXA_COMPRA,C.ID_MOEDA_VENDA,C.VL_TAXA_VENDA,VL_TAXA_VENDA_MIN

FROM TB_FRETE_TRANSPORTADOR A
                Left Join TB_TARIFARIO_FRETE_TRANSPORTADOR B ON B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR
        Left Join TB_TABELA_FRETE_TAXA C ON C.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR  
WHERE A.ID_FRETE_TRANSPORTADOR = " & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then

            'Informaçoes basicas
            txtID_FreteTransportador.Text = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()
            ddlTransportador.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
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
            txtValorFrequencia.Text = ds.Tables(0).Rows(0).Item("VL_FREQUENCIA").ToString()
            txtValidadeFinal.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_FINAL").ToString()

            If ds.Tables(0).Rows(0).Item("ID_VIA_ROTA").ToString() = 2 Then
                'divEscala.Visible = True
                divEscala.Attributes.CssStyle.Add("display", "block")

                ddlEscala1.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA").ToString()
                ddlEscala2.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA2").ToString()
                ddlEscala3.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ESCALA3").ToString()
            Else
                'divEscala.Visible = False
                divEscala.Attributes.CssStyle.Add("display", "none")
            End If




            'tarifario
            txtIDTarifario.Text = ds.Tables(0).Rows(0).Item("ID_TARIFARIO_FRETE_TRANSPORTADOR").ToString
            txtFreteTransportadorTarifario.Text = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()
            txtEstufagem.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString()
            txtValorCompra.Text = ds.Tables(0).Rows(0).Item("VL_COMPRA")
            ckbIMO.Checked = ds.Tables(0).Rows(0).Item("FL_IMO")
            ckbCargaEspecial.Checked = ds.Tables(0).Rows(0).Item("FL_CARGA_ESPECIAL")


            If ds.Tables(0).Rows(0).Item("ID_TIPO_COMEX").ToString() = 2 Then
                txtM3Inicial.Text = ds.Tables(0).Rows(0).Item("VL_M3_INICIAL")
                txtM3Final.Text = ds.Tables(0).Rows(0).Item("VL_M3_FINAL")
                txtValorMinimo.Text = ds.Tables(0).Rows(0).Item("VL_MINIMO")
            Else
                ddlContainer.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER")
                txtValidadeInicial.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")
                txtFreetime.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")

            End If

            If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString() = 2 Then
                'divMercadoriaServico.Visible = True
                divMercadoriaServico.Attributes.CssStyle.Add("display", "block")
                ddlMercadoria.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString()
                txtservico.Text = ds.Tables(0).Rows(0).Item("SERVICO").ToString()
            Else
                'divMercadoriaServico.Visible = False
                divMercadoriaServico.Attributes.CssStyle.Add("display", "none")

            End If




            'Taxas
            txtIDTaxa.Text = ds.Tables(0).Rows(0).Item("ID_TABELA_FRETE_TAXA")
            txtFreteTransportadorTaxa.Text = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR")
            ddlItemDespesa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ITEM_DESPESA")
            ddlOrigemPagamento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
            ddlBaseCalculoTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
            ddlMoedaCompra.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA")
            ddlMoedaVenda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA_VENDA")
            txtValorTaxaCompra.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_COMPRA")
            txtValorTaxaVenda.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA")
            txtValorTaxaVendaMin.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN")



        End If
    End Sub
    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click

        If txtEstufagem.Text = "" Then
            Session("ESTUFAGEM") = 1

        Else
            Session("ESTUFAGEM") = txtEstufagem.Text

        End If

        Dim v As New VerificaData

        Dim ESCALA1 As Integer = ddlEscala1.SelectedValue
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim TTInicial As Integer = txtTransittimeInicial.Text
        Dim TTFinal As Integer = txtTransittimeFinal.Text

        Dim TTMedia As Integer = (TTFinal + TTInicial)
        TTMedia = TTMedia / 2

        If ddlTransportador.SelectedValue = 0 Or ddlAgente.SelectedValue = 0 Or ddlOrigem.SelectedValue = 0 Or ddlDestino.SelectedValue = 0 Or ddlMoeda.SelectedValue = 0 Or ddlTipoCarga.SelectedValue = 0 Or ddlRota.SelectedValue = 0 Or ddlComex.SelectedValue = 0 Or txtTransittimeInicial.Text = "" Or txtTransittimeFinal.Text = "" Or ddlFrequencia.SelectedIndex = 0 Or txtValorFrequencia.Text = "" Or txtValidadeFinal.Text = "" Then
            lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Informações Básicas."
            diverro.Visible = True


        ElseIf v.ValidaData(txtValidadeFinal.Text) = False Then
            diverro.Visible = True
            lblmsgErro.Text = "A data de validade final é inválida."

        ElseIf ddlRota.SelectedValue = 2 And ESCALA1 = 0 Then
            diverro.Visible = True
            lblmsgErro.Text = "Preencha ao menos um porto para escala."



        ElseIf ddlItemDespesa.SelectedValue = 0 Or ddlOrigemPagamento.SelectedValue = 0 Or ddlBaseCalculoTaxa.SelectedValue = 0 Or ddlMoedaCompra.SelectedValue = 0 Or txtValorCompra.Text = "" Then
            lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Taxas. "
            diverro.Visible = True




        Else



            If Session("ESTUFAGEM") = 1 Then

                If ddlContainer.SelectedValue = 0 Or txtValidadeInicial.Text = "" Or txtValorCompra.Text = "" Or txtFreetime.Text = "" Then
                    lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Frete Tarifário."
                    diverro.Visible = True
                    Exit Sub
                End If

                If v.ValidaData(txtValidadeInicial.Text) = False Then
                    diverro.Visible = True
                    lblmsgErro.Text = "A data de validade inicial na aba Frete Tarifário é inválida."
                    Exit Sub
                End If



            ElseIf Session("ESTUFAGEM") = 2 Then

                If txtM3Final.Text = "" Or txtM3Inicial.Text = "" Or txtValorCompra.Text = "" Or txtValorMinimo.Text = "" Then
                    lblmsgErro.Text = "Preencha todos os campos obrigatórios na Aba de Frete Tarifário."
                    diverro.Visible = True
                    Exit Sub
                End If

            End If



            txtValorCompra.Text = txtValorCompra.Text.Replace(".", "")
            txtValorCompra.Text = txtValorCompra.Text.Replace(",", ".")

            txtValorMinimo.Text = txtValorMinimo.Text.Replace(".", "")
            txtValorMinimo.Text = txtValorMinimo.Text.Replace(",", ".")

            txtM3Inicial.Text = txtM3Inicial.Text.Replace(".", "")
            txtM3Inicial.Text = txtM3Inicial.Text.Replace(",", ".")

            txtM3Final.Text = txtM3Final.Text.Replace(".", "")
            txtM3Final.Text = txtM3Final.Text.Replace(",", ".")

            txtValorTaxaCompra.Text = txtValorTaxaCompra.Text.Replace(".", "")
            txtValorTaxaCompra.Text = txtValorTaxaCompra.Text.Replace(",", ".")

            txtValorTaxaVenda.Text = txtValorTaxaVenda.Text.Replace(".", "")
            txtValorTaxaVenda.Text = txtValorTaxaVenda.Text.Replace(",", ".")

            txtValorTaxaVendaMin.Text = txtValorTaxaVendaMin.Text.Replace(".", "")
            txtValorTaxaVendaMin.Text = txtValorTaxaVendaMin.Text.Replace(",", ".")




            Dim TaxasIncluded As String

            If txtTaxas.Text = "" Then
                TaxasIncluded = " NULL "
            Else
                TaxasIncluded = " '" & txtTaxas.Text & "' "
            End If

            If txtID_FreteTransportador.Text = "" Then



                ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                        diverro.Visible = True
                        lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
                        Exit Sub

                    Else
                        'INSETE FRETE TRANSPORTADOR
                        ds = Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR ( ID_TRANSPORTADOR, ID_AGENTE, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_PORTO_ESCALA,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3, ID_MOEDA_FRETE, ID_TIPO_CARGA, ID_VIA_ROTA, ID_TIPO_COMEX, QT_DIAS_TRANSITTIME_INICIAL, QT_DIAS_TRANSITTIME_FINAL, QT_DIAS_TRANSITTIME_MEDIA, ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, FL_ATIVO, DT_VALIDADE_FINAL) VALUES (" & ddlTransportador.SelectedValue & "," & ddlAgente.SelectedValue & "," & ddlOrigem.SelectedValue & " ," & ddlDestino.SelectedValue & ", " & ddlEscala1.SelectedValue & ", " & ddlEscala2.SelectedValue & ", " & ddlEscala3.SelectedValue & "," & ddlMoeda.SelectedValue & ", " & ddlTipoCarga.SelectedValue & ", " & ddlRota.SelectedValue & ", " & ddlComex.SelectedValue & ",'" & txtTransittimeInicial.Text & "','" & txtTransittimeFinal.Text & "','" & TTMedia & "'," & ddlFrequencia.SelectedIndex & "," & txtValorFrequencia.Text & "," & TaxasIncluded & ",'" & ckbAtivo.Checked & "',convert(date,'" & txtValidadeFinal.Text & "',103) ) Select SCOPE_IDENTITY() as ID_FRETE_TRANSPORTADOR ")

                        'PREENCHE SESSÃO E CAMPOS DO TARIFARIO E DA TAXA COM O ID DO NOVO FRETE TRANSPORTADOR
                        Session("ID_FRETE_TRANSPORTADOR") = ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString()
                        txtFreteTransportadorTarifario.Text = Session("ID_FRETE_TRANSPORTADOR")
                        txtFreteTransportadorTaxa.Text = Session("ID_FRETE_TRANSPORTADOR")



                        'VERIFIFA SE HÁ TARIFARIO EM ABERTO
                        ds = Con.ExecutarQuery("SELECT ID_TARIFARIO_FRETE_TRANSPORTADOR FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_TIPO_CONTAINER = " & ddlContainer.SelectedValue & " AND DT_VALIDADE_INICIAL >= convert(date,'" & txtValidadeInicial.Text & "',103) and ID_FRETE_TRANSPORTADOR =" & txtFreteTransportadorTarifario.Text)
                        If ds.Tables(0).Rows.Count > 0 Then
                            Dim data As DateTime = txtValidadeInicial.Text
                            data.AddDays(-1)
                            'ALTERA DATA DO TAFIFARIO EM ABERTO
                            For Each linha As DataRow In ds.Tables(0).Rows
                                Dim FreteTransportador As Integer = linha.Item("ID_TARIFARIO_FRETE_TRANSPORTADOR").ToString()
                                Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR SET DT_VALIDADE_INICIAL = convert(date,'" & data & "',103) WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = '" & FreteTransportador)
                            Next

                        End If

                        If txtservico.Text = "" Then
                            txtservico.Text = "NULL"
                        Else
                            txtservico.Text = "'" & txtservico.Text & "'"
                        End If

                        'VERIFICA TIPO DE ESTUFAGEM
                        If Session("ESTUFAGEM") = 2 Then

                            'INSERE TARIFARIO
                            ds = Con.ExecutarQuery("INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR,ID_TIPO_ESTUFAGEM, VL_COMPRA, VL_MINIMO,FL_IMO,FL_CARGA_ESPECIAL,VL_M3_INICIAL,VL_M3_FINAL,ID_MERCADORIA,SERVICO ) VALUES (" & txtFreteTransportadorTarifario.Text & "," & Session("ESTUFAGEM") & ",'" & txtValorCompra.Text & "','" & txtValorMinimo.Text & "','" & ckbIMO.Checked & "','" & ckbCargaEspecial.Checked & "','" & txtM3Inicial.Text & "', '" & txtM3Final.Text & "', " & ddlMercadoria.SelectedValue & ", " & txtservico.Text & " )")
                        Else
                            'INSERE TARIFARIO
                            ds = Con.ExecutarQuery("INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR, ID_TIPO_CONTAINER,ID_TIPO_ESTUFAGEM,DT_VALIDADE_INICIAL, VL_COMPRA, QT_DIAS_FREETIME,FL_IMO,FL_CARGA_ESPECIAL,ID_MERCADORIA,SERVICO ) VALUES (" & txtFreteTransportadorTarifario.Text & "," & ddlContainer.SelectedValue & "," & Session("ESTUFAGEM") & " , convert(date,'" & txtValidadeInicial.Text & "',103),'" & txtValorCompra.Text & "' ," & txtFreetime.Text & ",'" & ckbIMO.Checked & "','" & ckbCargaEspecial.Checked & "', " & ddlMercadoria.SelectedValue & ", " & txtservico.Text & ")")
                        End If



                        'INSERE TAXAS
                        ds = Con.ExecutarQuery("INSERT INTO TB_TABELA_FRETE_TAXA ( ID_FRETE_TRANSPORTADOR,ID_TIPO_ESTUFAGEM,ID_TIPO_ITEM_DESPESA,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_MIN) VALUES (" & txtFreteTransportadorTaxa.Text & "," & Session("ESTUFAGEM") & " ," & ddlItemDespesa.SelectedValue & "," & ddlOrigemPagamento.SelectedValue & "," & ddlBaseCalculoTaxa.Text & "," & ddlMoedaCompra.Text & ",'" & txtValorTaxaCompra.Text & "'," & ddlMoedaVenda.SelectedValue & ",'" & txtValorTaxaVenda.Text & "', '" & txtValorTaxaVendaMin.Text & "' )")


                        Call Limpar(Me)
                        Con.Fechar()
                        divsuccess.Visible = True

                    End If
                Else
                    diverro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                End If


            Else

                ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                        diverro.Visible = True
                        lblmsgErro.Text = "Usuário não possui permissão para alterar."
                        Exit Sub

                    Else

                        'REALIZA UPDATE DO FRETE TRANSPORTADOR
                        ds = Con.ExecutarQuery("UPDATE TB_FRETE_TRANSPORTADOR  SET  ID_TRANSPORTADOR = " & ddlTransportador.SelectedValue & ", ID_AGENTE = " & ddlAgente.SelectedValue & ", ID_PORTO_ORIGEM = " & ddlOrigem.SelectedValue & " , ID_PORTO_DESTINO = " & ddlDestino.SelectedValue & ", ID_PORTO_ESCALA = " & ddlEscala1.SelectedValue & ", ID_PORTO_ESCALA2 = " & ddlEscala2.SelectedValue & ",ID_PORTO_ESCALA3 = " & ddlEscala3.SelectedValue & ", ID_MOEDA_FRETE" & ddlMoeda.SelectedValue & ", ID_TIPO_CARGA = " & ddlTipoCarga.SelectedValue & ", ID_VIA_ROTA =  " & ddlRota.SelectedValue & ", ID_TIPO_COMEX = " & ddlComex.SelectedValue & ", QT_DIAS_TRANSITTIME_INICIAL =  " & txtTransittimeInicial.Text & ", QT_DIAS_TRANSITTIME_FINAL = " & txtTransittimeFinal.Text & ", QT_DIAS_TRANSITTIME_MEDIA = '" & TTMedia & "', ID_TIPO_FREQUENCIA = " & ddlFrequencia.SelectedValue & ", VL_FREQUENCIA = '" & txtValorFrequencia.Text & "', NM_TAXAS_INCLUDED =  " & TaxasIncluded & ", FL_ATIVO = " & ckbAtivo.Checked & ", DT_VALIDADE_FINAL = convert(date,'" & txtValidadeFinal.Text & "',103)) WHERE ID_FRETE_TRANSPORTADOR = " & txtID_FreteTransportador.Text)



                        If txtservico.Text = "" Then
                            txtservico.Text = "NULL"
                        Else
                            txtservico.Text = "'" & txtservico.Text & "'"
                        End If

                        'VERIFICA TIPO DE ESTUFAGEM
                        If Session("ESTUFAGEM") = 2 Then
                            'ALTERA TARIFARIO
                            ds = Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR SET ID_FRETE_TRANSPORTADOR =  " & txtFreteTransportadorTarifario.Text & ",ID_TIPO_ESTUFAGEM = " & Session("ESTUFAGEM") & ",VL_COMPRA = '" & txtValorCompra.Text & "', VL_MINIMO = '" & txtValorMinimo.Text & "',FL_IMO = '" & ckbIMO.Checked & "',FL_CARGA_ESPECIAL = '" & ckbCargaEspecial.Checked & "', VL_M3_INICIAL = '" & txtM3Inicial.Text & "', VL_M3_FINAL = '" & txtM3Final.Text & "', ID_MERCADORIA = " & ddlMercadoria.SelectedValue & ", SERVICO = " & txtservico.Text & " WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & txtIDTarifario.Text)
                        Else
                            'ALTERA TARIFARIO
                            ds = Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR SET ID_FRETE_TRANSPORTADOR =  " & txtFreteTransportadorTarifario.Text & ", ID_TIPO_CONTAINER = " & ddlContainer.SelectedValue & " ,ID_TIPO_ESTUFAGEM = " & Session("ESTUFAGEM") & ",DT_VALIDADE_INICIAL = convert(date,'" & txtValidadeInicial.Text & "',103), VL_COMPRA = '" & txtValorCompra.Text & "', QT_DIAS_FREETIME = " & txtFreetime.Text & ",FL_IMO = '" & ckbIMO.Checked & "',FL_CARGA_ESPECIAL =  '" & ckbCargaEspecial.Checked & "', ID_MERCADORIA = " & ddlMercadoria.SelectedValue & ", SERVICO = " & txtservico.Text & " WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & txtIDTarifario.Text)
                        End If

                        'ALTERA TAXAS
                        ds = Con.ExecutarQuery("UPDATE TB_TABELA_FRETE_TAXA SET ID_FRETE_TRANSPORTADOR =  " & txtFreteTransportadorTaxa.Text & ", ID_TIPO_ESTUFAGEM = " & Session("ESTUFAGEM") & ", ID_TIPO_ITEM_DESPESA =  " & ddlItemDespesa.SelectedValue & " , ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento.SelectedValue & ",ID_BASE_CALCULO_TAXA =  " & ddlBaseCalculoTaxa.SelectedValue & " ,ID_MOEDA_COMPRA =  " & ddlMoedaCompra.SelectedValue & ",VL_TAXA_COMPRA = '" & txtValorTaxaCompra.Text & "',ID_MOEDA_VENDA = " & ddlMoedaVenda.SelectedValue & ",VL_TAXA_VENDA = '" & txtValorTaxaVenda.Text & "', VL_TAXA_VENDA_MIN = '" & txtValorTaxaVendaMin.Text & "' WHERE ID_TABELA_FRETE_TAXA = " & txtIDTaxa.Text)

                        divsuccess.Visible = True
                        Con.Fechar()


                    End If

                    diverro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                End If





            End If


        End If



    End Sub



    'Sub FreteTarifario()


    '    Dim Con As New Conexao_sql
    '    Con.Conectar()
    '    Dim ds As DataSet







    '    If txtIDTarifario.Text = "" Then


    '        ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
    '        If ds.Tables(0).Rows.Count > 0 Then

    '            If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
    '                diverro.Visible = True
    '                lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
    '                Exit Sub

    '            Else

    '                ds = Con.ExecutarQuery("SELECT ID_TARIFARIO_FRETE_TRANSPORTADOR FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_TIPO_CONTAINER = " & ddlContainer.SelectedValue & " AND DT_VALIDADE_INICIAL >= convert(date,'" & txtValidadeInicial.Text & "',103)")
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    Dim data As DateTime = txtValidadeInicial.Text
    '                    data.AddDays(-1)

    '                    For Each linha As DataRow In ds.Tables(0).Rows
    '                        Dim FreteTransportador As Integer = ds.Tables(0).Rows(0).Item("ID_TARIFARIO_FRETE_TRANSPORTADOR")
    '                        Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR SET DT_VALIDADE_INICIAL = convert(date,'" & data & "',103) WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = '" & FreteTransportador)
    '                    Next

    '                End If

    '                If txtservico.Text = "" Then
    '                    txtservico.Text = " NULL"
    '                Else
    '                    txtservico.Text = "'" & txtservico.Text & "'"
    '                End If

    '                If Session("ESTUFAGEM") = 2 Then
    '                    ds = Con.ExecutarQuery("INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR,ID_TIPO_ESTUFAGEM, VL_COMPRA, VL_MINIMO,FL_IMO,FL_CARGA_ESPECIAL,VL_M3_INICIAL,VL_M3_FINAL,ID_MERCADORIA,SERVICO ) VALUES (" & txtFreteTransportadorTarifario.Text & ",," & Session("ESTUFAGEM") & ",'" & txtValorCompra.Text & "','" & txtValorMinimo.Text & "','" & ckbIMO.Checked & "','" & ckbCargaEspecial.Checked & "','" & txtM3Inicial.Text & "', '" & txtM3Final.Text & "', " & ddlMercadoria.SelectedValue & ", " & txtservico.Text & " )")
    '                    divsuccess.Visible = True
    '                Else

    '                    ds = Con.ExecutarQuery("INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR, ID_TIPO_CONTAINER,ID_TIPO_ESTUFAGEM,DT_VALIDADE_INICIAL, VL_COMPRA, QT_DIAS_FREETIME,FL_IMO,FL_CARGA_ESPECIAL,ID_MERCADORIA,SERVICO ) VALUES (" & txtFreteTransportadorTarifario.Text & "," & ddlContainer.SelectedValue & "," & Session("ESTUFAGEM") & " , convert(date,'" & txtValidadeInicial.Text & "',103),'" & txtValorCompra.Text & "' ," & txtFreetime.Text & ",'" & ckbIMO.Checked & "','" & ckbCargaEspecial.Checked & "', " & ddlMercadoria.SelectedValue & ", " & txtservico.Text & ")")
    '                    divsuccess.Visible = True
    '                End If



    '            End If
    '        Else
    '            diverro.Visible = True
    '            lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
    '            Exit Sub

    '        End If


    '    Else

    '        ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
    '        If ds.Tables(0).Rows.Count > 0 Then

    '            If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
    '                diverro.Visible = True
    '                lblmsgErro.Text = "Usuário não possui permissão para alterar."
    '                Exit Sub

    '            Else

    '                If txtservico.Text = "" Then
    '                    txtservico.Text = " NULL"
    '                Else
    '                    txtservico.Text = "'" & txtservico.Text & "'"
    '                End If


    '                If Session("ESTUFAGEM") = 2 Then
    '                    ds = Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR SET ID_FRETE_TRANSPORTADOR =  " & txtFreteTransportadorTarifario.Text & ",ID_TIPO_ESTUFAGEM = " & Session("ESTUFAGEM") & ",VL_COMPRA = '" & txtValorCompra.Text & "', VL_MINIMO = '" & txtValorMinimo.Text & "',FL_IMO = '" & ckbIMO.Checked & "',FL_CARGA_ESPECIAL = '" & ckbCargaEspecial.Checked & "', VL_M3_INICIAL = '" & txtM3Inicial.Text & "', VL_M3_FINAL = '" & txtM3Final.Text & "', ID_MERCADORIA = " & ddlMercadoria.SelectedValue & ", SERVICO = " & txtservico.Text & " WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & txtIDTarifario.Text)
    '                    divsuccess.Visible = True
    '                Else

    '                    ds = Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR SET ID_FRETE_TRANSPORTADOR =  " & txtFreteTransportadorTarifario.Text & ", ID_TIPO_CONTAINER = " & ddlContainer.SelectedValue & " ,ID_TIPO_ESTUFAGEM = " & Session("ESTUFAGEM") & ",DT_VALIDADE_INICIAL = convert(date,'" & txtValidadeInicial.Text & "',103), VL_COMPRA = '" & txtValorCompra.Text & "', QT_DIAS_FREETIME = " & txtFreetime.Text & ",FL_IMO = '" & ckbIMO.Checked & "',FL_CARGA_ESPECIAL =  '" & ckbCargaEspecial.Checked & "', ID_MERCADORIA = " & ddlMercadoria.SelectedValue & ", SERVICO = " & txtservico.Text & " WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & txtIDTarifario.Text)
    '                    divsuccess.Visible = True
    '                End If



    '            End If
    '        Else

    '            diverro.Visible = True
    '            lblmsgErro.Text = "Usuário não possui permissão para alterar."
    '            Exit Sub

    '        End If


    '    End If





    'End Sub

    'Sub Taxas()


    '    Dim Con As New Conexao_sql
    '    Con.Conectar()
    '    Dim ds As DataSet




    '    If txtIDTaxa.Text = "" Then


    '        ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
    '        If ds.Tables(0).Rows.Count > 0 Then

    '            If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
    '                diverro.Visible = True
    '                lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
    '            Else

    '                ds = Con.ExecutarQuery("INSERT INTO TB_TABELA_FRETE_TAXA ( ID_FRETE_TRANSPORTADOR,ID_TIPO_ESTUFAGEM,ID_TIPO_ITEM_DESPESA,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_MIN) VALUES (" & txtFreteTransportadorTaxa.Text & "," & Session("ESTUFAGEM") & " ," & ddlItemDespesa.SelectedValue & "'," & ddlOrigemPagamento.SelectedValue & "," & ddlBaseCalculoTaxa.Text & "," & ddlMoedaCompra.Text & "','" & txtValorTaxaCompra.Text & "','" & ddlMoedaVenda.SelectedValue & "','" & txtValorTaxaVenda.Text & "', '" & txtValorTaxaVendaMin.Text & "' )")
    '                divsuccess.Visible = True
    '            End If
    '        Else
    '            diverro.Visible = True
    '            lblmsgErro.Text = "Usuário não possui permissão para cadastrar."
    '        End If




    '    Else

    '        ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
    '        If ds.Tables(0).Rows.Count > 0 Then

    '            If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
    '                diverro.Visible = True
    '                lblmsgErro.Text = "Usuário não possui permissão para alterar."
    '                Exit Sub

    '            Else
    '                ds = Con.ExecutarQuery("UPDATE TB_TABELA_FRETE_TAXA SET ID_FRETE_TRANSPORTADOR =  " & txtFreteTransportadorTaxa.Text & ", ID_TIPO_ESTUFAGEM = " & Session("ESTUFAGEM") & ", ID_TIPO_ITEM_DESPESA =  " & ddlItemDespesa.SelectedValue & " , ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento.SelectedValue & ",ID_BASE_CALCULO_TAXA =  " & ddlBaseCalculoTaxa.SelectedValue & " ,ID_MOEDA_COMPRA =  " & ddlMoedaCompra.SelectedValue & ",VL_TAXA_COMPRA = '" & txtValorTaxaCompra.Text & "',ID_MOEDA_VENDA = " & ddlMoedaVenda.SelectedValue & ",VL_TAXA_VENDA = '" & txtValorTaxaVenda.Text & "', VL_TAXA_VENDA_MIN = '" & txtValorTaxaVendaMin.Text & "' WHERE ID_TABELA_FRETE_TAXA = " & txtIDTaxa.Text)
    '                divsuccess.Visible = True


    '            End If
    '        Else

    '            diverro.Visible = True
    '            lblmsgErro.Text = "Usuário não possui permissão para alterar."
    '            Exit Sub

    '        End If


    '    End If



    'End Sub

    Public Sub Limpar(ByVal controlP As Control)

        Dim ctl As Control

        For Each ctl In controlP.Controls

            If TypeOf ctl Is TextBox Then

                DirectCast(ctl, TextBox).Text = String.Empty

            ElseIf ctl.Controls.Count > 0 Then

                Limpar(ctl)

            End If

        Next

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
            divMercadoriaServico.Visible = True
            divMercadoriaServico.Attributes.CssStyle.Add("display", "block")

        Else
            divMercadoriaServico.Visible = False
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

    'Private Function GetSortDirectionTaxas(ByVal column As String) As String
    '    Dim sortDirection As String = "ASC"
    '    Dim sortExpression As String = TryCast(ViewState("SortExpression"), String)

    '    If sortExpression IsNot Nothing Then

    '        If sortExpression = column Then
    '            Dim lastDirection As String = TryCast(ViewState("SortDirection"), String)

    '            If (lastDirection IsNot Nothing) AndAlso (lastDirection = "ASC") Then
    '                sortDirection = "DESC"
    '            End If
    '        End If
    '    End If

    '    ViewState("SortDirection") = sortDirection
    '    ViewState("SortExpression") = column
    '    Return sortDirection
    'End Function


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
        divMsgErro.Visible = False
        If e.CommandName = "Excluir" Then

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ID As String = e.CommandArgument

            Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_EXCLUIR FROM [TB_GRUPO_PERMISSAO] WHERE ID_MENU = 1024 AND ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_EXCLUIR").ToString() = True Then
                    Con.ExecutarQuery("DELETE From TB_TARIFARIO_FRETE_TRANSPORTADOR Where ID_TARIFARIO_FRETE_TRANSPORTADOR = " & ID)
                    lblMsgExcluir.Text = "Registro deletado!"
                    divMsgExcluir.Visible = True
                    dgvFreteTarifario.DataBind()

                Else
                    lblmsgErro.Text = "Usuário não tem permissão para realizar exclusões"
                    divMsgErro.Visible = True
                End If
            End If
            Con.Fechar()

        End If

    End Sub


    Private Sub dgvTaxas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxas.RowCommand
        divDeleteTaxas.Visible = False
        divDeleteErro.Visible = False
        If e.CommandName = "Excluir" Then

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ID As String = e.CommandArgument

            Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_EXCLUIR FROM [TB_GRUPO_PERMISSAO] WHERE ID_MENU = 1024 AND ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_EXCLUIR").ToString() = True Then
                    Con.ExecutarQuery("DELETE From TB_TABELA_FRETE_TAXA Where ID_TABELA_FRETE_TAXA = " & ID)
                    lblDeleteTaxas.Text = "Registro deletado!"
                    divDeleteTaxas.Visible = True
                    dgvFreteTarifario.DataBind()

                Else
                    lblDeleteErro.Text = "Usuário não tem permissão para realizar exclusões"
                    divDeleteErro.Visible = True
                End If
            End If
            Con.Fechar()

        End If

    End Sub


    'Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click

    '    Dim guid = hddnGUID.Value

    '    If String.IsNullOrEmpty(guid) Then
    '        ModelState.AddModelError(String.Empty, "Ocorreu um problema ao exportar arquivo")
    '    End If

    '    If Not ModelState.IsValid Then Return
    '    Dim _notaFiscalDAO = New NotaFiscalDAO()
    '    Dim notas = _notaFiscalDAO.ObterNFsImportadasPorGUID(guid)
    '    Dim epackage As ExcelPackage = New ExcelPackage()
    '    Dim excel As ExcelWorksheet = epackage.Workbook.Worksheets.Add("CCT")
    '    excel.Cells("A1").LoadFromCollection(notas.[Select](Function(c) New With {c.DataRegistro, c.ChaveNF, c.SaldoCCT, c.PesoEntradaCCT, c.PesoAferido, c.Observacoes, c.Recinto, c.UnidadeReceita, c.Item, c.DUE, c.QtdeAverbada
    '}), True)
    '    Dim attachment As String = $"attachment; filename=ArquivoNotas.xlsx"
    '    HttpContext.Current.Response.Clear()
    '    HttpContext.Current.Response.ClearHeaders()
    '    HttpContext.Current.Response.ClearContent()
    '    HttpContext.Current.Response.AddHeader("content-disposition", attachment)
    '    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    '    HttpContext.Current.Response.BinaryWrite(epackage.GetAsByteArray())
    '    HttpContext.Current.Response.[End]()
    '    epackage.Dispose()

    'End Sub
End Class