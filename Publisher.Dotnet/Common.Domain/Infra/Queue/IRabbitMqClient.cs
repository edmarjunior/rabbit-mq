using Common.Domain.Entities;

namespace Common.Domain.Infra.Queue
{
    public interface IRabbitMqClient
    {
        void Send(QueueParams queueParams);
    }
}
