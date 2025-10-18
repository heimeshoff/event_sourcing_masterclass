using EatsNTreats.Domain.Orders;
using EatsNTreats.Infrastructure;
using EatsNTreats.Tests.Infrastructure;

namespace EatsNTreats.Tests.Orders;

/// <summary>
/// Domain-driven tests for the Order aggregate using Given-When-Then pattern.
/// Tests are written semantically using only Events and Commands.
/// </summary>
public partial class OrderTests : TestBase
{
    protected override object GetCommandHandler(IEnumerable<object> history, Action<object> publish)
    {
        return new Commandhandler(history, publish);
    }

    [Fact]
    public void A_new_order_can_be_placed()
    {
        Given();

        When(PlaceOrder(Order1(), Marco()));

        Then(OrderPlaced(Order1(), Marco()));
    }

    [Fact]
    public void An_item_can_be_added_to_a_placed_order()
    {
        Given(
            OrderPlaced(Order1(), Marco())
        );

        When(AddItem(Order1(), Pizza(), 2, 12.99m));

        Then(ItemAdded(Order1(), Pizza(), 2, 12.99m));
    }

    [Fact]
    public void Multiple_items_can_be_added_to_an_order()
    {
        Given(
            OrderPlaced(Order1(), Marco()),
            ItemAdded(Order1(), Pizza(), 2, 12.99m)
        );

        When(AddItem(Order1(), Burger(), 1, 8.50m));

        Then(ItemAdded(Order1(), Burger(), 1, 8.50m));
    }

    [Fact]
    public void An_item_cannot_be_added_to_an_unplaced_order()
    {
        Given();

        When(AddItem(Order1(), Pizza(), 1, 12.99m));

        Then(ItemCannotBeAdded(Order1(), Pizza(), "Order must be placed before adding items"));
    }

    [Fact]
    public void An_item_with_zero_quantity_cannot_be_added()
    {
        Given(
            OrderPlaced(Order1(), Marco())
        );

        When(AddItem(Order1(), Pizza(), 0, 12.99m));

        Then(ItemCannotBeAdded(Order1(), Pizza(), "Quantity must be greater than zero"));
    }

    [Fact]
    public void An_item_with_negative_quantity_cannot_be_added()
    {
        Given(
            OrderPlaced(Order1(), Tina())
        );

        When(AddItem(Order1(), Salad(), -1, 7.99m));

        Then(ItemCannotBeAdded(Order1(), Salad(), "Quantity must be greater than zero"));
    }
}
