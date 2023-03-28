using AutoMapper;
using Users.Domain.Entities;
using Users.Models.Users;

namespace Users.Application.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, GetUserDto>();

        }
    }
}
