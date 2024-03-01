using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using ABAINFRA.Web.Classes;
using Newtonsoft.Json;

namespace ABAINFRA.Web.Services
{
    /// <summary>
    /// Descrição resumida de Taxas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    [System.Web.Script.Services.ScriptService]
    public class Taxas : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string conferirTaxas(TaxasPersonal dados)
        {
            string SQL;

            if (string.IsNullOrEmpty(dados.VALOR))
            {
                dados.VALOR = "0.00";
            }

            SQL = "SELECT A.NR_PROCESSO, C.NM_ITEM_DESPESA, A.NR_BL, M.NR_BL, B.VL_TAXA_CALCULADO, A.DT_CHEGADA ";
            SQL += "FROM TB_BL A ";
            SQL += "JOIN TB_BL M ON A.ID_BL_MASTER = M.ID_BL ";
            SQL += "JOIN TB_BL_TAXA B ON A.ID_BL = B.ID_BL ";
            SQL += "JOIN TB_ITEM_DESPESA C ON B.ID_ITEM_DESPESA = C.ID_ITEM_DESPESA ";
            SQL += "WHERE B.ID_PARCEIRO_EMPRESA = 238332 ";
            SQL += "AND A.NR_PROCESSO = '" + dados.PROCESSO + "' ";
            SQL += "AND A.NR_BL = '" + dados.HOUSE + "' ";
            SQL += "AND M.NR_BL = '" + dados.MASTER + "' ";
            SQL += "AND B.VL_TAXA_CALCULADO = '" + dados.VALOR + "' ";
            SQL += "AND A.DT_CHEGADA = CONVERT(DATE,'" + dados.CHEGADA + "',103) ";
            SQL += "AND C.NM_ITEM_DESPESA = '" + dados.ITEM + "' ";
            DataTable listTable = new DataTable();
            listTable = DBS.List(SQL);

            if (listTable != null)
            {
                return "1";
            }

            return "0";
        }

