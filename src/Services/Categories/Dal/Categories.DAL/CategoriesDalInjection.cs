using Categories.BLL.Contracts.Finders;
using Categories.BLL.Contracts.Repositories;
using Categories.BLL.Contracts.UnitOfWork;
using Categories.DAL.Context;
using Categories.DAL.Finders;
using Categories.DAL.Options;
using Categories.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Categories.DAL
{
    public static class CategoriesDalInjection
    {
        public static IServiceCollection AddCategoriesDal(this IServiceCollection services, CategoryContextOptions categoryContextOptions)
        {
            services.AddSqlServer<CategoryContext>(categoryContextOptions.ConnectionString);

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryFinder, CategoryFinder>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}