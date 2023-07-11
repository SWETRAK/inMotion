using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IMS.Auth.BLL.Authentication;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IDAL.Repositories;
using IMS.Auth.Models.Exceptions;
using IMS.Auth.Models.Models;
using IMS.Shared.Domain.Entities.User;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Auth.BLL.Services;

public class JwtService: IJwtService
{
    private readonly AuthenticationConfiguration _authenticationConfiguration;
    private readonly IUserRepository _userRepository;

    public JwtService(AuthenticationConfiguration authenticationConfiguration, IUserRepository userRepository)
    {
        _authenticationConfiguration = authenticationConfiguration;
        _userRepository = userRepository;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Nickname),
            new (ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationConfiguration.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationConfiguration.JwtExpireDays);
        
        var token = new JwtSecurityToken(
            _authenticationConfiguration.JwtIssuer,
            _authenticationConfiguration.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred
        );
        
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public async Task<UserSuccessfulJwtValidation> ValidateToken(string token)
    {
        if (token.IsNullOrEmpty())
        {
            throw new MissingTokenException();
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authenticationConfiguration.JwtKey);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        if (validatedToken is not JwtSecurityToken jwtToken)
        {
            throw new WrongTokenException();
        }

        if (!Guid.TryParse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out var userId))
        {
            throw new IncorrectTokenUserIdException();
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            throw new IncorrectTokenUserIdException();
        }

        return new UserSuccessfulJwtValidation
        {
            Id = user.Id,
            Email = user.Email,
            Nickname = user.Nickname,
            Role = user.Role.ToString(),
        };
    }
}