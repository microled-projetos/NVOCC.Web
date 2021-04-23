using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;

namespace ABAINFRA.Web.Classes
{
    public class Processos
    {
        private int id_bl;
        private int id_status_bl;
        private string dt_flwp_lcl;
        private string nr_processo;
        private int id_parceiro_vendedor;
        private int id_parceiro_agente;
        private int id_parceiro_cliente;
        private int id_parceiro_exportador;
        private string vl_peso_bruto;
        private string vl_M3;
        private int qt_mercadoria;
        private string vl_peso_bruto_agente;
        private string vl_m3_agente;
        private int qt_mercadoria_agente;
        private string nm_mercadoria;
        private int id_mercadoria;
        private int id_incoterm;
        private string cd_incoterm;
        private string dt_ready_date;
        private string dt_forecast_wh;
        private string dt_arrive_wh;
        private string dt_draft_cutoff;
        private string dt_cutoff;
        private string nr_bl;
        private int id_week_container;

        public int ID_BL { get => id_bl; set => id_bl = value; }
        public int ID_STATUS_BL { get => id_status_bl; set => id_status_bl = value; }
        public string DT_FLWP_LCL { get => dt_flwp_lcl; set => dt_flwp_lcl = value; }
        public string NR_PROCESSO { get => nr_processo; set => nr_processo = value; }
        public int ID_PARCEIRO_VENDEDOR { get => id_parceiro_vendedor; set => id_parceiro_vendedor = value; }
        public int ID_PARCEIRO_AGENTE { get => id_parceiro_agente; set => id_parceiro_agente = value; }
        public int ID_PARCEIRO_CLIENTE { get => id_parceiro_cliente; set => id_parceiro_cliente = value; }
        public int ID_PARCEIRO_EXPORTADOR { get => id_parceiro_exportador; set => id_parceiro_exportador = value; }
        public string VL_PESO_BRUTO { get => vl_peso_bruto; set => vl_peso_bruto = value; }
        public string VL_M3 { get => vl_M3; set => vl_M3 = value; }
        public int QT_MERCADORIA { get => qt_mercadoria; set => qt_mercadoria = value; }
        public string VL_PESO_BRUTO_AGENTE { get => vl_peso_bruto_agente; set => vl_peso_bruto_agente = value; }
        public string VL_M3_AGENTE { get => vl_m3_agente; set => vl_m3_agente = value; }
        public int QT_MERCADORIA_AGENTE { get => qt_mercadoria_agente; set => qt_mercadoria_agente = value; }
        public string NM_MERCADORIA { get => nm_mercadoria; set => nm_mercadoria = value; }
        public int ID_MERCADORIA { get => id_mercadoria; set => id_mercadoria = value; }
        public int ID_INCOTERM { get => id_incoterm; set => id_incoterm = value; }
        public string CD_INCOTERM { get => cd_incoterm; set => cd_incoterm = value; }
        public string DT_READY_DATE { get => dt_ready_date; set => dt_ready_date = value; }
        public string DT_FORECAST_WH { get => dt_forecast_wh; set => dt_forecast_wh = value; }
        public string DT_ARRIVE_WH { get => dt_arrive_wh; set => dt_arrive_wh = value; }
        public string DT_DRAFT_CUTOFF { get => dt_draft_cutoff; set => dt_draft_cutoff = value; }
        public string DT_CUTOFF { get => dt_cutoff; set => dt_cutoff = value; }
        public string NR_BL { get => nr_bl; set => nr_bl = value; }
        public int ID_WEEK_CONTAINER { get => id_week_container; set => id_week_container = value; }
    }
}