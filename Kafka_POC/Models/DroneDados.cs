
using GeoCoordinatePortable;

namespace Kafka_POC.Models
{
    public class DroneDados
    {
        public int Drone_Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }   
        public int Temperatura { get; set; }
        public int Umidade { get; set; }
        public bool RastreioAtivo {get;set;}
    }
}
