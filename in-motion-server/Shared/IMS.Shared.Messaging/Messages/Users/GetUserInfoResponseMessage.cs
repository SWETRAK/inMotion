using IMS.Shared.Messaging.Messages.Users.Nested;

namespace IMS.Shared.Messaging.Messages.Users;

public class GetUserInfoResponseMessage
{
    public string Id { get; set; }
    public string Nickname { get; set; }

    public string Bio { get; set; }
    public UserProfileVideoVM UserProfileVideo { get; set; }
}