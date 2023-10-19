namespace IMS.Shared.Messaging.Messages.PostVideos;

public class VideoMetaDataMessage
{
    public string BucketName { get; set; }
    
    public string BucketLocation { get; set; }
    
    public string Filename { get; set; }
    
    public string ContentType { get; set; }
    
    public string Type { get; set; }
}