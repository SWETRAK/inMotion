using FluentValidation;
using IMS.Shared.Models.Dto.Post.Comment;

namespace IMS.Shared.Models.Validators.Post.Comment;

public class CreatePostCommentDtoValidator: AbstractValidator<CreatePostCommentDto>
{
    public CreatePostCommentDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(x => x.PostId)
            .NotEmpty();
    }
}