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
            BaseAddress = new Uri("https://graph.facebook.com/v2.8/")
        };
        _httpClient.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
    }

    public async Task<ImsHttpMessage<UserInfoDto>> SignIn(AuthenticateWithFacebookProviderDto authenticateWithFacebookProviderDto)
    {
        var requestTime = DateTime.UtcNow;
        var result = await GetFacebookUserAsync<dynamic>(authenticateWithFacebookProviderDto.FacebookToken, "me", "fields=first_name,last_name,email,picture.width(100).height(100)");

        var email = (string)result.email;
        
        User user;
        var provider = await _providerRepository.GetByTokenWithUserAsync(
            Providers.Facebook, authenticateWithFacebookProviderDto.FacebookToken);
        if (provider is null)
        {
            user = await CheckIfEmailTaken(payload, authenticateWithGoogleProviderDto.ProviderKey);
        }
        else
        {
            user = CheckIfActiveUser(provider);
        }
        
        var responseData = _mapper.Map<UserInfoDto>(new User());
        
        return new ImsHttpMessage<UserInfoDto>
        {
            ServerRequestTime = requestTime,
            ServerResponseTime = DateTime.UtcNow,
            Status = StatusCodes.Status200OK,
            Data = responseData
        };
    }
    
    private async Task<T> GetFacebookUserAsync<T>(string accessToken, string endpoint, string args = null)
    {
        var response = await _httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("User from this token not exist");

        var result = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine(result);

        return JsonConvert.DeserializeObject<T>(result);
    }
    
    private async Task<User> CheckIfEmailTaken(string email, dynamic payload, string providerKey)
    {
        var tempUser = await _userRepository.GetByEmailAsync(email);
        if (tempUser is not null)
        {
            throw new UserWithEmailAlreadyExistsException(email);
        }
        // return await CreateNewUserFromPayload(payload, providerKey);
        return new User();
    }
    
    private static User CheckIfActiveUser(Provider provider)
    {
        if (provider.User.ConfirmedAccount is not true)
        {
            throw new UserNotFoundException(provider.User.Email);
        }
        return provider.User;
    }
    
}