using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IMS.Shared.Messaging.Consumers;

public abstract class SimpleConsumerWithResponse<TIn, TOut>: ConsumerBase
{
    private readonly ILogger _logger;

    protected SimpleConsumerWithResponse(ILogger logger)
    {
        _logger = logger;
    }
    
    protected abstract Task<TOut> ExecuteTask(TIn message);

    protected override void ExecuteInternal(IModel channel)
    {

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (ch, ea) =>
        {
            try
            {
                var byteArray = ea.Body.ToArray();
                var jsonMessageString = Encoding.UTF8.GetString(byteArray);
                var message = JsonConvert.DeserializeObject<TIn>(jsonMessageString);

                var responseMessage = await ExecuteTask(message);
                var responseJsonMessageString = JsonConvert.SerializeObject(responseMessage);
                var response = Encoding.UTF8.GetBytes(responseJsonMessageString);

                var props = ea.BasicProperties;

                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: response);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Wrong");
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