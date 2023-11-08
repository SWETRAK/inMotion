using AutoMapper;
using IMS.Friends.Domain.Entities;
using IMS.Friends.IBLL.Services;
using IMS.Friends.IDAL.Repositories;
using IMS.Friends.Models.Dto.Outgoing;
using IMS.Friends.Models.Exceptions;
using IMS.Shared.Models.Exceptions;
using IMS.Shared.Utils.Parsers;
using Microsoft.Extensions.Logging;

namespace IMS.Friends.BLL.Services;

public class FriendsListsService : IFriendsListsService
{
    private readonly IFriendshipRepository _friendshipRepository;
    private readonly IUserService _userService;
    private readonly ILogger<FriendsListsService> _logger;
    private readonly IMapper _mapper;

    public FriendsListsService(
        IFriendshipRepository friendshipRepository,
        ILogger<FriendsListsService> logger,
        IMapper mapper,
        IUserService userService)
    {
        _friendshipRepository = friendshipRepository;
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<IEnumerable<Guid>> GetFriendsIdsAsync(string userStringId)
    {
        var userGuidId = userStringId.ParseGuid();
        var acceptedUsers = await _friendshipRepository.GetAccepted(userGuidId);
        return acceptedUsers.Select(x => !x.FirstUserId.Equals(userGuidId) ? x.FirstUserId : x.SecondUserId);
    }

    //TODO: Test this method
    public async Task<IEnumerable<AcceptedFriendshipDto>> GetFriendsAsync(string userStringId)
    {
        var userGuidId = userStringId.ParseGuid();
        
        var acceptedUsers = await _friendshipRepository.GetAccepted(userGuidId);

        if (acceptedUsers.Count <= 0)
            throw new UsersNotFoundException("User don't have friends");
        
        var userIds = acceptedUsers.Select(f => !f.FirstUserId.Equals(userGuidId) ? f.FirstUserId : f.SecondUserId);

        var friendsInfo = await _userService.GetUsersByIdsArray(userIds);

        var result = _mapper.Map<List<Friendship>, IEnumerable<AcceptedFriendshipDto>>(acceptedUsers,
            opt => opt.AfterMap((src, dest) =>
            {
                dest = dest.Select<AcceptedFriendshipDto, AcceptedFriendshipDto>((d) =>
                {
                    var sourceObject = src.First<Friendship>(f => f.Id.Equals(Guid.Parse(d.Id)));
                    d.ExternalUserId = !sourceObject.FirstUserId.Equals(userGuidId)
                        ? sourceObject.FirstUserId.ToString()
                        : sourceObject.SecondUserId.ToString();
                    d.ExternalUser = _mapper.Map<FriendInfoDto>(friendsInfo.FirstOrDefault(ui => ui.Id.Equals(Guid.Parse(d.ExternalUserId))));
                    return d;
                });
            }));
        return result;
    }

    // TODO: Test this method
    public async Task<IEnumerable<RequestFriendshipDto>> GetRequestsAsync(string userStringId)
    {
        var userGuidId = userStringId.ParseGuid();
        var requestUsers = await _friendshipRepository.GetRequested(userGuidId);
        
        if (requestUsers.Count <= 0)
            throw new UsersNotFoundException("User don't have friends");
        
        var userIds = requestUsers.Select(f => f.FirstUserId);
        var friendsInfo = await _userService.GetUsersByIdsArray(userIds);
        
        var result = _mapper.Map<List<Friendship>, IEnumerable<RequestFriendshipDto>>(requestUsers,
            opt => opt.AfterMap((src, dest) =>
            {
                dest = dest.Select<RequestFriendshipDto, RequestFriendshipDto>((d) =>
                {
                    var sourceObject = src.First<Friendship>(f => f.Id.Equals(Guid.Parse(d.Id)));
                    d.ExternalUserId = !sourceObject.FirstUserId.Equals(userGuidId)
                        ? sourceObject.FirstUserId.ToString()
                        : sourceObject.SecondUserId.ToString();
                    d.ExternalUser = _mapper.Map<FriendInfoDto>(friendsInfo.FirstOrDefault(ui => ui.Id.Equals(Guid.Parse(d.ExternalUserId))));
                    return d;
                });
            }));

        return result;
    }

    // TODO: Test this method
    public async Task<IEnumerable<InvitationFriendshipDto>> GetInvitationsAsync(string userStringId)
    {
        var userGuidId = userStringId.ParseGuid();

        var invitationUsers = await _friendshipRepository.GetInvitation(userGuidId);
        
        if (invitationUsers.Count <= 0)
            throw new UsersNotFoundException("User don't have friends");
        
        var userIds = invitationUsers.Select(f => f.SecondUserId);
        
        var friendsInfo = await _userService.GetUsersByIdsArray(userIds);
        
        var result = _mapper.Map<List<Friendship>, IEnumerable<InvitationFriendshipDto>>(invitationUsers,
            opt => opt.AfterMap((src, dest) =>
            {
                dest = dest.Select<InvitationFriendshipDto, InvitationFriendshipDto>((d) =>
                {
                    var sourceObject = src.First<Friendship>(f => f.Id.Equals(Guid.Parse(d.Id)));
                    d.ExternalUserId = !sourceObject.FirstUserId.Equals(userGuidId)
                        ? sourceObject.FirstUserId.ToString()
                        : sourceObject.SecondUserId.ToString();
                    d.ExternalUser = _mapper.Map<FriendInfoDto>(friendsInfo.FirstOrDefault(ui => ui.Id.Equals(Guid.Parse(d.ExternalUserId))));
                    return d;
                });
            }));

        return result;
    }
}