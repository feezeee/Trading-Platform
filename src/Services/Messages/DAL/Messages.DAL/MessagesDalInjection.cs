using Messages.BLL.Contracts.Finders;
using Messages.BLL.Contracts.Repositories;
using Messages.DAL.Configurations;
using Messages.DAL.Context;
using Messages.DAL.Finders;
using Messages.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Messages.DAL
{
    public static class MessagesDalInjection
    {
        public static IServiceCollection AddMessagesDal(this IServiceCollection services, MongoDbConfiguration mongoDbOptions)
        {
            services.AddSingleton<IMessageContext>(t => new MessageContext(mongoDbOptions));
            services.AddSingleton<IMessageFinder, MessageFinder>();
            services.AddSingleton<IMessageRepository, MessageRepository>(); 
            services.AddSingleton<IChatFinder, ChatFinder>();
            services.AddSingleton<IChatRepository, ChatRepository>();

            return services;
        }
    }
}
