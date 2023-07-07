using FluentValidation;
using IMS.Auth.Models.Dto.Incoming;

namespace IMS.Auth.BLL.Validators;

public class LoginUserWithEmailAndPasswordDtoValidator: AbstractValidator<LoginUserWithEmailAndPasswordDto>
{
    public LoginUserWithEmailAndPasswordDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(dto => dto.Password)
            .NotNull();
    }
}