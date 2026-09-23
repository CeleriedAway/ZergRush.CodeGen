using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;

namespace ZergRush.CodeGen;

// Ask the installed SDK for evaluated items: Directory.Build.props, implicit/global
// usings, package references and project references must match the real build.
internal sealed class ProjectCompilationInputs
{
    public HashSet<string> Projects { get; } = new(StringComparer.OrdinalIgnoreCase);
    public List<string> Files { get; } = new();
    public List<string> References { get; } = new();
    public List<string> Defines { get; } = new();

    public static ProjectCompilationInputs Load(IEnumerable<string> inputs)
    {
        var result = new ProjectCompilationInputs();
        foreach (var input in inputs)
        {
            if (Path.GetExtension(input).Equals(".sln", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var line in File.ReadLines(input).Where(l => l.TrimStart().StartsWith("Project(")))
                {
                    var parts = line.Split('"');
                    if (parts.Length > 5 && parts[5].EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
                        result.AddProject(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(input)!, parts[5].Replace('\\', Path.DirectorySeparatorChar))));
                }
            }
            else if (Path.GetExtension(input).Equals(".csproj", StringComparison.OrdinalIgnoreCase)) result.AddProject(Path.GetFullPath(input));
        }
        return result;
    }

    void AddProject(string path)
    {
        var doc = XDocument.Load(path);
        if (doc.Root?.Attribute("Sdk") == null && !doc.Descendants().Any(e => e.Name.LocalName == "Sdk")) return;
        if (!Projects.Add(path)) return;
        var start = new ProcessStartInfo("dotnet") { RedirectStandardOutput = true, RedirectStandardError = true, UseShellExecute = false, CreateNoWindow = true };
        foreach (var arg in new[] { "msbuild", path, "-restore", "-target:ResolveReferences,GenerateGlobalUsings", "-getItem:Compile,ReferencePath", "-getProperty:DefineConstants", "-verbosity:quiet", "-nologo" }) start.ArgumentList.Add(arg);
        using var process = Process.Start(start) ?? throw new InvalidOperationException("Unable to evaluate " + path);
        var stdout = process.StandardOutput.ReadToEndAsync();
        var stderr = process.StandardError.ReadToEndAsync();
        process.WaitForExit();
        var output = stdout.GetAwaiter().GetResult();
        if (process.ExitCode != 0) throw new InvalidOperationException($"Project evaluation failed for {path}: {output}\n{stderr.GetAwaiter().GetResult()}");
        // MSBuild may print restore notices before the JSON result.
        var jsonStart = output.IndexOf('{');
        if (jsonStart < 0) throw new InvalidOperationException("No evaluated project items returned for " + path);
        using var json = JsonDocument.Parse(output.Substring(jsonStart));
        var root = json.RootElement;
        foreach (var item in root.GetProperty("Items").GetProperty("Compile").EnumerateArray())
            Files.Add(item.GetProperty("FullPath").GetString()!);
        foreach (var item in root.GetProperty("Items").GetProperty("ReferencePath").EnumerateArray())
            References.Add(item.GetProperty("FullPath").GetString()!);
        Defines.AddRange(root.GetProperty("Properties").GetProperty("DefineConstants").GetString()!.Split(';', ',', ' '));
    }
}
