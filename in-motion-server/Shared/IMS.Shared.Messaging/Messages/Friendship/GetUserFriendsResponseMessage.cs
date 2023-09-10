namespace IMS.Shared.Messaging.Messages.Friendship;

public class GetUserFriendsResponseMessage
{
    public IEnumerable<string> FriendsIds { get; set; }
}