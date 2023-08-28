using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Authorization;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.User.BLL;

public static class LoadConsumers
{
    public static IServiceCollection AddUserMassTransit(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var rabbitMqConfiguration = new RabbitMqConfiguration();
        configuration.GetSection("EventBus").Bind(rabbitMqConfiguration);

        serviceCollection.AddSharedAuthentication(rabbitMqConfiguration);
        
        serviceCollection.AddMassTransit(x =>
        {
            // TODO: Add this endpoints to user service
            // x.AddRequestClient<ImsBaseMessage<GetUserInfoMessage>>(new Uri($"exchange:{EventsBusNames.GetUserInfoName}"));
            // x.AddRequestClient<ImsBaseMessage<GetUsersInfoMessage>>(new Uri($"exchange:{EventsBusNames.GetUsersInfoName}"));
            
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(rabbitMqConfiguration.Host, rabbitMqConfiguration.Port, "/", h =>
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