namespace IMS.Shared.Domain.Entities.Bases;

public class BaseReaction
{
    public Guid Id { get; set; }

    public virtual User.User Author { get; set; }
    public Guid AuthorId { get; set; }

    public string Emoji { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime LastModificationDate { get; set; }
}