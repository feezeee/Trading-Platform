using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Users.Api.Models.Request.RefreshToken;
using Users.Api.Models.Response.Token;
using Users.Application.Features.Users.Commands.RefreshToken;
using Users.Models.Exceptions;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/refresh-token")]
    public class RefreshTokenController : ControllerBase
    {
        private readonly ILogger<RefreshTokenController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RefreshTokenController(ILogger<RefreshTokenController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<GetTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest refreshToken, CancellationToken token = default)
        {
            try
            {
                var accessToken = new JwtSecurityToken(jwtEncodedString: refreshToken.AccessToken);
                var nicknameClaim = accessToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
                if (nicknameClaim is null)
                {
                    _logger.LogError("Not valid access token");
                    return Unauthorized();
                }

                var nickname = nicknameClaim.Value;
                var command = new RefreshTokenCommand
                {
                    Nickname = nickname,
                    RefreshToken = refreshToken.RefreshToken
                };
                var newToken = await _mediator.Send(command, token);


                return Ok(_mapper.Map<GetTokenResponse>(newToken));
            }
            catch (RefreshTokenIsExpiredException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
            catch (RefreshTokenNotFoundException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
            catch (UserNotFoundException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
