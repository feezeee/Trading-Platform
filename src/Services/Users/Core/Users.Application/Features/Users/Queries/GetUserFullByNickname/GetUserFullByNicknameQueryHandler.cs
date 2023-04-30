using AutoMapper;
using MediatR;
using Users.Domain.Contracts.Finders;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserFullByNickname
{
    public class GetUserFullByNicknameQueryHandler : IRequestHandler<GetUserFullByNicknameQuery, GetUserFullDto?>
    {
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetUserFullByNicknameQueryHandler(IUserFinder userFinder, IMapper mapper)
        {
            _userFinder = userFinder;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetUserFullDto?> Handle(GetUserFullByNicknameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userFinder.GetByNicknameAsync(request.Nickname, cancellationToken);
            return _mapper.Map<GetUserFullDto?>(user);
        }
    }
}
