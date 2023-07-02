using IMS.Shared.Models.Dto.User;

namespace IMS.Shared.Models.Dto.Auth;

public class UserSuccessfullyLoggedInResultDto
{
    public string Jwt { get; set; }
    public UserInfoDto UserInfo { get; set; }
}