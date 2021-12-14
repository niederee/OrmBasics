
using Build.Models;
using Bullseye;
using CommandLine;
using static Bullseye.Targets;
using SimpleExec;
using System.IO;
using System.Linq;
using System;

public class Program
{
    private static CommandLineArguments _options;

    public static void Main(params string[] args)
    {
        var options = Parser.Default.ParseArguments<CommandLineArguments>(args);
        _options = new CommandLineArguments();
        options.MapResult(result => _options = result, error => null);
        Run();
    }

    private static void Run()
    {
        Directory.SetCurrentDirectory(GetSolutionDirectory());
        Target("clean", () => DotNetClean());
        Target("cleanBuild", DependsOn("clean"), () => CleanProjectStuff());
        Target("restore", DependsOn("cleanBuild"), () => DotNetRestore());
        Target("restoreBuild", DependsOn("restore"), () => DotNetRestore("tools/Build/Build.csproj"));
        Target("test", DependsOn("restoreBuild"), () => Test());
        Target("cover", DependsOn("test"), () => GenerateTestCoverage());
        string[] Targets = _options.Targets?.Count() > 0 ? _options.Targets.ToArray() : new string[] { "default" };
        RunTargetsAndExit(Targets, new Options
        {
            Clear = _options.Clear,
            DryRun = _options.DryRun,
            NoColor = _options.NoColor,
            SkipDependencies = _options.SkipDependencies,
            Verbose = _options.Verbose,
            ListDependencies = _options.ListDependencies,
            ListInputs = _options.ListInputs,
            ListTargets = _options.ListTargets,
            ListTree = _options.ListTree,
        });
    }

    private static void GenerateTestCoverage(string directory = "CodeCoverage", string reportTypes = "HtmlInline,Badges")
    {
        var reportGeneratorDll = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("ReportGenerator.dll", SearchOption.AllDirectories).Where(a=> a.FullName.Contains("net5.0")).First();
        var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "projectTests", "**", "coverage.opencover.xml");
        foreach (var reportType in reportTypes.Split(','))
        {
            Command.Run("dotnet",
                $"{reportGeneratorDll.FullName} \"-reports:{reportPath}\" \"-targetdir:{directory}\" -reporttypes:{reportType}");

        }
    }

    private static void Test()
    {
        foreach (var project in new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "tests")).GetFiles("*.csproj", SearchOption.AllDirectories))
        {
            DotNetTest(project.FullName);
        }
    }

    private static void DotNetTest(string? projectFile = null)
    {
        string proj = string.IsNullOrEmpty(projectFile) ? string.Empty : $" {projectFile.TrimStart()}";
        Command.Run("dotnet",
            $"test{proj} --logger \"trx;v=d\" -c Debug -r projectTests --collect:\"XPlat Code Coverage\" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.ExcludeByAttribute=\"CodeCoverageIgnore\" --verbosity {_options.Verbosity}");
    }

    private static void DotNetRestore(string? projectFile = null, string source = "https://api.nuget.org/v3/index.json", string packageLocation = "packages")
    {
        string proj = string.IsNullOrEmpty(projectFile) ? string.Empty : $" {projectFile.TrimStart()}";
        Command.Run("dotnet",
            $"restore{proj} --source {source} --packages {packageLocation} --verbosity {_options.Verbosity}");
    }

    private static void DotNetClean(string? projectFile = null)
    {
        string proj = string.IsNullOrEmpty(projectFile) ? string.Empty : $" {projectFile.TrimStart()}";
        Command.Run("dotnet", $"clean{proj} --verbosity {_options.Verbosity} --nologo");
    }

    private static void CleanProjectStuff()
    {
        var folders = new string[]
        {
            "projectTests"
        };
        foreach (var folder in folders)
        {
            var fqdn = Path.Combine(Directory.GetCurrentDirectory(), folder);
            if (Directory.Exists(fqdn))
            {
                Directory.Delete(fqdn, true);
            }
        }
    }

    private static string GetSolutionDirectory()
    {
        for (int i = 0; i < 10; i++)
        {
            var up = Enumerable.Repeat("..", i);
            var dir = Path.Combine(Directory.GetCurrentDirectory(), string.Join("/", up));
            if (new DirectoryInfo(dir).GetFiles("*.sln", SearchOption.TopDirectoryOnly).Any())
            {
                return Path.GetRelativePath(Directory.GetCurrentDirectory(), dir);
            }
        }
        throw new FileNotFoundException();
    }
}