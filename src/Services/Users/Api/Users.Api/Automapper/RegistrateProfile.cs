using AutoMapper;
using Users.Api.Models.Request.Registration;
using Users.Application.Features.Users.Commands.RegisterUser;

namespace Users.Api.Automapper
{
    public class RegistrateProfile : Profile
    {
        public RegistrateProfile()
        {
            CreateMap<RegistrateUserRequest, RegisterUserCommand>();
        }
    }
}
