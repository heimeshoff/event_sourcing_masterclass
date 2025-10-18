using FluentAssertions;

namespace EatsNTreats.Tests.Infrastructure;

/// <summary>
/// Base class for event-driven aggregate tests using Given-When-Then pattern.
/// </summary>
public abstract class TestBase
{
    private List<object> _history = new();
    private List<object> _publishedEvents = new();

    protected TestBase()
    {
        _history = new List<object>();
        _publishedEvents = new List<object>();
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
}
