using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.RoleSetUp.Command
{
    public class RoleSetUpCommand : IRequest<GetUserFullDto>
    {
        public Guid UserId { get; set; }

        public List<Guid> RoleIds { get; set; } = new List<Guid>();
    }
}
