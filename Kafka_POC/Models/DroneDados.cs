using System.ComponentModel.DataAnnotations;

namespace Kafka_POC.Models
{
    public class DroneDados
    {
        public int DroneId { get; set; }

        [MaxLength(90)]
        [MinLength(-90)]
        public double Latitude { get; set; }

        [MaxLength(180)]
        [MinLength(-180)]
        public double Longitude { get; set; }

        [MaxLength(40)]
        [MinLength(-25)]
        public int Temperatura { get; set; }

        [MaxLength(100)]
        [MinLength(0)]
        public int Umidade { get; set; }
        public bool RastreioAtivo {get;set;}
    }
}