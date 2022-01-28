using Gisa.Domain;
using Gisa.Domain.Interfaces.Service;
using Gisa.WebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gisa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FluxoController : ControllerBase
    {
        public FluxoController(IFluxoService fluxoService)
        {
            _fluxoService = fluxoService;
        }

        readonly IFluxoService _fluxoService;

        /// <summary>
        /// Inclui um novo fluxo
        /// </summary>
        /// <param name="fluxo">Fluxo a ser incluido</param>
        /// <returns>Fluxo com o identificador</returns>
        [HttpPost]
        public async Task<ActionResult<Fluxo>> Post([FromBody] FluxoDTO fluxo)
        {
            try
            {
                fluxo.Processo = Newtonsoft.Json.JsonConvert.SerializeObject(Newtonsoft.Json.JsonConvert.DeserializeObject(fluxo.Processo));
                //var b = Newtonsoft.Json.JsonConvert.DeserializeObject(fluxo.ProcessoObject);
                await _fluxoService.IncluirAsync(fluxo);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return (ActionResult)Ok(fluxo);
        }

        /// <summary>
        /// Recupera um fluxo ativo por código
        /// </summary>
        /// <param name="codigo">Código do fluxo</param>
        /// <returns>Fluxo</returns>
        [HttpGet("{codigo}")]
        public async Task<ActionResult<Fluxo>> Get(string codigo)
        {
            Fluxo fluxo = null;
            try
            {
                fluxo = await _fluxoService.RecuperarPorCodigoAsync(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return fluxo != null ? (ActionResult)Ok(fluxo) : NoContent();
        }
    }
}
