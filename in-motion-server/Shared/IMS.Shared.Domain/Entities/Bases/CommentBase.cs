namespace IMS.Shared.Domain.Entities.Bases;

public class CommentBase
{
    public Guid Id { get; set; }

    public virtual User.User Author { get; set; }
    public Guid AuthorId { get; set; }

    public string Content { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
}