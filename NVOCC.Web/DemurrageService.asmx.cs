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
using System.Net.Mail;
using ABAINFRA.Web.Classes;
using System.Net;
using Microsoft.Exchange.WebServices.Data;
using System.Globalization;
using System.Text.RegularExpressions;

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
        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        public string carregarArmador()
        {
            string SQL;
            SQL = "SELECT ID_PARCEIRO, NM_RAZAO FROM tb_parceiro where FL_TRANSPORTADOR = 1 ORDER BY NM_RAZAO";
            DataTable parceiroTransportador = new DataTable();
            parceiroTransportador = DBS.List(SQL);
            return JsonConvert.SerializeObject(parceiroTransportador);
        }


        [WebMethod(EnableSession = true)]
        public string ListarDemurrageContainer(string armador)
        {
            string SQL;


            SQL = "SELECT A.ID_TABELA_DEMURRAGE, CASE WHEN A.FL_CARGA_IMO = 1 THEN C.CD_TAMANHO_CONTAINER ELSE B.NM_TIPO_CONTAINER END AS TIPO_CONTAINER, FORMAT(DT_VALIDADE_INICIAL,'dd/MM/yyyy') AS DT_VALIDADE_INICIAL_FORMAT, ";
            SQL += "CASE WHEN A.FL_CARGA_IMO = 1 THEN 'SIM' ELSE 'NAO' END AS CARGA_IMO ";
            SQL += "FROM TB_TABELA_DEMURRAGE A ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER B ON A.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_TAMANHO_CONTAINER C ON A.ID_TAMANHO_CONTAINER = C.ID_TAMANHO_CONTAINER ";
            if (armador != "")
            {
                SQL += "WHERE ID_PARCEIRO_TRANSPORTADOR = '" + armador + "' ";
            }
            DataTable listarDemurrageContainer = new DataTable();
            listarDemurrageContainer = DBS.List(SQL);
            return JsonConvert.SerializeObject(listarDemurrageContainer);
        }

        [WebMethod(EnableSession = true)]
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

            if (dados.ID_TIPO_CONTAINER.ToString() == "" && dados.FL_CARGA_IMO == "0")
            {
                return "0";
            }

            if (dados.ID_TAMANHO_CONTAINER.ToString() == "" && dados.FL_CARGA_IMO == "1")
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
            SQL += "ID_TIPO_CONTAINER ='" + dados.ID_TIPO_CONTAINER + "' AND DT_VALIDADE_INICIAL = '" + dados.DT_VALIDADE_INICIAL + "' AND FL_CARGA_IMO = '"+dados.FL_CARGA_IMO+"' ";
            DataTable consulta = new DataTable();
            consulta = DBS.List(SQL);
            if (consulta == null)
            {
                SQL = "insert into TB_TABELA_DEMURRAGE (ID_PARCEIRO_TRANSPORTADOR,ID_TIPO_CONTAINER,DT_VALIDADE_INICIAL,QT_DIAS_FREETIME, ";
                SQL += "ID_MOEDA, FL_ESCALONADA, FL_INICIO_CHEGADA, FL_CARGA_IMO, ID_TAMANHO_CONTAINER, QT_DIAS_01 ,VL_VENDA_01 ,QT_DIAS_02 ,VL_VENDA_02 ,QT_DIAS_03 ,VL_VENDA_03 ,QT_DIAS_04, ";
                SQL += "VL_VENDA_04 ,QT_DIAS_05 ,VL_VENDA_05 ,QT_DIAS_06 ,VL_VENDA_06 ,QT_DIAS_07 ,VL_VENDA_07 ,QT_DIAS_08 ,VL_VENDA_08) ";
                SQL += "VALUES( '" + dados.ID_PARCEIRO_TRANSPORTADOR + "'," + (dados.ID_TIPO_CONTAINER == "" ? "NULL": dados.ID_TIPO_CONTAINER) + ", ";
                SQL += "'" + dados.DT_VALIDADE_INICIAL + "','" + dados.QT_DIAS_FREETIME + "','" + dados.ID_MOEDA + "','" + dados.FL_ESCALONADA + "', '" + dados.FL_INICIO_CHEGADA + "', '"+dados.FL_CARGA_IMO+"', "+(dados.ID_TAMANHO_CONTAINER == "" ? "NULL":dados.ID_TAMANHO_CONTAINER)+", ";
                SQL += "'" + qtdias01 + "','" + vlVenda01.ToString().Replace(',', '.') + "', '" + qtdias02 + "','" + vlVenda02.ToString().Replace(',', '.') + "', ";
                SQL += "'" + qtdias03 + "','" + vlVenda03.ToString().Replace(',', '.') + "', '" + qtdias04 + "','" + vlVenda04.ToString().Replace(',', '.') + "', ";
                SQL += "'" + qtdias05 + "','" + vlVenda05.ToString().Replace(',', '.') + "', '" + qtdias06 + "','" + vlVenda06.ToString().Replace(',', '.') + "', ";
                SQL += "'" + qtdias07 + "','" + vlVenda07.ToString().Replace(',', '.') + "', '" + qtdias08 + "','" + vlVenda08.ToString().Replace(',', '.') + "') ";

                string demu = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                return "2";
            }
        }

        [WebMethod(EnableSession = true)]
        public string BuscarDemurrage(int Id)
        {

            string SQL;
            SQL = "SELECT ID_TABELA_DEMURRAGE, ID_PARCEIRO_TRANSPORTADOR, ID_TIPO_CONTAINER, ID_TAMANHO_CONTAINER, FORMAT(DT_VALIDADE_INICIAL,'yyyy-MM-dd') AS DT_VALIDADE_INICIAL_FORMAT, ";
            SQL += "QT_DIAS_FREETIME, ID_MOEDA, FL_ESCALONADA, FL_INICIO_CHEGADA, FL_CARGA_IMO, QT_DIAS_01, VL_VENDA_01, QT_DIAS_02, VL_VENDA_02, QT_DIAS_03, VL_VENDA_03, ";
            SQL += "QT_DIAS_04, VL_VENDA_04, QT_DIAS_05, VL_VENDA_05, QT_DIAS_06, VL_VENDA_06, QT_DIAS_07, VL_VENDA_07, QT_DIAS_08, VL_VENDA_08 ";
            SQL += "FROM TB_TABELA_DEMURRAGE ";
            SQL += "WHERE ID_TABELA_DEMURRAGE = '" + Id + "'";

            DataTable carregarDados = new DataTable();
            carregarDados = DBS.List(SQL);
            DemurragesCls resultado = new DemurragesCls();
            resultado.ID_TABELA_DEMURRAGE = (int)carregarDados.Rows[0]["ID_TABELA_DEMURRAGE"];
            resultado.ID_PARCEIRO_TRANSPORTADOR = (int)carregarDados.Rows[0]["ID_PARCEIRO_TRANSPORTADOR"];
            resultado.ID_TIPO_CONTAINER = carregarDados.Rows[0]["ID_TIPO_CONTAINER"].ToString();
            resultado.DT_VALIDADE_INICIAL = carregarDados.Rows[0]["DT_VALIDADE_INICIAL_FORMAT"].ToString();
            resultado.QT_DIAS_FREETIME = carregarDados.Rows[0]["QT_DIAS_FREETIME"].ToString();
            resultado.ID_MOEDA = (int)carregarDados.Rows[0]["ID_MOEDA"];
            resultado.FL_ESCALONADA = carregarDados.Rows[0]["FL_ESCALONADA"].ToString();
            resultado.FL_INICIO_CHEGADA = carregarDados.Rows[0]["FL_INICIO_CHEGADA"].ToString();
            resultado.FL_CARGA_IMO = carregarDados.Rows[0]["FL_CARGA_IMO"].ToString();
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
            resultado.ID_TAMANHO_CONTAINER = carregarDados.Rows[0]["ID_TAMANHO_CONTAINER"].ToString();

            return JsonConvert.SerializeObject(resultado);

        }

        [WebMethod(EnableSession = true)]
        public string DemurrageList()
        {
            string SQL;
            SQL = "SELECT ID_TABELA_DEMURRAGE, NM_TIPO_CONTAINER FROM TB_TABELA_DEMURRAGE JOIN TB_TIPO_CONTAINER ";
            SQL += "ON TB_TABELA_DEMURRAGE.ID_TIPO_CONTAINER = TB_TIPO_CONTAINER.ID_TIPO_CONTAINER ";

            DataTable demurrageList = new DataTable();
            demurrageList = DBS.List(SQL);
            return JsonConvert.SerializeObject(demurrageList);
        }

        [WebMethod(EnableSession = true)]
        public string EditarDemurrageContainer(DemurragesCls dadosEdit)
        {
            int qtdfreetime = Convert.ToInt32(dadosEdit.QT_DIAS_FREETIME);
            int qtdias01 = Convert.ToInt32(dadosEdit.QT_DIAS_01);
            double vlVenda01 = Convert.ToDouble(dadosEdit.VL_VENDA_01);
            if (dadosEdit.ID_PARCEIRO_TRANSPORTADOR.ToString() == "")
            {
                return "0";
            }

            if (dadosEdit.ID_TIPO_CONTAINER.ToString() == "" && dadosEdit.FL_CARGA_IMO == "0")
            {
                return "0";
            }

            if (dadosEdit.ID_TAMANHO_CONTAINER.ToString() == "" && dadosEdit.FL_CARGA_IMO == "1")
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
            SQL = "UPDATE TB_TABELA_DEMURRAGE SET ID_PARCEIRO_TRANSPORTADOR = '" + dadosEdit.ID_PARCEIRO_TRANSPORTADOR + "' , ID_TIPO_CONTAINER = " + (dadosEdit.ID_TIPO_CONTAINER == "" ? "NULL":dadosEdit.ID_TIPO_CONTAINER)+ ", ";
            SQL += "DT_VALIDADE_INICIAL = '" + dadosEdit.DT_VALIDADE_INICIAL + "', QT_DIAS_FREETIME = '" + dadosEdit.QT_DIAS_FREETIME + "', ";
            SQL += "ID_MOEDA = '" + dadosEdit.ID_MOEDA + "', FL_ESCALONADA ='" + dadosEdit.FL_ESCALONADA + "', FL_CARGA_IMO = '" + dadosEdit.FL_CARGA_IMO + "', ID_TAMANHO_CONTAINER = "+ (dadosEdit.ID_TAMANHO_CONTAINER == "" ? "NULL":dadosEdit.ID_TAMANHO_CONTAINER)+ ", ";
            SQL += "QT_DIAS_01 ='" + qtdias01 + "' , VL_VENDA_01 = '" + vlvenda01 + "', ";
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

        [WebMethod(EnableSession = true)]
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



        [WebMethod(EnableSession = true)]
        public string listarTabela()
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ";
            SQL += "ISNULL(P2.NM_RAZAO,'') AS TRANSPORTADOR, ISNULL(FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy'), '') AS DT_CHEGADA, ";
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

        [WebMethod(EnableSession = true)]
        public string filtrarTabela(string idFilter, string Filter, string Ativo, string Finalizado, string TarifaSpot)
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
                case "6":
                    idFilter = "AND B.NR_BL LIKE '" + Filter + "%' ";
                    break;
                default:
                    idFilter = "";
                    break;
            }

            if(TarifaSpot == "1")
			{
                TarifaSpot = " AND ISNULL(E.FL_TARIFA_SPOT, 0) <> 0 ";
			}
			else
			{
                TarifaSpot = " AND ISNULL(E.FL_TARIFA_SPOT, 0) = 0 ";
            }

            if (Ativo == "1" && Finalizado == "1")
            {
                Ativo = "";
                Finalizado = "";
            }
            else
            {
                switch (Finalizado)
                {
                    case "1":
                        Finalizado = " AND D.FL_FINALIZA_DEMURRAGE=1 AND D1.FL_FINALIZA_DEMURRAGE= 1 ";
                        break;
                    default:
                        Finalizado = "";
                        break;
                }

                switch (Ativo)
                {
                    case "1":
                        Ativo = " AND (D.FL_FINALIZA_DEMURRAGE != 1 OR D1.FL_FINALIZA_DEMURRAGE != 1) ";


                        break;
                    default:
                        Ativo = "";
                        break;
                }
            }

            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, B.NR_BL AS MBL, PFCL.NM_TIPO_CONTAINER, ISNULL(TC.NM_TIPO_CARGA,'') AS NM_TIPO_CARGA, PFCL.NR_PROCESSO, ISNULL(P.NM_RAZAO,C.NM_RAZAO) AS CLIENTE, ";
            SQL += "ISNULL(P2.NM_RAZAO, '') AS TRANSPORTADOR, ISNULL(FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy'), '') AS DT_CHEGADA, ";
            SQL += "CASE WHEN ISNULL(E.FL_TARIFA_SPOT,0) <> 0 THEN 'FRETE SPOT' ELSE '' END AS ACORDO_COMERCIAL, ";
            SQL += "ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME), '') AS QT_DIAS_FREETIME, ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME_CONFIRMA),'') AS QT_DIAS_FREETIME_CONFIRMA, ISNULL(FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy'), '') AS FINAL_FREETIME, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'), '') AS DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE,ISNULL(DFCL.QT_DIAS_DEMURRAGE_COMPRA,'')QT_DIAS_DEMURRAGE_COMPRA, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE, 'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, PFCL.DS_STATUS_DEMURRAGE, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE, 'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE_COMPRA, PFCL.DS_STATUS_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(PFCL.DS_OBSERVACAO, '') AS DS_OBSERVACAO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_DEMURRAGE_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS VL_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(FORMAT(DFCL.VL_DEMURRAGE_COMPRA_BR,'C','pt-br'),'') AS VL_DEMURRAGE_COMPRA_BR, ";
            SQL += "ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS PAG_DEMU, ";
            SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DEMURRAGE_VENDA,'C','pt-br')),'R$',''),'') AS VL_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(FORMAT(DFCL.VL_DEMURRAGE_VENDA_BR,'C','pt-br'),'') AS VL_DEMURRAGE_VENDA_BR, ";
            SQL += "ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS RECEB_DEMU ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
            SQL += "INNER JOIN TB_BL B ON PFCL.ID_BL_MASTER = B.ID_BL ";
            SQL += "INNER JOIN TB_COTACAO E ON PFCL.ID_COTACAO = E.ID_COTACAO ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_IMPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON PFCL.ID_PARCEIRO_CLIENTE = C.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_STATUS_DEMURRAGE D ON PFCL.ID_STATUS_DEMURRAGE_COMPRA= D.ID_STATUS_DEMURRAGE ";
            SQL += "LEFT JOIN TB_STATUS_DEMURRAGE D1 ON PFCL.ID_STATUS_DEMURRAGE = D1.ID_STATUS_DEMURRAGE ";
            SQL += "LEFT JOIN TB_TIPO_CARGA TC ON PFCL.ID_TIPO_CARGA = TC.ID_TIPO_CARGA ";
            SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
            SQL += "" + idFilter + " ";
            SQL += "" + Ativo + " ";
            SQL += "" + Finalizado + " ";
            SQL += "" + TarifaSpot + " ";
            SQL += "ORDER BY PFCL.DT_CHEGADA";
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string filtrarTabelaDetention(string idFilter, string Filter, string Ativo, string Finalizado)
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
                case "6":
                    idFilter = "AND B.NR_BL LIKE '" + Filter + "%' ";
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
                switch (Finalizado)
                {
                    case "1":
                        Finalizado = " AND D.FL_FINALIZA_DEMURRAGE=1 AND D1.FL_FINALIZA_DEMURRAGE= 1 ";
                        break;
                    default:
                        Finalizado = "";
                        break;
                }

                switch (Ativo)
                {
                    case "1":
                        Ativo = " AND (D.FL_FINALIZA_DEMURRAGE != 1 OR D1.FL_FINALIZA_DEMURRAGE != 1) ";


                        break;
                    default:
                        Ativo = "";
                        break;
                }
            }

            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, B.NR_BL AS MBL, PFCL.NM_TIPO_CONTAINER, PFCL.NR_PROCESSO, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ";
            SQL += "ISNULL(P2.NM_RAZAO, '') AS TRANSPORTADOR, ISNULL(FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy'), '') AS DT_CHEGADA, ";
            SQL += "ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME), '') AS QT_DIAS_FREETIME, ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME_CONFIRMA),'') AS QT_DIAS_FREETIME_CONFIRMA, ISNULL(FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy'), '') AS FINAL_FREETIME, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'), '') AS DEVOLUCAO_CNTR, ";
            SQL += "ISNULL(DFCL.QT_DIAS_DEMURRAGE,'') QT_DIAS_DEMURRAGE, ISNULL(DFCL.QT_DIAS_DEMURRAGE_COMPRA,'')QT_DIAS_DEMURRAGE_COMPRA, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE, 'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, PFCL.DS_STATUS_DETENTION, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE, 'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE_COMPRA, PFCL.DS_STATUS_DETENTION_COMPRA, ";
            SQL += "ISNULL(PFCL.DS_OBSERVACAO, '') AS DS_OBSERVACAO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_DEMURRAGE_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS VL_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(FORMAT(DFCL.VL_DEMURRAGE_COMPRA_BR,'C','pt-br'),'') AS VL_DEMURRAGE_COMPRA_BR, ";
            SQL += "ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS PAG_DEMU, ";
            SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DEMURRAGE_VENDA,'C','pt-br')),'R$',''),'') AS VL_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(FORMAT(DFCL.VL_DEMURRAGE_VENDA_BR,'C','pt-br'),'') AS VL_DEMURRAGE_VENDA_BR, ";
            SQL += "ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS RECEB_DEMU ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
            SQL += "INNER JOIN TB_BL B ON PFCL.ID_BL_MASTER = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_IMPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_STATUS_DEMURRAGE D ON PFCL.ID_STATUS_DEMURRAGE_COMPRA= D.ID_STATUS_DEMURRAGE ";
            SQL += "LEFT JOIN TB_STATUS_DEMURRAGE D1 ON PFCL.ID_STATUS_DEMURRAGE = D1.ID_STATUS_DEMURRAGE ";
            SQL += "WHERE RIGHT(PFCL.NR_PROCESSO,2) >= 18 ";
            SQL += "" + idFilter + " ";
            SQL += "" + Ativo + " ";
            SQL += "" + Finalizado + " ";
            SQL += "ORDER BY PFCL.DT_CHEGADA";
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }


        [WebMethod(EnableSession = true)]
        public string infoContainer(int idCont)
        {
            string SQL;
            /*SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, DFCL.QT_DIAS_DEMURRAGE, ";
            SQL += "PFCL.QT_DIAS_FREETIME,DFCL.QT_DIAS_DEMURRAGE_COMPRA, PFCL.QT_DIAS_FREETIME_CONFIRMA, PFCL.ID_STATUS_DEMURRAGE, DFCL.ID_DEMURRAGE_FATURA_PAGAR, DFCL.ID_DEMURRAGE_FATURA_RECEBER, PFCL.ID_STATUS_DEMURRAGE_COMPRA, FORMAT(PFCL.DT_STATUS_DEMURRAGE_COMPRA, 'yyyy-MM_dd') AS DATA_STATUS_DEMURRAGE_COMPRA, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'yyyy-MM-dd') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "PFCL.DS_OBSERVACAO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "'";*/

            SQL = "SELECT C.NR_CNTR, A.NR_PROCESSO, D.NM_RAZAO AS CLIENTE, C.QT_DIAS_FREETIME, ";
            SQL += "C.QT_DIAS_FREETIME_CONFIRMA, E.QT_DIAS_DEMURRAGE, E.QT_DIAS_DEMURRAGE_COMPRA, ";
            SQL += "C.ID_STATUS_DEMURRAGE, FORMAT(C.DT_STATUS_DEMURRAGE, 'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "C.ID_STATUS_DEMURRAGE_COMPRA, FORMAT(C.DT_STATUS_DEMURRAGE_COMPRA, 'dd/MM/yyyy') AS DATA_STATUS_DEMURRAGE_COMPRA, ";
            SQL += "C.DS_OBSERVACAO ";
            SQL += " FROM TB_BL A ";
            SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO D ON A.ID_PARCEIRO_CLIENTE = D.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE E ON C.ID_CNTR_BL = E.ID_CNTR_BL ";
            SQL += "WHERE C.ID_CNTR_BL = "+idCont+" AND A.GRAU IN('C') AND A.DT_CANCELAMENTO IS NULL ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string infoContainerExp(int idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
            SQL += "PFCL.QT_DIAS_FREETIME,DFCL.QT_DIAS_DEMURRAGE_COMPRA, PFCL.QT_DIAS_FREETIME_CONFIRMA, PFCL.ID_STATUS_DEMURRAGE, DFCL.ID_DEMURRAGE_FATURA_PAGAR, DFCL.ID_DEMURRAGE_FATURA_RECEBER, PFCL.ID_STATUS_DEMURRAGE_COMPRA, FORMAT(PFCL.DT_STATUS_DEMURRAGE_COMPRA, 'yyyy-MM_dd') AS DATA_STATUS_DEMURRAGE_COMPRA, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'yyyy-MM-dd') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "PFCL.DS_OBSERVACAO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "'";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        public string infoContainerDevolucaoExp(int idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
            SQL += "PFCL.ID_STATUS_DEMURRAGE, DFCL.ID_DEMURRAGE_FATURA_PAGAR, DFCL.ID_DEMURRAGE_FATURA_RECEBER, FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'yyyy-MM-dd') as DT_DEVOLUCAO_CNTR, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DEMURRAGE,'yyyy-MM-dd') AS DATA_STATUS_DEMURRAGE, DS_STATUS_DEMURRAGE, ";
            SQL += "PFCL.DS_OBSERVACAO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "'";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string atualizarContainer(int idCont, string dtStatus, int dsStatus, int qtDias, string dsObs, string qtDiasConfirm, string qtDiasDemurrageCompra, string qtDiasDemurrageVenda, string dtStatusCompra, int dsStatusCompra)
        {
            if (dsStatus.ToString() != "" && dsStatusCompra.ToString() != "")
            {
                string SQL;
                string flagF;
                SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + dsStatus + "' ";
                DataTable flFinaliza = new DataTable();
                flFinaliza = DBS.List(SQL);
                flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER, ";
                SQL += "ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS PAG_DEMU, ";
                SQL += "ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS RECEB_DEMU ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                if (dsStatus == 2)
                {
                    flagF = "1";
                }
                else
                {
                    if (faturaCompra != "" && faturaVenda != "")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        flagF = "0";
                    }
                }

                SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = '" + dsStatus + "', QT_DIAS_FREETIME = '" + qtDias + "', QT_DIAS_FREETIME_CONFIRMA = '" + qtDiasConfirm + "', ";
                SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "', DS_OBSERVACAO = '" + dsObs + "', ID_STATUS_DEMURRAGE_COMPRA = '" + dsStatusCompra +"', DT_STATUS_DEMURRAGE_COMPRA = '"+dtStatusCompra+"' WHERE ID_CNTR_BL = '" + idCont + "' ;  UPDATE TB_CNTR_DEMURRAGE SET QT_DIAS_DEMURRAGE_COMPRA = '" + qtDiasDemurrageCompra + "', QT_DIAS_DEMURRAGE = '" +qtDiasDemurrageVenda+ "' WHERE ID_CNTR_BL = '" + idCont + "'   ";
                string atualizarContainer = DBS.ExecuteScalar(SQL);
                return "1";


            }
            else
            {
                return "2";
            }
        }

        [WebMethod(EnableSession = true)]
        public string atualizarContainerExp(int idCont, string dtStatus, int dsStatus, int qtDias, string dsObs, string qtDiasConfirm, string qtDiasDemurrageCompra, string dtStatusCompra, int dsStatusCompra)
        {
            if (dsStatus.ToString() != "" && dsStatusCompra.ToString() != "")
            {
                string SQL;
                string flagF;
                SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + dsStatus + "' ";
                DataTable flFinaliza = new DataTable();
                flFinaliza = DBS.List(SQL);
                flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER, ";
                SQL += "ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS PAG_DEMU, ";
                SQL += "ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DEMURRAGE, 'dd/MM/yyyy'), '') AS RECEB_DEMU ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                if (dsStatus == 2)
                {
                    flagF = "1";
                }
                else
                {
                    if (faturaCompra != "" && faturaVenda != "")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        flagF = "0";
                    }
                }

                SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = '" + dsStatus + "', QT_DIAS_FREETIME = '" + qtDias + "', QT_DIAS_FREETIME_CONFIRMA = '" + qtDiasConfirm + "', ";
                SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "', DS_OBSERVACAO = '" + dsObs + "', ID_STATUS_DEMURRAGE_COMPRA = '" + dsStatusCompra + "', DT_STATUS_DEMURRAGE_COMPRA = '" + dtStatusCompra + "' WHERE ID_CNTR_BL = '" + idCont + "' ;  UPDATE TB_CNTR_DEMURRAGE SET QT_DIAS_DEMURRAGE_COMPRA = '" + qtDiasDemurrageCompra + "' WHERE ID_CNTR_BL = '" + idCont + "'   ";
                string atualizarContainer = DBS.ExecuteScalar(SQL);
                return "1";


            }
            else
            {
                return "2";
            }
        }

        [WebMethod(EnableSession = true)]
        public string infoCalculo(string idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.NR_PROCESSO as PROCESSO, P.NM_RAZAO AS CLIENTE, PFCL.ID_PARCEIRO_TRANSPORTADOR AS TRANSPORTADOR ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string infoCalculoExp(string idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.NR_PROCESSO as PROCESSO, P.NM_RAZAO AS CLIENTE, PFCL.ID_PARCEIRO_TRANSPORTADOR AS TRANSPORTADOR ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarCalculoDemurrage(string nrProcesso, string tipoCalculo)
        {

            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, ";
            SQL += "PFCL.NM_TIPO_CONTAINER, FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy') as DT_CHEGADA, ";
            SQL += "PFCL.QT_DIAS_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy') AS DT_FINAL_FREETIME, ";
            SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') as DT_DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE,DFCL.QT_DIAS_DEMURRAGE_COMPRA, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DEMURRAGE_COMPRA,'c','pt-br')),'R$',''),'') AS VL_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(M.NM_MOEDA,'') AS VENDA, ISNULL(M2.NM_MOEDA,'') AS COMPRA, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DEMURRAGE_VENDA,'c','pt-br')),'R$',''),'') AS VL_DEMURRAGE_VENDA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DEMURRAGE_VENDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_MOEDA M2 ON DFCL.ID_MOEDA_DEMURRAGE_COMPRA = M2.ID_MOEDA ";
            SQL += "WHERE PFCL.NR_PROCESSO = '" + nrProcesso + "' AND (PFCL.DT_CHEGADA IS NOT NULL OR PFCL.DT_CHEGADA != '') ";
            SQL += "AND (PFCL.QT_DIAS_FREETIME IS NOT NULL OR PFCL.QT_DIAS_FREETIME != '') ";
            SQL += "AND (PFCL.DT_DEVOLUCAO_CNTR IS NOT NULL OR PFCL.DT_DEVOLUCAO_CNTR != '') ";
            if (tipoCalculo == "1")
            {
                SQL += "AND (DFCL.ID_DEMURRAGE_FATURA_RECEBER IS NULL OR DFCL.ID_DEMURRAGE_FATURA_RECEBER = '') ";
                SQL += "AND DFCL.QT_DIAS_DEMURRAGE > 0 ";
            }
            else
            {
                SQL += "AND (DFCL.ID_DEMURRAGE_FATURA_PAGAR IS NULL OR DFCL.ID_DEMURRAGE_FATURA_PAGAR = '') ";
                SQL += "AND DFCL.QT_DIAS_DEMURRAGE_COMPRA > 0 ";
            }
            SQL += "ORDER BY PFCL.NR_CNTR ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string infoCalculoMarcadoVenda(string idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, TBD.QT_DIAS_FREETIME as FreeTimeTab, PFCL.QT_DIAS_FREETIME, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE,DFCL.QT_DIAS_DEMURRAGE_COMPRA, TBD.FL_ESCALONADA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string infoCalculoMarcadoVendaTaxa(string idCont)
        {
            string SQL;
            int somaDias;
            decimal vlTaxa = 0;

            SQL = "SELECT ISNULL(B.ID_TIPO_CARGA,0) AS ID_TIPO_CARGA FROM TB_CNTR_BL A ";
            SQL += "LEFT JOIN TB_CARGA_BL B ON A.ID_CNTR_BL=B.ID_CNTR_BL ";
            SQL += "WHERE A.ID_CNTR_BL = "+idCont+" ";
            string tipoCarga = DBS.ExecuteScalar(SQL);

            SQL = "SELECT PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, TBD.QT_DIAS_FREETIME as FreeTimeTab, PFCL.QT_DIAS_FREETIME, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, P.NM_RAZAO AS TABELA, M.NM_MOEDA AS MOEDA, TBD.FL_ESCALONADA, CD.VL_TAXA_DEMURRAGE_VENDA, ";
            SQL += "TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
            SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
            SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
            SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            if(tipoCarga == "9")
			{
                SQL += "LEFT JOIN TB_TIPO_CONTAINER TPC ON PFCL.NM_TIPO_CONTAINER = TPC.NM_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON TPC.ID_TAMANHO_CONTAINER = TBD.ID_TAMANHO_CONTAINER ";
			}
			else
			{
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
			}
            SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont +"' "+ (tipoCarga == "9" ? "AND TBD.FL_CARGA_IMO = 1 ":"") +"  ";
            SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 1 ";
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
                int diasDemurrage = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                int qtFreetime = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"];

                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    if (diasDemurrage == 0)
                    {
                        vlTaxa = 0;
                    }
                    else
                    {
                        diasDemurrage = diasDemurrage + qtFreetime;
                        if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                        {
                            if (diasDemurrage <= d1)
                            {
                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_01"];
                            }
                            else if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                            {
                                if (diasDemurrage <= d2)
                                {
                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                }
                                else if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                {
                                    if (diasDemurrage <= d3)
                                    {
                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                    }
                                    else if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                    {
                                        if (diasDemurrage <= d4)
                                        {
                                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                        }
                                        else if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                        {
                                            if (diasDemurrage <= d5)
                                            {
                                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                            }
                                            else if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                            {
                                                if (diasDemurrage <= d6)
                                                {
                                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                }
                                                else if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                {
                                                    if (diasDemurrage <= d7)
                                                    {
                                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                    }
                                                    else if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                    {
                                                        if (diasDemurrage <= d8)
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

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        public string infoCalculoMarcadoCompraTaxa(string idCont, string transportador)
        {
            string SQL;
            int somaDias;
            decimal vlTaxa = 0;

            SQL = "SELECT ISNULL(B.ID_TIPO_CARGA,0) AS ID_TIPO_CARGA FROM TB_CNTR_BL A ";
            SQL += "LEFT JOIN TB_CARGA_BL B ON A.ID_CNTR_BL=B.ID_CNTR_BL ";
            SQL += "WHERE A.ID_CNTR_BL = " + idCont + " ";
            string tipoCarga = DBS.ExecuteScalar(SQL);

            SQL = "SELECT PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, TBD.QT_DIAS_FREETIME as FreeTimeTab, PFCL.QT_DIAS_FREETIME, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE,CONVERT(INT,ISNULL(DFCL.QT_DIAS_DEMURRAGE_COMPRA,DFCL.QT_DIAS_DEMURRAGE))QT_DIAS_DEMURRAGE_COMPRA, P.NM_RAZAO as TABELA, M.NM_MOEDA AS MOEDA, TBD.FL_ESCALONADA, CD.VL_TAXA_DEMURRAGE_COMPRA, ";
            SQL += "TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
            SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
            SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
            SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            if (tipoCarga == "9")
            {
                SQL += "LEFT JOIN TB_TIPO_CONTAINER TPC ON PFCL.NM_TIPO_CONTAINER = TPC.NM_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON TPC.ID_TAMANHO_CONTAINER = TBD.ID_TAMANHO_CONTAINER ";
            }
            else
            {
                SQL += "LEFT JOIN TB_TABELA_DEMURRAGE TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            }
            SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' " + (tipoCarga == "9" ? "AND TBD.FL_CARGA_IMO = 1 " : "") + "  ";
            SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '" + transportador + "' ";
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
                int diasDemurrage = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"];

                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                    {
                        if (diasDemurrage <= d1)
                        {
                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_01"];
                        }
                        else if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                        {
                            if (diasDemurrage <= d1 + d2)
                            {
                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_02"];
                            }
                            else if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                            {
                                if (diasDemurrage <= d1 + d2 + d3)
                                {
                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                }
                                else if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                {
                                    if (diasDemurrage <= d1 + d2 + d3 + d4)
                                    {
                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                    }
                                    else if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                    {
                                        if (diasDemurrage <= d1 + d2 + d3 + d4 + d5)
                                        {
                                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                        }
                                        else if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                        {
                                            if (diasDemurrage <= d1 + d2 + d3 + d4 + d5 + d6)
                                            {
                                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                            }
                                            else if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                            {
                                                if (diasDemurrage <= d1 + d2 + d3 + d4 + d5 + d6 + d7)
                                                {
                                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                }
                                                else if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                {
                                                    if (diasDemurrage <= d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8)
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

        [WebMethod(EnableSession = true)]
        public void zerarCalculoVenda(string idCont)
        {
            string SQL;
            SQL = "select id_cntr_bl from TB_CNTR_DEMURRAGE ";
            SQL += "where id_cntr_bl = '" + idCont + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_CALCULO_DEMURRAGE_VENDA = NULL, ";
                SQL += "ID_MOEDA_DEMURRAGE_VENDA = NULL, VL_TAXA_DEMURRAGE_VENDA = NULL, ";
                SQL += "VL_DEMURRAGE_VENDA = NULL, DT_CAMBIO_DEMURRAGE_VENDA = NULL, ";
                SQL += "VL_CAMBIO_DEMURRAGE_VENDA = NULL, VL_DESCONTO_DEMURRAGE_VENDA = NULL, ";
                SQL += "VL_DEMURRAGE_VENDA_BR = NULL WHERE ID_CNTR_BL = " + idCont + " ";
                string zerar = DBS.ExecuteScalar(SQL);
            }
        }

        [WebMethod(EnableSession = true)]
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
                SQL += "VL_DEMURRAGE_COMPRA_BR = NULL WHERE ID_CNTR_BL = '" + idCont + "' ";
                string zerar = DBS.ExecuteScalar(SQL);
            }
        }

        [WebMethod(EnableSession = true)]
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
                SQL += "ISNULL(CONVERT(VARCHAR, DFCL.ID_DEMURRAGE_FATURA_PAGAR), '') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, ";
                SQL += "DFCL.QT_DIAS_DEMURRAGE,CONVERT(int,ISNULL(DFCL.QT_DIAS_DEMURRAGE_COMPRA,DFCL.QT_DIAS_DEMURRAGE))QT_DIAS_DEMURRAGE_COMPRA,";
                SQL += "DFCL.ID_MOEDA_DEMURRAGE_VENDA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
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
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 1 ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string faturaCompra = listTable.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = listTable.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();
                
                somaDias = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] * vlTaxa;

                SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                SQL += "DT_FINAL_DEMURRAGE, QT_DIAS_DEMURRAGE, DT_CALCULO_DEMURRAGE_VENDA, ID_MOEDA_DEMURRAGE_VENDA, VL_TAXA_DEMURRAGE_VENDA, ";
                SQL += "VL_DEMURRAGE_VENDA, DT_CAMBIO_DEMURRAGE_VENDA, VL_CAMBIO_DEMURRAGE_VENDA, VL_DESCONTO_DEMURRAGE_VENDA, VL_DEMURRAGE_VENDA_BR,QT_DIAS_DEMURRAGE_COMPRA ) VALUES ";
                SQL += "(" + idCont + ",'" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "','" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', ";
                SQL += "'" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "','" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "'," + somaDias + ", ";
                SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + "," + vlTaxa.ToString().Replace(",", ".") + "," + vlDemurr.ToString().Replace(",", ".") + ",null,null,null,null,'" + listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"] + "')";
                calcular = DBS.ExecuteScalar(SQL);

                string flagF;
                SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                DataTable flFinaliza = new DataTable();
                flFinaliza = DBS.List(SQL);
                flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                if (idStatus == "2")
                {
                    flagF = "1";
                }
                else
                {
                    if (faturaCompra != "" && faturaVenda != "")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        flagF = "0";
                    }
                }

                SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = '" + idStatus + "' ,DT_STATUS_DEMURRAGE = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
                string atualizarStatus = DBS.ExecuteScalar(SQL);

            }
            else
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "ISNULL(CONVERT(VARCHAR, DFCL.ID_DEMURRAGE_FATURA_PAGAR), '') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE,";
                SQL += "DFCL.QT_DIAS_DEMURRAGE,CONVERT(int,ISNULL(DFCL.QT_DIAS_DEMURRAGE_COMPRA,DFCL.QT_DIAS_DEMURRAGE))QT_DIAS_DEMURRAGE_COMPRA,";
                SQL += "DFCL.ID_MOEDA_DEMURRAGE_VENDA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
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
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 1 ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string faturaCompra = listTable.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = listTable.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                somaDias = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"];
                vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] * vlTaxa;

                SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DEMURRAGE = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ";
                SQL += "DT_FINAL_DEMURRAGE = '" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', QT_DIAS_DEMURRAGE = " + somaDias + ", ";
                SQL += "DT_CALCULO_DEMURRAGE_VENDA = '" + sqlFormattedDate + "', ID_MOEDA_DEMURRAGE_VENDA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DEMURRAGE_VENDA = " + vlTaxa.ToString().Replace(",", ".") + ", ";
                SQL += "VL_DEMURRAGE_VENDA = " + vlDemurr.ToString().Replace(",", ".") + ", DT_CAMBIO_DEMURRAGE_VENDA = null, VL_CAMBIO_DEMURRAGE_VENDA = null, VL_DESCONTO_DEMURRAGE_VENDA = null, VL_DEMURRAGE_VENDA_BR = null, QT_DIAS_DEMURRAGE_COMPRA = " + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"] + " WHERE ID_CNTR_BL = " + idCont + "";
                calcular = DBS.ExecuteScalar(SQL);

                string flagF;
                SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                DataTable flFinaliza = new DataTable();
                flFinaliza = DBS.List(SQL);
                flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                if (idStatus == "2")
                {
                    flagF = "1";
                }
                else
                {
                    if (faturaCompra != "" && faturaVenda != "")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        flagF = "0";
                    }
                }

                SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = '" + idStatus + "' ,DT_STATUS_DEMURRAGE = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
                string atualizarStatus = DBS.ExecuteScalar(SQL);

            }
        }

        [WebMethod(EnableSession = true)]
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
                SQL += "ISNULL(CONVERT(VARCHAR, DFCL.ID_DEMURRAGE_FATURA_PAGAR), '') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE, CONVERT(int ,ISNULL(DFCL.QT_DIAS_DEMURRAGE_COMPRA,DFCL.QT_DIAS_DEMURRAGE))QT_DIAS_DEMURRAGE_COMPRA, DFCL.ID_MOEDA_DEMURRAGE_COMPRA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
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
                string faturaCompra = listTable.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = listTable.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();


                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"];
                    vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"] * vlTaxa;

                    SQL = "INSERT INTO TB_CNTR_DEMURRAGE (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DEMURRAGE, ";
                    SQL += "DT_FINAL_DEMURRAGE, DT_CALCULO_DEMURRAGE_COMPRA, ID_MOEDA_DEMURRAGE_COMPRA, VL_TAXA_DEMURRAGE_COMPRA, ";
                    SQL += "VL_DEMURRAGE_COMPRA, DT_CAMBIO_DEMURRAGE_COMPRA, VL_CAMBIO_DEMURRAGE_COMPRA, VL_DESCONTO_DEMURRAGE_COMPRA, VL_DEMURRAGE_COMPRA_BR, QT_DIAS_DEMURRAGE_COMPRA,QT_DIAS_DEMURRAGE ) VALUES ";
                    SQL += "(" + idCont + ",'" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "','" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', ";
                    SQL += "'" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "','" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', ";
                    SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + "," + vlTaxa.ToString().Replace(",", ".") + "," + vlDemurr.ToString().Replace(",", ".") + ",null,null,null,null," + somaDias + "," + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] + " )";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    if (idStatus == "2")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        if (faturaCompra != "" && faturaVenda != "")
                        {
                            flagF = "1";
                        }
                        else
                        {
                            flagF = "0";
                        }
                    }

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE_COMPRA = '" + idStatus + "' ,DT_STATUS_DEMURRAGE_COMPRA = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
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
                    demurrage = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"];
                    vlDemurr = 0;

                    if (demurrage <= ft)
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
                    SQL += "DT_FINAL_DEMURRAGE, DT_CALCULO_DEMURRAGE_COMPRA, ID_MOEDA_DEMURRAGE_COMPRA, VL_TAXA_DEMURRAGE_COMPRA, ";
                    SQL += "VL_DEMURRAGE_COMPRA, DT_CAMBIO_DEMURRAGE_COMPRA, VL_CAMBIO_DEMURRAGE_COMPRA, VL_DESCONTO_DEMURRAGE_COMPRA, VL_DEMURRAGE_COMPRA_BR, QT_DIAS_DEMURRAGE_COMPRA,QT_DIAS_DEMURRAGE ) VALUES ";
                    SQL += "(" + idCont + "," + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "," + listTable.Rows[0]["DT_FINAL_FREETIME"] + ", ";
                    SQL += "" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "," + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + ", ";
                    SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + ",0,'" + vlDemurr.ToString().Replace(",", ".") + "',null,null,null,null," + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"] + "," + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE"] + " )";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    if (idStatus == "2")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        if (faturaCompra != "" && faturaVenda != "")
                        {
                            flagF = "1";
                        }
                        else
                        {
                            flagF = "0";
                        }
                    }

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE_COMPRA = '" + idStatus + "',DT_STATUS_DEMURRAGE_COMPRA = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);
                }
            }
            else
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "ISNULL(CONVERT(VARCHAR, DFCL.ID_DEMURRAGE_FATURA_PAGAR), '') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'yyyy-MM-dd') AS DT_INICIAL_DEMURRAGE, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'yyyy-MM-dd') AS DT_FINAL_DEMURRAGE,";
                SQL += "DFCL.QT_DIAS_DEMURRAGE, CONVERT(int ,ISNULL(DFCL.QT_DIAS_DEMURRAGE_COMPRA,DFCL.QT_DIAS_DEMURRAGE))QT_DIAS_DEMURRAGE_COMPRA, ";
                SQL += " DFCL.ID_MOEDA_DEMURRAGE_COMPRA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
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
                string faturaCompra = listTable.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = listTable.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"];
                    vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"] * vlTaxa;

                    SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                    SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DEMURRAGE = '" + listTable.Rows[0]["DT_INICIAL_DEMURRAGE"] + "', ";
                    SQL += "DT_FINAL_DEMURRAGE = '" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', QT_DIAS_DEMURRAGE_COMPRA = " + somaDias + ", ";
                    SQL += "DT_CALCULO_DEMURRAGE_COMPRA = '" + sqlFormattedDate + "', ID_MOEDA_DEMURRAGE_COMPRA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DEMURRAGE_COMPRA = " + vlTaxa.ToString().Replace(",", ".") + ", ";
                    SQL += "VL_DEMURRAGE_COMPRA = " + vlDemurr.ToString().Replace(",", ".") + ", DT_CAMBIO_DEMURRAGE_COMPRA = null, VL_CAMBIO_DEMURRAGE_COMPRA = null, VL_DESCONTO_DEMURRAGE_COMPRA = null, VL_DEMURRAGE_COMPRA_BR = null WHERE ID_CNTR_BL = " + idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    if (idStatus == "2")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        if (faturaCompra != "" && faturaVenda != "")
                        {
                            flagF = "1";
                        }
                        else
                        {
                            flagF = "0";
                        }
                    }
                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE_COMPRA = '" + idStatus + "',DT_STATUS_DEMURRAGE_COMPRA = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
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
                    demurrage = (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"];
                    vlDemurr = 0;

                    if (demurrage <= ft)
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
                    SQL += "DT_FINAL_DEMURRAGE = '" + listTable.Rows[0]["DT_FINAL_DEMURRAGE"] + "', QT_DIAS_DEMURRAGE_COMPRA = " + (int)listTable.Rows[0]["QT_DIAS_DEMURRAGE_COMPRA"] + ", ";
                    SQL += "DT_CALCULO_DEMURRAGE_COMPRA = '" + sqlFormattedDate + "', ID_MOEDA_DEMURRAGE_COMPRA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DEMURRAGE_COMPRA = 0, ";
                    SQL += "VL_DEMURRAGE_COMPRA = " + vlDemurr.ToString().Replace(",", ".") + ", DT_CAMBIO_DEMURRAGE_COMPRA = null, VL_CAMBIO_DEMURRAGE_COMPRA = null, VL_DESCONTO_DEMURRAGE_COMPRA = null, VL_DEMURRAGE_COMPRA_BR = null WHERE ID_CNTR_BL = " + idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DEMURRAGE FROM TB_STATUS_DEMURRAGE WHERE ID_STATUS_DEMURRAGE = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DEMURRAGE"].ToString();

                    if (idStatus == "2")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        if (faturaCompra != "" && faturaVenda != "")
                        {
                            flagF = "1";
                        }
                        else
                        {
                            flagF = "0";
                        }
                    }

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE_COMPRA = '" + idStatus + "',DT_STATUS_DEMURRAGE_COMPRA = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarContainerDevolucao(string nrProcesso)
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'),'') as DT_DEVOLUCAO_CNTR, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy'),'') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "PFCL.DS_STATUS_DEMURRAGE ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "WHERE PFCL.NR_PROCESSO = '" + nrProcesso + "' ";
            SQL += "AND (DFCL.DT_EXPORTACAO_DEMURRAGE_PAGAR IS NULL AND DFCL.DT_EXPORTACAO_DEMURRAGE_RECEBER IS NULL) ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarContainerDevolucaoExp(string nrProcesso)
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'),'') as DT_DEVOLUCAO_CNTR, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_STATUS_DEMURRAGE,'dd/MM/yyyy'),'') AS DATA_STATUS_DEMURRAGE, ";
            SQL += "PFCL.DS_STATUS_DETENTION ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "WHERE PFCL.NR_PROCESSO = '" + nrProcesso + "' ";
            SQL += "AND (DFCL.DT_EXPORTACAO_DEMURRAGE_PAGAR IS NULL AND DFCL.DT_EXPORTACAO_DEMURRAGE_RECEBER IS NULL) ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarFaturas(int check, string filtroFatura, string txtFiltro, string Ativo, string Finalizado)
        {
            DataTable listTable = new DataTable();
            switch (filtroFatura)
            {
                case "1":
                    filtroFatura = "AND C.NR_PROCESSO = '" + txtFiltro + "' ";
                    break;
                case "2":
                    filtroFatura = "AND C.NM_CLIENTE = '" + txtFiltro + "' ";
                    break;
                case "3":
                    filtroFatura = "AND C.NM_TRANSPORTADOR = '" + txtFiltro + "' ";
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
                if (check == 1)
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
                            Finalizado = "AND (C.DT_CANCELAMENTO IS NOT NULL OR C.DT_LIQUIDACAO IS NOT NULL OR C.DT_EXPORTACAO_DEMURRAGE IS NOT NULL) ";
                            break;
                        default:
                            Finalizado = "";
                            break;
                    }
                }
                else
                {
                    switch (Ativo)
                    {
                        case "1":
                            Ativo = "AND C.DT_CANCELAMENTO IS NULL AND C.DT_LIQUIDACAO IS NULL AND C.DT_EXPORTACAO_DEMURRAGE_COMPRA IS NULL ";
                            break;
                        default:
                            Ativo = "";
                            break;
                    }

                    switch (Finalizado)
                    {
                        case "1":
                            Finalizado = "AND (C.DT_CANCELAMENTO IS NOT NULL OR C.DT_LIQUIDACAO IS NOT NULL OR C.DT_EXPORTACAO_DEMURRAGE_COMPRA IS NOT NULL) ";
                            break;
                        default:
                            Finalizado = "";
                            break;
                    }
                }
            }

            if (check == 1)
            {
                string SQL;
                /*SQL = "SELECT C.ID_DEMURRAGE_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DEMURRAGE,'dd/MM/yyyy'),'') as DT_EXPORTACAO_DEMURRAGE, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO,'dd/MM/yyyy'),'') AS DT_LIQUIDACAO,ISNULL(FORMAT(C.DT_CANCELAMENTO,'dd/MM/yyyy'),'') AS DT_CANCELAMENTO ";
                SQL += "FROM VW_DEMURRAGE_FATURA C ";
                SQL += "JOIN TB_DEMURRAGE_FATURA B ON C.ID_DEMURRAGE_FATURA = B.ID_DEMURRAGE_FATURA ";
                SQL += "WHERE B.CD_PR = 'R' ";*/
                SQL = "SELECT C.ID_DEMURRAGE_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DEMURRAGE, 'dd/MM/yyyy'), '') as DT_EXPORTACAO_DEMURRAGE, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO, 'dd/MM/yyyy'), '') AS DT_LIQUIDACAO, ISNULL(FORMAT(C.DT_CANCELAMENTO, 'dd/MM/yyyy'), '') AS DT_CANCELAMENTO, ";
                SQL += "(SELECT MAX(CASE WHEN E.DT_CAMBIO_DEMURRAGE_VENDA IS NULL THEN 1 ELSE 0 END) ";
                SQL += "FROM TB_DEMURRAGE_FATURA_ITENS D INNER JOIN TB_CNTR_DEMURRAGE E ON E.ID_CNTR_DEMURRAGE = D.ID_CNTR_DEMURRAGE ";
                SQL += "WHERE D.ID_DEMURRAGE_FATURA = C.ID_DEMURRAGE_FATURA) AS FALTA_ATUALIZACAO_CAMBIAL, ";
                SQL += "(SELECT COUNT(*) FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA = C.ID_DEMURRAGE_FATURA) AS QT_PARCELAS ";
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
                /*SQL = "SELECT C.ID_DEMURRAGE_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DEMURRAGE,'dd/MM/yyyy'),'') as DT_EXPORTACAO_DEMURRAGE, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO,'dd/MM/yyyy'),'') AS DT_LIQUIDACAO,ISNULL(FORMAT(C.DT_CANCELAMENTO,'dd/MM/yyyy'),'') AS DT_CANCELAMENTO ";
                SQL += "FROM VW_DEMURRAGE_FATURA C ";
                SQL += "JOIN TB_DEMURRAGE_FATURA B ON C.ID_DEMURRAGE_FATURA = B.ID_DEMURRAGE_FATURA ";
                SQL += "WHERE B.CD_PR = 'P' ";*/
                SQL = "SELECT C.ID_DEMURRAGE_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DEMURRAGE_COMPRA, 'dd/MM/yyyy'), '') as DT_EXPORTACAO_DEMURRAGE, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO, 'dd/MM/yyyy'), '') AS DT_LIQUIDACAO, ISNULL(FORMAT(C.DT_CANCELAMENTO, 'dd/MM/yyyy'), '') AS DT_CANCELAMENTO, ";
                SQL += "(SELECT MAX(CASE WHEN E.DT_CAMBIO_DEMURRAGE_COMPRA IS NULL THEN 1 ELSE 0 END) ";
                SQL += "FROM TB_DEMURRAGE_FATURA_ITENS D INNER JOIN TB_CNTR_DEMURRAGE E ON E.ID_CNTR_DEMURRAGE = D.ID_CNTR_DEMURRAGE ";
                SQL += "WHERE D.ID_DEMURRAGE_FATURA = C.ID_DEMURRAGE_FATURA) AS FALTA_ATUALIZACAO_CAMBIAL, ";
                SQL += "(SELECT COUNT(*) FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA = C.ID_DEMURRAGE_FATURA) AS QT_PARCELAS ";
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

        [WebMethod(EnableSession = true)]
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
            if (listTable != null)
            {
                return "OK";
            }
            else
            {
                return null;
            }
        }

        [WebMethod(EnableSession = true)]
        public string ListarProcessoTaxaAberta(string dataI, string dataF, string chkB, string chkE, string filter, string text)
        {
            string sqlFormattedDate;
            string sqlFormattedDate2;
            string SQL;

            if (dataI != "")
            {
                string diaI = dataI.Substring(8, 2);
                string mesI = dataI.Substring(5, 2);
                string anoI = dataI.Substring(0, 4);
                sqlFormattedDate = diaI + "/" + mesI + "/" + anoI;
            }
            else
            {
                sqlFormattedDate = "01/01/1900";
            }

            if (dataF != "")
            {
                string diaF = dataF.Substring(8, 2);
                string mesF = dataF.Substring(5, 2);
                string anoF = dataF.Substring(0, 4);
                sqlFormattedDate2 = diaF + "/" + mesF + "/" + anoF;
            }
            else
            {
                sqlFormattedDate2 = "01/01/2900";
            }

            if (chkB == "1" && chkE == "1"){ chkB = ""; chkE = "";}
            else{
                switch (chkB)
                {
                    case "1":
                        chkB = " AND A.ID_ORIGEM_PAGAMENTO = 1 ";
                        break;
                    default:
                        chkB = "";
                        break;
                }

                switch (chkE)
                {
                    case "1":
                        chkE = " AND A.ID_ORIGEM_PAGAMENTO = 2 ";
                        break;
                    default:
                        chkE = "";
                        break;
                }
            }


            SQL = "SELECT A.NR_PROCESSO, A.NM_ITEM_DESPESA, ISNULL(A.DESTINATARIO_COBRANCA,'') AS DESTINATARIO_COBRANCA, A.VL_TAXA, A.VL_TAXA_CALCULADO, ";
            SQL += "A.SIGLA_MOEDA, A.TIPO, A.NM_TIPO_PAGAMENTO, A.NM_ORIGEM_PAGAMENTO, A.DT_CHEGADA ";
            SQL += "FROM (";
            SQL += "SELECT E.NR_PROCESSO, B.NM_ITEM_DESPESA, C.NM_RAZAO as DESTINATARIO_COBRANCA, A.VL_TAXA, A.VL_TAXA_CALCULADO,  ";
            SQL += "D.SIGLA_MOEDA, CASE WHEN A.CD_PR = 'P' THEN 'PAGAR' ELSE 'RECEBER' END AS TIPO, F.NM_TIPO_PAGAMENTO, G.NM_ORIGEM_PAGAMENTO, ISNULL(FORMAT(E.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA ";
            SQL += "FROM TB_BL_TAXA A ";
            SQL += "JOIN TB_ITEM_DESPESA B ON A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON A.ID_PARCEIRO_EMPRESA = C.ID_PARCEIRO ";
            SQL += "JOIN TB_MOEDA D ON A.ID_MOEDA = D.ID_MOEDA ";
            SQL += "JOIN TB_BL E ON A.ID_BL = E.ID_BL ";
            SQL += "JOIN TB_TIPO_PAGAMENTO F ON A.ID_TIPO_PAGAMENTO = F.ID_TIPO_PAGAMENTO ";
            SQL += "JOIN TB_ORIGEM_PAGAMENTO G ON A.ID_ORIGEM_PAGAMENTO = G.ID_ORIGEM_PAGAMENTO ";
            SQL += "WHERE A.ID_BL_TAXA NOT IN(SELECT ISNULL(ID_BL_TAXA, 0) FROM TB_CONTA_PAGAR_RECEBER_ITENS A ";
            SQL += "JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL) ";
            SQL += "AND E.ID_BL_MASTER IS NOT NULL ";
            SQL += "AND CONVERT(DATE, E.DT_ABERTURA,103) BETWEEN CONVERT(DATE, '"+sqlFormattedDate+"', 103)  ";
            SQL += "AND CONVERT(DATE, '" + sqlFormattedDate2 + "', 103)  ";
            SQL += "AND E.ID_BL IN(SELECT DISTINCT(A.ID_BL) FROM TB_CONTA_PAGAR_RECEBER_ITENS A ";
            SQL += "JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER ";
            SQL += "WHERE B.DT_CANCELAMENTO IS NULL AND A.ID_ITEM_DESPESA = 14 AND CD_PR = 'P') ";
            SQL += ""+ chkE + " "+ chkB+ " AND(A.FL_TAXA_INATIVA = 0 OR A.FL_TAXA_INATIVA IS NULL) AND A.VL_TAXA <> 0.00 ";
            SQL += " ";
            SQL += "UNION ";
            SQL += " ";
            SQL += "SELECT DISTINCT X.NR_PROCESSO, X.NM_ITEM_DESPESA, X.NM_RAZAO AS DESTINATARIO_COBRANCA, ABS(X.VL_ITEM_DESPESA) AS VL_TAXA, ";
            SQL += "ABS(X.VL_ITEM_DESPESA) AS VL_TAXA_CALCULADO, X.SIGLA_MOEDA, X.TIPO AS TIPO, '------' AS NM_TIPO_PAGAMENTO, X.NM_ORIGEM_PAGAMENTO, X.DT_CHEGADA FROM( ";
            SQL += "SELECT DISTINCT E.ID_BL, E.NR_PROCESSO, 'COMISSAO' AS NM_ITEM_DESPESA, G.SIGLA_MOEDA, H.NM_RAZAO, ";
            SQL += "(SELECT ISNULL(VL_TAXA_CALCULADO, 0) AS VL_ITEM_DESPESA FROM FN_ACCOUNT_DEVOLUCAO_COMISSAO(E.ID_BL, 'C') ";
            SQL += "WHERE VL_TAXA_CALCULADO IS NOT NULL AND VL_TAXA_CALCULADO != CONVERT(DECIMAL(15, 2), 0.00)) AS VL_ITEM_DESPESA, ";
            SQL += "  (SELECT CASE WHEN ISNULL(VL_TAXA_CALCULADO, 0) < 0 THEN 'RECEBER' ELSE 'PAGAR' END AS TIPO FROM FN_ACCOUNT_DEVOLUCAO_COMISSAO(E.ID_BL, 'C') ";
            SQL += "  WHERE VL_TAXA_CALCULADO IS NOT NULL AND VL_TAXA_CALCULADO != CONVERT(DECIMAL(15, 2), 0.00)) AS TIPO, ";
            SQL += "I.NM_ORIGEM_PAGAMENTO, ISNULL(FORMAT(E.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA ";
            SQL += "FROM TB_BL_TAXA A ";
            SQL += "JOIN TB_ITEM_DESPESA B ON A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON A.ID_PARCEIRO_EMPRESA = C.ID_PARCEIRO ";
            SQL += "JOIN TB_MOEDA D ON A.ID_MOEDA = D.ID_MOEDA ";
            SQL += "JOIN TB_BL E ON A.ID_BL = E.ID_BL ";
            SQL += "JOIN TB_TIPO_PAGAMENTO F ON A.ID_TIPO_PAGAMENTO = F.ID_TIPO_PAGAMENTO ";
            SQL += "JOIN TB_MOEDA G ON E.ID_MOEDA_FRETE = G.ID_MOEDA ";
            SQL += "JOIN TB_PARCEIRO H ON E.ID_PARCEIRO_AGENTE_INTERNACIONAL = H.ID_PARCEIRO ";
            SQL += "JOIN TB_ORIGEM_PAGAMENTO I ON A.ID_ORIGEM_PAGAMENTO = I.ID_ORIGEM_PAGAMENTO ";
            SQL += "WHERE A.ID_BL_TAXA NOT IN(SELECT ISNULL(ID_BL_TAXA, 0) FROM TB_CONTA_PAGAR_RECEBER_ITENS A ";
            SQL += "JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL) ";
            SQL += "AND E.ID_BL_MASTER IS NOT NULL ";
            SQL += "AND CONVERT(DATE, E.DT_ABERTURA,103) BETWEEN CONVERT(DATE, '" + sqlFormattedDate + "', 103)  ";
            SQL += "AND CONVERT(DATE, '" + sqlFormattedDate2 + "', 103)  ";
            SQL += "AND E.ID_BL IN(SELECT DISTINCT(A.ID_BL) FROM TB_CONTA_PAGAR_RECEBER_ITENS A ";
            SQL += "JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER ";
            SQL += "WHERE B.DT_CANCELAMENTO IS NULL AND A.ID_ITEM_DESPESA = 14 AND CD_PR = 'P') ";
            SQL += ") X ";
            SQL += "JOIN TB_CONTA_PAGAR_RECEBER_ITENS Z ON X.ID_BL = Z.ID_BL ";
            SQL += "WHERE VL_ITEM_DESPESA IS NOT NULL ";
            SQL += "AND X.NR_PROCESSO IN(SELECT A.NR_PROCESSO FROM ( ";
            SQL += "SELECT NR_PROCESSO, NM_ITEM_DESPESA, ";
            SQL += "CASE WHEN VL_SALDO_RECEBER > 0.00 THEN VL_SALDO_RECEBER ";
            SQL += "     WHEN VL_SALDO_PAGAR > 0.00 THEN VL_SALDO_PAGAR END AS VL_ITEM_DESPESA ";
            SQL += "FROM FN_PREVISIBILIDADE_SALDO('01/01/1900', '01/01/2900', '') WHERE NM_ITEM_DESPESA = 'COMISSAO') A ";
            SQL += "WHERE A.VL_ITEM_DESPESA IS NOT NULL)) A ";

            /*SQL = "SELECT E.NR_PROCESSO, B.NM_ITEM_DESPESA, C.NM_RAZAO as DESTINATARIO_COBRANCA, A.VL_TAXA, A.VL_TAXA_CALCULADO, ";
            SQL += "D.SIGLA_MOEDA, CASE WHEN A.CD_PR = 'P' THEN 'PAGAR' ELSE 'VENDER' END AS TIPO, F.NM_TIPO_PAGAMENTO ";
            SQL += "FROM TB_BL_TAXA A ";
            SQL += "JOIN TB_ITEM_DESPESA B ON A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON A.ID_PARCEIRO_EMPRESA = C.ID_PARCEIRO ";
            SQL += "JOIN TB_MOEDA D ON A.ID_MOEDA = D.ID_MOEDA ";
            SQL += "JOIN TB_BL E ON A.ID_BL = E.ID_BL ";
            SQL += "JOIN TB_TIPO_PAGAMENTO F ON A.ID_TIPO_PAGAMENTO = F.ID_TIPO_PAGAMENTO ";
            SQL += "WHERE A.ID_BL_TAXA NOT IN(SELECT ISNULL(ID_BL_TAXA, 0) FROM TB_CONTA_PAGAR_RECEBER_ITENS A ";
            SQL += "JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL) ";
            SQL += "AND E.ID_BL_MASTER IS NOT NULL ";
            SQL += "AND CONVERT(DATE, E.DT_ABERTURA,103) BETWEEN CONVERT(DATE, '" + sqlFormattedDate + "', 103) ";
            SQL += "AND CONVERT(DATE, '" + sqlFormattedDate2 + "', 103) ";
            SQL += "AND E.ID_BL IN(SELECT DISTINCT(A.ID_BL) FROM TB_CONTA_PAGAR_RECEBER_ITENS A ";
            SQL += "JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER ";
            SQL += "WHERE B.DT_CANCELAMENTO IS NULL AND A.ID_ITEM_DESPESA = 14 AND CD_PR = 'P') ";
            SQL += "AND A.ID_ORIGEM_PAGAMENTO = " + opt + " ";*/


            switch (filter)
            {
                case "1":
                    SQL += " WHERE DESTINATARIO_COBRANCA LIKE '" + text + "%' ORDER BY NR_PROCESSO ";
                    break;
                case "2":
					SQL += " WHERE NR_PROCESSO LIKE '" + text + "%' ORDER BY NR_PROCESSO ";
                    break;
                default:
                    SQL += " ORDER BY NR_PROCESSO ";
                    break;
            }


            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string listarProcessoFaturas(string processo, int check)
        {
            string SQL;
            DataTable listTable = new DataTable();
            if (check == 1)
            {
                SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, ";
                SQL += "M.NM_MOEDA ,ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_TAXA_DEMURRAGE_VENDA, 'c', 'pt-br')), 'R$', ''), '') AS TAXA_DEMURRAGE, FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'dd/MM/yyyy') as DT_INICIAL_DEMURRAGE, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'dd/MM/yyyy') AS DT_FINAL_DEMURRAGE,DFCL.QT_DIAS_DEMURRAGE,REPLACE(FORMAT(DFCL.VL_DEMURRAGE_VENDA,'C','PT-BR'),'R$','') AS VL_DEMURRAGE ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DEMURRAGE_VENDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.NR_PROCESSO = '" + processo + "' ";
                SQL += "AND DFCL.ID_DEMURRAGE_FATURA_RECEBER IS NULL ";
                SQL += "AND DFCL.VL_DEMURRAGE_VENDA IS NOT NULL ";
                SQL += "AND DFCL.DT_EXPORTACAO_DEMURRAGE_RECEBER IS NULL ";
                listTable = DBS.List(SQL);
            }
            else
            {
                SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, ";
                SQL += "M.NM_MOEDA ,ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_TAXA_DEMURRAGE_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS TAXA_DEMURRAGE,FORMAT(DFCL.DT_INICIAL_DEMURRAGE,'dd/MM/yyyy') as DT_INICIAL_DEMURRAGE, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DEMURRAGE,'dd/MM/yyyy') AS DT_FINAL_DEMURRAGE,ISNULL(DFCL.QT_DIAS_DEMURRAGE_COMPRA,DFCL.QT_DIAS_DEMURRAGE)QT_DIAS_DEMURRAGE,ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_DEMURRAGE_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS VL_DEMURRAGE ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DEMURRAGE_COMPRA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.NR_PROCESSO = '" + processo + "' ";
                SQL += "AND DFCL.ID_DEMURRAGE_FATURA_PAGAR IS NULL ";
                SQL += "AND DFCL.VL_DEMURRAGE_COMPRA IS NOT NULL ";
                SQL += "AND DFCL.DT_EXPORTACAO_DEMURRAGE_PAGAR IS NULL ";
                listTable = DBS.List(SQL);
            }
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string processarFatura(string processo, int check)
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
                SQL += "VALUES (" + idbl + ",'R','" + sqlFormattedDate + "','" + Session["ID_USUARIO"] + "') SELECT SCOPE_IDENTITY() AS ID_DEMURRAGE_FATURA ";
                string processarFatura = DBS.ExecuteScalar(SQL);
                return processarFatura;
            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_BL WHERE NR_PROCESSO = '" + processo + "' ";
                localizarFatura = DBS.List(SQL);
                int idbl = (int)localizarFatura.Rows[0]["ID_BL"];

                SQL = "INSERT INTO TB_DEMURRAGE_FATURA (ID_BL, CD_PR, DT_LANCAMENTO, ID_USUARIO_LANCAMENTO) ";
                SQL += "VALUES (" + idbl + ",'P','" + sqlFormattedDate + "','" + Session["ID_USUARIO"] + "') SELECT SCOPE_IDENTITY() AS ID_DEMURRAGE_FATURA ";
                string processarFatura = DBS.ExecuteScalar(SQL);
                return processarFatura;
            }

        }

        [WebMethod(EnableSession = true)]
        public void processarFaturaItens(int idcntr, int check, int fatura)
        {
            DataTable localizarFatura = new DataTable();
            string SQL;
            if (check == 1)
            {
                SQL = "select ID_CNTR_DEMURRAGE from tb_CNTR_DEMURRAGE A ";
                SQL += "join TB_AMR_CNTR_BL B on A.ID_CNTR_BL = B.ID_CNTR_BL ";
                SQL += "where b.ID_CNTR_BL = " + idcntr + " ";
                localizarFatura = DBS.List(SQL);
                int idcntrdemurrage = (int)localizarFatura.Rows[0]["ID_CNTR_DEMURRAGE"];

                SQL = "INSERT INTO TB_DEMURRAGE_FATURA_ITENS (ID_DEMURRAGE_FATURA, ID_CNTR_DEMURRAGE) ";
                SQL += "VALUES (" + fatura + ",'" + idcntrdemurrage + "') ";
                string processarFatura = DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "select ID_CNTR_DEMURRAGE from tb_CNTR_DEMURRAGE A ";
                SQL += "join TB_AMR_CNTR_BL B on A.ID_CNTR_BL = B.ID_CNTR_BL ";
                SQL += "where b.ID_CNTR_BL = " + idcntr + "";
                localizarFatura = DBS.List(SQL);
                int idcntrdemurrage = (int)localizarFatura.Rows[0]["ID_CNTR_DEMURRAGE"];

                SQL = "INSERT INTO TB_DEMURRAGE_FATURA_ITENS (ID_DEMURRAGE_FATURA, ID_CNTR_DEMURRAGE) ";
                SQL += "VALUES (" + fatura + ",'" + idcntrdemurrage + "') ";
                string processarFatura = DBS.ExecuteScalar(SQL);
            }

        }

        [WebMethod(EnableSession = true)]
        public string infoAtualizacao(int idFatura)
        {
            string SQL;
            DataTable listTable = new DataTable();

            SQL = "SELECT MIN(A.ID_DEMURRAGE_FATURA) AS ID_DEMURRAGE_FATURA , MIN(B.NR_PROCESSO) AS NR_PROCESSO, MIN(P.NM_RAZAO) as CLIENTE, ";
            SQL += "ISNULL(CONVERT(VARCHAR,MIN(MF.VL_TXOFICIAL)),'') AS VL_TAXA, ISNULL(FORMAT(MIN(DT_CAMBIO),'dd/MM/yyyy'),'') AS DT_CAMBIO, ";
            SQL += "CASE WHEN A.CD_PR = 'R' THEN SUM(D.VL_DEMURRAGE_VENDA_BR) ELSE SUM(D.VL_DEMURRAGE_COMPRA_BR) END AS VL_DEMURRAGE_TOTAL_BR ";
            SQL += "FROM TB_DEMURRAGE_FATURA A ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON B.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA_FRETE_ARMADOR MF ON B.ID_PARCEIRO_TRANSPORTADOR = MF.ID_ARMADOR ";
            SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS C ON A.ID_DEMURRAGE_FATURA = C.ID_DEMURRAGE_FATURA ";
            SQL += "LEFT JOIN TB_CNTR_DEMURRAGE D ON D.ID_CNTR_DEMURRAGE = C.ID_CNTR_DEMURRAGE ";
            SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + " ";
            SQL += "GROUP BY A.CD_PR ";

            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarFaturasAtualizacaoCambial(int idFatura, int check)
        {
            string SQL;
            DataTable listTable = new DataTable();
            if (check == 1)
            {
                SQL = "select D.ID_CNTR_BL, D.NR_CNTR, M.NM_MOEDA, REPLACE(FORMAT(E.VL_DEMURRAGE_VENDA,'C','PT-BR'),'R$','') AS VL_DEMURRAGE, ISNULL(REPLACE(FORMAT(E.VL_DESCONTO_DEMURRAGE_VENDA,'C','PT-BR'),'R$',''),0) AS DESCONTO, ISNULL(REPLACE(FORMAT(E.VL_MULTA_DEMURRAGE_VENDA,'C','PT-BR'),'R$',''),0) AS MULTA from tb_demurrage_fatura_itens a ";
                SQL += "LEFT join TB_DEMURRAGE_FATURA B ON B.ID_DEMURRAGE_FATURA = a.ID_DEMURRAGE_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DEMURRAGE E ON E.ID_CNTR_DEMURRAGE = a.ID_CNTR_DEMURRAGE ";
                SQL += "LEFT JOIN TB_CNTR_BL D ON E.ID_CNTR_BL = D.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON E.ID_MOEDA_DEMURRAGE_VENDA = M.ID_MOEDA ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + " ";
                SQL += "AND B.DT_CANCELAMENTO IS NULL ";
                listTable = DBS.List(SQL);
            }
            else
            {
                SQL = "select D.ID_CNTR_BL, D.NR_CNTR, M.NM_MOEDA, REPLACE(FORMAT(E.VL_DEMURRAGE_COMPRA,'C','PT-BR'),'R$','') AS VL_DEMURRAGE, ISNULL(REPLACE(FORMAT(E.VL_DESCONTO_DEMURRAGE_COMPRA,'C','PT-BR'),'R$',''),0) AS DESCONTO, ISNULL(REPLACE(FORMAT(E.VL_MULTA_DEMURRAGE_COMPRA,'C','PT-BR'),'R$',''),0) AS MULTA from tb_demurrage_fatura_itens a ";
                SQL += "LEFT join TB_DEMURRAGE_FATURA B ON B.ID_DEMURRAGE_FATURA = a.ID_DEMURRAGE_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DEMURRAGE E ON E.ID_CNTR_DEMURRAGE = a.ID_CNTR_DEMURRAGE ";
                SQL += "LEFT JOIN TB_CNTR_BL D ON E.ID_CNTR_BL = D.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON E.ID_MOEDA_DEMURRAGE_COMPRA = M.ID_MOEDA ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + " ";
                SQL += "AND B.DT_CANCELAMENTO IS NULL ";
                listTable = DBS.List(SQL);
            }
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string atualizacaoCambialFatura(int idFatura, string dtVencimento, string idContaBancaria)
        {
            string SQL;
            DataTable listTable = new DataTable();
            SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_VENCIMENTO = '" + dtVencimento + "', ";
            SQL += "ID_CONTA_BANCARIA = '" + idContaBancaria + "' ";
            SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
            string listTable2 = DBS.ExecuteScalar(SQL);
            return "ok";
        }

        public static double Trunc2(double Valor) // Função para truncar valores com 2 casas decimais 
        {
            int Valor_Inteiro = (int)(Valor * 100.00000000001);
            Valor = Valor_Inteiro / 100.0;
            return Valor;
        }

        [WebMethod(EnableSession = true)]
        public string atualizacaoCambialContainer(int idCntr, string vlDemurrage, string dtCambio, double vlCambio, string descontoBRL, string multaBRL, int check)
        {
            string SQL;
            DataTable listTable = new DataTable();
            string vlDemur = vlDemurrage.Replace(".", "");
            double vlDemuDolar = Convert.ToDouble(vlDemur);
            double vlDemu = Trunc2(vlDemuDolar) * vlCambio;
            string descontor = descontoBRL.Replace(".", "");
            double desconto = Convert.ToDouble(descontor);
            string multar = multaBRL.Replace(".", "");
            double multa = Convert.ToDouble(multar);
            double valorLiquido = Trunc2(vlDemu) - Trunc2(desconto) + Trunc2(multa);
            double valorDemurrageConvertido = Trunc2(vlDemu) * vlCambio;

            if (check == 1)
            {
                SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_CAMBIO_DEMURRAGE_VENDA = '" + dtCambio + "', ";
                SQL += "VL_CAMBIO_DEMURRAGE_VENDA = '" + vlCambio.ToString().Replace(",", ".") + "', VL_DESCONTO_DEMURRAGE_VENDA = '" + descontoBRL.ToString().Replace(",", ".") + "', ";
                SQL += "VL_DEMURRAGE_VENDA_BR = '" + Trunc2(vlDemu).ToString().Replace(",", ".") + "', VL_DEMURRAGE_LIQUIDO_VENDA = '" + Trunc2(valorLiquido).ToString().Replace(",", ".") + "', ";
                SQL += "VL_MULTA_DEMURRAGE_VENDA = '" + multaBRL.ToString().Replace(",", ".") + "' ";
                SQL += "WHERE ID_CNTR_BL = '" + idCntr + "' ";
                string atualizarContainer = DBS.ExecuteScalar(SQL);
                return "ok";
            }
            else
            {
                SQL = "UPDATE TB_CNTR_DEMURRAGE SET DT_CAMBIO_DEMURRAGE_COMPRA = '" + dtCambio + "', ";
                SQL += "VL_CAMBIO_DEMURRAGE_COMPRA = '" + vlCambio.ToString().Replace(",", ".") + "', VL_DESCONTO_DEMURRAGE_COMPRA = '" + descontoBRL.ToString().Replace(",", ".") + "', ";
                SQL += "VL_DEMURRAGE_COMPRA_BR = '" + Trunc2(vlDemu).ToString().Replace(",", ".") + "', VL_DEMURRAGE_LIQUIDO_COMPRA = '" + Trunc2(valorLiquido).ToString().Replace(",", ".") + "', ";
                SQL += "VL_MULTA_DEMURRAGE_COMPRA = '" + multaBRL.ToString().Replace(",", ".") + "' ";
                SQL += "WHERE ID_CNTR_BL = '" + idCntr + "' ";
                string atualizarContainer = DBS.ExecuteScalar(SQL);
                return "ok";
            }


        }

        [WebMethod(EnableSession = true)]
        public string infoAtualizacaoCambial(int idFatura, int check)
        {
            string SQL;

            if (check == 1)
            {
                SQL = "select DT_EXPORTACAO_DEMURRAGE FROM TB_DEMURRAGE_FATURA WHERE ID_DEMURRAGE_FATURA = " + idFatura + " ";
                DataTable listTableV = new DataTable();
                listTableV = DBS.List(SQL);
                if (listTableV.Rows[0]["DT_EXPORTACAO_DEMURRAGE"].ToString() != "" && listTableV.Rows[0]["DT_EXPORTACAO_DEMURRAGE"] != null)
                {
                    return "1";
                }
            }
            else
            {
                SQL = "select DT_EXPORTACAO_DEMURRAGE_COMPRA FROM TB_DEMURRAGE_FATURA WHERE ID_DEMURRAGE_FATURA = " + idFatura + " ";
                DataTable listTableC = new DataTable();
                listTableC = DBS.List(SQL);
                if (listTableC.Rows[0]["DT_EXPORTACAO_DEMURRAGE_COMPRA"].ToString() != "" && listTableC.Rows[0]["DT_EXPORTACAO_DEMURRAGE_COMPRA"] != null)
                {
                    return "1";
                }
            }
            return "0";
        }

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        public string cancelarFatura(int idFatura, string motivoCancelamento, int check)
        {
            string flagF;
            string SQL;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (check == 1)
            {
                SQL = "select * from tb_demurrage_fatura a ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + " ";
                SQL += "AND A.DT_EXPORTACAO_DEMURRAGE IS NOT NULL ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA = " + idFatura + "";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);

                if (listTable != null)
                {
                    SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"]: '3') + "', ";
                    SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE DT_COMPETENCIA = '" + idFatura + "' ";
                    string deleteContaPagarReceber = DBS.ExecuteScalar(SQL);

		    if (listTable2 != null) {
	                    for (int i = 0; i < listTable2.Rows.Count; i++)
	                    {
	                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
	                        SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_CONTA_PAGAR_RECEBER = '" + listTable2.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "' ";
	                        SQL += "UPDATE TB_FATURAMENTO SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
	                        SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_CONTA_PAGAR_RECEBER = '" + listTable2.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "' ";
	                        DBS.ExecuteScalar(SQL);
	                    }
		    }

                    SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', DT_EXPORTACAO_DEMURRAGE = NULL, ";
                    SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                    string updateFatura = DBS.ExecuteScalar(SQL);

                    SQL = "SELECT C.ID_CNTR_BL FROM TB_DEMURRAGE_FATURA A ";
                    SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS B ON B.ID_DEMURRAGE_FATURA = A.ID_DEMURRAGE_FATURA ";
                    SQL += "LEFT JOIN TB_CNTR_DEMURRAGE C ON C.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
                    SQL += "WHERE A.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                    DataTable cntr = new DataTable();
                    cntr = DBS.List(SQL);
                    string cntrbl = cntr.Rows[0]["ID_CNTR_BL"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = 1 WHERE ID_CNTR_BL = '" + cntrbl + "' ";
                    string atualizaStatus = DBS.ExecuteScalar(SQL);

                    return JsonConvert.SerializeObject("ok"); ;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                SQL = "select * from tb_demurrage_fatura a ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = " + idFatura + " ";
                SQL += "AND A.DT_EXPORTACAO_DEMURRAGE_COMPRA IS NOT NULL ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA = " + idFatura + "";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);

                if (listTable != null)
                {
                    SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
                    SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE DT_COMPETENCIA = '" + idFatura + "' ";
                    string deleteContaPagarReceber = DBS.ExecuteScalar(SQL);

		    if (listTable2 != null) {
	                    for (int i = 0; i < listTable2.Rows.Count; i++)
	                    {
	                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
	                        SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_CONTA_PAGAR_RECEBER = '" + listTable2.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "' ";
	                        SQL += "UPDATE TB_FATURAMENTO SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
	                        SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_CONTA_PAGAR_RECEBER = '" + listTable2.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "' ";
	                        DBS.ExecuteScalar(SQL);
	                    }
		    }

                    SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', DT_EXPORTACAO_DEMURRAGE_COMPRA = NULL, ";
                    SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                    string updateFatura = DBS.ExecuteScalar(SQL);

                    SQL = "SELECT C.ID_CNTR_BL FROM TB_DEMURRAGE_FATURA A ";
                    SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS B ON B.ID_DEMURRAGE_FATURA = A.ID_DEMURRAGE_FATURA ";
                    SQL += "LEFT JOIN TB_CNTR_DEMURRAGE C ON C.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
                    SQL += "WHERE A.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                    DataTable cntr = new DataTable();
                    cntr = DBS.List(SQL);
                    string cntrbl = cntr.Rows[0]["ID_CNTR_BL"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = 1 WHERE ID_CNTR_BL = '" + cntrbl + "' ";
                    string atualizaStatus = DBS.ExecuteScalar(SQL);

                    return JsonConvert.SerializeObject("ok");
                }
                else
                {
                    return null;
                }
            }
        }

        [WebMethod(EnableSession = true)]
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
        [WebMethod(EnableSession = true)]
        public string infoExportFatura(int idFatura, int check)
        {
            string SQL;
            if (check == 1)
            {
                SQL = "select b.ID_CNTR_BL, b.ID_BL, a.ID_DEMURRAGE_FATURA, c.FL_DEMURRAGE_FINALIZADA, ID_CONTA_BANCARIA,DT_EXPORTACAO_DEMURRAGE, DT_CANCELAMENTO from tb_demurrage_fatura a ";
                SQL += "inner join TB_AMR_CNTR_BL b on a.ID_BL = b.ID_BL ";
                SQL += "inner join TB_CNTR_BL c on b.ID_CNTR_BL = c.ID_CNTR_BL ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "" || listTable.Rows[0]["ID_CONTA_BANCARIA"] == null)
                {
                    return "1";
                }
                if ((listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE"].ToString() != "" && listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE"] != null) || (listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" && listTable.Rows[0]["DT_CANCELAMENTO"] != null))
                {
                    return "2";
                }
                if (listTable.Rows[0]["FL_DEMURRAGE_FINALIZADA"].ToString() == "1")
                {
                    return "3";
                }
            }
            else
            {
                SQL = "select b.ID_CNTR_BL, b.ID_BL, a.ID_DEMURRAGE_FATURA, c.FL_DEMURRAGE_FINALIZADA, ID_CONTA_BANCARIA,DT_EXPORTACAO_DEMURRAGE_COMPRA, DT_CANCELAMENTO from tb_demurrage_fatura a ";
                SQL += "inner join TB_AMR_CNTR_BL b on a.ID_BL = b.ID_BL ";
                SQL += "inner join TB_CNTR_BL c on b.ID_CNTR_BL = c.ID_CNTR_BL ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "" || listTable.Rows[0]["ID_CONTA_BANCARIA"] == null)
                {
                    return "1";
                }
                if ((listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE_COMPRA"].ToString() != "" && listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE_COMPRA"] != null) || (listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" && listTable.Rows[0]["DT_CANCELAMENTO"] != null))
                {
                    return "2";
                }
                if (listTable.Rows[0]["FL_DEMURRAGE_FINALIZADA"].ToString() == "1")
                {
                    return "3";
                }
            }
            return "0";
        }

        [WebMethod(EnableSession = true)]
        public string exportarCC(int idFatura, string dtLiquidacao, int check)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string SQL;
            int i;
            SQL = "select cd_pr,FORMAT(dt_lancamento,'yyyy-MM-dd hh:mm:ss') AS DT_LANCAMENTO, ID_USUARIO_LANCAMENTO,FORMAT(dt_vencimento,'yyyy-MM-dd') as DT_VENCIMENTO, ID_CONTA_BANCARIA, ";
            SQL += "DT_EXPORTACAO_DEMURRAGE, DT_CANCELAMENTO ";
            SQL += "from tb_demurrage_fatura ";
            SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "")
            {
                return "null";
            }
            if (listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE"].ToString() != "" || listTable.Rows[0]["DT_CANCELAMENTO"] == null || listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" || listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE"] == null)
            {
                return "null";
            }
            string cdpr = listTable.Rows[0]["cd_pr"].ToString();
            string dtLancamento = listTable.Rows[0]["dt_lancamento"].ToString();
            int idUsuario = (int)listTable.Rows[0]["ID_USUARIO_LANCAMENTO"];
            string dtVencimento = listTable.Rows[0]["DT_VENCIMENTO"].ToString();
            int idConta = (int)listTable.Rows[0]["ID_CONTA_BANCARIA"];
            string flagF;

            if (check == 1)
            {
                SQL = "SELECT ID_BL FROM TB_DEMURRAGE_FATURA ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                //SQL = "SELECT D.id_cntr_bl FROM TB_DEMURRAGE_FATURA a ";
                //SQL += "join TB_AMR_CNTR_BL b on a.ID_BL = b.ID_BL ";
                //SQL += "JOIN TB_BL C ON C.ID_BL = b.ID_BL ";
                //SQL += "JOIN TB_CNTR_BL D ON b.ID_CNTR_BL = D.ID_CNTR_BL ";
                //SQL += "WHERE A.ID_BL = '"+idbl+"' AND D.FL_DEMURRAGE_FINALIZADA = 0 ";               

                //DataTable listarContainers = new DataTable();
                //listarContainers = DBS.List(SQL);
                //int qtdRows = listarContainers.Rows.Count;
                //int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA ";
                SQL += ",ID_USUARIO_LANCAMENTO,DT_LIQUIDACAO,ID_USUARIO_LIQUIDACAO,CD_PR,DT_COMPETENCIA ";
                SQL += ",TP_EXPORTACAO) VALUES('" + dtLancamento + "','" + dtVencimento + "','" + idConta + "', ";
                SQL += "'" + idUsuario + "','" + dtLiquidacao + "','" + Session["ID_USUARIO"] + "','" + cdpr + "','" + idFatura + "','DEM') SELECT SCOPE_IDENTITY()";
                string insertConta = DBS.ExecuteScalar(SQL);

                SQL = "SELECT C.ID_CNTR_BL,C.ID_MOEDA_DEMURRAGE_VENDA, C.VL_DEMURRAGE_VENDA, D.ID_PARCEIRO_CLIENTE, ";
                SQL += "FORMAT(C.DT_CAMBIO_DEMURRAGE_VENDA,'yyyy-MM-dd') AS DT_CAMBIO_DEMURRAGE_VENDA, ";
                SQL += "C.VL_CAMBIO_DEMURRAGE_VENDA, C.VL_DEMURRAGE_VENDA_BR , ";
                SQL += "C.VL_DESCONTO_DEMURRAGE_VENDA, C.VL_MULTA_DEMURRAGE_VENDA, C.VL_DEMURRAGE_LIQUIDO_VENDA ";
                SQL += "FROM TB_DEMURRAGE_FATURA A ";
                SQL += "INNER JOIN TB_DEMURRAGE_FATURA_ITENS B ON A.ID_DEMURRAGE_FATURA = B.ID_DEMURRAGE_FATURA ";
                SQL += "INNER JOIN TB_CNTR_DEMURRAGE C ON B.ID_CNTR_DEMURRAGE = C.ID_CNTR_DEMURRAGE ";
                SQL += "LEFT  JOIN TB_BL D ON A.ID_BL = D.ID_BL ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA =  '" + idFatura + "' ";

                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];



                for (i = 0; i < qtdRows; i++)
                {
                    //SQL = "SELECT ID_MOEDA_DEMURRAGE_VENDA,VL_DEMURRAGE_VENDA ";
                    //SQL += ",ID_PARCEIRO_CLIENTE,FORMAT(DT_CAMBIO_DEMURRAGE_VENDA,'yyyy-MM-dd') AS DT_CAMBIO_DEMURRAGE_VENDA ";
                    //SQL += ",VL_CAMBIO_DEMURRAGE_VENDA,VL_DEMURRAGE_VENDA_BR ";
                    //SQL += ",VL_DESCONTO_DEMURRAGE_VENDA,VL_DEMURRAGE_LIQUIDO_VENDA ";
                    //SQL += "FROM TB_CNTR_DEMURRAGE A ";
                    //SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
                    //SQL += "LEFT JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
                    //SQL += "WHERE A.ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "'";
                    //DataTable vlDemurrage = new DataTable();
                    //vlDemurrage = DBS.List(SQL);

                    string idMoedaVenda = listarContainers.Rows[i]["ID_MOEDA_DEMURRAGE_VENDA"].ToString();
                    string vlDemurrageVenda = listarContainers.Rows[i]["VL_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    int parceiroCliente = (int)listarContainers.Rows[i]["ID_PARCEIRO_CLIENTE"];
                    string dtCambioVenda = listarContainers.Rows[i]["DT_CAMBIO_DEMURRAGE_VENDA"].ToString();
                    string vlCambioDemuVenda = listarContainers.Rows[i]["VL_CAMBIO_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    string vlDemuVendaBR = listarContainers.Rows[i]["VL_DEMURRAGE_VENDA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuVenda = listarContainers.Rows[i]["VL_DESCONTO_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    string vlMultaDemuVenda = listarContainers.Rows[i]["VL_MULTA_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    string vlDemuLiquidVenda = listarContainers.Rows[i]["VL_DEMURRAGE_LIQUIDO_VENDA"].ToString().Replace(",", ".");

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
                    SQL += "ID_MOEDA, ID_PARCEIRO_EMPRESA, VL_TAXA_CALCULADO, DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_DESCONTO, VL_MULTA, VL_LIQUIDO, FL_INTEGRA_PA) VALUES ";
                    SQL += "('" + insertConta + "','" + idbl + "','" + idItemD + "','1','" + idMoedaVenda + "', ";
                    SQL += "'" + parceiroCliente + "','" + vlDemurrageVenda + "','" + dtCambioVenda + "','" + vlCambioDemuVenda + "','" + vlDemuVendaBR + "' ";
                    SQL += ",'" + vlDescDemuVenda + "', '"+vlMultaDemuVenda+ "','" + vlDemuLiquidVenda + "','" + flIntegraPA + "') ";
                    string insertContaPGI = DBS.ExecuteScalar(SQL);

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = 10 WHERE ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "' ";
                    string updtDsStatus = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_EXPORTACAO_DEMURRAGE = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DEMURRAGE = '" + Session["ID_USUARIO"] + "', ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                string updtDemurrageFatura = DBS.ExecuteScalar(SQL);

                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + cntrBl + "' ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                
            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_DEMURRAGE_FATURA ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "SELECT * FROM TB_DEMURRAGE_FATURA A ";
                SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS B ON B.ID_DEMURRAGE_FATURA = A.ID_DEMURRAGE_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DEMURRAGE C ON C.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA ";
                SQL += ",ID_USUARIO_LANCAMENTO,DT_LIQUIDACAO,ID_USUARIO_LIQUIDACAO,CD_PR,DT_COMPETENCIA ";
                SQL += ",TP_EXPORTACAO) VALUES('" + dtLancamento + "','" + dtVencimento + "','" + idConta + "', ";
                SQL += "'" + idUsuario + "','" + dtLiquidacao + "','" + Session["ID_USUARIO"] + "','" + cdpr + "','" + idFatura + "','DEM') SELECT SCOPE_IDENTITY() ";
                string insertConta = DBS.ExecuteScalar(SQL);

                for (i = 0; i < qtdRows; i++)
                {
                    SQL = "SELECT ID_MOEDA_DEMURRAGE_COMPRA,VL_DEMURRAGE_COMPRA ";
                    SQL += ",ID_PARCEIRO_TRANSPORTADOR,FORMAT(DT_CAMBIO_DEMURRAGE_COMPRA,'yyyy-MM-dd') AS DT_CAMBIO_DEMURRAGE_COMPRA ";
                    SQL += ",VL_CAMBIO_DEMURRAGE_COMPRA,VL_DEMURRAGE_COMPRA_BR ";
                    SQL += ",VL_DESCONTO_DEMURRAGE_COMPRA, VL_MULTA_DEMURRAGE_COMPRA, VL_DEMURRAGE_LIQUIDO_COMPRA ";
                    SQL += "FROM TB_CNTR_DEMURRAGE A ";
                    SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
                    SQL += "WHERE A.ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "'";
                    DataTable vlDemurrage = new DataTable();
                    vlDemurrage = DBS.List(SQL);
                    int idMoedaCompra = (int)vlDemurrage.Rows[0]["ID_MOEDA_DEMURRAGE_COMPRA"];
                    string vlDemurrageCompra = vlDemurrage.Rows[0]["VL_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");
                    int parceiroTransportador = (int)vlDemurrage.Rows[0]["ID_PARCEIRO_TRANSPORTADOR"];
                    string dtCambioCompra = vlDemurrage.Rows[0]["DT_CAMBIO_DEMURRAGE_COMPRA"].ToString();
                    string vlCambioDemuCompra = vlDemurrage.Rows[0]["VL_CAMBIO_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");
                    string vlDemuCompraBR = vlDemurrage.Rows[0]["VL_DEMURRAGE_COMPRA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuCompra = vlDemurrage.Rows[0]["VL_DESCONTO_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");
                    string vlDemuLiquidCompra = vlDemurrage.Rows[0]["VL_DEMURRAGE_LIQUIDO_COMPRA"].ToString().Replace(",", ".");
                    string vlMultaDemuCompra = vlDemurrage.Rows[0]["VL_MULTA_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");

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
                    SQL += "ID_MOEDA, ID_PARCEIRO_EMPRESA, VL_TAXA_CALCULADO, DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_DESCONTO, VL_MULTA, VL_LIQUIDO, FL_INTEGRA_PA) VALUES ";
                    SQL += "('" + insertConta + "','" + idbl + "','" + idItemD + "','1','" + idMoedaCompra + "', ";
                    SQL += "'" + parceiroTransportador + "','" + vlDemurrageCompra + "','" + dtCambioCompra + "','" + vlCambioDemuCompra + "','" + vlDemuCompraBR + "' ";
                    SQL += ",'" + vlDescDemuCompra + "', '"+vlMultaDemuCompra+"', '" + vlDemuLiquidCompra + "','" + flIntegraPA + "') ";
                    string insertContaPGI = DBS.ExecuteScalar(SQL);

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE_COMPRA = 10 WHERE ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "' ";
                    string updtDsStatus = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_EXPORTACAO_DEMURRAGE_COMPRA = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DEMURRAGE = '" + Session["ID_USUARIO"] + "', ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                string updtDemurrageFatura = DBS.ExecuteScalar(SQL);

                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + cntrBl + "' ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                
            }
            return JsonConvert.SerializeObject("OK");
        }

        [WebMethod(EnableSession = true)]
        public string RegistrarTaxa(int idFatura, int idFaturaParcela, int check)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string SQL;
            int i;
            SQL = "select cd_pr,FORMAT(dt_lancamento,'yyyy-MM-dd hh:mm:ss') AS DT_LANCAMENTO, ID_USUARIO_LANCAMENTO,FORMAT(dt_vencimento,'yyyy-MM-dd') as DT_VENCIMENTO, ID_CONTA_BANCARIA, ";
            SQL += "DT_EXPORTACAO_DEMURRAGE, DT_CANCELAMENTO ";
            SQL += "from tb_demurrage_fatura ";
            SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "")
            {
                return "null";
            }
            if (listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE"].ToString() != "" || listTable.Rows[0]["DT_CANCELAMENTO"] == null || listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" || listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE"] == null)
            {
                return "null";
            }


            SQL = "SELECT ID_DEMURRAGE_FATURA_PARCELAS, ID_DEMURRAGE_FATURA, VL_DEMURRAGE_PARCELA, FORMAT(DT_VENCIMENTO_DEMURRAGE_PARCELA,'yyyy-MM-dd') as DT_VENCIMENTO_DEMURRAGE_PARCELA, ISNULL(VL_DEMURRAGE_PARCELA_JUROS,0.00) VL_DEMURRAGE_PARCELA_JUROS, ";
            SQL += "FL_PAGO, ISNULL(ID_CONTA_PAGAR_RECEBER,0) AS IDCPR FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idFaturaParcela + "";

            DataTable listaParcela = new DataTable();
            listaParcela = DBS.List(SQL);




            string cdpr = listTable.Rows[0]["cd_pr"].ToString();
            string dtLancamento = listTable.Rows[0]["dt_lancamento"].ToString();
            int idUsuario = (int)listTable.Rows[0]["ID_USUARIO_LANCAMENTO"];
            string dtVencimento = listaParcela.Rows[0]["DT_VENCIMENTO_DEMURRAGE_PARCELA"].ToString();
            int idConta = (int)listTable.Rows[0]["ID_CONTA_BANCARIA"];
            int idCPR = (int)listaParcela.Rows[0]["IDCPR"];
            string flagF;

            if (idCPR > 0) { return JsonConvert.SerializeObject("2"); }

            if (check == 1)
            {
                SQL = "SELECT ID_BL FROM TB_DEMURRAGE_FATURA ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA ";
                SQL += ",ID_USUARIO_LANCAMENTO,CD_PR,DT_COMPETENCIA ";
                SQL += ",TP_EXPORTACAO) VALUES('" + dtLancamento + "','" + dtVencimento + "','" + idConta + "', ";
                SQL += "'" + idUsuario + "','" + cdpr + "','" + idFaturaParcela + "','DEM') SELECT SCOPE_IDENTITY()";
                string insertConta = DBS.ExecuteScalar(SQL);

                SQL = "SELECT C.ID_CNTR_BL,C.ID_MOEDA_DEMURRAGE_VENDA, C.VL_DEMURRAGE_VENDA, D.ID_PARCEIRO_CLIENTE, ";
                SQL += "FORMAT(C.DT_CAMBIO_DEMURRAGE_VENDA,'yyyy-MM-dd') AS DT_CAMBIO_DEMURRAGE_VENDA, ";
                SQL += "C.VL_CAMBIO_DEMURRAGE_VENDA, C.VL_DEMURRAGE_VENDA_BR , ";
                SQL += "C.VL_DESCONTO_DEMURRAGE_VENDA, C.VL_DEMURRAGE_LIQUIDO_VENDA ";
                SQL += "FROM TB_DEMURRAGE_FATURA A ";
                SQL += "INNER JOIN TB_DEMURRAGE_FATURA_ITENS B ON A.ID_DEMURRAGE_FATURA = B.ID_DEMURRAGE_FATURA ";
                SQL += "INNER JOIN TB_CNTR_DEMURRAGE C ON B.ID_CNTR_DEMURRAGE = C.ID_CNTR_DEMURRAGE ";
                SQL += "LEFT  JOIN TB_BL D ON A.ID_BL = D.ID_BL ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA =  '" + idFatura + "' ";

                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];



                double vlParcela = (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA"]) + (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA"]) * (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA_JUROS"]) / 100))) / qtdRows;


                for (i = 0; i < qtdRows; i++)
                {
                    //string idMoedaVenda = listarContainers.Rows[i]["ID_MOEDA_DEMURRAGE_VENDA"].ToString();
                    //string vlDemurrageVenda = listarContainers.Rows[i]["VL_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    int parceiroCliente = (int)listarContainers.Rows[i]["ID_PARCEIRO_CLIENTE"];
                    string dtCambioVenda = listarContainers.Rows[i]["DT_CAMBIO_DEMURRAGE_VENDA"].ToString();
                    string vlCambioDemuVenda = listarContainers.Rows[i]["VL_CAMBIO_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    string vlDemuVendaBR = listarContainers.Rows[i]["VL_DEMURRAGE_VENDA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuVenda = listarContainers.Rows[i]["VL_DESCONTO_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    string vlDemuLiquidVenda = listarContainers.Rows[i]["VL_DEMURRAGE_LIQUIDO_VENDA"].ToString().Replace(",", ".");

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
                    SQL += "('" + insertConta + "','" + idbl + "','" + idItemD + "','1',124, ";
                    SQL += "'" + parceiroCliente + "','" + vlParcela.ToString().Replace(",", ".") + "','" + dtCambioVenda + "','" + vlCambioDemuVenda + "','" + vlParcela.ToString().Replace(",", ".") + "' ";
                    SQL += ",NULL,'" + vlParcela.ToString().Replace(",", ".") + "','" + flIntegraPA + "') ";
                    string insertContaPGI = DBS.ExecuteScalar(SQL);
                }

                SQL = "UPDATE TB_DEMURRAGE_FATURA_PARCELAS SET ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DEMURRAGE_FATURA_PARCELAS = '" + idFaturaParcela + "' ";
                DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_DEMURRAGE_FATURA ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "SELECT * FROM TB_DEMURRAGE_FATURA A ";
                SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS B ON B.ID_DEMURRAGE_FATURA = A.ID_DEMURRAGE_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DEMURRAGE C ON C.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                double vlParcela = (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA"]) + (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA"]) * (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA_JUROS"]) / 100))) / qtdRows;

                SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA ";
                SQL += ",ID_USUARIO_LANCAMENTO,CD_PR,DT_COMPETENCIA ";
                SQL += ",TP_EXPORTACAO) VALUES('" + dtLancamento + "','" + dtVencimento + "','" + idConta + "', ";
                SQL += "'" + idUsuario + "','" + cdpr + "','" + idFatura + "','DEM') SELECT SCOPE_IDENTITY() ";
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
                    string vlDemurrageCompra = vlDemurrage.Rows[0]["VL_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");
                    int parceiroTransportador = (int)vlDemurrage.Rows[0]["ID_PARCEIRO_TRANSPORTADOR"];
                    string dtCambioCompra = vlDemurrage.Rows[0]["DT_CAMBIO_DEMURRAGE_COMPRA"].ToString();
                    string vlCambioDemuCompra = vlDemurrage.Rows[0]["VL_CAMBIO_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");
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
                    SQL += "('" + insertConta + "','" + idbl + "','" + idItemD + "','1',124, ";
                    SQL += "'" + parceiroTransportador + "','" + vlParcela.ToString().Replace(",", ".") + "','" + dtCambioCompra + "','" + vlCambioDemuCompra + "','" + vlParcela.ToString().Replace(",", ".") + "' ";
                    SQL += ",NULL,'" + vlParcela.ToString().Replace(",", ".") + "','" + flIntegraPA + "') ";
                    string insertContaPGI = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DEMURRAGE_FATURA_PARCELAS SET ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DEMURRAGE_FATURA_PARCELAS = '" + idFaturaParcela + "' ";
                DBS.ExecuteScalar(SQL);
            }

            return JsonConvert.SerializeObject("OK");
        }

        [WebMethod(EnableSession = true)]
        public string exportarParcelaCC(int idFatura, int idFaturaParcela, string dtLiquidacao, int check)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string SQL;
            int i;
            SQL = "select cd_pr,FORMAT(dt_lancamento,'yyyy-MM-dd hh:mm:ss') AS DT_LANCAMENTO, ID_USUARIO_LANCAMENTO,FORMAT(dt_vencimento,'yyyy-MM-dd') as DT_VENCIMENTO, ID_CONTA_BANCARIA, ";
            SQL += "DT_EXPORTACAO_DEMURRAGE, DT_CANCELAMENTO ";
            SQL += "from tb_demurrage_fatura ";
            SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "")
            {
                return "null";
            }
            if (listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE"].ToString() != "" || listTable.Rows[0]["DT_CANCELAMENTO"] == null || listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" || listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE"] == null)
            {
                return "null";
            }
            

            SQL = "SELECT ID_DEMURRAGE_FATURA_PARCELAS, ID_DEMURRAGE_FATURA, VL_DEMURRAGE_PARCELA, FORMAT(DT_VENCIMENTO_DEMURRAGE_PARCELA,'yyyy-MM-dd') as DT_VENCIMENTO_DEMURRAGE_PARCELA, ISNULL(VL_DEMURRAGE_PARCELA_JUROS,0.00) VL_DEMURRAGE_PARCELA_JUROS, ";
            SQL += "FL_PAGO, ISNULL(ID_CONTA_PAGAR_RECEBER,0) AS IDCPR FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idFaturaParcela + "";

            DataTable listaParcela = new DataTable();
            listaParcela = DBS.List(SQL);




            string cdpr = listTable.Rows[0]["cd_pr"].ToString();
            string dtLancamento = listTable.Rows[0]["dt_lancamento"].ToString();
            int idUsuario = (int)listTable.Rows[0]["ID_USUARIO_LANCAMENTO"];
            string dtVencimento = listaParcela.Rows[0]["DT_VENCIMENTO_DEMURRAGE_PARCELA"].ToString();
            int idConta = (int)listTable.Rows[0]["ID_CONTA_BANCARIA"];
            int idCPR = (int)listaParcela.Rows[0]["IDCPR"];
            string flagF;

            if (check == 1)
            {
                SQL = "SELECT ID_BL FROM TB_DEMURRAGE_FATURA ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_LIQUIDACAO = '" + dtLiquidacao + "',ID_USUARIO_LIQUIDACAO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "' WHERE ID_CONTA_PAGAR_RECEBER = "+idCPR+" ";
                string insertConta = DBS.ExecuteScalar(SQL);

                SQL = "SELECT C.ID_CNTR_BL,C.ID_MOEDA_DEMURRAGE_VENDA, C.VL_DEMURRAGE_VENDA, D.ID_PARCEIRO_CLIENTE, ";
                SQL += "FORMAT(C.DT_CAMBIO_DEMURRAGE_VENDA,'yyyy-MM-dd') AS DT_CAMBIO_DEMURRAGE_VENDA, ";
                SQL += "C.VL_CAMBIO_DEMURRAGE_VENDA, C.VL_DEMURRAGE_VENDA_BR , ";
                SQL += "C.VL_DESCONTO_DEMURRAGE_VENDA, C.VL_DEMURRAGE_LIQUIDO_VENDA ";
                SQL += "FROM TB_DEMURRAGE_FATURA A ";
                SQL += "INNER JOIN TB_DEMURRAGE_FATURA_ITENS B ON A.ID_DEMURRAGE_FATURA = B.ID_DEMURRAGE_FATURA ";
                SQL += "INNER JOIN TB_CNTR_DEMURRAGE C ON B.ID_CNTR_DEMURRAGE = C.ID_CNTR_DEMURRAGE ";
                SQL += "LEFT  JOIN TB_BL D ON A.ID_BL = D.ID_BL ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA =  '" + idFatura + "' ";

                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                

                double vlParcela = (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA"]) + (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA"]) * (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA_JUROS"]) / 100)))/ qtdRows;
                

                for (i = 0; i < qtdRows; i++)
                {
                    //string idMoedaVenda = listarContainers.Rows[i]["ID_MOEDA_DEMURRAGE_VENDA"].ToString();
                    //string vlDemurrageVenda = listarContainers.Rows[i]["VL_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    int parceiroCliente = (int)listarContainers.Rows[i]["ID_PARCEIRO_CLIENTE"];
                    string dtCambioVenda = listarContainers.Rows[i]["DT_CAMBIO_DEMURRAGE_VENDA"].ToString();
                    string vlCambioDemuVenda = listarContainers.Rows[i]["VL_CAMBIO_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    string vlDemuVendaBR = listarContainers.Rows[i]["VL_DEMURRAGE_VENDA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuVenda = listarContainers.Rows[i]["VL_DESCONTO_DEMURRAGE_VENDA"].ToString().Replace(",", ".");
                    string vlDemuLiquidVenda = listarContainers.Rows[i]["VL_DEMURRAGE_LIQUIDO_VENDA"].ToString().Replace(",", ".");

                    SQL = "SELECT ID_ITEM_DEMURRAGE FROM TB_PARAMETROS ";
                    DataTable idItemDespesa = new DataTable();
                    idItemDespesa = DBS.List(SQL);
                    int idItemD = (int)idItemDespesa.Rows[0]["ID_ITEM_DEMURRAGE"];

                    SQL = "SELECT FL_INTEGRA_PA FROM TB_ITEM_DESPESA ";
                    SQL += "WHERE ID_ITEM_DESPESA = '" + idItemD + "' ";
                    DataTable flIntegra = new DataTable();
                    flIntegra = DBS.List(SQL);
                    string flIntegraPA = flIntegra.Rows[0]["FL_INTEGRA_PA"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = 10 WHERE ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "' ";
                    string updtDsStatus = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DEMURRAGE_FATURA_PARCELAS SET DT_EXPORTACAO_PARCELA = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DEMURRAGE_PARCELA ='" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "' ";
                SQL += "WHERE ID_DEMURRAGE_FATURA_PARCELAS = '" + idFaturaParcela + "' ";
                string updtDemurrageFatura = DBS.ExecuteScalar(SQL);

                SQL = "UPDATE TB_DEMURRAGE_FATURA_PARCELAS SET FL_PAGO = 1 WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idFaturaParcela + " ";

                DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_DEMURRAGE_FATURA ";
                SQL += "WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "SELECT * FROM TB_DEMURRAGE_FATURA A ";
                SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS B ON B.ID_DEMURRAGE_FATURA = A.ID_DEMURRAGE_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DEMURRAGE C ON C.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                double vlParcela = (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA"]) + (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA"]) * (Convert.ToDouble(listaParcela.Rows[0]["VL_DEMURRAGE_PARCELA_JUROS"]) / 100))) / qtdRows;

                SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_LIQUIDACAO = '" + dtLiquidacao + "',ID_USUARIO_LIQUIDACAO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "' WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                DBS.ExecuteScalar(SQL);

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
                    string vlDemurrageCompra = vlDemurrage.Rows[0]["VL_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");
                    int parceiroTransportador = (int)vlDemurrage.Rows[0]["ID_PARCEIRO_TRANSPORTADOR"];
                    string dtCambioCompra = vlDemurrage.Rows[0]["DT_CAMBIO_DEMURRAGE_COMPRA"].ToString();
                    string vlCambioDemuCompra = vlDemurrage.Rows[0]["VL_CAMBIO_DEMURRAGE_COMPRA"].ToString().Replace(",", ".");
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

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE_COMPRA = 10 WHERE ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "' ";
                    string updtDsStatus = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DEMURRAGE_FATURA_PARCELA SET DT_EXPORTACAO_PARCELA = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DEMURRAGE_PARCELA = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "' ";
                SQL += "WHERE ID_DEMURRAGE_FATURA_PARCELAS = '" + idFaturaParcela + "' ";
                string updtDemurrageFatura = DBS.ExecuteScalar(SQL);

                SQL = "UPDATE TB_DEMURRAGE_FATURA_PARCELAS SET FL_PAGO = 1 WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idFaturaParcela + " ";

                DBS.ExecuteScalar(SQL);

            }

            return JsonConvert.SerializeObject("OK");
        }


        [WebMethod(EnableSession = true)]
        public string finalizarFaturaDemurrage(int idFatura, int check)
		{
            string SQL;
            
            if (check == 1)
            {
                SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_EXPORTACAO_DEMURRAGE = GETDATE() WHERE ID_DEMURRAGE_FATURA = " + idFatura + " ";
                DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "UPDATE TB_DEMURRAGE_FATURA SET DT_EXPORTACAO_DEMURRAGE_COMPRA = GETDATE() WHERE ID_DEMURRAGE_FATURA = " + idFatura + " ";
                DBS.ExecuteScalar(SQL);
            }

            return JsonConvert.SerializeObject("OK");
        }

        [WebMethod(EnableSession = true)]
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

            SQL = "SELECT TOP 1 ISNULL(CLIENTE.NM_RAZAO,'') AS NM_RAZAO, ISNULL(CLIENTE.ENDERECO,'') AS ENDERECO, ISNULL(CLIENTE.NR_ENDERECO,'') AS NR_ENDERECO, ";
            SQL += "ISNULL(CIDADE.NM_CIDADE,'') AS NM_CIDADE, ISNULL(CLIENTE.BAIRRO,'') AS BAIRRO, ISNULL(ESTADO.NM_ESTADO,'') AS NM_ESTADO, ISNULL(CLIENTE.CEP,'') AS CEP, ";
            SQL += "ISNULL(CLIENTE.CNPJ,'') AS CNPJ, ISNULL(CLIENTE.INSCR_ESTADUAL,'') AS INSCR_ESTADUAL, ISNULL(A.NR_PROCESSO,'') AS NR_PROCESSO, ISNULL(SERV.NM_SERVICO,'') AS NM_SERVICO, ";
            SQL += "ISNULL(ORIGEM.NM_PORTO,'') AS ORIGEM, ISNULL(DESTINO.NM_PORTO,'') AS DESTINO, ISNULL(FORMAT(BL.DT_EMBARQUE,'dd/MM/yyyy'),'') AS DT_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ISNULL(NAV.NM_NAVIO,'') AS NAVIO, ISNULL(M.NR_BL,'') AS MASTER, ISNULL(BL.NR_BL,'') AS HOUSE, ";
            SQL += "ISNULL(TRANSPORTADOR.NM_RAZAO,'') AS TRANSPORTADOR, ISNULL(CONVERT(VARCHAR,BL.VL_PESO_BRUTO),'') AS VL_PESO_BRUTO, ISNULL(CONVERT(VARCHAR,BL.VL_M3),'') AS VL_M3, ISNULL(CONVERT(VARCHAR,BL.VL_INDICE_VOLUMETRICO),'') AS VL_INDICE_VOLUMETRICO ";
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

        [WebMethod(EnableSession = true)]
        public string imprimirDadosFatura(string idFatura)
        {
            string SQL;
            SQL = "SELECT ISNULL(A.DT_CANCELAMENTO,'') AS DT_CANCELAMENTO, ISNULL(P1.NM_RAZAO,'') AS CLIENTE, ISNULL(P1.ENDERECO,'') AS ENDERECO, ISNULL(P1.NR_ENDERECO,'') AS NR_ENDERECO, ISNULL(C.NM_CIDADE,'') AS NM_CIDADE, ISNULL(FORMAT(A.DT_LANCAMENTO,'dd/MM/yy'),'') AS DT_LANCAMENTO, ISNULL(FORMAT(A.DT_VENCIMENTO,'dd/MM/yy'),'') AS DT_VENCIMENTO, ";
            SQL += "ISNULL(P1.BAIRRO,'') AS BAIRRO, ISNULL(E.NM_ESTADO,'') AS NM_ESTADO, ISNULL(P1.CEP,'') AS CEP, ISNULL(P1.CNPJ,'') AS CNPJ, ISNULL(P1.INSCR_ESTADUAL,'') AS INSCR_ESTADUAL, ISNULL(B.NR_PROCESSO,'') AS NR_PROCESSO, (CASE WHEN S.TP_SERVICO = 'IMP' THEN ISNULL(P2.NM_RAZAO,'') ELSE '' END) AS TRANSPORTADOR, ";
            SQL += "ISNULL(S.NM_SERVICO,'') AS NM_SERVICO, ISNULL(ORIGEM.NM_PORTO,'') AS ORIGEM, ISNULL(DESTINO.NM_PORTO,'') as DESTINO, ISNULL(FORMAT(B.DT_EMBARQUE, 'dd/MM/yyyy'),'') as DT_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(B.DT_CHEGADA, 'dd/MM/yyyy'),'') AS DT_CHEGADA, isnull(CONVERT(VARCHAR,B.VL_PESO_BRUTO),'') as VL_PESO_BRUTO, isnull(CONVERT(VARCHAR,B.VL_M3),'') AS VL_M3, ISNULL(CONVERT(VARCHAR,B.VL_INDICE_VOLUMETRICO),'') AS VL_INDICE_VOLUMETRICO, ";
            SQL += "ISNULL(N.NM_NAVIO,'') AS NAVIO, ISNULL(M.NR_BL,'') AS MASTER, ISNULL(B.NR_BL,'') AS HOUSE ";
            SQL += "from TB_DEMURRAGE_FATURA A ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P1 ON B.ID_PARCEIRO_IMPORTADOR = P1.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON B.ID_PARCEIRO_EXPORTADOR = P2.ID_PARCEIRO ";
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

        [WebMethod(EnableSession = true)]
        public string listarContainerFaturaPrintVenda(string idFatura)
        {
            string SQL;
            /*SQL = "SELECT A.NR_CNTR, A.NM_TIPO_CONTAINER, FORMAT(B.DT_INICIAL_FREETIME,'dd/MM/yy') AS INICIALFT, ";
            SQL += "FORMAT(B.DT_FINAL_FREETIME,'dd/MM/yy') AS FINALFT,A.QT_DIAS_FREETIME, ";
            SQL += "FORMAT(B.DT_INICIAL_DEMURRAGE,'dd/MM/yy') AS INICIALDEM, FORMAT(B.DT_FINAL_DEMURRAGE,'dd/MM/yy') AS FINALDEM, ";
            SQL += "B.QT_DIAS_DEMURRAGE, MD.SIGLA_MOEDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DEMURRAGE_VENDA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(B.VL_DEMURRAGE_VENDA, 'C', 'PT-BR')), 'R$', ''),'') AS VL_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(B.VL_DESCONTO_DEMURRAGE_VENDA,'C','PT-BR')),'R$',''),'') AS VL_DESCONTO_DEMURRAGE_VENDA ";
            SQL += "FROM TB_DEMURRAGE_FATURA_ITENS DFI ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL B ON DFI.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL A ON B.ID_CNTR_BL = A.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_DEMURRAGE_FATURA DF ON DF.ID_DEMURRAGE_FATURA = DFI.ID_DEMURRAGE_FATURA ";
            SQL += "LEFT JOIN TB_MOEDA MD ON B.ID_MOEDA_DEMURRAGE_VENDA = MD.ID_MOEDA ";
            SQL += "WHERE DFI.ID_DEMURRAGE_FATURA = '"+idFatura+"' ";
            SQL += "AND DF.CD_PR = 'R' ";
            SQL += "AND DF.DT_CANCELAMENTO IS NULL ";*/
            SQL = "SELECT DISTINCT ISNULL(A.NR_CNTR,'') AS NR_CNTR, ISNULL(A.NM_TIPO_CONTAINER,'') AS NM_TIPO_CONTAINER, ISNULL(FORMAT(B.DT_INICIAL_FREETIME,'dd/MM/yy'),'') AS INICIALFT, ";
            SQL += "ISNULL(FORMAT(B.DT_FINAL_FREETIME,'dd/MM/yy'),'') AS FINALFT,ISNULL(A.QT_DIAS_FREETIME,'') AS QT_DIAS_FREETIME, ";
            SQL += "ISNULL(FORMAT(B.DT_INICIAL_DEMURRAGE,'dd/MM/yy'),'') AS INICIALDEM, ISNULL(FORMAT(B.DT_FINAL_DEMURRAGE,'dd/MM/yy'),'') AS FINALDEM, ";
            SQL += "ISNULL(B.QT_DIAS_DEMURRAGE,'') AS QT_DIAS_DEMURRAGE, ISNULL(MD.SIGLA_MOEDA,'') AS SIGLA_MOEDA, ";
            SQL += "ISNULL(B.VL_TAXA_DEMURRAGE_VENDA,0) AS VL_TAXA_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(B.VL_CAMBIO_DEMURRAGE_VENDA,0) AS VL_CAMBIO_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(B.VL_DEMURRAGE_LIQUIDO_VENDA,0) AS VL_DEMURRAGE_LIQUIDO_VENDA, ";
            SQL += "ISNULL(CONVERT(DECIMAL(13,2),B.VL_DESCONTO_DEMURRAGE_VENDA) * -1 + CONVERT(DECIMAL(13,2),B.VL_MULTA_DEMURRAGE_VENDA),0) AS VL_DESCONTO_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(B.VL_DEMURRAGE_VENDA_BR,0) AS VL_DEMURRAGE_VENDA_BR ";
            SQL += "FROM TB_DEMURRAGE_FATURA_ITENS DFI ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL B ON DFI.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL A ON B.ID_CNTR_BL = A.ID_CNTR_BL AND B.ID_BL = A.ID_BL ";
            SQL += "LEFT JOIN TB_DEMURRAGE_FATURA DF ON DF.ID_DEMURRAGE_FATURA = DFI.ID_DEMURRAGE_FATURA ";
            SQL += "LEFT JOIN TB_MOEDA MD ON B.ID_MOEDA_DEMURRAGE_VENDA = MD.ID_MOEDA ";
            SQL += "WHERE DFI.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
            SQL += "AND DF.CD_PR = 'R' ";

            DataTable imprimirDados = new DataTable();
            imprimirDados = DBS.List(SQL);
            return JsonConvert.SerializeObject(imprimirDados);
        }

        [WebMethod(EnableSession = true)]
        public string listarContainerFaturaPrintCompra(string idFatura)
        {
            string SQL;
            SQL = "SELECT DISTINCT ISNULL(A.NR_CNTR,'') AS NR_CNTR, ISNULL(A.NM_TIPO_CONTAINER,'') AS NM_TIPO_CONTAINER, ISNULL(FORMAT(B.DT_INICIAL_FREETIME,'dd/MM/yy'),'') AS INICIALFT, ";
            SQL += "ISNULL(FORMAT(B.DT_FINAL_FREETIME,'dd/MM/yy'),'') AS FINALFT, ISNULL(A.QT_DIAS_FREETIME,'') AS QT_DIAS_FREETIME, ";
            SQL += "ISNULL(FORMAT(B.DT_INICIAL_DEMURRAGE,'dd/MM/yy'),'') AS INICIALDEM, ISNULL(FORMAT(B.DT_FINAL_DEMURRAGE,'dd/MM/yy'),'') AS FINALDEM, ";
            SQL += "ISNULL(B.QT_DIAS_DEMURRAGE_COMPRA,'') AS QT_DIAS_DEMURRAGE_COMPRA, ISNULL(MD.SIGLA_MOEDA,'') AS SIGLA_MOEDA, ";
            SQL += "ISNULL(B.VL_TAXA_DEMURRAGE_COMPRA,0) AS VL_TAXA_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(B.VL_CAMBIO_DEMURRAGE_COMPRA,0) AS VL_CAMBIO_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(B.VL_DEMURRAGE_LIQUIDO_COMPRA,0) AS VL_DEMURRAGE_LIQUIDO_COMPRA, ";
            SQL += "ISNULL(CONVERT(DECIMAL(13,2),B.VL_DESCONTO_DEMURRAGE_COMPRA) * -1 + CONVERT(DECIMAL(13,2),B.VL_MULTA_DEMURRAGE_COMPRA),0) AS VL_DESCONTO_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(B.VL_DEMURRAGE_COMPRA_BR,0) AS VL_DEMURRAGE_COMPRA_BR ";
            SQL += "FROM TB_DEMURRAGE_FATURA_ITENS DFI ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL B ON DFI.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL A ON B.ID_CNTR_BL = A.ID_CNTR_BL AND B.ID_BL = A.ID_BL ";
            SQL += "LEFT JOIN TB_DEMURRAGE_FATURA DF ON DF.ID_DEMURRAGE_FATURA = DFI.ID_DEMURRAGE_FATURA ";
            SQL += "LEFT JOIN TB_MOEDA MD ON B.ID_MOEDA_DEMURRAGE_COMPRA = MD.ID_MOEDA ";
            SQL += "WHERE DFI.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
            SQL += "AND DF.CD_PR = 'P' ";

            DataTable imprimirDados = new DataTable();
            imprimirDados = DBS.List(SQL);
            return JsonConvert.SerializeObject(imprimirDados);
        }

        [WebMethod(EnableSession = true)]
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
            SQL = "select PFCL.ID_CNTR_BL, PFCL.NR_PROCESSO, B.NR_BL AS MBL, PFCL.NR_CNTR,PFCL.NM_TIPO_CONTAINER, ";
            SQL += "ISNULL(LEFT(P.NM_RAZAO,10),'') AS CLIENTE , ISNULL(LEFT(P2.NM_RAZAO,10),'') AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy') AS DT_CHEGADA, ";
            SQL += "ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME),'') AS QT_DIAS_FREETIME, ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME_CONFIRMA),'') AS QT_DIAS_FREETIME_CONFIRMA, FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy') AS DT_FINAL_FREETIME, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'),'') AS DT_DEVOLUCAO, ";
            SQL += "DFCL.QT_DIAS_DEMURRAGE, DFCL.QT_DIAS_DEMURRAGE_COMPRA, ";
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
            SQL += "PFCL.DS_STATUS_DEMURRAGE, ISNULL(FORMAT(PFCL.DT_STATUS_DEMURRAGE, 'dd/MM/yyyy'),'') AS DT_STATUS_DEMURRAGE, ISNULL(PFCL.DS_OBSERVACAO,'') AS DS_OBSERVACAO ";
            SQL += "from VW_PROCESSO_CONTAINER_FCL PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DEMURRAGE_COMPRA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_MOEDA M2 ON DFCL.ID_MOEDA_DEMURRAGE_VENDA = M2.ID_MOEDA ";
            SQL += "INNER JOIN TB_BL B ON PFCL.ID_BL_MASTER = B.ID_BL ";

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
                    SQL += "WHERE TBD.ID_PARCEIRO_TRANSPORTADOR = 51 ";
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

        [WebMethod(EnableSession = true)]
        public string excluirFatura(string idFatura, int check)
        {
            string SQL;
            
            if (check == 1)
            {
                SQL = "SELECT DT_EXPORTACAO_DEMURRAGE FROM TB_DEMURRAGE_FATURA WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string dtExport = listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE"].ToString();

                SQL = "SELECT COUNT(ID_CNTR_BL) AS COUNT_CNTR FROM TB_DEMURRAGE_FATURA A ";
                SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS B ON B.ID_DEMURRAGE_FATURA = A.ID_DEMURRAGE_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DEMURRAGE C ON C.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable cntrCount = new DataTable();
                cntrCount = DBS.List(SQL);
                int cntrCounts = (int)cntrCount.Rows[0]["COUNT_CNTR"];

                if (dtExport == "")
                {
                    for (int i = 0; i < cntrCounts; i++)
                    {
                        SQL = "SELECT ID_CNTR_BL FROM TB_DEMURRAGE_FATURA A ";
                        SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS B ON B.ID_DEMURRAGE_FATURA = A.ID_DEMURRAGE_FATURA ";
                        SQL += "LEFT JOIN TB_CNTR_DEMURRAGE C ON C.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
                        SQL += "WHERE A.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                        DataTable cntr = new DataTable();
                        cntr = DBS.List(SQL);
                        string cntrbl = cntr.Rows[0]["ID_CNTR_BL"].ToString();


                        SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = 1 WHERE ID_CNTR_BL = '" + cntrbl + "' ";
                        DBS.ExecuteScalar(SQL);
                    }

                    SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA = " + idFatura + "";
                    DataTable getId = new DataTable();
                    getId = DBS.List(SQL);

                    if (getId != null)
                    {

                        for (int i = 0; i < getId.Rows.Count; i++)
                        {
                            SQL = "DELETE FROM TB_FATURAMENTO WHERE ID_CONTA_PAGAR_RECEBER = '" + getId.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "'";
                            DBS.ExecuteScalar(SQL);

                            SQL = "DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = '" + getId.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "'";
                            DBS.ExecuteScalar(SQL);

                            SQL = "DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = '" + getId.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "'";
                            DBS.ExecuteScalar(SQL);
                        }
                    }

                    SQL = "DELETE FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA = " + idFatura + " ";
                    DBS.ExecuteScalar(SQL);

                    SQL = "DELETE FROM TB_DEMURRAGE_FATURA_ITENS WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
                    DBS.ExecuteScalar(SQL);

                    SQL = "DELETE FROM TB_DEMURRAGE_FATURA WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
                    DBS.ExecuteScalar(SQL);                   

                    return JsonConvert.SerializeObject("1");
                }
                else
                {
                    return JsonConvert.SerializeObject("2");
                }
            }
            else
            {
                SQL = "SELECT DT_EXPORTACAO_DEMURRAGE_COMPRA FROM TB_DEMURRAGE_FATURA WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string dtExport = listTable.Rows[0]["DT_EXPORTACAO_DEMURRAGE_COMPRA"].ToString();

                SQL = "SELECT COUNT(ID_CNTR_BL) AS COUNT_CNTR FROM TB_DEMURRAGE_FATURA A ";
                SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS B ON B.ID_DEMURRAGE_FATURA = A.ID_DEMURRAGE_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DEMURRAGE C ON C.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
                SQL += "WHERE A.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                DataTable cntrCount = new DataTable();
                cntrCount = DBS.List(SQL);
                int cntrCounts = (int)cntrCount.Rows[0]["COUNT_CNTR"];

                if (dtExport == "")
                {
                    for (int i = 0; i < cntrCounts; i++)
                    {
                        SQL = "SELECT ID_CNTR_BL FROM TB_DEMURRAGE_FATURA A ";
                        SQL += "LEFT JOIN TB_DEMURRAGE_FATURA_ITENS B ON B.ID_DEMURRAGE_FATURA = A.ID_DEMURRAGE_FATURA ";
                        SQL += "LEFT JOIN TB_CNTR_DEMURRAGE C ON C.ID_CNTR_DEMURRAGE = B.ID_CNTR_DEMURRAGE ";
                        SQL += "WHERE A.ID_DEMURRAGE_FATURA = '" + idFatura + "' ";
                        DataTable cntr = new DataTable();
                        cntr = DBS.List(SQL);
                        string cntrbl = cntr.Rows[0]["ID_CNTR_BL"].ToString();


                        SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DEMURRAGE = 1 WHERE ID_CNTR_BL = '" + cntrbl + "' ";
                        DBS.ExecuteScalar(SQL);
                    }

                    SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA = " + idFatura + "";
                    DataTable getId = new DataTable();
                    getId = DBS.List(SQL);

                    if (getId != null)
                    {
                        for (int i = 0; i < getId.Rows.Count; i++)
                        {
                            SQL = "DELETE FROM TB_FATURAMENTO WHERE ID_CONTA_PAGAR_RECEBER = '" + getId.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "'";
                            DBS.ExecuteScalar(SQL);

                            SQL = "DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = '" + getId.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "'";
                            DBS.ExecuteScalar(SQL);

                            SQL = "DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = '" + getId.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "'";
                            DBS.ExecuteScalar(SQL);
                        }
                    }

                    SQL = "DELETE FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA = " + idFatura + " ";
                    DBS.ExecuteScalar(SQL);

                    SQL = "DELETE FROM TB_DEMURRAGE_FATURA_ITENS WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
                    DBS.ExecuteScalar(SQL);

                    SQL = "DELETE FROM TB_DEMURRAGE_FATURA WHERE ID_DEMURRAGE_FATURA = '" + idFatura + "'";
                    DBS.ExecuteScalar(SQL);

                    

                    return JsonConvert.SerializeObject("1");
                }
                else
                {
                    return JsonConvert.SerializeObject("2");
                }
            }
        }

        [WebMethod(EnableSession = true)]
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
                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL in (" + idCont + ") ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                if (dsStatus == "2")
                {
                    flagF = "1";
                }
                else
                {
                    if (faturaCompra != "" && faturaVenda != "")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        flagF = "0";
                    }
                }

                

                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = " + dtDevolucao + ", ID_STATUS_DEMURRAGE = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "' ";
                if (dsStatus == "2") { SQL += ", ID_STATUS_DEMURRAGE_COMPRA = '" + dsStatus + "' "; }
                SQL += "WHERE ID_CNTR_BL in (" + idCont + ") ";
            }

            else
            {
                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL in (" + idCont + ") ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                if (dsStatus == "2")
                {
                    flagF = "1";
                }
                else
                {
                    if (faturaCompra != "" && faturaVenda != "")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        flagF = "0";
                    }
                }

                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = '" + dtDevolucao + "', ID_STATUS_DEMURRAGE = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "' ";
                if (dsStatus == "2") { SQL += ", ID_STATUS_DEMURRAGE_COMPRA = '" + dsStatus + "' "; }
                SQL += "WHERE ID_CNTR_BL in (" + idCont + ") ";
            }
            string attDevolu = DBS.ExecuteScalar(SQL);
            return "1";
        }

        [WebMethod(EnableSession = true)]
        public string atualizarDevolucaoExp(string idCont, string dtStatus, string dsStatus, string dtDevolucao)
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
                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL in (" + idCont + ") ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                if (dsStatus == "2")
                {
                    flagF = "1";
                }
                else
                {
                    if (faturaCompra != "" && faturaVenda != "")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        flagF = "0";
                    }
                }

                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = " + dtDevolucao + ", ID_STATUS_DEMURRAGE = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "' ";
                SQL += "WHERE ID_CNTR_BL in (" + idCont + ") ";
            }

            else
            {
                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_PAGAR),'') AS ID_DEMURRAGE_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DEMURRAGE_FATURA_RECEBER),'') AS ID_DEMURRAGE_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DEMURRAGE_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL in (" + idCont + ") ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DEMURRAGE_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DEMURRAGE_RECEBER"].ToString();

                if (dsStatus == "2")
                {
                    flagF = "1";
                }
                else
                {
                    if (faturaCompra != "" && faturaVenda != "")
                    {
                        flagF = "1";
                    }
                    else
                    {
                        flagF = "0";
                    }
                }

                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = '" + dtDevolucao + "', ID_STATUS_DEMURRAGE = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DEMURRAGE = '" + dtStatus + "' ";
                SQL += "WHERE ID_CNTR_BL in (" + idCont + ") ";
            }
            string attDevolu = DBS.ExecuteScalar(SQL);
            return "1";
        }

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
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

            SQL = "SELECT DISTINCT A.NR_CNTR, A.NM_TIPO_CONTAINER, ISNULL(FORMAT(B.DT_INICIAL_FREETIME,'dd/MM/yy'),'') AS INICIALFT, ";
            SQL += "ISNULL(FORMAT(B.DT_FINAL_FREETIME,'dd/MM/yy'),'') AS FINALFT,A.QT_DIAS_FREETIME, ";
            SQL += "ISNULL(FORMAT(B.DT_INICIAL_DEMURRAGE,'dd/MM/yy'),'') AS INICIALDEM, ISNULL(FORMAT(B.DT_FINAL_DEMURRAGE,'dd/MM/yy'),'') AS FINALDEM, ";
            SQL += "case when B.QT_DIAS_DEMURRAGE < 1 then '' else convert(varchar,B.QT_DIAS_DEMURRAGE) end QT_DIAS_DEMURRAGE,";
            SQL += "case when isnull(B.QT_DIAS_DEMURRAGE_COMPRA,0) < 1 then '' else convert(varchar,B.QT_DIAS_DEMURRAGE_COMPRA) end QT_DIAS_DEMURRAGE_COMPRA,";
            SQL += "ISNULL(MD.SIGLA_MOEDA,'') AS SIGLA_MOEDA, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DEMURRAGE_COMPRA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(REPLACE(FORMAT(B.VL_DEMURRAGE_COMPRA,'C','PT-BR'),'R$',''),0) AS VL_DEMURRAGE_COMPRA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DEMURRAGE_VENDA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DEMURRAGE_VENDA, ";
            SQL += "ISNULL(REPLACE(FORMAT(B.VL_DEMURRAGE_VENDA, 'C', 'PT-BR'), 'R$', ''),0) AS VL_DEMURRAGE_VENDA ";
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

        [WebMethod(EnableSession = true)]
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
        [WebMethod(EnableSession = true)]
        public string atualizarBaseFCA(DemurrageControl dadosEdit)
        {
            string SQL;
            if (dadosEdit.DS_STATUS == "Aberto")
            {

                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }

            else if (dadosEdit.DS_STATUS == "Devolvido Sem Detention")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "' ";
                SQL += "DT_DEVOLUCAO_CNTR = '" + dadosEdit.DT_DEVOLUCAO_CNTR + "' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Devolvido sem demurrage")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "' ";
                SQL += "DT_DEVOLUCAO_CNTR = '" + dadosEdit.DT_DEVOLUCAO_CNTR + "' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Fase final de faturamento")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Em cobrança")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "' ";
                SQL += "DT_DEVOLUCAO_CNTR = '" + dadosEdit.DT_DEVOLUCAO_CNTR + "', QT_DIAS_DEMURRAGE = '" + dadosEdit.QT_DIAS_DEMURRAGE + "' ";
                SQL += "VL_FATURA_TERC = '" + dadosEdit.VL_FATURA + "', DT_VENCIMENTO_FATURA_TERC = '" + dadosEdit.DT_VENCIMENTO_FATURA + "' ";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Baixado")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "' ";
                SQL += "DT_PAGAMENTO_FATURA_TERC = '" + dadosEdit.DT_PAGAMENTO + "'";
                SQL += "FROM TB_CNTR_BL JOIN TB_BL on dbo.TB_CNTR_BL.ID_BL_MASTER = dbo.TB_BL.ID_BL_MASTER ";
                SQL += "where TB_CNTR_BL.NR_CNTR = '" + dadosEdit.NR_CNTR + "' and TB_BL.NR_PROCESSO = '" + dadosEdit.NR_PROCESSO + "' ";
                string atualizarFCA = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else if (dadosEdit.DS_STATUS == "Cobrança cancelada")
            {
                SQL = "UPDATE TB_CNTR_BL SET DS_STATUS_TERC = '" + dadosEdit.DS_STATUS + "', DT_STATUS_TERC = '" + dadosEdit.DT_REPORT_DATE + "' ";
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

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        public string parcelarDemurrage(int idfatura, string vlDemurrageParcela, int qtDemurrageParcela, string dtPeriodoInicial)
        {
            string SQL;

            string cultureName = "pt-BR";

            CultureInfo culture = new CultureInfo(cultureName);

            DateTime dtVencimento = Convert.ToDateTime(dtPeriodoInicial, culture);

            for (int i = 0; i < qtDemurrageParcela; i++)
            {
                SQL = "EXEC insert_demurrage_parcelas " + idfatura + ", '" + vlDemurrageParcela.Replace(",", ".") + "', '" + dtVencimento + "',null, 0, null, null, null";

                DBS.ExecuteScalar(SQL);

                dtVencimento = dtVencimento.AddMonths(1);
            }

            

            return JsonConvert.SerializeObject("OK");
        }

        [WebMethod(EnableSession = true)]
        public string listarParcelamentoDemurrage(int fatura)
        {
            string SQL;

            SQL = "SELECT ID_DEMURRAGE_FATURA_PARCELAS, VL_DEMURRAGE_PARCELA, ";
            SQL += "FORMAT(DT_VENCIMENTO_DEMURRAGE_PARCELA, 'dd/MM/yyyy') AS DT_VENCIMENTO, ";
            SQL += "ISNULL(VL_DEMURRAGE_PARCELA_JUROS,0) AS VL_DEMURRAGE_PARCELA_JUROS, CASE WHEN FL_PAGO=1 THEN 'PAGO' ELSE 'PENDENTE' END AS FL_PAGO, A.ID_CONTA_PAGAR_RECEBER, ";
            SQL += "FORMAT(B.DT_ENVIO_FATURAMENTO,'dd/MM/yyyy') AS DT_ENVIO_FATURAMENTO ";
            SQL += "FROM TB_DEMURRAGE_FATURA_PARCELAS A ";
            SQL += "LEFT JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER ";
            SQL += "WHERE A.ID_DEMURRAGE_FATURA = "+fatura+" ";
            
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string infoParcela(int idParcela)
        {
            string SQL;

            SQL = "SELECT ID_DEMURRAGE_FATURA_PARCELAS, VL_DEMURRAGE_PARCELA, FORMAT(DT_VENCIMENTO_DEMURRAGE_PARCELA, 'yyyy-MM-dd') AS DT_VENCIMENTO_DEMURRAGE_PARCELA, ";
            SQL += "VL_DEMURRAGE_PARCELA_JUROS, FL_PAGO ";
            SQL += "FROM TB_DEMURRAGE_FATURA_PARCELAS ";
            SQL += "WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idParcela + " ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string editarParcela(int idParcela, string vencimento, string vlParcela, string vlParcelaJuros, string vlJuros, string pago)
        {
            string SQL;

            SQL = "UPDATE TB_DEMURRAGE_FATURA_PARCELAS SET VL_DEMURRAGE_PARCELA ='"+vlParcela.Replace(",",".")+"', DT_VENCIMENTO_DEMURRAGE_PARCELA='"+vencimento+"', VL_DEMURRAGE_PARCELA_JUROS='"+vlJuros.Replace(",",".")+"', FL_PAGO="+pago+" WHERE ID_DEMURRAGE_FATURA_PARCELAS = "+idParcela+" ";

            DBS.ExecuteScalar(SQL);
            return JsonConvert.SerializeObject("Success");
        }

        [WebMethod(EnableSession = true)]
        public string deletarParcela(int idParcela)
        {
            string SQL;

            SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idParcela + " ";
            string idCPR = DBS.ExecuteScalar(SQL);

            if (idCPR != "" && idCPR != "null")
            {
                SQL = "DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                DBS.ExecuteScalar(SQL);

                SQL = "DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                DBS.ExecuteScalar(SQL);
            }

            SQL = "DELETE FROM TB_FATURAMENTO WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
            DBS.ExecuteScalar(SQL);

            SQL = "DELETE FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idParcela + " ";
                DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("1");
 
        }

        [WebMethod(EnableSession = true)]
        public string cancelarExportarParcelaCC(int idParcela)
        {
            string SQL;

            SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idParcela + " ";
            string idCPR = DBS.ExecuteScalar(SQL);

            if (idCPR != "" && idCPR != "null")
            {
                SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_LIQUIDACAO = NULL, ID_USUARIO_LIQUIDACAO = NULL WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                DBS.ExecuteScalar(SQL);
            }

            SQL = "UPDATE TB_DEMURRAGE_FATURA_PARCELAS SET DT_EXPORTACAO_PARCELA = NULL, ID_USUARIO_EXPORTACAO_DEMURRAGE_PARCELA = NULL, FL_PAGO = 0 WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idParcela + " ";
            DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("1");

        }

        [WebMethod(EnableSession = true)]
        public string EnviarFaturamento(int idParcela)
        {
            string SQL;

            SQL = "SELECT ID_CONTA_PAGAR_RECEBER, ID_DEMURRAGE_FATURA, VL_DEMURRAGE_PARCELA, ISNULL(VL_DEMURRAGE_PARCELA_JUROS,0) as VL_DEMURRAGE_PARCELA_JUROS FROM TB_DEMURRAGE_FATURA_PARCELAS WHERE ID_DEMURRAGE_FATURA_PARCELAS = " + idParcela + " ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            string vlDemurrageParcela = listTable.Rows[0]["VL_DEMURRAGE_PARCELA"].ToString();
            string vlDemurrageParcelaJuros = listTable.Rows[0]["VL_DEMURRAGE_PARCELA_JUROS"].ToString();
            string idcpr = listTable.Rows[0]["ID_CONTA_PAGAR_RECEBER"].ToString();
            string idfat = listTable.Rows[0]["ID_DEMURRAGE_FATURA"].ToString();

            double vlNota = Convert.ToDouble(vlDemurrageParcela) + (Convert.ToDouble(vlDemurrageParcela) * (Convert.ToDouble(vlDemurrageParcelaJuros) / 100));

            SQL = "SELECT B.ID_PARCEIRO_CLIENTE, C.NM_RAZAO, C.CNPJ, ISNULL(C.INSCR_ESTADUAL,'') AS INSCR_ESTADUAL, ISNULL(C.INSCR_MUNICIPAL,'') AS INSCR_MUNICIPAL, ";
            SQL += "ISNULL(C.ENDERECO,'') AS ENDERECO, C.NR_ENDERECO, C.COMPL_ENDERECO, C.BAIRRO, C.CEP, D.NM_CIDADE, E.NM_ESTADO ";
            SQL += "FROM VW_PROCESSO_DEMURRAGE_FCL A ";
            SQL += "JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_PARCEIRO C ON B.ID_PARCEIRO_CLIENTE = C.ID_PARCEIRO ";
            SQL += "JOIN TB_CIDADE D ON C.ID_CIDADE = D.ID_CIDADE ";
            SQL += "JOIN TB_ESTADO E ON D.ID_ESTADO = E.ID_ESTADO ";
            SQL += "WHERE(ID_DEMURRAGE_FATURA_PAGAR = " + idfat + " OR ID_DEMURRAGE_FATURA_RECEBER = " + idfat + ") ";

            DataTable InfoCliente = new DataTable();
            InfoCliente = DBS.List(SQL);

            SQL = "INSERT INTO TB_FATURAMENTO (ID_CONTA_PAGAR_RECEBER, VL_NOTA, ID_PARCEIRO_CLIENTE, NM_CLIENTE, CNPJ, INSCR_ESTADUAL, INSCR_MUNICIPAL, ENDERECO, NR_ENDERECO, COMPL_ENDERECO, BAIRRO, CEP, CIDADE, ESTADO) VALUES (" + idcpr + ", " + vlNota.ToString().Replace(",",".") + "," + InfoCliente.Rows[0]["ID_PARCEIRO_CLIENTE"] + ", '" + InfoCliente.Rows[0]["NM_RAZAO"] + "','" + InfoCliente.Rows[0]["CNPJ"] + "', '" + InfoCliente.Rows[0]["INSCR_ESTADUAL"] + "','" + InfoCliente.Rows[0]["INSCR_MUNICIPAL"] + "', '" + InfoCliente.Rows[0]["ENDERECO"] + "','" + InfoCliente.Rows[0]["NR_ENDERECO"] + "', '" + InfoCliente.Rows[0]["COMPL_ENDERECO"] + "','" + InfoCliente.Rows[0]["BAIRRO"] + "', '" + InfoCliente.Rows[0]["CEP"] + "','" + InfoCliente.Rows[0]["NM_CIDADE"] + "', '" + InfoCliente.Rows[0]["NM_ESTADO"] + "') ";

            DBS.ExecuteScalar(SQL);

            SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_ENVIO_FATURAMENTO = GETDATE() WHERE ID_CONTA_PAGAR_RECEBER = "+idcpr+" ";

            DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("1");

        }

        [WebMethod(EnableSession = true)]
        public string listarCourrierFilter(Filtro dados)
        {
            string SQL;
            SQL = "WHERE SUBSTRING(BL.NR_PROCESSO,10,2)>= '18' ";
            SQL += "AND LEFT(BL.NR_PROCESSO,1) = 'M' ";
            SQL += "AND BL.ID_BL_MASTER IS NOT NULL ";
            SQL += "AND BL.DT_EMBARQUE IS NOT NULL ";

            if (dados.BLHOUSE != "")
            {
                SQL += "AND BL.NR_BL LIKE '" + dados.BLHOUSE + "%' ";

            }

            if (dados.DTRECEBIMENTOMBLINICIO != "")
            {
                if (dados.DTRECEBIMENTOMBLFIM != "")
                {
                    SQL += "AND M.DT_RECEBIMENTO_MBL >= '" + dados.DTRECEBIMENTOMBLINICIO + "' AND M.DT_RECEBIMENTO_MBL <= '" + dados.DTRECEBIMENTOMBLFIM + "' ";
                }
                else
                {
                    SQL += "AND M.DT_RECEBIMENTO_MBL >= '" + dados.DTRECEBIMENTOMBLINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTRECEBIMENTOMBLFIM != "")
                {
                    SQL += "AND M.DT_RECEBIMENTO_MBL >= '" + dados.DTRECEBIMENTOMBLFIM + "' ";
                }
            }

            if (dados.DTRECEBIMENTOHBLINICIO != "")
            {
                if (dados.DTRECEBIMENTOHBLFIM != "")
                {
                    SQL += "AND BL.DT_RECEBIMENTO_HBL >= '" + dados.DTRECEBIMENTOHBLINICIO + "' AND BL.DT_RECEBIMENTO_HBL <= '" + dados.DTRECEBIMENTOHBLFIM + "' ";
                }
                else
                {
                    SQL += "AND BL.DT_RECEBIMENTO_HBL >= '" + dados.DTRECEBIMENTOHBLINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTRECEBIMENTOHBLFIM != "")
                {
                    SQL += "AND BL.DT_RECEBIMENTO_HBL >= '" + dados.DTRECEBIMENTOHBLFIM + "' ";
                }
            }

            if (dados.DTRETIRADAINICIO != "")
            {
                if (dados.DTRETIRADAFIM != "")
                {
                    SQL += "AND BL.DT_RETIRADA_COURRIER >= '" + dados.DTRETIRADAINICIO + "' AND BL.DT_RETIRADA_COURRIER <= '" + dados.DTRETIRADAFIM + "' ";
                }
                else
                {
                    SQL += "AND BL.DT_RETIRADA_COURRIER >= '" + dados.DTRETIRADAINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTRETIRADAFIM != "")
                {
                    SQL += "AND BL.DT_RETIRADA_COURRIER >= '" + dados.DTRETIRADAFIM + "' ";
                }
            }

            if (dados.PREVISAOCHEGADAINICIO != "")
            {
                if (dados.PREVISAOCHEGADAFIM != "")
                {
                    SQL += "AND BL.DT_PREVISAO_CHEGADA >= '" + dados.PREVISAOCHEGADAINICIO + "' AND BL.DT_PREVISAO_CHEGADA <= '" + dados.PREVISAOCHEGADAFIM + "' ";
                }
                else
                {
                    SQL += "AND BL.DT_PREVISAO_CHEGADA >= '" + dados.PREVISAOCHEGADAINICIO + "' ";
                }
            }
            else
            {
                if (dados.PREVISAOCHEGADAFIM != "")
                {
                    SQL += "AND BL.DT_PREVISAO_CHEGADA >= '" + dados.PREVISAOCHEGADAFIM + "' ";
                }
            }

            if (dados.DTCHEGADAINICIO != "")
            {
                if (dados.DTCHEGADAFIM != "")
                {
                    SQL += "AND BL.DT_CHEGADA >= '" + dados.DTCHEGADAINICIO + "' AND BL.DT_CHEGADAL <= '" + dados.DTCHEGADAFIM + "' ";
                }
                else
                {
                    SQL += "AND BL.DT_CHEGADA >= '" + dados.DTCHEGADAINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTCHEGADAFIM != "")
                {
                    SQL += "AND BL.DT_CHEGADA >= '" + dados.DTCHEGADAFIM + "' ";
                }
            }

            if (dados.DTRETIRADAPEROSNALINICIO != "")
            {
                if (dados.DTRETIRADAPEROSNALFIM != "")
                {
                    SQL += "AND M.DT_RETIRADA_PERSONAL >= '" + dados.DTRETIRADAPEROSNALINICIO + "' AND BL.DT_RETIRADA_PERSONAL <= '" + dados.DTRETIRADAPEROSNALFIM + "' ";
                }
                else
                {
                    SQL += "AND M.DT_RETIRADA_PERSONAL >= '" + dados.DTRETIRADAPEROSNALINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTRETIRADAPEROSNALFIM != "")
                {
                    SQL += "AND M.DT_RETIRADA_PERSONAL >= '" + dados.DTRETIRADAPEROSNALFIM + "' ";
                }
            }

            if (dados.AGENTE != "")
            {
                SQL += "AND P2.NM_RAZAO LIKE '" + dados.AGENTE + "%' ";
            }

            if (dados.CDRASTREAMENTOHBL != "")
            {
                SQL += "AND BL.CD_RASTREAMENTO_HBL LIKE '" + dados.CDRASTREAMENTOHBL + "%' ";
            }

            if (dados.CDRASTREAMENTOMBL != "")
            {
                SQL += "AND M.CD_RASTREAMENTO_MBL LIKE '" + dados.CDRASTREAMENTOMBL + "%' ";
            }

            if (dados.RETIRADOPOR != "")
            {
                SQL += "AND BL.NM_RETIRADO_POR_COURRIER LIKE '" + dados.RETIRADOPOR + "%' ";
            }

            if (dados.FATURA != "")
            {
                SQL += "AND BL.NR_FATURA_COURRIER LIKE '" + dados.RETIRADOPOR + "%' ";
            }

            return SQL;
        }

        [WebMethod(EnableSession = true)]
        public string listarCourrier(string idFilter, string Filter, string tipo, Filtro dados)
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
                    idFilter = "AND P.NM_RAZAO LIKE '" + Filter + "%' ";
                    break;
                case "4":
                    idFilter = "AND N.NM_NAVIO LIKE '" + Filter + "%' ";
                    break;
                case "5":
                    idFilter = "AND BL.CD_RASTREAMENTO_HBL LIKE '" + Filter + "%' OR M.CD_RASTREAMENTO_MBL LIKE '" + Filter + "%' ";
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

            SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, ISNULL(M.NR_BL,'') as MASTER, ISNULL(BL.NR_BL,'') AS HOUSE, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
            SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ISNULL(FORMAT(M.DT_RETIRADA_PERSONAL,'dd/MM/yyyy'),'') AS DT_RETIRADA_PERSONAL, ";
            SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P2.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, BL.FL_TROCA, ";
            SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
            SQL += "FROM TB_BL BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON BL.ID_PARCEIRO_AGENTE_INTERNACIONAL = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
            SQL += "" + listarCourrierFilter(dados) + " ";
            SQL += "" + idFilter + "";
            SQL += "" + tipo + "";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarCourrierFilterTeste(Filtro dados)
        {
            string SQL;
            SQL = "WHERE SUBSTRING(BL.NR_PROCESSO,10,2)>= '18' ";
            SQL += "AND LEFT(BL.NR_PROCESSO,1) = 'M' ";
            SQL += "AND BL.ID_BL_MASTER IS NOT NULL ";

            if (dados.BLHOUSE != "")
            {
                SQL += "AND BL.NR_BL LIKE '" + dados.BLHOUSE + "%' ";

            }

            if (dados.DTRECEBIMENTOMBLINICIO != "")
            {
                if (dados.DTRECEBIMENTOMBLFIM != "")
                {
                    SQL += "AND M.DT_RECEBIMENTO_MBL >= '" + dados.DTRECEBIMENTOMBLINICIO + "' AND M.DT_RECEBIMENTO_MBL <= '" + dados.DTRECEBIMENTOMBLFIM + "' ";
                }
                else
                {
                    SQL += "AND M.DT_RECEBIMENTO_MBL >= '" + dados.DTRECEBIMENTOMBLINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTRECEBIMENTOMBLFIM != "")
                {
                    SQL += "AND M.DT_RECEBIMENTO_MBL >= '" + dados.DTRECEBIMENTOMBLFIM + "' ";
                }
            }

            if (dados.DTRECEBIMENTOHBLINICIO != "")
            {
                if (dados.DTRECEBIMENTOHBLFIM != "")
                {
                    SQL += "AND BL.DT_RECEBIMENTO_HBL >= '" + dados.DTRECEBIMENTOHBLINICIO + "' AND BL.DT_RECEBIMENTO_HBL <= '" + dados.DTRECEBIMENTOHBLFIM + "' ";
                }
                else
                {
                    SQL += "AND BL.DT_RECEBIMENTO_HBL >= '" + dados.DTRECEBIMENTOHBLINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTRECEBIMENTOHBLFIM != "")
                {
                    SQL += "AND BL.DT_RECEBIMENTO_HBL >= '" + dados.DTRECEBIMENTOHBLFIM + "' ";
                }
            }

            if (dados.DTRETIRADAINICIO != "")
            {
                if (dados.DTRETIRADAFIM != "")
                {
                    SQL += "AND BL.DT_RETIRADA_COURRIER >= '" + dados.DTRETIRADAINICIO + "' AND BL.DT_RETIRADA_COURRIER <= '" + dados.DTRETIRADAFIM + "' ";
                }
                else
                {
                    SQL += "AND BL.DT_RETIRADA_COURRIER >= '" + dados.DTRETIRADAINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTRETIRADAFIM != "")
                {
                    SQL += "AND BL.DT_RETIRADA_COURRIER >= '" + dados.DTRETIRADAFIM + "' ";
                }
            }

            if (dados.PREVISAOCHEGADAINICIO != "")
            {
                if (dados.PREVISAOCHEGADAFIM != "")
                {
                    SQL += "AND BL.DT_PREVISAO_CHEGADA >= '" + dados.PREVISAOCHEGADAINICIO + "' AND BL.DT_PREVISAO_CHEGADA <= '" + dados.PREVISAOCHEGADAFIM + "' ";
                }
                else
                {
                    SQL += "AND BL.DT_PREVISAO_CHEGADA >= '" + dados.PREVISAOCHEGADAINICIO + "' ";
                }
            }
            else
            {
                if (dados.PREVISAOCHEGADAFIM != "")
                {
                    SQL += "AND BL.DT_PREVISAO_CHEGADA >= '" + dados.PREVISAOCHEGADAFIM + "' ";
                }
            }

            if (dados.DTCHEGADAINICIO != "")
            {
                if (dados.DTCHEGADAFIM != "")
                {
                    SQL += "AND BL.DT_CHEGADA >= '" + dados.DTCHEGADAINICIO + "' AND BL.DT_CHEGADAL <= '" + dados.DTCHEGADAFIM + "' ";
                }
                else
                {
                    SQL += "AND BL.DT_CHEGADA >= '" + dados.DTCHEGADAINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTCHEGADAFIM != "")
                {
                    SQL += "AND BL.DT_CHEGADA >= '" + dados.DTCHEGADAFIM + "' ";
                }
            }

            if (dados.DTRETIRADAPEROSNALINICIO != "")
            {
                if (dados.DTRETIRADAPEROSNALFIM != "")
                {
                    SQL += "AND M.DT_RETIRADA_PERSONAL >= '" + dados.DTRETIRADAPEROSNALINICIO + "' AND BL.DT_RETIRADA_PERSONAL <= '" + dados.DTRETIRADAPEROSNALFIM + "' ";
                }
                else
                {
                    SQL += "AND M.DT_RETIRADA_PERSONAL >= '" + dados.DTRETIRADAPEROSNALINICIO + "' ";
                }
            }
            else
            {
                if (dados.DTRETIRADAPEROSNALFIM != "")
                {
                    SQL += "AND M.DT_RETIRADA_PERSONAL >= '" + dados.DTRETIRADAPEROSNALFIM + "' ";
                }
            }

            if (dados.AGENTE != "")
            {
                SQL += "AND P2.NM_RAZAO LIKE '" + dados.AGENTE + "%' ";
            }

            if (dados.CDRASTREAMENTOHBL != "")
            {
                SQL += "AND BL.CD_RASTREAMENTO_HBL LIKE '" + dados.CDRASTREAMENTOHBL + "%' ";
            }

            if (dados.CDRASTREAMENTOMBL != "")
            {
                SQL += "AND M.CD_RASTREAMENTO_MBL LIKE '" + dados.CDRASTREAMENTOMBL + "%' ";
            }

            if (dados.RETIRADOPOR != "")
            {
                SQL += "AND BL.NM_RETIRADO_POR_COURRIER LIKE '" + dados.RETIRADOPOR + "%' ";
            }

            if (dados.FATURA != "")
            {
                SQL += "AND BL.NR_FATURA_COURRIER LIKE '" + dados.RETIRADOPOR + "%' ";
            }

            return SQL;
        }

        [WebMethod(EnableSession = true)]
        public string listarCourrierTeste(string idFilter, string Filter, string tipo, Filtro dados)
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
                    idFilter = "AND P.NM_RAZAO LIKE '" + Filter + "%' ";
                    break;
                case "4":
                    idFilter = "AND N.NM_NAVIO LIKE '" + Filter + "%' ";
                    break;
                case "5":
                    idFilter = "AND BL.CD_RASTREAMENTO_HBL LIKE '" + Filter + "%' OR M.CD_RASTREAMENTO_MBL LIKE '" + Filter + "%' ";
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

            SQL = "SELECT ISNULL(BL.NR_PROCESSO,'') AS NR_PROCESSO, ISNULL(M.NR_BL,'') as MASTER, ISNULL(BL.NR_BL,'') AS HOUSE, BL.ID_BL, ISNULL(P.NM_RAZAO,'') AS CLIENTE, ISNULL(FORMAT(M.DT_RECEBIMENTO_MBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_MBL, ";
            SQL += "ISNULL(M.CD_RASTREAMENTO_MBL,'') AS CD_RASTREAMENTO_MBL, ISNULL(FORMAT(BL.DT_RECEBIMENTO_HBL,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO_HBL, ISNULL(BL.CD_RASTREAMENTO_HBL,'') AS CD_RASTREAMENTO_HBL, ISNULL(FORMAT(BL.DT_RETIRADA_COURRIER,'dd/MM/yyyy'),'') AS DT_RETIRADA_COURRIER, ISNULL(FORMAT(M.DT_RETIRADA_PERSONAL,'dd/MM/yyyy'),'') AS DT_RETIRADA_PERSONAL, ";
            SQL += "ISNULL(BL.NM_RETIRADO_POR_COURRIER,'') AS NM_RETIRADO_POR_COURRIER, ISNULL(P2.NM_RAZAO,'') AS AGENTE, ISNULL(N.NM_NAVIO,'') AS NM_NAVIO, ISNULL(FORMAT(BL.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ISNULL(FORMAT(BL.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, BL.FL_TROCA, ";
            SQL += "ISNULL(BL.NR_FATURA_COURRIER,'') AS NR_FATURA_COURRIER, ISNULL(TP.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM ";
            SQL += "FROM TB_BL BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON BL.ID_PARCEIRO_AGENTE_INTERNACIONAL = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
            SQL += "" + listarCourrierFilterTeste(dados) + " ";
            SQL += "" + idFilter + "";
            SQL += "" + tipo + "";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }



        [WebMethod(EnableSession = true)]
        public string BuscarCourrier(int id)
        {
            string SQL;
            SQL = "SELECT BL.NR_PROCESSO, M.NR_BL AS MASTER, BL.NR_BL AS HOUSE, P.NM_RAZAO AS CLIENTE, FORMAT(M.DT_RECEBIMENTO_MBL,'yyyy-MM-dd') AS DT_RECEBIMENTO_MBL, ";
            SQL += "M.CD_RASTREAMENTO_MBL, FORMAT(BL.DT_RECEBIMENTO_HBL,'yyyy-MM-dd') AS DT_RECEBIMENTO_HBL, BL.CD_RASTREAMENTO_HBL, FORMAT(BL.DT_RETIRADA_COURRIER,'yyyy-MM-dd') AS DT_RETIRADA_COURRIER, FORMAT(M.DT_RETIRADA_PERSONAL,'yyyy-MM-dd') AS DT_RETIRADA_PERSONAL, ";
            SQL += "BL.NM_RETIRADO_POR_COURRIER, P.NM_RAZAO AS AGENTE, N.NM_NAVIO, FORMAT(BL.DT_PREVISAO_CHEGADA,'yyyy-MM-dd') AS DT_PREVISAO_CHEGADA, FORMAT(BL.DT_CHEGADA,'yyyy-MM-dd') AS DT_CHEGADA, BL.FL_TROCA, ";
            SQL += "BL.NR_FATURA_COURRIER, TP.NM_TIPO_ESTUFAGEM ";
            SQL += "FROM TB_BL BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON BL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_NAVIO N ON BL.ID_NAVIO = N.ID_NAVIO ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM TP ON BL.ID_TIPO_ESTUFAGEM = TP.ID_TIPO_ESTUFAGEM ";
            SQL += "JOIN TB_BL M on BL.ID_BL_MASTER = M.ID_BL ";
            SQL += "WHERE BL.ID_BL = '" + id + "' ";

            DataTable carregarDados = new DataTable();
            carregarDados = DBS.List(SQL);
            if (carregarDados != null)
            {
                CourrierInfo resultado = new CourrierInfo();

                resultado.NR_PROCESSO = carregarDados.Rows[0]["NR_PROCESSO"].ToString();
                resultado.NM_RAZAO = carregarDados.Rows[0]["CLIENTE"].ToString();
                resultado.NR_BL = carregarDados.Rows[0]["HOUSE"].ToString();
                resultado.NR_BL_MASTER = carregarDados.Rows[0]["MASTER"].ToString();
                resultado.DT_RECEBIMENTO_MBL = carregarDados.Rows[0]["DT_RECEBIMENTO_MBL"].ToString();
                resultado.CD_RASTREAMENTO_MBL = carregarDados.Rows[0]["CD_RASTREAMENTO_MBL"].ToString();
                resultado.DT_RECEBIMENTO_HBL = carregarDados.Rows[0]["DT_RECEBIMENTO_HBL"].ToString();
                resultado.CD_RASTREAMENTO_HBL = carregarDados.Rows[0]["CD_RASTREAMENTO_HBL"].ToString();
                resultado.DT_RETIRADA_COURRIER = carregarDados.Rows[0]["DT_RETIRADA_COURRIER"].ToString();
                resultado.NM_RETIRADO_POR_COURRIER = carregarDados.Rows[0]["NM_RETIRADO_POR_COURRIER"].ToString();
                resultado.NR_FATURA_COURRIER = carregarDados.Rows[0]["NR_FATURA_COURRIER"].ToString();
                resultado.DT_RETIRADA_PERSONAL = carregarDados.Rows[0]["DT_RETIRADA_PERSONAL"].ToString();
                resultado.FL_TROCA = carregarDados.Rows[0]["FL_TROCA"].ToString();
                resultado.ID_USUARIO = "56";

                return JsonConvert.SerializeObject(resultado);
			}
			else
			{
                return JsonConvert.SerializeObject("erro");
			}
        }

        [WebMethod(EnableSession = true)]
        public string editarCourrier(CourrierInfo dadosEdit)
        {
            string SQL;
            SQL = "UPDATE TB_BL SET ";
            if (dadosEdit.DT_RECEBIMENTO_HBL == "")
            {
                SQL += "DT_RECEBIMENTO_HBL = NULL, ";
            }
            else
            {
                SQL += "DT_RECEBIMENTO_HBL = '" + dadosEdit.DT_RECEBIMENTO_HBL + "', ";
            }
            SQL += "CD_RASTREAMENTO_HBL = '" + dadosEdit.CD_RASTREAMENTO_HBL + "', ";
            if (dadosEdit.DT_RETIRADA_COURRIER == "")
            {
                SQL += "DT_RETIRADA_COURRIER = NULL, ";
            }
            else
            {
                SQL += "DT_RETIRADA_COURRIER = '" + dadosEdit.DT_RETIRADA_COURRIER + "', ";
            }
            if (dadosEdit.DT_RETIRADA_PERSONAL == "")
            {
                SQL += "DT_RETIRADA_PERSONAL = NULL, ";
            }
            else
            {
                SQL += "DT_RETIRADA_PERSONAL = '" + dadosEdit.DT_RETIRADA_PERSONAL + "', ";
            }
            SQL += "NM_RETIRADO_POR_COURRIER = '" + dadosEdit.NM_RETIRADO_POR_COURRIER + "', ";
            SQL += "NR_FATURA_COURRIER = '" + dadosEdit.NR_FATURA_COURRIER + "', ";
            SQL += "FL_TROCA = '" + dadosEdit.FL_TROCA + "' ";
            SQL += "WHERE ID_BL = '" + dadosEdit.ID_BL + "' ";

            string editCourrier = DBS.ExecuteScalar(SQL);

            SQL = "SELECT ID_BL_MASTER FROM TB_BL WHERE ID_BL = '" + dadosEdit.ID_BL + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string idblmaster = listTable.Rows[0]["ID_BL_MASTER"].ToString();

            SQL = "UPDATE TB_BL SET ";
            if (dadosEdit.DT_RECEBIMENTO_MBL == "")
            {
                SQL += "DT_RECEBIMENTO_MBL = NULL, ";
            }
            else
            {
                SQL += "DT_RECEBIMENTO_MBL = '" + dadosEdit.DT_RECEBIMENTO_MBL + "', ";
            }
            if (dadosEdit.DT_RETIRADA_PERSONAL == "")
            {
                SQL += "DT_RETIRADA_PERSONAL = NULL, ";
            }
            else
            {
                SQL += "DT_RETIRADA_PERSONAL = '" + dadosEdit.DT_RETIRADA_PERSONAL + "', ";
            }
            SQL += "CD_RASTREAMENTO_MBL = '" + dadosEdit.CD_RASTREAMENTO_MBL + "' ";
            SQL += "WHERE ID_BL = '" + idblmaster + "' ";

            string editCourrierMaster = DBS.ExecuteScalar(SQL);

            return "1";

        }

        [WebMethod(EnableSession = true)]
        public string editarCourrierPersonal(CourrierInfo dadosEdit)
        {
            string SQL;
            SQL = "UPDATE TB_BL SET ";
            if (dadosEdit.DT_RETIRADA_COURRIER == "")
            {
                SQL += "DT_RETIRADA_COURRIER = NULL, ";
            }
            else
            {
                SQL += "DT_RETIRADA_COURRIER = '" + dadosEdit.DT_RETIRADA_COURRIER + "', ";
            }
            SQL += "NM_RETIRADO_POR_COURRIER = '" + dadosEdit.NM_RETIRADO_POR_COURRIER + "' ";
            SQL += "WHERE ID_BL = '" + dadosEdit.ID_BL + "' ";

            string editCourrier = DBS.ExecuteScalar(SQL);

            return "1";

        }



        [WebMethod(EnableSession = true)]
        public string listarInvoices(string dataI, string dataF, string nota, string filter)
        {
            string SQL;

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
                    nota = "AND D.NM_RAZAO LIKE '%" + nota + "%' ";
                    break;
                case "2":
                    nota = " AND C.NR_PROCESSO LIKE '%" + nota + "%' ";
                    break;
                case "3":
                    nota = " AND C.NR_BL LIKE '%" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            SQL = "SELECT A.ID_ACCOUNT_INVOICE, A.NR_INVOICE, C.NR_PROCESSO, D.NM_RAZAO AS AGENTE, ";
            SQL += "ISNULL(FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy'),'') AS DT_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ";
            SQL += "ISNULL(FORMAT(C.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO, '') NM_TIPO_PAGAMENTO ";
            SQL += "FROM TB_BL A ";
            SQL += "JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO ";
            SQL += "WHERE A.ID_BL = C.ID_BL_MASTER),'') AS FRETE_MASTER, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO, '') NM_TIPO_PAGAMENTO ";
            SQL += "FROM TB_BL A ";
            SQL += "JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO ";
            SQL += "WHERE A.ID_BL = C.ID_BL),'') AS FRETE_HOUSE, ";
            SQL += "F.CD_SIGLA AS ORIGEM, ";
            SQL += "F1.CD_SIGLA AS DESTINO, ";
            SQL += "C.NR_BL AS HBL, ";
            SQL += "M.NR_BL AS MBL, ";
            SQL += "G.NM_TIPO_ESTUFAGEM AS ESTUFAGEM, ";
            SQL += "ISNULL(CONVERT(VARCHAR,MAX(H.VL_CAMBIO)),'') VL_CAMBIO, ";
            SQL += "MAX(ISNULL(FORMAT(I.DT_LIQUIDACAO,'dd/MM/yyyy'),'')) DT_LIQUIDACAO ";
            SQL += "FROM TB_ACCOUNT_INVOICE A ";
            SQL += "JOIN TB_ACCOUNT_INVOICE_ITENS B ON A.ID_ACCOUNT_INVOICE = B.ID_ACCOUNT_INVOICE ";
            SQL += "JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
            SQL += "JOIN TB_BL M ON C.ID_BL_MASTER=M.ID_BL ";
            SQL += "JOIN TB_PARCEIRO D ON A.ID_PARCEIRO_AGENTE = D.ID_PARCEIRO ";
            SQL += "JOIN TB_ITEM_DESPESA E ON B.ID_ITEM_DESPESA = E.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_PORTO F ON C.ID_PORTO_ORIGEM = F.ID_PORTO ";
            SQL += "JOIN TB_PORTO F1 ON C.ID_PORTO_DESTINO = F1.ID_PORTO ";
            SQL += "JOIN TB_TIPO_ESTUFAGEM G ON C.ID_TIPO_ESTUFAGEM = G.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS H ON C.ID_BL = H.ID_BL AND H.ID_ITEM_DESPESA = 14 ";
            SQL += "LEFT JOIN TB_CONTA_PAGAR_RECEBER I ON I.ID_CONTA_PAGAR_RECEBER = H.ID_CONTA_PAGAR_RECEBER ";
            SQL += "WHERE CONVERT(DATE,A.DT_VENCIMENTO,103) BETWEEN CONVERT(DATE,'" + dataI + "',103) AND CONVERT(DATE,'" + dataF + "',103) ";
            SQL += "" + nota + "";
            SQL += "GROUP BY A.ID_ACCOUNT_INVOICE, A.NR_INVOICE, C.NR_PROCESSO, D.NM_RAZAO, C.DT_EMBARQUE, ";
            SQL += "C.DT_PREVISAO_CHEGADA, C.DT_CHEGADA, C.ID_BL_MASTER, C.ID_BL, F.CD_SIGLA, F1.CD_SIGLA, G.NM_TIPO_ESTUFAGEM, ";
            SQL += "C.NR_BL, M.NR_BL ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }



        [WebMethod(EnableSession = true)]
        public string listarInvoicesQuitadas(string dataI, string dataF, string nota, string filter)
        {
            string SQL;

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
                    nota = "WHERE NM_AGENTE LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }
            SQL = "select ISNULL(NM_AGENTE,'') AS NM_AGENTE, ISNULL(FORMAT(DT_QUITACAO,'dd/MM/yyyy'),'') AS DT_QUITACAO, ISNULL(NR_CONTRATO,'') AS NR_CONTRATO, ISNULL(NR_MBL,'') AS NR_MBL, ISNULL(NR_HBL,'') AS NR_HBL, ISNULL(NR_PROCESSO,'') AS NR_PROCESSO, ";
            SQL += "ISNULL(NR_INVOICE,'') AS NR_INVOICE, ISNULL(TP_INVOICE,'') AS TP_INVOICE, ISNULL(FORMAT(DT_INVOICE,'dd/MM/yyyy'),'') AS DT_INVOICE, ISNULL(convert(varchar,TX_INVOICE),'') AS TX_INVOICE, CONVERT(DECIMAL(13,3), VL_INVOICE) AS VLINVOICE, ISNULL(SIGLA_MOEDA,'') AS SIGLA, CONVERT(DECIMAL(13,3),VL_INVOICE_BR) AS VLINVOICEBRL, ";
            SQL += "ISNULL(convert(varchar,TX_RECEBIMENTO),'') AS TX_RECEBIMENTO, ISNULL(FORMAT(DT_RECEBIMENTO,'dd/MM/yyyy'),'') AS DT_RECEBIMENTO, ISNULL(NM_IMPORTADOR,'') AS NM_IMPORTADOR, ISNULL(NM_TIPO_ESTUFAGEM, '') AS TPESTUFAGEM from FN_INVOICES_QUITADAS( '" + dataI + "', '" + dataF + "') ";
            SQL += "" + nota + "";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string imprimirInvoice(string dataI, string dataF, string[] invoices)
        {
            string SQL;

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = anoI + '-' + mesI + '-' + diaI;
            dataF = anoF + '-' + mesF + '-' + diaF;

            SQL = "SELECT ISNULL(AI.NR_INVOICE,'') AS NR_INVOICE, ISNULL(REPLACE(CONVERT(VARCHAR,G.VL_TAXA_CAMBIO,103),'.',','),'') VL_TAXA_CAMBIO, ISNULL(FORMAT(G.DT_LIQUIDACAO,'dd/MM/yyyy'),'') DT_LIQUIDACAO,  ISNULL(C.NR_PROCESSO,'') AS NR_PROCESSO, ";
            SQL += "ISNULL(C.NR_BL,'') as HBL, ISNULL(M.NR_BL,'') AS MBL, ISNULL(CLIENTE.NM_RAZAO,'') AS CLIENTE, ISNULL(AGENTE.NM_RAZAO,'') AS AGENTE, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = M.ID_BL),'') AS FRETE_MASTER, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = C.ID_BL),'') AS FRETE_HOUSE, ";
            SQL += "ISNULL(ORIGEM.CD_PORTO,'') as ORIGEM, ISNULL(DESTINO.CD_PORTO,'') AS DESTINO, ";
            SQL += "ISNULL(H.NM_TIPO_ESTUFAGEM,'') AS ESTUFAGEM, ";
            SQL += "ISNULL(TRANSPORTADOR.NM_RAZAO,'') AS TRANSPORTADOR,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_EMBARQUE,'dd/MM/yyyy'),'') AS DT_PREVISAO_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy'),'') AS DT_EMBARQUE,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA,  ";
            SQL += "ISNULL(FORMAT(C.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA ";
            SQL += "FROM TB_ACCOUNT_INVOICE AI ";
            SQL += " JOIN TB_BL C ON AI.ID_BL = C.ID_BL_MASTER ";
            SQL += " JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += " JOIN TB_PARCEIRO CLIENTE ON C.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PARCEIRO AGENTE ON AI.ID_PARCEIRO_AGENTE = AGENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PORTO ORIGEM ON C.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += " JOIN TB_PORTO DESTINO ON C.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += " JOIN TB_PARCEIRO TRANSPORTADOR ON C.ID_PARCEIRO_TRANSPORTADOR = TRANSPORTADOR.ID_PARCEIRO ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO_ITENS F ON AI.ID_ACCOUNT_INVOICE = F.ID_ACCOUNT_INVOICE ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO G ON F.ID_ACCOUNT_FECHAMENTO = G.ID_ACCOUNT_FECHAMENTO ";
            SQL += " LEFT JOIN TB_TIPO_ESTUFAGEM H ON C.ID_TIPO_ESTUFAGEM=H.ID_TIPO_ESTUFAGEM ";
            SQL += " WHERE ID_ACCOUNT_TIPO_INVOICE = 1 ";
            SQL += " AND AI.ID_ACCOUNT_INVOICE IN ( ";
            for (int i = 0; i < invoices.Length; i++)
            {
                if (i == invoices.Length - 1)
                {
                    SQL += "" + invoices[i] + " ";
                }
                else
                {
                    SQL += "" + invoices[i] + ", ";
                }
            }
            SQL += " ) UNION ";
            SQL += "SELECT ISNULL(AI.NR_INVOICE,'') AS NR_INVOICE, ISNULL(REPLACE(CONVERT(VARCHAR,G.VL_TAXA_CAMBIO,103),'.',','),'') VL_TAXA_CAMBIO, ISNULL(FORMAT(G.DT_LIQUIDACAO,'dd/MM/yyyy'),'') DT_LIQUIDACAO, ISNULL(C.NR_PROCESSO,'') AS NR_PROCESSO, ";
            SQL += "ISNULL(C.NR_BL,'') as HBL, ISNULL(M.NR_BL,'') AS MBL, ISNULL(CLIENTE.NM_RAZAO,'') AS CLIENTE, ISNULL(AGENTE.NM_RAZAO,'') AS AGENTE, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = M.ID_BL),'') AS FRETE_MASTER, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = C.ID_BL),'') AS FRETE_HOUSE, ";
            SQL += "ISNULL(ORIGEM.CD_PORTO,'') as ORIGEM, ISNULL(DESTINO.CD_PORTO,'') AS DESTINO, ";
            SQL += "ISNULL(H.NM_TIPO_ESTUFAGEM,'') AS ESTUFAGEM, ";
            SQL += "ISNULL(TRANSPORTADOR.NM_RAZAO,'') AS TRANSPORTADOR,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_EMBARQUE,'dd/MM/yyyy'),'') AS DT_PREVISAO_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy'),'') AS DT_EMBARQUE,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA,  ";
            SQL += "ISNULL(FORMAT(C.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA ";
            SQL += "FROM TB_ACCOUNT_INVOICE AI ";
            SQL += " JOIN TB_BL C ON AI.ID_BL = C.ID_BL ";
            SQL += " JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += " JOIN TB_PARCEIRO CLIENTE ON C.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PARCEIRO AGENTE ON AI.ID_PARCEIRO_AGENTE = AGENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PORTO ORIGEM ON C.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += " JOIN TB_PORTO DESTINO ON C.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += " JOIN TB_PARCEIRO TRANSPORTADOR ON C.ID_PARCEIRO_TRANSPORTADOR = TRANSPORTADOR.ID_PARCEIRO ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO_ITENS F ON AI.ID_ACCOUNT_INVOICE = F.ID_ACCOUNT_INVOICE ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO G ON F.ID_ACCOUNT_FECHAMENTO = G.ID_ACCOUNT_FECHAMENTO ";
            SQL += " LEFT JOIN TB_TIPO_ESTUFAGEM H ON C.ID_TIPO_ESTUFAGEM=H.ID_TIPO_ESTUFAGEM ";
            SQL += " WHERE ID_ACCOUNT_TIPO_INVOICE = 2 ";
            SQL += " AND AI.ID_ACCOUNT_INVOICE IN ( ";
            for (int i = 0; i < invoices.Length; i++)
            {
                if (i == invoices.Length - 1)
                {
                    SQL += "" + invoices[i] + " ";
                }
                else
                {
                    SQL += "" + invoices[i] + ", ";
                }
            }
            SQL += " ) UNION ";
            SQL += "SELECT ISNULL(AI.NR_INVOICE,'') AS NR_INVOICE, ISNULL(REPLACE(CONVERT(VARCHAR,G.VL_TAXA_CAMBIO,103),'.',','),'') VL_TAXA_CAMBIO, ISNULL(FORMAT(G.DT_LIQUIDACAO,'dd/MM/yyyy'),'') DT_LIQUIDACAO, ISNULL(C.NR_PROCESSO,'') AS NR_PROCESSO, ";
            SQL += "ISNULL(C.NR_BL,'') as HBL, ISNULL(M.NR_BL,'') AS MBL, ISNULL(CLIENTE.NM_RAZAO,'') AS CLIENTE, ISNULL(AGENTE.NM_RAZAO,'') AS AGENTE, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = M.ID_BL),'') AS FRETE_MASTER, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = C.ID_BL),'') AS FRETE_HOUSE, ";
            SQL += "ISNULL(ORIGEM.CD_PORTO,'') as ORIGEM, ISNULL(DESTINO.CD_PORTO,'') AS DESTINO, ";
            SQL += "ISNULL(H.NM_TIPO_ESTUFAGEM,'') AS ESTUFAGEM, ";
            SQL += "ISNULL(TRANSPORTADOR.NM_RAZAO,'') AS TRANSPORTADOR,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_EMBARQUE,'dd/MM/yyyy'),'') AS DT_PREVISAO_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy'),'') AS DT_EMBARQUE,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA,  ";
            SQL += "ISNULL(FORMAT(C.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA ";
            SQL += "FROM TB_ACCOUNT_INVOICE AI ";
            SQL += " JOIN TB_BL C ON AI.ID_BL = C.ID_BL ";
            SQL += " JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += " JOIN TB_PARCEIRO CLIENTE ON C.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PARCEIRO AGENTE ON AI.ID_PARCEIRO_AGENTE = AGENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PORTO ORIGEM ON C.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += " JOIN TB_PORTO DESTINO ON C.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += " JOIN TB_PARCEIRO TRANSPORTADOR ON C.ID_PARCEIRO_TRANSPORTADOR = TRANSPORTADOR.ID_PARCEIRO ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO_ITENS F ON AI.ID_ACCOUNT_INVOICE = F.ID_ACCOUNT_INVOICE ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO G ON F.ID_ACCOUNT_FECHAMENTO = G.ID_ACCOUNT_FECHAMENTO ";
            SQL += " LEFT JOIN TB_TIPO_ESTUFAGEM H ON C.ID_TIPO_ESTUFAGEM=H.ID_TIPO_ESTUFAGEM ";
            SQL += " WHERE ID_ACCOUNT_TIPO_INVOICE = 1 ";
            SQL += " AND AI.ID_ACCOUNT_INVOICE IN ( ";
            for (int i = 0; i < invoices.Length; i++)
            {
                if (i == invoices.Length - 1)
                {
                    SQL += "" + invoices[i] + " ";
                }
                else
                {
                    SQL += "" + invoices[i] + ", ";
                }
            }
            SQL += " ) ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }



        [WebMethod(EnableSession = true)]
        public string imprimirInvoiceExp(string dataI, string dataF, string[] invoices)
        {
            string SQL;

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = anoI + '-' + mesI + '-' + diaI;
            dataF = anoF + '-' + mesF + '-' + diaF;

            SQL = "SELECT ISNULL(AI.NR_INVOICE,'') AS NR_INVOICE, ISNULL(CONVERT(VARCHAR,G.VL_TAXA_CAMBIO,103),'') VL_TAXA_CAMBIO, ISNULL(FORMAT(G.DT_LIQUIDACAO,'dd/MM/yyyy'),'') DT_LIQUIDACAO,  ISNULL(C.NR_PROCESSO,'') AS NR_PROCESSO, ";
            SQL += "ISNULL(C.NR_BL,'') as HBL, ISNULL(M.NR_BL,'') AS MBL, ISNULL(CLIENTE.NM_RAZAO,'') AS CLIENTE, ISNULL(AGENTE.NM_RAZAO,'') AS AGENTE, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = M.ID_BL),'') AS FRETE_MASTER, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = C.ID_BL),'') AS FRETE_HOUSE, ";
            SQL += "ISNULL(ORIGEM.CD_PORTO,'') as ORIGEM, ISNULL(DESTINO.CD_PORTO,'') AS DESTINO, ";
            SQL += "ISNULL(H.NM_TIPO_ESTUFAGEM,'') AS ESTUFAGEM, ";
            SQL += "ISNULL(TRANSPORTADOR.NM_RAZAO,'') AS TRANSPORTADOR,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_EMBARQUE,'dd/MM/yyyy'),'') AS DT_PREVISAO_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy'),'') AS DT_EMBARQUE,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA,  ";
            SQL += "ISNULL(FORMAT(C.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA ";
            SQL += "FROM TB_ACCOUNT_INVOICE AI ";
            SQL += " JOIN TB_BL C ON AI.ID_BL = C.ID_BL_MASTER ";
            SQL += " JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += " JOIN TB_PARCEIRO CLIENTE ON C.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PARCEIRO AGENTE ON AI.ID_PARCEIRO_AGENTE = AGENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PORTO ORIGEM ON C.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += " JOIN TB_PORTO DESTINO ON C.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += " JOIN TB_PARCEIRO TRANSPORTADOR ON C.ID_PARCEIRO_TRANSPORTADOR = TRANSPORTADOR.ID_PARCEIRO ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO_ITENS F ON AI.ID_ACCOUNT_INVOICE = F.ID_ACCOUNT_INVOICE ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO G ON F.ID_ACCOUNT_FECHAMENTO = G.ID_ACCOUNT_FECHAMENTO ";
            SQL += " LEFT JOIN TB_TIPO_ESTUFAGEM H ON C.ID_TIPO_ESTUFAGEM=H.ID_TIPO_ESTUFAGEM ";
            for (int i = 0; i < invoices.Length; i++)
            {
                if (i == 0)
                {
                    SQL += " WHERE ID_ACCOUNT_TIPO_INVOICE = 1 AND (AI.ID_ACCOUNT_INVOICE = " + invoices[i] + " ";
                }
                else
                {
                    SQL += " OR AI.ID_ACCOUNT_INVOICE = " + invoices[i] + " ";
                }
            }
            SQL += " ) UNION ";
            SQL += "SELECT ISNULL(AI.NR_INVOICE,'') AS NR_INVOICE, ISNULL(CONVERT(VARCHAR,G.VL_TAXA_CAMBIO,103),'') VL_TAXA_CAMBIO, ISNULL(FORMAT(G.DT_LIQUIDACAO,'dd/MM/yyyy'),'') DT_LIQUIDACAO, ISNULL(C.NR_PROCESSO,'') AS NR_PROCESSO, ";
            SQL += "ISNULL(C.NR_BL,'') as HBL, ISNULL(M.NR_BL,'') AS MBL, ISNULL(CLIENTE.NM_RAZAO,'') AS CLIENTE, ISNULL(AGENTE.NM_RAZAO,'') AS AGENTE, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = M.ID_BL),'') AS FRETE_MASTER, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = C.ID_BL),'') AS FRETE_HOUSE, ";
            SQL += "ISNULL(ORIGEM.CD_PORTO,'') as ORIGEM, ISNULL(DESTINO.CD_PORTO,'') AS DESTINO, ";
            SQL += "ISNULL(H.NM_TIPO_ESTUFAGEM,'') AS ESTUFAGEM, ";
            SQL += "ISNULL(TRANSPORTADOR.NM_RAZAO,'') AS TRANSPORTADOR,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_EMBARQUE,'dd/MM/yyyy'),'') AS DT_PREVISAO_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy'),'') AS DT_EMBARQUE,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA,  ";
            SQL += "ISNULL(FORMAT(C.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA ";
            SQL += "FROM TB_ACCOUNT_INVOICE AI ";
            SQL += " JOIN TB_BL C ON AI.ID_BL = C.ID_BL ";
            SQL += " JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += " JOIN TB_PARCEIRO CLIENTE ON C.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PARCEIRO AGENTE ON AI.ID_PARCEIRO_AGENTE = AGENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PORTO ORIGEM ON C.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += " JOIN TB_PORTO DESTINO ON C.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += " JOIN TB_PARCEIRO TRANSPORTADOR ON C.ID_PARCEIRO_TRANSPORTADOR = TRANSPORTADOR.ID_PARCEIRO ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO_ITENS F ON AI.ID_ACCOUNT_INVOICE = F.ID_ACCOUNT_INVOICE ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO G ON F.ID_ACCOUNT_FECHAMENTO = G.ID_ACCOUNT_FECHAMENTO ";
            SQL += " LEFT JOIN TB_TIPO_ESTUFAGEM H ON C.ID_TIPO_ESTUFAGEM=H.ID_TIPO_ESTUFAGEM ";
            for (int i = 0; i < invoices.Length; i++)
            {
                if (i == 0)
                {
                    SQL += " WHERE ID_ACCOUNT_TIPO_INVOICE = 2 AND ( AI.ID_ACCOUNT_INVOICE = " + invoices[i] + " ";
                }
                else
                {
                    SQL += " OR AI.ID_ACCOUNT_INVOICE = " + invoices[i] + " ";
                }
            }
            SQL += " ) UNION ";
            SQL += "SELECT ISNULL(AI.NR_INVOICE,'') AS NR_INVOICE, ISNULL(CONVERT(VARCHAR,G.VL_TAXA_CAMBIO,103),'') VL_TAXA_CAMBIO, ISNULL(FORMAT(G.DT_LIQUIDACAO,'dd/MM/yyyy'),'') DT_LIQUIDACAO, ISNULL(C.NR_PROCESSO,'') AS NR_PROCESSO, ";
            SQL += "ISNULL(C.NR_BL,'') as HBL, ISNULL(M.NR_BL,'') AS MBL, ISNULL(CLIENTE.NM_RAZAO,'') AS CLIENTE, ISNULL(AGENTE.NM_RAZAO,'') AS AGENTE, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = M.ID_BL),'') AS FRETE_MASTER, ";
            SQL += "ISNULL((SELECT ISNULL(B.NM_TIPO_PAGAMENTO,'') NM_TIPO_PAGAMENTO FROM TB_BL A JOIN TB_TIPO_PAGAMENTO B ON A.ID_TIPO_PAGAMENTO = B.ID_TIPO_PAGAMENTO WHERE A.ID_BL = C.ID_BL),'') AS FRETE_HOUSE, ";
            SQL += "ISNULL(ORIGEM.CD_PORTO,'') as ORIGEM, ISNULL(DESTINO.CD_PORTO,'') AS DESTINO, ";
            SQL += "ISNULL(H.NM_TIPO_ESTUFAGEM,'') AS ESTUFAGEM, ";
            SQL += "ISNULL(TRANSPORTADOR.NM_RAZAO,'') AS TRANSPORTADOR,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_EMBARQUE,'dd/MM/yyyy'),'') AS DT_PREVISAO_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(C.DT_EMBARQUE,'dd/MM/yyyy'),'') AS DT_EMBARQUE,  ";
            SQL += "ISNULL(FORMAT(C.DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA,  ";
            SQL += "ISNULL(FORMAT(C.DT_CHEGADA,'dd/MM/yyyy'),'') AS DT_CHEGADA ";
            SQL += "FROM TB_ACCOUNT_INVOICE AI ";
            SQL += " JOIN TB_BL C ON AI.ID_BL = C.ID_BL ";
            SQL += " JOIN TB_BL M ON C.ID_BL_MASTER = M.ID_BL ";
            SQL += " JOIN TB_PARCEIRO CLIENTE ON C.ID_PARCEIRO_CLIENTE = CLIENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PARCEIRO AGENTE ON AI.ID_PARCEIRO_AGENTE = AGENTE.ID_PARCEIRO ";
            SQL += " JOIN TB_PORTO ORIGEM ON C.ID_PORTO_ORIGEM = ORIGEM.ID_PORTO ";
            SQL += " JOIN TB_PORTO DESTINO ON C.ID_PORTO_DESTINO = DESTINO.ID_PORTO ";
            SQL += " JOIN TB_PARCEIRO TRANSPORTADOR ON C.ID_PARCEIRO_TRANSPORTADOR = TRANSPORTADOR.ID_PARCEIRO ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO_ITENS F ON AI.ID_ACCOUNT_INVOICE = F.ID_ACCOUNT_INVOICE ";
            SQL += " LEFT JOIN TB_ACCOUNT_FECHAMENTO G ON F.ID_ACCOUNT_FECHAMENTO = G.ID_ACCOUNT_FECHAMENTO ";
            SQL += " LEFT JOIN TB_TIPO_ESTUFAGEM H ON C.ID_TIPO_ESTUFAGEM=H.ID_TIPO_ESTUFAGEM ";
            for (int i = 0; i < invoices.Length; i++)
            {
                if (i == 0)
                {
                    SQL += " WHERE ID_ACCOUNT_TIPO_INVOICE = 1 AND ( AI.ID_ACCOUNT_INVOICE = " + invoices[i] + " ";
                }
                else
                {
                    SQL += " OR AI.ID_ACCOUNT_INVOICE = " + invoices[i] + " ";
                }
            }
            SQL += " ) ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] conf = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    conf[i] += listTable.Rows[i]["NR_PROCESSO"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["NR_INVOICE"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["DT_LIQUIDACAO"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["VL_TAXA_CAMBIO"].ToString().Replace(".", ",") + ";";
                    conf[i] += listTable.Rows[i]["MBL"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["FRETE_MASTER"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["HBL"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["FRETE_HOUSE"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["ESTUFAGEM"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["ORIGEM"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["DESTINO"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["DT_EMBARQUE"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["DT_PREVISAO_CHEGADA"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["DT_CHEGADA"].ToString() + ";";
                }
                return JsonConvert.SerializeObject(conf);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }
        }

        

[WebMethod(EnableSession = true)]
        public string listarDemonstrativoRateio(string text, string filter)
        {
            string SQL;

            switch (filter)
            {
                case "1":
                    text = " WHERE C.NR_BL LIKE '" + text + "%' ";
                    break;
                case "2":
                    text = " WHERE C.NR_BL LIKE '" + text + "%' ";
                    break;
                default:
                    text = "";
                    break;
            }

            SQL = "SELECT C.NR_BL AS MBL, FORMAT(C.DT_CHEGADA,'dd/MM/yyyy') AS CHEGADA, C.ID_BL FROM TB_BL A ";
            SQL += "INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON A.ID_BL = B.ID_BL ";
            SQL += "INNER JOIN TB_BL C ON A.ID_BL_MASTER = C.ID_BL ";
            SQL += " " + text + " ";
            SQL += "GROUP BY C.NR_BL, C.DT_CHEGADA, C.ID_BL ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string indiceItemDespesa(string blmaster)
        {
            string SQL;

            SQL = "SELECT distinct(D.NM_ITEM_DESPESA) AS INDICEITEM FROM TB_BL A ";
            SQL += "INNER JOIN TB_BL B ON A.ID_BL_MASTER = B.ID_BL ";
            SQL += "INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS C ON A.ID_BL = C.ID_BL ";
            SQL += "INNER JOIN TB_CONTA_PAGAR_RECEBER E ON C.ID_CONTA_PAGAR_RECEBER = E.ID_CONTA_PAGAR_RECEBER ";
            SQL += "LEFT JOIN TB_ITEM_DESPESA D ON C.ID_ITEM_DESPESA = D.ID_ITEM_DESPESA ";
            SQL += "WHERE E.DT_CANCELAMENTO IS NULL AND E.CD_PR = 'P' AND A.ID_BL_MASTER = '" + blmaster + "' ";


            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string FCADebitCreditInvoice(string dataI, string dataF, string filter, string text) 
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
                    text = " AND F.NM_RAZAO LIKE '%" + text + "%' ";
                    break;
                case "2":
                    text = " AND C.NR_BL LIKE '%" + text + "%' ";
                    break;
                case "3":
                    text = " AND C.NR_PROCESSO LIKE '" + text + "%' ";
                    break;
                case "4":
                    text = " AND B.NR_INVOICE LIKE '%" + text + "%' ";
                    break;
                default:
                    text = "";
                    break;
            }

            SQL = "SELECT B.NR_INVOICE, G.NM_ACCOUNT_TIPO_INVOICE, D.NM_ACCOUNT_TIPO_EMISSOR, ";
            SQL += "FORMAT(B.DT_INVOICE,'dd/MM/yyyy') AS DT_INVOICE, FORMAT(B.DT_VENCIMENTO,'dd/MM/yyyy') AS DT_VENCIMENTO, C.NR_PROCESSO , C.NR_BL, F.NM_RAZAO, ";
            SQL += "E.NM_ACCOUNT_TIPO_FATURA, A.VL_TAXA, H.SIGLA_MOEDA ";
            SQL += "FROM TB_ACCOUNT_INVOICE_ITENS A ";
            SQL += "JOIN TB_ACCOUNT_INVOICE B ON A.ID_ACCOUNT_INVOICE = B.ID_ACCOUNT_INVOICE ";
            SQL += "JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
            SQL += "JOIN TB_ACCOUNT_TIPO_EMISSOR D ON B.ID_ACCOUNT_TIPO_EMISSOR = D.ID_ACCOUNT_TIPO_EMISSOR ";
            SQL += "JOIN TB_ACCOUNT_TIPO_FATURA E ON B.ID_ACCOUNT_TIPO_FATURA = E.ID_ACCOUNT_TIPO_FATURA ";
            SQL += "JOIN TB_PARCEIRO F ON B.ID_PARCEIRO_AGENTE = F.ID_PARCEIRO ";
            SQL += "JOIN TB_ACCOUNT_TIPO_INVOICE G ON B.ID_ACCOUNT_TIPO_INVOICE = G.ID_ACCOUNT_TIPO_INVOICE ";
            SQL += "JOIN TB_MOEDA H ON B.ID_MOEDA=H.ID_MOEDA ";
            SQL += "WHERE B.ID_ACCOUNT_TIPO_EMISSOR = 2 ";
            SQL += "AND CONVERT(DATE,B.DT_VENCIMENTO,103) BETWEEN CONVERT(DATE,'"+dataI+"',103) AND CONVERT(DATE,'"+dataF+"',103) ";
            SQL += "AND B.ID_ACCOUNT_INVOICE NOT IN (SELECT ID_ACCOUNT_INVOICE FROM TB_ACCOUNT_FECHAMENTO_ITENS) ";
            SQL += "" + text + "";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string imprimirDemonstrativoRateio(string blmaster)
        {
            string SQL;

            SQL = "SELECT ID_TIPO_ESTUFAGEM FROM TB_BL WHERE ID_BL = '" + blmaster + "' ";
            string tpestufagem = DBS.ExecuteScalar(SQL);

            SQL = "SELECT B.NR_BL, A.NR_PROCESSO AS PROCESSO, CONVERT(DECIMAL(13,3),A.VL_M3) AS CUBAGEM, ";
            SQL += "SUM(C.VL_LIQUIDO) + SUM(C.VL_ISS) AS RATEIO_NF, SUM(C.VL_ISS) AS RATEIO_ISS, ";
            SQL += "SUM(CONVERT(DECIMAL(13,2),C.VL_LIQUIDO)) AS HBL FROM TB_BL A ";
            SQL += "INNER JOIN TB_BL B ON A.ID_BL_MASTER = B.ID_BL ";
            SQL += "INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS C ON A.ID_BL = C.ID_BL ";
            SQL += "INNER JOIN TB_CONTA_PAGAR_RECEBER E ON C.ID_CONTA_PAGAR_RECEBER = E.ID_CONTA_PAGAR_RECEBER ";
            SQL += "LEFT JOIN TB_ITEM_DESPESA D ON C.ID_ITEM_DESPESA = D.ID_ITEM_DESPESA ";
            SQL += "WHERE E.DT_CANCELAMENTO IS NULL AND E.CD_PR = 'P' ";
            if(tpestufagem == "1")
			{
                SQL += "AND B.ID_BL = '" + blmaster + "' AND C.FL_ABATER_ISS = 1 ";
			}
			else
			{
                SQL += "AND B.ID_BL = '" + blmaster + "' AND C.FL_ABATER_ISS = 0 ";
            }
            SQL += "AND ISNULL(E.TP_EXPORTACAO ,'') = '' ";
            SQL += "GROUP BY A.ID_BL_MASTER, A.NR_PROCESSO, A.VL_M3, B.NR_BL ";
            SQL += "ORDER BY A.NR_PROCESSO ";


            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] rateio = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    rateio[i] += listTable.Rows[i]["NR_BL"].ToString() +";";
                    rateio[i] += listTable.Rows[i]["PROCESSO"].ToString() + ";";
                    rateio[i] += listTable.Rows[i]["CUBAGEM"].ToString() + ";";
                    rateio[i] += listTable.Rows[i]["RATEIO_NF"].ToString() + ";";
                    rateio[i] += listTable.Rows[i]["RATEIO_ISS"].ToString() + ";";
                    rateio[i] += listTable.Rows[i]["HBL"].ToString() + ";";
                }
                return JsonConvert.SerializeObject(rateio);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }
        }

        [WebMethod(EnableSession = true)]
        public string imprimirDemonstrativoRateioTotal(string blmaster)
        {
            string SQL;

            SQL = "SELECT ID_BL_MASTER, ID_BL, NR_PROCESSO as PROCESSO, M3 AS CUBAGEM, RATEIO_TOTAL, RATEIO_NF AS RATEIONF, RATEIO_ISS AS RATEIOISS, NF_LIQUIDO AS NFLIQUIDO FROM FN_RATEIO_TOTAIS(" + blmaster + ")";


            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public void OutlookService(string destinatario)
        {
            MailMessage email = new MailMessage();
            email.From = new MailAddress("thiago.amaro@abainfra.com.br");
            email.To.Add(new MailAddress("thiago.amaro@abainfra.com.br"));
            email.Subject = "Teste";
            email.Body = "<!doctypehtml><link href='https://fonts.googleapis.com/css?family=Roboto:400,400i,700,700i'rel=stylesheet><meta charset=utf-8><title></title><body style=font-family:Roboto><table cellpadding=10 cellspacing=0 style=width:770px><tr><td align=middle valign=middle><h1>Follow Up - Importação Marítima</h1></table><br><table cellpadding=10 cellspacing=0 style=width:770px><tr><td align=center valign=middle><img src='https://drive.google.com/file/d/12MnS7MTOpxvtP63VQ_o9zEVOYCCt0h1a/view?usp=sharing'><td align=left style='font-size:12px;' valign=middle><p style='margin:3px'><b>REFERÊNCIA:</b> IM000563<p style='margin:3px'><b>CLIENTE:</b> HAIMO INTERNATIONAL LOGISTICS (SHANGHAI) CO.,LTD<p style='margin:3px'><b>DATA:</b> 22/07/2021<p style='margin:3px'><b>REF. CLIENTE:</b> ALT-2021-46BP-1B</table><br><table cellpadding=1 cellspacing=0 style='width:770px'><tr><td bgcolor=#4f4f4f></table><br><table cellpadding=10 cellspacing=0 style=width:770px><tr><td align=left style='font-size:12px;' valign=middle><p style='margin:3px'><b>EXPORTADOR:</b>	SINO-DG INTERNATIONAL LOGISTICS CO., LTD<p style='margin:3px'><b>HBL:</b>	SHHM2106052<p style='margin:3px'><b>ORIGEM:</b>	SHANGHAI<p style='margin:3px'><b>LOCAL DE RECEBIMENTO:</b><p style='margin:3px'><b>PREV. DE EMBARQUE:</b>	22/07/2021<p style='margin:3px'><b>DATA DE EMBARQUE:</b><p style='margin:3px'><b>MODALIDADE DE FRETE:</b>	PREPAID<p style='margin:3px'><b>MERCADORIA:</b>	QUIMICO NÃO PERIGOSO<p style='margin:3px'><b>QTDE DE VOLUMES:</b>	0,00<td align=left style='font-size:12px;' valign=middle><p style='margin:3px'><b>EXPORTADOR:</b>	SINO-DG INTERNATIONAL LOGISTICS CO., LTD<p style='margin:3px'><b>HBL:</b>	SHHM2106052<p style='margin:3px'><b>ORIGEM:</b>	SHANGHAI<p style='margin:3px'><b>LOCAL DE RECEBIMENTO:</b><p style='margin:3px'><b>PREV. DE EMBARQUE:</b>	22/07/2021<p style='margin:3px'><b>DATA DE EMBARQUE:</b><p style='margin:3px'><b>MODALIDADE DE FRETE:</b>	PREPAID<p style='margin:3px'><b>MERCADORIA:</b>	QUIMICO NÃO PERIGOSO<p style='margin:3px'><b>QTDE DE VOLUMES:</b>	0,00</table><br>";

            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("thiago.amaro@abainfra.com.br", "ta!@#253*");
            smtp.EnableSsl = true;
            smtp.Send(email);
        }

        [WebMethod(EnableSession = true)]
        public string ZerarExportTOTVSDespesa(string dataI, string dataF, string value)
        {
            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_DESPESA = NULL WHERE ID_CONTA_PAGAR_RECEBER = '" + value + "'";
            DBS.ExecuteScalar(SQL);

            return "ok";

        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSNotaDespesa(string dataI, string dataF, string situacao, string nota)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO_TOTVS_DESPESA IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT DISTINCT ID_CONTA_PAGAR_RECEBER, ISNULL(NR_NOTA,'') AS NR_NOTA, TP_NOTA, ISNULL(FORMAT(DT_EMISSAO,'dd/MM/yyyy'),'') AS DT_EMISSAO, VL_NOTA, ";
            SQL += "NM_PARCEIRO, ISNULL(FORMAT(DT_VENCIMENTO,'dd/MM/yyyy'),'') AS DT_VENCIMENTO, ISNULL(FORMAT(DT_EXPORTACAO_TOTVS_DESPESA,'dd/MM/yyyy'),'') AS DT_EXPORTACAO_TOTVS_DESPESA, ";
            SQL += "NR_PROCESSO, ISNULL(NR_REFERENCIA_CLIENTE,'') AS NR_REFERENCIA_CLIENTE ";
            SQL += "FROM dbo.FN_NOTA_DESPESA(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (nota != "")
            {
                SQL += "WHERE NR_NOTA LIKE '" + nota + "%' ";

                if (situacao != "")
                {
                    SQL += "AND " + situacao + "";
                }
            }
            else if (situacao != "")
            {
                SQL += "WHERE " + situacao + "";
            }
            SQL += "ORDER BY NR_NOTA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string integrarTOTVSDespesa(string dataI, string dataF, string situacao, string nota, string values)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dtExportDespesa;
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO_TOTVS_DESPESA IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT DISTINCT ID_CONTA_PAGAR_RECEBER, NR_NOTA ";
            SQL += "FROM dbo.FN_NOTA_DESPESA(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (nota != "")
            {
                SQL += "WHERE NR_NOTA LIKE '" + nota + "%' ";

                if (situacao != "")
                {
                    SQL += "AND " + situacao + "";
                    SQL += "AND ID_CONTA_PAGAR_RECEBER IN ("+ values + ") ";
				}
				else
				{
                    SQL += "AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
                }
            }
            else if (situacao != "")
            {
                SQL += "WHERE " + situacao + "";
                SQL += "AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
			}
			else
			{
                SQL += "WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            SQL += "ORDER BY NR_NOTA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            DataTable listDtExportCredit = new DataTable();

            if (listTable != null)
            {
                string[] idContaPagarReceber = new string[listTable.Rows.Count];

                for (int i = 0; i < idContaPagarReceber.Length; i++)
                {
                    idContaPagarReceber[i] = listTable.Rows[i]["ID_CONTA_PAGAR_RECEBER"].ToString();

                    SQL = "SELECT DT_EXPORTACAO_TOTVS_DESPESA FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "'";
                    listDtExportCredit = DBS.List(SQL);
                    dtExportDespesa = listDtExportCredit.Rows[0]["DT_EXPORTACAO_TOTVS_DESPESA"].ToString();



                    if (dtExportDespesa == "")
                    {
                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_DESPESA = '" + sqlFormattedDate + "' ";
                        SQL += "WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "' ";
                        DBS.ExecuteScalar(SQL);
                    }
                }

                return JsonConvert.SerializeObject("ok");
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSNotaDespesaCLI(string dataI, string dataF, string situacao, string values)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO_TOTVS_DESPESA IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT COD, LOJA, NOME, NREDUZ, PESSOA, TIPO, ENDER, ";
            SQL += "EST, COD_MUN, MUN, NATUREZ, BAIRRO, CEP, ATVDA, TEL, TELEX, FAX, CONTATO, ";
            SQL += "CGC, INSCRI, INSCRM, CONTA, RECISS, CONT, PAIS ";
            SQL += "FROM dbo.FN_NOTA_DESPESA_CLI(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
                //SQL += " AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
			/*else
			{
                SQL += " WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }*/

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string[] cli = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD"].ToString(), 7);
                    cli[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NOME"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NREDUZ"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["PESSOA"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["ENDER"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["EST"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD_MUN"].ToString(), 5);
                    cli[i] += fmtTotvs(listTable.Rows[i]["MUN"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    cli[i] += fmtTotvs(listTable.Rows[i]["BAIRRO"].ToString(), 30);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CEP"].ToString(), 8);
                    cli[i] += fmtTotvs(listTable.Rows[i]["ATVDA"].ToString(), 7);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TEL"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TELEX"].ToString(), 10);
                    cli[i] += fmtTotvs(listTable.Rows[i]["FAX"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONTATO"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CGC"].ToString(), 14);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCRI"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCRM"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["RECISS"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONT"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["PAIS"].ToString(), 5);

                }
                return JsonConvert.SerializeObject(cli);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }


        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSNotaDespesaREC(string dataI, string dataF, string situacao, string values)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO_TOTVS_DESPESA IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "EXECUTE DBO.PR_NOTA_DESPESA_NEGATIVO '"+dataI+"','"+dataF+"' ";
            DBS.ExecuteScalar(SQL);

            SQL = "SELECT PREFIXO, NUM, PARCELA, TIPO, NATUREZ, CLIENTE, LOJA, EMISSAO, VENCTO, VENCREA, ";
            SQL += "VALOR, IRRF, ISS, HIST, INSS, COFINS, CSLL, PIS, CONTROL, ITEMCTA, XPROD, CONTA ";
            SQL += "FROM dbo.FN_NOTA_DESPESA_REC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
                SQL += " AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
			}
			else
			{
                SQL += " WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            SQL += " ORDER BY ITEMCTA ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string[] rec = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    rec[i] += fmtTotvs(listTable.Rows[i]["PREFIXO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NUM"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PARCELA"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CLIENTE"].ToString(), 7);
                    rec[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["EMISSAO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCTO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCREA"].ToString(), 10);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["VALOR"].ToString(), 17, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["IRRF"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["ISS"].ToString(), 14, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["HIST"].ToString(), 40);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["INSS"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["COFINS"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["CSLL"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["PIS"].ToString(), 14, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CONTROL"].ToString(), 1);
                    rec[i] += fmtTotvs(listTable.Rows[i]["ITEMCTA"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["XPROD"].ToString(), 200);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);

                }
                return JsonConvert.SerializeObject(rec);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }

        }

        [WebMethod(EnableSession = true)]
        public string ZerarExportTOTVSServico(string dataI, string dataF, string value)
        {
            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_SERVICO = NULL WHERE ID_CONTA_PAGAR_RECEBER = '" + value + "'";
            DBS.ExecuteScalar(SQL);

            return "ok";

        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSNotaServico(string dataI, string dataF, string situacao, string notai, string notaf)
        {

            string nota;

            if(notai.ToString() == "" && notaf.ToString() == ""){
                nota = "";
			}else if(notai.ToString() != "" && notaf.ToString() != ""){
                nota = "AND NR_NOTA BETWEEN " + notai + " AND '"+notaf+"' ";
			}else if(notai.ToString() != ""){
                nota = "AND NR_NOTA  >= '" + notai + "' ";
			}else{
                nota = "AND NR_NOTA  <= '" + notaf + "' ";
            }

            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "AND DT_EXPORTACAO_TOTVS_SERVICO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER, ISNULL(NR_NOTA,'') AS NR_NOTA, TP_NOTA, ISNULL(FORMAT(DT_EMISSAO,'dd/MM/yyyy'),'') AS DT_EMISSAO, VL_NOTA, ";
            SQL += "NM_PARCEIRO, ISNULL(FORMAT(DT_VENCIMENTO,'dd/MM/yyyy'),'') AS DT_VENCIMENTO, ISNULL(FORMAT(DT_EXPORTACAO_TOTVS_SERVICO,'dd/MM/yyyy'),'') AS DT_EXPORTACAO_TOTVS_SERVICO, ";
            SQL += "ISNULL(NR_PROCESSO,'') AS NR_PROCESSO, ISNULL(NR_REFERENCIA_CLIENTE,'') AS NR_REFERENCIA_CLIENTE ";
            SQL += "FROM dbo.FN_NOTA_SERVICO(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            SQL += "WHERE NR_NOTA IS NOT NULL ";
            SQL += "" + situacao + "";
            SQL += "" + nota + "";
            SQL += "ORDER BY NR_NOTA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSNotaServicoIntegra(string dataI, string dataF, string situacao, string values)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "WHERE DT_EXPORTACAO_TOTVS_SERVICO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER, ISNULL(NR_NOTA,'') AS NR_NOTA, TP_NOTA, ISNULL(FORMAT(DT_EMISSAO,'dd/MM/yyyy'),'') AS DT_EMISSAO, VL_NOTA, ";
            SQL += "NM_PARCEIRO, ISNULL(FORMAT(DT_VENCIMENTO,'dd/MM/yyyy'),'') AS DT_VENCIMENTO, ISNULL(FORMAT(DT_EXPORTACAO_TOTVS_SERVICO,'dd/MM/yyyy'),'') AS DT_EXPORTACAO_TOTVS_SERVICO, ";
            SQL += "ISNULL(NR_PROCESSO,'') AS NR_PROCESSO, ISNULL(NR_REFERENCIA_CLIENTE,'') AS NR_REFERENCIA_CLIENTE ";
            SQL += "FROM dbo.FN_NOTA_SERVICO(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            SQL += "" + situacao + " ";
            if (situacao != "")
            {
                SQL += "AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            else
            {
                SQL += "WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            SQL += "ORDER BY NR_NOTA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string integrarTOTVSServico(string dataI, string dataF, string situacao, string dado)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dtExportServico;
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "WHERE DT_EXPORTACAO_TOTVS_SERVICO IS NULL ";
                    break;
            }


            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER ";
            SQL += "FROM dbo.FN_NOTA_SERVICO(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            SQL += "" + situacao + " ";
            if (situacao != "")
            {
                SQL += "AND ID_CONTA_PAGAR_RECEBER IN (" + dado + ") ";
            }
            else
            {
                SQL += "WHERE ID_CONTA_PAGAR_RECEBER IN (" + dado + ") ";
            }
            SQL += "ORDER BY NR_NOTA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            DataTable listDtExportCredit = new DataTable();

            if (listTable != null)
            {
                string[] idContaPagarReceber = new string[listTable.Rows.Count];

                for (int i = 0; i < idContaPagarReceber.Length; i++)
                {
                    idContaPagarReceber[i] = listTable.Rows[i]["ID_CONTA_PAGAR_RECEBER"].ToString();

                    SQL = "SELECT DT_EXPORTACAO_TOTVS_SERVICO FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "'";
                    listDtExportCredit = DBS.List(SQL);
                    dtExportServico = listDtExportCredit.Rows[0]["DT_EXPORTACAO_TOTVS_SERVICO"].ToString();



                    if (dtExportServico == "")
                    {
                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_SERVICO = '" + sqlFormattedDate + "' ";
                        SQL += "WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "' ";
                        DBS.ExecuteScalar(SQL);
                    }
                }

                return JsonConvert.SerializeObject("ok");
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSNotaServicoCLI(string dataI, string dataF, string situacao, string values)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = " WHERE DT_EXPORTACAO_TOTVS_SERVICO IS NULL ";
                    break;
            }
            string nota;
            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            /*SQL = "SELECT NR_NOTA ";
            SQL += "FROM dbo.FN_NOTA_SERVICO(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            SQL += "" + situacao + " ";
            if (situacao != "")
            {
                SQL += "AND ID_CONTA_PAGAR_RECEBER = " + values + " ";
            }
            else
            {
                SQL += "WHERE ID_CONTA_PAGAR_RECEBER = " + values + " ";
            }
            SQL += "ORDER BY NR_NOTA ";
            nota = DBS.ExecuteScalar(SQL);*/



            SQL = "SELECT COD, LOJA, NOME, NREDUZ, PESSOA, TIPO, ENDER, ";
            SQL += "EST, COD_MUN, MUN, NATUREZ, BAIRRO, CEP, ATVDA, TEL, TELEX, FAX, CONTATO, ";
            SQL += "CGC, INSCRI, INSCRM, CONTA, RECISS, CONT, PAIS ";
            SQL += "FROM dbo.FN_NOTA_SERVICO_CLI(";
            SQL += "'" + dataI + "','" + dataF + "',''";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " " + situacao + " ";
                SQL += "AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            else
            {
                SQL += "WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] cli = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD"].ToString(), 7);
                    cli[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NOME"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NREDUZ"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["PESSOA"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["ENDER"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["EST"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD_MUN"].ToString(), 5);
                    cli[i] += fmtTotvs(listTable.Rows[i]["MUN"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    cli[i] += fmtTotvs(listTable.Rows[i]["BAIRRO"].ToString(), 30);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CEP"].ToString(), 8);
                    cli[i] += fmtTotvs(listTable.Rows[i]["ATVDA"].ToString(), 7);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TEL"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TELEX"].ToString(), 10);
                    cli[i] += fmtTotvs(listTable.Rows[i]["FAX"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONTATO"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CGC"].ToString(), 14);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCRI"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCRM"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["RECISS"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONT"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["PAIS"].ToString(), 5);
                }
                return JsonConvert.SerializeObject(cli);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }

        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSNotaServicoNOTA(string dataI, string dataF, string situacao, string values)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO_TOTVS_SERVICO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT DOC, SERIE, CLIENTE, LOJA, COND, DUPL, EMISSAO, EST, FRETE, SEGURO, TIPOCLI, ";
            SQL += "VALBRUT, VALIPI, BASEIPI, VALMERC, NFORI, SERIORI, TIPO, ESPEC1, VOLUME1, ICMSRET, PLIQUI, ";
            SQL += "PBRUTO, TRANSP, FILIAL, BASEISS, VALISS, VALFAT, ESPECIE, PREFIXO, BASIMP5, BASIMP6, VALIMP5, VALIMP6, VALINSS, ";
            SQL += "HORA, BASEINS, MOEDA, VALCOFI, VALCSLL, VALPIS, DTDIGIT, RECISS, NFELETR, EMINFE, CREDNFE, CODNFE, TPNFEXP, CLIENT, ";
            SQL += "LOJENT, CHVNFE, TPFRETE, HORNFE, XCNPJ ";
            SQL += "FROM dbo.FN_NOTA_SERVICO_NOTA(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
                SQL += " AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            else
            {
                SQL += " WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] nota = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    nota[i] += fmtTotvs(listTable.Rows[i]["DOC"].ToString(), 9);
                    nota[i] += fmtTotvs(listTable.Rows[i]["SERIE"].ToString(), 3);
                    nota[i] += fmtTotvs(listTable.Rows[i]["CLIENTE"].ToString(), 7);
                    nota[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    nota[i] += fmtTotvs(listTable.Rows[i]["COND"].ToString(), 3);
                    nota[i] += fmtTotvs(listTable.Rows[i]["DUPL"].ToString(), 9);
                    nota[i] += fmtTotvs(listTable.Rows[i]["EMISSAO"].ToString(), 10);
                    nota[i] += fmtTotvs(listTable.Rows[i]["EST"].ToString(), 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["FRETE"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["SEGURO"].ToString(), 14, 2);
                    nota[i] += fmtTotvs(listTable.Rows[i]["TIPOCLI"].ToString(), 1);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALBRUT"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALIPI"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["BASEIPI"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALMERC"].ToString(), 14, 2);
                    nota[i] += fmtTotvs(listTable.Rows[i]["NFORI"].ToString(), 9);
                    nota[i] += fmtTotvs(listTable.Rows[i]["SERIORI"].ToString(), 3);
                    nota[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 1);
                    nota[i] += fmtTotvs(listTable.Rows[i]["ESPEC1"].ToString(), 10);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VOLUME1"].ToString(), 6, 0);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["ICMSRET"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["PLIQUI"].ToString(), 9, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["PBRUTO"].ToString(), 9, 2);
                    nota[i] += fmtTotvs(listTable.Rows[i]["TRANSP"].ToString(), 6);
                    nota[i] += fmtTotvs(listTable.Rows[i]["FILIAL"].ToString(), 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["BASEISS"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALISS"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALFAT"].ToString(), 14, 2);
                    nota[i] += fmtTotvs(listTable.Rows[i]["ESPECIE"].ToString(), 5);
                    nota[i] += fmtTotvs(listTable.Rows[i]["PREFIXO"].ToString(), 3);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["BASIMP5"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["BASIMP6"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALIMP5"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALIMP6"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALINSS"].ToString(), 14, 2);
                    nota[i] += fmtTotvs(listTable.Rows[i]["HORA"].ToString(), 5);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["BASEINS"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["MOEDA"].ToString(), 2, 0);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALCOFI"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALCSLL"].ToString(), 14, 2);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["VALPIS"].ToString(), 14, 2);
                    nota[i] += fmtTotvs(listTable.Rows[i]["DTDIGIT"].ToString(), 10);
                    nota[i] += fmtTotvs(listTable.Rows[i]["RECISS"].ToString(), 1);
                    nota[i] += fmtTotvs(listTable.Rows[i]["NFELETR"].ToString(), 20);
                    nota[i] += fmtTotvs(listTable.Rows[i]["EMINFE"].ToString(), 8);
                    nota[i] += fmtTotvsNum(listTable.Rows[i]["CREDNFE"].ToString(), 16, 2);
                    nota[i] += fmtTotvs(listTable.Rows[i]["CODNFE"].ToString(), 50);
                    nota[i] += fmtTotvs(listTable.Rows[i]["TPNFEXP"].ToString(), 1);
                    nota[i] += fmtTotvs(listTable.Rows[i]["CLIENT"].ToString(), 7);
                    nota[i] += fmtTotvs(listTable.Rows[i]["LOJENT"].ToString(), 2);
                    nota[i] += fmtTotvs(listTable.Rows[i]["CHVNFE"].ToString(), 44);
                    nota[i] += fmtTotvs(listTable.Rows[i]["TPFRETE"].ToString(), 1);
                    nota[i] += fmtTotvs(listTable.Rows[i]["HORNFE"].ToString(), 8);
                    nota[i] += fmtTotvs(listTable.Rows[i]["XCNPJ"].ToString(), 14);
                }
                return JsonConvert.SerializeObject(nota);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }

        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSNotaServicoNOTITE(string dataI, string dataF, string situacao, string values)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO_TOTVS_SERVICO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT FILIAL, COD, UM, SEGUM, QUANT, PRCVEN, TOTAL, VALIPI, VALICM, TES, CF, ";
            SQL += "IPI, PESO, CONTA, PEDIDO, ITEMPV, CLIENTE, LOJA, LOCAL, DOC, EMISSAO, GRUPO, ";
            SQL += "TP, SERIE, CUSTO1, EST, TIPO, NFORI, SERIORI, QTDEDEV, ITEM, CODISS, CLASFIS, BASIMP5, BASIMP6, ";
            SQL += "VALIMP5, VALIMP6, ITEMORI, ALIQINS, ALIQISS, BASEINS, BASEIPI, BASEISS, CCUSTO, ITEMCC, ALQIMP5, DTDIGIT, VALISS, ALQIMP6 ";
            SQL += "FROM dbo.FN_NOTA_SERVICO_NOTITE(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
                SQL += " AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            else
            {
                SQL += " WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] notite = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    notite[i] += fmtTotvs(listTable.Rows[i]["FILIAL"].ToString(), 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["COD"].ToString(), 15);
                    notite[i] += fmtTotvs(listTable.Rows[i]["UM"].ToString(), 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["SEGUM"].ToString(), 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["QUANT"].ToString(), 14, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["PRCVEN"].ToString(), 16, 8);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["TOTAL"].ToString(), 14, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["VALIPI"].ToString(), 14, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["VALICM"].ToString(), 14, 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["TES"].ToString(), 3);
                    notite[i] += fmtTotvs(listTable.Rows[i]["CF"].ToString(), 5);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["IPI"].ToString(), 5, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["PESO"].ToString(), 10, 3);
                    notite[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);
                    notite[i] += fmtTotvs(listTable.Rows[i]["PEDIDO"].ToString(), 6);
                    notite[i] += fmtTotvs(listTable.Rows[i]["ITEMPV"].ToString(), 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["CLIENTE"].ToString(), 7);
                    notite[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["LOCAL"].ToString(), 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["DOC"].ToString(), 9);
                    notite[i] += fmtTotvs(listTable.Rows[i]["EMISSAO"].ToString(), 10);
                    notite[i] += fmtTotvs(listTable.Rows[i]["GRUPO"].ToString(), 4);
                    notite[i] += fmtTotvs(listTable.Rows[i]["TP"].ToString(), 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["SERIE"].ToString(), 3);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["CUSTO1"].ToString(), 14, 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["EST"].ToString(), 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 1);
                    notite[i] += fmtTotvs(listTable.Rows[i]["NFORI"].ToString(), 9);
                    notite[i] += fmtTotvs(listTable.Rows[i]["SERIORI"].ToString(), 3);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["QTDEDEV"].ToString(), 14, 5);
                    notite[i] += fmtTotvs(listTable.Rows[i]["ITEM"].ToString(), 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["CODISS"].ToString(), 8);
                    notite[i] += fmtTotvs(listTable.Rows[i]["CLASFIS"].ToString(), 3);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["BASIMP5"].ToString(), 14, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["BASIMP6"].ToString(), 14, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["VALIMP5"].ToString(), 14, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["VALIMP6"].ToString(), 14, 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["ITEMORI"].ToString(), 4);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["ALIQINS"].ToString(), 5, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["ALIQISS"].ToString(), 5, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["BASEINS"].ToString(), 14, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["BASEIPI"].ToString(), 14, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["BASEISS"].ToString(), 14, 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["CCUSTO"].ToString(), 9);
                    notite[i] += fmtTotvs(listTable.Rows[i]["ITEMCC"].ToString(), 15);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["ALQIMP5"].ToString(), 6, 2);
                    notite[i] += fmtTotvs(listTable.Rows[i]["DTDIGIT"].ToString(), 10);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["VALISS"].ToString(), 14, 2);
                    notite[i] += fmtTotvsNum(listTable.Rows[i]["ALQIMP6"].ToString(), 6, 2);
                }
                return JsonConvert.SerializeObject(notite);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }

        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSNotaServicoREC(string dataI, string dataF, string situacao, string values)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO_TOTVS_SERVICO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;

            SQL = "SELECT PREFIXO, NUM, PARCELA, TIPO, NATUREZ, CLIENTE, LOJA, EMISSAO, VENCTO, VENCREA, ";
            SQL += "VALOR, IRRF, ISS, HIST, INSS, COFINS, CSLL, PIS, CONTROL, ITEMCTA, CONTA ";
            SQL += "FROM dbo.FN_NOTA_SERVICO_REC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
                SQL += " AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            else
            {
                SQL += " WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            SQL += " ORDER BY ITEMCTA ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] rec = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    rec[i] += fmtTotvs(listTable.Rows[i]["PREFIXO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NUM"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PARCELA"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CLIENTE"].ToString(), 7);
                    rec[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["EMISSAO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCTO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCREA"].ToString(), 10);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["VALOR"].ToString(), 17, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["IRRF"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["ISS"].ToString(), 14, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["HIST"].ToString(), 40);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["INSS"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["COFINS"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["CSLL"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["PIS"].ToString(), 14, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CONTROL"].ToString(), 1);
                    rec[i] += fmtTotvs(listTable.Rows[i]["ITEMCTA"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);
                }
                return JsonConvert.SerializeObject(rec);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }

        }

        [WebMethod(EnableSession = true)]
        public string ZerarExportTOTVSCredit(string dataI, string dataF, string value)
        {
            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_CREDIT = NULL WHERE ID_CONTA_PAGAR_RECEBER = '" + value + "'";
            DBS.ExecuteScalar(SQL);

            return "ok";

        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSInvCredit(string dataI, string dataF, string situacao, string nota)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER, NR_CONTRATO, TP_CONTRATO, FORMAT(DT_PAGAMENTO,'dd/MM/yyyy') AS DT_PAGAMENTO, VL_LIQUIDO, ";
            SQL += "NM_PARCEIRO, FORMAT(DT_VENCIMENTO,'dd/MM/yyyy') AS DT_VENCIMENTO, ISNULL(FORMAT(DT_EXPORTACAO,'dd/MM/yyyy HH:mm:ss'),'') AS DT_EXPORTACAO, ";
            SQL += "NR_PROCESSO, NR_REFERENCIA_CLIENTE ";
            SQL += "FROM dbo.FN_INV_CREDIT(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (nota != "")
            {
                SQL += "WHERE NR_CONTRATO LIKE '" + nota + "%' ";

                if (situacao != "")
                {
                    SQL += "AND " + situacao + "";
                }
            }
            else if (situacao != "")
            {
                SQL += "WHERE " + situacao + "";
            }
            SQL += "ORDER BY NR_CONTRATO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string integrarTOTVSCredit(string dataI, string dataF, string situacao, string nota)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dtExportCredit;

            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER ";
            SQL += "FROM dbo.FN_INV_CREDIT(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (nota != "")
            {
                SQL += "WHERE NR_CONTRATO LIKE '" + nota + "%' ";

                if (situacao != "")
                {
                    SQL += "AND " + situacao + "";
                }
            }
            else if (situacao != "")
            {
                SQL += "WHERE " + situacao + "";
            }
            SQL += "ORDER BY NR_CONTRATO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            DataTable listDtExportCredit = new DataTable();

            if (listTable != null)
            {
                string[] idContaPagarReceber = new string[listTable.Rows.Count];

                for (int i = 0; i < idContaPagarReceber.Length; i++)
                {
                    idContaPagarReceber[i] = listTable.Rows[i]["ID_CONTA_PAGAR_RECEBER"].ToString();

                    SQL = "SELECT DT_EXPORTACAO_TOTVS_CREDIT FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "'";
                    listDtExportCredit = DBS.List(SQL);
                    dtExportCredit = listDtExportCredit.Rows[0]["DT_EXPORTACAO_TOTVS_CREDIT"].ToString();



                    if (dtExportCredit == "")
                    {
                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_CREDIT = '" + sqlFormattedDate + "' ";
                        SQL += "WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "' ";
                        DBS.ExecuteScalar(SQL);
                    }
                }

                return JsonConvert.SerializeObject("ok");
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSInvCreditCLI(string dataI, string dataF, string situacao)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT COD, LOJA, NOME, NREDUZ, PESSOA, TIPO, ENDER, ";
            SQL += "EST, COD_MUN, MUN, NATUREZ, BAIRRO, CEP, ATVDA, TEL, TELEX, FAX, CONTATO, ";
            SQL += "CGC, INSCRI, INSCRM, CONTA, RECISS, CONT, PAIS ";
            SQL += "FROM dbo.FN_INV_CREDIT_CLI(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
            }
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string[] cli = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD"].ToString(), 7);
                    cli[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NOME"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NREDUZ"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["PESSOA"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["ENDER"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["EST"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD_MUN"].ToString(), 5);
                    cli[i] += fmtTotvs(listTable.Rows[i]["MUN"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    cli[i] += fmtTotvs(listTable.Rows[i]["BAIRRO"].ToString(), 30);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CEP"].ToString(), 8);
                    cli[i] += fmtTotvs(listTable.Rows[i]["ATVDA"].ToString(), 7);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TEL"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TELEX"].ToString(), 10);
                    cli[i] += fmtTotvs(listTable.Rows[i]["FAX"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONTATO"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CGC"].ToString(), 14);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCRI"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCRM"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["RECISS"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONT"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["PAIS"].ToString(), 3);
                }
                return JsonConvert.SerializeObject(cli);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSInvCreditREC(string dataI, string dataF, string situacao)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;

            SQL = "SELECT PREFIXO, NUM, PARCELA, TIPO, NATUREZ, CLIENTE, LOJA, EMISSAO, VENCTO, VENCREA, ";
            SQL += "VALOR, IRRF, ISS, HIST, INSS, COFINS, CSLL, PIS, CONTROL, ITEMCTA, XPROD ";
            SQL += "FROM dbo.FN_INV_CREDIT_REC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
            }
            SQL += " ORDER BY ITEMCTA ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string[] rec = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    rec[i] += fmtTotvs(listTable.Rows[i]["PREFIXO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NUM"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PARCELA"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CLIENTE"].ToString(), 7);
                    rec[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["EMISSAO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCTO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCREA"].ToString(), 10);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["VALOR"].ToString(), 17, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["IRRF"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["ISS"].ToString(), 14, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["HIST"].ToString(), 40);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["INSS"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["COFINS"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["CSLL"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["PIS"].ToString(), 14, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CONTROL"].ToString(), 1);
                    rec[i] += fmtTotvs(listTable.Rows[i]["ITEMCTA"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["XPROD"].ToString(), 200);

                }
                return JsonConvert.SerializeObject(rec);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string ZerarExportTOTVSDebit(string dataI, string dataF, string value)
        {
            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_DEBIT = NULL WHERE ID_CONTA_PAGAR_RECEBER = '" + value + "'";
            DBS.ExecuteScalar(SQL);

            return "ok";

        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSInvDebit(string dataI, string dataF, string situacao, string nota, string filter)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            switch (filter)
            {
                case "1":
                    nota = "NR_PROCESSO LIKE '" + nota + "%' ";
                    break;

                case "2":
                    nota = "ID_BL_MASTER LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER, FORMAT(DT_PAGAMENTO,'dd/MM/yyyy') AS DT_PAGAMENTO, ID_BL_MASTER, NR_PROCESSO, NM_FORNECEDOR, FORMAT(DT_EMISSAO,'dd/MM/yyyy') AS DT_EMISSAO, ";
            SQL += "ISNULL(FORMAT(DT_EXPORTACAO,'dd/MM/yyyy HH:mm:ss'),'') AS DT_EXPORTACAO, ISNULL(NM_CLIENTE,'') AS NM_CLIENTE, NM_ITEM_DESPESA, VL_LIQUIDO, NR_DOCUMENTO ";
            SQL += "FROM dbo.FN_INV_DEBIT(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (situacao != "")
            {
                SQL += "WHERE " + situacao + " ";
                if (nota != "")
                {
                    SQL += "AND " + nota + " ";

                }
            }
            else if (nota != "")
            {
                SQL += "WHERE " + nota + " ";
            }
            SQL += "ORDER BY FORMAT(DT_PAGAMENTO,'dd/MM/yyyy'), NR_PROCESSO";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);


            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string integrarTOTVSDebit(string dataI, string dataF, string situacao, string nota, string filter)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dtExportDebit;

            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            switch (filter)
            {
                case "1":
                    nota = "NR_PROCESSO LIKE '" + nota + "%' ";
                    break;

                case "2":
                    nota = "ID_BL_MASTER LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER ";
            SQL += "FROM dbo.FN_INV_DEBIT(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (situacao != "")
            {
                SQL += "WHERE " + situacao + " ";
                if (nota != "")
                {
                    SQL += "AND " + nota + " ";

                }
            }
            else if (nota != "")
            {
                SQL += "WHERE " + nota + " ";
            }
            SQL += "ORDER BY FORMAT(DT_PAGAMENTO,'dd/MM/yyyy'), NR_PROCESSO";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            DataTable listDtExportDebit = new DataTable();

            if (listTable != null)
            {
                string[] idContaPagarReceber = new string[listTable.Rows.Count];

                for (int i = 0; i < idContaPagarReceber.Length; i++)
                {
                    idContaPagarReceber[i] = listTable.Rows[i]["ID_CONTA_PAGAR_RECEBER"].ToString();

                    SQL = "SELECT DT_EXPORTACAO_TOTVS_DEBIT FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "'";
                    listDtExportDebit = DBS.List(SQL);
                    dtExportDebit = listDtExportDebit.Rows[0]["DT_EXPORTACAO_TOTVS_DEBIT"].ToString();



                    if (dtExportDebit == "")
                    {
                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_DEBIT = '" + sqlFormattedDate + "' ";
                        SQL += "WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "' ";
                        DBS.ExecuteScalar(SQL);
                    }

                }

                return JsonConvert.SerializeObject("ok");
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSInvDebitFornec(string dataI, string dataF, string situacao)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT XGRUPO, COD, LOJA, NOME, NREDUZ, END1, ";
            SQL += "BAIRRO, EST, COD_MUN, CEP, TIPO, CGC, TEL, INSCR, INSCRM, EMAIL, DDD, ";
            SQL += "NATUREZ, CODPAIS, CONTATO, SIMPNAC, PAIS ";
            SQL += "FROM dbo.FN_INV_DEBIT_FORNEC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
            }

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string[] cli = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    cli[i] += fmtTotvs(listTable.Rows[i]["XGRUPO"].ToString(), 3);
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD"].ToString(), 7);
                    cli[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NOME"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NREDUZ"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["END1"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["BAIRRO"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["EST"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD_MUN"].ToString(), 5);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CEP"].ToString(), 8);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CGC"].ToString(), 14);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TEL"].ToString(), 50);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCR"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCRM"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["EMAIL"].ToString(), 30);
                    cli[i] += fmtTotvs(listTable.Rows[i]["DDD"].ToString(), 3);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CODPAIS"].ToString(), 5);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONTATO"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["SIMPNAC"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["PAIS"].ToString(), 5);
                }
                return JsonConvert.SerializeObject(cli);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSInvDebitREC(string dataI, string dataF, string situacao)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;

            SQL = "SELECT FILIAL, PREFIXO, NUM, PARCELA, TIPO, FORNECE, LOJA, NATUREZ, ISNULL(FORMAT(EMISSAO,'dd/MM/yyyy'),'') AS EMISSAO, ";
            SQL += "ISNULL(FORMAT(VENCTO,'dd/MM/yyyy'),'') AS VENCTO, ISNULL(FORMAT(VENCREA,'dd/MM/yyyy'),'') AS VENCREA, ";
            SQL += "VALOR, HIST, ITEMCTA, USERS, XPROD, CONTA, MASTER ";
            SQL += "FROM dbo.FN_INV_DEBIT_REC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
            }
            SQL += " ORDER BY ITEMCTA ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string[] rec = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    rec[i] += fmtTotvs(listTable.Rows[i]["FILIAL"].ToString(), 4);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PREFIXO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NUM"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PARCELA"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["FORNECE"].ToString(), 7);
                    rec[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["EMISSAO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCTO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCREA"].ToString(), 10);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["VALOR"].ToString(), 16, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["HIST"].ToString(), 40);
                    rec[i] += fmtTotvs(listTable.Rows[i]["ITEMCTA"].ToString(), 15);
                    rec[i] += fmtTotvs(listTable.Rows[i]["USERS"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["XPROD"].ToString(), 200);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);
                    rec[i] += fmtTotvs(listTable.Rows[i]["MASTER"].ToString(), 30);

                }
                return JsonConvert.SerializeObject(rec);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSPA(string dataI, string dataF, string situacao, string nota, string filter)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            switch (filter)
            {
                case "1":
                    nota = "NR_PROCESSO LIKE '" + nota + "%' ";
                    break;

                case "2":
                    nota = "ID_BL_MASTER LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER, ISNULL(FORMAT(DT_PAGAMENTO,'dd/MM/yyyy'),'') AS DT_PAGAMENTO , ID_BL_MASTER, NR_PROCESSO, NM_FORNECEDOR, ISNULL(FORMAT(DT_EMISSAO,'dd/MM/yyyy'),'') AS DT_EMISSAO, ";
            SQL += "ISNULL(FORMAT(DT_EXPORTACAO,'dd/MM/yyyy'),'') AS DT_EXPORTACAO, NM_CLIENTE, NM_ITEM_DESPESA, VL_LIQUIDO, VL_ISS ";
            SQL += "FROM dbo.FN_PA(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += "WHERE " + situacao + " ";
                if (nota != "")
                {
                    SQL += "AND " + nota + " ";

                }
            }
            else if (nota != "")
            {
                SQL += "WHERE " + nota + " ";
            }
            SQL += "ORDER BY DT_PAGAMENTO, NR_PROCESSO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string ZerarExportTOTVSPA(string dataI, string dataF, string value)
        {
            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_PA = NULL WHERE ID_CONTA_PAGAR_RECEBER = '" + value + "'";
            DBS.ExecuteScalar(SQL);

            return "ok";

        }

        [WebMethod(EnableSession = true)]
        public string integrarTOTVSPA(string dataI, string dataF, string situacao, string nota, string filter, string values)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dtExportPA;
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            switch (filter)
            {
                case "1":
                    nota = "NR_PROCESSO LIKE '" + nota + "%' ";
                    break;

                case "2":
                    nota = "ID_BL_MASTER LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER ";
            SQL += "FROM dbo.FN_PA(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (nota != "")
            {
                SQL += "WHERE NR_NOTA LIKE '" + nota + "%' ";

                if (situacao != "")
                {
                    SQL += "AND " + situacao + " ";
                    SQL += " AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
                }
                else
                {
                    SQL += " AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
                }
            }
            else if (situacao != "")
            {
                SQL += "WHERE " + situacao + " ";
                SQL += " AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            else
            {
                SQL += " WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            SQL += "ORDER BY DT_PAGAMENTO, NR_PROCESSO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            DataTable listDtExportPA = new DataTable();

            if (listTable != null)
            {
                string[] idContaPagarReceber = new string[listTable.Rows.Count];

                for (int i = 0; i < idContaPagarReceber.Length; i++)
                {
                    idContaPagarReceber[i] = listTable.Rows[i]["ID_CONTA_PAGAR_RECEBER"].ToString();

                    SQL = "SELECT DT_EXPORTACAO_TOTVS_PA FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "'";
                    listDtExportPA = DBS.List(SQL);
                    dtExportPA = listDtExportPA.Rows[0]["DT_EXPORTACAO_TOTVS_PA"].ToString();



                    if (dtExportPA == "")
                    {
                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_TOTVS_PA = '" + sqlFormattedDate + "' ";
                        SQL += "WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "' ";
                        DBS.ExecuteScalar(SQL);
                    }
                }

                return JsonConvert.SerializeObject("ok");
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSPAFORNEC(string dataI, string dataF, string situacao, string values)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT XGRUPO, COD, LOJA, NOME, NREDUZ, END1, BAIRRO, EST, ";
            SQL += "COD_MUN, CEP, TIPO, CGC, TEL, INSCR, INSCRM, EMAIL, DDD, NATUREZ, ";
            SQL += "CODPAIS, CONTATO, SIMPNAC ";
            SQL += "FROM DBO.FN_PA_FORNEC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
                /*SQL += " AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";*/
            }
			/*else
			{
                SQL += "WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }*/
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] fornec = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    fornec[i] += fmtTotvs(listTable.Rows[i]["XGRUPO"].ToString(), 3);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["COD"].ToString(), 7);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["NOME"].ToString(), 40);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["NREDUZ"].ToString(), 20);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["END1"].ToString(), 40);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["BAIRRO"].ToString(), 20);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["EST"].ToString(), 2);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["COD_MUN"].ToString(), 5);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["CEP"].ToString(), 8);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 1);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["CGC"].ToString(), 14);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["TEL"].ToString(), 50);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["INSCR"].ToString(), 18);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["INSCRM"].ToString(), 18);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["EMAIL"].ToString(), 30);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["DDD"].ToString(), 3);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["CODPAIS"].ToString(), 5);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["CONTATO"].ToString(), 15);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["SIMPNAC"].ToString(), 1);
                }
                return JsonConvert.SerializeObject(fornec);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSPAREC(string dataI, string dataF, string situacao, string values)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "EXECUTE DBO.PR_PA_NEGATIVO '" + dataI + "','" + dataF + "' ";
            DBS.ExecuteScalar(SQL);


            SQL = "SELECT FILIAL, PREFIXO, NUM, PARCELA, TIPO, FORNECE, LOJA, NATUREZ, EMISSAO, VENCTO, VENCREA, ";
            SQL += "VALOR, HIST, ITEMCTA, USERS, XPROD, CONTA, MASTER ";
            SQL += "FROM dbo.FN_PA_REC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
                SQL += "AND ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
			}
			else
			{
                SQL += "WHERE ID_CONTA_PAGAR_RECEBER IN (" + values + ") ";
            }
            SQL += "ORDER BY ITEMCTA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] rec = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    rec[i] += fmtTotvs(listTable.Rows[i]["FILIAL"].ToString(), 6);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PREFIXO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NUM"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PARCELA"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["FORNECE"].ToString(), 7);
                    rec[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["EMISSAO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCTO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCREA"].ToString(), 10);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["VALOR"].ToString(), 16, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["HIST"].ToString(), 40);
                    rec[i] += fmtTotvs(listTable.Rows[i]["ITEMCTA"].ToString(), 15);
                    rec[i] += fmtTotvs(listTable.Rows[i]["USERS"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["XPROD"].ToString(), 200);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);
                    rec[i] += fmtTotvs(listTable.Rows[i]["MASTER"].ToString(), 30);
                }
                return JsonConvert.SerializeObject(rec);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSDemurrage(string dataI, string dataF, string situacao, string nota, string filter)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            switch (filter)
            {
                case "1":
                    nota = "NR_PROCESSO LIKE '" + nota + "%' ";
                    break;

                case "2":
                    nota = "NM_FORNECEDOR LIKE '" + nota + "%' ";
                    break;

                case "3":
                    nota = "NM_CLIENTE LIKE '"+ nota +"' ";
                    break;

                default:
                    nota = "";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER, ISNULL(FORMAT(DT_PAGAMENTO,'dd/MM/yyyy'),'') AS DT_PAGAMENTO , ID_BL_MASTER, NR_PROCESSO, NM_FORNECEDOR, ISNULL(FORMAT(DT_EMISSAO,'dd/MM/yyyy'),'') AS DT_EMISSAO, ";
            SQL += "ISNULL(FORMAT(DT_EXPORTACAO,'dd/MM/yyyy'),'') AS DT_EXPORTACAO, NM_CLIENTE, NM_ITEM_DESPESA, VL_LIQUIDO ";
            SQL += "FROM dbo.FN_DEMURRAGE_PA(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += "WHERE " + situacao + " ";
                if (nota != "")
                {
                    SQL += "AND " + nota + " ";

                }
            }
            else if (nota != "")
            {
                SQL += "WHERE " + nota + " ";
            }
            SQL += "ORDER BY DT_PAGAMENTO, NR_PROCESSO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string integrarTOTVSDemurragePA(string dataI, string dataF, string situacao, string nota, string filter)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dtExportDemurragePA;
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            switch (filter)
            {
                case "1":
                    nota = "NR_PROCESSO LIKE '" + nota + "%' ";
                    break;

                case "2":
                    nota = "ID_BL_MASTER LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER ";
            SQL += "FROM dbo.FN_DEMURRAGE_PA(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (situacao != "")
            {
                SQL += "WHERE " + situacao + " ";
                if (nota != "")
                {
                    SQL += "AND " + nota + " ";

                }
            }
            else if (nota != "")
            {
                SQL += "WHERE " + nota + " ";
            }
            SQL += "ORDER BY DT_PAGAMENTO, NR_PROCESSO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            DataTable listDtExportPA = new DataTable();

            if (listTable != null)
            {
                string[] idContaPagarReceber = new string[listTable.Rows.Count];

                for (int i = 0; i < idContaPagarReceber.Length; i++)
                {
                    idContaPagarReceber[i] = listTable.Rows[i]["ID_CONTA_PAGAR_RECEBER"].ToString();

                    SQL = "SELECT DT_EXPORTACAO_DEMURRAGE_PA FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "'";
                    listDtExportPA = DBS.List(SQL);
                    dtExportDemurragePA = listDtExportPA.Rows[0]["DT_EXPORTACAO_DEMURRAGE_PA"].ToString();



                    if (dtExportDemurragePA == "")
                    {
                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_DEMURRAGE_PA = '" + sqlFormattedDate + "' ";
                        SQL += "WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "' ";
                        DBS.ExecuteScalar(SQL);
                    }
                }

                return JsonConvert.SerializeObject("ok");
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSDemurragePAFORNEC(string dataI, string dataF, string situacao)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT XGRUPO, COD, LOJA, NOME, NREDUZ, END1, BAIRRO, EST, ";
            SQL += "COD_MUN, CEP, TIPO, CGC, TEL, INSCR, INSCRM, EMAIL, DDD, NATUREZ, ";
            SQL += "CODPAIS, CONTATO, SIMPNAC, PAIS ";
            SQL += "FROM DBO.FN_DEMURRAGE_PA_FORNEC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
            }
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] fornec = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    fornec[i] += fmtTotvs(listTable.Rows[i]["XGRUPO"].ToString(), 3);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["COD"].ToString(), 7);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["NOME"].ToString(), 40);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["NREDUZ"].ToString(), 20);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["END1"].ToString(), 40);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["BAIRRO"].ToString(), 20);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["EST"].ToString(), 2);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["COD_MUN"].ToString(), 5);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["CEP"].ToString(), 8);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 1);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["CGC"].ToString(), 14);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["TEL"].ToString(), 50);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["INSCR"].ToString(), 18);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["INSCRM"].ToString(), 18);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["EMAIL"].ToString(), 30);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["DDD"].ToString(), 3);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["CODPAIS"].ToString(), 5);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["CONTATO"].ToString(), 15);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["SIMPNAC"].ToString(), 1);
                    fornec[i] += fmtTotvs(listTable.Rows[i]["PAIS"].ToString(), 3);
                }
                return JsonConvert.SerializeObject(fornec);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSDemurragePAREC(string dataI, string dataF, string situacao)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;

            SQL = "SELECT FILIAL, PREFIXO, NUM, PARCELA, TIPO, FORNECE, LOJA, NATUREZ, EMISSAO, VENCTO, VENCREA, ";
            SQL += "VALOR, HIST, ITEMCTA, USERS, XPROD, CONTA, VLCRUZ ";
            SQL += "FROM dbo.FN_DEMURRAGE_PA_REC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
            }
            SQL += "ORDER BY ITEMCTA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] rec = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    rec[i] += fmtTotvs(listTable.Rows[i]["FILIAL"].ToString(), 4);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PREFIXO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NUM"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PARCELA"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["FORNECE"].ToString(), 7);
                    rec[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["EMISSAO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCTO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCREA"].ToString(), 10);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["VALOR"].ToString(), 16, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["HIST"].ToString(), 40);
                    rec[i] += fmtTotvs(listTable.Rows[i]["ITEMCTA"].ToString(), 15);
                    rec[i] += fmtTotvs(listTable.Rows[i]["USERS"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["XPROD"].ToString(), 200);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["VLCRUZ"].ToString(), 16, 2);
                }
                return JsonConvert.SerializeObject(rec);
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSDemurrageRA(string dataI, string dataF, string situacao, string nota)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO_TOTVS IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER, ISNULL(NR_NOTA,'') AS NR_NOTA, TP_NOTA, ISNULL(FORMAT(DT_EMISSAO,'dd/MM/yyyy'),'') AS DT_EMISSAO, VL_NOTA, ";
            SQL += "NM_PARCEIRO, ISNULL(FORMAT(DT_VENCIMENTO,'dd/MM/yyyy'),'') AS DT_VENCIMENTO, ISNULL(FORMAT(DT_EXPORTACAO_TOTVS,'dd/MM/yyyy'),'') AS DT_EXPORTACAO, ";
            SQL += "NR_PROCESSO, ISNULL(NR_REFERENCIA_CLIENTE,'') AS NR_REFERENCIA_CLIENTE ";
            SQL += "FROM dbo.FN_DEMURRAGE_RA(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (nota != "")
            {
                SQL += "WHERE NR_NOTA LIKE '" + nota + "%' ";

                if (situacao != "")
                {
                    SQL += "AND " + situacao + "";
                }
            }
            else if (situacao != "")
            {
                SQL += "WHERE " + situacao + "";
            }
            SQL += "ORDER BY NR_NOTA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string integrarTOTVSDemurrageRA(string dataI, string dataF, string situacao, string nota)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string dtExportDespesa;
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO_TOTVS IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT ID_CONTA_PAGAR_RECEBER ";
            SQL += "FROM dbo.FN_DEMURRAGE_RA(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ")";
            if (nota != "")
            {
                SQL += "WHERE NR_NOTA LIKE '" + nota + "%' ";

                if (situacao != "")
                {
                    SQL += "AND " + situacao + "";
                }
            }
            else if (situacao != "")
            {
                SQL += "WHERE " + situacao + "";
            }
            SQL += "ORDER BY NR_NOTA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            DataTable listDtExportCredit = new DataTable();

            if (listTable != null)
            {
                string[] idContaPagarReceber = new string[listTable.Rows.Count];

                for (int i = 0; i < idContaPagarReceber.Length; i++)
                {
                    idContaPagarReceber[i] = listTable.Rows[i]["ID_CONTA_PAGAR_RECEBER"].ToString();

                    SQL = "SELECT DT_EXPORTACAO_DEMURRAGE_RA FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "'";
                    listDtExportCredit = DBS.List(SQL);
                    dtExportDespesa = listDtExportCredit.Rows[0]["DT_EXPORTACAO_DEMURRAGE_RA"].ToString();



                    if (dtExportDespesa == "")
                    {
                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_EXPORTACAO_DEMURRAGE_RA = '" + sqlFormattedDate + "' ";
                        SQL += "WHERE ID_CONTA_PAGAR_RECEBER = '" + idContaPagarReceber[i] + "' ";
                        DBS.ExecuteScalar(SQL);
                    }
                }

                return JsonConvert.SerializeObject("ok");
            }
            else
            {
                return JsonConvert.SerializeObject("erro");
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSDemurrageRACLI(string dataI, string dataF, string situacao)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;
            SQL = "SELECT COD, LOJA, NOME, NREDUZ, PESSOA, TIPO, ENDER, ";
            SQL += "EST, COD_MUN, MUN, NATUREZ, BAIRRO, CEP, ATVDA, TEL, TELEX, FAX, CONTATO, ";
            SQL += "CGC, INSCRI, INSCRM, CONTA, RECISS, CONT, PAIS ";
            SQL += "FROM dbo.FN_DEMURRAGE_RA_CLI(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
            }

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string[] cli = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD"].ToString(), 7);
                    cli[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NOME"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NREDUZ"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["PESSOA"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["ENDER"].ToString(), 40);
                    cli[i] += fmtTotvs(listTable.Rows[i]["EST"].ToString(), 2);
                    cli[i] += fmtTotvs(listTable.Rows[i]["COD_MUN"].ToString(), 5);
                    cli[i] += fmtTotvs(listTable.Rows[i]["MUN"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    cli[i] += fmtTotvs(listTable.Rows[i]["BAIRRO"].ToString(), 30);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CEP"].ToString(), 8);
                    cli[i] += fmtTotvs(listTable.Rows[i]["ATVDA"].ToString(), 7);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TEL"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["TELEX"].ToString(), 10);
                    cli[i] += fmtTotvs(listTable.Rows[i]["FAX"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONTATO"].ToString(), 15);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CGC"].ToString(), 14);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCRI"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["INSCRM"].ToString(), 18);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONTA"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["RECISS"].ToString(), 1);
                    cli[i] += fmtTotvs(listTable.Rows[i]["CONT"].ToString(), 20);
                    cli[i] += fmtTotvs(listTable.Rows[i]["PAIS"].ToString(), 3);
                }
                return JsonConvert.SerializeObject(cli);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }


        }

        [WebMethod(EnableSession = true)]
        public string listarTOTVSDemurrageRAREC(string dataI, string dataF, string situacao)
        {
            switch (situacao)
            {
                case "0":
                    situacao = "";
                    break;
                case "1":
                    situacao = "DT_EXPORTACAO IS NULL ";
                    break;
            }

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;

            SQL = "SELECT PREFIXO, NUM, PARCELA, TIPO, NATUREZ, CLIENTE, LOJA, EMISSAO, VENCTO, VENCREA, ";
            SQL += "VALOR, IRRF, ISS, HIST, INSS, COFINS, CSLL, PIS, CONTROL, ITEMCTA, XPROD ";
            SQL += "FROM dbo.FN_DEMURRAGE_RA_REC(";
            SQL += "'" + dataI + "','" + dataF + "'";
            SQL += ") ";
            if (situacao != "")
            {
                SQL += " WHERE " + situacao + " ";
            }
            SQL += " ORDER BY ITEMCTA ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string[] rec = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    rec[i] += fmtTotvs(listTable.Rows[i]["PREFIXO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NUM"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["PARCELA"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["TIPO"].ToString(), 3);
                    rec[i] += fmtTotvs(listTable.Rows[i]["NATUREZ"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CLIENTE"].ToString(), 7);
                    rec[i] += fmtTotvs(listTable.Rows[i]["LOJA"].ToString(), 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["EMISSAO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCTO"].ToString(), 10);
                    rec[i] += fmtTotvs(listTable.Rows[i]["VENCREA"].ToString(), 10);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["VALOR"].ToString(), 17, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["IRRF"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["ISS"].ToString(), 14, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["HIST"].ToString(), 40);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["INSS"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["COFINS"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["CSLL"].ToString(), 14, 2);
                    rec[i] += fmtTotvsNum(listTable.Rows[i]["PIS"].ToString(), 14, 2);
                    rec[i] += fmtTotvs(listTable.Rows[i]["CONTROL"].ToString(), 1);
                    rec[i] += fmtTotvs(listTable.Rows[i]["ITEMCTA"].ToString(), 9);
                    rec[i] += fmtTotvs(listTable.Rows[i]["XPROD"].ToString(), 200);
                }
                return JsonConvert.SerializeObject(rec);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }

        }

        [WebMethod(EnableSession = true)]
        public string listarRelacaoCotacao(string dataI, string dataF, string filter, string nota)
        {
            string SQL;
            string SQL2;
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
                    filter = " AND NM_VENDEDOR LIKE '%" + nota + "%' ";
                    break;
                case "2":
                    filter = " AND INSIDE LIKE '%" + nota + "%' ";
                    break;
                case "3":
                    filter = " AND NM_CLIENTE LIKE '%" + nota + "%' ";
                    break;
                case "4":
                    filter = " AND NM_STATUS_COTACAO LIKE '%" + nota + "%' ";
                    break;
                default:
                    filter = "";
                    break;
            }

            SQL = "SELECT ISNULL(FORMAT(ca.DT_SOLICITACAO,'dd/MM/yyyy'),'') AS SOLICITACAO, ";
            SQL += "ca.NR_COTACAO, ";
            SQL += "ISNULL(ca.INSIDE,'') AS INSIDE, ";
            SQL += "ISNULL(ca.MODAL, '') AS MODAL, ";
            SQL += "ISNULL(ca.CD_INCOTERM, '') AS INCOTERM, ";
            SQL += "ISNULL(ca.NM_CLIENTE, '') AS CLIENTE, ";
            SQL += "ISNULL(ca.NM_SUB_CLIENTE, '') AS SUB_CLIENTE, ";
            SQL += "ISNULL(ca.NM_ORIGEM, '') AS ORIGEM, ";
            SQL += "ISNULL(ca.NM_DESTINO, '') AS DESTINO, ";
            SQL += "ISNULL(ca.NM_VENDEDOR, '') AS VENDEDOR, ";
            SQL += "ISNULL(ca.PROCESSO,'') AS NR_PROCESSO, ";
            SQL += "ISNULL(ca.NR_COTACAO, '') AS NR_COTACAO, ";
            SQL += "ISNULL(ca.NM_STATUS_COTACAO, '') AS STATUS_COTACAO, ";
            SQL += "ISNULL(ca.FREE_HAND, '') AS FREE_HAND, ";
            SQL += "ISNULL(ca.DTA_HUB, '') AS DTA_HUB, ";
            SQL += "ISNULL(ca.LTL, '') AS LTL, ";
            SQL += "ISNULL(ca.NM_TRANSPORTADOR, '') AS TRANSPORTADOR, ";
            SQL += "ISNULL(ca.TRANSPORTE_DEDICADO, '') AS TRANSPORTE_DEDICADO, ";
            SQL += "ISNULL(ca.PESO_BRUTO, 0) AS PESO_BRUTO, ";
            SQL += "ISNULL(ca.VL_TOTAL_FRETE_COMPRA, 0) AS VL_TOTAL_FRETE_COMPRA, ";
            SQL += "ISNULL(ca.VL_TOTAL_FRETE_VENDA, 0) AS VL_TOTAL_FRETE_VENDA, ";
            SQL += "ISNULL(ca.VL_TOTAL_M3, 0) AS VL_TOTAL_M3, ";
            SQL += "ISNULL(ca.VL_PESO_TAXADO, 0) AS VL_PESO_TAXADO, ";
            SQL += "ISNULL(ca.MOEDA_FRETE, '') AS MOEDA_FRETE, ";
            SQL += "ISNULL(ca.MOTIVO, '') AS MOTIVO, ";
            SQL += "ISNULL(ca.OBS_MOTIVO, '') AS OBS_MOTIVO, ";
            SQL += "ISNULL(ca.TIPO_CONTAINER, '') AS TIPO_CONTAINER, ";
            SQL += "ISNULL(ca.MERCADORIA, 0) AS EMBALAGEM, ";
            SQL += "ISNULL(ca.FREETIME, 0) AS FREETIME, ";
            SQL += "ISNULL(ca.NM_EXPORTADOR, '') AS EXPORTADOR, ";
            SQL += "ISNULL(ca.NM_AGENTE_INTERNACIONAL, '') AS AGENTE_INTERNACIONAL, ";
            SQL += "ISNULL(ca.NM_IMPORTADOR, '') AS IMPORTADOR, ";
            SQL += "ISNULL(ca.NM_TIPO_ESTUFAGEM, '') AS NM_TIPO_ESTUFAGEM, ";
            SQL += "ISNULL(ca.QTD_CONTAINER, 0) AS QTD_CONTAINER, ";
            SQL += "ISNULL(ca.QTD_MERCADORIA, 0) AS QTD_MERCADORIA ";
            SQL += "FROM dbo.FN_COTACAO_ABERTURA('" + dataI + "','" + dataF + "','3') AS ca ";
            SQL += "WHERE ca.DT_SOLICITACAO IS NOT NULL " + filter + " ";
            SQL += "ORDER BY ca.DT_SOLICITACAO";


            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarStatusCotacao(string dataI, string dataF, string filter, string nota)
        {
            string SQL;
            string SQL2;
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
                    filter = " AND NM_VENDEDOR LIKE '%" + nota + "%' ";
                    break;
                case "2":
                    filter = " AND INSIDE LIKE '%" + nota + "%' ";
                    break;
                case "3":
                    filter = " AND NM_CLIENTE LIKE '%" + nota + "%' ";
                    break;
                case "4":
                    filter = " AND NM_STATUS_COTACAO LIKE '%" + nota + "%' ";
                    break;
                default:
                    filter = "";
                    break;
            }

            SQL = "select NM_STATUS_COTACAO, COUNT(NR_COTACAO) as QUANTIDADE ";
            SQL += "FROM dbo.FN_COTACAO_ABERTURA('" + dataI + "','" + dataF + "','" + Session["ID_USUARIO"] + "') ";
            SQL += "WHERE DT_SOLICITACAO IS NOT NULL ";
            SQL += " " + filter + "";
            SQL += "GROUP BY NM_STATUS_COTACAO ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarModalCotacao(string dataI, string dataF, string filter, string nota)
        {
            string SQL;
            string SQL2;
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
                    filter = " AND NM_VENDEDOR LIKE '%" + nota + "%' ";
                    break;
                case "2":
                    filter = " AND INSIDE LIKE '%" + nota + "%' ";
                    break;
                case "3":
                    filter = " AND NM_CLIENTE LIKE '%" + nota + "%' ";
                    break;
                case "4":
                    filter = " AND NM_STATUS_COTACAO LIKE '%" + nota + "%' ";
                    break;
                default:
                    filter = "";
                    break;
            }

            SQL = "select MODAL, COUNT(NR_COTACAO) AS QUANTIDADE ";
            SQL += "FROM dbo.FN_COTACAO_ABERTURA('" + dataI + "','" + dataF + "','" + Session["ID_USUARIO"] + "') ";
            SQL += "WHERE DT_SOLICITACAO IS NOT NULL ";
            SQL += " " + filter + "";
            SQL += "GROUP BY MODAL ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarIncotermCotacao(string dataI, string dataF, string filter, string nota)
        {
            string SQL;
            string SQL2;
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
                    filter = " AND NM_VENDEDOR LIKE '%" + nota + "%' ";
                    break;
                case "2":
                    filter = " AND INSIDE LIKE '%" + nota + "%' ";
                    break;
                case "3":
                    filter = " AND NM_CLIENTE LIKE '%" + nota + "%' ";
                    break;
                case "4":
                    filter = " AND NM_STATUS_COTACAO LIKE '%" + nota + "%' ";
                    break;
                default:
                    filter = "";
                    break;
            }

            SQL = "select CD_INCOTERM, COUNT(NR_COTACAO) AS QUANTIDADE ";
            SQL += "FROM dbo.FN_COTACAO_ABERTURA('" + dataI + "','" + dataF + "','" + Session["ID_USUARIO"] + "') ";
            SQL += "WHERE DT_SOLICITACAO IS NOT NULL ";
            SQL += " " + filter + "";
            SQL += "GROUP BY CD_INCOTERM ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarVendedorCotacao(string dataI, string dataF, string filter, string nota)
        {
            string SQL;
            string SQL2;
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
                    filter = " AND NM_VENDEDOR LIKE '%" + nota + "%' ";
                    break;
                case "2":
                    filter = " AND INSIDE LIKE '%" + nota + "%' ";
                    break;
                case "3":
                    filter = " AND NM_CLIENTE LIKE '%" + nota + "%' ";
                    break;
                case "4":
                    filter = " AND NM_STATUS_COTACAO LIKE '%" + nota + "%' ";
                    break;
                default:
                    filter = "";
                    break;
            }

            SQL = "select NM_VENDEDOR, COUNT(NR_COTACAO) AS QUANTIDADE ";
            SQL += "FROM dbo.FN_COTACAO_ABERTURA('" + dataI + "','" + dataF + "','" + Session["ID_USUARIO"] + "') ";
            SQL += "WHERE DT_SOLICITACAO IS NOT NULL ";
            SQL += " " + filter + "";
            SQL += "GROUP BY NM_VENDEDOR ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarInsideCotacao(string dataI, string dataF, string filter, string nota)
        {
            string SQL;
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
                    filter = " AND NM_VENDEDOR LIKE '%" + nota + "%' ";
                    break;
                case "2":
                    filter = " AND INSIDE LIKE '%" + nota + "%' ";
                    break;
                case "3":
                    filter = " AND NM_CLIENTE LIKE '%" + nota + "%' ";
                    break;
                case "4":
                    filter = " AND NM_STATUS_COTACAO LIKE '%" + nota + "%' ";
                    break;
                default:
                    filter = "";
                    break;
            }

            SQL = "select INSIDE, COUNT(NR_COTACAO) AS QUANTIDADE ";
            SQL += "FROM dbo.FN_COTACAO_ABERTURA('" + dataI + "','" + dataF + "','" + Session["ID_USUARIO"] + "') ";
            SQL += "WHERE DT_SOLICITACAO IS NOT NULL ";
            SQL += " " + filter + "";
            SQL += "GROUP BY INSIDE ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        /*[WebMethod(EnableSession = true)]
        public string ProcessosPorAgente(string dataI, string dataF, string nota, string filter)
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
            SQL += "ISNULL(FORMAT(CONVERT(DATE, B.DT_EMBARQUE, 103),'dd/MM/yyyy'),'') AS DATA_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(CONVERT(DATE, B.DT_CHEGADA, 103),'dd/MM/yyyy'),'') AS DATA_CHEGADA, ";
            SQL += "M.NR_BL AS NR_MASTER, ";
            SQL += "B.NR_BL AS NR_HOUSE, ";
            SQL += "E.SIGLA_MOEDA, ";
            SQL += "SUM(A.VL_TAXA_CALCULADO) AS VALOR_CALCULADO, ";
            SQL += "CASE WHEN A.CD_PR='P' THEN 'PAGAR' ELSE 'RECEBER' END AS TIPO ";
            SQL += "FROM TB_BL_TAXA A ";
            SQL += "JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "JOIN TB_PARCEIRO D ON B.ID_PARCEIRO_AGENTE_INTERNACIONAL = D.ID_PARCEIRO ";
            SQL += "JOIN TB_MOEDA E ON A.ID_MOEDA = E.ID_MOEDA ";
            SQL += "WHERE A.FL_DECLARADO = 1 AND A.ID_ORIGEM_PAGAMENTO = 2 ";
            SQL += "AND B.ID_BL NOT IN(SELECT ID_BL FROM TB_ACCOUNT_INVOICE) ";
            SQL += "AND A.ID_ITEM_DESPESA NOT IN(71) ";
            SQL += " "+nota+" ";
            SQL += "GROUP BY B.NR_PROCESSO, A.CD_PR, D.NM_RAZAO, CONVERT(DATE, B.DT_EMBARQUE, 103), CONVERT(DATE, B.DT_CHEGADA, 103), M.NR_BL, B.NR_BL, E.SIGLA_MOEDA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }*/

        [WebMethod(EnableSession = true)]
        public string ProcessosPorAgente(string dataI, string dataF, string nota, string filter)
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
                    nota = "AND NR_PROCESSO LIKE '" + nota + "%' ";
                    break;
                case "2":
                    nota = "AND NM_RAZAO LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            SQL = "SELECT NR_PROCESSO, NR_HOUSE, NR_MASTER, NM_RAZAO, DT_CHEGADA, DT_PREVISAO_CHEGADA, ";
            SQL += "DT_EMBARQUE, DT_PREVISAO_EMBARQUE, ";
            SQL += "SUM(VL_TAXA_CALCULADO) AS VALOR, SIGLA_MOEDA, TIPO_MOVIMENTO ";
            SQL += "FROM VW_ACCOUNT_TAXAS_ABERTA ";
            SQL += "WHERE CONVERT(DATE, DT_EMBARQUE, 103) BETWEEN CONVERT(DATE,'" + dataI + "',103) ";
            SQL += "AND CONVERT(DATE,'" + dataF + "',103) ";
            SQL += "" + nota + " ";
            SQL += "GROUP BY NR_PROCESSO, NR_HOUSE, NR_MASTER, NM_RAZAO, ";
            SQL += "DT_CHEGADA, DT_PREVISAO_CHEGADA, DT_EMBARQUE, DT_PREVISAO_EMBARQUE, SIGLA_MOEDA, TIPO_MOVIMENTO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }



        /*[WebMethod(EnableSession = true)]
        public string TaxaProcesso(string dataI, string dataF, string nota, string filter)
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
            SQL += "ISNULL(FORMAT(CONVERT(DATE, B.DT_EMBARQUE, 103),'dd/MM/yyyy'),'') AS DATA_EMBARQUE, ";
            SQL += "ISNULL(FORMAT(CONVERT(DATE, B.DT_CHEGADA, 103),'dd/MM/yyyy'),'') AS DATA_CHEGADA, ";
            SQL += "M.NR_BL AS NR_MASTER, ";
            SQL += "B.NR_BL AS NR_HOUSE, ";
            SQL += "C.NM_ITEM_DESPESA, ";
            SQL += "E.SIGLA_MOEDA, ";
            SQL += "A.VL_TAXA_CALCULADO AS VALOR_CALCULADO, ";
            SQL += "CASE WHEN A.CD_PR='P' THEN 'PAGAR' ELSE 'RECEBER' END AS TIPO ";
            SQL += "FROM TB_BL_TAXA A ";
            SQL += "JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_BL M ON B.ID_BL_MASTER = M.ID_BL ";
            SQL += "JOIN TB_ITEM_DESPESA C ON A.ID_ITEM_DESPESA = C.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_PARCEIRO D ON B.ID_PARCEIRO_AGENTE_INTERNACIONAL = D.ID_PARCEIRO ";
            SQL += "JOIN TB_MOEDA E ON A.ID_MOEDA = E.ID_MOEDA ";
            SQL += "WHERE A.FL_DECLARADO = 1 AND A.ID_ORIGEM_PAGAMENTO = 2 ";
            SQL += "AND B.ID_BL NOT IN(SELECT ID_BL FROM TB_ACCOUNT_INVOICE) ";
            SQL += "AND C.ID_ITEM_DESPESA NOT IN(71) ";
            SQL += "AND CONVERT(DATE,B.DT_EMBARQUE,103) BETWEEN CONVERT(DATE,'" + dataI + "',103) AND CONVERT(DATE,'" + dataF + "',103) ";
            SQL += " " + nota + " ";
            SQL += "ORDER BY B.NR_PROCESSO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }*/

        [WebMethod(EnableSession = true)]
        public string TaxaProcesso(string dataI, string dataF, string nota, string filter)
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
                    nota = "AND NR_PROCESSO LIKE '" + nota + "%' ";
                    break;
                case "2":
                    nota = "AND NM_RAZAO LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            SQL = "SELECT NR_PROCESSO, NR_HOUSE, NR_MASTER, ";
            SQL += "NM_RAZAO, PARCEIRO_TAXA, DT_CHEGADA, DT_PREVISAO_CHEGADA, ";
            SQL += "DT_EMBARQUE, DT_PREVISAO_EMBARQUE, ";
            SQL += "NM_ITEM_DESPESA, VL_TAXA_CALCULADO, SIGLA_MOEDA, TIPO_MOVIMENTO ";
            SQL += "FROM VW_ACCOUNT_TAXAS_ABERTA ";
            SQL += "WHERE CONVERT(DATE,DT_EMBARQUE,103) BETWEEN CONVERT(DATE,'" + dataI + "',103) AND CONVERT(DATE,'" + dataF + "',103) ";
            SQL += " " + nota + " ";
            SQL += "ORDER BY NR_PROCESSO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }


        [WebMethod(EnableSession = true)]
        public string listarConferenciaContaCorrente(string dataI, string dataF, string filter, string nota)
        {
            string SQL;
            string SQL2;
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
                    filter = " WHERE NM_PARCEIRO LIKE '%" + nota + "%' ";
                    break;
                case "2":
                    filter = " WHERE NR_PROCESSO LIKE '%" + nota + "%' ";
                    break;
                default:
                    filter = "";
                    break;
            }
             
            SQL = "SELECT NR_PROCESSO, ";
            SQL += "ISNULL(MAX(CASE WHEN TP_FATURA = 'VENDA' THEN NM_PARCEIRO ELSE '' END ),'') AS CLIENTE, ";
            SQL += "ISNULL(MAX(CASE WHEN TP_FATURA = 'COMPRA' THEN NM_PARCEIRO ELSE '' END ),'') AS TRANSPORTADOR, ";
            SQL += "SUM(CASE WHEN TP_FATURA = 'COMPRA' THEN VL_FATURA ELSE 0 END) AS COMPRA, ";
            SQL += "SUM(CASE WHEN TP_FATURA = 'VENDA' THEN VL_FATURA ELSE 0 END) AS VENDA, ";
            SQL += "SUM(CASE WHEN TP_FATURA = 'VENDA' THEN VL_FATURA ELSE 0 END) - ";
            SQL += "SUM(CASE WHEN TP_FATURA = 'COMPRA' THEN VL_FATURA ELSE 0 END) AS PROFIT, ";
            SQL += "FORMAT((CASE WHEN LEFT(ISNULL(MAX(CASE WHEN TP_FATURA = 'VENDA' THEN DT_LIQUIDACAO ELSE '' END),''),4) = '1900' ";
            SQL += "THEN NULL ELSE ISNULL(MAX(CASE WHEN TP_FATURA = 'VENDA' THEN DT_LIQUIDACAO ELSE '' END),'') END),'dd/MM/yyyy') AS DT_LIQUIDACAO, ";
            SQL += "FORMAT((CASE WHEN LEFT(ISNULL(MAX(CASE WHEN TP_FATURA = 'COMPRA' THEN DT_LIQUIDACAO ELSE '' END), ''), 4) = '1900' ";
            SQL += "THEN NULL ELSE ISNULL(MAX(CASE WHEN TP_FATURA = 'COMPRA' THEN DT_LIQUIDACAO ELSE '' END),'') END),'dd/MM/yyyy') AS DT_LIQUIDACAO_COMPRA ";
            SQL += "FROM FN_DEMURRAGE_FATURAS('"+ dataI + "','" + dataF + "') A ";
            SQL += "" + filter + "";
            SQL += "GROUP BY A.NR_PROCESSO ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarComissaoTransportadora(string filter, string nota, string parametro)
        {
            string SQL;
            string param;
            string dataI;
			
			
            DateTime dataInicial = DateTime.Now;
            string dataF;

            

            switch (filter)
            {
                case "1":
                    filter = " WHERE TRANSPORTADORA LIKE '%" + nota + "%' ";
                    break;
                case "2":
                    filter = " WHERE NR_PROCESSO LIKE '%" + nota + "%' ";
                    break;
                default:
                    filter = "";
                    break;
            }

            if (filter != "")
            {
                switch (parametro)
                {
                    case "0":
                        param = "";
                        dataI = "";
                        dataF = "";
                        break;
                    case "1":
                        dataI = "";
                        dataF = "";
                        param = "AND DT_LIQUIDACAO IS NULL ";
                        break;
                    case "2":
                        dataI = dataInicial.AddDays(-90).ToString("dd/MM/yyyy");
                        dataF = dataInicial.ToString("dd/MM/yyyy");
                        param = "AND DT_LIQUIDACAO IS NOT NULL ";
                        break;
                    case "3":
                        dataI = dataInicial.AddDays(-180).ToString("dd/MM/yyyy");
                        dataF = dataInicial.ToString("dd/MM/yyyy");
                        param = "AND DT_LIQUIDACAO IS NOT NULL ";
                        break;
                    case "4":
                        dataI = "";
                        dataF = "";
                        param = "AND DT_LIQUIDACAO IS NOT NULL ";
                        break;
                    default:
                        dataI = "";
                        dataF = "";
                        param = "";
                        break;
                }
			}
			else
			{
                switch (parametro)
                {
                    case "0":
                        param = "";
                        dataI = "";
                        dataF = "";
                        break;
                    case "1":
                        dataI = "";
                        dataF = "";
                        param = "WHERE DT_LIQUIDACAO IS NULL ";
                        break;
                    case "2":
                        dataI = dataInicial.AddDays(-90).ToString("dd/MM/yyyy");
                        dataF = dataInicial.ToString("dd/MM/yyyy");
                        param = "WHERE DT_LIQUIDACAO IS NOT NULL ";
                        break;
                    case "3":
                        dataI = dataInicial.AddDays(-180).ToString("dd/MM/yyyy");
                        dataF = dataInicial.ToString("dd/MM/yyyy");
                        param = "WHERE DT_LIQUIDACAO IS NOT NULL ";
                        break;
                    case "4":
                        dataI = "";
                        dataF = "";
                        param = "WHERE DT_LIQUIDACAO IS NOT NULL ";
                        break;
                    default:
                        dataI = "";
                        dataF = "";
                        param = "";
                        break;
                }
            }

            SQL = "SELECT ISNULL(NR_PROCESSO,'') AS PROCESSO, ";
            SQL += "ISNULL(TRANSPORTADORA, '') AS TRANSPORTADORA, ";
            SQL += "ISNULL(ITEM,'') AS ITEM, ";
            SQL += "ISNULL(NR_NOTA_FISCAL,'') AS NOTA, ";
            SQL += "ISNULL(FORMAT(DT_NOTA_FISCAL,'dd/MM/yyyy'),'') AS DT_NOTA, ";
            SQL += "ISNULL(FORMAT(DT_LIQUIDACAO,'dd/MM/yyyy'),'') AS DT_LIQ, ";
            SQL += "ISNULL(VL_COMISSAO,0) AS COMISSAO ";
            SQL += "FROM FN_COMISSAO_TRANSPORTADORAS('" + dataI + "','" + dataF + "', '"+parametro+"') A ";
            SQL += "" + filter + "";
            SQL += "" + param + "";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        public static string fmtDecV(double campo, int decimais)
        {
            if (string.IsNullOrEmpty(campo.ToString())) { campo = 0; }

            string novocampo = campo.ToString();

            if (decimais == 2) { novocampo = String.Format("{0:#0.00}", campo); }
            if (decimais == 0) { novocampo = String.Format("{0:#0.}", campo); }
            if (decimais == 1) { novocampo = String.Format("{0:#0.0}", campo); }
            if (decimais == 3) { novocampo = String.Format("{0:#0.000}", campo); }
            if (decimais == 4) { novocampo = String.Format("{0:#0.0000}", campo); }
            if (decimais == 5) { novocampo = String.Format("{0:#0.00000}", campo); }
            if (decimais == 6) { novocampo = String.Format("{0:#0.000000}", campo); }
            if (decimais == 7) { novocampo = String.Format("{0:#0.0000000}", campo); }
            if (decimais == 8) { novocampo = String.Format("{0:#0.00000000}", campo); }
            if (decimais == 9) { novocampo = String.Format("{0:#0.000000000}", campo); }

            return novocampo;
        }
        public static double cvDoub(string campo)
        {
            if (string.IsNullOrEmpty(campo)) { return 0; }
            if (campo == "") { return 0; }

            return Convert.ToDouble(campo);
        }
        public static string fmtPlanilha(string campo)
        {
            campo = campo.ToUpper();
            campo = campo.Replace(";", ",");
            campo = campo.Replace("Á", "A");
            campo = campo.Replace("À", "A");
            campo = campo.Replace("Ã", "A");
            campo = campo.Replace("Â", "A");
            campo = campo.Replace("Ä", "A");
            campo = campo.Replace("É", "E");
            campo = campo.Replace("Ê", "E");
            campo = campo.Replace("È", "E");
            campo = campo.Replace("Ë", "E");
            campo = campo.Replace("Í", "I");
            campo = campo.Replace("Ì", "I");
            campo = campo.Replace("Î", "I");
            campo = campo.Replace("Ï", "I");
            campo = campo.Replace("Ó", "O");
            campo = campo.Replace("Õ", "O");
            campo = campo.Replace("Ô", "O");
            campo = campo.Replace("Ò", "O");
            campo = campo.Replace("Ö", "O");
            campo = campo.Replace("Ú", "U");
            campo = campo.Replace("Û", "U");
            campo = campo.Replace("Ù", "U");
            campo = campo.Replace("Ü", "U");
            campo = campo.Replace(";", ",");
            campo = campo.Replace("[", "(");
            campo = campo.Replace("]", ")");
            campo = System.Text.RegularExpressions.Regex.Replace(campo, "[^0-9A-Z Ç/.,:*º&_<>{}()%=+$#@|!-]+", "");
            //
            return campo;
        }
        public static string fmtTotvsNum(string campo, int tam, int Decimais)
        {

            if (string.IsNullOrEmpty(campo)) { campo = "0"; }
            if (campo == "") { campo = "0"; }
            campo = fmtDecV(cvDoub(campo), Decimais);
            if (campo.Length < tam)
            {
                return campo + ";";
            }
            return campo.Substring(0, tam) + ";";
        }

        [WebMethod(EnableSession = true)]
        public string listarContasRecebidasPagas(string dataI, string dataF, string nota, string filter)
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
                    nota = "AND NR_PROCESSO LIKE '" + nota + "%' ";
                    break;
                case "2":
                    nota = "AND NM_PARCEIRO LIKE '" + nota + "%' ";
                    break;
                case "4":
                    nota = "AND NR_BL_MASTER LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            SQL = "SELECT ISNULL(NR_PROCESSO,'') AS NR_PROCESSO,  ISNULL(NR_BL_MASTER, '') AS MBL, ";
            SQL += "ISNULL(CASE WHEN VL_DIFERENCA > 0.00 THEN VL_DIFERENCA END,0) AS ACRESCIMO, ";
            SQL += "ISNULL(CASE WHEN VL_DIFERENCA < 0.00 THEN VL_DIFERENCA END,0) AS DECRESCIMO, VL_ITEM_DESPESA_BR + ISNULL(VL_DIFERENCA,0) AS TOTAL, ";
            SQL += "ISNULL(NM_ITEM_DESPESA, '') AS NM_ITEM_DESPESA, ISNULL(FORMAT(DT_LIQUIDACAO, 'dd/MM/yyyy'), '') AS DT_LIQUIDACAO, ";
            SQL += "ISNULL(NM_PARCEIRO, '') AS NM_PARCEIRO, ISNULL(TP_MOVIMENTO,'') AS TIPO, ISNULL(CONVERT(VARCHAR, VL_ITEM_DESPESA), '') AS VALOR, ";
            SQL += "ISNULL(MOEDA,'') AS MOEDA, ISNULL(VL_CAMBIO,0) AS CAMBIO, ISNULL(CONVERT(VARCHAR, VL_ITEM_DESPESA_BR), '') AS VALOR_BR, ";
            SQL += "ISNULL(FORMAT(DT_CAMBIO, 'dd/MM/yyyy'), '') AS DT_CAMBIO ";
            SQL += "FROM FN_PREVISIBILIDADE_CONTA_CORRENTE_TESTE('01/01/1900', '01/01/2900') ";
            SQL += "WHERE CONVERT(DATE, DT_LIQUIDACAO,103) BETWEEN CONVERT(DATE, '" + dataI + "',103) AND CONVERT(DATE, '" + dataF + "',103) ";
            SQL += "" + nota + "";
            SQL += "ORDER BY NR_PROCESSO, NM_ITEM_DESPESA ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarRelatorioContasRecebidasPagas(string dataI, string dataF)
        {
            string SQL;

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '/' + mesI + '/' + anoI;
            dataF = diaF + '/' + mesF + '/' + anoF;


            SQL = "SELECT ISNULL(NR_PROCESSO,'') AS PROCESSO, ISNULL(NR_BL_MASTER,'') AS MBL, ISNULL(NM_ITEM_DESPESA,'') AS ITEM_DESPESA, ";
            SQL += "ISNULL(DT_LIQUIDACAO_REC,'') AS DT_LIQ_REC, ISNULL(NM_CLIENTE_REC,'') AS CLI_REC, ISNULL(VL_DEVIDO_REC,0) AS DEVIDO_REC, ";
            SQL += "ISNULL(MOEDA_REC,'') AS MOEDA_REC, ISNULL(VL_CAMBIO_REC,0) AS CAMBIO_REC, ISNULL(VL_LIQUIDO_REC,0) AS LIQ_REC, ";
            SQL += "ISNULL(VL_ISS_REC,0) AS ISS_REC, ISNULL(DT_LIQUIDACAO_PAG,'') AS DT_LIQ_PAG, ISNULL(NM_FORNECEDOR_PAG,'') AS FORNEC_PAG, ";
            SQL += "ISNULL(VL_DEVIDO_PAG,0) AS DEV_PAG, ISNULL(MOEDA_PAG,'') AS MOEDA_PAG, ISNULL(VL_CAMBIO_PAG,0) AS CAMBIO_PAG, ";
            SQL += "ISNULL(VL_LIQUIDO_PAG,0) AS LIQ_PAG, ISNULL(VL_ISS_PAG,0) AS ISS_PAG, ISNULL(TP_EXPORTACAO,'') AS TIPO_EXPORT, ";
            SQL += "ISNULL(DT_EMISSAO,'') AS EMISSAO, ISNULL(FORMAT(DT_VENCIMENTO,'dd/MM/yyyy'),'') AS VENCIMENTO, ";
            SQL += "ISNULL(TP_FATURAMENTO,'') AS FATURAMENTO, ISNULL(FORMAT(DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS PREV_CHEGADA, ";
            SQL += "ISNULL(FORMAT(DT_CHEGADA,'dd/MM/yyyy'),'') AS CHEGADA, ISNULL(TP_ESTUFAGEM,'') AS ESTUFAGEM, ";
            SQL += "ISNULL(NM_CLIENTE_FINAL,'') AS CLI_FINAL, ISNULL(NM_AGENTE,'') AS AGENTE, ISNULL(TP_PAGTO_MASTER,'') AS PAG_MASTER, ";
            SQL += "ISNULL(TP_PAGTO_HOUSE,'') AS PAG_HOUSE FROM FN_CONTAS_RECEBIDAS_PAGAS_II('"+dataI+"','"+dataF+"') ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                string[] previ = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    previ[i] += listTable.Rows[i]["PROCESSO"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["MBL"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ITEM_DESPESA"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["DT_LIQ_REC"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CLI_REC"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["DEVIDO_REC"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["MOEDA_REC"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CAMBIO_REC"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["LIQ_REC"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ISS_REC"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["DT_LIQ_PAG"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["FORNEC_PAG"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["DEV_PAG"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["MOEDA_PAG"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CAMBIO_PAG"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["LIQ_PAG"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ISS_PAG"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["TIPO_EXPORT"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["EMISSAO"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["VENCIMENTO"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["FATURAMENTO"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["PREV_CHEGADA"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CHEGADA"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ESTUFAGEM"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CLI_FINAL"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["AGENTE"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["PAG_MASTER"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["PAG_HOUSE"].ToString() + ";";
                }
                return JsonConvert.SerializeObject(previ);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarContasAReceberAPagar(string dataI, string dataF, string nota, string filter, string origem, string chkConfSim, string chkConfNao)
        {
            string SQL;

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);
            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            switch (filter)
            {
                case "1":
                    nota = "AND NR_PROCESSO LIKE '" + nota + "%' ";
                    break;
                case "2":
                    nota = "AND NM_CLIENTE LIKE '" + nota + "%' ";
                    break;
                case "3":
                    nota = "AND NM_FORNECEDOR LIKE '" + nota + "%' ";
                    break;
                case "4":
                    nota = "AND NR_BL_MASTER LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            switch (origem)
            {
                case "1":
                    origem = "AND ID_ORIGEM_PAGAMENTO = " + origem + " ";
                    break;
                case "2":
                    origem = "AND ID_ORIGEM_PAGAMENTO = " + origem + " ";
                    break;
                default:
                    origem = "";
                    break;
            }

            if (chkConfSim == "1" && chkConfNao == "1")
            {
                chkConfSim = "";
                chkConfNao = "";
            }
            else
            {
                switch (chkConfNao)
                {
                    case "1":
                        chkConfNao = " AND (ISNULL(DOC_CONFERIDO_HOUSE,0)=0 AND ISNULL(DOC_CONFERIDO_MASTER,0)=0) ";
                        break;
                    default:
                        chkConfNao = "";
                        break;
                }

                switch (chkConfSim)
                {
                    case "1":
                        chkConfSim = " AND (DOC_CONFERIDO_HOUSE != 0 OR DOC_CONFERIDO_MASTER != 0) ";
                        break;
                    default:
                        chkConfSim = "";
                        break;
                }
            }

            SQL = "SELECT ISNULL(NR_PROCESSO,'') AS PROCESSO, ";
            SQL += "ISNULL(NR_BL_MASTER, '') AS MBL, ";
            SQL += "ISNULL(TP_SERVICO, '') AS SERVICO, ";
            SQL += "ISNULL(NM_ITEM_DESPESA, '') AS NM_ITEM_DESPESA, ";
            SQL += "ISNULL(NM_CLIENTE, '') AS CLIENTE, ";
            SQL += "ISNULL(VL_SALDO_RECEBER, 0) AS DEVIDO_REC, ";
            SQL += "ISNULL(MOEDA_SALDO_RECEBER, '') AS MOEDA_REC, ";
            SQL += "ISNULL(VL_CAMBIO_SALDO_RECEBER, 0) AS CAMBIO_REC, ";
            SQL += "ISNULL(FORMAT(DT_CAMBIO_SALDO_RECEBER, 'dd/MM/yyyy'), '') AS DT_CAMBIO_REC, ";
            SQL += "ISNULL(VL_SALDO_RECEBER_BR, 0) AS RECEBER, ";
            SQL += "ISNULL(NM_FORNECEDOR, '') AS FORNECEDOR, ";
            SQL += "ISNULL(VL_SALDO_PAGAR, 0) DEVIDO_PAG, ";
            SQL += "ISNULL(MOEDA_SALDO_PAGAR, '') AS MOEDA_PAG, ";
            SQL += "ISNULL(VL_CAMBIO_SALDO_PAGAR, 0) AS CAMBIO_PAGAR, ";
            SQL += "ISNULL(FORMAT(DT_CAMBIO_SALDO_PAGAR, 'dd/MM/yyyy'), '') AS DT_CAMBIO_PAG, ";
            SQL += "ISNULL(VL_SALDO_PAGAR_BR, 0) AS PAGAR, ";
            SQL += "ISNULL(FORMAT(DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') AS DT_PREVISAO_CHEGADA, ";
            SQL += "ISNULL(TP_ESTUFAGEM,'') AS ESTUFAGEM, ";
            SQL += "ISNULL(TP_FATURAMENTO,'') AS FATURAMENTO, ";
            SQL += "CASE WHEN ISNULL(DOC_CONFERIDO_HOUSE, 0) = 0 THEN 'NÃO' ELSE 'SIM' END AS DOC_CONFERIDO_HOUSE, ";
            SQL += "CASE WHEN ISNULL(DOC_CONFERIDO_MASTER, 0) = 0 THEN 'NÃO' ELSE 'SIM' END AS DOC_CONFERIDO_MASTER ";
            SQL += "FROM dbo.FN_PREVISIBILIDADE_SALDO_TESTE('" + dataI + "','" + dataF + "','') ";
            SQL += "WHERE RIGHT(NR_PROCESSO,2) >= 18 ";
            SQL += "" + nota + "";
            SQL += "" + origem + "";
            SQL += "" + chkConfSim + "";
            SQL += "" + chkConfNao + "";
            SQL += "ORDER BY NR_PROCESSO, NM_ITEM_DESPESA ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);


            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string ExportContaPrevisibilidadeProcesso(string dataI, string dataF, string nota, string filter, string chkConfSim, string chkConfNao)
        {

            string dtembarque;
            string dtprevisaochegada;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.AddDays(1).ToString("dd/MM/yyyy");
            string sqlFormattedDate2 = myDateTime.AddDays(90).ToString("dd/MM/yyyy");
            string SQL;

            if (chkConfSim == "1" && chkConfNao == "1")
            {
                chkConfSim = "";
                chkConfNao = "";
            }
            else
            {
                switch (chkConfNao)
                {
                    case "1":
                        chkConfNao = " AND (ISNULL(DOC_CONFERIDO_HOUSE,0)=0 AND ISNULL(DOC_CONFERIDO_MASTER,0)=0) ";
                        break;
                    default:
                        chkConfNao = "";
                        break;
                }

                switch (chkConfSim)
                {
                    case "1":
                        chkConfSim = " AND (DOC_CONFERIDO_HOUSE != 0 OR DOC_CONFERIDO_MASTER != 0) ";
                        break;
                    default:
                        chkConfSim = "";
                        break;
                }
            }

            SQL = "SELECT CASE WHEN ISNULL(DOC_CONFERIDO_HOUSE,0) = 0 THEN 'NÃO' ELSE 'SIM' END AS DOC_CONFERIDO_HOUSE, CASE WHEN ISNULL(DOC_CONFERIDO_MASTER,0) = 0 THEN 'NÃO' ELSE 'SIM' END AS DOC_CONFERIDO_MASTER, ISNULL(NR_PROCESSO,'') AS PROCESSO, ISNULL(NR_BL_MASTER,'') MASTER, ISNULL(NR_BL_HOUSE,'') AS HOUSE, ISNULL(TP_SERVICO,'') TPSERVICO, ISNULL(TP_ESTUFAGEM,'') TPESTUFAGEM, ISNULL(TP_PAGAMENTO_HOUSE,'') TPPAGAMENTOHOUSE, ISNULL(TP_PAGAMENTO_MASTER,'') TPPAGAMENTOMASTER, ISNULL(QT_CNTR_20,0) AS CNTR20, ISNULL(QT_CNTR_40,0) AS CNTR40, ISNULL(ORIGEM,'') AS ORIGEM, ISNULL(DESTINO,'') AS DESTINO, DT_EMBARQUE AS DTEMBARQUE, DT_PREVISAO_CHEGADA as DTPREVISAOCHEGADA, ISNULL(NM_CLIENTE,'') AS PARCEIRO, ISNULL(CNEE,'') AS CNEE, ISNULL(INDICADOR,'') AS INDICADOR, ISNULL(AGENTE,'') AS AGENTE,ISNULL(VL_RECEBER,0) AS ARECEBERBR, ISNULL(VL_PAGAR,0) AS APAGARBR, ISNULL(VL_SALDO,0) AS SALDOBR FROM FN_PREVISIBILIDADE_PROCESSO('" + sqlFormattedDate + "','" + sqlFormattedDate2 + "') ";
            SQL += "WHERE RIGHT(NR_PROCESSO,2) > 18 ";
            SQL += "" + chkConfSim + "";
            SQL += "" + chkConfNao + "";
            SQL += "ORDER BY NR_PROCESSO ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] previ = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    if (listTable.Rows[i]["DTEMBARQUE"] == null)
                    {
                        dtembarque = "";
                    }
                    else
                    {
                        dtembarque = listTable.Rows[i]["DTEMBARQUE"].ToString();
                    }

                    if (listTable.Rows[i]["DTPREVISAOCHEGADA"] == null)
                    {
                        dtprevisaochegada = "";
                    }
                    else
                    {
                        dtprevisaochegada = listTable.Rows[i]["DTPREVISAOCHEGADA"].ToString();
                    }
                    previ[i] += listTable.Rows[i]["DOC_CONFERIDO_HOUSE"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["DOC_CONFERIDO_MASTER"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["PROCESSO"].ToString() + ";";
                    previ[i] += fmtTotvs2(listTable.Rows[i]["MASTER"].ToString());
                    previ[i] += fmtTotvs2(listTable.Rows[i]["HOUSE"].ToString());
                    previ[i] += listTable.Rows[i]["TPSERVICO"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["TPESTUFAGEM"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["TPPAGAMENTOHOUSE"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["TPPAGAMENTOMASTER"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CNTR20"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CNTR40"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ORIGEM"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["DESTINO"].ToString() + ";";
                    previ[i] += dtembarque + ";";
                    previ[i] += dtprevisaochegada + ";";
                    previ[i] += listTable.Rows[i]["PARCEIRO"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CNEE"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["INDICADOR"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["AGENTE"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ARECEBERBR"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["APAGARBR"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["SALDOBR"].ToString() + ";";
                }
                return JsonConvert.SerializeObject(previ);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }
        }

        [WebMethod(EnableSession = true)]
        public string ContaPrevisibilidadeProcesso(string dataI, string dataF, string nota, string filter, string chkConfSim, string chkConfNao)
        {
            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);

            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;

            switch (filter)
            {
                case "1":
                    nota = "AND NR_PROCESSO LIKE '" + nota + "%' ";
                    break;
                case "2":
                    nota = "AND NM_CLIENTE LIKE '" + nota + "%' ";
                    break;
                case "3":
                    nota = "AND NM_FORNECEDOR LIKE '" + nota + "%' ";
                    break;
                case "4":
                    nota = "AND NR_BL_MASTER LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            if (chkConfSim == "1" && chkConfNao == "1")
            {
                chkConfSim = "";
                chkConfNao = "";
            }
            else
            {
                switch (chkConfNao)
                {
                    case "1":
                        chkConfNao = " AND (ISNULL(DOC_CONFERIDO_HOUSE,0)=0 AND ISNULL(DOC_CONFERIDO_MASTER,0)=0) ";
                        break;
                    default:
                        chkConfNao = "";
                        break;
                }

                switch (chkConfSim)
                {
                    case "1":
                        chkConfSim = " AND (DOC_CONFERIDO_HOUSE != 0 OR DOC_CONFERIDO_MASTER != 0) ";
                        break;
                    default:
                        chkConfSim = "";
                        break;
                }
            }

            SQL = "SELECT CASE WHEN ISNULL(DOC_CONFERIDO_HOUSE,0) = 0 THEN 'NÃO' ELSE 'SIM' END AS DOC_CONFERIDO_HOUSE, ";
            SQL += "CASE WHEN ISNULL(DOC_CONFERIDO_MASTER,0) = 0 THEN 'NÃO' ELSE 'SIM' END AS DOC_CONFERIDO_MASTER, ";
            SQL += "ISNULL(NR_PROCESSO,'') AS PROCESSO, ";
            SQL += "ISNULL(NR_BL_MASTER,'') MASTER, ";
            SQL += "ISNULL(NR_BL_HOUSE,'') AS HOUSE, ";
            SQL += "ISNULL(TP_SERVICO,'') TPSERVICO, ";
            SQL += "ISNULL(TP_ESTUFAGEM,'') TPESTUFAGEM, ";
            SQL += "ISNULL(TP_PAGAMENTO_HOUSE,'') TPPAGAMENTOHOUSE, ";
            SQL += "ISNULL(TP_PAGAMENTO_MASTER,'') TPPAGAMENTOMASTER, ";
            SQL += "ISNULL(QT_CNTR_20,0) AS CNTR20, ";
            SQL += "ISNULL(QT_CNTR_40,0) AS CNTR40, ";
            SQL += "ISNULL(ORIGEM,'') AS ORIGEM, ";
            SQL += "ISNULL(DESTINO,'') AS DESTINO, ";
            SQL += "FORMAT(DT_EMBARQUE,'dd/MM/yyyy') AS DTEMBARQUE, ";
            SQL += "FORMAT(DT_PREVISAO_CHEGADA,'dd/MM/yyyy') as DTPREVISAOCHEGADA, ";
            SQL += "ISNULL(NM_CLIENTE,'') AS PARCEIRO, ";
            SQL += "ISNULL(CNEE,'') AS CNEE, ";
            SQL += "ISNULL(INDICADOR,'') AS INDICADOR, ";
            SQL += "ISNULL(AGENTE,'') AS AGENTE, ";
            SQL += "CONVERT(DECIMAL(18,2),ISNULL(VL_RECEBER,0)) AS ARECEBERBR, ";
            SQL += "CONVERT(DECIMAL(18,2),ISNULL(VL_PAGAR,0)) AS APAGARBR, ";
            SQL += "ISNULL(TIPO_FATURAMENTO,0) AS TIPO_FATURAMENTO, ";
            SQL += "ISNULL(DIAS_FATURADOS,0) AS DIAS_FATURADOS, ";
            SQL += "CONVERT(DECIMAL(18,2),ISNULL(VL_SALDO,0)) AS SALDOBR, ";
            SQL += "ISNULL(ORIGEM_COMPRA,'') AS ORIGEM_COMPRA, ";
            SQL += "ISNULL(ORIGEM_VENDA, '') AS ORIGEM_VENDA, ";
            SQL += "ISNULL(NM_ITEM_DESPESA,'') AS NM_ITEM_DESPESA, ";
            SQL += "ISNULL(MODALIDADE,'') AS MODALIDADE ";
            SQL += "FROM FN_PREVISIBILIDADE_PROCESSO('" + dataI + "','" + dataF + "') ";
            SQL += "WHERE RIGHT(NR_PROCESSO,2) > 18 ";
            SQL += "" + chkConfSim + "";
            SQL += "" + chkConfNao + "";
            SQL += "" + nota + "";
            SQL += "ORDER BY NR_PROCESSO ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string CSVContaPrevisibilidadeProcesso(string dataI, string dataF, string nota, string filter, string chkConfSim, string chkConfNao)
        {
            string dtembarque;
            string dtprevisaochegada;

            string diaI = dataI.Substring(8, 2);
            string mesI = dataI.Substring(5, 2);
            string anoI = dataI.Substring(0, 4);

            string diaF = dataF.Substring(8, 2);
            string mesF = dataF.Substring(5, 2);
            string anoF = dataF.Substring(0, 4);

            dataI = diaI + '-' + mesI + '-' + anoI;
            dataF = diaF + '-' + mesF + '-' + anoF;

            string SQL;

            switch (filter)
            {
                case "1":
                    nota = "AND NR_PROCESSO LIKE '" + nota + "%' ";
                    break;
                case "2":
                    nota = "AND NM_CLIENTE LIKE '" + nota + "%' ";
                    break;
                case "3":
                    nota = "AND NM_FORNECEDOR LIKE '" + nota + "%' ";
                    break;
                case "4":
                    nota = "AND NR_BL_MASTER LIKE '" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            if (chkConfSim == "1" && chkConfNao == "1")
            {
                chkConfSim = "";
                chkConfNao = "";
            }
            else
            {
                switch (chkConfNao)
                {
                    case "1":
                        chkConfNao = " AND (ISNULL(DOC_CONFERIDO_HOUSE,0)=0 AND ISNULL(DOC_CONFERIDO_MASTER,0)=0) ";
                        break;
                    default:
                        chkConfNao = "";
                        break;
                }

                switch (chkConfSim)
                {
                    case "1":
                        chkConfSim = " AND (DOC_CONFERIDO_HOUSE != 0 OR DOC_CONFERIDO_MASTER != 0) ";
                        break;
                    default:
                        chkConfSim = "";
                        break;
                }
            }

            SQL = "SELECT CASE WHEN ISNULL(DOC_CONFERIDO_HOUSE,0) = 0 THEN 'NÃO' ELSE 'SIM' END AS DOC_CONFERIDO_HOUSE, ";
            SQL += "CASE WHEN ISNULL(DOC_CONFERIDO_MASTER,0) = 0 THEN 'NÃO' ELSE 'SIM' END AS DOC_CONFERIDO_MASTER, ";
            SQL += "ISNULL(NR_PROCESSO,'') AS PROCESSO, ISNULL(NR_BL_MASTER,'') MASTER, ISNULL(NR_BL_HOUSE,'') AS HOUSE, ";
            SQL += "ISNULL(TP_SERVICO,'') TPSERVICO, ISNULL(TP_ESTUFAGEM,'') TPESTUFAGEM, ISNULL(TP_PAGAMENTO_HOUSE,'') TPPAGAMENTOHOUSE, ";
            SQL += "ISNULL(TP_PAGAMENTO_MASTER,'') TPPAGAMENTOMASTER, ISNULL(QT_CNTR_20,0) AS CNTR20, ISNULL(QT_CNTR_40,0) AS CNTR40, ";
            SQL += "ISNULL(ORIGEM,'') AS ORIGEM, ISNULL(DESTINO,'') AS DESTINO, ISNULL(FORMAT(DT_EMBARQUE,'dd/MM/yyyy'),'') AS DTEMBARQUE, ";
            SQL += "ISNULL(FORMAT(DT_PREVISAO_CHEGADA,'dd/MM/yyyy'),'') as DTPREVISAOCHEGADA, ISNULL(NM_CLIENTE,'') AS PARCEIRO, ";
            SQL += "ISNULL(CNEE,'') AS CNEE, ISNULL(INDICADOR,'') AS INDICADOR, ISNULL(AGENTE,'') AS AGENTE,CONVERT(DECIMAL(18,2), ";
            SQL += "ISNULL(VL_RECEBER,0)) AS ARECEBERBR, CONVERT(DECIMAL(18,2),ISNULL(VL_PAGAR,0)) AS APAGARBR, ISNULL(TIPO_FATURAMENTO,0) AS TIPO_FATURAMENTO, ";
            SQL += "ISNULL(DIAS_FATURADOS,0) AS DIAS_FATURADOS, CONVERT(DECIMAL(18,2),ISNULL(VL_SALDO,0)) AS SALDOBR, ISNULL(ORIGEM_COMPRA,'') AS ORIGEM_COMPRA, ";
            SQL += "ISNULL(ORIGEM_VENDA, '') AS ORIGEM_VENDA, ";
            SQL += "ISNULL(ORIGEM_VENDA, '') AS ORIGEM_VENDA, ";
            SQL += "ISNULL(NM_ITEM_DESPESA,'') AS NM_ITEM_DESPESA, ";
            SQL += "ISNULL(MODALIDADE,'') AS MODALIDADE ";
            SQL += "FROM FN_PREVISIBILIDADE_PROCESSO('" + dataI + "','" + dataF + "') ";
            SQL += "WHERE RIGHT(NR_PROCESSO,2) > 18 ";
            SQL += "" + chkConfSim + "";
            SQL += "" + chkConfNao + "";
            SQL += "" + nota + "";
            SQL += "ORDER BY NR_PROCESSO ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] previ = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    if (listTable.Rows[i]["DTEMBARQUE"] == null)
                    {
                        dtembarque = "";
                    }
                    else
                    {
                        dtembarque = listTable.Rows[i]["DTEMBARQUE"].ToString();
                    }

                    if (listTable.Rows[i]["DTPREVISAOCHEGADA"] == null)
                    {
                        dtprevisaochegada = "";
                    }
                    else
                    {
                        dtprevisaochegada = listTable.Rows[i]["DTPREVISAOCHEGADA"].ToString();
                    }
                    previ[i] += listTable.Rows[i]["DOC_CONFERIDO_HOUSE"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["DOC_CONFERIDO_MASTER"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["PROCESSO"].ToString() + ";";
                    previ[i] += fmtTotvs2(listTable.Rows[i]["MASTER"].ToString());
                    previ[i] += fmtTotvs2(listTable.Rows[i]["HOUSE"].ToString());
                    previ[i] += listTable.Rows[i]["TPSERVICO"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["TPESTUFAGEM"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["TPPAGAMENTOHOUSE"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["TPPAGAMENTOMASTER"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CNTR20"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CNTR40"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ORIGEM"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["DESTINO"].ToString() + ";";
                    previ[i] += dtembarque + ";";
                    previ[i] += dtprevisaochegada + ";";
                    previ[i] += listTable.Rows[i]["PARCEIRO"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["CNEE"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["INDICADOR"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["AGENTE"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["TIPO_FATURAMENTO"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["DIAS_FATURADOS"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ORIGEM_COMPRA"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ORIGEM_VENDA"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["ARECEBERBR"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["APAGARBR"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["SALDOBR"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["NM_ITEM_DESPESA"].ToString() + ";";
                    previ[i] += listTable.Rows[i]["MODALIDADE"].ToString() + ";";
                }
                return JsonConvert.SerializeObject(previ);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }
        }

        [WebMethod(EnableSession = true)]
        public string ContaConferenciaProcesso()
        {
            string SQL;
            SQL = "SELECT ISNULL(NR_PROCESSO,'') PROCESSO, ISNULL(PROCEDENCIA,'') PROCEDENCIA, ISNULL(NM_ITEM,'') AS ITEM, ISNULL(VL_BR, 0) AS VLBR, ISNULL(ESTUF_MASTER, '') AS ESTUFMASTER, ISNULL(ESTUF_HOUSE, '') AS ESTUFHOUSE, ISNULL(PAGTO_MASTER, 0) AS PAGTOMASTER, ISNULL(PAGTO_HOUSE, 0) AS PAGTOHOUSE, ISNULL(PAGTO_TAXA, 0) AS PAGTOTAXA, ISNULL(NM_ORIGEM_PAGAMENTO, '') AS ORIGEM, ISNULL(DECLARADO, '') AS DECLARADO, ISNULL(FREE_HAND, '') as FREEHAND, ISNULL(STATUS_FRETE,'') AS STATUSFRETE, ISNULL(DIVISAO_PROFIT, 0) AS PROFIT FROM FN_CONTAS_CONFERENCIA(90) ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                string[] conf = new string[listTable.Rows.Count];
                for (int i = 0; i < listTable.Rows.Count; i++)
                {
                    conf[i] += listTable.Rows[i]["PROCESSO"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["PROCEDENCIA"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["ITEM"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["VLBR"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["ESTUFMASTER"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["ESTUFHOUSE"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["PAGTOMASTER"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["PAGTOHOUSE"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["PAGTOTAXA"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["ORIGEM"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["DECLARADO"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["FREEHAND"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["STATUSFRETE"].ToString() + ";";
                    conf[i] += listTable.Rows[i]["PROFIT"].ToString()+";";
                    
                }
                return JsonConvert.SerializeObject(conf);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarSaldo(string nota, string filter, string dataI, string dataF)
        {   
            string sqlFormattedDate;
            string sqlFormattedDate2;
            string SQL;

            if(dataI != "")
			{
                string diaI = dataI.Substring(8, 2);
                string mesI = dataI.Substring(5, 2);
                string anoI = dataI.Substring(0, 4);
                sqlFormattedDate = diaI+"/"+mesI+"/"+anoI;
			}
			else
			{
                sqlFormattedDate = "01/01/1900";
			}

            if (dataF != "")
            {
                string diaF = dataF.Substring(8, 2);
                string mesF = dataF.Substring(5, 2);
                string anoF = dataF.Substring(0, 4);
                sqlFormattedDate2 = diaF + "/" + mesF + "/" + anoF;
            }
            else
            {
                sqlFormattedDate2 = "01/01/2900";
            }

            if (nota != "")
            {
                switch (filter)
                {
                    case "0":
                        filter = " ";
                        break;
                    case "1":
                        filter = "WHERE NR_PROCESSO LIKE '" + nota + "%' ";
                        break;
                    case "2":
                        filter = "WHERE NM_CLIENTE LIKE '" + nota + "%' ";
                        break;
                    case "3":
                        filter = "WHERE NM_FORNECEDOR LIKE '" + nota + "%' ";
                        break;
                    case "4":
                        filter = "WHERE NR_BL_MASTER LIKE '" + nota + "%' ";
                        break;
                }
			}
			else
			{
                filter = "";
			}

            SQL = "SELECT ISNULL(NR_PROCESSO,'') PROCESSO, ISNULL(NR_BL_MASTER,'') AS MBL, ISNULL(NR_BL_HOUSE,'') AS HBL, ISNULL(TP_SERVICO,'') AS TPSERVICO, ISNULL(ORIGEM,'') AS ORIGEM, ISNULL(DESTINO,'') AS DESTINO, FORMAT(DT_CHEGADA,'dd/MM/yyyy') AS CHEGADA, ISNULL(TP_ESTUFAGEM, '') AS TPESTUFAGEM, ISNULL(FREE_HAND,'') AS FREEHAND, ISNULL(VL_SALDO_RECEBER_BR,0) AS RECEBER, ISNULL(VL_SALDO_PAGAR_BR,0) AS PAGAR, ISNULL(VL_SALDO_PROCESSO,0) AS SALDO_PROCESSO, ISNULL(NM_CLIENTE,'') AS CLIENTE, ISNULL(NM_FORNECEDOR,'') AS FORNECEDOR FROM FN_PREVISIBILIDADE_CONFERENCIA_TOTAIS('" + sqlFormattedDate + "','" + sqlFormattedDate2 + "') "+filter+" ORDER BY NR_PROCESSO";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                return JsonConvert.SerializeObject(listTable);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarConferencia(string nota, string filter)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = "01/01/1900";
            string sqlFormattedDate2 = "01/01/2900";
            string SQL;

            if (nota != "")
            {
                switch (filter)
                {
                    case "0":
                        filter = " ";
                        break;
                    case "1":
                        filter = "WHERE NR_PROCESSO LIKE '" + nota + "%' ";
                        break;
                    case "2":
                        filter = "WHERE NR_BL_MASTER LIKE '" + nota + "%' ";
                        break;
                    case "3":
                        filter = "WHERE NR_BL_HOUSE LIKE '" + nota + "%' ";
                        break;
                }
            }
            else
            {
                filter = "";
            }

            SQL = "SELECT ISNULL(NR_PROCESSO,'') PROCESSO, ISNULL(NR_BL_MASTER,'') AS MBL, ISNULL(NR_BL_HOUSE,'') AS HBL, FORMAT(DT_CHEGADA,'dd/MM/yyyy') AS CHEGADA, ISNULL(NM_ITEM_DESPESA, '') AS ITEM_DESPESA, ISNULL(VL_VENDA,0) AS VENDA, ISNULL(MOEDA_VENDA,'') AS MOEDA_VENDA, ISNULL(VL_COMPRA,0) AS COMPRA, ISNULL(MOEDA_COMPRA,'') AS MOEDA_COMPRA, ISNULL(FREE_HAND,'') AS FREEHAND, ISNULL(TP_ESTUFAGEM_MASTER,'') AS ESTUFAGEM_MASTER, ISNULL(TP_ESTUFAGEM_HOUSE,'') AS ESTUFAGEM_HOUSE, ISNULL(NM_STATUS_FRETE_AGENTE,'') AS STATUS_FRETE, ISNULL(TP_PROFIT,'') AS TIPO_PROFIT, ISNULL(VL_PROFIT,0) AS  PROFIT FROM FN_PREVISIBILIDADE_CONFERENCIA_ITENS('" + sqlFormattedDate + "','" + sqlFormattedDate2 + "') "+filter+" ORDER BY NR_PROCESSO";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                return JsonConvert.SerializeObject(listTable);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }
        }
        [WebMethod(EnableSession = true)]
        public string ListarProcessoFinalizado(string dataI, string dataF, string fcl, string lcl, string filter, string text)
		{
            string sqlFormattedDate;
            string sqlFormattedDate2;
            string SQL;
            string estufagem;

            if (dataI != "")
            {
                string diaI = dataI.Substring(8, 2);
                string mesI = dataI.Substring(5, 2);
                string anoI = dataI.Substring(0, 4);
                sqlFormattedDate = diaI + "/" + mesI + "/" + anoI;
            }
            else
            {
                sqlFormattedDate = "01/01/1900";
            }

            if (dataF != "")
            {
                string diaF = dataF.Substring(8, 2);
                string mesF = dataF.Substring(5, 2);
                string anoF = dataF.Substring(0, 4);
                sqlFormattedDate2 = diaF + "/" + mesF + "/" + anoF;
            }
            else
            {
                sqlFormattedDate2 = "01/01/2900";
            }            

			switch (filter)
			{
                case "2":
                    filter = " AND NR_PROCESSO LIKE '" + text + "%' ";
                    break;
                default:
                    filter = "";
                    break;
			}

            if(lcl == "1")
			{
                if(fcl == "1")
				{
                    estufagem = "";
				}
				else
				{
                    estufagem = " AND B.NM_TIPO_ESTUFAGEM = 'LCL' ";
                }
			}
			else
			{
                if (fcl == "1")
                {
                    estufagem = " AND B.NM_TIPO_ESTUFAGEM = 'FCL' ";
                }
                else
                {
                    estufagem = " AND (B.NM_TIPO_ESTUFAGEM != 'FCL' AND B.NM_TIPO_ESTUFAGEM != 'LCL') ";
                }
            }

            SQL = "SELECT NR_PROCESSO, B.NM_TIPO_ESTUFAGEM as NM_TIPO_ESTUFAGEM, ";
            SQL += "CASE WHEN B.NM_TIPO_ESTUFAGEM = 'FCL' THEN FORMAT(A.DT_FINALIZACAO_PROCESSO,'dd/MM/yyyy') ELSE '' END AS DT_DEMURRAGE_SALDO, ";
            SQL += "CASE WHEN B.NM_TIPO_ESTUFAGEM = 'LCL' THEN FORMAT(A.DT_FINALIZACAO_PROCESSO,'dd/MM/yyyy') ELSE '' END AS DT_DEV_VAZIO_SALDO ";
            SQL += "FROM TB_BL A ";
            SQL += "JOIN TB_TIPO_ESTUFAGEM B ON A.ID_TIPO_ESTUFAGEM = B.ID_TIPO_ESTUFAGEM ";
            SQL += "WHERE A.DT_FINALIZACAO_PROCESSO IS NOT NULL ";
            SQL += "" + filter + " ";
            SQL += "" + estufagem + " ";
            SQL += " AND CONVERT(DATE,A.DT_FINALIZACAO_PROCESSO,103) BETWEEN CONVERT(DATE,'"+ sqlFormattedDate + "',103) AND CONVERT(DATE,'"+ sqlFormattedDate2 + "',103) ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string listarRelatorioConsolidada(string dataI, string dataF, string dataC, string dataE)
        {
            string sqlFormattedDate = "";
            string sqlFormattedDate2 = "";
            string sqlFormattedDate3 = "";
            string sqlFormattedDate4 = "";
            string SQL;
            string filter = "";

            if (dataI != "")
            {
                string diaI = dataI.Substring(8, 2);
                string mesI = dataI.Substring(5, 2);
                string anoI = dataI.Substring(0, 4);
                sqlFormattedDate = diaI + "/" + mesI + "/" + anoI;
            }

            if (dataF != "")
            {
                string diaF = dataF.Substring(8, 2);
                string mesF = dataF.Substring(5, 2);
                string anoF = dataF.Substring(0, 4);
                sqlFormattedDate2 = diaF + "/" + mesF + "/" + anoF;
            }

            if (dataC != "")
            {
                string diaC = dataC.Substring(8, 2);
                string mesC = dataC.Substring(5, 2);
                string anoC = dataC.Substring(0, 4);
                sqlFormattedDate3 = diaC + "/" + mesC + "/" + anoC;
			}
			
            if (dataE != "")
            {
                string diaE = dataE.Substring(8, 2);
                string mesE = dataE.Substring(5, 2);
                string anoE = dataE.Substring(0, 4);
                sqlFormattedDate4 = diaE + "/" + mesE + "/" + anoE;
			}


            if (sqlFormattedDate != "")
            {
                filter += "AND CONVERT(DATE, X.DT_PREVISAO_CHEGADA, 103) >= CONVERT(DATE,'" + sqlFormattedDate + "',103) ";
            }

            if (sqlFormattedDate2 != "")
            {
                filter += "AND CONVERT(DATE, X.DT_PREVISAO_CHEGADA, 103) <= CONVERT(DATE, '" + sqlFormattedDate2 + "',103) ";
            }

            if (sqlFormattedDate3 != "")
			{
                filter += "AND X.DT_CHEGADA = CONVERT(DATE,'" + sqlFormattedDate3 + "',103) ";
			}

            if(sqlFormattedDate4 != "")
			{
                filter += "AND X.DT_EMBARQUE = CONVERT(DATE, '" + sqlFormattedDate4 + "',103) ";
			}


            SQL = "SELECT X.NM_NAVIO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,FORMAT(MAX(X.DT_CHEGADA), 'dd/MM/yyyy'),103),'') AS CHEGADA, ";
            SQL += "TC.NM_TIPO_CONTAINER as TIPO_CNTR, ";
            SQL += "FORMAT(MAX(X.DT_PREVISAO_CHEGADA), 'dd/MM/yyyy') AS PREVISAO_CHEGADA, ";
            SQL += "FORMAT(MAX(X.DT_EMBARQUE), 'dd/MM/yyyy') AS EMBARQUE, ";
            SQL += "X.NR_CNTR, ";
            SQL += "X.NM_TIPO_CARGA, ";
            SQL += "X.NM_TIPO_ESTUFAGEM, ";
            SQL += "COUNT(X.NR_BL) AS BL, ";
            SQL += "X.NM_PORTO AS PORTO_ORIGEM, ";
            SQL += "convert(decimal(13, 3), SUM(X.VL_M3)) AS METRAGEM, ";
            SQL += "convert(decimal(13, 3), SUM(X.VL_PESO_BRUTO)) AS PESO, ";
            SQL += "(SELECT COUNT(A.ID_BL) FROM TB_BL A ";
            SQL += "JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "WHERE A.ID_TIPO_CARGA = 9 AND C.NR_CNTR = X.NR_CNTR) AS IMO, ";
            SQL += "(SELECT COUNT(A.ID_COTACAO) FROM TB_COTACAO A ";
            SQL += "JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO ";
            SQL += "JOIN TB_AMR_CNTR_BL C ON B.ID_BL = C.ID_BL ";
            SQL += "JOIN TB_CNTR_BL D ON C.ID_CNTR_BL = D.ID_CNTR_BL ";
            SQL += "WHERE A.FL_LTL = 1 AND D.NR_CNTR = X.NR_CNTR) AS LTL, ";
            SQL += "(SELECT COUNT(A.ID_COTACAO) FROM TB_COTACAO A ";
            SQL += "JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO ";
            SQL += "JOIN TB_AMR_CNTR_BL C ON B.ID_BL = C.ID_BL ";
            SQL += "JOIN TB_CNTR_BL D ON C.ID_CNTR_BL = D.ID_CNTR_BL ";
            SQL += "WHERE A.FL_DTA_HUB = 1 AND D.NR_CNTR = X.NR_CNTR) AS DTA_HUB, ";
            SQL += "(SELECT COUNT(A.ID_COTACAO) FROM TB_COTACAO A ";
            SQL += "JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO ";
            SQL += "JOIN TB_AMR_CNTR_BL C ON B.ID_BL = C.ID_BL ";
            SQL += "JOIN TB_CNTR_BL D ON C.ID_CNTR_BL = D.ID_CNTR_BL ";
            SQL += "WHERE A.FL_FREE_HAND = 1 AND D.NR_CNTR = X.NR_CNTR) AS FREEHAND ";
            SQL += "FROM(SELECT A.NR_PROCESSO, TPC.ID_TIPO_CONTAINER, C.NR_CNTR, A.NR_BL, CONVERT(DATE, M.DT_PREVISAO_CHEGADA, 103) AS DT_PREVISAO_CHEGADA, ";
            SQL += "CONVERT(DATE, M.DT_CHEGADA, 103) AS DT_CHEGADA, ";
            SQL += "CONVERT(DATE, M.DT_EMBARQUE, 103) AS DT_EMBARQUE, D.NM_NAVIO, E.NM_TIPO_CARGA, F.NM_TIPO_ESTUFAGEM, G.NM_PORTO, A.VL_M3, A.VL_PESO_BRUTO ";
            SQL += "FROM TB_BL A JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "JOIN TB_TIPO_CONTAINER TPC ON C.ID_TIPO_CNTR = TPC.ID_TIPO_CONTAINER ";
            SQL += "JOIN TB_BL M ON A.ID_BL_MASTER = M.ID_BL ";
            SQL += "JOIN TB_NAVIO D ON A.ID_NAVIO = D.ID_NAVIO ";
            SQL += "JOIN TB_TIPO_CARGA E ON A.ID_TIPO_CARGA = E.ID_TIPO_CARGA ";
            SQL += "JOIN TB_TIPO_ESTUFAGEM F ON A.ID_TIPO_ESTUFAGEM = F.ID_TIPO_ESTUFAGEM ";
            SQL += "JOIN TB_PORTO G ON M.ID_PORTO_ORIGEM = G.ID_PORTO ";
            SQL += "WHERE A.ID_TIPO_ESTUFAGEM = 2) X ";
            SQL += "JOIN TB_TIPO_CONTAINER TC ON X.ID_TIPO_CONTAINER = TC.ID_TIPO_CONTAINER ";
            SQL += "WHERE X.NM_NAVIO <> '' ";
            SQL += " " + filter + " ";
            SQL += "GROUP BY X.NR_CNTR, X.NM_NAVIO, X.NM_TIPO_CARGA, X.NM_TIPO_ESTUFAGEM, X.NM_PORTO, TC.NM_TIPO_CONTAINER ";

            /*SQL = "SELECT X.NM_NAVIO, FORMAT(MAX(X.DT_PREVISAO_CHEGADA),'dd/MM/yyyy') AS PREVISAO_CHEGADA, ";
            SQL += "FORMAT(MAX(X.DT_EMBARQUE),'dd/MM/yyyy') AS EMBARQUE, ";
            SQL += "X.NR_CNTR, X.NM_TIPO_CARGA, X.NM_TIPO_ESTUFAGEM,COUNT(X.NR_BL) AS BL, ";
            SQL += "X.NM_PORTO AS PORTO_ORIGEM, convert(decimal(13,3),SUM(X.VL_M3)) AS METRAGEM, ";
            SQL += "convert(decimal(13,3),SUM(X.VL_PESO_BRUTO)) AS PESO ";
            SQL += "FROM(SELECT C.NR_CNTR, A.NR_BL, ";
            SQL += "CONVERT(DATE, M.DT_PREVISAO_CHEGADA, 103) AS DT_PREVISAO_CHEGADA, ";
            SQL += "CONVERT(DATE, M.DT_EMBARQUE, 103) AS DT_EMBARQUE, ";
            SQL += "D.NM_NAVIO, ";
            SQL += "E.NM_TIPO_CARGA, ";
            SQL += "F.NM_TIPO_ESTUFAGEM, ";
            SQL += "G.NM_PORTO, ";
            SQL += "A.VL_M3, ";
            SQL += "A.VL_PESO_BRUTO ";
            SQL += "FROM TB_BL A ";
            SQL += "JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "JOIN TB_BL M ON A.ID_BL_MASTER=M.ID_BL ";
            SQL += "JOIN TB_NAVIO D ON A.ID_NAVIO = D.ID_NAVIO ";
            SQL += "JOIN TB_TIPO_CARGA E ON  A.ID_TIPO_CARGA = E.ID_TIPO_CARGA ";
            SQL += "JOIN TB_TIPO_ESTUFAGEM F ON A.ID_TIPO_ESTUFAGEM = F.ID_TIPO_ESTUFAGEM ";
            SQL += "JOIN TB_PORTO G ON M.ID_PORTO_ORIGEM=G.ID_PORTO ";
            SQL += "WHERE A.ID_TIPO_ESTUFAGEM = 2 ";
            SQL += "AND CONVERT(DATE, A.DT_PREVISAO_CHEGADA, 103) ";
            SQL += "BETWEEN CONVERT(DATE,'" + sqlFormattedDate + "',103) AND  CONVERT(DATE,'" + sqlFormattedDate2 + "',103)) X ";
            SQL += "GROUP BY X.NR_CNTR, X.NM_NAVIO, X.NM_TIPO_CARGA, X.NM_TIPO_ESTUFAGEM, X.NM_PORTO ";*/

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);

        }

        [WebMethod(EnableSession = true)]
        public string listarRelatorioConsolidadaAnalitico(string dataI, string dataF, string dataC, string dataE, int ltl, int dtahub, int transp, int ltln, int dtahubn, int transpn)
        {
            string sqlFormattedDate = "";
            string sqlFormattedDate2 = "";
            string sqlFormattedDate3 = "";
            string sqlFormattedDate4 = "";
            string SQL;
            string filter = "";

            if (dataI != "")
            {
                string diaI = dataI.Substring(8, 2);
                string mesI = dataI.Substring(5, 2);
                string anoI = dataI.Substring(0, 4);
                sqlFormattedDate = diaI + "/" + mesI + "/" + anoI;
            }

            if (dataF != "")
            {
                string diaF = dataF.Substring(8, 2);
                string mesF = dataF.Substring(5, 2);
                string anoF = dataF.Substring(0, 4);
                sqlFormattedDate2 = diaF + "/" + mesF + "/" + anoF;
            }

            if (dataC != "")
            {
                string diaC = dataC.Substring(8, 2);
                string mesC = dataC.Substring(5, 2);
                string anoC = dataC.Substring(0, 4);
                sqlFormattedDate3 = diaC + "/" + mesC + "/" + anoC;
            }

            if (dataE != "")
            {
                string diaE = dataE.Substring(8, 2);
                string mesE = dataE.Substring(5, 2);
                string anoE = dataE.Substring(0, 4);
                sqlFormattedDate4 = diaE + "/" + mesE + "/" + anoE;
            }


            if (sqlFormattedDate != "")
            {
                filter += "AND CONVERT(DATE, X.DT_PREVISAO_CHEGADA, 103) >= CONVERT(DATE,'" + sqlFormattedDate + "',103) ";
            }

            if (sqlFormattedDate2 != "")
            {
                filter += "AND CONVERT(DATE, X.DT_PREVISAO_CHEGADA, 103) <= CONVERT(DATE, '" + sqlFormattedDate2 + "',103) ";
            }

            if (sqlFormattedDate3 != "")
            {
                filter += "AND X.DT_CHEGADA = CONVERT(DATE,'" + sqlFormattedDate3 + "',103) ";
            }

            if (sqlFormattedDate4 != "")
            {
                filter += "AND X.DT_EMBARQUE = CONVERT(DATE, '" + sqlFormattedDate4 + "',103) ";
            }

            if ((ltl == 1 && ltln == 1) || (ltl == 0 && ltln == 0))
            {
                filter += "";
            }
            else if (ltl == 1)
            {
                filter += "AND (SELECT CASE WHEN COUNT(A.ID_COTACAO) > 0 THEN 'SIM' ELSE 'NAO' END FROM TB_COTACAO A JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO WHERE A.FL_LTL = 1 AND B.ID_BL = X.ID_BL) = 'SIM' ";
            }
            else if (ltln == 1)
            {
                filter += "AND (SELECT CASE WHEN COUNT(A.ID_COTACAO) > 0 THEN 'SIM' ELSE 'NAO' END FROM TB_COTACAO A JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO WHERE A.FL_LTL = 1 AND B.ID_BL = X.ID_BL) = 'NAO' ";
            }
            else
            {
                filter += "";
            }


            if ((dtahub == 1 && dtahubn == 1) || (dtahub == 0 && dtahubn == 0))
            {
                filter += "";
            }
            else if (dtahub == 1)
            {
                filter += "AND (SELECT CASE WHEN COUNT(A.ID_COTACAO) > 0 THEN 'SIM' ELSE 'NAO' END FROM TB_COTACAO A JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO WHERE A.FL_DTA_HUB = 1 AND B.ID_BL = X.ID_BL) = 'SIM' ";
            }
            else if (dtahubn == 1)
            {
                filter += "AND (SELECT CASE WHEN COUNT(A.ID_COTACAO) > 0 THEN 'SIM' ELSE 'NAO' END FROM TB_COTACAO A JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO WHERE A.FL_DTA_HUB = 1 AND B.ID_BL = X.ID_BL) = 'NAO' ";
            }
            else
            {
                filter += "";
            }


            if ((transp == 1 && transpn == 1) || (transp == 0 && transpn == 0))
            {
                filter += "";
            }
            else if (transp == 1)
            {
                filter += "AND (SELECT CASE WHEN A.FL_TRANSP_DEDICADO = 1 THEN 'SIM' ELSE 'NAO' END FROM TB_COTACAO A JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO WHERE B.ID_BL = X.ID_BL) = 'SIM' ";
            }
            else if (transp == 1)
            {
                filter += "AND (SELECT CASE WHEN A.FL_TRANSP_DEDICADO = 1 THEN 'SIM' ELSE 'NAO' END FROM TB_COTACAO A JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO WHERE B.ID_BL = X.ID_BL) = 'NAO' ";
            }
            else
            {
                filter += "";
            }


            SQL = "SELECT X.NM_NAVIO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,FORMAT(MAX(X.DT_CHEGADA), 'dd/MM/yyyy'),103),'') AS CHEGADA, ";
            SQL += "ISNULL(CONVERT(VARCHAR,FORMAT(MAX(X.DT_PREVISAO_CHEGADA), 'dd/MM/yyyy'), 103), '') AS PREVISAO_CHEGADA, ";
            SQL += "ISNULL(CONVERT(VARCHAR,FORMAT(MAX(X.DT_EMBARQUE), 'dd/MM/yyyy'),103),'') AS EMBARQUE,  ";
            SQL += "ISNULL(X.NM_TIPO_CARGA,'') AS NM_TIPO_CARGA, ";
            SQL += "ISNULL(X.NM_TIPO_ESTUFAGEM,'') AS NM_TIPO_ESTUFAGEM, ";
            SQL += "ISNULL(X.NM_PORTO,'') AS PORTO_ORIGEM, ";
            SQL += "X.NR_PROCESSO,";
            SQL += "X.TRANSPORTADORA,";
            SQL += "ISNULL(convert(decimal(13, 3), SUM(X.VL_M3)),0.000) AS METRAGEM, ";
            SQL += "ISNULL(convert(decimal(13, 3), SUM(X.VL_PESO_BRUTO)), 0.000) AS PESO, ";
            SQL += "(SELECT CASE WHEN A.FL_TRANSP_DEDICADO = 1 THEN 'SIM' ELSE 'NAO' END ";
            SQL += "FROM TB_COTACAO A ";
            SQL += "JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO ";
            SQL += "WHERE B.ID_BL = X.ID_BL) AS TRANSP, ";
            SQL += "(SELECT CASE WHEN COUNT(A.ID_COTACAO) > 0 THEN 'SIM' ELSE 'NAO' END ";
            SQL += "FROM TB_COTACAO A ";
            SQL += "JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO ";
            SQL += "WHERE A.FL_LTL = 1 AND B.ID_BL = X.ID_BL) AS LTL, ";
            SQL += "(SELECT CASE WHEN COUNT(A.ID_COTACAO) > 0 THEN 'SIM' ELSE 'NAO' END ";
            SQL += "FROM TB_COTACAO A ";
            SQL += "JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO ";
            SQL += "WHERE A.FL_DTA_HUB = 1 AND B.ID_BL = X.ID_BL) AS DTA_HUB, ";
            SQL += "(SELECT CASE WHEN COUNT(A.ID_COTACAO) > 0 THEN 'SIM' ELSE 'NAO' END ";
            SQL += "FROM TB_COTACAO A ";
            SQL += "JOIN TB_BL B ON A.ID_COTACAO = B.ID_COTACAO ";
            SQL += "WHERE A.FL_FREE_HAND = 1 AND B.ID_BL = X.ID_BL) AS FREEHAND ";
            SQL += "FROM(SELECT A.ID_BL, A.NR_PROCESSO, TPC.ID_TIPO_CONTAINER, C.NR_CNTR, A.NR_BL, CONVERT(DATE, M.DT_PREVISAO_CHEGADA, 103) AS DT_PREVISAO_CHEGADA, ";
            SQL += "CONVERT(DATE, M.DT_CHEGADA, 103) AS DT_CHEGADA, ISNULL(H.NM_RAZAO,'') AS TRANSPORTADORA, ";
            SQL += "CONVERT(DATE, M.DT_EMBARQUE, 103) AS DT_EMBARQUE, D.NM_NAVIO, E.NM_TIPO_CARGA, F.NM_TIPO_ESTUFAGEM, G.NM_PORTO, A.VL_M3, A.VL_PESO_BRUTO ";
            SQL += "FROM TB_BL A JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER TPC ON C.ID_TIPO_CNTR = TPC.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_BL M ON A.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_NAVIO D ON A.ID_NAVIO = D.ID_NAVIO ";
            SQL += "LEFT JOIN TB_TIPO_CARGA E ON A.ID_TIPO_CARGA = E.ID_TIPO_CARGA ";
            SQL += "LEFT JOIN TB_TIPO_ESTUFAGEM F ON A.ID_TIPO_ESTUFAGEM = F.ID_TIPO_ESTUFAGEM ";
            SQL += "LEFT JOIN TB_PORTO G ON M.ID_PORTO_ORIGEM = G.ID_PORTO ";
            SQL += "LEFT JOIN TB_PARCEIRO H ON A.ID_PARCEIRO_RODOVIARIO=H.ID_PARCEIRO ";
            SQL += "WHERE A.ID_TIPO_ESTUFAGEM = 2 AND A.DT_CANCELAMENTO IS NULL) X ";
            SQL += "WHERE X.NM_NAVIO <> '' ";
            SQL += "" + filter + " ";
            SQL += "GROUP BY X.NR_PROCESSO, X.NR_CNTR, X.ID_BL, X.NM_NAVIO, X.NM_TIPO_CARGA, X.NM_TIPO_ESTUFAGEM, X.NM_PORTO, X.TRANSPORTADORA ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

            [WebMethod(EnableSession = true)]
        public string listarReportOCR()
        {
            string SQL;
            SQL = "SELECT Data, [Tipo Mov.] as TP_MOV, [Nr. Aut.] AS NR_AUT, [Veículo Lanç.] AS VEIC_LANC, NUMBER, [Usuário] AS USUARIO, [Balança] BALANCE, MOTIVO, ERRO_OCR, SIM, NAO, TOTAL FROM VW_REPORT_EXPORT_OCR ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);

        }

        public static string fmtTotvs2(string campo)
        {
            if (string.IsNullOrEmpty(campo)) { return ";"; }
            if (campo == "") { return ";"; }
            return "'" + campo + "';";
        }

        public static string fmtTotvs(string campo, int tam)
        {
            if (string.IsNullOrEmpty(campo)) { return ";"; }
            if (campo == "") { return ";"; }
            campo = fmtPlanilha(campo);
            if (campo.Length < tam)
            {
                return "'" + campo + "';";
            }
            return "'" + campo.Substring(0, tam) + "';";
        }

        [WebMethod(EnableSession = true)]
        public string listarComissao()
        {
            string SQL;

            SQL = "SELECT ISNULL(COMPETENCIA,'') AS COMPETENCIA, ISNULL(FORMAT(DT_INICIAL,'dd/MM/yyyy'),'') AS DT_INICIAL, ISNULL(FORMAT(DT_FINAL,'dd/MM/yyyy'),'') AS DT_FINAL, ISNULL(VL_COMISSAO,0) AS VL_COMISSAO, ISNULL(VL_COMISSAO_CC,0) AS VL_COMISSAO_CC, ISNULL(FORMAT(DT_EXPORTACAO_CC,'dd/MM/yyyy'),'') AS DT_EXPORTACAO_CC, ISNULL(FORMAT(DT_BAIXA,'dd/MM/yyyy'),'') AS DT_BAIXA FROM VW_COMISSAO_VENDEDOR_CC ORDER BY RIGHT(COMPETENCIA,4) DESC, LEFT(COMPETENCIA,2) DESC";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarComissaoNacional()
        {
            string SQL;

            SQL = "SELECT ISNULL(COMPETENCIA,'') AS COMPETENCIA, ISNULL(NR_QUINZENA,'') AS NR_QUINZENA, ISNULL(FORMAT(DT_INICIAL,'dd/MM/yyyy'),'') AS DT_INICIAL, ISNULL(FORMAT(DT_FINAL,'dd/MM/yyyy'),'') AS DT_FINAL, ISNULL(VL_COMISSAO,0) AS VL_COMISSAO, ISNULL(VL_COMISSAO_CC,0) AS VL_COMISSAO_CC, ISNULL(FORMAT(DT_EXPORTACAO_CC,'dd/MM/yyyy'),'') AS DT_EXPORTACAO_CC, ISNULL(FORMAT(DT_BAIXA,'dd/MM/yyyy'),'') AS DT_BAIXA FROM VW_COMISSAO_NACIONAL_CC ORDER BY RIGHT(COMPETENCIA,4) DESC, LEFT(COMPETENCIA,2) DESC, NR_QUINZENA";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarComissaoInternacional()
        {
            string SQL;

            SQL = "SELECT ISNULL(COMPETENCIA,'') AS COMPETENCIA, ISNULL(NR_QUINZENA,'') AS NR_QUINZENA, ISNULL(FORMAT(DT_INICIAL,'dd/MM/yyyy'),'') AS DT_INICIAL, ISNULL(FORMAT(DT_FINAL,'dd/MM/yyyy'),'') AS DT_FINAL, ISNULL(VL_COMISSAO,0) AS VL_COMISSAO, ISNULL(VL_COMISSAO_CC,0) AS VL_COMISSAO_CC, ISNULL(VL_COMISSAO_BR,0) AS VL_COMISSAO_BR, ISNULL(FORMAT(DT_EXPORTACAO_CC,'dd/MM/yyyy'),'') AS DT_EXPORTACAO_CC, ISNULL(FORMAT(DT_BAIXA,'dd/MM/yyyy'),'') AS DT_BAIXA FROM VW_COMISSAO_INTERNACIONAL_CC ORDER BY RIGHT(COMPETENCIA,4) DESC, LEFT(COMPETENCIA,2) DESC, NR_QUINZENA";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarTaxasAbertas(string dataI, string dataF, string nota, string filter)
        {
            string SQL;

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
                    nota = "AND NR_PROCESSO LIKE '%" + nota + "%' ";
                    break;
                default:
                    nota = "";
                    break;
            }

            SQL = "SELECT NR_PROCESSO, FORMAT(DT_EMBARQUE,'dd/MM/yyyy') AS DT_EMBARQUE, FORMAT(DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, TP_ESTUFAGEM, NM_ITEM_DESPESA, ";
            SQL += "VL_RECEBER, MOEDA_RECEBIDO, VL_RECEBIDO, VL_SALDO_RECEBER, ";
            SQL += "VL_PAGAR, MOEDA_PAGO, VL_PAGO, VL_SALDO_PAGAR ";
            SQL += "FROM FN_PREVISIBILIDADE_SALDO('" + dataI + "', '" + dataF + "', '') ";
            SQL += "WHERE(VL_SALDO_RECEBER <> 0 OR VL_SALDO_PAGAR <> 0) ";
            SQL += "" + nota + "";
            SQL += "ORDER BY NR_PROCESSO, NM_ITEM_DESPESA ";            
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarTaxasInativas(FiltrosTaxasInativas dados)
        {
            string SQL;
            string Filtro = "";

            string diaI = dados.DATAINICIAL.Substring(8, 2);
            string mesI = dados.DATAINICIAL.Substring(5, 2);
            string anoI = dados.DATAINICIAL.Substring(0, 4);

            string diaF = dados.DATAFINAL.Substring(8, 2);
            string mesF = dados.DATAFINAL.Substring(5, 2);
            string anoF = dados.DATAFINAL.Substring(0, 4);
            dados.DATAINICIAL = diaI + '/' + mesI + '/' + anoI;
            dados.DATAFINAL = diaF + '/' + mesF + '/' + anoF;

            if (dados.PROCESSO != "") { Filtro += "AND A.NR_PROCESSO LIKE '"+dados.PROCESSO+"%' " ; }
            if (dados.FORNECEDOR != "") { Filtro += "AND E1.ID_PARCEIRO = '" + dados.FORNECEDOR + "' "; }
            if (dados.ESTUFAGEM != "") { Filtro += "AND C.ID_TIPO_ESTUFAGEM = '" + dados.ESTUFAGEM+ "' "; }
            if (dados.MODAL != "") { Filtro += "AND D1.ID_VIATRANSPORTE = '" + dados.MODAL+ "' "; }
            if (dados.SERVICO != "") { Filtro += "AND D.ID_SERVICO = '" + dados.SERVICO+ "' "; }
            if (dados.AGENTEINTER != "") { Filtro += "AND E2.ID_PARCEIRO = '" + dados.AGENTEINTER+ "' "; }
            if (dados.CLIENTE != "") { Filtro += "AND E3.ID_PARCEIRO = '" + dados.CLIENTE + "' "; }
            if (dados.TPMOVIMENTO != "") { Filtro += "AND B.CD_PR = '" + dados.TPMOVIMENTO + "' "; }
            if (dados.ITEMDESPESA != "") { Filtro += "AND F.ID_ITEM_DESPESA = '" + dados.ITEMDESPESA + "' "; }
            if (dados.MOEDA != "") { Filtro += "AND G.ID_MOEDA = '" + dados.MOEDA + "' "; }
            if (dados.BASECALCULO != "") { Filtro += "AND H.ID_BASE_CALCULO_TAXA = '" + dados.BASECALCULO + "' "; }
            if (dados.USUARIO != "") { Filtro += "AND J.ID_USUARIO = '" + dados.USUARIO+ "' "; }

            SQL = "SELECT A.NR_PROCESSO, E1.NM_RAZAO AS FORNECEDOR, C.NM_TIPO_ESTUFAGEM, D1.NM_VIATRANSPORTE, ";
            SQL += "D.TP_SERVICO, E2.NM_RAZAO AS AGENTE, E3.NM_RAZAO AS CLIENTE, ";
            SQL += "CASE WHEN B.CD_PR = 'P' THEN 'PAGAR' ELSE 'RECEBER' END AS TIPO_MOVIMENTO, F.NM_ITEM_DESPESA, ";
            SQL += "G.SIGLA_MOEDA, B.VL_TAXA, H.NM_BASE_CALCULO_TAXA, ISNULL(J.NOME,'') AS USUARIO_INATIVACAO, ISNULL(FORMAT(I.DT_INATIVACAO,'dd/MM/yyyy'),'') AS DATA_INATIVACAO, CONCAT(NM_MOTIVO_INATIVACAO ,ISNULL(' - ' + DS_MOTIVO_INATIVACAO,'')) AS MOTIVO_INATIVACAO ";
            SQL += "FROM TB_BL A ";
            SQL += "JOIN TB_BL_TAXA B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_TIPO_ESTUFAGEM C ON A.ID_TIPO_ESTUFAGEM = C.ID_TIPO_ESTUFAGEM ";
            SQL += "JOIN TB_SERVICO D ON A.ID_SERVICO = D.ID_SERVICO ";
            SQL += "JOIN TB_VIATRANSPORTE D1 ON D.ID_VIATRANSPORTE = D1.ID_VIATRANSPORTE ";
            SQL += "LEFT JOIN TB_PARCEIRO E1 ON A.ID_PARCEIRO_IMPORTADOR = E1.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO E2 ON A.ID_PARCEIRO_AGENTE_INTERNACIONAL = E2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO E3 ON A.ID_PARCEIRO_CLIENTE = E3.ID_PARCEIRO ";
            SQL += "JOIN TB_ITEM_DESPESA F ON B.ID_ITEM_DESPESA = F.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_MOEDA G ON B.ID_MOEDA = G.ID_MOEDA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA H ON B.ID_BASE_CALCULO_TAXA = H.ID_BASE_CALCULO_TAXA ";
            SQL += "LEFT JOIN TB_INATIVACAO_TAXAS I ON B.ID_BL_TAXA=I.ID_BL_TAXA ";
            SQL += "LEFT JOIN TB_USUARIO J ON I.ID_USUARIO_INATIVACAO=J.ID_USUARIO ";
            SQL += "LEFT JOIN TB_MOTIVO_INATIVACAO K ON I.ID_MOTIVO_INATIVACAO = K.ID_MOTIVO_INATIVACAO ";
            SQL += "WHERE B.FL_TAXA_INATIVA = 1 ";
            SQL += "AND CONVERT(DATE,A.DT_EMBARQUE,103) BETWEEN CONVERT(DATE,'" + dados.DATAINICIAL + "',103) AND CONVERT(DATE,'" + dados.DATAFINAL+ "' ,103) ";
            SQL += "" + Filtro + "";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string getHomePage()
        {
            string SQL;

            SQL = "SELECT NM_LINK_BI FROM TB_VINCULO_USUARIO A ";
            SQL += "JOIN TB_POWER_BI B ON A.ID_TIPO_USUARIO = B.ID_GRUPO_USUARIO ";
            SQL += "WHERE ID_USUARIO = 3 ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            return JsonConvert.SerializeObject(listTable);
        }

    }
}
