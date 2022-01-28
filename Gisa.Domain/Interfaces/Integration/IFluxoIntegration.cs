using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Integration
{
    public interface IFluxoIntegration
    {
        Task Incluir(Fluxo fluxo);

        Task Excluir(Fluxo fluxo);

        Task Alterar(Fluxo fluxo);

        Task<Fluxo> RecuperarDetalhe(long identificador);
    }
}
