using IMS.Auth.Domain.Entities;
using IMS.Auth.Models.Models;

namespace IMS.Auth.IBLL.Services;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
    public Task<UserSuccessfulJwtValidation> ValidateToken(string token);
}