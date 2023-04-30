using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserShortByNickname
{
    public class GetUserShortByNicknameQuery : IRequest<GetUserShortDto?>
    {
        public string Nickname { get; set; } = string.Empty;
    }
}
