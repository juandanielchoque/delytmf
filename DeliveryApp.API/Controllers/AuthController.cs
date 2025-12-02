using DeliveryApp.Application.DTOs;
using DeliveryApp.Application.Services;
using DeliveryApp.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register/customer")]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.RegisterAsync(request, UserRole.Customer);
        return Ok(result);
    }

    [HttpPost("register/driver")]
    public async Task<IActionResult> RegisterDriver([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.RegisterAsync(request, UserRole.Driver);
        return Ok(result);
    }
    
    [HttpPost("register/admin")]
    public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request, UserRole.Admin);
        return Ok(result);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.LoginAsync(request);
        return Ok(result);
    }
}
