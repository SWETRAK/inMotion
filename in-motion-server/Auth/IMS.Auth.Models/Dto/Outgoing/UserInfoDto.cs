namespace IMS.Auth.Models.Dto.Outgoing;

public class UserInfoDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string Token { get; set; }
    public IEnumerable<string> Providers { get; set; }
}