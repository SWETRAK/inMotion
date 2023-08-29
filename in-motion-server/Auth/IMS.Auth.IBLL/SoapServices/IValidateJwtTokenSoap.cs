using System.ServiceModel;
using IMS.Auth.Models.Dto.Outgoing;

namespace IMS.Auth.IBLL.SoapServices;

[ServiceContract]
public interface IValidateJwtTokenSoap
{
    [OperationContract]
    public Task<UserInfoWithRoleDto> ValidateJwtToken(string jwtToken);
}