using IMS.Shared.Domain.Consts;

namespace IMS.Shared.Domain.Entities.Friendship;

public class Friendship
{
    public Guid Id { get; set; }

    public virtual User.User FirstUser { get; set; }
    public Guid FirstUserId { get; set; }

    public virtual User.User SecondUser { get; set; }
    public Guid SecondUserId { get; set; }

    public FriendshipStatus Status { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime LastModificationDate { get; set; }

    public Friendship()
    {
        CreationDate = DateTime.UtcNow;
        LastModificationDate = DateTime.UtcNow;
    }
}