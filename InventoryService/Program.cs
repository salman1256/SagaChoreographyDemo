using MassTransit;
using InventoryService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PaymentCompletedConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
        cfg.ReceiveEndpoint("inventory-service", e =>
        {
            e.ConfigureConsumer<PaymentCompletedConsumer>(ctx);
        });
    });
});
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
