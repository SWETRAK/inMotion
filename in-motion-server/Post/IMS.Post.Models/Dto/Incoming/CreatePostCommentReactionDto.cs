namespace IMS.Post.Models.Dto.Incoming;

public class CreatePostCommentReactionDto
{
    public string PostCommentId { get; set; }
    public string Emoji { get; set; }
}