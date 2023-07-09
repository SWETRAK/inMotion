using FluentValidation;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Dto.Incoming;

namespace IMS.Auth.BLL.Validators;

public class RegisterUserWithEmailAndPasswordDtoValidator: AbstractValidator<RegisterUserWithEmailAndPasswordDto>
{
    public RegisterUserWithEmailAndPasswordDtoValidator(IUserRepository userRepository)
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom((value, context) =>
            {
                var user = userRepository.GetByEmail(value);
                if (user is not null) context.AddFailure("Email", "That email is taken");
            });

        RuleFor(dto => dto.Nickname)
            .NotEmpty();

        RuleFor(dto => dto.Password)
            .NotEmpty();

        RuleFor(dto => dto.RepeatPassword)
            .NotEmpty()
            .Equal(e => e.Password);


    }
}