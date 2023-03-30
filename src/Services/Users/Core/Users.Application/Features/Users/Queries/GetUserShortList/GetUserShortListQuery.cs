using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserShortList
{
    public class GetUserShortListQuery : IRequest<List<GetUserShortDto>>
    {

    }
}
