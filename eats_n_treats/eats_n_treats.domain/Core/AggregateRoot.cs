using System.Reflection;

namespace EatsNTreats.Domain.Core;

/// <summary>
/// Base class for event-sourced aggregate roots.
/// Aggregates are consistency boundaries that enforce business rules and produce events.
/// </summary>
public abstract class AggregateRoot
{
    private readonly List<IEvent> _uncommittedEvents = new();

    /// <summary>
    /// The unique identifier of this aggregate.
    /// </summary>
    public string Id { get; protected set; } = string.Empty;

    /// <summary>
    /// The current version of this aggregate.
    /// Incremented with each applied event.
    /// </summary>
    public long Version { get; protected set; }

    /// <summary>
    /// Gets the collection of events that have been raised but not yet persisted.
    /// </summary>
    public IReadOnlyCollection<IEvent> UncommittedEvents => _uncommittedEvents.AsReadOnly();

    /// <summary>
    /// Loads the aggregate state from a historical sequence of events.
    /// </summary>
    /// <param name="events">The events to replay.</param>
    public void LoadFromHistory(IEnumerable<IEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyEvent(@event, isNew: false);
        }
    }

    /// <summary>
    /// Marks all uncommitted events as committed.
    /// Call this after successfully persisting events to the event store.
    /// </summary>
    public void MarkEventsAsCommitted()
    {
        _uncommittedEvents.Clear();
    }

    /// <summary>
    /// Raises a new event by applying it to the aggregate state and adding it to uncommitted events.
    /// </summary>
    /// <param name="event">The event to raise.</param>
    protected void RaiseEvent(IEvent @event)
    {
        ApplyEvent(@event, isNew: true);
    }

    /// <summary>
    /// Applies an event to the aggregate state.
    /// Uses reflection to find and invoke the appropriate Apply method.
    /// </summary>
    /// <param name="event">The event to apply.</param>
    /// <param name="isNew">Whether this is a new event being raised or a historical event being replayed.</param>
    private void ApplyEvent(IEvent @event, bool isNew)
    {
        // Find the Apply method for this event type
        var eventType = @event.GetType();
        var applyMethod = GetType()
            .GetMethod("Apply", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, new[] { eventType });

        if (applyMethod == null)
        {
            throw new InvalidOperationException(
                $"No Apply method found for event type {@event.GetType().Name} in aggregate {GetType().Name}. " +
                $"Please add a method: void Apply({eventType.Name} @event)");
        }

        // Invoke the Apply method
        applyMethod.Invoke(this, new object[] { @event });

        // Update version
        Version++;

        // Add to uncommitted events if this is a new event
        if (isNew)
        {
            // Set the version on the event
            if (@event is Event evt)
            {
                var updatedEvent = evt with { Version = Version };
                _uncommittedEvents.Add(updatedEvent);
            }
            else
            {
                _uncommittedEvents.Add(@event);
            }
        }
    }
}
