using AutoMapper;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.Shared.Models.Exceptions;
using IMS.User.IBLL.Services;
using IMS.User.IDAL.Repositories;
using IMS.User.Models.Dto.Outgoing;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMS.User.BLL.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IUserMetasRepository _userMetasRepository;
    private readonly IRequestClient<GetBaseUserInfoMessage> _getBaseUserInfoMessageRequestClient;
    private readonly IRequestClient<GetBaseUsersInfoMessage> _getBaseUsersInfoMessageRequestClient;

    public UserService(
        IRequestClient<GetBaseUserInfoMessage> getBaseUserInfoMessageRequestClient,
        IUserMetasRepository userMetasRepository,
        IMapper mapper,
        ILogger<UserService> logger, 
        IRequestClient<GetBaseUsersInfoMessage> getBaseUsersInfoMessageRequestClient)
    {
        _getBaseUserInfoMessageRequestClient = getBaseUserInfoMessageRequestClient;
        _userMetasRepository = userMetasRepository;
        _mapper = mapper;
        _logger = logger;
        _getBaseUsersInfoMessageRequestClient = getBaseUsersInfoMessageRequestClient;
    }

    public async Task<FullUserInfoDto> GetFullUserInfoAsync(string userIdString)
    {
        if (Guid.TryParse(userIdString, out var userIdGuid))
            throw new InvalidUserGuidStringException();

        var rabbitData = await GetBaseUserInfo(userIdString);
        var userMetaData = await _userMetasRepository.GetByExternalUserIdWithProfileVideoAsync(userIdGuid);

        if (userMetaData is null)
            throw new Exception();

        var response = _mapper.Map<GetBaseUserInfoResponseMessage, FullUserInfoDto>(
            rabbitData,
            f => f.AfterMap((src, dest) =>
            {
                dest.Bio = userMetaData.Bio;
                dest.UserProfileVideo = _mapper.Map<UserProfileVideoDto>(userMetaData.ProfileVideo);
            }));

        return response;
    }

    public async Task<IEnumerable<FullUserInfoDto>> GetFullUsersInfoAsync(IEnumerable<string> userId)
    {
        var userIdGuids = userId.Select(s =>
        {
            if (Guid.TryParse(s, out var userIdGuid))
                throw new InvalidUserGuidStringException($"Invalid parse for id {s}");
            return userIdGuid;
        });

        return new List<FullUserInfoDto>();
    }

    private async Task<GetBaseUserInfoResponseMessage> GetBaseUserInfo(string userIdString)
    {
        var requestBody = ImsBaseMessage<GetBaseUserInfoMessage>.CreateInstance(new GetBaseUserInfoMessage
        {
            UserId = userIdString
        });

        var responseFromRabbitMq = await _getBaseUserInfoMessageRequestClient.GetResponse<ImsBaseMessage<GetBaseUserInfoResponseMessage>>(requestBody);
        if (responseFromRabbitMq.Message.Error is false)
            throw new Exception();

        return responseFromRabbitMq.Message.Data;
    }
}
