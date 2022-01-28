using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Repository
{
    public interface IFluxoRepository : IRepository<Fluxo>
    {
        Task<Fluxo> RecuperarPorCodigoAsync(string codigo);

        Task<Fluxo> RecuperarPorConsultaAsync(long consulta);

        Task<Fluxo> RecuperarPorIdAsync(long identificador);
    }
}
