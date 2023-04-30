using AutoMapper;
using MediatR;
using Users.Domain.Contracts.Finders;
using Users.Models.Users;

namespace Users.Application.Features.Users.Queries.GetUserShortByNickname
{
    public class GetUserShortByNicknameQueryHandler : IRequestHandler<GetUserShortByNicknameQuery, GetUserShortDto?>
    {
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetUserShortByNicknameQueryHandler(IUserFinder userFinder, IMapper mapper)
        {
            _userFinder = userFinder;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetUserShortDto?> Handle(GetUserShortByNicknameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userFinder.GetByNicknameAsync(request.Nickname, cancellationToken);
            return _mapper.Map<GetUserShortDto?>(user);
        }
    }
}
