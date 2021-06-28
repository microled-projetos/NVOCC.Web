﻿using LogComexService.Model;
using LogComexService.Repositorio;
using LogComexService.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogComexService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RastreioController : ControllerBase
    {

        private readonly ILogger<RastreioController> _logger;
        private readonly IBlMasterRepositorio _repositorio_bl;
        private readonly ILogComexService _servico_logcomex;

        public RastreioController(ILogger<RastreioController> logger, IBlMasterRepositorio repositorioBl, ILogComexService logComexService)
        {
            _logger = logger;
            _repositorio_bl = repositorioBl;
            _servico_logcomex = logComexService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_repositorio_bl.ListarTodas().ToList());
        }
        [HttpPost]
        [Route("rastrear/iniciar")]
        public IActionResult IniciarRastreioBL([FromBody] BlMaster bl)
        {
            if (bl.NR_BL == null)
            {
                var listBl = _repositorio_bl.ListarTodas();
                foreach (var item in listBl)
                {
                    if (item.BL_TOKEN == null || item.BL_TOKEN == "")
                    {
                        bl = new BlMaster { NR_BL = item.NR_BL, ID_ARMADOR_LOGCOMEX = item.ID_ARMADOR_LOGCOMEX };
                        var token1 = _servico_logcomex.IniciarRastreioLogComex(bl);
                        if (token1 != "")
                        {
                            bl.BL_TOKEN = token1;
                            _repositorio_bl.CadastrarTokenBl(bl);
                            _servico_logcomex.AtualizarRastreamentoLogComex(bl.BL_TOKEN);
                        }
                    }
                    else
                    {
                        bl = new BlMaster { NR_BL = item.NR_BL, BL_TOKEN = item.BL_TOKEN };
                        _servico_logcomex.AtualizarRastreamentoLogComex(bl.BL_TOKEN);
                    }
                }
            }
            else
            {
                var token = _servico_logcomex.IniciarRastreioLogComex(bl);
                if (token != "")
                {
                    bl.BL_TOKEN = token;
                    _repositorio_bl.CadastrarTokenBl(bl);

                }
            };

            
            return Ok();
        }

    }
}