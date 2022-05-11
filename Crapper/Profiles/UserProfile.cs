using AutoMapper;
using Crapper.DTOs.User;
using Crapper.Models;

namespace Crapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
