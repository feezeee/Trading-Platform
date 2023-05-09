using Messages.BLL.Entities;
using MongoDB.Bson.Serialization;

namespace Messages.DAL.Configurations
{
    internal static partial class Configuration
    {
        internal static void SetUp(this BsonClassMap<MessageEntity> bsonClassMap)
        {
            bsonClassMap.AutoMap();
            bsonClassMap.MapIdMember(c => c.Id);
        }
    }
}
