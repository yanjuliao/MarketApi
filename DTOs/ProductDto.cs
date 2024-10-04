﻿namespace MarketApi.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryDto Category { get; set; }
    }

    public class UpdateProductDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
    }

    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
