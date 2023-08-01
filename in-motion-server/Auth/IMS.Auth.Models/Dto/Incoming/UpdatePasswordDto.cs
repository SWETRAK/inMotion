namespace IMS.Auth.Models.Dto.Incoming;

public class UpdatePasswordDto
{
    public string OldPassword { get; set; }
    
    public string NewPassword { get; set; }
    
    public string RepeatPassword { get; set; }
}