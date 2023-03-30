using AutoMapper;
using MediatR;
using Users.Domain.Contracts.Finders;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserFullList
{
    public class GetUserFullListQueryHandler : IRequestHandler<GetUserFullListQuery, List<GetUserFullDto>>
    {
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetUserFullListQueryHandler(IUserFinder userFinder, IMapper mapper)
        {
            _userFinder = userFinder;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<List<GetUserFullDto>> Handle(GetUserFullListQuery request, CancellationToken cancellationToken)
        {
            var users = await _userFinder.GetAllAsync(cancellationToken);
            return _mapper.Map<List<GetUserFullDto>>(users);
        }
    }
}
