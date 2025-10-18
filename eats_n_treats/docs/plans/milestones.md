# Event Sourcing Unit Testing Infrastructure - Milestone Plan

## Vision Summary
Implement a comprehensive event-driven testing infrastructure for the Eats N Treats project using xUnit and the Given-When-Then pattern. This will establish the foundation for semantic testing of event-sourced aggregates through a base class approach, including a sample Order domain to demonstrate the patterns.

---

## Milestone 1: Core Event Sourcing Infrastructure

**Overview**: Create the fundamental building blocks for event sourcing including base classes for Events, Commands, and AggregateRoot. These form the foundation that all domain objects will inherit from.

**Duration Estimate**: 2-3 days

**Acceptance Criteria**:
- Base event sourcing classes (Event, Command, AggregateRoot) are implemented
- Event application and publishing mechanism is functional
- Domain project structure is established with proper namespaces
- All core classes have XML documentation

### Task Plan
1. Create eats_n_treats.domain project structure - [Complexity: S]
2. Implement IEvent interface with timestamp and aggregate ID - [Complexity: S]
3. Implement ICommand interface with validation support - [Complexity: S]
4. Create AggregateRoot base class with event sourcing capabilities - [Complexity: L]
5. Implement event application mechanism (ApplyEvent method) - [Complexity: M]
6. Create event publishing/collection infrastructure - [Complexity: M]
7. Add aggregate versioning support - [Complexity: M]
8. Create unit tests for core infrastructure - [Complexity: M]
9. Add XML documentation to all public APIs - [Complexity: S]

**Dependencies**: .NET 9.0 SDK, xUnit NuGet packages

**Risks**: Design decisions around event versioning strategy may need iteration

---

## Milestone 2: Testing Infrastructure Framework

**Overview**: Build the testing framework including the AggregateTestFixture base class that provides Given-When-Then methods for semantic event testing.

**Duration Estimate**: 2-3 days

**Acceptance Criteria**:
- AggregateTestFixture base class is fully implemented
- Given/When/Then methods work correctly with event streams
- Event comparison and assertion utilities are functional
- Test project structure follows best practices

### Task Plan
1. Create eats_n_treats.tests project with xUnit - [Complexity: S]
2. Implement AggregateTestFixture<TAggregate> base class - [Complexity: L]
3. Create Given() method for setting up historical events - [Complexity: M]
4. Implement When() method for command execution - [Complexity: M]
5. Create Then() method for event assertion - [Complexity: M]
6. Build event comparison utilities with meaningful error messages - [Complexity: M]
7. Add support for exception testing (ThenThrows) - [Complexity: M]
8. Create in-memory event store for testing - [Complexity: M]
9. Implement test helper methods (NewId, etc.) - [Complexity: S]
10. Write meta-tests to verify testing framework behavior - [Complexity: M]

**Dependencies**: Milestone 1 completion, FluentAssertions (optional)

**Risks**: Event comparison logic needs careful design for good developer experience

---

## Milestone 3: Command Handling Infrastructure

**Overview**: Implement the command handling pattern including base command handler class and command dispatcher to connect commands to aggregates through a consistent interface.

**Duration Estimate**: 1-2 days

**Acceptance Criteria**:
- Command handler base class is implemented
- Command-to-aggregate flow is working
- Command validation is integrated
- Error handling patterns are established

### Task Plan
1. Create ICommandHandler interface - [Complexity: S]
2. Implement CommandHandler base class - [Complexity: M]
3. Build command validation pipeline - [Complexity: M]
4. Create aggregate repository interface - [Complexity: S]
5. Implement in-memory aggregate repository for testing - [Complexity: M]
6. Add command result types (success/failure) - [Complexity: S]
7. Implement error handling and validation error collection - [Complexity: M]
8. Create unit tests for command handling flow - [Complexity: M]

**Dependencies**: Milestones 1 and 2 completion

**Risks**: Balance between flexibility and ease of use in command handler design

---

## Milestone 4: Order Domain Implementation

**Overview**: Create a complete example domain with Order aggregate, demonstrating event sourcing patterns with PlaceOrder and AddItemToOrder commands, along with corresponding events.

**Duration Estimate**: 2-3 days

