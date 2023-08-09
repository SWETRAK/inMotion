using FluentValidation;
using IMS.Auth.Models.Dto.Incoming;

namespace IMS.Auth.BLL.Validators;

public class AddPasswordDtoValidator: AbstractValidator<AddPasswordDto>
{
    public AddPasswordDtoValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotNull();

        RuleFor(x => x.RepeatPassword)
            .NotNull()
            .Equal(x => x.NewPassword);
    }
}