#tool nuget:?package=NUnit.ConsoleRunner&version=3.8.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

// DEFINE RUN CONSTANTS
var ROOT_DIR = Context.Environment.WorkingDirectory.FullPath;
var NUNIT3_CONSOLE = ROOT_DIR + "/tools/NUnit.ConsoleRunner/tools/nunit3-console.exe";
var SOLUTION = "Samples.sln";

Task("Clean")
  .Does(() =>
  {
    MSBuild(SOLUTION, c =>
      c.SetConfiguration(configuration)
       .SetVerbosity(Verbosity.Minimal)
       .WithTarget("Clean"));
  });

Task("InitializeBuild")
  .Does(() =>
  {
    NuGetRestore(SOLUTION);
  });

Task("Build")
  .IsDependentOn("InitializeBuild")
  .Does(() =>
  {
    MSBuild(SOLUTION, c =>
      c.SetConfiguration(configuration)
       .SetVerbosity(Verbosity.Minimal)
       .WithTarget("Build")
       .WithProperty("TreatWarningsAsErrors","true"));
  });

Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
  {
    var testAssemblies = GetFiles($"./**/bin/{configuration}/*.Tests.dll");
    NUnit3(testAssemblies, new NUnit3Settings { });
  });

Task("Rebuild")
  .IsDependentOn("Clean")
  .IsDependentOn("Build");

Task("Appveyor")
  .IsDependentOn("Build")
  .IsDependentOn("Test");

Task("Travis")
  .IsDependentOn("Build")
  .IsDependentOn("Test");

Task("Default")
	.IsDependentOn("Build")
  .IsDependentOn("Test");

RunTarget(target);