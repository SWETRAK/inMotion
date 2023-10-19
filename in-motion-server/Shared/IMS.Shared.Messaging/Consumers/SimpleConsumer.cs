using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IMS.Shared.Messaging.Consumers;

public abstract class SimpleConsumer<TIn>: ConsumerBase
{
    private readonly ILogger _logger;

    protected SimpleConsumer(ILogger logger)
    {
        _logger = logger;
    }
    
    protected abstract Task ExecuteTask(TIn message);

    protected sealed override void ExecuteInternal(IModel channel)
    {
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (ch, ea) =>
        {
            try
            {
                var byteArray = ea.Body.ToArray();
                var jsonMessageString = Encoding.UTF8.GetString(byteArray);
                var message = JsonConvert.DeserializeObject<TIn>(jsonMessageString);

                await ExecuteTask(message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error");
            }
            finally
            {
                channel.BasicAck(ea.DeliveryTag, false);
            }
        };

        consumer.Shutdown += OnConsumerShutdown;
        consumer.Registered += OnConsumerRegistered;
        consumer.Unregistered += OnConsumerUnregistered;
        consumer.ConsumerCancelled += OnConsumerCancelled;

        channel.BasicConsume(QueueConfiguration.QueueName, false, consumer);
    }
}