namespace IMS.Post.Domain.Entities.Other;

public sealed class Tag
{
    public Guid Id { get; set; }

    public Guid ExternalAuthorId { get; set; }

    public string Name { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}