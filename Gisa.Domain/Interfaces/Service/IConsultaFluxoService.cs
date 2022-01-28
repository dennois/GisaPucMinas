using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Service
{
    public interface IConsultaFluxoService
    {
        public Task<ConsultaFluxo> IncluirAsync(ConsultaFluxo  consultaFluxo);

        public Task<ConsultaFluxo> AtualizarAsync(ConsultaFluxo consultaFluxo);

        public Task<IEnumerable<ConsultaFluxo>> RecuperarResumoAsync(long consultaIdentificador);
    }
}
