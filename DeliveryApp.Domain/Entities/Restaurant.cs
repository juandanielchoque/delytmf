using DeliveryApp.Domain.Common;

namespace DeliveryApp.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public bool IsActive { get; set; } = true;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
