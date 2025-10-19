# Event Store Infrastructure Implementation - Milestone Plan

## Vision Summary
Transform the existing in-memory event sourcing infrastructure into a production-ready system with persistent event storage, while maintaining backward compatibility with the existing test suite. This implementation will provide a real event store using SQLite for simplicity and portability, integrate with the shop_ui console application for command and query execution, and demonstrate the complete event sourcing lifecycle from command handling through persistence to query projections.

---

## Milestone 1: Event Store Abstraction and SQLite Implementation

**Overview**: Create the event store abstraction layer and implement a SQLite-based event store. SQLite is chosen for its simplicity, portability, and ease of inspection - perfect for a learning environment. This milestone establishes the foundation for persistent event storage while maintaining compatibility with the existing in-memory test infrastructure.

**Duration Estimate**: 3-4 days

**Acceptance Criteria**:
- IEventStore interface is defined with methods for saving and loading events
- SQLite database schema for events is implemented with proper indexing
- Event serialization/deserialization using System.Text.Json works correctly
- Optimistic concurrency control is implemented using version numbers
- Database initialization and migration support is in place
- Events can be manually inspected in the SQLite database using standard tools

### Task Plan
1. Create IEventStore interface in eats_n_treats.domain/Core - [Complexity: S]
   - Define AppendEvents method with expected version check
   - Define GetEvents method for loading aggregate history
   - Define GetAllEvents method for global event streaming
2. Set up SQLite infrastructure in new eats_n_treats.infrastructure project - [Complexity: M]
   - Add Microsoft.Data.Sqlite NuGet package
   - Create project structure and references
3. Design and implement event storage schema - [Complexity: M]
   - Create Events table (Id, AggregateId, AggregateType, EventType, EventData, EventVersion, GlobalSequence, Timestamp)
   - Add indexes for AggregateId and GlobalSequence
   - Create database initialization/migration logic
4. Implement EventSerializer class using System.Text.Json - [Complexity: M]
   - Configure JsonSerializerOptions for polymorphic serialization
   - Handle type discrimination for event deserialization
   - Add support for custom converters if needed
5. Create SqliteEventStore implementing IEventStore - [Complexity: L]
   - Implement connection string configuration
   - Add transaction support for append operations
   - Implement optimistic concurrency with version checking
6. Build EventStoreException hierarchy for error handling - [Complexity: S]
   - ConcurrencyException for version conflicts
   - SerializationException for data errors
   - EventStoreException base class
7. Create InMemoryEventStore for test compatibility - [Complexity: M]
   - Implement IEventStore with in-memory collections
   - Ensure thread-safety for concurrent tests
8. Add EventStore integration tests - [Complexity: M]
   - Test append and retrieve operations
   - Test optimistic concurrency
   - Test serialization round-trips
9. Create database viewer utility or documentation - [Complexity: S]
   - Document SQL queries to inspect events
   - Provide sample SQLite viewer recommendations
10. Add configuration for connection string management - [Complexity: S]

**Dependencies**:
- Microsoft.Data.Sqlite NuGet package
- System.Text.Json (included in .NET 9.0)

**Risks**:
- Schema evolution and event versioning strategies need careful planning
- JSON serialization performance for large event volumes
- SQLite file locking in multi-threaded scenarios

---

## Milestone 2: Repository Pattern and Aggregate Persistence

**Overview**: Implement the repository pattern to bridge aggregates and the event store, providing a clean abstraction for loading and saving aggregates. This creates the connection between the domain layer and infrastructure while maintaining separation of concerns.

**Duration Estimate**: 2-3 days

**Acceptance Criteria**:
- IAggregateRepository interface is properly defined
- Repository implementation correctly loads and saves aggregates via event store
- Aggregate factory pattern for creating instances is implemented
- Repository integrates seamlessly with existing CommandHandler
- Unit of Work pattern is considered for transaction boundaries

### Task Plan
1. Define IAggregateRepository interface in domain layer - [Complexity: S]
   - GetById<T> method for loading aggregates
   - Save method for persisting aggregate changes
   - Exists method for checking aggregate existence
2. Create AggregateRepository implementation - [Complexity: M]
   - Inject IEventStore dependency
   - Implement aggregate loading with event replay
   - Implement saving with uncommitted events
3. Build aggregate factory mechanism - [Complexity: M]
   - Create IAggregateFactory interface
   - Implement reflection-based factory
   - Support for aggregate type registration
4. Integrate repository with existing CommandHandler - [Complexity: L]
   - Refactor CommandHandler to use repository
   - Maintain backward compatibility for tests
   - Add proper dependency injection
