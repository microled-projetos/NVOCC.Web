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
	public partial class AtendimentoNegado : System.Web.UI.Page
	{
		string SQL;
		protected void Page_Load(object sender, EventArgs e)
		{
			Porto();
			Vendedores();
			Status();
			Inside();
			Incoterm();
			TipoServico();
			TipoEstufagem();
			TipoCarga();
			TipoEmbalagem();
			TipoContainer();
		}

		private void Porto()
		{
			SQL = "SELECT ID_PORTO, CASE WHEN ID_VIATRANSPORTE = 4 THEN CD_SIGLA + ' - ' + NM_PORTO ELSE NM_PORTO + ' - ' + CD_SIGLA END AS NM_PORTO FROM TB_PORTO ORDER BY ID_VIATRANSPORTE, NM_PORTO ";
			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["porto"] = listTable;
			ddlOrigem.DataSource = Session["porto"];
			ddlOrigem.DataBind();
			ddlOrigem.Items.Insert(0, new ListItem("Selecione", ""));
			ddlDestino.DataSource = Session["porto"];
			ddlDestino.DataBind();
			ddlDestino.Items.Insert(0, new ListItem("Selecione", ""));
		}

		private void Vendedores()
		{
			SQL = "SELECT ID_PARCEIRO, NM_RAZAO FROM TB_PARCEIRO WHERE FL_VENDEDOR_DIRETO = 1 ORDER BY NM_RAZAO";
			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["vendedor"] = listTable;
			ddlVendedor.DataSource = Session["vendedor"];
			ddlVendedor.DataBind();
			ddlVendedor.Items.Insert(0, new ListItem("Selecione", ""));
		}

		private void Status()
		{
			SQL = "SELECT ID_STATUS_COTACAO, NM_STATUS_COTACAO FROM TB_STATUS_COTACAO ORDER BY NM_STATUS_COTACAO ";
			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["status"] = listTable;
			ddlStatus.DataSource = Session["status"];
			ddlStatus.DataBind();
			ddlStatus.Items.Insert(0, new ListItem("Selecione", "0"));
		}

		private void Inside()
		{
			SQL = "select ID_PARCEIRO, NM_RAZAO from tb_parceiro where FL_EQUIPE_INSIDE_SALES = 1 ORDER BY NM_RAZAO ";
			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["inside"] = listTable;
			ddlInside.DataSource = Session["inside"];
			ddlInside.DataBind();
			ddlInside.Items.Insert(0, new ListItem("Selecione", ""));
		}

		private void Incoterm()
		{
			SQL = "SELECT ID_INCOTERM, NM_INCOTERM FROM TB_INCOTERM ORDER BY NM_INCOTERM ";

			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["incoterm"] = listTable;
			ddlIncoterm.DataSource = Session["incoterm"];
			ddlIncoterm.DataBind();
			ddlIncoterm.Items.Insert(0, new ListItem("Selecione", ""));
		}

		private void TipoServico()
		{
			SQL = "SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO ORDER BY NM_SERVICO ";

			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["servico"] = listTable;
			ddlTipoServico.DataSource = Session["servico"];
			ddlTipoServico.DataBind();
			ddlTipoServico.Items.Insert(0, new ListItem("Selecione", ""));
		}

		private void TipoEstufagem()
		{
			SQL = "SELECT ID_TIPO_ESTUFAGEM, NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM ORDER BY NM_TIPO_ESTUFAGEM";

			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["estufagem"] = listTable;
			ddlTipoEstufagem.DataSource = Session["estufagem"];
			ddlTipoEstufagem.DataBind();
			ddlTipoEstufagem.Items.Insert(0, new ListItem("Selecione", ""));
		}

		private void TipoCarga()
		{
			SQL = "SELECT ID_TIPO_CARGA, NM_TIPO_CARGA FROM TB_TIPO_CARGA ORDER BY NM_TIPO_CARGA";

			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["carga"] = listTable;
			ddlTipoCarga.DataSource = Session["carga"];
			ddlTipoCarga.DataBind();
			ddlTipoCarga.Items.Insert(0, new ListItem("Selecione", "0"));
		}
		
		private void TipoEmbalagem()
		{
			SQL = "SELECT ID_MERCADORIA, NM_MERCADORIA FROM TB_MERCADORIA ORDER BY NM_MERCADORIA";

			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["embalagem"] = listTable;
			ddlTipoEmbalagem.DataSource = Session["embalagem"];
			ddlTipoEmbalagem.DataBind();
			ddlTipoEmbalagem.Items.Insert(0, new ListItem("Selecione", "0"));
		}

		private void TipoContainer()
		{
			SQL = "SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER ORDER BY NM_TIPO_CONTAINER";

			DataTable listTable = new DataTable();
			listTable = DBS.List(SQL);

			Session["tpcontainer"] = listTable;
			ddlTipoContainer.DataSource = Session["tpcontainer"];
			ddlTipoContainer.DataBind();
			ddlTipoContainer.Items.Insert(0, new ListItem("Selecione", "0"));
		}
	}
}