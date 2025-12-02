using DeliveryApp.Domain.Common;

namespace DeliveryApp.Domain.Entities;

public class Product : BaseEntity
{
    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = default!;

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;
}
