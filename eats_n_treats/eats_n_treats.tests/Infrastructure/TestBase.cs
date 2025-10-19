using FluentAssertions;

namespace EatsNTreats.Tests.Infrastructure;

/// <summary>
/// Base class for event-driven aggregate tests using Given-When-Then pattern.
/// Supports both command testing (Given-When-Then) and policy testing (Given-WhenEvent-ThenExpectCommands).
/// </summary>
public abstract class TestBase
{
    private List<object> _history = new();
    private List<object> _publishedEvents = new();
    private List<object> _emittedCommands = new();

    protected TestBase()
    {
        _history = new List<object>();
        _publishedEvents = new List<object>();
        _emittedCommands = new List<object>();
    }

    /// <summary>
    /// Sets up the historical events that happened before the test scenario.
    /// </summary>
    /// <param name="events">The events that establish the initial state.</param>
    protected void Given(params object[] events)
    {
        _history = events.ToList();
    }

    /// <summary>
    /// Executes a command against the aggregate.
    /// Override GetCommandHandler to provide your specific command handler.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    protected void When(object command)
    {
        var handler = GetCommandHandler(_history, e => _publishedEvents.Add(e));
        ((dynamic)handler).handle(command);
    }

    /// <summary>
    /// Asserts that the expected events were published.
    /// </summary>
    /// <param name="expectedEvents">The events expected to be published.</param>
    protected void Then(params object[] expectedEvents)
    {
        _publishedEvents.Should().Equal(expectedEvents);
    }

    /// <summary>
    /// Override this method to provide your command handler implementation.
    /// </summary>
    /// <param name="history">The event history.</param>
    /// <param name="publish">The action to publish events.</param>
    /// <returns>A command handler for your domain.</returns>
    protected abstract object GetCommandHandler(IEnumerable<object> history, Action<object> publish);

    /// <summary>
    /// Executes a policy when a triggering event occurs.
    /// Used for policy tests: Given events → When event occurs → Then expect commands.
    /// </summary>
    /// <param name="triggeringEvent">The event that triggers the policy.</param>
    protected void when_event_occurs(object triggeringEvent)
    {
        var policyHandler = get_policy_handler(_history, triggeringEvent, c => _emittedCommands.Add(c));
        if (policyHandler != null)
        {
            ((dynamic)policyHandler).handle(triggeringEvent);
        }
    }

    /// <summary>
    /// Asserts that the expected commands were emitted by the policy.
    /// </summary>
    /// <param name="expectedCommands">The commands expected to be emitted.</param>
    protected void then_expect_commands(params object[] expectedCommands)
    {
        _emittedCommands.Should().Equal(expectedCommands);
    }

    /// <summary>
    /// Override this method to provide your policy handler implementation.
    /// Return null if the test class doesn't test policies.
    /// </summary>
    /// <param name="history">The event history.</param>
    /// <param name="triggeringEvent">The event that triggered the policy.</param>
    /// <param name="emitCommand">The action to emit commands.</param>
    /// <returns>A policy handler for your domain, or null.</returns>
    protected virtual object? get_policy_handler(IEnumerable<object> history, object triggeringEvent, Action<object> emitCommand)
    {
        return null;
    }
}
