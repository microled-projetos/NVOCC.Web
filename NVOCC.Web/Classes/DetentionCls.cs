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
    public class DetentionCls
    {
        private int id_tabela_detention;
        private int id_parceiro_transportador;
        private string id_tipo_container;
        private string dt_validade_final;
        private string dt_validade_inicial;
        private string qt_dias_freetime;
        private int id_moeda;
        private string fl_escalonada;
        private string fl_inicio_chegada;
        private string qt_dias_01;
        private string vl_venda_01;
        private string qt_dias_02;
        private string vl_venda_02;
        private string qt_dias_03;
        private string vl_venda_03;
        private string qt_dias_04;
        private string vl_venda_04;
        private string qt_dias_05;
        private string vl_venda_05;
        private string qt_dias_06;
        private string vl_venda_06;
        private string qt_dias_07;
        private string vl_venda_07;
        private string qt_dias_08;
        private string vl_venda_08;
        private string fl_carga_imo;
        private string id_tamanho_container;

        public int ID_TABELA_DETENTION { get => id_tabela_detention; set => id_tabela_detention = value; }
        public int ID_PARCEIRO_TRANSPORTADOR { get => id_parceiro_transportador; set => id_parceiro_transportador = value; }
        public string ID_TIPO_CONTAINER { get => id_tipo_container; set => id_tipo_container = value; }
        public string DT_VALIDADE_FINAL { get => dt_validade_final; set => dt_validade_final = value; }
        public string DT_VALIDADE_INICIAL { get => dt_validade_inicial; set => dt_validade_inicial = value; }
        public string QT_DIAS_FREETIME { get => qt_dias_freetime; set => qt_dias_freetime = value; }
        public int ID_MOEDA { get => id_moeda; set => id_moeda = value; }
        public string FL_ESCALONADA { get => fl_escalonada; set => fl_escalonada = value; }
        public string FL_INICIO_CHEGADA { get => fl_inicio_chegada; set => fl_inicio_chegada = value; }
        public string QT_DIAS_01 { get => qt_dias_01; set => qt_dias_01 = value; }
        public string VL_VENDA_01 { get => vl_venda_01; set => vl_venda_01 = value; }
        public string QT_DIAS_02 { get => qt_dias_02; set => qt_dias_02 = value; }
        public string VL_VENDA_02 { get => vl_venda_02; set => vl_venda_02 = value; }
        public string QT_DIAS_03 { get => qt_dias_03; set => qt_dias_03 = value; }
        public string VL_VENDA_03 { get => vl_venda_03; set => vl_venda_03 = value; }
        public string QT_DIAS_04 { get => qt_dias_04; set => qt_dias_04 = value; }
        public string VL_VENDA_04 { get => vl_venda_04; set => vl_venda_04 = value; }
        public string QT_DIAS_05 { get => qt_dias_05; set => qt_dias_05 = value; }
        public string VL_VENDA_05 { get => vl_venda_05; set => vl_venda_05 = value; }
        public string QT_DIAS_06 { get => qt_dias_06; set => qt_dias_06 = value; }
        public string VL_VENDA_06 { get => vl_venda_06; set => vl_venda_06 = value; }
        public string QT_DIAS_07 { get => qt_dias_07; set => qt_dias_07 = value; }
        public string VL_VENDA_07 { get => vl_venda_07; set => vl_venda_07 = value; }
        public string QT_DIAS_08 { get => qt_dias_08; set => qt_dias_08 = value; }
        public string VL_VENDA_08 { get => vl_venda_08; set => vl_venda_08 = value; }
        public string FL_CARGA_IMO { get => fl_carga_imo; set => fl_carga_imo = value; }
        public string ID_TAMANHO_CONTAINER { get => id_tamanho_container; set => id_tamanho_container = value; }
    }
}