using Kafka_POC.Interfaces;
using Kafka_POC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kafka_POC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DroneController : ControllerBase
    {
        private readonly ILogger<DroneController> _logger;
        private readonly IProducer _producer;

        public DroneController(ILogger<DroneController> logger, IProducer producer)
        {
            _producer = producer;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult InserirDado(DroneDados droneDados)
        {
            try
            {
                _logger.LogInformation($"Dados do drone {droneDados.DroneId} inserido com sucesso!");

                _producer.Insert(droneDados);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao inserir dados do Drone. {JsonConvert.SerializeObject(droneDados)}.");

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Lista")]
        public IActionResult InserirListaDados(List<DroneDados> dados)
        {
            try
            {
                foreach(DroneDados droneDados in dados)
                {
                    _producer.Insert(droneDados);

                    _logger.LogInformation($"Dados do drone {droneDados.DroneId} inserido com sucesso!");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir lista de dados do Drone");

                return BadRequest();
            }
        }
    }
}