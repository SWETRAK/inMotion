namespace IMS.Post.Models.Dto.Outgoing;

public class PostCommentDto
{
    public string Id { get; set; }
    public PostAuthorDto Author { get; set; }
    public string Content { get; set; }
    public string PostId { get; set; }
    public DateTime CreatedAt { get; set; }
    
}