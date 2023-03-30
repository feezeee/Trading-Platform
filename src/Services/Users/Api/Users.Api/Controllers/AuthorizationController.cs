using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Models.Request.Authorization;
using Users.Api.Models.Response.Token;
using Users.Application.Features.Users.Commands.AuthorizeUser;
using Users.Models.Exceptions;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthorizationController(ILogger<AuthorizationController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<GetTokenResponse>> Authorize(
            [FromBody] AuthorizeUserRequest authorizeUserRequest,
            CancellationToken token = default)
        {
            try
            {
                var command = _mapper.Map<AuthorizeUserCommand>(authorizeUserRequest);

                var tokenDto = await _mediator.Send(command, token);
                return Ok(_mapper.Map<GetTokenResponse>(tokenDto));
            }
            catch (EntityNotFoundException e)
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
