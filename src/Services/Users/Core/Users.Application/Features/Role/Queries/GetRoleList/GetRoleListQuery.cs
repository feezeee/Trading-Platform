using MediatR;
using Users.Models.Roles;

namespace Users.Application.Features.Role.Queries.GetRoleList
{
    public class GetRoleListQuery : IRequest<List<GetRoleDto>>
    {
    }
}
