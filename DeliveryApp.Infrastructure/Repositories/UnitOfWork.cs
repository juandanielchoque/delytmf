using DeliveryApp.Application.Interfaces;
using DeliveryApp.Infrastructure.Persistence;

namespace DeliveryApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IOrderRepository Orders { get; }
    public IRestaurantRepository Restaurants { get; }

    public UnitOfWork(
        AppDbContext context,
        IOrderRepository orders,
        IRestaurantRepository restaurants
    )
    {
        _context = context;
        Orders = orders;
        Restaurants = restaurants;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}