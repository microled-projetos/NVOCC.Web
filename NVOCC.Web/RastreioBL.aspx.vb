Imports Newtonsoft.Json

Public Class RastreioBL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        nr_bl.Text = Session("NR_BL")
        Dim data = DeserializarNewtonsoft()

        'Transporte e Logistica
        pais_procedencia.Text = data.transport.origin_country
        pais_destino.Text = data.transport.destination_country
        porto_embarque.Text = data.transport.lading_port
        porto_embarque.Text = data.transport.origin_port
        porto_descarga.Text = data.transport.landing_terminal
        porto_destino.Text = data.transport.transport
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
        ce.Text = data.aduana.ce_number
        manifesto.Text = data.aduana.manifest
        'Mercadoria
        volume_m3.Text = data.commodity.unit_quantity
        peso_bruto.Text = data.commodity.gross_weight_kg
        teus.Text = data.commodity.teus
        cntrs.Text = data.commodity.c40
        total_cntrs.Text = data.commodity.fcl_total
        'ITENS MERCADORIA
        m_container.Text = data.fcl(0).container_number
        m_lacre.Text = data.fcl(0).seal_number
        m_pes.Text = data.fcl(0).feet
        m_peso_bruto.Text = data.fcl(0).gross_weight_decimal
        m_tara.Text = data.fcl(0).tare_container
        m_volume_m3.Text = data.fcl(0).volume_m3

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
        'eta

    End Sub
    Private Function DeserializarNewtonsoft() As BL
        Dim Json = Session("TRAKING_BL")
        Return JsonConvert.DeserializeObject(Of BL)(Json)
    End Function

    Protected Sub btnAtualizar_Click(sender As Object, e As EventArgs) Handles btnAtualizar.Click
        Response.Redirect("https://localhost:44327/Rastreio/rastrear/iniciar")
    End Sub
End Class


