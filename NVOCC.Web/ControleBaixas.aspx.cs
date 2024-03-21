using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABAINFRA.Web
{
    public partial class ControleBaixas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarListaFiltros();
        }

        private void CarregarListaFiltros()
        {
            ddlTipoDocumento.DataBind();
            ddlTipoDocumento.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlTipoDocumento.Items.Insert(1, new ListItem("NF", "NF"));
            ddlTipoDocumento.Items.Insert(2, new ListItem("ND", "ND"));


        }
    }
}