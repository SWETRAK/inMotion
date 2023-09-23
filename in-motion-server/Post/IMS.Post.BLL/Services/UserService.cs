using AutoMapper;
using IMS.Post.IBLL.Services;
using IMS.Post.Models.Models.Author;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.Users;
using IMS.Shared.Models.Exceptions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.Post.BLL.Services;

public class UserService: IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IRequestClient<ImsBaseMessage<GetUsersInfoMessage>> _usersRequestClient;
    private readonly IRequestClient<ImsBaseMessage<GetUserInfoMessage>> _userRequestClient;

    public UserService(ILogger<UserService> logger,
        IRequestClient<ImsBaseMessage<GetUsersInfoMessage>> usersRequestClient,
        IMapper mapper,
        IRequestClient<ImsBaseMessage<GetUserInfoMessage>> userRequestClient)
    {
        _logger = logger;
        _usersRequestClient = usersRequestClient;
        _mapper = mapper;
        _userRequestClient = userRequestClient;
    }

    public async Task<IEnumerable<AuthorInfo>> GetUsersByIdsArray(IEnumerable<Guid> idArray)
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
        var result = _mapper.Map<IEnumerable<AuthorInfo>>(response.Message.Data.UsersInfo);
        return result;
    }

    public async Task<AuthorInfo> GetUserById(Guid userId)
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
        var result = _mapper.Map<AuthorInfo>(response.Message.Data);
        return result;
    }
}
