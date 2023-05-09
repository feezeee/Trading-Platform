using Messages.BLL.Contracts.Finders;
using Messages.BLL.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Messages.API.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageFinder _messageFinder;
        private readonly IMessageRepository _messageRepository;
        private readonly IChatFinder _chatFinder;
        private readonly IChatRepository _chatRepository;

        public MessageController(ILogger<MessageController> logger, IMessageFinder messageFinder, IMessageRepository messageRepository, IChatFinder chatFinder, IChatRepository chatRepository)
        {
            _logger = logger;
            _messageFinder = messageFinder;
            _messageRepository = messageRepository;
            _chatFinder = chatFinder;
            _chatRepository = chatRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage(CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetChatsForUser([FromQuery][BindRequired][Required] Guid userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var chats = await _chatFinder.GetAllForUserIdAsync(userId, cancellationToken);
                foreach(var chat in chats)
                {
                    chat.Messages = (await _messageFinder.GetAllForChatIdAsync(chat.Id, cancellationToken)).OrderByDescending(t => t.CreatedDate).ToList();
                }
                chats = chats.OrderByDescending(t => t.Messages.Max(t => t.CreatedDate)).ToList();
                return Ok(chats);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
