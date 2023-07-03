using IMS.Auth.IBLL.Services;
using IMS.Auth.Models.Dto;
using IMS.Shared.Models.Dto;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.Services;

public class EmailAuthService: IEmailAuthService
{
    private readonly ILogger<EmailAuthService> _logger;

    public EmailAuthService(ILogger<EmailAuthService> logger)
    {
        _logger = logger;
    }

    public async Task<ImsHttpMessage<UserInfoDto>> LoginWithEmail(LoginUserWithEmailAndPasswordDto requestData)
    {
        return new ImsHttpMessage<UserInfoDto>();
    }

    public async Task RegisterWithEmail()
    {
        
    }
}