using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Integration
{
    public interface IConveniadoIntegration
    {
        public Task Incluir(Consulta consulta);

        public Task Excluir(Consulta consulta);

        public Task Alterar(Consulta consulta);

        public Task<Consulta> RecuperarDetalhe(long identificador);
    }
}
