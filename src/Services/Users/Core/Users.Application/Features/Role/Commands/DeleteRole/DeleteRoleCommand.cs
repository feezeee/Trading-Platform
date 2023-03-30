using MediatR;

namespace Users.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
