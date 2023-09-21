using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

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
            ddlFiltro.Items.Insert(3, new ListItem("BL House", "3"));
            ddlFiltro.Items.Insert(4, new ListItem("Cliente", "4"));
            ddlFiltro.Items.Insert(5, new ListItem("Código Rastreio", "5"));

            ddlFiltroRetirada.DataBind();
            ddlFiltroRetirada.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlFiltroRetirada.Items.Insert(1, new ListItem("Nº Processo", "1"));
            ddlFiltroRetirada.Items.Insert(2, new ListItem("Bl House", "2"));
            ddlFiltroRetirada.Items.Insert(3, new ListItem("Cliente", "3"));

            ddlFiltroLiberacao.DataBind();
            ddlFiltroLiberacao.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlFiltroLiberacao.Items.Insert(1, new ListItem("Nº Processo", "1"));
            ddlFiltroLiberacao.Items.Insert(2, new ListItem("BL Master", "2"));
            ddlFiltroLiberacao.Items.Insert(3, new ListItem("Bl House", "3"));
            ddlFiltroLiberacao.Items.Insert(4, new ListItem("Cliente", "4"));
            ddlFiltroLiberacao.Items.Insert(5, new ListItem("Código Rastreio", "5"));

            ddlFiltroConcluido.DataBind();
            ddlFiltroConcluido.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlFiltroConcluido.Items.Insert(1, new ListItem("Nº Processo", "1"));
            ddlFiltroConcluido.Items.Insert(2, new ListItem("Bl House", "2"));
            ddlFiltroConcluido.Items.Insert(3, new ListItem("Cliente", "3"));
        }
    }
}