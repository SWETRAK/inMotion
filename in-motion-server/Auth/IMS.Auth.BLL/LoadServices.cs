using IMS.Auth.BLL.Services;
using IMS.Auth.Domain.Entities;
using IMS.Auth.IBLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.BLL;

public static class LoadServices
{
    public static IServiceCollection AddAuthServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEmailAuthService, EmailAuthService>();
        serviceCollection.AddScoped<IGoogleAuthService, GoogleAuthService>();
        serviceCollection.AddScoped<IJwtService, JwtService>();
        serviceCollection.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        
        serviceCollection.AddAuthorization((options) =>
        {
            
        });
        return serviceCollection;
    }
}