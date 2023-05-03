namespace IMS.Shared.Models.Dto.Auth;

public class UserRegisterWithEmailAndPasswordDto
{
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
}