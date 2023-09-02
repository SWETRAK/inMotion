namespace IMS.Auth.Models.Dto.Incoming;

public class AddPasswordDto
{
    public string NewPassword { get; set; }
    
    public string RepeatPassword { get; set; }
}