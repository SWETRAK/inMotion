namespace IMS.Shared.Messaging.Messages.Users;

public class GetUsersInfoMessage
{
    public IEnumerable<string> UserIds { get; set; }
}