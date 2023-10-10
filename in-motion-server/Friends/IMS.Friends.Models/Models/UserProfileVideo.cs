namespace IMS.Friends.Models.Models;

public class UserProfileVideo
{
    public Guid Id { get; set; }
    public string Filename { get; set; }
    public string BucketName { get; set; }
    public string BucketLocation { get; set; }
    public string ContentType { get; set; }
}