using Confluent.Kafka;
using Kafka_POC.Interfaces;
using Kafka_POC.Models;
using Newtonsoft.Json;

namespace Kafka_POC.Services
{
    public class ProducerService : IProducer
    {
        private readonly ILogger<ProducerService> _logger;
        private readonly ProducerConfig _producerConfig;
        private readonly IConfiguration _configuration;
        private readonly string _topic;

        public ProducerService(ILogger<ProducerService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _producerConfig = new ProducerConfig { BootstrapServers = _configuration["kafka:server"] };
            _topic = _configuration["kafka:topic"];
        }

        public async Task Insert(DroneDados dados)
        {
            try
            {
                using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();

                string payload = JsonConvert.SerializeObject(dados);

                var result = await producer.ProduceAsync(_topic, new() { Value = payload });

                _logger.LogInformation($"PRODUCER: Dado inserido: {payload} | Status: {result.Status}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção: {ex.GetType().FullName} | Mensagem: {ex.Message}");
            }
        }

        public async Task InsertMany(int quantidade)
        {
            try
            {
                List<DroneDados> listaDados = GenerateDroneDados(quantidade);

                using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                {
                    foreach (DroneDados dados in listaDados)
                    {
                        string payload = JsonConvert.SerializeObject(dados);

                        var result = await producer.ProduceAsync(_topic, new() { Value = payload });

                        _logger.LogInformation($"PRODUCER: Dado inserido: {payload} | Status: {result.Status}");
                    }
                }

                _logger.LogInformation($"Concluído o envio de {quantidade} mensagens.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção: {ex.GetType().FullName} | Mensagem: {ex.Message}");
            }
        }

        private static List<DroneDados> GenerateDroneDados(int quantidade)
        {
            List<DroneDados> droneDados = new();
            Random random = new();

            for (int i = 0; i < quantidade; i++)
            {
                droneDados.Add(new DroneDados()
                {
                    DroneId = random.Next(),
                    Latitude = Convert.ToDouble($"{random.Next(-89, 89)}.{random.Next(100000000, 999999999)}"  ),
                    Longitude = Convert.ToDouble($"{random.Next(-179, 179)}.{random.Next(100000000, 999999999)}"),
                    RastreioAtivo = random.NextDouble() >= 0.5,
                    Temperatura = random.Next(-25, 40),
                    Umidade = random.Next(0, 100)
                });
            }
            return droneDados;
        }
    }
}