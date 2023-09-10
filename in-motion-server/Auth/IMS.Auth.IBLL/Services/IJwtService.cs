using IMS.Auth.Domain.Entities;
using IMS.Auth.Models.Models;

namespace IMS.Auth.IBLL.Services;

public interface IJwtService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public string GenerateJwtToken(User user);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<UserSuccessfulJwtValidation> ValidateToken(string token);
}