using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Contracts.Repositories;
using Users.Persistence.Repositories;

namespace Users.Infrastructure.Configurations.Dependencies
{
    public static partial class Configuration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
