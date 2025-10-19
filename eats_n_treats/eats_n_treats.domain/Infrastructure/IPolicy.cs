namespace EatsNTreats.Infrastructure;

/// <summary>
/// Represents a policy (process manager) that reacts to events and emits commands.
/// Policies coordinate processes across aggregate boundaries by responding to domain events.
/// </summary>
public interface IPolicy
{
    /// <summary>
    /// Handles a triggering event by examining the event history and emitting zero or more commands.
    /// </summary>
    /// <param name="triggeringEvent">The event that triggered this policy.</param>
    void handle(object triggeringEvent);
}
