using IMS.Shared.Models.Dto.User;

namespace IMS.Shared.Models.Dto.Post.Comment;

public class PostCommentDto
{
    public string Id { get; set; }

    public AuthorInfoDto Author { get; set; }
    
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
}