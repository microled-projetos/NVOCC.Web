﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABAINFRA.Web
{
    public partial class ModuloGerencial : System.Web.UI.Page
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
            ddlFiltro.Items.Insert(2, new ListItem("Cliente", "2"));
            ddlServico.DataBind();
            ddlServico.Items.Insert(0, new ListItem("Todos", "0"));
            ddlServico.Items.Insert(1, new ListItem("Importação", "1"));
            ddlServico.Items.Insert(2, new ListItem("Exportação", "2"));
            ddlTipoEstufagem.DataBind();
            ddlTipoEstufagem.Items.Insert(0, new ListItem("Todos", "0"));
            ddlTipoEstufagem.Items.Insert(1, new ListItem("FCL", "1"));
            ddlTipoEstufagem.Items.Insert(2, new ListItem("LCL", "2"));
            ddlTipoEstufagem.Items.Insert(3, new ListItem("Aereo", "3"));
        }
    }
}