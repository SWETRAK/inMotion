using IMS.Shared.Domain.Entities.Bases;

namespace IMS.Shared.Domain.Entities.Post;

public class PostReaction: BaseReaction
{
    public virtual Post Post { get; set; }
    public Guid PostId { get; set; }
}