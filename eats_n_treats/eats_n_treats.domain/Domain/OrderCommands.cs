namespace EatsNTreats.Domain.Orders;

/// <summary>
/// Command to place a new order.
/// </summary>
public record PlaceOrder(string OrderId, string CustomerId);

/// <summary>
/// Command to add an item to an existing order.
/// </summary>
public record AddItemToOrder(string OrderId, string ProductId, int Quantity, decimal Price);
