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
using Newtonsoft.Json;
using ABAINFRA.Web.Classes;


namespace ABAINFRA.Web
{
    /// <summary>
    /// Descrição resumida de Gerencial
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    [System.Web.Script.Services.ScriptService]
    public class Gerencial : System.Web.Services.WebService
    {

        [WebMethod]
        public string CarregarVendedores()
        {
            string SQL;
            SQL = "select NM_RAZAO, ID_PARCEIRO from tb_parceiro where fl_vendedor = 1";
            DataTable vendedor = new DataTable();

            vendedor = DBS.List(SQL);

            return JsonConvert.SerializeObject(vendedor);
        }

        [WebMethod]
        public string CarregarEstatistica(string anoI, string anoF, string mesI, string mesF, int vendedor)
        {
            string periodoi;
            string periodof;

            string SQL;
            if (anoI != "")
            {
                if (mesI != "")
                {
                    if (anoF != "")
                    {
                        if (mesF != "")
                        {
                            if (vendedor != 0) {;
                                periodoi = anoI + mesI;
                                periodof = anoF + mesF;
                                SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                                SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                                SQL += "COUNT (D.TEU) AS TEUS, ";
                                SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                                SQL += "FROM TB_BL A ";
                                SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                                SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                                SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                                SQL += "WHERE A.GRAU IN('C') ";
                                SQL += "and A.DT_CANCELAMENTO IS NULL ";
                                SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '"+periodoi+"' and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) <= '"+periodof+"' ";
                                SQL += "AND A.ID_PARCEIRO_VENDEDOR = '" + vendedor + "' ";
                                SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                                DataTable total = new DataTable();

                                total = DBS.List(SQL);

                                return JsonConvert.SerializeObject(total);
                            }
                            else
                            {
                                periodoi = anoI + mesI;
                                periodof = anoF + mesF;
                                SQL = "SELECT MES+'/'+ANO PERIODO, ";
                                SQL += "COUNT(DISTINCT(NR_PROCESSO)) AS PROCESSO, ";
                                SQL += "isnull(sum(isnull(qtde20, 0) + isnull(qtde40, 0)), 0) as QtdCntr, ";
                                SQL += "isnull(sum(isnull(qtde20, 0) * 1 + isnull(qtde40, 0) * 2), 0) as teus ";
                                SQL += "FROM VW_PROCESSO_CONTAINER_FCL T ";
                                SQL += "JOIN VW_PROCESSO_TEUS TEUS ON T.ID_BL = TEUS.ID_BL ";
                                SQL += "JOIN VW_PROCESSO_CONTAINER_TEUS CT ON T.ID_BL = CT.ID_BL ";
                                SQL += "GROUP BY MES, ANO ";
                                SQL += "ORDER BY ANO, MES ";
                                DataTable total = new DataTable();

                                total = DBS.List(SQL);

                                return JsonConvert.SerializeObject(total);
                            }
                        }
                        else
                        {
                            if (vendedor != 0)
                            {
                                periodoi = anoI + mesI;
                                periodof = anoF + "01";
                                SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                                SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                                SQL += "COUNT (D.TEU) AS TEUS, ";
                                SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                                SQL += "FROM TB_BL A ";
                                SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                                SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                                SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                                SQL += "WHERE A.GRAU IN('C') ";
                                SQL += "and A.DT_CANCELAMENTO IS NULL ";
                                SQL += "AND RIGHT(A.NR_PROCESSO,2) BETWEEN '" + anoI + "' AND '" + anoF + "' ";
                                SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) <= '" + periodof + "' ";
                                SQL += "AND A.ID_PARCEIRO_VENDEDOR = '" + vendedor + "' ";
                                SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                                DataTable total = new DataTable();

                                total = DBS.List(SQL);

                                return JsonConvert.SerializeObject(total);
                            }
                            else
                            {
                                periodoi = anoI + mesI;
                                periodof = anoF + "12";
                                SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                                SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                                SQL += "COUNT (D.TEU) AS TEUS, ";
                                SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                                SQL += "FROM TB_BL A ";
                                SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                                SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                                SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                                SQL += "WHERE A.GRAU IN('C') ";
                                SQL += "and A.DT_CANCELAMENTO IS NULL ";
                                SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) <= '" + periodof + "' ";
                                SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                                DataTable total = new DataTable();

                                total = DBS.List(SQL);

                                return JsonConvert.SerializeObject(total);
                            }
                        }
                    }
                    else
                    {
                        if (vendedor != 0)
                        {
                            periodoi = anoI + mesI;
                            SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                            SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                            SQL += "COUNT (D.TEU) AS TEUS, ";
                            SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                            SQL += "FROM TB_BL A ";
                            SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                            SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                            SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                            SQL += "WHERE A.GRAU IN('C') ";
                            SQL += "and A.DT_CANCELAMENTO IS NULL ";
                            SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' ";
                            SQL += "AND A.ID_PARCEIRO_VENDEDOR = '" + vendedor + "' ";
                            SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";
                            DataTable total = new DataTable();

                            total = DBS.List(SQL);

                            return JsonConvert.SerializeObject(total);
                        }
                        else
                        {
                            periodoi = anoI + mesI;
                            SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                            SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                            SQL += "COUNT (D.TEU) AS TEUS, ";
                            SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                            SQL += "FROM TB_BL A ";
                            SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                            SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                            SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                            SQL += "WHERE A.GRAU IN('C') ";
                            SQL += "and A.DT_CANCELAMENTO IS NULL ";
                            SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' ";
                            SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                            DataTable total = new DataTable();

                            total = DBS.List(SQL);

                            return JsonConvert.SerializeObject(total);
                        }
                    }
                }
                else
                {
                    if (anoF != "")
                    {
                        if (mesF != "")
                        {
                            if (vendedor != 0)
                            {
                                ;
                                periodoi = anoI + "01";
                                periodof = anoF + mesF;
                                SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                                SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                                SQL += "COUNT (D.TEU) AS TEUS, ";
                                SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                                SQL += "FROM TB_BL A ";
                                SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                                SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                                SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                                SQL += "WHERE A.GRAU IN('C') ";
                                SQL += "and A.DT_CANCELAMENTO IS NULL ";
                                SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) <= '" + periodof + "' ";
                                SQL += "AND A.ID_PARCEIRO_VENDEDOR = '" + vendedor + "' ";
                                SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                                DataTable total = new DataTable();

                                total = DBS.List(SQL);

                                return JsonConvert.SerializeObject(total);
                            }
                            else
                            {
                                periodoi = anoI + "01";
                                periodof = anoF + mesF;
                                SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                                SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                                SQL += "COUNT (D.TEU) AS TEUS, ";
                                SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                                SQL += "FROM TB_BL A ";
                                SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                                SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                                SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                                SQL += "WHERE A.GRAU IN('C') ";
                                SQL += "and A.DT_CANCELAMENTO IS NULL ";
                                SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) <= '" + periodof + "' ";
                                SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                                DataTable total = new DataTable();

                                total = DBS.List(SQL);

                                return JsonConvert.SerializeObject(total);
                            }
                        }
                        else
                        {
                            if (vendedor != 0)
                            {
                                periodoi = anoI + "01";
                                periodof = anoF + "01";
                                SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                                SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                                SQL += "COUNT (D.TEU) AS TEUS, ";
                                SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                                SQL += "FROM TB_BL A ";
                                SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                                SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                                SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                                SQL += "WHERE A.GRAU IN('C') ";
                                SQL += "and A.DT_CANCELAMENTO IS NULL ";
                                SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) <= '" + periodof + "' ";
                                SQL += "AND A.ID_PARCEIRO_VENDEDOR = '" + vendedor + "' ";
                                SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                                DataTable total = new DataTable();

                                total = DBS.List(SQL);

                                return JsonConvert.SerializeObject(total);
                            }
                            else
                            {
                                periodoi = anoI + "01";
                                periodof = anoF + "01";
                                SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                                SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                                SQL += "COUNT (D.TEU) AS TEUS, ";
                                SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                                SQL += "FROM TB_BL A ";
                                SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                                SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                                SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                                SQL += "WHERE A.GRAU IN('C') ";
                                SQL += "and A.DT_CANCELAMENTO IS NULL ";
                                SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) <= '" + periodof + "' ";
                                SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                                DataTable total = new DataTable();

