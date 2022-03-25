Imports Newtonsoft.Json

Public Class CadastrarMaster
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

            If Not Page.IsPostBack And Request.QueryString("id") <> "" Then

                CarregaCampos()
                PermissoesEspeciais()


                Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD
from TB_BL_TAXA A 
INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
INNER JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 
WHERE  DT_CANCELAMENTO IS NULL and ID_BL_TAXA_MASTER in (select ID_BL_TAXA
from TB_BL_TAXA 
WHERE ID_BL=" & Request.QueryString("id") & ")")
                If ds2.Tables(0).Rows(0).Item("QTD") > 0 Then
                    'MARITIMO
                    '  btnGravar_BasicoMaritimo.Enabled = False
                    'btnSalvar_CNTRMaritimo.Visible = False
                    ddlTipoContainer_CNTRMaritimo.Enabled = False
                    txtTara_CNTRMaritimo.Enabled = False
                    btnNovoCNTRMaritimo.Enabled = False
                    '  btnNovaTaxasMaritimo.Enabled = False
                    ' btnSalvar_TaxasMaritimo.Visible = False
                    btnDesvincular.Visible = False
                    btnVincular.Visible = False
                    dgvContainer.Columns(7).Visible = False
                    dgvContainer.Columns(8).Visible = False
                    dgvContainer.Columns(7).Visible = False
                    dgvContainer.Columns(8).Visible = False
                    'dgvTaxasMaritimo.Columns(9).Visible = False
                    'dgvTaxasMaritimo.Columns(10).Visible = False


                    'AEREO
                    '  btnGravar_BasicoAereo.Enabled = False
                    'btnNovaTaxaAereo.Enabled = False
                    ' btnSalvar_TaxaAereo.Visible = False
                    'dgvTaxasAereo.Columns(9).Visible = False
                    'dgvTaxasAereo.Columns(10).Visible = False

                End If

                ds2 = Con.ExecutarQuery("SELECT count(*)QTD from TB_BL WHERE ID_BL_MASTER =" & Request.QueryString("id"))
                If ds2.Tables(0).Rows(0).Item("QTD") = 0 Then
                    'MARITIMO
                    btnNovaTaxasMaritimo.Enabled = False

                    'AEREO
                    btnGravar_BasicoAereo.Enabled = False

                End If


            End If

            If Not Page.IsPostBack And Request.QueryString("s") = "a" Then

                ddlServico_BasicoAereo.Text = Session("ddlServico_BasicoAereo")
                ddltransportador_BasicoAereo.SelectedValue = Session("ddlTransportador_BasicoAereo")
                ddlOrigem_BasicoAereo.SelectedValue = Session("ddlOrigem_BasicoAereo")
                ddlDestino_BasicoAereo.SelectedValue = Session("ddlDestino_BasicoAereo")
                ddlAgente_BasicoAereo.SelectedValue = Session("ddlAgente_BasicoAereo")
                ddlTipoPagamento_BasicoAereo.SelectedValue = Session("ddlTipoPagamento_BasicoAereo")
                ddlEstufagem_BasicoAereo.SelectedValue = Session("ddlEstufagem_BasicoAereo")


                txtCotacao_BasicoAereo.Text = Session("ID_COTACAO")
                ddlStatusFreteAgente_BasicoAereo.SelectedValue = Session("ID_STATUS_FRETE_AGENTE")




            ElseIf Not Page.IsPostBack And Request.QueryString("s") = "m" Then



                ddlServico_BasicoMaritimo.Text = Session("ddlServico_BasicoMaritimo")
                ddlTransportador_BasicoMaritimo.SelectedValue = Session("ddlTransportador_BasicoMaritimo")
                ddlOrigem_BasicoMaritimo.SelectedValue = Session("ddlOrigem_BasicoMaritimo")
                ddlDestino_BasicoMaritimo.SelectedValue = Session("ddlDestino_BasicoMaritimo")
                ddlAgente_BasicoMaritimo.SelectedValue = Session("ddlAgente_BasicoMaritimo")
                ddlTipoPagamento_BasicoMaritimo.SelectedValue = Session("ddlTipoPagamento_BasicoMaritimo")
                ddlEstufagem_BasicoMaritimo.SelectedValue = Session("ddlEstufagem_BasicoMaritimo")


                txtCotacao_BasicoMaritimo.Text = Session("ID_COTACAO")
                ddlStatusFreteAgente_BasicoMaritimo.SelectedValue = Session("ID_STATUS_FRETE_AGENTE")



            End If

        End If




        Con.Fechar()


    End Sub

    Sub CarregaCampos()
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_BL,ID_SERVICO,ID_WEEK,ID_BL_MASTER,ISNULL(NR_BL,0)NR_BL,NR_PROCESSO,ID_PARCEIRO_TRANSPORTADOR, YEAR(DT_ABERTURA)ANO_ABERTURA,ID_COTACAO ,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_PARCEIRO_AGENTE,ID_TIPO_PAGAMENTO,ID_TIPO_CARGA,ID_TIPO_ESTUFAGEM,NR_CE,CONVERT(varchar,DT_CE,103)DT_CE,CONVERT(varchar,DT_EMISSAO_BL,103)DT_EMISSAO_BL, CONVERT(varchar,DT_PREVISAO_EMBARQUE,103)DT_PREVISAO_EMBARQUE,CONVERT(varchar,DT_PREVISAO_CHEGADA,103)DT_PREVISAO_CHEGADA,CONVERT(varchar,DT_CHEGADA,103)DT_CHEGADA,CONVERT(varchar,DT_EMBARQUE,103)DT_EMBARQUE,
NM_RESUMO_MERCADORIA,OB_CLIENTE,OB_AGENTE_INTERNACIONAL,OB_COMERCIAL,OB_OPERACIONAL_INTERNA,NR_VIAGEM,NR_VIAGEM_1T,NR_VIAGEM_2T,NR_VIAGEM_3T,ID_NAVIO,ID_NAVIO_1T, ID_NAVIO_2T,ID_NAVIO_3T, DT_1T, DT_2T, DT_3T, ID_PORTO_1T,ID_PORTO_3T,ID_PORTO_2T,ID_PARCEIRO_AGENCIA,ID_MOEDA_FRETE,VL_FRETE,VL_PESO_BRUTO,VL_PESO_TAXADO,QT_MERCADORIA,CONVERT(varchar,DT_EMISSAO_CONHECIMENTO,103)DT_EMISSAO_CONHECIMENTO,VL_TARIFA_MASTER,VL_TARIFA_MASTER_MINIMA,ID_PARCEIRO_ARMAZEM_ATRACACAO,ID_PARCEIRO_ARMAZEM_DESCARGA,(SELECT NM_NAVIO FROM TB_NAVIO B WHERE B.ID_NAVIO = A.ID_NAVIO) NM_NAVIO,(SELECT NM_NAVIO FROM TB_NAVIO B WHERE B.ID_NAVIO = A.ID_NAVIO_1T) NM_NAVIO1,(SELECT NM_NAVIO FROM TB_NAVIO B WHERE B.ID_NAVIO = A.ID_NAVIO_2T) NM_NAVIO2,(SELECT NM_NAVIO FROM TB_NAVIO B WHERE B.ID_NAVIO = A.ID_NAVIO_3T) NM_NAVIO3,ID_STATUS_FRETE_AGENTE 
FROM TB_BL A where ID_BL =" & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
                    'AGENCIAMENTO DE IMPORTACAO MARITIMA
                    'AGENCIAMENTO DE EXPORTAÇÃO MARITIMA

                    If ds.Tables(0).Rows(0).Item("ANO_ABERTURA") >= 2022 And ds.Tables(0).Rows(0).Item("NR_BL") <> "0" Then
                        Dim Rastreio As New RastreioService
                        Rastreio.trackingbl(Request.QueryString("id"))
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_COTACAO")) Then
                        txtCotacao_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                        txtProcesso_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                        ddlServico_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        txtNumeroBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                        lblMaster_Titulo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EMISSAO_BL")) Then
                        txtEmissaoBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_EMISSAO_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                        ddlEstufagem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM")) Then
                        txtNumeroViagem_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        txtCodTransportador_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                        ddlTransportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_ATRACACAO")) Then
                        txtCodArmazemAtracacao_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_ATRACACAO")
                        ddlArmazemAtracacao_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_ATRACACAO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_DESCARGA")) Then
                        txtCodArmazemDescarga_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_DESCARGA")
                        ddlArmazemDescarga_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_DESCARGA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENCIA")) Then
                        txtCodAgenciaMaritima_Maritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENCIA")
                        ddlAgenciaMaritima_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENCIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NAVIO")) Then
                        If ds.Tables(0).Rows(0).Item("ID_NAVIO") > 0 Then
                            ddlNavio_BasicoMaritimo.Items.Insert(1, ds.Tables(0).Rows(0).Item("ID_NAVIO") & " - " & ds.Tables(0).Rows(0).Item("NM_NAVIO"))
                            ddlNavio_BasicoMaritimo.SelectedIndex = 1
                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM_1T")) Then
                        txtViagem1_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_1T")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NAVIO_1T")) Then
                        If ds.Tables(0).Rows(0).Item("ID_NAVIO_1T") > 0 Then
                            ddlNavio1_BasicoMaritimo.Items.Insert(1, ds.Tables(0).Rows(0).Item("ID_NAVIO_1T") & " - " & ds.Tables(0).Rows(0).Item("NM_NAVIO1"))
                            ddlNavio1_BasicoMaritimo.SelectedIndex = 1
                        End If

                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_1T")) Then
                        txtData1_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_1T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_1T")) Then
                        ddlPorto1_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_1T")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM_2T")) Then
                        txtViagem2_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_2T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NAVIO_2T")) Then
                        If ds.Tables(0).Rows(0).Item("ID_NAVIO_2T") > 0 Then
                            ddlNavio2_BasicoMaritimo.Items.Insert(1, ds.Tables(0).Rows(0).Item("ID_NAVIO_2T") & " - " & ds.Tables(0).Rows(0).Item("NM_NAVIO2"))
                            ddlNavio2_BasicoMaritimo.SelectedIndex = 1
                        End If

                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_2T")) Then
                        txtData2_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_2T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_2T")) Then
                        ddlPorto2_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_2T")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM_3T")) Then
                        txtViagem3_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_3T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NAVIO_3T")) Then
                        If ds.Tables(0).Rows(0).Item("ID_NAVIO_3T") > 0 Then
                            ddlNavio3_BasicoMaritimo.Items.Insert(1, ds.Tables(0).Rows(0).Item("ID_NAVIO_3T") & " - " & ds.Tables(0).Rows(0).Item("NM_NAVIO3"))
                            ddlNavio3_BasicoMaritimo.SelectedIndex = 1
                        End If
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_3T")) Then
                        txtData3_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_3T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_3T")) Then
                        ddlPorto3_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_3T")
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_PREVISAO_CHEGADA")) Then
                        txtPrevisaoChegada_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_PREVISAO_CHEGADA")

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CHEGADA")) Then
                        txtChegada_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_CHEGADA")
                        btnGravar_BasicoMaritimo.Visible = False
                        btnLimpar_BasicoMaritimo.Visible = False
                        btnNovaTaxasMaritimo.Visible = False
                        btnSalvar_TaxasMaritimo.Visible = False
                        btnNovoCNTRMaritimo.Visible = False
                        btnSalvar_CNTRMaritimo.Visible = False
                        btnVincular.Visible = False
                        btnDesvincular.Visible = False

                        PermissoesEspeciais()

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_PREVISAO_EMBARQUE")) Then
                        txtPrevisaoEmbarque_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_PREVISAO_EMBARQUE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EMBARQUE")) Then
                        txtEmbarque_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_EMBARQUE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                        ddlOrigem_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                        ddlDestino_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")) Then
                        ddlAgente_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                        ddlEstufagem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                            txtTarifaMasterMin_BasicoMaritimo.Enabled = False
                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TARIFA_MASTER_MINIMA")) Then
                        txtTarifaMasterMin_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TARIFA_MASTER_MINIMA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")) Then
                        ddlStatusFreteAgente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")
                    Else
                        ddlStatusFreteAgente_BasicoMaritimo.SelectedValue = 0
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CE")) Then
                        txtCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) Then
                        txtDataCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_WEEK")) Then

                        Dim sql As String = "SELECT ID_WEEK, NM_WEEK FROM TB_WEEK WHERE ID_PORTO_ORIGEM_DESTINO = " & ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM") & " AND ID_PORTO_ORIGEM_LOCAL = " & ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO") & " OR ID_WEEK = " & ds.Tables(0).Rows(0).Item("ID_WEEK") & "
union SELECT 0, 'Selecione' FROM TB_WEEK ORDER BY ID_WEEK"
                        Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                        If ds1.Tables(0).Rows.Count > 0 Then
                            dsWeekMaritimo.SelectCommand = sql
                            ddlWeekMaritimo.DataBind()
                        End If
                        Con.Fechar()

                        ddlWeekMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_WEEK")
                        Session("ID_WEEK") = ds.Tables(0).Rows(0).Item("ID_WEEK")
                    End If

                    btnGravar_BasicoAereo.Visible = False
                    btnLimpar_BasicoAereo.Visible = False
                    btnNovaTaxaAereo.Visible = False




                ElseIf ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                    'AGENCIAMENTO DE IMPORTACAO AEREO
                    'AGENCIAMENTO DE EXPORTAÇÃO AEREO

                    If ds.Tables(0).Rows(0).Item("ANO_ABERTURA") >= 2022 And ds.Tables(0).Rows(0).Item("NR_BL") <> "0" Then
                        Dim Rastreio As New RastreioService
                        Rastreio.trackingbl(Request.QueryString("id"))
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_COTACAO")) Then
                        txtCotacao_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                        txtProcesso_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                        ddlServico_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                        ddlEstufagem_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        ddltransportador_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                        ddlOrigem_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                        ddlDestino_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")) Then
                        ddlAgente_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        txtNumeroBL_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EMISSAO_CONHECIMENTO")) Then
                        txtDataConhecimento_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_EMISSAO_CONHECIMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        ddltransportador_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM_1T")) Then
                        txtVoo1_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_1T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_1T")) Then
                        txtDataPrevista1_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_1T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_1T")) Then
                        ddlAeroporto1_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_1T")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")) Then
                        ddlStatusFreteAgente_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")
                    Else
                        ddlStatusFreteAgente_BasicoAereo.SelectedValue = 0
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM_2T")) Then
                        txtVoo2_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_2T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_2T")) Then
                        txtDataPrevista2_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_2T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_2T")) Then
                        ddlAeroporto2_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_2T")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM_3T")) Then
                        txtVoo3_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_3T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_3T")) Then
                        txtDataPrevista3_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_3T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_3T")) Then
                        ddlAeroporto3_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_3T")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_PREVISAO_CHEGADA")) Then
                        txtPrevisaoChegada_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_PREVISAO_CHEGADA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CHEGADA")) Then
                        txtChegada_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_CHEGADA")
                        btnGravar_BasicoAereo.Visible = False
                        btnLimpar_BasicoAereo.Visible = False
                        btnNovaTaxaAereo.Visible = False
                        btnSalvar_TaxaAereo.Visible = False

                        PermissoesEspeciais()

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_PREVISAO_EMBARQUE")) Then
                        txtPrevisaoEmbarque_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_PREVISAO_EMBARQUE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EMBARQUE")) Then
                        txtEmbarque_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_EMBARQUE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                        ddlOrigem_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                        ddlDestino_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE")) Then
                        ddlMoedaFrete_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE")
                        ddlMoedaFrete_BasicoAereo.Enabled = False
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TARIFA_MASTER")) Then
                        txtTarifaMaster_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("VL_TARIFA_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_WEEK")) Then

                        Dim sql As String = "SELECT ID_WEEK, NM_WEEK FROM TB_WEEK WHERE ID_PORTO_ORIGEM_DESTINO = " & ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM") & " AND ID_PORTO_ORIGEM_LOCAL = " & ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO") & " OR ID_WEEK = " & ds.Tables(0).Rows(0).Item("ID_WEEK") & "
