using AutoMapper;
using Users.Application.Features.Users.Commands.RegisterUser;
using Users.Application.Features.Users.Commands.UpdateUser;
using Users.Domain.Entities;
using Users.Models.Users;

namespace Users.Application.Automapper
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserEntity, GetUserShortDto>();
            CreateMap<UserEntity, GetUserFullDto>();
            CreateMap<RegisterUserCommand, UserEntity>();
            CreateMap<UpdateUserCommand, UserEntity>();

        }
    }
}
