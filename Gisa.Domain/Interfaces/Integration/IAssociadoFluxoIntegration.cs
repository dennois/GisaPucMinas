using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Integration
{
    public interface IAssociadoIntegration
    {
        Task Incluir(Associado associado);

        Task Excluir(Associado associado);

        Task Alterar(Associado associado);

        Task<Associado> RecuperarDetalhe(long identificador);
    }
}
