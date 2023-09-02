using FluentValidation;
using IMS.Auth.Models.Dto.Incoming;

namespace IMS.Auth.BLL.Validators;

public class AuthenticateWithGoogleProviderDtoValidator: AbstractValidator<AuthenticateWithGoogleProviderDto>
{
    public AuthenticateWithGoogleProviderDtoValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}