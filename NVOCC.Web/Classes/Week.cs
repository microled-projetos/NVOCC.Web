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
    public class Week
    {
        private int id_week;
        private int id_week_container;
        private string nm_week;
        private int id_porto_origem_local;
        private int id_porto_origem_destino;
        private string nm_porto_origem_local;
        private string nm_porto_origem_destino;
        private string nm_mbl;
        private int id_parceiro;
        private string nm_vessel;
        private string dt_cutoff;
        private string dt_etd;
        private string dt_eta;
        private int id_tipo_container;
        private string nr_container;
        private string vl_peso_max;
        private string vl_cubagem;
        private string nr_fright;
        private int nr_freetime;

        public int ID_WEEK { get => id_week; set => id_week = value; }
        public int ID_WEEK_CONTAINER { get => id_week_container; set => id_week_container = value; }
        public string NM_WEEK { get => nm_week; set => nm_week = value; }
        public int ID_PORTO_ORIGEM_LOCAL { get => id_porto_origem_local; set => id_porto_origem_local = value; }
        public int ID_PORTO_ORIGEM_DESTINO { get => id_porto_origem_destino; set => id_porto_origem_destino = value; }
        public string NM_PORTO_ORIGEM_LOCAL { get => nm_porto_origem_local; set => nm_porto_origem_destino = value; }
        public string NM_PORTO_ORIGEM_DESTINO { get => nm_porto_origem_destino; set => nm_porto_origem_local = value; }
        public string NM_MBL { get => nm_mbl; set => nm_mbl = value; }
        public int ID_PARCEIRO { get => id_parceiro; set => id_parceiro = value; }
        public string NM_VESSEL { get => nm_vessel; set => nm_vessel = value; }
        public string DT_CUTOFF { get => dt_cutoff; set => dt_cutoff = value; }
        public string DT_ETD { get => dt_etd; set => dt_etd = value; }
        public string DT_ETA { get => dt_eta; set => dt_eta = value; }
        public int ID_TIPO_CONTAINER { get => id_tipo_container; set => id_tipo_container = value; }
        public string NR_CONTAINER { get => nr_container; set => nr_container = value; }
        public string VL_PESO_MAX { get => vl_peso_max; set => vl_peso_max = value; }
        public string VL_CUBAGEM { get => vl_cubagem; set => vl_cubagem = value; }
        public string NR_FRIGHT { get => nr_fright; set => nr_fright = value; }
        public int NR_FREETIME { get => nr_freetime; set => nr_freetime = value; }
    }
}