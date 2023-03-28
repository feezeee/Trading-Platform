using AutoMapper;
using Users.Api.Models.Request.Authorization;
using Users.Application.Features.Users.Commands.AuthorizeUser;

namespace Users.Api.Automapper
{
    public class AuthorizeProfile : Profile
    {
        public AuthorizeProfile()
        {
            CreateMap<AuthorizeUserRequest, AuthorizeUserCommand>();
        }
    }
}
