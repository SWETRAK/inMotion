using System.Net.Http.Headers;
using System.Net.Mime;
using AutoMapper;
using IMS.Auth.Domain.Consts;
using IMS.Auth.Domain.Entities;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Auth.Models.Exceptions;
using IMS.Auth.Models.Models;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Email.Auth;
using IMS.Shared.Models.Exceptions;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IMS.Auth.BLL.Services;

public class FacebookAuthService : IFacebookAuthService
{
    private readonly ILogger<FacebookAuthService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IProviderRepository _providerRepository;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;
    private readonly IJwtService _jwtServices;
    
    private readonly IPublishEndpoint _publishEndpoint;

    private const string FacebookBaseUri = "https://graph.facebook.com/v8.0";

    public FacebookAuthService(
        ILogger<FacebookAuthService> logger,
        IUserRepository userRepository,
        IProviderRepository providerRepository,
        IMapper mapper, 
        IJwtService jwtServices, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _userRepository = userRepository;
        _providerRepository = providerRepository;
        _mapper = mapper;
        _jwtServices = jwtServices;
        _publishEndpoint = publishEndpoint;

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(FacebookBaseUri)
        };
        _httpClient.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
    }
    
    /// <summary>
    /// Login or register with facebook provider
    /// </summary>
    /// <param name="authenticateWithFacebookProviderDto"></param>
    /// <returns></returns>
    public async Task<UserInfoDto> SignIn(AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto)
    {
        var facebookResponseData = await GetFacebookUserAsync(authenticateWithFacebookProviderDto.Token);
        var user = await CheckProvider(authenticateWithFacebookProviderDto, facebookResponseData);
        var responseData = _mapper.Map<UserInfoDto>(user);
        responseData.Token = _jwtServices.GenerateJwtToken(user);
        
        await _publishEndpoint.Publish<ImsBaseMessage<UserLoggedInEmailMessage>>(new ImsBaseMessage<UserLoggedInEmailMessage>
        {
            Data = new UserLoggedInEmailMessage
            {
                Email = user.Email,
                LoggedDate = DateTime.UtcNow
            }
        });
        
        _logger.LogInformation("User successfully logged in with email {Email}", user.Email);
        return responseData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authenticateWithFacebookProviderDto"></param>
    /// <param name="userIdString"></param>
    /// <returns></returns>
    /// <exception cref="InvalidGuidStringException"></exception>
    /// <exception cref="UserGuidStringEmptyException"></exception>
    /// <exception cref="UserNotFoundException"></exception>
    /// <exception cref="UserWithThisProviderExists"></exception>
    public async Task<bool> AddFacebookProvider(AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto, string userIdString)
    {

        if (userIdString is null) throw new InvalidGuidStringException();
        if (!Guid.TryParse(userIdString, out var userId)) throw new UserGuidStringEmptyException();
        
        await GetFacebookUserAsync(authenticateWithFacebookProviderDto.Token);
        
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null) throw new UserNotFoundException();

        if (user.Providers is null)
        {
            user.Providers = new [] {CreateNewProvider(authenticateWithFacebookProviderDto.UserId)};
        }
        else
        {
            var oldProvider = user.Providers.FirstOrDefault(x => x.AuthKey == authenticateWithFacebookProviderDto.UserId);

            if (oldProvider is not null) throw new UserWithThisProviderExists();
            
            user.Providers = user.Providers.Append(CreateNewProvider(authenticateWithFacebookProviderDto.UserId));
        }
        
        await _userRepository.Save();

        return true;
    }
    
    /// <summary>
    /// Method to verify facebook login token
    /// </summary>
    /// <param name="accessToken">Token from frontend application</param>
    /// <returns>FacebookUserResource object with authenticated facebook user</returns>
    /// <exception cref="IncorrectProviderTokenException"></exception>
    private async Task<FacebookUserResource> GetFacebookUserAsync(string accessToken)
    {
        var response = await _httpClient.GetAsync($"me?access_token={accessToken}&fields=email,first_name,last_name");

        if (!response.IsSuccessStatusCode)
            throw new IncorrectProviderTokenException();
        
        var responseBodyString = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBodyString);
        
        return new FacebookUserResource
        {
            Email = data["email"],
            FirstName = data["first_name"],
            LastName = data["last_name"],
            UserId = data["id"]
        };
    }

    /// <summary>
    /// Method checks if provider with this external Id exists, if is available logins user if not register new user
    /// </summary>
    /// <param name="authenticateWithFacebookProviderDto"></param>
    /// <param name="facebookAuthResponse"></param>
    /// <returns></returns>
    private async Task<User> CheckProvider(AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto, FacebookUserResource facebookAuthResponse)
    {
        var provider = await _providerRepository.GetByTokenWithUserAsync(
            Providers.Facebook, authenticateWithFacebookProviderDto.Token);
        
        if (provider is null)
        {
            // Provider doesnt exist and we can register new user with this credentials
            return await CheckIfEmailTaken(facebookAuthResponse, authenticateWithFacebookProviderDto.UserId);
        }
        // Provider exists and user can be logged in if activated
        return CheckIfActiveUser(provider);
    }
    
    /// <summary>
    /// Method checks if email received from provider is taken if not creates new user
    /// </summary>
    /// <param name="facebookPayload"></param>
    /// <param name="providerKey"></param>
    /// <returns></returns>
    /// <exception cref="UserWithEmailAlreadyExistsException"></exception>
    private async Task<User> CheckIfEmailTaken(FacebookUserResource facebookPayload, string providerKey)
    {
        var tempUser = await _userRepository.GetByEmailAsync(facebookPayload.Email);
        if (tempUser is not null)
            throw new UserWithEmailAlreadyExistsException(facebookPayload.Email);
        
        return await CreateNewUserFromPayload(facebookPayload, providerKey);
    }
    
    private async Task<User> CreateNewUserFromPayload(FacebookUserResource payload, string providerKey)
    {
        var activationCode = Guid.NewGuid().ToString();

        var newProvider = CreateNewProvider(providerKey);
        
        var user = new User
        {
            Email = payload.Email,
            Nickname = $"{payload.FirstName} {payload.LastName}",
            ConfirmedAccount = true,
            ActivationToken = string.Empty,
            Role = Roles.User ,
            Providers = new List<Provider> {newProvider}
        };

        await _userRepository.Insert(user);
        await _userRepository.Save();
        
        return user;
    }
    
    private static Provider CreateNewProvider(string providerKey)
    {
        return new Provider
        {
            AuthKey = providerKey,
            Name = Providers.Facebook,
        };
    }
    
    private static User CheckIfActiveUser(Provider provider)
    {
        if (provider.User.ConfirmedAccount is not true)
            throw new UserNotFoundException(provider.User.Email);
        
        return provider.User;
    }
}