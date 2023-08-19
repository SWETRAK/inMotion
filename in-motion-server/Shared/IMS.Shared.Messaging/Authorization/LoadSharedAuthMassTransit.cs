using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Shared.Messaging.Authorization;

public static class LoadSharedAuthMassTransit
{
    public static IServiceCollection AddSharedAuth(this IServiceCollection serviceCollection, RabbitMqConfiguration rabbitMqConfiguration)
    {
        serviceCollection.AddScoped<SharedAuthMiddleware>();
        serviceCollection.AddMassTransit<ISharedAuthBus>(x =>
        {
            x.AddRequestClient<ImsBaseMessage<RequestJwtValidationMessage>>(new Uri($"exchange:{EventsBusNames.ValidateJwtEventName}"));
            
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
        
        return serviceCollection;
    }

    public static void UseSharedAuth(this WebApplication app)
    {
        app.UseMiddleware<SharedAuthMiddleware>();
    }
}