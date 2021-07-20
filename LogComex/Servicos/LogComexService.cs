using LogComex.Models;
using LogComex.Repositorio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LogComex.Servicos
{
    public class LogComexService : ILogComexService
    {
        private readonly ILogComexRepositorio _logcomex_repositorio = new LogComexRepositorio();

        public Task EnviarLogComex()
        {
            throw new NotImplementedException();
        }

        public async Task<string> IniciarRastreioBl(BlMaster bl)
        {
            var token = "";
            string Url = ConfigurationManager.AppSettings["baseUrl"];
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            dynamic campos = null;
            campos = new
            {
                bl_number = bl.NR_BL,
                reference = "",
                consignee_cnpj = ConfigurationManager.AppSettings["cnpj"],
                emails = ConfigurationManager.AppSettings["email"],
                shipowner = bl.ID_ARMADOR_LOGCOMEX
            };
            var jsonString = JsonConvert.SerializeObject(campos);

            Task<string> response = await _logcomex_repositorio.ApiLogComex(Url, apiKey, jsonString);
            //dynamic jsonResponse = JsonConvert.DeserializeObject(response);
            var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Result);
            foreach (KeyValuePair<string, string> item in tokenResponse)
            {
                if (item.Key == "token")
                {
                    token = item.Value;
                }
            }
            return token;
        }
    }
}