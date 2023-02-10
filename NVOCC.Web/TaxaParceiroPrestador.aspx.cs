using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;

namespace ABAINFRA.Web
{
	public partial class TaxaParceiroPrestador : System.Web.UI.Page
    {
        string SQL;
        string URL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("https://localhost:44348/Default.aspx");
            }
            if (!IsPostBack)
            {
                CarregarItemDespesa();
                CarregarCobranca();
                CarregarBaseCalculo();
                CarregarMoedaVenda();
                CarregarParceiro();
                DisableField();
                CarregarOrigem();
                CarregarTipoPagamento();
                CarregarComex();
                CarregarVia();
                CarregarTipoEstufagem();
                CarregarPortos();
                CarregarTipoCobranca();
                CarregarVariacoes();
                CarregarIncoterm();
            }
        }
        private void DisableField()
        {
            txtCodigoTipoItem.Attributes.Add("disabled", "disabled");
            ddlTipoItem.Attributes.Add("disabled", "disabled");
            ddlBaseCalculo.Attributes.Add("disabled", "disabled");
            txtMoedaCompra.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaCompra.Attributes.Add("disabled", "disabled");
            baseCompra.Attributes.Add("disabled", "disabled");
            txtMoedaVenda.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaVenda.Attributes.Add("disabled", "disabled");
            baseVenda.Attributes.Add("disabled", "disabled");
            ddlDeclarado.Attributes.Add("disabled", "disabled");
            ddlProfit.Attributes.Add("disabled", "disabled");
            ddlCobranca.Attributes.Add("disabled", "disabled");
            txtObsTaxa.Attributes.Add("disabled", "disabled");

        }
        private void CarregarParceiro()
        {
            URL = Request.QueryString["id"];
            SQL = "SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = '" + URL + "' ";

            DataTable parceiro = new DataTable();
            parceiro = DBS.List(SQL);
            if (parceiro != null)
            {
                txtParceiros.Text = parceiro.Rows[0]["NM_RAZAO"].ToString();
            }
            else
            {
                Response.Redirect("https://localhost:44348/ConsultaParceiro.aspx");
            }


        }
        private void CarregarMoedaVenda()
        {
            SQL = "SELECT CD_MOEDA, NM_MOEDA FROM TB_MOEDA ORDER BY NM_MOEDA";

            DataTable moedaVenda = new DataTable();
            moedaVenda = DBS.List(SQL);
            Session["TaskTableMoedaVenda"] = moedaVenda;
            ddlTipoMoedaVenda.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaVenda.DataBind();
            ddlTipoMoedaVenda.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTipoMoedaCompra.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaCompra.DataBind();
            ddlTipoMoedaCompra.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregarBaseCalculo()
        {
            SQL = "SELECT ID_BASE_CALCULO_TAXA, NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA";

            DataTable baseCalculo = new DataTable();
            baseCalculo = DBS.List(SQL);
            Session["TaskTableBaseCalc"] = baseCalculo;
            ddlBaseCalculo.DataSource = Session["TaskTableBaseCalc"];
            ddlBaseCalculo.DataBind();
            ddlBaseCalculo.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregarItemDespesa()
        {
            SQL = "SELECT ID_ITEM_DESPESA, NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_ATIVO = 1 ORDER BY NM_ITEM_DESPESA";

            DataTable itemDespesa = new DataTable();
            itemDespesa = DBS.List(SQL);
            Session["TaskTableItemDespesa"] = itemDespesa;
            ddlTipoItem.DataSource = Session["TaskTableItemDespesa"];
            ddlTipoItem.DataBind();
            ddlTipoItem.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregarCobranca()
        {
            SQL = "SELECT ID_DESTINATARIO_COBRANCA, NM_DESTINATARIO_COBRANCA FROM TB_DESTINATARIO_COBRANCA";

            DataTable cobranca = new DataTable();
            cobranca = DBS.List(SQL);
            Session["TaskTableCobranca"] = cobranca;
            ddlCobranca.DataSource = Session["TaskTableCobranca"];
            ddlCobranca.DataBind();
            ddlCobranca.Items.Insert(0, new ListItem("Selecione", ""));

        }
        private void CarregarTipoPagamento()
        {
            SQL = "SELECT ID_TIPO_PAGAMENTO, NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO";

            DataTable tipoPagamento = new DataTable();
            tipoPagamento = DBS.List(SQL);
            Session["TaskTableTipoPagamento"] = tipoPagamento;
            ddlTipoPagamento.DataSource = Session["TaskTableTipoPagamento"];
            ddlTipoPagamento.DataBind();
            ddlTipoPagamento.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregarTipoEstufagem()
        {
            SQL = "SELECT ID_TIPO_ESTUFAGEM, NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM";

            DataTable tipoEstufagem = new DataTable();
            tipoEstufagem = DBS.List(SQL);
            Session["TaskTableTipoEstufagem"] = tipoEstufagem;
            ddlTipoEstufagem.DataSource = Session["TaskTableTipoEstufagem"];
            ddlTipoEstufagem.DataBind();
            ddlTipoEstufagem.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregarOrigem()
        {
            SQL = "SELECT ID_ORIGEM_PAGAMENTO, NM_ORIGEM_PAGAMENTO FROM TB_ORIGEM_PAGAMENTO";

            DataTable cobranca = new DataTable();
            cobranca = DBS.List(SQL);
            Session["TaskTableOrigem"] = cobranca;
            ddlOrigemServico.DataSource = Session["TaskTableOrigem"];
            ddlOrigemServico.DataBind();
            ddlOrigemServico.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregarComex()
        {
            SQL = "SELECT NM_TIPO_COMEX, ID_TIPO_COMEX FROM TB_TIPO_COMEX";

            DataTable comex = new DataTable();
            comex = DBS.List(SQL);
            Session["TaskTableComex"] = comex;
            ddlTipoComex.DataSource = Session["TaskTableComex"];
            ddlTipoComex.DataBind();
            ddlTipoComex.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregarVia()
        {
            SQL = "SELECT ID_VIATRANSPORTE, NM_VIATRANSPORTE FROM TB_VIATRANSPORTE WHERE ID_VIATRANSPORTE IN (1,4)";

            DataTable via = new DataTable();
            via = DBS.List(SQL);
            Session["TaskTableVia"] = via;
            ddlViaTransporte.DataSource = Session["TaskTableVia"];
            ddlViaTransporte.DataBind();
            ddlViaTransporte.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregarIncoterm()
        {
            SQL = "SELECT ID_INCOTERM, CONCAT(CD_INCOTERM,' - ', NM_INCOTERM) AS INCOTERM FROM TB_INCOTERM ";

            DataTable incoterm = new DataTable();
            incoterm = DBS.List(SQL);
            Session["TaskTableIncoterm"] = incoterm;
            ddlIncoterm.DataSource = Session["TaskTableIncoterm"];
            ddlIncoterm.DataBind();
            ddlIncoterm.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlIncoterm.SelectedValue = "0";
        }
        private void CarregarPortos()
        {
            SQL = "SELECT ID_PORTO, NM_PORTO FROM TB_PORTO";

            DataTable porto = new DataTable();
            porto = DBS.List(SQL);
            Session["TaskTablePorto"] = porto;
            ddlPortoDescarga.DataSource = Session["TaskTablePorto"];
            ddlPortoDescarga.DataBind();
            ddlPortoDescarga.Items.Insert(0, new ListItem("Selecione", "0"));

            ddlPortoCarregamento.DataSource = Session["TaskTablePorto"];
            ddlPortoCarregamento.DataBind();
            ddlPortoCarregamento.Items.Insert(0, new ListItem("Selecione", "0"));

            ddlPortoRecebimento.DataSource = Session["TaskTablePorto"];
            ddlPortoRecebimento.DataBind();
            ddlPortoRecebimento.Items.Insert(0, new ListItem("Selecione", "0"));

        }
        private void CarregarTipoCobranca()
        {
            SQL = "SELECT ID_TIPO_COBRANCA, NM_TIPO_COBRANCA FROM TB_TIPO_COBRANCA";

            DataTable cobranca = new DataTable();
            cobranca = DBS.List(SQL);
            Session["TaskTableCobranca"] = cobranca;
            ddlTipoCobranca.DataSource = Session["TaskTableCobranca"];
            ddlTipoCobranca.DataBind();
        }

        private void CarregarVariacoes()
		{
            SQL = "SELECT ID_BASE_CALCULO_VARIACAO, NM_BASE_CALCULO_VARIACAO FROM TB_BASE_CALCULO_VARIACAO";

            DataTable baseCalculo = new DataTable();
            baseCalculo = DBS.List(SQL);
            Session["TaskTableBaseCalc"] = baseCalculo;
            ddlBaseCalculoVariacao.DataSource = Session["TaskTableBaseCalc"];
            ddlBaseCalculoVariacao.DataBind();
            ddlBaseCalculoVariacao.Items.Insert(0, new ListItem("Selecione", ""));
            

        }

        public static string decBD(string numero)
        {
            if (string.IsNullOrEmpty(numero) == true) { return "0"; }

            return numero = numero.Replace(',', '.');
        }
    }
}