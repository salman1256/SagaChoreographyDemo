namespace OrderService.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
