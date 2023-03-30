using AutoMapper;
using MediatR;
using Users.Domain.Contracts.Finders;
using Users.Models.Roles;

namespace Users.Application.Features.Role.Queries.GetRoleList
{
    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, List<GetRoleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleFinder _roleFinder;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetRoleListQueryHandler(IMapper mapper, IRoleFinder roleFinder)
        {
            _mapper = mapper;
            _roleFinder = roleFinder;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<List<GetRoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var rolesEntityList = await _roleFinder.GetAllAsync(cancellationToken);
            return _mapper.Map<List<GetRoleDto>>(rolesEntityList);
        }
    }
}
