using EatsNTreats.Domain.Orders;

namespace EatsNTreats.Tests.Orders;

/// <summary>
/// Semantic test helpers for Order policy tests.
/// Factory methods make tests easier to read and decouple tests from implementation.
/// </summary>
public partial class OrderPolicyTests
{
    // Event factory methods (reused from OrderTests)
    protected OrderPlaced OrderPlaced(string orderId, string customerId) =>
        new(orderId, customerId);

    protected ItemAddedToOrder ItemAddedToOrder(string orderId, string productId, int quantity, decimal price) =>
        new(orderId, productId, quantity, price);

    // Command factory methods for policies
    protected CheckInventory CheckInventory(string orderId) =>
        new(orderId);

    protected ReserveInventory ReserveInventory(string orderId, string productId, int quantity) =>
        new(orderId, productId, quantity);

    protected ApplyDiscount ApplyDiscount(string orderId, decimal discountPercentage) =>
        new(orderId, discountPercentage);

    // Semantic identifiers - easier to reason about "Marco" than "99A83F47..."
    protected string Marco() => "customer-marco-123";
    protected string Tina() => "customer-tina-456";
    protected string Order1() => "order-1";
    protected string Order2() => "order-2";
    protected string Pizza() => "product-pizza";
    protected string Burger() => "product-burger";
    protected string Salad() => "product-salad";
}
