using IMS.Friends.Models.Dto.Outgoing;

namespace IMS.Friends.IBLL.Services;

public interface IFriendsService
{
    Task<RequestFriendshipDto> CreateFriendshipRequest(string externalUserDto, string userDto);
    Task<AcceptedFriendshipDto> AcceptFriendshipInvitation(string userIdString, string friendshipIdString);
    Task<RejectedFriendshipDto> RejectFriendshipInvitation(string userIdString, string friendshipIdString);
    Task<RejectedFriendshipDto> UnfriendExistingFriendship(string userIdString, string friendshipIdString);
    Task RevertFriendshipInvitation(string userIdString, string friendshipIdString);
}