using IMS.Email.BLL.Configurations;
using IMS.Email.BLL.Services;
using IMS.Email.IBLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Email.BLL;

public static class LoadServices
{
    public static IServiceCollection AddEmailServices(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        
        var emailConfiguration = new EmailConfiguration();
        configuration.GetSection("EventBus").Bind(emailConfiguration);

        serviceCollection.AddSingleton<EmailConfiguration>(emailConfiguration);
        
        serviceCollection.AddScoped<IEmailSenderService, EmailSenderService>();
        
        return serviceCollection;
    }
}