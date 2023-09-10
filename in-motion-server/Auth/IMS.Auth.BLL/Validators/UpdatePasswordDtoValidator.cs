using FluentValidation;
using IMS.Auth.Models.Dto.Incoming;

namespace IMS.Auth.BLL.Validators;

public class UpdatePasswordDtoValidator: AbstractValidator<UpdatePasswordDto>
{
    public UpdatePasswordDtoValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty();

        RuleFor(x => x.RepeatPassword)
            .NotEmpty()
            .Equal(x => x.NewPassword);
    }
}