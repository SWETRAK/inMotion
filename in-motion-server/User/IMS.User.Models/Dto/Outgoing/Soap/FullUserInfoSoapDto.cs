using System.Runtime.Serialization;

namespace IMS.User.Models.Dto.Outgoing.Soap;

[DataContract]
public class FullUserInfoSoapDto
{
    [DataMember]
    public string Id { get; set; }

    [DataMember]
    public string Nickname { get; set; }
    
    [DataMember]
    public string Bio { get; set; }

    [DataMember]
    public UserProfileVideoSoapDto ProfileVideo { get; set; }
}