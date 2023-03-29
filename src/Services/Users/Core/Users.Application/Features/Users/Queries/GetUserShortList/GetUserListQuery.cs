using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserShortList
{
    public class GetUserListQuery : IRequest<List<GetUserShortDto>>
    {

    }
}
