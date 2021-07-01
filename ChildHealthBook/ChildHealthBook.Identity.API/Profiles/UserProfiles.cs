using AutoMapper;
using ChildHealthBook.Common.Identity.DTOs;
using Common.Identity.Setup;
using System.Collections.Generic;

namespace ChildHealthBook.Identity.API.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<ParentRegisterDTO, User>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(item => item.UserCredentials.UserName))
                .ForMember(dest => dest.Email, src => src.MapFrom(item => item.UserCredentials.Email));
            CreateMap<UserRegisterDTO, User>();

            CreateMap<User, UserData>();
        }
    }
}
