using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Utils.PasswordEncrypter;
using Users.Application.Utils.TokenGenerator;

namespace Users.Application
{
    public static class UsersApplicationConfiguration
    {
        public static IServiceCollection AddUsersApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(t => t.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IPasswordEncrypter, PasswordEncrypter>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
