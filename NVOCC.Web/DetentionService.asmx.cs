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
using System.Net.Mail;
using ABAINFRA.Web.Classes;
using System.Net;
using Microsoft.Exchange.WebServices.Data;
using System.Globalization;

namespace ABAINFRA.Web
{
    /// <summary>
    /// Descrição resumida de DetentionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    [System.Web.Script.Services.ScriptService]
    public class DetentionService : System.Web.Services.WebService
    {

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
        public string ListarDetentionContainer(string armador)
        {
            string SQL;


            SQL = "SELECT A.ID_TABELA_DETENTION, CASE WHEN A.FL_CARGA_IMO = 1 THEN C.CD_TAMANHO_CONTAINER ELSE B.NM_TIPO_CONTAINER END AS TIPO_CONTAINER, FORMAT(DT_VALIDADE_INICIAL,'dd/MM/yyyy') AS DT_VALIDADE_INICIAL_FORMAT, ";
            SQL += "CASE WHEN A.FL_CARGA_IMO = 1 THEN 'SIM' ELSE 'NAO' END AS CARGA_IMO ";
            SQL += "FROM TB_TABELA_DETENTION A ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER B ON A.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_TAMANHO_CONTAINER C ON A.ID_TAMANHO_CONTAINER = C.ID_TAMANHO_CONTAINER ";
            if (armador != "")
            {
                SQL += "WHERE ID_PARCEIRO_TRANSPORTADOR = '" + armador + "' ";
            }
            DataTable listarDETENTIONContainer = new DataTable();
            listarDETENTIONContainer = DBS.List(SQL);
            return JsonConvert.SerializeObject(listarDETENTIONContainer);
        }