union SELECT 0, 'Selecione' FROM TB_WEEK ORDER BY ID_WEEK"
                        Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                        If ds1.Tables(0).Rows.Count > 0 Then
                            dsWeekAereo.SelectCommand = sql
                            ddlWeekAereo.DataBind()
                        End If

                        Con.Fechar()
                        ddlWeekAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_WEEK")
                        Session("ID_WEEK") = ds.Tables(0).Rows(0).Item("ID_WEEK")
                    End If

                    btnGravar_BasicoMaritimo.Visible = False
                    btnLimpar_BasicoMaritimo.Visible = False
                    btnNovaTaxasMaritimo.Visible = False
                    btnVincular.Visible = False
                    btnDesvincular.Visible = False
                    btnNovoCNTRMaritimo.Visible = False

                Else
                    'OUTROS                   


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_COTACAO")) Then
                        txtCotacao_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_COTACAO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                        txtProcesso_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                        ddlServico_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        txtNumeroBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                        lblMaster_Titulo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EMISSAO_BL")) Then
                        txtEmissaoBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_EMISSAO_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                        ddlEstufagem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM")) Then
                        txtNumeroViagem_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        ddlTransportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_ATRACACAO")) Then
                        ddlArmazemAtracacao_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_ATRACACAO")

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_DESCARGA")) Then
                        ddlArmazemDescarga_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_DESCARGA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENCIA")) Then
                        ddlAgenciaMaritima_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENCIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NAVIO")) Then
                        If ds.Tables(0).Rows(0).Item("ID_NAVIO") > 0 Then
                            ddlNavio_BasicoMaritimo.Items.Insert(1, ds.Tables(0).Rows(0).Item("ID_NAVIO") & " - " & ds.Tables(0).Rows(0).Item("NM_NAVIO"))
                            ddlNavio_BasicoMaritimo.SelectedIndex = 1
                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM_1T")) Then
                        txtViagem1_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_1T")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NAVIO_1T")) Then
                        If ds.Tables(0).Rows(0).Item("ID_NAVIO_1T") > 0 Then
                            ddlNavio1_BasicoMaritimo.Items.Insert(1, ds.Tables(0).Rows(0).Item("ID_NAVIO_1T") & " - " & ds.Tables(0).Rows(0).Item("NM_NAVIO1"))
                            ddlNavio1_BasicoMaritimo.SelectedIndex = 1
                        End If

                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_1T")) Then
                        txtData1_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_1T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_1T")) Then
                        ddlPorto1_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_1T")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM_2T")) Then
                        txtViagem2_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_2T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NAVIO_2T")) Then
                        If ds.Tables(0).Rows(0).Item("ID_NAVIO_2T") > 0 Then
                            ddlNavio2_BasicoMaritimo.Items.Insert(1, ds.Tables(0).Rows(0).Item("ID_NAVIO_2T") & " - " & ds.Tables(0).Rows(0).Item("NM_NAVIO2"))
                            ddlNavio2_BasicoMaritimo.SelectedIndex = 1
                        End If

                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_2T")) Then
                        txtData2_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_2T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_2T")) Then
                        ddlPorto2_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_2T")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_VIAGEM_3T")) Then
                        txtViagem3_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_3T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NAVIO_3T")) Then
                        If ds.Tables(0).Rows(0).Item("ID_NAVIO_3T") > 0 Then
                            ddlNavio3_BasicoMaritimo.Items.Insert(1, ds.Tables(0).Rows(0).Item("ID_NAVIO_3T") & " - " & ds.Tables(0).Rows(0).Item("NM_NAVIO3"))
                            ddlNavio3_BasicoMaritimo.SelectedIndex = 1
                        End If
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_3T")) Then
                        txtData3_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_3T")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_3T")) Then
                        ddlPorto3_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_3T")
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_PREVISAO_CHEGADA")) Then
                        txtPrevisaoChegada_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_PREVISAO_CHEGADA")

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CHEGADA")) Then
                        txtChegada_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_CHEGADA")
                        btnGravar_BasicoMaritimo.Visible = False
                        btnLimpar_BasicoMaritimo.Visible = False
                        btnNovaTaxasMaritimo.Visible = False
                        btnSalvar_TaxasMaritimo.Visible = False
                        btnNovoCNTRMaritimo.Visible = False
                        btnSalvar_CNTRMaritimo.Visible = False
                        btnVincular.Visible = False
                        btnDesvincular.Visible = False

                        PermissoesEspeciais()

                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_PREVISAO_EMBARQUE")) Then
                        txtPrevisaoEmbarque_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_PREVISAO_EMBARQUE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_EMBARQUE")) Then
                        txtEmbarque_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_EMBARQUE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                        ddlOrigem_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                        ddlDestino_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")) Then
                        ddlAgente_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                        ddlEstufagem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                            txtTarifaMasterMin_BasicoMaritimo.Enabled = False
                        End If
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TARIFA_MASTER_MINIMA")) Then
                        txtTarifaMasterMin_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TARIFA_MASTER_MINIMA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")) Then
                        ddlStatusFreteAgente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE")
                    Else
                        ddlStatusFreteAgente_BasicoMaritimo.SelectedValue = 0
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CE")) Then
                        txtCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) Then
                        txtDataCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_WEEK")) Then

                        Dim sql As String = "SELECT ID_WEEK, NM_WEEK FROM TB_WEEK WHERE ID_PORTO_ORIGEM_DESTINO = " & ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM") & " AND ID_PORTO_ORIGEM_LOCAL = " & ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO") & " OR ID_WEEK = " & ds.Tables(0).Rows(0).Item("ID_WEEK") & "
union SELECT 0, 'Selecione' FROM TB_WEEK ORDER BY ID_WEEK"
                        Dim ds1 As DataSet = Con.ExecutarQuery(sql)
                        If ds1.Tables(0).Rows.Count > 0 Then
                            dsWeekMaritimo.SelectCommand = sql
                            ddlWeekMaritimo.DataBind()
                        End If
                        Con.Fechar()

                        ddlWeekMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_WEEK")
                        Session("ID_WEEK") = ds.Tables(0).Rows(0).Item("ID_WEEK")
                    End If

                    btnGravar_BasicoAereo.Visible = False
                    btnLimpar_BasicoAereo.Visible = False
                    btnNovaTaxaAereo.Visible = False

                End If

            End If

        End If
    End Sub
    Sub PermissoesEspeciais()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_GRAVAR_MASTER_BASICO,FL_GRAVAR_MASTER_CONTAINER,FL_GRAVAR_MASTER_TAXAS,FL_GRAVAR_MASTER_VINCULAR,FL_GRAVAR_HOUSE_BASICO,FL_GRAVAR_HOUSE_CARGA,FL_GRAVAR_HOUSE_TAXAS
FROM TB_USUARIO where ID_USUARIO =" & Session("ID_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_MASTER_BASICO") = True Then
                btnGravar_BasicoAereo.Visible = True
                btnLimpar_BasicoAereo.Visible = True

                btnGravar_BasicoMaritimo.Visible = True
                btnLimpar_BasicoMaritimo.Visible = True
            End If

            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_MASTER_CONTAINER") = True Then
                btnNovoCNTRMaritimo.Visible = True
                btnSalvar_CNTRMaritimo.Visible = True
            End If

            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_MASTER_TAXAS") = True Then
                btnNovaTaxaAereo.Visible = True
                btnSalvar_TaxaAereo.Visible = True

                btnNovaTaxasMaritimo.Visible = True
                btnSalvar_TaxasMaritimo.Visible = True
            End If

            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_MASTER_VINCULAR") = True Then
                btnVincular.Visible = True
                btnDesvincular.Visible = True
            End If

        End If
    End Sub
    Private Sub btnFechar_TaxaAereo_Click(sender As Object, e As EventArgs) Handles btnFechar_TaxaAereo.Click
        divErro_TaxaAereo2.Visible = False
        divSuccess_TaxaAereo2.Visible = False
        ddlDespesa_TaxaAereo.SelectedValue = 0
        ddlTipoPagamento_TaxaAereo.SelectedValue = 0
        ddlOrigemPagamento_TaxasAereo.SelectedValue = 0
        ddlBaseCalculo_TaxaAereo.SelectedValue = 0
        ddlMoedaCompra_TaxaAereo.SelectedValue = 0
        ddlEmpresa_TaxaAereo.SelectedValue = 0
        txtTaxaCompra_TaxaAereo.Text = ""
        txtCalculoCompra_TaxaAereo.Text = ""
        txtMinimoCompra_TaxaAereo.Text = ""
        txtID_TaxaAereo.Text = ""
        txtCodEmpresa_TaxasAereo.Text = ""
        txtNomeEmpresa_TaxasAereo.Text = ""
        'txtCalculoVenda_TaxaAereo.Text = ""
        'txtTaxaVenda_TaxaAereo.Text = ""
        'txtMinimoVenda_TaxaAereo.Text = ""
        Session("CD_PR") = 0
        'divVendaAereo.Visible = True
        divCompraAereo.Visible = True
        btnSalvar_TaxaAereo.Visible = True
        mpeTaxaAereo.Hide()
    End Sub

    Private Sub btnLimpar_BasicoAereo_Click(sender As Object, e As EventArgs) Handles btnLimpar_BasicoAereo.Click
        Response.Redirect("CadastrarMaster.aspx")
    End Sub

    Private Sub btnLimpar_BasicoMaritimo_Click(sender As Object, e As EventArgs) Handles btnLimpar_BasicoMaritimo.Click
        Response.Redirect("CadastrarMaster.aspx")

    End Sub

    Private Sub btnFechar_TaxasMaritimo_Click(sender As Object, e As EventArgs) Handles btnFechar_TaxasMaritimo.Click
        divErro_TaxasMaritimo2.Visible = False
        divSuccess_TaxasMaritimo2.Visible = False
        ddlDespesa_TaxasMaritimo.SelectedValue = 0
        ddlTipoPagamento_TaxasMaritimo.SelectedValue = 0
        ddlOrigemPagamento_TaxasMaritimo.SelectedValue = 0
        ddlBaseCalculo_TaxasMaritimo.SelectedValue = 0
        ddlMoedaCompra_TaxasMaritimo.SelectedValue = 0
        ddlEmpresa_TaxasMaritimo.SelectedValue = 0
        txtNomeEmpresa_TaxasMaritimo.Text = ""
        txtCalculoCompra_TaxasMaritimo.Text = ""
        txtTaxaCompra_TaxasMaritimo.Text = ""
        txtMinimoCompra_TaxasMaritimo.Text = ""
        'txtCalculoVenda_TaxasMaritimo.Text = ""
        'txtTaxaVenda_TaxasMaritimo.Text = ""
        'txtMinimoVenda_TaxasMaritimo.Text = ""
        txtID_TaxasMaritimo.Text = ""
        Session("CD_PR") = 0
        'divVendaMaritimo.Visible = True
        divCompraMaritimo.Visible = True
        btnSalvar_TaxasMaritimo.Visible = True

        mpeTaxasMaritimo.Hide()
    End Sub

    Private Sub btnFechar_CNTRMaritimo_Click(sender As Object, e As EventArgs) Handles btnFechar_CNTRMaritimo.Click
        txtID_CNTRMaritimo.Text = ""
        ddlTipoContainer_CNTRMaritimo.SelectedValue = 0
        txtNumeroContainer_CNTRMaritimo.Text = ""
        txtLacre_CNTRMaritimo.Text = ""
        txtTara_CNTRMaritimo.Text = ""
        txtFreeTime_CNTRMaritimo.Text = ""
        ddlTipoContainer_CNTRMaritimo.Enabled = True
        txtTara_CNTRMaritimo.Enabled = True
        mpeCNTRMaritimo.Hide()
    End Sub

    Private Sub dgvContainer_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvContainer.RowCommand
        divSuccess_CNTRMaritimo1.Visible = False
        divErro_CNTRMaritimo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErro_CNTRMaritimo1.Text = "Usuário não tem permissão para realizar exclusões"
                divErro_CNTRMaritimo1.Visible = True
            Else
                Con.ExecutarQuery("DELETE From TB_CNTR_BL Where ID_CNTR_BL = " & ID)
                Con.ExecutarQuery("DELETE From TB_AMR_CNTR_BL Where ID_CNTR_BL = " & ID & " AND ID_BL = " & txtID_BasicoMaritimo.Text)
                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_CNTR_BL = 0 WHERE ID_CNTR_BL = " & ID)
                lblSuccess_CNTRMaritimo1.Text = "Registro deletado!"
                divSuccess_CNTRMaritimo1.Visible = True
                dgvContainer.DataBind()
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select ID_CNTR_BL,ID_BL_MASTER,NR_CNTR,NR_LACRE,VL_PESO_TARA,ID_TIPO_CNTR,QT_DIAS_FREETIME from TB_CNTR_BL

