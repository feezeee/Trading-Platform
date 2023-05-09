using AutoMapper;
using Messages.BLL.Entities;
using Messages.Models.Chat;

namespace Messages.API.AutoMapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatEntity, GetChatResponse>();
        }
    }
}
