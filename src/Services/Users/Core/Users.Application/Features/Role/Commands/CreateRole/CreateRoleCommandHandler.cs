using AutoMapper;
using MediatR;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Domain.Entities;
using Users.Models.Exceptions;
using Users.Models.Roles;

namespace Users.Application.Features.Role.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, GetRoleDto>
    {
        private readonly IRoleFinder _roleFinder;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CreateRoleCommandHandler(IRoleFinder roleFinder, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roleFinder = roleFinder;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetRoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var hasAny = await _roleFinder.HasAnyByNameAsync(request.Name, cancellationToken);
            if (hasAny)
            {
                throw new EntityAlreadyExistException($"Role with name - {request.Name} already exists");
            }

            var roleEntity = _mapper.Map<RoleEntity>(request);
            roleEntity.Id = Guid.NewGuid();
            _roleRepository.Create(roleEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetRoleDto>(roleEntity);
        }
    }
}
