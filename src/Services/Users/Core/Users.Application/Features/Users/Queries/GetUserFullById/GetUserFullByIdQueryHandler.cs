using AutoMapper;
using MediatR;
using Users.Domain.Contracts.Finders;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserFullById
{
    public class GetUserFullByIdQueryHandler : IRequestHandler<GetUserFullByIdQuery, GetUserFullDto?>
    {
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetUserFullByIdQueryHandler(IUserFinder userFinder, IMapper mapper)
        {
            _userFinder = userFinder;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetUserFullDto?> Handle(GetUserFullByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userFinder.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetUserFullDto?>(user);
        }
    }
}
