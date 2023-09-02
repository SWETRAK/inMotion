using IMS.User.BLL.SoapServices;
using IMS.User.IBLL.SoapServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace IMS.User.BLL;

public static class LoadSoapServices
{
    public static IServiceCollection AddUserSoapService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserSoapService, UserSoapService>();
        return serviceCollection;
    }

    public static void UseUserSoapService(this WebApplication webApplication)
    {
        webApplication.UseSoapEndpoint<IUserSoapService>("/soap/userService.asmx", new SoapEncoderOptions());
    }
}