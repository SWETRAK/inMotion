using IMS.Shared.Domain.Entities.Other;

namespace IMS.Shared.Domain.Entities.Post;

// TODO: Add reactions
public class Post
{
    public Guid Id { get; set; }
    
    public Guid AuthorId { get; set; }
    public virtual User.User Author { get; set; }

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
}