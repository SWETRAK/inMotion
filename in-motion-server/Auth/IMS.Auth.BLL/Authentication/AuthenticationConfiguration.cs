namespace IMS.Auth.BLL.Authentication;

public class AuthenticationConfiguration
{
    public string JwtKey { get; set; }
    public int JwtExpireDays { get; set; }
    public string JwtIssuer { get; set; }
}