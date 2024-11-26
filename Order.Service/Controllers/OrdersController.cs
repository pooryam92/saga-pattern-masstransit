using MassTransit;
using Message.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Order.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrdersController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrderRequest request)
    {
        var orderId = Guid.NewGuid();
            
        await _publishEndpoint.Publish(new OrderSubmitted
        {
            OrderId = orderId,
            Total = request.Total,
            Email = request.Email
        });

        return Ok(new { OrderId = orderId });
    }
}

public class SubmitOrderRequest
{
    public decimal Total { get; set; }
    public string Email { get; set; }
}