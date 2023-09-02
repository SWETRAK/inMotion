namespace IMS.User.Models.Dto.Outgoing;

public class FullUserInfoDto
{
    public string Id { get; set; }
    public string Nickname { get; set; }

    public string Bio { get; set; }
    public UserProfileVideoDto UserProfileVideo { get; set; }
}