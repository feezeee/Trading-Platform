using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<List<GetUserDto>>
    {     

    }
}
