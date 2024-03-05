using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABAINFRA.Web.Classes
{
    public class ResultadoPedido
    {
        public List<PedidoCompra> PEDIDOS { get; set; }
    }
    public class PedidoCompra
    {
        private string c7_filial;
        private int c7_tipo;
        private string c7_num;
        private string c7_item;
        private string c7_produto;
        private string c7_um;
        private int c7_quant;
        private double c7_preco;
        private double c7_total;
        private string c7_local;
        private string c7_obsm;
        private string c7_fornece;
        private string c7_loja;
        private string c7_conta;
        private string c7_itemcta;
        private string c7_cc;
        private string c7_cond;
        private string c7_contato;
        private string c7_filent;
        private string c7_emissao;
        private string c7_descri;
        private string c7_msg;
        private string c7_control;
        private string c7_dtimp;
        private double c7_alqcf2;
        private double c7_alqcof;
        private double c7_alqpis;
        private double c7_alqps2;
        private double c7_bascof;
        private double c7_baspis;
        private double c7_valcof;
        private double c7_valpis;
        private double c7_fiscori;
        private string c7_xcodpro;
        private string c7_xdespro;
        private string c7_xpedleg;
        private string c7_xcodfor;
        private string c7_xlojfor;
        private string c7_xnfiscal;
        private string c7_xserie;

        public string C7_FILIAL { get => c7_filial; set => c7_filial = value; }
        public int C7_TIPO { get => c7_tipo; set => c7_tipo = value; }
        public string C7_NUM { get => c7_num; set => c7_num = value; }
        public string C7_ITEM { get => c7_item; set => c7_item = value; }
        public string C7_PRODUTO { get => c7_produto; set => c7_produto = value; }
        public string C7_UM { get => c7_um; set => c7_um = value; }
        public int C7_QUANT { get => c7_quant; set => c7_quant = value; }
        public double C7_PRECO { get => c7_preco; set => c7_preco = value; }
        public double C7_TOTAL { get => c7_total; set => c7_total = value; }
        public string C7_LOCAL { get => c7_local; set => c7_local = value; }
        public string C7_OBSM { get => c7_obsm; set => c7_obsm = value; }
        public string C7_FORNECE { get => c7_fornece; set => c7_fornece = value; }
        public string C7_LOJA { get => c7_loja; set => c7_loja = value; }
        public string C7_CONTA { get => c7_conta; set => c7_conta = value; }
        public string C7_ITEMCTA { get => c7_itemcta; set => c7_itemcta = value; }
        public string C7_CC { get => c7_cc; set => c7_cc = value; }
        public string C7_COND { get => c7_cond; set => c7_cond = value; }
        public string C7_CONTATO { get => c7_contato; set => c7_contato = value; }
        public string C7_FILENT { get => c7_filent; set => c7_filent = value; }
        public string C7_EMISSAO { get => c7_emissao; set => c7_emissao = value; }
        public string C7_DESCRI { get => c7_descri; set => c7_descri = value; }
        public string C7_MSG { get => c7_msg; set => c7_msg = value; }
        public string C7_CONTROL { get => c7_control; set => c7_control = value; }
        public string C7_DTIMP { get => c7_dtimp; set => c7_dtimp = value; }
        public double C7_ALQCF2 { get => c7_alqcf2; set => c7_alqcf2 = value; }
        public double C7_ALQCOF { get => c7_alqcof; set => c7_alqcof = value; }
        public double C7_ALQPIS { get => c7_alqpis; set => c7_alqpis = value; }
        public double C7_ALQPS2 { get => c7_alqps2; set => c7_alqps2 = value; }
        public double C7_BASCOF { get => c7_bascof; set => c7_bascof = value; }
        public double C7_BASPIS { get => c7_baspis; set => c7_baspis = value; }
        public double C7_VALCOF { get => c7_valcof; set => c7_valcof = value; }
        public double C7_VALPIS { get => c7_valpis; set => c7_valpis = value; }
        public double C7_FISCORI { get => c7_fiscori; set => c7_fiscori = value; }
        public string C7_XCODPRO { get => c7_xcodpro; set => c7_xcodpro = value; }
        public string C7_XDESPRO { get => c7_xdespro; set => c7_xdespro = value; }
        public string C7_XPEDLEG { get => c7_xpedleg; set => c7_xpedleg = value; }
        public string C7_XCODFOR { get => c7_xcodfor; set => c7_xcodfor = value; }
        public string C7_XLOJFOR { get => c7_xlojfor; set => c7_xlojfor = value; }
        public string C7_XNFISCAL { get => c7_xnfiscal; set => c7_xnfiscal = value; }
        public string C7_XSERIE { get => c7_xserie; set => c7_xserie = value; }

    }
}