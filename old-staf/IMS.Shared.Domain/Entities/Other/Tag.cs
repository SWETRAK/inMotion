namespace IMS.Shared.Domain.Entities.Other;

public class Tag
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }
    public virtual User.User Author { get; set; }

    public string Name { get; set; }

    public DateTime CreationDate { get; set; }

    public Tag()
    {
        CreationDate = DateTime.UtcNow;
    }
}