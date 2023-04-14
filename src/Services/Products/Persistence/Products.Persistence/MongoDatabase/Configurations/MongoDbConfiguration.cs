namespace Products.Persistence.MongoDatabase.Configurations
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ProductCollectionName { get; set; } = string.Empty;
    }
}
