using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABAINFRA.Web
{
	public partial class TaxasInativas : System.Web.UI.Page
	{
        string SQL;
		protected void Page_Load(object sender, EventArgs e)
		{
            CarregarMoeda();
            listarTipoEstufagem();
            listarAgenteInternacional();
            listarCliente();
            listarItemDespesa();
            ListarServico();
            ListarModal();
            ListarBaseCalculo();
            ListarUsuarios();
        }

        private void ListarBaseCalculo()
        {
            SQL = "SELECT * FROM TB_BASE_CALCULO_TAXA";
            DataTable basec= new DataTable();
            basec = DBS.List(SQL);
            Session["TaskTableBaseCalculo"] = basec;
            ddlBaseCalculo.DataSource = Session["TaskTableBaseCalculo"];
            ddlBaseCalculo.DataBind();
            ddlBaseCalculo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarMoeda()
        {
            SQL = "SELECT * FROM TB_MOEDA";
            DataTable moeda = new DataTable();
            moeda = DBS.List(SQL);
            Session["TaskTableMoeda"] = moeda;
            ddlMoeda.DataSource = Session["TaskTableMoeda"];
            ddlMoeda.DataBind();
            ddlMoeda.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void ListarServico()
        {
            SQL = "SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO";
            DataTable servico = new DataTable();
            servico = DBS.List(SQL);
            Session["TaskTableServico"] = servico;
            ddlServico.DataSource = Session["TaskTableServico"];
            ddlServico.DataBind();
            ddlServico.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void ListarModal()
        {
            SQL = "SELECT ID_VIATRANSPORTE, NM_VIATRANSPORTE FROM TB_VIATRANSPORTE";
            DataTable modal = new DataTable();
            modal = DBS.List(SQL);
            Session["TaskTableModal"] = modal;
            ddlModal.DataSource = Session["TaskTableModal"];
            ddlModal.DataBind();
            ddlModal.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void listarTipoEstufagem()
        {
            string SQL;
            SQL = "SELECT NM_TIPO_ESTUFAGEM, ID_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM";
            DataTable estufagem = new DataTable();
            estufagem = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = estufagem;
            ddlTipoEstufagem.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlTipoEstufagem.DataBind();
            ddlTipoEstufagem.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void listarAgenteInternacional()
        {
            string SQL;
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_AGENTE_INTERNACIONAL = 1 ORDER BY NM_RAZAO";
            DataTable agente = new DataTable();
            agente = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = agente;
            ddlAgenteInternacional.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlAgenteInternacional.DataBind();
            ddlAgenteInternacional.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void listarCliente()
        {
            string SQL;
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO ORDER BY NM_RAZAO";
            DataTable cliente = new DataTable();
            cliente = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = cliente;
            ddlCliente.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem("Selecione", ""));
            
            ddlFornecedor.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlFornecedor.DataBind();
            ddlFornecedor.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void listarItemDespesa()
        {
            string SQL;
            SQL = "SELECT ID_ITEM_DESPESA, NM_ITEM_DESPESA FROM TB_ITEM_DESPESA ORDER BY NM_ITEM_DESPESA ";
            DataTable item = new DataTable();
            item = DBS.List(SQL);
            Session["TaskTableItem"] = item;
            ddlItemDespesa.DataSource = Session["TaskTableItem"];
            ddlItemDespesa.DataBind();
            ddlItemDespesa.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void ListarUsuarios()
        {
            string SQL;
            SQL = "SELECT ID_USUARIO, NOME FROM TB_USUARIO ORDER BY NOME ";
            DataTable user = new DataTable();
            user = DBS.List(SQL);
            Session["TaskTableUser"] = user;
            ddlUsuario.DataSource = Session["TaskTableUser"];
            ddlUsuario.DataBind();
            ddlUsuario.Items.Insert(0, new ListItem("Selecione", ""));
        }
    }
}