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
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesF, vendedor, tipo) + " ";
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
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesF, vendedor, tipo) + " ";
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
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesF, vendedor, tipo) + " ";
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
            SQL += "" + CarregaFiltro(anoI, anoF, mesI, mesF, vendedor, tipo) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregaFiltroPizza(string anoI, string mesI, int vendedor, string tipo)
        {
            string SQL;
            SQL = "and B.DT_CANCELAMENTO IS NULL ";
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
            return SQL;
        }

        [WebMethod]
        public string CarregarEstatisticaPizza(string anoI, string mesI, int vendedor, string tipo)
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
            SQL += "" + CarregaFiltroPizza(anoI, mesI, vendedor, tipo) + " ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregarProcessosPizza(string anoI, string mesI, int vendedor, string tipo)
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
            SQL += "" + CarregaFiltroPizza(anoI, mesI, vendedor, tipo) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, ";
            SQL += "A.ANO ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }


        [WebMethod]
        public string CarregarQtdCntrPizza(string anoI, string mesI, int vendedor, string tipo)
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
            SQL += "" + CarregaFiltroPizza(anoI, mesI, vendedor, tipo) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

        [WebMethod]
        public string CarregarTeusPizza(string anoI, string mesI, int vendedor, string tipo)
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
            SQL += "" + CarregaFiltroPizza(anoI, mesI, vendedor, tipo) + " ";
            SQL += "and B.DT_CANCELAMENTO IS NULL ";
            SQL += "GROUP BY A.MES, A.ANO ";
            SQL += "ORDER BY A.ANO, A.MES ";

            DataTable total = new DataTable();

            total = DBS.List(SQL);

            return JsonConvert.SerializeObject(total);
        }

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
            SQL = "WHERE SUBSTRING(C.NR_PROCESSO,10,2)>= '18' AND C.ID_BL_MASTER IS NOT NULL ";
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
        public string escreverCorpoEmail(string idProcessoMaster)
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
                    dtgerado = "AND A.DT_GERACAO = CONVERT (date, GETDATE()) ";
                    break;
                case "2":
                    dtgerado = "AND A.DT_GERACAO >= DATEADD(day, -7, CONVERT (date, GETDATE())) ";
                    break;
                case "3":
                    dtgerado = "AND A.DT_GERACAO >= DATEADD(day, -30, CONVERT (date, GETDATE())) ";
                    break;
                case "4":
                    dtgerado = "AND A.DT_GERACAO >= DATEADD(day, -60, CONVERT (date, GETDATE())) ";
                    break;
                case "5":
                    dtgerado = "AND A.DT_GERACAO >= DATEADD(day, -90, CONVERT (date, GETDATE())) ";
                    break;
            }
            string SQL;
            SQL = "SELECT A.AUTONUM AS IDEMAIL, ISNULL(B.NR_PROCESSO,'') AS PROCESSO, ISNULL(E.NMTIPOAVISO,'') AS NMTIPOAVISO , ISNULL(FORMAT(A.DT_GERACAO,'dd/MM/yyyy'),'') AS DT_GERACAO, ";
            SQL += "ISNULL(FORMAT(A.DT_START, 'dd/MM/yyyy'), '') AS PREVISAO, ISNULL(FORMAT(A.DT_ENVIO, 'dd/MM/yyyy'), '') AS DT_ENVIO, ";
            SQL += "ISNULL(D.NM_RAZAO, '') AS CLIENTE, ISNULL(A.IDARMAZEM,'') AS IDARMAZEM, ISNULL(C.NM_RAZAO, '') AS PARCEIRO ";
            SQL += "FROM TB_GER_EMAIL A ";
            SQL += "LEFT JOIN TB_BL B ON A.IDPROCESSO = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON A.IDPARCEIRO = C.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO D ON A.IDCLIENTE = D.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_TIPOAVISO E ON A.IDTIPOAVISO = E.IDTIPOAVISO ";
            SQL += "WHERE A.AUTONUM IS NOT NULL ";
            SQL += ""+filtro+" ";
            SQL += ""+enviado+" ";
            SQL += ""+nenviado+" ";
            SQL += ""+dtgerado+" ";
            
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
                    dtgerado = "AND A.DT_SOLICITACAO = CONVERT (date, GETDATE()) ";
                    break;
                case "2":
                    dtgerado = "AND A.DT_SOLICITACAO >= DATEADD(day, -7, CONVERT (date, GETDATE())) ";
                    break;
                case "3":
                    dtgerado = "AND A.DT_SOLICITACAO >= DATEADD(day, -30, CONVERT (date, GETDATE())) ";
                    break;
                case "4":
                    dtgerado = "AND A.DT_SOLICITACAO >= DATEADD(day, -60, CONVERT (date, GETDATE())) ";
                    break;
                case "5":
                    dtgerado = "AND A.DT_SOLICITACAO >= DATEADD(day, -90, CONVERT (date, GETDATE())) ";
                    break;
            }
            string SQL;
            SQL = "SELECT A.ID_SOLICITACAO_EMAIL, B.NR_PROCESSO, M.NR_BL, E.NMTIPOAVISO, ISNULL(FORMAT(A.DT_SOLICITACAO,'dd/MM/yyyy'),'') AS DT_SOLICITACAO, ";
            SQL += "ISNULL(FORMAT(A.DT_START,'dd/MM/yyyy'),'') AS DT_START, ISNULL(FORMAT(A.DT_GERACAO_EMAIL,'dd/MM/yyyy'),'') AS DT_GERACAO_EMAIL, ";
            SQL += "ISNULL(FORMAT(A.DT_CANCELAMENTO,'dd/MM/yyyy'),'') AS DT_CANCELAMENTO, ISNULL(A.OB_CANCELAMENTO,'') AS OB_CANCELAMENTO, ";
            SQL += "C.NM_RAZAO AS CLIENTE, ISNULL(CONVERT(VARCHAR,A.IDARMAZEM),'') AS IDARMAZEM, ISNULL(D.NM_RAZAO,'') AS PARCEIRO ";
            SQL += "FROM TB_solicitacao_email A ";
            SQL += "LEFT JOIN TB_BL B ON A.IDPROCESSO = B.ID_BL ";
            SQL += "LEFT JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON B.ID_PARCEIRO_CLIENTE = C.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO D ON A.IDPARCEIRO = D.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_TIPOAVISO E ON A.IDTIPOAVISO = E.IDTIPOAVISO ";
            SQL += "WHERE M.NR_BL IS NOT NULL ";
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
            SQL = "SELECT DT_CANCELAMENTO FROM TB_SOLICITACAO_EMAIL WHERE ID_SOLICITACAO_EMAIL = '"+idEmail+"' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if(listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "") 
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
            SQL = "UPDATE TB_SOLICITACAO_EMAIL SET DT_CANCELAMENTO = '"+ sqlFormattedDate + "', OB_CANCELAMENTO = 'CANCELADO PELO USUÁRIO' WHERE ID_SOLICITACAO_EMAIL = '" + idEmail + "' ";
            string cancelar = DBS.ExecuteScalar(SQL);

            return "ok";
        }

        [WebMethod]
        public string reativarAgendamento(string idEmail)
        {
            string SQL;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            SQL = "UPDATE TB_SOLICITACAO_EMAIL SET DT_GERACAO_EMAIL = NULL, DT_CANCELAMENTO = null, OB_CANCELAMENTO = NULL WHERE ID_SOLICITACAO_EMAIL = '"+idEmail+"' ";
            string reativar = DBS.ExecuteScalar(SQL);

            return "ok";
        }

        [WebMethod]
        public string checarDiretorio()
        {
            string SQL;
            SQL = "SELECT PATHDOCUMENTOS FROM TB_AVISOPARAM WHERE IDTIPOAVISOPARAM=1 ";
            string PathDocumentos = DBS.ExecuteScalar(SQL);
            string path = "C:\\FCA\\DOCUMENTOS";
            if (Directory.Exists(path) == false)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        [WebMethod]
        public string criarDiretorio(string idProcesso, string path, string tipoaviso)
        {
            string SQL;
            string result;
            SQL = "SELECT IDTIPOAVISO, TPPROCESSO FROM TB_TIPOAVISO WHERE IDTIPOAVISO = '" + tipoaviso + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string tipoprocesso = listTable.Rows[0]["TPPROCESSO"].ToString();
            string idtipoaviso = listTable.Rows[0]["IDTIPOAVISO"].ToString();

            SQL = "SELECT B.NM_TIPO_ESTUFAGEM, D.NM_VIATRANSPORTE, ";
            SQL += "A.DT_PREVISAO_EMBARQUE, A.DT_PREVISAO_CHEGADA ";
            SQL += "from TB_BL A ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM B ON A.ID_TIPO_ESTUFAGEM = B.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN TB_SERVICO C ON A.ID_SERVICO = C.ID_SERVICO ";
            SQL += "LEFT JOIN TB_VIATRANSPORTE D ON C.ID_VIATRANSPORTE = D.ID_VIATRANSPORTE ";
            SQL += "WHERE A.ID_BL = '" + idProcesso + "' ";
            DataTable verifica = new DataTable();
            verifica = DBS.List(SQL);
            string tipoEstufagem = verifica.Rows[0]["NM_TIPO_ESTUFAGEM"].ToString();
            string viatransporte = verifica.Rows[0]["NM_VIATRANSPORTE"].ToString();
            string previsaoEmbarque = verifica.Rows[0]["DT_PREVISAO_EMBARQUE"].ToString();
            string previsaoChegada = verifica.Rows[0]["DT_PREVISAO_CHEGADA"].ToString();

            /*if(idtipoaviso == "2" && tipoEstufagem != "LCL"){
                result = "1";
                return JsonConvert.SerializeObject(result);
            } else if (idtipoaviso == "3" && viatransporte != "Aérea" || previsaoChegada == "" || previsaoEmbarque == "" ) {
                result = "1";
                return JsonConvert.SerializeObject(result);
            }*/



            if (listTable.Rows[0]["TPPROCESSO"].ToString() == "P")
            {

                SQL = "SELECT M.NR_BL as NRMASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idProcesso + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                string anoH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(9, 2);
                string mesH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(6, 2);
                string diretorio = "C:\\FCA\\DOCUMENTOS\\20" + anoH + "\\" + mesH + "\\" + listTable2.Rows[0]["NRHOUSE"].ToString().Replace("/", "") + "\\";
                string documentos = "C:\\UPLOADS\\" + path + "";
                string docup = "C:\\UPLOADS\\";
                if (Directory.Exists(docup) == false)
                {
                    return JsonConvert.SerializeObject("1");
                }
                if (Directory.Exists(diretorio) == false)
                {
                    DirectoryInfo di = Directory.CreateDirectory(diretorio);
                }
                
                File.Copy(documentos, diretorio + path, true);
            }
            else
            {
                SQL = "SELECT M.NR_BL as BL_MASTER, C.NR_PROCESSO AS NRHOUSE FROM TB_BL C LEFT JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL WHERE C.ID_BL = '" + idProcesso + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                string anoH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(9, 2);
                string mesH = listTable2.Rows[0]["NRHOUSE"].ToString().Substring(6, 2);
                string blmaster = listTable2.Rows[0]["BL_MASTER"].ToString();
                string diretorio = "C:\\FCA\\DOCUMENTOS\\20" + anoH + "\\" + mesH + "\\MASTER-"+blmaster+"\\";
                string documentos = "C:\\UPLOADS\\" + path+"";
                string docup = "C:\\UPLOADS\\";
                if (Directory.Exists(docup) == false)
                {
                    return JsonConvert.SerializeObject("1");
                }
                if (Directory.Exists(diretorio) == false)
                {
                    DirectoryInfo di = Directory.CreateDirectory(diretorio);
                }
                
                File.Copy(documentos, diretorio + path, true);
            }
            result = "0";
            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public string uploadArquivo(string idprocesso, string iddocumento, string arquivo, string idtipoaviso)
        {
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
                string diretorio = "C:\\FCA\\DOCUMENTOS\\20" + anoH + "\\" + mesH + "\\" + listTable2.Rows[0]["NRHOUSE"].ToString().Replace("/", "") + "\\";

                SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                SQL += "VALUES ('" + idprocesso + "',NULL,'" + iddocumento + "','" + sqlFormattedDate + "','" + diretorio + "','" + arquivo + "','"+ pathrobo + "') ";

                string upload = DBS.ExecuteScalar(SQL);

                SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                SQL += "VALUES ('"+ sqlFormattedDate + "','"+ sqlFormattedDate + "','"+ idtipoaviso + "', '"+ idprocesso + "',NULL,'"+idprocesso+"',NULL,NULL) ";

                string uploadSolicitacao = DBS.ExecuteScalar(SQL);
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
                string diretorio = "C:\\FCA\\DOCUMENTOS\\20" + anoH + "\\" + mesH + "\\MASTER-" + nrblmaster + "\\";

                SQL = "INSERT INTO TB_GER_ANEXO (IDPROCESSO, IDMASTER, IDDOCUMENTO, DTPOSTAGEM, DCPATHARQUIVO, NMARQUIVO, DCPATHARQUIVOROBO) ";
                SQL += "VALUES (NULL,'" + blmaster + "','" + iddocumento + "','" + sqlFormattedDate + "','" + diretorio + "','" + arquivo + "','" + pathrobo + "') ";
                string upload2 = DBS.ExecuteScalar(SQL);

                if (idtipoaviso == "1")
                {
                    SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                    SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + idtipoaviso + "', NULL, '"+ blmaster + "','" + idprocesso + "','"+parceiroRD+"',NULL) ";
                    string solicitacao = DBS.ExecuteScalar(SQL);
                }
                else if(idtipoaviso == "2")
                {
                    SQL = "INSERT INTO TB_SOLICITACAO_EMAIL (DT_SOLICITACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDMASTER, IDCLIENTE, IDARMAZEM, IDPARCEIRO) ";
                    SQL += "VALUES ('" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + idtipoaviso + "', NULL, '"+ blmaster + "','" + idprocesso + "',NULL,'"+parceiroD+"') ";
                    string solicitacao2 = DBS.ExecuteScalar(SQL);
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
                SQL += "WHERE A.IDPROCESSO = '"+ idprocesso + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);

                return JsonConvert.SerializeObject(listTable2);
            }
            else
            {
                SQL = "SELECT FORMAT(A.DTPOSTAGEM,'dd/MM/yyyy hh:mm:ss') AS DTPOSTAGEM, A.AUTONUM, B.NMDOCUMENTO ";
                SQL += "FROM TB_GER_ANEXO A ";
                SQL += "LEFT JOIN TB_DOCUMENTO B ON A.IDDOCUMENTO = B.IDDOCUMENTO ";
                SQL += "WHERE A.IDMASTER = '"+ blmaster + "' ";
                DataTable listTable3 = new DataTable();
                listTable3 = DBS.List(SQL);

                return JsonConvert.SerializeObject(listTable3);
            }
        }

        [WebMethod]
        public string deleterDocumentoArquivado(string documentoArquivado)
        {
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
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            SQL = "SELECT A.NR_BL, A.NR_PROCESSO, ORIGEM.NM_PORTO AS ORIGEM, DESTINO.NM_PORTO AS DESTINO,ID_PARCEIRO_CLIENTE, CLIENTE.NM_RAZAO AS CLIENTE, ";
            SQL += "CLIENTE.CNPJ, C.NM_VIATRANSPORTE as VIA ";
            SQL += "FROM TB_BL A ";
            SQL += "LEFT JOIN TB_PORTO ORIGEM ON A.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO DESTINO ON A.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += "LEFT JOIN TB_PARCEIRO CLIENTE ON A.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
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

            if (nmVia == "MARÍTIMA")
            {
                SQL = "SELECT ID_DESTINATARIO_MAR, ORIGEM FROM TB_TIPOAVISO WHERE IDTIPOAVISO = 12";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                destinatario = listTable2.Rows[0]["ID_DESTINATARIO_MAR"].ToString();
                origem = listTable2.Rows[0]["ORIGEM"].ToString();
            }
            else {
                if (nmVia == "AÉREA")
                {
                    SQL = "SELECT ID_DESTINATARIO_AER, ORIGEM FROM TB_TIPOAVISO WHERE ID_TIPOAVISO = 12";
                    DataTable listTable3 = new DataTable();
                    listTable3 = DBS.List(SQL);
                    destinatario = listTable3.Rows[0]["ID_DESTINATARIO_AER"].ToString();
                    origem = listTable3.Rows[0]["ORIGEM"].ToString();
                }
            }

            SQL = "INSERT INTO TB_GER_EMAIL (ASSUNTO, CORPO, DT_GERACAO, DT_START, IDTIPOAVISO, IDPROCESSO, IDCLIENTE, TPORIGEM, ID_DESTINATARIO) ";
            SQL += "VALUES ('"+nrProcesso+" - AVISO DE EMBARQUE - "+origemP+" X "+destinoP+" - HBL: "+nrHouse+"<br>"+nmCliente+" - "+cnpj+"', ";
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
            SQL += "WHERE A.IDTIPOAVISO = '"+ idtipoaviso + "' ";
            SQL += "ORDER BY B.NMDOCUMENTO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string visualizarEmail(string idProcesso)
        {
            string SQL;

            SQL = "SELECT ASSUNTO, CORPO FROM TB_GER_EMAIL C WHERE C.AUTONUM = '" + idProcesso + "' ";
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
            if (listTable.Rows[0]["DT_ENVIO"].ToString() == "" && listTable.Rows[0]["ENVIADO"].ToString() == "")
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
            SQL = "SELECT C.DT_ENVIO, C.ENVIADO FROM TB_GER_EMAIL C WHERE C.AUTONUM = '" + idProcesso + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable.Rows[0]["DT_ENVIO"].ToString() != "" && listTable.Rows[0]["ENVIADO"].ToString() != "")
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
