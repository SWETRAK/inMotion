namespace IMS.Post.Models.Dto.Outgoing;

public class PostReactionDto
{
    public string Id { get; set; }
    public PostAuthorDto Author { get; set; }
    
    public string Emoji { get; set; }
    
    public string PostId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}