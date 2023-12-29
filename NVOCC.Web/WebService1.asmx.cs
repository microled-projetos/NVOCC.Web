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

        [WebMethod(EnableSession = true)]
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
                SQL += "ORDER BY TB_ITEM_DESPESA.NM_ITEM_DESPESA ASC ";

                DataTable fclimpo = new DataTable();
                
                fclimpo = DBS.List(SQL);
                
                return JsonConvert.SerializeObject(fclimpo);
        }
        [WebMethod(EnableSession = true)]
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
            SQL += "ORDER BY TB_ITEM_DESPESA.NM_ITEM_DESPESA ASC ";

            DataTable lclimpo = new DataTable();
            lclimpo = DBS.List(SQL);
            return JsonConvert.SerializeObject(lclimpo);
        }
        [WebMethod(EnableSession = true)]
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
            SQL += "ORDER BY TB_ITEM_DESPESA.NM_ITEM_DESPESA ASC ";

            DataTable fclexpo = new DataTable();

            fclexpo = DBS.List(SQL);

            return JsonConvert.SerializeObject(fclexpo);
        }
        [WebMethod(EnableSession = true)]
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
            SQL += "ORDER BY TB_ITEM_DESPESA.NM_ITEM_DESPESA ASC ";

            DataTable lclexpo = new DataTable();

            lclexpo = DBS.List(SQL);

            return JsonConvert.SerializeObject(lclexpo);
        }
        [WebMethod(EnableSession = true)]
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
            SQL += "ORDER BY TB_ITEM_DESPESA.NM_ITEM_DESPESA ASC ";

            DataTable aereoImpo = new DataTable();

            aereoImpo = DBS.List(SQL);

            return JsonConvert.SerializeObject(aereoImpo);
        }
        [WebMethod(EnableSession = true)]
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
            SQL += "ORDER BY TB_ITEM_DESPESA.NM_ITEM_DESPESA ASC ";

            DataTable aereoExpo = new DataTable();

            aereoExpo = DBS.List(SQL);

            return JsonConvert.SerializeObject(aereoExpo);
        }

        [WebMethod(EnableSession = true)]
        public string BuscaCliente(int Id)
        {
            if (Id != 0)
            {
                string SQL;
                SQL = "SELECT TB_TAXA_CLIENTE.OB_TAXAS, TB_ITEM_DESPESA.ID_ITEM_DESPESA,TB_TAXA_CLIENTE.ID_BASE_CALCULO_TAXA,VL_TAXA_COMPRA, ";
                SQL += "REPLICATE('0', 3 - LEN(ID_MOEDA_COMPRA)) + RTRIM(ID_MOEDA_COMPRA) AS MOEDA_COMPRA, VL_TAXA_VENDA, REPLICATE('0', 3 - LEN(ID_MOEDA_VENDA)) + RTRIM(ID_MOEDA_VENDA) AS MOEDA_VENDA, TB_TAXA_CLIENTE.FL_DECLARADO, TB_TAXA_CLIENTE.FL_DIVISAO_PROFIT, ";
                SQL += "TB_TAXA_CLIENTE.ID_DESTINATARIO_COBRANCA, TB_TAXA_CLIENTE.ID_TIPO_PAGAMENTO, TB_TAXA_CLIENTE.ID_ORIGEM_PAGAMENTO, ";
                SQL += "TB_TAXA_CLIENTE.ID_ITEM_DESPESA, FL_TAXA_TRANSPORTADOR, isnull(TB_TAXA_CLIENTE.VL_TARIFA_MINIMA,0) as VL_TARIFA_MINIMA, isnull(TB_TAXA_CLIENTE.VL_TARIFA_MINIMA_COMPRA,0) as VL_TARIFA_MINIMA_COMPRA ";
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
                resultado.VL_TARIFA_MINIMA = carregarDados.Rows[0]["VL_TARIFA_MINIMA"].ToString();
                resultado.VL_TARIFA_MINIMA_COMPRA = carregarDados.Rows[0]["VL_TARIFA_MINIMA_COMPRA"].ToString();
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
                if (carregarDados.Rows[0]["FL_TAXA_TRANSPORTADOR"].ToString() == "true")
                {
                    resultado.FL_TAXA_TRANSPORTADOR = 1;
                }
                else
                {
                    resultado.FL_TAXA_TRANSPORTADOR = 0;
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
        
        [WebMethod(EnableSession = true)]
        public string CadastrarTaxaFCLimpo(Taxas dados)
        {
            string tarifaV;
            string tarifaC;

            if(dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if(dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if ((dados.ID_BASE_CALCULO_TAXA == 6 || dados.ID_BASE_CALCULO_TAXA == 7 || dados.ID_BASE_CALCULO_TAXA == 13 || dados.ID_BASE_CALCULO_TAXA == 14 || dados.ID_BASE_CALCULO_TAXA == 37) && dados.VL_TARIFA_MINIMA.ToString() == "")
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
            if(dados.VL_TARIFA_MINIMA.ToString() == "")
			{
                tarifaV = "0";
			}
			else
			{
                tarifaV = dados.VL_TARIFA_MINIMA.ToString();
			}
            if (dados.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dados.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM, ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,1,1,'"+dados.ID_ORIGEM_PAGAMENTO+"','"+ dados.ID_TIPO_PAGAMENTO + "','"+dados.FL_TAXA_TRANSPORTADOR+"', '"+tarifaV.ToString().Replace(',', '.') + "','" + tarifaC.ToString().Replace(',', '.') + "') ";
                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,1,1,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', '" + tarifaV.ToString().Replace(',', '.') + "','" + tarifaC.ToString().Replace(',', '.') + "') ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string CadastrarTaxaLCLimpo(Taxas dados)
        {
            string tarifaV;
            string tarifaC;
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if ((dados.ID_BASE_CALCULO_TAXA == 6 || dados.ID_BASE_CALCULO_TAXA == 7 || dados.ID_BASE_CALCULO_TAXA == 13 || dados.ID_BASE_CALCULO_TAXA == 14 || dados.ID_BASE_CALCULO_TAXA == 37) && dados.VL_TARIFA_MINIMA.ToString() == "")
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
            if (dados.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dados.VL_TARIFA_MINIMA.ToString();
            }
            if (dados.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dados.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,1,2,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV.ToString().Replace(',', '.') + "," + tarifaC.ToString().Replace(',', '.') + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,1,2,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV + "," + tarifaC + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string CadastrarTaxaFCLexpo(Taxas dados)
        {
            string tarifaC;
            string tarifaV;
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if ((dados.ID_BASE_CALCULO_TAXA == 6 || dados.ID_BASE_CALCULO_TAXA == 7 || dados.ID_BASE_CALCULO_TAXA == 13 || dados.ID_BASE_CALCULO_TAXA == 14 || dados.ID_BASE_CALCULO_TAXA == 37) && dados.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dados.VL_TARIFA_MINIMA.ToString();
            }
            if (dados.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dados.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,2,1,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV + "," + tarifaC + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,2,1,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV + "," + tarifaC + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string CadastrarTaxaLCLexpo(Taxas dados)
        {
            string tarifaC;
            string tarifaV;
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if ((dados.ID_BASE_CALCULO_TAXA == 6 || dados.ID_BASE_CALCULO_TAXA == 7 || dados.ID_BASE_CALCULO_TAXA == 13 || dados.ID_BASE_CALCULO_TAXA == 14 || dados.ID_BASE_CALCULO_TAXA == 37) && dados.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dados.VL_TARIFA_MINIMA.ToString();
            }
            if (dados.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dados.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,2,2,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV + "," + tarifaC + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',1,2,2,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV + "," + tarifaC + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string CadastrarTaxaAereoImpo(Taxas dados)
        {
            string tarifaV;
            string tarifaC;
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if ((dados.ID_BASE_CALCULO_TAXA == 6 || dados.ID_BASE_CALCULO_TAXA == 7 || dados.ID_BASE_CALCULO_TAXA == 13 || dados.ID_BASE_CALCULO_TAXA == 14 || dados.ID_BASE_CALCULO_TAXA == 37) && dados.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dados.VL_TARIFA_MINIMA.ToString();
            }
            if (dados.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dados.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',2,1,null,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV + "," + tarifaC + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',2,1,null,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV + "," + tarifaC + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string CadastrarTaxaAereoExpo(Taxas dados)
        {
            string tarifaC;
            string tarifaV;
            if (dados.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if (dados.ID_BASE_CALCULO_TAXA.ToString() == "")
            {
                return "0";
            }
            if ((dados.ID_BASE_CALCULO_TAXA == 6 || dados.ID_BASE_CALCULO_TAXA == 7 || dados.ID_BASE_CALCULO_TAXA == 13 || dados.ID_BASE_CALCULO_TAXA == 14 || dados.ID_BASE_CALCULO_TAXA == 37) && dados.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dados.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dados.VL_TARIFA_MINIMA.ToString();
            }
            if (dados.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
			}
			else
			{
                tarifaC = dados.VL_TARIFA_MINIMA_COMPRA.ToString();

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
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "',0,0,'" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',2,2,null,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV + "," + tarifaC + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
            else
            {
                SQL = "INSERT INTO TB_TAXA_CLIENTE(ID_ITEM_DESPESA, ID_BASE_CALCULO_TAXA, ";
                SQL += "ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA, FL_DECLARADO, ID_DESTINATARIO_COBRANCA, ";
                SQL += "FL_DIVISAO_PROFIT, OB_TAXAS, ID_PARCEIRO,ID_VIATRANSPORTE, ID_TIPO_COMEX, ID_TIPO_ESTUFAGEM,ID_ORIGEM_PAGAMENTO, ID_TIPO_PAGAMENTO, FL_TAXA_TRANSPORTADOR, VL_TARIFA_MINIMA, VL_TARIFA_MINIMA_COMPRA) ";
                SQL += "VALUES('" + dados.ID_ITEM_DESPESA + "',";
                SQL += "'" + dados.ID_BASE_CALCULO_TAXA + "','" + dados.ID_MOEDA_COMPRA + "','" + vlTaxaCompra + "','" + dados.ID_MOEDA_VENDA + "', ";
                SQL += "'" + vlTaxaVenda + "','" + dados.FL_DECLARADO + "','" + dados.ID_DESTINATARIO_COBRANCA + "', ";
                SQL += "'" + dados.FL_DIVISAO_PROFIT + "',";
                SQL += "'" + dados.OB_TAXAS + "','" + dados.ID_PARCEIRO + "',2,2,null,'" + dados.ID_ORIGEM_PAGAMENTO + "','" + dados.ID_TIPO_PAGAMENTO + "','" + dados.FL_TAXA_TRANSPORTADOR + "', " + tarifaV + "," + tarifaC + ") ";

                string taxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string EditarTaxaFCLimpo(Taxas dadosEdit)
        {
            string tarifaV;
            string tarifaC;
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
            if ((dadosEdit.ID_BASE_CALCULO_TAXA == 6 || dadosEdit.ID_BASE_CALCULO_TAXA == 7 || dadosEdit.ID_BASE_CALCULO_TAXA == 13 || dadosEdit.ID_BASE_CALCULO_TAXA == 14 || dadosEdit.ID_BASE_CALCULO_TAXA == 37) && dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dadosEdit.VL_TARIFA_MINIMA.ToString();
            }
            if (dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
			}
			else
			{
                tarifaC = dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 1, ID_TIPO_ESTUFAGEM = 1, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "', VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = "+tarifaC+" ";
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 1, ID_TIPO_ESTUFAGEM = 1, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "', VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + "  ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string EditarTaxaLCLimpo(Taxas dadosEdit)
        {
            string tarifaC;
            string tarifaV;
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
            if ((dadosEdit.ID_BASE_CALCULO_TAXA == 6 || dadosEdit.ID_BASE_CALCULO_TAXA == 7 || dadosEdit.ID_BASE_CALCULO_TAXA == 13 || dadosEdit.ID_BASE_CALCULO_TAXA == 14 || dadosEdit.ID_BASE_CALCULO_TAXA == 37) && dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dadosEdit.VL_TARIFA_MINIMA.ToString();
            }
            if (dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 1, ID_TIPO_ESTUFAGEM = 2, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "',VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + "  ";
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 1, ID_TIPO_ESTUFAGEM = 2, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "', VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + " ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string EditarTaxaFCLexpo(Taxas dadosEdit)
        {
            string tarifaC;
            string tarifaV;
            if (dadosEdit.ID_TAXA_CLIENTE.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if ((dadosEdit.ID_BASE_CALCULO_TAXA == 6 || dadosEdit.ID_BASE_CALCULO_TAXA == 7 || dadosEdit.ID_BASE_CALCULO_TAXA == 13 || dadosEdit.ID_BASE_CALCULO_TAXA == 14 || dadosEdit.ID_BASE_CALCULO_TAXA == 37) && dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dadosEdit.VL_TARIFA_MINIMA.ToString();
            }
            if (dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 2, ID_TIPO_ESTUFAGEM = 1, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "', VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + " ";
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 2, ID_TIPO_ESTUFAGEM = 1, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "', VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + " ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string EditarTaxaLCLexpo(Taxas dadosEdit)
        {
            string tarifaC;
            string tarifaV;
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
            if ((dadosEdit.ID_BASE_CALCULO_TAXA == 6 || dadosEdit.ID_BASE_CALCULO_TAXA == 7 || dadosEdit.ID_BASE_CALCULO_TAXA == 13 || dadosEdit.ID_BASE_CALCULO_TAXA == 14 || dadosEdit.ID_BASE_CALCULO_TAXA == 37) && dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_MOEDA_VENDA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dadosEdit.VL_TARIFA_MINIMA.ToString();
            }
            if (dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 2, ID_TIPO_ESTUFAGEM = 2, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "', VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + " ";
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 1, ID_TIPO_COMEX = 2, ID_TIPO_ESTUFAGEM = 2, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "',VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + " ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string EditarTaxaAereoImpo(Taxas dadosEdit)
        {
            string tarifaC;
            string tarifaV;
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
            if ((dadosEdit.ID_BASE_CALCULO_TAXA == 6 || dadosEdit.ID_BASE_CALCULO_TAXA == 7 || dadosEdit.ID_BASE_CALCULO_TAXA == 13 || dadosEdit.ID_BASE_CALCULO_TAXA == 14 || dadosEdit.ID_BASE_CALCULO_TAXA == 37) && dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dadosEdit.VL_TARIFA_MINIMA.ToString();
            }
            if (dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 2, ID_TIPO_COMEX = 1, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "',VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + "  ";
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 2, ID_TIPO_COMEX = 1, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "', VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + " ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
        public string EditarTaxaAereoExpo(Taxas dadosEdit)
        {
            string tarifaV;
            string tarifaC;
            if (dadosEdit.ID_TAXA_CLIENTE.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.ID_ITEM_DESPESA.ToString() == "")
            {
                return "0";
            }
            if ((dadosEdit.ID_BASE_CALCULO_TAXA == 6 || dadosEdit.ID_BASE_CALCULO_TAXA == 7 || dadosEdit.ID_BASE_CALCULO_TAXA == 13 || dadosEdit.ID_BASE_CALCULO_TAXA == 14 || dadosEdit.ID_BASE_CALCULO_TAXA == 37) && dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                return "0";
            }
            if (dadosEdit.VL_TARIFA_MINIMA.ToString() == "")
            {
                tarifaV = "0";
            }
            else
            {
                tarifaV = dadosEdit.VL_TARIFA_MINIMA.ToString();
            }
            if (dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString() == "")
            {
                tarifaC = "0";
            }
            else
            {
                tarifaC = dadosEdit.VL_TARIFA_MINIMA_COMPRA.ToString();
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 2, ID_TIPO_COMEX = 2, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "', VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + " ";
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
                SQL += "OB_TAXAS = '" + dadosEdit.OB_TAXAS + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', ID_VIATRANSPORTE = 2, ID_TIPO_COMEX = 2, FL_TAXA_TRANSPORTADOR = '" + dadosEdit.FL_TAXA_TRANSPORTADOR + "', VL_TARIFA_MINIMA = '" + tarifaV + "', VL_TARIFA_MINIMA_COMPRA = " + tarifaC + " ";
                SQL += "WHERE ID_TAXA_CLIENTE = '" + dadosEdit.ID_TAXA_CLIENTE + "' ";

                string editTaxa = DBS.ExecuteScalar(SQL);
                return "1";
            }
        }

        [WebMethod(EnableSession = true)]
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
        [WebMethod(EnableSession = true)]
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
        [WebMethod(EnableSession = true)]
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
        [WebMethod(EnableSession = true)]
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
        [WebMethod(EnableSession = true)]
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
        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        public string ListarContainerInsideWeek(int Id)
        {
            string SQL;
            SQL = "SELECT ID_WEEK_CONTAINER, ID_TIPO_CONTAINER, NR_CONTAINER, VL_PESO_MAX, VL_CUBAGEM FROM TB_WEEK_CNTR WHERE ID_WEEK = '" + Id + "' ";
            DataTable containerInsideWeek= new DataTable();
            containerInsideWeek = DBS.List(SQL);
            return JsonConvert.SerializeObject(containerInsideWeek);
        }
        [WebMethod(EnableSession = true)]
        public string ListarWeek()
        {
            string SQL;
            SQL = "SELECT A.ID_WEEK, A.NM_WEEK, B.NM_PORTO AS NMPORTOORIGEM, C.NM_PORTO AS NMPORTODESTINO, ";
            SQL += "isnull(format(A.DT_ETD,'yyyy/MM/dd'),'') as DT_ETD, isnull(format(A.DT_CUTOFF,'yyyy/MM/dd'),'') as DT_CUTOFF ";
            SQL += "FROM TB_WEEK A ";
            SQL += "LEFT JOIN TB_PORTO B ON A.ID_PORTO_ORIGEM_LOCAL = B.ID_PORTO ";
            SQL += "LEFT JOIN TB_PORTO C ON A.ID_PORTO_ORIGEM_DESTINO = C.ID_PORTO ";


            DataTable listarweek = new DataTable();
            listarweek = DBS.List(SQL);
            return JsonConvert.SerializeObject(listarweek);
        }
        [WebMethod(EnableSession = true)]
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
        [WebMethod(EnableSession = true)]
        public string CadastrarWeek(Week dados)
        {
            string SQL;
            
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
                dados.NM_MBL = "";
            }
            if(dados.NM_VESSEL.ToString() == "")
            {
                dados.NM_VESSEL = "";
            }
            if(dados.DT_CUTOFF.ToString() == "")
            {
                dados.DT_CUTOFF = "NULL";
            }
            else
            {
                dados.DT_CUTOFF = "'" + dados.DT_CUTOFF + "'";
            }
            if(dados.DT_ETD.ToString() == "")
            {
                dados.DT_ETD = "NULL";
            }
            else
            {
                dados.DT_ETD = "'" + dados.DT_ETD + "'";
            }
            if (dados.DT_ETA.ToString() == "")
            {
                dados.DT_ETA = "NULL";
            }
            else
            {
                dados.DT_ETA = "'"+dados.DT_ETA+"'";
            }
            if (dados.NR_FREETIME.ToString() == "")
            {
                dados.NR_FREETIME = "";
            }
            if(dados.NR_FRIGHT.ToString() == "")
            {
                dados.NR_FRIGHT = "";
            }
            
            string freight = decBD(dados.NR_FRIGHT.ToString());
            SQL = "insert into TB_WEEK(NM_WEEK, ID_PORTO_ORIGEM_LOCAL, ID_PORTO_ORIGEM_DESTINO, NM_MBL, NM_VESSEL, DT_CUTOFF, DT_ETD, DT_ETA, ";
            SQL += "NR_FREETIME, ID_PARCEIRO, NR_FRIGHT) VALUES('" + dados.NM_WEEK + "', '" + dados.ID_PORTO_ORIGEM_LOCAL +"',  '" + dados.ID_PORTO_ORIGEM_DESTINO +"', ";
            SQL += " '" + dados.NM_MBL +"', '" + dados.NM_VESSEL +"', " + dados.DT_CUTOFF +", ";
            SQL += " " + dados.DT_ETD +", " + dados.DT_ETA +", ";
            SQL += " '" + dados.NR_FREETIME +"','" + dados.ID_PARCEIRO + "', '" + freight + "') ";

            string week = DBS.ExecuteScalar(SQL);
            return "1";
        }

        [WebMethod(EnableSession = true)]
        public string CadastrarContainer(string nrCont, string tipo)
        {
            if (tipo.ToString() == "" || tipo.ToString() == "0")
            {
                return "0";
            }

            string SQL;
            SQL = "INSERT INTO TB_CNTR_BL (NR_CNTR, ID_BL_MASTER, ID_TIPO_CNTR) VALUES ('" + nrCont + "',0, '" + tipo + "' )";
            string cadastrarContainer = DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("ok");
        }

        [WebMethod(EnableSession = true)]
        public string BuscaWeek(string Id)
        {
                string SQL;
                
                SQL = "SELECT ID_WEEK,NM_WEEK,ID_PORTO_ORIGEM_LOCAL,ID_PORTO_ORIGEM_DESTINO,NM_MBL,NM_VESSEL,FORMAT(DT_CUTOFF,'yyyy-MM-dd') as DT_CUTOFF, ";
                SQL += "ISNULL(FORMAT(DT_ETD, 'yyyy-MM-dd'),'') as DT_ETD, ISNULL(FORMAT(DT_ETA, 'yyyy-MM-dd'),'') as DT_ETA, ";
                SQL += "ISNULL(CONVERT(VARCHAR,NR_FREETIME),'') AS NR_FREETIME, ID_PARCEIRO, ISNULL(CONVERT(VARCHAR,NR_FRIGHT),'') AS NR_FRIGHT ";
                SQL += "FROM TB_WEEK WHERE ID_WEEK = '" + Id + "' ";

                DataTable carregarDados = new DataTable();
                carregarDados = DBS.List(SQL);
                Week resultado = new Week();
                resultado.ID_WEEK = Id;
                resultado.NM_WEEK = carregarDados.Rows[0]["NM_WEEK"].ToString();
                resultado.ID_PORTO_ORIGEM_LOCAL = carregarDados.Rows[0]["ID_PORTO_ORIGEM_LOCAL"].ToString();
                resultado.ID_PORTO_ORIGEM_DESTINO = carregarDados.Rows[0]["ID_PORTO_ORIGEM_DESTINO"].ToString();
                resultado.NM_MBL = carregarDados.Rows[0]["NM_MBL"].ToString();
                resultado.NM_VESSEL = carregarDados.Rows[0]["NM_VESSEL"].ToString();
                resultado.DT_CUTOFF = carregarDados.Rows[0]["DT_CUTOFF"].ToString();
                resultado.DT_ETD = carregarDados.Rows[0]["DT_ETD"].ToString();
                resultado.DT_ETA = carregarDados.Rows[0]["DT_ETA"].ToString();
                resultado.NR_FRIGHT = carregarDados.Rows[0]["NR_FRIGHT"].ToString();
                resultado.NR_FREETIME = carregarDados.Rows[0]["NR_FREETIME"].ToString();
                resultado.ID_PARCEIRO = carregarDados.Rows[0]["ID_PARCEIRO"].ToString();

                return JsonConvert.SerializeObject(resultado);

        }
        /*[WebMethod(EnableSession = true)]
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

        }*/
        [WebMethod(EnableSession = true)]
        public string ListarIdWeek()
        {
            string SQL;

            SQL = "SELECT ID_WEEK, NM_WEEK ";
            SQL += "FROM TB_WEEK ";

            DataTable weekList = new DataTable();
            weekList = DBS.List(SQL);
            return JsonConvert.SerializeObject(weekList);
        }
        [WebMethod(EnableSession = true)]
        public string ListarIdContainer(int week)
        {
            string SQL;

            SQL = "SELECT ID_WEEK_CONTAINER, NR_CONTAINER ";
            SQL += "FROM TB_WEEK_CNTR WHERE ID_WEEK = '" + week + "' ";

            DataTable weekList = new DataTable();
            weekList = DBS.List(SQL);
            return JsonConvert.SerializeObject(weekList);
        }
        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
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
                dadosEdit.NM_MBL = "";
            }
            if (dadosEdit.NM_VESSEL.ToString() == "")
            {
                dadosEdit.NM_VESSEL = "";
            }
            if (dadosEdit.DT_CUTOFF.ToString() == "")
            {
                dadosEdit.DT_CUTOFF = "NULL";
            }
            else
            {
                dadosEdit.DT_CUTOFF = "'" + dadosEdit.DT_CUTOFF + "' ";
            }
            if (dadosEdit.DT_ETD.ToString() == "")
            {
                dadosEdit.DT_ETD = "NULL";
            }
            else
            {
                dadosEdit.DT_ETD = "'" + dadosEdit.DT_ETD + "' ";
            }
            if (dadosEdit.DT_ETA.ToString() == "")
            {
                dadosEdit.DT_ETA = "NULL";
            }
            else
            {
                dadosEdit.DT_ETA = "'" + dadosEdit.DT_ETA + "' ";
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
            SQL += "DT_CUTOFF = " + dadosEdit.DT_CUTOFF + ", DT_ETD = " + dadosEdit.DT_ETD+ ", ";
            SQL += "DT_ETA = " + dadosEdit.DT_ETA+ ", ";
            SQL += "NR_FREETIME = '" + dadosEdit.NR_FREETIME + "', ID_PARCEIRO = '" + dadosEdit.ID_PARCEIRO + "', NR_FRIGHT = '" + freight + "' ";
            SQL += "WHERE ID_WEEK = '" + dadosEdit.ID_WEEK + "' ";

            string editTaxa = DBS.ExecuteScalar(SQL);
            return "1";
        }
        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        public string CadastrarContainerWeek(string nrContainer, string tipo)
        {
            if(tipo.ToString() == "" || tipo.ToString() == "0")
            {
                return "0";
            }

            string SQL;
            SQL = "INSERT INTO TB_CNTR_BL (NR_CNTR, ID_BL_MASTER, ID_TIPO_CNTR) VALUES ('"+nrContainer+"',0, '"+tipo+"' )";
            string cadastrarContainer = DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("ok");
        }

        [WebMethod(EnableSession = true)]
        public string listarContainer(string week)
        {

            string SQL;
            SQL = "SELECT DISTINCT(A.ID_CNTR_BL), A.NR_CNTR, B.NM_TIPO_CONTAINER, B.VL_PESO_MAX, B.VL_VOLUME_M3 FROM TB_CNTR_BL A ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER B ON A.ID_TIPO_CNTR = B.ID_TIPO_CONTAINER ";
            SQL += "LEFT JOIN TB_AMR_CNTR_BL C ON A.ID_CNTR_BL = C.ID_CNTR_BL ";
            SQL += "LEFT JOIN TB_BL D ON C.ID_BL = D.ID_BL ";
            SQL += "LEFT JOIN TB_WEEK E ON D.ID_WEEK = E.ID_WEEK ";
            SQL += "WHERE A.ID_BL_MASTER = 0 ";
            SQL += "AND(C.ID_BL IS NULL OR D.ID_WEEK = '"+week+"') ";
            DataTable container = new DataTable();
            container = DBS.List(SQL);
            return JsonConvert.SerializeObject(container);
        }

        [WebMethod(EnableSession = true)]
        public string ListarWeekInside(string week)
        {
            string SQL;
            SQL = "SELECT B.ID_BL, ";
            SQL += "ISNULL(NM_STATUS_BL,'') AS NM_STATUS_BL, ";
            SQL += "ISNULL(format(DT_FLWP_LCL,'yyyy/MM/dd'),'') AS DT_FLWP_LCL, ";
            SQL += "ISNULL(CONVERT(VARCHAR,NR_PROCESSO),'') AS NR_PROCESSO, ";
            SQL += "ISNULL(P1.NM_RAZAO,'') AS VENDEDOR, ";
            SQL += "ISNULL(P2.NM_RAZAO,'') AS AGENTE, ";
            SQL += "ISNULL(P3.NM_RAZAO,'') AS IMPORTADOR, ";
            SQL += "ISNULL(P4.NM_RAZAO,'') AS EXPORTADOR, ";
            SQL += "ISNULL(B.VL_PESO_BRUTO,0) AS VL_PESO_BRUTO, ";
            SQL += "ISNULL(B.VL_M3,0) AS VL_M3, ";
            SQL += "ISNULL(B.QT_MERCADORIA,0) AS QT_MERCADORIA, ";
            SQL += "ISNULL(B.OB_REFERENCIA_AUXILIAR,'') AS REF_AUX, ";
            SQL += "ISNULL(B.OB_REFERENCIA_COMERCIAL,'') AS REF_COM, ";
            SQL += "ISNULL(VL_PESO_BRUTO_AGENTE,0) AS VL_PESO_BRUTO_AGENTE, ";
            SQL += "ISNULL(VL_M3_AGENTE,0) AS VL_M3_AGENTE, ";
            SQL += "ISNULL(QT_MERCADORIA_AGENTE,0) AS QT_MERCADORIA_AGENTE, ";
            SQL += "ISNULL(NM_MERCADORIA,'') AS NM_MERCADORIA, ";
            SQL += "ISNULL(CD_INCOTERM,'') AS CD_INCOTERM, ";
            SQL += "ISNULL(format(DT_READY_DATE,'yyyy/MM/dd'),'') AS DT_READY_DATE, ";
            SQL += "ISNULL(format(DT_FORECAST_WH,'yyyy/MM/dd'),'') AS DT_FORECAST_WH, ";
            SQL += "ISNULL(format(DT_ARRIVE_WH,'yyyy/MM/dd'),'') AS DT_ARRIVE_WH, ";
            SQL += "ISNULL(format(DT_DRAFT_CUTOFF,'yyyy/MM/dd'),'') AS DT_DRAFT_CUTOFF, ";
            SQL += "ISNULL(format(TB_WEEK.DT_CUTOFF,'yyyy/MM/dd'),'') AS DT_CUTOFF, ";
            SQL += "ISNULL(CONVERT(VARCHAR(30),NR_BL),'') AS NR_BL,";
            SQL += "ISNULL(E.VL_FRETE_VENDA,0) AS VL_FRETE_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(varchar,FORMAT(VL_FRETE_VENDA_MIN,'C','PT-BR')),'R$',''),0) AS VL_FRETE_VENDA_MIN, ";
            SQL += "(SELECT ISNULL(REPLACE(CONVERT(varchar,FORMAT(Sum(H.VL_TAXA_VENDA),'C','PT-BR')),'R$',''),0) as VL_TAXA_VENDA FROM TB_BL J ";
            SQL += "LEFT JOIN TB_COTACAO G ON J.ID_COTACAO = G.ID_COTACAO ";
            SQL += "LEFT JOIN TB_COTACAO_TAXA H ON G.ID_COTACAO = H.ID_COTACAO ";
            SQL += "LEFT JOIN TB_MOEDA K ON G.ID_MOEDA_FRETE = K.ID_MOEDA ";
            SQL += "WHERE J.GRAU = 'C' ";
            SQL += "AND H.FL_DECLARADO = 1 ";
            SQL += "AND J.ID_BL = B.ID_BL )AS VL_TAXA_VENDA ";
            SQL += "FROM TB_BL B ";
            SQL += "LEFT JOIN TB_PARCEIRO P1 ON B.ID_PARCEIRO_VENDEDOR = P1.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON B.ID_PARCEIRO_CLIENTE = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P3 ON B.ID_PARCEIRO_IMPORTADOR = P3.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P4 ON B.ID_PARCEIRO_EXPORTADOR = P4.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_STATUS_BL ON B.ID_STATUS_BL = TB_STATUS_BL.ID_STATUS_BL ";
            SQL += "LEFT JOIN TB_MERCADORIA ON B.ID_MERCADORIA = TB_MERCADORIA.ID_MERCADORIA ";
            SQL += "LEFT JOIN TB_INCOTERM ON B.ID_INCOTERM = TB_INCOTERM.ID_INCOTERM ";
            SQL += "LEFT JOIN TB_WEEK ON B.ID_WEEK = TB_WEEK.ID_WEEK ";
            SQL += "LEFT JOIN TB_AMR_CNTR_BL D ON D.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_COTACAO C ON B.ID_COTACAO = C.ID_COTACAO ";
            SQL += "LEFT JOIN TB_COTACAO_MERCADORIA E ON C.ID_COTACAO = E.ID_COTACAO ";
            SQL += "WHERE B.ID_WEEK = '"+week+"' ";
            SQL += "AND D.ID_CNTR_BL IS NULL ";

            DataTable processoBl = new DataTable();
            processoBl = DBS.List(SQL);
            return JsonConvert.SerializeObject(processoBl);

        }

        [WebMethod(EnableSession = true)]
        public string ListarProcessoContainer(string cntr, string week)
        {
            string SQL;
            SQL = "SELECT B.ID_BL, ";
            SQL += "ISNULL(NM_STATUS_BL,'') AS NM_STATUS_BL, ";
            SQL += "ISNULL(format(DT_FLWP_LCL,'yyyy/MM/dd'),'') AS DT_FLWP_LCL, ";
            SQL += "ISNULL(CONVERT(VARCHAR,NR_PROCESSO),'') AS NR_PROCESSO, ";
            SQL += "ISNULL(P1.NM_RAZAO,'') AS VENDEDOR, ";
            SQL += "ISNULL(P2.NM_RAZAO,'') AS AGENTE, ";
            SQL += "ISNULL(P3.NM_RAZAO,'') AS IMPORTADOR, ";
            SQL += "ISNULL(P4.NM_RAZAO,'') AS EXPORTADOR, ";
            SQL += "ISNULL(B.VL_PESO_BRUTO,0) AS VL_PESO_BRUTO, ";
            SQL += "ISNULL(B.VL_M3,0) AS VL_M3, ";
            SQL += "ISNULL(B.QT_MERCADORIA,0) AS QT_MERCADORIA, ";
            SQL += "ISNULL(B.OB_REFERENCIA_AUXILIAR,'') AS REF_AUX, ";
            SQL += "ISNULL(B.OB_REFERENCIA_COMERCIAL,'') AS REF_COM, ";
            SQL += "ISNULL(VL_PESO_BRUTO_AGENTE,0) AS VL_PESO_BRUTO_AGENTE, ";
            SQL += "ISNULL(VL_M3_AGENTE,0) AS VL_M3_AGENTE, ";
            SQL += "ISNULL(QT_MERCADORIA_AGENTE,0) AS QT_MERCADORIA_AGENTE, ";
            SQL += "ISNULL(NM_MERCADORIA,'') AS NM_MERCADORIA, ";
            SQL += "ISNULL(CD_INCOTERM,'') AS CD_INCOTERM, ";
            SQL += "ISNULL(format(DT_READY_DATE,'yyyy/MM/dd'),'') AS DT_READY_DATE, ";
            SQL += "ISNULL(format(DT_FORECAST_WH,'yyyy/MM/dd'),'') AS DT_FORECAST_WH, ";
            SQL += "ISNULL(format(DT_ARRIVE_WH,'yyyy/MM/dd'),'') AS DT_ARRIVE_WH, ";
            SQL += "ISNULL(format(DT_DRAFT_CUTOFF,'yyyy/MM/dd'),'') AS DT_DRAFT_CUTOFF, ";
            SQL += "ISNULL(format(TB_WEEK.DT_CUTOFF,'yyyy/MM/dd'),'') AS DT_CUTOFF, ";
            SQL += "ISNULL(CONVERT(VARCHAR(30),NR_BL),'') AS NR_BL,";
            SQL += "ISNULL(E.VL_FRETE_VENDA,0) AS VL_FRETE_VENDA, ";
            SQL += "ISNULL(REPLACE(CONVERT(varchar,FORMAT(VL_FRETE_VENDA_MIN,'C','PT-BR')),'R$',''),'') AS VL_FRETE_VENDA_MIN, ";
            SQL += "(SELECT ISNULL(REPLACE(CONVERT(varchar,FORMAT(Sum(H.VL_TAXA_VENDA),'C','PT-BR')),'R$',''),'') as VL_TAXA_VENDA FROM TB_BL J ";
            SQL += "LEFT JOIN TB_COTACAO G ON J.ID_COTACAO = G.ID_COTACAO ";
            SQL += "LEFT JOIN TB_COTACAO_TAXA H ON G.ID_COTACAO = H.ID_COTACAO ";
            SQL += "LEFT JOIN TB_MOEDA K ON G.ID_MOEDA_FRETE = K.ID_MOEDA ";
            SQL += "WHERE J.GRAU = 'C' ";
            SQL += "AND H.FL_DECLARADO = 1 ";
            SQL += "AND J.ID_BL = B.ID_BL )AS VL_TAXA_VENDA ";
            SQL += "FROM TB_BL B ";
            SQL += "LEFT JOIN TB_PARCEIRO P1 ON B.ID_PARCEIRO_VENDEDOR = P1.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P2 ON B.ID_PARCEIRO_CLIENTE = P2.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P3 ON B.ID_PARCEIRO_IMPORTADOR = P3.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_PARCEIRO P4 ON B.ID_PARCEIRO_EXPORTADOR = P4.ID_PARCEIRO ";
            SQL += "LEFT JOIN TB_STATUS_BL ON B.ID_STATUS_BL = TB_STATUS_BL.ID_STATUS_BL ";
            SQL += "LEFT JOIN TB_MERCADORIA ON B.ID_MERCADORIA = TB_MERCADORIA.ID_MERCADORIA ";
            SQL += "LEFT JOIN TB_INCOTERM ON B.ID_INCOTERM = TB_INCOTERM.ID_INCOTERM ";
            SQL += "LEFT JOIN TB_WEEK ON B.ID_WEEK = TB_WEEK.ID_WEEK ";
            SQL += "LEFT JOIN TB_AMR_CNTR_BL D ON D.ID_BL = B.ID_BL ";
            SQL += "LEFT JOIN TB_COTACAO C ON B.ID_COTACAO = C.ID_COTACAO ";
            SQL += "LEFT JOIN TB_COTACAO_MERCADORIA E ON C.ID_COTACAO = E.ID_COTACAO ";
            SQL += "WHERE B.ID_WEEK = '" + week + "' ";
            SQL += "AND D.ID_CNTR_BL = '" + cntr + "' ";

            DataTable processoBl = new DataTable();
            processoBl = DBS.List(SQL);
            return JsonConvert.SerializeObject(processoBl);

        }

        [WebMethod(EnableSession = true)]
        public string limitesContainer(string cntr)
        {
            string SQL;
            SQL = "SELECT VL_PESO_MAX, VL_VOLUME_M3 FROM TB_CNTR_BL A ";
            SQL += "LEFT JOIN TB_TIPO_CONTAINER B ON B.ID_TIPO_CONTAINER = A.ID_TIPO_CNTR ";
            SQL += "WHERE ID_CNTR_BL = '"+cntr+"' ";

            DataTable limites = new DataTable();
            limites = DBS.List(SQL);
            return JsonConvert.SerializeObject(limites);

        }

        [WebMethod(EnableSession = true)]
        public string RemoverProcessoContainer(string processo, string cntr)
        {
            string SQL;
            SQL = "DELETE FROM TB_AMR_CNTR_BL WHERE ID_BL = '" + processo + "' AND ID_CNTR_BL = '"+cntr+"' ";

            string vincularProcesso = DBS.ExecuteScalar(SQL);
            return JsonConvert.SerializeObject("ok");

        }


        [WebMethod(EnableSession = true)]
        public string VincularProcessoWeek(string processo, string week)
        {
            string SQL;
            SQL = "UPDATE TB_BL SET ID_WEEK = '"+week+"' WHERE ID_BL = '"+processo+"' ";

            string vincularProcesso = DBS.ExecuteScalar(SQL);
            if(vincularProcesso != null)
            {
                return JsonConvert.SerializeObject("1");
            }
            else
            {
                return JsonConvert.SerializeObject("2");
            }
            
        }

        [WebMethod(EnableSession = true)]
        public string listarProcessos(int week)
        {
            string SQL;
            SQL = "SELECT ID_PARCEIRO FROM TB_WEEK WHERE ID_WEEK = '" + week + "'";
            DataTable agente = new DataTable();
            agente = DBS.List(SQL);
            string agentei = agente.Rows[0]["ID_PARCEIRO"].ToString();

            SQL = "SELECT A.ID_BL, A.NR_PROCESSO FROM TB_BL A ";
            SQL += "LEFT JOIN TB_SERVICO B ON A.ID_SERVICO = B.ID_SERVICO ";
            SQL += "LEFT JOIN TB_VIATRANSPORTE C ON B.ID_VIATRANSPORTE = C.ID_VIATRANSPORTE ";
            SQL += "LEFT JOIN TB_AMR_CNTR_BL D ON A.ID_BL = D.ID_BL ";
            SQL += "LEFT JOIN TB_WEEK E ON A.ID_WEEK = E.ID_WEEK ";
            SQL += "WHERE GRAU = 'C' AND C.ID_VIATRANSPORTE = 1";
            SQL += "AND A.ID_PARCEIRO_AGENTE_INTERNACIONAL = '" + agentei + "' ";
            SQL += "AND D.ID_CNTR_BL IS NULL ";
            SQL += "AND (A.ID_WEEK IS NULL OR A.ID_WEEK = 0)"; 
            DataTable processo = new DataTable();
            processo = DBS.List(SQL);

            return JsonConvert.SerializeObject(processo);
        }
        
        [WebMethod(EnableSession = true)]
        public string BuscarBLFCA(int processo)
        {
            string SQL;
            SQL = "SELECT ISNULL(ID_PARCEIRO_VENDEDOR,'') AS ID_PARCEIRO_VENDEDOR, ISNULL(ID_PARCEIRO_AGENTE,'') AS ID_PARCEIRO_AGENTE, ";
            SQL += "ISNULL(ID_PARCEIRO_IMPORTADOR,'') AS ID_PARCEIRO_IMPORTADOR, ISNULL(ID_PARCEIRO_EXPORTADOR,'') AS ID_PARCEIRO_EXPORTADOR, ";
            SQL += "ISNULL(CONVERT(VARCHAR,VL_PESO_BRUTO),'') AS VL_PESO_BRUTO, ISNULL(CONVERT(VARCHAR,VL_M3),'') AS VL_M3,  ISNULL(CONVERT(VARCHAR,QT_MERCADORIA),'') AS QT_MERCADORIA ";
            SQL += "FROM TB_BL ";
            SQL += "WHERE ID_BL = '"+processo+"' ";

            DataTable blfca = new DataTable();
            blfca = DBS.List(SQL);
            return JsonConvert.SerializeObject(blfca);
        }

        [WebMethod(EnableSession = true)]
        public string RemoverProcessoWeek(int processo)
        {
            string SQL;
            SQL = "UPDATE TB_BL SET ID_WEEK = NULL ";
            SQL += "WHERE ID_BL = '" + processo + "' ";

            string removerweek = DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("ok");
        }

        [WebMethod(EnableSession = true)]
        public string EditarBLFCA(string vendedor, string agente, string importador, string exportador, string pesobruto, string m3, string qtmercadoria, string processo)
        {
            if(vendedor == "" || vendedor == "0")
            {
                vendedor = "ID_PARCEIRO_VENDEDOR = null, ";
            }
            else
            {
                vendedor = "ID_PARCEIRO_VENDEDOR = " + vendedor + ", ";
            }

            if(agente == "" || agente == "0")
            {
                agente = "ID_PARCEIRO_AGENTE = null, ";
            }
            else
            {
                agente = "ID_PARCEIRO_AGENTE = "+agente+", ";
            }

            if(importador == "" || importador == "0")
            {
                importador = "ID_PARCEIRO_IMPORTADOR = null, ";
            }
            else
            {
                importador = "ID_PARCEIRO_IMPORTADOR = " + importador + ", ";
            }

            if(exportador == "" || exportador == "0")
            {
                exportador = "ID_PARCEIRO_EXPORTADOR = null, ";
            }
            else
            {
                exportador = "ID_PARCEIRO_EXPORTADOR = " + exportador + ", ";
            }

            if(pesobruto == "")
            {
                pesobruto = "VL_PESO_BRUTO = NULL, ";
            }
            else
            {
                pesobruto = "VL_PESO_BRUTO = " + pesobruto.Replace(",", ".") + ", ";
            }

            if(m3 == "")
            {
                m3 = "VL_M3 = NULL, ";
            }
            else
            {
                m3 = "VL_M3 = " + m3.Replace(",", ".") + ", ";
            }

            if(qtmercadoria == "")
            {
                qtmercadoria = "QT_MERCADORIA = NULL ";
            }
            else
            {
                qtmercadoria = "QT_MERCADORIA = "+qtmercadoria.Replace(",",".") + " ";
            }
            string SQL;
            SQL = "UPDATE TB_BL SET ";
            SQL += "" + vendedor + ""+agente+""+importador+""+exportador+""+pesobruto+""+m3+""+qtmercadoria+"";
            SQL += "WHERE ID_BL = '" + processo + "'";

            string editProcessos = DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("ok"); 
        }

        [WebMethod(EnableSession = true)]
        public string vincularContainer(string cntr, string bl)
        {
            string SQL;
            SQL = "INSERT INTO TB_AMR_CNTR_BL (ID_BL, ID_CNTR_BL) VALUES ('" + bl + "','" + cntr + "')";            
            string bindblcntr = DBS.ExecuteScalar(SQL);
            return JsonConvert.SerializeObject("ok");
        }

        [WebMethod(EnableSession = true)]
        public string BuscarBLParceiro(int processo)
        {
            string SQL;
            SQL = "SELECT ISNULL(CONVERT(VARCHAR,A.VL_PESO_BRUTO_AGENTE),'') AS VL_PESO_BRUTO_AGENTE, ISNULL(CONVERT(VARCHAR,A.VL_M3_AGENTE),'') AS VL_M3_AGENTE, ";
            SQL += "ISNULL(CONVERT(VARCHAR,A.QT_MERCADORIA_AGENTE),'') AS QT_MERCADORIA_AGENTE, ISNULL(B.ID_MERCADORIA,'') AS ID_MERCADORIA, ";
            SQL += "ISNULL(CONVERT(VARCHAR,A.ID_INCOTERM),'') AS ID_INCOTERM, ISNULL(CONVERT(VARCHAR,A.DT_READY_DATE),'') AS DT_READY_DATE,  ISNULL(CONVERT(VARCHAR,A.DT_FORECAST_WH),'') AS DT_FORECAST_WH, ";
            SQL += "ISNULL(CONVERT(VARCHAR,A.DT_ARRIVE_WH),'') AS DT_ARRIVE_WH, ISNULL(CONVERT(VARCHAR,A.DT_DRAFT_CUTOFF),'') AS DT_DRAFT_CUTOFF, ISNULL(CONVERT(VARCHAR,C.DT_CUTOFF),'') AS DT_CUTOFF, ";
            SQL += "ISNULL(A.NR_BL,'') AS NR_BL ";
            SQL += "FROM TB_BL A ";
            SQL += "LEFT JOIN TB_MERCADORIA B ON A.ID_MERCADORIA = B.ID_MERCADORIA ";
            SQL += "LEFT JOIN TB_WEEK C ON A.ID_WEEK = C.ID_WEEK ";
            SQL += "WHERE ID_BL = '" + processo + "' ";

            DataTable blfca = new DataTable();
            blfca = DBS.List(SQL);
            return JsonConvert.SerializeObject(blfca);
        }

        [WebMethod(EnableSession = true)]
        public string EditarBLPartner(string pesobrutoagente, string m3agente, string pcsagente, string packaging, string incoterm, string cargoreadydate, string deliveryforecastwh, string datearrivewh, string draftcutoff, string processo)
        {
            if (pesobrutoagente == "")
            {
                pesobrutoagente = "VL_PESO_BRUTO_AGENTE = null, ";
            }
            else
            {
                pesobrutoagente = "VL_PESO_BRUTO_AGENTE = " + pesobrutoagente.Replace(",", ".") + ", ";
            }

            if (m3agente == "")
            {
                m3agente = "VL_M3_AGENTE = null, ";
            }
            else
            {
                m3agente = "VL_M3_AGENTE = " + m3agente.Replace(",", ".") + ", ";
            }

            if (pcsagente == "")
            {
                pcsagente = "QT_MERCADORIA_AGENTE = null, ";
            }
            else
            {
                pcsagente = "QT_MERCADORIA_AGENTE = " + pcsagente.Replace(",", ".") + ", ";
            }

            if (packaging == "" || packaging == "0")
            {
                packaging = "ID_MERCADORIA = null, ";
            }
            else
            {
                packaging = "ID_MERCADORIA = " + packaging + ", ";
            }

            if (incoterm == "" || incoterm == "0")
            {
                incoterm = "ID_INCOTERM = NULL, ";
            }
            else
            {
                incoterm = "ID_INCOTERM = " + incoterm + ", ";
            }

            if (cargoreadydate == "")
            {
                cargoreadydate = "DT_READY_DATE = NULL, ";
            }
            else
            {
                cargoreadydate = "DT_READY_DATE = '" + cargoreadydate + "', ";
            }

            if (deliveryforecastwh == "")
            {
                deliveryforecastwh = "DT_FORECAST_WH = NULL, ";
            }
            else
            {
                deliveryforecastwh = "DT_FORECAST_WH = '" + deliveryforecastwh + "', ";
            }

            if (datearrivewh == "")
            {
                datearrivewh = "DT_ARRIVE_WH = NULL, ";
            }
            else
            {
                datearrivewh = "DT_ARRIVE_WH = '" + datearrivewh + "', ";
            }

            if (draftcutoff == "")
            {
                draftcutoff = "DT_DRAFT_CUTOFF = NULL ";
            }
            else
            {
                draftcutoff = "DT_DRAFT_CUTOFF = '" + draftcutoff + "' ";
            }
            string SQL;
            SQL = "UPDATE TB_BL SET ";
            SQL += "" + pesobrutoagente + "" + m3agente + "" + pcsagente + "" + packaging + "" + incoterm + "" + cargoreadydate + "" + deliveryforecastwh + ""+ datearrivewh + ""+ draftcutoff + "";
            SQL += "WHERE ID_BL = '" + processo + "'";

            string editProcessos = DBS.ExecuteScalar(SQL);

            return JsonConvert.SerializeObject("ok");
        }

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        public void EditarProcessoFCA(Processos dadosEdit)
        {
            string SQL;
            SQL = "UPDATE TB_BL SET ID_STATUS_BL = '" + dadosEdit.ID_STATUS_BL + "' ";
            SQL += "WHERE ID_BL = '" + dadosEdit.ID_BL + "' ";

            string editProcessos = DBS.ExecuteScalar(SQL);
        }

        [WebMethod(EnableSession = true)]
        public void DeletarTaxa(int Id, int IdTaxa)
        {
            string SQL;
            SQL = "DELETE FROM TB_TAXA_CLIENTE WHERE ID_PARCEIRO = '" + Id + "' AND ID_TAXA_CLIENTE = '" + IdTaxa + "' ";

            string deleteTaxa = DBS.ExecuteScalar(SQL);
            
        }

       /* [WebMethod(EnableSession = true)]
        public void EnviarEmail()
        {
            string SQL;
            SQL = "SELECT * FROM"
            Outlook.Application app = new Outlook.Application();
            Outlook.MailItem mail = (Outlook.MailItem)app.CreateItem(Outlook.OlItemType.olMailItem);
            mail.To = "thiago.amaro.r@gmail.com";
            mail.Subject = "Teste";
            mail.
            mail.Body = "";
            mail.Importance = Outlook.OlImportance.olImportanceNormal;
        }*/
    }
}
