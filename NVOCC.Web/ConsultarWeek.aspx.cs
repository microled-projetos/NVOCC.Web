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
    public partial class ConsultarWeek : System.Web.UI.Page
    {
        string SQL;
        string SQL2;
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarPorto();
            CarregarTipoContainer();
            CarregarParceiro();
            CarregarSales();
            CarregarBroker();
            CarregarImporter();
            CarregarShipper();
            CarregarTipoMercadoria();
            CarregarIncoterm();

        }

        protected void CarregarIncoterm()
        {
            SQL = "SELECT ID_INCOTERM, CD_INCOTERM FROM TB_INCOTERM ";

            DataTable incoterm = new DataTable();
            incoterm = DBS.List(SQL);
            Session["TaskTableincoterm"] = incoterm;
            ddlIncoterm.DataSource = Session["TaskTableincoterm"];
            ddlIncoterm.DataBind();
            ddlIncoterm.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void CarregarTipoMercadoria()
        {
            SQL = "SELECT ID_MERCADORIA, NM_MERCADORIA FROM TB_MERCADORIA";

            DataTable mercadoria = new DataTable();
            mercadoria = DBS.List(SQL);
            Session["TaskTablemercadoria"] = mercadoria;
            ddlTipoMercadoria.DataSource = Session["TaskTablemercadoria"];
            ddlTipoMercadoria.DataBind();
            ddlTipoMercadoria.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void CarregarSales()
        {
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_VENDEDOR = 1";

            DataTable vendedor = new DataTable();
            vendedor = DBS.List(SQL);
            Session["TaskTablevendedor"] = vendedor;
            ddlVendedor.DataSource = Session["TaskTablevendedor"];
            ddlVendedor.DataBind();
            ddlVendedor.Items.Insert(0, new ListItem("Selecione", "0"));
        }
        protected void CarregarBroker()
        {
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_AGENTE = 1";

            DataTable agente = new DataTable();
            agente = DBS.List(SQL);
            Session["TaskTableagente"] = agente;
            ddlBroker.DataSource = Session["TaskTableagente"];
            ddlBroker.DataBind();
            ddlBroker.Items.Insert(0, new ListItem("Selecione", "0"));
        }
        protected void CarregarImporter()
        {
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_IMPORTADOR = 1";

            DataTable cliente = new DataTable();
            cliente = DBS.List(SQL);
            Session["TaskTablecliente"] = cliente;
            ddlImporter.DataSource = Session["TaskTablecliente"];
            ddlImporter.DataBind();
            ddlImporter.Items.Insert(0, new ListItem("Selecione", "0"));
        }
        protected void CarregarShipper()
        {
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_SHIPPER = 1";

            DataTable shipper = new DataTable();
            shipper = DBS.List(SQL);
            Session["TaskTableshipper"] = shipper;
            ddlShipper.DataSource = Session["TaskTableshipper"];
            ddlShipper.DataBind();
            ddlShipper.Items.Insert(0, new ListItem("Selecione", "0"));
        }


        protected void CarregarPorto()
        {
            SQL = "SELECT ID_PORTO, NM_PORTO FROM TB_PORTO ORDER BY NM_PORTO";

            DataTable porto = new DataTable();
            porto = DBS.List(SQL);
            Session["TaskTablePorto"] = porto;
            ddlPortoLocal.DataSource = Session["TaskTablePorto"];
            ddlPortoLocal.DataBind();
            ddlPortoLocal.Items.Insert(0, new ListItem("Selecione", ""));
            DataTable portoDestino = new DataTable();
            portoDestino = DBS.List(SQL);
            Session["TaskTablePortoDestino"] = portoDestino;
            ddlPortoDestino.DataSource = Session["TaskTablePortoDestino"];
            ddlPortoDestino.DataBind();
            ddlPortoDestino.Items.Insert(0, new ListItem("Selecione", ""));
        }
        protected void CarregarTipoContainer()
        {
            SQL = "select ID_TIPO_CONTAINER, NM_TIPO_CONTAINER from tb_tipo_container order by SUBSTRING(NM_TIPO_CONTAINER, 2, 10)";

            DataTable container = new DataTable();
            container = DBS.List(SQL);
            Session["TaskTableContainer"] = container;
            ddlTipoConteiner.DataSource = Session["TaskTableContainer"];
            ddlTipoConteiner.DataBind();
            ddlTipoConteiner.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void CarregarParceiro()
        {
            SQL = "SELECT ID_PARCEIRO, NM_RAZAO FROM TB_PARCEIRO";

            DataTable parceiroWeek = new DataTable();
            parceiroWeek = DBS.List(SQL);
            Session["TaskTableParceiroWeek"] = parceiroWeek;
            ddlParceiroWeek.DataSource = Session["TaskTableParceiroWeek"];
            ddlParceiroWeek.DataBind();
            ddlParceiroWeek.Items.Insert(0, new ListItem("Selecione", ""));
        }

    }
}