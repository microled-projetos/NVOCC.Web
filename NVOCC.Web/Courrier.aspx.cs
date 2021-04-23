using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABAINFRA.Web
{
    public partial class Courrier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarListaFiltros();
            }
        }

        private void CarregarListaFiltros()
        {
            ddlFiltro.DataBind();
            ddlFiltro.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlFiltro.Items.Insert(1, new ListItem("Nº Processo", "1"));
            ddlFiltro.Items.Insert(2, new ListItem("BL Master", "2"));
            ddlFiltro.Items.Insert(3, new ListItem("Cliente", "3"));
            ddlFiltro.Items.Insert(4, new ListItem("Navio", "4"));


        }
    }
}