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
    public partial class ModuloOperacional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            listarVia();
            listarEtapa();
            listarServico();
            listarStatus();
            listarWeek();
        }

        private void listarVia()
        {
            ddlVia.DataBind();
            ddlVia.Items.Insert(0, new ListItem("Todas",""));
            ddlVia.Items.Insert(1, new ListItem("Marítima", "1"));
            ddlVia.Items.Insert(2, new ListItem("Aérea", "4"));
        }
        private void listarEtapa()
        {
            ddlEtapa.DataBind();
            ddlEtapa.Items.Insert(0, new ListItem("Todas",""));
            ddlEtapa.Items.Insert(1, new ListItem("Pré-Embarque", "1"));
            ddlEtapa.Items.Insert(2, new ListItem("Pós-Embarque", "2"));
            ddlEtapa.Items.Insert(3, new ListItem("Pós-Chegada", "3"));

        }
        private void listarServico()
        {
            ddlServico.DataBind();
            ddlServico.Items.Insert(0, new ListItem("Todos",""));
            ddlServico.Items.Insert(1, new ListItem("Importação", "1"));
            ddlServico.Items.Insert(2, new ListItem("Exportação", "2"));
        }
        private void listarStatus()
        {
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Todos",""));
            ddlStatus.Items.Insert(1, new ListItem("Ativos", "1"));
            ddlStatus.Items.Insert(2, new ListItem("Cancelados", "2"));
            ddlStatus.Items.Insert(2, new ListItem("Finalizados", "3"));
        }
        private void listarWeek()
        {
            string SQL;
            SQL = "SELECT ID_WEEK, NM_WEEK FROM TB_WEEK";
            DataTable week = new DataTable();
            week = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = week;
            ddlWeek.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlWeek.DataBind();
            ddlWeek.Items.Insert(0, new ListItem("Selecione", ""));
        }
    }
}