using AutoMapper;
using Google.Apis.Auth;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Shared.Domain.Consts;
using IMS.Shared.Domain.Entities.User;
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.Services;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly ILogger<GoogleAuthService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IProviderRepository _providerRepository;
    private readonly IMapper _mapper;

    public GoogleAuthService(
        IUserRepository userRepository,
        ILogger<GoogleAuthService> logger,
        IMapper mapper,
        IProviderRepository providerRepository
    )
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
        _providerRepository = providerRepository;
    }

    public async Task<ImsHttpMessage<UserInfoDto>> SignIn(AuthenticateWithGoogleProviderDto authenticateWithGoogleProviderDto)
    {
        var requestTime = DateTime.UtcNow;
        
        var payload = await ValidateGooglePayload(authenticateWithGoogleProviderDto.IdToken);
        User user;
        
        var provider = await _providerRepository.GetByTokenWithUserAsync(Providers.Google, authenticateWithGoogleProviderDto.IdToken);
        if (provider is null)
        {
            user = await CheckIfEmailTaken(payload, authenticateWithGoogleProviderDto.ProviderKey);
        }
        else
        {
            user = CheckIfActiveUser(provider);
        }

        var userInfoDto = _mapper.Map<UserInfoDto>(user);

        return new ImsHttpMessage<UserInfoDto>
        {
            ServerRequestTime = requestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = userInfoDto
        };
    }

    private async Task<GoogleJsonWebSignature.Payload> ValidateGooglePayload(string idToken)
    {
        var googleSettings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string>() { "https://accounts.google.com/o/oauth2/token" }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, googleSettings);

        if (payload is null)
        {
            // User not validated by Google
            throw new Exception();
        }

        return payload;
    }

    private async Task<User> CheckIfEmailTaken(GoogleJsonWebSignature.Payload payload, string providerKey)
    {
        var tempUser = await _userRepository.GetByEmailAsync(payload.Email);
        if (tempUser is not null)
        {
            // User with email already exists
            throw new Exception();
        }
        return await CreateNewUserFromPayload(payload, providerKey);
    }

    private static User CheckIfActiveUser(Provider provider)
    {
        if (provider.User.ConfirmedAccount is not true)
        {
            // User is not active
            throw new Exception();
        }
        return provider.User;
    }
    
    private async Task<User> CreateNewUserFromPayload(GoogleJsonWebSignature.Payload payload, string providerKey)
    {
        var activationCode = Guid.NewGuid().ToString();
        
        var newProvider = new Provider
        {
            AuthKey = providerKey,
            Name = Providers.Google,
        };
        
        var user = new User
        {
            Email = payload.Email,
            Nickname = $"{payload.GivenName} {payload.FamilyName}",
            ConfirmedAccount = false,
            ActivationToken = activationCode,
            Role = Roles.User ,
            Providers = new List<Provider> {newProvider}
        };

        await _userRepository.Insert(user);
        await _userRepository.Save();
        
        return user;
    }
}