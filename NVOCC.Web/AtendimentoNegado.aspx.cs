﻿using Antlr.Runtime;
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
		}

		private void Porto()
		{
			SQL = "SELECT ID_PORTO, NM_PORTO FROM TB_PORTO ORDER BY NM_PORTO ";
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
			SQL = "SELECT ID_PARCEIRO, NM_RAZAO FROM TB_PARCEIRO WHERE FL_VENDEDOR = 1 ORDER BY NM_RAZAO";
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
			ddlStatus.Items.Insert(0, new ListItem("Selecione", ""));
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
	}
}