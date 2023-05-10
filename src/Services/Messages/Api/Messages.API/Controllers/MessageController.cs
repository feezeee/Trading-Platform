using Messages.BLL.Contracts.Finders;
using Messages.BLL.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Messages.BLL.Entities;
using Messages.Models.Chat;
using Messages.Models.Message;
using Microsoft.AspNetCore.SignalR;
using System.Threading;

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
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _chatHub;

        public MessageController(ILogger<MessageController> logger, IMessageFinder messageFinder, IMessageRepository messageRepository, IChatFinder chatFinder, IChatRepository chatRepository, IMapper mapper, IHubContext<ChatHub> chatHub)
        {
            _logger = logger;
            _messageFinder = messageFinder;
            _messageRepository = messageRepository;
            _chatFinder = chatFinder;
            _chatRepository = chatRepository;
            _mapper = mapper;
            _chatHub = chatHub;
        }

        [HttpPost("new-message")]
        public async Task<IActionResult> SendNewMessage([FromBody] PostNewMessageRequest newMessageRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var chatExist = await _chatFinder.GetChatBetweenUsersAsync(new List<Guid>
                {
                    newMessageRequest.FromUserId,
                    newMessageRequest.ToUserId,
                }, cancellationToken);

                if (chatExist is null)
                {
                    chatExist = new ChatEntity
                    {
                        Id = Guid.NewGuid(),
                        Messages = new List<MessageEntity>(),
                        Users = new List<Guid>
                        {
                            newMessageRequest.FromUserId,
                            newMessageRequest.ToUserId,
                        }
                    };
                    await _chatRepository.CreateAsync(chatExist, cancellationToken);
                }

                var newMessage = new MessageEntity
                {
                    Id = Guid.NewGuid(),
                    ChatId = chatExist.Id,
                    CreatedDate = DateTime.Now,
                    Message = newMessageRequest.Message,
                    UserId = newMessageRequest.FromUserId
                };
                await _messageRepository.CreateAsync(newMessage, cancellationToken);
                await _chatHub.Clients.All.SendAsync("Notify", "update chats", cancellationToken);
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
                return Ok(_mapper.Map<List<GetChatResponse>>(chats));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
