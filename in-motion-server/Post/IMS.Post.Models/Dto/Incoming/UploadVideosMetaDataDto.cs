namespace IMS.Post.Models.Dto.Incoming;

public class UploadVideosMetaDataDto
{
    public string PostId { get; set; }
    public string AuthorId { get; set; }
    
    public IEnumerable<VideoMetaDataDto> VideosMetaData { get; set; }

}