using IMS.Shared.Domain.Entities.Bases;

namespace IMS.Shared.Domain.Entities.Post;

public class PostCommentReaction: BaseReaction
{
    public virtual PostComment PostComment { get; set; }
    public Guid PostCommentId { get; set; }

    public PostCommentReaction()
    {
        CreationDate = DateTime.UtcNow;
        LastModificationDate = DateTime.UtcNow;
    }
}