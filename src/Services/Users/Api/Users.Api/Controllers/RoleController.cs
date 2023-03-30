using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Users.Api.Models.Request.Role;
using Users.Api.Models.Response.Role;
using Users.Application.Features.Role.Commands.CreateRole;
using Users.Application.Features.Role.Commands.DeleteRole;
using Users.Application.Features.Role.Queries.GetRoleById;
using Users.Application.Features.Role.Queries.GetRoleList;
using Users.Application.Features.Role.Queries.RoleIsFree;
using Users.Models.Exceptions;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RoleController(ILogger<RoleController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<List<GetRoleResponse>>> Get(CancellationToken token = default)
        {
            try
            {
                var command = new GetRoleListQuery();
                var roles = await _mediator.Send(command, token);

                return Ok(_mapper.Map<List<GetRoleResponse>>(roles));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("is-free")]
        public async Task<IActionResult> CheckRole([FromQuery][BindRequired][Required] string role, CancellationToken token = default)
        {
            try
            {
                var command = new RoleIsFreeQuery
                {
                    Name = role
                };

                var isFree = await _mediator.Send(command, token);

                return Ok(new
                {
                    is_free = isFree
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetRoleResponse?>> GetById([FromRoute] Guid id,
            CancellationToken token = default)
        {
            try
            {
                var command = new GetRoleByIdQuery
                {
                    Id = id
                };
                var role = await _mediator.Send(command, token);
                return Ok(_mapper.Map<GetRoleResponse?>(role));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetRoleResponse>> Create(
            [FromBody] CreateRoleRequest request,
            CancellationToken token = default)
        {
            try
            {
                var command = _mapper.Map<CreateRoleCommand>(request);
                var roleDto = await _mediator.Send(command, token);
                var mappedRole = _mapper.Map<GetRoleResponse>(roleDto);
                return CreatedAtAction(nameof(GetById), new { id = mappedRole.Id }, mappedRole);
            }
            catch (EntityAlreadyExistException e)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token = default)
        {
            try
            {
                var command = new DeleteRoleCommand
                {
                    Id = id
                };
                await _mediator.Send(command, token);
                return Ok();
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
