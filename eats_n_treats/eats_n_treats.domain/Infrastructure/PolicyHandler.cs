namespace EatsNTreats.Infrastructure;

/// <summary>
/// Base class for implementing policies (process managers).
/// Supports both stateless reactive policies and stateful process managers.
/// </summary>
public abstract class PolicyHandler : IPolicy
{
    private readonly IEnumerable<object> _history;
    private readonly Action<object> _emitCommand;

    /// <summary>
    /// Creates a new policy handler.
    /// </summary>
    /// <param name="history">The complete event history available to this policy.</param>
    /// <param name="emitCommand">Action to emit commands in response to events.</param>
    protected PolicyHandler(IEnumerable<object> history, Action<object> emitCommand)
    {
        _history = history;
        _emitCommand = emitCommand;
    }

    /// <summary>
    /// The complete event history available to this policy for building state or making decisions.
    /// </summary>
    protected IEnumerable<object> history => _history;

    /// <summary>
    /// Emits a command in response to the triggering event.
    /// </summary>
    /// <param name="command">The command to emit.</param>
    protected void emit(object command)
    {
        _emitCommand(command);
    }

    /// <summary>
    /// Handles the triggering event. Override this to implement policy logic.
    /// </summary>
    /// <param name="triggeringEvent">The event that triggered this policy.</param>
    public abstract void handle(object triggeringEvent);
}
