using DeliveryApp.Application.DTOs;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uow;

    public OrderService(
        IOrderRepository orderRepository,
        IRestaurantRepository restaurantRepository,
        IProductRepository productRepository,
        IUnitOfWork uow)
    {
        _orderRepository = orderRepository;
        _restaurantRepository = restaurantRepository;
        _productRepository = productRepository;
        _uow = uow;
    }

    public async Task<OrderDto> CreateOrderAsync(Guid customerId, CreateOrderRequest request)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId)
                         ?? throw new InvalidOperationException("Restaurant not found.");

        var order = new Order
        {
            CustomerId = customerId,
            RestaurantId = restaurant.Id,
            DeliveryAddress = request.DeliveryAddress,
            Status = OrderStatus.Pending
        };

        decimal total = 0;

        foreach (var itemReq in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemReq.ProductId)
                          ?? throw new InvalidOperationException("Product not found.");

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemReq.Quantity,
                UnitPrice = product.Price
            };

            order.Items.Add(orderItem);
            total += product.Price * itemReq.Quantity;
        }

        order.Total = total;

        await _orderRepository.AddAsync(order);
        await _uow.SaveChangesAsync();

        return new OrderDto
        {
            Id = order.Id,
            Status = order.Status,
            Total = order.Total,
            RestaurantName = restaurant.Name,
            DeliveryAddress = order.DeliveryAddress
        };
    }

    public async Task<List<OrderDto>> GetCustomerOrdersAsync(Guid customerId)
    {
        var orders = await _orderRepository.GetByCustomerAsync(customerId);

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            Status = o.Status,
            Total = o.Total,
            RestaurantName = o.Restaurant.Name,
            DeliveryAddress = o.DeliveryAddress
        }).ToList();
    }
}
