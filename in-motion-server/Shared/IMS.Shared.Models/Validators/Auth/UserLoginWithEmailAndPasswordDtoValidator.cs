using FluentValidation;
using IMS.Shared.Models.Dto.Auth;

namespace IMS.Shared.Models.Validators.Auth;

public class UserLoginWithEmailAndPasswordDtoValidator: AbstractValidator<UserLoginWithEmailAndPasswordDto>
{
    public UserLoginWithEmailAndPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}