using System.Security.Claims;
using DeliveryApp.Application.DTOs;
using DeliveryApp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = GetUserId();
        var result = await _orderService.CreateOrderAsync(userId, request);
        return Ok(result);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = GetUserId();
        var result = await _orderService.GetCustomerOrdersAsync(userId);
        return Ok(result);
    }

    private Guid GetUserId()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)
                  ?? User.FindFirstValue("sub");

        if (sub == null)
            throw new InvalidOperationException("User id not found in token.");

        return Guid.Parse(sub);
    }
}
