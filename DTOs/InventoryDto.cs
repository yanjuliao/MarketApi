using MarketApi.Models;

namespace MarketApi.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int QuantityInStock { get; set; }
    }
}
