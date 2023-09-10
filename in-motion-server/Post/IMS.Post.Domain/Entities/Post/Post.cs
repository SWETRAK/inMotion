using IMS.Post.Domain.Consts;
using IMS.Post.Domain.Entities.Other;

namespace IMS.Post.Domain.Entities.Post;

public sealed class Post
{
    public Guid Id { get; set; }
    
    public Guid ExternalAuthorId { get; set; }

    public PostVisibility Visibility { get; set; }
    
    public string Description { get; set; }
    public string Title { get; set; }
    
    public IEnumerable<Tag> Tags { get; set; }

    public Guid LocalizationId { get; set; }
    public Localization Localization { get; set; }

    public Guid FrontVideoId { get; set; }
    public PostVideo FrontVideo { get; set; }

    public Guid RearVideoId { get; set; }
    public PostVideo RearVideo {get; set; }

    public IEnumerable<PostComment> PostComments { get; set; }
    public IEnumerable<PostReaction> PostReactions { get; set; }
    
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
}