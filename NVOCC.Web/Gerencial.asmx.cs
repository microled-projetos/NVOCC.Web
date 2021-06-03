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
    }
}
