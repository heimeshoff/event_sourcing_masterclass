namespace EatsNTreats.Domain.Orders;

/// <summary>
/// Order aggregate that protects invariants around ordering.
/// Ensures transactional consistency for all order operations.
/// </summary>
public class Order
{
    private readonly Action<object> _publish;
    private readonly Order_state _state;

    public Order(Order_state state, Action<object> publish)
    {
        _publish = publish;
        _state = state;
    }

    /// <summary>
    /// Places a new order for a customer.
    /// </summary>
    public void place(string orderId, string customerId)
    {
        if (_state.IsPlaced)
        {
            // Order already placed - could publish an error event if needed
            return;
        }

        _publish(new OrderPlaced(orderId, customerId));
    }

    /// <summary>
    /// Adds an item to the order.
    /// </summary>
    public void add_item(string orderId, string productId, int quantity, decimal price)
    {
        if (!_state.IsPlaced)
        {
            _publish(new ItemCannotBeAdded(orderId, productId, "Order must be placed before adding items"));
            return;
        }

        if (quantity <= 0)
        {
            _publish(new ItemCannotBeAdded(orderId, productId, "Quantity must be greater than zero"));
            return;
        }

        _publish(new ItemAddedToOrder(orderId, productId, quantity, price));
    }
}
