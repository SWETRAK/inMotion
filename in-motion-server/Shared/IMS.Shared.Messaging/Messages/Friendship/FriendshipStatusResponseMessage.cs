namespace IMS.Shared.Messaging.Messages.Friendship;

public class FriendshipStatusResponseMessage
{
    public string FriendshipStatus { get; set; }

    public string FirstUserId { get; set; }
    public string LastUserId { get; set; }
}