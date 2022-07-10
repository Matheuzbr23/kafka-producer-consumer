using System.ComponentModel.DataAnnotations;

namespace Kafka_POC.Models
{
    public class DroneDados
    {
        public int DroneId { get; set; }

        [Range(-90,90)]
        public double Latitude { get; set; }

        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Range(-25,40)]
        public int Temperatura { get; set; }

        [Range(0,100)]
        public int Umidade { get; set; }
        public bool RastreioAtivo {get;set;}
    }
}