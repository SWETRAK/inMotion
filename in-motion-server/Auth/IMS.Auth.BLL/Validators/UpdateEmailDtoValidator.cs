using FluentValidation;
using IMS.Auth.Models.Dto.Incoming;

namespace IMS.Auth.BLL.Validators;

public class UpdateEmailDtoValidator: AbstractValidator<UpdateEmailDto>
{
    public UpdateEmailDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}