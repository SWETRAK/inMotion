using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;

namespace IMS.Auth.IBLL.Services;

public interface IGoogleAuthService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="authenticateWithGoogleProviderDto"></param>
    /// <returns></returns>
    Task<ImsHttpMessage<UserInfoDto>> SignIn(AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authenticateWithGoogleProviderDto"></param>
    /// <param name="userIdString"></param>
    /// <returns></returns>
    Task<ImsHttpMessage<bool>> AddGoogleProvider(AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto, string userIdString);
}