using AutoMapper;
using IMS.Shared.Utils.Parsers;
using IMS.User.Domain.Entities;
using IMS.User.IBLL.Services;
using IMS.User.IDAL.Repositories;
using IMS.User.Models.Dto.Incoming;
using IMS.User.Models.Dto.Outgoing;

namespace IMS.User.BLL.Services;

public class UserVideoPartService: IUserVideoPartService
{
    private readonly IMapper _mapper;
    private readonly IUserMetasRepository _userMetasRepository;

    public UserVideoPartService(IMapper mapper, IUserMetasRepository userMetasRepository)
    {
        _mapper = mapper;
        _userMetasRepository = userMetasRepository;
    }

    public async Task<UpdatedUserProfileVideoDto> UpdateUserProfileVideo(string userId,
        UpdateUserProfileVideoDto updateUserProfileVideoDto)
    {
        var userIdGuid = userId.ParseGuid();

        var userMetas = await _userMetasRepository.GetByExternalUserIdWithProfileVideoAsync(userIdGuid);
        if (userMetas is null)
        {
            userMetas = new UserMetas
            {
                UserExternalId = userIdGuid,
                ProfileVideo = CreateNewUserProfileVideo(userIdGuid, updateUserProfileVideoDto)
            };
            await _userMetasRepository.Add(userMetas);
        } 
        else if (userMetas.ProfileVideo is null)
        {
            userMetas.ProfileVideo = CreateNewUserProfileVideo(userIdGuid, updateUserProfileVideoDto);
        }
        else
        {
            userMetas.ProfileVideo.Filename = userMetas.ProfileVideo.Filename;
            userMetas.ProfileVideo.BucketName = userMetas.ProfileVideo.BucketName;
            userMetas.ProfileVideo.BucketLocation = userMetas.ProfileVideo.BucketLocation;
            userMetas.ProfileVideo.ContentType = userMetas.ProfileVideo.ContentType;
            userMetas.ProfileVideo.LastEditionDate = DateTime.UtcNow;
        }

        await _userMetasRepository.SaveAsync();
        return _mapper.Map<UpdatedUserProfileVideoDto>(userMetas.ProfileVideo);
    }
    
    private static UserProfileVideo CreateNewUserProfileVideo(Guid userIdGuid,
        UpdateUserProfileVideoDto updateUserProfileVideoDto)
    {
        return new UserProfileVideo
        {
            AuthorExternalId = userIdGuid,
            Filename = updateUserProfileVideoDto.Filename,
            BucketName = updateUserProfileVideoDto.BucketName,
            BucketLocation = updateUserProfileVideoDto.BucketLocation,
            ContentType = updateUserProfileVideoDto.ContentType,
        };
    }
}