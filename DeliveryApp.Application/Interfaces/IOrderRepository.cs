using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<List<Order>> GetByCustomerAsync(Guid customerId);
    Task AddAsync(Order order);
}
