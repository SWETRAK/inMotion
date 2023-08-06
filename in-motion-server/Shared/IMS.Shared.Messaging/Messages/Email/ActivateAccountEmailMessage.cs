namespace IMS.Shared.Messaging.Messages.Email;

public class ActivateAccountEmailMessage
{
    public string Email { get; set; }
    public DateTime DateTime{ get; set; }
    public string ActivationCode { get; set; }
}