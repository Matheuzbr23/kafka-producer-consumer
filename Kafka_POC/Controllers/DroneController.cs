using GeoCoordinatePortable;
using Kafka_POC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kafka_POC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DroneController : ControllerBase
    {
        private readonly ILogger<DroneController> _logger;

        public DroneController(ILogger<DroneController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult InserirDado(DroneDados droneDados)
        {
            try
            {
                _logger.LogInformation($"Dados do drone {droneDados.Drone_Id} inserido com sucesso!");

                GeoCoordinate coordinate = new GeoCoordinate(droneDados.Latitude, droneDados.Longitude);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir dados do Drone");

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
                    _logger.LogInformation($"Dados do drone {droneDados.Drone_Id} inserido com sucesso!");

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