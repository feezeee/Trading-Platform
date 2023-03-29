using MediatR;
using Users.Domain.Contracts.Finders;

namespace Users.Application.Features.Users.Queries.NicknameIsFree
{
    public class NicknameIsFreeQueryHandler : IRequestHandler<NicknameIsFreeQuery, bool>
    {
        private readonly IUserFinder _userFinder;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public NicknameIsFreeQueryHandler(IUserFinder userFinder)
        {
            _userFinder = userFinder;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<bool> Handle(NicknameIsFreeQuery request, CancellationToken cancellationToken)
        {
            return !await _userFinder.HasAnyByNicknameAsync(request.Nickname, cancellationToken);
        }
    }
}
