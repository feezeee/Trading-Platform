using AutoMapper;
using MediatR;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Models.Exceptions;
using Users.Models.Users;

namespace Users.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GetUserShortDto>
    {
        private readonly IUserFinder _userFinder;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UpdateUserCommandHandler(IUserFinder userFinder, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userFinder = userFinder;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetUserShortDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existUserEntity = await _userFinder.GetByIdAsync(request.Id, cancellationToken);
            if (existUserEntity is null)
            {
                throw new EntityNotFoundException($"User with id - {request.Id} doesn't exist");
            }

            if (!string.Equals(existUserEntity.Nickname, request.Nickname, StringComparison.CurrentCultureIgnoreCase))
            {
                var hasAny = await _userFinder.HasAnyByNicknameAsync(request.Nickname, cancellationToken);
                if (hasAny)
                {
                    throw new EntityAlreadyExistException($"User with nickname - {request.Nickname} already exists");
                }
            }

            _mapper.Map(request, existUserEntity);

            _userRepository.Update(existUserEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetUserShortDto>(existUserEntity);
        }
    }
}
