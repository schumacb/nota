#tool "KuduSync.NET" "https://www.nuget.org/api/v2/"
#tool nuget:?package=Wyam&prerelease

#addin "Cake.Kudu" "https://www.nuget.org/api/v2/"
#addin nuget:?package=Cake.Wyam&prerelease

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var isRunningLocal = !Kudu.IsRunningOnKudu;
///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var websitePath     = MakeAbsolute(Directory("./src/TestWebSite"));


var deploymentPath = Kudu.Deployment.Target;
if (deploymentPath!=null && !DirectoryExists(deploymentPath) && !isRunningLocal)
{
     throw new DirectoryNotFoundException(
        string.Format(
            "Deployment target directory not found {0}",
            deploymentPath
            )
        );
}

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////


Task("Publish")
    .IsDependentOn("Build")
    .Does(() =>
{
    if (!Kudu.IsRunningOnKudu)
    {
        throw new Exception("Not running on Kudu");
    }

    Information("Deploying web from {0} to {1}", websitePath, deploymentPath);
    Kudu.Sync(websitePath);
});


Task("Build")
    .Does(() =>
    {
        Wyam(new WyamSettings
        { 
            OutputPath  = websitePath,
            LogFilePath = "wyam.log"
        });        
    });
    
Task("Preview")
    .Does(() =>
    {
        Wyam(new WyamSettings
        { 
            OutputPath  = websitePath ?? "output",
            Preview = true,
            Watch = true
        });        
    });

Task("Default")
    .IsDependentOn(isRunningLocal?"Build":"Publish");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);