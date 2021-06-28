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
            SQL = "SELECT ID_TABELA_DEMURRAGE, NM_TIPO_CONTAINER, FORMAT(DT_VALIDADE_INICIAL,'dd/MM/yyyy') AS DT_VALIDADE_INICIAL_FORMAT ";
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
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, ISNULL(LEFT(P.NM_RAZAO,10),'') AS CLIENTE, ";
            SQL += "ISNULL(LEFT(P2.NM_RAZAO,10),'') AS TRANSPORTADOR, ISNULL(FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy'), '') AS DT_CHEGADA, ";
            SQL += "ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME), '') AS QT_DIAS_FREETIME, ISNULL(FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy'), '') AS FINAL_FREETIME, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'), '') AS DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_STATUS_DEMURRAGE, 'dd/MM/yyyy'),'') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "ISNULL(PFCL.DS_OBSERVACAO, '') AS DS_OBSERVACAO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,(SELECT ISNULL(CONVERT(VARCHAR,DF.ID_DEMURRAGE_FATURA),'') AS COMPRA ";
            SQL += "FROM TB_DEMURRAGE_FATURA DF ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL PFCL2 ON DF.ID_BL = PFCL2.ID_BL ";
            SQL += "WHERE DF.CD_PR = 'P' ";
            SQL += "AND DF.DT_CANCELAMENTO IS NULL ";
            SQL += "AND PFCL2.ID_CNTR_BL = PFCL.ID_CNTR_BL ";
            SQL += ")),'') AS CALC_DEMU_COMPRA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_DEMURRAGE_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS VL_DEMURRAGE_COMPRA, ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS PAG_DEMU, ";
            SQL += "ISNULL(CONVERT(VARCHAR,(SELECT ISNULL(CONVERT(VARCHAR, DF.ID_DEMURRAGE_FATURA), '') AS VENDA ";
            SQL += "FROM TB_DEMURRAGE_FATURA DF ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL PFCL3 ON DF.ID_BL = PFCL3.ID_BL ";
            SQL += "WHERE DF.CD_PR = 'R' ";
            SQL += "AND PFCL3.ID_CNTR_BL = PFCL.ID_CNTR_BL ";
            SQL += "AND DF.DT_CANCELAMENTO IS NULL ";
            SQL += ")),'') AS CALC_DEMU_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DEMURRAGE_VENDA,'C','pt-br')),'R$',''),'') AS VL_DEMURRAGE_VENDA, ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS RECEB_DEMU ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
            SQL += "AND PFCL.FL_DEMURRAGE_FINALIZADA = 0 ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string filtrarTabela(string idFilter, string Filter, string Ativo, string Finalizado)
        {
            string SQL;
            DataTable listTable = new DataTable();

            switch (idFilter)
            {
                case "1":
                    idFilter = "AND PFCL.NR_PROCESSO LIKE '" + Filter + "%' ";
                    break;
                case "2":
                    idFilter = "AND PFCL.NR_CNTR LIKE '" + Filter + "%' ";
                    break;
                case "3":
                    idFilter = "AND P.NM_RAZAO LIKE '" + Filter + "%' ";
                    break;
                case "4":
                    idFilter = "AND P2.NM_RAZAO LIKE '" + Filter + "%' ";
                    break;
                case "5":
                    idFilter = "AND PFCL.DS_STATUS_DEMURRAGE LIKE '" + Filter + "%' ";
                    break;
                default:
                    idFilter = "";
                    break;
            }

            if (Ativo == "1" && Finalizado == "1")
            {
                Ativo = "";
                Finalizado = "";
            }
            else
            {
                switch (Ativo)
                {
                    case "1":
                        Ativo = "AND PFCL.FL_DEMURRAGE_FINALIZADA = 0 ";
                        break;
                    default:
                        Ativo = "";
                        break;
                }

                switch (Finalizado)
                {
                    case "1":
                        Finalizado = "AND PFCL.FL_DEMURRAGE_FINALIZADA = 1 ";
                        break;
                    default:
                        Finalizado = "";
                        break;
                }
            }

            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, ISNULL(LEFT(P.NM_RAZAO,10),'') AS CLIENTE, ";
            SQL += "ISNULL(LEFT(P2.NM_RAZAO,10), '') AS TRANSPORTADOR, ISNULL(FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy'), '') AS DT_CHEGADA, ";
            SQL += "ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME), '') AS QT_DIAS_FREETIME, ISNULL(FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy'), '') AS FINAL_FREETIME, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'), '') AS DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE, 'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "ISNULL(PFCL.DS_OBSERVACAO, '') AS DS_OBSERVACAO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,(SELECT ISNULL(CONVERT(VARCHAR,DF.ID_DEMURRAGE_FATURA),'') AS COMPRA ";
            SQL += "FROM TB_DEMURRAGE_FATURA_ITENS DFI ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL2 ON DFI.ID_CNTR_DEMURRAGE = DFCL2.ID_CNTR_DEMURRAGE ";
            SQL += "LEFT JOIN TB_DEMURRAGE_FATURA DF ON DF.ID_DEMURRAGE_FATURA = DFI.ID_DEMURRAGE_FATURA ";
            SQL += "WHERE DF.CD_PR = 'P' ";
            SQL += "AND DFCL2.ID_CNTR_BL = PFCL.ID_CNTR_BL ";
            SQL += "AND DF.DT_CANCELAMENTO IS NULL ";
            SQL += ")),'') AS CALC_DEMU_COMPRA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_DEMURRAGE_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS VL_DEMURRAGE_COMPRA, ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS PAG_DEMU, ";
            SQL += "ISNULL(CONVERT(VARCHAR,(SELECT ISNULL(CONVERT(VARCHAR, DF.ID_DEMURRAGE_FATURA), '') AS VENDA ";
            SQL += "FROM TB_DEMURRAGE_FATURA_ITENS DFI ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL3 ON DFI.ID_CNTR_DEMURRAGE = DFCL3.ID_CNTR_DEMURRAGE ";
            SQL += "LEFT JOIN TB_DEMURRAGE_FATURA DF ON DFI.ID_DEMURRAGE_FATURA = DF.ID_DEMURRAGE_FATURA ";
            SQL += "WHERE DF.CD_PR = 'R' ";
            SQL += "AND DFCL3.ID_CNTR_BL = PFCL.ID_CNTR_BL ";
            SQL += "AND DF.DT_CANCELAMENTO IS NULL ";
            SQL += ")),'') AS CALC_DEMU_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DEMURRAGE_VENDA,'C','pt-br')),'R$',''),'') AS VL_DEMURRAGE_VENDA, ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS RECEB_DEMU ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
            SQL += ""+ idFilter + " ";
            SQL += ""+ Ativo + " ";
            SQL += "" + Finalizado + " ";
            listTable = DBS.List(SQL);

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
            if (dsStatus.ToString() != "")
            {
                string SQL;
                string flagF;
                SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '"+dsStatus+"' ";
                DataTable flFinaliza = new DataTable();
                flFinaliza = DBS.List(SQL);
                flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = '" + dsStatus + "', QT_DIAS_FREETIME = '" + qtDias + "', ";
                SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "', DS_OBSERVACAO = '" + dsObs + "', FL_DEMURRAGE_FINALIZADA = '"+ flagF +"' WHERE ID_CNTR_BL = '" + idCont + "' ";
                string atualizarContainer = DBS.ExecuteScalar(SQL);
                return "1";


            }
            else
            {
                return "2";
            }
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
            SQL += "DFCL.QT_DIAS_DEMURRAGE, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DEMURRAGE_COMPRA,'c','pt-br')),'R$',''),'') AS VL_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(M.NM_MOEDA,'') AS VENDA, ISNULL(M2.NM_MOEDA,'') AS COMPRA, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DEMURRAGE_VENDA,'c','pt-br')),'R$',''),'') AS VL_DEMURRAGE_VENDA ";
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
            SQL += "AND PFCL.FL_DEMURRAGE_FINALIZADA = 0 ";
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
            SQL += "DFCL.QT_DIAS_DEMURRAGE, TBD.FL_ESCALONADA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";

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
            SQL += "DFCL.QT_DIAS_DEMURRAGE, P.NM_RAZAO AS TABELA, M.NM_MOEDA AS MOEDA, TBD.FL_ESCALONADA, CD.VL_TAXA_DEMURRAGE_VENDA, ";
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
            SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 0 ";
            SQL += "AND PFCL.DT_DEVOLUCAO_CNTR >= TBD.DT_VALIDADE_INICIAL ";

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
        public string infoCalculoMarcadoCompra(string idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, TBD.QT_DIAS_FREETIME as FreeTimeTab, PFCL.QT_DIAS_FREETIME, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, TBD.FL_ESCALONADA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
            SQL += "AND PFCL.DT_DEVOLUCAO_CNTR >= TBD.DT_VALIDADE_INICIAL ";

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
            SQL += "DFCL.QT_DIAS_DEMURRAGE, P.NM_RAZAO as TABELA, M.NM_MOEDA AS MOEDA, TBD.FL_ESCALONADA, CD.VL_TAXA_DEMURRAGE_COMPRA, ";
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
            SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '"+ transportador + "' ";
            SQL += "AND PFCL.DT_DEVOLUCAO_CNTR >= TBD.DT_VALIDADE_INICIAL ";

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
                        if (somaDias <= ft + d1)
                        {
                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_01"];
                        }
                        else if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                        {
                            if (somaDias <= ft + d1 + d2)
                            {
                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_02"];
                            }
                            else if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                            {
                                if (somaDias <= ft + d1 + d2 + d3)
                                {
                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                }
                                else if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                {
                                    if (somaDias <= ft + d1 + d2 + d3 + d4)
                                    {
                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                    }
                                    else if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                    {
                                        if (somaDias <= ft + d1 + d2 + d3 + d4 + d5)
                                        {
                                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                        }
                                        else if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                        {
                                            if (somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6)
                                            {
                                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                            }
                                            else if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                            {
                                                if (somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6 + d7)
                                                {
                                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                }
                                                else if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                {
                                                    if (somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8)
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
                    listTable.Columns.Add("vlTaxa");
                    listTable.Rows[0]["vlTaxa"] = vlTaxa;
                }
                else
                {
                    listTable.Columns.Add("vlTaxa");
                    listTable.Rows[0]["vlTaxa"] = vlTaxa;
                }
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
        public void calcularDemurrageVenda(string idCont, decimal vlTaxa, string idStatus, string dtStatus)
        {
            string SQL;
            int somaDias;
            decimal vlDemurr;
            string calcular;
            int demurrage;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
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

                
                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] * vlTaxa;

                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_VENDA, ID_MOEDA_DEMURRAGE_VENDA, VL_TAXA_DEMURRAGE_VENDA, ";
                    SQL += "VL_DEMURRAGE_VENDA, DT_CAMBIO_DEMURRAGE_VENDA, VL_CAMBIO_DEMURRAGE_VENDA, VL_DESCONTO_DEMURRAGE_VENDA, VL_DEMURRAGE_VENDA_BR ) VALUES ";
                    SQL += "(" + idCont + ",'" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "','" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', ";
                    SQL += "'" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "','" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "'," + somaDias + ", ";
                    SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + "," + vlTaxa.ToString().Replace(",", ".") + "," + vlDemurr.ToString().Replace(",",".") + ",null,null,null,null)";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = " + idStatus + " ,DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '"+flagF+"' WHERE ID_CNTR_BL = "+ idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);

                }
                else
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

                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"];
                    demurrage = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    vlDemurr = 0;

                    if (somaDias <= ft)
                    {
                        vlDemurr = 0;
                    }
                    else
                    {
                        if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                        {
                            if (demurrage - d1 <= 0)
                            {
                                vlDemurr = demurrage * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                            }
                            else
                            {
                                demurrage = demurrage - d1;
                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                                {
                                    if (demurrage - d2 <= 0)
                                    {
                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_02"]);
                                    }
                                    else
                                    {
                                        demurrage = demurrage - d2;
                                        vlDemurr = d2 * (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                        if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                        {
                                            if (demurrage - d3 <= 0)
                                            {
                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_03"]);
                                            }
                                            else
                                            {
                                                demurrage = demurrage - d3;
                                                vlDemurr = d3 * (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                                if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                                {
                                                    if (demurrage - d4 <= 0)
                                                    {
                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_04"]);
                                                    }
                                                    else
                                                    {
                                                        demurrage = demurrage - d4;
                                                        vlDemurr = d4 * (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                                        if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                                        {
                                                            if (demurrage - d5 <= 0)
                                                            {
                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_05"]);
                                                            }
                                                            else
                                                            {
                                                                demurrage = demurrage - d5;
                                                                vlDemurr = d5 * (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                                                if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                                                {
                                                                    if (demurrage - d6 <= 0)
                                                                    {
                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_06"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        demurrage = demurrage - d6;
                                                                        vlDemurr = d6 * (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                                        if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                                        {
                                                                            if (demurrage - d7 <= 0)
                                                                            {
                                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_07"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                demurrage = demurrage - d7;
                                                                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                                                if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                                                {
                                                                                    if (demurrage - d8 <= 0)
                                                                                    {
                                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_08"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        demurrage = demurrage - d8;
                                                                                        vlDemurr = d8 * (decimal)listTable.Rows[0]["VL_VENDA_08"];
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
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_VENDA, ID_MOEDA_DEMURRAGE_VENDA, VL_TAXA_DEMURRAGE_VENDA ";
                    SQL += "VL_DEMURRAGE_VENDA, DT_CAMBIO_DEMURRAGE_VENDA, VL_CAMBIO_DEMURRAGE_VENDA, VL_DESCONTO_DEMURRAGE_VENDA, VL_DEMURRAGE_VENDA_BR ) VALUES ";
                    SQL += "(" + idCont + "," + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "," + listTable.Rows[0]["DT_FINAL_FREETIME"] + ", ";
                    SQL += "" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "," + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "," + somaDias + ", ";
                    SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + ",0," + vlDemurr.ToString().Replace(",", ".") + ",null,null,null,null)";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = " + idStatus + " ,DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);
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

                
                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] * vlTaxa;

                    SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                    SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DEMURRAGE = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ";
                    SQL += "DT_FINAL_DEMURRAGE = '" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', QT_DIAS_DEMURRAGE = " + somaDias + ", ";
                    SQL += "DT_CALCULO_DEMURRAGE_VENDA = '" + sqlFormattedDate + "', ID_MOEDA_DEMURRAGE_VENDA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DEMURRAGE_VENDA = " + vlTaxa.ToString().Replace(",", ".") + ", ";
                    SQL += "VL_DEMURRAGE_VENDA = " + vlDemurr.ToString().Replace(",", ".") + ", DT_CAMBIO_DEMURRAGE_VENDA = null, VL_CAMBIO_DEMURRAGE_VENDA = null, VL_DESCONTO_DEMURRAGE_VENDA = null, VL_DEMURRAGE_VENDA_BR = null WHERE ID_CNTR_BL = "+ idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = " + idStatus + " ,DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);

                }
                else
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

                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"];
                    demurrage = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    vlDemurr = 0;

                    if (somaDias <= ft)
                    {
                        vlDemurr = 0;
                    }
                    else
                    {
                        if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                        {
                            if (demurrage - d1 <= 0)
                            {
                                vlDemurr = demurrage * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                            }
                            else
                            {
                                demurrage = demurrage - d1;
                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                                {
                                    if (demurrage - d2 <= 0)
                                    {
                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_02"]);
                                    }
                                    else
                                    {
                                        demurrage = demurrage - d2;
                                        vlDemurr = d2 * (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                        if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                        {
                                            if (demurrage - d3 <= 0)
                                            {
                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_03"]);
                                            }
                                            else
                                            {
                                                demurrage = demurrage - d3;
                                                vlDemurr = d3 * (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                                if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                                {
                                                    if (demurrage - d4 <= 0)
                                                    {
                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_04"]);
                                                    }
                                                    else
                                                    {
                                                        demurrage = demurrage - d4;
                                                        vlDemurr = d4 * (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                                        if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                                        {
                                                            if (demurrage - d5 <= 0)
                                                            {
                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_05"]);
                                                            }
                                                            else
                                                            {
                                                                demurrage = demurrage - d5;
                                                                vlDemurr = d5 * (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                                                if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                                                {
                                                                    if (demurrage - d6 <= 0)
                                                                    {
                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_06"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        demurrage = demurrage - d6;
                                                                        vlDemurr = d6 * (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                                        if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                                        {
                                                                            if (demurrage - d7 <= 0)
                                                                            {
                                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_07"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                demurrage = demurrage - d7;
                                                                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                                                if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                                                {
                                                                                    if (demurrage - d8 <= 0)
                                                                                    {
                                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_08"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        demurrage = demurrage - d8;
                                                                                        vlDemurr = d8 * (decimal)listTable.Rows[0]["VL_VENDA_08"];
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
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                    SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DEMURRAGE = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ";
                    SQL += "DT_FINAL_DEMURRAGE = '" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', QT_DIAS_DEMURRAGE = " + somaDias + ", ";
                    SQL += "DT_CALCULO_DEMURRAGE_VENDA = '" + sqlFormattedDate + "', ID_MOEDA_DEMURRAGE_VENDA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DEMURRAGE_VENDA = 0, ";
                    SQL += "VL_DEMURRAGE_VENDA = " + vlDemurr.ToString().Replace(",",".") + ", DT_CAMBIO_DEMURRAGE_VENDA = null, VL_CAMBIO_DEMURRAGE_VENDA = null, VL_DESCONTO_DEMURRAGE_VENDA = null, VL_DEMURRAGE_VENDA_BR = null WHERE ID_CNTR_BL = " + idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = " + idStatus + " ,DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);
                }
            }
        }

        [WebMethod]
        public void calcularDemurrageCompra(string idCont, decimal vlTaxa, string transportador, string idStatus, string dtStatus)
        {
            string SQL;
            int somaDias;
            decimal vlDemurr;
            string calcular;
            int demurrage;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            SQL = "SELECT * FROM TB_CNTR_DEMURRAGE WHERE ID_CNTR_BL = " + idCont + " ";
            DataTable search = new DataTable();
            search = DBS.List(SQL);
            if (search == null)
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE, DFCL.ID_MOEDA_DEMURRAGE_COMPRA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
                SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '" + transportador + "' ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                
                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] * vlTaxa;

                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_COMPRA, ID_MOEDA_DEMURRAGE_COMPRA, VL_TAXA_DEMURRAGE_COMPRA, ";
                    SQL += "VL_DEMURRAGE_COMPRA, DT_CAMBIO_DEMURRAGE_COMPRA, VL_CAMBIO_DEMURRAGE_COMPRA, VL_DESCONTO_DEMURRAGE_COMPRA, VL_DEMURRAGE_COMPRA_BR ) VALUES ";
                    SQL += "(" + idCont + ",'" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "','" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', ";
                    SQL += "'" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "','" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "'," + somaDias + ", ";
                    SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + "," + vlTaxa.ToString().Replace(",", ".") + "," + vlDemurr.ToString().Replace(",", ".") + ",null,null,null,null)";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = " + idStatus + " ,DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);
                }
                else
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

                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"];
                    demurrage = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    vlDemurr = 0;

                    if (somaDias <= ft)
                    {
                        vlDemurr = 0;
                    }
                    else
                    {
                        if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                        {
                            if(demurrage - d1 <= 0)
                            {
                                vlDemurr = demurrage * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                            }
                            else
                            {
                                demurrage = demurrage - d1;
                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                                {
                                    if (demurrage - d2 <= 0)
                                    {
                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_02"]);
                                    }
                                    else
                                    {
                                        demurrage = demurrage - d2;
                                        vlDemurr = d2 * (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                        if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                        {
                                            if (demurrage - d3 <= 0)
                                            {
                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_03"]);
                                            }
                                            else
                                            {
                                                demurrage = demurrage - d3;
                                                vlDemurr = d3 * (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                                if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                                {
                                                    if (demurrage - d4 <= 0)
                                                    {
                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_04"]);
                                                    }
                                                    else
                                                    {
                                                        demurrage = demurrage - d4;
                                                        vlDemurr = d4 * (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                                        if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                                        {
                                                            if (demurrage - d5 <= 0)
                                                            {
                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_05"]);
                                                            }
                                                            else
                                                            {
                                                                demurrage = demurrage - d5;
                                                                vlDemurr = d5 * (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                                                if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                                                {
                                                                    if (demurrage - d6 <= 0)
                                                                    {
                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_06"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        demurrage = demurrage - d6;
                                                                        vlDemurr = d6 * (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                                        if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                                        {
                                                                            if (demurrage - d7 <= 0)
                                                                            {
                                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_07"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                demurrage = demurrage - d7;
                                                                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                                                if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                                                {
                                                                                    if (demurrage - d8 <= 0)
                                                                                    {
                                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_08"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        demurrage = demurrage - d8;
                                                                                        vlDemurr = d8 * (decimal)listTable.Rows[0]["VL_VENDA_08"];
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
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_COMPRA, ID_MOEDA_DEMURRAGE_COMPRA, VL_TAXA_DEMURRAGE_COMPRA, ";
                    SQL += "VL_DEMURRAGE_COMPRA, DT_CAMBIO_DEMURRAGE_COMPRA, VL_CAMBIO_DEMURRAGE_COMPRA, VL_DESCONTO_DEMURRAGE_COMPRA, VL_DEMURRAGE_COMPRA_BR ) VALUES ";
                    SQL += "(" + idCont + "," + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "," + listTable.Rows[0]["DT_FINAL_FREETIME"] + ", ";
                    SQL += "" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "," + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "," + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] + ", ";
                    SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + ",0,'" + vlDemurr.ToString().Replace(",",".") + "',null,null,null,null)";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = " + idStatus + " ,DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);
                }
            }
            else
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE, DFCL.ID_MOEDA_DEMURRAGE_COMPRA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
                SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '"+ transportador + "' ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                
                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] * vlTaxa;

                    SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                    SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DEMURRAGE = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ";
                    SQL += "DT_FINAL_DEMURRAGE = '" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', QT_DIAS_DEMURRAGE = " + somaDias + ", ";
                    SQL += "DT_CALCULO_DEMURRAGE_COMPRA = '" + sqlFormattedDate + "', ID_MOEDA_DEMURRAGE_COMPRA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DEMURRAGE_COMPRA = " + vlTaxa.ToString().Replace(",", ".") + ", ";
                    SQL += "VL_DEMURRAGE_COMPRA = " + vlDemurr.ToString().Replace(",", ".") + ", DT_CAMBIO_DEMURRAGE_COMPRA = null, VL_CAMBIO_DEMURRAGE_COMPRA = null, VL_DESCONTO_DEMURRAGE_COMPRA = null, VL_DEMURRAGE_COMPRA_BR = null WHERE ID_CNTR_BL = " + idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = " + idStatus + " ,DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);
                }
                else
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

                    somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"];
                    demurrage = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                    vlDemurr = 0;

                    if (somaDias <= ft)
                    {
                        vlDemurr = 0;
                    }
                    else
                    {
                        if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                        {
                            if (demurrage - d1 <= 0)
                            {
                                vlDemurr = demurrage * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                            }
                            else
                            {
                                demurrage = demurrage - d1;
                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                                {
                                    if (demurrage - d2 <= 0)
                                    {
                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_02"]);
                                    }
                                    else
                                    {
                                        demurrage = demurrage - d2;
                                        vlDemurr = d2 * (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                        if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                        {
                                            if (demurrage - d3 <= 0)
                                            {
                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_03"]);
                                            }
                                            else
                                            {
                                                demurrage = demurrage - d3;
                                                vlDemurr = d3 * (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                                if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                                {
                                                    if (demurrage - d4 <= 0)
                                                    {
                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_04"]);
                                                    }
                                                    else
                                                    {
                                                        demurrage = demurrage - d4;
                                                        vlDemurr = d4 * (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                                        if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                                        {
                                                            if (demurrage - d5 <= 0)
                                                            {
                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_05"]);
                                                            }
                                                            else
                                                            {
                                                                demurrage = demurrage - d5;
                                                                vlDemurr = d5 * (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                                                if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                                                {
                                                                    if (demurrage - d6 <= 0)
                                                                    {
                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_06"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        demurrage = demurrage - d6;
                                                                        vlDemurr = d6 * (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                                        if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                                        {
                                                                            if (demurrage - d7 <= 0)
                                                                            {
                                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_07"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                demurrage = demurrage - d7;
                                                                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                                                if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                                                {
                                                                                    if (demurrage - d8 <= 0)
                                                                                    {
                                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_08"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        demurrage = demurrage - d8;
                                                                                        vlDemurr = d8 * (decimal)listTable.Rows[0]["VL_VENDA_08"];
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
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                    SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DEMURRAGE = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ";
                    SQL += "DT_FINAL_DEMURRAGE = '" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', QT_DIAS_DEMURRAGE = " + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] + ", ";
                    SQL += "DT_CALCULO_DEMURRAGE_COMPRA = '" + sqlFormattedDate + "', ID_MOEDA_DEMURRAGE_COMPRA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DEMURRAGE_COMPRA = 0, ";
                    SQL += "VL_DEMURRAGE_COMPRA = " + vlDemurr.ToString().Replace(",",".") + ", DT_CAMBIO_DEMURRAGE_COMPRA = null, VL_CAMBIO_DEMURRAGE_COMPRA = null, VL_DESCONTO_DEMURRAGE_COMPRA = null, VL_DEMURRAGE_COMPRA_BR = null WHERE ID_CNTR_BL = " + idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = " + idStatus + " ,DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);
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
        public string listarFaturas(int check, string filtroFatura, string txtFiltro, string Ativo, string Finalizado)
        {
            DataTable listTable = new DataTable();
            switch (filtroFatura)
            {
                case "1":
                    filtroFatura = "AND C.NR_PROCESSO = '"+ txtFiltro + "' ";
                    break;
                case "2":
                    filtroFatura = "AND C.NM_CLIENTE = '"+ txtFiltro + "' ";
                    break;
                case "3":
                    filtroFatura = "AND C.NM_TRANSPORTADOR = '"+ txtFiltro + "' ";
                    break;
                default:
                    filtroFatura = "";
                    break;
            }

            if (Ativo == "1" && Finalizado == "1")
            {
                Ativo = "";
                Finalizado = "";
            }
            else
            {
                switch (Ativo)
                {
                    case "1":
                        Ativo = "AND C.DT_CANCELAMENTO IS NULL AND C.DT_LIQUIDACAO IS NULL AND C.DT_EXPORTACAO_DEMURRAGE IS NULL ";
                        break;
                    default:
                        Ativo = "";
                        break;
                }

                switch (Finalizado)
                {
                    case "1":
                        Finalizado = "AND (C.DT_CANCELAMENTO IS NOT NULL OR C.DT_LIQUIDACAO IS NOT NULL AND C.DT_EXPORTACAO_DEMURRAGE IS NOT NULL) ";
                        break;
                    default:
                        Finalizado = "";
                        break;
                }
            }

            if (check == 1)
            {
                string SQL;
                SQL = "SELECT C.ID_DEMURRAGE_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DEMURRAGE,'dd/MM/yyyy'),'') as DT_EXPORTACAO_DEMURRAGE, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO,'dd/MM/yyyy'),'') AS DT_LIQUIDACAO,ISNULL(FORMAT(C.DT_CANCELAMENTO,'dd/MM/yyyy'),'') AS DT_CANCELAMENTO ";
                SQL += "FROM VW_DEMURRAGE_FATURA C ";
                SQL += "JOIN TB_DEMURRAGE_FATURA B ON C.ID_DEMURRAGE_FATURA = B.ID_DEMURRAGE_FATURA ";
                SQL += "WHERE B.CD_PR = 'R' ";
                SQL += "" + filtroFatura + " ";
                SQL += "" + Ativo + " ";
                SQL += "" + Finalizado + " ";
                listTable = DBS.List(SQL);
            }
            else
            {
                string SQL;
                SQL = "SELECT C.ID_DEMURRAGE_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DEMURRAGE,'dd/MM/yyyy'),'') as DT_EXPORTACAO_DEMURRAGE, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO,'dd/MM/yyyy'),'') AS DT_LIQUIDACAO,ISNULL(FORMAT(C.DT_CANCELAMENTO,'dd/MM/yyyy'),'') AS DT_CANCELAMENTO ";
                SQL += "FROM VW_DEMURRAGE_FATURA C ";
                SQL += "JOIN TB_DEMURRAGE_FATURA B ON C.ID_DEMURRAGE_FATURA = B.ID_DEMURRAGE_FATURA ";
                SQL += "WHERE B.CD_PR = 'P' ";
                SQL += "" + filtroFatura + " ";
                SQL += "" + Ativo + " ";
                SQL += "" + Finalizado + " ";
                listTable = DBS.List(SQL);
            }

            
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string verificarProcesso(string processo)
        {
            string SQL;
            SQL = "SELECT * ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_BL B ON DFCL.ID_BL = B.ID_BL ";
            SQL += "WHERE B.DT_CANCELAMENTO IS NULL ";
            SQL += "AND PFCL.NR_PROCESSO = '" + processo + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if(listTable != null)
            {
                return "OK";
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public string listarProcessoFaturas(string processo, int check)
        {
            string SQL;
            DataTable listTable = new DataTable();
            if (check == 1) {
                SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, ";
                SQL += "M.NM_MOEDA ,DFCL.VL_TAXA_DEMURRAGE_VENDA AS TAXA_DEMURRAGE,FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'dd/MM/yyyy') as DT_INICIAL_DEMURRAGE, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'dd/MM/yyyy') AS DT_FINAL_DEMURRAGE,DFCL.QT_DIAS_DEMURRAGE,DFCL.VL_DEMURRAGE_VENDA AS VL_DEMURRAGE ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DEMURRAGE_VENDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.NR_PROCESSO = '" + processo + "' ";
                SQL += "AND DFCL.ID_DEMURRAGE_FATURA_RECEBER IS NULL ";
                SQL += "AND DFCL.VL_DEMURRAGE_VENDA IS NOT NULL ";
                SQL += "AND PFCL.FL_DEMURRAGE_FINALIZADA = 0 ";
                listTable = DBS.List(SQL);
            }
            else
            {
                SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, ";
                SQL += "M.NM_MOEDA ,DFCL.VL_TAXA_DEMURRAGE_COMPRA AS TAXA_DEMURRAGE,FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'dd/MM/yyyy') as DT_INICIAL_DEMURRAGE, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'dd/MM/yyyy') AS DT_FINAL_DEMURRAGE,DFCL.QT_DIAS_DEMURRAGE,DFCL.VL_DEMURRAGE_COMPRA AS VL_DEMURRAGE ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DEMURRAGE_COMPRA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.NR_PROCESSO = '" + processo + "' ";
                SQL += "AND DFCL.ID_DEMURRAGE_FATURA_PAGAR IS NULL ";
                SQL += "AND DFCL.VL_DEMURRAGE_COMPRA IS NOT NULL ";
                SQL += "AND PFCL.FL_DEMURRAGE_FINALIZADA = 0 ";
                listTable = DBS.List(SQL);
            }
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public void processarFatura(string processo, int check)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DataTable localizarFatura = new DataTable();
            string SQL;
            if (check == 1)
            {
                SQL = "SELECT ID_BL FROM TB_BL WHERE NR_PROCESSO = '" + processo + "' ";
                localizarFatura = DBS.List(SQL);
                int idbl = (int)localizarFatura.Rows[0]["ID_BL"];

                SQL = "INSERT INTO TB_DEMURRAGE_FATURA (ID_BL, CD_PR, DT_LANCAMENTO, ID_USUARIO_LANCAMENTO) ";
                SQL += "VALUES (" + idbl + ",'R','" + sqlFormattedDate + "','12') ";
                string processarFatura = DBS.ExecuteScalar(SQL);

                
            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_BL WHERE NR_PROCESSO = '" + processo + "' ";
                localizarFatura = DBS.List(SQL);
                int idbl = (int)localizarFatura.Rows[0]["ID_BL"];

                SQL = "INSERT INTO TB_DEMURRAGE_FATURA (ID_BL, CD_PR, DT_LANCAMENTO, ID_USUARIO_LANCAMENTO) ";
                SQL += "VALUES (" + idbl + ",'P','" + sqlFormattedDate + "','12') ";
                string processarFatura = DBS.ExecuteScalar(SQL);

            }
            
        }

        [WebMethod]
        public void processarFaturaItens(int idcntr, int check)
        {
            DataTable localizarFatura = new DataTable();
            string SQL;
            if (check == 1)
            {
                SQL = "select id_demurrage_fatura from tb_DEMURRAGE_FATURA A ";
                SQL += "join TB_AMR_CNTR_BL B on A.ID_BL = B.ID_BL ";
                SQL += "where b.ID_CNTR_BL = " + idcntr + "";
                SQL += "AND A.CD_PR = 'R'";
                localizarFatura = DBS.List(SQL);
                int iddemurragefatura = (int)localizarFatura.Rows[0]["id_demurrage_fatura"];

                SQL = "select ID_CNTR_DEMURRAGE from tb_CNTR_DEMURRAGE A ";
                SQL += "join TB_AMR_CNTR_BL B on A.ID_CNTR_BL = B.ID_CNTR_BL ";
                SQL += "where b.ID_CNTR_BL = " + idcntr + "";
                localizarFatura = DBS.List(SQL);
                int idcntrdemurrage = (int)localizarFatura.Rows[0]["ID_CNTR_DEMURRAGE"];

                SQL = "INSERT INTO TB_DEMURRAGE_FATURA_ITENS (ID_DEMURRAGE_FATURA, ID_CNTR_DEMURRAGE) ";
                SQL += "VALUES (" + iddemurragefatura + ",'" + idcntrdemurrage + "') ";
                string processarFatura = DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "select id_demurrage_fatura from tb_DEMURRAGE_FATURA A ";
                SQL += "join TB_AMR_CNTR_BL B on A.ID_BL = B.ID_BL ";
                SQL += "where b.ID_CNTR_BL = " + idcntr + "";
                SQL += "AND A.CD_PR = 'P'";
                localizarFatura = DBS.List(SQL);
                int iddemurragefatura = (int)localizarFatura.Rows[0]["id_demurrage_fatura"];

                SQL = "select ID_CNTR_DEMURRAGE from tb_CNTR_DEMURRAGE A ";
                SQL += "join TB_AMR_CNTR_BL B on A.ID_CNTR_BL = B.ID_CNTR_BL ";
                SQL += "where b.ID_CNTR_BL = " + idcntr + "";
                localizarFatura = DBS.List(SQL);
                int idcntrdemurrage = (int)localizarFatura.Rows[0]["ID_CNTR_DEMURRAGE"];

                SQL = "INSERT INTO TB_DEMURRAGE_FATURA_ITENS (ID_DEMURRAGE_FATURA, ID_CNTR_DEMURRAGE) ";
                SQL += "VALUES (" + iddemurragefatura + ",'" + idcntrdemurrage + "') ";
                string processarFatura = DBS.ExecuteScalar(SQL);
            }

        }

        [WebMethod]
        public string infoAtualizacao(int idFatura)
        {
            string SQL;
            DataTable listTable = new DataTable();

            SQL = "SELECT MIN(A.ID_DEMURRAGE_FATURA) AS ID_DEMURRAGE_FATURA , MIN(B.NR_PROCESSO) AS NR_PROCESSO, MIN(P.NM_RAZAO) as CLIENTE, ";
            SQL += "ISNULL(CONVERT(VARCHAR,MIN(MF.VL_TXOFICIAL)),'') AS VL_TAXA, ISNULL(FORMAT(MIN(DT_CAMBIO),'dd/MM/yyyy'),'') AS DT_CAMBIO ";
            SQL += "FROM TB_DEMURRAGE_FATURA A ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON B.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA_FRETE_ARMADOR MF ON B.ID_PARCEIRO_TRANSPORTADOR = MF.ID_ARMADOR ";
            SQL += "WHERE A.ID_DEMURRAGE_FATURA = "+idFatura+" ";

            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }
        
        [WebMethod]
        public string listarFaturasAtualizacaoCambial(int idFatura, int check)
        {
            string SQL;
            DataTable listTable = new DataTable();
            if (check == 1)
            {
                SQL = "select D.ID_CNTR_BL, D.NR_CNTR, M.NM_MOEDA, E.VL_DEMURRAGE_VENDA AS VL_DEMURRAGE, ISNULL(E.VL_DESCONTO_DEMURRAGE_VENDA,0) AS DESCONTO from tb_demurrage_fatura_itens a ";
                SQL += "LEFT join TB_DEMURRAGE_FATURA B ON B.ID_DEMURRAGE_FATURA = a.ID_DEMURRAGE_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DEMURRAGE E ON E.ID_CNTR_DEMURRAGE = a.ID_CNTR_DEMURRAGE ";
                SQL += "LEFT JOIN TB_CNTR_BL D ON E.ID_CNTR_BL = D.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON E.ID_MOEDA_DEMURRAGE_VENDA = M.ID_MOEDA ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + "";
                listTable = DBS.List(SQL);
            }
            else
            {
                SQL = "select D.ID_CNTR_BL, D.NR_CNTR, M.NM_MOEDA, E.VL_DEMURRAGE_COMPRA AS VL_DEMURRAGE, ISNULL(E.VL_DESCONTO_DEMURRAGE_COMPRA,0) AS DESCONTO from tb_demurrage_fatura_itens a ";
                SQL += "LEFT join TB_DEMURRAGE_FATURA B ON B.ID_DEMURRAGE_FATURA = a.ID_DEMURRAGE_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DEMURRAGE E ON E.ID_CNTR_DEMURRAGE = a.ID_CNTR_DEMURRAGE ";
                SQL += "LEFT JOIN TB_CNTR_BL D ON E.ID_CNTR_BL = D.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON E.ID_MOEDA_DEMURRAGE_COMPRA = M.ID_MOEDA ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + "";
                listTable = DBS.List(SQL);
            }
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string atualizacaoCambialFatura(int idFatura, string dtVencimento,string idContaBancaria)
        {
            string SQL;
            DataTable listTable = new DataTable();
            SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_VENCIMENTO = '" + dtVencimento + "', ";
            SQL += "ID_CONTA_BANCARIA = '" + idContaBancaria + "' ";
            SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
            string listTable2 = DBS.ExecuteScalar(SQL);
            return "ok";
        }

        [WebMethod]
        public string atualizacaoCambialContainer(int idCntr, double vlDemurrage, string dtCambio, double vlCambio, double descontoBRL, int check)
        {
            string SQL;
            DataTable listTable = new DataTable();
            double valorLiquido = vlDemurrage - descontoBRL;
            double valorDemurrageConvertido = vlDemurrage * vlCambio;

            if (check == 1)
            {
                SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_CAMBIO_DEMURRAGE_VENDA = '" + dtCambio + "', ";
                SQL += "VL_CAMBIO_DEMURRAGE_VENDA = '" + vlCambio.ToString().Replace(",", ".") + "', VL_DESCONTO_DEMURRAGE_VENDA = '" + descontoBRL.ToString().Replace(",", ".") + "', ";
                SQL += "VL_DEMURRAGE_VENDA_BR = '" + valorDemurrageConvertido.ToString().Replace(",", ".") + "', VL_DEMURRAGE_LIQUIDO_VENDA = '" + valorLiquido.ToString().Replace(",",".") + "' ";
                SQL += "WHERE ID_CNTR_BL = '" + idCntr + "' ";
                string atualizarContainer = DBS.ExecuteScalar(SQL);
                return "ok";
            }
            else
            {
                SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_CAMBIO_DEMURRAGE_COMPRA = '" + dtCambio + "', ";
                SQL += "VL_CAMBIO_DEMURRAGE_COMPRA = '" + vlCambio.ToString().Replace(",", ".") + "', VL_DESCONTO_DEMURRAGE_COMPRA = '" + descontoBRL.ToString().Replace(",", ".") + "', ";
                SQL += "VL_DEMURRAGE_COMPRA_BR = '" + valorDemurrageConvertido.ToString().Replace(",",".") + "', VL_DEMURRAGE_LIQUIDO_COMPRA = '" + valorLiquido.ToString().Replace(",", ".") + "' ";
                SQL += "WHERE ID_CNTR_BL = '" + idCntr + "' ";
                string atualizarContainer = DBS.ExecuteScalar(SQL);
                return "ok";
            }

            
        }

        [WebMethod]
        public string infoCancelar(int idFatura)
        {
            string SQL;

            SQL = "select a.ID_DEMURRAGE_FATURA, b.NR_PROCESSO, b.NM_CLIENTE ";
            SQL += "from tb_demurrage_fatura a ";
            SQL += "join VW_DEMURRAGE_FATURA b on a.ID_DEMURRAGE_FATURA = b.ID_DEMURRAGE_FATURA ";
            SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + "";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string cancelarFatura(int idFatura,string motivoCancelamento)
        {
            string SQL;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            SQL = "select * from tb_demurrage_fatura a ";
            SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + " ";
            SQL += "AND A.DT_EXPORTACAO_DEMURRAGE IS NOT NULL ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if(listTable != null)
            {
                SQL = "DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE DT_COMPETENCIA = '" + idFatura + "' AND TP_EXPORTACAO='DEM' ";
                string deleteContaPagarReceber = DBS.ExecuteScalar(SQL);

                SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = 12, ";
                SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' ";
                string updateFatura = DBS.ExecuteScalar(SQL);

                return "ok";
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public string infoExportCC(int idFatura)
        {
            string SQL;

            SQL = "SELECT DISTINCT A.ID_DEMURRAGE_FATURA, B.NR_PROCESSO, ";
            SQL += "P.NM_RAZAO AS CLIENTE, B.ID_STATUS_DEMURRAGE ";
            SQL += "from TB_DEMURRAGE_FATURA A ";
            SQL += "JOIN VW_PROCESSO_CONTAINER_FCL B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN VW_PROCESSO_DEMURRAGE_FCL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "JOIN TB_PARCEIRO P ON B.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + "";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod]
        public string exportarCC(int idFatura, string dtLiquidacao, int check, int dsStatus)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string SQL;
            int i;
            SQL = "select cd_pr,FORMAT(dt_lancamento,'YYYY-MM-DD hh:mm:ss') AS DT_LANCAMENTO, ID_USUARIO_LANCAMENTO,FORMAT(dt_vencimento,'yyyy/MM/dd') as DT_VENCIMENTO,id_conta_bancaria ";
            SQL += "from tb_demurrage_fatura ";
            SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string cdpr = listTable.Rows[0]["cd_pr"].ToString();
            string dtLancamento = listTable.Rows[0]["dt_lancamento"].ToString();
            int idUsuario = (int)listTable.Rows[0]["ID_USUARIO_LANCAMENTO"];
            string dtVencimento = listTable.Rows[0]["DT_VENCIMENTO"].ToString();
            int idConta = (int)listTable.Rows[0]["ID_CONTA_BANCARIA"];
            string flagF;
            SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + dsStatus + "' ";
            DataTable flFinaliza = new DataTable();
            flFinaliza = DBS.List(SQL);
            flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

            if (check == 1) {
                SQL = "SELECT ID_BL FROM TB_DEMURRAGE_FATURA ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "SELECT id_cntr_bl FROM TB_DEMURRAGE_FATURA a ";
                SQL += "join TB_AMR_CNTR_BL b on a.ID_BL = b.ID_BL ";
                SQL += "WHERE A.ID_BL = '" + idbl + "'";
                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA ";
                SQL += ",ID_USUARIO_LANCAMENTO,DT_LIQUIDACAO,ID_USUARIO_LIQUIDACAO,CD_PR,DT_COMPETENCIA ";
                SQL += ",TP_EXPORTACAO) VALUES('" + dtLancamento + "','" + dtVencimento + "','" + idConta + "', ";
                SQL += "'" + idUsuario + "','" + dtLiquidacao + "','12','" + cdpr + "','" + idFatura + "','DEM') SELECT SCOPE_IDENTITY()";
                string insertConta = DBS.ExecuteScalar(SQL);

                for (i = 0; i < qtdRows; i++) {
                    SQL = "SELECT ID_MOEDA_DEMURRAGE_VENDA,VL_DEMURRAGE_VENDA ";
                    SQL += ",ID_PARCEIRO_CLIENTE,FORMAT(DT_CAMBIO_DEMURRAGE_VENDA,'yyyy-MM-dd') AS DT_CAMBIO_DEMURRAGE_VENDA ";
                    SQL += ",VL_CAMBIO_DEMURRAGE_VENDA,VL_DEMURRAGE_VENDA_BR ";
                    SQL += ",VL_DESCONTO_DEMURRAGE_VENDA,VL_DEMURRAGE_LIQUIDO_VENDA ";
                    SQL += "FROM TB_CNTR_DEMURRAGE A ";
                    SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
                    SQL += "WHERE A.ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "'";
                    DataTable vlDemurrage = new DataTable();
                    vlDemurrage = DBS.List(SQL);
                    int idMoedaVenda = (int)vlDemurrage.Rows[0]["ID_MOEDA_DEMURRAGE_VENDA"];
                    string vlDemurrageVenda = vlDemurrage.Rows[0]["VL_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    int parceiroCliente = (int)vlDemurrage.Rows[0]["ID_PARCEIRO_CLIENTE"];
                    string dtCambioVenda = vlDemurrage.Rows[0]["DT_CAMBIO_DEMURRAGE_VENDA"].ToString();
                    string vlCambioDemuVenda = vlDemurrage.Rows[0]["VL_CAMBIO_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    string vlDemuVendaBR = vlDemurrage.Rows[0]["VL_DEMURRAGE_VENDA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuVenda = vlDemurrage.Rows[0]["VL_DESCONTO_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    string vlDemuLiquidVenda = vlDemurrage.Rows[0]["VL_DEMURRAGE_LIQUIDO_VENDA"].ToString().Replace(",", ".");

                    SQL = "SELECT ID_ITEM_DEMURRAGE FROM TB_PARAMETROS ";
                    DataTable idItemDespesa = new DataTable();
                    idItemDespesa = DBS.List(SQL);
                    int idItemD = (int)idItemDespesa.Rows[0]["ID_ITEM_DEMURRAGE"];

                    SQL = "SELECT FL_INTEGRA_PA FROM TB_ITEM_DESPESA ";
                    SQL += "WHERE ID_ITEM_DESPESA = '" + idItemD + "' ";
                    DataTable flIntegra = new DataTable();
                    flIntegra = DBS.List(SQL);
                    string flIntegraPA = flIntegra.Rows[0]["FL_INTEGRA_PA"].ToString();

                    SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER, ID_BL, ID_ITEM_DESPESA, ID_DESTINATARIO_COBRANCA, ";
                    SQL += "ID_MOEDA, ID_PARCEIRO_EMPRESA, VL_TAXA_CALCULADO, DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_DESCONTO,VL_LIQUIDO, FL_INTEGRA_PA) VALUES ";
                    SQL += "('"+ insertConta + "','"+ idbl + "','"+ idItemD + "','1','" + idMoedaVenda + "', ";
                    SQL += "'" + parceiroCliente + "','" + vlDemurrageVenda + "','"+ dtCambioVenda + "','"+ vlCambioDemuVenda + "','"+ vlDemuVendaBR + "' ";
                    SQL += ",'"+ vlDescDemuVenda + "','"+ vlDemuLiquidVenda + "','"+ flIntegraPA + "') ";
                    string insertContaPGI = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_EXPORTACAO_DEMURRAGE = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DEMURRAGE = '12', ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                string updtDemurrageFatura = DBS.ExecuteScalar(SQL);


                SQL += "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = '" + dsStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' WHERE ID_CNTR_BL = '" + cntrBl + "' ";
                string updtDsStatus = DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_DEMURRAGE_FATURA ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "SELECT id_cntr_bl FROM TB_DEMURRAGE_FATURA a ";
                SQL += "join TB_AMR_CNTR_BL b on a.ID_BL = b.ID_BL ";
                SQL += "WHERE A.ID_BL = '" + idbl + "'";
                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA ";
                SQL += ",ID_USUARIO_LANCAMENTO,DT_LIQUIDACAO,ID_USUARIO_LIQUIDACAO,CD_PR,DT_COMPETENCIA ";
                SQL += ",TP_EXPORTACAO) VALUES('" + dtLancamento + "','" + dtVencimento + "','" + idConta + "', ";
                SQL += "'" + idUsuario + "','" + dtLiquidacao + "','12','" + cdpr + "','" + idFatura + "','DEM') SELECT SCOPE_IDENTITY() ";
                string insertConta = DBS.ExecuteScalar(SQL);

                for (i = 0; i < qtdRows; i++)
                {
                    SQL = "SELECT ID_MOEDA_DEMURRAGE_COMPRA,VL_DEMURRAGE_COMPRA ";
                    SQL += ",ID_PARCEIRO_TRANSPORTADOR,FORMAT(DT_CAMBIO_DEMURRAGE_COMPRA,'yyyy-MM-dd') AS DT_CAMBIO_DEMURRAGE_COMPRA ";
                    SQL += ",VL_CAMBIO_DEMURRAGE_COMPRA,VL_DEMURRAGE_COMPRA_BR ";
                    SQL += ",VL_DESCONTO_DEMURRAGE_COMPRA,VL_DEMURRAGE_LIQUIDO_COMPRA ";
                    SQL += "FROM TB_CNTR_DEMURRAGE A ";
                    SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
                    SQL += "WHERE A.ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "'";
                    DataTable vlDemurrage = new DataTable();
                    vlDemurrage = DBS.List(SQL);
                    int idMoedaCompra = (int)vlDemurrage.Rows[0]["ID_MOEDA_DEMURRAGE_COMPRA"];
                    string vlDemurrageCompra= vlDemurrage.Rows[0]["VL_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");
                    int parceiroTransportador = (int)vlDemurrage.Rows[0]["ID_PARCEIRO_TRANSPORTADOR"];
                    string dtCambioCompra = vlDemurrage.Rows[0]["DT_CAMBIO_DEMURRAGE_COMPRA"].ToString();
                    string vlCambioDemuCompra = vlDemurrage.Rows[0]["VL_CAMBIO_DEMURRAGE_COMPRA"].ToString().Replace(",",".");
                    string vlDemuCompraBR = vlDemurrage.Rows[0]["VL_DEMURRAGE_COMPRA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuCompra = vlDemurrage.Rows[0]["VL_DESCONTO_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");
                    string vlDemuLiquidCompra = vlDemurrage.Rows[0]["VL_DEMURRAGE_LIQUIDO_COMPRA"].ToString().Replace(",", ".");

                    SQL = "SELECT ID_ITEM_DEMURRAGE FROM TB_PARAMETROS ";
                    DataTable idItemDespesa = new DataTable();
                    idItemDespesa = DBS.List(SQL);
                    int idItemD = (int)idItemDespesa.Rows[0]["ID_ITEM_DEMURRAGE"];

                    SQL = "SELECT FL_INTEGRA_PA FROM TB_ITEM_DESPESA ";
                    SQL += "WHERE ID_ITEM_DESPESA = '" + idItemD + "' ";
                    DataTable flIntegra = new DataTable();
                    flIntegra = DBS.List(SQL);
                    string flIntegraPA = flIntegra.Rows[0]["FL_INTEGRA_PA"].ToString();

                    SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER, ID_BL, ID_ITEM_DESPESA, ID_DESTINATARIO_COBRANCA, ";
                    SQL += "ID_MOEDA, ID_PARCEIRO_EMPRESA, VL_TAXA_CALCULADO, DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_DESCONTO,VL_LIQUIDO, FL_INTEGRA_PA) VALUES ";
                    SQL += "('" + insertConta + "','" + idbl + "','" + idItemD + "','1','" + idMoedaCompra + "', ";
                    SQL += "'" + parceiroTransportador + "','" + vlDemurrageCompra + "','" + dtCambioCompra + "','" + vlCambioDemuCompra + "','" + vlDemuCompraBR + "' ";
                    SQL += ",'" + vlDescDemuCompra + "','" + vlDemuLiquidCompra + "','" + flIntegraPA + "') ";
                    string insertContaPGI = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_EXPORTACAO_DEMURRAGE = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DEMURRAGE = '12', ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                string updtDemurrageFatura = DBS.ExecuteScalar(SQL);

                SQL += "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = '" + dsStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' WHERE ID_CNTR_BL = '" + cntrBl + "' ";
                string updtDsStatus = DBS.ExecuteScalar(SQL);
            }
            return "ok";
        }

        [WebMethod]
        public string imprimirDadosCalc(string id)
        {
            string SQL;
            SQL = "SELECT TOP 1 A.NR_PROCESSO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
            SQL += "WHERE A.ID_CNTR_BL = '" + id + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string processoNr = listTable.Rows[0]["NR_PROCESSO"].ToString();

            SQL = "SELECT TOP 1 CLIENTE.NM_RAZAO, CLIENTE.ENDERECO, CLIENTE.NR_ENDERECO, ";
            SQL += "CIDADE.NM_CIDADE, CLIENTE.BAIRRO, ESTADO.NM_ESTADO, CLIENTE.CEP, ";
            SQL += "CLIENTE.CNPJ, ISNULL(CLIENTE.INSCR_ESTADUAL,'') AS INSCR_ESTADUAL, A.NR_PROCESSO, SERV.NM_SERVICO, ";
            SQL += "ORIGEM.NM_PORTO AS ORIGEM, DESTINO.NM_PORTO AS DESTINO, FORMAT(BL.DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, ";
            SQL += "FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, NAV.NM_NAVIO AS NAVIO, M.NR_BL AS MASTER, BL.NR_BL AS HOUSE, ";
            SQL += "TRANSPORTADOR.NM_RAZAO AS TRANSPORTADOR, ISNULL(CONVERT(VARCHAR,BL.VL_PESO_BRUTO),'') AS VL_PESO_BRUTO, ISNULL(CONVERT(VARCHAR,BL.VL_M3),'') AS VL_M3, ISNULL(CONVERT(VARCHAR,BL.VL_INDICE_VOLUMETRICO),'') AS VL_INDICE_VOLUMETRICO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO CLIENTE ON A.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO TRANSPORTADOR ON A.ID_PARCEIRO_TRANSPORTADOR = TRANSPORTADOR.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_BL BL ON A.ID_BL = BL.ID_BL ";
            SQL += "LEFT JOIN TB_BL M ON BL.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_CIDADE CIDADE ON CLIENTE.ID_CIDADE = CIDADE.ID_CIDADE ";
            SQL += "LEFT JOIN TB_ESTADO ESTADO ON CIDADE.ID_ESTADO = ESTADO.ID_ESTADO ";
            SQL += "LEFT JOIN TB_SERVICO SERV ON BL.ID_SERVICO = SERV.ID_SERVICO ";
            SQL += "LEFT JOIN TB_PORTO ORIGEM ON BL.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO DESTINO ON BL.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += "LEFT JOIN TB_NAVIO NAV ON BL.ID_NAVIO = NAV.ID_NAVIO ";
            SQL += "WHERE A.NR_PROCESSO = '" + processoNr + "' ";
            DataTable imprimirDados = new DataTable();
            imprimirDados = DBS.List(SQL);
            return JsonConvert.SerializeObject(imprimirDados);
        }

        [WebMethod]
        public string imprimirDadosFatura(string idFatura)
        {
            string SQL;
            SQL = "SELECT P1.NM_RAZAO AS CLIENTE, P1.ENDERECO,P1.NR_ENDERECO, C.NM_CIDADE, ISNULL(FORMAT(A.DT_LANCAMENTO,'dd/MM/yy'),'') AS DT_LANCAMENTO, ISNULL(FORMAT(A.DT_VENCIMENTO,'dd/MM/yy'),'') AS DT_VENCIMENTO, ";
            SQL += "P1.BAIRRO, E.NM_ESTADO, P1.CEP, P1.CNPJ, ISNULL(P1.INSCR_ESTADUAL,'') AS INSCR_ESTADUAL, B.NR_PROCESSO, P2.NM_RAZAO AS TRANSPORTADOR, ";
            SQL += "S.NM_SERVICO, ORIGEM.NM_PORTO AS ORIGEM, DESTINO.NM_PORTO as DESTINO, FORMAT(B.DT_EMBARQUE, 'dd/MM/yyyy') as DT_EMBARQUE, ";
            SQL += "FORMAT(B.DT_CHEGADA, 'dd/MM/yyyy') AS DT_CHEGADA, isnull(CONVERT(VARCHAR,B.VL_PESO_BRUTO),'') as VL_PESO_BRUTO, isnull(CONVERT(VARCHAR,B.VL_M3),'') AS VL_M3, ISNULL(CONVERT(VARCHAR,B.VL_INDICE_VOLUMETRICO),'') AS VL_INDICE_VOLUMETRICO, ";
            SQL += "N.NM_NAVIO AS NAVIO, M.NR_BL AS MASTER, B.NR_BL AS HOUSE ";
            SQL += "from TB_DEMURRAGE_FATURA A ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P1 ON B.ID_PARCEIRO_CLIENTE = P1.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON B.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_NAVIO N ON B.ID_NAVIO = N.ID_NAVIO ";
            SQL += "LEFT JOIN TB_PORTO ORIGEM ON B.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO DESTINO ON B.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += "LEFT JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_CIDADE C ON P1.ID_CIDADE = C.ID_CIDADE ";
            SQL += "LEFT JOIN TB_ESTADO E ON C.ID_ESTADO = E.ID_ESTADO ";
            SQL += "LEFT JOIN TB_SERVICO S ON B.ID_SERVICO = S.ID_SERVICO ";
            SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + " ";

            DataTable imprimirDados = new DataTable();
            imprimirDados = DBS.List(SQL);
            return JsonConvert.SerializeObject(imprimirDados);
        }

        [WebMethod]
        public string listarContainerFaturaPrint(string idFatura)
        {
            string SQL;
            SQL = "SELECT A.NR_CNTR, A.NM_TIPO_CONTAINER, FORMAT(B.DT_INICIAL_FREETIME,'dd/MM/yy') AS INICIALFT, ";
            SQL += "FORMAT(B.DT_FINAL_FREETIME,'dd/MM/yy') AS FINALFT,A.QT_DIAS_FREETIME, ";
            SQL += "FORMAT(B.DT_INICIAL_DEMURRAGE,'dd/MM/yy') AS INICIALDEM, FORMAT(B.DT_FINAL_DEMURRAGE,'dd/MM/yy') AS FINALDEM, ";
            SQL += "B.QT_DIAS_DEMURRAGE, MD.SIGLA_MOEDA, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DEMURRAGE_COMPRA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_DEMURRAGE_COMPRA,'C','PT-BR')),'R$',''),'') AS VL_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DEMURRAGE_VENDA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(B.VL_DEMURRAGE_VENDA, 'C', 'PT-BR')), 'R$', ''),'') AS VL_DEMURRAGE_VENDA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_BL BL ON A.ID_BL = BL.ID_BL ";
            SQL += "LEFT JOIN TB_BL M ON BL.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_MOEDA MD ON B.ID_MOEDA_DEMURRAGE_COMPRA = MD.ID_MOEDA ";
            SQL += "LEFT JOIN TB_DEMURRAGE_FATURA DF ON A.ID_BL = DF.ID_BL ";
            SQL += "WHERE DF.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
            SQL += "AND A.FL_DEMURRAGE_FINALIZADA = 0 ";
            DataTable imprimirDados = new DataTable();
            imprimirDados = DBS.List(SQL);
            return JsonConvert.SerializeObject(imprimirDados);
        }

        [WebMethod]
        public string listarEstimativa()
        {
            int somaDias;
            int somaDiasv;
            decimal vlDemurr = 0;
            decimal vlDemurrv = 0;
            string SQL;
            decimal vlTaxa = 0;
            decimal vlTaxaV = 0;
            int demurrage = 0;
            int demurragev = 0;
            int def = 0;
            SQL = "select PFCL.ID_CNTR_BL, PFCL.NR_PROCESSO, PFCL.NR_CNTR,PFCL.NM_TIPO_CONTAINER, ";
            SQL += "ISNULL(LEFT(P.NM_RAZAO,10),'') AS CLIENTE , ISNULL(LEFT(P2.NM_RAZAO,10),'') AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy') AS DT_CHEGADA, ";
            SQL += "ISNULL(PFCL.QT_DIAS_FREETIME,'') AS QT_DIAS_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy') AS DT_FINAL_FREETIME, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'),'') AS DT_DEVOLUCAO, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, ";
            SQL += "VALOR_COMPRA_ESTIMADO = CASE WHEN DFCL.VL_DEMURRAGE_COMPRA = 0 OR DFCL.VL_DEMURRAGE_COMPRA IS NULL THEN 1 ELSE 0 END, ";
            SQL += "MOEDA_COMPRA = CASE WHEN DFCL.VL_DEMURRAGE_COMPRA > 0 THEN ISNULL(M.NM_MOEDA,'') ELSE ISNULL('','') END, ";
            SQL += "VALOR_COMPRA = CASE WHEN DFCL.VL_DEMURRAGE_COMPRA > 0 THEN DFCL.VL_DEMURRAGE_COMPRA ELSE 0 END, ";
            SQL += "VALOR_COMPRA_REAL = CASE WHEN DFCL.VL_DEMURRAGE_COMPRA > 0 THEN DFCL.VL_DEMURRAGE_LIQUIDO_COMPRA ELSE 0 END, ";
            SQL += "DATA_PAGAMENTO = CASE WHEN DFCL.VL_DEMURRAGE_COMPRA > 0 THEN ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE,'dd/MM/yyyy'),'') ELSE ISNULL('','') END, ";
            SQL += "VALOR_VENDA_ESTIMADO = CASE WHEN DFCL.VL_DEMURRAGE_VENDA = 0 OR DFCL.VL_DEMURRAGE_VENDA IS NULL THEN 1 ELSE 0 END, ";
            SQL += "MOEDA_VENDA = CASE WHEN DFCL.VL_DEMURRAGE_VENDA > 0 THEN ISNULL(M2.NM_MOEDA,'') ELSE ISNULL('','') END, ";
            SQL += "VALOR_VENDA = CASE WHEN DFCL.VL_DEMURRAGE_VENDA > 0 THEN DFCL.VL_DEMURRAGE_VENDA ELSE 0 END, ";
            SQL += "VALOR_VENDA_REAL = CASE WHEN DFCL.VL_DEMURRAGE_VENDA > 0 THEN COALESCE(DFCL.VL_DEMURRAGE_LIQUIDO_VENDA,0) ELSE 0 END, ";
            SQL += "DATA_RECEBIMENTO = CASE WHEN DFCL.VL_DEMURRAGE_VENDA > 0 THEN ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE,'dd/MM/yyyy'),'') ELSE ISNULL('','') END, ";
            SQL += "PFCL.DS_STATUS_DEMURRAGE, FORMAT(PFCL.DT_STATUS_DEMURRAGE, 'dd/MM/yyyy') AS DT_STATUS_DEMURRAGE, ISNULL(PFCL.DS_OBSERVACAO,'') AS DS_OBSERVACAO ";
            SQL += "from VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DEMURRAGE_COMPRA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_MOEDA M2 ON DFCL.ID_MOEDA_DEMURRAGE_VENDA = M2.ID_MOEDA ";

            DataTable nEscalonada = new DataTable();
            nEscalonada = DBS.List(SQL);
            if (nEscalonada != null)
            {
                var valoresD = new string[nEscalonada.Rows.Count];
                var moedasD = new string[nEscalonada.Rows.Count];
                var valoresDv = new string[nEscalonada.Rows.Count];
                var moedasDv = new string[nEscalonada.Rows.Count];

                for (int i = 0; i < nEscalonada.Rows.Count; i++)
                {
                    SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                    SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                    SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE, DFCL.ID_MOEDA_DEMURRAGE_COMPRA, TBD.FL_ESCALONADA, M.NM_MOEDA, ";
                    SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                    SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                    SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                    SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                    SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                    SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                    SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                    SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                    SQL += "WHERE TBD.ID_PARCEIRO_TRANSPORTADOR = PFCL.ID_PARCEIRO_TRANSPORTADOR ";
                    SQL += "AND PFCL.DT_DEVOLUCAO_CNTR >= TBD.DT_VALIDADE_INICIAL ";
                    SQL += "AND PFCL.ID_CNTR_BL = " + (int)nEscalonada.Rows[i]["ID_CNTR_BL"] + " ";
                    SQL += "ORDER BY TBD.DT_VALIDADE_INICIAL DESC ";

                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);

                    if (listTable != null)
                    {
                        if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
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

                            somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];

                            if (somaDias <= ft)
                            {
                                vlTaxa = 0;
                            }
                            else if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                            {
                                if (somaDias <= ft + d1)
                                {
                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                }
                                else if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                                {
                                    if (somaDias <= ft + d1 + d2)
                                    {
                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                    }
                                    else if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                    {
                                        if (somaDias <= ft + d1 + d2 + d3)
                                        {
                                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                        }
                                        else if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                        {
                                            if (somaDias <= ft + d1 + d2 + d3 + d4)
                                            {
                                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                            }
                                            else if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                            {
                                                if (somaDias <= ft + d1 + d2 + d3 + d4 + d5)
                                                {
                                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                                }
                                                else if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                                {
                                                    if (somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6)
                                                    {
                                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                    }
                                                    else if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                    {
                                                        if (somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6 + d7)
                                                        {
                                                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                        }
                                                        else if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                        {
                                                            if (somaDias <= ft + d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8)
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

                            vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] * vlTaxa;
                            valoresD[i] = vlDemurr.ToString();
                            moedasD[i] = listTable.Rows[0]["NM_MOEDA"].ToString();
                            if (nEscalonada.Rows[i]["VALOR_COMPRA"].ToString() == "0,00")
                            {
                                nEscalonada.Rows[i]["VALOR_COMPRA"] = valoresD[i];
                                nEscalonada.Rows[i]["MOEDA_COMPRA"] = moedasD[i];
                            }
                        }
                        else
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

                            somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"];
                            demurrage = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                            vlDemurr = 0;

                            if (somaDias <= ft)
                            {
                                vlDemurr = 0;
                            }
                            else
                            {
                                if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                                {
                                    if (demurrage - d1 <= 0)
                                    {
                                        vlDemurr = demurrage * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                    }
                                    else
                                    {
                                        demurrage = demurrage - d1;
                                        vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                        if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                                        {
                                            if (demurrage - d2 <= 0)
                                            {
                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_02"]);
                                            }
                                            else
                                            {
                                                demurrage = demurrage - d2;
                                                vlDemurr = d2 * (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                                if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                                {
                                                    if (demurrage - d3 <= 0)
                                                    {
                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_03"]);
                                                    }
                                                    else
                                                    {
                                                        demurrage = demurrage - d3;
                                                        vlDemurr = d3 * (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                                        if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                                        {
                                                            if (demurrage - d4 <= 0)
                                                            {
                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_04"]);
                                                            }
                                                            else
                                                            {
                                                                demurrage = demurrage - d4;
                                                                vlDemurr = d4 * (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                                                if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                                                {
                                                                    if (demurrage - d5 <= 0)
                                                                    {
                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_05"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        demurrage = demurrage - d5;
                                                                        vlDemurr = d5 * (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                                                        if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                                                        {
                                                                            if (demurrage - d6 <= 0)
                                                                            {
                                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_06"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                demurrage = demurrage - d6;
                                                                                vlDemurr = d6 * (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                                                if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                                                {
                                                                                    if (demurrage - d7 <= 0)
                                                                                    {
                                                                                        vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_07"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        demurrage = demurrage - d7;
                                                                                        vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                                                        if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                                                        {
                                                                                            if (demurrage - d8 <= 0)
                                                                                            {
                                                                                                vlDemurr = vlDemurr + (demurrage * (decimal)listTable.Rows[0]["VL_VENDA_08"]);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                demurrage = demurrage - d8;
                                                                                                vlDemurr = d8 * (decimal)listTable.Rows[0]["VL_VENDA_08"];
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
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        valoresD[i] = vlDemurr.ToString();
                        moedasD[i] = listTable.Rows[0]["NM_MOEDA"].ToString();
                        if (nEscalonada.Rows[i]["VALOR_COMPRA"].ToString() == "0,00")
                        {
                            nEscalonada.Rows[i]["VALOR_COMPRA"] = valoresD[i];
                            nEscalonada.Rows[i]["MOEDA_COMPRA"] = moedasD[i];
                        }
                    }
                    else
                    {
                        valoresD[i] = def.ToString();
                        moedasD[i] = def.ToString();
                        if (nEscalonada.Rows[i]["VALOR_COMPRA"].ToString() == "0,00")
                        {
                            nEscalonada.Rows[i]["VALOR_COMPRA"] = valoresD[i];
                            nEscalonada.Rows[i]["MOEDA_COMPRA"] = moedasD[i];
                        }
                    }

                    SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                    SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                    SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE, DFCL.ID_MOEDA_DEMURRAGE_COMPRA, TBD.FL_ESCALONADA, M.NM_MOEDA, ";
                    SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                    SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                    SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                    SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                    SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                    SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                    SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                    SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                    SQL += "WHERE TBD.ID_PARCEIRO_TRANSPORTADOR = 0 ";
                    SQL += "AND PFCL.DT_DEVOLUCAO_CNTR >= TBD.DT_VALIDADE_INICIAL ";
                    SQL += "AND PFCL.ID_CNTR_BL = " + (int)nEscalonada.Rows[i]["ID_CNTR_BL"] + " ";
                    SQL += "ORDER BY TBD.DT_VALIDADE_INICIAL DESC ";

                    DataTable listTable2 = new DataTable();
                    listTable2 = DBS.List(SQL);

                    if (listTable2 != null)
                    {
                        if (!(Boolean)listTable2.Rows[0]["FL_ESCALONADA"])
                        {
                            int d1v = (Int16)listTable2.Rows[0]["QT_DIAS_01"];
                            int d2v = (Int16)listTable2.Rows[0]["QT_DIAS_02"];
                            int d3v = (Int16)listTable2.Rows[0]["QT_DIAS_03"];
                            int d4v = (Int16)listTable2.Rows[0]["QT_DIAS_04"];
                            int d5v = (Int16)listTable2.Rows[0]["QT_DIAS_05"];
                            int d6v = (Int16)listTable2.Rows[0]["QT_DIAS_06"];
                            int d7v = (Int16)listTable2.Rows[0]["QT_DIAS_07"];
                            int d8v = (Int16)listTable2.Rows[0]["QT_DIAS_08"];
                            int ftv = (Int16)listTable2.Rows[0]["FreeTimeTab"];

                            somaDiasv = (Int16)listTable2.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable2.Rows[0]["QT_DIAS_DEMURRAGE"];

                            if (somaDiasv <= ftv)
                            {
                                vlTaxaV = 0;
                            }
                            else if (d1v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_01"] != null)
                            {
                                if (somaDiasv <= ftv + d1v)
                                {
                                    vlTaxaV = (decimal)listTable2.Rows[0]["VL_VENDA_01"];
                                }
                                else if (d2v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_02"] != null)
                                {
                                    if (somaDiasv <= ftv + d1v + d2v)
                                    {
                                        vlTaxaV = (decimal)listTable2.Rows[0]["VL_VENDA_02"];
                                    }
                                    else if (d3v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_03"] != null)
                                    {
                                        if (somaDiasv <= ftv + d1v + d2v + d3v)
                                        {
                                            vlTaxaV = (decimal)listTable2.Rows[0]["VL_VENDA_03"];
                                        }
                                        else if (d4v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_04"] != null)
                                        {
                                            if (somaDiasv <= ftv + d1v + d2v + d3v + d4v)
                                            {
                                                vlTaxaV = (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                            }
                                            else if (d5v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_05"] != null)
                                            {
                                                if (somaDiasv <= ftv + d1v + d2v + d3v + d4v + d5v)
                                                {
                                                    vlTaxaV = (decimal)listTable2.Rows[0]["VL_VENDA_05"];
                                                }
                                                else if (d6v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_06"] != null)
                                                {
                                                    if (somaDiasv <= ftv + d1v + d2v + d3v + d4v + d5v + d6v)
                                                    {
                                                        vlTaxaV = (decimal)listTable2.Rows[0]["VL_VENDA_06"];
                                                    }
                                                    else if (d7v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_07"] != null)
                                                    {
                                                        if (somaDiasv <= ftv + d1v + d2v + d3v + d4v + d5v + d6v + d7v)
                                                        {
                                                            vlTaxaV = (decimal)listTable2.Rows[0]["VL_VENDA_07"];
                                                        }
                                                        else if (d8v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_08"] != null)
                                                        {
                                                            if (somaDiasv <= ftv + d1v + d2v + d3v + d4v + d5v + d6v + d7v + d8v)
                                                            {
                                                                vlTaxaV = (decimal)listTable2.Rows[0]["VL_VENDA_08"];
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            vlDemurrv = (int)listTable2.Rows[0]["QT_DIAS_DEMURRAGE"] * vlTaxaV;
                            valoresDv[i] = vlDemurrv.ToString();
                            moedasDv[i] = listTable2.Rows[0]["NM_MOEDA"].ToString();
                            if (nEscalonada.Rows[i]["VALOR_VENDA"].ToString() == "0,00")
                            {
                                nEscalonada.Rows[i]["VALOR_VENDA"] = valoresDv[i];
                                nEscalonada.Rows[i]["MOEDA_VENDA"] = moedasDv[i];
                            }
                        }
                        else
                        {
                            int d1v = (Int16)listTable2.Rows[0]["QT_DIAS_01"];
                            int d2v = (Int16)listTable2.Rows[0]["QT_DIAS_02"];
                            int d3v = (Int16)listTable2.Rows[0]["QT_DIAS_03"];
                            int d4v = (Int16)listTable2.Rows[0]["QT_DIAS_04"];
                            int d5v = (Int16)listTable2.Rows[0]["QT_DIAS_05"];
                            int d6v = (Int16)listTable2.Rows[0]["QT_DIAS_06"];
                            int d7v = (Int16)listTable2.Rows[0]["QT_DIAS_07"];
                            int d8v = (Int16)listTable2.Rows[0]["QT_DIAS_08"];
                            int ftv = (Int16)listTable2.Rows[0]["FreeTimeTab"];

                            somaDiasv = (Int16)listTable2.Rows[0]["QT_DIAS_FREETIME"];
                            demurragev = (int)listTable2.Rows[0]["QT_DIAS_DEMURRAGE"];
                            vlDemurrv = 0;

                            if (somaDiasv <= ftv)
                            {
                                vlDemurrv = 0;
                            }
                            else
                            {
                                if (d1v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_01"] != null)
                                {
                                    if (demurragev - d1v <= 0)
                                    {
                                        vlDemurrv = demurragev * (decimal)listTable2.Rows[0]["VL_VENDA_01"];
                                    }
                                    else
                                    {
                                        demurragev = demurragev - d1v;
                                        vlDemurrv = d1v * (decimal)listTable2.Rows[0]["VL_VENDA_01"];
                                        if (d2v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_02"] != null)
                                        {
                                            if (demurragev - d2v <= 0)
                                            {
                                                vlDemurrv = vlDemurrv + (demurragev * (decimal)listTable2.Rows[0]["VL_VENDA_02"]);
                                            }
                                            else
                                            {
                                                demurragev = demurragev - d2v;
                                                vlDemurrv = d2v * (decimal)listTable2.Rows[0]["VL_VENDA_02"];
                                                if (d3v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_03"] != null)
                                                {
                                                    if (demurragev - d3v <= 0)
                                                    {
                                                        vlDemurrv = vlDemurrv + (demurragev * (decimal)listTable2.Rows[0]["VL_VENDA_03"]);
                                                    }
                                                    else
                                                    {
                                                        demurragev = demurragev - d3v;
                                                        vlDemurrv = d3v * (decimal)listTable2.Rows[0]["VL_VENDA_03"];
                                                        if (d4v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_04"] != null)
                                                        {
                                                            if (demurragev - d4v <= 0)
                                                            {
                                                                vlDemurrv = vlDemurrv + (demurragev * (decimal)listTable2.Rows[0]["VL_VENDA_04"]);
                                                            }
                                                            else
                                                            {
                                                                demurragev = demurragev - d4v;
                                                                vlDemurrv = d4v * (decimal)listTable2.Rows[0]["VL_VENDA_04"];
                                                                if (d5v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_05"] != null)
                                                                {
                                                                    if (demurragev - d5v <= 0)
                                                                    {
                                                                        vlDemurrv = vlDemurrv + (demurragev * (decimal)listTable2.Rows[0]["VL_VENDA_05"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        demurragev = demurragev - d5v;
                                                                        vlDemurrv = d5v * (decimal)listTable2.Rows[0]["VL_VENDA_05"];
                                                                        if (d6v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_06"] != null)
                                                                        {
                                                                            if (demurragev - d6v <= 0)
                                                                            {
                                                                                vlDemurrv = vlDemurrv + (demurragev * (decimal)listTable2.Rows[0]["VL_VENDA_06"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                demurragev = demurragev - d6v;
                                                                                vlDemurrv = d6v * (decimal)listTable2.Rows[0]["VL_VENDA_06"];
                                                                                if (d7v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_07"] != null)
                                                                                {
                                                                                    if (demurragev - d7v <= 0)
                                                                                    {
                                                                                        vlDemurrv = vlDemurrv + (demurragev * (decimal)listTable2.Rows[0]["VL_VENDA_07"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        demurragev = demurragev - d7v;
                                                                                        vlDemurrv = d1v * (decimal)listTable2.Rows[0]["VL_VENDA_07"];
                                                                                        if (d8v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_08"] != null)
                                                                                        {
                                                                                            if (demurragev - d8v <= 0)
                                                                                            {
                                                                                                vlDemurrv = vlDemurrv + (demurragev * (decimal)listTable2.Rows[0]["VL_VENDA_08"]);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                demurragev = demurragev - d8v;
                                                                                                vlDemurrv = d8v * (decimal)listTable2.Rows[0]["VL_VENDA_08"];
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
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        valoresDv[i] = vlDemurrv.ToString();
                        moedasDv[i] = listTable2.Rows[0]["NM_MOEDA"].ToString();
                        if (nEscalonada.Rows[i]["VALOR_VENDA"].ToString() == "0,00")
                        {
                            nEscalonada.Rows[i]["VALOR_VENDA"] = valoresDv[i];
                            nEscalonada.Rows[i]["MOEDA_VENDA"] = moedasDv[i];
                        }
                    }
                    else
                    {
                        valoresDv[i] = def.ToString();
                        moedasDv[i] = def.ToString();
                        if (nEscalonada.Rows[i]["VALOR_VENDA"].ToString() == "0,00")
                        {
                            nEscalonada.Rows[i]["VALOR_VENDA"] = valoresDv[i];
                            nEscalonada.Rows[i]["MOEDA_VENDA"] = moedasDv[i];
                        }
                    }
                }
                return JsonConvert.SerializeObject(nEscalonada);
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public string excluirFatura(string idFatura)
        {
            string SQL;
            SQL = "DELETE FROM TB_DEMURRAGE_FATURA_ITENS WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
            string deleteitens = DBS.ExecuteScalar(SQL);

            SQL = "DELETE FROM TB_DEMURRAGE_FATURA WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
            string deletefatura = DBS.ExecuteScalar(SQL);
            return "1";
        }

        [WebMethod]
        public string atualizarDevolucao(string idCont, string dtStatus, string dsStatus, string dtDevolucao)
        {
            string SQL;
            string flagF;
            SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + dsStatus + "' ";
            DataTable flFinaliza = new DataTable();
            flFinaliza = DBS.List(SQL);
            flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();
            switch (dtDevolucao)
            {
                case "":
                    dtDevolucao = "null";
                    break;
            }
            if (dtDevolucao == "null")
            {
                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = " + dtDevolucao + ", ID_STATUS_DEMURRAGE = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' ";
                SQL += "WHERE ID_CNTR_BL = '" + idCont + "' ";
            }

            else
            {
                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = '" + dtDevolucao + "', ID_STATUS_DEMURRAGE = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "', FL_DEMURRAGE_FINALIZADA = '" + flagF + "' ";
                SQL += "WHERE ID_CNTR_BL = '" + idCont + "' ";
            }
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
        public string listarContainerCalculoPrint(string idprocess)
        {
            string SQL;
            SQL = "SELECT TOP 1 A.NR_PROCESSO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
            SQL += "WHERE A.ID_CNTR_BL = '" + idprocess + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string processoNr = listTable.Rows[0]["NR_PROCESSO"].ToString();

            SQL = "SELECT A.NR_CNTR, A.NM_TIPO_CONTAINER, FORMAT(B.DT_INICIAL_FREETIME,'dd/MM/yy') AS INICIALFT, ";
            SQL += "FORMAT(B.DT_FINAL_FREETIME,'dd/MM/yy') AS FINALFT,A.QT_DIAS_FREETIME, ";
            SQL += "FORMAT(B.DT_INICIAL_DEMURRAGE,'dd/MM/yy') AS INICIALDEM, FORMAT(B.DT_FINAL_DEMURRAGE,'dd/MM/yy') AS FINALDEM, ";
            SQL += "B.QT_DIAS_DEMURRAGE, MD.SIGLA_MOEDA, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DEMURRAGE_COMPRA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_DEMURRAGE_COMPRA,'C','PT-BR')),'R$',''),'') AS VL_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DEMURRAGE_VENDA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(B.VL_DEMURRAGE_VENDA, 'C', 'PT-BR')), 'R$', ''),'') AS VL_DEMURRAGE_VENDA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL A ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_BL BL ON A.ID_BL = BL.ID_BL ";
            SQL += "LEFT JOIN TB_BL M ON BL.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_MOEDA MD ON B.ID_MOEDA_DEMURRAGE_COMPRA = MD.ID_MOEDA ";
            SQL += "WHERE A.NR_PROCESSO = '" + processoNr + "' ";
            DataTable imprimirDados = new DataTable();
            imprimirDados = DBS.List(SQL);
            return JsonConvert.SerializeObject(imprimirDados);
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
        public string listarCourrier(string idFilter, string Filter, string tipo)
        {
            string SQL;
            switch (idFilter)
            {
                case "1":
                    idFilter = "AND BL.NR_PROCESSO LIKE '" + Filter + "%' ";
                    break;
                case "2":
                    idFilter = "AND M.NR_BL LIKE '" + Filter + "%' ";
                    break;
                case "3":
                    idFilter = "AND CLIENTE LIKE '" + Filter + "%' ";
                    break;
                case "4":
                    idFilter = "AND N.NM_NAVIO LIKE '" + Filter + "%' ";
                    break;
                default:
                    idFilter = "";
                    break;
            }

            switch (tipo)
            {
                case "1":
                    tipo = "AND TP.ID_TIPO_ESTUFAGEM = 1 ";
                    break;
                case "0":
                    tipo = "AND TP.ID_TIPO_ESTUFAGEM = 2 ";
                    break;
            }

            SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
            SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
            SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
            SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
            SQL += "FROM TB_BL BL ";
            SQL += "JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
            SQL += "JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
            SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
            SQL += "WHERE SUBSTRING(BL.NR_PROCESSO,10,2)>= '18' ";
            SQL += "" + idFilter + "";
            SQL += "" + tipo + "";

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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
                    SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, BL.ID_BL_MASTER, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
                    SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ";
                    SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
                    SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
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
