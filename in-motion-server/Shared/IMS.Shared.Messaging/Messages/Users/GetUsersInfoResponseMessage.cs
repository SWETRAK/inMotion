namespace IMS.Shared.Messaging.Messages.Users;

public class GetUsersInfoResponseMessage
{
    public IEnumerable<GetUserInfoResponseMessage> UsersInfo { get; set; }
}