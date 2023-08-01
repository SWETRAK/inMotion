using FluentValidation;
using IMS.Auth.Models.Dto.Incoming;

namespace IMS.Auth.BLL.Validators;

public class UpdateNicknameDtoValidator: AbstractValidator<UpdateNicknameDto>
{
    public UpdateNicknameDtoValidator()
    {
        RuleFor(x => x.Nickname)
            .NotEmpty();
    }
}