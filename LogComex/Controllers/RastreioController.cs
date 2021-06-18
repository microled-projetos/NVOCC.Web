using LogComex.Models;
using LogComex.Repositorio;
using LogComex.Servicos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LogComex.Controllers
{
    public class RastreioController : ApiController
    {
        private readonly IBlMasterRepositorio _blmaster_repositorio = new BlMasterRepositorio();
        private readonly ILogComexService _servico_logcomex = new LogComexService();
       
        public RastreioController()
        {

        }

        [HttpGet]
        public HttpResponseMessage GetBlsMaster()
        {
            List<BlMaster> listaBlsMaster = _blmaster_repositorio.ListarTodas().ToList();
            
            return Request.CreateResponse<List<BlMaster>>(HttpStatusCode.OK, listaBlsMaster);
        }

        public HttpResponseMessage Post([FromBody] BlMaster bl)
        {
            _servico_logcomex.IniciarRastreioBl(bl);
            return Request.CreateResponse("tudo certo");
        }


    }
}
