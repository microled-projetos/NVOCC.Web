using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABAINFRA.Web
{
    public partial class CadastroGerencial : System.Web.UI.Page
    {
        string SQL;
        protected void Page_Load(object sender, EventArgs e)
        {
            listaPais();
            listaCidade();
            listaViaTransporte();
            listaSigla();
            listaPorto();
        }

        private void listaPais()
        {
            SQL = "SELECT ID_PAIS, NM_PAIS FROM TB_PAIS";
            DataTable pais = new DataTable();
            pais = DBS.List(SQL);
            Session["TaskTablePais"] = pais;
            ddlPais.DataSource = Session["TaskTablePais"];
            ddlPais.DataBind();
            ddlPais.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        private void listaCidade()
        {
            SQL = "SELECT ID_CIDADE, NM_CIDADE FROM TB_CIDADE";
            DataTable cidade = new DataTable();
            cidade = DBS.List(SQL);
            Session["TaskTableCidade"] = cidade;
            ddlCidade.DataSource = Session["TaskTableCidade"];
            ddlCidade.DataBind();
            ddlCidade.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlCidadeCadastro.DataSource = Session["TaskTableCidade"];
            ddlCidadeCadastro.DataBind();
            ddlCidadeCadastro.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlCidadeEdit.DataSource = Session["TaskTableCidade"];
            ddlCidadeEdit.DataBind();
            ddlCidadeEdit.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        private void listaViaTransporte()
        {
            SQL = "SELECT ID_VIATRANSPORTE, NM_VIATRANSPORTE FROM TB_VIATRANSPORTE";
            DataTable viatransporte = new DataTable();
            viatransporte = DBS.List(SQL);
            Session["TaskTableViaTransporte"] = viatransporte;
            ddlViaTransporte.DataSource = Session["TaskTableViaTransporte"];
            ddlViaTransporte.DataBind();
            ddlViaTransporte.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlViaTransporteCadastro.DataSource = Session["TaskTableViaTransporte"];
            ddlViaTransporteCadastro.DataBind();
            ddlViaTransporteCadastro.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlViaTransporteEdit.DataSource = Session["TaskTableViaTransporte"];
            ddlViaTransporteEdit.DataBind();
            ddlViaTransporteEdit.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        private void listaSigla()
        {
            SQL = "SELECT ID_PORTO, CD_SIGLA FROM TB_PORTO";
            DataTable sigla = new DataTable();
            sigla = DBS.List(SQL);
            Session["TaskTableSigla"] = sigla;
            ddlSigla.DataSource = Session["TaskTableSigla"];
            ddlSigla.DataBind();
            ddlSigla.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        private void listaPorto()
        {
            SQL = "SELECT ID_PORTO, NM_PORTO FROM TB_PORTO";
            DataTable porto = new DataTable();
            porto = DBS.List(SQL);
            Session["TaskTablePorto"] = porto;
            ddlPorto.DataSource = Session["TaskTablePorto"];
            ddlPorto.DataBind();
            ddlPorto.Items.Insert(0, new ListItem("Selecione", "0"));
        }
    }
}