namespace IMS.Shared.Domain.Entities.Friendship;

public class BlockedUser
{
    public Guid Id { get; set; }

    public virtual User.User FirstUser { get; set; }
    public Guid FirstUserId { get; set; }

    public virtual User.User SecondUser { get; set; }
    public Guid SecondUserId { get; set; }

    public DateTime CreationDate { get; set; }

    public BlockedUser()
    {
        CreationDate = DateTime.Now;
    }
}