namespace IMS.Post.Models.Dto.Outgoing;

public class GetPostResponseDto
{
    public string Id { get; set; }
    
    public string Description { get; set;}
    public string Title { get; set; }
    
    public PostAuthorDto Author { get; set; }
    
    public IEnumerable<PostTagDto> Tags { get; set; }
    public IEnumerable<PostVideoDto> Videos { get; set; }
    
    public long PostCommentsCount { get; set; }
    public long PostReactionsCount { get; set; }
    
    public DateTime CreatedAt { get; set; }
}