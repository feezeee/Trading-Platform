using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserFullByNickname
{
    public class GetUserFullByNicknameQuery : IRequest<GetUserFullDto?>
    {
        public string Nickname { get; set; } = string.Empty;
    }
}
