Imports Newtonsoft.Json
Public Class RastreioHBL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" Then

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ID_BL,NR_BL,TRAKING_BL,BL_TOKEN FROM [TB_BL] WHERE NR_BL IS NOT NULL AND ID_BL = " & Request.QueryString("id"))
            If ds1.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds1.Tables(0).Rows(0).Item("TRAKING_BL")) Then
                    Dim data = DeserializarNewtonsoft(ds1.Tables(0).Rows(0).Item("TRAKING_BL"))

                    nr_bl.Text = ds1.Tables(0).Rows(0).Item("NR_BL")

                    If data.references.bill_of_lading <> ds1.Tables(0).Rows(0).Item("NR_BL") Then

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
                        bl.Text = data.references.bill_of_lading
                        consig_informado.Text = data.tracking.consignee_info
                        fluxo.Text = data.references.transaction
                        navio.Text = data.transport.vessel_name
                        imo.Text = data.transport.vessel_imo
                        conta.Text = data.tracking.customer_name
                        embarque.Text = data.tracking.maritime_type_name
                        tipo.Text = "Maritimo"
                        tipo_carga.Text = data.references.cargo_type
                        data_operacao.Text = data.dates.bl_emission_date
                        viagem.Text = data.transport.voyage_number
                        identificador_token.Text = data.references.tracking_token
                        situacao.Text = "Ativo"
                        eta.Text = data.dates.eta
                        'ADUANA
                        'ce.Text = data.aduana.ce_number
                        manifesto.Text = data.aduana.manifest
                        'Mercadoria
                        volume_m3.Text = data.commodity.unit_quantity
                        peso_bruto.Text = data.commodity.gross_weight_kg
                        teus.Text = data.commodity.teus
                        cntrs.Text = data.commodity.c40
                        total_cntrs.Text = data.commodity.fcl_total











                        'ENVOLVIDOS
                        consignatario.Text = data.logistics.forwarder_name
                        armador.Text = data.logistics.carrier_name
                        agente_carga.Text = data.logistics.forwarder_name
                        agencia_maritima.Text = data.logistics.shipping_agency
                        armador_informado.Text = data.logistics.informed_carrier_name
                        agente_internacional.Text = data.logistics.forwarder_name_foreign

                        'IMPORTADOR
                        'atividade.Text = data.consignee.activity
                        'telefone.Text = data.consignee.phone
                        'natureza.Text = data.consignee.legal_nature
                        'email.Text = data.consignee.email

                        'DATAS
                        data_cadastro.Text = data.dates.created_datetime
                        data_emissao_bl.Text = data.dates.bl_emission_date
                        data_embarque.Text = data.dates.loading
                        data_operacao2.Text = data.dates.operation_date
                        data_eta_armador.Text = data.dates.shipowner_eta
                        data_ultima_atualizacao.Text = data.dates.last_update
                        data_emissao_ce.Text = data.dates.bl_emission_date
                        data_manifesto.Text = data.dates.manifested_at
                        data_presenca_carga.Text = data.dates.last_update
                        eta2.Text = data.dates.eta

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
                        For Each item As Documentos In data.documents
                            tabela &= "<tr>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'><a href=" & item.url & ">Baixar Arquivo</a></td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & item.created_at & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & item.name & "</td>"
                            tabela &= "</tr>"
                        Next
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
                        Dim tb_folloup As String = "<table class='table'>"
                        tb_folloup &= "<thead>"
                        tb_folloup &= "<tr>"
                        tb_folloup &= "<th style='padding-left:10px;padding-right:10px'>Data / Hora</th>"
                        tb_folloup &= "<th style='padding-left:10px;padding-right:10px'>COMENTÁRIO/HISTÓRICO</th>"
                        tb_folloup &= "<th style='padding-left:10px;padding-right:10px'>RESPONSÁVEL / Arquivo</th>"
                        tb_folloup &= "<tr>"
                        tb_folloup &= "</thead>"
                        tb_folloup &= "<tbody>"
                        For Each item As FollowUps In data.follow_ups
                            tb_folloup &= "<tr>"
                            tb_folloup &= "<td style='padding-left:10px;padding-right:10px'>" & item.date_time & "</a></td>"
                            tb_folloup &= "<td style='padding-left:10px;padding-right:10px'>" & item.comment_history & "</td>"
                            tb_folloup &= "<td style='padding-left:10px;padding-right:10px'>" & item.user & "</td>"
                            tb_folloup &= "</tr>"
                        Next
                        tb_folloup &= "</tbody>"
                        tb_folloup &= "</table>"
                        followup.InnerHtml = tb_folloup





                        'ITENS MERCADORIA
                        Dim contador As Integer = 0
                        Dim ds As DataSet = Con.ExecutarQuery("select NR_CNTR from TB_CNTR_BL WHERE ID_CNTR_BL IN (select ID_CNTR_BL from TB_CARGA_BL where ID_BL =" & Request.QueryString("id") & ")")
                        If ds.Tables(0).Rows.Count > 0 Then
                            volume_m3.Text = data.commodity.unit_quantity
                            peso_bruto.Text = data.commodity.gross_weight_kg
                            ' teus.Text = data.commodity.teus
                            cntrs.Text = data.commodity.c40
                            total_cntrs.Text = data.commodity.fcl_total
                            Dim Container As String = ""
                            Dim tabela1 As String = "<table Class='table'>"
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


                            For Each linha As DataRow In ds.Tables(0).Rows

                                If Not IsDBNull(linha.Item("NR_CNTR")) Then

                                    Container = linha.Item("NR_CNTR")
                                    Dim ds2 As DataSet = Con.ExecutarQuery("Select TRAKING_BL FROM [TB_BL] WHERE ID_BL =(Select ID_BL_MASTER FROM [TB_BL] WHERE ID_BL = " & Request.QueryString("id") & " )")

                                    If ds2.Tables(0).Rows.Count > 0 Then
                                        If Not IsDBNull(ds2.Tables(0).Rows(0).Item("TRAKING_BL")) Then


                                            Dim CNTR = DeserializarNewtonsoft(ds2.Tables(0).Rows(0).Item("TRAKING_BL"))


                                            For Each item As Itens In CNTR.fcl()

                                                If item.container_number = Container Then
                                                    contador = contador + 1

                                                    tabela1 &= "<tr>"
                                                    tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.container_number & "</td>"
                                                    tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.seal_number & "</td>"
                                                    tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.feet & "</td>"
                                                    tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.gross_weight_decimal & "</td>"
                                                    tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.tare_container & "</td>"
                                                    tabela1 &= "<td style='padding-left:10px;padding-right:10px'>" & item.volume_m3 & "</td>"
                                                    tabela1 &= "<td style='padding-left:10px;padding-right:10px'></td>"
                                                    tabela1 &= "</tr>"
                                                End If

                                            Next


                                        End If

                                    End If

                                End If

                            Next

                            tabela1 &= "</tbody>"
                            tabela1 &= "</table>"
                            divCNTR.InnerHtml = tabela1
                            total_cntrs.Text = contador
                            cntrs.Text = contador
                        End If


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
                Response.Redirect("RastreioHBL.aspx?id=" & Request.QueryString("id"))
            End If
        End If
    End Sub

    Protected Sub btnAtualizar_Click(sender As Object, e As EventArgs) Handles btnAtualizar.Click
        Atualizar()
    End Sub
End Class


