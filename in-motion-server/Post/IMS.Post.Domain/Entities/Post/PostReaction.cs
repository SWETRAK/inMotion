namespace IMS.Post.Domain.Entities.Post;

public class PostReaction
{
    public Guid Id { get; set; }

    public Guid ExternalAuthorId { get; set; }

    public string Emoji { get; set; }
    
    public virtual Post Post { get; set; }
    public Guid PostId { get; set; }
    
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastModificationDate { get; set; } = DateTime.UtcNow;
}