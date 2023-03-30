using MediatR;
using Microsoft.Extensions.Options;
using Users.Application.Utils.TokenGenerator;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Models.Exceptions;
using Users.Models.Options;
using Users.Models.Tokens;

namespace Users.Application.Features.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, GetTokenDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenFinder _refreshTokenFinder;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserFinder _userFinder;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly AuthOptions _authOptions;


        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IRefreshTokenFinder refreshTokenFinder, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IUserFinder userFinder, ITokenGenerator tokenGenerator, IOptions<AuthOptions> authOptions)
        {
            _unitOfWork = unitOfWork;
            _refreshTokenFinder = refreshTokenFinder;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _userFinder = userFinder;
            _tokenGenerator = tokenGenerator;
            _authOptions = authOptions.Value;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<GetTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // Пытаемся найти юзера по никнейму
            var user = await _userFinder.GetByNicknameAsync(request.Nickname, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException($"User with nickname - {request.Nickname} doesn't exist");
            }
            var refreshTokenEntity = user.RefreshTokens.FirstOrDefault(t => t.RefreshToken == request.RefreshToken);
            if (refreshTokenEntity is null)
            {
                throw new RefreshTokenNotFoundException("Refresh token not found");
            }

            if (refreshTokenEntity.IsExpired)
            {
                _refreshTokenRepository.Delete(refreshTokenEntity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                throw new RefreshTokenIsExpiredException("Refresh token is expired");
            }


            var jwtToken = _tokenGenerator.GenerateJwtToken(user);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();
            refreshTokenEntity.ReplacedByToken = refreshTokenEntity.RefreshToken;
            refreshTokenEntity.RefreshToken = refreshToken;
            refreshTokenEntity.Expires = DateTime.UtcNow.AddMinutes(_authOptions.RefreshTokenLifetime);


            _refreshTokenRepository.Update(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new GetTokenDto
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken,
                RefreshTokenExpired = refreshTokenEntity.Expires
            };
        }
    }
}
