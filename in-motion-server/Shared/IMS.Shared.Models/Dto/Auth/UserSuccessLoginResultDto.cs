using IMS.Shared.Models.Dto.User;

namespace IMS.Shared.Models.Dto.Auth;

public class UserSuccessLoginResultDto
{
    public string Jwt { get; set; }
    public UserInfoDto UserInfo { get; set; }
}