using Confluent.Kafka;
using Kafka_POC.Models;
using Newtonsoft.Json;

namespace Kafka_POC.Jobs
{
    public class ConsumerBackgroundService : BackgroundService
    {
        private readonly Interfaces.IConsumer _consumerService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ConsumerBackgroundService> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private readonly string _topic;

        public ConsumerBackgroundService(Interfaces.IConsumer consumerService, IConfiguration configuration, ILogger<ConsumerBackgroundService> logger)
        {
            _consumerService = consumerService; 
            _configuration = configuration;
            _logger = logger;
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:Server"],
                GroupId = _configuration["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _topic = _configuration["Kafka:Topic"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();

                consumer.Subscribe(_topic);

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {

                        await Task.Run(() => { 
                            var cr = consumer.Consume(stoppingToken);

                            var dados = JsonConvert.DeserializeObject<DroneDados>(cr.Message.Value);

                            if (dados is not null)
                            {
                                _consumerService.ProcessarDados(dados);
                            }
                            else
                            {
                                _logger.LogError($"Erro ao consumir dados. Payload invalido: {cr.Message.Value}");
                            }
                        }, stoppingToken);
                        
                    }
                }
                catch (OperationCanceledException ex)
                {
                    consumer.Close();
                    _logger.LogWarning(ex, "Cancelada a execução do Consumer.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao consumir dados.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao iniciar a execução do Consumer.");
            }
        }
    }
}
