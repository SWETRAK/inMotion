using IMS.Auth.Models;
using IMS.Shared.Domain.Entities.User;

namespace IMS.Auth.IBLL.Services;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
    public JwtValidatedUser ValidateToken(string token);
}