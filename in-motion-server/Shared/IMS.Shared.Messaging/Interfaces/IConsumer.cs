using IMS.Shared.Messaging.Models;
using RabbitMQ.Client;

namespace IMS.Shared.Messaging.Interfaces;

public interface IConsumer
{
    protected QueueConfiguration QueueConfiguration { get; }

    void ExecuteBase(IModel channel);
}