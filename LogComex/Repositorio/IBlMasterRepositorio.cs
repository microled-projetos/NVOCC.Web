using LogComex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComex.Repositorio
{
    public interface IBlMasterRepositorio
    {
        IEnumerable<BlMaster> ListarTodas();
        BlMaster CadastrarTokenBl(BlMaster bl);
    }
}
