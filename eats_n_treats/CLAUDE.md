# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Eats N Treats** is a comprehensive event sourcing masterclass project built on .NET 9.0. This hands-on workshop explores the full spectrum of event-driven architecture, from foundational concepts to advanced distributed system patterns.

## Development Commands

### Build
```bash
dotnet build
```

### Run
```bash
dotnet run --project shop_ui
```

### Clean
```bash
dotnet clean
```

## Architecture

Current structure:
- **shop_ui**: Console application project (entry point: shop_ui/Program.cs)

The solution uses .NET 9.0 with nullable reference types enabled and implicit usings.

As the workshop progresses, the architecture will evolve to demonstrate:
- **Event Stores**: Immutable event storage and retrieval
- **Aggregates**: Domain models that produce and apply events
- **Command Handlers**: Validation and business logic execution
- **Projections**: Read models built from event streams
- **Sagas**: Long-running distributed processes
- **CQRS Infrastructure**: Separated command and query pathways

## Workshop Topics

### Event Sourcing Fundamentals
- **Events**: Immutable facts representing state changes
- **Event Streams**: Ordered sequences of events per aggregate
- **Domain Models**: Aggregates that make decisions and produce events
- **Auditability & Traceability**: Complete history of all changes

### Events and Storage
- **Event Store Management**: Persistence, retrieval, and optimization
- **Storage Strategies**: Choosing and implementing event storage
- **Stream Management**: Working with event streams

### Event Replay & Versioning
- **Rebuilding State**: Reconstructing current state from event history
- **Event Evolution**: Handling schema changes over time
- **Upcasting**: Migrating old events to new formats
- **Temporal Queries**: Viewing state at any point in time

### Eventual Consistency
- **Distributed Systems**: Managing consistency across boundaries
- **Consistency Boundaries**: Defining aggregate boundaries
- **Compensating Actions**: Handling distributed failures

### Semantic Testing with Events
- **Event Flow Testing**: Verifying event sequences
- **State Transition Testing**: Given-When-Then with events
- **Behavior-Driven Development**: Testing domain logic through events

### CQRS (Command Query Responsibility Segregation)
- **Command Models**: Write-optimized domain models
- **Query Models**: Read-optimized projections
- **Model Separation**: Benefits of independent scaling and optimization
- **Event-Driven CQRS**: Using Event Sourcing to power CQRS systems

### Command and Query Models
- **Commands**: Expressing intent and validation
- **Command Handlers**: Business logic and event production
- **Projections**: Building read models from events
- **Query Optimization**: Efficient data retrieval

### Sagas (Process Managers)
- **Reactive Behavior**: Responding to events across boundaries
- **Distributed Transactions**: Coordinating multi-aggregate processes
- **Compensation Logic**: Handling failures in long-running processes
- **Saga State Management**: Tracking process progress

## Workshop Agenda

### Day 1: Mastering Event Sourcing
1. **Why Event Sourcing?**
   - Immutable facts and temporal modeling
   - Auditability, traceability, and system evolution
   - When to use (and when not to use) Event Sourcing

2. **Core Concepts**
   - Events, streams, and aggregates
   - Projections and read models
   - Event stores and persistence

3. **Designing Event-Driven Systems**
   - Event modeling and storming
   - Consistency boundaries
   - Storage choices and trade-offs

4. **Rebuilding & Evolving Systems**
   - Event replay techniques
   - Versioning strategies
   - Handling breaking changes

## Learning Objectives

By the end of this masterclass, you will be able to:
- Design and implement event-sourced aggregates
- Build and maintain event stores
- Create projections for query optimization
- Apply CQRS patterns effectively
- Handle eventual consistency in distributed systems
- Implement sagas for complex business processes
- Test event-driven systems semantically
- Evolve events and rebuild state confidently

## Domain Context

The "Eats N Treats" domain involves a food ordering/delivery system, providing rich scenarios for exploring:
- Order management (commands, validation, state transitions)
- Inventory tracking (eventual consistency)
- Delivery coordination (sagas)
- Customer history (projections, temporal queries)
- Business analytics (event replay, read models)

# Documentation
All documentation can be found in .md files
plans in the folder docs/plans/
technical documentation in the folder docs/technical_documentation/

# Agents
Use Available agents whenever appropriate:
project-manager: when the user says 'Capture Idea', 'Capture Task', 'Update Task' or 'Remove Task', use this agent
documentation-generator: when the user asks you to document a feature, use this agent.
findings-manager: if you make any surprising or interesting findings at any point, call this agent and let it know what you've found
qa-agent: for testing, regression prevention, and quality analysis
devlog-writer: When asked to write a devlog for a feature, use this agent


# Claude self-maintaince
Whenever something changes out of sync with this document, update it.
