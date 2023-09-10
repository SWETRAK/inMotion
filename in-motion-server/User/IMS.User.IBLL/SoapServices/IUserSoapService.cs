using System.ServiceModel;
using IMS.User.Models.Dto.Incoming.Soap;
using IMS.User.Models.Dto.Outgoing.Soap;

namespace IMS.User.IBLL.SoapServices;

[ServiceContract]
public interface IUserSoapService
{
    [OperationContract]
    Task<FullUserInfoSoapDto> GetFullUserInfo(string userId);
    
    [OperationContract]
    Task<IEnumerable<FullUserInfoSoapDto>> GetFullUsersInfoByEmail(IEnumerable<string> userIds);

    [OperationContract]
    Task<UpdateUserProfileVideoSoapDto> UpdateUserProfileVideo(string userId, UpdateUserProfileVideoSoapDto updateUserProfileVideoSoapDto);
}