namespace IMS.Shared.Messaging.Messages.JWT;

public class GetBaseUsersInfoMessage
{
    public IEnumerable<string> UsersId { get; set; }
}