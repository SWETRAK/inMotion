using IMS.Shared.Models.Dto.User;

namespace IMS.Shared.Models.Dto.Post.Reaction;

public class PostReactionDto
{
    public string Id { get; set; }

    public AuthorInfoDto Author { get; set; }
    public DateTime CreationDate { get; set; }
    
    public string Emoji { get; set; }
    public string PostId { get; set; }
}