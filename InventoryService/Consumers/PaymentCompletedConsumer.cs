using MassTransit;
using Shared.Events;

namespace InventoryService.Consumers
{
    public class PaymentCompletedConsumer : IConsumer<PaymentCompleted>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentCompletedConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<PaymentCompleted> context)
        {
            Console.WriteLine($"Reserving inventory for Order {context.Message.OrderId}");
            await Task.Delay(300); // Simulate inventory allocation
            await _publishEndpoint.Publish(new InventoryReserved { OrderId = context.Message.OrderId });
        }
    }
}
