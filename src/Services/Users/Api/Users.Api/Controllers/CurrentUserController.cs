using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Users.Api.Models.Response.User;
using Users.Application.Features.Users.Queries.GetUserFullByNickname;
using Users.Application.Features.Users.Queries.GetUserShortByNickname;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/current-user")]
    public class CurrentUserController : ControllerBase
    {
        private readonly ILogger<NicknameController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CurrentUserController(ILogger<NicknameController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("short-information")]
        [Authorize]
        public async Task<ActionResult<GetUserShortResponse>> GetShortCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var token = HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
                if (token is null)
                {
                    _logger.LogError("Some problem with jwt token");
                    return Unauthorized();
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodeToken = tokenHandler.ReadJwtToken(token);
                var claims = decodeToken.Claims;
                var nicknameClaim = claims.First(t => t.Type == ClaimTypes.NameIdentifier);
                var res = await _mediator.Send(new GetUserShortByNicknameQuery
                {
                    Nickname = nicknameClaim.Value
                }, cancellationToken);
                if (res is null)
                {
                    _logger.LogError("Some problems");
                    return BadRequest();
                }
                return Ok(_mapper.Map<GetUserShortResponse>(res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
        
        [HttpGet("full-information")]
        [Authorize]
        public async Task<ActionResult<GetUserFullResponse?>> GetFullCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var token = HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
                if (token is null)
                {
                    _logger.LogError("Some problem with jwt token");
                    return Unauthorized();
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodeToken = tokenHandler.ReadJwtToken(token);
                var claims = decodeToken.Claims;
                var nicknameClaim = claims.First(t => t.Type == ClaimTypes.NameIdentifier);

                var res = await _mediator.Send(new GetUserFullByNicknameQuery
                {
                    Nickname = nicknameClaim.Value
                }, cancellationToken);
                if (res is null)
                {
                    _logger.LogError("Some problems");
                    return BadRequest();
                }
                return Ok(_mapper.Map<GetUserFullResponse>(res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
