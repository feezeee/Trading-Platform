using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<List<GetUserDto>>
    {     

    }
}
