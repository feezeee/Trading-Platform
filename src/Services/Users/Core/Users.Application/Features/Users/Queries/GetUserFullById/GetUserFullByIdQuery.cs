using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserFullById
{
    public class GetUserFullByIdQuery : IRequest<GetUserFullDto?>
    {
        public Guid Id { get; set; }
    }
}
