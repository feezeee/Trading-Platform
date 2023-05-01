using Categories.BLL.Contracts.Services;
using Categories.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Categories.BLL
{
    public static class CategoriesDalInjection
    {   
        public static IServiceCollection AddCategoriesBll(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}