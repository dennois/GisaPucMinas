using Gisa.Domain;
using Gisa.Domain.Interfaces.Integration;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gisa.SAF
{
    public class EspecialidadeIntegration : IEspecialidadeIntegration
    {
        private readonly IConfiguration _configuration;
        private readonly string serviceBusConnectionString;
        readonly string _queueName = "especialidade";

        public EspecialidadeIntegration(IConfiguration configuration)
        {
           
        }

        public Task AtualizarEspecialidade(Especialidade especialidade)
        {
            throw new NotImplementedException();
        }

        public async Task IncluirEspecialidade(Especialidade especialidade)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Especialidade>> RecuperarEspecialidades()
        {
            throw new NotImplementedException();
        }
    }
}
