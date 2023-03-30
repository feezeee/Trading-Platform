using AutoMapper;
using Users.Api.Models.Request.RoleSetUp;
using Users.Application.Features.RoleSetUp.Command;

namespace Users.Api.Automapper
{
    public class RoleSetUpProfile : Profile
    {
        public RoleSetUpProfile()
        {
            CreateMap<RoleSetUpRequest, RoleSetUpCommand>();
        }
    }
}
