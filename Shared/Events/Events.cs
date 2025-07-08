namespace Shared.Events
{
    public class OrderCreated
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }

    public class PaymentCompleted
    {
        public Guid OrderId { get; set; }
    }

    public class InventoryReserved
    {
        public Guid OrderId { get; set; }
    }
}
