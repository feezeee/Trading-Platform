using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Features.Users.Queries.GetUsersList;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken token)
        {
            try
            {
                var getUsers = new GetUsersListQuery();
                var users = await _mediator.Send(getUsers, token);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
