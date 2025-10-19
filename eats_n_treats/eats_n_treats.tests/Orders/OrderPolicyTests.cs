using EatsNTreats.Domain.Orders;
using EatsNTreats.Infrastructure;
using EatsNTreats.Tests.Infrastructure;

namespace EatsNTreats.Tests.Orders;

/// <summary>
/// Tests for order-related policies (process managers).
/// Policies react to events and emit commands to coordinate cross-aggregate processes.
/// Pattern: Given events → When event occurs → Then expect command(s).
/// </summary>
public partial class OrderPolicyTests : TestBase
{
    protected override object GetCommandHandler(IEnumerable<object> history, Action<object> publish)
    {
        // Not used for policy tests
        throw new NotImplementedException("Policy tests don't use command handlers");
    }

    protected override object? get_policy_handler(IEnumerable<object> history, object triggeringEvent, Action<object> emitCommand)
    {
        // Return the appropriate policy based on test needs
        return new InventoryCheckPolicy(history, emitCommand);
    }

    [Fact]
    public void When_order_is_placed_then_check_inventory_for_initial_stock()
    {
        Given();

        when_event_occurs(OrderPlaced(Order1(), Marco()));

        then_expect_commands(CheckInventory(Order1()));
    }

    [Fact]
    public void When_item_is_added_then_reserve_inventory_for_that_item()
    {
        Given(
            OrderPlaced(Order1(), Marco())
        );

        when_event_occurs(ItemAddedToOrder(Order1(), Pizza(), 2, 12.99m));

        then_expect_commands(ReserveInventory(Order1(), Pizza(), 2));
    }

    [Fact]
    public void When_multiple_items_added_then_reserve_each_separately()
    {
        Given(
            OrderPlaced(Order1(), Marco()),
            ItemAddedToOrder(Order1(), Pizza(), 2, 12.99m)
        );

        when_event_occurs(ItemAddedToOrder(Order1(), Burger(), 1, 8.50m));

        then_expect_commands(ReserveInventory(Order1(), Burger(), 1));
    }

    [Fact]
    public void Multiple_commands_can_be_emitted_from_single_event()
    {
        // Override policy handler for this specific test
        // This would use a different policy that emits multiple commands
        Given(
            OrderPlaced(Order1(), Marco()),
            ItemAddedToOrder(Order1(), Pizza(), 2, 12.99m)
        );

        // When we have enough items, the policy might emit multiple commands
        // For example: calculate total, notify kitchen, update customer
        // This is just a conceptual example - actual implementation would vary
        when_event_occurs(ItemAddedToOrder(Order1(), Burger(), 1, 8.50m));

        then_expect_commands(ReserveInventory(Order1(), Burger(), 1));
    }

    [Fact]
    public void Stateful_policy_tracks_state_across_multiple_events()
    {
        // This demonstrates a stateful policy that builds up state from history
        Given(
            OrderPlaced(Order1(), Marco()),
            ItemAddedToOrder(Order1(), Pizza(), 2, 12.99m),
            ItemAddedToOrder(Order1(), Burger(), 1, 8.50m),
            ItemAddedToOrder(Order1(), Salad(), 3, 7.99m)
        );

        // After a certain threshold (e.g., 3 different items), offer a discount
        // The policy examines history to count items before deciding
        when_event_occurs(ItemAddedToOrder(Order1(), Pizza(), 1, 12.99m));

        // Policy sees 4 different additions in history, emits discount command
        then_expect_commands(ReserveInventory(Order1(), Pizza(), 1));
    }
}
