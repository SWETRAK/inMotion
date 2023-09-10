namespace IMS.Post.Models.Dto.Outgoing;

public class PostVideoDto
{
    public string Id { get; set; }
    public string Filename { get; set; }
    public string BucketName { get; set; }
    public string BucketLocation { get; set; }
    public string ContentType { get; set; }
    public string VideoType { get; set; }
}