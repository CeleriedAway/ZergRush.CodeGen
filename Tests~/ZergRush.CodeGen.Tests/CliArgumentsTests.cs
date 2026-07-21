using ZergRush.CodeGen;

namespace ZergRush.CodeGen.Tests;

public sealed class CliArgumentsTests
{
    [Fact]
    public void Generation_preserves_target_folders_by_default_with_flat_output_as_opt_in()
    {
        var local = ZRCodeGenCliArguments.Parse(["-p", "Game.csproj", "--generate", "Generated"]);
        var flat = ZRCodeGenCliArguments.Parse([
            "-p", "Game.csproj", "--generate", "Generated", "--single-output-folder"
        ]);

        Assert.True(local.PreserveTargetFolders);
        Assert.False(flat.PreserveTargetFolders);
    }

    [Fact]
    public void Search_up_resolves_nearest_typed_project()
    {
        using var tree = new TempTree();
        var project = tree.Write("Assembly-CSharp.csproj", "<Project />");
        var assets = tree.Directory("Assets/Game/Scripts");
        var options = ZRCodeGenCliArguments.Parse(["--search-up", "-p", "Assembly-CSharp.csproj"]);

        var resolved = options.ResolveInputs(assets);

        Assert.Equal(ZRCodeGenSearchMode.Up, options.SearchMode);
        Assert.Equal([project], resolved);
    }

    [Fact]
    public void Search_down_without_value_discovers_all_files_of_the_selected_kind()
    {
        using var tree = new TempTree();
        var first = tree.Write("Client/Client.csproj", "<Project />");
        var second = tree.Write("Server/Nested/Server.csproj", "<Project />");
        tree.Write("Server/Nested/Ignored.cs", "public class Ignored {}");
        var options = ZRCodeGenCliArguments.Parse(["--search-down", "-p"]);

        var resolved = options.ResolveInputs(tree.Root);

        Assert.Equal([first, second], resolved);
    }

    [Fact]
    public void File_project_and_solution_inputs_can_be_combined()
    {
        using var tree = new TempTree();
        var source = tree.Write("Direct.cs", "public class Direct {}");
        var project = tree.Write("Project.csproj", "<Project />");
        var solution = tree.Write("Game.sln", "");
        var options = ZRCodeGenCliArguments.Parse([
            "-f", "Direct.cs",
            "-p", "Project.csproj",
            "-s", "Game.sln"
        ]);

        var resolved = options.ResolveInputs(tree.Root);

        Assert.Equal([source, solution, project], resolved);
    }

    [Fact]
    public void Solution_input_expands_its_projects_and_source_files()
    {
        using var tree = new TempTree();
        var clientSource = tree.Write("Client/Client.cs", "public class Client {}");
        var serverSource = tree.Write("Server/Server.cs", "public class Server {}");
        tree.Write("Client/Client.csproj", "<Project><ItemGroup><Compile Include=\"Client.cs\" /></ItemGroup></Project>");
        tree.Write("Server/Server.csproj", "<Project><ItemGroup><Compile Include=\"Server.cs\" /></ItemGroup></Project>");
        var solution = tree.Write("Game.sln", """
            Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Client", "Client\Client.csproj", "{11111111-1111-1111-1111-111111111111}"
            EndProject
            Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Server", "Server\Server.csproj", "{22222222-2222-2222-2222-222222222222}"
            EndProject
            """);

        var files = ZRCodeParser.ExpandInputFiles([solution]);

        Assert.Equal([clientSource, serverSource], files);
    }

    [Fact]
    public void Conflicting_search_modes_and_untyped_inputs_are_rejected()
    {
        Assert.Throws<ArgumentException>(() =>
            ZRCodeGenCliArguments.Parse(["--search-up", "--search-down", "-p", "Game.csproj"]));
        Assert.Throws<ArgumentException>(() => ZRCodeGenCliArguments.Parse(["Game.csproj"]));
        Assert.Throws<ArgumentException>(() => ZRCodeGenCliArguments.Parse(["-p", "Game.sln"]));
    }

    sealed class TempTree : IDisposable
    {
        public string Root { get; } = Path.Combine(Path.GetTempPath(), $"ZergRushCliTests_{Guid.NewGuid():N}");

        public TempTree() => System.IO.Directory.CreateDirectory(Root);

        public string Directory(string relativePath)
        {
            var path = Path.GetFullPath(Path.Combine(Root, relativePath));
            System.IO.Directory.CreateDirectory(path);
            return path;
        }

        public string Write(string relativePath, string content)
        {
            var path = Path.GetFullPath(Path.Combine(Root, relativePath));
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, content);
            return path;
        }

        public void Dispose()
        {
            if (System.IO.Directory.Exists(Root)) System.IO.Directory.Delete(Root, true);
        }
    }
}
