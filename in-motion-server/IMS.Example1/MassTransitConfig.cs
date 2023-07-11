using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using MassTransit;

namespace IMS.Example1;

public static class MassTransitConfig
{
    public static IServiceCollection AddMassTransitServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMassTransit(x =>
        {
            // If event should return something you need add AddRequestClient to sending object type 
            // x.AddConsumer<OrderConsumer>().Endpoint(e =>
            // {
            //     e.Name = "order-service";
            // });
            // x.AddRequestClient<OrderDto>();
            
            
            //Add this line to have access to rabbitmq auth event queues 
            x.AddRequestClient<ImsBaseMessage<RequestJwtValidationMessage>>(new Uri($"exchange:{EventsBusNames.ValidateJwtEventName}"));

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.UseMessageRetry(r => r.Immediate(1));
                cfg.ConfigureEndpoints(context);
            });
        });
        return serviceCollection;
    }
}