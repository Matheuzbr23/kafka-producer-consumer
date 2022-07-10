using Kafka_POC.Interfaces;

namespace Kafka_POC.Jobs
{
    public class ProducerBackgroundService : BackgroundService
    {
        private readonly ILogger<ProducerBackgroundService> _logger;
        private readonly IProducer _producerService;
        private readonly int _quantityMock;
        private readonly int _secondsBetweenExecution;
        private readonly bool _producerIsActive;

        public ProducerBackgroundService(ILogger<ProducerBackgroundService> logger, IProducer producerService, IConfiguration configuration)
        {
            _logger = logger;
            _producerService = producerService;

            if (!int.TryParse(configuration["QuantityMock"], out _quantityMock))
                _quantityMock = 10;

            if (!int.TryParse(configuration["SecondsBetweenExecution"], out _secondsBetweenExecution))
                _secondsBetweenExecution = 10000;

            if (!bool.TryParse(configuration["AutoProducerIsActive"], out _producerIsActive))
                _producerIsActive = false;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_producerIsActive)
                {
                    _logger.LogInformation($"PRODUCER: Enviando {_quantityMock} mensagens de dados coletados.");

                    await _producerService.InsertMany(_quantityMock);

                    await Task.Delay(_secondsBetweenExecution*1000, stoppingToken);
                }
            }
        }
    }
}