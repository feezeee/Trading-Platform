using AutoMapper;
using MediatR;
using Users.Application.Utils.PasswordEncryptor;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Domain.Entities;
using Users.Models.Exceptions;
using Users.Models.Users;

namespace Users.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, GetUserShortDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFinder _userFinder;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordEncryptor _passwordEncryptor;
        private readonly IRoleFinder _roleFinder;
        private readonly IRoleRepository _roleRepository;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public RegisterUserCommandHandler(IUserRepository userRepository, IUserFinder userFinder, IUnitOfWork unitOfWork, IMapper mapper, IPasswordEncryptor passwordEncryptor, IRoleFinder roleFinder, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userFinder = userFinder;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordEncryptor = passwordEncryptor;
            _roleFinder = roleFinder;
            _roleRepository = roleRepository;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetUserShortDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existUser = await _userFinder.HasAnyByNicknameAsync(request.Nickname, cancellationToken);
            if (existUser)
            {
                throw new UserAlreadyExistException($"User with nickname - {request.Nickname} already exists");
            }

            var userRole = await _roleFinder.GetByNameAsync("User", cancellationToken);
            if (userRole is null)
            {
                userRole = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "User"
                };
                _roleRepository.Create(userRole);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            var passwordHash = _passwordEncryptor.GeneratePassword(request.Password);

            var userEntity = _mapper.Map<UserEntity>(request);
            userEntity.Id = Guid.NewGuid();
            userEntity.Roles.Add(userRole);
            userEntity.Password = passwordHash;

            _userRepository.Create(userEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var getUserShortDto = _mapper.Map<GetUserShortDto>(userEntity);
            return getUserShortDto;
        }
    }
}
