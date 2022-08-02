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
using System.IO;

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
        public string CarregaFiltro(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
        {
            string SQL;
            SQL = "WHERE RIGHT(A.NR_PROCESSO,2) >= 18 ";
            SQL += "AND B.ID_STATUS_COTACAO NOT IN(7, 8, 11) ";
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
                SQL += " AND LEFT(A.NR_PROCESSO,1) != 'A' ";
            }
            else if (tipo == "3")
            {
                SQL += " AND A.NM_TIPO_ESTUFAGEM IN('FCL','LCL') ";
                SQL += " AND LEFT(A.NR_PROCESSO,1) != 'A' ";
            }
            else if (tipo == "4")
            {
                SQL += " AND C.AP_VIA = 'AER' ";
            }

            if (embarque == "0")
            {
                SQL += " AND ID_BL_MASTER IS NOT NULL ";
            }
            return SQL;
        }


        [WebMethod]
        public string CarregaFiltroProcesso(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
        {
            string SQL;
            SQL = "where a.GRAU in ('c') ";
            if (vendedor != 0)
            {
                SQL += " AND A.ID_PARCEIRO_VENDEDOR = " + vendedor;
            }

            string anof;
            string periodoi;
            string periodof;

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
                periodoi = mesI +"/"+ anoI;
            }
            else
            {
                periodoi = "01/"+ anoI;
            }

            if (mesF != "")
            {
                periodof = mesF +"/"+ anof;
            }
            else
            {
                periodof = "12/"+anof;
            }


            SQL += " AND RIGHT(A.NR_PROCESSO,5) BETWEEN '" + periodoi + "' AND '" + periodof + "' ";


            if (tipo == "1")
            {
                SQL += " AND A.ID_TIPO_ESTUFAGEM ='1' ";
            }
            else if (tipo == "2")
            {
                SQL += " AND A.ID_TIPO_ESTUFAGEM ='2' ";
                SQL += " AND LEFT(A.NR_PROCESSO,1) != 'A' ";
            }
            else if (tipo == "3")
            {
                SQL += " AND A.ID_TIPO_ESTUFAGEM IN('1','2') ";
                SQL += " AND LEFT(A.NR_PROCESSO,1) != 'A' ";
            }
          

            if (embarque == "0")
            {
                SQL += " AND A.ID_BL_MASTER IS NOT NULL ";
            }
            return SQL;
        }

        [WebMethod]
        public string CarregaFiltroCntr(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
        {
            string SQL;
            SQL = "where C.GRAU in ('c') ";
            if (vendedor != 0)
            {
                SQL += " AND C.ID_PARCEIRO_VENDEDOR = " + vendedor;
            }

            string anof;
            string periodoi;
            string periodof;

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
                periodoi = mesI + "/" + anoI;
            }
            else
            {
                periodoi = "01/" + anoI;
            }

            if (mesF != "")
            {
                periodof = mesF + "/" + anof;
            }
            else
            {
                periodof = "12/" + anof;
            }


            SQL += " AND RIGHT(C.NR_PROCESSO,5) BETWEEN '" + periodoi + "' AND '" + periodof + "' ";


            if (tipo == "1")
            {
                SQL += " AND C.ID_TIPO_ESTUFAGEM ='1' ";
            }
            else if (tipo == "2")
            {
                SQL += " AND C.ID_TIPO_ESTUFAGEM ='2' ";
                SQL += " AND LEFT(C.NR_PROCESSO,1) != 'A' ";
            }
            else if (tipo == "3")
            {
                SQL += " AND C.ID_TIPO_ESTUFAGEM IN('1','2') ";
                SQL += " AND LEFT(C.NR_PROCESSO,1) != 'A' ";
            }


            if (embarque == "0")
            {
                SQL += " AND C.ID_BL_MASTER IS NOT NULL ";
            }
            return SQL;
        }

        [WebMethod]
        public string CarregaFiltroTeus(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
        {
            string SQL;
            SQL = "where C.GRAU in ('c') ";
            if (vendedor != 0)
            {
                SQL += " AND C.ID_PARCEIRO_VENDEDOR = " + vendedor;
            }

            string anof;
            string periodoi;
            string periodof;

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
                periodoi = mesI + "/" + anoI;
            }
            else
            {
                periodoi = "01/" + anoI;
            }

            if (mesF != "")
            {
                periodof = mesF + "/" + anof;
            }
            else
            {
                periodof = "12/" + anof;
            }


            SQL += " AND RIGHT(C.NR_PROCESSO,5) BETWEEN '" + periodoi + "' AND '" + periodof + "' ";


            if (tipo == "1")
            {
                SQL += " AND C.ID_TIPO_ESTUFAGEM ='1' ";
            }
            else if (tipo == "2")
            {
                SQL += " AND C.ID_TIPO_ESTUFAGEM ='2' ";
                SQL += " AND LEFT(C.NR_PROCESSO,1) != 'A' ";
            }
            else if (tipo == "3")
            {
                SQL += " AND C.ID_TIPO_ESTUFAGEM IN('1','2') ";
                SQL += " AND LEFT(C.NR_PROCESSO,1) != 'A' ";
            }


            if (embarque == "0")
            {
                SQL += " AND C.ID_BL_MASTER IS NOT NULL ";
            }
            return SQL;
        }

        /*[WebMethod]
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
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesF, vendedor, tipo) + " ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }*/

        [WebMethod]
        public string CarregarProcessos(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
        {
            string SQL;

            SQL = "select count(*) PROCESS, MAX(RIGHT(a.NR_PROCESSO,5)) AS PERIODO ";
            SQL += "from tb_bl a ";
            SQL += "" + CarregaFiltroProcesso(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + "";
            SQL += "and a.DT_CANCELAMENTO is null ";
            

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregarCntr(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
        {
            string SQL;

            SQL = "select ISNULL(SUM(CASE WHEN C.ID_TIPO_ESTUFAGEM = 1 THEN 1 ELSE 0 END),0) AS CNTR ";
            SQL += "from tb_cntr_bl A ";
            SQL += "JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
            SQL += "JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
            SQL += "" + CarregaFiltroCntr(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + " ";
            SQL += "AND C.DT_CANCELAMENTO IS NULL ";


            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregarTEUS(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
        {
            string SQL;

            SQL = "SELECT SUM(CASE WHEN C.ID_TIPO_ESTUFAGEM = 1 THEN D.TEU ELSE 0 END) AS TEUS ";
            SQL += "from tb_cntr_bl A ";
            SQL += "JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
            SQL += "JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
            SQL += "JOIN TB_TIPO_CONTAINER D ON D.ID_TIPO_CONTAINER = A.ID_TIPO_CNTR ";
            SQL += ""+ CarregaFiltroTeus(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + "";
            SQL += "AND C.DT_CANCELAMENTO IS NULL ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }


        [WebMethod]
         public string CarregarEstatistica(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
         {
             string SQL;

             SQL = "SELECT MES+'/'+ANO as PERIODO, COUNT(NR_PROCESSO) PROC_TOTAL, (SELECT COUNT(DISTINCT(NR_PROCESSO)) AS TOTAL ";
             SQL += "FROM VW_PROCESSO_CONTAINER A ";
             SQL += "INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ) AS TOTAL, ";
             SQL += "SUM(CNTR_IMP) + SUM(CNTR_EXP) CNTR_TOTAL, ";
             SQL += "SUM(TEUS_IMP) + SUM(TEUS_EXP) TEUS_TOTAL, ";
             SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'M' THEN 1 ELSE 0 END) AS PROC_IMP, ";
             SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'A' THEN 1 ELSE 0 END) AS PROC_AR, ";
             SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'E' THEN 1 ELSE 0 END) AS PROC_EXP, ";
             SQL += "SUM(CNTR_IMP) AS CNTR_IMP, ";
             SQL += "SUM(CNTR_EXP) AS CNTR_EXP, ";
             SQL += "SUM(TEUS_IMP) AS TEUS_IMP, ";
             SQL += "SUM(TEUS_EXP) AS TEUS_EXP ";
             SQL += "FROM( SELECT A.MES, A.ANO, A.NR_PROCESSO, ";
             SQL += "ISNULL(SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' AND A.NM_TIPO_ESTUFAGEM = 'FCL' AND A.ID_CNTR_BL IS NOT NULL THEN 1 ELSE 0 END),0) AS CNTR_IMP, ";
             SQL += "ISNULL(SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' AND A.ID_CNTR_BL IS NOT NULL THEN 1 ELSE 0 END),0) AS CNTR_EXP, ";
             SQL += "ISNULL(SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' THEN A.TEU ELSE 0 END),0) AS TEUS_IMP,";
             SQL += "ISNULL(SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' THEN A.TEU ELSE 0 END),0) AS TEUS_EXP ";
             SQL += "FROM VW_PROCESSO_CONTAINER A ";
             SQL += "JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO ";
             SQL += "JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
             SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + "";
             SQL += "GROUP BY A.MES, A.ANO, A.NR_PROCESSO ";
             SQL += ") X ";
             SQL += "GROUP BY MES, ANO ";

             DataTable total = new DataTable();

             total = DBS.List(SQL);

             return JsonConvert.SerializeObject(total);
         }

        [WebMethod]
        public static string TaxaProcesso(string dataI, string dataF, string nota, string filter)
        {
            string SQL;
            string diaI;
            string mesI;
            string anoI;
            string diaF;
            string mesF;
            string anoF;

            if (dataI != "")
            {
                diaI = dataI.Substring(8, 2);
                mesI = dataI.Substring(5, 2);
                anoI = dataI.Substring(0, 4);
                dataI = diaI + '-' + mesI + '-' + anoI;
            }
            else
            {
                dataI = "01-01-1900";
            }

            if (dataF != "")
            {
                diaF = dataF.Substring(8, 2);
                mesF = dataF.Substring(5, 2);
                anoF = dataF.Substring(0, 4);
                dataF = diaF + '-' + mesF + '-' + anoF;
            }
            else
            {
                dataF = "01-01-2900";
            }



            switch (filter)
            {
                case "1":
                    nota = "AND B.NR_PROCESSO LIKE '" + nota + "%' ";
                    break;
                case "2":
                    nota = "AND D.NM_RAZAO LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            SQL = "SELECT B.NR_PROCESSO, ";
            SQL += "D.NM_RAZAO, ";
            SQL += "CONVERT(DATE, B.DT_EMBARQUE, 103) AS DATA_EMBARQUE, ";
            SQL += "CONVERT(DATE, B.DT_CHEGADA, 103) AS DATA_CHEGADA, ";
            SQL += "M.NR_BL AS NR_MASTER, ";
            SQL += "B.NR_BL AS NR_HOUSE, ";
            SQL += "C.NM_ITEM_DESPESA, ";
            SQL += "E.SIGLA_MOEDA, ";
            SQL += "A.VL_TAXA_CALCULADO, ";
            SQL += "CASE WHEN A.CD_PR='P' THEN 'PAGAR' ELSE 'RECEBER' END AS TIPO ";
            SQL += "FROM TB_BL_TAXA A ";
            SQL += "JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "JOIN TB_ITEM_DESPESA C ON A.ID_ITEM_DESPESA = C.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_PARCEIRO D ON A.ID_PARCEIRO_EMPRESA = D.ID_PARCEIRO ";
            SQL += "JOIN TB_MOEDA E ON A.ID_MOEDA = E.ID_MOEDA ";
            SQL += "WHERE A.FL_DECLARADO = 1 AND A.ID_ORIGEM_PAGAMENTO = 2 ";
            SQL += "AND A.CD_PR = 'P' AND B.ID_BL NOT IN(SELECT ID_BL FROM TB_ACCOUNT_INVOICE) ";
            SQL += "AND C.ID_ITEM_DESPESA NOT IN(71) ";
            SQL += "AND CONVERT(DATE,B.DT_EMBARQUE,103) BETWEEN CONVERT(DATE,'" + dataI + "',103) AND CONVERT(DATE,'" + dataF + "',103) ";
            SQL += "ORDER BY B.NR_PROCESSO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string CarregaFiltroPizza(string anoI, string mesI, int vendedor, string tipo, string embarque)
        {
            string SQL;
            SQL = "WHERE RIGHT(A.NR_PROCESSO,2) >= 18 ";
            if (vendedor != 0)
            {
                SQL += " AND A.ID_PARCEIRO_VENDEDOR = " + vendedor;
            }

            string periodoi;
            if (mesI != "")
            {
                periodoi = anoI + mesI;
            }
            else
            {
                periodoi = anoI + "01";
            }

            SQL += " AND A.ANO+A.MES = " + periodoi + " ";


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

            if (embarque == "0")
            {
                SQL += " AND ID_BL_MASTER IS NOT NULL ";
            }
            return SQL;
        }

        [WebMethod]
        public string CarregarEstatisticaPizza(string anoI, string mesI, int vendedor, string tipo, string embarque)
        {
            string SQL;

            SQL = "SELECT MES+'/'+ANO as PERIODO, COUNT(NR_PROCESSO) PROC_TOTAL, (SELECT COUNT(DISTINCT(NR_PROCESSO)) AS TOTAL ";
            SQL += "FROM VW_PROCESSO_CONTAINER A ";
            SQL += "INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ) AS TOTAL, ";
            SQL += "SUM(CNTR_IMP) + SUM(CNTR_EXP) CNTR_TOTAL, ";
            SQL += "SUM(TEUS_IMP) + SUM(TEUS_EXP) TEUS_TOTAL, ";
            SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'M' THEN 1 ELSE 0 END) AS PROC_IMP, ";
            SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'A' THEN 1 ELSE 0 END) AS PROC_AR, ";
            SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'E' THEN 1 ELSE 0 END) AS PROC_EXP, ";
            SQL += "SUM(CNTR_IMP) AS CNTR_IMP, ";
            SQL += "SUM(CNTR_EXP) AS CNTR_EXP, ";
            SQL += "SUM(TEUS_IMP) AS TEUS_IMP, ";
            SQL += "SUM(TEUS_EXP) AS TEUS_EXP ";
            SQL += "FROM( SELECT A.MES, A.ANO, A.NR_PROCESSO, ";
            SQL += "ISNULL(SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' AND A.NM_TIPO_ESTUFAGEM = 'FCL' AND A.ID_CNTR_BL IS NOT NULL THEN 1 ELSE 0 END),0) AS CNTR_IMP, ";
            SQL += "ISNULL(SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' AND A.NM_TIPO_ESTUFAGEM = 'FCL' AND A.ID_CNTR_BL IS NOT NULL THEN 1 ELSE 0 END),0) AS CNTR_EXP, ";
            SQL += "ISNULL(SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' THEN A.TEU ELSE 0 END),0) AS TEUS_IMP,";
            SQL += "ISNULL(SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' THEN A.TEU ELSE 0 END),0) AS TEUS_EXP ";
            SQL += "FROM VW_PROCESSO_CONTAINER A ";
            SQL += "" + CarregaFiltroPizza(anoI, mesI, vendedor, tipo, embarque) + "";
            SQL += "GROUP BY A.MES, A.ANO, A.NR_PROCESSO ";
            SQL += ") X ";
            SQL += "GROUP BY MES, ANO ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregarProcessosPizza(string anoI, string mesI, int vendedor, string tipo, string embarque)
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
            SQL += "" + CarregaFiltroPizza(anoI, mesI, vendedor, tipo, embarque) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, ";
            SQL += "A.ANO ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }


        [WebMethod]
        public string CarregarQtdCntrPizza(string anoI, string mesI, int vendedor, string tipo, string embarque)
        {
            string SQL;

            SQL = "SELECT  A.MES+'/'+A.ANO as PERIODO, ";
            SQL += "sum(case when substring(A.NR_PROCESSO, 1, 1) = 'M' AND A.ID_CNTR_BL IS NOT NULL THEN isnull(E.QTDE20, 0) + isnull(E.QTDE40, 0) else 0 end) IMP, ";
            SQL += "sum(case when substring(A.NR_PROCESSO, 1, 1) = 'E' AND A.ID_CNTR_BL IS NOT NULL THEN isnull(E.QTDE20, 0)  + isnull(E.QTDE40, 0) else 0 end) EXP ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "INNER JOIN TB_CNTR_BL C ON A.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER D ON C.ID_TIPO_CNTR = D.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_TEUS E ON A.ID_BL = E.ID_BL ";
            SQL += "WHERE B.GRAU IN('C') ";
            SQL += "" + CarregaFiltroPizza(anoI, mesI, vendedor, tipo, embarque) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregarTeusPizza(string anoI, string mesI, int vendedor, string tipo, string embarque)
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
            SQL += "" + CarregaFiltroPizza(anoI, mesI, vendedor, tipo, embarque) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregaFiltroIndicador(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
        {
            string SQL;
            SQL = "WHERE RIGHT(A.NR_PROCESSO,2) >= 18 ";
            SQL += "AND B.ID_STATUS_COTACAO NOT IN(7, 8, 11) ";
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


            SQL += " AND A.ANO+A.MES >=" + periodoi + " AND A.ANO+A.MES <=" + periodof +" ";


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
                SQL += " AND C.AP_VIA = 'AER' ";
            }

            if (embarque == "0")
            {
                SQL += " AND ID_BL_MASTER IS NOT NULL ";
            }
            return SQL;
        }

        [WebMethod]
        public string ProcessosIndicador(string anoI, string anoF, string mesI, string mesF, int vendedor, string tipo, string embarque)
        {
            string SQL;

            SQL = "SELECT NM_RAZAO AS VENDEDOR, ";
            SQL += "(SELECT count(distinct(A.NR_PROCESSO)) AS SOMA ";
            SQL += "FROM VW_PROCESSO_CONTAINER A INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ";
            SQL += "JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO ";
            SQL += "JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += ""+ CarregaFiltroIndicador(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + " AND ";
            SQL += "SUBSTRING(A.NR_PROCESSO,1,1) = 'M') TOTAL_PROC_IMP, ";
            SQL += "(SELECT count(distinct(A.NR_PROCESSO)) AS SOMA ";
            SQL += "FROM VW_PROCESSO_CONTAINER A INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ";
            SQL += "JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO ";
            SQL += "JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += ""+ CarregaFiltroIndicador(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + "AND ";
            SQL += "SUBSTRING(A.NR_PROCESSO,1,1) = 'E') TOTAL_PROC_EXP, ";
            SQL += "(SELECT count(distinct(A.NR_PROCESSO)) AS SOMA ";
            SQL += "FROM VW_PROCESSO_CONTAINER A INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ";
            SQL += "JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO ";
            SQL += "JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += ""+ CarregaFiltroIndicador(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + "AND ";
            SQL += "SUBSTRING(A.NR_PROCESSO,1,1) = 'A') TOTAL_PROC_AR, ";
            SQL += "(SELECT SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' THEN A.TEU ELSE 0 END) ";
            SQL += "FROM VW_PROCESSO_CONTAINER A INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ";
            SQL += "JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO ";
            SQL += "JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += ""+ CarregaFiltroIndicador(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + ") AS TOTAL_TEUS_IMP, ";
            SQL += "(SELECT SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' THEN A.TEU ELSE 0 END) ";
            SQL += "FROM VW_PROCESSO_CONTAINER A INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ";
            SQL += "JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO ";
            SQL += "JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += ""+ CarregaFiltroIndicador(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + ") AS TOTAL_TEUS_EXP, ";
            SQL += "(SELECT SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' AND A.NM_TIPO_ESTUFAGEM = 'FCL' AND A.ID_CNTR_BL IS NOT NULL THEN 1 ELSE 0 END) ";
            SQL += "FROM VW_PROCESSO_CONTAINER A INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ";
            SQL += "JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO ";
            SQL += "JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += "JOIN TB_VIATRANSPORTE D ON C.ID_VIATRANSPORTE = D.ID_VIATRANSPORTE ";
            SQL += ""+ CarregaFiltroIndicador(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + ") AS TOTAL_CNTR_IMP, ";
            SQL += "(SELECT SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' AND A.NM_TIPO_ESTUFAGEM = 'FCL' AND A.ID_CNTR_BL IS NOT NULL THEN 1 ELSE 0 END) ";
            SQL += "FROM VW_PROCESSO_CONTAINER A INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ";
            SQL += "JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO ";
            SQL += "JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += ""+ CarregaFiltroIndicador(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + ") AS TOTAL_CNTR_EXP, ";
            SQL += "(SELECT COUNT(DISTINCT(A.NR_PROCESSO)) FROM VW_PROCESSO_CONTAINER A JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO AND B.ID_STATUS_COTACAO NOT IN (7,8,11) " + CarregaFiltroIndicador(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + ") AS TOTAL, ";
            SQL += "SUM(ISNULL(CNTR_IMP, 0)) +SUM(ISNULL(CNTR_EXP, 0)) AS CNTR_TOTAL, ";
            SQL += "SUM(ISNULL(TEUS_IMP, 0)) +SUM(ISNULL(TEUS_EXP, 0)) AS TEUS_TOTAL, ";
            SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'M' THEN 1 ELSE 0 END) AS PROC_IMP, ";
            SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'A' THEN 1 ELSE 0 END) AS PROC_AR, ";
            SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'E' THEN 1 ELSE 0 END) AS PROC_EXP, ";
            SQL += "SUM(CNTR_IMP) AS CNTR_IMP, ";
            SQL += "SUM(ISNULL(CNTR_EXP, 0)) AS CNTR_EXP, ";
            SQL += "SUM(TEUS_IMP) AS TEUS_IMP, ";
            SQL += "SUM(ISNULL(TEUS_EXP, 0)) AS TEUS_EXP ";
            SQL += "FROM( ";
            SQL += "SELECT A.MES, A.ANO, A.NR_PROCESSO, P.NM_RAZAO, ";
            SQL += "SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' AND A.NM_TIPO_ESTUFAGEM = 'FCL' AND A.ID_CNTR_BL IS NOT NULL THEN 1 ELSE 0 END) AS CNTR_IMP, ";
            SQL += "SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' AND A.NM_TIPO_ESTUFAGEM = 'FCL' AND A.ID_CNTR_BL IS NOT NULL THEN 1 ELSE 0 END) AS CNTR_EXP, ";
            SQL += "SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' THEN A.TEU ELSE 0 END) AS TEUS_IMP, ";
            SQL += "SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' THEN A.TEU ELSE 0 END) AS TEUS_EXP ";
            SQL += "FROM VW_PROCESSO_CONTAINER A ";
            SQL += "JOIN TB_COTACAO B ON B.ID_COTACAO = A.ID_COTACAO ";
            SQL += "JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += "INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ";
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesF, vendedor, tipo, embarque) + " ";
            SQL += "GROUP BY A.MES, A.ANO, A.NR_PROCESSO, P.NM_RAZAO ";
            SQL += ") X ";
            SQL += "GROUP BY NM_RAZAO ";

            DataTable processosIndicador = new DataTable();

            processosIndicador = DBS.List(SQL);

            return JsonConvert.SerializeObject(processosIndicador);
        }

        [WebMethod]
        public string ProcessosIndicadorPizza(string anoI, string mesI, int vendedor, string tipo, string embarque)
        {
            string SQL;

            SQL = "SELECT NM_RAZAO AS VENDEDOR, ";
            SQL += "COUNT(NR_PROCESSO) AS PROC_TOTAL, ";
            SQL += "SUM(ISNULL(CNTR_IMP, 0)) +SUM(ISNULL(CNTR_EXP, 0)) AS CNTR_TOTAL, ";
            SQL += "SUM(ISNULL(TEUS_IMP, 0)) +SUM(ISNULL(TEUS_EXP, 0)) AS TEUS_TOTAL, ";
            SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'M' THEN 1 ELSE 0 END) AS PROC_IMP, ";
            SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'A' THEN 1 ELSE 0 END) AS PROC_AR, ";
            SQL += "SUM(CASE WHEN SUBSTRING(NR_PROCESSO, 1, 1) = 'E' THEN 1 ELSE 0 END) AS PROC_EXP, ";
            SQL += "SUM(CNTR_IMP) AS CNTR_IMP, ";
            SQL += "SUM(ISNULL(CNTR_EXP, 0)) AS CNTR_EXP, ";
            SQL += "SUM(TEUS_IMP) AS TEUS_IMP, ";
            SQL += "SUM(ISNULL(TEUS_EXP, 0)) AS TEUS_EXP ";
            SQL += "FROM( ";
            SQL += "SELECT A.MES, A.ANO, A.NR_PROCESSO, P.NM_RAZAO, ";
            SQL += "SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' AND A.NM_TIPO_ESTUFAGEM = 'FCL' AND A.NR_CNTR IS NOT NULL THEN 1 ELSE 0 END) AS CNTR_IMP, ";
            SQL += "SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' AND A.NM_TIPO_ESTUFAGEM = 'FCL' AND A.NR_CNTR IS NOT NULL THEN 1 ELSE 0 END) AS CNTR_EXP, ";
            SQL += "SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'M' THEN A.TEU ELSE 0 END) AS TEUS_IMP, ";
            SQL += "SUM(CASE WHEN SUBSTRING(A.NR_PROCESSO, 1, 1) = 'E' THEN A.TEU ELSE 0 END) AS TEUS_EXP ";
            SQL += "FROM VW_PROCESSO_CONTAINER A ";
            SQL += "INNER JOIN TB_PARCEIRO P ON A.ID_PARCEIRO_VENDEDOR = P.ID_PARCEIRO ";
            SQL += "" + CarregaFiltroPizza(anoI, mesI, vendedor, tipo, embarque) + " ";
            SQL += "GROUP BY A.MES, A.ANO, A.NR_PROCESSO, P.NM_RAZAO ";
            SQL += ") X ";
            SQL += "GROUP BY MES, ANO, NM_RAZAO ";

            DataTable processosIndicador = new DataTable();

            processosIndicador = DBS.List(SQL);

            return JsonConvert.SerializeObject(processosIndicador);
        }

        [WebMethod]
        public string listarProcessos(string nmfilter, string txtfilter, string estufagem, string servico, string dtSiIni, string dtSiFim)
        {
            if (dtSiIni != "")
            {
                string diaI = dtSiIni.Substring(8, 2);
                string mesI = dtSiIni.Substring(5, 2);
                string anoI = dtSiIni.Substring(0, 4);
                dtSiIni = diaI + '/' + mesI + '/' + anoI;
            }
			else
			{
                dtSiIni = "01/01/1900";
			}

            if (dtSiFim != "")
            {
                string diaF = dtSiFim.Substring(8, 2);
                string mesF = dtSiFim.Substring(5, 2);
                string anoF = dtSiFim.Substring(0, 4);
                dtSiFim = diaF + '/' + mesF + '/' + anoF;
            }
            else
            {
                dtSiFim = "01/01/2900";
            }

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
                    estufagem = "AND TP.NM_TIPO_ESTUFAGEM = 'LCL' AND LEFT(A.NR_PROCESSO,1) != 'A' ";
                    break;
                case "3":
                    estufagem = "AND LEFT(A.NR_PROCESSO,1) = 'A' ";
                    break;
                default:
                    estufagem = "";
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
            SQL += "isnull(CNTR_TEUS.QTDE20,0) as QTDE20, isnull(CNTR_TEUS.QTDE40,0) AS QTDE40, ISNULL(Z1.NM_STATUS_COTACAO,'') AS STATUS_COTACAO, ";
            SQL += "ISNULL((SELECT STUFF((SELECT DISTINCT '/ ' + (SELECT isnull(T.NM_TIPO_CONTAINER,'') as NM_TIPO_CONTAINER FROM TB_CNTR_BL C INNER JOIN TB_TIPO_CONTAINER T ON C.ID_TIPO_CNTR = T.ID_TIPO_CONTAINER WHERE C.ID_CNTR_BL =B.ID_CNTR_BL ) FROM TB_CARGA_BL B WHERE A.ID_BL = B.ID_BL FOR XML PATH('')), 1, 1, '')),'') AS TIPO, ";
            SQL += "isnull(P1.NM_PORTO,'') AS ORIGEM, isnull(P2.NM_PORTO,'') AS DESTINO, ";
            SQL += "isnull(FORMAT(A.DT_ABERTURA,'dd/MM/yyyy'),'') AS DTABERTURA, isnull(FORMAT(A.DT_CANCELAMENTO,'dd/mm/yyyy'),'') as DTCANCELAMENTO, isnull(FORMAT(A.DT_PREVISAO_EMBARQUE ,'dd/MM/yyyy'),'') AS ETD, isnull(FORMAT(A.DT_EMBARQUE ,'dd/MM/yyyy'),'') AS ETA, ";
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
            SQL += "LEFT JOIN TB_COTACAO Z ON Z.ID_COTACAO = A.ID_COTACAO ";
            SQL += "LEFT JOIN TB_STATUS_COTACAO Z1 ON Z1.ID_STATUS_COTACAO = Z.ID_STATUS_COTACAO ";
            SQL += "WHERE A.GRAU = 'C' ";
            SQL += "" + nmfilter + " ";
            SQL += "" + estufagem + " ";
            SQL += "" + servico + " ";
            SQL += " AND CONVERT(DATE,A.DT_ABERTURA,103) BETWEEN CONVERT(DATE,'" + dtSiIni + "',103) AND CONVERT(DATE,'" + dtSiFim + "',103) ";
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
            SQL = "WHERE SUBSTRING(C.NR_PROCESSO,10,2)>= '18' AND C.ID_BL_MASTER IS NOT NULL ";
            if (dados.VIA != "")
            {
                SQL += "AND V.ID_VIATRANSPORTE = " + dados.VIA + " ";
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
                SQL += "AND C.NR_PROCESSO LIKE '" + dados.PROCESSO + "%' ";
            }
            if (dados.CLIENTE != "")
            {
                SQL += "AND C.ID_PARCEIRO_CLIENTE = '" + dados.CLIENTE + "' ";
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
                SQL += "AND C.ID_PARCEIRO_AGENTE = '" + dados.AGENTE + "' ";
            }
            if (dados.AGENTEINTERNACIONAL != "")
            {
                SQL += "AND C.ID_PARCEIRO_AGENTE_INTERNACIONAL = '" + dados.AGENTEINTERNACIONAL + "' ";
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
                    SQL += "AND M.DT_REDESTINACAO >= '" + dados.DTREDESTINACAOINICIO + "' AND M.DT_REDESTINACAO <= '" + dados.DTREDESTINACAOFIM + "' ";
                }
                else
                {
                    SQL += "AND M.DT_REDESTINACAO >= '" + dados.DTREDESTINACAOINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTREDESTINACAOFIM != "")
                {
                    SQL += "AND M.DT_REDESTINACAO <= '" + dados.DTREDESTINACAOFIM + "' ";
                }
            }

            if (dados.DTDESCONSOLIDACAOINICIO != "")
            {
                if (dados.DTDESCONSOLIDACAOFIM != "")
                {
                    SQL += "AND M.DT_DESCONSOLIDACAO >= '" + dados.DTDESCONSOLIDACAOINICIO + "' AND M.DT_DESCONSOLIDACAO <= '" + dados.DTDESCONSOLIDACAOFIM + "' ";
                }
                else
                {
                    SQL += "AND M.DT_DESCONSOLIDACAO >= '" + dados.DTDESCONSOLIDACAOINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTDESCONSOLIDACAOFIM != "")
                {
                    SQL += "AND M.DT_DESCONSOLIDACAO <= '" + dados.DTDESCONSOLIDACAOFIM + "' ";
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
            SQL += ", ISNULL(TRANSP.NM_RAZAO,'') AS TRANSPORTADOR, ISNULL(M.NR_BL,'') as BLMASTER, ISNULL(C.NR_BL,'') as BLHOUSE, ISNULL(FORMAT(M.DT_REDESTINACAO,'dd/MM/yyyy'),'') AS REDESTINACAO, ISNULL(FORMAT(M.DT_DESCONSOLIDACAO,'dd/MM/yyyy'),'') AS DESCONSOLIDACAO ";
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
            SQL = "SELECT NR_PROCESSO, ISNULL(FORMAT(DT_DESCONSOLIDACAO,'yyyy-MM-dd'),'') as DT_DESCONSOLIDACAO, ISNULL(FORMAT(DT_REDESTINACAO,'yyyy-MM-dd'),'') AS DT_REDESTINACAO, ISNULL(W.ID_WEEK,'') AS ID_WEEK, ISNULL(DS_TERMO,'') as TERMO FROM TB_BL LEFT JOIN TB_WEEK W ON TB_BL.ID_WEEK = W.ID_WEEK WHERE ID_BL = '" + idProcesso + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string dadosUpload(string idProcesso)
        {
            string SQL;
            SQL = "SELECT M.NR_BL as NRMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idProcesso + "' ";

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
            SQL = "SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = '" + idProcessoMaster + "' ";
            DataTable listTable2 = new DataTable();
            listTable2 = DBS.List(SQL);
            string idblmaster = listTable2.Rows[0]["ID_BL_MASTER"].ToString();


            SQL = "SELECT C.ID_BL AS HOUSE, C.NR_PROCESSO as PROCESSO, P.NM_RAZAO AS CLIENTE FROM TB_BL C ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON C.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += "WHERE M.ID_BL = '" + idblmaster + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string escreverTituloEmail(string idProcessoMaster)
        {
            string SQL;
            SQL = "SELECT ISNULL(M.NR_BL,'') AS NR_BL, ISNULL(P1.NM_RAZAO,'') AS TRANSPORTADOR, ISNULL(P2.NM_RAZAO,'') AS ARMAZEM_ATRACACAO ";
            SQL += "FROM TB_BL C ";
            SQL += "LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P1 ON M.ID_PARCEIRO_TRANSPORTADOR = P1.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON M.ID_PARCEIRO_ARMAZEM_ATRACACAO = P2.ID_PARCEIRO ";
            SQL += "WHERE C.ID_BL = '" + idProcessoMaster + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string escreverCorpoEmail()
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
            SQL += "WHERE C.ID_BL = '" + idprocesso + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string listarEmail(string filtro, string consulta, string enviado, string nenviado, string dtgerado)
        {
            switch (filtro)
            {
                case "1":
                    filtro = "AND B.NR_PROCESSO LIKE '" + consulta + "%' ";
                    break;
                case "2":
                    filtro = "AND D.NM_RAZAO LIKE '" + consulta + "%' ";
                    break;
                case "3":
                    filtro = "AND E.NMTIPOAVISO LIKE '%" + consulta + "%' ";
                    break;
                default:
                    filtro = "";
                    break;
            }

            if (enviado == "1" && nenviado == "1")
            {
                enviado = "";
                nenviado = "";
            }
            else
            {
                switch (enviado)
                {
                    case "1":
                        enviado = "AND A.DT_ENVIO IS NULL ";
                        break;
                    default:
                        enviado = "";
                        break;
                }

                switch (nenviado)
                {
                    case "1":
                        nenviado = "AND A.DT_ENVIO IS NOT NULL ";
                        break;
                    default:
                        nenviado = "";
                        break;
                }
            }

            switch (dtgerado)
            {
                case "1":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) = CONVERT (date, GETDATE()) ";
                    break;
                case "2":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) >= DATEADD(day, -7, CONVERT (date, GETDATE())) ";
                    break;
                case "3":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) >= DATEADD(day, -30, CONVERT (date, GETDATE())) ";
                    break;
                case "4":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) >= DATEADD(day, -60, CONVERT (date, GETDATE())) ";
                    break;
                case "5":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) >= DATEADD(day, -90, CONVERT (date, GETDATE())) ";
                    break;
            }
            string SQL;
            SQL = "SELECT A.AUTONUM AS IDEMAIL, ISNULL(B.NR_PROCESSO,'') AS PROCESSO, ISNULL(E.NMTIPOAVISO,'') AS NMTIPOAVISO , ISNULL(FORMAT(A.DT_GERACAO,'dd/MM/yyyy'),'') AS DT_GERACAO, ";
            SQL += "ISNULL(FORMAT(A.DT_START, 'dd/MM/yyyy'), '') AS PREVISAO, ISNULL(FORMAT(A.DT_ENVIO, 'dd/MM/yyyy'), '') AS DT_ENVIO, ";
            SQL += "ISNULL(D.NM_RAZAO, '') AS CLIENTE, ISNULL(A.IDARMAZEM,'') AS IDARMAZEM, ISNULL(C.NM_RAZAO, '') AS PARCEIRO, isnull((SELECT TOP 1 CRITICA FROM TB_GER_LOG GL WHERE GL.AUTONUM_EMAIL = A.AUTONUM ORDER BY GL.DT_CRITICA DESC),'') AS OCORRENCIA ";
            SQL += "FROM TB_GER_EMAIL A ";
            SQL += "LEFT JOIN TB_BL B ON A.IDPROCESSO = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON A.IDPARCEIRO = C.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO D ON A.IDCLIENTE = D.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_TIPOAVISO E ON A.IDTIPOAVISO = E.IDTIPOAVISO ";
            SQL += "WHERE A.AUTONUM IS NOT NULL ";
            SQL += "AND E.ORIGEM = 'OP' ";
            SQL += "" + filtro + " ";
            SQL += "" + enviado + " ";
            SQL += "" + nenviado + " ";
            SQL += "" + dtgerado + " ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string listarEmailAgendado(string filtro, string consulta, string enviado, string nenviado, string dtgerado)
        {
            switch (filtro)
            {
                case "1":
                    filtro = "AND B.NR_PROCESSO LIKE '" + consulta + "%' ";
                    break;
                case "2":
                    filtro = "AND C.NM_RAZAO LIKE '" + consulta + "%' ";
                    break;
                case "3":
                    filtro = "AND E.NMTIPOAVISO LIKE '%" + consulta + "%' ";
                    break;
                default:
                    filtro = "";
                    break;
            }

            if (enviado == "1" && nenviado == "1")
            {
                enviado = "";
                nenviado = "";
            }
            else
            {
                switch (enviado)
                {
                    case "1":
                        enviado = "AND A.DT_GERACAO_EMAIL IS NULL ";
                        break;
                    default:
                        enviado = "";
                        break;
                }

                switch (nenviado)
                {
                    case "1":
                        nenviado = "AND A.DT_GERACAO_EMAIL IS NOT NULL ";
                        break;
                    default:
                        nenviado = "";
                        break;
                }
            }

            switch (dtgerado)
            {
                case "1":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) = CONVERT (date, GETDATE()) ";
                    break;
                case "2":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) >= DATEADD(day, -7, CONVERT (date, GETDATE())) ";
                    break;
                case "3":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) >= DATEADD(day, -30, CONVERT (date, GETDATE())) ";
                    break;
                case "4":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) >= DATEADD(day, -60, CONVERT (date, GETDATE())) ";
                    break;
                case "5":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) >= DATEADD(day, -90, CONVERT (date, GETDATE())) ";
                    break;
            }
            string SQL;
            SQL = "SELECT A.ID_SOLICITACAO_EMAIL, ISNULL(B.NR_PROCESSO,'') AS NR_PROCESSO, ISNULL(M.NR_BL,'') AS NR_BL, ISNULL(E.NMTIPOAVISO,'') AS NMTIPOAVISO, ISNULL(FORMAT(A.DT_SOLICITACAO,'dd/MM/yyyy'),'') AS DT_SOLICITACAO, ";
            SQL += "ISNULL(FORMAT(A.DT_START,'dd/MM/yyyy'),'') AS DT_START, ISNULL(FORMAT(A.DT_GERACAO_EMAIL,'dd/MM/yyyy'),'') AS DT_GERACAO_EMAIL, ";
            SQL += "ISNULL(FORMAT(A.DT_CANCELAMENTO,'dd/MM/yyyy'),'') AS DT_CANCELAMENTO, ISNULL(A.OB_CANCELAMENTO,'') AS OB_CANCELAMENTO, ";
            SQL += "ISNULL(C.NM_RAZAO,'') AS CLIENTE, ISNULL(CONVERT(VARCHAR,A.IDARMAZEM),'') AS IDARMAZEM, ISNULL(D.NM_RAZAO,'') AS PARCEIRO ";
            SQL += "FROM TB_solicitacao_email A ";
            SQL += "LEFT JOIN TB_BL B ON A.IDPROCESSO = B.ID_BL ";
            SQL += "LEFT JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON B.ID_PARCEIRO_CLIENTE = C.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO D ON A.IDPARCEIRO = D.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_TIPOAVISO E ON A.IDTIPOAVISO = E.IDTIPOAVISO ";
            SQL += "WHERE ORIGEM = 'OP' ";
            SQL += "" + filtro + " ";
            SQL += "" + enviado + " ";
            SQL += "" + nenviado + " ";
            SQL += "" + dtgerado + " ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string verificarCancelamento(string idEmail)
        {
            string SQL;
            SQL = "SELECT DT_CANCELAMENTO FROM TB_SOLICITACAO_EMAIL WHERE ID_SOLICITACAO_EMAIL = '" + idEmail + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string dtcancelamento = listTable.Rows[0]["DT_CANCELAMENTO"].ToString();
            if (dtcancelamento != "")
            {
                return "cancelado";
            }
            else
            {
                return "cancelar";
            }
        }

        [WebMethod]
        public string cancelarAgendamento(string idEmail)
        {
            string SQL;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            SQL = "SELECT DT_GERACAO_EMAIL FROM TB_solicitacao_email A WHERE ID_SOLICITACAO_EMAIL = '" + idEmail + "'";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string dtenvio = listTable.Rows[0]["DT_GERACAO_EMAIL"].ToString();
            if (dtenvio == "")
            {
                SQL = "UPDATE TB_SOLICITACAO_EMAIL SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', OB_CANCELAMENTO = 'CANCELADO PELO USUÁRIO' WHERE ID_SOLICITACAO_EMAIL = '" + idEmail + "' ";
                string cancelar = DBS.ExecuteScalar(SQL);

                return "ok";
            }
            else
            {
                return "erro";
            }
        }

        [WebMethod]
        public string reativarAgendamento(string idEmail)
        {
            string SQL;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            SQL = "UPDATE TB_SOLICITACAO_EMAIL SET DT_GERACAO_EMAIL = NULL, DT_CANCELAMENTO = null, OB_CANCELAMENTO = NULL WHERE ID_SOLICITACAO_EMAIL = '" + idEmail + "' ";
            string reativar = DBS.ExecuteScalar(SQL);

            return "ok";
        }

        [WebMethod]
        public string checarDiretorio()
        {
            string SQL;
            SQL = "SELECT PATHDOCUMENTOS FROM TB_AVISOPARAM WHERE IDTIPOAVISOPARAM=1 ";
            string PathDocumentos = DBS.ExecuteScalar(SQL);
            string path = HttpContext.Current.Server.MapPath("~/UPLOADS/");
            if (Directory.Exists(path) == false)
            {
                return JsonConvert.SerializeObject("0");
            }
            else
            {
                return JsonConvert.SerializeObject("1");
            }
        }

        [WebMethod]
        public string uploadArquivo(string idprocesso, string iddocumento, string arquivo, string idtipoaviso)
        {
            string path = HttpContext.Current.Server.MapPath("~/UPLOADS/");
            string SQL;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            SQL = "SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = '" + idprocesso + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string blmaster = listTable.Rows[0]["ID_BL_MASTER"].ToString();

            SQL = "SELECT PATHDOCUMENTOSROBO FROM TB_AVISOPARAM ";
            DataTable robo = new DataTable();
            robo = DBS.List(SQL);
            string pathrobo = robo.Rows[0]["PATHDOCUMENTOSROBO"].ToString();

            SQL = "SELECT ID_PARCEIRO_DESCONSOLIDACAO, ID_PARCEIRO_REDESTINACAO_CONSOLIDADA FROM TB_PARAMETROS ";
            DataTable idparceiroc = new DataTable();
            idparceiroc = DBS.List(SQL);
            string parceiroD = idparceiroc.Rows[0]["ID_PARCEIRO_DESCONSOLIDACAO"].ToString();
            string parceiroRD = idparceiroc.Rows[0]["ID_PARCEIRO_REDESTINACAO_CONSOLIDADA"].ToString();

            SQL = "SELECT IDTIPOAVISO, TPPROCESSO FROM TB_TIPOAVISO WHERE IDTIPOAVISO = '" + idtipoaviso + "' ";
            DataTable tpprocesso = new DataTable();
            tpprocesso = DBS.List(SQL);
            string tipoprocesso = tpprocesso.Rows[0]["TPPROCESSO"].ToString();
            string idtipoavisop = tpprocesso.Rows[0]["IDTIPOAVISO"].ToString();

            if (idtipoaviso == "3")
            {
                SQL = "SELECT NR_PROCESSO AS NRHOUSE FROM TB_BL WHERE ID_BL = '" + idprocesso + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                string anoH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(9, 2);
                string mesH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(6, 2);
                string diretorio = path + "20" + anoH + "\\" + mesH + "\\" + listTable2.Rows[0]["NRHOUSE"].ToString().Replace("/", "") + "\\";

                SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                SQL += "VALUES ('" + idprocesso + "',NULL,'" + iddocumento + "','" + sqlFormattedDate + "','" + diretorio + "','" + arquivo + "','" + pathrobo + "') ";

                DBS.ExecuteScalar(SQL);

                SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + idtipoaviso + "', '" + idprocesso + "',NULL,'" + idprocesso + "',NULL,NULL) ";

                DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "SELECT NR_PROCESSO AS NRHOUSE FROM TB_BL WHERE ID_BL = '" + idprocesso + "' ";
                DataTable listTable3 = new DataTable();
                listTable3 = DBS.List(SQL);
                string anoH = listTable3.Rows[0]["NRHOUSE"].ToString().Substring(9, 2);
                string mesH = listTable3.Rows[0]["NRHOUSE"].ToString().Substring(6, 2);

                SQL = "SELECT M.NR_BL AS NRMASTER FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL_MASTER = '" + blmaster + "' ";
                DataTable listTable4 = new DataTable();
                listTable4 = DBS.List(SQL);
                string nrblmaster = listTable4.Rows[0]["NRMASTER"].ToString();
                string diretorio = path + "20" + anoH + "\\" + mesH + "\\MASTER-" + nrblmaster + "\\";

                SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                SQL += "VALUES (NULL,'" + blmaster + "','" + iddocumento + "','" + sqlFormattedDate + "','" + diretorio + "','" + arquivo + "','" + pathrobo + "') ";
                DBS.ExecuteScalar(SQL);

                if (idtipoaviso == "1")
                {
                    SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                    SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + idtipoaviso + "', NULL, '" + blmaster + "',NULL,NULL,'" + parceiroD + "') ";
                    DBS.ExecuteScalar(SQL);
                }
                else if (idtipoaviso == "2")
                {
                    SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                    SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + idtipoaviso + "', NULL, '" + blmaster + "','" + idprocesso + "','" + parceiroRD + "',NULL) ";
                    DBS.ExecuteScalar(SQL);
                }
            }

            return JsonConvert.SerializeObject("ok");
        }

        [WebMethod]
        public string listarDcoumentosArquivados(string idprocesso, string idtipoaviso)
        {
            string SQL;
            SQL = "SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = '" + idprocesso + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string blmaster = listTable.Rows[0]["ID_BL_MASTER"].ToString();


            if (idtipoaviso == "3")
            {
                SQL = "SELECT FORMAT(A.DTPOSTAGEM,'dd/MM/yyyy hh:mm:ss') AS DTPOSTAGEM, A.AUTONUM, B.NMDOCUMENTO ";
                SQL += "FROM TB_GER_ANEXO A ";
                SQL += "LEFT JOIN TB_DOCUMENTO B ON A.IDDOCUMENTO = B.IDDOCUMENTO ";
                SQL += "WHERE A.IDPROCESSO = '" + idprocesso + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);

                return JsonConvert.SerializeObject(listTable2);
            }
            else
            {
                SQL = "SELECT FORMAT(A.DTPOSTAGEM,'dd/MM/yyyy hh:mm:ss') AS DTPOSTAGEM, A.AUTONUM, B.NMDOCUMENTO ";
                SQL += "FROM TB_GER_ANEXO A ";
                SQL += "LEFT JOIN TB_DOCUMENTO B ON A.IDDOCUMENTO = B.IDDOCUMENTO ";
                SQL += "WHERE A.IDMASTER = '" + blmaster + "' ";
                DataTable listTable3 = new DataTable();
                listTable3 = DBS.List(SQL);

                return JsonConvert.SerializeObject(listTable3);
            }
        }

        [WebMethod]
        public string deleterDocumentoArquivado(string documentoArquivado)
        {
            string path = DBS.ExecuteScalar("SELECT DCPATHARQUIVO FROM TB_GER_ANEXO WHERE AUTONUM = '" + documentoArquivado + "' ");
            string arquivo = DBS.ExecuteScalar("SELECT NMARQUIVO FROM TB_GER_ANEXO WHERE AUTONUM = '" + documentoArquivado + "' ");
            string concat = path + arquivo;
            File.Delete(concat);

            string SQL;
            SQL = "DELETE FROM TB_GER_ANEXO WHERE AUTONUM = '" + documentoArquivado + "' ";
            string deleteAnexo = DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("ok");
        }

        [WebMethod]
        public string enviarEmail(string house, string corpo)
        {
            string SQL;
            string destinatario = "";
            string origem = "";
            string refCliente = "";
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            SQL = "SELECT A.NR_BL, A.NR_PROCESSO, ORIGEM.NM_PORTO AS ORIGEM, DESTINO.NM_PORTO AS DESTINO,ID_PARCEIRO_CLIENTE, CLIENTE.NM_RAZAO AS CLIENTE, ";
            SQL += "CLIENTE.CNPJ, IMPORTADOR.NM_RAZAO AS IMPORTADOR, C.NM_VIATRANSPORTE as VIA ";
            SQL += "FROM TB_BL A ";
            SQL += "LEFT JOIN TB_PORTO ORIGEM ON A.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO DESTINO ON A.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += "LEFT JOIN TB_PARCEIRO CLIENTE ON A.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO IMPORTADOR ON A.ID_PARCEIRO_IMPORTADOR = IMPORTADOR.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_SERVICO B ON A.ID_SERVICO = B.ID_SERVICO ";
            SQL += "LEFT JOIN TB_VIATRANSPORTE C ON B.ID_VIATRANSPORTE = C.ID_VIATRANSPORTE ";
            SQL += "WHERE A.ID_BL = '" + house + "' ";


            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string nmCliente = listTable.Rows[0]["CLIENTE"].ToString();
            string nrHouse = listTable.Rows[0]["NR_BL"].ToString();
            string nrProcesso = listTable.Rows[0]["NR_PROCESSO"].ToString();
            string origemP = listTable.Rows[0]["ORIGEM"].ToString();
            string destinoP = listTable.Rows[0]["DESTINO"].ToString();
            string cnpj = listTable.Rows[0]["CNPJ"].ToString();
            string nmVia = listTable.Rows[0]["VIA"].ToString();
            string idCliente = listTable.Rows[0]["ID_PARCEIRO_CLIENTE"].ToString();
            string nmImportador = listTable.Rows[0]["IMPORTADOR"].ToString();

            if (nmVia == "MARÍTIMA")
            {
                SQL = "SELECT ID_DESTINATARIO_MAR, ORIGEM FROM TB_TIPOAVISO WHERE IDTIPOAVISO = 12";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                destinatario = listTable2.Rows[0]["ID_DESTINATARIO_MAR"].ToString();
                origem = listTable2.Rows[0]["ORIGEM"].ToString();
            }
            else
            {
                if (nmVia == "AÉREA")
                {
                    SQL = "SELECT ID_DESTINATARIO_AER, ORIGEM FROM TB_TIPOAVISO WHERE IDTIPOAVISO = 12";
                    DataTable listTable3 = new DataTable();
                    listTable3 = DBS.List(SQL);
                    destinatario = listTable3.Rows[0]["ID_DESTINATARIO_AER"].ToString();
                    origem = listTable3.Rows[0]["ORIGEM"].ToString();
                }
            }

            refCliente = DBS.ExecuteScalar("SELECT dbo.FN_REFERENCIA_CLIENTE(" + house + ")");
            if (!string.IsNullOrEmpty(nmImportador))
            {
                nmImportador = "- IMPORTADOR: " + nmImportador + " - ";
            }
            SQL = "INSERT INTO TB_GER_EMAIL (ASSUNTO, CORPO, DT_GERACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDCLIENTE, TPORIGEM, ID_DESTINATARIO) ";
            SQL += "VALUES ('" + nrProcesso + " - COMUNICADO - " + origemP + " X " + destinoP + " - HBL: " + nrHouse + "<br>" + nmCliente + " - " + cnpj + "<br>"+ nmImportador + refCliente + "<br>', ";
            SQL += "'" + corpo + "','" + sqlFormattedDate + "','" + sqlFormattedDate + "',12,'" + house + "','" + idCliente + "','" + origem + "','" + destinatario + "') ";
            string gerarEmail = DBS.ExecuteScalar(SQL);

            return "ok";
        }

        [WebMethod]
        public string listarTipoAviso()
        {
            string SQL;

            SQL = "SELECT DISTINCT A.IDTIPOAVISO, A.NMTIPOAVISO ";
            SQL += "FROM dbo.TB_TIPOAVISO A ";
            SQL += "INNER JOIN dbo.TB_TIPOAVISOITEM B ON A.IDTIPOAVISO = B.IDTIPOAVISO ";
            SQL += "WHERE A.ORIGEM = 'OP' ";
            SQL += "ORDER BY A.NMTIPOAVISO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string listarTipoDocumento(string idtipoaviso)
        {
            string SQL;

            SQL = "SELECT A.IDDOCUMENTO, B.NMDOCUMENTO ";
            SQL += "FROM dbo.TB_TIPOAVISOITEM A ";
            SQL += "INNER JOIN TB_DOCUMENTO B ON A.IDDOCUMENTO = B.IDDOCUMENTO ";
            SQL += "WHERE A.IDTIPOAVISO = '" + idtipoaviso + "' ";
            SQL += "ORDER BY B.NMDOCUMENTO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string visualizarEmail(string idProcesso)
        {
            string SQL;

            SQL = "SELECT AUTONUM, ASSUNTO, REPLACE(REPLACE(CORPO,'''',''),Char(10) ,'<br>') as CORPO FROM TB_GER_EMAIL C WHERE C.AUTONUM = '" + idProcesso + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string verificarRemocao(string idProcesso)
        {
            string SQL;
            string result;
            SQL = "SELECT C.DT_ENVIO, C.ENVIADO FROM TB_GER_EMAIL C WHERE C.AUTONUM = '" + idProcesso + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string dtenvio = listTable.Rows[0]["DT_ENVIO"].ToString();
            if (dtenvio == "")
            {
                result = "ok";
                return JsonConvert.SerializeObject(result);
            }
            else
            {
                result = "nok";
                return JsonConvert.SerializeObject(result);
            }
        }

        [WebMethod]
        public string verificarReenvio(string idProcesso)
        {
            string SQL;
            string result;
            SQL = "SELECT C.DT_ENVIO FROM TB_GER_EMAIL C WHERE C.AUTONUM = '" + idProcesso + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string dtenvio = listTable.Rows[0]["DT_ENVIO"].ToString();
            if (dtenvio != "")
            {
                result = "ok";
                return JsonConvert.SerializeObject(result);
            }
            else
            {
                result = "nok";
                return JsonConvert.SerializeObject(result);
            }
        }

        [WebMethod]
        public string removerEmail(string idProcesso)
        {
            string SQL;
            string result;
            SQL = "DELETE FROM TB_GER_EMAIL WHERE AUTONUM = '" + idProcesso + "' ";
            string deletar = DBS.ExecuteScalar(SQL);

            result = "ok";

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public string reenviarEmail(string idProcesso)
        {
            string SQL;
            string result;
            SQL = "UPDATE TB_GER_EMAIL SET DT_ENVIO = NULL, ENVIADO = 0 WHERE AUTONUM = '" + idProcesso + "' ";
            string deletar = DBS.ExecuteScalar(SQL);

            result = "ok";

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public string inserirDados(string week, string dtRedestinacao, string dtDesconsolidacao, string idProcesso, string termo)
        {
            string SQL;
            string desconsolidacao;
            string redestinacao;
            if (dtDesconsolidacao == "")
            {
                desconsolidacao = "DT_DESCONSOLIDACAO = NULL";
            }
            else
            {
                desconsolidacao = "DT_DESCONSOLIDACAO = '" + dtDesconsolidacao + "' ";

            }

            if (dtRedestinacao == "")
            {
                redestinacao = "DT_REDESTINACAO = NULL";
            }
            else
            {
                redestinacao = "DT_REDESTINACAO = '" + dtRedestinacao + "'";

            }
            SQL = "UPDATE TB_BL SET ID_WEEK = '" + week + "'," + desconsolidacao + "," + redestinacao + ", DS_TERMO = '" + termo + "' ";
            SQL += "WHERE ID_BL = '" + idProcesso + "' ";

            string weekS = DBS.ExecuteScalar(SQL);
            if (weekS == null)
            {
                return null;
            }
            else
            {
                return "fail";
            }
        }

        [WebMethod]
        public string CarregarCliente(string idvendedor)
        {
            string SQL;
            SQL = "SELECT id_parceiro, NM_RAZAO FROM tb_parceiro where id_vendedor = '" + idvendedor + "' order by NM_RAZAO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }


        [WebMethod]
        public string deletarAtendimento(string id)
        {
            string SQL;
            SQL = "DELETE FROM TB_ATENDIMENTO_NEGADO WHERE ID_ATENDIMENTO_NEGADO = '" + id + "' ";

            DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("0");
        }

        [WebMethod]
        public string CarregarClienteFinal(string idcliente)
        {
            string SQL;
            SQL = "select ID_CLIENTE_FINAL, NM_CLIENTE_FINAL from tb_cliente_final where ID_PARCEIRO = '" + idcliente + "' order by NM_CLIENTE_FINAL ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }


        [WebMethod(EnableSession = true)]
        public string listarAtendimento(string dataI, string dataF, string filter, string text)
        {

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '/' + mesI + '/' + anoI;
            dataF = diaF + '/' + mesF + '/' + anoF;

            switch (filter)
            {
                case "1":
                    filter = "AND VENDEDOR LIKE '" + text + "%' ";
                    break;
                case "2":
                    filter = "AND INSIDE LIKE '" + text + "%' ";
                    break;
                case "3":
                    filter = "AND CLIENTE LIKE '" + text + "%' ";
                    break;
                default:
                    filter = "";
                    break;
            }

            string SQL;
            SQL = "SELECT ID_ATENDIMENTO_NEGADO, format(DT_SOLICITACAO,'dd/MM/yyyy') as DT_SOLICITACAO, INSIDE, SERVICO, TIPO_ESTUFAGEM AS ESTUFAGEM, CD_INCOTERM AS INCOTERM, NM_CLIENTE AS CLIENTE, NM_ORIGEM AS ORIGEM, ";
            SQL += "NM_DESTINO AS DESTINO, NM_VENDEDOR AS VENDEDOR, NM_STATUS_COTACAO AS STATUS, DS_OBS AS OBS, QTCARGA, CARGA, PESO, METRAGEM, CASE WHEN IDMERCADORIA = 22 THEN MERCADORIA + ' - ' + TIPO_CONTAINER ELSE MERCADORIA END AS MERCADORIA ";
            SQL += "FROM dbo.FN_ATENDIMENTO_NEGADO('"+dataI+"','"+dataF+"',"+Session["ID_USUARIO"]+") ";
            SQL += "WHERE ID_ATENDIMENTO_NEGADO IS NOT NULL ";
            SQL += "" + filter + "";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string buscarAtendimento(string id)
        {
            string SQL;
            SQL = "SELECT FORMAT(DT_ATENDIMENTO_NEGADO,'dd/MM/yyyy') AS ATENDIMENTO, ";
            SQL += "ID_PARCEIRO_INSIDE AS INSIDE,ID_SERVICO AS SERVICO, ID_TIPO_ESTUFAGEM AS ESTUFAGEM, ";
            SQL += "ID_INCOTERM AS INCOTERM, ";
            SQL += "ID_PARCEIRO_CLIENTE AS CLIENTE, ";
            SQL += "ID_PARCEIRO_CLIENTE_FINAL AS CLIENTEF, ID_PORTO_ORIGEM AS ORIGEM, ";
            SQL += "ID_PORTO_DESTINO AS DESTINO, ";
            SQL += "ID_VENDEDOR AS VENDEDOR, ID_STATUS AS STATUS ";
            SQL += "FROM TB_ATENDIMENTO_NEGADO ";
            SQL += "WHERE ID_ATENDIMENTO_NEGADO = '" + id + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }


        [WebMethod]
        public string CadastrarAtendimento(Atendimento dados)
        {

            if (dados.ID_MERCADORIA == "22" && dados.ID_TIPO_CONTAINER == "0")
			{
                return JsonConvert.SerializeObject("0");
            }
            if (dados.DT_ATENDIMENTO_NEGADO == "")
            {
                return JsonConvert.SerializeObject("0");
            }

            if (dados.ID_PARCEIRO_INSIDE == "")
            {
                return JsonConvert.SerializeObject("0");
            }

            if (dados.ID_VENDEDOR == "")
            {
                return JsonConvert.SerializeObject("0");
            }

            if (dados.ID_PARCEIRO_CLIENTE == "")
            {
                return JsonConvert.SerializeObject("0");
            }

            if (dados.ID_STATUS.ToString() == "0")
            {
                return JsonConvert.SerializeObject("0");
            }

            if (dados.QT_CARGA == "")
            {
                dados.QT_CARGA = "0";
            }
            if (dados.QT_PESO == "")
            {
                dados.QT_PESO = "0";
            }
            if (dados.QT_METRAGEM == "")
            {
                dados.QT_METRAGEM = "0";
            }
            if (dados.ID_TIPO_CONTAINER.ToString() == "0")
            {
                dados.ID_TIPO_CONTAINER = "null";
            }



            string SQL;

            SQL = "INSERT INTO TB_ATENDIMENTO_NEGADO (DT_ATENDIMENTO_NEGADO, ID_PARCEIRO_INSIDE, ID_VENDEDOR, ID_PARCEIRO_CLIENTE, ";
            SQL += "ID_SERVICO, ID_TIPO_ESTUFAGEM, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_INCOTERM, ID_STATUS, ID_TIPO_CARGA, QT_CARGA, ";
            SQL += "ID_MERCADORIA, QT_PESO, QT_METRAGEM, DS_OBS, ID_TIPO_CONTAINER) VALUES('" + dados.DT_ATENDIMENTO_NEGADO + "', ";
            SQL += " '" + dados.ID_PARCEIRO_INSIDE + "','" + dados.ID_VENDEDOR + "','" + dados.ID_PARCEIRO_CLIENTE + "', ";
            SQL += " '" + dados.ID_SERVICO + "','" + dados.ID_TIPO_ESTUFAGEM + "','" + dados.ID_PORTO_ORIGEM + "', ";
            SQL += " '" + dados.ID_PORTO_DESTINO + "','" + dados.ID_INCOTERM + "'," + dados.ID_STATUS + ", ";
            SQL += "" + dados.ID_TIPO_CARGA + "," + dados.QT_CARGA + "," + dados.ID_MERCADORIA + "," + dados.QT_PESO + ", ";
            SQL += "" + dados.QT_METRAGEM.ToString().Replace(",",".") +",'" + dados.DS_OBS + "',"+dados.ID_TIPO_CONTAINER+")";
            DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("1");
        }

        [WebMethod]
        public string listarEmailFinanceiro(string filtro, string consulta, string enviado, string nenviado, string dtgerado)
        {
            switch (filtro)
            {
                case "1":
                    filtro = "AND B.NR_PROCESSO LIKE '" + consulta + "%' ";
                    break;
                case "2":
                    filtro = "AND D.NM_RAZAO LIKE '" + consulta + "%' ";
                    break;
                case "3":
                    filtro = "AND E.NMTIPOAVISO LIKE '%" + consulta + "%' ";
                    break;
                default:
                    filtro = "";
                    break;
            }

            if (enviado == "1" && nenviado == "1")
            {
                enviado = "";
                nenviado = "";
            }
            else
            {
                switch (enviado)
                {
                    case "1":
                        enviado = "AND A.DT_ENVIO IS NULL ";
                        break;
                    default:
                        enviado = "";
                        break;
                }

                switch (nenviado)
                {
                    case "1":
                        nenviado = "AND A.DT_ENVIO IS NOT NULL ";
                        break;
                    default:
                        nenviado = "";
                        break;
                }
            }

            switch (dtgerado)
            {
                case "1":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) = CONVERT (date, GETDATE()) ";
                    break;
                case "2":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) >= DATEADD(day, -7, CONVERT (date, GETDATE())) ";
                    break;
                case "3":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) >= DATEADD(day, -30, CONVERT (date, GETDATE())) ";
                    break;
                case "4":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) >= DATEADD(day, -60, CONVERT (date, GETDATE())) ";
                    break;
                case "5":
                    dtgerado = "AND CONVERT (date,A.DT_GERACAO) >= DATEADD(day, -90, CONVERT (date, GETDATE())) ";
                    break;
            }
            string SQL;
            SQL = "SELECT A.AUTONUM AS IDEMAIL, ISNULL(B.NR_PROCESSO,'') AS PROCESSO, ISNULL(E.NMTIPOAVISO,'') AS NMTIPOAVISO , ISNULL(FORMAT(A.DT_GERACAO,'dd/MM/yyyy'),'') AS DT_GERACAO, ";
            SQL += "ISNULL(FORMAT(A.DT_START, 'dd/MM/yyyy'), '') AS PREVISAO, ISNULL(FORMAT(A.DT_ENVIO, 'dd/MM/yyyy'), '') AS DT_ENVIO, ";
            SQL += "ISNULL(D.NM_RAZAO, '') AS CLIENTE, ISNULL(A.IDARMAZEM,'') AS IDARMAZEM, ISNULL(C.NM_RAZAO, '') AS PARCEIRO, ";
            SQL += "ISNULL(F.CRITICA,'') AS OCORRENCIA ";
            SQL += "FROM TB_GER_EMAIL A ";
            SQL += "LEFT JOIN TB_BL B ON A.IDPROCESSO = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON A.IDPARCEIRO = C.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO D ON A.IDCLIENTE = D.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_TIPOAVISO E ON A.IDTIPOAVISO = E.IDTIPOAVISO ";
            SQL += "LEFT JOIN TB_GER_LOG F ON A.AUTONUM = F.AUTONUM_EMAIL ";
            SQL += "WHERE A.AUTONUM IS NOT NULL ";
            SQL += "AND E.ORIGEM = 'FN' ";
            SQL += "" + filtro + " ";
            SQL += "" + enviado + " ";
            SQL += "" + nenviado + " ";
            SQL += "" + dtgerado + " ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string listarEmailAgendadoFinanceiro(string filtro, string consulta, string enviado, string nenviado, string dtgerado)
        {
            switch (filtro)
            {
                case "1":
                    filtro = "AND B.NR_PROCESSO LIKE '" + consulta + "%' ";
                    break;
                case "2":
                    filtro = "AND C.NM_RAZAO LIKE '" + consulta + "%' ";
                    break;
                case "3":
                    filtro = "AND E.NMTIPOAVISO LIKE '%" + consulta + "%' ";
                    break;
                default:
                    filtro = "";
                    break;
            }

            if (enviado == "1" && nenviado == "1")
            {
                enviado = "";
                nenviado = "";
            }
            else
            {
                switch (enviado)
                {
                    case "1":
                        enviado = "AND A.DT_GERACAO_EMAIL IS NULL ";
                        break;
                    default:
                        enviado = "";
                        break;
                }

                switch (nenviado)
                {
                    case "1":
                        nenviado = "AND A.DT_GERACAO_EMAIL IS NOT NULL ";
                        break;
                    default:
                        nenviado = "";
                        break;
                }
            }

            switch (dtgerado)
            {
                case "1":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) = CONVERT (date, GETDATE()) ";
                    break;
                case "2":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) >= DATEADD(day, -7, CONVERT (date, GETDATE())) ";
                    break;
                case "3":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) >= DATEADD(day, -30, CONVERT (date, GETDATE())) ";
                    break;
                case "4":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) >= DATEADD(day, -60, CONVERT (date, GETDATE())) ";
                    break;
                case "5":
                    dtgerado = "AND CONVERT (date,A.DT_SOLICITACAO) >= DATEADD(day, -90, CONVERT (date, GETDATE())) ";
                    break;
            }
            string SQL;
            SQL = "SELECT A.ID_SOLICITACAO_EMAIL, ISNULL(B.NR_PROCESSO,'') AS NR_PROCESSO, ISNULL(M.NR_BL,'') AS NR_BL, ISNULL(E.NMTIPOAVISO,'') AS NMTIPOAVISO, ISNULL(FORMAT(A.DT_SOLICITACAO,'dd/MM/yyyy'),'') AS DT_SOLICITACAO, ";
            SQL += "ISNULL(FORMAT(A.DT_START,'dd/MM/yyyy'),'') AS DT_START, ISNULL(FORMAT(A.DT_GERACAO_EMAIL,'dd/MM/yyyy'),'') AS DT_GERACAO_EMAIL, ";
            SQL += "ISNULL(FORMAT(A.DT_CANCELAMENTO,'dd/MM/yyyy'),'') AS DT_CANCELAMENTO, ISNULL(A.OB_CANCELAMENTO,'') AS OB_CANCELAMENTO, ";
            SQL += "ISNULL(C.NM_RAZAO,'') AS CLIENTE, ISNULL(CONVERT(VARCHAR,A.IDARMAZEM),'') AS IDARMAZEM, ISNULL(D.NM_RAZAO,'') AS PARCEIRO ";
            SQL += "FROM TB_solicitacao_email A ";
            SQL += "LEFT JOIN TB_BL B ON A.IDPROCESSO = B.ID_BL ";
            SQL += "LEFT JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON B.ID_PARCEIRO_CLIENTE = C.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO D ON A.IDPARCEIRO = D.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_TIPOAVISO E ON A.IDTIPOAVISO = E.IDTIPOAVISO ";
            SQL += "WHERE ORIGEM = 'FN' ";
            SQL += "" + filtro + " ";
            SQL += "" + enviado + " ";
            SQL += "" + nenviado + " ";
            SQL += "" + dtgerado + " ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string listarPremiacao(string dtCompetencia, string quinzena)
        {
            string SQL;
            SQL = "SELECT ID_CABECALHO_COMISSAO_NACIONAL FROM TB_CABECALHO_COMISSAO_NACIONAL WHERE DT_COMPETENCIA = '" + dtCompetencia + "' and NR_QUINZENA = '" + quinzena + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string idPremiacao = listTable.Rows[0]["ID_CABECALHO_COMISSAO_NACIONAL"].ToString();
           
                SQL = "SELECT AGENTE, COMPETENCIA, PROCESSO, INDICADOR, MBL, HBL, CLIENTE, ESTUFAGEM, ";
                SQL += "MOEDA, VALOR, CAMBIO, PREMIACAO, RATEIO, TOTAL FROM FN_INDICADOR_NACIONAL_RATEIO(" + idPremiacao + ") ORDER BY AGENTE";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);

                return JsonConvert.SerializeObject(listTable2);
            }
            else
            {
                return null;
            }
        }
    }
}
