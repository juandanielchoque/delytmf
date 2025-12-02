using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.DTOs;

public class OrderItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderRequest
{
    public Guid RestaurantId { get; set; }
    public string DeliveryAddress { get; set; } = default!;
    public List<OrderItemRequest> Items { get; set; } = new();
}

public class OrderDto
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Total { get; set; }
    public string RestaurantName { get; set; } = default!;
    public string DeliveryAddress { get; set; } = default!;
}
