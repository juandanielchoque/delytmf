using DeliveryApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<RestaurantService>();
        services.AddScoped<OrderService>();

        return services;
    }
}
