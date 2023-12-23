using AutoMapper;
using IMS.Friends.Domain.Entities;
using IMS.Friends.Models.Dto.Outgoing;

namespace IMS.Friends.Models.Mappers;

public class FriendshipProfile: Profile
{
    public FriendshipProfile()
    {
        CreateMap<Friendship, AcceptedFriendshipDto>()
            .ForMember(afd => afd.ExternalUser, opt => opt.Ignore())
            .ForMember(afd => afd.ExternalUserId, opt => opt.Ignore())
            .ForMember(afd => afd.Id, opt => opt.MapFrom(p => p.Id))
            .ForMember(afd => afd.FriendsSince, opt => opt.MapFrom(p => p.LastModificationDate));
        
        CreateMap<Friendship, RequestFriendshipDto>()
            .ForMember(rfd => rfd.ExternalUser, opt => opt.Ignore())
            .ForMember(rfd => rfd.ExternalUserId, opt => opt.Ignore())
            .ForMember(rfd => rfd.Id, opt => opt.MapFrom(p => p.Id))
            .ForMember(rfd => rfd.Requested, opt => opt.MapFrom(p => p.LastModificationDate));
        
        CreateMap<Friendship, InvitationFriendshipDto>()
            .ForMember(ifd => ifd.ExternalUser, opt => opt.Ignore())
            .ForMember(ifd => ifd.ExternalUserId, opt => opt.Ignore())
            .ForMember(ifd => ifd.Id, opt => opt.MapFrom(p => p.Id))
            .ForMember(ifd => ifd.Invited, opt => opt.MapFrom(p => p.LastModificationDate));
        
        CreateMap<Friendship, RejectedFriendshipDto>()
            .ForMember(ifd => ifd.ExternalUser, opt => opt.Ignore())
            .ForMember(ifd => ifd.ExternalUserId, opt => opt.Ignore())
            .ForMember(ifd => ifd.Id, opt => opt.MapFrom(p => p.Id))
            .ForMember(ifd => ifd.Rejected, opt => opt.MapFrom(p => p.LastModificationDate));
    }
}