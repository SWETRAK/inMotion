namespace IMS.Post.Models.Dto.Outgoing;

public class PostReactionWithoutAuthorDto
{
    public string Id { get; set; }
    
    public string AuthorId { get; set; }
    
    public string Emoji { get; set; }
    
    public DateTime CreatedAt { get; set; }
}