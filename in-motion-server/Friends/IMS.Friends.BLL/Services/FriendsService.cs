using AutoMapper;
using IMS.Friends.Domain.Consts;
using IMS.Friends.Domain.Entities;
using IMS.Friends.IBLL.Services;
using IMS.Friends.IDAL.Repositories;
using IMS.Friends.Models.Dto.Outgoing;
using IMS.Shared.Models.Exceptions;
using Microsoft.Extensions.Logging;

namespace IMS.Friends.BLL.Services;

// TODO: Implement notification service and connect to it
public class FriendsService : IFriendsService
{
    private readonly IFriendshipRepository _friendshipRepository;
    private readonly IUserService _userService;
    private readonly ILogger<FriendsService> _logger;
    private readonly IMapper _mapper;

    public FriendsService(
        IFriendshipRepository friendshipRepository,
        IUserService userService,
        ILogger<FriendsService> logger,
        IMapper mapper
    )
    {
        _friendshipRepository = friendshipRepository;
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RequestFriendshipDto> CreateFriendshipRequest(string userString, string externalUserString)
    {
        if (!Guid.TryParse(userString, out var userIdGuid)) throw new InvalidUserGuidStringException();
        if (!Guid.TryParse(externalUserString, out var externalUserIdGuid)) throw new InvalidUserGuidStringException();

        var relation = await _friendshipRepository.GetByUsersId(userIdGuid, externalUserIdGuid);
        if (relation is not null && relation.Status != FriendshipStatus.Rejected) throw new Exception();

        var externalUserResponse = await _userService.GetUserFromIdArray(externalUserIdGuid);

        var newFriendship = new Friendship
        {
            FirstUserId = userIdGuid,
            SecondUserId = externalUserResponse.Id,
            Status = FriendshipStatus.Waiting
        };

        await _friendshipRepository.InsertAsync(newFriendship);
        await _friendshipRepository.SaveAsync();

        var response = _mapper.Map<Friendship, RequestFriendshipDto>(newFriendship, opt =>
            opt.AfterMap((src, dest) =>
            {
                dest.ExternalUserId = externalUserResponse.Id.ToString();
                dest.ExternalUser = _mapper.Map<FriendInfoDto>(externalUserResponse);
            }));
        return response;
    }

    public async Task<AcceptedFriendshipDto> AcceptFriendshipInvitation(string userIdString, string friendshipIdString)
    {
        if (!Guid.TryParse(userIdString, out var userIdGuid)) throw new InvalidUserGuidStringException();
        if (!Guid.TryParse(friendshipIdString, out var friendshipIdGuid)) throw new InvalidUserGuidStringException();

        var relation = await _friendshipRepository.GetById(friendshipIdGuid);
        if (
            relation is null ||
            !relation.SecondUserId.Equals(userIdGuid) ||
            !relation.Status.Equals(FriendshipStatus.Waiting)
        ) throw new Exception();

        var externalUserResponse = await _userService.GetUserFromIdArray(relation.FirstUserId);

        relation.Status = FriendshipStatus.Accepted;
        relation.LastModificationDate = DateTime.UtcNow;
        await _friendshipRepository.SaveAsync();

        var response = _mapper.Map<Friendship, AcceptedFriendshipDto>(relation, opt =>
            opt.AfterMap((src, dest) =>
            {
                dest.ExternalUserId = externalUserResponse.Id.ToString();
                dest.ExternalUser = _mapper.Map<FriendInfoDto>(externalUserResponse);
            }));

        return response;
    }

    public async Task<RejectedFriendshipDto> RejectFriendshipInvitation(string userIdString, string friendshipIdString)
    {
        if (!Guid.TryParse(userIdString, out var userIdGuid)) throw new InvalidUserGuidStringException();
        if (!Guid.TryParse(friendshipIdString, out var friendshipIdGuid)) throw new InvalidUserGuidStringException();

        var relation = await _friendshipRepository.GetById(friendshipIdGuid);
        if (
            relation is null ||
            !relation.SecondUserId.Equals(userIdGuid) ||
            !relation.Status.Equals(FriendshipStatus.Waiting)
        ) throw new Exception();

        var externalUserResponse = await _userService.GetUserFromIdArray(relation.FirstUserId);

        relation.Status = FriendshipStatus.Rejected;
        relation.LastModificationDate = DateTime.UtcNow;
        await _friendshipRepository.SaveAsync();

        var response = _mapper.Map<Friendship, RejectedFriendshipDto>(relation, opt =>
            opt.AfterMap((src, dest) =>
            {
                dest.ExternalUserId = externalUserResponse.Id.ToString();
                dest.ExternalUser = _mapper.Map<FriendInfoDto>(externalUserResponse);
            }));

        return response;
    }

    public async Task<RejectedFriendshipDto> UnfriendExistingFriendship(string userIdString, string friendshipIdString)
    {
        if (!Guid.TryParse(userIdString, out var userIdGuid)) throw new InvalidUserGuidStringException();
        if (!Guid.TryParse(friendshipIdString, out var friendshipIdGuid)) throw new InvalidUserGuidStringException();
     

        var relation = await _friendshipRepository.GetById(friendshipIdGuid);
        if (relation is null || !relation.Status.Equals(FriendshipStatus.Accepted)) throw new Exception();

        var externalUserResponse = await _userService.GetUserFromIdArray(relation.FirstUserId);
        
        relation.Status = FriendshipStatus.Rejected;
        relation.LastModificationDate = DateTime.UtcNow;
        await _friendshipRepository.SaveAsync();

        var response = _mapper.Map<Friendship, RejectedFriendshipDto>(relation, opt =>
            opt.AfterMap((src, dest) =>
            {
                dest.ExternalUserId = externalUserResponse.Id.ToString();
                dest.ExternalUser = _mapper.Map<FriendInfoDto>(externalUserResponse);
            }));
        
        return response;
    }

    public async Task RevertFriendshipInvitation(string userIdString, string friendshipIdString)
    {
        if (!Guid.TryParse(userIdString, out var userIdGuid)) throw new InvalidUserGuidStringException();
        if (!Guid.TryParse(friendshipIdString, out var friendshipIdGuid)) throw new InvalidUserGuidStringException();

        var relation = await _friendshipRepository.GetById(friendshipIdGuid);
        if (relation is null ||
            !relation.FirstUserId.Equals(userIdGuid) ||
            !relation.Status.Equals(FriendshipStatus.Waiting)
        ) throw new Exception();
        
        await _userService.GetUserFromIdArray(relation.SecondUserId);
        
        _friendshipRepository.RemoveAsync(relation);
        await _friendshipRepository.SaveAsync();
    }
}