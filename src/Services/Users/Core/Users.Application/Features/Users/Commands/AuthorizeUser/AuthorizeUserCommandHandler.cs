using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Users.Application.Utils.TokenGenerator;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Domain.Entities;
using Users.Models.Exceptions;
using Users.Models.Options;
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
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly AuthOptions _authOptions;


        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public AuthorizeUserCommandHandler(IMapper mapper, IUserRepository userRepository, IUserFinder userFinder, IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator, IRefreshTokenRepository refreshTokenRepository, IOptions<AuthOptions> authOptions)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userFinder = userFinder;
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _authOptions = authOptions.Value;
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

            foreach (var userRefreshToken in user.RefreshTokens)
            {
                if (userRefreshToken.IsExpired)
                {
                    _refreshTokenRepository.Delete(userRefreshToken);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            // Генерим jwt токен
            var jwtToken = _tokenGenerator.GenerateJwtToken(user);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_authOptions.RefreshTokenLifetime),
                RefreshToken = refreshToken,
                ReplacedByToken = null,
                UserId = user.Id,
            };
            _refreshTokenRepository.Create(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var result = new GetTokenDto();
            result.AccessToken = jwtToken;
            result.RefreshToken = refreshToken;
            result.RefreshTokenExpired = refreshTokenEntity.Expires;
            return result;

        }
    }
}
