using Microsoft.Extensions.DependencyInjection;
using Users.Application.Contracts;
using Users.Application.Options;
using Users.Infrastructure.Services;

namespace Users.Application
{
    public static class UsersInfrastructureConfiguration
    {
        public static IServiceCollection AddUsersInfrastructure(this IServiceCollection services, ProductsApiOptions productsApiOptions)
        {
            services.AddScoped<IProductService>(t => new ProductService(productsApiOptions));
            return services;
        }
    }
}
