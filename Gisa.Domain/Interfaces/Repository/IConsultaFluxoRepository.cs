using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Repository
{
    public interface IConsultaFluxoRepository : IRepository<ConsultaFluxo>
    {
        Task<IEnumerable<ConsultaFluxo>> RecuperarResumoAsync(long consultaIdentificador);

        Task<ConsultaFluxo> RecuperarProximoAsync(long identificador, long consulta);
    }
}
