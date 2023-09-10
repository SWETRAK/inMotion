using IMS.Friends.BLL.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Friends.BLL;

public static class LoadMiddlewares
{
    public static IServiceCollection AddFriendsMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandlerMiddleware>();

        return services;
    }
    
    public static void UseFriendsMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}