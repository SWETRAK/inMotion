using IMS.Auth.Models.Dto;
using IMS.Shared.Models.Dto;

namespace IMS.Auth.IBLL.Services;

public interface IEmailAuthService
{
    public Task<ImsHttpMessage<UserInfoDto>> LoginWithEmail(LoginUserWithEmailAndPasswordDto requestData);

    public Task RegisterWithEmail();
}