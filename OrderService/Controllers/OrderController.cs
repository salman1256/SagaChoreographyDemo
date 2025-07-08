using Microsoft.AspNetCore.Mvc;
using MassTransit;
using OrderService.Data;
using OrderService.Models;
using Shared.Events;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(OrderDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

              order.Id = Guid.NewGuid();
            order.Status = "Created";
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await _publishEndpoint.Publish(new OrderCreated { OrderId = order.Id, Amount = order.Amount });

            return Ok(order);
        }
    }
}
