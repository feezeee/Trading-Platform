﻿namespace Products.Models.Products
{
    public class GetProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal? Price { get; set; }
    }
}
