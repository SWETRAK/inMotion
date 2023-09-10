using FluentValidation;
using IMS.Auth.Models.Dto.Incoming;

namespace IMS.Auth.BLL.Validators;

public class AuthenticateWithFacebookProviderDtoValidator: AbstractValidator<AuthenticateWithFacebookProviderDto>
{
    public AuthenticateWithFacebookProviderDtoValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}