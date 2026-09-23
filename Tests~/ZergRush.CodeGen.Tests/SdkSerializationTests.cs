using System.Diagnostics;
using System.Security;
using ZergRush;

namespace ZergRush.CodeGen.Tests;

public class SdkSerializationTests
{
    [Theory]
    [InlineData("implicit")]
    [InlineData("explicit")]
    [InlineData("global")]
    [InlineData("qualified")]
    public void Evaluated_sdk_imports_and_project_references_round_trip(string imports)
    {
        InDirectory(dir =>
        {
            var dependency = Path.Combine(dir, "Dependency");
            Directory.CreateDirectory(dependency);
            File.WriteAllText(Path.Combine(dependency, "Dependency.csproj"), "<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net10.0</TargetFramework></PropertyGroup></Project>");
            File.WriteAllText(Path.Combine(dependency, "External.cs"), "namespace External; public class Data { public int value; }");
            var app = Path.Combine(dir, "App");
            Directory.CreateDirectory(app);
            var project = Path.Combine(app, "App.csproj");
            File.WriteAllText(project, Project(imports == "implicit", "<ProjectReference Include=\"../Dependency/Dependency.csproj\" />"));
            string prefix = imports == "qualified" ? "System.Collections.Generic." : "";
            string usings = imports == "global" ? "global using System.Collections.Generic;" : imports == "explicit" ? "using System.Collections.Generic;" : "";
            File.WriteAllText(Path.Combine(app, "Input.cs"), $$"""
                {{usings}}
                using ZergRush.CodeGen;
                [GenTask(GenTaskFlags.Serialization | GenTaskFlags.JsonSerialization | GenTaskFlags.DefaultConstructor)]
                public partial class Model {
                    public {{prefix}}Dictionary<long, int> ranks = new();
                    public {{prefix}}List<int> values = new();
                    public External.Data external = new();
                }
                """);
            var generated = Path.Combine(app, "Generated");
            RunCli(project, generated, true);
            File.WriteAllText(Path.Combine(app, "Program.cs"), """
                var value = new Model(); value.ranks.Add(101, 2); value.values.Add(7); value.external.value = 42;
                var json = value.WriteToJsonString().ReadFromJson<Model>();
                var bin = value.WriteToByteArray().Read<Model>();
                if (json.ranks[101] != 2 || bin.ranks[101] != 2 || json.values[0] != 7 || bin.values[0] != 7 || json.external.value != 42 || bin.external.value != 42) throw new System.Exception("Data lost");
                """);
            var run = Run("run", "--project", project);
            Assert.True(run.Code == 0, run.Output);
        });
    }

    [Fact]
    public void Unresolved_model_member_fails_cli_without_replacing_good_output()
    {
        InDirectory(dir =>
        {
            var project = Path.Combine(dir, "Input.csproj");
            File.WriteAllText(project, Project(true));
            File.WriteAllText(Path.Combine(dir, "Input.cs"), "using ZergRush.CodeGen; [GenTask(GenTaskFlags.Serialization)] public partial class Model { public MissingType badField; }");
            var generated = Path.Combine(dir, "Generated");
            Directory.CreateDirectory(generated);
            var sentinel = Path.Combine(generated, "Previous.gen.cs");
            File.WriteAllText(sentinel, "// good existing output");
            var result = RunCli(project, generated, false);
            Assert.Contains("Model.badField", result);
            Assert.Contains("MissingType", result);
            Assert.DoesNotContain("Generated source into", result);
            Assert.Equal("// good existing output", File.ReadAllText(sentinel));
        });
    }

    static string Project(bool implicitUsings, string items = "") => $"""
        <Project Sdk="Microsoft.NET.Sdk"><PropertyGroup><TargetFramework>net10.0</TargetFramework><OutputType>Exe</OutputType><ImplicitUsings>{(implicitUsings ? "enable" : "disable")}</ImplicitUsings></PropertyGroup><ItemGroup>
        <Reference Include="ZergRush.CodeGen.Core"><HintPath>{SecurityElement.Escape(typeof(GenTask).Assembly.Location)}</HintPath></Reference>
        <Reference Include="ZergRush.Reactive"><HintPath>{SecurityElement.Escape(typeof(SimpleList<int>).Assembly.Location)}</HintPath></Reference>
        <Reference Include="Newtonsoft.Json"><HintPath>{SecurityElement.Escape(typeof(Newtonsoft.Json.JsonReader).Assembly.Location)}</HintPath></Reference>
        {items}</ItemGroup></Project>
        """;

    static string RunCli(string project, string output, bool success)
    {
        var root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../.."));
        var cli = Path.Combine(root, "src/ZergRush.CodeGen.Cli/bin~", new DirectoryInfo(AppContext.BaseDirectory).Parent!.Name, "net10.0/ZergRush.CodeGen.Cli.dll");
        var result = Run(cli, "-p", project, "--generate", output, "--single-output-folder");
        Assert.True(success ? result.Code == 0 : result.Code != 0, result.Output);
        return result.Output;
    }

    static (int Code, string Output) Run(params string[] args)
    {
        var start = new ProcessStartInfo("dotnet") { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true };
        foreach (var arg in args) start.ArgumentList.Add(arg);
        using var process = Process.Start(start)!;
        var output = process.StandardOutput.ReadToEndAsync();
        var error = process.StandardError.ReadToEndAsync();
        process.WaitForExit();
        return (process.ExitCode, output.Result + error.Result);
    }

    static void InDirectory(Action<string> action)
    {
        var dir = Path.Combine(Path.GetTempPath(), "ZergRushSdkTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(dir);
        try { action(dir); }
        finally { Directory.Delete(dir, true); }
    }
}
