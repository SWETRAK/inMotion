namespace IMS.Email.Models.Models;

public class SendFailedLoginAttempt
{
    public string Email { get; set; }
    public DateTime AttemptDateTime { get; set; }
}