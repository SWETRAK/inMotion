namespace IMS.Shared.Messaging.Messages.JWT;

public class ValidatedUserInfoMessage
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string Role { get; set; }
}