namespace IMS.Shared.Messaging.Messages.Users;

public class UpdateUserProfileVideoMessage
{
    public string UserId { get; set; }
    public string Filename { get; set; }
    public string BucketName { get; set; }
    public string BucketLocation { get; set; }
    public string ContentType { get; set; }
}