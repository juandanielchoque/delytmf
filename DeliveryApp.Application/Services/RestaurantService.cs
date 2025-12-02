using DeliveryApp.Application.DTOs;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Application.Models.Requests;
using DeliveryApp.Application.Models.Responses;
using DeliveryApp.Domain.Entities;


namespace DeliveryApp.Application.Services;

public class RestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uow;

    public RestaurantService(
        IRestaurantRepository restaurantRepository,
        IProductRepository productRepository,
        IUnitOfWork uow)
    {
        _restaurantRepository = restaurantRepository;
        _productRepository = productRepository;
        _uow = uow;
    }

    public async Task<List<RestaurantDto>> GetAllAsync()
    {
        var restaurants = await _restaurantRepository.GetAllAsync();
        return restaurants.Select(r => new RestaurantDto
        {
            Id = r.Id,
            Name = r.Name,
            Address = r.Address
        }).ToList();
    }

    public async Task<List<ProductDto>> GetMenuAsync(Guid restaurantId)
    {
        var products = await _productRepository.GetByRestaurantAsync(restaurantId);
        return products.Where(p => p.IsAvailable)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();
    }

    public async Task<RestaurantDto> CreateRestaurantAsync(string name, string address)
    {
        var restaurant = new Restaurant
        {
            Name = name,
            Address = address
        };

        await _restaurantRepository.AddAsync(restaurant);
        await _uow.SaveChangesAsync();

        return new RestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address
        };
    }
    
    public async Task<ProductResponse> AddProductAsync(Guid restaurantId, CreateProductRequest request)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);

        if (restaurant is null)
            throw new Exception("Restaurant not found");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            RestaurantId = restaurantId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };

        await _productRepository.AddAsync(product);
        await _uow.SaveChangesAsync();

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            IsAvailable = product.IsAvailable
        };
    }

    
}
