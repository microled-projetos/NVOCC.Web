using LogComexService.Model;
using LogComexService.Repositorio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogComexService.Servico
{
    public class LogComexServico : ILogComexService
    {
        private readonly ILogComexRepositorio _logComexRepositorio;
        private readonly IBlMasterRepositorio _blmasterRepositorio;
        public LogComexServico(ILogComexRepositorio logComexRepositorio, IBlMasterRepositorio blMasterRepositorio)
        {
            _logComexRepositorio = logComexRepositorio;
            _blmasterRepositorio = blMasterRepositorio;
        }
        public string IniciarRastreioLogComex(BlMaster bl)
        {
            var token = "";
            string Url = "https://api.logcomex.io/api/v3/";
            string apiKey = "7b86d436a5d89ac4c8be11553b432bad";
            //var nbl = new BL { BlNUmber = "123456", BlTOken = "", Id = 0, PartnerIdCustomer = 0 };
            dynamic campos = null;
            campos = new
            {
                bl_number = bl.NR_BL,
                reference = "",
                consignee_cnpj = "58138058003100",
                emails = "andre.rodrigues@abainfra.com.br",
                shipowner = bl.ID_ARMADOR_LOGCOMEX
            };
            var jsonString = JsonConvert.SerializeObject(campos);

            Task<string> response = _logComexRepositorio.ApiLogComex(Url, apiKey, jsonString);
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
        public List<BlMaster> ListarBls()
        {
            return _blmasterRepositorio.ListarTodas();
        }
        public object AtualizarRastreamentoLogComex(string token)
        {
            string Url = "https://api.logcomex.io/api/v3/";
            string apiKey = "7b86d436a5d89ac4c8be11553b432bad";

            Task<string> response = _logComexRepositorio.AtualizarRastreamento(Url, apiKey, token);
            var detailResponse = response.Result;
            var json = JsonConvert.DeserializeObject(detailResponse);
            //
            var listBl = _blmasterRepositorio.ListarTodas();
            var bl = listBl.Find(x => x.BL_TOKEN == token);
            if (bl.BL_TOKEN != null || bl.BL_TOKEN != "")
            {
                bl.TRAKING_BL = json.ToString();
                _blmasterRepositorio.AtualizarBlTraking(bl);
            }
            else
            {
                return null;
            }
            return json;
        }
    }
}
