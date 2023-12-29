using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABAINFRA.Web.Classes
{
	public class Variacao
	{
		public string idBaseCalculoVariacao { get; set; }
		public string idVariacao { get; set; }
		public string idTaxaCliente { get; set; }
		public string qtInicial { get; set; }
		public string qtFinal { get; set; }
		public string moedaC { get; set; }
		public string valorC { get; set; }
	}
}