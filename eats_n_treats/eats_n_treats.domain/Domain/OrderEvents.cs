namespace EatsNTreats.Domain.Orders;

/// <summary>
/// Event raised when an order is placed.
/// </summary>
public record OrderPlaced(string OrderId, string CustomerId);

/// <summary>
/// Event raised when an item is added to an order.
/// </summary>
public record ItemAddedToOrder(string OrderId, string ProductId, int Quantity, decimal Price);

/// <summary>
/// Event raised when an item cannot be added to an order (business rule violation).
/// </summary>
public record ItemCannotBeAdded(string OrderId, string ProductId, string Reason);
