using FluentValidation;
using IMS.Shared.Models.Dto.Auth;

namespace IMS.Shared.Models.Validators.Auth;

public class UserRegisterWithEmailAndPasswordDtoValidator: AbstractValidator<UserRegisterWithEmailAndPasswordDto>
{
    public UserRegisterWithEmailAndPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            // .Custom(CheckEmail)
            .NotEmpty();

        RuleFor(x => x.Nickname)
            // .Custom(CheckNickname)
            .NotEmpty();

        RuleFor(x => x.Password)
            .MinimumLength(8)
            .NotEmpty();

        RuleFor(x => x.RepeatPassword)
            .MinimumLength(8)
            .NotEmpty()
            .Equal(x => x.Password);
    }
    
    // TODO: Change this to synchronous data access
    // private async void CheckEmail(string value, ValidationContext<RegisterUserWithPasswordDto> context)
    // {
    //     var user = await _userRepository.GetByEmail(value.ToLower());
    //     if (user is not null) context.AddFailure("Email", "That email is taken");
    // }
    
    // TODO: Change this to synchronous data access
    // private async void CheckNickname(string value, ValidationContext<RegisterUserWithPasswordDto> context)
    // {
    //     var user = await _userRepository.GetByEmail(value.ToLower());
    //     if (user is not null) context.AddFailure("Email", "That email is taken");
    // }
}