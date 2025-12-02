using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Order?> GetByIdAsync(Guid id) =>
        _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .SingleOrDefaultAsync(o => o.Id == id);

    public Task<List<Order>> GetByCustomerAsync(Guid customerId) =>
        _context.Orders
            .Include(o => o.Restaurant)
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task AddAsync(Order order) =>
        await _context.Orders.AddAsync(order);
}
