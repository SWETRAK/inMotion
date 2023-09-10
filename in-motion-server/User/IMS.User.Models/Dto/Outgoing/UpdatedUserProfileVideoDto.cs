namespace IMS.User.Models.Dto.Outgoing;

public class UpdatedUserProfileVideoDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Filename { get; set; }
    public string BucketName { get; set; }
    public string BucketLocation { get; set; }
    public string ContentType { get; set; }
}