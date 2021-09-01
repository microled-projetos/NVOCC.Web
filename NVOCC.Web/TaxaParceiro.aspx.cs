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
    public partial class TaxaParceiro : System.Web.UI.Page
    {
        string SQL;
        string URL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] == null)
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
            }
        }
        private void DisableField()
        {
            txtCodigoTipoItemFCLimpo.Attributes.Add("disabled", "disabled");
            ddlTipoItemFCLimpo.Attributes.Add("disabled", "disabled");
            ddlBaseCalculoFCLimpo.Attributes.Add("disabled", "disabled");
            txtMoedaCompraFCLimpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaCompraFCLimpo.Attributes.Add("disabled", "disabled");
            baseCompraFCLimpo.Attributes.Add("disabled", "disabled");
            txtMoedaVendaFCLimpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaVendaFCLimpo.Attributes.Add("disabled", "disabled");
            baseVendaFCLimpo.Attributes.Add("disabled", "disabled");
            ddlDeclaradoFCLimpo.Attributes.Add("disabled", "disabled");
            ddlProfitFCLimpo.Attributes.Add("disabled", "disabled");
            ddlCobrancaFCLimpo.Attributes.Add("disabled", "disabled");
            txtObsTaxaFCLimpo.Attributes.Add("disabled", "disabled");
            txtCodigoTipoItemLCLimpo.Attributes.Add("disabled", "disabled");
            ddlTipoItemLCLimpo.Attributes.Add("disabled", "disabled");
            ddlBaseCalculoLCLimpo.Attributes.Add("disabled", "disabled");
            txtMoedaCompraLCLimpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaCompraLCLimpo.Attributes.Add("disabled", "disabled");
            baseCompraLCLimpo.Attributes.Add("disabled", "disabled");
            txtMoedaVendaLCLimpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaVendaLCLimpo.Attributes.Add("disabled", "disabled");
            baseVendaLCLimpo.Attributes.Add("disabled", "disabled");
            ddlDeclaradoLCLimpo.Attributes.Add("disabled", "disabled");
            ddlProfitLCLimpo.Attributes.Add("disabled", "disabled");
            ddlCobrancaLCLimpo.Attributes.Add("disabled", "disabled");
            txtObsTaxaLCLimpo.Attributes.Add("disabled", "disabled");
            txtCodigoTipoItemFCLexpo.Attributes.Add("disabled", "disabled");
            ddlTipoItemFCLexpo.Attributes.Add("disabled", "disabled");
            ddlBaseCalculoFCLexpo.Attributes.Add("disabled", "disabled");
            txtMoedaCompraFCLexpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaCompraFCLexpo.Attributes.Add("disabled", "disabled");
            baseCompraFCLexpo.Attributes.Add("disabled", "disabled");
            txtMoedaVendaFCLexpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaVendaFCLexpo.Attributes.Add("disabled", "disabled");
            baseVendaFCLexpo.Attributes.Add("disabled", "disabled");
            ddlDeclaradoFCLexpo.Attributes.Add("disabled", "disabled");
            ddlProfitFCLexpo.Attributes.Add("disabled", "disabled");
            ddlCobrancaFCLexpo.Attributes.Add("disabled", "disabled");
            txtObsTaxaFCLexpo.Attributes.Add("disabled", "disabled");
            txtCodigoTipoItemLCLexpo.Attributes.Add("disabled", "disabled");
            ddlTipoItemLCLexpo.Attributes.Add("disabled", "disabled");
            ddlBaseCalculoLCLexpo.Attributes.Add("disabled", "disabled");
            txtMoedaCompraLCLexpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaCompraLCLexpo.Attributes.Add("disabled", "disabled");
            baseCompraLCLexpo.Attributes.Add("disabled", "disabled");
            txtMoedaVendaLCLexpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaVendaLCLexpo.Attributes.Add("disabled", "disabled");
            baseVendaLCLexpo.Attributes.Add("disabled", "disabled");
            ddlDeclaradoLCLexpo.Attributes.Add("disabled", "disabled");
            ddlProfitLCLexpo.Attributes.Add("disabled", "disabled");
            ddlCobrancaLCLexpo.Attributes.Add("disabled", "disabled");
            txtObsTaxaLCLexpo.Attributes.Add("disabled", "disabled");
            txtCodigoTipoItemAereoImpo.Attributes.Add("disabled", "disabled");
            ddlTipoItemAereoImpo.Attributes.Add("disabled", "disabled");
            ddlBaseCalculoAereoImpo.Attributes.Add("disabled", "disabled");
            txtMoedaCompraAereoImpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaCompraAereoImpo.Attributes.Add("disabled", "disabled");
            baseCompraAereoImpo.Attributes.Add("disabled", "disabled");
            txtMoedaVendaAereoImpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaVendaAereoImpo.Attributes.Add("disabled", "disabled");
            baseVendaAereoImpo.Attributes.Add("disabled", "disabled");
            ddlDeclaradoAereoImpo.Attributes.Add("disabled", "disabled");
            ddlProfitAereoImpo.Attributes.Add("disabled", "disabled");
            ddlCobrancaAereoImpo.Attributes.Add("disabled", "disabled");
            txtObsTaxaAereoImpo.Attributes.Add("disabled", "disabled");
            txtCodigoTipoItemAereoExpo.Attributes.Add("disabled", "disabled");
            ddlTipoItemAereoExpo.Attributes.Add("disabled", "disabled");
            ddlBaseCalculoAereoExpo.Attributes.Add("disabled", "disabled");
            txtMoedaCompraAereoExpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaCompraAereoExpo.Attributes.Add("disabled", "disabled");
            baseCompraAereoExpo.Attributes.Add("disabled", "disabled");
            txtMoedaVendaAereoExpo.Attributes.Add("disabled", "disabled");
            ddlTipoMoedaVendaAereoExpo.Attributes.Add("disabled", "disabled");
            baseVendaAereoExpo.Attributes.Add("disabled", "disabled");
            ddlDeclaradoAereoExpo.Attributes.Add("disabled", "disabled");
            ddlProfitAereoExpo.Attributes.Add("disabled", "disabled");
            ddlCobrancaAereoExpo.Attributes.Add("disabled", "disabled");
            txtObsTaxaAereoExpo.Attributes.Add("disabled", "disabled");
            ddlTipoPagamentoFCLimpo.Attributes.Add("disabled", "disabled");
            ddlOrigemServicoFCLimpo.Attributes.Add("disabled", "disabled");
            ddlTipoPagamentoFCLexpo.Attributes.Add("disabled", "disabled");
            ddlOrigemServicoFCLexpo.Attributes.Add("disabled", "disabled");
            ddlTipoPagamentoLCLimpo.Attributes.Add("disabled", "disabled");
            ddlOrigemServicoLCLimpo.Attributes.Add("disabled", "disabled");
            ddlTipoPagamentoLCLexpo.Attributes.Add("disabled", "disabled");
            ddlOrigemServicoLCLexpo.Attributes.Add("disabled", "disabled");
            ddlTipoPagamentoAereoImpo.Attributes.Add("disabled", "disabled");
            ddlOrigemServicoAereoImpo.Attributes.Add("disabled", "disabled");
            ddlTipoPagamentoAereoExpo.Attributes.Add("disabled", "disabled");
            ddlOrigemServicoAereoExpo.Attributes.Add("disabled", "disabled");

        }
        private void CarregarParceiro()
        {
            URL = Request.QueryString["id"];
            SQL = "SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = '" + URL + "' ";

            DataTable parceiro = new DataTable();
            parceiro = DBS.List(SQL);
            if (parceiro != null)
            {
                txtParceiroFCLimpo.Text = parceiro.Rows[0]["NM_RAZAO"].ToString();
                txtParceiroFCLexpo.Text = parceiro.Rows[0]["NM_RAZAO"].ToString();
                txtParceiroLCLimpo.Text = parceiro.Rows[0]["NM_RAZAO"].ToString();
                txtParceiroLCLexpo.Text = parceiro.Rows[0]["NM_RAZAO"].ToString();
                txtParceiroAereoImpo.Text = parceiro.Rows[0]["NM_RAZAO"].ToString();
                txtParceiroAereoExpo.Text = parceiro.Rows[0]["NM_RAZAO"].ToString();
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
            ddlTipoMoedaVendaFCLimpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaVendaFCLimpo.DataBind();
            ddlTipoMoedaVendaFCLimpo.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTipoMoedaCompraFCLimpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaCompraFCLimpo.DataBind();
            ddlTipoMoedaCompraFCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoMoedaVendaFCLexpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaVendaFCLexpo.DataBind();
            ddlTipoMoedaVendaFCLexpo.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTipoMoedaCompraFCLexpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaCompraFCLexpo.DataBind();
            ddlTipoMoedaCompraFCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoMoedaVendaLCLimpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaVendaLCLimpo.DataBind();
            ddlTipoMoedaVendaLCLimpo.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTipoMoedaCompraLCLimpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaCompraLCLimpo.DataBind();
            ddlTipoMoedaCompraLCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoMoedaVendaLCLexpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaVendaLCLexpo.DataBind();
            ddlTipoMoedaVendaLCLexpo.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTipoMoedaCompraLCLexpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaCompraLCLexpo.DataBind();
            ddlTipoMoedaCompraLCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoMoedaVendaAereoImpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaVendaAereoImpo.DataBind();
            ddlTipoMoedaVendaAereoImpo.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTipoMoedaCompraAereoImpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaCompraAereoImpo.DataBind();
            ddlTipoMoedaCompraAereoImpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoMoedaVendaAereoExpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaVendaAereoExpo.DataBind();
            ddlTipoMoedaVendaAereoExpo.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTipoMoedaCompraAereoExpo.DataSource = Session["TaskTableMoedaVenda"];
            ddlTipoMoedaCompraAereoExpo.DataBind();
            ddlTipoMoedaCompraAereoExpo.Items.Insert(0, new ListItem("Selecione", ""));
        }    
        private void CarregarBaseCalculo()
        {
            SQL = "SELECT ID_BASE_CALCULO_TAXA, NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA";

            DataTable baseCalculo = new DataTable();
            baseCalculo = DBS.List(SQL);
            Session["TaskTableBaseCalc"] = baseCalculo;
            ddlBaseCalculoFCLimpo.DataSource = Session["TaskTableBaseCalc"];
            ddlBaseCalculoFCLimpo.DataBind();
            ddlBaseCalculoFCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlBaseCalculoFCLexpo.DataSource = Session["TaskTableBaseCalc"];
            ddlBaseCalculoFCLexpo.DataBind();
            ddlBaseCalculoFCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlBaseCalculoLCLimpo.DataSource = Session["TaskTableBaseCalc"];
            ddlBaseCalculoLCLimpo.DataBind();
            ddlBaseCalculoLCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlBaseCalculoLCLexpo.DataSource = Session["TaskTableBaseCalc"];
            ddlBaseCalculoLCLexpo.DataBind();
            ddlBaseCalculoLCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlBaseCalculoAereoImpo.DataSource = Session["TaskTableBaseCalc"];
            ddlBaseCalculoAereoImpo.DataBind();
            ddlBaseCalculoAereoImpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlBaseCalculoAereoExpo.DataSource = Session["TaskTableBaseCalc"];
            ddlBaseCalculoAereoExpo.DataBind();
            ddlBaseCalculoAereoExpo.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregarItemDespesa()
        {
            SQL = "SELECT ID_ITEM_DESPESA, NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_ATIVO = 1 ORDER BY NM_ITEM_DESPESA";

            DataTable itemDespesaFCLimpo = new DataTable();
            itemDespesaFCLimpo = DBS.List(SQL);
            Session["TaskTableItemDespesaFCLimpo"] = itemDespesaFCLimpo;
            ddlTipoItemFCLimpo.DataSource = Session["TaskTableItemDespesaFCLimpo"];
            ddlTipoItemFCLimpo.DataBind();
            ddlTipoItemFCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            SQL = "SELECT ID_ITEM_DESPESA, NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_ATIVO = 1";

            DataTable itemDespesaFCLexpo = new DataTable();
            itemDespesaFCLexpo = DBS.List(SQL);
            Session["TaskTableItemDespesa"] = itemDespesaFCLexpo;
            ddlTipoItemFCLexpo.DataSource = Session["TaskTableItemDespesa"];
            ddlTipoItemFCLexpo.DataBind();
            ddlTipoItemFCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            SQL = "SELECT ID_ITEM_DESPESA, NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_ATIVO = 1";

            DataTable itemDespesa = new DataTable();
            itemDespesa = DBS.List(SQL);
            Session["TaskTableItemDespesa"] = itemDespesa;
            ddlTipoItemLCLimpo.DataSource = Session["TaskTableItemDespesa"];
            ddlTipoItemLCLimpo.DataBind();
            ddlTipoItemLCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            SQL = "SELECT ID_ITEM_DESPESA, NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_ATIVO = 1";

            DataTable itemDespesaLCLexpo = new DataTable();
            itemDespesaLCLexpo = DBS.List(SQL);
            Session["TaskTableItemDespesaLCLexpo"] = itemDespesaLCLexpo;
            ddlTipoItemLCLexpo.DataSource = Session["TaskTableItemDespesaLCLexpo"];
            ddlTipoItemLCLexpo.DataBind();
            ddlTipoItemLCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            SQL = "SELECT ID_ITEM_DESPESA, NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_ATIVO = 1";

            DataTable itemDespesaAereoImpo = new DataTable();
            itemDespesaAereoImpo = DBS.List(SQL);
            Session["TaskTableItemDespesaAereoImpo"] = itemDespesaAereoImpo;
            ddlTipoItemAereoImpo.DataSource = Session["TaskTableItemDespesaAereoImpo"];
            ddlTipoItemAereoImpo.DataBind();
            ddlTipoItemAereoImpo.Items.Insert(0, new ListItem("Selecione", ""));

            SQL = "SELECT ID_ITEM_DESPESA, NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_ATIVO = 1";

            DataTable itemDespesaAereoExpo = new DataTable();
            itemDespesaAereoExpo = DBS.List(SQL);
            Session["TaskTableItemDespesaAereoExpo"] = itemDespesaAereoExpo;
            ddlTipoItemAereoExpo.DataSource = Session["TaskTableItemDespesaAereoExpo"];
            ddlTipoItemAereoExpo.DataBind();
            ddlTipoItemAereoExpo.Items.Insert(0, new ListItem("Selecione", ""));

        }
        private void CarregarCobranca()
        {
            SQL = "SELECT ID_DESTINATARIO_COBRANCA, NM_DESTINATARIO_COBRANCA FROM TB_DESTINATARIO_COBRANCA";

            DataTable cobranca = new DataTable();
            cobranca = DBS.List(SQL);
            Session["TaskTableCobranca"] = cobranca;
            ddlCobrancaFCLimpo.DataSource = Session["TaskTableCobranca"];
            ddlCobrancaFCLimpo.DataBind();
            ddlCobrancaFCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlCobrancaFCLexpo.DataSource = Session["TaskTableCobranca"];
            ddlCobrancaFCLexpo.DataBind();
            ddlCobrancaFCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlCobrancaLCLimpo.DataSource = Session["TaskTableCobranca"];
            ddlCobrancaLCLimpo.DataBind();
            ddlCobrancaLCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlCobrancaLCLexpo.DataSource = Session["TaskTableCobranca"];
            ddlCobrancaLCLexpo.DataBind();
            ddlCobrancaLCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlCobrancaAereoImpo.DataSource = Session["TaskTableCobranca"];
            ddlCobrancaAereoImpo.DataBind();
            ddlCobrancaAereoImpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlCobrancaAereoExpo.DataSource = Session["TaskTableCobranca"];
            ddlCobrancaAereoExpo.DataBind();
            ddlCobrancaAereoExpo.Items.Insert(0, new ListItem("Selecione", ""));

        }

        private void CarregarTipoPagamento()
        {
            SQL = "SELECT ID_TIPO_PAGAMENTO, NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO";

            DataTable tipoPagamento = new DataTable();
            tipoPagamento = DBS.List(SQL);
            Session["TaskTableTipoPagamento"] = tipoPagamento;
            ddlTipoPagamentoFCLimpo.DataSource = Session["TaskTableTipoPagamento"];
            ddlTipoPagamentoFCLimpo.DataBind();
            ddlTipoPagamentoFCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoPagamentoFCLexpo.DataSource = Session["TaskTableTipoPagamento"];
            ddlTipoPagamentoFCLexpo.DataBind();
            ddlTipoPagamentoFCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoPagamentoLCLimpo.DataSource = Session["TaskTableTipoPagamento"];
            ddlTipoPagamentoLCLimpo.DataBind();
            ddlTipoPagamentoLCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoPagamentoLCLexpo.DataSource = Session["TaskTableTipoPagamento"];
            ddlTipoPagamentoLCLexpo.DataBind();
            ddlTipoPagamentoLCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoPagamentoAereoImpo.DataSource = Session["TaskTableTipoPagamento"];
            ddlTipoPagamentoAereoImpo.DataBind();
            ddlTipoPagamentoAereoImpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlTipoPagamentoAereoExpo.DataSource = Session["TaskTableTipoPagamento"];
            ddlTipoPagamentoAereoExpo.DataBind();
            ddlTipoPagamentoAereoExpo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarOrigem()
        {
            SQL = "SELECT ID_ORIGEM_PAGAMENTO, NM_ORIGEM_PAGAMENTO FROM TB_ORIGEM_PAGAMENTO";

            DataTable cobranca = new DataTable();
            cobranca = DBS.List(SQL);
            Session["TaskTableOrigem"] = cobranca;
            ddlOrigemServicoFCLimpo.DataSource = Session["TaskTableOrigem"];
            ddlOrigemServicoFCLimpo.DataBind();
            ddlOrigemServicoFCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlOrigemServicoFCLexpo.DataSource = Session["TaskTableOrigem"];
            ddlOrigemServicoFCLexpo.DataBind();
            ddlOrigemServicoFCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlOrigemServicoLCLimpo.DataSource = Session["TaskTableOrigem"];
            ddlOrigemServicoLCLimpo.DataBind();
            ddlOrigemServicoLCLimpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlOrigemServicoLCLexpo.DataSource = Session["TaskTableOrigem"];
            ddlOrigemServicoLCLexpo.DataBind();
            ddlOrigemServicoLCLexpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlOrigemServicoAereoImpo.DataSource = Session["TaskTableOrigem"];
            ddlOrigemServicoAereoImpo.DataBind();
            ddlOrigemServicoAereoImpo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlOrigemServicoAereoExpo.DataSource = Session["TaskTableOrigem"];
            ddlOrigemServicoAereoExpo.DataBind();
            ddlOrigemServicoAereoExpo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        public static string decBD(string numero)
        {
            if (string.IsNullOrEmpty(numero) == true) { return "0"; }

            return numero = numero.Replace(',', '.');
        }
    }
}