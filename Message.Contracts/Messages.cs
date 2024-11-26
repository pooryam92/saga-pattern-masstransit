namespace Message.Contracts;

public record OrderSubmitted
{
    public Guid OrderId { get; init; }
    public decimal Total { get; init; }
    public string Email { get; init; }
}

public record ProcessPayment
{
    public Guid OrderId { get; init; }
    public decimal Amount { get; init; }
}

public record PaymentProcessed
{
    public Guid OrderId { get; init; }
    public string PaymentIntentId { get; init; }
}

public record ReserveInventory
{
    public Guid OrderId { get; init; }
}

public record InventoryReserved
{
    public Guid OrderId { get; init; }
}

public record RefundPayment
{
    public Guid OrderId { get; init; }
    public decimal Amount { get; init; }
}

public record OrderConfirmed
{
    public Guid OrderId { get; init; }
}

public record OrderFailed
{
    public Guid OrderId { get; init; }
    public string Reason { get; init; }
}
