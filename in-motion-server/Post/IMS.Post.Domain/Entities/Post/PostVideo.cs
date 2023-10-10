using IMS.Post.Domain.Consts;

namespace IMS.Post.Domain.Entities.Post;

public class PostVideo
{
    public Guid Id { get; set; }

    public Guid ExternalAuthorId { get; set; }
    
    public string Filename { get; set; }
    public string BucketLocation { get; set; }
    public string BucketName { get; set; }
    public string ContentType { get; set; }
    public PostVideoType Type { get; set; }

    public virtual Post Post { get; set; }
    public Guid PostId { get; set; }
    
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastEditionDate { get; set; } = DateTime.UtcNow;
}