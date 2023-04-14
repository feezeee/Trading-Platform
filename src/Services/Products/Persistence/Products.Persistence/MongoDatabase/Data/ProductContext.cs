using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Products.Domain.Entities;
using Products.Persistence.MongoDatabase.Configurations;
using Products.Persistence.MongoDatabase.Configurations.EntityConfig;

namespace Products.Persistence.MongoDatabase.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(MongoDbConfiguration mongoDbOptions)
        {
            var client = new MongoClient(mongoDbOptions.ConnectionString);
            var database = client.GetDatabase(mongoDbOptions.DatabaseName);

            BsonClassMap.RegisterClassMap<ProductEntity>(cm => cm.SetUp());

            Products = database.GetCollection<ProductEntity>(mongoDbOptions.ProductCollectionName);
        }

        public IMongoCollection<ProductEntity> Products { get; }
    }
}
