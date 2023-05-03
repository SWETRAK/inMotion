using IMS.Shared.Models.Dto.Other;
using IMS.Shared.Models.Dto.Post.Comment;
using IMS.Shared.Models.Dto.Post.Reaction;
using IMS.Shared.Models.Dto.User;

namespace IMS.Shared.Models.Dto.Post;

public class PostDetailsDto
{
    public string Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }

    public AuthorInfoDto Author { get; set; }
    
    public DateTime CreationDate { get; set; }
    public LocalizationDto Localization { get; set; }

    public IEnumerable<TagDto> Tags { get; set; }
    public IEnumerable<PostCommentDto> Comments { get; set; }
    public IEnumerable<PostReactionDto> Reactions { get; set; }
}