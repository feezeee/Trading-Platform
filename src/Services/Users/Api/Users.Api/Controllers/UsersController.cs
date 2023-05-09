using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Models.Request.User;
using Users.Api.Models.Response.User;
using Users.Application.Features.Users.Commands.DeleteUser;
using Users.Application.Features.Users.Commands.UpdateUser;
using Users.Application.Features.Users.Queries.GetUserFullById;
using Users.Application.Features.Users.Queries.GetUserFullList;
using Users.Application.Features.Users.Queries.GetUserShortById;
using Users.Application.Features.Users.Queries.GetUserShortList;
using Users.Models.Exceptions;
using Users.Models.Users;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("short-information")]
        public async Task<ActionResult<List<GetUserShortResponse>>> GetUsersShort(CancellationToken token = default)
        {
            try
            {
                var getUsers = new GetUserShortListQuery();
                var users = await _mediator.Send(getUsers, token);


                return Ok(_mapper.Map<List<GetUserShortResponse>>(users));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("full-information")]
        public async Task<ActionResult<List<GetUserFullResponse>>> GetUsersFull(CancellationToken token = default)
        {
            try
            {
                var getUsers = new GetUserFullListQuery();
                var users = await _mediator.Send(getUsers, token);


                return Ok(_mapper.Map<List<GetUserFullResponse>>(users));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }


        [HttpGet("short-information/{id}")]
        public async Task<ActionResult<GetUserShortResponse?>> GetUsersShort([FromRoute] Guid id, CancellationToken token = default)
        {
            try
            {
                var query = new GetUserShortByIdQuery
                {
                    Id = id,
                };
                var user = await _mediator.Send(query, token);


                return Ok(_mapper.Map<GetUserShortResponse?>(user));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("full-information/{id}")]
        public async Task<ActionResult<GetUserFullResponse?>> GetUsersFull([FromRoute] Guid id, CancellationToken token = default)
        {
            try
            {
                var query = new GetUserFullByIdQuery
                {
                    Id = id,
                };
                var user = await _mediator.Send(query, token);


                return Ok(_mapper.Map<GetUserFullResponse?>(user));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }


        [HttpPut]
        public async Task<ActionResult<GetUserShortResponse>> Update([FromBody] UpdateUserRequest request,
            CancellationToken token = default)
        {
            try
            {
                var command = _mapper.Map<UpdateUserCommand>(request);
                var user = await _mediator.Send(command, token);
                return Ok(_mapper.Map<GetUserShortDto>(user));
            }
            catch (EntityAlreadyExistException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token = default)
        {
            try
            {
                var command = new DeleteUserCommand
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
