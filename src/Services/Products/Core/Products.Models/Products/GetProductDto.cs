namespace Products.Models.Products
{
    public class GetProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<string> PhoneNumbers { get; set; } = new List<string>();

        public decimal? Price { get; set; }
    }
}
