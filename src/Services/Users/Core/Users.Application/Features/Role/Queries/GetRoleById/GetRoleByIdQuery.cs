using MediatR;
using Users.Models.Roles;

namespace Users.Application.Features.Role.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<GetRoleDto?>
    {
        public Guid Id { get; set; }
    }
}
