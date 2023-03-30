using MediatR;

namespace Users.Application.Features.Role.Queries.RoleIsFree
{
    public class RoleIsFreeQuery : IRequest<bool>
    {
        public string Name { get; set; } = string.Empty;
    }
}
