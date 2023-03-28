using MediatR;
using Users.Models.Tokens;

namespace Users.Application.Features.Users.Commands.AuthorizeUser
{
    public class AuthorizeUserCommand : IRequest<GetTokenDto>
    {
        public string Nickname { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
