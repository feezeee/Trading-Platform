using MediatR;
using Users.Models.Tokens;

namespace Users.Application.Features.Users.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<GetTokenDto>
    {
        public string Nickname { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}
