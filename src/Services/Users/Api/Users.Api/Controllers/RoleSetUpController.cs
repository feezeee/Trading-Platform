using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Models.Request.RoleSetUp;
using Users.Api.Models.Response.User;
using Users.Application.Features.RoleSetUp.Command;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/roles/set-up")]
    public class RoleSetUpController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleSetUpController> _logger;
        private readonly IMediator _mediator;

        public RoleSetUpController(IMapper mapper, ILogger<RoleSetUpController> logger, IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<GetUserFullResponse>> SetUpRoles([FromBody] RoleSetUpRequest request,
            CancellationToken token = default)
        {
            try
            {
                var command = _mapper.Map<RoleSetUpCommand>(request);
                var userDto = await _mediator.Send(command, token);
                return Ok(_mapper.Map<GetUserFullResponse>(userDto));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        } 
    }
}
