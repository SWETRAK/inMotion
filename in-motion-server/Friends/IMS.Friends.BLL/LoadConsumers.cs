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

        serviceCollection.AddSharedAuthentication(rabbitMqConfiguration);
        
        serviceCollection.AddMassTransit(x =>
        {
            x.AddRequestClient<ImsBaseMessage<GetUserInfoMessage>>(new Uri($"exchange:{EventsBusNames.GetUserInfoName}"), RequestTimeout.After(s: 30));
            x.AddRequestClient<ImsBaseMessage<GetUsersInfoMessage>>(new Uri($"exchange:{EventsBusNames.GetUsersInfoName}"), RequestTimeout.After(s: 30));
            
            x.AddConsumer<CheckFriendshipStatusConsumer>().Endpoint(e =>
            {
                e.Name = EventsBusNames.CheckFriendshipStatusName;
            });
            
            x.AddConsumer<GetUserFriendsConsumer>().Endpoint(e =>
            {
                e.Name = EventsBusNames.GetUserFriendsName;
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