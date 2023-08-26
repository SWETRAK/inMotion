namespace IMS.Shared.Messaging.Messages.Friendship;

public class FriendshipStatusResponseMessage
{
    public string FriendshipStatus { get; set; }

    public string FirstUserId { get; set; }
    public string SecondUserId { get; set; }
}