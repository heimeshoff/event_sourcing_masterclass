namespace EatsNTreats.Domain.Orders;

/// <summary>
/// Command to place a new order.
/// </summary>
public record PlaceOrder(string OrderId, string CustomerId);

/// <summary>
/// Command to add an item to an existing order.
/// </summary>
public record AddItemToOrder(string OrderId, string ProductId, int Quantity, decimal Price);

/// <summary>
/// Command to check inventory availability for an order.
/// Emitted by policies when an order is placed.
/// </summary>
public record CheckInventory(string OrderId);

/// <summary>
/// Command to reserve inventory for a specific item in an order.
/// Emitted by policies when items are added to orders.
/// </summary>
public record ReserveInventory(string OrderId, string ProductId, int Quantity);

/// <summary>
/// Command to apply a discount to an order.
/// Emitted by policies based on business rules (e.g., bulk orders, promotions).
/// </summary>
public record ApplyDiscount(string OrderId, decimal DiscountPercentage);
