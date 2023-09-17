using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class EditPostRequestDtoValidator: AbstractValidator<EditPostRequestDto>
{
    public EditPostRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(256)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(2048)
            .NotEmpty();
    }
}