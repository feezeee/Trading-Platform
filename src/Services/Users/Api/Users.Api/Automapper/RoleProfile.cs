using AutoMapper;
using Users.Api.Models.Request.Role;
using Users.Api.Models.Response.Role;
using Users.Application.Features.Role.Commands.CreateRole;
using Users.Models.Roles;

namespace Users.Api.Automapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<GetRoleDto, GetRoleResponse>();
            CreateMap<CreateRoleRequest, CreateRoleCommand>();
        }
    }
}
