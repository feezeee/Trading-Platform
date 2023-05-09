using AutoMapper;
using Messages.BLL.Entities;
using Messages.Models.Message;

namespace Messages.API.AutoMapper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageEntity, GetMessageResponse>();
        }
    }
}
