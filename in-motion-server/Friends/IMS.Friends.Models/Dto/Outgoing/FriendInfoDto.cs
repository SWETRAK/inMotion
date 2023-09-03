namespace IMS.Friends.Models.Dto.Outgoing;

public class FriendInfoDto
{
    public string Id { get; set; }
    public string Nickname { get; set; }
    public string Bio { get; set; }
    public FriendInfoDto FrontVideo { get; set; }
}