using Products.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Products.Persistence.MongoDatabase.Configurations.EntityConfig
{
    internal static partial class Configuration
    {
        internal static void SetUp(this BsonClassMap<ProductEntity> bsonClassMap)
        {

            bsonClassMap.AutoMap();
            bsonClassMap.MapIdMember(c => c.Id);

        }
    }
}
