using AutoMapper;
using MediatR;
using Users.Domain.Contracts.Finders;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<GetUserDto>>
    {
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IUserFinder userFinder, IMapper mapper)
        {
            _userFinder = userFinder;
            _mapper = mapper;
        }

        public async Task<List<GetUserDto>> Handle(
            GetUserListQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _userFinder.GetAllAsync(cancellationToken);
            var mappedUsers = _mapper.Map<List<GetUserDto>>(users);
            return mappedUsers;
        }
    }
}
