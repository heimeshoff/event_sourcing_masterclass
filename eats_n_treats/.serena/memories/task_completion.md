# Task Completion Checklist

When completing a development task, follow these steps:

## 1. Build Verification
```bash
dotnet build
```
- Ensure the solution builds without errors
- Address any compilation warnings if applicable

## 2. Run the Application
```bash
dotnet run --project shop_ui
```
- Verify the application runs without crashes
- Test the implemented functionality manually

## 3. Code Quality (When Applicable)
- No automated testing framework configured yet
- No linting/formatting tools configured yet
- Manually review code for:
  - Following C# naming conventions
  - Proper use of nullable reference types
  - Adherence to event sourcing patterns (when applicable)

## 4. Version Control
```bash
git status              # Check what changed
git add .               # Stage changes
git commit -m "msg"     # Commit with descriptive message
```

## Future Additions
As the project matures, this checklist should include:
- Running unit tests: `dotnet test`
- Running code formatters
- Running static analysis tools
- Integration tests
- Performance validation
