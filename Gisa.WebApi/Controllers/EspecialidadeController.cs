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
    public class EspecialidadeController : ControllerBase
    {
        #region [ Construtor ]

        public EspecialidadeController(IEspecialidadeService especialidadeService, IDistributedCache cache)
        {
            _especialidadeService = especialidadeService;
            _cache = cache;
            cacheOptions.SetAbsoluteExpiration( TimeSpan.FromMinutes(1));
        }

        #endregion

        #region [ Membros ]

        readonly IEspecialidadeService _especialidadeService;
        IDistributedCache _cache;
        readonly DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();

        #endregion

        /// <summary>
        /// Recupera todas as especialidades
        /// </summary>
        /// <returns>Lista de especialidades</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especialidade>>> Get()
        {
            IEnumerable<Especialidade> especialidades = null;
            try
            {
                string cache = await _cache.GetStringAsync("Gisa.Especialidade");
                if (String.IsNullOrEmpty(cache))
                {
                    especialidades = await _especialidadeService.RecuperarTudo();
                    await _cache.SetStringAsync("Gisa.Especialidade", Newtonsoft.Json.JsonConvert.SerializeObject(especialidades), cacheOptions);
                }
                else
                {
                    especialidades = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Especialidade>>(cache);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return especialidades != null ? (ActionResult)Ok(especialidades.OrderBy(p => p.Nome)) : NoContent();
        }

        /// <summary>
        /// Recupera especialidades atendidadas por tipo de conveniado
        /// </summary>
        /// <param name="tipoConveniado">Tipo de conveniado</param>
        /// <returns>Lista de especialidades</returns>
        [HttpGet("recuperar/{tipoConveniado}")]
        public async Task<ActionResult<IEnumerable<Especialidade>>> GetPorTipo(string tipoConveniado)
        {
            IEnumerable<Especialidade> especialidades = null;
            try
            {
                especialidades = await _especialidadeService.RecuperarPorConveniadoTipo(tipoConveniado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return especialidades != null ? (ActionResult)Ok(especialidades.OrderBy(p => p.Nome)) : NoContent();
        }

        /// <summary>
        /// Recupera os detalhes de uma especialidade
        /// </summary>
        /// <param name="id">Identificador único da especialidade</param>
        /// <returns>Detalhes da especialidade</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidade>> Get(int id)
        {
            Especialidade especialidade = null;
            try
            {
                especialidade = await _especialidadeService.RecuperarPorIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return especialidade != null ? (ActionResult)Ok(especialidade) : NoContent();
        }

        /// <summary>
        /// Inclui uma nova especialidade
        /// </summary>
        /// <param name="especialidade">Dados da especialidade a ser incluída</param>
        /// <returns>Detalhes da especialidade</returns>
        [HttpPost]
        public async Task<ActionResult<Especialidade>> Post([FromBody] Especialidade especialidade)
        {
            try
            {
                especialidade = await _especialidadeService.IncluirAsync(especialidade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return especialidade != null && especialidade.Identificador > 0 ? (ActionResult)Ok(especialidade) : BadRequest("Ocorreu um erro ao salvar a consulta, tente novamente!");
        }

        /// <summary>
        /// Atualiza os dados de uma especialidade
        /// </summary>
        /// <param especialidade="value">Dados da especialidade</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Especialidade especialidade)
        {
            try
            {
                await _especialidadeService.AtualizarAsync(especialidade);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma especialidade
        /// </summary>
        /// <param name="id">Identificador único da especialidade</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _especialidadeService.ExcluirAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
