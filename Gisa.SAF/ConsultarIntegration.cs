using Gisa.Domain;
using Gisa.Domain.Interfaces.Integration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.SAF
{
    public class ConsultarIntegration : BaseIntegration, IConsultarIntegration
    {
        public ConsultarIntegration(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task AgendarConsulta(Consulta consulta)
        {
            await EnviarMensagem(consulta, "consulta-agendar");
        }

        public async Task CancelarConsulta(Consulta consulta)
        {
            await EnviarMensagem(consulta, "consulta-cancelar");
        }

        public async Task AlterarStatusConsulta(Consulta consulta)
        {
            await EnviarMensagem(consulta, "consulta-status");
        }
    }
}
