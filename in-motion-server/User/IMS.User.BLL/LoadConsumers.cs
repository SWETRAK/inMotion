using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Authorization;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.User.BLL.Consumers;
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
            x.AddConsumer<GetUserInfoConsumer>().Endpoint(e =>
            {
                e.Name = EventsBusNames.GetUserInfoName;
            });
            
            x.AddConsumer<GetUsersInfoConsumer>().Endpoint(e =>
            {
                e.Name = EventsBusNames.GetUsersInfoName;
            });
            
            x.AddRequestClient<ImsBaseMessage<GetBaseUserInfoMessage>>(new Uri($"exchange:{EventsBusNames.GetBaseUserInfoName}"));
            x.AddRequestClient<ImsBaseMessage<GetBaseUsersInfoMessage>>(new Uri($"exchange:{EventsBusNames.GetBaseUsersInfoName}"));
            x.AddRequestClient<ImsBaseMessage<GetBaseUserInfoByNicknameMessage>>(new Uri($"exchange:{EventsBusNames.GetBaseUserInfoByNicknameName}"), RequestTimeout.After(s: 30));
            
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