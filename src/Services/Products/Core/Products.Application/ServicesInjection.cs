using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Contracts;
using Products.Application.Services;

namespace Products.Application
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
