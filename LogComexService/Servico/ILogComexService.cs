using LogComexService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogComexService.Servico
{
    public interface ILogComexService
    {
        string IniciarRastreioLogComex(BlMaster bl);
        string IniciarRastreioBLHouseLogComex(BlHouse bl);
        object AtualizarRastreamentoLogComex(string token);
        


    }
}
