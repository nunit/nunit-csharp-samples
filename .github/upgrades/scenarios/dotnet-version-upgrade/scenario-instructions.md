# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0 (.NET 10 - LTS)
- **Project Path**: C:\repos\nunit\nunit-csharp-samples\DataDrivenTests\DataDrivenTests.csproj

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

## Source Control
- **Source Branch**: update
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: Single Commit at End
- **Branch Sync**: Auto (Merge)

## Strategy
**Selected**: All-at-Once
**Rationale**: Single project with no dependencies, already on modern .NET (net6.0), straightforward TFM update with no compatibility issues.

### Execution Constraints
- Single atomic upgrade — all projects (in this case, one project) updated together
- Validate full solution build after upgrade with zero errors and zero warnings
- No tier ordering or phasing — complete upgrade in one pass
- Build and fix all compilation errors in a single bounded pass (not a retry loop)

## User Preferences

## Key Decisions Log

## Reminders & Deferred Items
- Convert to Central Package Management (CPM) after .NET upgrade completes (2025-01-20)
