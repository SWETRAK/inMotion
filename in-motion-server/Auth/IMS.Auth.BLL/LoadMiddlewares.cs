using IMS.Auth.BLL.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.BLL;

public static class LoadMiddlewares
{
    public static IServiceCollection AddAuthMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandlerMiddleware>();

        return services;
    }
    
    public static void UseAuthMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}