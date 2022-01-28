using Gisa.Domain;
using Gisa.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gisa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsultaFluxoController : ApiControllerBase
    {
        #region [ Construtor ]

        public ConsultaFluxoController(IConsultaFluxoService consultaFluxoService)
        {
            _consultaFluxoService = consultaFluxoService;
        }

        #endregion

        #region [ Membros ]

        readonly IConsultaFluxoService _consultaFluxoService;

        #endregion

        #region [ Métodos ]

        /// <summary>
        /// Atualiza os dados de um passo do fluxo da consulta
        /// </summary>
        /// <param name="value">Dados do passo do fluxo da consulta</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ConsultaFluxo>> Put([FromBody] ConsultaFluxo value)
        {
            try
            {
                value = await _consultaFluxoService.AtualizarAsync(value);
                return (ActionResult)Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ConsultaFluxo>> Post([FromBody] ConsultaFluxo value)
        {
            try
            {
                value = await _consultaFluxoService.IncluirAsync(value);
                return (ActionResult)Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        #endregion
    }
}
