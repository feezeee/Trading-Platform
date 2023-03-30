using MediatR;
using Users.Models.Roles;

namespace Users.Application.Features.Role.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<GetRoleDto>
    {
        public string Name { get; set; } = string.Empty;
    }
}
