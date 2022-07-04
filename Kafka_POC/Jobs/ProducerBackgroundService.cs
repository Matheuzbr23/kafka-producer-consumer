using Kafka_POC.Interfaces;

namespace Kafka_POC.Jobs
{
    public class ProducerBackgroundService : BackgroundService
    {
        private readonly ILogger<ProducerBackgroundService> _logger;
        private readonly IProducer _producerService;
        private readonly int _qtdDadosMock;
        private readonly int _secondsBetweenExecution;
        private readonly bool _producerIsActive;

        public ProducerBackgroundService(ILogger<ProducerBackgroundService> logger, IProducer producerService, IConfiguration configuration)
        {
            _logger = logger;
            _producerService = producerService;

            if (!int.TryParse(configuration["QtdDadosMock"], out _qtdDadosMock))
                _qtdDadosMock = 10;

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
                    _logger.LogInformation($"Producer: Enviando {_qtdDadosMock} mensagens de dados coletados.");

                    await _producerService.InsertMany(_qtdDadosMock);

                    await Task.Delay(_secondsBetweenExecution, stoppingToken);
                }
            }
        }
    }
}