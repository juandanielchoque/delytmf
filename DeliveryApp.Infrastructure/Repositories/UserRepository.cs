using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public Task<User?> GetByEmailAsync(string email) =>
        _context.Users.SingleOrDefaultAsync(u => u.Email == email);

    public Task<User?> GetByIdAsync(Guid id) =>
        _context.Users.FindAsync(id).AsTask();

    public async Task AddAsync(User user) =>
        await _context.Users.AddAsync(user);
}