        [WebMethod(EnableSession = true)]
        public string CadastrarDetentionContainer(DetentionCls dados)
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
            SQL = "SELECT * FROM TB_TABELA_DETENTION WHERE ID_PARCEIRO_TRANSPORTADOR = '" + dados.ID_PARCEIRO_TRANSPORTADOR + "' AND ";
            SQL += "ID_TIPO_CONTAINER ='" + dados.ID_TIPO_CONTAINER + "' AND DT_VALIDADE_INICIAL = '" + dados.DT_VALIDADE_INICIAL + "' AND FL_CARGA_IMO = '" + dados.FL_CARGA_IMO + "' ";
            DataTable consulta = new DataTable();
            consulta = DBS.List(SQL);
            if (consulta == null)
            {
                SQL = "insert into TB_TABELA_DETENTION (ID_PARCEIRO_TRANSPORTADOR,ID_TIPO_CONTAINER,DT_VALIDADE_INICIAL,QT_DIAS_FREETIME, ";
                SQL += "ID_MOEDA, FL_ESCALONADA, FL_INICIO_CHEGADA, FL_CARGA_IMO, ID_TAMANHO_CONTAINER, QT_DIAS_01 ,VL_VENDA_01 ,QT_DIAS_02 ,VL_VENDA_02 ,QT_DIAS_03 ,VL_VENDA_03 ,QT_DIAS_04, ";
                SQL += "VL_VENDA_04 ,QT_DIAS_05 ,VL_VENDA_05 ,QT_DIAS_06 ,VL_VENDA_06 ,QT_DIAS_07 ,VL_VENDA_07 ,QT_DIAS_08 ,VL_VENDA_08) ";
                SQL += "VALUES( '" + dados.ID_PARCEIRO_TRANSPORTADOR + "'," + (dados.ID_TIPO_CONTAINER == "" ? "NULL" : dados.ID_TIPO_CONTAINER) + ", ";
                SQL += "'" + dados.DT_VALIDADE_INICIAL + "','" + dados.QT_DIAS_FREETIME + "','" + dados.ID_MOEDA + "','" + dados.FL_ESCALONADA + "', '" + dados.FL_INICIO_CHEGADA + "', '" + dados.FL_CARGA_IMO + "', " + (dados.ID_TAMANHO_CONTAINER == "" ? "NULL" : dados.ID_TAMANHO_CONTAINER) + ", ";
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
        public string BuscarDetention(int Id)
        {

            string SQL;
            SQL = "SELECT ID_TABELA_DETENTION, ID_PARCEIRO_TRANSPORTADOR, ID_TIPO_CONTAINER, ID_TAMANHO_CONTAINER, FORMAT(DT_VALIDADE_INICIAL,'yyyy-MM-dd') AS DT_VALIDADE_INICIAL_FORMAT, ";
            SQL += "QT_DIAS_FREETIME, ID_MOEDA, FL_ESCALONADA, FL_INICIO_CHEGADA, FL_CARGA_IMO, QT_DIAS_01, VL_VENDA_01, QT_DIAS_02, VL_VENDA_02, QT_DIAS_03, VL_VENDA_03, ";
            SQL += "QT_DIAS_04, VL_VENDA_04, QT_DIAS_05, VL_VENDA_05, QT_DIAS_06, VL_VENDA_06, QT_DIAS_07, VL_VENDA_07, QT_DIAS_08, VL_VENDA_08 ";
            SQL += "FROM TB_TABELA_DETENTION ";
            SQL += "WHERE ID_TABELA_DETENTION = '" + Id + "'";

            DataTable carregarDados = new DataTable();
            carregarDados = DBS.List(SQL);
            DetentionCls resultado = new DetentionCls();
            resultado.ID_TABELA_DETENTION = (int)carregarDados.Rows[0]["ID_TABELA_DETENTION"];
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
        public string DetentionList()
        {
            string SQL;
            SQL = "SELECT ID_TABELA_DETENTION, NM_TIPO_CONTAINER FROM TB_TABELA_DETENTION JOIN TB_TIPO_CONTAINER ";
            SQL += "ON TB_TABELA_DETENTION.ID_TIPO_CONTAINER = TB_TIPO_CONTAINER.ID_TIPO_CONTAINER ";

            DataTable DETENTIONList = new DataTable();
            DETENTIONList = DBS.List(SQL);
            return JsonConvert.SerializeObject(DETENTIONList);
        }

        [WebMethod(EnableSession = true)]
        public string EditarDetentionContainer(DetentionCls dadosEdit)
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
            SQL = "UPDATE TB_TABELA_DETENTION SET ID_PARCEIRO_TRANSPORTADOR = '" + dadosEdit.ID_PARCEIRO_TRANSPORTADOR + "' , ID_TIPO_CONTAINER = " + (dadosEdit.ID_TIPO_CONTAINER == "" ? "NULL" : dadosEdit.ID_TIPO_CONTAINER) + ", ";
            SQL += "DT_VALIDADE_INICIAL = '" + dadosEdit.DT_VALIDADE_INICIAL + "', QT_DIAS_FREETIME = '" + dadosEdit.QT_DIAS_FREETIME + "', ";
            SQL += "ID_MOEDA = '" + dadosEdit.ID_MOEDA + "', FL_ESCALONADA ='" + dadosEdit.FL_ESCALONADA + "', FL_CARGA_IMO = '" + dadosEdit.FL_CARGA_IMO + "', ID_TAMANHO_CONTAINER = " + (dadosEdit.ID_TAMANHO_CONTAINER == "" ? "NULL" : dadosEdit.ID_TAMANHO_CONTAINER) + ", ";
            SQL += "QT_DIAS_01 ='" + qtdias01 + "' , VL_VENDA_01 = '" + vlvenda01 + "', ";
            SQL += "QT_DIAS_02 = '" + dadosEdit.QT_DIAS_02 + "', VL_VENDA_02 = '" + vlvenda02 + "', ";
            SQL += "QT_DIAS_03 = '" + dadosEdit.QT_DIAS_03 + "', VL_VENDA_03 = '" + vlvenda03 + "', ";
            SQL += "QT_DIAS_04 = '" + dadosEdit.QT_DIAS_04 + "', VL_VENDA_04 = '" + vlvenda04 + "', QT_DIAS_05 = '" + dadosEdit.QT_DIAS_05 + "', ";
            SQL += "VL_VENDA_05 = '" + vlvenda05 + "', QT_DIAS_06 = '" + dadosEdit.QT_DIAS_06 + "', ";
            SQL += "VL_VENDA_06 = '" + vlvenda06 + "', QT_DIAS_07 = '" + dadosEdit.QT_DIAS_07 + "', ";
            SQL += "VL_VENDA_07 = '" + vlvenda07 + "', QT_DIAS_08 = '" + dadosEdit.QT_DIAS_08 + "', ";
            SQL += "VL_VENDA_08 = '" + vlvenda08 + "', FL_INICIO_CHEGADA = '" + dadosEdit.FL_INICIO_CHEGADA + "' ";
            SQL += "WHERE ID_TABELA_DETENTION = '" + dadosEdit.ID_TABELA_DETENTION + "' ";

            string editDETENTION = DBS.ExecuteScalar(SQL);
            return "1";
        }

        [WebMethod(EnableSession = true)]
        public string DeletarDetention(int Id)
        {
            string SQL;
            SQL = "DELETE FROM TB_TABELA_DETENTION WHERE ID_TABELA_DETENTION = '" + Id + "' ";
            string deleteDETENTION = DBS.ExecuteScalar(SQL);
            if (deleteDETENTION == null)
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
            SQL += "DFCL.QT_DIAS_DETENTION, PFCL.DS_STATUS_DETENTION, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_STATUS_DETENTION, 'dd/MM/yyyy'),'') AS DATA_STATUS_DETENTION, ";
            SQL += "ISNULL(PFCL.DS_OBSERVACAO, '') AS DS_OBSERVACAO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,(SELECT ISNULL(CONVERT(VARCHAR,DF.ID_DETENTION_FATURA),'') AS COMPRA ";
            SQL += "FROM TB_DETENTION_FATURA DF ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL_EXP PFCL2 ON DF.ID_BL = PFCL2.ID_BL ";
            SQL += "WHERE DF.CD_PR = 'P' ";
            SQL += "AND DF.DT_CANCELAMENTO IS NULL ";
            SQL += "AND PFCL2.ID_CNTR_BL = PFCL.ID_CNTR_BL ";
            SQL += ")),'') AS CALC_DEMU_COMPRA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_DETENTION_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS VL_DETENTION_COMPRA, ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DETENTION, 'dd/MM/yyyy'), '') AS PAG_DEMU, ";
            SQL += "ISNULL(CONVERT(VARCHAR,(SELECT ISNULL(CONVERT(VARCHAR, DF.ID_DETENTION_FATURA), '') AS VENDA ";
            SQL += "FROM TB_DETENTION_FATURA DF ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL_EXP PFCL3 ON DF.ID_BL = PFCL3.ID_BL ";
            SQL += "WHERE DF.CD_PR = 'R' ";
            SQL += "AND PFCL3.ID_CNTR_BL = PFCL.ID_CNTR_BL ";
            SQL += "AND DF.DT_CANCELAMENTO IS NULL ";
            SQL += ")),'') AS CALC_DEMU_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DETENTION_VENDA,'C','pt-br')),'R$',''),'') AS VL_DETENTION_VENDA, ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DETENTION, 'dd/MM/yyyy'), '') AS RECEB_DEMU ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.DT_CHEGADA IS NOT NULL ";
            SQL += "AND PFCL.FL_DETENTION_FINALIZADA = 0 ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
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
                    idFilter = "AND PFCL.DS_STATUS_DETENTION LIKE '" + Filter + "%' ";
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
                        Finalizado = " AND D.FL_FINALIZA_DETENTION=1 AND D1.FL_FINALIZA_DETENTION= 1 ";
                        break;
                    default:
                        Finalizado = "";
                        break;
                }

                switch (Ativo)
                {
                    case "1":
                        Ativo = " AND ((D.FL_FINALIZA_DETENTION != 1 OR D1.FL_FINALIZA_DETENTION != 1)  OR (D.FL_FINALIZA_DETENTION IS NULL OR D1.FL_FINALIZA_DETENTION IS NULL)) ";


                        break;
                    default:
                        Ativo = "";
                        break;
                }
            }

            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, B.NR_BL AS MBL, PFCL.NM_TIPO_CONTAINER, ISNULL(TC.NM_TIPO_CARGA,'') AS NM_TIPO_CARGA, PFCL.NR_PROCESSO, ISNULL(P.NM_RAZAO,C.NM_RAZAO) AS CLIENTE, ";
            SQL += "ISNULL(P2.NM_RAZAO, '') AS TRANSPORTADOR, ISNULL(FORMAT(PFCL.DT_EMBARQUE, 'dd/MM/yyyy'), '') AS DT_EMBARQUE, ";
            SQL += "ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME), '') AS QT_DIAS_FREETIME, ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME_CONFIRMA),'') AS QT_DIAS_FREETIME_CONFIRMA, ISNULL(FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy'), '') AS FINAL_FREETIME, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'), '') AS DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DETENTION,ISNULL(DFCL.QT_DIAS_DETENTION_COMPRA,'')QT_DIAS_DETENTION_COMPRA, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DETENTION, 'dd/MM/yyyy') AS DATA_STATUS_DETENTION, ISNULL(PFCL.DS_STATUS_DETENTION,'') DS_STATUS_DETENTION, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DETENTION, 'dd/MM/yyyy') AS DATA_STATUS_DETENTION_COMPRA, ISNULL(PFCL.DS_STATUS_DETENTION_COMPRA,'') DS_STATUS_DETENTION_COMPRA, ";
            SQL += "ISNULL(PFCL.DS_OBSERVACAO, '') AS DS_OBSERVACAO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_DETENTION_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS VL_DETENTION_COMPRA, ";
            SQL += "ISNULL(FORMAT(DFCL.VL_DETENTION_COMPRA_BR,'C','pt-br'),'') AS VL_DETENTION_COMPRA_BR, ";
            SQL += "ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DETENTION, 'dd/MM/yyyy'), '') AS PAG_DETENT, ";
            SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DETENTION_VENDA,'C','pt-br')),'R$',''),'') AS VL_DETENTION_VENDA, ";
            SQL += "ISNULL(FORMAT(DFCL.VL_DETENTION_VENDA_BR,'C','pt-br'),'') AS VL_DETENTION_VENDA_BR, ";
            SQL += "ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DETENTION, 'dd/MM/yyyy'), '') AS RECEB_DETENT ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
            SQL += "INNER JOIN TB_BL B ON PFCL.ID_BL_MASTER = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_IMPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO C ON PFCL.ID_PARCEIRO_CLIENTE = C.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_STATUS_DETENTION D ON PFCL.ID_STATUS_DETENTION_COMPRA= D.ID_STATUS_DETENTION ";
            SQL += "LEFT JOIN TB_STATUS_DETENTION D1 ON PFCL.ID_STATUS_DETENTION = D1.ID_STATUS_DETENTION ";
            SQL += "LEFT JOIN TB_TIPO_CARGA TC ON PFCL.ID_TIPO_CARGA = TC.ID_TIPO_CARGA ";
            SQL += "WHERE PFCL.DT_EMBARQUE IS NOT NULL ";
            SQL += "" + idFilter + " ";
            SQL += "" + Ativo + " ";
            SQL += "" + Finalizado + " ";
            SQL += "ORDER BY PFCL.DT_EMBARQUE";
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
                    idFilter = "AND PFCL.DS_STATUS_DETENTION LIKE '" + Filter + "%' ";
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
                        Finalizado = " AND D.FL_FINALIZA_DETENTION=1 AND D1.FL_FINALIZA_DETENTION= 1 ";
                        break;
                    default:
                        Finalizado = "";
                        break;
                }

                switch (Ativo)
                {
                    case "1":
                        Ativo = " AND (D.FL_FINALIZA_DETENTION != 1 OR D1.FL_FINALIZA_DETENTION != 1) ";


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
            SQL += "ISNULL(DFCL.QT_DIAS_DETENTION,'') QT_DIAS_DETENTION, ISNULL(DFCL.QT_DIAS_DETENTION_COMPRA,'')QT_DIAS_DETENTION_COMPRA, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DETENTION, 'dd/MM/yyyy') AS DATA_STATUS_DETENTION, PFCL.DS_STATUS_DETENTION, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DETENTION, 'dd/MM/yyyy') AS DATA_STATUS_DETENTION_COMPRA, PFCL.DS_STATUS_DETENTION_COMPRA, ";
            SQL += "ISNULL(PFCL.DS_OBSERVACAO, '') AS DS_OBSERVACAO, ";
            SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_DETENTION_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS VL_DETENTION_COMPRA, ";
            SQL += "ISNULL(FORMAT(DFCL.VL_DETENTION_COMPRA_BR,'C','pt-br'),'') AS VL_DETENTION_COMPRA_BR, ";
            SQL += "ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DETENTION, 'dd/MM/yyyy'), '') AS PAG_DEMU, ";
            SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DETENTION_VENDA,'C','pt-br')),'R$',''),'') AS VL_DETENTION_VENDA, ";
            SQL += "ISNULL(FORMAT(DFCL.VL_DETENTION_VENDA_BR,'C','pt-br'),'') AS VL_DETENTION_VENDA_BR, ";
            SQL += "ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DETENTION, 'dd/MM/yyyy'), '') AS RECEB_DEMU ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
            SQL += "INNER JOIN TB_BL B ON PFCL.ID_BL_MASTER = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_IMPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_STATUS_DETENTION D ON PFCL.ID_STATUS_DETENTION_COMPRA= D.ID_STATUS_DETENTION ";
            SQL += "LEFT JOIN TB_STATUS_DETENTION D1 ON PFCL.ID_STATUS_DETENTION = D1.ID_STATUS_DETENTION ";
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
            /*SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, DFCL.QT_DIAS_DETENTION, ";
            SQL += "PFCL.QT_DIAS_FREETIME,DFCL.QT_DIAS_DETENTION_COMPRA, PFCL.QT_DIAS_FREETIME_CONFIRMA, PFCL.ID_STATUS_DETENTION, DFCL.ID_DETENTION_FATURA_PAGAR, DFCL.ID_DETENTION_FATURA_RECEBER, PFCL.ID_STATUS_DETENTION_COMPRA, FORMAT(PFCL.DT_STATUS_DETENTION_COMPRA, 'yyyy-MM_dd') AS DATA_STATUS_DETENTION_COMPRA, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DETENTION,'yyyy-MM-dd') AS DATA_STATUS_DETENTION, ";
            SQL += "PFCL.DS_OBSERVACAO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "'";*/

            SQL = "SELECT C.NR_CNTR, A.NR_PROCESSO, D.NM_RAZAO AS CLIENTE, C.QT_DIAS_FREETIME, ";
            SQL += "C.QT_DIAS_FREETIME_CONFIRMA, E.QT_DIAS_DETENTION, E.QT_DIAS_DETENTION_COMPRA, ";
            SQL += "C.ID_STATUS_DETENTION, FORMAT(C.DT_STATUS_DETENTION, 'dd/MM/yyyy') AS DATA_STATUS_DETENTION, ";
            SQL += "C.ID_STATUS_DETENTION_COMPRA, FORMAT(C.DT_STATUS_DETENTION_COMPRA, 'dd/MM/yyyy') AS DATA_STATUS_DETENTION_COMPRA, ";
            SQL += "C.DS_OBSERVACAO ";
            SQL += " FROM TB_BL A ";
            SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_CNTR_BL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO D ON A.ID_PARCEIRO_CLIENTE = D.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_CNTR_DETENTION E ON C.ID_CNTR_BL = E.ID_CNTR_BL ";
            SQL += "WHERE C.ID_CNTR_BL = " + idCont + " AND A.GRAU IN('C') AND A.DT_CANCELAMENTO IS NULL ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string infoContainerExp(int idCont)
        {
            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, PFCL.NR_PROCESSO, P.NM_RAZAO AS CLIENTE, ";
            SQL += "PFCL.QT_DIAS_FREETIME,DFCL.QT_DIAS_DETENTION_COMPRA, PFCL.QT_DIAS_FREETIME_CONFIRMA, PFCL.ID_STATUS_DETENTION, DFCL.ID_DETENTION_FATURA_PAGAR, DFCL.ID_DETENTION_FATURA_RECEBER, PFCL.ID_STATUS_DETENTION_COMPRA, FORMAT(PFCL.DT_STATUS_DETENTION_COMPRA, 'yyyy-MM_dd') AS DATA_STATUS_DETENTION_COMPRA, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DETENTION,'yyyy-MM-dd') AS DATA_STATUS_DETENTION, ";
            SQL += "PFCL.DS_OBSERVACAO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
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
            SQL += "PFCL.ID_STATUS_DETENTION, DFCL.ID_DETENTION_FATURA_PAGAR, DFCL.ID_DETENTION_FATURA_RECEBER, FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'yyyy-MM-dd') as DT_DEVOLUCAO_CNTR, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DETENTION,'yyyy-MM-dd') AS DATA_STATUS_DETENTION, DS_STATUS_DETENTION, ";
            SQL += "PFCL.DS_OBSERVACAO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
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
            SQL += "PFCL.ID_STATUS_DETENTION, DFCL.ID_DETENTION_FATURA_PAGAR, DFCL.ID_DETENTION_FATURA_RECEBER, FORMAT(PFCL.DT_DEVOLUCAO_CNTR,'yyyy-MM-dd') as DT_DEVOLUCAO_CNTR, ";
            SQL += "FORMAT(PFCL.DT_STATUS_DETENTION,'yyyy-MM-dd') AS DATA_STATUS_DETENTION, DS_STATUS_DETENTION, ";
            SQL += "PFCL.DS_OBSERVACAO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "'";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string atualizarContainer(int idCont, string dtStatus, int dsStatus, int qtDias, string dsObs, string qtDiasConfirm, string qtDiasDetentionCompra, string qtDiasDetentionVenda, string dtStatusCompra, int dsStatusCompra)
        {
            if (dsStatus.ToString() != "" && dsStatusCompra.ToString() != "")
            {
                string SQL;
                string flagF;
                SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + dsStatus + "' ";
                DataTable flFinaliza = new DataTable();
                flFinaliza = DBS.List(SQL);
                flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();

                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER, ";
                SQL += "ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DETENTION, 'dd/MM/yyyy'), '') AS PAG_DETENT, ";
                SQL += "ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DETENTION, 'dd/MM/yyyy'), '') AS RECEB_DETENT ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DETENTION_RECEBER"].ToString();

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

                SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = '" + dsStatus + "', QT_DIAS_FREETIME = '" + qtDias + "', QT_DIAS_FREETIME_CONFIRMA = '" + qtDiasConfirm + "', ";
                SQL += "DT_STATUS_DETENTION = '" + dtStatus + "', DS_OBSERVACAO = '" + dsObs + "', ID_STATUS_DETENTION_COMPRA = '" + dsStatusCompra + "', DT_STATUS_DETENTION_COMPRA = '" + dtStatusCompra + "' WHERE ID_CNTR_BL = '" + idCont + "' ;  UPDATE TB_CNTR_DETENTION SET QT_DIAS_DETENTION_COMPRA = '" + qtDiasDetentionCompra + "', QT_DIAS_DETENTION = '" + qtDiasDetentionVenda + "' WHERE ID_CNTR_BL = '" + idCont + "'   ";
                string atualizarContainer = DBS.ExecuteScalar(SQL);
                return "1";


            }
            else
            {
                return "2";
            }
        }

        [WebMethod(EnableSession = true)]
        public string atualizarContainerExp(int idCont, string dtStatus, int dsStatus, int qtDias, string dsObs, string qtDiasConfirm, string qtDiasDetentionCompra, string dtStatusCompra, int dsStatusCompra)
        {
            if (dsStatus.ToString() != "" && dsStatusCompra.ToString() != "")
            {
                string SQL;
                string flagF;
                SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + dsStatus + "' ";
                DataTable flFinaliza = new DataTable();
                flFinaliza = DBS.List(SQL);
                flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();

                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER, ";
                SQL += "ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DETENTION, 'dd/MM/yyyy'), '') AS PAG_DETENT, ";
                SQL += "ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DETENTION, 'dd/MM/yyyy'), '') AS RECEB_DETENT ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DETENTION_RECEBER"].ToString();

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

                SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = '" + dsStatus + "', QT_DIAS_FREETIME = '" + qtDias + "', QT_DIAS_FREETIME_CONFIRMA = '" + qtDiasConfirm + "', ";
                SQL += "DT_STATUS_DETENTION = '" + dtStatus + "', DS_OBSERVACAO = '" + dsObs + "', ID_STATUS_DETENTION_COMPRA = '" + dsStatusCompra + "', DT_STATUS_DETENTION_COMPRA = '" + dtStatusCompra + "' WHERE ID_CNTR_BL = '" + idCont + "' ;  UPDATE TB_CNTR_DETENTION SET QT_DIAS_DETENTION_COMPRA = '" + qtDiasDetentionCompra + "' WHERE ID_CNTR_BL = '" + idCont + "'   ";
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
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
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
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string listarCalculoDetention(string nrProcesso, string tipoCalculo)
        {

            string SQL;
            SQL = "SELECT PFCL.ID_CNTR_BL as ID_CNTR, PFCL.NR_CNTR, ";
            SQL += "PFCL.NM_TIPO_CONTAINER, FORMAT(PFCL.DT_EMBARQUE, 'dd/MM/yyyy') as DT_EMBARQUE, ";
            SQL += "PFCL.QT_DIAS_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy') AS DT_FINAL_FREETIME, ";
            SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') as DT_DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DETENTION,DFCL.QT_DIAS_DETENTION_COMPRA, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DETENTION_COMPRA,'c','pt-br')),'R$',''),'') AS VL_DETENTION_COMPRA, ";
            SQL += "ISNULL(M.NM_MOEDA,'') AS VENDA, ISNULL(M2.NM_MOEDA,'') AS COMPRA, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(DFCL.VL_DETENTION_VENDA,'c','pt-br')),'R$',''),'') AS VL_DETENTION_VENDA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DETENTION_VENDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_MOEDA M2 ON DFCL.ID_MOEDA_DETENTION_COMPRA = M2.ID_MOEDA ";
            SQL += "WHERE PFCL.NR_PROCESSO = '" + nrProcesso + "' AND (PFCL.DT_EMBARQUE IS NOT NULL OR PFCL.DT_EMBARQUE != '') ";
            SQL += "AND (PFCL.QT_DIAS_FREETIME IS NOT NULL OR PFCL.QT_DIAS_FREETIME != '') ";
            SQL += "AND (PFCL.DT_DEVOLUCAO_CNTR IS NOT NULL OR PFCL.DT_DEVOLUCAO_CNTR != '') ";
            if (tipoCalculo == "1")
            {
                SQL += "AND (DFCL.ID_DETENTION_FATURA_RECEBER IS NULL OR DFCL.ID_DETENTION_FATURA_RECEBER = '') ";
                SQL += "AND DFCL.QT_DIAS_DETENTION > 0 ";
            }
            else
            {
                SQL += "AND (DFCL.ID_DETENTION_FATURA_PAGAR IS NULL OR DFCL.ID_DETENTION_FATURA_PAGAR = '') ";
                SQL += "AND DFCL.QT_DIAS_DETENTION_COMPRA > 0 ";
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
            SQL += "DFCL.QT_DIAS_DETENTION,DFCL.QT_DIAS_DETENTION_COMPRA, TBD.FL_ESCALONADA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_CNTR_DETENTION CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
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
            SQL += "WHERE A.ID_CNTR_BL = " + idCont + " ";
            string tipoCarga = DBS.ExecuteScalar(SQL);

            SQL = "SELECT PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, TBD.QT_DIAS_FREETIME as FreeTimeTab, PFCL.QT_DIAS_FREETIME, ";
            SQL += "DFCL.QT_DIAS_DETENTION, P.NM_RAZAO AS TABELA, M.NM_MOEDA AS MOEDA, TBD.FL_ESCALONADA, CD.VL_TAXA_DETENTION_VENDA, ";
            SQL += "TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
            SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
            SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
            SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            if (tipoCarga == "9")
            {
                SQL += "LEFT JOIN TB_TIPO_CONTAINER TPC ON PFCL.NM_TIPO_CONTAINER = TPC.NM_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON TPC.ID_TAMANHO_CONTAINER = TBD.ID_TAMANHO_CONTAINER ";
            }
            else
            {
                SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            }
            SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_CNTR_DETENTION CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
            SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' " + (tipoCarga == "9" ? "AND TBD.FL_CARGA_IMO = 1 " : "") + "  ";
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
                int diasDetention = (int)listTable.Rows[0]["QT_DIAS_DETENTION"];
                int qtFreetime = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"];

                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    if (diasDetention == 0)
                    {
                        vlTaxa = 0;
                    }
                    else
                    {
                        diasDetention = diasDetention + qtFreetime;
                        if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                        {
                            if (diasDetention <= d1)
                            {
                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_01"];
                            }
                            else if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                            {
                                if (diasDetention <= d2)
                                {
                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                }
                                else if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                {
                                    if (diasDetention <= d3)
                                    {
                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                    }
                                    else if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                    {
                                        if (diasDetention <= d4)
                                        {
                                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                        }
                                        else if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                        {
                                            if (diasDetention <= d5)
                                            {
                                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                            }
                                            else if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                            {
                                                if (diasDetention <= d6)
                                                {
                                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                }
                                                else if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                {
                                                    if (diasDetention <= d7)
                                                    {
                                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                    }
                                                    else if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                    {
                                                        if (diasDetention <= d8)
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
            SQL += "DFCL.QT_DIAS_DETENTION, TBD.FL_ESCALONADA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_CNTR_DETENTION CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
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
            SQL += "DFCL.QT_DIAS_DETENTION,CONVERT(INT,ISNULL(DFCL.QT_DIAS_DETENTION_COMPRA,DFCL.QT_DIAS_DETENTION))QT_DIAS_DETENTION_COMPRA, P.NM_RAZAO as TABELA, M.NM_MOEDA AS MOEDA, TBD.FL_ESCALONADA, CD.VL_TAXA_DETENTION_COMPRA, ";
            SQL += "TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
            SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
            SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
            SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            if (tipoCarga == "9")
            {
                SQL += "LEFT JOIN TB_TIPO_CONTAINER TPC ON PFCL.NM_TIPO_CONTAINER = TPC.NM_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON TPC.ID_TAMANHO_CONTAINER = TBD.ID_TAMANHO_CONTAINER ";
            }
            else
            {
                SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
            }
            SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_CNTR_DETENTION CD ON PFCL.ID_CNTR_BL = CD.ID_CNTR_BL ";
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
                int diasDetention = (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"];

                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                    {
                        if (diasDetention <= d1)
                        {
                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_01"];
                        }
                        else if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                        {
                            if (diasDetention <= d1 + d2)
                            {
                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_02"];
                            }
                            else if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                            {
                                if (diasDetention <= d1 + d2 + d3)
                                {
                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                }
                                else if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                {
                                    if (diasDetention <= d1 + d2 + d3 + d4)
                                    {
                                        vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                    }
                                    else if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                    {
                                        if (diasDetention <= d1 + d2 + d3 + d4 + d5)
                                        {
                                            vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                        }
                                        else if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                        {
                                            if (diasDetention <= d1 + d2 + d3 + d4 + d5 + d6)
                                            {
                                                vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                            }
                                            else if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                            {
                                                if (diasDetention <= d1 + d2 + d3 + d4 + d5 + d6 + d7)
                                                {
                                                    vlTaxa = (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                }
                                                else if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                {
                                                    if (diasDetention <= d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8)
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
            SQL = "select id_cntr_bl from TB_CNTR_DETENTION ";
            SQL += "where id_cntr_bl = '" + idCont + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                SQL = "UPDATE TB_CNTR_DETENTION SET DT_CALCULO_DETENTION_VENDA = NULL, ";
                SQL += "ID_MOEDA_DETENTION_VENDA = NULL, VL_TAXA_DETENTION_VENDA = NULL, ";
                SQL += "VL_DETENTION_VENDA = NULL, DT_CAMBIO_DETENTION_VENDA = NULL, ";
                SQL += "VL_CAMBIO_DETENTION_VENDA = NULL, VL_DESCONTO_DETENTION_VENDA = NULL, ";
                SQL += "VL_DETENTION_VENDA_BR = NULL WHERE ID_CNTR_BL = " + idCont + " ";
                string zerar = DBS.ExecuteScalar(SQL);
            }
        }

        [WebMethod(EnableSession = true)]
        public void zerarCalculoCompra(string idCont)
        {
            string SQL;
            SQL = "select id_cntr_bl from TB_CNTR_DETENTION ";
            SQL += "where id_cntr_bl = '" + idCont + "' ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable != null)
            {
                SQL = "UPDATE TB_CNTR_DETENTION SET DT_CALCULO_DETENTION_COMPRA = NULL, ";
                SQL += "ID_MOEDA_DETENTION_COMPRA = NULL, VL_TAXA_DETENTION_COMPRA = NULL, ";
                SQL += "VL_DETENTION_COMPRA = NULL, DT_CAMBIO_DETENTION_COMPRA = NULL, ";
                SQL += "VL_CAMBIO_DETENTION_COMPRA = NULL, VL_DESCONTO_DETENTION_COMPRA = NULL, ";
                SQL += "VL_DETENTION_COMPRA_BR = NULL WHERE ID_CNTR_BL = '" + idCont + "' ";
                string zerar = DBS.ExecuteScalar(SQL);
            }
        }

        [WebMethod(EnableSession = true)]
        public void calcularDETENTIONVenda(string idCont, decimal vlTaxa, string idStatus, string dtStatus)
        {
            string SQL;
            int somaDias;
            decimal vlDemurr;
            string calcular;
            int detention;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            SQL = "SELECT * FROM TB_CNTR_DETENTION WHERE ID_CNTR_BL = " + idCont + " ";
            DataTable search = new DataTable();
            search = DBS.List(SQL);
            if (search == null)
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "ISNULL(CONVERT(VARCHAR, DFCL.ID_DETENTION_FATURA_PAGAR), '') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DETENTION,'yyyy-MM-dd') AS DT_INICIAL_DETENTION, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DETENTION,'yyyy-MM-dd') AS DT_FINAL_DETENTION, ";
                SQL += "DFCL.QT_DIAS_DETENTION,CONVERT(int,ISNULL(DFCL.QT_DIAS_DETENTION_COMPRA,DFCL.QT_DIAS_DETENTION))QT_DIAS_DETENTION_COMPRA,";
                SQL += "DFCL.ID_MOEDA_DETENTION_VENDA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
                SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 1 ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string faturaCompra = listTable.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = listTable.Rows[0]["ID_DETENTION_RECEBER"].ToString();

                somaDias = (int)listTable.Rows[0]["QT_DIAS_DETENTION"];
                vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DETENTION"] * vlTaxa;

                SQL = "INSERT INTO TB_CNTR_DETENTION (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DETENTION, ";
                SQL += "DT_FINAL_DETENTION, QT_DIAS_DETENTION, DT_CALCULO_DETENTION_VENDA, ID_MOEDA_DETENTION_VENDA, VL_TAXA_DETENTION_VENDA, ";
                SQL += "VL_DETENTION_VENDA, DT_CAMBIO_DETENTION_VENDA, VL_CAMBIO_DETENTION_VENDA, VL_DESCONTO_DETENTION_VENDA, VL_DETENTION_VENDA_BR,QT_DIAS_DETENTION_COMPRA ) VALUES ";
                SQL += "(" + idCont + ",'" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "','" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', ";
                SQL += "'" + listTable.Rows[0]["DT_INICIAL_DETENTION"] + "','" + listTable.Rows[0]["DT_FINAL_DETENTION"] + "'," + somaDias + ", ";
                SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + "," + vlTaxa.ToString().Replace(",", ".") + "," + vlDemurr.ToString().Replace(",", ".") + ",null,null,null,null,'" + listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"] + "')";
                calcular = DBS.ExecuteScalar(SQL);

                string flagF;
                SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + idStatus + "' ";
                DataTable flFinaliza = new DataTable();
                flFinaliza = DBS.List(SQL);
                flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();

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

                SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = '" + idStatus + "' ,DT_STATUS_DETENTION = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
                string atualizarStatus = DBS.ExecuteScalar(SQL);

            }
            else
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "ISNULL(CONVERT(VARCHAR, DFCL.ID_DETENTION_FATURA_PAGAR), '') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DETENTION,'yyyy-MM-dd') AS DT_INICIAL_DETENTION, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DETENTION,'yyyy-MM-dd') AS DT_FINAL_DETENTION,";
                SQL += "DFCL.QT_DIAS_DETENTION,CONVERT(int,ISNULL(DFCL.QT_DIAS_DETENTION_COMPRA,DFCL.QT_DIAS_DETENTION))QT_DIAS_DETENTION_COMPRA,";
                SQL += "DFCL.ID_MOEDA_DETENTION_VENDA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
                SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = 1 ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string faturaCompra = listTable.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = listTable.Rows[0]["ID_DETENTION_RECEBER"].ToString();

                somaDias = (int)listTable.Rows[0]["QT_DIAS_DETENTION"];
                vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DETENTION"] * vlTaxa;

                SQL = "UPDATE TB_CNTR_DETENTION SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DETENTION = '" + listTable.Rows[0]["DT_INICIAL_DETENTION"] + "', ";
                SQL += "DT_FINAL_DETENTION = '" + listTable.Rows[0]["DT_FINAL_DETENTION"] + "', QT_DIAS_DETENTION = " + somaDias + ", ";
                SQL += "DT_CALCULO_DETENTION_VENDA = '" + sqlFormattedDate + "', ID_MOEDA_DETENTION_VENDA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DETENTION_VENDA = " + vlTaxa.ToString().Replace(",", ".") + ", ";
                SQL += "VL_DETENTION_VENDA = " + vlDemurr.ToString().Replace(",", ".") + ", DT_CAMBIO_DETENTION_VENDA = null, VL_CAMBIO_DETENTION_VENDA = null, VL_DESCONTO_DETENTION_VENDA = null, VL_DETENTION_VENDA_BR = null, QT_DIAS_DETENTION_COMPRA = " + (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"] + " WHERE ID_CNTR_BL = " + idCont + "";
                calcular = DBS.ExecuteScalar(SQL);

                string flagF;
                SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + idStatus + "' ";
                DataTable flFinaliza = new DataTable();
                flFinaliza = DBS.List(SQL);
                flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();

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

                SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = '" + idStatus + "' ,DT_STATUS_DETENTION = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
                string atualizarStatus = DBS.ExecuteScalar(SQL);

            }
        }

        [WebMethod(EnableSession = true)]
        public void calcularDetentionCompra(string idCont, decimal vlTaxa, string transportador, string idStatus, string dtStatus)
        {
            string SQL;
            int somaDias;
            decimal vlDemurr;
            string calcular;
            int detention;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            SQL = "SELECT * FROM TB_CNTR_DETENTION WHERE ID_CNTR_BL = " + idCont + " ";
            DataTable search = new DataTable();
            search = DBS.List(SQL);
            if (search == null)
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "ISNULL(CONVERT(VARCHAR, DFCL.ID_DETENTION_FATURA_PAGAR), '') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DETENTION,'yyyy-MM-dd') AS DT_INICIAL_DETENTION, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DETENTION,'yyyy-MM-dd') AS DT_FINAL_DETENTION, DFCL.QT_DIAS_DETENTION, CONVERT(int ,ISNULL(DFCL.QT_DIAS_DETENTION_COMPRA,DFCL.QT_DIAS_DETENTION))QT_DIAS_DETENTION_COMPRA, DFCL.ID_MOEDA_DETENTION_COMPRA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
                SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '" + transportador + "' ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string faturaCompra = listTable.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = listTable.Rows[0]["ID_DETENTION_RECEBER"].ToString();


                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"];
                    vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"] * vlTaxa;

                    SQL = "INSERT INTO TB_CNTR_DETENTION (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DETENTION, ";
                    SQL += "DT_FINAL_DETENTION, DT_CALCULO_DETENTION_COMPRA, ID_MOEDA_DETENTION_COMPRA, VL_TAXA_DETENTION_COMPRA, ";
                    SQL += "VL_DETENTION_COMPRA, DT_CAMBIO_DETENTION_COMPRA, VL_CAMBIO_DETENTION_COMPRA, VL_DESCONTO_DETENTION_COMPRA, VL_DETENTION_COMPRA_BR, QT_DIAS_DETENTION_COMPRA,QT_DIAS_DETENTION ) VALUES ";
                    SQL += "(" + idCont + ",'" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "','" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', ";
                    SQL += "'" + listTable.Rows[0]["DT_INICIAL_DETENTION"] + "','" + listTable.Rows[0]["DT_FINAL_DETENTION"] + "', ";
                    SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + "," + vlTaxa.ToString().Replace(",", ".") + "," + vlDemurr.ToString().Replace(",", ".") + ",null,null,null,null," + somaDias + "," + (int)listTable.Rows[0]["QT_DIAS_DETENTION"] + " )";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();

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

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION_COMPRA = '" + idStatus + "' ,DT_STATUS_DETENTION_COMPRA = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
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
                    detention = (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"];
                    vlDemurr = 0;

                    if (detention <= ft)
                    {
                        vlDemurr = 0;
                    }
                    else
                    {
                        if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                        {
                            if (detention - d1 <= 0)
                            {
                                vlDemurr = detention * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                            }
                            else
                            {
                                detention = detention - d1;
                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                                {
                                    if (detention - d2 <= 0)
                                    {
                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_02"]);
                                    }
                                    else
                                    {
                                        detention = detention - d2;
                                        vlDemurr = d2 * (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                        if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                        {
                                            if (detention - d3 <= 0)
                                            {
                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_03"]);
                                            }
                                            else
                                            {
                                                detention = detention - d3;
                                                vlDemurr = d3 * (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                                if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                                {
                                                    if (detention - d4 <= 0)
                                                    {
                                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_04"]);
                                                    }
                                                    else
                                                    {
                                                        detention = detention - d4;
                                                        vlDemurr = d4 * (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                                        if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                                        {
                                                            if (detention - d5 <= 0)
                                                            {
                                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_05"]);
                                                            }
                                                            else
                                                            {
                                                                detention = detention - d5;
                                                                vlDemurr = d5 * (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                                                if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                                                {
                                                                    if (detention - d6 <= 0)
                                                                    {
                                                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_06"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        detention = detention - d6;
                                                                        vlDemurr = d6 * (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                                        if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                                        {
                                                                            if (detention - d7 <= 0)
                                                                            {
                                                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_07"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                detention = detention - d7;
                                                                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                                                if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                                                {
                                                                                    if (detention - d8 <= 0)
                                                                                    {
                                                                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_08"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        detention = detention - d8;
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

                    SQL = "INSERT INTO TB_CNTR_DETENTION (ID_CNTR_BL, DT_INICIAL_FREETIME, DT_FINAL_FREETIME, DT_INICIAL_DETENTION, ";
                    SQL += "DT_FINAL_DETENTION, DT_CALCULO_DETENTION_COMPRA, ID_MOEDA_DETENTION_COMPRA, VL_TAXA_DETENTION_COMPRA, ";
                    SQL += "VL_DETENTION_COMPRA, DT_CAMBIO_DETENTION_COMPRA, VL_CAMBIO_DETENTION_COMPRA, VL_DESCONTO_DETENTION_COMPRA, VL_DETENTION_COMPRA_BR, QT_DIAS_DETENTION_COMPRA,QT_DIAS_DETENTION ) VALUES ";
                    SQL += "(" + idCont + "," + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "," + listTable.Rows[0]["DT_FINAL_FREETIME"] + ", ";
                    SQL += "" + listTable.Rows[0]["DT_INICIAL_DETENTION"] + "," + listTable.Rows[0]["DT_FINAL_DETENTION"] + ", ";
                    SQL += "'" + sqlFormattedDate + "'," + listTable.Rows[0]["ID_MOEDA"] + ",0,'" + vlDemurr.ToString().Replace(",", ".") + "',null,null,null,null," + (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"] + "," + (int)listTable.Rows[0]["QT_DIAS_DETENTION"] + " )";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();

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

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION_COMPRA = '" + idStatus + "',DT_STATUS_DETENTION_COMPRA = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
                    string atualizarStatus = DBS.ExecuteScalar(SQL);
                }
            }
            else
            {
                SQL = "SELECT FORMAT(DFCL.DT_INICIAL_FREETIME,'yyyy-MM-dd') AS DT_INICIAL_FREETIME, FORMAT(DFCL.DT_FINAL_FREETIME,'yyyy-MM-dd') AS DT_FINAL_FREETIME, ";
                SQL += "ISNULL(CONVERT(VARCHAR, DFCL.ID_DETENTION_FATURA_PAGAR), '') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER, ";
                SQL += "FORMAT(DFCL.DT_INICIAL_DETENTION,'yyyy-MM-dd') AS DT_INICIAL_DETENTION, PFCL.QT_DIAS_FREETIME, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DETENTION,'yyyy-MM-dd') AS DT_FINAL_DETENTION,";
                SQL += "DFCL.QT_DIAS_DETENTION, CONVERT(int ,ISNULL(DFCL.QT_DIAS_DETENTION_COMPRA,DFCL.QT_DIAS_DETENTION))QT_DIAS_DETENTION_COMPRA, ";
                SQL += " DFCL.ID_MOEDA_DETENTION_COMPRA, TBD.FL_ESCALONADA, TBD.ID_MOEDA, ";
                SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON TBD.ID_PARCEIRO_TRANSPORTADOR = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_MOEDA M ON TBD.ID_MOEDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + idCont + "' ";
                SQL += "AND TBD.ID_PARCEIRO_TRANSPORTADOR = '" + transportador + "' ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string faturaCompra = listTable.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = listTable.Rows[0]["ID_DETENTION_RECEBER"].ToString();

                if (!(Boolean)listTable.Rows[0]["FL_ESCALONADA"])
                {
                    somaDias = (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"];
                    vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"] * vlTaxa;

                    SQL = "UPDATE TB_CNTR_DETENTION SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                    SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DETENTION = '" + listTable.Rows[0]["DT_INICIAL_DETENTION"] + "', ";
                    SQL += "DT_FINAL_DETENTION = '" + listTable.Rows[0]["DT_FINAL_DETENTION"] + "', QT_DIAS_DETENTION_COMPRA = " + somaDias + ", ";
                    SQL += "DT_CALCULO_DETENTION_COMPRA = '" + sqlFormattedDate + "', ID_MOEDA_DETENTION_COMPRA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DETENTION_COMPRA = " + vlTaxa.ToString().Replace(",", ".") + ", ";
                    SQL += "VL_DETENTION_COMPRA = " + vlDemurr.ToString().Replace(",", ".") + ", DT_CAMBIO_DETENTION_COMPRA = null, VL_CAMBIO_DETENTION_COMPRA = null, VL_DESCONTO_DETENTION_COMPRA = null, VL_DETENTION_COMPRA_BR = null WHERE ID_CNTR_BL = " + idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();

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
                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION_COMPRA = '" + idStatus + "',DT_STATUS_DETENTION_COMPRA = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
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
                    detention = (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"];
                    vlDemurr = 0;

                    if (detention <= ft)
                    {
                        vlDemurr = 0;
                    }
                    else
                    {
                        if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                        {
                            if (detention - d1 <= 0)
                            {
                                vlDemurr = detention * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                            }
                            else
                            {
                                detention = detention - d1;
                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                                {
                                    if (detention - d2 <= 0)
                                    {
                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_02"]);
                                    }
                                    else
                                    {
                                        detention = detention - d2;
                                        vlDemurr = d2 * (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                        if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                        {
                                            if (detention - d3 <= 0)
                                            {
                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_03"]);
                                            }
                                            else
                                            {
                                                detention = detention - d3;
                                                vlDemurr = d3 * (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                                if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                                {
                                                    if (detention - d4 <= 0)
                                                    {
                                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_04"]);
                                                    }
                                                    else
                                                    {
                                                        detention = detention - d4;
                                                        vlDemurr = d4 * (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                                        if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                                        {
                                                            if (detention - d5 <= 0)
                                                            {
                                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_05"]);
                                                            }
                                                            else
                                                            {
                                                                detention = detention - d5;
                                                                vlDemurr = d5 * (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                                                if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                                                {
                                                                    if (detention - d6 <= 0)
                                                                    {
                                                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_06"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        detention = detention - d6;
                                                                        vlDemurr = d6 * (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                                        if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                                        {
                                                                            if (detention - d7 <= 0)
                                                                            {
                                                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_07"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                detention = detention - d7;
                                                                                vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                                                if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                                                {
                                                                                    if (detention - d8 <= 0)
                                                                                    {
                                                                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_08"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        detention = detention - d8;
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
                    SQL = "UPDATE TB_CNTR_DETENTION SET DT_INICIAL_FREETIME = '" + listTable.Rows[0]["DT_INICIAL_FREETIME"] + "', ";
                    SQL += "DT_FINAL_FREETIME = '" + listTable.Rows[0]["DT_FINAL_FREETIME"] + "', DT_INICIAL_DETENTION = '" + listTable.Rows[0]["DT_INICIAL_DETENTION"] + "', ";
                    SQL += "DT_FINAL_DETENTION = '" + listTable.Rows[0]["DT_FINAL_DETENTION"] + "', QT_DIAS_DETENTION_COMPRA = " + (int)listTable.Rows[0]["QT_DIAS_DETENTION_COMPRA"] + ", ";
                    SQL += "DT_CALCULO_DETENTION_COMPRA = '" + sqlFormattedDate + "', ID_MOEDA_DETENTION_COMPRA = " + listTable.Rows[0]["ID_MOEDA"] + ", VL_TAXA_DETENTION_COMPRA = 0, ";
                    SQL += "VL_DETENTION_COMPRA = " + vlDemurr.ToString().Replace(",", ".") + ", DT_CAMBIO_DETENTION_COMPRA = null, VL_CAMBIO_DETENTION_COMPRA = null, VL_DESCONTO_DETENTION_COMPRA = null, VL_DETENTION_COMPRA_BR = null WHERE ID_CNTR_BL = " + idCont + "";
                    calcular = DBS.ExecuteScalar(SQL);

                    string flagF;
                    SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + idStatus + "' ";
                    DataTable flFinaliza = new DataTable();
                    flFinaliza = DBS.List(SQL);
                    flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();

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

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION_COMPRA = '" + idStatus + "',DT_STATUS_DETENTION_COMPRA = '" + dtStatus + "' WHERE ID_CNTR_BL = " + idCont + " ";
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
            SQL += "ISNULL(FORMAT(PFCL.DT_STATUS_DETENTION,'dd/MM/yyyy'),'') AS DATA_STATUS_DETENTION, ";
            SQL += "PFCL.DS_STATUS_DETENTION ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "WHERE PFCL.NR_PROCESSO = '" + nrProcesso + "' ";
            SQL += "AND (DFCL.DT_EXPORTACAO_DETENTION_PAGAR IS NULL AND DFCL.DT_EXPORTACAO_DETENTION_RECEBER IS NULL) ";

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
            SQL += "ISNULL(FORMAT(PFCL.DT_STATUS_DETENTION,'dd/MM/yyyy'),'') AS DATA_STATUS_DETENTION, ";
            SQL += "PFCL.DS_STATUS_DETENTION ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "WHERE PFCL.NR_PROCESSO = '" + nrProcesso + "' ";
            SQL += "AND (DFCL.DT_EXPORTACAO_DETENTION_PAGAR IS NULL AND DFCL.DT_EXPORTACAO_DETENTION_RECEBER IS NULL) ";

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
                            Ativo = "AND C.DT_CANCELAMENTO IS NULL AND C.DT_LIQUIDACAO IS NULL AND C.DT_EXPORTACAO_DETENTION IS NULL ";
                            break;
                        default:
                            Ativo = "";
                            break;
                    }

                    switch (Finalizado)
                    {
                        case "1":
                            Finalizado = "AND (C.DT_CANCELAMENTO IS NOT NULL OR C.DT_LIQUIDACAO IS NOT NULL OR C.DT_EXPORTACAO_DETENTION IS NOT NULL) ";
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
                            Ativo = "AND C.DT_CANCELAMENTO IS NULL AND C.DT_LIQUIDACAO IS NULL AND C.DT_EXPORTACAO_DETENTION_COMPRA IS NULL ";
                            break;
                        default:
                            Ativo = "";
                            break;
                    }

                    switch (Finalizado)
                    {
                        case "1":
                            Finalizado = "AND (C.DT_CANCELAMENTO IS NOT NULL OR C.DT_LIQUIDACAO IS NOT NULL OR C.DT_EXPORTACAO_DETENTION_COMPRA IS NOT NULL) ";
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
                /*SQL = "SELECT C.ID_DETENTION_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DETENTION,'dd/MM/yyyy'),'') as DT_EXPORTACAO_DETENTION, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO,'dd/MM/yyyy'),'') AS DT_LIQUIDACAO,ISNULL(FORMAT(C.DT_CANCELAMENTO,'dd/MM/yyyy'),'') AS DT_CANCELAMENTO ";
                SQL += "FROM VW_DETENTION_FATURA C ";
                SQL += "JOIN TB_DETENTION_FATURA B ON C.ID_DETENTION_FATURA = B.ID_DETENTION_FATURA ";
                SQL += "WHERE B.CD_PR = 'R' ";*/
                SQL = "SELECT C.ID_DETENTION_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DETENTION, 'dd/MM/yyyy'), '') as DT_EXPORTACAO_DETENTION, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO, 'dd/MM/yyyy'), '') AS DT_LIQUIDACAO, ISNULL(FORMAT(C.DT_CANCELAMENTO, 'dd/MM/yyyy'), '') AS DT_CANCELAMENTO, ";
                SQL += "(SELECT MAX(CASE WHEN E.DT_CAMBIO_DETENTION_VENDA IS NULL THEN 1 ELSE 0 END) ";
                SQL += "FROM TB_DETENTION_FATURA_ITENS D INNER JOIN TB_CNTR_DETENTION E ON E.ID_CNTR_DETENTION = D.ID_CNTR_DETENTION ";
                SQL += "WHERE D.ID_DETENTION_FATURA = C.ID_DETENTION_FATURA) AS FALTA_ATUALIZACAO_CAMBIAL, ";
                SQL += "(SELECT COUNT(*) FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA = C.ID_DETENTION_FATURA) AS QT_PARCELAS ";
                SQL += "FROM VW_DETENTION_FATURA C ";
                SQL += "JOIN TB_DETENTION_FATURA B ON C.ID_DETENTION_FATURA = B.ID_DETENTION_FATURA ";
                SQL += "WHERE B.CD_PR = 'R' ";
                SQL += "" + filtroFatura + " ";
                SQL += "" + Ativo + " ";
                SQL += "" + Finalizado + " ";
                listTable = DBS.List(SQL);
            }
            else
            {
                string SQL;
                /*SQL = "SELECT C.ID_DETENTION_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DETENTION,'dd/MM/yyyy'),'') as DT_EXPORTACAO_DETENTION, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO,'dd/MM/yyyy'),'') AS DT_LIQUIDACAO,ISNULL(FORMAT(C.DT_CANCELAMENTO,'dd/MM/yyyy'),'') AS DT_CANCELAMENTO ";
                SQL += "FROM VW_DETENTION_FATURA C ";
                SQL += "JOIN TB_DETENTION_FATURA B ON C.ID_DETENTION_FATURA = B.ID_DETENTION_FATURA ";
                SQL += "WHERE B.CD_PR = 'P' ";*/
                SQL = "SELECT C.ID_DETENTION_FATURA, C.NR_PROCESSO, C.NM_CLIENTE, ";
                SQL += "C.NM_TRANSPORTADOR, ISNULL(FORMAT(C.DT_EXPORTACAO_DETENTION_COMPRA, 'dd/MM/yyyy'), '') as DT_EXPORTACAO_DETENTION, ";
                SQL += "ISNULL(FORMAT(C.DT_LIQUIDACAO, 'dd/MM/yyyy'), '') AS DT_LIQUIDACAO, ISNULL(FORMAT(C.DT_CANCELAMENTO, 'dd/MM/yyyy'), '') AS DT_CANCELAMENTO, ";
                SQL += "(SELECT MAX(CASE WHEN E.DT_CAMBIO_DETENTION_COMPRA IS NULL THEN 1 ELSE 0 END) ";
                SQL += "FROM TB_DETENTION_FATURA_ITENS D INNER JOIN TB_CNTR_DETENTION E ON E.ID_CNTR_DETENTION = D.ID_CNTR_DETENTION ";
                SQL += "WHERE D.ID_DETENTION_FATURA = C.ID_DETENTION_FATURA) AS FALTA_ATUALIZACAO_CAMBIAL, ";
                SQL += "(SELECT COUNT(*) FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA = C.ID_DETENTION_FATURA) AS QT_PARCELAS ";
                SQL += "FROM VW_DETENTION_FATURA C ";
                SQL += "JOIN TB_DETENTION_FATURA B ON C.ID_DETENTION_FATURA = B.ID_DETENTION_FATURA ";
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
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
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

            if (chkB == "1" && chkE == "1") { chkB = ""; chkE = ""; }
            else
            {
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
            SQL += "AND CONVERT(DATE, E.DT_ABERTURA,103) BETWEEN CONVERT(DATE, '" + sqlFormattedDate + "', 103)  ";
            SQL += "AND CONVERT(DATE, '" + sqlFormattedDate2 + "', 103)  ";
            SQL += "AND E.ID_BL IN(SELECT DISTINCT(A.ID_BL) FROM TB_CONTA_PAGAR_RECEBER_ITENS A ";
            SQL += "JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER ";
            SQL += "WHERE B.DT_CANCELAMENTO IS NULL AND A.ID_ITEM_DESPESA = 14 AND CD_PR = 'P') ";
            SQL += "" + chkE + " " + chkB + " AND(A.FL_TAXA_INATIVA = 0 OR A.FL_TAXA_INATIVA IS NULL) AND A.VL_TAXA <> 0.00 ";
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
                SQL += "M.NM_MOEDA ,ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_TAXA_DETENTION_VENDA, 'c', 'pt-br')), 'R$', ''), '') AS TAXA_DETENTION, FORMAT(DFCL.DT_INICIAL_DETENTION,'dd/MM/yyyy') as DT_INICIAL_DETENTION, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DETENTION,'dd/MM/yyyy') AS DT_FINAL_DETENTION,DFCL.QT_DIAS_DETENTION,REPLACE(FORMAT(DFCL.VL_DETENTION_VENDA,'C','PT-BR'),'R$','') AS VL_DETENTION ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DETENTION_VENDA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.NR_PROCESSO = '" + processo + "' ";
                SQL += "AND DFCL.ID_DETENTION_FATURA_RECEBER IS NULL ";
                SQL += "AND DFCL.VL_DETENTION_VENDA IS NOT NULL ";
                SQL += "AND DFCL.DT_EXPORTACAO_DETENTION_RECEBER IS NULL ";
                listTable = DBS.List(SQL);
            }
            else
            {
                SQL = "SELECT PFCL.ID_CNTR_BL, PFCL.NR_CNTR, PFCL.NM_TIPO_CONTAINER, ";
                SQL += "M.NM_MOEDA ,ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_TAXA_DETENTION_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS TAXA_DETENTION,FORMAT(DFCL.DT_INICIAL_DETENTION,'dd/MM/yyyy') as DT_INICIAL_DETENTION, ";
                SQL += "FORMAT(DFCL.DT_FINAL_DETENTION,'dd/MM/yyyy') AS DT_FINAL_DETENTION,ISNULL(DFCL.QT_DIAS_DETENTION_COMPRA,DFCL.QT_DIAS_DETENTION)QT_DIAS_DETENTION,ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(DFCL.VL_DETENTION_COMPRA, 'c', 'pt-br')), 'R$', ''), '') AS VL_DETENTION ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DETENTION_COMPRA = M.ID_MOEDA ";
                SQL += "WHERE PFCL.NR_PROCESSO = '" + processo + "' ";
                SQL += "AND DFCL.ID_DETENTION_FATURA_PAGAR IS NULL ";
                SQL += "AND DFCL.VL_DETENTION_COMPRA IS NOT NULL ";
                SQL += "AND DFCL.DT_EXPORTACAO_DETENTION_PAGAR IS NULL ";
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

                SQL = "INSERT INTO TB_DETENTION_FATURA (ID_BL, CD_PR, DT_LANCAMENTO, ID_USUARIO_LANCAMENTO) ";
                SQL += "VALUES (" + idbl + ",'R','" + sqlFormattedDate + "','" + Session["ID_USUARIO"] + "') SELECT SCOPE_IDENTITY() AS ID_DETENTION_FATURA ";
                string processarFatura = DBS.ExecuteScalar(SQL);
                return processarFatura;
            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_BL WHERE NR_PROCESSO = '" + processo + "' ";
                localizarFatura = DBS.List(SQL);
                int idbl = (int)localizarFatura.Rows[0]["ID_BL"];

                SQL = "INSERT INTO TB_DETENTION_FATURA (ID_BL, CD_PR, DT_LANCAMENTO, ID_USUARIO_LANCAMENTO) ";
                SQL += "VALUES (" + idbl + ",'P','" + sqlFormattedDate + "','" + Session["ID_USUARIO"] + "') SELECT SCOPE_IDENTITY() AS ID_DETENTION_FATURA ";
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
                SQL = "select ID_CNTR_DETENTION from tb_CNTR_DETENTION A ";
                SQL += "join TB_AMR_CNTR_BL B on A.ID_CNTR_BL = B.ID_CNTR_BL ";
                SQL += "where b.ID_CNTR_BL = " + idcntr + " ";
                localizarFatura = DBS.List(SQL);
                int idcntrDETENTION = (int)localizarFatura.Rows[0]["ID_CNTR_DETENTION"];

                SQL = "INSERT INTO TB_DETENTION_FATURA_ITENS (ID_DETENTION_FATURA, ID_CNTR_DETENTION) ";
                SQL += "VALUES (" + fatura + "," + idcntrDETENTION + ") ";
                string processarFatura = DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "select ID_CNTR_DETENTION from tb_CNTR_DETENTION A ";
                SQL += "join TB_AMR_CNTR_BL B on A.ID_CNTR_BL = B.ID_CNTR_BL ";
                SQL += "where b.ID_CNTR_BL = " + idcntr + "";
                localizarFatura = DBS.List(SQL);
                int idcntrDETENTION = (int)localizarFatura.Rows[0]["ID_CNTR_DETENTION"];

                SQL = "INSERT INTO TB_DETENTION_FATURA_ITENS (ID_DETENTION_FATURA, ID_CNTR_DETENTION) ";
                SQL += "VALUES (" + fatura + ",'" + idcntrDETENTION + "') ";
                string processarFatura = DBS.ExecuteScalar(SQL);
            }

        }

        [WebMethod(EnableSession = true)]
        public string infoAtualizacao(int idFatura)
        {
            string SQL;
            DataTable listTable = new DataTable();

            SQL = "SELECT MIN(A.ID_DETENTION_FATURA) AS ID_DETENTION_FATURA , MIN(B.NR_PROCESSO) AS NR_PROCESSO, MIN(P.NM_RAZAO) as CLIENTE, ";
            SQL += "ISNULL(CONVERT(VARCHAR,MIN(MF.VL_TXOFICIAL)),'') AS VL_TAXA, ISNULL(FORMAT(MIN(DT_CAMBIO),'dd/MM/yyyy'),'') AS DT_CAMBIO, ";
            SQL += "CASE WHEN A.CD_PR = 'R' THEN SUM(D.VL_DETENTION_VENDA_BR) ELSE SUM(D.VL_DETENTION_COMPRA_BR) END AS VL_DETENTION_TOTAL_BR ";
            SQL += "FROM TB_DETENTION_FATURA A ";
            SQL += "LEFT JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON B.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA_FRETE_ARMADOR MF ON B.ID_PARCEIRO_TRANSPORTADOR = MF.ID_ARMADOR ";
            SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS C ON A.ID_DETENTION_FATURA = C.ID_DETENTION_FATURA ";
            SQL += "LEFT JOIN TB_CNTR_DETENTION D ON D.ID_CNTR_DETENTION = C.ID_CNTR_DETENTION ";
            SQL += "WHERE A.ID_DETENTION_FATURA = " + idFatura + " ";
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
                SQL = "select D.ID_CNTR_BL, D.NR_CNTR, M.NM_MOEDA, REPLACE(FORMAT(E.VL_DETENTION_VENDA,'C','PT-BR'),'R$','') AS VL_DETENTION, ISNULL(REPLACE(FORMAT(E.VL_DESCONTO_DETENTION_VENDA,'C','PT-BR'),'R$',''),0) AS DESCONTO, ISNULL(REPLACE(FORMAT(E.VL_MULTA_DETENTION_VENDA,'C','PT-BR'),'R$',''),0) AS MULTA from tb_DETENTION_fatura_itens a ";
                SQL += "LEFT join TB_DETENTION_FATURA B ON B.ID_DETENTION_FATURA = a.ID_DETENTION_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DETENTION E ON E.ID_CNTR_DETENTION = a.ID_CNTR_DETENTION ";
                SQL += "LEFT JOIN TB_CNTR_BL D ON E.ID_CNTR_BL = D.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON E.ID_MOEDA_DETENTION_VENDA = M.ID_MOEDA ";
                SQL += "WHERE A.ID_DETENTION_FATURA = " + idFatura + " ";
                SQL += "AND B.DT_CANCELAMENTO IS NULL ";
                listTable = DBS.List(SQL);
            }
            else
            {
                SQL = "select D.ID_CNTR_BL, D.NR_CNTR, M.NM_MOEDA, REPLACE(FORMAT(E.VL_DETENTION_COMPRA,'C','PT-BR'),'R$','') AS VL_DETENTION, ISNULL(REPLACE(FORMAT(E.VL_DESCONTO_DETENTION_COMPRA,'C','PT-BR'),'R$',''),0) AS DESCONTO, ISNULL(REPLACE(FORMAT(E.VL_MULTA_DETENTION_COMPRA,'C','PT-BR'),'R$',''),0) AS MULTA from tb_DETENTION_fatura_itens a ";
                SQL += "LEFT join TB_DETENTION_FATURA B ON B.ID_DETENTION_FATURA = a.ID_DETENTION_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DETENTION E ON E.ID_CNTR_DETENTION = a.ID_CNTR_DETENTION ";
                SQL += "LEFT JOIN TB_CNTR_BL D ON E.ID_CNTR_BL = D.ID_CNTR_BL ";
                SQL += "LEFT JOIN TB_MOEDA M ON E.ID_MOEDA_DETENTION_COMPRA = M.ID_MOEDA ";
                SQL += "WHERE A.ID_DETENTION_FATURA = " + idFatura + " ";
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
            SQL = "UPDATE TB_DETENTION_FATURA SET DT_VENCIMENTO = '" + dtVencimento + "', ";
            SQL += "ID_CONTA_BANCARIA = '" + idContaBancaria + "' ";
            SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
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
        public string atualizacaoCambialContainer(int idCntr, string vlDetention, string dtCambio, double vlCambio, string descontoBRL, string multaBRL, int check)
        {
            string SQL;
            DataTable listTable = new DataTable();
            string vlDemur = vlDetention.Replace(".", "");
            double vlDemuDolar = Convert.ToDouble(vlDemur);
            double vlDemu = Trunc2(vlDemuDolar) * vlCambio;
            string descontor = descontoBRL.Replace(".", "");
            double desconto = Convert.ToDouble(descontor);
            string multar = multaBRL.Replace(".", "");
            double multa = Convert.ToDouble(multar);
            double valorLiquido = Trunc2(vlDemu) - Trunc2(desconto) + Trunc2(multa);
            double valorDETENTIONConvertido = Trunc2(vlDemu) * vlCambio;

            if (check == 1)
            {
                SQL = "UPDATE TB_CNTR_DETENTION SET DT_CAMBIO_DETENTION_VENDA = '" + dtCambio + "', ";
                SQL += "VL_CAMBIO_DETENTION_VENDA = '" + vlCambio.ToString().Replace(",", ".") + "', VL_DESCONTO_DETENTION_VENDA = '" + descontoBRL.ToString().Replace(",", ".") + "', ";
                SQL += "VL_DETENTION_VENDA_BR = '" + Trunc2(vlDemu).ToString().Replace(",", ".") + "', VL_DETENTION_LIQUIDO_VENDA = '" + Trunc2(valorLiquido).ToString().Replace(",", ".") + "', ";
                SQL += "VL_MULTA_DETENTION_VENDA = '" + multaBRL.ToString().Replace(",", ".") + "' ";
                SQL += "WHERE ID_CNTR_BL = '" + idCntr + "' ";
                string atualizarContainer = DBS.ExecuteScalar(SQL);
                return "ok";
            }
            else
            {
                SQL = "UPDATE TB_CNTR_DETENTION SET DT_CAMBIO_DETENTION_COMPRA = '" + dtCambio + "', ";
                SQL += "VL_CAMBIO_DETENTION_COMPRA = '" + vlCambio.ToString().Replace(",", ".") + "', VL_DESCONTO_DETENTION_COMPRA = '" + descontoBRL.ToString().Replace(",", ".") + "', ";
                SQL += "VL_DETENTION_COMPRA_BR = '" + Trunc2(vlDemu).ToString().Replace(",", ".") + "', VL_DETENTION_LIQUIDO_COMPRA = '" + Trunc2(valorLiquido).ToString().Replace(",", ".") + "', ";
                SQL += "VL_MULTA_DETENTION_COMPRA = '" + multaBRL.ToString().Replace(",", ".") + "' ";
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
                SQL = "select DT_EXPORTACAO_DETENTION FROM TB_DETENTION_FATURA WHERE ID_DETENTION_FATURA = " + idFatura + " ";
                DataTable listTableV = new DataTable();
                listTableV = DBS.List(SQL);
                if (listTableV.Rows[0]["DT_EXPORTACAO_DETENTION"].ToString() != "" && listTableV.Rows[0]["DT_EXPORTACAO_DETENTION"] != null)
                {
                    return "1";
                }
            }
            else
            {
                SQL = "select DT_EXPORTACAO_DETENTION_COMPRA FROM TB_DETENTION_FATURA WHERE ID_DETENTION_FATURA = " + idFatura + " ";
                DataTable listTableC = new DataTable();
                listTableC = DBS.List(SQL);
                if (listTableC.Rows[0]["DT_EXPORTACAO_DETENTION_COMPRA"].ToString() != "" && listTableC.Rows[0]["DT_EXPORTACAO_DETENTION_COMPRA"] != null)
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

            SQL = "select a.ID_DETENTION_FATURA, b.NR_PROCESSO, b.NM_CLIENTE ";
            SQL += "from tb_DETENTION_fatura a ";
            SQL += "join VW_DETENTION_FATURA b on a.ID_DETENTION_FATURA = b.ID_DETENTION_FATURA ";
            SQL += "WHERE A.ID_DETENTION_FATURA = " + idFatura + "";

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
                SQL = "select * from tb_DETENTION_fatura a ";
                SQL += "WHERE A.ID_DETENTION_FATURA = " + idFatura + " ";
                SQL += "AND A.DT_EXPORTACAO_DETENTION IS NOT NULL ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA = " + idFatura + "";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);

                if (listTable != null)
                {
                    SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
                    SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE DT_COMPETENCIA = '" + idFatura + "' ";
                    string deleteContaPagarReceber = DBS.ExecuteScalar(SQL);


                    for (int i = 0; i < listTable2.Rows.Count; i++)
                    {
                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
                        SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_CONTA_PAGAR_RECEBER = '" + listTable2.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "' ";
                        SQL += "UPDATE TB_FATURAMENTO SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
                        SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_CONTA_PAGAR_RECEBER = '" + listTable2.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "' ";
                        DBS.ExecuteScalar(SQL);
                    }

                    SQL = "UPDATE TB_DETENTION_FATURA SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', DT_EXPORTACAO_DETENTION = NULL, ";
                    SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                    string updateFatura = DBS.ExecuteScalar(SQL);

                    SQL = "SELECT C.ID_CNTR_BL FROM TB_DETENTION_FATURA A ";
                    SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS B ON B.ID_DETENTION_FATURA = A.ID_DETENTION_FATURA ";
                    SQL += "LEFT JOIN TB_CNTR_DETENTION C ON C.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
                    SQL += "WHERE A.ID_DETENTION_FATURA = '" + idFatura + "' ";
                    DataTable cntr = new DataTable();
                    cntr = DBS.List(SQL);
                    string cntrbl = cntr.Rows[0]["ID_CNTR_BL"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = 1 WHERE ID_CNTR_BL = '" + cntrbl + "' ";
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
                SQL = "select * from tb_DETENTION_fatura a ";
                SQL += "WHERE A.ID_DETENTION_FATURA = " + idFatura + " ";
                SQL += "AND A.DT_EXPORTACAO_DETENTION_COMPRA IS NOT NULL ";

                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA = " + idFatura + "";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);

                if (listTable != null)
                {
                    SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
                    SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE DT_COMPETENCIA = '" + idFatura + "' ";
                    string deleteContaPagarReceber = DBS.ExecuteScalar(SQL);

                    for (int i = 0; i < listTable2.Rows.Count; i++)
                    {
                        SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
                        SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_CONTA_PAGAR_RECEBER = '" + listTable2.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "' ";
                        SQL += "UPDATE TB_FATURAMENTO SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', ";
                        SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_CONTA_PAGAR_RECEBER = '" + listTable2.Rows[i]["ID_CONTA_PAGAR_RECEBER"] + "' ";
                        DBS.ExecuteScalar(SQL);
                    }

                    SQL = "UPDATE TB_DETENTION_FATURA SET DT_CANCELAMENTO = '" + sqlFormattedDate + "', ID_USUARIO_CANCELAMENTO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "', DT_EXPORTACAO_DETENTION_COMPRA = NULL, ";
                    SQL += "DS_MOTIVO_CANCELAMENTO = '" + motivoCancelamento + "' WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                    string updateFatura = DBS.ExecuteScalar(SQL);

                    SQL = "SELECT C.ID_CNTR_BL FROM TB_DETENTION_FATURA A ";
                    SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS B ON B.ID_DETENTION_FATURA = A.ID_DETENTION_FATURA ";
                    SQL += "LEFT JOIN TB_CNTR_DETENTION C ON C.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
                    SQL += "WHERE A.ID_DETENTION_FATURA = '" + idFatura + "' ";
                    DataTable cntr = new DataTable();
                    cntr = DBS.List(SQL);
                    string cntrbl = cntr.Rows[0]["ID_CNTR_BL"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = 1 WHERE ID_CNTR_BL = '" + cntrbl + "' ";
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

            SQL = "SELECT DISTINCT A.ID_DETENTION_FATURA, B.NR_PROCESSO, ";
            SQL += "P.NM_RAZAO AS CLIENTE, B.ID_STATUS_DETENTION ";
            SQL += "from TB_DETENTION_FATURA A ";
            SQL += "JOIN VW_PROCESSO_CONTAINER_FCL_EXP B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN VW_PROCESSO_DETENTION_FCL C ON B.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "JOIN TB_PARCEIRO P ON B.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "WHERE A.ID_DETENTION_FATURA = " + idFatura + "";

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
                SQL = "select b.ID_CNTR_BL, b.ID_BL, a.ID_DETENTION_FATURA, c.FL_DETENTION_FINALIZADA, ID_CONTA_BANCARIA,DT_EXPORTACAO_DETENTION, DT_CANCELAMENTO from tb_DETENTION_fatura a ";
                SQL += "inner join TB_AMR_CNTR_BL b on a.ID_BL = b.ID_BL ";
                SQL += "inner join TB_CNTR_BL c on b.ID_CNTR_BL = c.ID_CNTR_BL ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "" || listTable.Rows[0]["ID_CONTA_BANCARIA"] == null)
                {
                    return "1";
                }
                if ((listTable.Rows[0]["DT_EXPORTACAO_DETENTION"].ToString() != "" && listTable.Rows[0]["DT_EXPORTACAO_DETENTION"] != null) || (listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" && listTable.Rows[0]["DT_CANCELAMENTO"] != null))
                {
                    return "2";
                }
                if (listTable.Rows[0]["FL_DETENTION_FINALIZADA"].ToString() == "1")
                {
                    return "3";
                }
            }
            else
            {
                SQL = "select b.ID_CNTR_BL, b.ID_BL, a.ID_DETENTION_FATURA, c.FL_DETENTION_FINALIZADA, ID_CONTA_BANCARIA,DT_EXPORTACAO_DETENTION_COMPRA, DT_CANCELAMENTO from tb_DETENTION_fatura a ";
                SQL += "inner join TB_AMR_CNTR_BL b on a.ID_BL = b.ID_BL ";
                SQL += "inner join TB_CNTR_BL c on b.ID_CNTR_BL = c.ID_CNTR_BL ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "" || listTable.Rows[0]["ID_CONTA_BANCARIA"] == null)
                {
                    return "1";
                }
                if ((listTable.Rows[0]["DT_EXPORTACAO_DETENTION_COMPRA"].ToString() != "" && listTable.Rows[0]["DT_EXPORTACAO_DETENTION_COMPRA"] != null) || (listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" && listTable.Rows[0]["DT_CANCELAMENTO"] != null))
                {
                    return "2";
                }
                if (listTable.Rows[0]["FL_DETENTION_FINALIZADA"].ToString() == "1")
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
            SQL += "DT_EXPORTACAO_DETENTION, DT_CANCELAMENTO ";
            SQL += "from tb_DETENTION_fatura ";
            SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "'";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "")
            {
                return "null";
            }
            if (listTable.Rows[0]["DT_EXPORTACAO_DETENTION"].ToString() != "" || listTable.Rows[0]["DT_CANCELAMENTO"] == null || listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" || listTable.Rows[0]["DT_EXPORTACAO_DETENTION"] == null)
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
                SQL = "SELECT ID_BL FROM TB_DETENTION_FATURA ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                //SQL = "SELECT D.id_cntr_bl FROM TB_DETENTION_FATURA a ";
                //SQL += "join TB_AMR_CNTR_BL b on a.ID_BL = b.ID_BL ";
                //SQL += "JOIN TB_BL C ON C.ID_BL = b.ID_BL ";
                //SQL += "JOIN TB_CNTR_BL D ON b.ID_CNTR_BL = D.ID_CNTR_BL ";
                //SQL += "WHERE A.ID_BL = '"+idbl+"' AND D.FL_DETENTION_FINALIZADA = 0 ";               

                //DataTable listarContainers = new DataTable();
                //listarContainers = DBS.List(SQL);
                //int qtdRows = listarContainers.Rows.Count;
                //int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA ";
                SQL += ",ID_USUARIO_LANCAMENTO,DT_LIQUIDACAO,ID_USUARIO_LIQUIDACAO,CD_PR,DT_COMPETENCIA ";
                SQL += ",TP_EXPORTACAO) VALUES('" + dtLancamento + "','" + dtVencimento + "','" + idConta + "', ";
                SQL += "'" + idUsuario + "','" + dtLiquidacao + "','" + Session["ID_USUARIO"] + "','" + cdpr + "','" + idFatura + "','DEM') SELECT SCOPE_IDENTITY()";
                string insertConta = DBS.ExecuteScalar(SQL);

                SQL = "SELECT C.ID_CNTR_BL,C.ID_MOEDA_DETENTION_VENDA, C.VL_DETENTION_VENDA, D.ID_PARCEIRO_CLIENTE, ";
                SQL += "FORMAT(C.DT_CAMBIO_DETENTION_VENDA,'yyyy-MM-dd') AS DT_CAMBIO_DETENTION_VENDA, ";
                SQL += "C.VL_CAMBIO_DETENTION_VENDA, C.VL_DETENTION_VENDA_BR , ";
                SQL += "C.VL_DESCONTO_DETENTION_VENDA, C.VL_MULTA_DETENTION_VENDA, C.VL_DETENTION_LIQUIDO_VENDA ";
                SQL += "FROM TB_DETENTION_FATURA A ";
                SQL += "INNER JOIN TB_DETENTION_FATURA_ITENS B ON A.ID_DETENTION_FATURA = B.ID_DETENTION_FATURA ";
                SQL += "INNER JOIN TB_CNTR_DETENTION C ON B.ID_CNTR_DETENTION = C.ID_CNTR_DETENTION ";
                SQL += "LEFT  JOIN TB_BL D ON A.ID_BL = D.ID_BL ";
                SQL += "WHERE A.ID_DETENTION_FATURA =  '" + idFatura + "' ";

                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];



                for (i = 0; i < qtdRows; i++)
                {
                    //SQL = "SELECT ID_MOEDA_DETENTION_VENDA,VL_DETENTION_VENDA ";
                    //SQL += ",ID_PARCEIRO_CLIENTE,FORMAT(DT_CAMBIO_DETENTION_VENDA,'yyyy-MM-dd') AS DT_CAMBIO_DETENTION_VENDA ";
                    //SQL += ",VL_CAMBIO_DETENTION_VENDA,VL_DETENTION_VENDA_BR ";
                    //SQL += ",VL_DESCONTO_DETENTION_VENDA,VL_DETENTION_LIQUIDO_VENDA ";
                    //SQL += "FROM TB_CNTR_DETENTION A ";
                    //SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
                    //SQL += "LEFT JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
                    //SQL += "WHERE A.ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "'";
                    //DataTable vlDETENTION = new DataTable();
                    //vlDETENTION = DBS.List(SQL);

                    string idMoedaVenda = listarContainers.Rows[i]["ID_MOEDA_DETENTION_VENDA"].ToString();
                    string vlDetentionVenda = listarContainers.Rows[i]["VL_DETENTION_VENDA"].ToString().Replace(",", ".");
                    int parceiroCliente = (int)listarContainers.Rows[i]["ID_PARCEIRO_CLIENTE"];
                    string dtCambioVenda = listarContainers.Rows[i]["DT_CAMBIO_DETENTION_VENDA"].ToString();
                    string vlCambioDemuVenda = listarContainers.Rows[i]["VL_CAMBIO_DETENTION_VENDA"].ToString().Replace(",", ".");
                    string vlDemuVendaBR = listarContainers.Rows[i]["VL_DETENTION_VENDA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuVenda = listarContainers.Rows[i]["VL_DESCONTO_DETENTION_VENDA"].ToString().Replace(",", ".");
                    string vlMultaDemuVenda = listarContainers.Rows[i]["VL_MULTA_DETENTION_VENDA"].ToString().Replace(",", ".");
                    string vlDemuLiquidVenda = listarContainers.Rows[i]["VL_DETENTION_LIQUIDO_VENDA"].ToString().Replace(",", ".");

                    SQL = "SELECT ID_ITEM_DETENTION FROM TB_PARAMETROS ";
                    DataTable idItemDespesa = new DataTable();
                    idItemDespesa = DBS.List(SQL);
                    int idItemD = (int)idItemDespesa.Rows[0]["ID_ITEM_DETENTION"];

                    SQL = "SELECT FL_INTEGRA_PA FROM TB_ITEM_DESPESA ";
                    SQL += "WHERE ID_ITEM_DESPESA = '" + idItemD + "' ";
                    DataTable flIntegra = new DataTable();
                    flIntegra = DBS.List(SQL);
                    string flIntegraPA = flIntegra.Rows[0]["FL_INTEGRA_PA"].ToString();

                    SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER, ID_BL, ID_ITEM_DESPESA, ID_DESTINATARIO_COBRANCA, ";
                    SQL += "ID_MOEDA, ID_PARCEIRO_EMPRESA, VL_TAXA_CALCULADO, DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_DESCONTO, VL_MULTA, VL_LIQUIDO, FL_INTEGRA_PA) VALUES ";
                    SQL += "('" + insertConta + "','" + idbl + "','" + idItemD + "','1','" + idMoedaVenda + "', ";
                    SQL += "'" + parceiroCliente + "','" + vlDetentionVenda + "','" + dtCambioVenda + "','" + vlCambioDemuVenda + "','" + vlDemuVendaBR + "' ";
                    SQL += ",'" + vlDescDemuVenda + "', '" + vlMultaDemuVenda + "','" + vlDemuLiquidVenda + "','" + flIntegraPA + "') ";
                    string insertContaPGI = DBS.ExecuteScalar(SQL);

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = 10 WHERE ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "' ";
                    string updtDsStatus = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DETENTION_FATURA SET DT_EXPORTACAO_DETENTION = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DETENTION = '" + Session["ID_USUARIO"] + "', ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                string updtDETENTIONFatura = DBS.ExecuteScalar(SQL);

                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + cntrBl + "' ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DETENTION_RECEBER"].ToString();


            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_DETENTION_FATURA ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "SELECT * FROM TB_DETENTION_FATURA A ";
                SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS B ON B.ID_DETENTION_FATURA = A.ID_DETENTION_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DETENTION C ON C.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
                SQL += "WHERE A.ID_DETENTION_FATURA = '" + idFatura + "' ";
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
                    SQL = "SELECT ID_MOEDA_DETENTION_COMPRA,VL_DETENTION_COMPRA ";
                    SQL += ",ID_PARCEIRO_TRANSPORTADOR,FORMAT(DT_CAMBIO_DETENTION_COMPRA,'yyyy-MM-dd') AS DT_CAMBIO_DETENTION_COMPRA ";
                    SQL += ",VL_CAMBIO_DETENTION_COMPRA,VL_DETENTION_COMPRA_BR ";
                    SQL += ",VL_DESCONTO_DETENTION_COMPRA, VL_MULTA_DETENTION_COMPRA, VL_DETENTION_LIQUIDO_COMPRA ";
                    SQL += "FROM TB_CNTR_DETENTION A ";
                    SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
                    SQL += "WHERE A.ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "'";
                    DataTable vlDetention = new DataTable();
                    vlDetention = DBS.List(SQL);
                    int idMoedaCompra = (int)vlDetention.Rows[0]["ID_MOEDA_DETENTION_COMPRA"];
                    string vlDetentionCompra = vlDetention.Rows[0]["VL_DETENTION_COMPRA"].ToString().Replace(",", ".");
                    int parceiroTransportador = (int)vlDetention.Rows[0]["ID_PARCEIRO_TRANSPORTADOR"];
                    string dtCambioCompra = vlDetention.Rows[0]["DT_CAMBIO_DETENTION_COMPRA"].ToString();
                    string vlCambioDemuCompra = vlDetention.Rows[0]["VL_CAMBIO_DETENTION_COMPRA"].ToString().Replace(",", ".");
                    string vlDemuCompraBR = vlDetention.Rows[0]["VL_DETENTION_COMPRA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuCompra = vlDetention.Rows[0]["VL_DESCONTO_DETENTION_COMPRA"].ToString().Replace(",", ".");
                    string vlDemuLiquidCompra = vlDetention.Rows[0]["VL_DETENTION_LIQUIDO_COMPRA"].ToString().Replace(",", ".");
                    string vlMultaDemuCompra = vlDetention.Rows[0]["VL_MULTA_DETENTION_COMPRA"].ToString().Replace(",", ".");

                    SQL = "SELECT ID_ITEM_DETENTION FROM TB_PARAMETROS ";
                    DataTable idItemDespesa = new DataTable();
                    idItemDespesa = DBS.List(SQL);
                    int idItemD = (int)idItemDespesa.Rows[0]["ID_ITEM_DETENTION"];

                    SQL = "SELECT FL_INTEGRA_PA FROM TB_ITEM_DESPESA ";
                    SQL += "WHERE ID_ITEM_DESPESA = '" + idItemD + "' ";
                    DataTable flIntegra = new DataTable();
                    flIntegra = DBS.List(SQL);
                    string flIntegraPA = flIntegra.Rows[0]["FL_INTEGRA_PA"].ToString();

                    SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER_ITENS (ID_CONTA_PAGAR_RECEBER, ID_BL, ID_ITEM_DESPESA, ID_DESTINATARIO_COBRANCA, ";
                    SQL += "ID_MOEDA, ID_PARCEIRO_EMPRESA, VL_TAXA_CALCULADO, DT_CAMBIO,VL_CAMBIO,VL_LANCAMENTO,VL_DESCONTO, VL_MULTA, VL_LIQUIDO, FL_INTEGRA_PA) VALUES ";
                    SQL += "('" + insertConta + "','" + idbl + "','" + idItemD + "','1','" + idMoedaCompra + "', ";
                    SQL += "'" + parceiroTransportador + "','" + vlDetentionCompra + "','" + dtCambioCompra + "','" + vlCambioDemuCompra + "','" + vlDemuCompraBR + "' ";
                    SQL += ",'" + vlDescDemuCompra + "', '" + vlMultaDemuCompra + "', '" + vlDemuLiquidCompra + "','" + flIntegraPA + "') ";
                    string insertContaPGI = DBS.ExecuteScalar(SQL);

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION_COMPRA = 10 WHERE ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "' ";
                    string updtDsStatus = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DETENTION_FATURA SET DT_EXPORTACAO_DETENTION_COMPRA = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DETENTION = '" + Session["ID_USUARIO"] + "', ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                string updtDETENTIONFatura = DBS.ExecuteScalar(SQL);

                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL = '" + cntrBl + "' ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DETENTION_RECEBER"].ToString();


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
            SQL += "DT_EXPORTACAO_DETENTION, DT_CANCELAMENTO ";
            SQL += "from tb_DETENTION_fatura ";
            SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "'";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "")
            {
                return "null";
            }
            if (listTable.Rows[0]["DT_EXPORTACAO_DETENTION"].ToString() != "" || listTable.Rows[0]["DT_CANCELAMENTO"] == null || listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" || listTable.Rows[0]["DT_EXPORTACAO_DETENTION"] == null)
            {
                return "null";
            }


            SQL = "SELECT ID_DETENTION_FATURA_PARCELAS, ID_DETENTION_FATURA, VL_DETENTION_PARCELA, FORMAT(DT_VENCIMENTO_DETENTION_PARCELA,'yyyy-MM-dd') as DT_VENCIMENTO_DETENTION_PARCELA, ISNULL(VL_DETENTION_PARCELA_JUROS,0.00) VL_DETENTION_PARCELA_JUROS, ";
            SQL += "FL_PAGO, ISNULL(ID_CONTA_PAGAR_RECEBER,0) AS IDCPR FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA_PARCELAS = " + idFaturaParcela + "";

            DataTable listaParcela = new DataTable();
            listaParcela = DBS.List(SQL);




            string cdpr = listTable.Rows[0]["cd_pr"].ToString();
            string dtLancamento = listTable.Rows[0]["dt_lancamento"].ToString();
            int idUsuario = (int)listTable.Rows[0]["ID_USUARIO_LANCAMENTO"];
            string dtVencimento = listaParcela.Rows[0]["DT_VENCIMENTO_DETENTION_PARCELA"].ToString();
            int idConta = (int)listTable.Rows[0]["ID_CONTA_BANCARIA"];
            int idCPR = (int)listaParcela.Rows[0]["IDCPR"];
            string flagF;

            if (idCPR > 0) { return JsonConvert.SerializeObject("2"); }

            if (check == 1)
            {
                SQL = "SELECT ID_BL FROM TB_DETENTION_FATURA ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA ";
                SQL += ",ID_USUARIO_LANCAMENTO,CD_PR,DT_COMPETENCIA ";
                SQL += ",TP_EXPORTACAO) VALUES('" + dtLancamento + "','" + dtVencimento + "','" + idConta + "', ";
                SQL += "'" + idUsuario + "','" + cdpr + "','" + idFaturaParcela + "','DEM') SELECT SCOPE_IDENTITY()";
                string insertConta = DBS.ExecuteScalar(SQL);

                SQL = "SELECT C.ID_CNTR_BL,C.ID_MOEDA_DETENTION_VENDA, C.VL_DETENTION_VENDA, D.ID_PARCEIRO_CLIENTE, ";
                SQL += "FORMAT(C.DT_CAMBIO_DETENTION_VENDA,'yyyy-MM-dd') AS DT_CAMBIO_DETENTION_VENDA, ";
                SQL += "C.VL_CAMBIO_DETENTION_VENDA, C.VL_DETENTION_VENDA_BR , ";
                SQL += "C.VL_DESCONTO_DETENTION_VENDA, C.VL_DETENTION_LIQUIDO_VENDA ";
                SQL += "FROM TB_DETENTION_FATURA A ";
                SQL += "INNER JOIN TB_DETENTION_FATURA_ITENS B ON A.ID_DETENTION_FATURA = B.ID_DETENTION_FATURA ";
                SQL += "INNER JOIN TB_CNTR_DETENTION C ON B.ID_CNTR_DETENTION = C.ID_CNTR_DETENTION ";
                SQL += "LEFT  JOIN TB_BL D ON A.ID_BL = D.ID_BL ";
                SQL += "WHERE A.ID_DETENTION_FATURA =  '" + idFatura + "' ";

                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];



                double vlParcela = (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA"]) + (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA"]) * (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA_JUROS"]) / 100))) / qtdRows;


                for (i = 0; i < qtdRows; i++)
                {
                    //string idMoedaVenda = listarContainers.Rows[i]["ID_MOEDA_DETENTION_VENDA"].ToString();
                    //string vlDETENTIONVenda = listarContainers.Rows[i]["VL_DETENTION_VENDA"].ToString().Replace(",", ".");
                    int parceiroCliente = (int)listarContainers.Rows[i]["ID_PARCEIRO_CLIENTE"];
                    string dtCambioVenda = listarContainers.Rows[i]["DT_CAMBIO_DETENTION_VENDA"].ToString();
                    string vlCambioDemuVenda = listarContainers.Rows[i]["VL_CAMBIO_DETENTION_VENDA"].ToString().Replace(",", ".");
                    string vlDemuVendaBR = listarContainers.Rows[i]["VL_DETENTION_VENDA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuVenda = listarContainers.Rows[i]["VL_DESCONTO_DETENTION_VENDA"].ToString().Replace(",", ".");
                    string vlDemuLiquidVenda = listarContainers.Rows[i]["VL_DETENTION_LIQUIDO_VENDA"].ToString().Replace(",", ".");

                    SQL = "SELECT ID_ITEM_DETENTION FROM TB_PARAMETROS ";
                    DataTable idItemDespesa = new DataTable();
                    idItemDespesa = DBS.List(SQL);
                    int idItemD = (int)idItemDespesa.Rows[0]["ID_ITEM_DETENTION"];

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

                SQL = "UPDATE TB_DETENTION_FATURA_PARCELAS SET ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DETENTION_FATURA_PARCELAS = '" + idFaturaParcela + "' ";
                DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_DETENTION_FATURA ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "SELECT * FROM TB_DETENTION_FATURA A ";
                SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS B ON B.ID_DETENTION_FATURA = A.ID_DETENTION_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DETENTION C ON C.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
                SQL += "WHERE A.ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                double vlParcela = (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA"]) + (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA"]) * (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA_JUROS"]) / 100))) / qtdRows;

                SQL = "INSERT INTO TB_CONTA_PAGAR_RECEBER (DT_LANCAMENTO,DT_VENCIMENTO,ID_CONTA_BANCARIA ";
                SQL += ",ID_USUARIO_LANCAMENTO,CD_PR,DT_COMPETENCIA ";
                SQL += ",TP_EXPORTACAO) VALUES('" + dtLancamento + "','" + dtVencimento + "','" + idConta + "', ";
                SQL += "'" + idUsuario + "','" + cdpr + "','" + idFatura + "','DEM') SELECT SCOPE_IDENTITY() ";
                string insertConta = DBS.ExecuteScalar(SQL);

                for (i = 0; i < qtdRows; i++)
                {
                    SQL = "SELECT ID_MOEDA_DETENTION_COMPRA,VL_DETENTION_COMPRA ";
                    SQL += ",ID_PARCEIRO_TRANSPORTADOR,FORMAT(DT_CAMBIO_DETENTION_COMPRA,'yyyy-MM-dd') AS DT_CAMBIO_DETENTION_COMPRA ";
                    SQL += ",VL_CAMBIO_DETENTION_COMPRA,VL_DETENTION_COMPRA_BR ";
                    SQL += ",VL_DESCONTO_DETENTION_COMPRA,VL_DETENTION_LIQUIDO_COMPRA ";
                    SQL += "FROM TB_CNTR_DETENTION A ";
                    SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
                    SQL += "WHERE A.ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "'";
                    DataTable vlDetention = new DataTable();
                    vlDetention = DBS.List(SQL);
                    int idMoedaCompra = (int)vlDetention.Rows[0]["ID_MOEDA_DETENTION_COMPRA"];
                    string vlDetentionCompra = vlDetention.Rows[0]["VL_DETENTION_COMPRA"].ToString().Replace(",", ".");
                    int parceiroTransportador = (int)vlDetention.Rows[0]["ID_PARCEIRO_TRANSPORTADOR"];
                    string dtCambioCompra = vlDetention.Rows[0]["DT_CAMBIO_DETENTION_COMPRA"].ToString();
                    string vlCambioDemuCompra = vlDetention.Rows[0]["VL_CAMBIO_DETENTION_COMPRA"].ToString().Replace(",", ".");
                    string vlDemuCompraBR = vlDetention.Rows[0]["VL_DETENTION_COMPRA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuCompra = vlDetention.Rows[0]["VL_DESCONTO_DETENTION_COMPRA"].ToString().Replace(",", ".");
                    string vlDemuLiquidCompra = vlDetention.Rows[0]["VL_DETENTION_LIQUIDO_COMPRA"].ToString().Replace(",", ".");

                    SQL = "SELECT ID_ITEM_DETENTION FROM TB_PARAMETROS ";
                    DataTable idItemDespesa = new DataTable();
                    idItemDespesa = DBS.List(SQL);
                    int idItemD = (int)idItemDespesa.Rows[0]["ID_ITEM_DETENTION"];

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
                SQL = "UPDATE TB_DETENTION_FATURA_PARCELAS SET ID_CONTA_PAGAR_RECEBER = '" + insertConta + "' ";
                SQL += "WHERE ID_DETENTION_FATURA_PARCELAS = '" + idFaturaParcela + "' ";
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
            SQL += "DT_EXPORTACAO_DETENTION, DT_CANCELAMENTO ";
            SQL += "from tb_DETENTION_fatura ";
            SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "'";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            if (listTable.Rows[0]["ID_CONTA_BANCARIA"].ToString() == "")
            {
                return "null";
            }
            if (listTable.Rows[0]["DT_EXPORTACAO_DETENTION"].ToString() != "" || listTable.Rows[0]["DT_CANCELAMENTO"] == null || listTable.Rows[0]["DT_CANCELAMENTO"].ToString() != "" || listTable.Rows[0]["DT_EXPORTACAO_DETENTION"] == null)
            {
                return "null";
            }


            SQL = "SELECT ID_DETENTION_FATURA_PARCELAS, ID_DETENTION_FATURA, VL_DETENTION_PARCELA, FORMAT(DT_VENCIMENTO_DETENTION_PARCELA,'yyyy-MM-dd') as DT_VENCIMENTO_DETENTION_PARCELA, ISNULL(VL_DETENTION_PARCELA_JUROS,0.00) VL_DETENTION_PARCELA_JUROS, ";
            SQL += "FL_PAGO, ISNULL(ID_CONTA_PAGAR_RECEBER,0) AS IDCPR FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA_PARCELAS = " + idFaturaParcela + "";

            DataTable listaParcela = new DataTable();
            listaParcela = DBS.List(SQL);




            string cdpr = listTable.Rows[0]["cd_pr"].ToString();
            string dtLancamento = listTable.Rows[0]["dt_lancamento"].ToString();
            int idUsuario = (int)listTable.Rows[0]["ID_USUARIO_LANCAMENTO"];
            string dtVencimento = listaParcela.Rows[0]["DT_VENCIMENTO_DETENTION_PARCELA"].ToString();
            int idConta = (int)listTable.Rows[0]["ID_CONTA_BANCARIA"];
            int idCPR = (int)listaParcela.Rows[0]["IDCPR"];
            string flagF;

            if (check == 1)
            {
                SQL = "SELECT ID_BL FROM TB_DETENTION_FATURA ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_LIQUIDACAO = '" + dtLiquidacao + "',ID_USUARIO_LIQUIDACAO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "' WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                string insertConta = DBS.ExecuteScalar(SQL);

                SQL = "SELECT C.ID_CNTR_BL,C.ID_MOEDA_DETENTION_VENDA, C.VL_DETENTION_VENDA, D.ID_PARCEIRO_CLIENTE, ";
                SQL += "FORMAT(C.DT_CAMBIO_DETENTION_VENDA,'yyyy-MM-dd') AS DT_CAMBIO_DETENTION_VENDA, ";
                SQL += "C.VL_CAMBIO_DETENTION_VENDA, C.VL_DETENTION_VENDA_BR , ";
                SQL += "C.VL_DESCONTO_DETENTION_VENDA, C.VL_DETENTION_LIQUIDO_VENDA ";
                SQL += "FROM TB_DETENTION_FATURA A ";
                SQL += "INNER JOIN TB_DETENTION_FATURA_ITENS B ON A.ID_DETENTION_FATURA = B.ID_DETENTION_FATURA ";
                SQL += "INNER JOIN TB_CNTR_DETENTION C ON B.ID_CNTR_DETENTION = C.ID_CNTR_DETENTION ";
                SQL += "LEFT  JOIN TB_BL D ON A.ID_BL = D.ID_BL ";
                SQL += "WHERE A.ID_DETENTION_FATURA =  '" + idFatura + "' ";

                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];



                double vlParcela = (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA"]) + (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA"]) * (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA_JUROS"]) / 100))) / qtdRows;


                for (i = 0; i < qtdRows; i++)
                {
                    //string idMoedaVenda = listarContainers.Rows[i]["ID_MOEDA_DETENTION_VENDA"].ToString();
                    //string vlDETENTIONVenda = listarContainers.Rows[i]["VL_DETENTION_VENDA"].ToString().Replace(",", ".");
                    int parceiroCliente = (int)listarContainers.Rows[i]["ID_PARCEIRO_CLIENTE"];
                    string dtCambioVenda = listarContainers.Rows[i]["DT_CAMBIO_DETENTION_VENDA"].ToString();
                    string vlCambioDemuVenda = listarContainers.Rows[i]["VL_CAMBIO_DETENTION_VENDA"].ToString().Replace(",", ".");
                    string vlDemuVendaBR = listarContainers.Rows[i]["VL_DETENTION_VENDA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuVenda = listarContainers.Rows[i]["VL_DESCONTO_DETENTION_VENDA"].ToString().Replace(",", ".");
                    string vlDemuLiquidVenda = listarContainers.Rows[i]["VL_DETENTION_LIQUIDO_VENDA"].ToString().Replace(",", ".");

                    SQL = "SELECT ID_ITEM_DETENTION FROM TB_PARAMETROS ";
                    DataTable idItemDespesa = new DataTable();
                    idItemDespesa = DBS.List(SQL);
                    int idItemD = (int)idItemDespesa.Rows[0]["ID_ITEM_DETENTION"];

                    SQL = "SELECT FL_INTEGRA_PA FROM TB_ITEM_DESPESA ";
                    SQL += "WHERE ID_ITEM_DESPESA = '" + idItemD + "' ";
                    DataTable flIntegra = new DataTable();
                    flIntegra = DBS.List(SQL);
                    string flIntegraPA = flIntegra.Rows[0]["FL_INTEGRA_PA"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = 10 WHERE ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "' ";
                    string updtDsStatus = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DETENTION_FATURA_PARCELAS SET DT_EXPORTACAO_PARCELA = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DETENTION_PARCELA ='" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "' ";
                SQL += "WHERE ID_DETENTION_FATURA_PARCELAS = '" + idFaturaParcela + "' ";
                string updtDETENTIONFatura = DBS.ExecuteScalar(SQL);

                SQL = "UPDATE TB_DETENTION_FATURA_PARCELAS SET FL_PAGO = 1 WHERE ID_DETENTION_FATURA_PARCELAS = " + idFaturaParcela + " ";

                DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "SELECT ID_BL FROM TB_DETENTION_FATURA ";
                SQL += "WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable2 = new DataTable();
                listTable2 = DBS.List(SQL);
                int idbl = (int)listTable2.Rows[0]["ID_BL"];

                SQL = "SELECT * FROM TB_DETENTION_FATURA A ";
                SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS B ON B.ID_DETENTION_FATURA = A.ID_DETENTION_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DETENTION C ON C.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
                SQL += "WHERE A.ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listarContainers = new DataTable();
                listarContainers = DBS.List(SQL);
                int qtdRows = listarContainers.Rows.Count;
                int cntrBl = (int)listarContainers.Rows[0]["id_cntr_bl"];

                double vlParcela = (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA"]) + (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA"]) * (Convert.ToDouble(listaParcela.Rows[0]["VL_DETENTION_PARCELA_JUROS"]) / 100))) / qtdRows;

                SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_LIQUIDACAO = '" + dtLiquidacao + "',ID_USUARIO_LIQUIDACAO = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "' WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                DBS.ExecuteScalar(SQL);

                for (i = 0; i < qtdRows; i++)
                {
                    SQL = "SELECT ID_MOEDA_DETENTION_COMPRA,VL_DETENTION_COMPRA ";
                    SQL += ",ID_PARCEIRO_TRANSPORTADOR,FORMAT(DT_CAMBIO_DETENTION_COMPRA,'yyyy-MM-dd') AS DT_CAMBIO_DETENTION_COMPRA ";
                    SQL += ",VL_CAMBIO_DETENTION_COMPRA,VL_DETENTION_COMPRA_BR ";
                    SQL += ",VL_DESCONTO_DETENTION_COMPRA,VL_DETENTION_LIQUIDO_COMPRA ";
                    SQL += "FROM TB_CNTR_DETENTION A ";
                    SQL += "LEFT JOIN TB_AMR_CNTR_BL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_BL C ON B.ID_BL = C.ID_BL ";
                    SQL += "WHERE A.ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "'";
                    DataTable vlDetention = new DataTable();
                    vlDetention = DBS.List(SQL);
                    int idMoedaCompra = (int)vlDetention.Rows[0]["ID_MOEDA_DETENTION_COMPRA"];
                    string vlDETENTIONCompra = vlDetention.Rows[0]["VL_DETENTION_COMPRA"].ToString().Replace(",", ".");
                    int parceiroTransportador = (int)vlDetention.Rows[0]["ID_PARCEIRO_TRANSPORTADOR"];
                    string dtCambioCompra = vlDetention.Rows[0]["DT_CAMBIO_DETENTION_COMPRA"].ToString();
                    string vlCambioDemuCompra = vlDetention.Rows[0]["VL_CAMBIO_DETENTION_COMPRA"].ToString().Replace(",", ".");
                    string vlDemuCompraBR = vlDetention.Rows[0]["VL_DETENTION_COMPRA_BR"].ToString().Replace(",", ".");
                    string vlDescDemuCompra = vlDetention.Rows[0]["VL_DESCONTO_DETENTION_COMPRA"].ToString().Replace(",", ".");
                    string vlDemuLiquidCompra = vlDetention.Rows[0]["VL_DETENTION_LIQUIDO_COMPRA"].ToString().Replace(",", ".");

                    SQL = "SELECT ID_ITEM_DETENTION FROM TB_PARAMETROS ";
                    DataTable idItemDespesa = new DataTable();
                    idItemDespesa = DBS.List(SQL);
                    int idItemD = (int)idItemDespesa.Rows[0]["ID_ITEM_DETENTION"];

                    SQL = "SELECT FL_INTEGRA_PA FROM TB_ITEM_DESPESA ";
                    SQL += "WHERE ID_ITEM_DESPESA = '" + idItemD + "' ";
                    DataTable flIntegra = new DataTable();
                    flIntegra = DBS.List(SQL);
                    string flIntegraPA = flIntegra.Rows[0]["FL_INTEGRA_PA"].ToString();

                    SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION_COMPRA = 10 WHERE ID_CNTR_BL = '" + listarContainers.Rows[i]["ID_CNTR_BL"] + "' ";
                    string updtDsStatus = DBS.ExecuteScalar(SQL);
                }
                SQL = "UPDATE TB_DETENTION_FATURA_PARCELA SET DT_EXPORTACAO_PARCELA = '" + sqlFormattedDate + "', ID_USUARIO_EXPORTACAO_DETENTION_PARCELA = '" + (Session["ID_USUARIO"] != null ? Session["ID_USUARIO"] : '3') + "' ";
                SQL += "WHERE ID_DETENTION_FATURA_PARCELAS = '" + idFaturaParcela + "' ";
                string updtDETENTIONFatura = DBS.ExecuteScalar(SQL);

                SQL = "UPDATE TB_DETENTION_FATURA_PARCELAS SET FL_PAGO = 1 WHERE ID_DETENTION_FATURA_PARCELAS = " + idFaturaParcela + " ";

                DBS.ExecuteScalar(SQL);

            }

            return JsonConvert.SerializeObject("OK");
        }


        [WebMethod(EnableSession = true)]
        public string finalizarFaturaDETENTION(int idFatura, int check)
        {
            string SQL;

            if (check == 1)
            {
                SQL = "UPDATE TB_DETENTION_FATURA SET DT_EXPORTACAO_DETENTION = GETDATE() WHERE ID_DETENTION_FATURA = " + idFatura + " ";
                DBS.ExecuteScalar(SQL);
            }
            else
            {
                SQL = "UPDATE TB_DETENTION_FATURA SET DT_EXPORTACAO_DETENTION_COMPRA = GETDATE() WHERE ID_DETENTION_FATURA = " + idFatura + " ";
                DBS.ExecuteScalar(SQL);
            }

            return JsonConvert.SerializeObject("OK");
        }

        [WebMethod(EnableSession = true)]
        public string imprimirDadosCalc(string id)
        {
            string SQL;
            SQL = "SELECT TOP 1 A.NR_PROCESSO ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP A ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
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
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP A ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
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
            SQL += "from TB_DETENTION_FATURA A ";
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
            SQL += "WHERE A.ID_DETENTION_FATURA = " + idFatura + " ";

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
            SQL += "FORMAT(B.DT_INICIAL_DETENTION,'dd/MM/yy') AS INICIALDEM, FORMAT(B.DT_FINAL_DETENTION,'dd/MM/yy') AS FINALDEM, ";
            SQL += "B.QT_DIAS_DETENTION, MD.SIGLA_MOEDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DETENTION_VENDA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DETENTION_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(B.VL_DETENTION_VENDA, 'C', 'PT-BR')), 'R$', ''),'') AS VL_DETENTION_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR, FORMAT(B.VL_DESCONTO_DETENTION_VENDA,'C','PT-BR')),'R$',''),'') AS VL_DESCONTO_DETENTION_VENDA ";
            SQL += "FROM TB_DETENTION_FATURA_ITENS DFI ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL B ON DFI.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL_EXP A ON B.ID_CNTR_BL = A.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_DETENTION_FATURA DF ON DF.ID_DETENTION_FATURA = DFI.ID_DETENTION_FATURA ";
            SQL += "LEFT JOIN TB_MOEDA MD ON B.ID_MOEDA_DETENTION_VENDA = MD.ID_MOEDA ";
            SQL += "WHERE DFI.ID_DETENTION_FATURA = '"+idFatura+"' ";
            SQL += "AND DF.CD_PR = 'R' ";
            SQL += "AND DF.DT_CANCELAMENTO IS NULL ";*/
            SQL = "SELECT DISTINCT ISNULL(A.NR_CNTR,'') AS NR_CNTR, ISNULL(A.NM_TIPO_CONTAINER,'') AS NM_TIPO_CONTAINER, ISNULL(FORMAT(B.DT_INICIAL_FREETIME,'dd/MM/yy'),'') AS INICIALFT, ";
            SQL += "ISNULL(FORMAT(B.DT_FINAL_FREETIME,'dd/MM/yy'),'') AS FINALFT,ISNULL(A.QT_DIAS_FREETIME,'') AS QT_DIAS_FREETIME, ";
            SQL += "ISNULL(FORMAT(B.DT_INICIAL_DETENTION,'dd/MM/yy'),'') AS INICIALDEM, ISNULL(FORMAT(B.DT_FINAL_DETENTION,'dd/MM/yy'),'') AS FINALDEM, ";
            SQL += "ISNULL(B.QT_DIAS_DETENTION,'') AS QT_DIAS_DETENTION, ISNULL(MD.SIGLA_MOEDA,'') AS SIGLA_MOEDA, ";
            SQL += "ISNULL(B.VL_TAXA_DETENTION_VENDA,0) AS VL_TAXA_DETENTION_VENDA, ";
            SQL += "ISNULL(B.VL_CAMBIO_DETENTION_VENDA,0) AS VL_CAMBIO_DETENTION_VENDA, ";
            SQL += "ISNULL(B.VL_DETENTION_LIQUIDO_VENDA,0) AS VL_DETENTION_LIQUIDO_VENDA, ";
            SQL += "ISNULL(CONVERT(DECIMAL(13,2),B.VL_DESCONTO_DETENTION_VENDA) * -1 + CONVERT(DECIMAL(13,2),B.VL_MULTA_DETENTION_VENDA),0) AS VL_DESCONTO_DETENTION_VENDA, ";
            SQL += "ISNULL(B.VL_DETENTION_VENDA_BR,0) AS VL_DETENTION_VENDA_BR ";
            SQL += "FROM TB_DETENTION_FATURA_ITENS DFI ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL B ON DFI.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL_EXP A ON B.ID_CNTR_BL = A.ID_CNTR_BL AND B.ID_BL = A.ID_BL ";
            SQL += "LEFT JOIN TB_DETENTION_FATURA DF ON DF.ID_DETENTION_FATURA = DFI.ID_DETENTION_FATURA ";
            SQL += "LEFT JOIN TB_MOEDA MD ON B.ID_MOEDA_DETENTION_VENDA = MD.ID_MOEDA ";
            SQL += "WHERE DFI.ID_DETENTION_FATURA = '" + idFatura + "' ";
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
            SQL += "ISNULL(FORMAT(B.DT_INICIAL_DETENTION,'dd/MM/yy'),'') AS INICIALDEM, ISNULL(FORMAT(B.DT_FINAL_DETENTION,'dd/MM/yy'),'') AS FINALDEM, ";
            SQL += "ISNULL(B.QT_DIAS_DETENTION_COMPRA,'') AS QT_DIAS_DETENTION_COMPRA, ISNULL(MD.SIGLA_MOEDA,'') AS SIGLA_MOEDA, ";
            SQL += "ISNULL(B.VL_TAXA_DETENTION_COMPRA,0) AS VL_TAXA_DETENTION_COMPRA, ";
            SQL += "ISNULL(B.VL_CAMBIO_DETENTION_COMPRA,0) AS VL_CAMBIO_DETENTION_COMPRA, ";
            SQL += "ISNULL(B.VL_DETENTION_LIQUIDO_COMPRA,0) AS VL_DETENTION_LIQUIDO_COMPRA, ";
            SQL += "ISNULL(CONVERT(DECIMAL(13,2),B.VL_DESCONTO_DETENTION_COMPRA) * -1 + CONVERT(DECIMAL(13,2),B.VL_MULTA_DETENTION_COMPRA),0) AS VL_DESCONTO_DETENTION_COMPRA, ";
            SQL += "ISNULL(B.VL_DETENTION_COMPRA_BR,0) AS VL_DETENTION_COMPRA_BR ";
            SQL += "FROM TB_DETENTION_FATURA_ITENS DFI ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL B ON DFI.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
            SQL += "LEFT JOIN VW_PROCESSO_CONTAINER_FCL_EXP A ON B.ID_CNTR_BL = A.ID_CNTR_BL AND B.ID_BL = A.ID_BL ";
            SQL += "LEFT JOIN TB_DETENTION_FATURA DF ON DF.ID_DETENTION_FATURA = DFI.ID_DETENTION_FATURA ";
            SQL += "LEFT JOIN TB_MOEDA MD ON B.ID_MOEDA_DETENTION_COMPRA = MD.ID_MOEDA ";
            SQL += "WHERE DFI.ID_DETENTION_FATURA = '" + idFatura + "' ";
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
            int detention = 0;
            int detentionv = 0;
            int def = 0;
            SQL = "select PFCL.ID_CNTR_BL, PFCL.NR_PROCESSO, B.NR_BL AS MBL, PFCL.NR_CNTR,PFCL.NM_TIPO_CONTAINER, ";
            SQL += "ISNULL(LEFT(P.NM_RAZAO,10),'') AS CLIENTE , ISNULL(LEFT(P2.NM_RAZAO,10),'') AS TRANSPORTADOR, FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy') AS DT_CHEGADA, ";
            SQL += "ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME),'') AS QT_DIAS_FREETIME, ISNULL(CONVERT(VARCHAR,PFCL.QT_DIAS_FREETIME_CONFIRMA),'') AS QT_DIAS_FREETIME_CONFIRMA, FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy') AS DT_FINAL_FREETIME, ";
            SQL += "ISNULL(FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy'),'') AS DT_DEVOLUCAO, ";
            SQL += "DFCL.QT_DIAS_DETENTION, DFCL.QT_DIAS_DETENTION_COMPRA, ";
            SQL += "VALOR_COMPRA_ESTIMADO = CASE WHEN DFCL.VL_DETENTION_COMPRA = 0 OR DFCL.VL_DETENTION_COMPRA IS NULL THEN 1 ELSE 0 END, ";
            SQL += "MOEDA_COMPRA = CASE WHEN DFCL.VL_DETENTION_COMPRA > 0 THEN ISNULL(M.NM_MOEDA,'') ELSE ISNULL('','') END, ";
            SQL += "VALOR_COMPRA = CASE WHEN DFCL.VL_DETENTION_COMPRA > 0 THEN DFCL.VL_DETENTION_COMPRA ELSE 0 END, ";
            SQL += "VALOR_COMPRA_REAL = CASE WHEN DFCL.VL_DETENTION_COMPRA > 0 THEN DFCL.VL_DETENTION_LIQUIDO_COMPRA ELSE 0 END, ";
            SQL += "DATA_PAGAMENTO = CASE WHEN DFCL.VL_DETENTION_COMPRA > 0 THEN ISNULL(FORMAT(DFCL.DT_PAGAMENTO_DETENTION,'dd/MM/yyyy'),'') ELSE ISNULL('','') END, ";
            SQL += "VALOR_VENDA_ESTIMADO = CASE WHEN DFCL.VL_DETENTION_VENDA = 0 OR DFCL.VL_DETENTION_VENDA IS NULL THEN 1 ELSE 0 END, ";
            SQL += "MOEDA_VENDA = CASE WHEN DFCL.VL_DETENTION_VENDA > 0 THEN ISNULL(M2.NM_MOEDA,'') ELSE ISNULL('','') END, ";
            SQL += "VALOR_VENDA = CASE WHEN DFCL.VL_DETENTION_VENDA > 0 THEN DFCL.VL_DETENTION_VENDA ELSE 0 END, ";
            SQL += "VALOR_VENDA_REAL = CASE WHEN DFCL.VL_DETENTION_VENDA > 0 THEN COALESCE(DFCL.VL_DETENTION_LIQUIDO_VENDA,0) ELSE 0 END, ";
            SQL += "DATA_RECEBIMENTO = CASE WHEN DFCL.VL_DETENTION_VENDA > 0 THEN ISNULL(FORMAT(DFCL.DT_RECEBIMENTO_DETENTION,'dd/MM/yyyy'),'') ELSE ISNULL('','') END, ";
            SQL += "PFCL.DS_STATUS_DETENTION, ISNULL(FORMAT(PFCL.DT_STATUS_DETENTION, 'dd/MM/yyyy'),'') AS DT_STATUS_DETENTION, ISNULL(PFCL.DS_OBSERVACAO,'') AS DS_OBSERVACAO ";
            SQL += "from VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_MOEDA M ON DFCL.ID_MOEDA_DETENTION_COMPRA = M.ID_MOEDA ";
            SQL += "LEFT JOIN TB_MOEDA M2 ON DFCL.ID_MOEDA_DETENTION_VENDA = M2.ID_MOEDA ";
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
                    SQL += "FORMAT(DFCL.DT_INICIAL_DETENTION,'yyyy-MM-dd') AS DT_INICIAL_DETENTION, PFCL.QT_DIAS_FREETIME, ";
                    SQL += "FORMAT(DFCL.DT_FINAL_DETENTION,'yyyy-MM-dd') AS DT_FINAL_DETENTION, DFCL.QT_DIAS_DETENTION, DFCL.ID_MOEDA_DETENTION_COMPRA, TBD.FL_ESCALONADA, M.NM_MOEDA, ";
                    SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                    SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                    SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                    SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                    SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                    SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
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

                            somaDias = (Int16)listTable.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable.Rows[0]["QT_DIAS_DETENTION"];

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

                            vlDemurr = (int)listTable.Rows[0]["QT_DIAS_DETENTION"] * vlTaxa;
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
                            detention = (int)listTable.Rows[0]["QT_DIAS_DETENTION"];
                            vlDemurr = 0;

                            if (somaDias <= ft)
                            {
                                vlDemurr = 0;
                            }
                            else
                            {
                                if (d1.ToString() != "0" && listTable.Rows[0]["QT_DIAS_01"] != null)
                                {
                                    if (detention - d1 <= 0)
                                    {
                                        vlDemurr = detention * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                    }
                                    else
                                    {
                                        detention = detention - d1;
                                        vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_01"];
                                        if (d2.ToString() != "0" && listTable.Rows[0]["QT_DIAS_02"] != null)
                                        {
                                            if (detention - d2 <= 0)
                                            {
                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_02"]);
                                            }
                                            else
                                            {
                                                detention = detention - d2;
                                                vlDemurr = d2 * (decimal)listTable.Rows[0]["VL_VENDA_02"];
                                                if (d3.ToString() != "0" && listTable.Rows[0]["QT_DIAS_03"] != null)
                                                {
                                                    if (detention - d3 <= 0)
                                                    {
                                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_03"]);
                                                    }
                                                    else
                                                    {
                                                        detention = detention - d3;
                                                        vlDemurr = d3 * (decimal)listTable.Rows[0]["VL_VENDA_03"];
                                                        if (d4.ToString() != "0" && listTable.Rows[0]["QT_DIAS_04"] != null)
                                                        {
                                                            if (detention - d4 <= 0)
                                                            {
                                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_04"]);
                                                            }
                                                            else
                                                            {
                                                                detention = detention - d4;
                                                                vlDemurr = d4 * (decimal)listTable.Rows[0]["VL_VENDA_04"];
                                                                if (d5.ToString() != "0" && listTable.Rows[0]["QT_DIAS_05"] != null)
                                                                {
                                                                    if (detention - d5 <= 0)
                                                                    {
                                                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_05"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        detention = detention - d5;
                                                                        vlDemurr = d5 * (decimal)listTable.Rows[0]["VL_VENDA_05"];
                                                                        if (d6.ToString() != "0" && listTable.Rows[0]["QT_DIAS_06"] != null)
                                                                        {
                                                                            if (detention - d6 <= 0)
                                                                            {
                                                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_06"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                detention = detention - d6;
                                                                                vlDemurr = d6 * (decimal)listTable.Rows[0]["VL_VENDA_06"];
                                                                                if (d7.ToString() != "0" && listTable.Rows[0]["QT_DIAS_07"] != null)
                                                                                {
                                                                                    if (detention - d7 <= 0)
                                                                                    {
                                                                                        vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_07"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        detention = detention - d7;
                                                                                        vlDemurr = d1 * (decimal)listTable.Rows[0]["VL_VENDA_07"];
                                                                                        if (d8.ToString() != "0" && listTable.Rows[0]["QT_DIAS_08"] != null)
                                                                                        {
                                                                                            if (detention - d8 <= 0)
                                                                                            {
                                                                                                vlDemurr = vlDemurr + (detention * (decimal)listTable.Rows[0]["VL_VENDA_08"]);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                detention = detention - d8;
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
                    SQL += "FORMAT(DFCL.DT_INICIAL_DETENTION,'yyyy-MM-dd') AS DT_INICIAL_DETENTION, PFCL.QT_DIAS_FREETIME, ";
                    SQL += "FORMAT(DFCL.DT_FINAL_DETENTION,'yyyy-MM-dd') AS DT_FINAL_DETENTION, DFCL.QT_DIAS_DETENTION, DFCL.ID_MOEDA_DETENTION_COMPRA, TBD.FL_ESCALONADA, M.NM_MOEDA, ";
                    SQL += "TBD.QT_DIAS_FREETIME as FreeTimeTab, TBD.QT_DIAS_01, TBD.QT_DIAS_02,TBD.QT_DIAS_03, TBD.QT_DIAS_04, ";
                    SQL += "TBD.QT_DIAS_05, TBD.QT_DIAS_06, TBD.QT_DIAS_07, TBD.QT_DIAS_08, ";
                    SQL += "TBD.VL_VENDA_01, TBD.VL_VENDA_02,TBD.VL_VENDA_03, TBD.VL_VENDA_04, ";
                    SQL += "TBD.VL_VENDA_05, TBD.VL_VENDA_06, TBD.VL_VENDA_07, TBD.VL_VENDA_08 ";
                    SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                    SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
                    SQL += "LEFT JOIN TB_TABELA_DETENTION TBD ON PFCL.ID_TIPO_CNTR = TBD.ID_TIPO_CONTAINER ";
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

                            somaDiasv = (Int16)listTable2.Rows[0]["QT_DIAS_FREETIME"] + (int)listTable2.Rows[0]["QT_DIAS_DETENTION"];

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

                            vlDemurrv = (int)listTable2.Rows[0]["QT_DIAS_DETENTION"] * vlTaxaV;
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
                            detentionv = (int)listTable2.Rows[0]["QT_DIAS_DETENTION"];
                            vlDemurrv = 0;

                            if (somaDiasv <= ftv)
                            {
                                vlDemurrv = 0;
                            }
                            else
                            {
                                if (d1v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_01"] != null)
                                {
                                    if (detentionv - d1v <= 0)
                                    {
                                        vlDemurrv = detentionv * (decimal)listTable2.Rows[0]["VL_VENDA_01"];
                                    }
                                    else
                                    {
                                        detentionv = detentionv - d1v;
                                        vlDemurrv = d1v * (decimal)listTable2.Rows[0]["VL_VENDA_01"];
                                        if (d2v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_02"] != null)
                                        {
                                            if (detentionv - d2v <= 0)
                                            {
                                                vlDemurrv = vlDemurrv + (detentionv * (decimal)listTable2.Rows[0]["VL_VENDA_02"]);
                                            }
                                            else
                                            {
                                                detentionv = detentionv - d2v;
                                                vlDemurrv = d2v * (decimal)listTable2.Rows[0]["VL_VENDA_02"];
                                                if (d3v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_03"] != null)
                                                {
                                                    if (detentionv - d3v <= 0)
                                                    {
                                                        vlDemurrv = vlDemurrv + (detentionv * (decimal)listTable2.Rows[0]["VL_VENDA_03"]);
                                                    }
                                                    else
                                                    {
                                                        detentionv = detentionv - d3v;
                                                        vlDemurrv = d3v * (decimal)listTable2.Rows[0]["VL_VENDA_03"];
                                                        if (d4v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_04"] != null)
                                                        {
                                                            if (detentionv - d4v <= 0)
                                                            {
                                                                vlDemurrv = vlDemurrv + (detentionv * (decimal)listTable2.Rows[0]["VL_VENDA_04"]);
                                                            }
                                                            else
                                                            {
                                                                detentionv = detentionv - d4v;
                                                                vlDemurrv = d4v * (decimal)listTable2.Rows[0]["VL_VENDA_04"];
                                                                if (d5v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_05"] != null)
                                                                {
                                                                    if (detentionv - d5v <= 0)
                                                                    {
                                                                        vlDemurrv = vlDemurrv + (detentionv * (decimal)listTable2.Rows[0]["VL_VENDA_05"]);
                                                                    }
                                                                    else
                                                                    {
                                                                        detentionv = detentionv - d5v;
                                                                        vlDemurrv = d5v * (decimal)listTable2.Rows[0]["VL_VENDA_05"];
                                                                        if (d6v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_06"] != null)
                                                                        {
                                                                            if (detentionv - d6v <= 0)
                                                                            {
                                                                                vlDemurrv = vlDemurrv + (detentionv * (decimal)listTable2.Rows[0]["VL_VENDA_06"]);
                                                                            }
                                                                            else
                                                                            {
                                                                                detentionv = detentionv - d6v;
                                                                                vlDemurrv = d6v * (decimal)listTable2.Rows[0]["VL_VENDA_06"];
                                                                                if (d7v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_07"] != null)
                                                                                {
                                                                                    if (detentionv - d7v <= 0)
                                                                                    {
                                                                                        vlDemurrv = vlDemurrv + (detentionv * (decimal)listTable2.Rows[0]["VL_VENDA_07"]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        detentionv = detentionv - d7v;
                                                                                        vlDemurrv = d1v * (decimal)listTable2.Rows[0]["VL_VENDA_07"];
                                                                                        if (d8v.ToString() != "0" && listTable2.Rows[0]["QT_DIAS_08"] != null)
                                                                                        {
                                                                                            if (detentionv - d8v <= 0)
                                                                                            {
                                                                                                vlDemurrv = vlDemurrv + (detentionv * (decimal)listTable2.Rows[0]["VL_VENDA_08"]);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                detentionv = detentionv - d8v;
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
                SQL = "SELECT DT_EXPORTACAO_DETENTION FROM TB_DETENTION_FATURA WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string dtExport = listTable.Rows[0]["DT_EXPORTACAO_DETENTION"].ToString();

                SQL = "SELECT COUNT(ID_CNTR_BL) AS COUNT_CNTR FROM TB_DETENTION_FATURA A ";
                SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS B ON B.ID_DETENTION_FATURA = A.ID_DETENTION_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DETENTION C ON C.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
                SQL += "WHERE A.ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable cntrCount = new DataTable();
                cntrCount = DBS.List(SQL);
                int cntrCounts = (int)cntrCount.Rows[0]["COUNT_CNTR"];

                if (dtExport == "")
                {
                    for (int i = 0; i < cntrCounts; i++)
                    {
                        SQL = "SELECT ID_CNTR_BL FROM TB_DETENTION_FATURA A ";
                        SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS B ON B.ID_DETENTION_FATURA = A.ID_DETENTION_FATURA ";
                        SQL += "LEFT JOIN TB_CNTR_DETENTION C ON C.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
                        SQL += "WHERE A.ID_DETENTION_FATURA = '" + idFatura + "' ";
                        DataTable cntr = new DataTable();
                        cntr = DBS.List(SQL);
                        string cntrbl = cntr.Rows[0]["ID_CNTR_BL"].ToString();


                        SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = 1 WHERE ID_CNTR_BL = '" + cntrbl + "' ";
                        DBS.ExecuteScalar(SQL);
                    }

                    SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA = " + idFatura + "";
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

                    SQL = "DELETE FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA = " + idFatura + " ";
                    DBS.ExecuteScalar(SQL);

                    SQL = "DELETE FROM TB_DETENTION_FATURA_ITENS WHERE ID_DETENTION_FATURA = '" + idFatura + "'";
                    DBS.ExecuteScalar(SQL);

                    SQL = "DELETE FROM TB_DETENTION_FATURA WHERE ID_DETENTION_FATURA = '" + idFatura + "'";
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
                SQL = "SELECT DT_EXPORTACAO_DETENTION_COMPRA FROM TB_DETENTION_FATURA WHERE ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);
                string dtExport = listTable.Rows[0]["DT_EXPORTACAO_DETENTION_COMPRA"].ToString();

                SQL = "SELECT COUNT(ID_CNTR_BL) AS COUNT_CNTR FROM TB_DETENTION_FATURA A ";
                SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS B ON B.ID_DETENTION_FATURA = A.ID_DETENTION_FATURA ";
                SQL += "LEFT JOIN TB_CNTR_DETENTION C ON C.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
                SQL += "WHERE A.ID_DETENTION_FATURA = '" + idFatura + "' ";
                DataTable cntrCount = new DataTable();
                cntrCount = DBS.List(SQL);
                int cntrCounts = (int)cntrCount.Rows[0]["COUNT_CNTR"];

                if (dtExport == "")
                {
                    for (int i = 0; i < cntrCounts; i++)
                    {
                        SQL = "SELECT ID_CNTR_BL FROM TB_DETENTION_FATURA A ";
                        SQL += "LEFT JOIN TB_DETENTION_FATURA_ITENS B ON B.ID_DETENTION_FATURA = A.ID_DETENTION_FATURA ";
                        SQL += "LEFT JOIN TB_CNTR_DETENTION C ON C.ID_CNTR_DETENTION = B.ID_CNTR_DETENTION ";
                        SQL += "WHERE A.ID_DETENTION_FATURA = '" + idFatura + "' ";
                        DataTable cntr = new DataTable();
                        cntr = DBS.List(SQL);
                        string cntrbl = cntr.Rows[0]["ID_CNTR_BL"].ToString();


                        SQL = "UPDATE TB_CNTR_BL SET ID_STATUS_DETENTION = 1 WHERE ID_CNTR_BL = '" + cntrbl + "' ";
                        DBS.ExecuteScalar(SQL);
                    }

                    SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA = " + idFatura + "";
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

                    SQL = "DELETE FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA = " + idFatura + " ";
                    DBS.ExecuteScalar(SQL);

                    SQL = "DELETE FROM TB_DETENTION_FATURA_ITENS WHERE ID_DETENTION_FATURA = '" + idFatura + "'";
                    DBS.ExecuteScalar(SQL);

                    SQL = "DELETE FROM TB_DETENTION_FATURA WHERE ID_DETENTION_FATURA = '" + idFatura + "'";
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
            SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + dsStatus + "' ";
            DataTable flFinaliza = new DataTable();
            flFinaliza = DBS.List(SQL);
            flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();
            switch (dtDevolucao)
            {
                case "":
                    dtDevolucao = "null";
                    break;
            }
            if (dtDevolucao == "null")
            {
                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL in (" + idCont + ") ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DETENTION_RECEBER"].ToString();

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



                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = " + dtDevolucao + ", ID_STATUS_DETENTION = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DETENTION = '" + dtStatus + "' ";
                if (dsStatus == "2") { SQL += ", ID_STATUS_DETENTION_COMPRA = '" + dsStatus + "' "; }
                SQL += "WHERE ID_CNTR_BL in (" + idCont + ") ";
            }

            else
            {
                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL in (" + idCont + ") ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DETENTION_RECEBER"].ToString();

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

                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = '" + dtDevolucao + "', ID_STATUS_DETENTION = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DETENTION = '" + dtStatus + "' ";
                if (dsStatus == "2") { SQL += ", ID_STATUS_DETENTION_COMPRA = '" + dsStatus + "' "; }
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
            SQL = "SELECT FL_FINALIZA_DETENTION FROM TB_STATUS_DETENTION WHERE ID_STATUS_DETENTION = '" + dsStatus + "' ";
            DataTable flFinaliza = new DataTable();
            flFinaliza = DBS.List(SQL);
            flagF = flFinaliza.Rows[0]["FL_FINALIZA_DETENTION"].ToString();
            switch (dtDevolucao)
            {
                case "":
                    dtDevolucao = "null";
                    break;
            }
            if (dtDevolucao == "null")
            {
                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL in (" + idCont + ") ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DETENTION_RECEBER"].ToString();

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

                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = " + dtDevolucao + ", ID_STATUS_DETENTION = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DETENTION = '" + dtStatus + "' ";
                SQL += "WHERE ID_CNTR_BL in (" + idCont + ") ";
            }

            else
            {
                SQL = "SELECT ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_PAGAR),'') AS ID_DETENTION_PAGAR, ";
                SQL += "ISNULL(CONVERT(VARCHAR,DFCL.ID_DETENTION_FATURA_RECEBER),'') AS ID_DETENTION_RECEBER ";
                SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP_EXP PFCL ";
                SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL_EXP DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL AND PFCL.ID_BL = DFCL.ID_BL ";
                SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
                SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";
                SQL += "WHERE PFCL.ID_CNTR_BL in (" + idCont + ") ";
                DataTable faturas = new DataTable();
                faturas = DBS.List(SQL);
                string faturaCompra = faturas.Rows[0]["ID_DETENTION_PAGAR"].ToString();
                string faturaVenda = faturas.Rows[0]["ID_DETENTION_RECEBER"].ToString();

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

                SQL = "UPDATE TB_CNTR_BL SET DT_DEVOLUCAO_CNTR = '" + dtDevolucao + "', ID_STATUS_DETENTION = '" + dsStatus + "', ";
                SQL += "DT_STATUS_DETENTION = '" + dtStatus + "' ";
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
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP A ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
            SQL += "WHERE A.ID_CNTR_BL = '" + idprocess + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            string processoNr = listTable.Rows[0]["NR_PROCESSO"].ToString();

            SQL = "SELECT DISTINCT A.NR_CNTR, A.NM_TIPO_CONTAINER, ISNULL(FORMAT(B.DT_INICIAL_FREETIME,'dd/MM/yy'),'') AS INICIALFT, ";
            SQL += "ISNULL(FORMAT(B.DT_FINAL_FREETIME,'dd/MM/yy'),'') AS FINALFT,A.QT_DIAS_FREETIME, ";
            SQL += "ISNULL(FORMAT(B.DT_INICIAL_DETENTION,'dd/MM/yy'),'') AS INICIALDEM, ISNULL(FORMAT(B.DT_FINAL_DETENTION,'dd/MM/yy'),'') AS FINALDEM, ";
            SQL += "case when B.QT_DIAS_DETENTION < 1 then '' else convert(varchar,B.QT_DIAS_DETENTION) end QT_DIAS_DETENTION,";
            SQL += "case when isnull(B.QT_DIAS_DETENTION_COMPRA,0) < 1 then '' else convert(varchar,B.QT_DIAS_DETENTION_COMPRA) end QT_DIAS_DETENTION_COMPRA,";
            SQL += "ISNULL(MD.SIGLA_MOEDA,'') AS SIGLA_MOEDA, ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DETENTION_COMPRA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DETENTION_COMPRA, ";
            SQL += "ISNULL(REPLACE(FORMAT(B.VL_DETENTION_COMPRA,'C','PT-BR'),'R$',''),0) AS VL_DETENTION_COMPRA, ";
            SQL += "ISNULL(REPLACE(CONVERT(VARCHAR,FORMAT(B.VL_TAXA_DETENTION_VENDA,'C','PT-BR')),'R$',''),'') AS VL_TAXA_DETENTION_VENDA, ";
            SQL += "ISNULL(REPLACE(FORMAT(B.VL_DETENTION_VENDA, 'C', 'PT-BR'), 'R$', ''),0) AS VL_DETENTION_VENDA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP A ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL B ON A.ID_CNTR_BL = B.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_BL BL ON A.ID_BL = BL.ID_BL ";
            SQL += "LEFT JOIN TB_BL M ON BL.ID_BL_MASTER = M.ID_BL ";
            SQL += "LEFT JOIN TB_MOEDA MD ON B.ID_MOEDA_DETENTION_COMPRA = MD.ID_MOEDA ";
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
        public string listarDetentionVenda()
        {
            string SQL;

            SQL = "SELECT PFCL.NR_CNTR, FORMAT(PFCL.DT_CHEGADA, 'dd/MM/yyyy') AS DT_CHEGADA, ";
            SQL += "PFCL.QT_DIAS_FREETIME,FORMAT(DFCL.DT_FINAL_FREETIME, 'dd/MM/yyyy') AS FINAL_FREETIME, ";
            SQL += "FORMAT(PFCL.DT_DEVOLUCAO_CNTR, 'dd/MM/yyyy') AS DEVOLUCAO_CNTR, ";
            SQL += "DFCL.QT_DIAS_DETENTION, FORMAT(DFCL.DT_CALCULO_DETENTION_VENDA, 'dd/MM/yyyy') AS CALC_DEMU_VENDA, ";
            SQL += "DFCL.ID_MOEDA_DETENTION_VENDA,DFCL.VL_TAXA_DETENTION_VENDA, ";
            SQL += "DFCL.VL_DETENTION_VENDA, DFCL.ID_DETENTION_FATURA_RECEBER, ";
            SQL += "FORMAT(DFCL.DT_RECEBIMENTO_DETENTION, 'dd/MM/yyyy') AS RECEB_DEMU, ";
            SQL += "P.NM_FANTASIA ";
            SQL += "FROM VW_PROCESSO_CONTAINER_FCL_EXP PFCL ";
            SQL += "LEFT JOIN VW_PROCESSO_DETENTION_FCL DFCL ON PFCL.ID_CNTR_BL = DFCL.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_PARCEIRO P ON PFCL.ID_PARCEIRO_CLIENTE = P.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON PFCL.ID_PARCEIRO_TRANSPORTADOR = P2.ID_PARCEIRO ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string parcelarDetention(int idfatura, string vlDetentionParcela, int qtDetentionParcela, string dtPeriodoInicial)
        {
            string SQL;

            string cultureName = "pt-BR";

            CultureInfo culture = new CultureInfo(cultureName);

            DateTime dtVencimento = Convert.ToDateTime(dtPeriodoInicial, culture);

            for (int i = 0; i < qtDetentionParcela; i++)
            {
                SQL = "EXEC insert_detention_parcelas " + idfatura + ", '" + vlDetentionParcela.Replace(",", ".") + "', '" + dtVencimento + "',null, 0, null, null, null";

                DBS.ExecuteScalar(SQL);

                dtVencimento = dtVencimento.AddMonths(1);
            }



            return JsonConvert.SerializeObject("OK");
        }

        [WebMethod(EnableSession = true)]
        public string listarParcelamentoDetention(int fatura)
        {
            string SQL;

            SQL = "SELECT ID_DETENTION_FATURA_PARCELAS, VL_DETENTION_PARCELA, ";
            SQL += "FORMAT(DT_VENCIMENTO_DETENTION_PARCELA, 'dd/MM/yyyy') AS DT_VENCIMENTO, ";
            SQL += "ISNULL(VL_DETENTION_PARCELA_JUROS,0) AS VL_DETENTION_PARCELA_JUROS, CASE WHEN FL_PAGO=1 THEN 'PAGO' ELSE 'PENDENTE' END AS FL_PAGO, A.ID_CONTA_PAGAR_RECEBER, ";
            SQL += "FORMAT(B.DT_ENVIO_FATURAMENTO,'dd/MM/yyyy') AS DT_ENVIO_FATURAMENTO ";
            SQL += "FROM TB_DETENTION_FATURA_PARCELAS A ";
            SQL += "LEFT JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER ";
            SQL += "WHERE A.ID_DETENTION_FATURA = " + fatura + " ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string infoParcela(int idParcela)
        {
            string SQL;

            SQL = "SELECT ID_DETENTION_FATURA_PARCELAS, VL_DETENTION_PARCELA, FORMAT(DT_VENCIMENTO_DETENTION_PARCELA, 'yyyy-MM-dd') AS DT_VENCIMENTO_DETENTION_PARCELA, ";
            SQL += "VL_DETENTION_PARCELA_JUROS, FL_PAGO ";
            SQL += "FROM TB_DETENTION_FATURA_PARCELAS ";
            SQL += "WHERE ID_DETENTION_FATURA_PARCELAS = " + idParcela + " ";

            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);
            return JsonConvert.SerializeObject(listTable);
        }

        [WebMethod(EnableSession = true)]
        public string editarParcela(int idParcela, string vencimento, string vlParcela, string vlParcelaJuros, string vlJuros, string pago)
        {
            string SQL;

            SQL = "UPDATE TB_DETENTION_FATURA_PARCELAS SET VL_DETENTION_PARCELA ='" + vlParcela.Replace(",", ".") + "', DT_VENCIMENTO_DETENTION_PARCELA='" + vencimento + "', VL_DETENTION_PARCELA_JUROS='" + vlJuros.Replace(",", ".") + "', FL_PAGO=" + pago + " WHERE ID_DETENTION_FATURA_PARCELAS = " + idParcela + " ";

            DBS.ExecuteScalar(SQL);
            return JsonConvert.SerializeObject("Success");
        }

        [WebMethod(EnableSession = true)]
        public string deletarParcela(int idParcela)
        {
            string SQL;

            SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA_PARCELAS = " + idParcela + " ";
            string idCPR = DBS.ExecuteScalar(SQL);

            if (idCPR != "" && idCPR != "null")
            {
                SQL = "DELETE FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                DBS.ExecuteScalar(SQL);

                SQL = "DELETE FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                DBS.ExecuteScalar(SQL);

                SQL = "DELETE FROM TB_FATURAMENTO WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                DBS.ExecuteScalar(SQL);
            }
            SQL = "DELETE FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA_PARCELAS = " + idParcela + " ";
            DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("1");

        }

        [WebMethod(EnableSession = true)]
        public string cancelarExportarParcelaCC(int idParcela)
        {
            string SQL;

            SQL = "SELECT ID_CONTA_PAGAR_RECEBER FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA_PARCELAS = " + idParcela + " ";
            string idCPR = DBS.ExecuteScalar(SQL);

            if (idCPR != "" && idCPR != "null")
            {
                SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_LIQUIDACAO = NULL, ID_USUARIO_LIQUIDACAO = NULL WHERE ID_CONTA_PAGAR_RECEBER = " + idCPR + " ";
                DBS.ExecuteScalar(SQL);
            }

            SQL = "UPDATE TB_DETENTION_FATURA_PARCELAS SET DT_EXPORTACAO_PARCELA = NULL, ID_USUARIO_EXPORTACAO_DETENTION_PARCELA = NULL, FL_PAGO = 0 WHERE ID_DETENTION_FATURA_PARCELAS = " + idParcela + " ";
            DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("1");

        }

        [WebMethod(EnableSession = true)]
        public string EnviarFaturamento(int idParcela)
        {
            string SQL;

            SQL = "SELECT ID_CONTA_PAGAR_RECEBER, ID_DETENTION_FATURA, VL_DETENTION_PARCELA, ISNULL(VL_DETENTION_PARCELA_JUROS,0) as VL_DETENTION_PARCELA_JUROS FROM TB_DETENTION_FATURA_PARCELAS WHERE ID_DETENTION_FATURA_PARCELAS = " + idParcela + " ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            string vlDetentionParcela = listTable.Rows[0]["VL_DETENTION_PARCELA"].ToString();
            string vlDetentionParcelaJuros = listTable.Rows[0]["VL_DETENTION_PARCELA_JUROS"].ToString();
            string idcpr = listTable.Rows[0]["ID_CONTA_PAGAR_RECEBER"].ToString();
            string idfat = listTable.Rows[0]["ID_DETENTION_FATURA"].ToString();

            double vlNota = Convert.ToDouble(vlDetentionParcela) + (Convert.ToDouble(vlDetentionParcela) * (Convert.ToDouble(vlDetentionParcelaJuros) / 100));

            SQL = "SELECT B.ID_PARCEIRO_CLIENTE, C.NM_RAZAO, C.CNPJ, ISNULL(C.INSCR_ESTADUAL,'') AS INSCR_ESTADUAL, ISNULL(C.INSCR_MUNICIPAL,'') AS INSCR_MUNICIPAL, ";
            SQL += "ISNULL(C.ENDERECO,'') AS ENDERECO, C.NR_ENDERECO, C.COMPL_ENDERECO, C.BAIRRO, C.CEP, D.NM_CIDADE, E.NM_ESTADO ";
            SQL += "FROM VW_PROCESSO_DETENTION_FCL A ";
            SQL += "JOIN TB_BL B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_PARCEIRO C ON B.ID_PARCEIRO_CLIENTE = C.ID_PARCEIRO ";
            SQL += "JOIN TB_CIDADE D ON C.ID_CIDADE = D.ID_CIDADE ";
            SQL += "JOIN TB_ESTADO E ON D.ID_ESTADO = E.ID_ESTADO ";
            SQL += "WHERE(ID_DETENTION_FATURA_PAGAR = " + idfat + " OR ID_DETENTION_FATURA_RECEBER = " + idfat + ") ";

            DataTable InfoCliente = new DataTable();
            InfoCliente = DBS.List(SQL);

            SQL = "INSERT INTO TB_FATURAMENTO (ID_CONTA_PAGAR_RECEBER, VL_NOTA, ID_PARCEIRO_CLIENTE, NM_CLIENTE, CNPJ, INSCR_ESTADUAL, INSCR_MUNICIPAL, ENDERECO, NR_ENDERECO, COMPL_ENDERECO, BAIRRO, CEP, CIDADE, ESTADO) VALUES (" + idcpr + ", " + vlNota.ToString().Replace(",", ".") + "," + InfoCliente.Rows[0]["ID_PARCEIRO_CLIENTE"] + ", '" + InfoCliente.Rows[0]["NM_RAZAO"] + "','" + InfoCliente.Rows[0]["CNPJ"] + "', '" + InfoCliente.Rows[0]["INSCR_ESTADUAL"] + "','" + InfoCliente.Rows[0]["INSCR_MUNICIPAL"] + "', '" + InfoCliente.Rows[0]["ENDERECO"] + "','" + InfoCliente.Rows[0]["NR_ENDERECO"] + "', '" + InfoCliente.Rows[0]["COMPL_ENDERECO"] + "','" + InfoCliente.Rows[0]["BAIRRO"] + "', '" + InfoCliente.Rows[0]["CEP"] + "','" + InfoCliente.Rows[0]["NM_CIDADE"] + "', '" + InfoCliente.Rows[0]["NM_ESTADO"] + "') ";

            DBS.ExecuteScalar(SQL);

            SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET DT_ENVIO_FATURAMENTO = GETDATE() WHERE ID_CONTA_PAGAR_RECEBER = " + idcpr + " ";

            DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("1");

        }
    }
}