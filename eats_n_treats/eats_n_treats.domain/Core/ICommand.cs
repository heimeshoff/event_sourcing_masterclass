namespace EatsNTreats.Domain.Core;

/// <summary>
/// Represents a command that expresses intent to change the state of an aggregate.
/// Commands may be rejected if they violate business rules.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// The unique identifier of the aggregate this command targets.
    /// </summary>
    string AggregateId { get; }

    /// <summary>
    /// Validates the command structure and basic business rules.
    /// </summary>
    /// <returns>A collection of validation errors, or empty if valid.</returns>
    IEnumerable<string> Validate();
}
