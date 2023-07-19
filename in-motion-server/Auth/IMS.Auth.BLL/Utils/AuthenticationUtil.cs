using System.Security.Claims;

namespace IMS.Auth.BLL.Utils;

public static class AuthenticationUtil
{
    public static string GetUserId(ClaimsPrincipal user)
    {
        return user.Claims
            .ToDictionary(claim => claim.Type, claim => claim.Value)
            .FirstOrDefault(p => p.Key == ClaimTypes.NameIdentifier)
            .Value;
    }
}