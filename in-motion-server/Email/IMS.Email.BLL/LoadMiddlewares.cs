using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Email.BLL;

public static class LoadMiddlewares
{
    public static IServiceCollection AddEmailMiddlewares(this IServiceCollection services)
    {

        return services;
    }
    
    public static void UseEmailMiddlewares(this WebApplication app)
    {
    }
}