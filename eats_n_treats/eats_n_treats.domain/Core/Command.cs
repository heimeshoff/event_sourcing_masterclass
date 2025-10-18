namespace EatsNTreats.Domain.Core;

/// <summary>
/// Base class for domain commands providing common command behavior.
/// Inherit from this class to create specific domain commands.
/// </summary>
public abstract record Command : ICommand
{
    /// <summary>
    /// The unique identifier of the aggregate this command targets.
    /// </summary>
    public string AggregateId { get; init; } = string.Empty;

    /// <summary>
    /// Validates the command structure and basic business rules.
    /// Override this method to add command-specific validation logic.
    /// </summary>
    /// <returns>A collection of validation errors, or empty if valid.</returns>
    public virtual IEnumerable<string> Validate()
    {
        if (string.IsNullOrWhiteSpace(AggregateId))
        {
            yield return "AggregateId is required";
        }
    }
}
