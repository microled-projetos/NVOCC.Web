using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogComexService.Repositorio
{
    public interface ILogComexRepositorio
    {
        Task<string> ApiLogComex(string url, string apiKey, string trakingRegister);
        Task<string> AtualizarRastreamento(string url, string apiKey, string trakingRegister);
    }
}
