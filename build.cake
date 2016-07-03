//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// DISCOVERY VARS
//////////////////////////////////////////////////////////////////////

string[] SolutionList = null;
string[] ProjList = null;
var PROJ_EXT = "*.csproj";

//////////////////////////////////////////////////////////////////////
// DEFINE RUN CONSTANTS
//////////////////////////////////////////////////////////////////////

var ROOT_DIR = Context.Environment.WorkingDirectory.FullPath;
var NUNIT3_CONSOLE = ROOT_DIR + "/tools/NUnit.ConsoleRunner/tools/nunit3-console.exe";

//////////////////////////////////////////////////////////////////////
// ERROR LOG
//////////////////////////////////////////////////////////////////////

var ErrorDetail = new List<string>();

//////////////////////////////////////////////////////////////////////
// DISCOVER SOLUTIONS
//////////////////////////////////////////////////////////////////////

Task("DiscoverSolutions")
    .Does(() =>
    {
        SolutionList = System.IO.Directory.GetFiles(ROOT_DIR, "*.sln", SearchOption.AllDirectories);
        ProjList = System.IO.Directory.GetFiles(ROOT_DIR, PROJ_EXT, SearchOption.AllDirectories);
    });

//////////////////////////////////////////////////////////////////////
// CLEAN
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .IsDependentOn("DiscoverSolutions")
    .Does(() =>
    {
        foreach(var proj in ProjList)
            CleanDirectory(DirFrom(proj) + "/bin/" + configuration);
    });

//////////////////////////////////////////////////////////////////////
// RESTORE PACKAGES
//////////////////////////////////////////////////////////////////////

Task("InitializeBuild")
    .IsDependentOn("DiscoverSolutions")
    .Does(() =>
    {
        foreach(var sln in SolutionList)
            NuGetRestore(sln);
    });

//////////////////////////////////////////////////////////////////////
// BUILD
//////////////////////////////////////////////////////////////////////

Task("Build")
    .IsDependentOn("InitializeBuild")
    .Does(() =>
    {
        foreach(var proj in ProjList)
        {
            var sample = System.IO.Path.GetFileNameWithoutExtension(proj);
            DisplayHeading("Building " + sample + " sample");

            try
            {
				BuildProject(proj, configuration);
            }
            catch(Exception ex)
            {
                // Just record and continue, since samples are independent
                ErrorDetail.Add("     * " + sample + " build failed.");
            }
        }
    });

//////////////////////////////////////////////////////////////////////
// TEST
//////////////////////////////////////////////////////////////////////

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        foreach(var proj in ProjList)
        {
            var sample = System.IO.Path.GetFileNameWithoutExtension(proj);
            var bin = DirFrom(proj) + "/bin/" + configuration + "/";
            var dllName = bin + sample + ".dll";

            DisplayHeading("Testing " + sample + " sample");

            int rc = StartProcess(
				NUNIT3_CONSOLE,
				new ProcessSettings()
				{
					Arguments = dllName
				});

            if (rc > 0)
                ErrorDetail.Add(string.Format("{0}: {1} tests failed", sample, rc));
            else if (rc < 0)
                ErrorDetail.Add(string.Format("{0} exited with rc = {1}", sample, rc));
        }
    });

//////////////////////////////////////////////////////////////////////
// TEARDOWN TASK
//////////////////////////////////////////////////////////////////////

Teardown(() =>
{
    CheckForError(ref ErrorDetail);
});

void CheckForError(ref List<string> errorDetail)
{
    if(errorDetail.Count != 0)
    {
        var copyError = new List<string>();
        copyError = errorDetail.Select(s => s).ToList();
        errorDetail.Clear();
        throw new Exception("There were errors in some tasks:\n"
            + copyError.Aggregate((x,y) => x + "\n" + y));
    }
}

//////////////////////////////////////////////////////////////////////
// HELPER METHODS
//////////////////////////////////////////////////////////////////////

void BuildProject(string projPath, string configuration)
{
    if (IsRunningOnWindows())
    {
        MSBuild(projPath, new MSBuildSettings()
            .SetConfiguration(configuration)
            .SetMSBuildPlatform(MSBuildPlatform.Automatic)
            .SetVerbosity(Verbosity.Minimal)
            .SetNodeReuse(false));
    }
    else
    {
        XBuild(projPath, new XBuildSettings()
            .WithTarget("Build")
            .WithProperty("Configuration", configuration)
            .SetVerbosity(Verbosity.Minimal));
    }
}

string DirFrom(string filePath)
{
    return System.IO.Path.GetDirectoryName(filePath);
}

void DisplayHeading(string heading)
{
    Information("");
    Information("----------------------------------------");
    Information(heading);
    Information("----------------------------------------");
    Information("");
}

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

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
	.IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);