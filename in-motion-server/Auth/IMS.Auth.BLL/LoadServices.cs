using IMS.Auth.BLL.RabbitConsumers;
using IMS.Auth.BLL.Services;
using IMS.Auth.Domain.Entities;
using IMS.Auth.IBLL.Services;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Interfaces;
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

        serviceCollection.AddScoped<IUserService, UserService>();

        serviceCollection.AddScoped<IConsumer, ValidateJwtTokenRabbitConsumer>();
        serviceCollection.AddCustomRabbit();
        
        serviceCollection.AddAuthorization((options) =>
        {
        });
        return serviceCollection;
    }
}