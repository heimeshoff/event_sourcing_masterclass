namespace EatsNTreats.Domain.Orders;

/// <summary>
/// The Aggregate state that protects the invariants around ordering.
/// Ensures transactional consistent and domain-correct behaviour.
/// </summary>
public class Order_state
{
    public bool IsPlaced { get; private set; }
    public string? CustomerId { get; private set; }
    public List<string> ItemsAdded { get; private set; } = new();

    public Order_state(IEnumerable<object> events)
    {
        foreach (dynamic e in events)
        {
            Apply(e);
        }
    }

    private void Apply(object e) { }

    private void Apply(OrderPlaced e)
    {
        IsPlaced = true;
        CustomerId = e.CustomerId;
    }

    private void Apply(ItemAddedToOrder e)
    {
        ItemsAdded.Add(e.ProductId);
    }
}
