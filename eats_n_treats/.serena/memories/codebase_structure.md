# Codebase Structure

## Root Directory
```
eats_n_treats/
├── .claude/              # Claude Code configuration
├── .idea/                # JetBrains Rider IDE configuration
├── .serena/              # Serena MCP configuration
├── shop_ui/              # Main console application project
├── CLAUDE.md             # Instructions for Claude Code
└── eats_n_treats.sln     # Solution file
```

## Projects

### shop_ui
- **Type**: Console Application (Exe)
- **Framework**: .NET 9.0
- **Entry Point**: Program.cs
- **Current Status**: Basic "Hello, World!" implementation

```
shop_ui/
├── bin/                  # Build output (Debug/Release)
├── obj/                  # Intermediate build files
├── Program.cs            # Main entry point
└── shop_ui.csproj        # Project configuration
```

## Future Structure
As the masterclass progresses, expect additional projects for:
- Domain layer (aggregates, events, commands)
- Infrastructure layer (event store implementation)
- Application layer (CQRS handlers)
- Read models and projections
- Possibly test projects
