using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces;

public interface IRestaurantRepository
{
    Task<Restaurant?> GetByIdAsync(Guid id);
    Task<List<Restaurant>> GetAllAsync();
    Task AddAsync(Restaurant restaurant);
}
