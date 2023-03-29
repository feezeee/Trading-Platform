using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Users.Application.Utils.PasswordEncrypter;
using Users.Application.Utils.PasswordEncryptor;
using Users.Application.Utils.TokenGenerator;

namespace Users.Application
{
    public static class UsersApplicationConfiguration
    {
        public static IServiceCollection AddUsersApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(t => t.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IPasswordEncryptor, PasswordEncryptor>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
