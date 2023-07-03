using IMS.Auth.BLL.Services;
using IMS.Auth.IBLL.Services;
using IMS.Shared.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.BLL;

public static class LoadServices
{
    public static IServiceCollection AddAuthServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEmailAuthService, EmailAuthService>();
        serviceCollection.AddScoped<IJwtService, JwtService>();
        serviceCollection.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        return serviceCollection;
    }
}