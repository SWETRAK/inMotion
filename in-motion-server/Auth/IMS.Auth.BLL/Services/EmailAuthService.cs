using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
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
    
    /// <summary>
    /// Login user logic
    /// </summary>
    /// <param name="requestData">Request data which contains needed info</param>
    /// <returns>Logged user info and token</returns>
    /// <exception cref="IncorrectLoginDataException">Throws exception if user is not found</exception>
    public async Task<UserInfoDto> LoginWithEmail(LoginUserWithEmailAndPasswordDto requestData)
    {
        var user = await _userRepository.GetByEmailAsync(requestData.Email);
        if (user is null || user.HashedPassword.IsNullOrEmpty() || user.ConfirmedAccount is not true)
        {
            throw new IncorrectLoginDataException(requestData.Email);
        }

        var passwordCheckResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, requestData.Password);
        if (passwordCheckResult == PasswordVerificationResult.Failed)
        {
            // TODO: Send email if try login 
            throw new IncorrectLoginDataException(requestData.Email);
        }

        // TODO: Send email if login success
        var result = _mapper.Map<UserInfoDto>(user);
        
        result.Token = _jwtService.GenerateJwtToken(user);
        
        return result;
    }

    /// <summary>
    /// Register user logic
    /// </summary>
    /// <param name="requestDto">Request data which contains needed info</param>
    /// <returns>Registered user result</returns>
    public async Task<SuccessfulRegistrationResponseDto> RegisterWithEmail(RegisterUserWithEmailAndPasswordDto requestDto)
    {
        var activationCode = Guid.NewGuid().ToString();
        
        var newUser = new User
        {
            Email = requestDto.Email,
            Nickname = requestDto.Nickname,
            ConfirmedAccount = false,
            ActivationToken = activationCode
        };

        // TODO: Send email to confirm user creation with email and activation code 
        newUser.HashedPassword = _passwordHasher.HashPassword(newUser, requestDto.Password);
        await _userRepository.Insert(newUser);
        await _userRepository.Save();

        var result = new SuccessfulRegistrationResponseDto()
        {
            Email = newUser.Email
        };

        return result;
    }

    /// <summary>
    /// Confirm user account via email
    /// </summary>
    /// <param name="email">Gets user email</param>
    /// <param name="token">Gets user activation code</param>
    /// <exception cref="UserNotFoundException">If user with this email and activation code not found</exception>
    public async Task ConfirmRegisterWithEmail(string email, string token)
    {
        Console.WriteLine(email);
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null || !user.ActivationToken.Equals(token))
        {
            throw new UserNotFoundException(email);
        }

        user.ActivationToken = null;
        user.ConfirmedAccount = true;
        await _userRepository.Save();
    }
}
