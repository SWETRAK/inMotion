using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class EditPostReactionDtoValidator: AbstractValidator<EditPostReactionDto>
{
    public EditPostReactionDtoValidator()
    {
        RuleFor(x => x.PostId).NotEmpty();
        RuleFor(x => x.Emoji).NotEmpty();
    }
}