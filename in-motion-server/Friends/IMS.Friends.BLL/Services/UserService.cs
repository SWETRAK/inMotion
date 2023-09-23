using AutoMapper;
using IMS.Friends.IBLL.Services;
using IMS.Friends.Models.Exceptions;
using IMS.Friends.Models.Models;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Users;
using IMS.Shared.Models.Exceptions;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Friends.BLL.Services;

public class UserService: IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IRequestClient<ImsBaseMessage<GetUsersInfoMessage>> _usersRequestClient;
    private readonly IRequestClient<ImsBaseMessage<GetUserInfoMessage>> _userRequestClient;

    public UserService(
        ILogger<UserService> logger, 
        IMapper mapper, 
        IRequestClient<ImsBaseMessage<GetUsersInfoMessage>> usersRequestClient, 
        IRequestClient<ImsBaseMessage<GetUserInfoMessage>> userRequestClient
    )
    {
        _logger = logger;
        _mapper = mapper;
        _usersRequestClient = usersRequestClient;
        _userRequestClient = userRequestClient;
    }
    
    public async Task<IEnumerable<UserInfo>> GetUsersByIdsArray(IEnumerable<Guid> idArray)
    {
        var idStrings = idArray.Select(x => x.ToString());
        
        var requestData = new ImsBaseMessage<GetUsersInfoMessage>
        {
            Data = new GetUsersInfoMessage
            {
                UserIds = idStrings
            }
        };
        
        var response = await _usersRequestClient.GetResponse<ImsBaseMessage<GetUsersInfoResponseMessage>>(requestData);
        if (!response.Message.Data.UsersInfo.Any()) throw new RabbitMqException("Data is missing");

        _logger.LogInformation("Users data downloaded via RabbitMQ from other service");
        var result = _mapper.Map<IEnumerable<UserInfo>>(response.Message.Data.UsersInfo);
        return result;
    }

    public async Task<UserInfo> GetUserByIdArray(Guid userId)
    {
        var request = new ImsBaseMessage<GetUserInfoMessage>
        {
            Data = new GetUserInfoMessage
            {
                UserId = userId.ToString()
            }
        };
        
        var response = await _userRequestClient.GetResponse<ImsBaseMessage<GetUserInfoResponseMessage>>(request);
        if (response.Message.Data is null) throw new RabbitMqException("Data is missing");
        
        _logger.LogInformation("User data downloaded via RabbitMQ from other service");
        var result = _mapper.Map<UserInfo>(response.Message.Data);
        return result;
    }
}