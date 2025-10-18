namespace EatsNTreats.Domain.Core;

/// <summary>
/// Base class for domain events providing common event metadata.
/// Inherit from this class to create specific domain events.
/// </summary>
public abstract record Event : IEvent
{
    /// <summary>
    /// The unique identifier of the aggregate that produced this event.
    /// </summary>
    public string AggregateId { get; init; } = string.Empty;

    /// <summary>
    /// The timestamp when this event occurred.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// The version of the aggregate after this event was applied.
    /// Used for optimistic concurrency control.
    /// </summary>
    public long Version { get; init; }
}
