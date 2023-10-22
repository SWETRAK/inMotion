namespace IMS.Shared.Messaging.Models;

public class QueueConfiguration
{
    public QueueConfiguration(string queueName, string exchangeName, string routingKey)
    {
        QueueName = queueName;
        ExchangeName = exchangeName;
        RoutingKey = routingKey;
    }
    
    public string QueueName { get; private set; }
    public string ExchangeName { get; private set; }
    public string RoutingKey { get; private set; }
}