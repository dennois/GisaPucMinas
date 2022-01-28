using Gisa.Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gisa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        /// <summary>
        /// Retorna todos os laboratórios cadastrados.
        /// </summary>
        /// <returns>Lista com todos os labotarórios</returns>
        [HttpGet]
        [SwaggerOperation]
        public IEnumerable<Laboratorio> Get()
        {
            return new List<Laboratorio>();
        }

        /// <summary>
        /// Retorna os detalhes do laboratório inforamdo.
        /// </summary>
        /// <param name="id">Identificador único do laboratório</param>
        /// <returns>Laboratório</returns>
        [HttpGet("{id}")]
        public Laboratorio Get(int id)
        {
            return new Laboratorio();
        }

        /// <summary>
        /// Inclui um novo laboratório
        /// </summary>
        /// <param name="value">Objeto com as informações do laboratório a ser incluído</param>
        [HttpPost]
        public void Post([FromBody] Laboratorio value)
        {
        }

        /// <summary>
        /// Atualiza as informações do laboratório
        /// </summary>
        /// <param name="id">Identificador único do laboratório</param>
        /// <param name="value">Objeto com as informações do laboratório a ser atualizado</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Laboratorio value)
        {
        }

        /// <summary>
        /// Exclui o laboratório com identificador informado
        /// </summary>
        /// <param name="id">Identificador único do laboratório</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
