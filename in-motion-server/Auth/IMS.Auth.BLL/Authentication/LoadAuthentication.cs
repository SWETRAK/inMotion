using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Auth.BLL.Authentication;

public static class LoadAuthentication
{
    public static IServiceCollection AddAuthAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var authenticationConfiguration = new AuthenticationConfiguration();
        configuration.GetSection("Authentication").Bind(authenticationConfiguration);
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer(cfg =>
            {
                
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationConfiguration.JwtIssuer,
                    ValidAudience = authenticationConfiguration.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.JwtKey))
                };
                
                cfg.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                        {
                            context.Token = context.Request.Cookies["X-Access-Token"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddSingleton<AuthenticationConfiguration>(authenticationConfiguration);
        return services;
    }
}