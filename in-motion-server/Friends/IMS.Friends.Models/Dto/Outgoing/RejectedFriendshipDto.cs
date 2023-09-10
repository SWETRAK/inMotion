namespace IMS.Friends.Models.Dto.Outgoing;

public class RejectedFriendshipDto
{
    public string Id { get; set; }
    
    public string ExternalUserId { get; set; }
    
    public FriendInfoDto ExternalUser { get; set; }
    
    public DateTime Rejected { get; set; }
}