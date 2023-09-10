using System.Runtime.Serialization;

namespace IMS.User.Models.Dto.Incoming.Soap;

[DataContract]
public class UpdateUserProfileVideoSoapDto
{
    [DataMember]
    public string Filename { get; set;}
   
    [DataMember]
    public string BucketName { get; set; }
    
    [DataMember]
    public string BucketLocation { get; set; }
    
    [DataMember]
    public string ContentType { get; set; }
}
