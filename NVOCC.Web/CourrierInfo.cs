using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABAINFRA.Web.Classes
{
    public class CourrierInfo
    {
        private string nr_processo;
        private string dt_recebimento_mbl;
        private string dt_recebimento_hbl;
        private string cd_rastreamento_mbl;
        private string cd_rastreamento_hbl;
        private string nr_fatura_courrier;
        private string dt_retirada_courrier;
        private string dt_retirada_personal;
        private string nm_retirado_por_courrier;
        private string nm_razao;
        private string id_bl;
        private string id_bl_master;
        private string nr_bl_master;
        private string nr_bl;

        public string NR_PROCESSO { get => nr_processo; set => nr_processo = value; }
        public string DT_RECEBIMENTO_MBL { get => dt_recebimento_mbl; set => dt_recebimento_mbl = value; }
        public string DT_RECEBIMENTO_HBL { get => dt_recebimento_hbl; set => dt_recebimento_hbl = value; }
        public string CD_RASTREAMENTO_MBL { get => cd_rastreamento_mbl; set => cd_rastreamento_mbl = value; }
        public string CD_RASTREAMENTO_HBL { get => cd_rastreamento_hbl; set => cd_rastreamento_hbl = value; }
        public string NR_FATURA_COURRIER { get => nr_fatura_courrier; set => nr_fatura_courrier = value; }
        public string DT_RETIRADA_COURRIER { get => dt_retirada_courrier; set => dt_retirada_courrier = value; }
        public string DT_RETIRADA_PERSONAL { get => dt_retirada_personal; set => dt_retirada_personal = value; }
        public string NM_RETIRADO_POR_COURRIER { get => nm_retirado_por_courrier; set => nm_retirado_por_courrier = value; }
        public string NM_RAZAO { get => nm_razao; set => nm_razao = value; }
        public string ID_BL { get => id_bl; set => id_bl = value; }
        public string ID_BL_MASTER { get => id_bl_master; set => id_bl_master = value; }
        public string NR_BL_MASTER { get => nr_bl_master; set => nr_bl_master = value; }
        public string NR_BL{ get => nr_bl; set => nr_bl= value; }
    }
}