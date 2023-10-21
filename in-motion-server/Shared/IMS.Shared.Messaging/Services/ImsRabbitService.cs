using IMS.Shared.Messaging.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace IMS.Shared.Messaging.Services;

public class ImsRabbitService: BackgroundService
{
    private readonly ILogger<ImsRabbitService> _logger;
    private readonly IEnumerable<IConsumer> _consumers;
    private readonly RabbitMqConfiguration _mqConfiguration;
    private IConnection _connection;
    private IModel _channel;
    
    public ImsRabbitService(
        ILogger<ImsRabbitService> logger, 
        IServiceScopeFactory serviceScopeFactory, 
        RabbitMqConfiguration mqConfiguration)
    {
        _logger = logger;
        _mqConfiguration = mqConfiguration;
        using var scope = serviceScopeFactory.CreateScope();
        _consumers = scope.ServiceProvider.GetRequiredService<IEnumerable<IConsumer>>();
        InitRabbitMq();
    }

    private void InitRabbitMq()
    {
        var factory = new ConnectionFactory
        {
            HostName = _mqConfiguration.Host, 
            UserName = _mqConfiguration.Username, 
            Password = _mqConfiguration.Password,
            Port = _mqConfiguration.Port
        };

        factory.DispatchConsumersAsync = true;
        
        _connection = factory.CreateConnection();  
        _channel = _connection.CreateModel();
        
        _channel.BasicQos(0, 1, false);  
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;  
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        foreach (var consumer in _consumers)
        {
            consumer.ExecuteBase(_channel);
        }
        
        return Task.CompletedTask;  
    }
    
    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)  {  }  
  
    public override void Dispose()  
    {  
        _channel.Close();  
        _connection.Close();  
        GC.SuppressFinalize(this);
        base.Dispose();  
    }  
}