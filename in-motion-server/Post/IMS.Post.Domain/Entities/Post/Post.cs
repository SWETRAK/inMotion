using IMS.Post.Domain.Entities.Other;

namespace IMS.Post.Domain.Entities.Post;

public class Post
{
    public Guid Id { get; set; }
    
    public Guid ExternalAuthorId { get; set; }

    public string Description { get; set; }
    public string Title { get; set; }
    
    public virtual IEnumerable<Tag> Tags { get; set; }

    public Guid LocalizationId { get; set; }
    public virtual Localization Localization { get; set; }

    public Guid FrontVideoId { get; set; }
    public virtual PostVideo FrontVideo { get; set; }

    public Guid RearVideoId { get; set; }
    public virtual PostVideo RearVideo {get; set; }

    public virtual IEnumerable<PostComment> PostComments { get; set; }
    public virtual IEnumerable<PostReaction> PostReactions { get; set; }
    
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
}