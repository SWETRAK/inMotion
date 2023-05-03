namespace IMS.Shared.Models.Dto.User.Video.Reaction;

public class UserProfileVideoReactionDto
{
    public Guid Id { get; set; }

    public AuthorInfoDto Author { get; set; }

    public string Emoji { get; set; }
    
    public DateTime LastModificationDate { get; set; }
}