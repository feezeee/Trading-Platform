using AutoMapper;
using Users.Api.Models.Request.User;
using Users.Api.Models.Response.User;
using Users.Application.Features.Users.Commands.UpdateUser;
using Users.Models.Users;

namespace Users.Api.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GetUserShortDto, GetUserShortResponse>();
            CreateMap<GetUserFullDto, GetUserFullResponse>();
            CreateMap<UpdateUserRequest, UpdateUserCommand>();
        }
    }
}
