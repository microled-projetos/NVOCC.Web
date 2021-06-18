using LogComex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComex.Servicos
{
    public interface ILogComexService
    {
        Task<string> IniciarRastreioBl(BlMaster bl);
        Task EnviarLogComex();
    }
}
