namespace IMS.Shared.Messaging.Messages.JWT;

public class GetBaseUsersInfoResponseMessage
{
    public IEnumerable<GetBaseUserInfoResponseMessage> UsersInfo { get; set; }
}