using AutoMapper;
using MediatR;
using Users.Domain.Contracts.Finders;
using Users.Models.Roles;

namespace Users.Application.Features.Role.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, GetRoleDto?>
    {
        private readonly IRoleFinder _roleFinder;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetRoleByIdQueryHandler(IRoleFinder roleFinder, IMapper mapper)
        {
            _roleFinder = roleFinder;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetRoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var roleEntity = await _roleFinder.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetRoleDto?>(roleEntity);

        }
    }
}
