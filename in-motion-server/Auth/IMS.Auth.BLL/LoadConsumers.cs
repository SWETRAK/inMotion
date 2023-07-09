using IMS.Auth.BLL.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.BLL;

public static class LoadConsumers
{
    public static IServiceCollection AddAuthMassTransit(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMassTransit(x =>
        {
            x.AddConsumer<ValidateJwtTokenConsumer>().Endpoint(e =>
            {
                e.Name = "validate-jwt-event";
            });

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.UseMessageRetry(r => r.Immediate(2));
                cfg.ConfigureEndpoints(ctx);
            });
        });
        return serviceCollection;
    }
}