using DeliveryApp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Application.Models.Requests;
using DeliveryApp.Application.Models.Responses;


namespace DeliveryApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly RestaurantService _restaurantService;

    public RestaurantsController(RestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetRestaurants()
    {
        var result = await _restaurantService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}/menu")]
    [AllowAnonymous]
    public async Task<IActionResult> GetMenu(Guid id)
    {
        var result = await _restaurantService.GetMenuAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _restaurantService.CreateRestaurantAsync(request.Name, request.Address);
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("{restaurantId}/products")]
    public async Task<IActionResult> AddProduct(Guid restaurantId, [FromBody] CreateProductRequest request)
    {
        var result = await _restaurantService.AddProductAsync(restaurantId, request);
        return Ok(result);
    }

}

public class CreateRestaurantRequest
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
}
