using System.Text;
using DeliveryApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("orders/csv")]
    public async Task<IActionResult> GetOrdersCsv()
    {
        var orders = await _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.Customer)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        var sb = new StringBuilder();
        sb.AppendLine("OrderId,CustomerEmail,RestaurantName,Total,Status,CreatedAt");

        foreach (var o in orders)
        {
            sb.AppendLine($"{o.Id},{o.Customer.Email},{o.Restaurant.Name},{o.Total},{o.Status},{o.CreatedAt:O}");
        }

        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        return File(bytes, "text/csv", "orders_report.csv");
    }
}
