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
        }

        protected void CarregarPorto()
        {
            SQL = "SELECT ID_PORTO, NM_PORTO FROM TB_PORTO";

            DataTable porto = new DataTable();
            porto = DBS.List(SQL);
            Session["TaskTablePorto"] = porto;
            ddlPortoLocal.DataSource = Session["TaskTablePorto"];
            ddlPortoLocal.DataBind();
            ddlPortoLocal.Items.Insert(0, new ListItem("Selecione", ""));

            SQL2 = "SELECT ID_PORTO, NM_PORTO FROM TB_PORTO";

            DataTable portoDestino = new DataTable();
            portoDestino = DBS.List(SQL2);
            Session["TaskTablePortoDestino"] = portoDestino;
            ddlPortoDestino.DataSource = Session["TaskTablePortoDestino"];
            ddlPortoDestino.DataBind();
            ddlPortoDestino.Items.Insert(0, new ListItem("Selecione", ""));
        }
        protected void CarregarTipoContainer()
        {
            SQL = "SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER";

            DataTable container = new DataTable();
            container = DBS.List(SQL);
            Session["TaskTableContainer"] = container;
            ddlTipoContainer.DataSource = Session["TaskTableContainer"];
            ddlTipoContainer.DataBind();
            ddlTipoContainer.Items.Insert(0, new ListItem("Selecione", ""));
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