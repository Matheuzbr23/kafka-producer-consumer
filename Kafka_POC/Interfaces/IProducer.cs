using Kafka_POC.Models;

namespace Kafka_POC.Interfaces
{
    public interface IProducer
    {
        Task InsertMany(int quantidade);
        Task Insert(DroneDados dados);
    }
}
