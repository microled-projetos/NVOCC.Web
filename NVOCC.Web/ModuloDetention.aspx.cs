using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABAINFRA.Web
{
	public partial class ModuloDetention : System.Web.UI.Page
	{
        string SQL;
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                CarregarListaFiltros();
                CarregarParceiroTransportador();
                CarregarTipoContainer();
                CarregarMoeda();
                CarregarStatus();
                CarregarContaBancaria();
                CarregarFiltroFatura();
                CarregarTamanhoContainer();
                /*                CarregarArmador();
                */
            }
        }

        private void CarregarListaFiltros()
        {
            ddlFiltro.DataBind();
            ddlFiltro.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlFiltro.Items.Insert(1, new ListItem("Nº Processo", "1"));
            ddlFiltro.Items.Insert(2, new ListItem("Nº Container", "2"));
            ddlFiltro.Items.Insert(3, new ListItem("Cliente", "3"));
            ddlFiltro.Items.Insert(4, new ListItem("Transportador", "4"));
            ddlFiltro.Items.Insert(5, new ListItem("MBL", "5"));
            ddlFiltro.Items.Insert(6, new ListItem("HBL", "6"));
        }


        protected void CarregarParceiroTransportador()
        {
            SQL = "SELECT * FROM tb_parceiro where FL_TRANSPORTADOR = 1";
            DataTable parceiroTransportador = new DataTable();
            parceiroTransportador = DBS.List(SQL);
            Session["TaskTableParceiroTransportador"] = parceiroTransportador;
            ddlParceiroTransportador.DataSource = Session["TaskTableParceiroTransportador"];
            ddlParceiroTransportador.DataBind();
            ddlParceiroTransportador.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTransportador.DataSource = Session["TaskTableParceiroTransportador"];
            ddlTransportador.DataBind();
        }

        protected void CarregarTipoContainer()
        {
            SQL = "SELECT * FROM tb_tipo_container ORDER BY SUBSTRING(NM_TIPO_CONTAINER,0,2)";
            DataTable tipoContainerDemurrage = new DataTable();
            tipoContainerDemurrage = DBS.List(SQL);
            Session["TaskTableTipoContainerDemurrage"] = tipoContainerDemurrage;
            ddlTipoContainer.DataSource = Session["TaskTableTipoContainerDemurrage"];
            ddlTipoContainer.DataBind();
            ddlTipoContainer.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void CarregarTamanhoContainer()
        {
            SQL = "SELECT * FROM TB_TAMANHO_CONTAINER ";
            DataTable tamanhoContainerDemurrage = new DataTable();
            tamanhoContainerDemurrage = DBS.List(SQL);
            Session["TaskTableTamanhoContainerDemurrage"] = tamanhoContainerDemurrage;
            ddlTamanhoContainer.DataSource = Session["TaskTableTamanhoContainerDemurrage"];
            ddlTamanhoContainer.DataBind();
            ddlTamanhoContainer.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /*protected void CarregarArmador()
        {
            SQL = "SELECT ID_PARCEIRO, NM_RAZAO FROM tb_parceiro where FL_TRANSPORTADOR = 1";
            DataTable parceiroTransportador = new DataTable();
            parceiroTransportador = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = parceiroTransportador;
            ddlfiltroTabelaDemu.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlfiltroTabelaDemu.DataBind();
            ddlfiltroTabelaDemu.Items.Insert(0, new ListItem("Selecione", ""));
        }*/

        protected void CarregarMoeda()
        {
            SQL = "SELECT * FROM TB_MOEDA";
            DataTable moedaDemurrage = new DataTable();
            moedaDemurrage = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = moedaDemurrage;
            ddlMoeda.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlMoeda.DataBind();
            ddlMoeda.Items.Insert(0, new ListItem("Selecione", ""));
        }
        protected void CarregarContaBancaria()
        {
            SQL = "SELECT * FROM TB_CONTA_BANCARIA ";
            DataTable contaBancaria = new DataTable();
            contaBancaria = DBS.List(SQL);
            Session["TaskTablecontaBancaria"] = contaBancaria;
            ddlContaBancaria.DataSource = Session["TaskTablecontaBancaria"];
            ddlContaBancaria.DataBind();
            ddlContaBancaria.Items.Insert(0, new ListItem("Selecione", ""));
        }
        protected void CarregarStatus()
        {
            DataTable statusDemurrage = new DataTable();

            //SELECT DE DROP DE EDIÇÃO DE CNTR
            SQL = "SELECT * FROM TB_STATUS_DETENTION ";
            statusDemurrage = DBS.List(SQL);
            Session["statusDemurrage"] = statusDemurrage;
            dsStatus.DataSource = Session["statusDemurrage"];
            dsStatus.DataBind();
            dsStatus.Items.Insert(0, new ListItem("Selecione", ""));
            dsStatusCompra.DataSource = Session["statusDemurrage"];
            dsStatusCompra.DataBind();
            dsStatusCompra.Items.Insert(0, new ListItem("Selecione", ""));
            ddlStatusDevolucao.DataSource = Session["statusDemurrage"];
            ddlStatusDevolucao.DataBind();
            ddlStatusDevolucao.Items.Insert(0, new ListItem("Selecione", ""));
            ddlStatusCalculoSelecionado.DataSource = Session["statusDemurrage"];
            ddlStatusCalculoSelecionado.DataBind();
            ddlStatusCalculoSelecionado.Items.Insert(0, new ListItem("Selecione", ""));
            ddlStatusCalculoSelecionadoCompra.DataSource = Session["statusDemurrage"];
            ddlStatusCalculoSelecionadoCompra.DataBind();
            ddlStatusCalculoSelecionadoCompra.Items.Insert(0, new ListItem("Selecione", ""));
            //SELECT DE DROP DE DEVOLUCAO


            //SELECT DE DROP DE CALCULO VENDA
            /*SQL = "SELECT * FROM TB_STATUS_DEMURRAGE WHERE FL_VENDA = 1 AND FL_ATIVO = 1 ";
            statusDemurrage = DBS.List(SQL);
            Session["statusDemurrage"] = statusDemurrage;
            ddlStatusCalculoSelecionado.DataSource = Session["statusDemurrage"];
            ddlStatusCalculoSelecionado.DataBind();
            ddlStatusCalculoSelecionado.Items.Insert(0, new ListItem("Selecione", ""));*/

            //SELECT DE DROP DE CALCULO COMPRA
            /*SQL = "SELECT * FROM TB_STATUS_DEMURRAGE WHERE FL_COMPRA = 1 AND FL_ATIVO = 1 ";
            statusDemurrage = DBS.List(SQL);
            Session["statusDemurrage"] = statusDemurrage;
            ddlStatusCalculoSelecionadoCompra.DataSource = Session["statusDemurrage"];
            ddlStatusCalculoSelecionadoCompra.DataBind();
            ddlStatusCalculoSelecionadoCompra.Items.Insert(0, new ListItem("Selecione", ""));*/


            //SELECT DE DROP DE ENVIO PARA CONTA CORRENTE VENDA
            //SQL = "SELECT * FROM TB_STATUS_DEMURRAGE WHERE FL_VENDA = 1 AND FL_ATIVO = 1 ";
            //statusDemurrage = DBS.List(SQL);
            //Session["statusDemurrage"] = statusDemurrage;
            //ddlStatusFaturaContaCorrente.DataSource = Session["statusDemurrage"];
            //ddlStatusFaturaContaCorrente.DataBind();
            //ddlStatusFaturaContaCorrente.Items.Insert(0, new ListItem("Selecione", ""));

            //SELECT DE DROP DE ENVIO PARA CONTA CORRENTE COMPRA
            //SQL = "SELECT * FROM TB_STATUS_DEMURRAGE WHERE FL_COMPRA = 1 AND FL_ATIVO = 1 ";
            //statusDemurrage = DBS.List(SQL);
            //Session["statusDemurrage"] = statusDemurrage;
            //ddlStatusFaturaContaCorrenteCompra.DataSource = Session["statusDemurrage"];
            //ddlStatusFaturaContaCorrenteCompra.DataBind();
            //ddlStatusFaturaContaCorrenteCompra.Items.Insert(0, new ListItem("Selecione", ""));

        }
        protected void CarregarFiltroFatura()
        {
            ddlFaturaFiltro.DataBind();
            ddlFaturaFiltro.Items.Insert(0, new ListItem("Selecione", ""));
            ddlFaturaFiltro.Items.Insert(1, new ListItem("Nº Processo", "1"));
            ddlFaturaFiltro.Items.Insert(2, new ListItem("Cliente", "2"));
            ddlFaturaFiltro.Items.Insert(3, new ListItem("Armador", "3"));
        }
    }
}