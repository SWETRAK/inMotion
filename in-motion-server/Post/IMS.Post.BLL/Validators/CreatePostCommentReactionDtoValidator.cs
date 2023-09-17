using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class CreatePostCommentReactionDtoValidator: AbstractValidator<CreatePostCommentReactionDto>
{
    public CreatePostCommentReactionDtoValidator()
    {
        RuleFor(x => x.PostCommentId).NotEmpty();
        RuleFor(x => x.Emoji).NotEmpty();
    }
}