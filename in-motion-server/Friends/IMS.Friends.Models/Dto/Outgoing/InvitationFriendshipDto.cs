namespace IMS.Friends.Models.Dto.Outgoing;

public class InvitationFriendshipDto
{
    public string Id { get; set; }
    
    public string ExternalUserId { get; set; }
    
    public FriendInfoDto ExternalUser { get; set; }
    
    public DateTime Invited { get; set; }
}