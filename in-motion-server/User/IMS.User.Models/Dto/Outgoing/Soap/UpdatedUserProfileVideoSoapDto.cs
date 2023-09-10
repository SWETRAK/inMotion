using System.Runtime.Serialization;

namespace IMS.User.Models.Dto.Outgoing.Soap;

[DataContract]
public class UpdatedUserProfileVideoSoapDto
{
    
    [DataMember]
    public string Id { get; set; }
    
    [DataMember]
    public string Filename { get; set; }
    
    [DataMember]
    public string BucketName { get; set; }
    
    [DataMember]
    public string BucketLocation { get; set; }
    
    [DataMember]
    public string ContentType { get; set; }
}