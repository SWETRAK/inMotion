using System.Runtime.Serialization;

namespace IMS.Post.Models.Models.Soap;

[DataContract]
public class VideoMetaData
{
    
    [DataMember]
    public string BucketName { get; set; }
    
    [DataMember]
    public string BucketLocation { get; set; }
    
    [DataMember]
    public string Filename { get; set; }
    
    [DataMember]
    public string ContentType { get; set; }
    
    [DataMember]
    public string Type { get; set; }
}