5. Implement EventStream class for aggregate history - [Complexity: M]
   - Encapsulate event collection and version
   - Add stream metadata support
6. Create snapshot support infrastructure (optional) - [Complexity: L]
   - ISnapshotStore interface
   - Snapshot serialization
   - Hybrid loading (snapshot + events)
7. Add repository-level caching strategy - [Complexity: M]
   - In-memory aggregate cache
   - Cache invalidation policies
8. Write comprehensive repository tests - [Complexity: M]
   - Test aggregate lifecycle
   - Test concurrent modifications
   - Test non-existent aggregate handling
9. Create aggregate registration mechanism - [Complexity: S]
   - Auto-discovery via reflection
   - Manual registration support

**Dependencies**: Milestone 1 completion

**Risks**:
- Performance implications of loading large event streams
- Memory management for cached aggregates
- Transaction boundary decisions

---

## Milestone 3: Read Model Infrastructure and Projections

**Overview**: Implement CQRS read-side infrastructure with projections that build denormalized views from events. This enables efficient querying without loading full aggregate history and demonstrates the power of event sourcing for creating multiple read models from a single source of truth.

**Duration Estimate**: 3-4 days

**Acceptance Criteria**:
- IProjection interface for defining projections is created
- Projection dispatcher processes events to update read models
- At least two sample projections are implemented (OrderSummary, OrderDetails)
- Read model storage using SQLite tables
- Projection rebuild capability from event history

### Task Plan
1. Define IProjection interface and base classes - [Complexity: S]
   - When<TEvent> method pattern
   - Projection versioning support
2. Create IReadModelStore interface - [Complexity: S]
   - Generic CRUD operations for read models
   - Query support abstractions
3. Implement SqliteReadModelStore - [Complexity: M]
   - Dynamic table creation from model types
   - Generic serialization for storage
   - Query translation support
4. Build ProjectionDispatcher for event routing - [Complexity: L]
   - Event type to projection mapping
   - Concurrent projection updates
   - Error handling and retry logic
5. Create OrderSummaryProjection - [Complexity: M]
   - Track order count, total value, status
   - Handle OrderPlaced and ItemAddedToOrder events
6. Implement OrderDetailsProjection - [Complexity: M]
   - Full order information denormalized
   - Support for querying by various fields
7. Add projection position tracking - [Complexity: M]
   - Track last processed event per projection
   - Support for projection catch-up
8. Implement projection rebuilder - [Complexity: M]
   - Clear and rebuild projections from event history
   - Progress tracking and cancellation
9. Create IQueryHandler abstraction - [Complexity: S]
   - Define query handling pattern
   - Support for typed queries and results
10. Implement sample query handlers - [Complexity: M]
    - GetOrderSummaryQuery
    - GetOrderDetailsQuery
    - GetOrdersByCustomerQuery
11. Add projection integration tests - [Complexity: M]
    - Test event processing
    - Test rebuilding
    - Test query accuracy

**Dependencies**: Milestones 1 and 2 completion

**Risks**:
- Projection consistency during rebuild
- Performance of projection updates at scale
- Handling projection versioning and schema changes

---

## Milestone 4: Console Application Integration and User Interface

**Overview**: Transform the shop_ui console application into a functional event-sourced application that demonstrates command execution, event persistence, and query capabilities. This provides a tangible way to interact with the event sourcing infrastructure and inspect the results.

**Duration Estimate**: 2-3 days

**Acceptance Criteria**:
- shop_ui has a menu-driven interface for commands and queries
- Dependency injection is properly configured
- Users can execute PlaceOrder and AddItemToOrder commands
- Users can query order information via projections
- Users can view raw events in the database
- Application demonstrates both command and query sides of CQRS

### Task Plan
1. Set up dependency injection in shop_ui - [Complexity: M]
   - Add Microsoft.Extensions.DependencyInjection
   - Configure service registration
   - Set up application lifetime management
2. Create application configuration - [Complexity: S]
   - appsettings.json for connection strings
   - Configuration binding
   - Environment-specific settings
3. Implement main menu system - [Complexity: M]
   - Command menu (Place Order, Add Item)
   - Query menu (View Orders, Order Details)
   - Admin menu (View Events, Rebuild Projections)
4. Build command input handlers - [Complexity: M]
   - PlaceOrder command UI
   - AddItemToOrder command UI
   - Input validation and error display
5. Create query result displays - [Complexity: M]
   - Format order summaries
   - Display order details
   - Show customer order history
6. Implement event viewer functionality - [Complexity: M]
   - Display raw events from store
   - Format JSON for readability
   - Filter by aggregate or event type
7. Add database management commands - [Complexity: S]
   - Initialize database
   - Clear all data
   - Export/import events
