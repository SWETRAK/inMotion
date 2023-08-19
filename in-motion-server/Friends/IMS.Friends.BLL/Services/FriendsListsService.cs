using AutoMapper;
using IMS.Friends.IBLL.Services;
using IMS.Friends.IDAL.Repositories;
using Microsoft.Extensions.Logging;

namespace IMS.Friends.BLL.Services;

public class FriendsListsService : IFriendsListsService
{
    private readonly IFriendshipRepository _friendshipRepository;
    private readonly ILogger<FriendsListsService> _logger;
    private readonly IMapper _mapper;

    public FriendsListsService(
        IFriendshipRepository friendshipRepository, 
        ILogger<FriendsListsService> logger, 
        IMapper mapper
    )
    {
        _friendshipRepository = friendshipRepository;
        _logger = logger;
        _mapper = mapper;
    }
}