using IMS.Shared.Domain.Entities.Bases;

namespace IMS.Shared.Domain.Entities.Post;

//TODO: Add reactions
public class PostComment: CommentBase
{
    public virtual Post Post { get; set; }
    public Guid PostId { get; set; }
}