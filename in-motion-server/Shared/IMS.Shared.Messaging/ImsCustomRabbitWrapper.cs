using IMS.Shared.Messaging.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Shared.Messaging;

public static class ImsCustomRabbitWrapper
{
    public static void AddCustomRabbit(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService<ImsRabbitService>();
    }
}