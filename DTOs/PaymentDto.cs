using MarketApi.Models;

namespace MarketApi.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