WHERE ID_CNTR_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CNTR_BL")) Then
                    txtID_CNTRMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_CNTR_BL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CNTR")) Then
                    ddlTipoContainer_CNTRMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CNTR")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CNTR")) Then
                    txtNumeroContainer_CNTRMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_CNTR")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")) Then
                    txtFreeTime_CNTRMaritimo.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_LACRE")) Then
                    txtLacre_CNTRMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_LACRE")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TARA")) Then
                    txtTara_CNTRMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TARA")
                End If

                txtControle.Text = 1

                mpeCNTRMaritimo.Show()

            End If

        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("INSERT INTO TB_CNTR_BL (ID_BL_MASTER,ID_TIPO_CNTR,NR_CNTR,NR_LACRE,VL_PESO_TARA,QT_DIAS_FREETIME,DT_DEVOLUCAO_CNTR,FL_DEMURRAGE_FINALIZADA,FL_INICIO_CHEGADA,ID_STATUS_DEMURRAGE,DT_STATUS_DEMURRAGE,DS_OBSERVACAO)  select ID_BL_MASTER,ID_TIPO_CNTR,NR_CNTR,NR_LACRE,VL_PESO_TARA,QT_DIAS_FREETIME,DT_DEVOLUCAO_CNTR,FL_DEMURRAGE_FINALIZADA,FL_INICIO_CHEGADA,ID_STATUS_DEMURRAGE,DT_STATUS_DEMURRAGE,DS_OBSERVACAO from TB_CNTR_BL
WHERE ID_CNTR_BL =  " & ID)
            lblSuccess_CNTRMaritimo1.Text = "Registro duplicado!"
            divSuccess_CNTRMaritimo1.Visible = True
            dgvContainer.DataBind()
        End If
        Con.Fechar()
    End Sub

    Private Sub dgvTaxasMaritimo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxasMaritimo.RowCommand

        divSuccess_TaxasMaritimo1.Visible = False
        divErro_TaxasMaritimo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErro_TaxasMaritimo1.Text = "Usuário não tem permissão para realizar exclusões"
                divErro_TaxasMaritimo1.Visible = True

            Else
                Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


                If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                    divErro_TaxasMaritimo1.Visible = True
                    lblErro_TaxasMaritimo1.Text = "Não foi possível excluir o registro: a taxa já foi enviada para contas a pagar/receber!"
                Else

                    Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD
from TB_BL_TAXA A 
INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
INNER JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 
WHERE  DT_CANCELAMENTO IS NULL and ID_BL_TAXA_MASTER in (select ID_BL_TAXA
from TB_BL_TAXA 
WHERE ID_BL=" & Request.QueryString("id") & " and ID_BL_TAXA = " & ID & ")")
                    If ds2.Tables(0).Rows(0).Item("QTD") > 0 Then
                        divErro_TaxasMaritimo1.Visible = True
                        lblErro_TaxasMaritimo1.Text = "Não foi possível excluir o registro: a taxa já foi enviada para contas a pagar/receber!"
                    Else
                        Dim ds3 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD from  View_Taxa_Bloqueada WHERE ID_BL_TAXA = " & ID & " or ID_BL_TAXA_MASTER= " & ID)
                        If ds3.Tables(0).Rows(0).Item("QTD") > 0 Then
                            divErro_TaxasMaritimo1.Visible = True
                            lblErro_TaxasMaritimo1.Text = "Não foi possível excluir o registro: a taxa já foi enviada para contas a pagar/receber ou invoice!"
                        Else
                            Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                            lblSuccess_TaxasMaritimo1.Text = "Registro deletado!"
                            divSuccess_TaxasMaritimo1.Visible = True
                            dgvTaxasMaritimo.DataBind()

                        End If
                    End If

                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_BL,A.ID_ITEM_DESPESA,A.ID_BASE_CALCULO_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA,A.VL_TAXA_MIN,A.ID_MOEDA,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_STATUS_PAGAMENTO,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,c.DT_CANCELAMENTO,A.FL_PREMIACAO,A.CD_PR 
from TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 

WHERE A.ID_BL_TAXA = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA")) Then
                    txtID_TaxasMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                    ddlDespesa_TaxasMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_PREMIACAO")) Then
                    ckbPremiacao_TaxasMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_PREMIACAO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    ddlTipoPagamento_TaxasMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                    ddlOrigemPagamento_TaxasMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculo_TaxasMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                    ddlMoedaCompra_TaxasMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")) Then
                    ddlStatusPagamento_TaxasMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtTaxaCompra_TaxasMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")) Then
                    txtCalculoCompra_TaxasMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")) Then
                    txtMinimoCompra_TaxasMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")) Then
                    txtCodEmpresa_TaxasMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                    ddlEmpresa_TaxasMaritimo.DataBind()
                    ddlEmpresa_TaxasMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")) Then
                    If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        btnSalvar_TaxasMaritimo.Visible = False
                    Else
                        btnSalvar_TaxasMaritimo.Visible = True
                    End If
                Else
                    btnSalvar_TaxasMaritimo.Visible = True
                End If


                Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD
from TB_BL_TAXA A 
INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
INNER JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 
WHERE  DT_CANCELAMENTO IS NULL and ID_BL_TAXA_MASTER in (select ID_BL_TAXA
from TB_BL_TAXA 
WHERE ID_BL=" & Request.QueryString("id") & " and ID_BL_TAXA = " & ID & ")")
                If ds2.Tables(0).Rows(0).Item("QTD") > 0 Then
                    btnSalvar_TaxasMaritimo.Visible = False
                End If

                Dim ds3 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD from  View_Taxa_Bloqueada WHERE ID_BL_TAXA = " & ID & " or ID_BL_TAXA_MASTER= " & ID)
                If ds3.Tables(0).Rows(0).Item("QTD") > 0 Then
                    btnSalvar_TaxasMaritimo.Visible = False
                End If

                mpeTaxasMaritimo.Show()

            End If

        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument
            Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


            If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                divErro_TaxasMaritimo1.Visible = True
                lblErro_TaxasMaritimo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
            Else


                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_ITEM_DESPESA,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,VL_TAXA,VL_TAXA_MIN,VL_TAXA_CALCULADO,VL_TAXA_BR,ID_MOEDA,VL_CAMBIO,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_PARCEIRO_EMPRESA,VL_PESO_TAXADO,OB_TAXAS,DT_ATUALIZACAO_CAMBIO,DT_SOLICITACAO_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,FL_INTEGRA_PA,FL_NOTIFICADO,FL_ACCOUNT,FL_TAXA_TRANSPORTADOR,FL_PREMIACAO,CD_PR)    select ID_BL,ID_ITEM_DESPESA,ID_TIPO_ITEM_DESPESA,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,VL_TAXA,VL_TAXA_MIN,VL_TAXA_CALCULADO,VL_TAXA_BR,ID_MOEDA,VL_CAMBIO,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_PARCEIRO_EMPRESA,VL_PESO_TAXADO,OB_TAXAS,DT_ATUALIZACAO_CAMBIO,DT_SOLICITACAO_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,FL_INTEGRA_PA,FL_NOTIFICADO,FL_ACCOUNT,FL_TAXA_TRANSPORTADOR,FL_PREMIACAO,CD_PR from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                lblSuccess_TaxasMaritimo1.Text = "Registro duplicado!"
                divSuccess_TaxasMaritimo1.Visible = True
                dgvTaxasMaritimo.DataBind()
            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub dgvTaxasAereo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxasAereo.RowCommand
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
                    lblErro_TaxaAereo1.Text = "Não foi possível excluir o registro: a taxa já foi enviada para contas a pagar/receber!"
                Else

                    Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD
