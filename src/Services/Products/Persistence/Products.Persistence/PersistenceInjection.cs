using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Contracts.Finders;
using Products.Domain.Contracts.Repositories;
using Products.Persistence.MongoDatabase.Configurations;
using Products.Persistence.MongoDatabase.Data;
using Products.Persistence.MongoDatabase.Finders;
using Products.Persistence.MongoDatabase.Repositories;

namespace Products.Persistence
{
    public static class PersistenceInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, MongoDbConfiguration mongoDbOptions)
        {
            services.AddScoped<IProductContext>(t => new ProductContext(mongoDbOptions));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductFinder, ProductFinder>();
            return services;
        }
    }
}
