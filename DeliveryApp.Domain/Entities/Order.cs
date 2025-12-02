using DeliveryApp.Domain.Common;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Domain.Entities;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }
    public User Customer { get; set; } = default!;

    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = default!;

    public Guid? DriverId { get; set; }
    public User? Driver { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal Total { get; set; }
    public string DeliveryAddress { get; set; } = default!;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
