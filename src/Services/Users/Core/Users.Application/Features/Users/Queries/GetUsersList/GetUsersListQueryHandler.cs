using AutoMapper;
using MediatR;
using Users.Application.Features.Users.Queries.GetUsersList;
using Users.Domain.Contracts.Findres;
using Users.Models.Users;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, List<GetUserDto>>
    {
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IUserFinder userFinder, IMapper mapper)
        {
            _userFinder = userFinder;
            _mapper = mapper;
        }

        public async Task<List<GetUserDto>> Handle(
            GetUsersListQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _userFinder.GetUsersAsync(cancellationToken);
            var mappedUsers = _mapper.Map<List<GetUserDto>>(users);
            return mappedUsers;
        }
    }
}
