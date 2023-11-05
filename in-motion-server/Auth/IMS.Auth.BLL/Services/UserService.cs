using AutoMapper;
using IMS.Auth.Domain.Entities;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Auth.Models.Exceptions;
using IMS.Shared.Models.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Auth.BLL.Services;

public class UserService: IUserService
{
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public UserService(
        IMapper mapper, 
        ILogger<UserService> logger, 
        IUserRepository userRepository, 
        IJwtService jwtService
        )
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<UserInfoDto> UpdateUserEmail(UpdateEmailDto updateEmailDto, string userIdString)
    {
        if(!Guid.TryParse(userIdString, out var userIdGuid)) throw new UserGuidStringEmptyException();
        var user = await _userRepository.GetByIdWithProvidersAsync(userIdGuid);
        if (user is null) throw new UserNotFoundException();

        var checkUser = await _userRepository.GetByEmailAsync(updateEmailDto.Email);

        if (checkUser is not null)
            throw new UserWithEmailAlreadyExistsException();

        user.Email = updateEmailDto.Email;
        await _userRepository.Save();
        _logger.LogInformation("Updated user email for user with id: {UserIdString}", userIdString);
        
        var result = _mapper.Map<UserInfoDto>(user);
        result.Token = _jwtService.GenerateJwtToken(user);
        result.Providers = GetProvidersInfo(user);
        return result;
    }

    public async Task<UserInfoDto> UpdateUserNickname(UpdateNicknameDto updateNicknameDto, string userIdString)
    {
        if(!Guid.TryParse(userIdString, out var userIdGuid)) throw new UserGuidStringEmptyException();
        var user = await _userRepository.GetByIdWithProvidersAsync(userIdGuid);
        if (user is null) throw new UserNotFoundException();

        user.Nickname = updateNicknameDto.Nickname;
        await _userRepository.Save();
        _logger.LogInformation("Updated nickname {Nickname} for user id {UserId}", updateNicknameDto.Nickname, userIdString);
        
        var result = _mapper.Map<UserInfoDto>(user);
        result.Token = _jwtService.GenerateJwtToken(user);
        result.Providers = GetProvidersInfo(user);
        return result;
    }

    public async Task<UserInfoDto> GetUserInfo(string userIdString)
    {
        if (!Guid.TryParse(userIdString, out var userIdGuid))
            throw new InvalidGuidStringException();

        var user = await _userRepository.GetByIdWithProvidersAsync(userIdGuid);
        
        var userInfoDto =  _mapper.Map<UserInfoDto>(user);
        userInfoDto.Token = _jwtService.GenerateJwtToken(user);
        userInfoDto.Providers = GetProvidersInfo(user);
        return userInfoDto;
    }

    public async Task<IEnumerable<UserInfoDto>> GetUsersInfo(IEnumerable<string> userIdStrings)
    {
        var userIdGuids = userIdStrings.Select(s =>
        {
            if (!Guid.TryParse(s, out var userIdGuid))
                throw new InvalidGuidStringException();
            return userIdGuid;
        });

        var users = await _userRepository.GetManyByIdRangeAsync(userIdGuids);


        return _mapper.Map<IEnumerable<UserInfoDto>>(users);
    }

    public List<string> GetProvidersInfo(User user)
    {
        var providers = new List<string>();

        if (!user.HashedPassword.IsNullOrEmpty())
        {
            providers.Add("Password");
        }

        if (!user.Providers.IsNullOrEmpty())
        {
            providers.AddRange(user.Providers.Select(provider => provider.Name.ToString()));
        }
        
        return providers;
    }
}