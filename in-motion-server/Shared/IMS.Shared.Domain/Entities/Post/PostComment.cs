using IMS.Shared.Domain.Entities.Bases;

namespace IMS.Shared.Domain.Entities.Post;


public class PostComment: BaseComment
{
    public virtual Post Post { get; set; }
    public Guid PostId { get; set; }

    public PostComment()
    {
        CreationDate = DateTime.UtcNow;
        LastModifiedDate = DateTime.UtcNow;
    }
}