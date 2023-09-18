using System.Runtime.Serialization;

namespace IMS.Post.Models.Models.Soap;

[DataContract]
public class UploadVideosMetaData
{
    [DataMember]
    public string PostId { get; set; }
    
    [DataMember]
    public string AuthorId { get; set; }
    
    [DataMember]
    public IEnumerable<VideoMetaData> VideosMetaData { get; set; }
}