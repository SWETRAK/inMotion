namespace IMS.Post.Domain.Entities.Post;

public class PostCommentReaction
{
    public virtual PostComment PostComment { get; set; }
    public Guid PostCommentId { get; set; }

    public Guid Id { get; set; }
    
    public Guid ExternalAuthorId { get; set; }

    public string Emoji { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastModificationDate { get; set; } = DateTime.UtcNow;
}