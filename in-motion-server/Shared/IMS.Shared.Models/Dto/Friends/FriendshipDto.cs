using IMS.Shared.Models.Dto.User;

namespace IMS.Shared.Models.Dto.Friends;

public class FriendshipDto
{
    public string Id { get; set; }
    public AuthorInfoDto FirstUsers { get; set; }

    public AuthorInfoDto SecondUser { get; set; }

    public int Status { get; set; }

    public DateTime CreationDate { get; set; }
}