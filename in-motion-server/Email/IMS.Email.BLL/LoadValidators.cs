using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Email.BLL;

public static class LoadValidators
{
    public static IServiceCollection AddEmailValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidationAutoValidation();
        
        return serviceCollection;
    }
}