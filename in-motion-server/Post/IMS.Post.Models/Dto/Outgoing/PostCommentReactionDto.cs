namespace IMS.Post.Models.Dto.Outgoing;

public class PostCommentReactionDto
{
    public string Id { get; set; }
    public PostAuthorDto Author { get; set; }

    public string Emoji { get; set; }
    
    public string PostCommentId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}