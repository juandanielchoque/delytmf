namespace DeliveryApp.Application.Interfaces;

public interface IUnitOfWork
{
    IOrderRepository Orders { get; }
    IRestaurantRepository Restaurants { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
