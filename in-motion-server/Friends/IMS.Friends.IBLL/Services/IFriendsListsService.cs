using IMS.Friends.Models.Dto.Outgoing;

namespace IMS.Friends.IBLL.Services;

public interface IFriendsListsService
{
    Task<IEnumerable<Guid>> GetFriendsIdsAsync(string userStringId);
    Task<IEnumerable<AcceptedFriendshipDto>> GetFriendsAsync(string userStringId);
    Task<IEnumerable<RequestFriendshipDto>> GetRequestsAsync(string userStringId);
    Task<IEnumerable<InvitationFriendshipDto>> GetInvitationsAsync(string userStringId);
}