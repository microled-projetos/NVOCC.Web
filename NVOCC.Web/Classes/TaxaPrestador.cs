using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;

namespace ABAINFRA.Web
{
    public class TaxasPrestador
    {
        private string id_taxa_cliente;
        private string id_item_despesa;
        private string id_base_calculo_taxa;
        private string vl_tarifa_minima;
        private string vl_tarifa_minima_compra;
        private string vl_taxa_compra;
        private string id_moeda_compra;
        private string vl_taxa_venda;
        private string id_moeda_venda;
        private string fl_declarado;
        private string fl_divisao_profit;
        private string fl_taxa_transportador;
        private string id_destinatario_cobranca;
        private string id_tipo_pagamento;
        private string id_origem_pagamento;
        private string ob_taxas;
        private string id_parceiro;
        private string nm_item_despesa;
        private string nm_base_calculo_taxa;
        private string nm_moeda;
        private string id_porto_recebimento;
        private string id_porto_carregamento;
        private string id_porto_descarga;
        private string id_aeroporto_carregamento;
        private string id_aeroporto_descarga;
        private string id_incoterm;
        private string id_tipo_cobranca;
        private string ds_observacao_geral;
        private string id_viatransporte;
        private string id_tipo_comex;
        private string id_tipo_estufagem;

        public string ID_TAXA_CLIENTE { get => id_taxa_cliente; set => id_taxa_cliente = value; }
        public string ID_ITEM_DESPESA { get => id_item_despesa; set => id_item_despesa = value; }
        public string ID_BASE_CALCULO_TAXA { get => id_base_calculo_taxa; set => id_base_calculo_taxa = value; }
        public string VL_TARIFA_MINIMA { get => vl_tarifa_minima; set => vl_tarifa_minima = value; }
        public string VL_TARIFA_MINIMA_COMPRA { get => vl_tarifa_minima_compra; set => vl_tarifa_minima_compra = value; }
        public string VL_TAXA_COMPRA { get => vl_taxa_compra; set => vl_taxa_compra = value; }
        public string ID_MOEDA_COMPRA { get => id_moeda_compra; set => id_moeda_compra = value; }
        public string VL_TAXA_VENDA { get => vl_taxa_venda; set => vl_taxa_venda = value; }
        public string ID_MOEDA_VENDA { get => id_moeda_venda; set => id_moeda_venda = value; }
        public string FL_DECLARADO { get => fl_declarado; set => fl_declarado = value; }
        public string FL_DIVISAO_PROFIT { get => fl_divisao_profit; set => fl_divisao_profit = value; }
        public string FL_TAXA_TRANSPORTADOR { get => fl_taxa_transportador; set => fl_taxa_transportador = value; }
        public string ID_DESTINATARIO_COBRANCA { get => id_destinatario_cobranca; set => id_destinatario_cobranca = value; }
        public string ID_TIPO_PAGAMENTO { get => id_tipo_pagamento; set => id_tipo_pagamento = value; }
        public string ID_ORIGEM_PAGAMENTO { get => id_origem_pagamento; set => id_origem_pagamento = value; }
        public string OB_TAXAS { get => ob_taxas; set => ob_taxas = value; }
        public string ID_PARCEIRO { get => id_parceiro; set => id_parceiro = value; }
        public string NM_ITEM_DESPESA { get => nm_item_despesa; set => nm_item_despesa = value; }
        public string NM_BASE_CALCULO_TAXA { get => nm_base_calculo_taxa; set => nm_base_calculo_taxa = value; }
        public string NM_MOEDA { get => nm_moeda; set => nm_moeda = value; }
        public string ID_PORTO_RECEBIMENTO { get => id_porto_recebimento; set => id_porto_recebimento = value; }
        public string ID_PORTO_CARREGAMENTO { get => id_porto_carregamento; set => id_porto_carregamento = value; }
        public string ID_PORTO_DESCARGA { get => id_porto_descarga; set => id_porto_descarga = value; }
        public string ID_AEROPORTO_CARREGAMENTO { get => id_aeroporto_carregamento; set => id_aeroporto_carregamento = value; }
        public string ID_AEROPORTO_DESCARGA { get => id_aeroporto_descarga; set => id_aeroporto_descarga = value; }
        public string ID_INCOTERM { get => id_incoterm; set => id_incoterm = value; }
        public string ID_TIPO_COBRANCA { get => id_tipo_cobranca; set => id_tipo_cobranca = value; }
        public string DS_OBSERVACAO_GERAL { get => ds_observacao_geral; set => ds_observacao_geral = value; }
        public string ID_TIPO_ESTUFAGEM { get => id_tipo_estufagem; set => id_tipo_estufagem = value; }
        public string ID_TIPO_COMEX { get => id_tipo_comex; set => id_tipo_comex = value; }
        public string ID_VIATRANSPORTE { get => id_viatransporte; set => id_viatransporte = value; }
    }
}