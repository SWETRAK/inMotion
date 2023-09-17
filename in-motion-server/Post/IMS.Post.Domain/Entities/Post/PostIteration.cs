namespace IMS.Post.Domain.Entities.Post;

public class PostIteration
{
    public Guid Id { get; set;}
    public DateTime StartDateTime { get; set; }
    
    public string IterationName { get; set; }
    public virtual IEnumerable<Post> IterationPosts { get; set; }
}