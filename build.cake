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
    DotNetBuild(SOLUTION, settings =>
      settings.SetConfiguration(configuration)
        .WithTarget("Clean")
        .WithProperty("TreatWarningsAsErrors","false"));
  });

Task("InitializeBuild")
  .Does(() =>
  {
    NuGetRestore(SOLUTION);
  });

Task("Build")
    .Does(() =>
    {
      DotNetBuild(SOLUTION, settings =>
        settings.SetConfiguration(configuration)
          .WithTarget("Build")
          .WithProperty("TreatWarningsAsErrors","false"));
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