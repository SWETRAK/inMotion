namespace IMS.Friends.Models.Dto.Outgoing;

public class AcceptedFriendshipDto
{
    public string Id { get; set; }
    
    public string ExternalUserId { get; set; }
    
    public FriendInfoDto ExternalUser { get; set; }
    
    public DateTime FriendsSince { get; set; }
}