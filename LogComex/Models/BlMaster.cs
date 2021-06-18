using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LogComex.Models
{
    public class BlMaster
    {
        public string NR_BL { get; set; }
        public int ID_ARMADOR_LOGCOMEX { get; set; }
        public string BL_TOKEN { get; set; }
    }
}