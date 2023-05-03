using FluentValidation;
using IMS.Shared.Models.Dto.Post.Comment.Reaction;

namespace IMS.Shared.Models.Validators.Post.Comment.Reaction;

public class CreatePostCommentReactionDtoValidator: AbstractValidator<CreatePostCommentReactionDto>
{
    public CreatePostCommentReactionDtoValidator()
    {
        RuleFor(x => x.CommentId)
            .NotEmpty();

        RuleFor(x => x.Emoji)
            .NotEmpty();
    }
}