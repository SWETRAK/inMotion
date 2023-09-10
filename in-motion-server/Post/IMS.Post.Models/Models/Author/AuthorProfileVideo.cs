namespace IMS.Post.Models.Models.Author;

public class AuthorProfileVideo
{
    public Guid Id { get; set; }
    public string Filename { get; set; }
    public string BucketName { get; set; }
    public string BucketLocation { get; set; }
    public string ContentType { get; set; }
}