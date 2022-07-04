using Kafka_POC.Interfaces;
using Kafka_POC.Models;
using Newtonsoft.Json;

namespace Kafka_POC.Services
{
    public class ConsumerService : IConsumer
    {
        private readonly ILogger<ConsumerService> _logger;

        public ConsumerService(ILogger<ConsumerService> logger)
        {
            _logger = logger;
        }

        public Task ProcessarDados(DroneDados dados)
        {
            if(dados.Temperatura >= 35 || dados.Temperatura <=0 || dados.Umidade <= 15)
                _logger.LogInformation($"CONSUMER: Os dados do drone {dados.DroneId} foram processados com exito! Dados: {JsonConvert.SerializeObject(dados)}");
            else
                _logger.LogInformation($"CONSUMER: Os dados do drone {dados.DroneId} não são validos.");

            return Task.CompletedTask;
        }
    }
}
