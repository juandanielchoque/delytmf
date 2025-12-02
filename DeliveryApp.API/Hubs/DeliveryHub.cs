using Microsoft.AspNetCore.SignalR;

namespace DeliveryApp.API.Hubs;

public class DeliveryHub : Hub
{
    public async Task JoinOrderGroup(string orderId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, orderId);
    }
    
    public async Task UpdateDriverLocation(string orderId, double lat, double lng)
    {
        await Clients.Group(orderId).SendAsync("DriverLocationUpdated", new 
        {
            OrderId = orderId,
            Latitude = lat,
            Longitude = lng,
            Timestamp = DateTime.UtcNow
        });
    }
}