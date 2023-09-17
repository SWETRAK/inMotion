using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class CreatePostReactionDtoValidator: AbstractValidator<CreatePostReactionDto>
{
    public CreatePostReactionDtoValidator()
    {
        RuleFor(x => x.PostId).NotEmpty();
        RuleFor(x => x.Emoji).NotEmpty();
    }
}