namespace DeliveryApp.Application.DTOs;

public class OrderPanelDto
{
    public Guid Id { get; set; }
    public string RestaurantName { get; set; }
    public string CustomerName { get; set; }
    public string DriverName { get; set; }
    public string DeliveryAddress { get; set; }
    public string Status { get; set; }
    public decimal Total { get; set; }
}
