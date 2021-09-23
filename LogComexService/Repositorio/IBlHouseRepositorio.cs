using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogComexService.Repositorio;
using LogComexService.Model;

namespace LogComexService.Repositorio
{
    public interface IBlHouseRepositorio
    {
        List<BlHouse> GetAllDadosBlHouse();
        void CadastrarTokenBLHouse(BlHouse bl);
        void AtualizarBLTrackingHouse(BlHouse bl);
    }
}
