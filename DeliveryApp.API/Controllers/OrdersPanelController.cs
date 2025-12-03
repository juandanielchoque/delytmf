using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersPanelController : ControllerBase
{
    private readonly IUnitOfWork _unit;

    public OrdersPanelController(IUnitOfWork unit)
    {
        _unit = unit;
    }

    // ADMIN - ver todos los pedidos
    [HttpGet("all")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _unit.Orders.GetAllWithRelationsAsync();

        return Ok(orders);
    }

    // DRIVER - ver pedidos pendientes
    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingOrders()
    {
        var orders = await _unit.Orders.GetPendingWithRelationsAsync();

        return Ok(orders);
    }

    // DRIVER - aceptar / tomar pedido
    [HttpPut("{id}/assign")]
    public async Task<IActionResult> Assign(Guid id)
    {
        var order = await _unit.Orders.GetByIdAsync(id);
        if (order == null) return NotFound();

        order.Status = OrderStatus.Accepted;
        await _unit.Orders.UpdateAsync(order);
        await _unit.SaveChangesAsync();

        return Ok(order);
    }

    // DRIVER - marcar como entregado
    [HttpPut("{id}/delivered")]
    public async Task<IActionResult> Deliver(Guid id)
    {
        var order = await _unit.Orders.GetByIdAsync(id);
        if (order == null) return NotFound();

        order.Status = OrderStatus.Delivered;
        await _unit.Orders.UpdateAsync(order);
        await _unit.SaveChangesAsync();

        return Ok(order);
    }
    
}