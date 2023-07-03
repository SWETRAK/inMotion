using Microsoft.VisualBasic.CompilerServices;

namespace IMS.Auth.Models.Dto;

public class LoginUserWithEmailAndPasswordDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}