namespace Messages.DAL.Configurations
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ChatCollectionName { get; set; } = string.Empty;
        public string MessageCollectionName { get; set; } = string.Empty;
    }
}
