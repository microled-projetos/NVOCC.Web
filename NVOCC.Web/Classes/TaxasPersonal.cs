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
    public class TaxasPersonal
    {
        private string processo;
        private string item;
        private string master;
        private string house;
        private string chegada;
        private string valor;
        private string competencia;
        private string documento;
        private string tipo;

        public string PROCESSO { get => processo; set => processo = value; }
        public string ITEM { get => item; set => item = value; }
        public string MASTER { get => master; set => master = value; }
        public string HOUSE { get => house; set => house = value; }
        public string CHEGADA { get => chegada; set => chegada = value; }
        public string VALOR { get => valor; set => valor = value; }
        public string COMPETENCIA { get => competencia; set => competencia = value; }
        public string DOCUMENTO { get => documento; set => documento = value; }
        public string TIPO { get => tipo; set => tipo = value; }
    }
}