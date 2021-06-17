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
    /// Descrição resumida de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string CarregarTaxaClienteFCLimpo(string Id)
        {
            
            string SQL;
            
                SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA as ITEM,TB_BASE_CALCULO_TAXA.NM_BASE_CALCULO_TAXA,TB_MOEDA.NM_MOEDA, VL_TAXA_VENDA ";
                SQL += "FROM TB_TAXA_CLIENTE ";
                SQL += "INNER JOIN TB_ITEM_DESPESA ";
                SQL += "ON TB_TAXA_CLIENTE.ID_ITEM_DESPESA = TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
                SQL += "JOIN TB_BASE_CALCULO_TAXA ";
                SQL += "ON TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
                SQL += "JOIN TB_MOEDA ";
                SQL += "ON TB_TAXA_CLIENTE.ID_MOEDA_VENDA= TB_MOEDA.CD_MOEDA ";
                SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
                SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 1 ";
                SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 1 ";
                SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_ESTUFAGEM = 1 ";
                SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

                DataTable fclimpo = new DataTable();
                
                fclimpo = DBS.List(SQL);
                
                return JsonConvert.SerializeObject(fclimpo);
        }
        [WebMethod]
        public string CarregarTaxaClienteLCLimpo(string Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA as ITEM,TB_BASE_CALCULO_TAXA.NM_BASE_CALCULO_TAXA,TB_MOEDA.NM_MOEDA, VL_TAXA_VENDA ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "INNER JOIN TB_ITEM_DESPESA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_ITEM_DESPESA = TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "JOIN TB_MOEDA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_MOEDA_VENDA = TB_MOEDA.CD_MOEDA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_ESTUFAGEM = 2 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable lclimpo = new DataTable();
            lclimpo = DBS.List(SQL);
            return JsonConvert.SerializeObject(lclimpo);
        }
        [WebMethod]
        public string CarregarTaxaClienteFCLexpo(string Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA as ITEM,TB_BASE_CALCULO_TAXA.NM_BASE_CALCULO_TAXA,TB_MOEDA.NM_MOEDA, VL_TAXA_VENDA ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "INNER JOIN TB_ITEM_DESPESA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_ITEM_DESPESA = TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "JOIN TB_MOEDA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_MOEDA_VENDA= TB_MOEDA.CD_MOEDA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 2 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_ESTUFAGEM = 1 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable fclexpo = new DataTable();

            fclexpo = DBS.List(SQL);

            return JsonConvert.SerializeObject(fclexpo);
        }
        [WebMethod]
        public string CarregarTaxaClienteLCLexpo(string Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA as ITEM,TB_BASE_CALCULO_TAXA.NM_BASE_CALCULO_TAXA,TB_MOEDA.NM_MOEDA, VL_TAXA_VENDA ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "INNER JOIN TB_ITEM_DESPESA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_ITEM_DESPESA = TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "JOIN TB_MOEDA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_MOEDA_VENDA= TB_MOEDA.CD_MOEDA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 2 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_ESTUFAGEM = 2 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable lclexpo = new DataTable();

            lclexpo = DBS.List(SQL);

            return JsonConvert.SerializeObject(lclexpo);
        }
        [WebMethod]
        public string CarregarTaxaClienteAereoImpo(string Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA as ITEM,TB_BASE_CALCULO_TAXA.NM_BASE_CALCULO_TAXA,TB_MOEDA.NM_MOEDA, VL_TAXA_VENDA ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "INNER JOIN TB_ITEM_DESPESA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_ITEM_DESPESA = TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "JOIN TB_MOEDA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_MOEDA_VENDA= TB_MOEDA.CD_MOEDA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 2 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 1 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable aereoImpo = new DataTable();

            aereoImpo = DBS.List(SQL);

            return JsonConvert.SerializeObject(aereoImpo);
        }
        [WebMethod]
        public string CarregarTaxaClienteAereoExpo(string Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA as ITEM,TB_BASE_CALCULO_TAXA.NM_BASE_CALCULO_TAXA,TB_MOEDA.NM_MOEDA, VL_TAXA_VENDA ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "INNER JOIN TB_ITEM_DESPESA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_ITEM_DESPESA = TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "JOIN TB_MOEDA ";
            SQL += "ON TB_TAXA_CLIENTE.ID_MOEDA_VENDA= TB_MOEDA.CD_MOEDA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 2 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 2 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable aereoExpo = new DataTable();

            aereoExpo = DBS.List(SQL);

            return JsonConvert.SerializeObject(aereoExpo);
        }

        [WebMethod]
        public string BuscaCliente(int Id)
        {
            if (Id != 0)
            {
                string SQL;
                SQL = "SELECT TB_TAXA_CLIENTE.OB_TAXAS, TB_ITEM_DESPESA.ID_ITEM_DESPESA,TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA,VL_TAXA_COMPRA, ";
                SQL += "REPLICATE('0', 3 - LEN(ID_MOEDA_COMPRA)) + RTRIM(ID_MOEDA_COMPRA) AS MOEDA_COMPRA, VL_TAXA_VENDA, REPLICATE('0', 3 - LEN(ID_MOEDA_VENDA)) + RTRIM(ID_MOEDA_VENDA) AS MOEDA_VENDA, TB_TAXA_CLIENTE.FL_DECLARADO, TB_TAXA_CLIENTE.FL_DIVISAO_PROFIT, ";
                SQL += "TB_TAXA_CLIENTE.ID_DESTINATARIO_COBRANCA, TB_TAXA_CLIENTE.ID_TIPO_PAGAMENTO, TB_TAXA_CLIENTE.ID_ORIGEM_PAGAMENTO, ";
                SQL += "TB_TAXA_CLIENTE.ID_ITEM_DESPESA ";
                SQL += "FROM TB_TAXA_CLIENTE ";
                SQL += "INNER JOIN TB_ITEM_DESPESA ";
                SQL += "ON TB_TAXA_CLIENTE.ID_ITEM_DESPESA = TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
                SQL += "JOIN TB_BASE_CALCULO_TAXA ";
                SQL += "ON TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";             
                SQL += "JOIN TB_MOEDA ";
                SQL += "ON TB_TAXA_CLIENTE.ID_MOEDA_VENDA = TB_MOEDA.CD_MOEDA ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + Id + "'";

                DataTable carregarDados = new DataTable();
                carregarDados = DBS.List(SQL);
                Taxas resultado = new Taxas();
                resultado.ID_TAXA_CLIENTE = Id;
                resultado.ID_ITEM_DESPESA = (int)carregarDados.Rows[0]["ID_ITEM_DESPESA"];
                resultado.ID_BASE_CALCULO_TAXA = (int)carregarDados.Rows[0]["ID_BASE_CALCULO_TAXA"];
                resultado.VL_TAXA_COMPRA = carregarDados.Rows[0]["VL_TAXA_COMPRA"].ToString();
                resultado.ID_MOEDA_COMPRA = carregarDados.Rows[0]["MOEDA_COMPRA"].ToString();
                resultado.VL_TAXA_VENDA = (decimal)carregarDados.Rows[0]["VL_TAXA_VENDA"];
                resultado.ID_MOEDA_VENDA = carregarDados.Rows[0]["MOEDA_VENDA"].ToString();
                if (carregarDados.Rows[0]["FL_DECLARADO"].ToString() == "true")
                {
                    resultado.FL_DECLARADO = 1;
                }
                else
                {
                    resultado.FL_DECLARADO = 0;
                }
                if (carregarDados.Rows[0]["FL_DIVISAO_PROFIT"].ToString() == "true")
                {
                    resultado.FL_DIVISAO_PROFIT = 1;
                }
                else
                {
                    resultado.FL_DIVISAO_PROFIT = 0;
                }
                resultado.ID_DESTINATARIO_COBRANCA = (int)carregarDados.Rows[0]["ID_DESTINATARIO_COBRANCA"];
                resultado.ID_TIPO_PAGAMENTO = carregarDados.Rows[0]["ID_TIPO_PAGAMENTO"].ToString();
                resultado.ID_ORIGEM_PAGAMENTO = carregarDados.Rows[0]["ID_ORIGEM_PAGAMENTO"].ToString();
                resultado.OB_TAXAS = carregarDados.Rows[0]["OB_TAXAS"].ToString();



                return JsonConvert.SerializeObject(resultado);
            }
            else
            {
                return "selecione outro valor";
            }
        }

        public static string decBD(string numero)
        {
            if (string.IsNullOrEmpty(numero) == true) { return "0"; }

            return numero = numero.Replace(',', '.');
        }
        
        [WebMethod]
        public string CadastrarTaxaFCLimpo(Taxas dados)
        {
            if(dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if(dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if(dados.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dados.ID_ORIGEM_PAGAMENTO = "null";
            }
            if(dados.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dados.ID_TIPO_PAGAMENTO = "null";
            }
            string SQL;
            string vlTaxaVenda = dados.VL_TAXA_VENDA.ToString().Replace(',', '.');
            string vlTaxaCompra = dados.VL_TAXA_COMPRA.ToString().Replace(',', '.');

            if (dados.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA,";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM, ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,1,1,'"+dados.ID_ORIGEM_PAGAMENTO+"','"+ dados.ID_TIPO_PAGAMENTO + "') ";
                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,1,1,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string CadastrarTaxaLCLimpo(Taxas dados)
        {
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dados.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dados.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dados.ID_TIPO_PAGAMENTO = "null";
            }

            string SQL;
            string vlTaxaVenda = dados.VL_TAXA_VENDA.ToString().Replace(',', '.');
            string vlTaxaCompra = dados.VL_TAXA_COMPRA.ToString().Replace(',', '.');


            if (dados.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,1,2,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,1,2,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string CadastrarTaxaFCLexpo(Taxas dados)
        {
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dados.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dados.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dados.ID_TIPO_PAGAMENTO = "null";
            }

            string SQL;
            string vlTaxaVenda = dados.VL_TAXA_VENDA.ToString().Replace(',', '.');
            string vlTaxaCompra = dados.VL_TAXA_COMPRA.ToString().Replace(',', '.');


            if (dados.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,2,1,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,2,1,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string CadastrarTaxaLCLexpo(Taxas dados)
        {
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dados.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dados.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dados.ID_TIPO_PAGAMENTO = "null";
            }

            string SQL;
            string vlTaxaVenda = dados.VL_TAXA_VENDA.ToString().Replace(',', '.');
            string vlTaxaCompra = dados.VL_TAXA_COMPRA.ToString().Replace(',', '.');

            if (dados.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,2,2,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,2,2,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string CadastrarTaxaAereoImpo(Taxas dados)
        {
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dados.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dados.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dados.ID_TIPO_PAGAMENTO = "null";
            }

            string SQL;
            string vlTaxaVenda = dados.VL_TAXA_VENDA.ToString().Replace(',', '.');
            string vlTaxaCompra = dados.VL_TAXA_COMPRA.ToString().Replace(',', '.');

            if (dados.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',2,1,null,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',2,1,null,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string CadastrarTaxaAereoExpo(Taxas dados)
        {
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dados.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dados.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dados.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dados.ID_TIPO_PAGAMENTO = "null";
            }
            string SQL;
            string vlTaxaVenda = dados.VL_TAXA_VENDA.ToString().Replace(',', '.');
            string vlTaxaCompra = dados.VL_TAXA_COMPRA.ToString().Replace(',', '.');

            if (dados.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',2,2,null,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',2,2,null,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string EditarTaxaFCLimpo(Taxas dadosEdit)
        {
            if (dadosEdit.ID_TAXA_CLIENTE.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dadosEdit.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_TIPO_PAGAMENTO = "null";
            }
            string SQL;
            string vlTaxaVenda = decBD(dadosEdit.VL_TAXA_VENDA.ToString());
            string vlTaxaCompra = decBD(dadosEdit.VL_TAXA_COMPRA.ToString());

            if (dadosEdit.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = 0, VL_TAXA_COMPRA = 0, ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "',";
                SQL += "FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 1, ID_TIPO_ESTUFAGEM = 1 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = '" + dadosEdit.ID_MOEDA_COMPRA + "', VL_TAXA_COMPRA = '" + vlTaxaCompra + "', ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 1, ID_TIPO_ESTUFAGEM = 1 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string EditarTaxaLCLimpo(Taxas dadosEdit)
        {
            if (dadosEdit.ID_TAXA_CLIENTE.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dadosEdit.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_TIPO_PAGAMENTO = "null";
            }
            string SQL;
            string vlTaxaVenda = decBD(dadosEdit.VL_TAXA_VENDA.ToString());
            string vlTaxaCompra = decBD(dadosEdit.VL_TAXA_COMPRA.ToString());

            if (dadosEdit.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = 0, VL_TAXA_COMPRA = 0, ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 1, ID_TIPO_ESTUFAGEM = 2 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = '" + dadosEdit.ID_MOEDA_COMPRA + "', VL_TAXA_COMPRA = '" + vlTaxaCompra + "', ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 1, ID_TIPO_ESTUFAGEM = 2 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string EditarTaxaFCLexpo(Taxas dadosEdit)
        {
            if (dadosEdit.ID_TAXA_CLIENTE.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dadosEdit.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_TIPO_PAGAMENTO = "null";
            }
            string SQL;
            string vlTaxaVenda = decBD(dadosEdit.VL_TAXA_VENDA.ToString());
            string vlTaxaCompra = decBD(dadosEdit.VL_TAXA_COMPRA.ToString());

            if (dadosEdit.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = 0, VL_TAXA_COMPRA = 0, ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 2, ID_TIPO_ESTUFAGEM = 1 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = '" + dadosEdit.ID_MOEDA_COMPRA + "', VL_TAXA_COMPRA = '" + vlTaxaCompra + "', ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 2, ID_TIPO_ESTUFAGEM = 1 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string EditarTaxaLCLexpo(Taxas dadosEdit)
        {
            if (dadosEdit.ID_TAXA_CLIENTE.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dadosEdit.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_TIPO_PAGAMENTO = "null";
            }
            string SQL;
            string vlTaxaVenda = decBD(dadosEdit.VL_TAXA_VENDA.ToString());
            string vlTaxaCompra = decBD(dadosEdit.VL_TAXA_COMPRA.ToString());

            if (dadosEdit.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = 0, VL_TAXA_COMPRA = 0, ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 2, ID_TIPO_ESTUFAGEM = 2 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = '" + dadosEdit.ID_MOEDA_COMPRA + "', VL_TAXA_COMPRA = '" + vlTaxaCompra + "', ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 2, ID_TIPO_ESTUFAGEM = 2 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string EditarTaxaAereoImpo(Taxas dadosEdit)
        {
            if (dadosEdit.ID_TAXA_CLIENTE.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_MOEDA_COMPRA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dadosEdit.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_TIPO_PAGAMENTO = "null";
            }
            string SQL;
            string vlTaxaVenda = decBD(dadosEdit.VL_TAXA_VENDA.ToString());
            string vlTaxaCompra = decBD(dadosEdit.VL_TAXA_COMPRA.ToString());

            if (dadosEdit.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = 0, VL_TAXA_COMPRA = 0, ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 2, ID_TIPO_COMEX = 1 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = '" + dadosEdit.ID_MOEDA_COMPRA + "', VL_TAXA_COMPRA = '" + vlTaxaCompra + "', ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 2, ID_TIPO_COMEX = 1 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string EditarTaxaAereoExpo(Taxas dadosEdit)
        {
            if (dadosEdit.ID_TAXA_CLIENTE.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TAXA_COMPRA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TAXA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DECLARADO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_DESTINATARIO_COBRANCA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.FL_DIVISAO_PROFIT.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.OB_TAXAS.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ORIGEM_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_ORIGEM_PAGAMENTO = "null";
            }
            if (dadosEdit.ID_TIPO_PAGAMENTO.ToString() == "")
            {
                dadosEdit.ID_TIPO_PAGAMENTO = "null";
            }
            string SQL;
            string vlTaxaVenda = decBD(dadosEdit.VL_TAXA_VENDA.ToString());
            string vlTaxaCompra = decBD(dadosEdit.VL_TAXA_COMPRA.ToString());

            if (dadosEdit.ID_MOEDA_COMPRA.ToString() == "")
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = 0, VL_TAXA_COMPRA = 0, ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 2, ID_TIPO_COMEX = 2 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "UPDATE TB_TAXA_CLIENTE SET ID_ITEM_DESPESA = '" + dadosEdit.ID_ITEM_DESPESA + "', ";
                SQL += "ID_BASE_CALCULO_TAXA = '" + dadosEdit.ID_BASE_CALCULO_TAXA + "', ";
                SQL += "ID_MOEDA_COMPRA = '" + dadosEdit.ID_MOEDA_COMPRA + "', VL_TAXA_COMPRA = '" + vlTaxaCompra + "', ID_MOEDA_VENDA = '" + dadosEdit.ID_MOEDA_VENDA + "', ";
                SQL += "VL_TAXA_VENDA = '" + vlTaxaVenda + "', FL_DECLARADO = '" + dadosEdit.FL_DECLARADO + "',ID_ORIGEM_PAGAMENTO = '" + dadosEdit.ID_ORIGEM_PAGAMENTO + "' , ID_TIPO_PAGAMENTO = '" + dadosEdit.ID_TIPO_PAGAMENTO + "', ";
                SQL += "ID_DESTINATARIO_COBRANCA = '" + dadosEdit.ID_DESTINATARIO_COBRANCA + "', FL_DIVISAO_PROFIT = '" + dadosEdit.FL_DIVISAO_PROFIT + "', ";
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 2, ID_TIPO_COMEX = 2 ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod]
        public string ListarTaxaClienteFCLimpo(int Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA,TB_TAXA_CLIENTE.ID_ITEM_DESPESA, NM_BASE_CALCULO_TAXA, NM_ITEM_DESPESA + ' (' + NM_BASE_CALCULO_TAXA + ')' AS DataField ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "JOIN TB_ITEM_DESPESA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_ITEM_DESPESA = dbo.TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = dbo.TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_ESTUFAGEM = 1 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable taxaClienteFCLexpo = new DataTable();
            taxaClienteFCLexpo = DBS.List(SQL);
            return JsonConvert.SerializeObject(taxaClienteFCLexpo);
        }
        [WebMethod]
        public string ListarTaxaClienteLCLimpo(int Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA,TB_TAXA_CLIENTE.ID_ITEM_DESPESA, NM_BASE_CALCULO_TAXA, NM_ITEM_DESPESA + ' (' + NM_BASE_CALCULO_TAXA + ')' AS DataField ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "JOIN TB_ITEM_DESPESA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_ITEM_DESPESA = dbo.TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = dbo.TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_ESTUFAGEM = 2 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable taxaClienteLCLimpo = new DataTable();
            taxaClienteLCLimpo = DBS.List(SQL);
            return JsonConvert.SerializeObject(taxaClienteLCLimpo);
        }
        [WebMethod]
        public string ListarTaxaClienteFCLexpo(int Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA,TB_TAXA_CLIENTE.ID_ITEM_DESPESA, NM_BASE_CALCULO_TAXA, NM_ITEM_DESPESA + ' (' + NM_BASE_CALCULO_TAXA + ')' AS DataField ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "JOIN TB_ITEM_DESPESA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_ITEM_DESPESA = dbo.TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = dbo.TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 2 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_ESTUFAGEM = 1 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable taxaClienteFCLexpo = new DataTable();
            taxaClienteFCLexpo = DBS.List(SQL);
            return JsonConvert.SerializeObject(taxaClienteFCLexpo);
        }
        [WebMethod]
        public string ListarTaxaClienteLCLexpo(int Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA,TB_TAXA_CLIENTE.ID_ITEM_DESPESA, NM_BASE_CALCULO_TAXA, NM_ITEM_DESPESA + ' (' + NM_BASE_CALCULO_TAXA + ')' AS DataField ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "JOIN TB_ITEM_DESPESA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_ITEM_DESPESA = dbo.TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = dbo.TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 1 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 2 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_ESTUFAGEM = 2 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable taxaClienteLCLexpo = new DataTable();
            taxaClienteLCLexpo = DBS.List(SQL);
            return JsonConvert.SerializeObject(taxaClienteLCLexpo);
        }
        [WebMethod]
        public string ListarTaxaClienteAereoImpo(int Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA,TB_TAXA_CLIENTE.ID_ITEM_DESPESA, NM_BASE_CALCULO_TAXA, NM_ITEM_DESPESA + ' (' + NM_BASE_CALCULO_TAXA + ')' AS DataField ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "JOIN TB_ITEM_DESPESA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_ITEM_DESPESA = dbo.TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = dbo.TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 2 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 1 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable taxaClienteAereoImpo = new DataTable();
            taxaClienteAereoImpo = DBS.List(SQL);
            return JsonConvert.SerializeObject(taxaClienteAereoImpo);
        }
        [WebMethod]
        public string ListarTaxaClienteAereoExpo(int Id)
        {
            string SQL;
            SQL = "SELECT ID_TAXA_CLIENTE, TB_ITEM_DESPESA.NM_ITEM_DESPESA,TB_TAXA_CLIENTE.ID_ITEM_DESPESA, NM_BASE_CALCULO_TAXA, NM_ITEM_DESPESA + ' (' + NM_BASE_CALCULO_TAXA + ')' AS DataField ";
            SQL += "FROM TB_TAXA_CLIENTE ";
            SQL += "JOIN TB_ITEM_DESPESA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_ITEM_DESPESA = dbo.TB_ITEM_DESPESA.ID_ITEM_DESPESA ";
            SQL += "JOIN TB_BASE_CALCULO_TAXA ";
            SQL += "ON dbo.TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA = dbo.TB_BASE_CALCULO_TAXA.ID_BASE_CALCULO_TAXA ";
            SQL += "WHERE TB_TAXA_CLIENTE.ID_PARCEIRO = '" + Id + "' ";
            SQL += "AND TB_TAXA_CLIENTE.ID_VIATRANSPORTE = 2 ";
            SQL += "AND TB_TAXA_CLIENTE.ID_TIPO_COMEX = 2 ";
            SQL += "ORDER BY ID_TAXA_CLIENTE ASC ";

            DataTable taxaClienteAereoExpo = new DataTable();
            taxaClienteAereoExpo= DBS.List(SQL);
            return JsonConvert.SerializeObject(taxaClienteAereoExpo);
        }

        [WebMethod]
        public string ListarContainerInsideWeek(int Id)
        {
            string SQL;
            SQL = "SELECT ID_WEEK_CONTAINER, ID_TIPO_CONTAINER, NR_CONTAINER, VL_PESO_MAX, VL_CUBAGEM FROM TB_WEEK_CNTR WHERE ID_WEEK = '" + Id + "' ";
            DataTable containerInsideWeek= new DataTable();
            containerInsideWeek = DBS.List(SQL);
            return JsonConvert.SerializeObject(containerInsideWeek);
        }
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
        public string ListarContainerWeek(int Id)
        {
            string SQL;
            SQL = "SELECT ID_WEEK_CONTAINER, NM_TIPO_CONTAINER, NR_CONTAINER, VL_PESO_MAX, VL_CUBAGEM ";
            SQL += "FROM TB_WEEK_CNTR ";
            SQL += "JOIN TB_TIPO_CONTAINER ";
            SQL += "ON dbo.TB_WEEK_CNTR.ID_TIPO_CONTAINER = dbo.TB_TIPO_CONTAINER.ID_TIPO_CONTAINER ";
            SQL += "WHERE ID_WEEK = '" + Id + "' ";
            DataTable listarcontainerweek = new DataTable();
            listarcontainerweek = DBS.List(SQL);
            return JsonConvert.SerializeObject(listarcontainerweek);
        }
        [WebMethod]
        public string CadastrarWeek(Week dados)
        {
            if(dados.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if(dados.NM_WEEK.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_PORTO_ORIGEM_LOCAL.ToString() == "")
            {
                return "0";
            }
            if(dados.ID_PORTO_ORIGEM_DESTINO.ToString() == "")
            {
                return "0";
            }
            if (dados.NM_MBL.ToString() == "")
            {
                return "0";
            }
            if(dados.NM_VESSEL.ToString() == "")
            {
                return "0";
            }
            if(dados.DT_CUTOFF.ToString() == "")
            {
                return "0";
            }
            if(dados.DT_ETD.ToString() == "")
            {
                return "0";
            }
            if(dados.NR_FREETIME.ToString() == "")
            {
                return "0";
            }
            if(dados.NR_FRIGHT.ToString() == "")
            {
                return "0";
            }
            string SQL;
            string freight = decBD(dados.NR_FRIGHT.ToString());
            SQL = "insert into TB_WEEK(NM_WEEK, ID_PORTO_ORIGEM_LOCAL, ID_PORTO_ORIGEM_DESTINO, NM_MBL, NM_VESSEL, DT_CUTOFF, DT_ETD, DT_ETA, ";
            SQL += "NR_FREETIME, ID_PARCEIRO, NR_FRIGHT) VALUES('" + dados.NM_WEEK + "', '" + dados.ID_PORTO_ORIGEM_LOCAL +"',  '" + dados.ID_PORTO_ORIGEM_DESTINO +"', ";
            SQL += " '" + dados.NM_MBL +"', '" + dados.NM_VESSEL +"', '" + dados.DT_CUTOFF +"', ";
            SQL += " '" + dados.DT_ETD +"', '" + dados.DT_ETA +"', ";
            SQL += " '" + dados.NR_FREETIME +"','" + dados.ID_PARCEIRO + "', '" + freight + "') ";

            string week = DBS.ExecuteScalar(SQL);
            return "1";
        }
        [WebMethod]
        public string BuscaWeek(int Id)
        {
                string SQL;
                
                SQL = "SELECT ID_WEEK,NM_WEEK,ID_PORTO_ORIGEM_LOCAL,ID_PORTO_ORIGEM_DESTINO,NM_MBL,NM_VESSEL,FORMAT(DT_CUTOFF,'yyyy-MM-dd') as DT_CUTOFF, ";
                SQL += "FORMAT(DT_ETD, 'yyyy-MM-dd') as DT_ETD,FORMAT(DT_ETA, 'yyyy-MM-dd') as DT_ETA, ";
                SQL += "NR_FREETIME,ID_PARCEIRO,NR_FRIGHT ";
                SQL += "FROM TB_WEEK WHERE ID_WEEK = '" + Id + "' ";

                DataTable carregarDados = new DataTable();
                carregarDados = DBS.List(SQL);
                Week resultado = new Week();
                resultado.ID_WEEK = Id;
                resultado.NM_WEEK = carregarDados.Rows[0]["NM_WEEK"].ToString();
                resultado.ID_PORTO_ORIGEM_LOCAL = (int)carregarDados.Rows[0]["ID_PORTO_ORIGEM_LOCAL"];
                resultado.ID_PORTO_ORIGEM_DESTINO = (int)carregarDados.Rows[0]["ID_PORTO_ORIGEM_DESTINO"];
                resultado.NM_MBL = carregarDados.Rows[0]["NM_MBL"].ToString();
                resultado.NM_VESSEL = carregarDados.Rows[0]["NM_VESSEL"].ToString();
                resultado.DT_CUTOFF = carregarDados.Rows[0]["DT_CUTOFF"].ToString();
                resultado.DT_ETD = carregarDados.Rows[0]["DT_ETD"].ToString();
                resultado.DT_ETA = carregarDados.Rows[0]["DT_ETA"].ToString();
                resultado.NR_FRIGHT = carregarDados.Rows[0]["NR_FRIGHT"].ToString();
                resultado.NR_FREETIME = (int)carregarDados.Rows[0]["NR_FREETIME"];
                resultado.ID_PARCEIRO = (int)carregarDados.Rows[0]["ID_PARCEIRO"];

                return JsonConvert.SerializeObject(resultado);

        }
        [WebMethod]
        public string BuscaContainer(int Id)
        {
            string SQL;

            SQL = "SELECT ID_WEEK_CONTAINER, ID_WEEK, ID_TIPO_CONTAINER,NR_CONTAINER,VL_PESO_MAX,VL_CUBAGEM FROM TB_WEEK_CNTR ";
            SQL += "WHERE ID_WEEK_CONTAINER = '" + Id + "' ";

            DataTable carregarDados = new DataTable();
            carregarDados = DBS.List(SQL);
            Week resultado = new Week();
            resultado.ID_WEEK_CONTAINER = Id;
            resultado.ID_WEEK = (int)carregarDados.Rows[0]["ID_WEEK"];
            resultado.ID_TIPO_CONTAINER = (int)carregarDados.Rows[0]["ID_TIPO_CONTAINER"];
            resultado.NR_CONTAINER = carregarDados.Rows[0]["NR_CONTAINER"].ToString();
            resultado.VL_PESO_MAX = carregarDados.Rows[0]["VL_PESO_MAX"].ToString();
            resultado.VL_CUBAGEM = carregarDados.Rows[0]["VL_CUBAGEM"].ToString();

            return JsonConvert.SerializeObject(resultado);

        }
        [WebMethod]
        public string ListarIdWeek()
        {
            string SQL;

            SQL = "SELECT ID_WEEK, NM_WEEK ";
            SQL += "FROM TB_WEEK ";

            DataTable weekList = new DataTable();
            weekList = DBS.List(SQL);
            return JsonConvert.SerializeObject(weekList);
        }
        [WebMethod]
        public string ListarIdContainer(int week)
        {
            string SQL;

            SQL = "SELECT ID_WEEK_CONTAINER, NR_CONTAINER ";
            SQL += "FROM TB_WEEK_CNTR WHERE ID_WEEK = '" + week + "' ";

            DataTable weekList = new DataTable();
            weekList = DBS.List(SQL);
            return JsonConvert.SerializeObject(weekList);
        }
        [WebMethod]
        public string ListarWeekList()
        {
            string SQL;

            SQL = "SELECT A.ID_WEEK, A.NM_WEEK, B.NM_PORTO AS NMPORTOORIGEM, C.NM_PORTO AS NMPORTODESTINO ";
            SQL += "FROM TB_WEEK A ";
            SQL += "LEFT JOIN TB_PORTO B ON A.ID_PORTO_ORIGEM_LOCAL = B.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO C ON A.ID_PORTO_ORIGEM_DESTINO = C.ID_PORTO ";
            SQL += "ORDER BY ID_WEEK";

            DataTable weekList = new DataTable();
            weekList = DBS.List(SQL);
            return JsonConvert.SerializeObject(weekList);
        }
        [WebMethod]
        public string EditarWeek(Week dadosEdit)
        {
            if (dadosEdit.ID_PARCEIRO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.NM_WEEK.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_PORTO_ORIGEM_LOCAL.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_PORTO_ORIGEM_DESTINO.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.NM_MBL.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.NM_VESSEL.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.DT_CUTOFF.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.DT_ETD.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.NR_FREETIME.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.NR_FRIGHT.ToString() == "")
            {
                return "0";
            }
            if(Convert.ToDouble(dadosEdit.NR_FRIGHT) < 0)
            {
                return "0";
            }
            string SQL;
            string freight = decBD(dadosEdit.NR_FRIGHT.ToString());
            SQL = "UPDATE TB_WEEK SET NM_WEEK = '" + dadosEdit.NM_WEEK + "', ";
            SQL += "ID_PORTO_ORIGEM_LOCAL = '" + dadosEdit.ID_PORTO_ORIGEM_LOCAL + "', ";
            SQL += "ID_PORTO_ORIGEM_DESTINO = '" + dadosEdit.ID_PORTO_ORIGEM_DESTINO + "', NM_MBL = '" + dadosEdit.NM_MBL+ "', NM_VESSEL = '" + dadosEdit.NM_VESSEL + "', ";
            SQL += "DT_CUTOFF = '" + dadosEdit.DT_CUTOFF + "', DT_ETD = '" + dadosEdit.DT_ETD+ "', ";
            SQL += "DT_ETA = '" + dadosEdit.DT_ETA+ "', ";
            SQL += "NR_FREETIME = '" + dadosEdit.NR_FREETIME + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', NR_FRIGHT = '" + freight + "' ";
            SQL += "WHERE ID_WEEK = '" + dadosEdit.ID_WEEK + "' ";

            string editTaxa = DBS.ExecuteScalar(SQL);
            return "1";
        }
        [WebMethod]
        public string EditarContainer(Week dadosEdit)
        {
            string SQL;
            string idweek = dadosEdit.ID_WEEK.ToString();
            if ( idweek != "" && dadosEdit.VL_CUBAGEM != "" && dadosEdit.VL_PESO_MAX != "" && dadosEdit.NR_CONTAINER != "")
            {
                double vlPesoMaximo = Convert.ToDouble(dadosEdit.VL_PESO_MAX);
                double vlCub = Convert.ToDouble(dadosEdit.VL_CUBAGEM);
                if (vlPesoMaximo > 0 && vlCub > 0)
                {
                    string vlPesoMax = dadosEdit.VL_PESO_MAX.ToString().Replace(',', '.');
                    string vlCubagem = dadosEdit.VL_CUBAGEM.ToString().Replace(',', '.');
                    SQL = "UPDATE TB_WEEK_CNTR SET ID_WEEK = '" + dadosEdit.ID_WEEK + "', ";
                    SQL += "NR_CONTAINER = '" + dadosEdit.NR_CONTAINER + "', ";
                    SQL += "ID_TIPO_CONTAINER = '" + dadosEdit.ID_TIPO_CONTAINER + "', VL_PESO_MAX = '" + vlPesoMax + "', VL_CUBAGEM = '" + vlCubagem + "' ";
                    SQL += "WHERE ID_WEEK_CONTAINER = '" + dadosEdit.ID_WEEK_CONTAINER + "' ";

                    string editTaxa = DBS.ExecuteScalar(SQL);
                    return "1";
                }
                return "0";
            }
            else
            {
                return "0";
            }
        }
        [WebMethod]
        public string CadastrarContainer(Week dados)
        {
            if (dados.VL_PESO_MAX != "" && dados.VL_CUBAGEM != "" && dados.NR_CONTAINER != "")
            {
                double vlPesoMaximo = Convert.ToDouble(dados.VL_PESO_MAX);
                double vlCub = Convert.ToDouble(dados.VL_CUBAGEM);
                string idTipoContainer = dados.ID_TIPO_CONTAINER.ToString();
                string SQL;
                if (vlPesoMaximo > 0 && vlCub > 0 && dados.VL_CUBAGEM != null && dados.VL_PESO_MAX != null && dados.NR_CONTAINER != null && idTipoContainer != "")
                {
                    string vlPesoMax = dados.VL_PESO_MAX.ToString().Replace(',', '.');
                    string vlCubagem = dados.VL_CUBAGEM.ToString().Replace(',', '.');
                    SQL = "insert into TB_WEEK_CNTR(VL_PESO_MAX, ID_TIPO_CONTAINER, ID_WEEK, VL_CUBAGEM, NR_CONTAINER) ";
                    SQL += "VALUES('" + vlPesoMax + "', '" + dados.ID_TIPO_CONTAINER + "',  '" + dados.ID_WEEK + "', ";
                    SQL += " '" + vlCubagem + "', '" + dados.NR_CONTAINER + "') ";

                    string week = DBS.ExecuteScalar(SQL);
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        [WebMethod]
        public string ListarProcessosBL(int Id,int IdCont)
        {
            string SQL;
            SQL = "SELECT ID_BL, NM_STATUS_BL, format(DT_FLWP_LCL,'yyyy/MM/dd') AS DT_FLWP_LCL, NR_PROCESSO,P1.NM_RAZAO AS VENDEDOR,P2.NM_RAZAO AS AGENTE,P3.NM_RAZAO AS CLIENTE,P4.NM_RAZAO AS EXPORTADOR, ";
            SQL += "VL_PESO_BRUTO, VL_M3, QT_MERCADORIA, VL_PESO_BRUTO_AGENTE, VL_M3_AGENTE, QT_MERCADORIA_AGENTE, NM_MERCADORIA, CD_INCOTERM, ";
            SQL += "format(DT_READY_DATE,'yyyy/MM/dd') AS DT_READY_DATE, format(DT_FORECAST_WH,'yyyy/MM/dd') AS DT_FORECAST_WH, ";
            SQL += "format(DT_ARRIVE_WH,'yyyy/MM/dd') AS DT_ARRIVE_WH, format(DT_DRAFT_CUTOFF,'yyyy/MM/dd') AS DT_DRAFT_CUTOFF, "; 
            SQL += "format(TB_WEEK.DT_CUTOFF,'yyyy/MM/dd') AS DT_CUTOFF, NR_BL FROM TB_BL B ";
            SQL += "LEFT JOIN TB_PARCEIRO P1 ON B.ID_PARCEIRO_VENDEDOR = P1.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON B.ID_PARCEIRO_AGENTE = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P3 ON B.ID_PARCEIRO_CLIENTE = P3.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P4 ON B.ID_PARCEIRO_EXPORTADOR = P4.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_STATUS_BL ON B.ID_STATUS_BL = TB_STATUS_BL.ID_STATUS_BL ";
            SQL += "LEFT JOIN TB_MERCADORIA ON B.ID_MERCADORIA = TB_MERCADORIA.ID_MERCADORIA ";
            SQL += "LEFT JOIN TB_INCOTERM ON B.ID_INCOTERM = TB_INCOTERM.ID_INCOTERM ";
            SQL += "LEFT JOIN TB_WEEK ON B.ID_WEEK = TB_WEEK.ID_WEEK ";
            SQL += "WHERE B.ID_WEEK = '" + Id + "' ";
            SQL += "AND B.ID_WEEK_CONTAINER = '" + IdCont + "' ";

            DataTable processoBl = new DataTable();
            processoBl = DBS.List(SQL);
            return JsonConvert.SerializeObject(processoBl);

        }

        [WebMethod]
        public string PesagemMax(int Id, int IdCont)
        {
            string SQL;
            SQL = "SELECT VL_PESO_MAX, VL_CUBAGEM FROM TB_WEEK_CNTR ";
            SQL += "WHERE ID_WEEK_CONTAINER = '" + IdCont + "' ";
            SQL += "AND ID_WEEK = '" + Id + "' ";

            DataTable pesagem = new DataTable();
            pesagem = DBS.List(SQL);
            return JsonConvert.SerializeObject(pesagem);
        }

        [WebMethod]
        public string ListarSomaProcessosBL(int Id, int IdCont)
        {
            string SQL;
            SQL = "SELECT ISNULL(SUM(VL_PESO_BRUTO),0) as SUM_PESO_BRUTO, ISNULL(SUM(VL_M3),0) AS SUM_VL_M3, ISNULL(SUM(QT_MERCADORIA),0) AS SUM_QT_MERCADORIA ";
            SQL += "FROM TB_BL B ";
            SQL += "JOIN TB_WEEK_CNTR ";
            SQL += "ON B.ID_WEEK_CONTAINER = TB_WEEK_CNTR.ID_WEEK_CONTAINER ";
            SQL += "WHERE B.ID_WEEK = '" + Id + "' ";
            SQL += "AND B.ID_WEEK_CONTAINER = '" + IdCont + "' ";

            DataTable processoSomaBl = new DataTable();
            processoSomaBl = DBS.List(SQL);
            return JsonConvert.SerializeObject(processoSomaBl);
        }

        [WebMethod]
        public string ListarSomaProcessosBLPartner(int Id, int IdCont)
        {
            string SQL;
            SQL = "SELECT ISNULL(SUM(VL_PESO_BRUTO_AGENTE),0) as SUM_PESO_BRUTO_AGENTE, ISNULL(SUM(VL_M3_AGENTE),0) AS SUM_VL_M3_AGENTE, ISNULL(SUM(QT_MERCADORIA_AGENTE),0) AS SUM_QT_MERCADORIA_AGENTE ";
            SQL += "FROM TB_BL B ";
            SQL += "JOIN TB_WEEK_CNTR ";
            SQL += "ON B.ID_WEEK_CONTAINER = TB_WEEK_CNTR.ID_WEEK_CONTAINER ";
            SQL += "WHERE B.ID_WEEK = '" + Id + "' ";
            SQL += "AND B.ID_WEEK_CONTAINER = '" + IdCont + "' ";

            DataTable processoSomaBl = new DataTable();
            processoSomaBl = DBS.List(SQL);
            return JsonConvert.SerializeObject(processoSomaBl);
        }

        [WebMethod]
        public string BuscarInfoProcessoPartner(int Id)
        {
            string SQL;
            SQL = "SELECT ID_BL, ID_WEEK_CONTAINER, NR_PROCESSO, VL_PESO_BRUTO_AGENTE, VL_M3_AGENTE, QT_MERCADORIA_AGENTE, B.ID_MERCADORIA, B.ID_INCOTERM, ";
            SQL += "FORMAT(DT_READY_DATE,'yyyy-MM-dd') AS DT_READY_DATE, FORMAT(DT_FORECAST_WH,'yyyy-MM-dd') AS DT_FORECAST_WH, ";
            SQL += "FORMAT(DT_ARRIVE_WH,'yyyy-MM-dd') AS DT_ARRIVE_WH, FORMAT(DT_DRAFT_CUTOFF,'yyyy-MM-dd') AS DT_DRAFT_CUTOFF, ";
            SQL += "NR_BL FROM TB_BL B ";
            SQL += "JOIN TB_WEEK ON B.ID_WEEK = TB_WEEK.ID_WEEK ";
            SQL += "JOIN TB_INCOTERM ON B.ID_INCOTERM = TB_INCOTERM.ID_INCOTERM ";
            SQL += "JOIN TB_MERCADORIA ON B.ID_MERCADORIA = TB_MERCADORIA.ID_MERCADORIA ";
            SQL += "WHERE B.ID_BL = '" + Id + "' ";

            DataTable carregarDados = new DataTable();
            carregarDados = DBS.List(SQL);
            Processos resultado = new Processos();
            resultado.ID_WEEK_CONTAINER = (int)carregarDados.Rows[0]["ID_WEEK_CONTAINER"];
            resultado.ID_BL = (int)carregarDados.Rows[0]["ID_BL"];
            resultado.NR_PROCESSO = carregarDados.Rows[0]["NR_PROCESSO"].ToString();
            resultado.VL_PESO_BRUTO_AGENTE = carregarDados.Rows[0]["VL_PESO_BRUTO_AGENTE"].ToString();
            resultado.VL_M3_AGENTE = carregarDados.Rows[0]["VL_M3_AGENTE"].ToString();
            resultado.QT_MERCADORIA_AGENTE = (int)carregarDados.Rows[0]["QT_MERCADORIA_AGENTE"];
            resultado.ID_MERCADORIA = (int)carregarDados.Rows[0]["ID_MERCADORIA"];
            resultado.ID_INCOTERM = (int)carregarDados.Rows[0]["ID_INCOTERM"];
            resultado.DT_READY_DATE = carregarDados.Rows[0]["DT_READY_DATE"].ToString();
            resultado.DT_FORECAST_WH = carregarDados.Rows[0]["DT_FORECAST_WH"].ToString();
            resultado.DT_ARRIVE_WH = carregarDados.Rows[0]["DT_ARRIVE_WH"].ToString();
            resultado.DT_DRAFT_CUTOFF = carregarDados.Rows[0]["DT_DRAFT_CUTOFF"].ToString();
            resultado.NR_BL = carregarDados.Rows[0]["NR_BL"].ToString();



            return JsonConvert.SerializeObject(resultado);
        }

        [WebMethod]
        public string BuscarInfoProcessoFCA(int Id)
        {
            string SQL;
            SQL = "SELECT ID_BL, ID_WEEK_CONTAINER, ID_STATUS_BL FROM TB_BL B ";
            SQL += "WHERE B.ID_BL = '" + Id + "' ";

            DataTable carregarDados = new DataTable();
            carregarDados = DBS.List(SQL);
            Processos resultado = new Processos();
            resultado.ID_WEEK_CONTAINER = (int)carregarDados.Rows[0]["ID_WEEK_CONTAINER"];
            resultado.ID_BL = (int)carregarDados.Rows[0]["ID_BL"];
            resultado.ID_STATUS_BL = (int)carregarDados.Rows[0]["ID_STATUS_BL"];



            return JsonConvert.SerializeObject(resultado);
        }

        [WebMethod]
        public string EditarProcessoPartner(Processos dadosEdit)
        {
            string SQL;
            double vlPesoBruto = Convert.ToDouble(dadosEdit.VL_PESO_BRUTO_AGENTE);
            double vlM3ag = Convert.ToDouble(dadosEdit.VL_M3_AGENTE);
            if (vlPesoBruto >= 0 && vlM3ag >= 0)
            {
                string vlPesoBrutoAgente = decBD(vlPesoBruto.ToString());
                string vlM3Agente = decBD(vlM3ag.ToString());
                SQL = "UPDATE TB_BL SET VL_PESO_BRUTO_AGENTE = '" + vlPesoBrutoAgente + "', ";
                SQL += "VL_M3_AGENTE = '" + vlM3Agente + "', ";
                SQL += "QT_MERCADORIA_AGENTE = '" + dadosEdit.QT_MERCADORIA_AGENTE + "', ID_MERCADORIA = '" + dadosEdit.ID_MERCADORIA + "', ";
                SQL += "ID_INCOTERM = '" + dadosEdit.ID_INCOTERM + "',DT_READY_DATE = '" + dadosEdit.DT_READY_DATE + "',DT_FORECAST_WH = '" + dadosEdit.DT_FORECAST_WH + "', ";
                SQL += "DT_ARRIVE_WH = '" + dadosEdit.DT_ARRIVE_WH + "',DT_DRAFT_CUTOFF = '" + dadosEdit.DT_DRAFT_CUTOFF + "',";
                SQL += "NR_BL = '" + dadosEdit.NR_BL + "' ";
                SQL += "WHERE ID_BL = '" + dadosEdit.ID_BL + "' ";

                string editProcessos = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                return "0";
            }
        }

        [WebMethod]
        public void EditarProcessoFCA(Processos dadosEdit)
        {
            string SQL;
            SQL = "UPDATE TB_BL SET ID_STATUS_BL = '" + dadosEdit.ID_STATUS_BL + "' ";
            SQL += "WHERE ID_BL = '" + dadosEdit.ID_BL + "' ";

            string editProcessos = DBS.ExecuteScalar(SQL);
        }

        [WebMethod]
        public void DeletarTaxa(int Id, int IdTaxa)
        {
            string SQL;
            SQL = "DELETE FROM TB_TAXA_CLIENTE WHERE ID_PARCEIRO = '" + Id + "' AND ID_TAXA_CLIENTE = '" + IdTaxa + "' ";

            string deleteTaxa = DBS.ExecuteScalar(SQL);
            
        }
    }
}
