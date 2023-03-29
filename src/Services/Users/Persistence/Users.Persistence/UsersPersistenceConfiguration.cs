using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Persistence.Finders;
using Users.Persistence.Options;
using Users.Persistence.Repositories;

namespace Users.Persistence
{
    public static class UsersPersistenceConfiguration
    {
        public static IServiceCollection AddUsersPersistence(this IServiceCollection services,
            UserContextOptions userContextOptions)
        {
            services.AddSqlServer<UserContext>(userContextOptions.ConnectionString);

            services.AddScoped<IUserFinder, UserFinder>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRefreshTokenFinder, RefreshTokenFinder>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IRoleFinder, RoleFinder>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
