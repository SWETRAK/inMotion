using IMS.Auth.BLL.Consumers;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Email.Auth;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.BLL;

public static class LoadConsumers
{
    public static IServiceCollection AddAuthMassTransit(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var rabbitMqConfiguration = new RabbitMqConfiguration();
        
        configuration.GetSection("EventBus").Bind(rabbitMqConfiguration);
        
        serviceCollection.AddMassTransit(x =>
        {
            x.AddConsumer<ValidateJwtTokenConsumer>().Endpoint(e =>
            {
                e.Name = EventsBusNames.ValidateJwtEventName;
            });
            
            // Email service rabbitMq service
            x.AddRequestClient<ImsBaseMessage<ActivateAccountEmailMessage>>(new Uri($"exchange:{EventsBusNames.SendAccountActivationEmail}"));
            x.AddRequestClient<ImsBaseMessage<FailureLoginAttemptEmailMessage>>(new Uri($"exchange:{EventsBusNames.SendFailureLoggedInEmail}"));
            x.AddRequestClient<ImsBaseMessage<UserLoggedInEmailMessage>>(new Uri($"exchange:{EventsBusNames.SendUserLoggedInEmail}"));

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