using Gisa.Domain;
using Gisa.Domain.DTO;
using Gisa.Domain.Enum;
using Gisa.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ConveniadoController : ApiControllerBase
    {
        #region [ Construtor ]

        public ConveniadoController(IConveniadoService conveniadoService)
        {
            _conveniadoService = conveniadoService;
        }

        #endregion

        #region [ Membros ]

        readonly IConveniadoService _conveniadoService;

        #endregion

        /// <summary>
        /// Recupera os detalhes de um conveniado
        /// </summary>
        /// <param name="id">Identificador único do conveniado</param>
        /// <returns>Dados do conveniado</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Conveniado>> Get(long id)
        {
            Conveniado conveniado = null;
            try
            {
                conveniado = await _conveniadoService.RecuperarPorIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return conveniado != null ? (ActionResult)Ok(conveniado) : NoContent();
        }

        /// <summary>
        /// Recupera uma lista de conveniados
        /// </summary>
        /// <param name="filtro">Filtro para os conveniados</param>
        /// <returns>Lista de conveniados</returns>
        [HttpGet("Filtrar")]
        public async Task<IEnumerable<Conveniado>> Filtrar(string nome, string tipo, long? especialidade, string estado, string cidade)
        {
            ConveniadoFiltro filtro = new ConveniadoFiltro();
            filtro.Nome = nome;
            filtro.Especialidade = especialidade;
            if (!string.IsNullOrWhiteSpace(tipo))
                filtro.ConveniadoTipo = (Enums.ConveniadoTipo)tipo.ToCharArray()[0];
            if (!string.IsNullOrWhiteSpace(estado))
                filtro.Estado = estado;
            if (!string.IsNullOrWhiteSpace(cidade))
                filtro.Cidade = cidade;
            return await _conveniadoService.RecuperarResumo(filtro);
        }

        /// <summary>
        /// Inclui um novo conveniado
        /// </summary>
        /// <param name="conveniado">Dados do conveniado a ser incluído</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Conveniado>> Post([FromBody] Conveniado  conveniado)
        {
            try
            {
                conveniado = await _conveniadoService.IncluirAsync(conveniado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return conveniado != null && conveniado.Identificador > 0 ? (ActionResult)Ok(conveniado) : BadRequest("Ocorreu um erro ao salvar a consulta, tente novamente!");
        }

        /// <summary>
        /// Atualiza os dados de um conveniado
        /// </summary>
        /// <param name="value">Dados do conveniado</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Conveniado>> Put([FromBody] Conveniado value)
        {
            try
            {
                value = await _conveniadoService.AtualizarAsync(value);
                return (ActionResult)Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um conveniado
        /// </summary>
        /// <param name="id">Identificador único do conveniado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _conveniadoService.ExcluirAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Recupera todos os estados atendidos pela rede de conveniados
        /// </summary>
        /// <returns>Lista de estados</returns>
        [HttpGet("estados")]
        public async Task<ActionResult<IEnumerable<string>>> GetEstados()
        {
            IEnumerable<string> estados = null;
            try
            {
                estados =  await _conveniadoService.RecuperarEstados();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return estados != null ? (ActionResult)Ok(estados) : NoContent();
        }

        /// <summary>
        /// Recupera todas as cidades atendidos pela rede de conveniados do estado selecionado
        /// </summary>
        /// <param name="estado">Estado para filtrar as cidades</param>
        /// <returns>Lista de cidades</returns>
        [HttpGet("cidades/{estado}")]
        public async Task<ActionResult<IEnumerable<string>>> GetCidades(string estado)
        {
            IEnumerable<string> estados = null;
            try
            {
                estados = await _conveniadoService.RecuperarCidades(estado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return estados != null ? (ActionResult)Ok(estados) : NoContent();
        }
    }
}
