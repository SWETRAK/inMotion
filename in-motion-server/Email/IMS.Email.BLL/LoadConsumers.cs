using IMS.Email.BLL.Consumers;
using IMS.Shared.Messaging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace IMS.Email.BLL;

public static class LoadConsumers
{
    public static IServiceCollection AddEmailMassTransit(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var rabbitMqConfiguration = new RabbitMqConfiguration();
        configuration.GetSection("EventBus").Bind(rabbitMqConfiguration);
        
        serviceCollection.AddMassTransit(x =>
        {
            x.AddConsumer<SendUserLoggedInEmailConsumer>().Endpoint(e =>
            {
                e.Name = EventsBusNames.SendUserLoggedInEmail;
            });
            
            x.AddConsumer<SendFailureLoginAttemptEmailConsumer>().Endpoint(e =>
            {
                e.Name = EventsBusNames.SendFailureLoggedInEmail;
            });

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(rabbitMqConfiguration.Host, (ushort)rabbitMqConfiguration.Port, "/", h =>
                {
                    h.Username(rabbitMqConfiguration.Username);
                    h.Password(rabbitMqConfiguration.Password);
                });

                cfg.UseMessageRetry(r => r.Immediate(2));
                cfg.ConfigureEndpoints(ctx);
            });
        });

        serviceCollection.AddSingleton<RabbitMqConfiguration>(rabbitMqConfiguration);
        return serviceCollection;
    }
}
