# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade DataDrivenTests project from .NET 6 to .NET 10
**Scope**: Single project (~37 LOC), SDK-style, no dependencies, no compatibility issues

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: Single project with no dependencies, already on modern .NET (net6.0), straightforward TFM update.

## Tasks

### 01-prerequisites: Verify upgrade prerequisites

Validate that the development environment is ready for .NET 10. Check that .NET 10 SDK is installed and that any global.json files in the repository are compatible with .NET 10. This ensures the upgrade can proceed smoothly without toolchain issues.

**Done when**: 
- .NET 10 SDK installation confirmed
- global.json compatibility verified (or no global.json present)
- No toolchain blockers identified

---

### 02-upgrade-project: Upgrade DataDrivenTests to .NET 10

Update the DataDrivenTests project to target net10.0. This involves updating the TargetFramework in the project file and verifying that all NuGet packages (if any are added in the future) remain compatible. Since the assessment shows no packages currently, this is primarily a TFM update with verification.

Assessment context:
- Current: net6.0, SDK-style
- No NuGet packages to update
- No API compatibility issues detected
- 2 code files, 37 lines of code

**Done when**:
- Project file updated to target net10.0
- Solution builds successfully with zero errors and zero warnings
- All existing tests pass
