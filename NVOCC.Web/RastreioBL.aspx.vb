Imports Newtonsoft.Json
Public Class RastreioBL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("id") <> "" Then


            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_BL,NR_BL,TRAKING_BL,BL_TOKEN FROM [TB_BL] WHERE NR_BL IS NOT NULL AND ID_BL = " & Request.QueryString("id"))
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TRAKING_BL")) Then
                    Dim data = DeserializarNewtonsoft(ds.Tables(0).Rows(0).Item("TRAKING_BL"))

                    nr_bl.Text = ds.Tables(0).Rows(0).Item("NR_BL")

                    If data.references.bill_of_lading <> ds.Tables(0).Rows(0).Item("NR_BL") Then

                        Con.ExecutarQuery("UPDATE TB_BL SET BL_TOKEN = NULL, TRAKING_BL = NULL WHERE ID_BL = " & Request.QueryString("id"))
                        Atualizar()


                    Else

                        'Transporte e Logistica
                        pais_procedencia.Text = data.transport.origin_country
                        pais_destino.Text = data.transport.destination_country
                        porto_embarque.Text = data.transport.lading_port
                        porto_embarque.Text = data.transport.origin_port
                        porto_descarga.Text = data.transport.landing_terminal
                        porto_destino.Text = data.transport.destination_port
                        porto_origem.Text = data.transport.origin_port

                        'Principal
                        status.Text = data.tracking.status_description
                        Dim estimatedTime As DateTime = Nothing
                        Dim dataOperacao As DateTime = Nothing
                        Dim dataEmissao As DateTime = Nothing
                        Dim dataEmbarque As DateTime = Nothing
                        Dim dataOperacao2 As DateTime = Nothing
                        Dim dataETAArmador As DateTime = Nothing
                        Dim dataUltimaAtualizacao As DateTime = Nothing
                        Dim dataEmissaoCE As DateTime = Nothing
                        Dim dataManifesto As DateTime = Nothing
                        Dim dataPresenca As DateTime = Nothing



                        If data.dates.eta <> Nothing Then
                            estimatedTime = data.dates.eta
                        End If

                        If data.dates.bl_emission_date <> Nothing Then
                            dataOperacao = data.dates.bl_emission_date
                            dataEmissao = data.dates.bl_emission_date
                        End If

                        If data.dates.loading <> Nothing Then
                            dataEmbarque = data.dates.loading
                        End If

                        If data.dates.operation_date <> Nothing Then
                            dataOperacao2 = data.dates.operation_date
                        End If

                        If data.dates.bl_emission_date <> Nothing Then
                            dataETAArmador = data.dates.bl_emission_date
                        End If

                        If data.dates.last_update <> Nothing Then
                            dataUltimaAtualizacao = data.dates.last_update
                        End If

                        If data.dates.manifested_at <> Nothing Then
                            dataManifesto = data.dates.manifested_at
                        End If

                        If data.dates.last_update <> Nothing Then
                            dataPresenca = data.dates.last_update
                        End If

                        If data.dates.emission <> Nothing Then
                            dataEmissaoCE = data.dates.emission
                        End If

                        'Transporte e Logistica
                        pais_procedencia.Text = data.transport.origin_country
                        pais_origem.Text = data.transport.origin_country
                        pais_destino.Text = data.transport.destination_country

                        porto_embarque.Text = data.transport.origin_port & " " & data.transport.origin_port_code


                        'porto_embarque.Text = data.transport.origin_port
                        terminal_descarga.Text = data.transport.landing_terminal

                        porto_descarga.Text = data.transport.destination_port
                        porto_destino.Text = data.transport.destination_port & " " & data.transport.destination_port_code
                        porto_origem.Text = data.transport.origin_port & " " & data.transport.origin_port_code
                        terminal_embarque.Text = data.transport.lading_terminal


                        conteiners.Text = data.transport.containers

                        'Principal
                        status.Text = data.tracking.status_description
                        bl.Text = data.references.bill_of_lading
                        consig_informado.Text = data.tracking.consignee_info
                        fluxo.Text = data.references.transaction
                        navio.Text = data.transport.vessel_name
                        imo.Text = data.transport.vessel_imo
                        conta.Text = data.tracking.customer_name
                        embarque.Text = data.tracking.maritime_type_name
                        tipo.Text = "Maritimo"
                        tipo_carga.Text = data.references.cargo_type
                        data_operacao.Text = dataOperacao
                        viagem.Text = data.transport.voyage_number
                        identificador_token.Text = data.references.tracking_token
                        situacao.Text = "Ativo"
                        eta.Text = estimatedTime.ToString("dd/MM/yyyy")

                        'ADUANA
                        ce.Text = data.aduana.ce_number
                        manifesto.Text = data.aduana.manifest

                        'MERCADORIA
                        volume_m3.Text = data.commodity.unit_quantity
                        peso_bruto.Text = data.commodity.gross_weight_kg
                        teus.Text = data.commodity.teus
                        cntrs.Text = data.commodity.c40
                        total_cntrs.Text = data.commodity.fcl_total
                        Dim f = data.fcl.Count()

                        'ITENS MERCADORIA
                        Dim tabela1 As String = "<table class='table'>"
                        tabela1 &= "<thead>"
                        tabela1 &= "<tr>"
                        tabela1 &= "<th style='padding-left:10px;padding-right:10px'>CONTAINER</th>"
                        tabela1 &= "<th style='padding-left:10px;padding-right:10px'>LACRE</th>"
                        tabela1 &= "<th style='padding-left:10px;padding-right:10px'>PES</th>"
                        tabela1 &= "<th style='padding-left:10px;padding-right:10px'>TARA</th>"
                        tabela1 &= "<th style='padding-left:10px;padding-right:10px'>PESO BRUTO</th>"
                        tabela1 &= "<th style='padding-left:10px;padding-right:10px'>VOLUME(M3)</th>"
                        tabela1 &= "<th style='padding-left:10px;padding-right:10px'>HISTORICO</th>"
                        tabela1 &= "</tr>"
                        tabela1 &= "</thead>"
                        tabela1 &= "<tbody>"

                        If f > 0 Then
                            For Each item As Itens In data.fcl()


                                tabela1 &= "<tr>"
                                tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.container_number & "</td>"
                                tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.seal_number & "</td>"
                                tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.feet & "</td>"
                                tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.gross_weight_decimal & "</td>"
                                tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.tare_container & "</td>"
                                tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.volume_m3 & "</td>"
                                tabela1 &= "<td style='padding-left:10px;padding-right:10px'></td>"
                                tabela1 &= "</tr>"
                            Next
                        Else
                            tabela1 &= "<tr>"
                            tabela1 &= "<td colspan='6' style='padding-left:10px;padding-right:10px'>Não há dados de retorno</td>"
                            tabela1 &= "</tr>"
                        End If


                        tabela1 &= "</tbody>"
                        tabela1 &= "</table>"
                        divCNTR.InnerHtml = tabela1


                        'ENVOLVIDOS
                        consignatario.Text = data.tracking.consignee_info
                        armador.Text = data.logistics.carrier_name
                        agente_carga.Text = data.logistics.forwarder_name
                        agencia_maritima.Text = data.logistics.shipping_agency
                        armador_informado.Text = data.logistics.informed_carrier_name '
                        agente_internacional.Text = data.logistics.forwarder_name_foreign

                        'IMPORTADOR
                        'atividade.Text = data.consignee.activity
                        'telefone.Text = data.consignee.phone
                        'natureza.Text = data.consignee.legal_nature
                        'email.Text = data.consignee.email

                        'DATAS
                        data_cadastro.Text = data.dates.created_datetime
                        data_emissao_bl.Text = dataEmissao.ToString("dd/MM/yyyy")
                        data_embarque.Text = dataEmbarque.ToString("dd/MM/yyyy")
                        data_operacao2.Text = dataOperacao2.ToString("dd/MM/yyyy")
                        data_eta_armador.Text = dataETAArmador.ToString("dd/MM/yyyy")

                        data_ultima_atualizacao.Text = dataUltimaAtualizacao.ToString("dd/MM/yyyy hh:mm")
                        data_emissao_ce.Text = dataEmissaoCE.ToString("dd/MM/yyyy")
                        data_manifesto.Text = dataManifesto.ToString("dd/MM/yyyy")
                        data_presenca_carga.Text = dataPresenca.ToString("dd/MM/yyyy")
                        eta2.Text = estimatedTime.ToString("dd/MM/yyyy")


                        Dim c As Integer = data.documents.Count()

                        'DOCUMENTOS
                        Dim tabela As String = "<table class='table'>"
                        tabela &= "<thead>"
                        tabela &= "<tr>"
                        tabela &= "<th style='padding-left:10px;padding-right:10px'>Acões</th>"
                        tabela &= "<th style='padding-left:10px;padding-right:10px'>Data / Hora</th>"
                        tabela &= "<th style='padding-left:10px;padding-right:10px'>Documento / Arquivo</th>"
                        tabela &= "<tr>"
                        tabela &= "</thead>"
                        tabela &= "<tbody>"

                        If c > 0 Then
                            For Each item As Documentos In data.documents()
                                tabela &= "<tr>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'><a href=" & item.url & ">Baixar Arquivo</a></td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & item.created_at & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & item.name & "</td>"
                                tabela &= "</tr>"
                            Next
                        Else
                            tabela &= "<tr>"
                            tabela &= "<td colspan='4' style='padding-left:10px;padding-right:10px'> Não há dados de retorno.</td>"
                            tabela &= "</tr>"
                        End If
                        tabela &= "</tbody>"
                        tabela &= "</table>"
                        divConteudoDinamico.InnerHtml = tabela

                        'WORKFLOW

                        Dim traking As String = ""
                        For Each item As FollowUps In data.follow_ups
                            traking &= "<div class='tracking-item'>"
                            traking &= "<div class='tracking-icon status-intransit'>"
                            traking &= "<svg class='svg-inline--fa fa-circle fa-w-16' aria-hidden='true' data-prefix='fas' data-icon='circle' role='img' xmlns='http://www.w3.org/2000/svg' viewBox='0 0 512 512' data-fa-i2svg=''>"
                            traking &= "<path fill='currentColor' d='M256 8C119 8 8 119 8 256s111 248 248 248 248-111 248-248S393 8 256 8z'></path>"
                            traking &= "</svg>"
                            traking &= "</div>"
                            traking &= "<div class='tracking-date'>" & item.date_time & "</span></div>"
                            traking &= "<div class='tracking-content'>" & item.comment_history & "</span></div>"
                            traking &= "</div>"
                        Next
                        trakinglist.InnerHtml = traking
                        'FOLLOWUP
                        Dim countFollowup As Integer = data.follow_ups.Count()
                        Dim tb_folloup As String = "<table class='table'>"
                        tb_folloup &= "<thead>"
                        tb_folloup &= "<tr>"
                        tb_folloup &= "<th style='padding-left:10px;padding-right:10px'>Data / Hora</th>"
                        tb_folloup &= "<th style='padding-left:10px;padding-right:10px'>COMENTÁRIO/HISTÓRICO</th>"
                        tb_folloup &= "<th style='padding-left:10px;padding-right:10px'>RESPONSÁVEL / Arquivo</th>"
                        tb_folloup &= "<tr>"
                        tb_folloup &= "</thead>"
                        tb_folloup &= "<tbody>"

                        If countFollowup > 0 Then
                            For Each item As FollowUps In data.follow_ups
                                tb_folloup &= "<tr>"
                                tb_folloup &= "<td style='padding-left:10px;padding-right:10px'>" & item.date_time & "</td>"
                                tb_folloup &= "<td style='padding-left:10px;padding-right:10px'>" & item.comment_history & "</td>"
                                tb_folloup &= "<td style='padding-left:10px;padding-right:10px'>" & item.user & "</td>"
                                tb_folloup &= "</tr>"
                            Next
                        Else
                            tb_folloup &= "<tr>"
                            tb_folloup &= "<td style='padding-left:10px;padding-right:10px'>Não há dados de retorno</td>"
                            tb_folloup &= "</tr>"
                        End If


                        tb_folloup &= "</tbody>"
                        tb_folloup &= "</table>"
                        followup.InnerHtml = tb_folloup

                        mapa.Attributes.Add("src", "https://www.google.com/maps?saddr=[" & data.transport.origin_country & "]&daddr=[" & data.transport.destination_country & "]&z=2&output=embed")

                    End If
                End If
            End If
        End If

    End Sub
    Private Function DeserializarNewtonsoft(tracking As String) As BL
        Dim Json = tracking
        Return JsonConvert.DeserializeObject(Of BL)(Json)
    End Function

    Sub Atualizar()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim Rastreio As New RastreioService
        Rastreio.trackingbl(Request.QueryString("id"))
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_BL,NR_BL,TRAKING_BL FROM [TB_BL] WHERE NR_BL IS NOT NULL AND ID_BL = " & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("TRAKING_BL")) Then
                Response.Redirect("RastreioBL.aspx?id=" & Request.QueryString("id"))
            End If
        End If
    End Sub
    Protected Sub btnAtualizar_Click(sender As Object, e As EventArgs) Handles btnAtualizar.Click
        Atualizar()
    End Sub
End Class


