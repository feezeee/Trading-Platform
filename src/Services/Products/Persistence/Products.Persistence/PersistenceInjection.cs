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
            services.AddSingleton<IProductContext>(t => new ProductContext(mongoDbOptions));
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IProductFinder, ProductFinder>();
            return services;
        }
    }
}
