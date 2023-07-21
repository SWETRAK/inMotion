using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;

namespace IMS.Auth.IBLL.Services;

public interface IFacebookAuthService
{
    Task<ImsHttpMessage<UserInfoDto>> SignIn(AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto);
}