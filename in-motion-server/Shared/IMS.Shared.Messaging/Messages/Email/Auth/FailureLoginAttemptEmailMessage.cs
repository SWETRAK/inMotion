namespace IMS.Shared.Messaging.Messages.Email.Auth;

public class FailureLoginAttemptEmailMessage
{
    public string Email { get; set; }
    public DateTime DateTime { get; set; }
}