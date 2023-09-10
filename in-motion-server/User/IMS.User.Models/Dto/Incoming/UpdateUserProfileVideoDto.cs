namespace IMS.User.Models.Dto.Incoming;

public class UpdateUserProfileVideoDto
{
    public string Filename { get; set;}
    public string BucketName { get; set; }
    public string BucketLocation { get; set; }
    public string ContentType { get; set; }
}