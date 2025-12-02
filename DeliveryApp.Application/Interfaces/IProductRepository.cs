using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetByRestaurantAsync(Guid restaurantId);
    Task<Product?> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
}
