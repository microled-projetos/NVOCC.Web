using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComex.Repositorio
{
    public interface ILogComexRepositorio
    {
        Task<string> ApiLogComex(string url, string apiKey, string trakingRegister);
        Task<string> DetalheRastreio(string url, string apiKey, string trakingRegister);
    }
}
