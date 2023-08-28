namespace IMS.User.Domain.Entities;

public class UserProfileVideo
{ 
    public Guid Id { get; set; }

    public Guid AuthorExternalId { get; set; }

    public Guid UserMetasId { get; set; }
    public virtual UserMetas UserMetas { get; set; }
    public string Filename { get; set; }
    public string BucketLocation { get; set; }
    public string BucketName { get; set; }
    public string ContentType { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastEditionDate { get; set; } = DateTime.UtcNow;
}