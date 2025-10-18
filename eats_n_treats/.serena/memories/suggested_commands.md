# Suggested Development Commands

## Build Commands
```bash
# Build the entire solution
dotnet build

# Clean build artifacts
dotnet clean

# Restore dependencies (usually automatic)
dotnet restore
```

## Run Commands
```bash
# Run the shop_ui application
dotnet run --project shop_ui

# Run with specific configuration
dotnet run --project shop_ui --configuration Release
```

## Testing Commands
- No test projects configured yet
- When tests are added, use: `dotnet test`

## Git Commands
```bash
# Standard git commands work in Git Bash
git status
git add .
git commit -m "message"
git push
```

## System Utility Commands (Git Bash on Windows)
```bash
# File operations
ls -la          # List files with details
find . -name    # Find files
grep            # Search in files

# Directory operations
cd              # Change directory
mkdir           # Create directory
rm              # Remove files
```

## Solution Management
```bash
# Add new project to solution
dotnet sln add <project-path>

# List projects in solution
dotnet sln list
```
