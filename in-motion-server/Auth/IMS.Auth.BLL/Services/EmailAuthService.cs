using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Dto;
using IMS.Auth.Models.Exceptions;
using IMS.Shared.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Auth.BLL.Services;

public class EmailAuthService : IEmailAuthService
{
    private readonly ILogger<EmailAuthService> _logger;
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;

    public EmailAuthService(
        ILogger<EmailAuthService> logger,
        IJwtService jwtService,
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher, IMapper mapper)
    {
        _logger = logger;
        _jwtService = jwtService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<UserInfoDto> LoginWithEmail(LoginUserWithEmailAndPasswordDto requestData)
    {
        var user = await _userRepository.GetByEmailAsync(requestData.Email);
        if (user is null || user.HashedPassword.IsNullOrEmpty())
        {
            _logger.LogWarning("User with {RequestDataEmail} try to login but not found", requestData.Email);
            throw new IncorrectLoginDataException();
        }

        var passwordCheckResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, requestData.Password);
        if (passwordCheckResult == PasswordVerificationResult.Failed)
        {
            _logger.LogWarning("User with {RequestDataEmail} try to login but not found", requestData.Email);
            throw new IncorrectLoginDataException();
        }

        var result = _mapper.Map<UserInfoDto>(user);
        
        result.Token = _jwtService.GenerateJwtToken(user);
        
        return result;
    }

    public async Task RegisterWithEmail()
    {

    }
}