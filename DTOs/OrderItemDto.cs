using MarketApi.Models;

namespace MarketApi.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
