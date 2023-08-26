using IMS.Friends.BLL.Consumers;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Authorization;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Users;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Friends.BLL;

public static class LoadConsumers
{
    public static IServiceCollection AddFriendsMassTransit(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var rabbitMqConfiguration = new RabbitMqConfiguration();
        
        configuration.GetSection("EventBus").Bind(rabbitMqConfiguration);

        serviceCollection.AddSharedAuth(rabbitMqConfiguration);
        
        serviceCollection.AddMassTransit(x =>
        {
            // TODO: Add this endpoints to user service
            x.AddRequestClient<ImsBaseMessage<GetUserInfoMessage>>(new Uri($"exchange:{EventsBusNames.GetUserInfoName}"));
            x.AddRequestClient<ImsBaseMessage<GetUsersInfoMessage>>(new Uri($"exchange:{EventsBusNames.GetUsersInfoName}"));
            
            x.AddConsumer<CheckFriendshipStatusConsumer>().Endpoint(e =>
            {
                e.Name = EventsBusNames.CheckFriendshipStatus;
            });
            
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