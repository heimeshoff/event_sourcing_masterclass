namespace EatsNTreats.Domain.Core;

/// <summary>
/// Represents an immutable event that captures a state change in the domain.
/// Events are the source of truth in an event-sourced system.
/// </summary>
public interface IEvent
{
    /// <summary>
    /// The unique identifier of the aggregate that produced this event.
    /// </summary>
    string AggregateId { get; }

    /// <summary>
    /// The timestamp when this event occurred.
    /// </summary>
    DateTimeOffset Timestamp { get; }

    /// <summary>
    /// The version of the aggregate after this event was applied.
    /// Used for optimistic concurrency control.
    /// </summary>
    long Version { get; }
}
