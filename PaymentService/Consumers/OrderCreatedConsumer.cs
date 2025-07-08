using MassTransit;
using Shared.Events;

namespace PaymentService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderCreatedConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            Console.WriteLine($"Processing payment for Order {context.Message.OrderId}");
            await Task.Delay(500); // Simulate payment processing
            await _publishEndpoint.Publish(new PaymentCompleted { OrderId = context.Message.OrderId });
        }
    }
}
