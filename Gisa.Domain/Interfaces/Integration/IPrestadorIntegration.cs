using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Integration
{
    public interface IPrestadorIntegration
    {
        Task Incluir(Prestador prestador);

        Task Excluir(Prestador prestador);

        Task Alterar(Prestador prestador);

        Task<Prestador> RecuperarDetalhe(long identificador);
    }
}
