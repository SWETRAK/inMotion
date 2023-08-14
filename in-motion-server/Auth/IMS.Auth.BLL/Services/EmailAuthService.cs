using AutoMapper;
using IMS.Auth.Domain.Consts;
using IMS.Auth.Domain.Entities;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Auth.Models.Exceptions;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Email.Auth;
using MassTransit;
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

    private readonly IPublishEndpoint _publishEndpoint;

    public EmailAuthService(
        ILogger<EmailAuthService> logger,
        IJwtService jwtService,
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _jwtService = jwtService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Login user logic
    /// </summary>
    /// <param name="requestData">Request data which contains needed info</param>
    /// <returns>Logged user info and token</returns>
    /// <exception cref="IncorrectLoginDataException">Throws exception if user is not found</exception>
    public async Task<UserInfoDto> LoginWithEmail(LoginUserWithEmailAndPasswordDto requestData)
    {
        var user = await _userRepository.GetByEmailAsync(requestData.Email.Trim().ToLower());
        if (user is null || user.HashedPassword.IsNullOrEmpty() || user.ConfirmedAccount is not true)
            throw new IncorrectLoginDataException(requestData.Email.Trim().ToLower());

        var passwordCheckResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, requestData.Password);
        if (passwordCheckResult == PasswordVerificationResult.Failed)
        {
            await _publishEndpoint.Publish<ImsBaseMessage<FailureLoginAttemptEmailMessage>>(new ImsBaseMessage<FailureLoginAttemptEmailMessage>
            {
                Data = new FailureLoginAttemptEmailMessage
                {
                    Email = requestData.Email,
                    DateTime = DateTime.Now
                }
            });
            throw new IncorrectLoginDataException(requestData.Email);
        }
        
        await _publishEndpoint.Publish<ImsBaseMessage<UserLoggedInEmailMessage>>(new ImsBaseMessage<UserLoggedInEmailMessage>
        {
            Data = new UserLoggedInEmailMessage
            {
                Email = requestData.Email,
                LoggedDate = DateTime.UtcNow
            }
        });
        
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
            Email = requestDto.Email.Trim().ToLower(),
            Nickname = requestDto.Nickname,
            ConfirmedAccount = false,
            ActivationToken = activationCode,
            Role = Roles.User
        };
        
        await _publishEndpoint.Publish<ImsBaseMessage<ActivateAccountEmailMessage>>(new ImsBaseMessage<ActivateAccountEmailMessage>
        {
            Data = new ActivateAccountEmailMessage
            {
                Email = requestDto.Email,
                DateTime = DateTime.UtcNow,
                ActivationCode = activationCode
            }
        });
            
        newUser.HashedPassword = _passwordHasher.HashPassword(newUser, requestDto.Password);
        await _userRepository.Insert(newUser);
        await _userRepository.Save();

        _logger.LogInformation("User successfully created");
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
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null || !user.ActivationToken.Equals(token))
            throw new UserNotFoundException(email);

        user.ActivationToken = null;
        user.ConfirmedAccount = true;
        await _userRepository.Save();
    }

    public async Task<bool> UpdatePassword(UpdatePasswordDto updatePasswordDto, string userIdString)
    {
        if (Guid.TryParse(userIdString, out var userIdGuid)) throw new UserGuidStringEmptyException();
        var user = await _userRepository.GetByIdAsync(userIdGuid);

        if (user is null) throw new UserNotFoundException();

        var passwordValidationResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, updatePasswordDto.OldPassword);
        if (passwordValidationResult == PasswordVerificationResult.Failed)
            throw new IncorrectLoginDataException(user.Email);

        user.HashedPassword = _passwordHasher.HashPassword(user, updatePasswordDto.NewPassword);

        await _userRepository.Save();
        return true;
    }

    public async Task<bool> AddPasswordToExistingAccount(AddPasswordDto addPasswordDto, string userIdString)
    {
        if (Guid.TryParse(userIdString, out var userIdGuid)) throw new UserGuidStringEmptyException();
        var user = await _userRepository.GetByIdAsync(userIdGuid);
        if (user is null) throw new UserNotFoundException();

        user.HashedPassword = _passwordHasher.HashPassword(user, addPasswordDto.NewPassword);

        await _userRepository.Save();
        return true;
    }
}
