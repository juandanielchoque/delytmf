using DeliveryApp.Domain.Common;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public UserRole Role { get; set; }
    public string? FullName { get; set; }
}
