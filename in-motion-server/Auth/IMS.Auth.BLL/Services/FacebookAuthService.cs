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
using IMS.Shared.Models.Dto;
using Microsoft.AspNetCore.Http;
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

    private const string FacebookBaseUri = "https://graph.facebook.com/v8.0";

    public FacebookAuthService(
        ILogger<FacebookAuthService> logger,
        IUserRepository userRepository,
        IProviderRepository providerRepository,
        IMapper mapper)
    {
        _logger = logger;
        _userRepository = userRepository;
        _providerRepository = providerRepository;
        _mapper = mapper;
        
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(FacebookBaseUri)
        };
        _httpClient.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
    }

    public async Task<ImsHttpMessage<UserInfoDto>> SignIn(AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto)
    {
        var requestTime = DateTime.UtcNow;
        var facebookResponseData = await GetFacebookUserAsync(authenticateWithFacebookProviderDto.Token);
        var user = await CheckProvider(authenticateWithFacebookProviderDto, facebookResponseData);
        var responseData = _mapper.Map<UserInfoDto>(user);
        
        return new ImsHttpMessage<UserInfoDto>
        {
            ServerRequestTime = requestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = responseData
        };
    }
    
    /// <summary>
    /// Method to verify facebook login token
    /// </summary>
    /// <param name="accessToken">Token from frontend application</param>
    /// <returns>Dictionary with email and user id field</returns>
    /// <exception cref="Exception"></exception>
    private async Task<FacebookUserResource> GetFacebookUserAsync(string accessToken)
    {
        var response = await _httpClient.GetAsync($"me?access_token={accessToken}&fields=email,first_name,last_name");

        if (!response.IsSuccessStatusCode)
            throw new Exception("User from this token not exist");
        
        var responseBodyString = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBodyString);
        
        //TODO: Check names of keys here
        Console.WriteLine(responseBodyString);
        return new FacebookUserResource
        {
            Email = data["email"],
            FirstName = data["first_name"],
            LastName = data["last_name"],
            UserId = data["id"]
        };
    }

    private async Task<User> CheckProvider(AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto, FacebookUserResource facebookAuthResponse)
    {
        var provider = await _providerRepository.GetByTokenWithUserAsync(
            Providers.Facebook, authenticateWithFacebookProviderDto.Token);
        
        if (provider is null)
        {
            return await CheckIfEmailTaken(facebookAuthResponse, authenticateWithFacebookProviderDto.UserId);
        }
        
        return CheckIfActiveUser(provider);
    }
    
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
            ConfirmedAccount = false,
            ActivationToken = activationCode,
            Role = Roles.User ,
            Providers = new List<Provider> {newProvider}
        };
        
        await _userRepository.Save();
        
        return user;
    }
    
    private Provider CreateNewProvider(string providerKey)
    {
        Console.WriteLine(providerKey);
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