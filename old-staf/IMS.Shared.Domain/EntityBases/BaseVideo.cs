namespace IMS.Shared.Domain.Entities.Bases;

public class BaseVideo
{
    public Guid Id { get; set; }

    public Guid AuthrorId { get; set; }
    public virtual User.User Author { get; set; }
    
    public string Filename { get; set; }
    public string BucketLocation { get; set; }
    public string BucketName { get; set; }
    public string ContentType { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastEditionDate { get; set; }
}