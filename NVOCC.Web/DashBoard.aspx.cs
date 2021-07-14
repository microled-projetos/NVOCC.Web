using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABAINFRA.Web
{
    public partial class DashBoard : System.Web.UI.Page
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
            ddlMesInicial.DataBind();
            ddlMesInicial.Items.Insert(0, new ListItem("Selecione", ""));
            ddlMesInicial.Items.Insert(1, new ListItem("Janeiro", "01"));
            ddlMesInicial.Items.Insert(2, new ListItem("Fevereiro", "02"));
            ddlMesInicial.Items.Insert(3, new ListItem("Março", "03"));
            ddlMesInicial.Items.Insert(4, new ListItem("Abril", "04"));
            ddlMesInicial.Items.Insert(5, new ListItem("Maio", "05"));
            ddlMesInicial.Items.Insert(6, new ListItem("Junho", "06"));
            ddlMesInicial.Items.Insert(7, new ListItem("Julho", "07"));
            ddlMesInicial.Items.Insert(8, new ListItem("Agosto", "08"));
            ddlMesInicial.Items.Insert(9, new ListItem("Setembro", "09"));
            ddlMesInicial.Items.Insert(10, new ListItem("Outubro", "10"));
            ddlMesInicial.Items.Insert(11, new ListItem("Novembro", "11"));
            ddlMesInicial.Items.Insert(12, new ListItem("Dezembro", "12"));
            
            ddlMesFinal.Items.Insert(0, new ListItem("Selecione", ""));
            ddlMesFinal.Items.Insert(1, new ListItem("Janeiro", "01"));
            ddlMesFinal.Items.Insert(2, new ListItem("Fevereiro", "02"));
            ddlMesFinal.Items.Insert(3, new ListItem("Março", "03"));
            ddlMesFinal.Items.Insert(4, new ListItem("Abril", "04"));
            ddlMesFinal.Items.Insert(5, new ListItem("Maio", "05"));
            ddlMesFinal.Items.Insert(6, new ListItem("Junho", "06"));
            ddlMesFinal.Items.Insert(7, new ListItem("Julho", "07"));
            ddlMesFinal.Items.Insert(8, new ListItem("Agosto", "08"));
            ddlMesFinal.Items.Insert(9, new ListItem("Setembro", "09"));
            ddlMesFinal.Items.Insert(10, new ListItem("Outubro", "10"));
            ddlMesFinal.Items.Insert(11, new ListItem("Novembro", "11"));
            ddlMesFinal.Items.Insert(12, new ListItem("Dezembro", "12"));
            
            ddlTipoOperacao.DataBind();
            ddlTipoOperacao.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTipoOperacao.Items.Insert(1, new ListItem("FCL", "1"));
            ddlTipoOperacao.Items.Insert(2, new ListItem("LCL", "2"));
            ddlTipoOperacao.Items.Insert(3, new ListItem("FCL/LCL", "3"));
            ddlTipoOperacao.Items.Insert(4, new ListItem("Aéreo", "4"));
        }
    }
}