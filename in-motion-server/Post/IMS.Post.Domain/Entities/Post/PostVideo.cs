using IMS.Postś.Domain.Consts;

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

    public virtual Post PostFront { get; set; }
    public Guid PostFrontId { get; set; }
    
    public virtual Post PostRear { get; set; }
    public Guid PostRearId { get; set; }
    
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastEditionDate { get; set; } = DateTime.UtcNow;
}