﻿namespace Products.Domain.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal? Price { get; set; }
    }
}
