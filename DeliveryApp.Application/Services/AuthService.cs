using System.Security.Cryptography;
using System.Text;
using DeliveryApp.Application.DTOs;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;
    private readonly IJwtTokenGenerator _jwt;

    public AuthService(
        IUserRepository userRepository,
        IUnitOfWork uow,
        IJwtTokenGenerator jwt)
    {
        _userRepository = userRepository;
        _uow = uow;
        _jwt = jwt;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, UserRole role)
    {
        var existing = await _userRepository.GetByEmailAsync(request.Email);
        if (existing != null)
            throw new InvalidOperationException("Email already registered.");

        var user = new User
        {
            Email = request.Email,
            FullName = request.FullName,
            PasswordHash = HashPassword(request.Password),
            Role = role
        };

        await _userRepository.AddAsync(user);
        await _uow.SaveChangesAsync();

        var token = _jwt.GenerateToken(user);

        return new AuthResponse
        {
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = token
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            throw new InvalidOperationException("Invalid credentials.");

        var hash = HashPassword(request.Password);
        if (!string.Equals(hash, user.PasswordHash, StringComparison.Ordinal))
            throw new InvalidOperationException("Invalid credentials.");

        var token = _jwt.GenerateToken(user);

        return new AuthResponse
        {
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = token
        };
    }

    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}
