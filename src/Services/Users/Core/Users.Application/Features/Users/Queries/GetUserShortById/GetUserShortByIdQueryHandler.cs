using AutoMapper;
using MediatR;
using Users.Domain.Contracts.Finders;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserShortById
{
    public class GetUserShortByIdQueryHandler : IRequestHandler<GetUserShortByIdQuery, GetUserShortDto?>
    {
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetUserShortByIdQueryHandler(IUserFinder userFinder, IMapper mapper)
        {
            _userFinder = userFinder;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetUserShortDto?> Handle(GetUserShortByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userFinder.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetUserShortDto?>(user);
        }
    }
}
