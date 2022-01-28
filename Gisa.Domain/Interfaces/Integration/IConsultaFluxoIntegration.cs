using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Integration
{
    public interface IConsultaFluxoIntegration
    {
        Task Incluir(ConsultaFluxo consultaFluxo);

        Task Excluir(ConsultaFluxo consultaFluxo);

        Task Alterar(ConsultaFluxo consultaFluxo);

        Task<ConsultaFluxo> RecuperarDetalhe(long identificador);
    }
}
