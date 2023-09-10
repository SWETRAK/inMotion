using IMS.Post.BLL.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Post.BLL;

public static class LoadMiddlewares
{
    public static IServiceCollection AddPostMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandlerMiddleware>();

        return services;
    }
    
    public static void UsePostMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}