**Acceptance Criteria**:
- Order aggregate is fully implemented with event sourcing
- PlaceOrder and AddItemToOrder commands work correctly
- Events properly capture state changes
- Business rules are enforced (e.g., can't add items to unplaced order)

### Task Plan
1. Create Order aggregate class - [Complexity: M]
2. Implement OrderId value object - [Complexity: S]
3. Create PlaceOrder command and validation - [Complexity: M]
4. Implement OrderPlaced event - [Complexity: S]
5. Create AddItemToOrder command and validation - [Complexity: M]
6. Implement ItemAddedToOrder event - [Complexity: S]
7. Add Order aggregate business logic and invariants - [Complexity: M]
8. Create OrderCommandHandler - [Complexity: M]
9. Implement order item value objects (ProductId, Quantity, Price) - [Complexity: M]
10. Add domain-specific exceptions - [Complexity: S]

**Dependencies**: Milestones 1-3 completion

**Risks**: Ensuring the example is complex enough to be educational but simple enough to understand

---

## Milestone 5: Comprehensive Test Suite

**Overview**: Create a full test suite for the Order domain that demonstrates all testing patterns and serves as a template for future aggregate testing.

**Duration Estimate**: 2 days

**Acceptance Criteria**:
- All Order aggregate commands have semantic tests
- Happy path and error scenarios are covered
- Tests demonstrate Given-When-Then pattern clearly
- Test code is clean and serves as good documentation

### Task Plan
1. Create OrderAggregateTests class structure - [Complexity: S]
2. Implement PlaceOrder tests (new aggregate scenario) - [Complexity: M]
3. Write AddItemToOrder tests with existing order - [Complexity: M]
4. Add tests for business rule violations - [Complexity: M]
5. Create tests for edge cases (empty orders, duplicate items) - [Complexity: M]
6. Implement multi-command sequence tests - [Complexity: M]
7. Add tests for concurrent command scenarios - [Complexity: L]
8. Write regression test examples - [Complexity: M]
9. Create test data builders for complex scenarios - [Complexity: M]
10. Document testing patterns in code comments - [Complexity: S]

**Dependencies**: Milestone 4 completion

**Risks**: Ensuring tests are maintainable and don't become brittle

---

## Milestone 6: Documentation and Examples

**Overview**: Create comprehensive documentation and additional examples to ensure the testing infrastructure is easy to adopt and extend for workshop participants.

**Duration Estimate**: 1-2 days

**Acceptance Criteria**:
- README files explain the testing approach clearly
- Code examples are well-commented
- Common patterns are documented
- Troubleshooting guide is included

### Task Plan
1. Write main README for testing infrastructure - [Complexity: M]
2. Create testing best practices guide - [Complexity: M]
3. Document Given-When-Then pattern usage - [Complexity: S]
4. Add inline code documentation with examples - [Complexity: M]
5. Create troubleshooting guide for common issues - [Complexity: M]
6. Write guide for extending the test framework - [Complexity: M]
7. Add example of testing sagas/process managers - [Complexity: L]
8. Create quick reference card for test patterns - [Complexity: S]

**Dependencies**: All previous milestones

**Risks**: Documentation becoming out of sync with code

---

## Implementation Notes

### Technology Stack
- **.NET 9.0**: Latest framework version for modern C# features
- **xUnit**: Most popular and flexible testing framework for .NET
- **FluentAssertions** (optional): For more readable test assertions

### Key Design Decisions
1. **Base Class Approach**: Chosen over builder pattern for simpler, more discoverable API
2. **Separate Domain Project**: Keeps domain logic pure and testable
3. **In-Memory Testing**: Fast execution, no external dependencies
4. **Command Handlers**: Provide clear separation of concerns

### Testing Philosophy
- Tests should read like specifications
- Each test tells a story: Given (context), When (action), Then (outcome)
- Test names describe business behavior, not technical implementation
- Failures should provide clear, actionable error messages

### Best Practices for Implementation
1. Start with the simplest possible implementation that works
2. Refactor for clarity once tests are green
3. Keep test setup minimal - use factory methods for complex objects
4. Avoid test interdependencies - each test should be independent
5. Use meaningful variable names that express business concepts

### Extension Points
The infrastructure should be designed with these future extensions in mind:
- **Snapshot Testing**: Ability to snapshot aggregate state
- **Property-Based Testing**: Generate random valid commands
- **Integration Testing**: Connect to real event stores
- **Performance Testing**: Measure aggregate reconstruction time

---

## Project Structure

```
eats_n_treats/
   eats_n_treats.domain/
      Core/
         Event.cs
         Command.cs
         AggregateRoot.cs
      Orders/
         Order.cs (aggregate)
         OrderCommands.cs
         OrderEvents.cs
         OrderCommandHandler.cs
      eats_n_treats.domain.csproj
   eats_n_treats.tests/
      Infrastructure/
         AggregateTestFixture.cs
      Orders/
         OrderAggregateTests.cs
      eats_n_treats.tests.csproj
   shop_ui/ (existing console app)
```

---

## Summary

This milestone plan provides a structured approach to building a robust event sourcing testing infrastructure that will serve as the foundation for the workshop's hands-on exercises. The plan balances educational value with practical implementation, creating reusable patterns that participants can apply to their own event-sourced systems.

**Total Estimated Duration**: 10-15 days
**Total Tasks**: 55 tasks across 6 milestones
