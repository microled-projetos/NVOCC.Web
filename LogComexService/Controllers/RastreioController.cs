using LogComexService.Model;
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
        private readonly IBlHouseRepositorio _repositorio_bl_house;
        private readonly ILogComexService _servico_logcomex;

        public RastreioController(ILogger<RastreioController> logger, IBlMasterRepositorio repositorioBl, ILogComexService logComexService, IBlHouseRepositorio repositorio_bl_house)
        {
            _logger = logger;
            _repositorio_bl = repositorioBl;
            _servico_logcomex = logComexService;
            _repositorio_bl_house = repositorio_bl_house;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            List<object> list = new List<object>();
            var query = _repositorio_bl.ListarTodas().ToList();            
            foreach (var item in query)
            {
                
                try
                {
                    BlMaster bl = new BlMaster();

                    bl.BL_TOKEN = item.BL_TOKEN;
                    bl.ID_ARMADOR_LOGCOMEX = item.ID_ARMADOR_LOGCOMEX;
                    bl.TRAKING_BL = item.TRAKING_BL;
                    bl.NR_BL = item.NR_BL;                    

                    IniciarRastreioBL(bl);                   

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }                
            }
            
            var queryHouse = _repositorio_bl_house.GetAllDadosBlHouse().ToList();
            foreach (var itemIn in queryHouse)
            {
                BlHouse blHouse = new BlHouse();

                blHouse.BL_TOKEN = itemIn.BL_TOKEN;
                blHouse.ID_ARMADOR_LOGCOMEX = itemIn.ID_ARMADOR_LOGCOMEX;
                blHouse.TRAKING_BL = itemIn.TRAKING_BL;
                blHouse.NR_BL = itemIn.NR_BL;

                IniciarRastreioBLHouse(blHouse);

            }

            list.AddRange(query.ToList());
            list.AddRange(queryHouse.ToList());                     

            return Ok(list);
        }
        [HttpPost]
        [Route("rastrear/iniciarBLHouse")]
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
                    try
                    {
                        bl.BL_TOKEN = token;
                        _servico_logcomex.AtualizarRastreamentoLogComex(bl.BL_TOKEN);
                        _repositorio_bl.CadastrarTokenBl(bl);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            };
            
            return Ok();
        }
        public IActionResult IniciarRastreioBLHouse([FromBody] BlHouse bl)
        {
            if (bl.NR_BL == null)
            {
                var listBLHouse = _repositorio_bl_house.GetAllDadosBlHouse();

                foreach (var item in listBLHouse)
                {
                    if (item.BL_TOKEN == null || item.BL_TOKEN == "")
                    {
                        bl = new BlHouse
                        {
                            NR_BL = item.NR_BL,
                            ID_ARMADOR_LOGCOMEX = item.ID_ARMADOR_LOGCOMEX,
                        };

                        var token_ = _servico_logcomex.IniciarRastreioBLHouseLogComex(bl);

                        if (token_ != "")
                        {
                            bl.BL_TOKEN = token_;
                            _repositorio_bl_house.CadastrarTokenBLHouse(bl);
                            _servico_logcomex.AtualizarRastreamentoLogComex(bl.BL_TOKEN);
                        }
                    }
                    else
                    {
                        bl = new BlHouse
                        {
                            NR_BL = bl.NR_BL,
                            BL_TOKEN = bl.BL_TOKEN,
                        };

                        _servico_logcomex.AtualizarRastreamentoLogComex(bl.BL_TOKEN);
                    }
                }
            }
            else
            {
                var token__ = _servico_logcomex.IniciarRastreioBLHouseLogComex(bl);

                if (token__ != "")
                {
                    try
                    {
                        bl.BL_TOKEN = token__;
                        _servico_logcomex.AtualizarRastreamentoLogComex(bl.BL_TOKEN);
                        _repositorio_bl_house.CadastrarTokenBLHouse(bl);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return Ok();
        }
    }
}
