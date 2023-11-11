namespace IMS.Shared.Messaging.Messages.JWT;

public class GetBaseUserInfoByNicknameResponseMessage
{
    public IEnumerable<GetBaseUserInfoResponseMessage> Users { get; set; }
}