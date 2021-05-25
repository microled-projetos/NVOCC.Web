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
    /// Descrição resumida de DemurrageService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    [System.Web.Script.Services.ScriptService]
    public class DemurrageService : System.Web.Services.WebService
    {

        [WebMethod]
        public string ListarWeek()
        {
            string SQL;
            SQL = "SELECT A.ID_WEEK, A.NM_WEEK, B.NM_PORTO AS NMPORTOORIGEM, C.NM_PORTO AS NMPORTODESTINO, ";
            SQL += "format(A.DT_ETD,'yyyy/MM/dd') as DT_ETD, format(A.DT_CUTOFF,'yyyy/MM/dd') as DT_CUTOFF ";
            SQL += "FROM TB_WEEK A ";
            SQL += "LEFT JOIN TB_PORTO B ON A.ID_PORTO_ORIGEM_LOCAL = B.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO C ON A.ID_PORTO_ORIGEM_DESTINO = C.ID_PORTO ";


            DataTable listarweek = new DataTable();
            listarweek = DBS.List(SQL);
            return JsonConvert.SerializeObject(listarweek);
        }

        [WebMethod]
        public string ListarDemurrageContainer()
        {
            string SQL;
            SQL = "SELECT ID_TABELA_DEMURRAGE, NM_TIPO_CONTAINER, FORMAT(DT_VALIDADE_FINAL,'dd/MM/yyyy') AS DT_VALIDADE_FINAL_FORMAT ";
            SQL += "FROM TB_TABELA_DEMURRAGE ";
            SQL += "JOIN TB_TIPO_CONTAINER ";
            SQL += "ON dbo.TB_TABELA_DEMURRAGE.ID_TIPO_CONTAINER = dbo.TB_TIPO_CONTAINER.ID_TIPO_CONTAINER ";

            DataTable listarDemurrageContainer = new DataTable();
            listarDemurrageContainer = DBS.List(SQL);
            return JsonConvert.SerializeObject(listarDemurrageContainer);
        }

        [WebMethod]
        public string CadastrarDemurrageContainer(DemurragesCls dados)
        {
            int qtdfreetime = Convert.ToInt32(dados.QT_DIAS_FREETIME);
            int qtdias01 = Convert.ToInt32(dados.QT_DIAS_01);
            double vlVenda01 = Convert.ToDouble(dados.VL_VENDA_01);
            int qtdias02 = Convert.ToInt32(dados.QT_DIAS_02);
            double vlVenda02 = Convert.ToDouble(dados.VL_VENDA_02);
            int qtdias03 = Convert.ToInt32(dados.QT_DIAS_03);
            double vlVenda03 = Convert.ToDouble(dados.VL_VENDA_03);
            int qtdias04 = Convert.ToInt32(dados.QT_DIAS_04);
            double vlVenda04 = Convert.ToDouble(dados.VL_VENDA_04);
            int qtdias05 = Convert.ToInt32(dados.QT_DIAS_05);
            double vlVenda05 = Convert.ToDouble(dados.VL_VENDA_05);
            int qtdias06 = Convert.ToInt32(dados.QT_DIAS_06);
            double vlVenda06 = Convert.ToDouble(dados.VL_VENDA_06);
            int qtdias07 = Convert.ToInt32(dados.QT_DIAS_07);
            double vlVenda07 = Convert.ToDouble(dados.VL_VENDA_07);
            int qtdias08 = Convert.ToInt32(dados.QT_DIAS_08);
            double vlVenda08 = Convert.ToDouble(dados.VL_VENDA_08);

            if (dados.ID_PARCEIRO_TRANSPORTADOR.ToString() == "")
            {
                return "0";
            }

            if (dados.ID_TIPO_CONTAINER.ToString() == "")
            {
                return "0";
            }

            if (dados.DT_VALIDADE_INICIAL.ToString() == "")
            {
                return "0";
            }

            if (qtdfreetime < 0 || dados.QT_DIAS_FREETIME == "")
            {
                return "0";
            }

            if (dados.ID_MOEDA.ToString() == "" || dados.ID_MOEDA < 0)
            {
                return "0";
            }

            if (qtdias01 < 0 || dados.QT_DIAS_01 == "")
            {
                return "0";
            }

            if (dados.VL_VENDA_01.ToString() == "" || vlVenda01 < 0)
            {
                return "0";
            }

            if (qtdias02 < 0 || dados.QT_DIAS_02 == "")
            {
                return "0";
            }

            if (dados.VL_VENDA_02.ToString() == "" || vlVenda02 < 0)
            {
                return "0";
            }

            if (qtdias03 < 0 || dados.QT_DIAS_03 == "")
            {
                return "0";
            }

            if (dados.VL_VENDA_03.ToString() == "" || vlVenda03 < 0)
            {
                return "0";
            }

            if (qtdias04 < 0 || dados.QT_DIAS_04 == "")
            {
                return "0";
            }

            if (dados.VL_VENDA_04.ToString() == "" || vlVenda04 < 0)
            {
                return "0";
            }

            if (qtdias05 < 0 || dados.QT_DIAS_05 == "")
            {
                return "0";
            }

            if (dados.VL_VENDA_05.ToString() == "" || vlVenda05 < 0)
            {
                return "0";
            }

            if (qtdias06 < 0 || dados.QT_DIAS_06 == "")
            {
                return "0";
            }

            if (dados.VL_VENDA_06.ToString() == "" || vlVenda06 < 0)
            {
                return "0";
            }

            if (qtdias07 < 0 || dados.QT_DIAS_07 == "")
            {
                return "0";
            }

            if (dados.VL_VENDA_07.ToString() == "" || vlVenda07 < 0)
            {
                return "0";
            }

            if (qtdias08 < 0 || dados.QT_DIAS_08 == "")
            {
                return "0";
            }

            if (dados.VL_VENDA_08.ToString() == "" || vlVenda08 < 0)
            {
                return "0";
            }

            if (qtdias02 > 0)
            {
                if (vlVenda02 > 0)
                {
                    if (vlVenda01 == 0 || qtdias01 == 0)
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }

            if (qtdias03 > 0)
            {
                if (vlVenda03 > 0)
                {
                    if (vlVenda01 == 0 || vlVenda02 == 0 || qtdias01 == 0 || qtdias02 == 0)
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }

            if (qtdias04 > 0)
            {
                if (vlVenda04 > 0)
                {
                    if (vlVenda01 == 0 || vlVenda02 == 0 || vlVenda03 == 0 || qtdias01 == 0 || qtdias02 == 0 || qtdias03 == 0)
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }

            if (qtdias05 > 0)
            {
                if (vlVenda05 > 0)
                {
                    if (vlVenda01 == 0 || vlVenda02 == 0 || vlVenda03 == 0 || vlVenda04 == 0 || qtdias01 == 0 || qtdias02 == 0 || qtdias03 == 0 || qtdias04 == 0)
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }

            if (qtdias06 > 0)
            {
                if (vlVenda06 > 0)
                {
                    if (vlVenda01 == 0 || vlVenda02 == 0 || vlVenda03 == 0 || vlVenda04 == 0 || vlVenda05 == 0 || qtdias01 == 0 || qtdias02 == 0 || qtdias03 == 0 || qtdias04 == 0 || qtdias05 == 0)
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }

            if (qtdias07 > 0)
            {
                if (vlVenda07 > 0)
                {
                    if (vlVenda01 == 0 || vlVenda02 == 0 || vlVenda03 == 0 || vlVenda04 == 0 || vlVenda05 == 0 || vlVenda06 == 0 || qtdias01 == 0 || qtdias02 == 0 || qtdias03 == 0 || qtdias04 == 0 || qtdias05 == 0 || qtdias06 == 0)
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }

            if (qtdias08 > 0)
            {
                if (vlVenda08 > 0)
                {
                    if (vlVenda01 == 0 || vlVenda02 == 0 || vlVenda03 == 0 || vlVenda04 == 0 || vlVenda05 == 0 || vlVenda06 == 0 || vlVenda07 == 0 || qtdias01 == 0 || qtdias02 == 0 || qtdias03 == 0 || qtdias04 == 0 || qtdias05 == 0 || qtdias06 == 0 || qtdias07 == 0)
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            string vlvenda01 = dados.VL_VENDA_01.ToString().Replace(',', '.');
            string vlvenda02 = dados.VL_VENDA_02.ToString().Replace(',', '.');
            string vlvenda03 = dados.VL_VENDA_03.ToString().Replace(',', '.');
            string vlvenda04 = dados.VL_VENDA_04.ToString().Replace(',', '.');
            string vlvenda05 = dados.VL_VENDA_05.ToString().Replace(',', '.');
            string vlvenda06 = dados.VL_VENDA_06.ToString().Replace(',', '.');
            string vlvenda07 = dados.VL_VENDA_07.ToString().Replace(',', '.');
            string vlvenda08 = dados.VL_VENDA_08.ToString().Replace(',', '.');
            string SQL;
            SQL = "SELECT * FROM TB_TABELA_DEMURRAGE WHERE ID_PARCEIRO_TRANSPORTADOR = '" + dados.ID_PARCEIRO_TRANSPORTADOR + "' AND ";
            SQL += "ID_TIPO_CONTAINER ='" + dados.ID_TIPO_CONTAINER + "' AND DT_VALIDADE_INICIAL = '" + dados.DT_VALIDADE_INICIAL + "' ";
            DataTable consulta = new DataTable();
            consulta = DBS.List(SQL);
            if (consulta == null)
            {
                SQL = "insert into TB_TABELA_DEMURRAGE (ID_PARCEIRO_TRANSPORTADOR,ID_TIPO_CONTAINER,DT_VALIDADE_INICIAL,QT_DIAS_FREETIME, ";
                SQL += "ID_MOEDA, FL_ESCALONADA ,QT_DIAS_01 ,VL_VENDA_01 ,QT_DIAS_02 ,VL_VENDA_02 ,QT_DIAS_03 ,VL_VENDA_03 ,QT_DIAS_04, ";
                SQL += "VL_VENDA_04 ,QT_DIAS_05 ,VL_VENDA_05 ,QT_DIAS_06 ,VL_VENDA_06 ,QT_DIAS_07 ,VL_VENDA_07 ,QT_DIAS_08 ,VL_VENDA_08) ";
                SQL += "VALUES( '" + dados.ID_PARCEIRO_TRANSPORTADOR + "','" + dados.ID_TIPO_CONTAINER + "', ";
                SQL += "'" + dados.DT_VALIDADE_INICIAL + "','" + dados.QT_DIAS_FREETIME + "','" + dados.ID_MOEDA + "','" + dados.FL_ESCALONADA + "', ";
                SQL += "'" + qtdias01 + "','" + vlVenda01 + "', '" + qtdias02 + "','" + vlVenda02 + "', ";
                SQL += "'" + qtdias03 + "','" + vlVenda03 + "', '" + qtdias04 + "','" + vlVenda04 + "', ";
                SQL += "'" + qtdias05 + "','" + vlVenda05 + "', '" + qtdias06 + "','" + vlVenda06 + "', ";
                SQL += "'" + qtdias07 + "','" + vlVenda07 + "', '" + qtdias08 + "','" + vlVenda08 + "') ";

                string demu = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                return "2";
            }
        }

        [WebMethod]
        public string BuscarDemurrage(int Id)
        {

            string SQL;
            SQL = "SELECT ID_TABELA_DEMURRAGE, ID_PARCEIRO_TRANSPORTADOR, ID_TIPO_CONTAINER, FORMAT(DT_VALIDADE_INICIAL,'yyyy-MM-dd') AS DT_VALIDADE_INICIAL_FORMAT, ";
            SQL += "QT_DIAS_FREETIME, ID_MOEDA, FL_ESCALONADA, FL_INICIO_CHEGADA, QT_DIAS_01, VL_VENDA_01, QT_DIAS_02, VL_VENDA_02, QT_DIAS_03, VL_VENDA_03, ";
            SQL += "QT_DIAS_04, VL_VENDA_04, QT_DIAS_05, VL_VENDA_05, QT_DIAS_06, VL_VENDA_06, QT_DIAS_07, VL_VENDA_07, QT_DIAS_08, VL_VENDA_08 ";
            SQL += "FROM TB_TABELA_DEMURRAGE ";
            SQL += "WHERE ID_TABELA_DEMURRAGE = '" + Id + "'";

            DataTable carregarDados = new DataTable();
            carregarDados = DBS.List(SQL);
            DemurragesCls resultado = new DemurragesCls();
            resultado.ID_TABELA_DEMURRAGE = (int)carregarDados.Rows[0]["ID_TABELA_DEMURRAGE"];
            resultado.ID_PARCEIRO_TRANSPORTADOR = (int)carregarDados.Rows[0]["ID_PARCEIRO_TRANSPORTADOR"];
            resultado.ID_TIPO_CONTAINER = (int)carregarDados.Rows[0]["ID_TIPO_CONTAINER"];
            resultado.DT_VALIDADE_INICIAL = carregarDados.Rows[0]["DT_VALIDADE_INICIAL_FORMAT"].ToString();
            resultado.QT_DIAS_FREETIME = carregarDados.Rows[0]["QT_DIAS_FREETIME"].ToString();
            resultado.ID_MOEDA = (int)carregarDados.Rows[0]["ID_MOEDA"];
            resultado.FL_ESCALONADA = carregarDados.Rows[0]["FL_ESCALONADA"].ToString();
            resultado.FL_INICIO_CHEGADA = carregarDados.Rows[0]["FL_INICIO_CHEGADA"].ToString();
            resultado.QT_DIAS_01 = carregarDados.Rows[0]["QT_DIAS_01"].ToString();
            resultado.VL_VENDA_01 = carregarDados.Rows[0]["VL_VENDA_01"].ToString();
            resultado.QT_DIAS_02 = carregarDados.Rows[0]["QT_DIAS_02"].ToString();
            resultado.VL_VENDA_02 = carregarDados.Rows[0]["VL_VENDA_02"].ToString();
            resultado.QT_DIAS_03 = carregarDados.Rows[0]["QT_DIAS_03"].ToString();
            resultado.VL_VENDA_03 = carregarDados.Rows[0]["VL_VENDA_03"].ToString();
            resultado.QT_DIAS_04 = carregarDados.Rows[0]["QT_DIAS_04"].ToString();
            resultado.VL_VENDA_04 = carregarDados.Rows[0]["VL_VENDA_04"].ToString();
            resultado.QT_DIAS_05 = carregarDados.Rows[0]["QT_DIAS_05"].ToString();
            resultado.VL_VENDA_05 = carregarDados.Rows[0]["VL_VENDA_05"].ToString();
            resultado.QT_DIAS_06 = carregarDados.Rows[0]["QT_DIAS_06"].ToString();
            resultado.VL_VENDA_06 = carregarDados.Rows[0]["VL_VENDA_06"].ToString();
            resultado.QT_DIAS_07 = carregarDados.Rows[0]["QT_DIAS_07"].ToString();
            resultado.VL_VENDA_07 = carregarDados.Rows[0]["VL_VENDA_07"].ToString();
            resultado.QT_DIAS_08 = carregarDados.Rows[0]["QT_DIAS_08"].ToString();
            resultado.VL_VENDA_08 = carregarDados.Rows[0]["VL_VENDA_08"].ToString();

            return JsonConvert.SerializeObject(resultado);

        }

        [WebMethod]
        public string DemurrageList()
        {
            string SQL;
            SQL = "SELECT ID_TABELA_DEMURRAGE, NM_TIPO_CONTAINER FROM TB_TABELA_DEMURRAGE JOIN TB_TIPO_CONTAINER ";
            SQL += "ON TB_TABELA_DEMURRAGE.ID_TIPO_CONTAINER = TB_TIPO_CONTAINER.ID_TIPO_CONTAINER ";

            DataTable demurrageList = new DataTable();
            demurrageList = DBS.List(SQL);
            return JsonConvert.SerializeObject(demurrageList);
        }

        [WebMethod]
        public string EditarDemurrageContainer(DemurragesCls dadosEdit)
        {
            int qtdfreetime = Convert.ToInt32(dadosEdit.QT_DIAS_FREETIME);
            int qtdias01 = Convert.ToInt32(dadosEdit.QT_DIAS_01);
            double vlVenda01 = Convert.ToDouble(dadosEdit.VL_VENDA_01);
            if (dadosEdit.ID_PARCEIRO_TRANSPORTADOR.ToString() == "")
            {
                return "0";
            }

            if (dadosEdit.ID_TIPO_CONTAINER.ToString() == "")
            {
                return "0";
            }

            if (dadosEdit.DT_VALIDADE_INICIAL.ToString() == "")
            {
                return "0";
            }

            if (qtdfreetime <= 0 || dadosEdit.QT_DIAS_FREETIME == "")
            {
                return "0";
            }

            if (dadosEdit.ID_MOEDA.ToString() == "" || dadosEdit.ID_MOEDA < 0)
            {
                return "0";
            }

            if (qtdias01 <= 0 || dadosEdit.QT_DIAS_01 == "")
            {
                return "0";
            }

            if (dadosEdit.VL_VENDA_01.ToString() == "" || vlVenda01 < 0)
            {
                return "0";
            }

            string SQL;
            string vlvenda01 = dadosEdit.VL_VENDA_01.ToString().Replace(',', '.');
            string vlvenda02 = dadosEdit.VL_VENDA_02.ToString().Replace(',', '.');
            string vlvenda03 = dadosEdit.VL_VENDA_03.ToString().Replace(',', '.');
            string vlvenda04 = dadosEdit.VL_VENDA_04.ToString().Replace(',', '.');
            string vlvenda05 = dadosEdit.VL_VENDA_05.ToString().Replace(',', '.');
            string vlvenda06 = dadosEdit.VL_VENDA_06.ToString().Replace(',', '.');
            string vlvenda07 = dadosEdit.VL_VENDA_07.ToString().Replace(',', '.');
            string vlvenda08 = dadosEdit.VL_VENDA_08.ToString().Replace(',', '.');

            SQL = "SELECT * FROM TB_TABELA_DEMURRAGE WHERE ID_PARCEIRO_TRANSPORTADOR = '" + dadosEdit.ID_PARCEIRO_TRANSPORTADOR + "' AND ";
            SQL += "ID_TIPO_CONTAINER ='" + dadosEdit.ID_TIPO_CONTAINER + "' AND DT_VALIDADE_INICIAL = '" + dadosEdit.DT_VALIDADE_INICIAL + "' ";
            DataTable consulta = new DataTable();
            consulta = DBS.List(SQL);
            if (consulta == null)
            {
                SQL = "UPDATE TB_TABELA_DEMURRAGE SET ID_PARCEIRO_TRANSPORTADOR = '" + dadosEdit.ID_PARCEIRO_TRANSPORTADOR + "' , ID_TIPO_CONTAINER = '" + dadosEdit.ID_TIPO_CONTAINER + "', ";
                SQL += "DT_VALIDADE_INICIAL = '" + dadosEdit.DT_VALIDADE_INICIAL + "', QT_DIAS_FREETIME = '" + dadosEdit.QT_DIAS_FREETIME + "', ";
                SQL += "ID_MOEDA = '" + dadosEdit.ID_MOEDA + "', FL_ESCALONADA ='" + dadosEdit.FL_ESCALONADA + "' , QT_DIAS_01 ='" + qtdias01 + "' , VL_VENDA_01 = '" + vlvenda01 + "', ";
                SQL += "QT_DIAS_02 = '" + dadosEdit.QT_DIAS_02 + "', VL_VENDA_02 = '" + vlvenda02 + "', ";
                SQL += "QT_DIAS_03 = '" + dadosEdit.QT_DIAS_03 + "', VL_VENDA_03 = '" + vlvenda03 + "', ";
                SQL += "QT_DIAS_04 = '" + dadosEdit.QT_DIAS_04 + "', VL_VENDA_04 = '" + vlvenda04 + "', QT_DIAS_05 = '" + dadosEdit.QT_DIAS_05 + "', ";
                SQL += "VL_VENDA_05 = '" + vlvenda05 + "', QT_DIAS_06 = '" + dadosEdit.QT_DIAS_06 + "', ";
                SQL += "VL_VENDA_06 = '" + vlvenda06 + "', QT_DIAS_07 = '" + dadosEdit.QT_DIAS_07 + "', ";
                SQL += "VL_VENDA_07 = '" + vlvenda07 + "', QT_DIAS_08 = '" + dadosEdit.QT_DIAS_08 + "', ";
                SQL += "VL_VENDA_08 = '" + vlvenda08 + "', FL_INICIO_CHEGADA = '" + dadosEdit.FL_INICIO_CHEGADA + "' ";
                SQL += "WHERE ID_TABELA_DEMURRAGE = '" + dadosEdit.ID_TABELA_DEMURRAGE + "' ";

                string editDemurrage = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                return "2";
            }
        }

        [WebMethod]
        public string DeletarDemurrage(int Id)
        {
            string SQL;
            SQL = "DELETE FROM TB_TABELA_DEMURRAGE WHERE ID_TABELA_DEMURRAGE = '" + Id + "' ";
            string deleteDemurrage = DBS.ExecuteScalar(SQL);
            if (deleteDemurrage == null)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        [WebMethod]
        public string listarTabela()
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
            SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
            SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
            SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
            SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
            SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
            SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string filtrarTabela(int idFilter, string Filter, string Ativo, string Finalizado)
        {
            string SQL;
            DataTable listTable = new DataTable();


            if (idFilter == 1)
            {
                if (Ativo == "1")
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND PFCL.NR_PROCESSO LIKE '" + Filter + "%'";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND PFCL.NR_PROCESSO LIKE '" + Filter + "%' AND PFCL.FL_DEMURRAGE_FINALIZADA = 0";
                        listTable = DBS.List(SQL);
                    }
                }
                else
                {
                    if (Finalizado == "1")
                    {

                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND PFCL.NR_PROCESSO LIKE '" + Filter + "%' AND PFCL.FL_DEMURRAGE_FINALIZADA = 1";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else if (idFilter == 2)
            {
                if (Ativo == "1")
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND PFCL.NR_CNTR LIKE '" + Filter + "%'";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND PFCL.NR_CNTR LIKE '" + Filter + "%' AND PFCL.FL_DEMURRAGE_FINALIZADA = 0";
                        listTable = DBS.List(SQL);
                    }
                }
                else
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND PFCL.NR_CNTR LIKE '" + Filter + "%' AND PFCL.FL_DEMURRAGE_FINALIZADA = 1";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            else if (idFilter == 3)
            {
                if (Ativo == "1")
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND P.NM_RAZAO LIKE '" + Filter + "%'";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND P.NM_RAZAO LIKE '" + Filter + "%'  AND PFCL.FL_DEMURRAGE_FINALIZADA = 0";
                        listTable = DBS.List(SQL);
                    }
                }
                else
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND P.NM_RAZAO LIKE '" + Filter + "%'  AND PFCL.FL_DEMURRAGE_FINALIZADA = 1";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            else if (idFilter == 4)
            {
                if (Ativo == "1")
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND P2.NM_RAZAO LIKE '" + Filter + "%'";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND P2.NM_RAZAO LIKE '" + Filter + "%'  AND PFCL.FL_DEMURRAGE_FINALIZADA = 0";
                        listTable = DBS.List(SQL);
                    }
                }
                else
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND P2.NM_RAZAO LIKE '" + Filter + "%'  AND PFCL.FL_DEMURRAGE_FINALIZADA = 1";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            else if (idFilter == 5)
            {
                if (Ativo == "1")
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND PFCL.DS_STATUS_DEMURRAGE LIKE '" + Filter + "%'";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND PFCL.DS_STATUS_DEMURRAGE LIKE '" + Filter + "%' and PFCL.FL_DEMURRAGE_FINALIZADA = 0 ";
                        listTable = DBS.List(SQL);
                    }
                }
                else
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        SQL += "AND PFCL.DS_STATUS_DEMURRAGE LIKE '" + Filter + "%' and PFCL.FL_DEMURRAGE_FINALIZADA = 1 ";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                if (Ativo == "1")
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL AND PFCL.FL_DEMURRAGE_FINALIZADA = 0 ";
                        listTable = DBS.List(SQL);
                    }
                }
                else
                {
                    if (Finalizado == "1")
                    {
                        SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
                        SQL += "P2.NM_RAZAO AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                        SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME,'dd/MM/yyyy') AS FINAL_FREETIME, ";
                        SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
                        SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
                        SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
                        SQL += "PFCL.DS_OBSERVACAO, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_COMPRA,'dd/MM/yyyy') AS CALC_DEMU_COMPRA, ";
                        SQL += "DFCL.VL_DEMURRAGE_COMPRA, FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy') AS PAG_DEMU, ";
                        SQL += "FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA,'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
                        SQL += "DFCL.VL_DEMURRAGE_VENDA, FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy') AS RECEB_DEMU ";
                        SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                        SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                        SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                        SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                        SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL AND PFCL.FL_DEMURRAGE_FINALIZADA = 1 ";
                        listTable = DBS.List(SQL);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return JsonConvert.SerializeObject(listTable);
        }


        [WebMethod]
        public string infoContainer(int idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
            SQL += "PFCL.QT_DIAS_FREETIME, PFCL.ID_STATUS_DEMURRAGE, DFCL.ID_DEMURRAGE_FATURA_PAGAR, DFCL.ID_DEMURRAGE_FATURA_RECEBER, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'yyyy-MM-dd') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "PFCL.DS_OBSERVACAO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '"+idCont+"'";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string infoContainerDevolucao(int idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
            SQL += "PFCL.ID_STATUS_DEMURRAGE, DFCL.ID_DEMURRAGE_FATURA_PAGAR, DFCL.ID_DEMURRAGE_FATURA_RECEBER, FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'yyyy-MM-dd') as DT_DEVOLUCAO_CNTR, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'yyyy-MM-dd') AS DATA_STATUS_DEMURRAGE, DS_STATUS_DEMURRAGE, ";
            SQL += "PFCL.DS_OBSERVACAO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "'";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string atualizarContainer(int idCont, string dtStatus, int dsStatus ,int qtDias, string dsObs)
        {
            string SQL;
            SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = '" + dsStatus + "', QT_DIAS_FREETIME = '"+ qtDias + "', ";
            SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "', DS_OBSERVACAO = '" + dsObs + "' WHERE ID_CNTR_BL = '" + idCont + "' ";

            string atualizarContainer = DBS.ExecuteScalar(SQL);
            return "1";

        }

        [WebMethod]
        public string infoCalculo(string idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.NR_PROCESSO as PROCESSO, P.NM_RAZAO AS CLIENTE, PFCL.ID_PARCEIRO_TRANSPORTADOR AS TRANSPORTADOR ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '"+idCont+"' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string listarCalculoDemurrage(string nrProcesso)
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, ";
            SQL += "PFCL.NM_TIPO_CONTAINER, FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy') as DT_CHEGADA, ";
            SQL += "PFCL.QT_DIAS_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy') AS DT_FINAL_FREETIME, ";
            SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') as DT_DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, DFCL.ID_MOEDA_DEMURRAGE_COMPRA, DFCL.VL_DEMURRAGE_COMPRA, ";
            SQL += "M.NM_MOEDA, M2.NM_MOEDA AS COMPRA, DFCL.VL_DEMURRAGE_VENDA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DEMURRAGE_VENDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_MOEDA M2 ON DFCL.ID_MOEDA_DEMURRAGE_COMPRA = M2.ID_MOEDA ";
            SQL += "WHERE PFCL.NR_PROCESSO = '"+nrProcesso+"' AND (PFCL.DT_CHEGADA IS NOT NULL OR PFCL.DT_CHEGADA != '') ";
            SQL += "AND (PFCL.QT_DIAS_FREETIME IS NOT NULL OR PFCL.QT_DIAS_FREETIME != '') ";
            SQL += "AND (PFCL.DT_DEVOLUCAO_CNTR IS NOT NULL OR PFCL.DT_DEVOLUCAO_CNTR != '') ";
            SQL += "AND (DFCL.ID_DEMURRAGE_FATURA_PAGAR IS NULL OR DFCL.ID_DEMURRAGE_FATURA_PAGAR = '') ";
            SQL += "AND (DFCL.ID_DEMURRAGE_FATURA_RECEBER IS NULL OR DFCL.ID_DEMURRAGE_FATURA_RECEBER = '') ";
            SQL += "AND DFCL.QT_DIAS_DEMURRAGE > 0 ";
            SQL += "ORDER BY PFCL.NR_CNTR ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string infoCalculoMarcadoVenda(string idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, TBD.QT_DIAS_FREETIME as FreeTimeTab, PFCL.QT_DIAS_FREETIME, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, P.NM_RAZAO, M.NM_MOEDA, TBD.FL_ESCALONADA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
            SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 0";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string infoCalculoMarcadoVendaTaxa(string idCont)
        {
            string SQL;
            int somaDias;
            decimal vlTaxa = 0;
            SQL = "SELECT PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, TBD.QT_DIAS_FREETIME as FreeTimeTab, PFCL.QT_DIAS_FREETIME, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, P.NM_RAZAO, M.NM_MOEDA, TBD.FL_ESCALONADA, CD.VL_TAXA_DEMURRAGE_VENDA, ";
            SQL += "TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
            SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
            SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
            SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
            SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 0";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                int d1 = (Int16)listTable.Rows[0]["QT_DIAS_01"];
                int d2 = (Int16)listTable.Rows[0]["QT_DIAS_02"];
                int d3 = (Int16)listTable.Rows[0]["QT_DIAS_03"];
                int d4 = (Int16)listTable.Rows[0]["QT_DIAS_04"];
                int d5 = (Int16)listTable.Rows[0]["QT_DIAS_05"];
                int d6 = (Int16)listTable.Rows[0]["QT_DIAS_06"];
                int d7 = (Int16)listTable.Rows[0]["QT_DIAS_07"];
                int d8 = (Int16)listTable.Rows[0]["QT_DIAS_08"];
                int ft = (Int16)listTable.Rows[0]["FreeTimeTab"];

                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    if (somaDias <= ft)
                    {
                        vlTaxa = 0;
                    }
                    else if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                    {
                        if (somaDias > ft && somaDias <= ft + d1)
                        {
                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_01"];
                        }
                        else if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                        {
                            if (somaDias > ft + d1 && somaDias <= ft + d1 + d2)
                            {
                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_02"];
                            }
                            else if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                            {
                                if (somaDias > ft + d1 + d2 && somaDias <= ft + d1 + d2 + d3)
                                {
                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                }
                                else if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                {
                                    if (somaDias > ft + d1 + d2 + d3 && somaDias <= ft + d1 + d2 + d3 + d4)
                                    {
                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                    }
                                    else if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                    {
                                        if (somaDias > ft + d1 + d2 + d3 + d4 && somaDias <= ft + d1 + d2 + d3 + d4 + d5)
                                        {
                                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                        }
                                        else if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                        {
                                            if (somaDias > ft + d1 + d2 + d3 + d4 + d5 && somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6)
                                            {
                                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                            }
                                            else if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                            {
                                                if (somaDias > ft + d1 + d2 + d3 + d4 + d5 + d6 && somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6 + d7)
                                                {
                                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                }
                                                else if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                {
                                                    if (somaDias > ft + d1 + d2 + d3 + d4 + d5 + d6 + d7 && somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8)
                                                    {
                                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_08"];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                listTable.Columns.Add("vlTaxa");
                listTable.Rows[0]["vlTaxa"] = vlTaxa;
                return JsonConvert.SerializeObject(listTable);
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public string infoCalculoMarcadoCompra(string idCont, string transportador)
        {
            string SQL;
            SQL = "SELECT PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, TBD.QT_DIAS_FREETIME as FreeTimeTab, PFCL.QT_DIAS_FREETIME, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, P.NM_RAZAO, M.NM_MOEDA, TBD.FL_ESCALONADA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
            SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '" + transportador + "'";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string infoCalculoMarcadoCompraTaxa(string idCont, string transportador)
        {
            string SQL;
            int somaDias;
            decimal vlTaxa = 0;
            SQL = "SELECT PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, TBD.QT_DIAS_FREETIME as FreeTimeTab, PFCL.QT_DIAS_FREETIME, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, P.NM_RAZAO, M.NM_MOEDA, TBD.FL_ESCALONADA, CD.VL_TAXA_DEMURRAGE_VENDA, ";
            SQL += "TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
            SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
            SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
            SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
            SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '"+ transportador + "'";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                int d1 = (Int16)listTable.Rows[0]["QT_DIAS_01"];
                int d2 = (Int16)listTable.Rows[0]["QT_DIAS_02"];
                int d3 = (Int16)listTable.Rows[0]["QT_DIAS_03"];
                int d4 = (Int16)listTable.Rows[0]["QT_DIAS_04"];
                int d5 = (Int16)listTable.Rows[0]["QT_DIAS_05"];
                int d6 = (Int16)listTable.Rows[0]["QT_DIAS_06"];
                int d7 = (Int16)listTable.Rows[0]["QT_DIAS_07"];
                int d8 = (Int16)listTable.Rows[0]["QT_DIAS_08"];
                int ft = (Int16)listTable.Rows[0]["FreeTimeTab"];

                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    if (somaDias <= ft)
                    {
                        vlTaxa = 0;
                    }
                    else if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                    {
                        if (somaDias > ft && somaDias <= ft + d1)
                        {
                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_01"];
                        }
                        else if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                        {
                            if (somaDias > ft + d1 && somaDias <= ft + d1 + d2)
                            {
                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_02"];
                            }
                            else if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                            {
                                if (somaDias > ft + d1 + d2 && somaDias <= ft + d1 + d2 + d3)
                                {
                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                }
                                else if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                {
                                    if (somaDias > ft + d1 + d2 + d3 && somaDias <= ft + d1 + d2 + d3 + d4)
                                    {
                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                    }
                                    else if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                    {
                                        if (somaDias > ft + d1 + d2 + d3 + d4 && somaDias <= ft + d1 + d2 + d3 + d4 + d5)
                                        {
                                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                        }
                                        else if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                        {
                                            if (somaDias > ft + d1 + d2 + d3 + d4 + d5 && somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6)
                                            {
                                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                            }
                                            else if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                            {
                                                if (somaDias > ft + d1 + d2 + d3 + d4 + d5 + d6 && somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6 + d7)
                                                {
                                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                }
                                                else if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                {
                                                    if (somaDias > ft + d1 + d2 + d3 + d4 + d5 + d6 + d7 && somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8)
                                                    {
                                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_08"];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                listTable.Columns.Add("vlTaxa");
                listTable.Rows[0]["vlTaxa"] = vlTaxa;
                return JsonConvert.SerializeObject(listTable);
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public void zerarCalculoVenda(string idCont)
        {
            string SQL;
            SQL = "select id_cntr_bl from TB_CNTR_DEMURRAGE ";
            SQL += "where id_cntr_bl = '" + idCont + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if(listTable != null)
            {
                SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_CALCULO_DEMURRAGE_VENDA = NULL, ";
                SQL += "ID_MOEDA_DEMURRAGE_VENDA = NULL, VL_TAXA_DEMURRAGE_VENDA = NULL, ";
                SQL += "VL_DEMURRAGE_VENDA = NULL, DT_CAMBIO_DEMURRAGE_VENDA = NULL, ";
                SQL += "VL_CAMBIO_DEMURRAGE_VENDA = NULL, VL_DESCONTO_DEMURRAGE_VENDA = NULL, ";
                SQL += "VL_DEMURRAGE_VENDA_BR = NULL WHERE ID_CNTR_BL = "+ idCont +" ";
                string zerar = DBS.ExecuteScalar(SQL);
            }
        }

        [WebMethod]
        public void zerarCalculoCompra(string idCont)
        {
            string SQL;
            SQL = "select id_cntr_bl from TB_CNTR_DEMURRAGE ";
            SQL += "where id_cntr_bl = '" + idCont + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_CALCULO_DEMURRAGE_COMPRA = NULL, ";
                SQL += "ID_MOEDA_DEMURRAGE_COMPRA = NULL, VL_TAXA_DEMURRAGE_COMPRA = NULL, ";
                SQL += "VL_DEMURRAGE_COMPRA = NULL, DT_CAMBIO_DEMURRAGE_COMPRA = NULL, ";
                SQL += "VL_CAMBIO_DEMURRAGE_COMPRA = NULL, VL_DESCONTO_DEMURRAGE_COMPRA = NULL, ";
                SQL += "VL_DEMURRAGE_COMPRA_BR = NULL WHERE ID_CNTR_BL = '"+idCont+"' ";
                string zerar = DBS.ExecuteScalar(SQL);
            }
        }

        [WebMethod]
        public void calcularDemurrageVenda(string idCont, float vlTaxa)
        {
            string SQL;
            int somaDias;
            float vlDemurr;
            string calcular;
            SQL = "SELECT * FROM TB_CNTR_DEMURRAGE WHERE ID_CNTR_BL = " + idCont + " ";
            DataTable search = new DataTable();
            search = DBS.List(SQL);
            if (search == null)
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE, DFCL.ID_MOEDA_DEMURRAGE_VENDA, TBD.FL_ESCALONADA, TBD.ID_MOEDA ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 0 ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                vlDemurr = somaDias * vlTaxa;
                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_VENDA, ID_MOEDA_DEMURRAGE_VENDA, VL_TAXA_DEMURRAGE_VENDA, ";
                    SQL += "VL_DEMURRAGE_VENDA, DT_CAMBIO_DEMURRAGE_VENDA, VL_CAMBIO_DEMURRAGE_VENDA, VL_DESCONTO_DEMURRAGE_VENDA, VL_DEMURRAGE_VENDA_BR ) VALUES ";
                    SQL += "(" + idCont + ",'" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "','" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', ";
                    SQL += "'" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "','" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "'," + somaDias + ", ";
                    SQL += "'" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "'," + listTable.Rows[0]["ID_MOEDA"] + "," + vlTaxa + "," + vlDemurr + ",null,null,null,null)";
                    calcular = DBS.ExecuteScalar(SQL);

                }
                else
                {
                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_VENDA, ID_MOEDA_DEMURRAGE_VENDA, VL_TAXA_DEMURRAGE_VENDA ";
                    SQL += "VL_DEMURRAGE_VENDA, DT_CAMBIO_DEMURRAGE_VENDA, VL_CAMBIO_DEMURRAGE_VENDA, VL_DESCONTO_DEMURRAGE_VENDA, VL_DEMURRAGE_VENDA_BR ) VALUES ";
                    SQL += "(" + idCont + "," + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "," + listTable.Rows[0]["DT_FINAL_FREETIME"] + ", ";
                    SQL += "" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "," + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "," + somaDias + ", ";
                    SQL += "'" + DateTime.Now + "'," + listTable.Rows[0]["ID_MOEDA_DEMURRAGE_VENDA"] + ",0,)";
                    calcular = DBS.ExecuteScalar(SQL);
                }
            }
            else
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE, DFCL.ID_MOEDA_DEMURRAGE_VENDA, TBD.FL_ESCALONADA, TBD.ID_MOEDA ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 0 ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                vlDemurr = somaDias * vlTaxa;
                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                    SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DEMURRAGE = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ";
                    SQL += "DT_FINAL_DEMURRAGE = '" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', QT_DIAS_DEMURRAGE = " + somaDias + ", ";
                    SQL += "DT_CALCULO_DEMURRAGE_VENDA = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ID_MOEDA_DEMURRAGE_VENDA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DEMURRAGE_VENDA = " + vlTaxa + ", ";
                    SQL += "VL_DEMURRAGE_VENDA = " + vlDemurr + ", DT_CAMBIO_DEMURRAGE_VENDA = null, VL_CAMBIO_DEMURRAGE_VENDA = null, VL_DESCONTO_DEMURRAGE_VENDA = null, VL_DEMURRAGE_VENDA_BR = null WHERE ID_CNTR_BL = "+ idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                }
                else
                {
                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_VENDA, ID_MOEDA_DEMURRAGE_VENDA, VL_TAXA_DEMURRAGE_VENDA ";
                    SQL += "VL_DEMURRAGE_VENDA, DT_CAMBIO_DEMURRAGE_VENDA, VL_CAMBIO_DEMURRAGE_VENDA, VL_DESCONTO_DEMURRAGE_VENDA, VL_DEMURRAGE_VENDA_BR ) VALUES ";
                    SQL += "(" + idCont + "," + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "," + listTable.Rows[0]["DT_FINAL_FREETIME"] + ", ";
                    SQL += "" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "," + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "," + somaDias + ", ";
                    SQL += "'" + DateTime.Now + "'," + listTable.Rows[0]["ID_MOEDA"] + ",0,)";
                    calcular = DBS.ExecuteScalar(SQL);
                }
            }
        }

        [WebMethod]
        public void calcularDemurrageCompra(string idCont, float vlTaxa, string transportador)
        {
            string SQL;
            int somaDias;
            float vlDemurr;
            string calcular;
            SQL = "SELECT * FROM TB_CNTR_DEMURRAGE WHERE ID_CNTR_BL = " + idCont + " ";
            DataTable search = new DataTable();
            search = DBS.List(SQL);
            if (search == null)
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE, DFCL.ID_MOEDA_DEMURRAGE_COMPRA, TBD.FL_ESCALONADA, TBD.ID_MOEDA ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '" + transportador + "' ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                vlDemurr = somaDias * vlTaxa;
                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_COMPRA, ID_MOEDA_DEMURRAGE_COMPRA, VL_TAXA_DEMURRAGE_COMPRA, ";
                    SQL += "VL_DEMURRAGE_COMPRA, DT_CAMBIO_DEMURRAGE_COMPRA, VL_CAMBIO_DEMURRAGE_COMPRA, VL_DESCONTO_DEMURRAGE_COMPRA, VL_DEMURRAGE_COMPRA_BR ) VALUES ";
                    SQL += "(" + idCont + ",'" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "','" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', ";
                    SQL += "'" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "','" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "'," + somaDias + ", ";
                    SQL += "'" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "'," + listTable.Rows[0]["ID_MOEDA"] + "," + vlTaxa + "," + vlDemurr + ",null,null,null,null)";
                    calcular = DBS.ExecuteScalar(SQL);

                }
                else
                {
                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_COMPRA, ID_MOEDA_DEMURRAGE_COMPRA, VL_TAXA_DEMURRAGE_COMPRA ";
                    SQL += "VL_DEMURRAGE_COMPRA, DT_CAMBIO_DEMURRAGE_COMPRA, VL_CAMBIO_DEMURRAGE_COMPRA, VL_DESCONTO_DEMURRAGE_COMPRA, VL_DEMURRAGE_COMPRA_BR ) VALUES ";
                    SQL += "(" + idCont + "," + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "," + listTable.Rows[0]["DT_FINAL_FREETIME"] + ", ";
                    SQL += "" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "," + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "," + somaDias + ", ";
                    SQL += "'" + DateTime.Now + "'," + listTable.Rows[0]["ID_MOEDA_DEMURRAGE_COMPRA"] + ",0,)";
                    calcular = DBS.ExecuteScalar(SQL);
                }
            }
            else
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE, DFCL.ID_MOEDA_DEMURRAGE_COMPRA, TBD.FL_ESCALONADA, TBD.ID_MOEDA ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '"+ transportador + "' ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                vlDemurr = somaDias * vlTaxa;
                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                    SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DEMURRAGE = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ";
                    SQL += "DT_FINAL_DEMURRAGE = '" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', QT_DIAS_DEMURRAGE = " + somaDias + ", ";
                    SQL += "DT_CALCULO_DEMURRAGE_COMPRA = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ID_MOEDA_DEMURRAGE_COMPRA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DEMURRAGE_COMPRA = " + vlTaxa + ", ";
                    SQL += "VL_DEMURRAGE_COMPRA = " + vlDemurr + ", DT_CAMBIO_DEMURRAGE_COMPRA = null, VL_CAMBIO_DEMURRAGE_COMPRA = null, VL_DESCONTO_DEMURRAGE_COMPRA = null, VL_DEMURRAGE_COMPRA_BR = null WHERE ID_CNTR_BL = " + idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                }
                else
                {
                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_COMPRA, ID_MOEDA_DEMURRAGE_COMPRA, VL_TAXA_DEMURRAGE_COMPRA ";
                    SQL += "VL_DEMURRAGE_COMPRA, DT_CAMBIO_DEMURRAGE_COMPRA, VL_CAMBIO_DEMURRAGE_COMPRA, VL_DESCONTO_DEMURRAGE_COMPRA, VL_DEMURRAGE_COMPRA_BR ) VALUES ";
                    SQL += "(" + idCont + "," + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "," + listTable.Rows[0]["DT_FINAL_FREETIME"] + ", ";
                    SQL += "" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "," + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "," + somaDias + ", ";
                    SQL += "'" + DateTime.Now + "'," + listTable.Rows[0]["ID_MOEDA"] + ",0,)";
                    calcular = DBS.ExecuteScalar(SQL);
                }
            }
        }

        [WebMethod]
        public string listarContainerDevolucao(string nrProcesso)
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, ";
            SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') as DT_DEVOLUCAO_CNTR, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "PFCL.DS_STATUS_DEMURRAGE ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "WHERE PFCL.NR_PROCESSO = '" + nrProcesso + "' ";
            SQL += "AND (DFCL.ID_DEMURRAGE_FATURA_PAGAR IS NULL OR DFCL.ID_DEMURRAGE_FATURA_PAGAR = '') ";
            SQL += "AND (DFCL.ID_DEMURRAGE_FATURA_RECEBER IS NULL OR DFCL.ID_DEMURRAGE_FATURA_RECEBER = '') ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string atualizarDevolucao(string idCont, string dtStatus, string dsStatus, string dtDevolucao)
        {
            string SQL;
            SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = '" + dtDevolucao + "', ID_STATUS_DEMURRAGE = '" + dsStatus + "', ";
            SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "' ";
            SQL += "WHERE ID_CNTR_BL = '" + idCont + "' ";

            string attDevolu = DBS.ExecuteScalar(SQL);
            return "1";
        }

        [WebMethod]
        public string consultarProcesso(string nrProcesso)
        {
            string SQL;
            SQL = "select * from tb_bl join TB_CNTR_BL on dbo.TB_BL.ID_BL_MASTER = dbo.TB_CNTR_BL.ID_BL_MASTER ";
            SQL += "join TB_TIPO_CONTAINER on dbo.TB_CNTR_BL.ID_TIPO_CNTR = dbo.TB_TIPO_CONTAINER.ID_TIPO_CONTAINER ";
            SQL += "join TB_PARCEIRO P1 on TB_BL.ID_PARCEIRO_CLIENTE = P1.ID_PARCEIRO ";
            SQL += "join TB_PARCEIRO P2 on dbo.TB_BL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "where NR_PROCESSO = '" + nrProcesso + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        [WebMethod]
        public string consultarNumeroContainer(string nrContainer, string nrProcesso)
        {
            string SQL;
            SQL = "select * from tb_bl join TB_CNTR_BL on dbo.TB_BL.ID_BL_MASTER = dbo.TB_CNTR_BL.ID_BL_MASTER ";
            SQL += "join TB_TIPO_CONTAINER on dbo.TB_CNTR_BL.ID_TIPO_CNTR = dbo.TB_TIPO_CONTAINER.ID_TIPO_CONTAINER ";
            SQL += "join TB_PARCEIRO P1 on TB_BL.ID_PARCEIRO_CLIENTE = P1.ID_PARCEIRO ";
            SQL += "join TB_PARCEIRO P2 on dbo.TB_BL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "where TB_CNTR_BL.NR_CNTR = '" + nrContainer + "' and TB_BL.NR_PROCESSO = '" + nrProcesso + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        [WebMethod]
        public string exportExcel()
        {
            string SQL;
            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO ";
            SQL += "FROM TB_AMR_CNTR_BL AMR ";
            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
            SQL += "WHERE CO.DT_EXPORTACAO_TERCEIRIZADA IS NOT NULL";


            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string exportExcelFiltrada(string inicial, string final, string regExpo, string regNExpo)
        {
            string SQL;
            DataTable listTable = new DataTable();

            if (inicial != "")
            {
                if (final != "")
                {
                    if (regExpo == "1")
                    {
                        if (regNExpo == "1")
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "WHERE C.DT_EMBARQUE BETWEEN '" + inicial + "' AND '" + final + "' ";
                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                        else
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "WHERE C.DT_EMBARQUE BETWEEN '" + inicial + "' AND '" + final + "' and CO.DT_EXPORTACAO_TERCEIRIZADA IS NOT NULL ";
                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                    }
                    else
                    {
                        if (regNExpo == "1")
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "WHERE C.DT_EMBARQUE BETWEEN '" + inicial + "' AND '" + final + "' and CO.DT_EXPORTACAO_TERCEIRIZADA IS NULL";
                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    if (regExpo == "1")
                    {
                        if (regNExpo == "1")
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "WHERE C.DT_EMBARQUE >= '" + inicial + "' ";
                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                        else
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "WHERE C.DT_EMBARQUE >= '" + inicial + "' and CO.DT_EXPORTACAO_TERCEIRIZADA IS NOT NULL ";
                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                    }
                    else
                    {
                        if (regNExpo == "1")
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "WHERE C.DT_EMBARQUE >= '" + inicial + "' and CO.DT_EXPORTACAO_TERCEIRIZADA IS NULL";
                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            else
            {
                if (final != "")
                {
                    if (regExpo == "1")
                    {
                        if (regNExpo == "1")
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "where C.DT_EMBARQUE <= '" + final + "' ";
                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                        else
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "where C.DT_EMBARQUE <= '" + final + "' and CO.DT_EXPORTACAO_TERCEIRIZADA IS NOT NULL ";
                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                    }
                    else
                    {
                        if (regNExpo == "1")
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "where C.DT_EMBARQUE <= '" + final + "' CO.DT_EXPORTACAO_TERCEIRIZADA IS NULL";

                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    if (regExpo == "1")
                    {
                        if (regNExpo == "1")
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";

                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                        else
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "WHERE CO.DT_EXPORTACAO_TERCEIRIZADA IS NOT NULL ";
                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                    }
                    else
                    {
                        if (regNExpo == "1")
                        {
                            SQL = "SELECT FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
                            SQL += "C.NR_PROCESSO, CO.NR_CNTR, TC.NM_TIPO_CONTAINER, CO.QT_DIAS_FREETIME, FORMAT(CO.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DT_DEVOLUCAO_CNTR, ";
                            SQL += "P1.NM_PORTO AS ORIGEM, P2.NM_PORTO AS DESTINO, N.NM_NAVIO AS NAVIO, C.NR_VIAGEM AS VIAGEM, FORMAT(C.DT_PREVISAO_CHEGADA, 'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, ";
                            SQL += "C.nr_bl AS HOUSE, M.nr_bl AS MASTER, TRANS.NM_RAZAO AS TRANSPORTADOR, CONSIG.NM_RAZAO AS CONSIGNATARIO, CONSIG.CNPJ AS CNPJ_CONSIGNATARIO, ";
                            SQL += "EXPORT.NM_RAZAO AS EXPORTADOR, EXPORT.CNPJ AS CNPJ_EXPORTADOR, REFER.NR_REFERENCIA_CLIENTE AS REFER, FORMAT(CO.DT_EXPORTACAO_TERCEIRIZADA,'dd/MM/yyyy') AS DT_EXPORTACAO  ";
                            SQL += "FROM TB_AMR_CNTR_BL AMR ";
                            SQL += "JOIN TB_BL C ON AMR.ID_BL = C.ID_BL ";
                            SQL += "JOIN TB_CNTR_BL CO ON AMR.ID_CNTR_BL = CO.ID_CNTR_BL ";
                            SQL += "JOIN TB_TIPO_CONTAINER TC ON CO.ID_TIPO_CNTR = TC.ID_TIPO_CONTAINER ";
                            SQL += "JOIN TB_PORTO P1 ON C.ID_PORTO_ORIGEM = P1.ID_PORTO ";
                            SQL += "JOIN TB_PORTO P2 ON C.ID_PORTO_ORIGEM = P2.ID_PORTO ";
                            SQL += "JOIN TB_NAVIO N ON C.ID_NAVIO = N.ID_NAVIO ";
                            SQL += "join tb_parceiro TRANS on C.ID_PARCEIRO_TRANSPORTADOR = TRANS.ID_PARCEIRO ";
                            SQL += "join tb_parceiro CONSIG on C.ID_PARCEIRO_CLIENTE = CONSIG.ID_PARCEIRO ";
                            SQL += "join tb_parceiro EXPORT on C.ID_PARCEIRO_EXPORTADOR = EXPORT.ID_PARCEIRO ";
                            SQL += "join tb_referencia_cliente REFER on C.ID_BL = REFER.ID_BL ";
                            SQL += "join tb_bl M on C.ID_BL_MASTER = M.ID_BL ";
                            SQL += "WHERE CO.DT_EXPORTACAO_TERCEIRIZADA IS NULL";

                            listTable = DBS.List(SQL);
                            return JsonConvert.SerializeObject(listTable);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        [WebMethod]
        public string atualizarBaseFCA(DemurrageControl dadosEdit)
        {
            string SQL;
            if (dadosEdit.DS_STATUS == "Aberto")
            {

                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "', FL_DEMURRAGE_FINALIZADA = '0' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }

            else if (dadosEdit.DS_STATUS == "Devolvido Sem Detention")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "', FL_DEMURRAGE_FINALIZADA = '1' ";
                SQL += "DT_DEVOLUCAO_CNTR = '" + dadosEdit.DT_DEVOLUCAO_CNTR + "' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Devolvido sem demurrage")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "', FL_DEMURRAGE_FINALIZADA = '1' ";
                SQL += "DT_DEVOLUCAO_CNTR = '" + dadosEdit.DT_DEVOLUCAO_CNTR + "' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Fase final de faturamento")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "', FL_DEMURRAGE_FINALIZADA = '0' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Em cobrança")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "', FL_DEMURRAGE_FINALIZADA = '0' ";
                SQL += "DT_DEVOLUCAO_CNTR = '" + dadosEdit.DT_DEVOLUCAO_CNTR + "', QT_DIAS_DEMURRAGE = '" + dadosEdit.QT_DIAS_DEMURRAGE + "' ";
                SQL += "VL_FATURA_TERC = '" + dadosEdit.VL_FATURA + "', DT_VENCIMENTO_FATURA_TERC = '" + dadosEdit.DT_VENCIMENTO_FATURA + "' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Baixado")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "', FL_DEMURRAGE_FINALIZADA = '1' ";
                SQL += "DT_PAGAMENTO_FATURA_TERC = '" + dadosEdit.DT_PAGAMENTO + "'";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Cobrança cancelada")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "', FL_DEMURRAGE_FINALIZADA = '0' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                return "0";
            }
        }

        [WebMethod]
        public string listarDemurrageVenda() 
        {
            string SQL;

            SQL = "SELECT PFCL.NR_CNTR, FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy') AS DT_CHEGADA, ";
            SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy') AS FINAL_FREETIME, ";
            SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, FORMAT(DFCL.DT_CALCULO_DEMURRAGE_VENDA, 'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
            SQL += "DFCL.ID_MOEDA_DEMURRAGE_VENDA,DFCL.VL_TAXA_DEMURRAGE_VENDA, ";
            SQL += "DFCL.VL_DEMURRAGE_VENDA, DFCL.ID_DEMURRAGE_FATURA_RECEBER, ";
            SQL += "FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE, 'dd/MM/yyyy') AS RECEB_DEMU, ";
            SQL += "P.NM_FANTASIA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }
        [WebMethod]
        public string listarCourrier()
        {
            string SQL;
            SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
            SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
            SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
            SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
            SQL += "FROM TB_BL BL ";
            SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
            SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
            SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string listarCourrierFiltrada(int idFilter, string Filter, string tipo)
        {
            if (idFilter == 1)
            {
                if (tipo == "1")
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE BL.NR_PROCESSO LIKE '" + Filter + "%' AND TP.ID_TIPO_ESTUFAGEM = 1 ";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
                else
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE BL.NR_PROCESSO LIKE '" + Filter + "%' AND TP.ID_TIPO_ESTUFAGEM = 2 ";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
            }
            else if (idFilter == 2)
            {
                if (tipo == "1")
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE BL.ID_BL_MASTER LIKE '" + Filter + "%' AND TP.ID_TIPO_ESTUFAGEM = 1 ";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
                else
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE BL.ID_BL_MASTER LIKE '" + Filter + "%' AND TP.ID_TIPO_ESTUFAGEM = 2 ";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
            }
            else if (idFilter == 3)
            {
                if (tipo == "1")
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE P.NM_RAZAO LIKE '" + Filter + "%' AND TP.ID_TIPO_ESTUFAGEM = 1 ";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
                else
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE P.NM_RAZAO LIKE '" + Filter + "%' AND TP.ID_TIPO_ESTUFAGEM = 2 ";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
            }
            else if (idFilter == 4)
            {
                if (tipo == "1")
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE N.NM_NAVIO LIKE '" + Filter + "%' AND TP.ID_TIPO_ESTUFAGEM = 1 ";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
                else
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE N.NM_NAVIO LIKE '" + Filter + "%' AND TP.ID_TIPO_ESTUFAGEM = 2 ";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
            }
            else
            {
                if (tipo == "1")
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE TP.ID_TIPO_ESTUFAGEM = 1";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
                else
                {
                    string SQL;
                    SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy') AS DT_RETIRADA_COURRIER, ";
                    SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, ";
                    SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
                    SQL += "FROM TB_BL BL ";
                    SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                    SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
                    SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
                    SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
                    SQL += "WHERE TP.ID_TIPO_ESTUFAGEM = 2";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);
                    return JsonConvert.SerializeObject(listTable);
                }
            }
        }
        [WebMethod]
        public string BuscarCourrier(int id)
        {
            string SQL;
            SQL = "SELECT BL.NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'yyyy-MM-dd') AS DT_RECEBIMENTO_MBL, ";
            SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'yyyy-MM-dd') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'yyyy-MM-dd') AS DT_RETIRADA_COURRIER, ";
            SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'yyyy-MM-dd') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'yyyy-MM-dd') AS DT_CHEGADA, ";
            SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
            SQL += "FROM TB_BL BL ";
            SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
            SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
            SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
            SQL += "WHERE BL.ID_BL = '" + id + "' ";

            DataTable carregarDados = new DataTable();
            carregarDados = DBS.List(SQL);
            CourrierInfo resultado = new CourrierInfo();

            resultado.NR_PROCESSO = carregarDados.Rows[0]["NR_PROCESSO"].ToString();
            resultado.NM_RAZAO = carregarDados.Rows[0]["CLIENTE"].ToString();
            resultado.ID_BL = id.ToString();
            resultado.ID_BL_MASTER = carregarDados.Rows[0]["ID_BL_MASTER"].ToString();
            resultado.DT_RECEBIMENTO_MBL = carregarDados.Rows[0]["DT_RECEBIMENTO_MBL"].ToString();
            resultado.CD_RASTREAMENTO_MBL = carregarDados.Rows[0]["CD_RASTREAMENTO_MBL"].ToString();
            resultado.DT_RECEBIMENTO_HBL = carregarDados.Rows[0]["DT_RECEBIMENTO_HBL"].ToString();
            resultado.CD_RASTREAMENTO_HBL = carregarDados.Rows[0]["CD_RASTREAMENTO_HBL"].ToString();
            resultado.DT_RETIRADA_COURRIER = carregarDados.Rows[0]["DT_RETIRADA_COURRIER"].ToString();
            resultado.NM_RETIRADO_POR_COURRIER = carregarDados.Rows[0]["NM_RETIRADO_POR_COURRIER"].ToString();
            resultado.NR_FATURA_COURRIER = carregarDados.Rows[0]["NR_FATURA_COURRIER"].ToString();

            return JsonConvert.SerializeObject(resultado);
        }

        [WebMethod]
        public string editarCourrier(CourrierInfo dadosEdit)
        {
            string SQL;
            SQL = "UPDATE TB_BL SET DT_RECEBIMENTO_HBL = '" + dadosEdit.DT_RECEBIMENTO_HBL + "', ";
            SQL += "CD_RASTREAMENTO_HBL = '" + dadosEdit.CD_RASTREAMENTO_HBL + "', ";
            SQL += "DT_RETIRADA_COURRIER = '" + dadosEdit.DT_RETIRADA_COURRIER + "', ";
            SQL += "NM_RETIRADO_POR_COURRIER = '" + dadosEdit.NM_RETIRADO_POR_COURRIER + "', NR_FATURA_COURRIER = '" + dadosEdit.NR_FATURA_COURRIER+ "' ";
            SQL += "WHERE ID_BL = '" + dadosEdit.ID_BL + "' ";

            string editCourrier = DBS.ExecuteScalar(SQL);

            SQL = "UPDATE TB_BL SET DT_RECEBIMENTO_MBL = '" + dadosEdit.DT_RECEBIMENTO_MBL + "', ";
            SQL += "CD_RASTREAMENTO_MBL = '" + dadosEdit.CD_RASTREAMENTO_MBL + "' ";
            SQL += "WHERE ID_BL = '" + dadosEdit.ID_BL_MASTER + "' ";

            string editCourrierMaster = DBS.ExecuteScalar(SQL);

            return "1";
            
        }


    }
}
