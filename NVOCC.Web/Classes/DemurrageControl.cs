using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABAINFRA.Web.Classes
{
    public class DemurrageControl
    {
        private string nr_processo;
        private string nr_cntr;
        private string ds_status;
        private string dt_devolucao_cntr;
        private string qt_dias_demurrage;
        private string vl_fatura;
        private string dt_vencimento_fatura;
        private string dt_pagamento;
        private string dt_report_date;
        
        public string NR_PROCESSO { get => nr_processo; set => nr_processo = value; }
        public string NR_CNTR { get => nr_cntr; set => nr_cntr = value; }
        public string DS_STATUS { get => ds_status; set => ds_status = value; }
        public string DT_DEVOLUCAO_CNTR { get => dt_devolucao_cntr; set => dt_devolucao_cntr = value; }
        public string QT_DIAS_DEMURRAGE { get => qt_dias_demurrage; set => qt_dias_demurrage = value; }
        public string VL_FATURA { get => vl_fatura; set => vl_fatura = value; }
        public string DT_VENCIMENTO_FATURA { get => dt_vencimento_fatura; set => dt_vencimento_fatura = value; }
        public string DT_PAGAMENTO { get => dt_pagamento; set => dt_pagamento = value; }
        public string DT_REPORT_DATE { get => dt_report_date; set => dt_report_date = value; }
        

    }
}