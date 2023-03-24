using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Contracts;

namespace Users.Infrastructure.Configurations.Dependencies
{
    public static partial class Configuration
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
