using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Product>> GetByRestaurantAsync(Guid restaurantId) =>
        _context.Products.Where(p => p.RestaurantId == restaurantId).ToListAsync();

    public Task<Product?> GetByIdAsync(Guid id) =>
        _context.Products.FindAsync(id).AsTask();

    public async Task AddAsync(Product product) =>
        await _context.Products.AddAsync(product);
}
