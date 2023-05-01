using IMS.Shared.Domain.Entities.Bases;

namespace IMS.Shared.Domain.Entities.User;

public class UserProfileVideoReaction: BaseReaction
{
    public Guid UserProfileVideoId { get; set; }
    public virtual UserProfileVideo UserProfileVideo { get; set; }
}