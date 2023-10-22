namespace IMS.Shared.Messaging.Messages.PostVideos;

public class UpdatePostVideoMetadataMessage
{
    public string PostId { get; set; }
    
    public string AuthorId { get; set; }
    
    public IEnumerable<VideoMetaDataMessage> VideosMetaData { get; set; }
}