        [WebMethod]
        public bool LiberarTaxas(List<TaxasPersonal> dados)
        {
            string idConta;
            bool status;
            string SQL;
            string SQL2 = "";
            try
            {
                SQL = "EXEC BaixaPersonalContaPagarReceber 3, '" + dados[0].DOCUMENTO + "', '" + dados[0].COMPETENCIA + "'; ";
                idConta = DBS.ExecuteScalar(SQL);

                for (int i = 0; i < dados.Count; i++)
                {
                    if (string.IsNullOrEmpty(dados[i].VALOR))
                    {
                        dados[i].VALOR = "0.00";
                    }

                    SQL = "SELECT B.ID_BL_TAXA ";
                    SQL += "FROM TB_BL A ";
                    SQL += "JOIN TB_BL M ON A.ID_BL_MASTER = M.ID_BL ";
                    SQL += "JOIN TB_BL_TAXA B ON A.ID_BL = B.ID_BL ";
                    SQL += "JOIN TB_ITEM_DESPESA C ON B.ID_ITEM_DESPESA = C.ID_ITEM_DESPESA ";
                    SQL += "WHERE B.ID_PARCEIRO_EMPRESA = 238332 ";
                    SQL += "AND A.NR_PROCESSO = '" + dados[i].PROCESSO + "' ";
                    SQL += "AND A.NR_BL = '" + dados[i].HOUSE + "' ";
                    SQL += "AND M.NR_BL = '" + dados[i].MASTER + "' ";
                    SQL += "AND B.VL_TAXA_CALCULADO = '" + dados[i].VALOR + "' ";
                    SQL += "AND A.DT_CHEGADA = CONVERT(DATE,'" + dados[i].CHEGADA + "',103) ";
                    SQL += "AND C.NM_ITEM_DESPESA = '" + dados[i].ITEM + "' ";
                    DataTable listTable = new DataTable();
                    listTable = DBS.List(SQL);

                    SQL2 += "EXEC BaixaPersonal " + idConta + ", " + listTable.Rows[0]["ID_BL_TAXA"] + ", '" + dados[0].DOCUMENTO + "', '" + dados[0].TIPO + "'; ";
                }
                status = DBS.BeginTransaction(SQL2);
                return status;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [WebMethod(EnableSession = true)]
        public string listarTaxas()
        {
            string SQL;
            try
            {
                SQL = "SELECT D.ID_CONTA_PAGAR_RECEBER_ITENS, E.NR_DOCUMENTO, D.TP_DOCUMENTO, ISNULL(E.NR_PEDIDO_COMPRA_TOTVS,'') NR_PEDIDO_COMPRA_TOTVS, 'BAIXA TOTVS' AS BAIXA_TOTVS, FORMAT(A.DT_CHEGADA,'dd/MM/yyyy') AS DT_CHEGADA, A.NR_PROCESSO, M.NR_BL AS MASTER, A.NR_BL AS HOUSE, ISNULL(FORMAT(E.DT_LIQUIDACAO, 'dd/MM/yyyy'),'') AS DT_LIQUIDACAO, E.DT_COMPETENCIA, C.NM_ITEM_DESPESA, B.VL_TAXA_CALCULADO, ISNULL(E.DS_STATUS_TOTVS,'') AS DS_STATUS_TOTVS ";
                SQL += "FROM TB_BL A ";
                SQL += "JOIN TB_BL M ON A.ID_BL_MASTER=M.ID_BL ";
                SQL += "JOIN TB_BL_TAXA B ON A.ID_BL = B.ID_BL ";
                SQL += "JOIN TB_ITEM_DESPESA C ON B.ID_ITEM_DESPESA = C.ID_ITEM_DESPESA ";
                SQL += "JOIN TB_CONTA_PAGAR_RECEBER_ITENS D ON B.ID_BL_TAXA = D.ID_BL_TAXA ";
                SQL += "JOIN TB_CONTA_PAGAR_RECEBER E ON E.ID_CONTA_PAGAR_RECEBER = D.ID_CONTA_PAGAR_RECEBER ";
                SQL += "WHERE B.ID_PARCEIRO_EMPRESA = 238332 ";
                SQL += "AND (E.NR_DOCUMENTO IS NOT NULL OR E.NR_DOCUMENTO <> '') ";
                SQL += "AND (D.TP_DOCUMENTO IS NOT NULL OR D.TP_DOCUMENTO <> '') ";
                SQL += "AND (E.DT_COMPETENCIA IS NOT NULL OR E.DT_COMPETENCIA <> '') ";
                SQL += "ORDER BY NR_DOCUMENTO";
                DataTable listTable = new DataTable();
                listTable = DBS.List(SQL);

                if (listTable != null)
                {
                    return JsonConvert.SerializeObject(listTable);
                }
                else
                {
                    return JsonConvert.SerializeObject("0");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public class ResultadoItem
        {
            public string code { get; set; }
            public string mensagem { get; set; }
            public string solucao { get; set; }
        }

        public class Resposta
        {
            public ResultadoItem[] resultado { get; set; }
        }

        public class Pedido
        {
            public string NR_PEDIDO { get; set; }
        }

        [WebMethod]
        public string CriarPedido(List<int> dados)
        {
            string SQL;
            string filter = "";
            var client = new HttpClient();

            for (int i = 0; i < dados.Count; i++)
            {
                if (i == 0)
                {
                    filter += dados[i].ToString();
                }
                else
                {
                    filter += "," + dados[i].ToString();
                }
            }


            try
            {
                SQL = "SELECT ( ";
                SQL += "SELECT '0602' AS C7_FILIAL, ";
                SQL += "1 AS C7_TIPO, ";
                SQL += "'000000' AS C7_NUM, ";
                SQL += "RIGHT('00000' + CAST(ROW_NUMBER() OVER (ORDER BY E.ID_CONTA_PAGAR_RECEBER) AS VARCHAR(4)), 4) AS C7_ITEM, ";
                SQL += "'' AS C7_PRODUTO, ";
                SQL += "' ' AS C7_UM, ";
                SQL += "1 AS C7_QUANT, ";
                SQL += "D.VL_LANCAMENTO AS C7_PRECO, ";
                SQL += "D.VL_LANCAMENTO AS C7_TOTAL, ";
                SQL += "'01' AS C7_LOCAL, ";
                SQL += "'' AS C7_OBSM, ";
                SQL += "'FOR1044' AS C7_FORNECE, ";
                SQL += "'01' AS C7_LOJA, ";
                SQL += "'' AS C7_CONTA, ";
                SQL += "REPLACE(REPLACE(A.NR_PROCESSO,'-',''),'/','') AS C7_ITEMCTA, ";
                SQL += "'' AS C7_CC, ";
                SQL += "'001' AS C7_COND, ";
                SQL += "'' AS C7_CONTATO, ";
                SQL += "'06' AS C7_FILENT, ";
                SQL += "REPLACE(FORMAT(GETDATE(), 'yyyy-MM-dd'), '-', '') AS C7_EMISSAO, ";
                SQL += "C.NM_ITEM_DESPESA AS C7_DESCRI, ";
                SQL += "'' AS C7_MSG, ";
                SQL += "'I' AS C7_CONTROL, ";
                SQL += "REPLACE(FORMAT(GETDATE(), 'yyyy-MM-dd'), '-', '') AS C7_DTIMP, ";
                SQL += "0 AS C7_ALQCF2, ";
                SQL += "0 AS C7_ALQCOF, ";
                SQL += "0 AS C7_ALQPIS, ";
                SQL += "0 AS C7_ALQPS2, ";
                SQL += "0 AS C7_BASCOF, ";
                SQL += "0 AS C7_BASPIS, ";
                SQL += "0 AS C7_VALCOF, ";
                SQL += "0 AS C7_VALPIS, ";
                SQL += "0 AS C7_VALISS, ";
                SQL += "0 AS C7_FISCORI, ";
                SQL += "C.ID_ITEM_DESPESA AS C7_XCODPRO, ";
                SQL += "C.NM_ITEM_DESPESA AS C7_XDESPRO, ";
                SQL += "E.ID_CONTA_PAGAR_RECEBER AS C7_XPEDLEG, ";
                SQL += "B.ID_PARCEIRO_EMPRESA AS C7_XCODFOR, ";
                SQL += "'01' AS C7_XLOJFOR, ";
                SQL += "D.NR_DOCUMENTO AS C7_XNFISC, ";
                SQL += "'0' AS C7_XSERIE, ";
                SQL += "D.TP_DOCUMENTO AS C7_XTPDOC ";
                SQL += "FROM TB_BL A ";
                SQL += "JOIN TB_BL M ON A.ID_BL_MASTER=M.ID_BL ";
                SQL += "JOIN TB_BL_TAXA B ON A.ID_BL = B.ID_BL ";
                SQL += "JOIN TB_ITEM_DESPESA C ON B.ID_ITEM_DESPESA = C.ID_ITEM_DESPESA ";
                SQL += "JOIN TB_CONTA_PAGAR_RECEBER_ITENS D ON B.ID_BL_TAXA = D.ID_BL_TAXA ";
                SQL += "JOIN TB_CONTA_PAGAR_RECEBER E ON E.ID_CONTA_PAGAR_RECEBER = D.ID_CONTA_PAGAR_RECEBER ";
                SQL += "WHERE B.ID_PARCEIRO_EMPRESA = 238332 ";
                SQL += "AND (E.NR_DOCUMENTO IS NOT NULL OR E.NR_DOCUMENTO <> '') ";
                SQL += "AND (D.TP_DOCUMENTO IS NOT NULL OR D.TP_DOCUMENTO <> '') ";
                SQL += "AND (E.DT_COMPETENCIA IS NOT NULL OR E.DT_COMPETENCIA <> '') ";
                SQL += "AND D.ID_CONTA_PAGAR_RECEBER_ITENS IN (" + filter + ") ";
                SQL += "ORDER BY E.NR_DOCUMENTO ";
                SQL += "FOR JSON PATH, ROOT ('PEDIDOS') ";
                SQL += ") AS JSON ";

                if (!string.IsNullOrEmpty(SQL))
                {
                    client.DefaultRequestHeaders.Add("IdEmp", "01");
                    client.DefaultRequestHeaders.Add("IdFilial", "0602");
                    var pedido = DBS.Json(SQL);
                    var dado = new StringContent(pedido, Encoding.UTF8, "application/json");

                    var response = client.PostAsync("http://abainfraestrutura144398.protheus.cloudtotvs.com.br:1116/api-rest/Pedidos", dado).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = response.Content.ReadAsStringAsync().Result;

                        // Desserializar a resposta JSON em um objeto C#
                        var responseObject = JsonConvert.DeserializeObject<Resposta>(responseContent);

                        // Adicionar um campo adicional à resposta


                        if (responseObject.resultado[0].code == "200")
                        {
                            string mensagem = responseObject.resultado[0].mensagem;
                            Match match = Regex.Match(mensagem, @"\[(\d+)\]");

                            Console.WriteLine(responseObject.resultado[0]);
                            string idConta = DBS.ExecuteScalar("SELECT MAX(ID_CONTA_PAGAR_RECEBER) AS ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER_ITENS IN (" + filter + ")");
                            try
                            {
                                SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET NR_PEDIDO_COMPRA_TOTVS = " + match.Groups[1].Value + " WHERE ID_CONTA_PAGAR_RECEBER = "+idConta+"";
                                DBS.BeginTransaction(SQL);
                                string modifiedResponseContent = JsonConvert.SerializeObject(responseObject);
                                return modifiedResponseContent;
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                        else
                        {
                            string message = responseObject.resultado[0].mensagem;
                            string modifiedResponseContent = JsonConvert.SerializeObject(responseObject);
                            return modifiedResponseContent;
                        }

                        // Serializar a resposta modificada de volta para uma string JSON
                    }
                    else
                    {
                        return $"HTTP Error: {response.StatusCode}";
                    }
                }
                else
                {
                    return "Consulta SQL retornou nulo ou vazio.";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [WebMethod]
        public string EstornarPedido(List<int> dados)
        {
            try
            {
                // Construir o filtro
                string filter = string.Join(",", dados);

                // Construir a consulta SQL
                string SQL = @"
            SELECT (
                SELECT '0602' AS C7_FILIAL,
                    1 AS C7_TIPO,
                    '000000' AS C7_NUM,
                    RIGHT('00000' + CAST(ROW_NUMBER() OVER (ORDER BY E.ID_CONTA_PAGAR_RECEBER) AS VARCHAR(4)), 4) AS C7_ITEM,
                    '' AS C7_PRODUTO,
                    ' ' AS C7_UM,
                    1 AS C7_QUANT,
                    D.VL_LANCAMENTO AS C7_PRECO,
                    D.VL_LANCAMENTO AS C7_TOTAL,
                    '01' AS C7_LOCAL,
                    '' AS C7_OBSM,
                    'FOR1044' AS C7_FORNECE,
                    '01' AS C7_LOJA,
                    '' AS C7_CONTA,
                    REPLACE(REPLACE(A.NR_PROCESSO,'-',''),'/','') AS C7_ITEMCTA,
                    '' AS C7_CC,
                    '001' AS C7_COND,
                    '' AS C7_CONTATO,
                    '06' AS C7_FILENT,
                    REPLACE(FORMAT(GETDATE(), 'yyyy-MM-dd'), '-', '') AS C7_EMISSAO,
                    C.NM_ITEM_DESPESA AS C7_DESCRI,
                    '' AS C7_MSG,
                    'I' AS C7_CONTROL,
                    REPLACE(FORMAT(GETDATE(), 'yyyy-MM-dd'), '-', '') AS C7_DTIMP,
                    0 AS C7_ALQCF2,
                    0 AS C7_ALQCOF,
                    0 AS C7_ALQPIS,
                    0 AS C7_ALQPS2,
                    0 AS C7_BASCOF,
                    0 AS C7_BASPIS,
                    0 AS C7_VALCOF,
                    0 AS C7_VALPIS,
                    0 AS C7_VALISS,
                    0 AS C7_FISCORI,
                    C.ID_ITEM_DESPESA AS C7_XCODPRO,
                    C.NM_ITEM_DESPESA AS C7_XDESPRO,
                    E.ID_CONTA_PAGAR_RECEBER AS C7_XPEDLEG,
                    B.ID_PARCEIRO_EMPRESA AS C7_XCODFOR,
                    '01' AS C7_XLOJFOR,
                    D.NR_DOCUMENTO AS C7_XNFISC,
                    '0' AS C7_XSERIE,
                    D.TP_DOCUMENTO AS C7_XTPDOC
                FROM TB_BL A
                JOIN TB_BL M ON A.ID_BL_MASTER=M.ID_BL
                JOIN TB_BL_TAXA B ON A.ID_BL = B.ID_BL
                JOIN TB_ITEM_DESPESA C ON B.ID_ITEM_DESPESA = C.ID_ITEM_DESPESA
                JOIN TB_CONTA_PAGAR_RECEBER_ITENS D ON B.ID_BL_TAXA = D.ID_BL_TAXA
                JOIN TB_CONTA_PAGAR_RECEBER E ON E.ID_CONTA_PAGAR_RECEBER = D.ID_CONTA_PAGAR_RECEBER
                WHERE B.ID_PARCEIRO_EMPRESA = 238332
                    AND (E.NR_DOCUMENTO IS NOT NULL OR E.NR_DOCUMENTO <> '')
                    AND (D.TP_DOCUMENTO IS NOT NULL OR D.TP_DOCUMENTO <> '')
                    AND (E.DT_COMPETENCIA IS NOT NULL OR E.DT_COMPETENCIA <> '')
                    AND D.ID_CONTA_PAGAR_RECEBER_ITENS IN (" + filter + @")
                ORDER BY E.NR_DOCUMENTO
                FOR JSON PATH, ROOT ('PEDIDOS')
            ) AS JSON";

                // Verificar se a consulta SQL não está vazia
                if (!string.IsNullOrEmpty(SQL))
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Delete, "http://abainfraestrutura144398.protheus.cloudtotvs.com.br:1116/api-rest/Pedidos");
                    request.Headers.Add("IdEmp", "01");
                    request.Headers.Add("IdFilial", "0602");
                    var content = new StringContent("{\"PEDIDOS\":[{\"C7_FILIAL\":\"0602\",\"C7_TIPO\":1,\"C7_NUM\":\"000000\",\"C7_ITEM\":\"0001\",\"C7_PRODUTO\":\"\",\"C7_UM\":\" \",\"C7_QUANT\":1,\"C7_PRECO\":40.00,\"C7_TOTAL\":40.00,\"C7_LOCAL\":\"01\",\"C7_OBSM\":\"\",\"C7_FORNECE\":\"FOR1044\",\"C7_LOJA\":\"01\",\"C7_CONTA\":\"\",\"C7_ITEMCTA\":\"M18970523\",\"C7_CC\":\"\",\"C7_COND\":\"001\",\"C7_CONTATO\":\"\",\"C7_FILENT\":\"06\",\"C7_EMISSAO\":\"20240219\",\"C7_DESCRI\":\"DESCONSOLIDACAO\",\"C7_MSG\":\"\",\"C7_CONTROL\":\"I\",\"C7_DTIMP\":\"20240219\",\"C7_ALQCF2\":0,\"C7_ALQCOF\":0,\"C7_ALQPIS\":0,\"C7_ALQPS2\":0,\"C7_BASCOF\":0,\"C7_BASPIS\":0,\"C7_VALCOF\":0,\"C7_VALPIS\":0,\"C7_VALISS\":0,\"C7_FISCORI\":0,\"C7_XCODPRO\":55,\"C7_XDESPRO\":\"DESCONSOLIDACAO\",\"C7_XPEDLEG\":208677,\"C7_XCODFOR\":238332,\"C7_XLOJFOR\":\"01\",\"C7_XNFISC\":\"123456\",\"C7_XSERIE\":\"0\",\"C7_XTPDOC\":\"NF\"},{\"C7_FILIAL\":\"0602\",\"C7_TIPO\":1,\"C7_NUM\":\"000000\",\"C7_ITEM\":\"0002\",\"C7_PRODUTO\":\"\",\"C7_UM\":\" \",\"C7_QUANT\":1,\"C7_PRECO\":40.00,\"C7_TOTAL\":40.00,\"C7_LOCAL\":\"01\",\"C7_OBSM\":\"\",\"C7_FORNECE\":\"FOR1044\",\"C7_LOJA\":\"01\",\"C7_CONTA\":\"\",\"C7_ITEMCTA\":\"M19450523\",\"C7_CC\":\"\",\"C7_COND\":\"001\",\"C7_CONTATO\":\"\",\"C7_FILENT\":\"06\",\"C7_EMISSAO\":\"20240219\",\"C7_DESCRI\":\"DESCONSOLIDACAO\",\"C7_MSG\":\"\",\"C7_CONTROL\":\"I\",\"C7_DTIMP\":\"20240219\",\"C7_ALQCF2\":0,\"C7_ALQCOF\":0,\"C7_ALQPIS\":0,\"C7_ALQPS2\":0,\"C7_BASCOF\":0,\"C7_BASPIS\":0,\"C7_VALCOF\":0,\"C7_VALPIS\":0,\"C7_VALISS\":0,\"C7_FISCORI\":0,\"C7_XCODPRO\":55,\"C7_XDESPRO\":\"DESCONSOLIDACAO\",\"C7_XPEDLEG\":208677,\"C7_XCODFOR\":238332,\"C7_XLOJFOR\":\"01\",\"C7_XNFISC\":\"123456\",\"C7_XSERIE\":\"0\",\"C7_XTPDOC\":\"NF\"},{\"C7_FILIAL\":\"0602\",\"C7_TIPO\":1,\"C7_NUM\":\"000000\",\"C7_ITEM\":\"0003\",\"C7_PRODUTO\":\"\",\"C7_UM\":\" \",\"C7_QUANT\":1,\"C7_PRECO\":40.00,\"C7_TOTAL\":40.00,\"C7_LOCAL\":\"01\",\"C7_OBSM\":\"\",\"C7_FORNECE\":\"FOR1044\",\"C7_LOJA\":\"01\",\"C7_CONTA\":\"\",\"C7_ITEMCTA\":\"M19470523\",\"C7_CC\":\"\",\"C7_COND\":\"001\",\"C7_CONTATO\":\"\",\"C7_FILENT\":\"06\",\"C7_EMISSAO\":\"20240219\",\"C7_DESCRI\":\"DESCONSOLIDACAO\",\"C7_MSG\":\"\",\"C7_CONTROL\":\"I\",\"C7_DTIMP\":\"20240219\",\"C7_ALQCF2\":0,\"C7_ALQCOF\":0,\"C7_ALQPIS\":0,\"C7_ALQPS2\":0,\"C7_BASCOF\":0,\"C7_BASPIS\":0,\"C7_VALCOF\":0,\"C7_VALPIS\":0,\"C7_VALISS\":0,\"C7_FISCORI\":0,\"C7_XCODPRO\":55,\"C7_XDESPRO\":\"DESCONSOLIDACAO\",\"C7_XPEDLEG\":208677,\"C7_XCODFOR\":238332,\"C7_XLOJFOR\":\"01\",\"C7_XNFISC\":\"123456\",\"C7_XSERIE\":\"0\",\"C7_XTPDOC\":\"NF\"},{\"C7_FILIAL\":\"0602\",\"C7_TIPO\":1,\"C7_NUM\":\"000000\",\"C7_ITEM\":\"0004\",\"C7_PRODUTO\":\"\",\"C7_UM\":\" \",\"C7_QUANT\":1,\"C7_PRECO\":40.00,\"C7_TOTAL\":40.00,\"C7_LOCAL\":\"01\",\"C7_OBSM\":\"\",\"C7_FORNECE\":\"FOR1044\",\"C7_LOJA\":\"01\",\"C7_CONTA\":\"\",\"C7_ITEMCTA\":\"M19000523\",\"C7_CC\":\"\",\"C7_COND\":\"001\",\"C7_CONTATO\":\"\",\"C7_FILENT\":\"06\",\"C7_EMISSAO\":\"20240219\",\"C7_DESCRI\":\"DESCONSOLIDACAO\",\"C7_MSG\":\"\",\"C7_CONTROL\":\"I\",\"C7_DTIMP\":\"20240219\",\"C7_ALQCF2\":0,\"C7_ALQCOF\":0,\"C7_ALQPIS\":0,\"C7_ALQPS2\":0,\"C7_BASCOF\":0,\"C7_BASPIS\":0,\"C7_VALCOF\":0,\"C7_VALPIS\":0,\"C7_VALISS\":0,\"C7_FISCORI\":0,\"C7_XCODPRO\":55,\"C7_XDESPRO\":\"DESCONSOLIDACAO\",\"C7_XPEDLEG\":208677,\"C7_XCODFOR\":238332,\"C7_XLOJFOR\":\"01\",\"C7_XNFISC\":\"123456\",\"C7_XSERIE\":\"0\",\"C7_XTPDOC\":\"NF\"}]}\r\n", null, "application/json");
                    request.Content = content;



                    try
                    {
                        var response = client.SendAsync(request).Result;
                        response.EnsureSuccessStatusCode();
                        response.Content.ReadAsStringAsync().Wait();

                        // Verificar se a solicitação foi bem-sucedida
                        if (response.IsSuccessStatusCode)
                        {
                            // Ler o conteúdo da resposta
                            var responseContent = response.Content.ReadAsStringAsync().Result;

                            // Desserializar a resposta JSON em um objeto C#
                            var responseObject = JsonConvert.DeserializeObject<Resposta>(responseContent);

                            // Verificar se o código de resposta é 200
                            if (responseObject.resultado[0].code == "200")
                            {
                                string mensagem = responseObject.resultado[0].mensagem;
                                Match match = Regex.Match(mensagem, @"\[(\d+)\]");

                                // Registrar o resultado no console
                                Console.WriteLine(responseObject.resultado[0]);

                                // Executar uma consulta SQL para atualizar um campo no banco de dados
                                string idConta = DBS.ExecuteScalar("SELECT MAX(ID_CONTA_PAGAR_RECEBER) AS ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER_ITENS IN (" + filter + ")");
                                try
                                {
                                    SQL = "UPDATE TB_CONTA_PAGAR_RECEBER SET NR_PEDIDO_COMPRA_TOTVS = NULL WHERE ID_CONTA_PAGAR_RECEBER = "+idConta+"";
                                    DBS.ExecuteScalar(SQL);

                                    // Serializar a resposta modificada de volta para uma string JSON
                                    string modifiedResponseContent = JsonConvert.SerializeObject(responseObject);
                                    return modifiedResponseContent;
                                }
                                catch (Exception e)
                                {
                                    throw e;
                                }
                            }
                            else
                            {
                                string message = responseObject.resultado[0].mensagem;
                                string modifiedResponseContent = JsonConvert.SerializeObject(responseObject);
                                return modifiedResponseContent;
                            }
                        }
                        else
                        {
                            return $"HTTP Error: {response.StatusCode}";
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                else
                {
                    return "Consulta SQL retornou nulo ou vazio.";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
