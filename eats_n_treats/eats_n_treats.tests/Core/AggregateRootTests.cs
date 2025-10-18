using EatsNTreats.Domain.Core;

namespace EatsNTreats.Tests.Core;

/// <summary>
/// Tests for the AggregateRoot base class to verify event sourcing infrastructure.
/// </summary>
public class AggregateRootTests
{
    // Test aggregate for testing purposes
    private class TestAggregate : AggregateRoot
    {
        public string? Name { get; private set; }
        public int Counter { get; private set; }

        public void Create(string id, string name)
        {
            RaiseEvent(new TestCreated { AggregateId = id, Name = name });
        }

        public void Increment()
        {
            RaiseEvent(new TestIncremented { AggregateId = Id });
        }

        // Apply methods for events
        private void Apply(TestCreated @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
        }

        private void Apply(TestIncremented @event)
        {
            Counter++;
        }
    }

    // Test events
    private record TestCreated : Event
    {
        public string Name { get; init; } = string.Empty;
    }

    private record TestIncremented : Event;

    [Fact]
    public void NewAggregate_HasNoUncommittedEvents()
    {
        // Arrange & Act
        var aggregate = new TestAggregate();

        // Assert
        Assert.Empty(aggregate.UncommittedEvents);
        Assert.Equal(0, aggregate.Version);
    }

    [Fact]
    public void RaisingEvent_AddsToUncommittedEvents()
    {
        // Arrange
        var aggregate = new TestAggregate();

        // Act
        aggregate.Create("test-123", "Test Name");

        // Assert
        Assert.Single(aggregate.UncommittedEvents);
        Assert.Equal(1, aggregate.Version);
    }

    [Fact]
    public void RaisingEvent_AppliesEventToState()
    {
        // Arrange
        var aggregate = new TestAggregate();

        // Act
        aggregate.Create("test-123", "Test Name");

        // Assert
        Assert.Equal("test-123", aggregate.Id);
        Assert.Equal("Test Name", aggregate.Name);
    }

    [Fact]
    public void RaisingMultipleEvents_IncrementsVersion()
    {
        // Arrange
        var aggregate = new TestAggregate();

        // Act
        aggregate.Create("test-123", "Test Name");
        aggregate.Increment();
        aggregate.Increment();

        // Assert
        Assert.Equal(3, aggregate.Version);
        Assert.Equal(3, aggregate.UncommittedEvents.Count);
    }

    [Fact]
    public void MarkEventsAsCommitted_ClearsUncommittedEvents()
    {
        // Arrange
        var aggregate = new TestAggregate();
        aggregate.Create("test-123", "Test Name");
        aggregate.Increment();

        // Act
        aggregate.MarkEventsAsCommitted();

        // Assert
        Assert.Empty(aggregate.UncommittedEvents);
        Assert.Equal(2, aggregate.Version); // Version should remain
    }

    [Fact]
    public void LoadFromHistory_ReplaysEventsAndRestoresState()
    {
        // Arrange
        var events = new IEvent[]
        {
            new TestCreated { AggregateId = "test-123", Name = "Test Name", Version = 1 },
            new TestIncremented { AggregateId = "test-123", Version = 2 },
            new TestIncremented { AggregateId = "test-123", Version = 3 }
        };

        // Act
        var aggregate = new TestAggregate();
        aggregate.LoadFromHistory(events);

        // Assert
        Assert.Equal("test-123", aggregate.Id);
        Assert.Equal("Test Name", aggregate.Name);
        Assert.Equal(2, aggregate.Counter);
        Assert.Equal(3, aggregate.Version);
        Assert.Empty(aggregate.UncommittedEvents); // Historical events should not be in uncommitted
    }

    [Fact]
    public void EventsHaveCorrectVersion()
    {
        // Arrange
        var aggregate = new TestAggregate();

        // Act
        aggregate.Create("test-123", "Test Name");
        aggregate.Increment();

        // Assert
        var events = aggregate.UncommittedEvents.ToList();
        Assert.Equal(1, events[0].Version);
        Assert.Equal(2, events[1].Version);
    }

    [Fact]
    public void MissingApplyMethod_ThrowsException()
    {
        // Arrange
        var aggregate = new TestAggregate();
        var unknownEvent = new UnknownEvent { AggregateId = "test-123" };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            aggregate.LoadFromHistory(new[] { unknownEvent }));

        Assert.Contains("No Apply method found", exception.Message);
    }

    // Event without Apply method to test error handling
    private record UnknownEvent : Event;
}
