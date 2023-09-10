namespace IMS.Auth.Models.Dto.Incoming;

public class RegisterUserWithEmailAndPasswordDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
    public string Nickname { get; set; }
}