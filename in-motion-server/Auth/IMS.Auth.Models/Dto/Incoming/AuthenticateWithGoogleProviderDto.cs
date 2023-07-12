namespace IMS.Auth.Models.Dto.Incoming;

public class AuthenticateWithGoogleProviderDto
{
    public string ProviderKey { get; set; }
    public string IdToken { get; set; }
}