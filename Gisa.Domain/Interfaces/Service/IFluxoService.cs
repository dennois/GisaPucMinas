using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Service
{
    public interface IFluxoService
    {
        Task<Fluxo> IncluirAsync(Fluxo fluxo);

        Task<Fluxo> RecuperarPorCodigoAsync(string codigo);

        Task<Fluxo> RecuperarPorConsultaAsync(long consulta);

        Task<Fluxo> RecuperarPorIdAsync(long id);
    }
}
