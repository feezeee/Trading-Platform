using AutoMapper;
using Users.Api.Models.Response.User;
using Users.Models.Users;

namespace Users.Api.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GetUserShortDto, GetUserShortResponse>();
            CreateMap<GetUserFullDto, GetUserFullResponse>();
        }
    }
}
