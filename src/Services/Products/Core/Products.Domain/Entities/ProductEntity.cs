namespace Products.Domain.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        
        public List<string> ImageUrls { get; set; } = new List<string>();

        public List<string> PhoneNumbers { get; set; } = new List<string>();

        public List<Guid> CategoryIdList { get; set; } = new List<Guid>();

        public decimal? Price { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
