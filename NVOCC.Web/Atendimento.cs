﻿using System;
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
        private string id_tipo_carga;
        private string qt_carga;
        private string id_mercadoria;
        private string qt_peso;
        private string qt_metragem;
        private string ds_obs;
        private string id_tipo_container;

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
        public string ID_TIPO_CARGA { get => id_tipo_carga; set => id_tipo_carga = value; }
        public string QT_CARGA { get => qt_carga; set => qt_carga = value; }
        public string ID_MERCADORIA { get => id_mercadoria; set => id_mercadoria = value; }
        public string QT_PESO { get => qt_peso; set => qt_peso = value; }
        public string QT_METRAGEM { get => qt_metragem; set => qt_metragem = value; }
        public string DS_OBS { get => ds_obs; set => ds_obs = value; }
        public string ID_TIPO_CONTAINER { get => id_tipo_container; set => id_tipo_container = value; }
    }
}