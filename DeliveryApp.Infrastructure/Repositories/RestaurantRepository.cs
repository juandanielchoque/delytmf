using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _context;

    public RestaurantRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Restaurant?> GetByIdAsync(Guid id) =>
        _context.Restaurants.FindAsync(id).AsTask();

    public Task<List<Restaurant>> GetAllAsync() =>
        _context.Restaurants.Where(r => r.IsActive).ToListAsync();

    public async Task AddAsync(Restaurant restaurant) =>
        await _context.Restaurants.AddAsync(restaurant);
}
