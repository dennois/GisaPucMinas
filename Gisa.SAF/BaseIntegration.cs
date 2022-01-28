using Confluent.Kafka;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gisa.SAF
{
    public class BaseIntegration
    {
        private readonly IConfiguration _configuration;
        private readonly string serviceBusConnectionString;

        public BaseIntegration(IConfiguration configuration)
        {
            this._configuration = configuration;
            serviceBusConnectionString = _configuration.GetConnectionString("KafkaConnectionString");
        }

        public async Task EnviarMensagem(object mensagem, string nomeFila)
        {
            //var client = new QueueClient(serviceBusConnectionString, nomeFila, ReceiveMode.PeekLock);
            string messageBody = JsonSerializer.Serialize(mensagem);
            //var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            //await client.SendAsync(message);
            //await client.CloseAsync();

            var config = new ProducerConfig
            {
                BootstrapServers = serviceBusConnectionString
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var result = await producer.ProduceAsync(nomeFila, new Message<Null, string> { Value = messageBody });
            }

        }
    }
}
