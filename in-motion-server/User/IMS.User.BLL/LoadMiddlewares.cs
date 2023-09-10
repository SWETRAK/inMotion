using IMS.User.BLL.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.User.BLL;

public static class LoadMiddlewares
{
    public static IServiceCollection AddUserMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandlerMiddleware>();

        return services;
    }
    
    public static void UseUserMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}