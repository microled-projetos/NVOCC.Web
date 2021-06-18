using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogComex.Repositorio
{
    public class LogComexRepositorio : ILogComexRepositorio
    {
        public async Task<string> ApiLogComex(string url, string apiKey, string trakingRegister)
        {
            string tokenResponse = "";
            StringContent content = new StringContent(trakingRegister, Encoding.UTF8, "application/json");
            var baseAddress = new Uri(url);

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
            HttpResponseMessage response = await cliente.PostAsync(baseAddress + "rastreamento/maritimo/novo", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            //var response = await cliente.PostAsync(baseAddress + "rastreamento/maritimo/novo", content);

            string tokenGerado = await response.Content.ReadAsStringAsync();

            return tokenGerado;
        }

        public Task<string> DetalheRastreio(string url, string apiKey, string trakingRegister)
        {
            throw new NotImplementedException();
        }
    }
}