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
            listarImportador();
            listarAgenteInternacional();
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
            ddlClienteFinal.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlClienteFinal.DataBind();
            ddlClienteFinal.Items.Insert(0, new ListItem("Selecione", ""));
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

        private void listarImportador()
        {
            string SQL;
            SQL = "SELECT NM_RAZAO, ID_PARCEIRO FROM TB_PARCEIRO WHERE FL_IMPORTADOR = 1 ORDER BY NM_RAZAO";
            DataTable agente = new DataTable();
            agente = DBS.List(SQL);
            Session["TaskTableMoedaDemurrage"] = agente;
            ddlImportadorFilter.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlImportadorFilter.DataBind();
            ddlImportadorFilter.Items.Insert(0, new ListItem("Selecione", ""));
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
            ddlNavioTransbordoFilter.DataSource = Session["TaskTableMoedaDemurrage"];
            ddlNavioTransbordoFilter.DataBind();
            ddlNavioTransbordoFilter.Items.Insert(0, new ListItem("Selecione", ""));
        }

		protected void uploadFile(object sender, EventArgs e)
		{
            /*
            string SQL;
            string idprocess = idprocesso.Value;
            string idtipoaviso = tipoaviso.Value;
            string fileName = dadoUpload.FileName;
            string documento = iddocumento.Value;
            
            if(documento == "0")
			{
                msgUploadError.Text = "Erro ao realizar Upload. Selecione um tipo de documento";
                return;
			}

            if(idtipoaviso == "0")
			{
                msgUploadError.Text = "Erro ao realizar Upload. Selecione um tipo de aviso";
                return;
            }

            if(fileName == "")
			{
                msgUploadError.Text = "Erro ao realizar Upload. Selecione um arquivo";
                return;
            }

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            string path = HttpContext.Current.Server.MapPath("~/UPLOADS/");
            string blmaster;
            string idblmaster;

            SQL = "SELECT IDTIPOAVISO, TPPROCESSO FROM TB_TIPOAVISO WHERE IDTIPOAVISO = '" + idtipoaviso + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string tipoprocesso = listTable.Rows[0]["TPPROCESSO"].ToString();
            string tipoavisos = listTable.Rows[0]["IDTIPOAVISO"].ToString();

            SQL = "SELECT PATHDOCUMENTOSROBO FROM TB_AVISOPARAM ";
            DataTable robo = new DataTable();
            robo = DBS.List(SQL);
            string pathrobo = robo.Rows[0]["PATHDOCUMENTOSROBO"].ToString();

            SQL = "SELECT ID_PARCEIRO_DESCONSOLIDACAO, ID_PARCEIRO_REDESTINACAO_CONSOLIDADA FROM TB_PARAMETROS ";
            DataTable idparceiroc = new DataTable();
            idparceiroc = DBS.List(SQL);
            string parceiroD = idparceiroc.Rows[0]["ID_PARCEIRO_DESCONSOLIDACAO"].ToString();
            string parceiroRD = idparceiroc.Rows[0]["ID_PARCEIRO_REDESTINACAO_CONSOLIDADA"].ToString();

            SQL = "SELECT B.NM_TIPO_ESTUFAGEM, D.NM_VIATRANSPORTE, ";
            SQL += "A.DT_PREVISAO_EMBARQUE, A.DT_PREVISAO_CHEGADA ";
            SQL += "from TB_BL A ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM B ON A.ID_TIPO_ESTUFAGEM = B.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += "LEFT JOIN TB_VIATRANSPORTE D ON C.ID_VIATRANSPORTE = D.ID_VIATRANSPORTE ";
            SQL += "WHERE A.ID_BL = '" + idprocess + "' ";
            DataTable verifica = new DataTable();
            verifica = DBS.List(SQL);
            string tipoEstufagem = verifica.Rows[0]["NM_TIPO_ESTUFAGEM"].ToString();
            string viatransporte = verifica.Rows[0]["NM_VIATRANSPORTE"].ToString();
            string previsaoEmbarque = verifica.Rows[0]["DT_PREVISAO_EMBARQUE"].ToString();
            string previsaoChegada = verifica.Rows[0]["DT_PREVISAO_CHEGADA"].ToString();

            try
            {
                if (listTable.Rows[0]["TPPROCESSO"].ToString() == "P")
                {

                    SQL = "SELECT M.ID_BL AS BLMASTER, M.NR_BL as NRMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idprocess + "' ";
                    DataTable listTable2 = new DataTable();
                    listTable2 = DBS.List(SQL);
                    string anoH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(9, 2);
                    string mesH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(6, 2);
                    blmaster = listTable2.Rows[0]["BLMASTER"].ToString();
                    idblmaster = listTable2.Rows[0]["BLMASTER"].ToString();
                    string diretorio = path + "20" + anoH + "\\" + mesH + "\\" + listTable2.Rows[0]["NRHOUSE"].ToString().Replace("/", "") + "\\";

                    if (Directory.Exists(diretorio) == false)
                    {
                        DirectoryInfo di = Directory.CreateDirectory(diretorio);
                    }

                    SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                    SQL += "VALUES ('" + idprocess + "',NULL,'" + documento + "','" + sqlFormattedDate + "','" + diretorio + "','" + fileName + "','" + pathrobo + "') ";

                    DBS.ExecuteScalar(SQL);

                    SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                    SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + idtipoaviso + "', '" + idprocess + "',NULL, '"+idprocess+"', NULL, NULL) ";

                    DBS.ExecuteScalar(SQL);

                    dadoUpload.SaveAs(Path.Combine(diretorio, fileName));
                }
                else
                {
                    SQL = "SELECT M.NR_BL as BL_MASTER, M.ID_BL AS BLMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idprocess + "' ";
                    DataTable listTable2 = new DataTable();
                    listTable2 = DBS.List(SQL);
                    string anoH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(9, 2);
                    string mesH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(6, 2);
                    blmaster = listTable2.Rows[0]["BL_MASTER"].ToString();
                    idblmaster = listTable2.Rows[0]["BLMASTER"].ToString();
                    string diretorio = path + "20" + anoH + "\\" + mesH + "\\MASTER-" + blmaster + "\\";

                    if (Directory.Exists(diretorio) == false)
                    {
                        DirectoryInfo di = Directory.CreateDirectory(diretorio);
                    }

                    SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                    SQL += "VALUES (NULL,'" + idblmaster + "','" + documento + "','" + sqlFormattedDate + "','" + diretorio + "','" + fileName + "','" + pathrobo + "') ";
                    DBS.ExecuteScalar(SQL);

                    if (idtipoaviso == "1")
					{
                        SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                        SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + idtipoaviso + "', NULL, '" + idblmaster + "','" + idprocess + "','" + parceiroRD + "',NULL) ";
                        DBS.ExecuteScalar(SQL);
                    }
					else
					{
                        SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                        SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + idtipoaviso + "', NULL, '" + idblmaster + "','" + idprocess + "',NULL,'" + parceiroD + "') ";
                        DBS.ExecuteScalar(SQL);
                    }
                   
                    dadoUpload.SaveAs(Path.Combine(diretorio, fileName));                    
                }
                msgUploadSuccess.Text = "Upload realizado com sucesso";
            }
			catch
			{
                msgUploadError.Text = "Erro ao realizar Upload";
            }
            */
        }
	}
}