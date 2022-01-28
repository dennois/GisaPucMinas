using Gisa.Domain;
using Gisa.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gisa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrestadorController : ControllerBase
    {
        #region [ Construtor ]

        public PrestadorController(IPrestadorService  prestadorService, IDistributedCache cache)
        {
            _prestadorService = prestadorService;
            _cache = cache;
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
        }

        #endregion

        #region [ Membros ]

        readonly IPrestadorService  _prestadorService;
        IDistributedCache _cache;
        readonly DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();

        #endregion

        /// <summary>
        /// Recupera os detalhes de um prestador
        /// </summary>
        /// <param name="id">Identificador único do prestador</param>
        /// <returns>Dados do prestador</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestador>> Get(long id)
        {
            Prestador prestador = null;
            try
            {
                prestador = await _prestadorService.RecuperarPorIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return prestador != null ? (ActionResult)Ok(prestador) : NoContent();
        }

        /// <summary>
        /// Recupera uma lista de prestadores
        /// </summary>
        /// <param name="conveniado"></param>
        /// <param name="especialidade"></param>
        /// <returns>Lista de prestadores</returns>
        [HttpGet("{conveniado}/{especialidade}")]
        public async Task<IEnumerable<Prestador>> Filtrar(long conveniado, long? especialidade)
        {
            return await _prestadorService.RecuperarResumo(conveniado, especialidade);
        }

        /// <summary>
        /// Inclui um novo prestador
        /// </summary>
        /// <param name="prestador">Dados do prestador a ser incluído</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Prestador>> Post([FromBody] Prestador prestador)
        {
            try
            {
                prestador = await _prestadorService.IncluirAsync(prestador);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return prestador != null && prestador.Identificador > 0 ? (ActionResult)Ok(prestador) : BadRequest("Ocorreu um erro ao salvar a consulta, tente novamente!");
        }

        /// <summary>
        /// Atualiza os dados de um prestador
        /// </summary>
        /// <param name="prestador">Dados do prestador</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Prestador prestador)
        {
            try
            {
                await _prestadorService.AtualizarAsync(prestador);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um prestador
        /// </summary>
        /// <param name="id">Identificador único do prestador</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _prestadorService.ExcluirAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