from TB_BL_TAXA A 
INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
INNER JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 
WHERE  DT_CANCELAMENTO IS NULL and ID_BL_TAXA_MASTER in (select ID_BL_TAXA
from TB_BL_TAXA 
WHERE ID_BL=" & Request.QueryString("id") & " and ID_BL_TAXA = " & ID & ")")
                    If ds2.Tables(0).Rows(0).Item("QTD") > 0 Then
                        divErro_TaxaAereo1.Visible = True
                        lblErro_TaxaAereo1.Text = "Não foi possível excluir o registro: a taxa já foi enviada para contas a pagar/receber!"
                    Else

                        Dim ds3 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD from  View_Taxa_Bloqueada WHERE ID_BL_TAXA = " & ID & " or ID_BL_TAXA_MASTER= " & ID)
                        If ds3.Tables(0).Rows(0).Item("QTD") > 0 Then
                            divErro_TaxaAereo1.Visible = True
                            lblErro_TaxaAereo1.Text = "Não foi possível excluir o registro: a taxa já foi enviada para contas a pagar/receber ou invoice!"
                        Else
                            Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                            lblSuccess_TaxaAereo1.Text = "Registro deletado!"
                            divSuccess_TaxaAereo1.Visible = True
                            dgvTaxasAereo.DataBind()
                        End If

                    End If

                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_BL,A.ID_ITEM_DESPESA,A.ID_BASE_CALCULO_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA,A.VL_TAXA_MIN,A.ID_MOEDA,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_STATUS_PAGAMENTO,A.ID_PARCEIRO_EMPRESA,B.ID_CONTA_PAGAR_RECEBER_ITENS,c.DT_CANCELAMENTO,A.FL_PREMIACAO,A.CD_PR,A.ID_BL_TAXA_MASTER  from TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA")) Then
                    txtID_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL_TAXA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")) Then
                    txtCodEmpresa_TaxasAereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                    ddlEmpresa_TaxaAereo.DataBind()
                    ddlEmpresa_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                    ddlDespesa_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_PREMIACAO")) Then
                    ckbPremiacao_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_PREMIACAO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    ddlTipoPagamento_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                    ddlOrigemPagamento_TaxasAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")) Then
                    ddlStatusPagamento_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculo_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                    ddlMoedaCompra_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                    'ddlMoedaVenda_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtTaxaCompra_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                    ' txtTaxaVenda_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")) Then
                    txtCalculoCompra_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")
                    ' txtCalculoVenda_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")) Then
                    txtMinimoCompra_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                    ' txtMinimoVenda_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
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
WHERE ID_BL=" & Request.QueryString("id") & " and ID_BL_TAXA = " & ID & ")")
                If ds2.Tables(0).Rows(0).Item("QTD") > 0 And Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA_MASTER")) Then
                    btnSalvar_TaxaAereo.Visible = False
                End If

                Dim ds3 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD from  View_Taxa_Bloqueada WHERE ID_BL_TAXA = " & ID & " or ID_BL_TAXA_MASTER= " & ID)
                If ds3.Tables(0).Rows(0).Item("QTD") > 0 Then
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
                divErro_TaxasMaritimo1.Visible = True
                lblErro_TaxasMaritimo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
            Else

                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_ITEM_DESPESA,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,VL_TAXA,VL_TAXA_MIN,VL_TAXA_CALCULADO,VL_TAXA_BR,ID_MOEDA,VL_CAMBIO,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_PARCEIRO_EMPRESA,VL_PESO_TAXADO,OB_TAXAS,DT_ATUALIZACAO_CAMBIO,DT_SOLICITACAO_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,FL_INTEGRA_PA,FL_NOTIFICADO,FL_ACCOUNT,FL_TAXA_TRANSPORTADOR,FL_PREMIACAO,CD_PR)    select ID_BL,ID_ITEM_DESPESA,ID_TIPO_ITEM_DESPESA,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,VL_TAXA,VL_TAXA_MIN,VL_TAXA_CALCULADO,VL_TAXA_BR,ID_MOEDA,VL_CAMBIO,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_PARCEIRO_EMPRESA,VL_PESO_TAXADO,OB_TAXAS,DT_ATUALIZACAO_CAMBIO,DT_SOLICITACAO_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,FL_INTEGRA_PA,FL_NOTIFICADO,FL_ACCOUNT,FL_TAXA_TRANSPORTADOR,FL_PREMIACAO,CD_PR from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                lblSuccess_TaxaAereo1.Text = "Registro duplicado!"
                divSuccess_TaxaAereo1.Visible = True
                dgvTaxasAereo.DataBind()
            End If
        End If
        Con.Fechar()
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

            txtNumeroBL_BasicoAereo.Text = txtNumeroBL_BasicoAereo.Text.Replace(" ", "")
            txtTarifaMaster_BasicoAereo.Text = txtTarifaMaster_BasicoAereo.Text.Replace(".", "")
            txtTarifaMaster_BasicoAereo.Text = txtTarifaMaster_BasicoAereo.Text.Replace(",", ".")

            If txtCotacao_BasicoAereo.Text = "" Then
                txtCotacao_BasicoAereo.Text = 0
            End If

            If txtNumeroBL_BasicoAereo.Text = "" Then
                txtNumeroBL_BasicoAereo.Text = "NULL"
            Else
                txtNumeroBL_BasicoAereo.Text = "'" & txtNumeroBL_BasicoAereo.Text & "'"
            End If

            If txtNumeroVoo_BasicoAereo.Text = "" Then
                txtNumeroVoo_BasicoAereo.Text = "NULL"
            Else
                txtNumeroVoo_BasicoAereo.Text = "'" & txtNumeroVoo_BasicoAereo.Text & "'"
            End If

            If txtTarifaMaster_BasicoAereo.Text = "" Then
                txtTarifaMaster_BasicoAereo.Text = 0
            End If

            If txtDataConhecimento_BasicoAereo.Text = "" Then
                txtDataConhecimento_BasicoAereo.Text = "NULL"
            Else
                If v.ValidaData(txtDataConhecimento_BasicoAereo.Text) = False Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "O valor informado no campo Data de conhecimento é inválido."
                Else
                    txtDataConhecimento_BasicoAereo.Text = "CONVERT(date,'" & txtDataConhecimento_BasicoAereo.Text & "',103)"

                End If
            End If


            If txtPrevisaoChegada_BasicoAereo.Text = "" Then
                txtPrevisaoChegada_BasicoAereo.Text = "NULL"
            Else
                If v.ValidaData(txtPrevisaoChegada_BasicoAereo.Text) = False Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "O valor informado no campo Data CE é inválido."
                Else
                    txtPrevisaoChegada_BasicoAereo.Text = "CONVERT(date,'" & txtPrevisaoChegada_BasicoAereo.Text & "',103)"

                End If
            End If

            If txtChegada_BasicoAereo.Text = "" Then
                txtChegada_BasicoAereo.Text = "NULL"
            Else

                If v.ValidaData(txtChegada_BasicoAereo.Text) = False Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "O valor informado no campo Data CE é inválido."
                Else
                    txtChegada_BasicoAereo.Text = "CONVERT(date,'" & txtChegada_BasicoAereo.Text & "',103)"

                End If
            End If

            If txtPrevisaoEmbarque_BasicoAereo.Text = "" Then
                txtPrevisaoEmbarque_BasicoAereo.Text = "NULL"
            Else

                If v.ValidaData(txtPrevisaoEmbarque_BasicoAereo.Text) = False Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "O valor informado no campo Data CE é inválido."
                Else
                    txtPrevisaoEmbarque_BasicoAereo.Text = "CONVERT(date,'" & txtPrevisaoEmbarque_BasicoAereo.Text & "',103)"

                End If
            End If

            If txtEmbarque_BasicoAereo.Text = "" Then
                txtEmbarque_BasicoAereo.Text = "NULL"
            Else

                If v.ValidaData(txtEmbarque_BasicoAereo.Text) = False Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "O valor informado no campo Data CE é inválido."
                Else
                    txtEmbarque_BasicoAereo.Text = "CONVERT(date,'" & txtEmbarque_BasicoAereo.Text & "',103)"

                End If
            End If

            If txtDataPrevista1_BasicoAereo.Text = "" Then
                txtDataPrevista1_BasicoAereo.Text = "NULL"
            Else
                If v.ValidaData(txtDataPrevista1_BasicoAereo.Text) = False Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "O valor informado no campo Data CE é inválido."
                Else
                    txtDataPrevista1_BasicoAereo.Text = "CONVERT(date,'" & txtDataPrevista1_BasicoAereo.Text & "',103)"

                End If
            End If

            If txtVoo1_BasicoAereo.Text = "" Then
                txtVoo1_BasicoAereo.Text = "NULL"
            Else
                txtVoo1_BasicoAereo.Text = "'" & txtVoo1_BasicoAereo.Text & "'"
            End If



            If txtDataPrevista2_BasicoAereo.Text = "" Then
                txtDataPrevista2_BasicoAereo.Text = "NULL"
            Else
                If v.ValidaData(txtDataPrevista2_BasicoAereo.Text) = False Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "O valor informado no campo Data CE é inválido."
                Else
                    txtDataPrevista2_BasicoAereo.Text = "CONVERT(date,'" & txtDataPrevista2_BasicoAereo.Text & "',103)"

                End If
            End If

            If txtVoo2_BasicoAereo.Text = "" Then
                txtVoo2_BasicoAereo.Text = "NULL"
            Else
                txtVoo2_BasicoAereo.Text = "'" & txtVoo2_BasicoAereo.Text & "'"
            End If


            If txtDataPrevista3_BasicoAereo.Text = "" Then
                txtDataPrevista3_BasicoAereo.Text = "NULL"
            Else
                If v.ValidaData(txtDataPrevista3_BasicoAereo.Text) = False Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "O valor informado no campo Data CE é inválido."
                Else
                    txtDataPrevista3_BasicoAereo.Text = "CONVERT(date,'" & txtDataPrevista3_BasicoAereo.Text & "',103)"

                End If
            End If

            If txtVoo3_BasicoAereo.Text = "" Then
                txtVoo3_BasicoAereo.Text = "NULL"
            Else
                txtVoo3_BasicoAereo.Text = "'" & txtVoo3_BasicoAereo.Text & "'"
            End If

            If txtID_BasicoAereo.Text = "" Then

                Session("ID_WEEK") = 0

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else

                    Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL)QTD FROM TB_BL WHERE FL_CANCELADO = 0 AND NR_BL = " & txtNumeroBL_BasicoAereo.Text & "")
                    If ds1.Tables(0).Rows(0).Item("QTD") = 0 Then


                        'INSERE 
                        ds = Con.ExecutarQuery("INSERT INTO TB_BL (GRAU,NR_BL,ID_PARCEIRO_TRANSPORTADOR,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_TIPO_PAGAMENTO,NR_VIAGEM,NR_VIAGEM_1T,NR_VIAGEM_2T,NR_VIAGEM_3T, DT_1T, DT_2T, DT_3T, ID_PORTO_1T,ID_PORTO_2T,ID_PORTO_3T,ID_MOEDA_FRETE, DT_PREVISAO_EMBARQUE,DT_PREVISAO_CHEGADA,DT_CHEGADA,DT_EMBARQUE,DT_EMISSAO_CONHECIMENTO,VL_TARIFA_MASTER,ID_SERVICO,DT_ABERTURA,ID_STATUS_FRETE_AGENTE,ID_TIPO_ESTUFAGEM,ID_COTACAO) VALUES ('M'," & txtNumeroBL_BasicoAereo.Text & "," & ddltransportador_BasicoAereo.SelectedValue & ", " & ddlOrigem_BasicoAereo.SelectedValue & ", " & ddlDestino_BasicoAereo.SelectedValue & "," & ddlAgente_BasicoAereo.SelectedValue & "," & ddlTipoPagamento_BasicoAereo.SelectedValue & "," & txtNumeroVoo_BasicoAereo.Text & "," & txtVoo1_BasicoAereo.Text & "," & txtVoo2_BasicoAereo.Text & "," & txtVoo3_BasicoAereo.Text & "," & txtDataPrevista1_BasicoAereo.Text & "," & txtDataPrevista2_BasicoAereo.Text & "," & txtDataPrevista3_BasicoAereo.Text & "," & ddlAeroporto1_BasicoAereo.SelectedValue & "," & ddlAeroporto2_BasicoAereo.SelectedValue & "," & ddlAeroporto3_BasicoAereo.SelectedValue & "," & ddlMoedaFrete_BasicoAereo.SelectedValue & "," & txtPrevisaoEmbarque_BasicoAereo.Text & ", " & txtPrevisaoChegada_BasicoAereo.Text & ", " & txtChegada_BasicoAereo.Text & ", " & txtEmbarque_BasicoAereo.Text & "," & txtDataConhecimento_BasicoAereo.Text & ", " & txtTarifaMaster_BasicoAereo.Text & ", " & ddlServico_BasicoAereo.Text & ",GETDATE()," & ddlStatusFreteAgente_BasicoAereo.SelectedValue & "," & ddlEstufagem_BasicoAereo.SelectedValue & "," & txtCotacao_BasicoAereo.Text & ") Select SCOPE_IDENTITY() as ID_BL ")

                        'PREENCHE SESSÃO E CAMPO DE ID
                        Session("ID_BL") = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                        txtID_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()

                        Dim Rastreio As New RastreioService
                        Rastreio.trackingbl(ds.Tables(0).Rows(0).Item("ID_BL").ToString())

                        If ddlWeekAereo.SelectedValue <> Session("ID_WEEK") Then
                            Week(2)
                        End If

                        NumeroProcesso()
                        AtualizaHouse(2)



                        txtPrevisaoChegada_BasicoAereo.Text = txtPrevisaoChegada_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                        txtPrevisaoChegada_BasicoAereo.Text = txtPrevisaoChegada_BasicoAereo.Text.Replace("',103)", "")
                        txtPrevisaoChegada_BasicoAereo.Text = txtPrevisaoChegada_BasicoAereo.Text.Replace("NULL", "")


                        txtPrevisaoEmbarque_BasicoAereo.Text = txtPrevisaoEmbarque_BasicoAereo.Text.Replace("NULL", "")
                        txtPrevisaoEmbarque_BasicoAereo.Text = txtPrevisaoEmbarque_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                        txtPrevisaoEmbarque_BasicoAereo.Text = txtPrevisaoEmbarque_BasicoAereo.Text.Replace("',103)", "")

                        txtChegada_BasicoAereo.Text = txtChegada_BasicoAereo.Text.Replace("NULL", "")
                        txtChegada_BasicoAereo.Text = txtChegada_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                        txtChegada_BasicoAereo.Text = txtChegada_BasicoAereo.Text.Replace("',103)", "")

                        txtEmbarque_BasicoAereo.Text = txtEmbarque_BasicoAereo.Text.Replace("NULL", "")
                        txtEmbarque_BasicoAereo.Text = txtEmbarque_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                        txtEmbarque_BasicoAereo.Text = txtEmbarque_BasicoAereo.Text.Replace("',103)", "")

                        txtDataPrevista1_BasicoAereo.Text = txtDataPrevista1_BasicoAereo.Text.Replace("NULL", "")
                        txtDataPrevista1_BasicoAereo.Text = txtDataPrevista1_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                        txtDataPrevista1_BasicoAereo.Text = txtDataPrevista1_BasicoAereo.Text.Replace("',103)", "")

                        txtDataPrevista2_BasicoAereo.Text = txtDataPrevista2_BasicoAereo.Text.Replace("NULL", "")
                        txtDataPrevista2_BasicoAereo.Text = txtDataPrevista2_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                        txtDataPrevista2_BasicoAereo.Text = txtDataPrevista2_BasicoAereo.Text.Replace("',103)", "")

                        txtDataPrevista3_BasicoAereo.Text = txtDataPrevista3_BasicoAereo.Text.Replace("NULL", "")
                        txtDataPrevista3_BasicoAereo.Text = txtDataPrevista3_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                        txtDataPrevista3_BasicoAereo.Text = txtDataPrevista3_BasicoAereo.Text.Replace("',103)", "")

                        txtDataConhecimento_BasicoAereo.Text = txtDataConhecimento_BasicoAereo.Text.Replace("NULL", "")
                        txtDataConhecimento_BasicoAereo.Text = txtDataConhecimento_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                        txtDataConhecimento_BasicoAereo.Text = txtDataConhecimento_BasicoAereo.Text.Replace("',103)", "")

                        txtNumeroVoo_BasicoAereo.Text = txtNumeroVoo_BasicoAereo.Text.Replace("'", "")
                        txtNumeroVoo_BasicoAereo.Text = txtNumeroVoo_BasicoAereo.Text.Replace("NULL", "")

                        txtVoo1_BasicoAereo.Text = txtVoo1_BasicoAereo.Text.Replace("'", "")
                        txtVoo1_BasicoAereo.Text = txtVoo1_BasicoAereo.Text.Replace("NULL", "")

                        txtVoo2_BasicoAereo.Text = txtVoo2_BasicoAereo.Text.Replace("'", "")
                        txtVoo2_BasicoAereo.Text = txtVoo2_BasicoAereo.Text.Replace("NULL", "")

                        txtVoo3_BasicoAereo.Text = txtVoo3_BasicoAereo.Text.Replace("'", "")
                        txtVoo3_BasicoAereo.Text = txtVoo3_BasicoAereo.Text.Replace("NULL", "")

                        txtNumeroBL_BasicoAereo.Text = txtNumeroBL_BasicoAereo.Text.Replace("'", "")
                        txtNumeroBL_BasicoAereo.Text = txtNumeroBL_BasicoAereo.Text.Replace("NULL", "")

                        Con.Fechar()
                        divSuccess_BasicoAereo.Visible = True

                    Else
                        divErro_BasicoAereo.Visible = True
                        lblErro_BasicoAereo.Text = "Já existe BL cadastrada com este número."
                        Exit Sub
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
                    Con.ExecutarQuery("UPDATE TB_BL SET GRAU = 'M',NR_BL = " & txtNumeroBL_BasicoAereo.Text & ",ID_PARCEIRO_TRANSPORTADOR = " & ddltransportador_BasicoAereo.SelectedValue & ",ID_PORTO_ORIGEM = " & ddlOrigem_BasicoAereo.SelectedValue & ",ID_PORTO_DESTINO = " & ddlDestino_BasicoAereo.SelectedValue & ", ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgente_BasicoAereo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoAereo.SelectedValue & ",NR_VIAGEM = " & txtNumeroVoo_BasicoAereo.Text & ",NR_VIAGEM_1T = " & txtVoo1_BasicoAereo.Text & ",NR_VIAGEM_2T = " & txtVoo2_BasicoAereo.Text & ",NR_VIAGEM_3T = " & txtVoo3_BasicoAereo.Text & ", DT_1T = " & txtDataPrevista1_BasicoAereo.Text & ", DT_2T = " & txtDataPrevista2_BasicoAereo.Text & ", DT_3T = " & txtDataPrevista3_BasicoAereo.Text & ", ID_PORTO_1T =" & ddlAeroporto1_BasicoAereo.SelectedValue & ",ID_PORTO_3T =" & ddlAeroporto3_BasicoAereo.SelectedValue & ",ID_PORTO_2T =" & ddlAeroporto2_BasicoAereo.SelectedValue & ",ID_MOEDA_FRETE = " & ddlMoedaFrete_BasicoAereo.SelectedValue & ", DT_PREVISAO_EMBARQUE =  " & txtPrevisaoEmbarque_BasicoAereo.Text & ",DT_PREVISAO_CHEGADA =" & txtPrevisaoChegada_BasicoAereo.Text & ",DT_CHEGADA =  " & txtChegada_BasicoAereo.Text & ",DT_EMBARQUE =  " & txtEmbarque_BasicoAereo.Text & ",DT_EMISSAO_CONHECIMENTO = " & txtDataConhecimento_BasicoAereo.Text & ",VL_TARIFA_MASTER =  " & txtTarifaMaster_BasicoAereo.Text & ",ID_SERVICO = " & ddlServico_BasicoAereo.SelectedValue & ",ID_STATUS_FRETE_AGENTE =  " & ddlStatusFreteAgente_BasicoAereo.SelectedValue & " WHERE ID_BL = " & txtID_BasicoAereo.Text & "")


                    ds = Con.ExecutarQuery("SELECT YEAR(DT_ABERTURA)ANO_ABERTURA,ISNULL(NR_BL,0)NR_BL FROM [TB_BL] WHERE ID_BL = " & txtID_BasicoAereo.Text & "")
                    If ds.Tables(0).Rows(0).Item("ANO_ABERTURA") >= 2022 And ds.Tables(0).Rows(0).Item("NR_BL") <> "0" Then
                        Dim Rastreio As New RastreioService
                        Rastreio.trackingbl(txtID_BasicoAereo.Text)
                    End If


                    If ddlWeekAereo.SelectedValue <> Session("ID_WEEK") Then
                        Week(2)
                    End If
                    AtualizaHouse(2)

                    txtPrevisaoChegada_BasicoAereo.Text = txtPrevisaoChegada_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                    txtPrevisaoChegada_BasicoAereo.Text = txtPrevisaoChegada_BasicoAereo.Text.Replace("',103)", "")
                    txtPrevisaoChegada_BasicoAereo.Text = txtPrevisaoChegada_BasicoAereo.Text.Replace("NULL", "")


                    txtPrevisaoEmbarque_BasicoAereo.Text = txtPrevisaoEmbarque_BasicoAereo.Text.Replace("NULL", "")
                    txtPrevisaoEmbarque_BasicoAereo.Text = txtPrevisaoEmbarque_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                    txtPrevisaoEmbarque_BasicoAereo.Text = txtPrevisaoEmbarque_BasicoAereo.Text.Replace("',103)", "")

                    txtChegada_BasicoAereo.Text = txtChegada_BasicoAereo.Text.Replace("NULL", "")
                    txtChegada_BasicoAereo.Text = txtChegada_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                    txtChegada_BasicoAereo.Text = txtChegada_BasicoAereo.Text.Replace("',103)", "")

                    txtEmbarque_BasicoAereo.Text = txtEmbarque_BasicoAereo.Text.Replace("NULL", "")
                    txtEmbarque_BasicoAereo.Text = txtEmbarque_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                    txtEmbarque_BasicoAereo.Text = txtEmbarque_BasicoAereo.Text.Replace("',103)", "")

                    txtDataPrevista1_BasicoAereo.Text = txtDataPrevista1_BasicoAereo.Text.Replace("NULL", "")
                    txtDataPrevista1_BasicoAereo.Text = txtDataPrevista1_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                    txtDataPrevista1_BasicoAereo.Text = txtDataPrevista1_BasicoAereo.Text.Replace("',103)", "")

                    txtDataPrevista2_BasicoAereo.Text = txtDataPrevista2_BasicoAereo.Text.Replace("NULL", "")
                    txtDataPrevista2_BasicoAereo.Text = txtDataPrevista2_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                    txtDataPrevista2_BasicoAereo.Text = txtDataPrevista2_BasicoAereo.Text.Replace("',103)", "")

                    txtDataPrevista3_BasicoAereo.Text = txtDataPrevista3_BasicoAereo.Text.Replace("NULL", "")
                    txtDataPrevista3_BasicoAereo.Text = txtDataPrevista3_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                    txtDataPrevista3_BasicoAereo.Text = txtDataPrevista3_BasicoAereo.Text.Replace("',103)", "")

                    txtDataConhecimento_BasicoAereo.Text = txtDataConhecimento_BasicoAereo.Text.Replace("NULL", "")
                    txtDataConhecimento_BasicoAereo.Text = txtDataConhecimento_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
                    txtDataConhecimento_BasicoAereo.Text = txtDataConhecimento_BasicoAereo.Text.Replace("',103)", "")

                    txtNumeroVoo_BasicoAereo.Text = txtNumeroVoo_BasicoAereo.Text.Replace("'", "")
                    txtNumeroVoo_BasicoAereo.Text = txtNumeroVoo_BasicoAereo.Text.Replace("NULL", "")

                    txtVoo1_BasicoAereo.Text = txtVoo1_BasicoAereo.Text.Replace("'", "")
                    txtVoo1_BasicoAereo.Text = txtVoo1_BasicoAereo.Text.Replace("NULL", "")

                    txtVoo2_BasicoAereo.Text = txtVoo2_BasicoAereo.Text.Replace("'", "")
                    txtVoo2_BasicoAereo.Text = txtVoo2_BasicoAereo.Text.Replace("NULL", "")

                    txtVoo3_BasicoAereo.Text = txtVoo3_BasicoAereo.Text.Replace("'", "")
                    txtVoo3_BasicoAereo.Text = txtVoo3_BasicoAereo.Text.Replace("NULL", "")

                    txtNumeroBL_BasicoAereo.Text = txtNumeroBL_BasicoAereo.Text.Replace("'", "")
                    txtNumeroBL_BasicoAereo.Text = txtNumeroBL_BasicoAereo.Text.Replace("NULL", "")

                    divSuccess_BasicoAereo.Visible = True
                    Con.Fechar()


                End If

            End If

        End If

        txtTarifaMaster_BasicoAereo.Text = txtTarifaMaster_BasicoAereo.Text.Replace(".", ",")

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

            txtNumeroBL_BasicoMaritimo.Text = txtNumeroBL_BasicoMaritimo.Text.Replace(" ", "")

            txtTarifaMasterMin_BasicoMaritimo.Text = txtTarifaMasterMin_BasicoMaritimo.Text.Replace(".", "")
            txtTarifaMasterMin_BasicoMaritimo.Text = txtTarifaMasterMin_BasicoMaritimo.Text.Replace(",", ".")



            If txtCotacao_BasicoMaritimo.Text = "" Then
                txtCotacao_BasicoMaritimo.Text = 0
            End If

            If txtNumeroBL_BasicoMaritimo.Text = "" Then
                txtNumeroBL_BasicoMaritimo.Text = "NULL"
            Else
                txtNumeroBL_BasicoMaritimo.Text = "'" & txtNumeroBL_BasicoMaritimo.Text & "'"
            End If

            If txtNumeroViagem_BasicoMaritimo.Text = "" Then
                txtNumeroViagem_BasicoMaritimo.Text = "NULL"
            Else
                txtNumeroViagem_BasicoMaritimo.Text = "'" & txtNumeroViagem_BasicoMaritimo.Text & "'"
            End If



            If txtTarifaMasterMin_BasicoMaritimo.Text = "" Then
                txtTarifaMasterMin_BasicoMaritimo.Text = 0
            End If

            If txtEmissaoBL_BasicoMaritimo.Text = "" Then
                txtEmissaoBL_BasicoMaritimo.Text = "NULL"
            Else
                If v.ValidaData(txtEmissaoBL_BasicoMaritimo.Text) = False Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Data de previsão de chegada é inválida."
                Else
                    txtEmissaoBL_BasicoMaritimo.Text = "CONVERT(date,'" & txtEmissaoBL_BasicoMaritimo.Text & "',103)"

                End If
            End If

            If txtPrevisaoChegada_BasicoMaritimo.Text = "" Then
                txtPrevisaoChegada_BasicoMaritimo.Text = "NULL"
            Else
                If v.ValidaData(txtPrevisaoChegada_BasicoMaritimo.Text) = False Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Data de previsão de chegada é inválida."
                Else
                    txtPrevisaoChegada_BasicoMaritimo.Text = "CONVERT(date,'" & txtPrevisaoChegada_BasicoMaritimo.Text & "',103)"

                End If
            End If

            If txtChegada_BasicoMaritimo.Text = "" Then
                txtChegada_BasicoMaritimo.Text = "NULL"
            Else

                If v.ValidaData(txtChegada_BasicoMaritimo.Text) = False Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Data de chegada é inválida."
                Else
                    txtChegada_BasicoMaritimo.Text = "CONVERT(date,'" & txtChegada_BasicoMaritimo.Text & "',103)"

                End If
            End If

            If txtPrevisaoEmbarque_BasicoMaritimo.Text = "" Then
                txtPrevisaoEmbarque_BasicoMaritimo.Text = "NULL"
            Else

                If v.ValidaData(txtPrevisaoEmbarque_BasicoMaritimo.Text) = False Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Data de previsão de embarque é inválida."
                Else
                    txtPrevisaoEmbarque_BasicoMaritimo.Text = "CONVERT(date,'" & txtPrevisaoEmbarque_BasicoMaritimo.Text & "',103)"

                End If
            End If

            If txtEmbarque_BasicoMaritimo.Text = "" Then
                txtEmbarque_BasicoMaritimo.Text = "NULL"
            Else

                If v.ValidaData(txtEmbarque_BasicoMaritimo.Text) = False Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Data de embarque é inválida."
                Else
                    txtEmbarque_BasicoMaritimo.Text = "CONVERT(date,'" & txtEmbarque_BasicoMaritimo.Text & "',103)"

                End If
            End If

            If txtData1_BasicoMaritimo.Text = "" Then
                txtData1_BasicoMaritimo.Text = "NULL"
            Else
                If v.ValidaData(txtData1_BasicoMaritimo.Text) = False Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Data de transbordo 1 inválida."
                Else
                    txtData1_BasicoMaritimo.Text = "CONVERT(date,'" & txtData1_BasicoMaritimo.Text & "',103)"

                End If
            End If


            If txtData2_BasicoMaritimo.Text = "" Then
                txtData2_BasicoMaritimo.Text = "NULL"
            Else
                If v.ValidaData(txtData2_BasicoMaritimo.Text) = False Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Data de transbordo 2 inválida."
                Else
                    txtData2_BasicoMaritimo.Text = "CONVERT(date,'" & txtData2_BasicoMaritimo.Text & "',103)"

                End If
            End If

            If txtData3_BasicoMaritimo.Text = "" Then
                txtData3_BasicoMaritimo.Text = "NULL"
            Else
                If v.ValidaData(txtData3_BasicoMaritimo.Text) = False Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Data de transbordo 3 inválida."
                Else
                    txtData3_BasicoMaritimo.Text = "CONVERT(date,'" & txtData3_BasicoMaritimo.Text & "',103)"

                End If
            End If

            If txtViagem1_BasicoMaritimo.Text = "" Then
                txtViagem1_BasicoMaritimo.Text = "NULL"
            Else
                txtViagem1_BasicoMaritimo.Text = "'" & txtViagem1_BasicoMaritimo.Text & "'"
            End If

            If txtViagem2_BasicoMaritimo.Text = "" Then
                txtViagem2_BasicoMaritimo.Text = "NULL"
            Else
                txtViagem2_BasicoMaritimo.Text = "'" & txtViagem2_BasicoMaritimo.Text & "'"
            End If

            If txtViagem3_BasicoMaritimo.Text = "" Then
                txtViagem3_BasicoMaritimo.Text = "NULL"
            Else
                txtViagem3_BasicoMaritimo.Text = "'" & txtViagem3_BasicoMaritimo.Text & "'"
            End If


            Dim ID_NAVIO As String = 0
            Dim ID_NAVIO1 As String = 0
            Dim ID_NAVIO2 As String = 0
            Dim ID_NAVIO3 As String = 0

            If ddlNavio_BasicoMaritimo.SelectedIndex <> 0 Then
                ID_NAVIO = SeparaIDNavio(ddlNavio_BasicoMaritimo.SelectedValue)
            End If
            If ddlNavio1_BasicoMaritimo.SelectedIndex <> 0 Then
                ID_NAVIO1 = SeparaIDNavio(ddlNavio1_BasicoMaritimo.SelectedValue)
            End If
            If ddlNavio2_BasicoMaritimo.SelectedIndex <> 0 Then
                ID_NAVIO2 = SeparaIDNavio(ddlNavio2_BasicoMaritimo.SelectedValue)
            End If
            If ddlNavio3_BasicoMaritimo.SelectedIndex <> 0 Then
                ID_NAVIO3 = SeparaIDNavio(ddlNavio3_BasicoMaritimo.SelectedValue)
            End If

            If txtID_BasicoMaritimo.Text = "" Then

                Session("ID_WEEK") = 0

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else

                    Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_BL)QTD FROM TB_BL WHERE FL_CANCELADO = 0 AND NR_BL = " & txtNumeroBL_BasicoMaritimo.Text & "")
                    If ds1.Tables(0).Rows(0).Item("QTD") = 0 Then

                        If txtCE_BasicoMaritimo.Text = "" Then
                            txtCE_BasicoMaritimo.Text = "NULL"
                            txtDataCE_BasicoMaritimo.Text = "NULL"
                        Else
                            txtCE_BasicoMaritimo.Text = "'" & txtCE_BasicoMaritimo.Text & "'"
                            txtDataCE_BasicoMaritimo.Text = " getdate() "
                        End If


                        'INSERE 
                        ds = Con.ExecutarQuery("INSERT INTO TB_BL (GRAU,NR_BL,ID_PARCEIRO_TRANSPORTADOR,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_TIPO_PAGAMENTO,NR_VIAGEM,NR_VIAGEM_1T,NR_VIAGEM_2T,NR_VIAGEM_3T, DT_1T, DT_2T, DT_3T, ID_PORTO_1T,ID_PORTO_2T,ID_PORTO_3T, DT_PREVISAO_EMBARQUE,DT_PREVISAO_CHEGADA,DT_CHEGADA,DT_EMBARQUE,DT_CE,VL_TARIFA_MASTER_MINIMA,NR_CE,ID_SERVICO,ID_PARCEIRO_AGENCIA,ID_TIPO_ESTUFAGEM,DT_EMISSAO_BL,ID_PARCEIRO_ARMAZEM_ATRACACAO,ID_PARCEIRO_ARMAZEM_DESCARGA, ID_NAVIO,ID_NAVIO_1T, ID_NAVIO_2T,ID_NAVIO_3T,DT_ABERTURA,ID_STATUS_FRETE_AGENTE,ID_COTACAO) VALUES ('M'," & txtNumeroBL_BasicoMaritimo.Text & "," & ddlTransportador_BasicoMaritimo.SelectedValue & ", " & ddlOrigem_BasicoMaritimo.SelectedValue & ", " & ddlDestino_BasicoMaritimo.SelectedValue & "," & ddlAgente_BasicoMaritimo.SelectedValue & "," & ddlTipoPagamento_BasicoMaritimo.SelectedValue & "," & txtNumeroViagem_BasicoMaritimo.Text & "," & txtViagem1_BasicoMaritimo.Text & "," & txtViagem2_BasicoMaritimo.Text & "," & txtViagem3_BasicoMaritimo.Text & "," & txtData1_BasicoMaritimo.Text & "," & txtData2_BasicoMaritimo.Text & "," & txtData3_BasicoMaritimo.Text & "," & ddlPorto1_BasicoMaritimo.SelectedValue & "," & ddlPorto2_BasicoMaritimo.SelectedValue & "," & ddlPorto3_BasicoMaritimo.SelectedValue & "," & txtPrevisaoEmbarque_BasicoMaritimo.Text & ", " & txtPrevisaoChegada_BasicoMaritimo.Text & ", " & txtChegada_BasicoMaritimo.Text & ", " & txtEmbarque_BasicoMaritimo.Text & "," & txtDataCE_BasicoMaritimo.Text & ", " & txtTarifaMasterMin_BasicoMaritimo.Text & "," & txtCE_BasicoMaritimo.Text & ", " & ddlServico_BasicoMaritimo.Text & "," & ddlAgenciaMaritima_BasicoMaritimo.SelectedValue & ", " & ddlEstufagem_BasicoMaritimo.SelectedValue & "," & txtEmissaoBL_BasicoMaritimo.Text & "," & ddlArmazemAtracacao_BasicoMaritimo.Text & "," & ddlArmazemDescarga_BasicoMaritimo.Text & "," & ID_NAVIO & ", " & ID_NAVIO1 & "," & ID_NAVIO2 & "," & ID_NAVIO3 & ",GETDATE()," & ddlStatusFreteAgente_BasicoMaritimo.SelectedValue & "," & txtCotacao_BasicoMaritimo.Text & ") Select SCOPE_IDENTITY() as ID_BL ")

                        'PREENCHE SESSÃO E CAMPO DE ID
                        Session("ID_BL") = ds.Tables(0).Rows(0).Item("ID_BL").ToString()


                        txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()

                        If ddlWeekMaritimo.SelectedValue <> Session("ID_WEEK") Then
                            Week(1)
                        End If
                        AtualizaHouse(1)
                        NumeroProcesso()

                        txtPrevisaoChegada_BasicoMaritimo.Text = txtPrevisaoChegada_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                        txtPrevisaoChegada_BasicoMaritimo.Text = txtPrevisaoChegada_BasicoMaritimo.Text.Replace("',103)", "")
                        txtPrevisaoChegada_BasicoMaritimo.Text = txtPrevisaoChegada_BasicoMaritimo.Text.Replace("NULL", "")


                        txtPrevisaoEmbarque_BasicoMaritimo.Text = txtPrevisaoEmbarque_BasicoMaritimo.Text.Replace("NULL", "")
                        txtPrevisaoEmbarque_BasicoMaritimo.Text = txtPrevisaoEmbarque_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                        txtPrevisaoEmbarque_BasicoMaritimo.Text = txtPrevisaoEmbarque_BasicoMaritimo.Text.Replace("',103)", "")

                        txtChegada_BasicoMaritimo.Text = txtChegada_BasicoMaritimo.Text.Replace("NULL", "")
                        txtChegada_BasicoMaritimo.Text = txtChegada_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                        txtChegada_BasicoMaritimo.Text = txtChegada_BasicoMaritimo.Text.Replace("',103)", "")

                        txtEmbarque_BasicoMaritimo.Text = txtEmbarque_BasicoMaritimo.Text.Replace("NULL", "")
                        txtEmbarque_BasicoMaritimo.Text = txtEmbarque_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                        txtEmbarque_BasicoMaritimo.Text = txtEmbarque_BasicoMaritimo.Text.Replace("',103)", "")

                        txtData1_BasicoMaritimo.Text = txtData1_BasicoMaritimo.Text.Replace("NULL", "")
                        txtData1_BasicoMaritimo.Text = txtData1_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                        txtData1_BasicoMaritimo.Text = txtData1_BasicoMaritimo.Text.Replace("',103)", "")

                        txtData2_BasicoMaritimo.Text = txtData2_BasicoMaritimo.Text.Replace("NULL", "")
                        txtData2_BasicoMaritimo.Text = txtData2_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                        txtData2_BasicoMaritimo.Text = txtData2_BasicoMaritimo.Text.Replace("',103)", "")

                        txtData3_BasicoMaritimo.Text = txtData3_BasicoMaritimo.Text.Replace("NULL", "")
                        txtData3_BasicoMaritimo.Text = txtData3_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                        txtData3_BasicoMaritimo.Text = txtData3_BasicoMaritimo.Text.Replace("',103)", "")

                        txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("NULL", "")
                        txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                        txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("',103)", "")

                        txtEmissaoBL_BasicoMaritimo.Text = txtEmissaoBL_BasicoMaritimo.Text.Replace("NULL", "")
                        txtEmissaoBL_BasicoMaritimo.Text = txtEmissaoBL_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                        txtEmissaoBL_BasicoMaritimo.Text = txtEmissaoBL_BasicoMaritimo.Text.Replace("',103)", "")

                        txtCE_BasicoMaritimo.Text = txtCE_BasicoMaritimo.Text.Replace("'", "")
                        txtCE_BasicoMaritimo.Text = txtCE_BasicoMaritimo.Text.Replace("NULL", "")

                        txtNumeroViagem_BasicoMaritimo.Text = txtNumeroViagem_BasicoMaritimo.Text.Replace("'", "")
                        txtNumeroViagem_BasicoMaritimo.Text = txtNumeroViagem_BasicoMaritimo.Text.Replace("NULL", "")

                        txtViagem1_BasicoMaritimo.Text = txtViagem1_BasicoMaritimo.Text.Replace("'", "")
                        txtViagem1_BasicoMaritimo.Text = txtViagem1_BasicoMaritimo.Text.Replace("NULL", "")

                        txtViagem2_BasicoMaritimo.Text = txtViagem2_BasicoMaritimo.Text.Replace("'", "")
                        txtViagem2_BasicoMaritimo.Text = txtViagem2_BasicoMaritimo.Text.Replace("NULL", "")

                        txtViagem3_BasicoMaritimo.Text = txtViagem3_BasicoMaritimo.Text.Replace("'", "")
                        txtViagem3_BasicoMaritimo.Text = txtViagem3_BasicoMaritimo.Text.Replace("NULL", "")

                        txtNumeroBL_BasicoMaritimo.Text = txtNumeroBL_BasicoMaritimo.Text.Replace("'", "")
                        txtNumeroBL_BasicoMaritimo.Text = txtNumeroBL_BasicoMaritimo.Text.Replace("NULL", "")

                        If txtNumeroBL_BasicoMaritimo.Text <> "" Then
                            Dim Rastreio As New RastreioService
                            Rastreio.trackingbl(ds.Tables(0).Rows(0).Item("ID_BL").ToString())
                        End If


                        Con.Fechar()
                        divSuccess_BasicoMaritimo.Visible = True
                    Else
                        divErro_BasicoMaritimo.Visible = True
                        lblErro_BasicoMaritimo.Text = "Já existe BL cadastrada com este número!"
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
                    Con.ExecutarQuery("UPDATE TB_BL SET GRAU = 'M',NR_BL = " & txtNumeroBL_BasicoMaritimo.Text & ",ID_PARCEIRO_TRANSPORTADOR = " & ddlTransportador_BasicoMaritimo.SelectedValue & ",ID_PORTO_ORIGEM = " & ddlOrigem_BasicoMaritimo.SelectedValue & ",ID_PORTO_DESTINO = " & ddlDestino_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgente_BasicoMaritimo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoMaritimo.SelectedValue & ",NR_VIAGEM = " & txtNumeroViagem_BasicoMaritimo.Text & ",NR_VIAGEM_1T = " & txtViagem1_BasicoMaritimo.Text & ",NR_VIAGEM_2T = " & txtViagem2_BasicoMaritimo.Text & ",NR_VIAGEM_3T = " & txtViagem3_BasicoMaritimo.Text & ", DT_1T = " & txtData1_BasicoMaritimo.Text & ", DT_2T = " & txtData2_BasicoMaritimo.Text & ", DT_3T = " & txtData3_BasicoMaritimo.Text & ", ID_PORTO_1T =" & ddlPorto1_BasicoMaritimo.SelectedValue & ",ID_PORTO_3T =" & ddlPorto3_BasicoMaritimo.SelectedValue & ",ID_PORTO_2T =" & ddlPorto2_BasicoMaritimo.SelectedValue & ", DT_PREVISAO_EMBARQUE =  " & txtPrevisaoEmbarque_BasicoMaritimo.Text & ",DT_PREVISAO_CHEGADA =" & txtPrevisaoChegada_BasicoMaritimo.Text & ",DT_CHEGADA =  " & txtChegada_BasicoMaritimo.Text & ",DT_EMBARQUE =  " & txtEmbarque_BasicoMaritimo.Text & ",DT_EMISSAO_BL = " & txtEmissaoBL_BasicoMaritimo.Text & ",VL_TARIFA_MASTER_MINIMA =  " & txtTarifaMasterMin_BasicoMaritimo.Text & ",ID_SERVICO = " & ddlServico_BasicoMaritimo.SelectedValue & ",ID_PARCEIRO_AGENCIA = " & ddlAgenciaMaritima_BasicoMaritimo.SelectedValue & " , ID_TIPO_ESTUFAGEM = " & ddlEstufagem_BasicoMaritimo.SelectedValue & ", ID_NAVIO = " & ID_NAVIO & " ,ID_NAVIO_1T = " & ID_NAVIO1 & " , ID_NAVIO_2T = " & ID_NAVIO2 & " ,ID_NAVIO_3T =  " & ID_NAVIO3 & ",ID_PARCEIRO_ARMAZEM_ATRACACAO = " & ddlArmazemAtracacao_BasicoMaritimo.Text & ",ID_PARCEIRO_ARMAZEM_DESCARGA = " & ddlArmazemDescarga_BasicoMaritimo.Text & "  , ID_STATUS_FRETE_AGENTE = " & ddlStatusFreteAgente_BasicoMaritimo.SelectedValue & " WHERE ID_BL = " & txtID_BasicoMaritimo.Text & "")



                    If txtCE_BasicoMaritimo.Text <> "" Then
                        ds = Con.ExecutarQuery("SELECT ISNULL(NR_CE,'')NR_CE, DT_CE FROM TB_BL WHERE ID_BL = " & txtID_BasicoMaritimo.Text & "")
                        If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) And txtCE_BasicoMaritimo.Text <> "" Then
                            Con.ExecutarQuery("UPDATE TB_BL SET DT_CE = GETDATE(), NR_CE = '" & txtCE_BasicoMaritimo.Text & "' WHERE DT_CE IS NULL AND ID_BL = " & txtID_BasicoMaritimo.Text & "")
                        ElseIf ds.Tables(0).Rows(0).Item("NR_CE").ToString <> txtCE_BasicoMaritimo.Text Then
                            Con.ExecutarQuery("UPDATE TB_BL SET DT_CE = GETDATE(), NR_CE = '" & txtCE_BasicoMaritimo.Text & "' WHERE ID_BL = " & txtID_BasicoMaritimo.Text & "")
                        End If
                    End If

                    ds = Con.ExecutarQuery("SELECT YEAR(DT_ABERTURA)ANO_ABERTURA,ISNULL(NR_BL,0)NR_BL FROM TB_BL WHERE ID_BL = " & txtID_BasicoMaritimo.Text & "")
                    If ds.Tables(0).Rows(0).Item("ANO_ABERTURA") >= 2022 And ds.Tables(0).Rows(0).Item("NR_BL") <> "0" Then
                        Dim Rastreio As New RastreioService
                        Rastreio.trackingbl(txtID_BasicoMaritimo.Text)
                    End If


                    If ddlWeekMaritimo.SelectedValue <> Session("ID_WEEK") Then
                        Week(1)
                    End If
                    AtualizaHouse(1)


                    txtPrevisaoChegada_BasicoMaritimo.Text = txtPrevisaoChegada_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                    txtPrevisaoChegada_BasicoMaritimo.Text = txtPrevisaoChegada_BasicoMaritimo.Text.Replace("',103)", "")
                    txtPrevisaoChegada_BasicoMaritimo.Text = txtPrevisaoChegada_BasicoMaritimo.Text.Replace("NULL", "")


                    txtPrevisaoEmbarque_BasicoMaritimo.Text = txtPrevisaoEmbarque_BasicoMaritimo.Text.Replace("NULL", "")
                    txtPrevisaoEmbarque_BasicoMaritimo.Text = txtPrevisaoEmbarque_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                    txtPrevisaoEmbarque_BasicoMaritimo.Text = txtPrevisaoEmbarque_BasicoMaritimo.Text.Replace("',103)", "")

                    txtChegada_BasicoMaritimo.Text = txtChegada_BasicoMaritimo.Text.Replace("NULL", "")
                    txtChegada_BasicoMaritimo.Text = txtChegada_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                    txtChegada_BasicoMaritimo.Text = txtChegada_BasicoMaritimo.Text.Replace("',103)", "")

                    txtEmbarque_BasicoMaritimo.Text = txtEmbarque_BasicoMaritimo.Text.Replace("NULL", "")
                    txtEmbarque_BasicoMaritimo.Text = txtEmbarque_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                    txtEmbarque_BasicoMaritimo.Text = txtEmbarque_BasicoMaritimo.Text.Replace("',103)", "")

                    txtData1_BasicoMaritimo.Text = txtData1_BasicoMaritimo.Text.Replace("NULL", "")
                    txtData1_BasicoMaritimo.Text = txtData1_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                    txtData1_BasicoMaritimo.Text = txtData1_BasicoMaritimo.Text.Replace("',103)", "")

                    txtData2_BasicoMaritimo.Text = txtData2_BasicoMaritimo.Text.Replace("NULL", "")
                    txtData2_BasicoMaritimo.Text = txtData2_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                    txtData2_BasicoMaritimo.Text = txtData2_BasicoMaritimo.Text.Replace("',103)", "")

                    txtData3_BasicoMaritimo.Text = txtData3_BasicoMaritimo.Text.Replace("NULL", "")
                    txtData3_BasicoMaritimo.Text = txtData3_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                    txtData3_BasicoMaritimo.Text = txtData3_BasicoMaritimo.Text.Replace("',103)", "")

                    txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("NULL", "")
                    txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                    txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("',103)", "")

                    txtEmissaoBL_BasicoMaritimo.Text = txtEmissaoBL_BasicoMaritimo.Text.Replace("NULL", "")
                    txtEmissaoBL_BasicoMaritimo.Text = txtEmissaoBL_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
                    txtEmissaoBL_BasicoMaritimo.Text = txtEmissaoBL_BasicoMaritimo.Text.Replace("',103)", "")

                    txtCE_BasicoMaritimo.Text = txtCE_BasicoMaritimo.Text.Replace("'", "")
                    txtCE_BasicoMaritimo.Text = txtCE_BasicoMaritimo.Text.Replace("NULL", "")

                    txtNumeroViagem_BasicoMaritimo.Text = txtNumeroViagem_BasicoMaritimo.Text.Replace("'", "")
                    txtNumeroViagem_BasicoMaritimo.Text = txtNumeroViagem_BasicoMaritimo.Text.Replace("NULL", "")

                    txtViagem1_BasicoMaritimo.Text = txtViagem1_BasicoMaritimo.Text.Replace("'", "")
                    txtViagem1_BasicoMaritimo.Text = txtViagem1_BasicoMaritimo.Text.Replace("NULL", "")

                    txtViagem2_BasicoMaritimo.Text = txtViagem2_BasicoMaritimo.Text.Replace("'", "")
                    txtViagem2_BasicoMaritimo.Text = txtViagem2_BasicoMaritimo.Text.Replace("NULL", "")

                    txtViagem3_BasicoMaritimo.Text = txtViagem3_BasicoMaritimo.Text.Replace("'", "")
                    txtViagem3_BasicoMaritimo.Text = txtViagem3_BasicoMaritimo.Text.Replace("NULL", "")

                    txtNumeroBL_BasicoMaritimo.Text = txtNumeroBL_BasicoMaritimo.Text.Replace("'", "")
                    txtNumeroBL_BasicoMaritimo.Text = txtNumeroBL_BasicoMaritimo.Text.Replace("NULL", "")

                    divSuccess_BasicoMaritimo.Visible = True
                    Con.Fechar()


                End If


            End If


        End If

        txtTarifaMasterMin_BasicoMaritimo.Text = txtTarifaMasterMin_BasicoMaritimo.Text.Replace(".", ",")
    End Sub

    Private Sub btnSalvar_TaxaAereo_Click(sender As Object, e As EventArgs) Handles btnSalvar_TaxaAereo.Click
        divSuccess_TaxaAereo2.Visible = False
        divErro_TaxaAereo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        txtMinimoCompra_TaxaAereo.Text = txtMinimoCompra_TaxaAereo.Text.Replace(".", "")
        txtMinimoCompra_TaxaAereo.Text = txtMinimoCompra_TaxaAereo.Text.Replace(",", ".")

        txtTaxaCompra_TaxaAereo.Text = txtTaxaCompra_TaxaAereo.Text.Replace(".", "")
        txtTaxaCompra_TaxaAereo.Text = txtTaxaCompra_TaxaAereo.Text.Replace(",", ".")

        If txtTaxaCompra_TaxaAereo.Text = "" Then
            txtTaxaCompra_TaxaAereo.Text = 0
        End If

        If txtMinimoCompra_TaxaAereo.Text = "" Then
            txtMinimoCompra_TaxaAereo.Text = 0
        End If

        Dim FL_DECLARADO As Integer = 0
        If ddlOrigemPagamento_TaxasAereo.SelectedValue = 2 Then
            FL_DECLARADO = 1
        End If

        If txtTaxaCompra_TaxaAereo.Text <> "" And txtTaxaCompra_TaxaAereo.Text <> 0 Then
            If txtID_TaxaAereo.Text = "" Then


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxaAereo2.Visible = True
                    lblErro_TaxaAereo2.Text = "Usuário não possui permissão para cadastrar."

                    txtTaxaCompra_TaxaAereo.Text = txtTaxaCompra_TaxaAereo.Text.Replace(".", ",")
                    txtMinimoCompra_TaxaAereo.Text = txtMinimoCompra_TaxaAereo.Text.Replace(".", ",")

                    Exit Sub

                Else



                    'REALIZA INSERT TAXA COMPRA
                    Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,ID_PARCEIRO_EMPRESA, FL_PREMIACAO,CD_PR,FL_DECLARADO ) VALUES (" & txtID_BasicoAereo.Text & "," & ddlDespesa_TaxaAereo.SelectedValue & "," & ddlTipoPagamento_TaxaAereo.SelectedValue & "," & ddlOrigemPagamento_TaxasAereo.SelectedValue & "," & ddlBaseCalculo_TaxaAereo.SelectedValue & "," & ddlMoedaCompra_TaxaAereo.SelectedValue & "," & txtTaxaCompra_TaxaAereo.Text & "," & txtMinimoCompra_TaxaAereo.Text & "," & ddlEmpresa_TaxaAereo.SelectedValue & ",'" & ckbPremiacao_TaxaAereo.Checked & "','P', " & FL_DECLARADO & ") Select SCOPE_IDENTITY() as ID_BL_TAXA ")


                    txtTaxaCompra_TaxaAereo.Text = txtTaxaCompra_TaxaAereo.Text.Replace(".", ",")
                    txtMinimoCompra_TaxaAereo.Text = txtMinimoCompra_TaxaAereo.Text.Replace(".", ",")

                    dgvTaxasAereo.DataBind()
                    Con.Fechar()
                    divSuccess_TaxaAereo2.Visible = True

                End If



            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxaAereo2.Visible = True
                    lblErro_TaxaAereo2.Text = "Usuário não possui permissão para alterar."

                    txtTaxaCompra_TaxaAereo.Text = txtTaxaCompra_TaxaAereo.Text.Replace(".", ",")
                    txtMinimoCompra_TaxaAereo.Text = txtMinimoCompra_TaxaAereo.Text.Replace(".", ",")

                    Exit Sub

                Else
                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & txtID_TaxaAereo.Text & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxasMaritimo1.Visible = True
                        lblErro_TaxasMaritimo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"

                        txtTaxaCompra_TaxaAereo.Text = txtTaxaCompra_TaxaAereo.Text.Replace(".", ",")
                        txtMinimoCompra_TaxaAereo.Text = txtMinimoCompra_TaxaAereo.Text.Replace(".", ",")

                    Else

                        Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD from  View_Taxa_Bloqueada WHERE ID_BL_TAXA = " & txtID_TaxaAereo.Text & " or ID_BL_TAXA_MASTER= " & txtID_TaxaAereo.Text)
                        If ds2.Tables(0).Rows(0).Item("QTD") > 0 Then
                            divErro_TaxaAereo1.Visible = True
                            lblErro_TaxaAereo1.Text = "Não foi possível excluir o registro: a taxa já foi enviada para contas a pagar/receber ou invoice!"

                            txtTaxaCompra_TaxaAereo.Text = txtTaxaCompra_TaxaAereo.Text.Replace(".", ",")
                            txtMinimoCompra_TaxaAereo.Text = txtMinimoCompra_TaxaAereo.Text.Replace(".", ",")

                        Else



                            'REALIZA UPDATE TAXA COMPRA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoAereo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoAereo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxasAereo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaAereo.SelectedValue & ",ID_MOEDA =" & ddlMoedaCompra_TaxaAereo.SelectedValue & ",VL_TAXA = " & txtTaxaCompra_TaxaAereo.Text & ",VL_TAXA_MIN = " & txtMinimoCompra_TaxaAereo.Text & ", ID_PARCEIRO_EMPRESA =  " & ddlEmpresa_TaxaAereo.SelectedValue & ",FL_CALCULADO = 0, FL_PREMIACAO = '" & ckbPremiacao_TaxaAereo.Checked & "', FL_DECLARADO = " & FL_DECLARADO & "WHERE ID_BL_TAXA = " & txtID_TaxaAereo.Text)



                            ''REALIZA UPDATE TAXA VENDA
                            'Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoAereo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoAereo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxasAereo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaAereo.SelectedValue & ",ID_MOEDA =" & ddlMoedaVenda_TaxaAereo.SelectedValue & ",VL_TAXA = " & txtTaxaVenda_TaxaAereo.Text & ",VL_TAXA_MIN = " & txtMinimoVenda_TaxaAereo.Text & ", ID_PARCEIRO_EMPRESA =  " & ddlEmpresa_TaxaAereo.SelectedValue & ",FL_CALCULADO = 0, FL_PREMIACAO = '" & ckbPremiacao_TaxaAereo.Checked & "' WHERE ID_BL_TAXA = " & txtID_TaxaAereo.Text)

                            txtTaxaCompra_TaxaAereo.Text = txtTaxaCompra_TaxaAereo.Text.Replace(".", ",")
                            txtMinimoCompra_TaxaAereo.Text = txtMinimoCompra_TaxaAereo.Text.Replace(".", ",")

                            divSuccess_TaxaAereo2.Visible = True
                            Con.Fechar()
                            dgvTaxasAereo.DataBind()
                        End If
                    End If

                End If

            End If
        Else
            divErro_TaxaAereo2.Visible = True
            lblErro_TaxaAereo2.Text = "É necessário informar um valor para que a taxa seja cadastrada/alterada."
            txtTaxaCompra_TaxaAereo.Text = txtTaxaCompra_TaxaAereo.Text.Replace(".", ",")
            txtMinimoCompra_TaxaAereo.Text = txtMinimoCompra_TaxaAereo.Text.Replace(".", ",")
        End If



        mpeTaxaAereo.Show()

    End Sub


    Private Sub btnSalvar_TaxasMaritimo_Click(sender As Object, e As EventArgs) Handles btnSalvar_TaxasMaritimo.Click
        divSuccess_TaxasMaritimo2.Visible = False
        divErro_TaxasMaritimo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(".", "")
        txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(",", ".")

        txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(".", "")
        txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(",", ".")

        If txtTaxaCompra_TaxasMaritimo.Text = "" Then
            txtTaxaCompra_TaxasMaritimo.Text = 0
        End If

        If txtMinimoCompra_TaxasMaritimo.Text = "" Then
            txtMinimoCompra_TaxasMaritimo.Text = 0
        End If

        Dim FL_DECLARADO As Integer = 0
        If ddlOrigemPagamento_TaxasMaritimo.SelectedValue = 2 Then
            FL_DECLARADO = 1
        End If

        If txtTaxaCompra_TaxasMaritimo.Text <> "" And txtTaxaCompra_TaxasMaritimo.Text <> 0 Then
            If txtID_TaxasMaritimo.Text = "" Then


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxasMaritimo2.Visible = True
                    lblErro_TaxasMaritimo2.Text = "Usuário não possui permissão para cadastrar."

                    txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(".", ",")
                    txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(".", ",")

                    Exit Sub

                Else

                    Dim ID_BL_TAXA As String
                    Dim Calcula As New CalculaBL
                    Dim retorno As String

                    'REALIZA INSERT TAXA COMPRA
                    Dim dstaxa As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,ID_PARCEIRO_EMPRESA,FL_PREMIACAO,CD_PR,FL_DECLARADO) VALUES (" & txtID_BasicoMaritimo.Text & "," & ddlDespesa_TaxasMaritimo.SelectedValue & "," & ddlTipoPagamento_TaxasMaritimo.SelectedValue & "," & ddlOrigemPagamento_TaxasMaritimo.SelectedValue & "," & ddlStatusPagamento_TaxasMaritimo.SelectedValue & "," & ddlBaseCalculo_TaxasMaritimo.SelectedValue & "," & ddlMoedaCompra_TaxasMaritimo.SelectedValue & "," & txtTaxaCompra_TaxasMaritimo.Text & "," & txtMinimoCompra_TaxasMaritimo.Text & "," & ddlEmpresa_TaxasMaritimo.SelectedValue & ",'" & ckbPremiacao_TaxasMaritimo.Checked & "','P'," & FL_DECLARADO & ") Select SCOPE_IDENTITY() as ID_BL_TAXA ")
                    ID_BL_TAXA = dstaxa.Tables(0).Rows(0).Item("ID_BL_TAXA")
                    retorno = Calcula.Calcular(ID_BL_TAXA, "M")

                    dgvTaxasMaritimo.DataBind()

                    txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(".", ",")
                    txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(".", ",")

                    Con.Fechar()

                    divSuccess_TaxasMaritimo2.Visible = True
                End If

            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_TaxasMaritimo2.Visible = True
                    lblErro_TaxasMaritimo2.Text = "Usuário não possui permissão para alterar."

                    txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(".", ",")
                    txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(".", ",")

                    Exit Sub

                Else
                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & txtID_TaxasMaritimo.Text & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxasMaritimo1.Visible = True
                        lblErro_TaxasMaritimo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"

                        txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(".", ",")
                        txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(".", ",")

                    Else

                        Dim ds2 As DataSet = Con.ExecutarQuery("SELECT count(*)QTD from  View_Taxa_Bloqueada WHERE ID_BL_TAXA = " & txtID_TaxasMaritimo.Text & " or ID_BL_TAXA_MASTER= " & txtID_TaxasMaritimo.Text)
                        If ds2.Tables(0).Rows(0).Item("QTD") > 0 Then
                            divErro_TaxasMaritimo1.Visible = True
                            lblErro_TaxasMaritimo1.Text = "Não foi possível excluir o registro: a taxa já foi enviada para contas a pagar/receber ou invoice!"

                            txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(".", ",")
                            txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(".", ",")

                        Else





                            Dim ID_BL_TAXA As String
                            Dim Calcula As New CalculaBL
                            Dim retorno As String

                            'REALIZA UPDATE TAXA COMPRA
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoMaritimo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxasMaritimo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_TaxasMaritimo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxasMaritimo.SelectedValue & ",ID_STATUS_PAGAMENTO = " & ddlStatusPagamento_TaxasMaritimo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxasMaritimo.SelectedValue & ",ID_MOEDA =" & ddlMoedaCompra_TaxasMaritimo.SelectedValue & ",VL_TAXA = " & txtTaxaCompra_TaxasMaritimo.Text & ",VL_TAXA_MIN = " & txtMinimoCompra_TaxasMaritimo.Text & ",ID_PARCEIRO_EMPRESA = " & ddlEmpresa_TaxasMaritimo.SelectedValue & ",FL_CALCULADO = 0, FL_PREMIACAO ='" & ckbPremiacao_TaxasMaritimo.Checked & "', FL_DECLARADO = " & FL_DECLARADO & " WHERE ID_BL_TAXA = " & txtID_TaxasMaritimo.Text)

                            ID_BL_TAXA = txtID_TaxasMaritimo.Text
                            retorno = Calcula.Calcular(ID_BL_TAXA, "M")

                            txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(".", ",")
                            txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(".", ",")

                            dgvTaxasMaritimo.DataBind()
                            divSuccess_TaxasMaritimo2.Visible = True
                            Con.Fechar()
                        End If

                    End If


                End If

            End If
        Else
            txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(".", ",")
            txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(".", ",")
            divErro_TaxasMaritimo2.Visible = True
            lblErro_TaxasMaritimo2.Text = "É necessário informar um valor para que a taxa seja cadastrada/alterada."
        End If
        txtTaxaCompra_TaxasMaritimo.Text = txtTaxaCompra_TaxasMaritimo.Text.Replace(".", ",")
        txtMinimoCompra_TaxasMaritimo.Text = txtMinimoCompra_TaxasMaritimo.Text.Replace(".", ",")
        mpeTaxasMaritimo.Show()

    End Sub

    Private Sub btnSalvar_CNTRMaritimo_Click(sender As Object, e As EventArgs) Handles btnSalvar_CNTRMaritimo.Click
        divSuccess_CNTRMaritimo2.Visible = False
        divErro_CNTRMaritimo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        If txtControle.Text = 0 Then
            divErro_CNTRMaritimo2.Visible = True
            lblErro_CNTRMaritimo2.Text = "Número do Container inválido"
        Else
            txtTara_CNTRMaritimo.Text = txtTara_CNTRMaritimo.Text.Replace(".", "")
            txtTara_CNTRMaritimo.Text = txtTara_CNTRMaritimo.Text.Replace(",", ".")

            If txtFreeTime_CNTRMaritimo.Text = "" Then
                txtFreeTime_CNTRMaritimo.Text = 0
            End If

            If txtTara_CNTRMaritimo.Text = "" Then
                txtTara_CNTRMaritimo.Text = 0
            End If

            If txtNumeroContainer_CNTRMaritimo.Text = "" Then
                txtNumeroContainer_CNTRMaritimo.Text = "NULL"
            Else
                txtNumeroContainer_CNTRMaritimo.Text = "'" & txtNumeroContainer_CNTRMaritimo.Text & "'"
            End If

            If txtLacre_CNTRMaritimo.Text = "" Then
                txtLacre_CNTRMaritimo.Text = "NULL"
            Else
                txtLacre_CNTRMaritimo.Text = "'" & txtLacre_CNTRMaritimo.Text & "'"
            End If

            If txtID_CNTRMaritimo.Text = "" Then


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_CNTRMaritimo2.Visible = True
                    lblErro_CNTRMaritimo2.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else

                    'INSERE 
                    ds = Con.ExecutarQuery("INSERT INTO TB_CNTR_BL (ID_BL_MASTER,ID_TIPO_CNTR,NR_CNTR,NR_LACRE,VL_PESO_TARA,QT_DIAS_FREETIME)  
 VALUES (" & txtID_BasicoMaritimo.Text & "," & ddlTipoContainer_CNTRMaritimo.SelectedValue & "," & txtNumeroContainer_CNTRMaritimo.Text & "," & txtLacre_CNTRMaritimo.Text & "," & txtTara_CNTRMaritimo.Text & "," & txtFreeTime_CNTRMaritimo.Text & ") Select SCOPE_IDENTITY() as ID_CNTR_BL ")

                    AMR_CNTR_INSERT(txtID_BasicoMaritimo.Text, ds.Tables(0).Rows(0).Item("ID_CNTR_BL"))
                    dgvContainer.DataBind()

                    Con.Fechar()

                    txtNumeroContainer_CNTRMaritimo.Text = txtNumeroContainer_CNTRMaritimo.Text.Replace("NULL", "")
                    txtNumeroContainer_CNTRMaritimo.Text = txtNumeroContainer_CNTRMaritimo.Text.Replace("'", "")

                    txtLacre_CNTRMaritimo.Text = txtLacre_CNTRMaritimo.Text.Replace("NULL", "")
                    txtLacre_CNTRMaritimo.Text = txtLacre_CNTRMaritimo.Text.Replace("'", "")

                    divSuccess_CNTRMaritimo2.Visible = True
                End If



            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro_CNTRMaritimo2.Visible = True
                    lblErro_CNTRMaritimo2.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else


                    'REALIZA UPDATE 
                    Con.ExecutarQuery("UPDATE TB_CNTR_BL SET ID_BL_MASTER = " & txtID_BasicoMaritimo.Text & ",ID_TIPO_CNTR = " & ddlTipoContainer_CNTRMaritimo.SelectedValue & ",NR_CNTR = " & txtNumeroContainer_CNTRMaritimo.Text & " ,NR_LACRE = " & txtLacre_CNTRMaritimo.Text & " ,VL_PESO_TARA = " & txtTara_CNTRMaritimo.Text & " ,QT_DIAS_FREETIME = " & txtFreeTime_CNTRMaritimo.Text & " WHERE ID_CNTR_BL = " & txtID_CNTRMaritimo.Text)

                    dgvContainer.DataBind()
                    divSuccess_CNTRMaritimo2.Visible = True
                    Con.Fechar()

                    txtNumeroContainer_CNTRMaritimo.Text = txtNumeroContainer_CNTRMaritimo.Text.Replace("NULL", "")
                    txtNumeroContainer_CNTRMaritimo.Text = txtNumeroContainer_CNTRMaritimo.Text.Replace("'", "")

                    txtLacre_CNTRMaritimo.Text = txtLacre_CNTRMaritimo.Text.Replace("NULL", "")
                    txtLacre_CNTRMaritimo.Text = txtLacre_CNTRMaritimo.Text.Replace("'", "")

                End If



            End If


        End If
        txtTara_CNTRMaritimo.Text = txtTara_CNTRMaritimo.Text.Replace(".", ",")
        mpeCNTRMaritimo.Show()

    End Sub

    Private Sub btnDesvincular_Click(sender As Object, e As EventArgs) Handles btnDesvincular.Click
        divSuccess_Vinculo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        For Each linha As GridViewRow In dgvVinculadas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text

            Dim check As CheckBox = linha.FindControl("PROCESSO")

            If check.Checked Then

                Con.ExecutarQuery("UPDATE [dbo].[TB_BL] SET  [ID_BL_MASTER] = NULL WHERE ID_BL = " & ID)

            End If
        Next
        Con.Fechar()
        divSuccess_Vinculo.Visible = True
        dgvNaoVinculadas.DataBind()
        dgvVinculadas.DataBind()

    End Sub

    Private Sub btnVincular_Click(sender As Object, e As EventArgs) Handles btnVincular.Click
        divSuccess_Vinculo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        For Each linha As GridViewRow In dgvNaoVinculadas.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text

            Dim check As CheckBox = linha.FindControl("PROCESSO")

            If check.Checked Then

                Con.ExecutarQuery("UPDATE [dbo].[TB_BL] SET  [ID_BL_MASTER] = " & txtID_BasicoMaritimo.Text & " WHERE ID_BL = " & ID)

                If txtCotacao_BasicoMaritimo.Text = "" Then
                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL] SET ID_COTACAO = (SELECT ID_COTACAO FROM TB_BL WHERE ID_BL = " & ID & ") WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
                    Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO FROM TB_BL WHERE ID_BL = " & txtID_BasicoMaritimo.Text)

                    Session("ID_COTACAO") = dsCotacao.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                    txtCotacao_BasicoMaritimo.Text = Session("ID_COTACAO")

                ElseIf txtCotacao_BasicoMaritimo.Text = 0 Then
                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL] SET ID_COTACAO = (SELECT ID_COTACAO FROM TB_BL WHERE ID_BL = " & ID & ") WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
                    Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO FROM TB_BL WHERE ID_BL = " & txtID_BasicoMaritimo.Text)

                    Session("ID_COTACAO") = dsCotacao.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                    txtCotacao_BasicoMaritimo.Text = Session("ID_COTACAO")

                End If
                AtualizaHouse(1)

            End If
        Next
        Con.Fechar()
        divSuccess_Vinculo.Visible = True
        dgvNaoVinculadas.DataBind()
        dgvVinculadas.DataBind()

    End Sub

    Private Sub txtNavioFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtNavioFiltro.TextChanged
        dsNavios.SelectParameters("Nome").DefaultValue = txtNavioFiltro.Text
        rdNavios.DataBind()
    End Sub
    Private Sub txtNavioFiltro1_TextChanged(sender As Object, e As EventArgs) Handles txtNavioFiltro1.TextChanged
        dsNavios.SelectParameters("Nome").DefaultValue = txtNavioFiltro1.Text
        rdNavios1.DataBind()
    End Sub
    Private Sub txtNavioFiltro2_TextChanged(sender As Object, e As EventArgs) Handles txtNavioFiltro2.TextChanged
        dsNavios.SelectParameters("Nome").DefaultValue = txtNavioFiltro2.Text
        rdNavios2.DataBind()
    End Sub
    Private Sub txtNavioFiltro3_TextChanged(sender As Object, e As EventArgs) Handles txtNavioFiltro3.TextChanged
        dsNavios.SelectParameters("Nome").DefaultValue = txtNavioFiltro3.Text
        rdNavios3.DataBind()
    End Sub
    Private Sub btnSalvarNavio_Click(sender As Object, e As EventArgs) Handles btnSalvarNavio.Click
        Dim id As String = rdNavios.SelectedValue
        Dim nome As String = rdNavios.SelectedItem.Text

        ddlNavio_BasicoMaritimo.Items.Insert(1, id & " - " & nome)
        ddlNavio_BasicoMaritimo.SelectedIndex = 1
    End Sub

    Private Sub btnSalvarNavio1_Click(sender As Object, e As EventArgs) Handles btnSalvarNavio1.Click
        Dim id As String = rdNavios1.SelectedValue
        Dim nome As String = rdNavios1.SelectedItem.Text

        ddlNavio1_BasicoMaritimo.Items.Insert(1, id & " - " & nome)
        ddlNavio1_BasicoMaritimo.SelectedIndex = 1
    End Sub
    Private Sub btnSalvarNavio2_Click(sender As Object, e As EventArgs) Handles btnSalvarNavio2.Click
        Dim id As String = rdNavios2.SelectedValue
        Dim nome As String = rdNavios2.SelectedItem.Text


        ddlNavio2_BasicoMaritimo.Items.Insert(1, id & " - " & nome)
        ddlNavio2_BasicoMaritimo.SelectedIndex = 1
    End Sub
    Private Sub btnSalvarNavio3_Click(sender As Object, e As EventArgs) Handles btnSalvarNavio3.Click
        Dim id As String = rdNavios3.SelectedValue
        Dim nome As String = rdNavios3.SelectedItem.Text


        ddlNavio3_BasicoMaritimo.Items.Insert(1, id & " - " & nome)
        ddlNavio3_BasicoMaritimo.SelectedIndex = 1
    End Sub
    Function SeparaIDNavio(TEXTO As String) As String
        Dim ID As String = ""

        If TEXTO <> "" Then 'Len(TEXTO) > 0
            TEXTO = TEXTO.Substring(0, TEXTO.IndexOf("-"))
            ID = TEXTO
        End If

        Return ID
    End Function

    Sub Week(tipo As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If tipo = 1 Then
            ds = Con.ExecutarQuery("UPDATE TB_BL SET ID_WEEK = " & ddlWeekMaritimo.SelectedValue & " WHERE ID_BL = " & txtID_BasicoMaritimo.Text & "")
            ds = Con.ExecutarQuery("UPDATE TB_BL SET ID_WEEK = " & ddlWeekMaritimo.SelectedValue & " WHERE GRAU = 'C' AND ID_BL_MASTER = " & txtID_BasicoMaritimo.Text & "")


        ElseIf tipo = 2 Then
            ds = Con.ExecutarQuery("UPDATE TB_BL SET ID_WEEK = " & ddlWeekAereo.SelectedValue & " WHERE ID_BL = " & txtID_BasicoAereo.Text & "")
            ds = Con.ExecutarQuery("UPDATE TB_BL SET ID_WEEK = " & ddlWeekAereo.SelectedValue & " WHERE GRAU = 'C' AND ID_BL_MASTER = " & txtID_BasicoAereo.Text & "")

        End If

    End Sub

    Sub AtualizaHouse(tipo As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If tipo = 1 Then



            Con.ExecutarQuery("UPDATE TB_BL SET 
NR_VIAGEM = (SELECT NR_VIAGEM FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
NR_VIAGEM_1T = (SELECT NR_VIAGEM_1T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
NR_VIAGEM_2T =(SELECT NR_VIAGEM_2T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") ,
NR_VIAGEM_3T = (SELECT NR_VIAGEM_3T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") , 
DT_1T =(SELECT DT_1T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") , 
DT_2T = (SELECT DT_2T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "), 
DT_3T =(SELECT DT_3T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") , 
ID_PORTO_1T =(SELECT ID_PORTO_1T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
ID_PORTO_3T =(SELECT ID_PORTO_3T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
ID_PORTO_2T =(SELECT ID_PORTO_2T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "), 
DT_PREVISAO_EMBARQUE =  (SELECT DT_PREVISAO_EMBARQUE FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
DT_PREVISAO_CHEGADA = (SELECT DT_PREVISAO_CHEGADA FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
DT_CHEGADA = (SELECT DT_CHEGADA FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
DT_EMBARQUE = (SELECT DT_EMBARQUE FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
ID_NAVIO = (SELECT ID_NAVIO FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") ,
ID_NAVIO_1T = (SELECT ID_NAVIO_1T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") , 
ID_NAVIO_2T = (SELECT ID_NAVIO_2T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") ,
ID_NAVIO_3T =  (SELECT ID_NAVIO_3T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
ID_PARCEIRO_ARMAZEM_ATRACACAO =(SELECT ID_PARCEIRO_ARMAZEM_ATRACACAO FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
ID_PARCEIRO_ARMAZEM_DESCARGA = (SELECT ID_PARCEIRO_ARMAZEM_DESCARGA FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "), 
ID_STATUS_FRETE_AGENTE =(SELECT ID_STATUS_FRETE_AGENTE FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & "),
ID_PARCEIRO_TRANSPORTADOR =(SELECT ID_PARCEIRO_TRANSPORTADOR FROM TB_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ")
WHERE GRAU = 'C' AND ID_BL_MASTER = " & txtID_BasicoMaritimo.Text)



            ds = Con.ExecutarQuery("SELECT 
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
end ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO,

CASE 
 WHEN ID_TIPO_PAGAMENTO = 1
 THEN 0
 WHEN ID_TIPO_PAGAMENTO = 2
 THEN 1
 END FL_DECLARADO FROM TB_BL WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
            If ds.Tables(0).Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then

                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET 
ID_TIPO_PAGAMENTO = " & ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO") & "
WHERE ID_BL IN (SELECT ID_BL FROM TB_BL  WHERE ID_BL_MASTER = " & txtID_BasicoMaritimo.Text & " AND GRAU= 'C')
AND ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)
AND CD_PR='P'
AND ID_BL_TAXA NOT IN (SELECT ISNULL(F.ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS AS F INNER JOIN TB_CONTA_PAGAR_RECEBER AS E ON F.ID_CONTA_PAGAR_RECEBER = E.ID_CONTA_PAGAR_RECEBER WHERE E.DT_CANCELAMENTO IS NULL)")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET

ID_ORIGEM_PAGAMENTO = " & ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO") & "

WHERE ID_BL IN (SELECT ID_BL FROM TB_BL  WHERE ID_BL_MASTER = " & txtID_BasicoMaritimo.Text & " AND GRAU= 'C')
AND ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)
AND CD_PR='P'
AND ID_BL_TAXA NOT IN (SELECT ISNULL(F.ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS AS F INNER JOIN TB_CONTA_PAGAR_RECEBER AS E ON F.ID_CONTA_PAGAR_RECEBER = E.ID_CONTA_PAGAR_RECEBER WHERE E.DT_CANCELAMENTO IS NULL)")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DECLARADO")) Then

                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET 
FL_DECLARADO = " & ds.Tables(0).Rows(0).Item("FL_DECLARADO") & "
WHERE ID_BL IN (SELECT ID_BL FROM TB_BL  WHERE ID_BL_MASTER = " & txtID_BasicoMaritimo.Text & " AND GRAU= 'C')
AND ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)
AND CD_PR='P'
AND ID_BL_TAXA NOT IN (SELECT ISNULL(F.ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS AS F INNER JOIN TB_CONTA_PAGAR_RECEBER AS E ON F.ID_CONTA_PAGAR_RECEBER = E.ID_CONTA_PAGAR_RECEBER WHERE E.DT_CANCELAMENTO IS NULL)")
                End If

            End If

        ElseIf tipo = 2 Then


            ds = Con.ExecutarQuery("UPDATE TB_BL SET 
NR_VIAGEM = (SELECT NR_VIAGEM FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "),
NR_VIAGEM_1T = (SELECT NR_VIAGEM_1T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "),
NR_VIAGEM_2T =(SELECT NR_VIAGEM_2T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") ,
NR_VIAGEM_3T = (SELECT NR_VIAGEM_3T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") , 
DT_1T =(SELECT DT_1T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") , 
DT_2T = (SELECT DT_2T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "), 
DT_3T =(SELECT DT_3T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") , 
ID_PORTO_1T =(SELECT ID_PORTO_1T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "),
ID_PORTO_3T =(SELECT ID_PORTO_3T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "),
ID_PORTO_2T =(SELECT ID_PORTO_2T FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "), 
DT_PREVISAO_EMBARQUE =  (SELECT DT_PREVISAO_EMBARQUE FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "),
DT_PREVISAO_CHEGADA = (SELECT DT_PREVISAO_CHEGADA FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "),
DT_CHEGADA = (SELECT DT_CHEGADA FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "),
DT_EMBARQUE = (SELECT DT_EMBARQUE FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "),
ID_STATUS_FRETE_AGENTE =(SELECT ID_STATUS_FRETE_AGENTE FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & "),
ID_PARCEIRO_TRANSPORTADOR =(SELECT ID_PARCEIRO_TRANSPORTADOR FROM TB_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ")
WHERE GRAU = 'C' AND ID_BL_MASTER = " & txtID_BasicoAereo.Text)

            ds = Con.ExecutarQuery("SELECT 
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
end ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO,

CASE 
 WHEN ID_TIPO_PAGAMENTO = 1
 THEN 0
 WHEN ID_TIPO_PAGAMENTO = 2
 THEN 1
 END FL_DECLARADO FROM TB_BL WHERE ID_BL = " & txtID_BasicoAereo.Text)
            If ds.Tables(0).Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then

                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET 
ID_TIPO_PAGAMENTO = " & ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO") & "
WHERE ID_BL IN (SELECT ID_BL FROM TB_BL  WHERE ID_BL_MASTER = " & txtID_BasicoAereo.Text & " AND GRAU= 'C')
AND ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)
AND CD_PR='P'
AND ID_BL_TAXA NOT IN (SELECT ISNULL(F.ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS AS F INNER JOIN TB_CONTA_PAGAR_RECEBER AS E ON F.ID_CONTA_PAGAR_RECEBER = E.ID_CONTA_PAGAR_RECEBER WHERE E.DT_CANCELAMENTO IS NULL)")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET

ID_ORIGEM_PAGAMENTO = " & ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO") & "

WHERE ID_BL IN (SELECT ID_BL FROM TB_BL  WHERE ID_BL_MASTER = " & txtID_BasicoAereo.Text & " AND GRAU= 'C')
AND ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)
AND CD_PR='P'
AND ID_BL_TAXA NOT IN (SELECT ISNULL(F.ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS AS F INNER JOIN TB_CONTA_PAGAR_RECEBER AS E ON F.ID_CONTA_PAGAR_RECEBER = E.ID_CONTA_PAGAR_RECEBER WHERE E.DT_CANCELAMENTO IS NULL)")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DECLARADO")) Then

                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET 
FL_DECLARADO = " & ds.Tables(0).Rows(0).Item("FL_DECLARADO") & "
WHERE ID_BL IN (SELECT ID_BL FROM TB_BL  WHERE ID_BL_MASTER = " & txtID_BasicoAereo.Text & " AND GRAU= 'C')
AND ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)
AND CD_PR='P'
AND ID_BL_TAXA NOT IN (SELECT ISNULL(F.ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS AS F INNER JOIN TB_CONTA_PAGAR_RECEBER AS E ON F.ID_CONTA_PAGAR_RECEBER = E.ID_CONTA_PAGAR_RECEBER WHERE E.DT_CANCELAMENTO IS NULL)")
                End If

            End If

        End If

    End Sub

    Private Sub ddlDestino_BasicoMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDestino_BasicoMaritimo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim sql As String = "SELECT ID_WEEK, NM_WEEK FROM TB_WEEK WHERE  ID_PORTO_ORIGEM_LOCAL = " & ddlDestino_BasicoMaritimo.SelectedValue & " AND ID_PORTO_ORIGEM_DESTINO = " & ddlOrigem_BasicoMaritimo.SelectedValue & "
union SELECT 0, 'Selecione' FROM TB_WEEK ORDER BY ID_WEEK"
        Dim ds As DataSet = Con.ExecutarQuery(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsWeekMaritimo.SelectCommand = sql
            ddlWeekMaritimo.DataBind()
        End If
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
        If Now.Month <10 Then
            mes_atual="0" & Now.Month.ToString
        Else
            mes_atual= Now.Month.ToString
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

                Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "' , ANOSEQUENCIALPROCESSO = year(getdate())")

                Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = '" & PROCESSO_FINAL & "' WHERE ID_BL = " & txtID_BasicoAereo.Text)
                txtProcesso_BasicoAereo.Text = PROCESSO_FINAL

            End If



        End If
    End Sub
    Private Sub ddlDestino_BasicoAereo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDestino_BasicoAereo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim sql As String = "SELECT ID_WEEK, NM_WEEK FROM TB_WEEK WHERE ID_PORTO_ORIGEM_LOCAL = " & ddlDestino_BasicoAereo.SelectedValue & " AND ID_PORTO_ORIGEM_DESTINO = " & ddlOrigem_BasicoAereo.SelectedValue & "
union SELECT 0, 'Selecione' FROM TB_WEEK ORDER BY ID_WEEK"
        Dim ds As DataSet = Con.ExecutarQuery(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsWeekAereo.SelectCommand = sql
            ddlWeekAereo.DataBind()
        End If
    End Sub

    Private Sub ddlDespesa_TaxasMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDespesa_TaxasMaritimo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT FL_PREMIACAO FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlDespesa_TaxasMaritimo.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            ckbPremiacao_TaxasMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_PREMIACAO")
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

    Sub AMR_CNTR_INSERT(ID_BL As Integer, CNTR As Integer)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim dsAMR As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_AMR_CNTR_BL)QTD FROM TB_AMR_CNTR_BL WHERE ID_BL =" & ID_BL & "  AND ID_CNTR_BL = " & CNTR)

        If dsAMR.Tables(0).Rows(0).Item("QTD") = 0 Then
            Con.ExecutarQuery("INSERT INTO TB_AMR_CNTR_BL (ID_BL,ID_CNTR_BL) VALUES(" & ID_BL & "," & CNTR & ")")
        End If

    End Sub

    Private Sub ddlTipoContainer_CNTRMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoContainer_CNTRMaritimo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        If ddlTipoContainer_CNTRMaritimo.SelectedValue <> 0 Then
            Dim ds As DataSet = Con.ExecutarQuery("SELECT QT_DIAS_FREETIME FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = (SELECT ID_COTACAO FROM TB_BL WHERE ID_BL = " & txtID_BasicoMaritimo.Text & ") AND ID_TIPO_CONTAINER  = " & ddlTipoContainer_CNTRMaritimo.SelectedValue)

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")) Then
                    txtFreeTime_CNTRMaritimo.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FREETIME")
                End If
            End If
        End If
    End Sub

    Private Sub txtNomeEmpresa_TaxasMaritimo_TextChanged(sender As Object, e As EventArgs) Handles txtNomeEmpresa_TaxasMaritimo.TextChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCodEmpresa_TaxasMaritimo.Text = "" Then
            txtCodEmpresa_TaxasMaritimo.Text = 0
        End If
        If txtNomeEmpresa_TaxasMaritimo.Text = "" Then
            txtNomeEmpresa_TaxasMaritimo.Text = "NULL"
        End If

        Dim Sql As String = "SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE  (NM_RAZAO like '%" & txtNomeEmpresa_TaxasMaritimo.Text & "%' or ID_PARCEIRO =  " & txtCodEmpresa_TaxasMaritimo.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE  (NM_FANTASIA like '%" & txtNomeEmpresa_TaxasMaritimo.Text & "%' or ID_PARCEIRO =  " & txtCodEmpresa_TaxasMaritimo.Text & ") 
UNION
SELECT ID_PARCEIRO,NM_RAZAO,Case when TP_PESSOA = 1 then NM_RAZAO +' - ' + CNPJ when TP_PESSOA = 2 then  NM_RAZAO +' - ' + CPF  else NM_RAZAO + ' (' + CONVERT(VARCHAR,ID_PARCEIRO) + ')' end as Descricao FROM TB_PARCEIRO WHERE (CNPJ like '%" & txtNomeEmpresa_TaxasMaritimo.Text & "%' or ID_PARCEIRO =  " & txtCodEmpresa_TaxasMaritimo.Text & ") 
UNION 
SELECT  0,'', ' Selecione' FROM TB_PARCEIRO ORDER BY NM_RAZAO"


        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsFornecedorMaritimo.SelectCommand = Sql
            dsFornecedorMaritimo.DataBind()
            ddlEmpresa_TaxasMaritimo.DataBind()
        End If
        txtNomeEmpresa_TaxasMaritimo.Text = txtNomeEmpresa_TaxasMaritimo.Text.Replace("NULL", "")

    End Sub

    Private Sub btnDesvincularAereo_Click(sender As Object, e As EventArgs) Handles btnDesvincularAereo.Click
        divSuccess_VinculoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        For Each linha As GridViewRow In dgvVinculadosAereos.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text

            Dim check As CheckBox = linha.FindControl("PROCESSO")

            If check.Checked Then

                Con.ExecutarQuery("UPDATE [dbo].[TB_BL] SET  [ID_BL_MASTER] = NULL WHERE ID_BL = " & ID)

            End If
        Next
        Con.Fechar()
        divSuccess_VinculoAereo.Visible = True
        dgvNaoVinculadosAereos.DataBind()
        dgvVinculadosAereos.DataBind()

    End Sub

    Private Sub btnVincularAereo_Click(sender As Object, e As EventArgs) Handles btnVincularAereo.Click
        divSuccess_VinculoAereo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        For Each linha As GridViewRow In dgvNaoVinculadosAereos.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text

            Dim check As CheckBox = linha.FindControl("PROCESSO")

            If check.Checked Then

                Con.ExecutarQuery("UPDATE [dbo].[TB_BL] SET  [ID_BL_MASTER] = " & txtID_BasicoAereo.Text & " WHERE ID_BL = " & ID)

                If txtCotacao_BasicoAereo.Text = "" Then
                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL] SET ID_COTACAO = (SELECT ID_COTACAO FROM TB_BL WHERE ID_BL = " & ID & ") WHERE ID_BL = " & txtID_BasicoAereo.Text)
                    Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO FROM TB_BL WHERE ID_BL = " & txtID_BasicoAereo.Text)

                    Session("ID_COTACAO") = dsCotacao.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                    txtCotacao_BasicoAereo.Text = Session("ID_COTACAO")

                ElseIf txtCotacao_BasicoAereo.Text = 0 Then
                    Con.ExecutarQuery("UPDATE [dbo].[TB_BL] SET ID_COTACAO = (SELECT ID_COTACAO FROM TB_BL WHERE ID_BL = " & ID & ") WHERE ID_BL = " & txtID_BasicoAereo.Text)
                    Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO FROM TB_BL WHERE ID_BL = " & txtID_BasicoAereo.Text)

                    Session("ID_COTACAO") = dsCotacao.Tables(0).Rows(0).Item("ID_COTACAO").ToString()
                    txtCotacao_BasicoAereo.Text = Session("ID_COTACAO")

                End If
                AtualizaHouse(2)

            End If
        Next
        Con.Fechar()
        divSuccess_VinculoAereo.Visible = True
        dgvNaoVinculadosAereos.DataBind()
        dgvVinculadosAereos.DataBind()
    End Sub
End Class