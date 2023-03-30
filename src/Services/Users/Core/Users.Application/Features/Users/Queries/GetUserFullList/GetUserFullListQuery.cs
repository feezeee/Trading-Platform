using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserFullList
{
    public class GetUserFullListQuery : IRequest<List<GetUserFullDto>>
    {

    }
}
