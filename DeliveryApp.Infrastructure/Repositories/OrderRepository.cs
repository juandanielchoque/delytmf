using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using DeliveryApp.Domain.Enums;


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
    public async Task<List<Order>> GetAllAsync() =>
        await _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.Items).ThenInclude(i => i.Product)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<List<Order>> GetPendingAsync() =>
        await _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.Items).ThenInclude(i => i.Product)
            .Where(o => o.Status == OrderStatus.Pending)

            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        return Task.CompletedTask;
    }

}