                                total = DBS.List(SQL);

                                return JsonConvert.SerializeObject(total);
                            }
                        }
                    }
                    else
                    {
                        if (vendedor != 0)
                        {
                            periodoi = anoI + "01";
                            SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                            SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                            SQL += "COUNT (D.TEU) AS TEUS, ";
                            SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                            SQL += "FROM TB_BL A ";
                            SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                            SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                            SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                            SQL += "WHERE A.GRAU IN('C') ";
                            SQL += "and A.DT_CANCELAMENTO IS NULL ";
                            SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' ";
                            SQL += "AND A.ID_PARCEIRO_VENDEDOR = '" + vendedor + "' ";
                            SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                            DataTable total = new DataTable();

                            total = DBS.List(SQL);

                            return JsonConvert.SerializeObject(total);
                        }
                        else
                        {
                            periodoi = anoI + "01";
                            SQL = "SELECT COUNT(A.NR_PROCESSO) AS PROCESSO, ";
                            SQL += "COUNT(C.NR_CNTR) AS CONTAINER, ";
                            SQL += "COUNT (D.TEU) AS TEUS, ";
                            SQL += "SUBSTRING(A.NR_PROCESSO, 7, 2) AS MES ";
                            SQL += "FROM TB_BL A ";
                            SQL += "INNER JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
                            SQL += "INNER JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
                            SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
                            SQL += "WHERE A.GRAU IN('C') ";
                            SQL += "and A.DT_CANCELAMENTO IS NULL ";
                            SQL += "and RIGHT(A.NR_PROCESSO,2)+SUBSTRING(A.NR_PROCESSO, 7, 2) >= '" + periodoi + "' ";
                            SQL += "GROUP BY SUBSTRING(A.NR_PROCESSO, 7, 2)";

                            DataTable total = new DataTable();

                            total = DBS.List(SQL);

                            return JsonConvert.SerializeObject(total);
                        }
                    }
                }
            }
            else
            {
                return "1";
            }
        }

        [WebMethod]
        public string listarProcessos()
        {
            string SQL;
            SQL = "SELECT DISTINCT isnull(NR_PROCESSO,'') AS PROCESSO, isnull(LEFT(CLIENTE.NM_RAZAO,10),'') AS CLIENTE, ";
            SQL += "isnull(LEFT(TRANSPORTADOR.NM_RAZAO,10),'') AS CARRIER, isnull(TP.NM_TIPO_ESTUFAGEM,'') AS TIPOESTUFAGEM, ";
            SQL += "isnull(CNTR_TEUS.QTDE20,0) as QTDE20, isnull(CNTR_TEUS.QTDE40,0) AS QTDE40, ";
            SQL += "isnull(SUBSTRING(TPC.NM_TIPO_CONTAINER,3,6),'') AS TIPO,isnull(P1.NM_PORTO,'') AS ORIGEM, isnull(P2.NM_PORTO,'') AS DESTINO, ";
            SQL += "isnull(FORMAT(A.DT_ABERTURA,'dd/MM/yyyy'),'') AS DTABERTURA, isnull(FORMAT(A.DT_PREVISAO_EMBARQUE ,'dd/MM/yyyy'),'') AS ETD, isnull(FORMAT(A.DT_EMBARQUE ,'dd/MM/yyyy'),'') AS ETA, ";
            SQL += "isnull(FORMAT(A.DT_CHEGADA,'dd/MM/yyyy'),'') AS CHEGADA, isnull(FORMAT(A.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DATARECEBIMENTO, ";
            SQL += "isnull(VENDEDOR.NM_RAZAO,'') AS VENDEDOR, isnull(LEFT(AGENTEI.NM_RAZAO,10),'') AS AGENTECARGA, isnull(LEFT(AGENTED.NM_RAZAO,10),'') AS NMCOMISSARIA, ";
            SQL += "isnull(A.ID_WEEK,'') AS WEEK, A.ID_SERVICO, A.ID_INCOTERM ";
            SQL += "FROM TB_BL A ";
            SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PORTO P1 ON A.ID_PORTO_ORIGEM = P1.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO P2 ON A.ID_PORTO_DESTINO = P2.ID_PORTO ";
            SQL += "LEFT JOIN TB_PARCEIRO CLIENTE ON A.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO TRANSPORTADOR ON A.ID_PARCEIRO_TRANSPORTADOR = TRANSPORTADOR.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO VENDEDOR ON A.ID_PARCEIRO_VENDEDOR = VENDEDOR.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO AGENTEI ON A.ID_PARCEIRO_AGENTE_INTERNACIONAL = AGENTEI.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO AGENTED ON A.ID_PARCEIRO_COMISSARIA = AGENTED.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM TP ON A.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER TPC ON C.ID_TIPO_CNTR = TPC.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_WEEK TW ON A.ID_WEEK = TW.ID_WEEK ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE TCD ON C.ID_CNTR_BL = TCD.ID_CNTR_BL ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_TEUS CNTR_TEUS ON A.ID_BL = CNTR_TEUS.ID_BL ";
            SQL += "WHERE A.GRAU = 'C' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }
    }
}
