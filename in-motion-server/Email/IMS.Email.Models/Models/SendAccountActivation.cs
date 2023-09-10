namespace IMS.Email.Models.Models;

public class SendAccountActivation
{
    public string Email { get; set; }
    public DateTime RegisterTime { get; set; }
    public string ActivationCode { get; set; }
}