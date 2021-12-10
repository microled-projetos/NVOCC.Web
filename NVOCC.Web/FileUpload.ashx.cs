using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data;
using System.Threading;

namespace ABAINFRA.Web
{
	/// <summary>
	/// Descrição resumida de FileUpload
	/// </summary>
	public class FileUpload : IHttpHandler
	{
        public void ProcessRequest(HttpContext context)
        {
            string SQL;
            HttpPostedFile postedFile = context.Request.Files[0];
            string idprocesso = context.Request.Form["id"];
            string tipoaviso = context.Request.Form["tipoaviso"];
            string documento = context.Request.Form["documento"];
            string path = context.Server.MapPath("~/UPLOADS/");
            string filename = postedFile.FileName;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            string blmaster;
            string idblmaster;

            SQL = "SELECT IDTIPOAVISO, TPPROCESSO FROM TB_TIPOAVISO WHERE IDTIPOAVISO = '" + tipoaviso + "' ";
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
            SQL += "WHERE A.ID_BL = '" + idprocesso + "' ";
            DataTable verifica = new DataTable();
            verifica = DBS.List(SQL);
            if (listTable.Rows[0]["TPPROCESSO"].ToString() == "P")
            {

                SQL = "SELECT M.ID_BL AS BLMASTER, M.NR_BL as NRMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idprocesso + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                blmaster = listTable2.Rows[0]["BLMASTER"].ToString();
                idblmaster = listTable2.Rows[0]["BLMASTER"].ToString();
                string diretorio = path;
                try
                {
                    if (Directory.Exists(diretorio))
                    {
                        postedFile.SaveAs(Path.Combine(diretorio, filename));
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                if (File.Exists(Path.Combine(diretorio, filename)))
                {
                    SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                    SQL += "VALUES ('" + idprocesso + "',NULL,'" + documento + "','" + sqlFormattedDate + "','" + diretorio + "','" + filename + "','" + pathrobo + "\\" + filename + "') ";

                    DBS.ExecuteScalar(SQL);

                    SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                    SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + tipoaviso + "', '" + idprocesso + "',NULL, '" + idprocesso + "', NULL, NULL) ";

                    DBS.ExecuteScalar(SQL);
                }
            }
            else
            {
                SQL = "SELECT M.NR_BL as BL_MASTER, M.ID_BL AS BLMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idprocesso + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                blmaster = listTable2.Rows[0]["BL_MASTER"].ToString();
                idblmaster = listTable2.Rows[0]["BLMASTER"].ToString();
                string diretorio = path;
                try
                {
                    if (Directory.Exists(diretorio))
                    {
                        postedFile.SaveAs(Path.Combine(diretorio, filename));
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                if (File.Exists(Path.Combine(diretorio, filename)))
                {
                    SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                    SQL += "VALUES (NULL,'" + idblmaster + "','" + documento + "','" + sqlFormattedDate + "','" + diretorio + "','" + filename + "','" + pathrobo + "\\" + filename + "') ";
                    DBS.ExecuteScalar(SQL);



                    if (tipoaviso == "1")
                    {
                        SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                        SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + tipoaviso + "', NULL, '" + idblmaster + "','" + idprocesso + "',NULL,'" + parceiroD + "') ";
                        DBS.ExecuteScalar(SQL);
                    }
                    else
                    {
                        SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                        SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + tipoaviso + "', NULL, '" + idblmaster + "','" + idprocesso + "','" + parceiroRD + "',NULL) ";
                        DBS.ExecuteScalar(SQL);
                    }
                }
            }
        }
        /*public void ProcessRequest(HttpContext context)
		{
            string SQL;
            HttpPostedFile postedFile = context.Request.Files[0];
            string idprocesso = context.Request.Form["id"];
            string tipoaviso = context.Request.Form["tipoaviso"];
            string documento = context.Request.Form["documento"];
            string path = context.Server.MapPath("~/UPLOADS/");
            string filename = postedFile.FileName;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            string blmaster;
            string idblmaster;

            SQL = "SELECT IDTIPOAVISO, TPPROCESSO FROM TB_TIPOAVISO WHERE IDTIPOAVISO = '" + tipoaviso + "' ";
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
            SQL += "WHERE A.ID_BL = '" + idprocesso + "' ";
            DataTable verifica = new DataTable();
            verifica = DBS.List(SQL);
            string tipoEstufagem = verifica.Rows[0]["NM_TIPO_ESTUFAGEM"].ToString();
            string viatransporte = verifica.Rows[0]["NM_VIATRANSPORTE"].ToString();
            string previsaoEmbarque = verifica.Rows[0]["DT_PREVISAO_EMBARQUE"].ToString();
            string previsaoChegada = verifica.Rows[0]["DT_PREVISAO_CHEGADA"].ToString();
            if (listTable.Rows[0]["TPPROCESSO"].ToString() == "P")
            {

                SQL = "SELECT M.ID_BL AS BLMASTER, M.NR_BL as NRMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idprocesso + "' ";
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
            }
            else
            {
                SQL = "SELECT M.NR_BL as BL_MASTER, M.ID_BL AS BLMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idprocesso + "' ";
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
            }
            upfile(context);
        }

        public void upfile(HttpContext context)
		{
            string SQL;
            HttpPostedFile postedFile = context.Request.Files[0];
            string idprocesso = context.Request.Form["id"];
            string tipoaviso = context.Request.Form["tipoaviso"];
            string documento = context.Request.Form["documento"];
            string path = context.Server.MapPath("~/UPLOADS/");
            string filename = postedFile.FileName;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            string blmaster;
            string idblmaster;

            SQL = "SELECT IDTIPOAVISO, TPPROCESSO FROM TB_TIPOAVISO WHERE IDTIPOAVISO = '" + tipoaviso + "' ";
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
            SQL += "WHERE A.ID_BL = '" + idprocesso + "' ";
            DataTable verifica = new DataTable();
            verifica = DBS.List(SQL);
            string tipoEstufagem = verifica.Rows[0]["NM_TIPO_ESTUFAGEM"].ToString();
            string viatransporte = verifica.Rows[0]["NM_VIATRANSPORTE"].ToString();
            string previsaoEmbarque = verifica.Rows[0]["DT_PREVISAO_EMBARQUE"].ToString();
            string previsaoChegada = verifica.Rows[0]["DT_PREVISAO_CHEGADA"].ToString();
            if (listTable.Rows[0]["TPPROCESSO"].ToString() == "P")
            {

                SQL = "SELECT M.ID_BL AS BLMASTER, M.NR_BL as NRMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idprocesso + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                string anoH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(9, 2);
                string mesH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(6, 2);
                blmaster = listTable2.Rows[0]["BLMASTER"].ToString();
                idblmaster = listTable2.Rows[0]["BLMASTER"].ToString();
                string diretorio = path + "20" + anoH + "\\" + mesH + "\\" + listTable2.Rows[0]["NRHOUSE"].ToString().Replace("/", "") + "\\";
                try
                {
                    if (Directory.Exists(diretorio))
                    {
                        postedFile.SaveAs(Path.Combine(diretorio, filename));
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                if (File.Exists(Path.Combine(diretorio, filename)))
                {
                    SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                    SQL += "VALUES ('" + idprocesso + "',NULL,'" + documento + "','" + sqlFormattedDate + "','" + diretorio + "','" + filename + "','" + pathrobo + "20" + anoH + "\\" + mesH + "\\" + listTable2.Rows[0]["NRHOUSE"].ToString().Replace("/", "") + "\\" + filename + "') ";

                    DBS.ExecuteScalar(SQL);

                    SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                    SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + tipoaviso + "', '" + idprocesso + "',NULL, '" + idprocesso + "', NULL, NULL) ";

                    DBS.ExecuteScalar(SQL);
                }
            }
            else
            {
                SQL = "SELECT M.NR_BL as BL_MASTER, M.ID_BL AS BLMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idprocesso + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                string anoH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(9, 2);
                string mesH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(6, 2);
                blmaster = listTable2.Rows[0]["BL_MASTER"].ToString();
                idblmaster = listTable2.Rows[0]["BLMASTER"].ToString();
                string diretorio = path + "20" + anoH + "\\" + mesH + "\\MASTER-" + blmaster + "\\";

                try
                {
                    if (Directory.Exists(diretorio))
                    {
                        postedFile.SaveAs(Path.Combine(diretorio, filename));
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                if (File.Exists(Path.Combine(diretorio, filename)))
                {
                    SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                    SQL += "VALUES (NULL,'" + idblmaster + "','" + documento + "','" + sqlFormattedDate + "','" + diretorio + "','" + filename + "','" + pathrobo + "20" + anoH + "\\" + mesH + "\\MASTER-" + blmaster + "\\" + filename + "') ";
                    DBS.ExecuteScalar(SQL);



                    if (tipoaviso == "1")
                    {
                        SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                        SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + tipoaviso + "', NULL, '" + idblmaster + "','" + idprocesso + "',NULL,'" + parceiroD + "') ";
                        DBS.ExecuteScalar(SQL);
                    }
                    else
                    {
                        SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                        SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + tipoaviso + "', NULL, '" + idblmaster + "','" + idprocesso + "','" + parceiroRD + "',NULL) ";
                        DBS.ExecuteScalar(SQL);
                    }
                }
            }
        }*/

        public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}