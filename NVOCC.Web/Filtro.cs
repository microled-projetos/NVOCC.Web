using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABAINFRA.Web.Classes
{
	public class Filtro
	{
        private string idfilter;
        private string filter;
        private string tipo;
        private string blhouse;
        private string dtrecebimentomblinicio;
        private string dtrecebimentomblfim;
        private string cdrastreamentombl;
        private string dtrecebimentohblinicio;
        private string dtrecebimentohblfim;
        private string cdrastreamentohbl;
        private string dtretiradainicio;
        private string dtretiradafim;
        private string retiradopor;
        private string agente;
        private string previsaochegadainicio;
        private string previsaochegadafim;
        private string dtchegadainicio;
        private string dtchegadafim;
        private string fatura;

        public string IDFILTER { get => idfilter; set => idfilter = value; }
        public string FILTER { get => filter; set => filter = value; }
        public string TIPO { get => tipo; set => tipo = value; }
        public string BLHOUSE { get => blhouse; set => blhouse = value; }
        public string DTRECEBIMENTOMBLINICIO { get => dtrecebimentomblinicio; set => dtrecebimentomblinicio = value; }
        public string DTRECEBIMENTOMBLFIM { get => dtrecebimentomblfim; set => dtrecebimentomblfim = value; }
        public string CDRASTREAMENTOMBL { get => cdrastreamentombl; set => cdrastreamentombl = value; }
        public string DTRECEBIMENTOHBLINICIO { get => dtrecebimentohblinicio; set => dtrecebimentohblinicio = value; }
        public string DTRECEBIMENTOHBLFIM { get => dtrecebimentohblfim; set => dtrecebimentohblfim = value; }
        public string CDRASTREAMENTOHBL { get => cdrastreamentohbl; set => cdrastreamentohbl = value; }
        public string DTRETIRADAINICIO { get => dtretiradainicio; set => dtretiradainicio = value; }
        public string DTRETIRADAFIM { get => dtretiradafim; set => dtretiradafim = value; }
        public string RETIRADOPOR { get => retiradopor; set => retiradopor = value; }
        public string AGENTE { get => agente; set => agente = value; }
        public string PREVISAOCHEGADAINICIO { get => previsaochegadainicio; set => previsaochegadainicio = value; }
        public string PREVISAOCHEGADAFIM { get => previsaochegadafim; set => previsaochegadafim = value; }
        public string DTCHEGADAINICIO { get => dtchegadainicio; set => dtchegadainicio = value; }
        public string DTCHEGADAFIM { get => dtchegadafim; set => dtchegadafim = value; }
        public string FATURA { get => fatura; set => fatura = value; }
    }
}