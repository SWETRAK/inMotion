using IMS.Shared.Models.Dto.User;

namespace IMS.Shared.Models.Dto.Post.Comment.Reaction;

public class PostCommentReactionDto
{
    public string Id { get; set; }

    public AuthorInfoDto Author { get; set; }
    public DateTime CreationDate { get; set; }
    
    public string Emoji { get; set; }
    public string PostId { get; set; }
}