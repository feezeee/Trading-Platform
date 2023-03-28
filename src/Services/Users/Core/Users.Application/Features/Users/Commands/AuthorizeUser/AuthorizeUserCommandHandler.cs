using AutoMapper;
using MediatR;
using Users.Application.Utils.TokenGenerator;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Models.Exceptions;
using Users.Models.Tokens;

namespace Users.Application.Features.Users.Commands.AuthorizeUser
{
    public class AuthorizeUserCommandHandler : IRequestHandler<AuthorizeUserCommand, GetTokenDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserFinder _userFinder;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;


        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public AuthorizeUserCommandHandler(IMapper mapper, IUserRepository userRepository, IUserFinder userFinder, IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userFinder = userFinder;
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetTokenDto> Handle(AuthorizeUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userFinder.GetByNicknameAndPasswordAsync(request.Nickname, request.Password, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException("Bad {Nickname} or {Password}");
            }

            // Генерим jwt токен
            var jwtToken = _tokenGenerator.GenerateJwtToken(user);
            var result = new GetTokenDto();
            result.AccessToken = jwtToken;
            return result;

        }
    }
}
