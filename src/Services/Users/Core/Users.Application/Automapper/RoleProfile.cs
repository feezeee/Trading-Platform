using AutoMapper;
using Users.Application.Features.Role.Commands.CreateRole;
using Users.Domain.Entities;
using Users.Models.Roles;

namespace Users.Application.Automapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleEntity, GetRoleDto>();
            CreateMap<CreateRoleCommand, RoleEntity>();
        }
    }
}
