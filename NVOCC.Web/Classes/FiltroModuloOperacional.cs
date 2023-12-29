using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABAINFRA.Web.Classes
{
    public class FiltroModuloOperacional
    {
        private string via;
        private string etapa;
        private string servico;
        private string status;
        private string processo;
        private string cliente;
        private string origem;
        private string destino;
        private string frete;
        private string estufagem;
        private string agente;
        private string importador;
        private string clientefinal;
        private string pembarqueinicio;
        private string pembarquefim;
        private string dtembarqueinicio;
        private string dtembarquefim;
        private string pchegadainicio;
        private string pchegadafim;
        private string dtchegadainicio;
        private string dtchegadafim;
        private string freetime;
        private string transportador;
        private string blmaster;
        private string blhouse;
        private string cemaster;
        private string cehouse;
        private string dtredestinacaoinicio;
        private string dtredestinacaofim;
        private string dtdesconsolidacaoinicio;
        private string dtdesconsolidacaofim;
        private string week;
        private string navio;
        private string naviotransbordo;
        private string agenteinternacional;

        public string VIA { get => via; set => via = value; }
        public string ETAPA { get => etapa; set => etapa = value; }
        public string SERVICO { get => servico; set => servico = value; }
        public string STATUS { get => status; set => status = value; }
        public string PROCESSO { get => processo; set => processo = value; }
        public string CLIENTE { get => cliente; set => cliente = value; }
        public string ORIGEM { get => origem; set => origem = value; }
        public string DESTINO { get => destino; set => destino = value; }
        public string FRETE { get => frete; set => frete = value; }
        public string ESTUFAGEM { get => estufagem; set => estufagem = value; }
        public string AGENTE { get => agente; set => agente = value; }
        public string IMPORTADOR { get => importador; set => importador = value; }
        public string CLIENTEFINAL { get => clientefinal; set => clientefinal = value; }
        public string PEMBARQUEINICIO { get => pembarqueinicio; set => pembarqueinicio = value; }
        public string PEMBARQUEFIM { get => pembarquefim; set => pembarquefim = value; }
        public string DTEMBARQUEINICIO { get => dtembarqueinicio; set => dtembarqueinicio = value; }
        public string DTEMBARQUEFIM { get => dtembarquefim; set => dtembarquefim = value; }
        public string PCHEGADAINICIO { get => pchegadainicio; set => pchegadainicio = value; }
        public string PCHEGADAFIM { get => pchegadafim; set => pchegadafim = value; }
        public string DTCHEGADAINICIO { get => dtchegadainicio; set => dtchegadainicio = value; }
        public string DTCHEGADAFIM { get => dtchegadafim; set => dtchegadafim = value; }
        public string FREETIME { get => freetime; set => freetime = value; }
        public string TRANSPORTADOR { get => transportador; set => transportador = value; }
        public string BLMASTER { get => blmaster; set => blmaster = value; }
        public string BLHOUSE { get => blhouse; set => blhouse = value; }
        public string CEMASTER { get => cemaster; set => cemaster = value; }
        public string CEHOUSE { get => cehouse; set => cehouse = value; }
        public string DTREDESTINACAOINICIO { get => dtredestinacaoinicio; set => dtredestinacaoinicio = value; }
        public string DTREDESTINACAOFIM { get => dtredestinacaofim; set => dtredestinacaofim = value; }
        public string DTDESCONSOLIDACAOINICIO { get => dtdesconsolidacaoinicio; set => dtdesconsolidacaoinicio = value; }
        public string DTDESCONSOLIDACAOFIM { get => dtdesconsolidacaofim; set => dtdesconsolidacaofim = value; }
        public string WEEK { get => week; set => week = value; }
        public string NAVIO { get => navio; set => navio = value; }
        public string NAVIOTRANSBORDO { get => naviotransbordo; set => naviotransbordo = value; }
        public string AGENTEINTERNACIONAL { get => agenteinternacional; set => agenteinternacional = value; }
    }
}