using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<GetUserShortDto>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string? ProfileImageUrl { get; set; }
    }
}
