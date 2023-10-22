using IMS.User.Models.Dto.Incoming;
using IMS.User.Models.Dto.Outgoing;

namespace IMS.User.IBLL.Services;

public interface IUserVideoPartService
{
    Task<UpdatedUserProfileVideoDto> UpdateUserProfileVideo(string userId,
        UpdateUserProfileVideoDto updateUserProfileVideoDto);
}