using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
