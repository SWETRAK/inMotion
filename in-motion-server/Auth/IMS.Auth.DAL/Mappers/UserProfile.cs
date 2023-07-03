using AutoMapper; 
using IMS.Auth.Models.Dto;
using IMS.Shared.Domain.Entities.User;

namespace IMS.Auth.DAL.Mappers;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserInfoDto>()
            .ForMember(dto => dto.Email, opt => opt.MapFrom(u => u.Email))
            .ForMember(dto => dto.Nickname, opt => opt.MapFrom(u => u.Nickname))
            .ForMember(dto => dto.Id, opt => opt.MapFrom(u => u.Id.ToString()))
            .ForMember(dto => dto.Token, opt => opt.Ignore());
    }
}