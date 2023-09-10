using IMS.Auth.BLL.SoapServices;
using IMS.Auth.IBLL.SoapServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace IMS.Auth.BLL;

public static class LoadSoapServices
{
    public static IServiceCollection AddAuthSoapService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidateJwtTokenSoap, ValidateJwtTokenSoap>();
        return serviceCollection;
    }

    public static void UseAuthSoapService(this WebApplication webApplication)
    {
        webApplication.UseSoapEndpoint<IValidateJwtTokenSoap>("/soap/jwtService.asmx", new SoapEncoderOptions());
    }
}