8. Create sample data generator - [Complexity: M]
   - Generate random orders
   - Simulate realistic scenarios
   - Bulk data for performance testing
9. Implement error handling and user feedback - [Complexity: S]
   - Friendly error messages
   - Success confirmations
   - Loading indicators
10. Add application logging - [Complexity: S]
    - Console and file logging
    - Structured logging with context
11. Create user documentation - [Complexity: S]
    - Command descriptions
    - Query explanations
    - Keyboard shortcuts

**Dependencies**: Milestones 1-3 completion

**Risks**:
- Console UI complexity vs usability
- Dependency injection configuration complexity
- Error handling and user experience

---

## Implementation Notes

### Technology Choices and Rationale

**SQLite for Event Store**:
- Zero configuration database perfect for learning
- Single file storage easy to inspect and share
- Supports all necessary features (transactions, indexes, JSON)
- Can be easily replaced with SQL Server/PostgreSQL later
- Tools like DB Browser for SQLite provide easy inspection

**System.Text.Json for Serialization**:
- Built into .NET, no additional dependencies
- High performance and low allocation
- Source generators available for optimization
- Native support for polymorphic serialization in .NET 9

**In-Process Projections**:
- Simpler to understand and debug
- No additional infrastructure needed
- Can evolve to out-of-process later
- Sufficient for learning scenarios

### Architecture Principles

1. **Maintain Test Isolation**: Existing tests must continue using in-memory infrastructure
2. **Dependency Injection**: All components should be injectable and testable
3. **Abstraction Layers**: Clear interfaces between domain and infrastructure
4. **Fail-Fast**: Clear error messages for configuration and runtime issues
5. **Observability**: Make it easy to see what the system is doing

### Database Schema Design

```sql
-- Events table
CREATE TABLE Events (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    AggregateId TEXT NOT NULL,
    AggregateType TEXT NOT NULL,
    EventType TEXT NOT NULL,
    EventData TEXT NOT NULL,
    EventVersion INTEGER NOT NULL,
    GlobalSequence INTEGER NOT NULL,
    Timestamp TEXT NOT NULL,
    UNIQUE(AggregateId, EventVersion)
);

CREATE INDEX idx_events_aggregate ON Events(AggregateId);
CREATE INDEX idx_events_sequence ON Events(GlobalSequence);

-- Projections table
CREATE TABLE ProjectionPositions (
    ProjectionName TEXT PRIMARY KEY,
    LastProcessedPosition INTEGER NOT NULL,
    LastUpdated TEXT NOT NULL
);

-- Read model tables created dynamically based on projection needs
```

### Configuration Structure

```json
{
  "EventStore": {
    "ConnectionString": "Data Source=events.db",
    "InitializeDatabase": true
  },
  "Projections": {
    "AutoRebuildOnStartup": false,
    "ProcessingBatchSize": 100
  },
  "Application": {
    "EnableLogging": true,
    "LogLevel": "Information"
  }
}
```

### Testing Strategy

1. **Unit Tests**: Continue using InMemoryEventStore
2. **Integration Tests**: Test with real SQLite in-memory mode
3. **Acceptance Tests**: Use temporary SQLite files
4. **Manual Testing**: Console application with sample data

### Migration Path

For teams wanting to migrate to production databases:
1. Implement SqlServerEventStore or PostgreSqlEventStore
2. Update dependency injection configuration
3. Migrate event data using built-in export/import
4. No changes needed in domain or application layer

### Performance Considerations

- Event batching for write operations
- Projection processing in batches
- Connection pooling for SQLite
- Async/await throughout the stack
- Consider snapshot frequency for large aggregates

### Debugging and Troubleshooting

1. **Event Inspection**: SQLite database can be opened with any SQLite viewer
2. **Logging**: Structured logging shows command/event flow
3. **Projection State**: Dedicated UI to show projection positions
4. **Event Replay**: Ability to replay events for debugging

---

## Summary

This milestone plan transforms the in-memory event sourcing prototype into a fully functional, persistent system while maintaining the educational focus of the masterclass. The progression from basic persistence through repositories and projections to a working console application provides hands-on experience with all aspects of event sourcing.

The choice of SQLite keeps the complexity manageable while still demonstrating real-world patterns. The architecture remains flexible enough to scale to production systems by simply swapping infrastructure implementations.

**Total Estimated Duration**: 10-14 days
**Total Tasks**: 41 tasks across 4 milestones
**Key Deliverables**:
- Persistent event store with SQLite
- Repository pattern implementation
- CQRS read models with projections
- Interactive console application
- Complete testing strategy maintaining existing tests