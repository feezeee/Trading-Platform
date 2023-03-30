using AutoMapper;
using MediatR;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Models.Exceptions;
using Users.Models.Roles;

namespace Users.Application.Features.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, GetRoleDto>
    {
        private readonly IRoleFinder _roleFinder;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UpdateRoleCommandHandler(IRoleFinder roleFinder, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<GetRoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var existRoleEntity = await _roleFinder.GetByIdAsync(request.Id, cancellationToken);

            if (existRoleEntity is null)
            {
                throw new EntityNotFoundException($"Role with id - {request.Id} doesn't exist");
            }

            if (!string.Equals(existRoleEntity.Name, request.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                var hasAny = await _roleFinder.HasAnyByNameAsync(request.Name, cancellationToken);
                if (hasAny)
                {
                    throw new EntityAlreadyExistException($"Role with name - {request.Name} already exists");
                }
            }

            _mapper.Map(request, existRoleEntity);

            _roleRepository.Update(existRoleEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetRoleDto>(existRoleEntity);
        }
    }
}
