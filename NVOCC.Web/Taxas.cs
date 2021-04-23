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
    public class Taxas
    {
        private int id_taxa_cliente;
        private int id_item_despesa;
        private int id_base_calculo_taxa;
        private string vl_taxa_compra;
        private string id_moeda_compra;
        private decimal vl_taxa_venda;
        private string id_moeda_venda;
        private int fl_declarado;
        private int fl_divisao_profit;
        private int id_destinatario_cobranca;
        private string ob_taxas;
        private int id_parceiro;
        private string nm_item_despesa;
        private string nm_base_calculo_taxa;
        private string nm_moeda;

        public int ID_TAXA_CLIENTE { get => id_taxa_cliente; set => id_taxa_cliente = value; }
        public int ID_ITEM_DESPESA { get => id_item_despesa; set => id_item_despesa = value; }
        public int ID_BASE_CALCULO_TAXA { get => id_base_calculo_taxa; set => id_base_calculo_taxa = value; }
        public string VL_TAXA_COMPRA { get => vl_taxa_compra; set => vl_taxa_compra = value; }
        public string ID_MOEDA_COMPRA { get => id_moeda_compra; set => id_moeda_compra = value; }
        public decimal VL_TAXA_VENDA { get => vl_taxa_venda; set => vl_taxa_venda = value; }
        public string ID_MOEDA_VENDA { get => id_moeda_venda; set => id_moeda_venda = value; }
        public int FL_DECLARADO { get => fl_declarado; set => fl_declarado = value; }
        public int FL_DIVISAO_PROFIT { get => fl_divisao_profit; set => fl_divisao_profit = value; }
        public int ID_DESTINATARIO_COBRANCA { get => id_destinatario_cobranca; set => id_destinatario_cobranca = value; }
        public string OB_TAXAS { get => ob_taxas; set => ob_taxas = value; }
        public int ID_PARCEIRO { get => id_parceiro; set => id_parceiro = value; }
        public string NM_ITEM_DESPESA { get => nm_item_despesa; set => nm_item_despesa = value; }
        public string NM_BASE_CALCULO_TAXA { get => nm_base_calculo_taxa; set => nm_base_calculo_taxa = value; }
        public string NM_MOEDA { get => nm_moeda; set => nm_moeda = value; }






    }
}