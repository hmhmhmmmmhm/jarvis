# Jarvis

Jarvis is a personal AI assistant primarily developed with C# and .NET.

## Development environments

The project is developed on both:

- macOS on Apple Silicon using VS Code
- Windows using VS Code and Visual Studio

Code should be cross-platform unless a feature is explicitly platform-specific.

## Project principles

- Prefer simple and readable solutions.
- Avoid unnecessary abstractions.
- Do not add new dependencies without a clear reason.
- Keep business logic independent from UI and operating-system integrations.
- Isolate platform-specific functionality behind abstractions.
- Prefer solutions that are easy to test.

## C#

- Use modern C# and .NET.
- Enable nullable reference types.
- Use async/await for I/O-bound operations.
- Avoid .Result and .Wait() for asynchronous code.
- Use clear and meaningful names.

## Tests

- Business logic should be testable.
- Add or update tests when behavior changes.
- Do not delete failing tests just to make the build pass.

## Repository structure

- Source code: src/
- Tests: tests/
- Documentation: docs/

## Documentation

Before making architectural changes, review:

- docs/architecture.md
- docs/decisions.md
- docs/roadmap.md

Update the documentation when architectural decisions change.

## Codex workflow

Before making a substantial change:

1. Inspect the relevant project files.
2. Explain the intended change.
3. Keep the change focused.
4. Build affected projects.
5. Run relevant tests.
6. Report what changed and whether validation passed.

Do not perform unrelated refactoring unless explicitly requested.