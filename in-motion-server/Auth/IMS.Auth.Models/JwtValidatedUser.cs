namespace IMS.Auth.Models;

public class JwtValidatedUser
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }
}