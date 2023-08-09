using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Auth.Models.Exceptions;
using Microsoft.Extensions.Logging;

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
        if(Guid.TryParse(userIdString, out var userIdGuid)) throw new UserGuidStringEmptyException();
        var user = await _userRepository.GetByIdAsync(userIdGuid);
        if (user is null) throw new UserNotFoundException();

        user.Email = updateEmailDto.Email;
        await _userRepository.Save();
        _logger.LogInformation("Updated user email for user with id: {UserIdString}", userIdString);
        
        var result = _mapper.Map<UserInfoDto>(user);
        result.Token = _jwtService.GenerateJwtToken(user);
        return result;
    }

    public async Task<UserInfoDto> UpdateUserNickname(UpdateNicknameDto updateNicknameDto, string userIdString)
    {
        if(Guid.TryParse(userIdString, out var userIdGuid)) throw new UserGuidStringEmptyException();
        var user = await _userRepository.GetByIdAsync(userIdGuid);
        if (user is null) throw new UserNotFoundException();

        user.Nickname = updateNicknameDto.Nickname;
        await _userRepository.Save();
        _logger.LogInformation("Updated nickname {Nickname} for user id {UserId}", updateNicknameDto.Nickname, userIdString);
        
        var result = _mapper.Map<UserInfoDto>(user);
        result.Token = _jwtService.GenerateJwtToken(user);
        return result;
    }
}