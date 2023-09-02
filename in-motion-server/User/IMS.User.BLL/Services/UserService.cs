using AutoMapper;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using IMS.Shared.Models.Exceptions;
using IMS.User.Domain.Entities;
using IMS.User.IBLL.Services;
using IMS.User.IDAL.Repositories;
using IMS.User.Models.Dto.Incoming;
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

    public async Task<IEnumerable<FullUserInfoDto>> GetFullUsersInfoAsync(IList<string> userIds)
    {
        var userIdGuids = userIds.Select(s =>
        {
            if (Guid.TryParse(s, out var userIdGuid))
                throw new InvalidUserGuidStringException($"Invalid parse for id {s}");
            return userIdGuid;
        });

        var rabbitData = await GetBaseUsersInfo(userIds);
        var userMetaData = await _userMetasRepository.GetByExternalUsersIdWithProfileVideoAsync(userIdGuids);
        
        var response = _mapper.Map<IEnumerable<GetBaseUserInfoResponseMessage>, List<FullUserInfoDto>>(
            rabbitData.UsersInfo,
            f => f.AfterMap((src, dest) =>
            {
                dest.ForEach(element =>
                {
                    var userInfo = userMetaData.FirstOrDefault(x => x.Id.ToString().Equals(element.Id));
                    if (userInfo is null) return;
                    element.Bio = userInfo.Bio;
                    element.UserProfileVideo = _mapper.Map<UserProfileVideoDto>(userInfo.ProfileVideo);
                });
            }));
        return response;
    }

    public async Task<UpdatedUserBioDto> UpdateBioAsync(string userId, UpdateUserBioDto updateUserBioDto)
    {
        var userIdGuid = Guid.Parse(userId);

        var userMetas = await _userMetasRepository.GetByExternalUserIdAsync(userIdGuid);
        if (userMetas is null)
        {
            userMetas = new UserMetas
            {
                UserExternalId = userIdGuid,
                Bio = updateUserBioDto.Bio
            };

            await _userMetasRepository.SaveAsync();
        }
        else
        {
            userMetas.Bio = updateUserBioDto.Bio;
        }
        await _userMetasRepository.SaveAsync();

        return new UpdatedUserBioDto
        {
            NewBio = userMetas.Bio
        };
    }
    
    public async Task<UpdatedUserProfileVideoDto> UpdateUserProfileVideo(string userId,
        UpdateUserProfileVideoDto updateUserProfileVideoDto)
    {
        var userIdGuid = Guid.Parse(userId);

        var userMetas = await _userMetasRepository.GetByExternalUserIdWithProfileVideoAsync(userIdGuid);
        if (userMetas is null)
        {
            userMetas = new UserMetas
            {
                UserExternalId = userIdGuid,
                ProfileVideo = new UserProfileVideo
                {
                    AuthorExternalId = userIdGuid,
                    Filename = updateUserProfileVideoDto.Filename,
                    BucketName = updateUserProfileVideoDto.BucketName,
                    BucketLocation = updateUserProfileVideoDto.BucketLocation,
                    ContentType = updateUserProfileVideoDto.ContentType,
                }
            };

        } 
        else if (userMetas.ProfileVideo is null)
        {
            userMetas.ProfileVideo = new UserProfileVideo
            {
                AuthorExternalId = userIdGuid,
                Filename = updateUserProfileVideoDto.Filename,
                BucketName = updateUserProfileVideoDto.BucketName,
                BucketLocation = updateUserProfileVideoDto.BucketLocation,
                ContentType = updateUserProfileVideoDto.ContentType,
            };
        }
        else
        {
            userMetas.ProfileVideo.Filename = userMetas.ProfileVideo.Filename;
            userMetas.ProfileVideo.BucketName = userMetas.ProfileVideo.BucketName;
            userMetas.ProfileVideo.BucketLocation = userMetas.ProfileVideo.BucketLocation;
            userMetas.ProfileVideo.ContentType = userMetas.ProfileVideo.ContentType;
        }

        await _userMetasRepository.SaveAsync();
        return new UpdatedUserProfileVideoDto
        {
            Id = userMetas.ProfileVideo.Id.ToString(),
            Filename = userMetas.ProfileVideo.Filename,
            BucketName = userMetas.ProfileVideo.BucketName,
            BucketLocation = userMetas.ProfileVideo.BucketLocation,
            ContentType = userMetas.ProfileVideo.ContentType
        };
    }

    private async Task<GetBaseUserInfoResponseMessage> GetBaseUserInfo(string userIdString)
    {
        var requestBody = ImsBaseMessage<GetBaseUserInfoMessage>.CreateInstance(new GetBaseUserInfoMessage
        {
            UserId = userIdString
        });

        var responseFromRabbitMq = await _getBaseUserInfoMessageRequestClient.GetResponse<ImsBaseMessage<GetBaseUserInfoResponseMessage>>(requestBody);
        if (responseFromRabbitMq.Message.Error)
            throw new NestedRabbitMqRequestException(responseFromRabbitMq.Message.ErrorMessage);

        return responseFromRabbitMq.Message.Data;
    }

    private async Task<GetBaseUsersInfoResponseMessage> GetBaseUsersInfo(IEnumerable<string> userIdStrings)
    {
        var requestBody = ImsBaseMessage<GetBaseUsersInfoMessage>.CreateInstance(new GetBaseUsersInfoMessage
        {
            UsersId = userIdStrings
        });

        var responseFromRabbitMq = await _getBaseUsersInfoMessageRequestClient.GetResponse<ImsBaseMessage<GetBaseUsersInfoResponseMessage>>(requestBody);
        if (responseFromRabbitMq.Message.Error)
            throw new NestedRabbitMqRequestException(responseFromRabbitMq.Message.ErrorMessage);

        return responseFromRabbitMq.Message.Data;
    }
}
