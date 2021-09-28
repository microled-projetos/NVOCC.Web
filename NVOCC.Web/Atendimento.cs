using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABAINFRA.Web.Classes
{
	public class Atendimento
	{
        private string dt_atendimento_negado;
        private string id_parceiro_inside;
        private string id_vendedor;
        private string id_parceiro_cliente;
        private string id_servico;
        private string id_tipo_estufagem;
        private string id_porto_origem;
        private string id_porto_destino;
        private string id_incoterm;
        private string id_status;
        private string ds_obs;


        public string DT_ATENDIMENTO_NEGADO { get => dt_atendimento_negado; set => dt_atendimento_negado = value; }
        public string ID_PARCEIRO_INSIDE { get => id_parceiro_inside; set => id_parceiro_inside = value; }
        public string ID_VENDEDOR { get => id_vendedor; set => id_vendedor = value; }
        public string ID_PARCEIRO_CLIENTE { get => id_parceiro_cliente; set => id_parceiro_cliente = value; }
        public string ID_SERVICO { get => id_servico; set => id_servico = value; }
        public string ID_TIPO_ESTUFAGEM { get => id_tipo_estufagem; set => id_tipo_estufagem = value; }
        public string ID_PORTO_ORIGEM { get => id_porto_origem; set => id_porto_origem = value; }
        public string ID_PORTO_DESTINO { get => id_porto_destino; set => id_porto_destino = value; }
        public string ID_INCOTERM { get => id_incoterm; set => id_incoterm = value; }
        public string ID_STATUS { get => id_status; set => id_status = value; }
        public string DS_OBS { get => ds_obs; set => ds_obs = value; }
    }
}