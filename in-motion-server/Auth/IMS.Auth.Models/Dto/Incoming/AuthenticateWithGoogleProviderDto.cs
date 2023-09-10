namespace IMS.Auth.Models.Dto.Incoming;

public class AuthenticateWithGoogleProviderDto
{
    public string UserId { get; set; }
    public string Token { get; set; }
}