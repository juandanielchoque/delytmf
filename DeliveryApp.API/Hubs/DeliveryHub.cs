using Microsoft.AspNetCore.SignalR;

namespace DeliveryApp.API.Hubs;

public class DeliveryHub : Hub
{
    public async Task SendLocation(Guid orderId, double latitude, double longitude)
    {
        await Clients.Group(orderId.ToString()).SendAsync("ReceiveLocation", latitude, longitude);
    }
    
    public async Task JoinOrder(Guid orderId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, orderId.ToString());
    }
}