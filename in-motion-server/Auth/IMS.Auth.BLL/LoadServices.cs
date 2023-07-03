using IMS.Auth.BLL.Services;
using IMS.Auth.IBLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.BLL;

public static class LoadServices
{
    public static IServiceCollection AddAuthServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEmailAuthService, EmailAuthService>();
        serviceCollection.AddScoped<IJwtService, JwtService>();
        return serviceCollection;
    }
}