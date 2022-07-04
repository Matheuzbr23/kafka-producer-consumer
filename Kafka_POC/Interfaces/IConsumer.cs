using Kafka_POC.Models;

namespace Kafka_POC.Interfaces
{
    public interface IConsumer
    {
        public Task ProcessarDados(DroneDados dados);
    }
}
