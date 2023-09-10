using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;

namespace IMS.Auth.IBLL.Services;

public interface IFacebookAuthService
{
    Task<UserInfoDto> SignIn(AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto);
    Task<bool> AddFacebookProvider(AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto, string userIdString);
}