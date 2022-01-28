using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Integration
{
    public interface IConsultaIntegration
    {
        public Task AgendarConsulta(Consulta consulta);

        public Task CancelarConsulta(Consulta consulta);
    }
}
