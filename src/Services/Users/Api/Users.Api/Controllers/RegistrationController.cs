using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Models.Request.Registration;
using Users.Application.Features.Users.Commands.RegisterUser;
using Users.Models.Exceptions;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RegistrationController(ILogger<RegistrationController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registrate(
            [FromBody] RegistrateUserRequest user,
            CancellationToken token = default)
        {
            try
            {
                var command = _mapper.Map<RegisterUserCommand>(user);
                var getUserShortDto = await _mediator.Send(command, token);
                return StatusCode(201);
            }
            catch (UserAlreadyExistException e)
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
