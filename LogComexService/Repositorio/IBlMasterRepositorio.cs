using LogComexService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogComexService.Repositorio
{
    public interface IBlMasterRepositorio
    {
        List<BlMaster> ListarTodas();
        void CadastrarTokenBl(BlMaster bl);
        void AtualizarBlTraking(BlMaster bl);
    }
}
