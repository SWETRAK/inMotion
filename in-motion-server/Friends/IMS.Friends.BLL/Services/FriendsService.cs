using AutoMapper;
using IMS.Friends.Domain.Consts;
using IMS.Friends.Domain.Entities;
using IMS.Friends.IBLL.Services;
using IMS.Friends.IDAL.Repositories;
using IMS.Friends.Models.Dto.Outgoing;
using IMS.Friends.Models.Exceptions;
using IMS.Shared.Models.Exceptions;
using Microsoft.Extensions.Logging;

namespace IMS.Friends.BLL.Services;

// TODO: [EXTRA] Implement notification service and connect to it
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

    public async Task<string> GetFriendshipStatus(string userIdString, string externalUserIdString)
    {
        if (!Guid.TryParse(userIdString, out var userIdGuid)) throw new InvalidGuidStringException();
        if (!Guid.TryParse(externalUserIdString, out var externalUserIdGuid)) throw new InvalidGuidStringException();
        
        var relation = await _friendshipRepository.GetByUsersId(userIdGuid, externalUserIdGuid);
        return relation is null ? FriendshipStatus.Unknown.ToString() : relation.Status.ToString();
    }

    public async Task<RequestFriendshipDto> CreateFriendshipRequest(string userString, string externalUserString)
    {
        if (!Guid.TryParse(userString, out var userIdGuid)) throw new InvalidGuidStringException();
        if (!Guid.TryParse(externalUserString, out var externalUserIdGuid)) throw new InvalidGuidStringException();
        
        var relation = await _friendshipRepository.GetByUsersId(userIdGuid, externalUserIdGuid);
        var externalUserResponse = await _userService.GetUserByIdArray(externalUserIdGuid);

        var responseFriendship = await FriendshipActionWrapper(relation, userIdGuid, externalUserResponse.Id);
        
        var response = _mapper.Map<Friendship, RequestFriendshipDto>(responseFriendship, opt =>
            opt.AfterMap((src, dest) =>
            {
                dest.ExternalUserId = externalUserResponse.Id.ToString();
                dest.ExternalUser = _mapper.Map<FriendInfoDto>(externalUserResponse);
            }));
        return response;
    }

    public async Task<AcceptedFriendshipDto> AcceptFriendshipInvitation(string userIdString, string friendshipIdString)
    {
        if (!Guid.TryParse(userIdString, out var userIdGuid)) throw new InvalidGuidStringException();
        if (!Guid.TryParse(friendshipIdString, out var friendshipIdGuid)) throw new InvalidGuidStringException();

        var relation = await _friendshipRepository.GetById(friendshipIdGuid);
        if (
            relation is null ||
            !relation.SecondUserId.Equals(userIdGuid) ||
            relation.Status is not FriendshipStatus.Waiting
        ) throw new InvalidFriendshipActionException("Acceptance of friendship is impossible");

        var externalUserResponse = await _userService.GetUserByIdArray(relation.FirstUserId);

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
        if (!Guid.TryParse(userIdString, out var userIdGuid)) throw new InvalidGuidStringException();
        if (!Guid.TryParse(friendshipIdString, out var friendshipIdGuid)) throw new InvalidGuidStringException();

        var relation = await _friendshipRepository.GetById(friendshipIdGuid);
        if (
            relation is null ||
            !relation.SecondUserId.Equals(userIdGuid) ||
            relation.Status is not FriendshipStatus.Waiting
        ) throw new InvalidFriendshipActionException("Reject of Friendship is impossible");

        var externalUserResponse = await _userService.GetUserByIdArray(relation.FirstUserId);

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
        if (!Guid.TryParse(userIdString, out var userIdGuid)) throw new InvalidGuidStringException();
        if (!Guid.TryParse(friendshipIdString, out var friendshipIdGuid)) throw new InvalidGuidStringException();
     

        var relation = await _friendshipRepository.GetById(friendshipIdGuid);
        if (relation?.Status is not FriendshipStatus.Accepted) 
            throw new InvalidFriendshipActionException("Unfriend is impossible");

        var externalUserResponse = await _userService.GetUserByIdArray(relation.FirstUserId);
        
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
        if (!Guid.TryParse(userIdString, out var userIdGuid)) throw new InvalidGuidStringException();
        if (!Guid.TryParse(friendshipIdString, out var friendshipIdGuid)) throw new InvalidGuidStringException();

        var relation = await _friendshipRepository.GetById(friendshipIdGuid);
        if (relation is null ||
            !relation.FirstUserId.Equals(userIdGuid) ||
            relation.Status is not FriendshipStatus.Waiting
        ) throw new InvalidFriendshipActionException("Friendship revert is impossible");
        
        await _userService.GetUserByIdArray(relation.SecondUserId);
        
        _friendshipRepository.RemoveAsync(relation);
        await _friendshipRepository.SaveAsync();
    }

    private async Task<Friendship> FriendshipActionWrapper(Friendship relation, Guid firstUserId, Guid secondUserId)
    {
        if (relation is not null)
        {
            return await UpdateExistingFriendship(relation, firstUserId, secondUserId);
        }

        return await CreateNewFriendship(firstUserId, secondUserId);
    }
    
    private async Task<Friendship> UpdateExistingFriendship(Friendship relation, Guid firstUserId, Guid secondUserId)
    {
        if (relation.Status is FriendshipStatus.Rejected or FriendshipStatus.Inverted)
        {
            if (relation.FirstUserId != firstUserId)
            {
                relation.FirstUserId = firstUserId;
                relation.SecondUserId = secondUserId;
            }

            relation.Status = FriendshipStatus.Waiting;
            await _friendshipRepository.SaveAsync();
            return relation;
        }
        else
        {
            throw new InvalidFriendshipActionException("You cannot create a relationship");
        }
    }

    private async Task<Friendship> CreateNewFriendship(Guid userIdGuid, Guid externalUserId)
    {
        var newFriendship = new Friendship
        {
            FirstUserId = userIdGuid,
            SecondUserId = externalUserId,
            Status = FriendshipStatus.Waiting
        };

        await _friendshipRepository.InsertAsync(newFriendship);
        await _friendshipRepository.SaveAsync();
        return newFriendship;
    }
}