namespace IMS.Post.Domain.Entities.Post;

public class PostComment
{
    public Guid Id { get; set; }
    public Guid ExternalAuthorId { get; set; }

    public virtual string Content { get; set; }
    public Post Post { get; set; }
    public Guid PostId { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
}
