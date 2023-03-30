using AutoMapper;
using MediatR;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Models.Exceptions;
using Users.Models.Users;

namespace Users.Application.Features.RoleSetUp.Command
{
    public class RoleSetUpCommandHandler : IRequestHandler<RoleSetUpCommand, GetUserFullDto>
    {
        private readonly IRoleFinder _roleFinder;
        private readonly IUserFinder _userFinder;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public RoleSetUpCommandHandler(IRoleFinder roleFinder, IUserFinder userFinder, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roleFinder = roleFinder;
            _userFinder = userFinder;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetUserFullDto> Handle(RoleSetUpCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _userFinder.GetByIdAsync(request.UserId, cancellationToken);
            if (userEntity is null)
            {
                throw new EntityNotFoundException($"User with id - {request.UserId} doesn't exist");
            }

            userEntity.Roles.Clear();
            
            foreach (var requestRoleId in request.RoleIds)
            {
                var roleEntity = await _roleFinder.GetByIdAsync(requestRoleId, cancellationToken);
                if (roleEntity is null)
                {
                    throw new EntityNotFoundException($"Role with id - {requestRoleId} doesn't exist");
                }
                userEntity.Roles.Add(roleEntity);
            }
            
            _userRepository.Update(userEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GetUserFullDto>(userEntity);
        }
    }
}
