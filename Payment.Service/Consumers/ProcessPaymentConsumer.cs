using MassTransit;
using Message.Contracts;

namespace Inventory.Service.Consumers;

public class ProcessPaymentConsumer : IConsumer<ProcessPayment>
{
    private readonly ILogger<ProcessPaymentConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProcessPaymentConsumer(ILogger<ProcessPaymentConsumer> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<ProcessPayment> context)
    {
        _logger.LogInformation("Processing payment for order: {OrderId}", context.Message.OrderId);

        // Simulate payment processing
        await Task.Delay(1000);

        // 90% success rate
        if (Random.Shared.Next(100) < 90)
        {
            await _publishEndpoint.Publish(new PaymentProcessed
            {
                OrderId = context.Message.OrderId,
                PaymentIntentId = $"pi_{Guid.NewGuid():N}"
            });
        }
        else
        {
            await _publishEndpoint.Publish(new OrderFailed
            {
                OrderId = context.Message.OrderId,
                Reason = "Payment failed"
            });
        }
    }
}
