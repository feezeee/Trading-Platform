using MediatR;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserShortById
{
    public class GetUserShortByIdQuery : IRequest<GetUserShortDto?>
    {
        public Guid Id { get; set; }
    }
}
