using MediatR;

namespace Users.Application.Features.Users.Queries.NicknameIsFree
{
    public class NicknameIsFreeQuery : IRequest<bool>
    {
        public string Nickname { get; set; } = string.Empty;
    }
}
