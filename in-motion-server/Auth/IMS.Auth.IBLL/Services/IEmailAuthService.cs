using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;
using IMS.Auth.Models.Exceptions;

namespace IMS.Auth.IBLL.Services;

public interface IEmailAuthService
{
    
    /// <summary>
    /// Login user logic
    /// </summary>
    /// <param name="requestData">Request data which contains needed info</param>
    /// <returns>Logged user info and token</returns>
    /// <exception cref="IncorrectLoginDataException">Throws exception if user is not found</exception>
    public Task<UserInfoDto> LoginWithEmail(LoginUserWithEmailAndPasswordDto requestData);

    /// <summary>
    /// Register user logic
    /// </summary>
    /// <param name="requestDto">Request data which contains needed info</param>
    /// <returns>Registered user result</returns>
    public Task<SuccessfulRegistrationResponseDto> RegisterWithEmail(RegisterUserWithEmailAndPasswordDto requestDto);

    /// <summary>
    /// Confirm user account via email
    /// </summary>
    /// <param name="email">Gets user email</param>
    /// <param name="token">Gets user activation code</param>
    /// <exception cref="UserNotFoundException">If user with this email and activation code not found</exception>
    public Task ConfirmRegisterWithEmail(string email, string token);

    public Task<bool> UpdatePassword(UpdatePasswordDto updatePasswordDto, string userIdString);
    
    public Task<bool> AddPasswordToExistingAccount(AddPasswordDto addPasswordDto, string userIdString);
    
}