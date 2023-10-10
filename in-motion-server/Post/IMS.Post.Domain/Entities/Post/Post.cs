using IMS.Post.Domain.Consts;
using IMS.Post.Domain.Entities.Other;

namespace IMS.Post.Domain.Entities.Post;

public class Post
{
    public Guid Id { get; set; }
    
    public Guid ExternalAuthorId { get; set; }
    
    public Guid IterationId { get; set; }
    public virtual PostIteration Iteration { get; set; }

    public PostVisibility Visibility { get; set; }
    
    public string Description { get; set; }
    public string Title { get; set; }
    
    public virtual IEnumerable<Tag> Tags { get; set; }

    public Guid LocalizationId { get; set; }
    public virtual Localization Localization { get; set; }
    
    public virtual IEnumerable<PostVideo> Videos { get; set; }

    public virtual IEnumerable<PostComment> PostComments { get; set; }
    public virtual IEnumerable<PostReaction> PostReactions { get; set; }
    
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
}