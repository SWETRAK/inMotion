using IMS.Friends.Domain.Consts;

namespace IMS.Friends.Domain.Entities;

public class Friendship
{
    public Guid Id { get; set; }
    
    public Guid FirstUserId { get; set; }
    
    public Guid SecondUserId { get; set; }

    public FriendshipStatus Status { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastModificationDate { get; set; } = DateTime.UtcNow;
}