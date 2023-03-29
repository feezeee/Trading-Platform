using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<GetUserShortDto>
    {

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
