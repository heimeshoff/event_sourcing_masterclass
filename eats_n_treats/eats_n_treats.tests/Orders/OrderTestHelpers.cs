using EatsNTreats.Domain.Orders;

namespace EatsNTreats.Tests.Orders;

/// <summary>
/// Semantic test helpers for Order tests.
/// Factory methods make tests easier to read and decouple tests from implementation.
/// </summary>
public partial class OrderTests
{
    // Command factory methods
    protected PlaceOrder PlaceOrder(string orderId, string customerId) =>
        new(orderId, customerId);

    protected AddItemToOrder AddItem(string orderId, string productId, int quantity, decimal price) =>
        new(orderId, productId, quantity, price);

    // Event factory methods
    protected OrderPlaced OrderPlaced(string orderId, string customerId) =>
        new(orderId, customerId);

    protected ItemAddedToOrder ItemAdded(string orderId, string productId, int quantity, decimal price) =>
        new(orderId, productId, quantity, price);

    protected ItemCannotBeAdded ItemCannotBeAdded(string orderId, string productId, string reason) =>
        new(orderId, productId, reason);

    // Semantic identifiers - easier to reason about "Marco" than "99A83F47..."
    protected string Marco() => "customer-marco-123";
    protected string Tina() => "customer-tina-456";
    protected string Order1() => "order-1";
    protected string Order2() => "order-2";
    protected string Pizza() => "product-pizza";
    protected string Burger() => "product-burger";
    protected string Salad() => "product-salad";
}
