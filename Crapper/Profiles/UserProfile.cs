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
            //todo: fix bullshit
            CreateMap<User, UserDto>().ForMember("Subscribers", options =>
            {
                options.MapFrom(user => user.SubscribersCount);
            })
                .ForMember("Subscriptions", options =>
            {
                options.MapFrom(user => user.SubscriptionsCount);
            })
                .ForMember("Posts", options =>
                {
                    options.MapFrom(user => user.Posts.Count);
                })
            ;
        }
    }
}
