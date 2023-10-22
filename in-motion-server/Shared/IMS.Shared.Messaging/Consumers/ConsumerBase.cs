using IMS.Shared.Messaging.Interfaces;
using IMS.Shared.Messaging.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IMS.Shared.Messaging.Consumers;

public abstract class ConsumerBase : IConsumer
{
    public QueueConfiguration QueueConfiguration { get; private set; }

    protected virtual QueueConfiguration ConfigureQueue()
    {
        return new QueueConfiguration(string.Empty, string.Empty, string.Empty);
    }

    protected abstract void ExecuteInternal(IModel channel); 
    
    public void ExecuteBase(IModel channel)
    {
        InitQueueAndExchange(channel);
        ExecuteInternal(channel);
    }
    
    private void InitQueueAndExchange(IModel channel)
    {
        this.QueueConfiguration = ConfigureQueue();
        channel.QueueDeclare(this.QueueConfiguration.QueueName, false, false, false, null); 
        
        channel.ExchangeDeclare(this.QueueConfiguration.ExchangeName, ExchangeType.Topic, false, false);  
        channel.QueueBind(this.QueueConfiguration.QueueName, this.QueueConfiguration.ExchangeName, this.QueueConfiguration.RoutingKey, null);  
    }

    protected virtual Task OnConsumerCancelled(object sender, ConsumerEventArgs e)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnConsumerUnregistered(object sender, ConsumerEventArgs e)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnConsumerRegistered(object sender, ConsumerEventArgs e)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnConsumerShutdown(object sender, ShutdownEventArgs e)
    {
        return Task.CompletedTask;
    }
}