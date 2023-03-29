using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Users.Application.Features.Users.Queries.NicknameIsFree;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/nickname")]
    public class NicknameController : ControllerBase
    {
        private readonly ILogger<NicknameController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public NicknameController(ILogger<NicknameController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("is-free")]
        public async Task<IActionResult> NicknameIsFree([FromQuery][BindRequired] string nickname, CancellationToken token = default)
        {
            try
            {
                if (nickname.Length < 1)
                {
                    _logger.LogError("Some problems with nickname");
                    return BadRequest();
                }

                var isFree = await _mediator.Send(new NicknameIsFreeQuery { Nickname = nickname }, token);
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

    }
}
