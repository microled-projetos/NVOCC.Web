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
            ddlAnoInicial.DataBind();
            ddlAnoInicial.Items.Insert(0, new ListItem("Selecione", ""));
            ddlAnoInicial.Items.Insert(1, new ListItem("2018", "18"));
            ddlAnoInicial.Items.Insert(2, new ListItem("2019", "19"));
            ddlAnoInicial.Items.Insert(3, new ListItem("2020", "20"));
            ddlAnoInicial.Items.Insert(4, new ListItem("2021", "21"));
            ddlAnoInicial.Items.Insert(5, new ListItem("2022", "22"));
            ddlAnoInicial.Items.Insert(6, new ListItem("2023", "23"));
            ddlAnoInicial.Items.Insert(7, new ListItem("2024", "24"));
            ddlAnoInicial.Items.Insert(8, new ListItem("2025", "25"));
            ddlAnoInicial.Items.Insert(9, new ListItem("2026", "26"));
            ddlAnoInicial.Items.Insert(10, new ListItem("2027", "27"));
            ddlAnoInicial.Items.Insert(11, new ListItem("2028", "28"));
            ddlAnoInicial.Items.Insert(12, new ListItem("2029", "29"));
            ddlAnoInicial.Items.Insert(13, new ListItem("2030", "30"));
            ddlAnoInicial.Items.Insert(14, new ListItem("2031", "31"));
            ddlAnoInicial.Items.Insert(15, new ListItem("2032", "32"));
            ddlAnoInicial.Items.Insert(16, new ListItem("2033", "33"));
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
            ddlAnoFinal.DataBind();
            ddlAnoFinal.Items.Insert(0, new ListItem("Selecione", ""));
            ddlAnoFinal.Items.Insert(1, new ListItem("2018", "18"));
            ddlAnoFinal.Items.Insert(2, new ListItem("2019", "19"));
            ddlAnoFinal.Items.Insert(3, new ListItem("2020", "20"));
            ddlAnoFinal.Items.Insert(4, new ListItem("2021", "21"));
            ddlAnoFinal.Items.Insert(5, new ListItem("2022", "22"));
            ddlAnoFinal.Items.Insert(6, new ListItem("2023", "23"));
            ddlAnoFinal.Items.Insert(7, new ListItem("2024", "24"));
            ddlAnoFinal.Items.Insert(8, new ListItem("2025", "25"));
            ddlAnoFinal.Items.Insert(9, new ListItem("2026", "26"));
            ddlAnoFinal.Items.Insert(10, new ListItem("2027", "27"));
            ddlAnoFinal.Items.Insert(11, new ListItem("2028", "28"));
            ddlAnoFinal.Items.Insert(12, new ListItem("2029", "29"));
            ddlAnoFinal.Items.Insert(13, new ListItem("2030", "30"));
            ddlAnoFinal.Items.Insert(14, new ListItem("2031", "31"));
            ddlAnoFinal.Items.Insert(15, new ListItem("2032", "32"));
            ddlAnoFinal.Items.Insert(16, new ListItem("2033", "33"));
            ddlTipoOperacao.DataBind();
            ddlTipoOperacao.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTipoOperacao.Items.Insert(1, new ListItem("FCL", "1"));
            ddlTipoOperacao.Items.Insert(2, new ListItem("LCL", "2"));
            ddlTipoOperacao.Items.Insert(3, new ListItem("FCL/LCL", "3"));
            ddlTipoOperacao.Items.Insert(4, new ListItem("Aéreo", "4"));
        }
    }
}