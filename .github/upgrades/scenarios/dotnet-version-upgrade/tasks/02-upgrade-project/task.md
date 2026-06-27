# 02-upgrade-project: Upgrade DataDrivenTests to .NET 10

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
