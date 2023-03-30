using MediatR;
using Users.Models.Roles;

namespace Users.Application.Features.Role.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<GetRoleDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set;} = string.Empty;
    }
}
