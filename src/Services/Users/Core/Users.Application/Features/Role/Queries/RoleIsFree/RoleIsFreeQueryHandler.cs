using MediatR;
using Users.Domain.Contracts.Finders;

namespace Users.Application.Features.Role.Queries.RoleIsFree
{
    public class RoleIsFreeQueryHandler : IRequestHandler<RoleIsFreeQuery, bool>
    {
        private readonly IRoleFinder _roleFinder;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public RoleIsFreeQueryHandler(IRoleFinder roleFinder)
        {
            _roleFinder = roleFinder;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<bool> Handle(RoleIsFreeQuery request, CancellationToken cancellationToken)
        {
            var hasAny = await _roleFinder.HasAnyByNameAsync(request.Name, cancellationToken);
            return !hasAny;
        }
    }
}
