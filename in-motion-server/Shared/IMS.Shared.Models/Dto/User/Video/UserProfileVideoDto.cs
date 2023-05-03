using IMS.Shared.Models.Dto.User.Video.Reaction;

namespace IMS.Shared.Models.Dto.User.Video;

public class UserProfileVideoDto
{
    public string Id { get; set; }
    
    public string Filename { get; set; }
    public string ContentType { get; set; }
    public DateTime CreationDate { get; set; }

    public IEnumerable<UserProfileVideoReactionDto> Reactions { get; set; }
}