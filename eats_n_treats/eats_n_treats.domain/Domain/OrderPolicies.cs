using EatsNTreats.Infrastructure;

namespace EatsNTreats.Domain.Orders;

/// <summary>
/// Policy that manages inventory checks and reservations for orders.
/// This is a reactive policy that responds to order events by coordinating with inventory.
/// </summary>
public class InventoryCheckPolicy : PolicyHandler
{
    public InventoryCheckPolicy(IEnumerable<object> history, Action<object> emitCommand)
        : base(history, emitCommand)
    {
    }

    public override void handle(object triggeringEvent)
    {
        switch (triggeringEvent)
        {
            case OrderPlaced orderPlaced:
                // When an order is placed, initiate inventory check
                emit(new CheckInventory(orderPlaced.OrderId));
                break;

            case ItemAddedToOrder itemAdded:
                // When an item is added, reserve inventory for that specific item
                emit(new ReserveInventory(itemAdded.OrderId, itemAdded.ProductId, itemAdded.Quantity));
                break;
        }
    }
}

/// <summary>
/// Example of a stateful policy that tracks order state and applies business rules.
/// This policy examines the event history to make decisions about discounts.
/// </summary>
public class DiscountPolicy : PolicyHandler
{
    public DiscountPolicy(IEnumerable<object> history, Action<object> emitCommand)
        : base(history, emitCommand)
    {
    }

    public override void handle(object triggeringEvent)
    {
        if (triggeringEvent is ItemAddedToOrder itemAdded)
        {
            // Build state from history: count unique items in this order
            var uniqueItemsCount = history
                .OfType<ItemAddedToOrder>()
                .Where(e => e.OrderId == itemAdded.OrderId)
                .Select(e => e.ProductId)
                .Distinct()
                .Count();

            // Business rule: 10% discount for orders with 3+ different items
            if (uniqueItemsCount >= 3)
            {
                // Check if we've already applied a discount
                var discountAlreadyApplied = history
                    .OfType<ApplyDiscount>()
                    .Any(cmd => cmd.OrderId == itemAdded.OrderId);

                if (!discountAlreadyApplied)
                {
                    emit(new ApplyDiscount(itemAdded.OrderId, 10m));
                }
            }
        }
    }
}

/// <summary>
/// Example of a policy that emits multiple commands in response to a single event.
/// This coordinates multiple cross-aggregate processes.
/// </summary>
public class OrderCompletionPolicy : PolicyHandler
{
    public OrderCompletionPolicy(IEnumerable<object> history, Action<object> emitCommand)
        : base(history, emitCommand)
    {
    }

    public override void handle(object triggeringEvent)
    {
        // Example: When order is confirmed, emit multiple commands
        // - Reserve inventory
        // - Notify kitchen
        // - Update customer
        // - Schedule delivery
        // This is just a conceptual example
    }
}
