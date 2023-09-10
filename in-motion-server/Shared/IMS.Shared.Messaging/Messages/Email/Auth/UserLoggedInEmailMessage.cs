namespace IMS.Shared.Messaging.Messages.Email.Auth;

public class UserLoggedInEmailMessage
{
    public string Email { get; set; }
    public DateTime LoggedDate { get; set; }
}