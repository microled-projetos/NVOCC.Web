using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABAINFRA.Web.Classes
{
	public class FiltrosTaxasInativas
	{
        private string processo;
        private string fornecedor;
        private string estufagem;
        private string modal;
        private string servico;
        private string agenteinter;
        private string cliente;
        private string itemdespesa;
        private string moeda;
        private string basecalculo;
        private string usuario;
        private string datainicial;
        private string datafinal;
        private string tpmovimento;

        public string PROCESSO { get => processo; set => processo = value; }
        public string FORNECEDOR { get => fornecedor; set => fornecedor = value; }
        public string ESTUFAGEM { get => estufagem; set => estufagem= value; }
        public string MODAL { get => modal; set => modal = value; }
        public string SERVICO { get => servico; set => servico= value; }
        public string AGENTEINTER { get => agenteinter; set => agenteinter= value; }
        public string CLIENTE { get => cliente; set => cliente= value; }
        public string ITEMDESPESA { get => itemdespesa; set => itemdespesa = value; }
        public string MOEDA { get => moeda; set => moeda = value; }
        public string BASECALCULO { get => basecalculo; set => basecalculo= value; }
        public string USUARIO { get => usuario; set => usuario = value; }
        public string DATAINICIAL { get => datainicial; set => datainicial = value; }
        public string DATAFINAL { get => datafinal; set => datafinal = value; }
        public string TPMOVIMENTO { get => tpmovimento; set => tpmovimento = value; }
    }
}