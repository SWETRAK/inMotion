using IMS.Friends.IBLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Friends.API.Controllers;

[Route("friends/lists")]
[ApiController]
public class FriendsListController: ControllerBase
{
    private readonly IFriendsListsService _friendsListsService;
    private readonly ILogger<FriendsListController> _logger;

    public FriendsListController(
        IFriendsListsService friendsListsService, 
        ILogger<FriendsListController> logger
        )
    {
        _friendsListsService = friendsListsService;
        _logger = logger;
    }
    
    
    

}
