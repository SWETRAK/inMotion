using IMS.Auth.Models;
using IMS.Auth.Models.Models;
using IMS.Shared.Domain.Entities.User;

namespace IMS.Auth.IBLL.Services;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
    public UserSuccessfulJwtValidation ValidateToken(string token);
}