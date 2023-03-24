using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Contracts.Findres;
using Users.Persistence.Finders;

namespace Users.Infrastructure.Configurations.Dependencies
{
    public static partial class Configuration
    {
        public static IServiceCollection AddFinders(this IServiceCollection services)
        {
            services.AddScoped<IUserFinder, UserFinder>();
            return services;
        }
    }
}
