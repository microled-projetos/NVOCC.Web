﻿using Antlr.Runtime;
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
        public string CarregaFiltro(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo)
        {
            string SQL;
            SQL = "and B.DT_CANCELAMENTO IS NULL ";
            if (vendedor != 0)
            {
                SQL += " AND A.ID_PARCEIRO_VENDEDOR = " + vendedor;
            }

            string periodoi;
            string periodof;
            string anof;

            if (anoF != "")
            {
                anof = anoF;
            }
            else
            {
                anof = anoI;
            }

            if (mesI != "")
            {
                periodoi = anoI + mesI;
            }
            else
            {
                periodoi = anoI + "01";
            }

            if (mesF != "")
            {
                periodof = anof + mesF;
            }
            else
            {
                periodof = anof + "12";
            }

            
            SQL += " AND A.ANO+A.MES >=" + periodoi + " AND A.ANO+A.MES <=" + periodof;


            if (tipo == "1")
            {
                SQL += " AND A.NM_TIPO_ESTUFAGEM ='FCL' ";
            }
            else if (tipo == "2")
            {
                SQL += " AND A.NM_TIPO_ESTUFAGEM ='LCL' ";
            }
            else if (tipo == "3")
            {
                SQL += " AND A.NM_TIPO_ESTUFAGEM IN('FCL','LCL') ";
            }
            else if (tipo == "4")
            {
                SQL += " AND UPPER(VIATRANSPORTE)='AÉREA' ";
            }
            return SQL;
        }

        [WebMethod]
        public string CarregarEstatistica(string anoI, string anoF, string mesI, string mesF, int vendedor,string tipo)
        {
            string SQL;

            SQL = "SELECT  ISNULL(A.MES,'')+'/'+ISNULL(A.ANO,'') as PERIODO, ";
            SQL += "ISNULL(COUNT(C.NR_CNTR),0) AS CONTAINER, ";
            SQL += "ISNULL(COUNT (D.TEU),0) AS TEUS, ";
            SQL += "COUNT(DISTINCT(A.NR_PROCESSO)) AS PROCESSO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "INNER JOIN TB_CNTR_BL C ON A.ID_CNTR_BL = C.ID_CNTR_BL  ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "WHERE B.GRAU IN('C') ";
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesI, vendedor, tipo) + " ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregarProcessos(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo)
        {
            string SQL;

            SQL = "SELECT  A.MES+'/'+A.ANO as PERIODO, ";
            SQL += "sum(case when substring(A.NR_PROCESSO , 1, 1) = 'M' THEN 1 else 0 end) IMP, ";
            SQL += "sum(case when substring(A.NR_PROCESSO, 1, 1) = 'A' THEN 1 else 0 end) AR, ";
            SQL += "sum(case when substring(A.NR_PROCESSO, 1, 1) = 'E' THEN 1 else 0 end) EXP ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "INNER JOIN TB_CNTR_BL C ON A.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "WHERE B.GRAU IN('C') ";
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesI, vendedor, tipo) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, ";
            SQL += "A.ANO ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }


        [WebMethod]
        public string CarregarQtdCntr(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo)
        {
            string SQL;

            SQL = "SELECT  A.MES+'/'+A.ANO as PERIODO, ";
            SQL += "sum(case when substring(A.NR_PROCESSO, 1, 1) = 'M' THEN isnull(E.QTDE20, 0) + isnull(E.QTDE40, 0) else 0 end) IMP, ";
            SQL += "sum(case when substring(A.NR_PROCESSO, 1, 1) = 'E' THEN isnull(E.QTDE20, 0)  + isnull(E.QTDE40, 0) else 0 end) EXP ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "INNER JOIN TB_CNTR_BL C ON A.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_TEUS E ON A.ID_BL = E.ID_BL ";
            SQL += "WHERE B.GRAU IN('C') ";
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesI, vendedor, tipo) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregarTeus(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo)
        {
            string SQL;

            SQL = "SELECT  A.MES+'/'+A.ANO as PERIODO, ";
            SQL += "sum(case when substring(A.NR_PROCESSO, 1, 1) = 'M' THEN isnull(E.QTDE20, 0) * 1 + isnull(E.QTDE40, 0) * 2 else 0 end) IMP, ";
            SQL += "sum(case when substring(A.NR_PROCESSO, 1, 1) = 'E' THEN isnull(E.QTDE20, 0) * 1 + isnull(E.QTDE40, 0) * 2 else 0 end) EXP ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "INNER JOIN TB_CNTR_BL C ON A.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_TEUS E ON A.ID_BL = E.ID_BL ";
            SQL += "WHERE B.GRAU IN('C') ";
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesI, vendedor, tipo) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }
        /*[WebMethod]
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
                            if (vendedor != 0) {
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
        }*/

        [WebMethod]
        public string listarProcessos(string nmfilter, string txtfilter, string estufagem, string via, string servico)
        {
            switch (nmfilter)
            {
                case "1":
                    nmfilter = "AND A.NR_PROCESSO LIKE '" + txtfilter + "%' ";
                    break;
                case "2":
                    nmfilter = "AND CLIENTE.NM_RAZAO LIKE '" + txtfilter + "%' ";
                    break;
                default:
                    nmfilter = "";
                    break;
            }

            switch (estufagem)
            {
                case "1":
                    estufagem = "AND TP.NM_TIPO_ESTUFAGEM = 'FCL' ";
                    break;
                case "2":
                    estufagem = "AND TP.NM_TIPO_ESTUFAGEM = 'LCL' ";
                    break;
                default:
                    estufagem = "";
                    break;
            }

            switch (via)
            {
                case "1":
                    via = "AND V.ID_VIATRANSPORTE = '4' ";
                    break;
                case "2":
                    via = "AND V.ID_VIATRANSPORTE = '1' ";
                    break;
                default:
                    via = "";
                    break;
            }

            switch (servico)
            {
                case "1":
                    servico = "AND SUBSTRING(A.NR_PROCESSO,1,1) IN ('M','A') ";
                    break;
                case "2":
                    servico = "AND SUBSTRING(A.NR_PROCESSO,1,1) IN ('E') ";
                    break;
                default:
                    servico = "";
                    break;
            }

            string SQL;
            SQL = "SELECT DISTINCT isnull(NR_PROCESSO,'') AS PROCESSO, isnull(CLIENTE.NM_RAZAO,'') AS CLIENTE, ";
            SQL += "isnull(TRANSPORTADOR.NM_RAZAO,'') AS CARRIER, isnull(TP.NM_TIPO_ESTUFAGEM,'') AS TIPOESTUFAGEM, ";
            SQL += "isnull(CNTR_TEUS.QTDE20,0) as QTDE20, isnull(CNTR_TEUS.QTDE40,0) AS QTDE40, ";
            SQL += "isnull(SUBSTRING(TPC.NM_TIPO_CONTAINER,3,6),'') AS TIPO,isnull(P1.NM_PORTO,'') AS ORIGEM, isnull(P2.NM_PORTO,'') AS DESTINO, ";
            SQL += "isnull(FORMAT(A.DT_ABERTURA,'dd/MM/yyyy'),'') AS DTABERTURA, isnull(FORMAT(A.DT_PREVISAO_EMBARQUE ,'dd/MM/yyyy'),'') AS ETD, isnull(FORMAT(A.DT_EMBARQUE ,'dd/MM/yyyy'),'') AS ETA, ";
            SQL += "isnull(FORMAT(A.DT_CHEGADA,'dd/MM/yyyy'),'') AS CHEGADA, isnull(FORMAT(A.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DATARECEBIMENTO, ";
            SQL += "isnull(VENDEDOR.NM_RAZAO,'') AS VENDEDOR, isnull(AGENTEI.NM_RAZAO,'') AS AGENTECARGA, isnull(AGENTED.NM_RAZAO,'') AS NMCOMISSARIA, ";
            SQL += "isnull(CONVERT(VARCHAR,A.ID_WEEK),'') AS WEEK, A.ID_SERVICO, A.ID_INCOTERM, ";
            SQL += "ISNULL(FORMAT(PR.DT_LIQUIDACAO, 'dd/MM/yyyy'), '') AS DT_RECEBIDO, ";
            SQL += "ISNULL(FORMAT(PP.DT_LIQUIDACAO, 'dd/MM/yyyy'), '') AS DT_PAGO, ";
            SQL += "ISNULL(FORMAT(PR.VL_LANCAMENTO, 'c', 'pt-br'), '') AS VL_RECEBIDO, ";
            SQL += "ISNULL(FORMAT(PP.VL_LANCAMENTO, 'c', 'pt-br'), '') AS VL_PAGO ";
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
            SQL += "LEFT JOIN VW_PROCESSO_RECEBIDO_TOTAL PR ON A.ID_BL = PR.ID_BL ";
            SQL += "LEFT JOIN VW_PROCESSO_PAGO_TOTAL PP ON A.ID_BL = PP.ID_BL ";
            SQL += "LEFT JOIN TB_SERVICO S ON A.ID_SERVICO = S.ID_SERVICO ";
            SQL += "LEFT JOIN TB_VIATRANSPORTE V ON S.ID_VIATRANSPORTE = V.ID_VIATRANSPORTE ";
            SQL += "WHERE A.GRAU = 'C' ";
            SQL += ""+nmfilter+" ";
            SQL += ""+estufagem+" ";
            SQL += ""+via+" ";
            SQL += ""+servico+" ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }


        [WebMethod]
        public string listarProcessosMaster()
        {
            string SQL;
            SQL = "SELECT M.NR_BL, ISNULL(LEFT(MIN(B1.NM_RAZAO),13),'') AS TRANSPORTADOR, ";
            SQL += "ISNULL((MIN(D1.NM_TIPO_ESTUFAGEM) +' - '+ MIN(D2.NM_TIPO_ESTUFAGEM) ),'') AS NMTPESTUFAGEM, "; ;
            SQL += "ISNULL(MIN(TEUS.QTDE20),'') AS QTDE20, ISNULL(MIN(TEUS.QTDE40),'') as QTDE40, ";
            SQL += "ISNULL(MIN(P1.NM_PORTO),'') AS ORIGEM, ISNULL(MIN(P2.NM_PORTO),'') AS DESTINO, ";
            SQL += "ISNULL(FORMAT(MIN(M.DT_EMBARQUE),'dd/MM/yyyy'),'') AS DTEMBARQUE, ";
            SQL += "ISNULL(FORMAT(MIN(M.DT_PREVISAO_CHEGADA),'dd/MM/yyyy'),'') AS DTPREVISAOCHEGADA, ";
            SQL += "ISNULL(FORMAT(MIN(M.DT_CHEGADA),'dd/MM/yyyy'),'') AS DTCHEGADA, ";
            SQL += "ISNULL(FORMAT(MIN(PP.DT_LIQUIDACAO),'dd/MM/yyyy'),'') AS DT_PAGAMENTO, ";
            SQL += "ISNULL(FORMAT(MIN(PR.DT_LIQUIDACAO),'dd/MM/yyyy'),'') AS DT_RECEBIMENTO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,MIN(FORMAT(PP.VL_LANCAMENTO,'C','PT-BR'))),'') AS VL_PAGO, ";
            SQL += "ISNULL(CONVERT(VARCHAR, MIN(FORMAT(PR.VL_LANCAMENTO,'C','PT-BR'))),'') AS VL_RECEBIDO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,MIN(PP.VL_CAMBIO)),'') AS VL_CAMBIO_PGTO, ";
            SQL += "ISNULL(CONVERT(VARCHAR, MIN(PR.VL_CAMBIO)), '') AS VL_CAMBIO_RECEBIDO ";
            SQL += "FROM TB_BL M  ";
            SQL += "LEFT JOIN TB_PARCEIRO B1 ON M.ID_PARCEIRO_TRANSPORTADOR = B1.ID_PARCEIRO  ";
            SQL += "LEFT JOIN TB_BL C ON M.ID_BL = C.ID_BL_MASTER ";
            SQL += "LEFT JOIN TB_AMR_CNTR_BL A ON C.ID_BL = A.ID_BL ";
            SQL += "LEFT JOIN TB_CNTR_BL E ON A.ID_CNTR_BL = E.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM D1 ON M.ID_TIPO_ESTUFAGEM = D1.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM D2 ON C.ID_TIPO_ESTUFAGEM = D2.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_TEUS TEUS ON C.ID_BL = TEUS.ID_BL ";
            SQL += "LEFT JOIN TB_PORTO P1 ON M.ID_PORTO_ORIGEM = P1.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO P2 ON M.ID_PORTO_DESTINO = P2.ID_PORTO ";
            SQL += "LEFT JOIN VW_PROCESSO_PAGO_TOTAL PP ON M.ID_BL = PP.ID_BL_MASTER ";
            SQL += "LEFT JOIN VW_PROCESSO_RECEBIDO_TOTAL PR ON M.ID_BL = PR.ID_BL_MASTER ";
            SQL += "WHERE M.NR_BL IS NOT NULL ";
            SQL += "GROUP BY M.ID_BL_MASTER, M.NR_BL ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string listarProcessosOperacionalFilter(FiltroModuloOperacional dados)
        {
            string SQL;
            SQL = "WHERE SUBSTRING(C.NR_PROCESSO,10,2)>= '18' ";
            if (dados.VIA != "")
            {
                SQL += "AND V.ID_VIATRANSPORTE = "+dados.VIA+" ";
            }
            switch (dados.ETAPA)
            {
                case "1":
                    SQL += "AND C.DT_EMBARQUE IS NULL ";
                    break;
                case "2":
                    SQL += "AND C.DT_EMBARQUE IS NOT NULL AND C.DT_CHEGADA IS NULL ";
                    break;
                case "3":
                    SQL += "AND C.DT_CHEGADA IS NOT NULL ";
                    break;
                default:
                    SQL += "";
                    break;
            }

            switch (dados.SERVICO)
            {
                case "1":
                    SQL += "AND SUBSTRING(C.NR_PROCESSO,1,1) IN ('M','A') ";
                    break;
                case "2":
                    SQL += "AND SUBSTRING(C.NR_PROCESSO,1,1) IN ('E') ";
                    break;
                default:
                    SQL += "";
                    break;
            }

            switch (dados.STATUS)
            {
                case "1":
                    SQL += "AND C.DT_CANCELAMENTO IS NULL AND C.DT_FINALIZACAO_PROCESSO IS NULL ";
                    break;
                case "2":
                    SQL += "AND C.DT_CANCELAMENTO IS NOT NULL ";
                    break;
                case "3":
                    SQL += "AND C.DT_FINALIZACAO_PROCESSO IS NOT NULL ";
                    break;
                default:
                    SQL += "";
                    break;
            }

            if (dados.PROCESSO != "")
            {
                SQL += "AND C.NR_PROCESSO LIKE '"+dados.PROCESSO+"%' ";
            }
            if (dados.CLIENTE != "")
            {
                SQL += "AND C.ID_PARCEIRO_CLIENTE = '"+dados.CLIENTE+"' ";
            }
            if (dados.ORIGEM != "")
            {
                SQL += "AND C.ID_PORTO_ORIGEM = '" + dados.ORIGEM + "' ";
            }
            if (dados.DESTINO != "")
            {
                SQL += "AND C.ID_PORTO_DESTINO = '" + dados.DESTINO + "' ";
            }
            if (dados.FRETE != "")
            {
                SQL += "AND C.ID_TIPO_PAGAMENTO = '" + dados.FRETE + "' ";
            }
            if (dados.ESTUFAGEM != "")
            {
                SQL += "AND C.ID_TIPO_ESTUFAGEM = '" + dados.ESTUFAGEM + "' ";
            }
            if (dados.AGENTE != "")
            {
                SQL += "AND C.ID_PARCEIR_AGENTE = '" + dados.AGENTE + "' ";
            }
            if (dados.PEMBARQUEINICIO != "")
            {
                if (dados.PEMBARQUEFIM != "")
                {
                    SQL += "AND C.DT_PREVISAO_EMBARQUE >= '" + dados.PEMBARQUEINICIO + "' AND C.DT_PREVISAO_EMBARQUE <= '" + dados.PEMBARQUEFIM + "' ";
                }
                else
                {
                    SQL += "AND C.DT_PREVISAO_EMBARQUE >= '" + dados.PEMBARQUEINICIO + "' ";
                }
            }
            else
            {
                if (dados.PEMBARQUEFIM != "")
                {
                    SQL += "AND C.DT_PREVISAO_EMBARQUE <= '" + dados.PEMBARQUEFIM + "' ";
                }
            }

            if (dados.DTEMBARQUEINICIO != "")
            {
                if (dados.DTEMBARQUEFIM != "")
                {
                    SQL += "AND C.DT_EMBARQUE >= '" + dados.DTEMBARQUEINICIO + "' AND C.DT_EMBARQUE <= '" + dados.DTEMBARQUEFIM + "' ";
                }
                else
                {
                    SQL += "AND C.DT_EMBARQUE >= '" + dados.DTEMBARQUEINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTEMBARQUEFIM != "")
                {
                    SQL += "AND C.DT_EMBARQUE <= '" + dados.DTEMBARQUEFIM + "' ";
                }
            }

            if (dados.PCHEGADAINICIO != "")
            {
                if (dados.PCHEGADAFIM != "")
                {
                    SQL += "AND C.DT_PREVISAO_CHEGADA >= '" + dados.PCHEGADAINICIO + "' AND C.DT_PREVISAO_CHEGADA <= '" + dados.PCHEGADAFIM + "' ";
                }
                else
                {
                    SQL += "AND C.DT_PREVISAO_CHEGADA >= '" + dados.PCHEGADAINICIO + "' ";
                }
            }
            else
            {
                if (dados.PCHEGADAFIM != "")
                {
                    SQL += "AND C.DT_PREVISAO_CHEGADA <= '" + dados.PCHEGADAFIM + "' ";
                }
            }

            if (dados.DTCHEGADAINICIO != "")
            {
                if (dados.DTCHEGADAFIM != "")
                {
                    SQL += "AND C.DT_CHEGADA >= '" + dados.DTCHEGADAINICIO + "' AND C.DT_CHEGADA <= '" + dados.DTCHEGADAFIM + "' ";
                }
                else
                {
                    SQL += "AND C.DT_CHEGADA >= '" + dados.DTCHEGADAINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTCHEGADAFIM != "")
                {
                    SQL += "AND C.DT_CHEGADA <= '" + dados.DTCHEGADAFIM + "' ";
                }
            }

            if (dados.TRANSPORTADOR != "")
            {
                SQL += "AND C.ID_PARCEIRO_TRANSPORTADOR = '" + dados.TRANSPORTADOR + "' ";
            }
            if (dados.BLMASTER != "")
            {
                SQL += "AND M.NR_BL LIKE '" + dados.BLMASTER + "%' ";
            }
            if (dados.BLHOUSE != "")
            {
                SQL += "AND C.NR_BL LIKE '" + dados.BLHOUSE + "%' ";
            }
            if (dados.CEMASTER != "")
            {
                SQL += "AND M.NR_CE LIKE '" + dados.CEMASTER + "%' ";
            }
            if (dados.CEHOUSE != "")
            {
                SQL += "AND C.NR_CE LIKE '" + dados.CEHOUSE + "%' ";
            }
            if (dados.DTREDESTINACAOINICIO != "")
            {
                if (dados.DTREDESTINACAOFIM != "")
                {
                    SQL += "AND C.DT_REDESTINACAO >= '" + dados.DTREDESTINACAOINICIO + "' AND C.DT_REDESTINACAO <= '" + dados.DTREDESTINACAOFIM + "' ";
                }
                else
                {
                    SQL += "AND C.DT_REDESTINACAO >= '" + dados.DTREDESTINACAOINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTREDESTINACAOFIM != "")
                {
                    SQL += "AND C.DT_REDESTINACAO <= '" + dados.DTREDESTINACAOFIM + "' ";
                }
            }

            if (dados.DTDESCONSOLIDACAOINICIO != "")
            {
                if (dados.DTDESCONSOLIDACAOFIM != "")
                {
                    SQL += "AND C.DT_DESCONSOLIDACAO >= '" + dados.DTDESCONSOLIDACAOINICIO + "' AND C.DT_DESCONSOLIDACAO <= '" + dados.DTDESCONSOLIDACAOFIM + "' ";
                }
                else
                {
                    SQL += "AND C.DT_DESCONSOLIDACAO >= '" + dados.DTDESCONSOLIDACAOINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTDESCONSOLIDACAOFIM != "")
                {
                    SQL += "AND C.DT_DESCONSOLIDACAO <= '" + dados.DTDESCONSOLIDACAOFIM + "' ";
                }
            }
            if (dados.WEEK != "")
            {
                SQL += "AND C.ID_WEEK = '" + dados.WEEK + "' ";
            }
            if (dados.NAVIO != "")
            {
                SQL += "AND C.ID_NAVIO = '" + dados.NAVIO + "' ";
            }



            return SQL;
        }

        [WebMethod]
        public string listarProcessosOperacional(FiltroModuloOperacional dados)
        {
            
            

            string SQL;
            SQL = "SELECT M.ID_BL AS MASTER, C.ID_BL AS HOUSE, ISNULL(C.NR_PROCESSO,'') AS PROCESSO, ISNULL(CLT.NM_RAZAO,'') AS CLIENTE, ISNULL(PORT1.NM_PORTO,'') AS ORIGEM, ISNULL(PORT2.NM_PORTO,'') AS DESTINO ";
            SQL += ", ISNULL(TPAG.NM_TIPO_PAGAMENTO,'') AS TPAGAMENTO, ISNULL(TESTUF.NM_TIPO_ESTUFAGEM,'') AS TESTUFAGEM, ISNULL(AGT.NM_RAZAO,'') AS AGENTE ";
            SQL += ", ISNULL(FORMAT(C.DT_PREVISAO_EMBARQUE,'dd/MM/yyyy'),'') AS PEMBARQUE, ISNULL(FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy'),'') AS EMBARQUE, ISNULL(FORMAT(C.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS PCHEGADA, ISNULL(FORMAT(C.DT_CHEGADA,'dd/MM/yyyy'),'') AS CHEGADA ";
            SQL += ", ISNULL(TRANSP.NM_RAZAO,'') AS TRANSPORTADOR, ISNULL(M.NR_BL,'') as BLMASTER, ISNULL(C.NR_BL,'') as BLHOUSE, ISNULL(FORMAT(C.DT_REDESTINACAO,'dd/MM/yyyy'),'') AS REDESTINACAO, ISNULL(FORMAT(C.DT_DESCONSOLIDACAO,'dd/MM/yyyy'),'') AS DESCONSOLIDACAO ";
            SQL += ", ISNULL(W.NM_WEEK,'') AS WEEK, ISNULL(NAV.NM_NAVIO,'') AS NAVIO, ISNULL(M.NR_CE,'') as CEMASTER, ISNULL(C.NR_CE,'') AS CEHOUSE, ISNULL(C.DS_TERMO,'') AS TERMO ";
            SQL += "FROM TB_BL C ";
            SQL += "LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO CLT ON C.ID_PARCEIRO_CLIENTE = CLT.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO AGT ON C.ID_PARCEIRO_AGENTE = AGT.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO TRANSP ON C.ID_PARCEIRO_TRANSPORTADOR = TRANSP.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PORTO PORT1 ON C.ID_PORTO_ORIGEM = PORT1.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO PORT2 ON C.ID_PORTO_DESTINO = PORT2.ID_PORTO ";
            SQL += "LEFT JOIN TB_TIPO_PAGAMENTO TPAG ON C.ID_TIPO_PAGAMENTO = TPAG.ID_TIPO_PAGAMENTO ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM TESTUF ON C.ID_TIPO_ESTUFAGEM = TESTUF.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN TB_NAVIO NAV ON C.ID_NAVIO = NAV.ID_NAVIO ";
            SQL += "LEFT JOIN TB_WEEK W ON C.ID_WEEK = W.ID_WEEK ";
            SQL += "LEFT JOIN TB_SERVICO S ON C.ID_SERVICO = S.ID_SERVICO ";
            SQL += "LEFT JOIN TB_VIATRANSPORTE V ON S.ID_VIATRANSPORTE = V.ID_VIATRANSPORTE ";
            SQL += "" + listarProcessosOperacionalFilter(dados) + " ";            
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string NumeroProcesso(string idProcesso)
        {
            string SQL;
            SQL = "SELECT NR_PROCESSO, DT_DESCONSOLIDACAO, DT_REDESTINACAO, W.ID_WEEK, DS_TERMO as TERMO FROM TB_BL LEFT JOIN TB_WEEK W ON TB_BL.ID_WEEK = W.ID_WEEK WHERE ID_BL = '"+idProcesso+"' ";           
            
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string dadosUpload(string idProcesso)
        {
            string SQL;
            SQL = "SELECT M.NR_BL as NRMASTER FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idProcesso + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string verificarProcesso(string idProcesso)
        {
            string SQL;
            SQL = "SELECT  FROM TB_BL C ";
            SQL += "LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_SERVICO S ON C.ID_SERVICO = S.ID_SERVICO ";
            SQL += "LEFT JOIN TB_VIATRANSPORTE V ON S.ID_VIATRANSPORTE = V.ID_VIATRANSPORTE ";
            SQL += "WHERE C.ID_BL = '" + idProcesso + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string listarProcessosEmail(string idProcessoMaster)
        {
            string SQL;
            SQL = "SELECT C.ID_BL AS HOUSE, C.NR_PROCESSO as PROCESSO, P.NM_RAZAO AS CLIENTE FROM TB_BL C ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON C.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += "WHERE M.NR_BL = '" + idProcessoMaster + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string escreverEmail()
        {
            string SQL;
            SQL = "SELECT NM_SETOR, NR_TELEFONE_SETOR, EMAIL_SETOR FROM TB_TIPOAVISO WHERE IDTIPOAVISO = 12";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string escreverAssuntoEmail(string idprocesso)
        {
            string SQL;
            SQL = "SELECT C.NR_PROCESSO as PROCESSO, P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, ";
            SQL += "C.NR_BL, P.NM_RAZAO AS CLIENTE, P.CNPJ FROM TB_BL C ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON C.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_PORTO P1 ON M.ID_PORTO_ORIGEM = P1.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO P2 ON M.ID_PORTO_DESTINO = P2.ID_PORTO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string inserirDados(string week, string dtRedestinacao, string dtDesconsolidacao, string idProcesso, string termo)
        {
            string SQL;
            if(dtDesconsolidacao == "")
            {
                dtDesconsolidacao = "null";
            }

            if(dtRedestinacao == "")
            {
                dtRedestinacao = "null";
            }
            SQL = "UPDATE TB_BL SET ID_WEEK = '" + week + "', DT_DESCONSOLIDACAO = " + dtDesconsolidacao + ", DT_REDESTINACAO = " + dtRedestinacao + ", DS_TERMO = '"+termo+"' ";
            SQL += "WHERE ID_BL = '" + idProcesso + "' ";

            string weekS = DBS.ExecuteScalar(SQL);
            if(weekS == null)
            {
                return null;
            }
            else
            {
                return "fail";
            }
        }
    }
}
