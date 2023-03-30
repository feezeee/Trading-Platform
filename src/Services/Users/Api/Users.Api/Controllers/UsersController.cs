using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Models.Response.User;
using Users.Application.Features.Users.Queries.GetUserFullList;
using Users.Application.Features.Users.Queries.GetUserShortList;

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
    }
}
