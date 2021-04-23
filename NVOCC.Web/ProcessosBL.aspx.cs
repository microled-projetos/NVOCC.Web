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
    public partial class ProcessosBL : System.Web.UI.Page
    {
        string SQL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("https://localhost:44348/ConsultarWeek.aspx");
            }
            if (!IsPostBack)
            {
                CarregarTerms();
                CarregarPackaging();
                CarregarStatus();
            }
        }

        protected void CarregarPackaging()
        {
            SQL = "SELECT NM_MERCADORIA, ID_MERCADORIA FROM TB_MERCADORIA";
            DataTable nmMercadoria = new DataTable();
            nmMercadoria = DBS.List(SQL);
            Session["TaskTableMercadoria"] = nmMercadoria;
            ddlMercadoria.DataSource = Session["TaskTableMercadoria"];
            ddlMercadoria.DataBind();
            ddlMercadoria.Items.Insert(0, new ListItem("Selecione", ""));
        }
        protected void CarregarTerms()
        {
            SQL = "SELECT ID_INCOTERM, CD_INCOTERM  + ' - ' + NM_INCOTERM AS DATATEXT FROM TB_INCOTERM";
            DataTable nmMercadoria = new DataTable();
            nmMercadoria = DBS.List(SQL);
            Session["TaskTableTerms"] = nmMercadoria;
            ddlTerms.DataSource = Session["TaskTableTerms"];
            ddlTerms.DataBind();
            ddlTerms.Items.Insert(0, new ListItem("Selecione", ""));
        }
        protected void CarregarStatus()
        {
            SQL = "SELECT ID_STATUS_BL, NM_STATUS_BL FROM TB_STATUS_BL";
            DataTable statusBl = new DataTable();
            statusBl = DBS.List(SQL);
            Session["TaskTableStatus"] = statusBl;
            ddlStatus.DataSource = Session["TaskTableStatus"];
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Selecione", ""));
        }
    }
}