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
    public partial class ModuloOperacional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            listarVia();
            listarEtapa();
            listarServico();
            listarStatus();
            listarWeek();
            listarCliente();
            listarNavio();
            listarPorto();
            listarTipoEstufagem();
            listarTipoFrete();
            listarTransportador();
            listarAgente();
        }

        private void listarVia()
        {
            ddlVia.DataBind();
            ddlVia.Items.Insert(0, new ListItem("Todas",""));
            ddlVia.Items.Insert(1, new ListItem("Marítima", "1"));
            ddlVia.Items.Insert(2, new ListItem("Aérea", "4"));
        }
        private void listarEtapa()
        {
            ddlEtapa.DataBind();
            ddlEtapa.Items.Insert(0, new ListItem("Todas",""));
            ddlEtapa.Items.Insert(1, new ListItem("Pré-Embarque", "1"));
            ddlEtapa.Items.Insert(2, new ListItem("Pós-Embarque", "2"));
            ddlEtapa.Items.Insert(3, new ListItem("Pós-Chegada", "3"));

        }
        private void listarServico()
        {
            ddlServico.DataBind();
            ddlServico.Items.Insert(0, new ListItem("Todos",""));
            ddlServico.Items.Insert(1, new ListItem("Importação", "1"));
            ddlServico.Items.Insert(2, new ListItem("Exportação", "2"));
        }
        private void listarStatus()
        {
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Todos",""));
            ddlStatus.Items.Insert(1, new ListItem("Ativos", "1"));
            ddlStatus.Items.Insert(2, new ListItem("Cancelados", "2"));
            ddlStatus.Items.Insert(2, new ListItem("Finalizados", "3"));
        }
        private void listarWeek()
        {
            string SQL;
            SQL = "SELECT ID_WEEK, NM_WEEK FROM TB_WEEK";
            DataTable week = new DataTable();
            week = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = week;
            ddlWeek.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlWeek.DataBind();
            ddlWeek.Items.Insert(0, new ListItem("Selecione", ""));
            ddlWeekFilter.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlWeekFilter.DataBind();
            ddlWeekFilter.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void listarCliente()
        {
            string SQL;
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO ORDER BY NM_RAZAO";
            DataTable cliente = new DataTable();
            cliente = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = cliente;
            ddlClienteFilter.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlClienteFilter.DataBind();
            ddlClienteFilter.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void listarPorto()
        {
            string SQL;
            SQL = "SELECT NM_PORTO, ID_PORTO FROM TB_PORTO ORDER BY NM_PORTO";
            DataTable porto = new DataTable();
            porto = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = porto;
            ddlPortoDestinoFilter.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlPortoDestinoFilter.DataBind();
            ddlPortoDestinoFilter.Items.Insert(0, new ListItem("Selecione", ""));
            ddlPortoOrigemFilter.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlPortoOrigemFilter.DataBind();
            ddlPortoOrigemFilter.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void listarTipoFrete()
        {
            string SQL;
            SQL = "SELECT NM_TIPO_PAGAMENTO, ID_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO";
            DataTable frete = new DataTable();
            frete = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = frete;
            ddlTipoFrete.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlTipoFrete.DataBind();
            ddlTipoFrete.Items.Insert(0, new ListItem("Selecione", ""));
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

        private void listarAgente()
        {
            string SQL;
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_AGENTE = 1 ORDER BY NM_RAZAO";
            DataTable agente = new DataTable();
            agente = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = agente;
            ddlAgenteFilter.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlAgenteFilter.DataBind();
            ddlAgenteFilter.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void listarTransportador()
        {
            string SQL;
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_TRANSPORTADOR = 1 ORDER BY NM_RAZAO";
            DataTable transportador = new DataTable();
            transportador = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = transportador;
            ddlTransportadorFilter.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlTransportadorFilter.DataBind();
            ddlTransportadorFilter.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void listarNavio()
        {
            string SQL;
            SQL = "SELECT NM_NAVIO, ID_NAVIO FROM TB_NAVIO ORDER BY NM_NAVIO";
            DataTable navio = new DataTable();
            navio = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = navio;
            ddlNavioFilter.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlNavioFilter.DataBind();
            ddlNavioFilter.Items.Insert(0, new ListItem("Selecione", ""));
        }
	}
}