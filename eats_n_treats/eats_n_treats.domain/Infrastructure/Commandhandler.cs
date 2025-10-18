using EatsNTreats.Domain.Orders;

namespace EatsNTreats.Infrastructure;

// A very simple commandhandler that is not yet optimised for real life usage.
// Use it as a starting point. Depending on your needs you could add:
//   Real routing, pluggable logging/auth/etc.,
//   Chain of responsibility pattern through parent class... or the opposite
//   go all in on functional programming.
// Whatever you do, the basic pattern of separating infrastructure from domain
// starts here.
public class Commandhandler
{
    private readonly IEnumerable<object> _history;
    private readonly Action<object> _publish;

    public Commandhandler(IEnumerable<object> history, Action<object> publish)
    {
        _history = history;
        _publish = publish;
    }

    public void handle(object command)
    {
        if (command is PlaceOrder cmd)
        {
            var state = new Order_state(_history);
            var order = new Order(state, _publish);
            order.place(cmd.OrderId, cmd.CustomerId);
        }
        else if (command is AddItemToOrder cmd2)
        {
            var state = new Order_state(_history);
            var order = new Order(state, _publish);
            order.add_item(cmd2.OrderId, cmd2.ProductId, cmd2.Quantity, cmd2.Price);
        }
    }
}
