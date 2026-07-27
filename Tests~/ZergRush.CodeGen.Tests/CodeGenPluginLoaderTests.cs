using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ZergRush.CodeGen;

namespace ZergRush.CodeGen.Tests;

public sealed class CodeGenPluginLoaderTests
{
    [Fact]
    public void Attributed_entry_points_are_discovered_in_deterministic_order_with_shared_contract_identity()
    {
        using var tree = new TempTree();
        var plugin = tree.CompilePlugin("""
            using ZergRush.CodeGen;

            public static class SecondPlugin
            {
                [CodeGenPluginEntryPoint]
                public static void Configure(CodeGenPluginContext context) { }
            }

            public static class FirstPlugin
            {
                [CodeGenPluginEntryPoint]
                public static void Zeta(CodeGenPluginContext context) { }

                [CodeGenPluginEntryPoint]
                public static void Alpha(CodeGenPluginContext context) { }
            }
            """);
        File.Copy(typeof(CodeGenSession).Assembly.Location,
            Path.Combine(tree.Root, Path.GetFileName(typeof(CodeGenSession).Assembly.Location)));
        var session = new CodeGenSession([], tree.Root);

        var loaded = CodeGenPluginLoader.LoadAndConfigure(
            [plugin], session, [], tree.Root, tree.Root);

        Assert.Equal([
            "FirstPlugin.Alpha",
            "FirstPlugin.Zeta",
            "SecondPlugin.Configure"
        ], loaded.Select(entry => entry.EntryPoint));
    }

    [Fact]
    public void Invalid_entry_point_signatures_and_missing_entry_points_fail_before_generation()
    {
        using var tree = new TempTree();
        var invalid = tree.CompilePlugin("""
            using ZergRush.CodeGen;

            public static class InvalidPlugin
            {
                [CodeGenPluginEntryPoint]
                public static int Configure(CodeGenPluginContext context) => 0;
            }
            """, "InvalidPlugin");
        var empty = tree.CompilePlugin("public static class EmptyPlugin { }", "EmptyPlugin");
        var session = new CodeGenSession([], tree.Root);

        var invalidError = Assert.Throws<CodeGenPluginException>(() =>
            CodeGenPluginLoader.LoadAndConfigure([invalid], session, [], tree.Root, tree.Root));
        var emptyError = Assert.Throws<CodeGenPluginException>(() =>
            CodeGenPluginLoader.LoadAndConfigure([empty], session, [], tree.Root, tree.Root));

        Assert.Contains("public, static, non-generic void method", invalidError.Message);
        Assert.Contains("has no methods marked", emptyError.Message);
    }

    [Fact]
    public void All_entry_points_are_discovered_before_any_plugin_is_invoked()
    {
        using var tree = new TempTree();
        var marker = Path.Combine(tree.Root, "invoked.txt");
        var valid = tree.CompilePlugin($$"""
            using System.IO;
            using ZergRush.CodeGen;

            public static class ValidPlugin
            {
                [CodeGenPluginEntryPoint]
                public static void Configure(CodeGenPluginContext context) =>
                    File.WriteAllText(@"{{marker}}", "invoked");
            }
            """, "ValidPlugin");
        var invalid = tree.CompilePlugin("""
            using ZergRush.CodeGen;

            public static class InvalidPlugin
            {
                [CodeGenPluginEntryPoint]
                public static int Configure(CodeGenPluginContext context) => 0;
            }
            """, "InvalidPlugin");
        var session = new CodeGenSession([], tree.Root);

        Assert.Throws<CodeGenPluginException>(() =>
            CodeGenPluginLoader.LoadAndConfigure(
                [valid, invalid], session, [], tree.Root, tree.Root));
        Assert.False(File.Exists(marker));
    }

    [Fact]
    public void Entry_point_exceptions_include_plugin_and_entry_point_identity()
    {
        using var tree = new TempTree();
        var plugin = tree.CompilePlugin("""
            using System;
            using ZergRush.CodeGen;

            public static class ThrowingPlugin
            {
                [CodeGenPluginEntryPoint]
                public static void Configure(CodeGenPluginContext context) =>
                    throw new InvalidOperationException("plugin exploded");
            }
            """);
        var session = new CodeGenSession([], tree.Root);

        var error = Assert.Throws<CodeGenPluginException>(() =>
            CodeGenPluginLoader.LoadAndConfigure([plugin], session, [], tree.Root, tree.Root));

        Assert.Equal(plugin, error.PluginPath);
        Assert.Equal("ThrowingPlugin.Configure", error.EntryPoint);
        Assert.IsType<InvalidOperationException>(error.InnerException);
        Assert.Equal("plugin exploded", error.InnerException!.Message);
    }

    [Fact]
    public void Generator_exceptions_include_plugin_and_entry_point_identity()
    {
        using var tree = new TempTree();
        var plugin = tree.CompilePlugin("""
            using System;
            using ZergRush.CodeGen;

            public static class ThrowingGeneratorPlugin
            {
                [CodeGenPluginEntryPoint]
                public static void Configure(CodeGenPluginContext context) =>
                    context.AddGenerator(session =>
                        throw new InvalidOperationException("generator exploded"));
            }
            """);
        var session = new CodeGenSession([], tree.Root)
        {
            GenerateBuiltInTasks = false
        };
        CodeGenPluginLoader.LoadAndConfigure(
            [plugin], session, [], tree.Root, tree.Root);

        var error = Assert.Throws<CodeGenPluginException>(() => session.Generate());

        Assert.Equal(plugin, error.PluginPath);
        Assert.Equal("ThrowingGeneratorPlugin.Configure", error.EntryPoint);
        Assert.IsType<InvalidOperationException>(error.InnerException);
        Assert.Equal("generator exploded", error.InnerException!.Message);
    }

    [Fact]
    public void Plugin_private_dependencies_are_loaded_from_the_plugin_directory()
    {
        using var tree = new TempTree();
        var helper = tree.CompileAssembly("""
            public static class PrivatePluginHelper
            {
                public static string Value => "loaded";
            }
            """, "PrivatePluginHelper");
        var plugin = tree.CompilePlugin("""
            using System;
            using ZergRush.CodeGen;

            public static class PluginWithDependency
            {
                [CodeGenPluginEntryPoint]
                public static void Configure(CodeGenPluginContext context)
                {
                    if (PrivatePluginHelper.Value != "loaded")
                        throw new InvalidOperationException("helper was not loaded");
                }
            }
            """, "PluginWithDependency", [helper]);
        var session = new CodeGenSession([], tree.Root);

        var loaded = CodeGenPluginLoader.LoadAndConfigure(
            [plugin], session, [], tree.Root, tree.Root);

        Assert.Equal("PluginWithDependency.Configure", Assert.Single(loaded).EntryPoint);
    }

    [Fact]
    public void Missing_plugin_dependencies_report_the_plugin_and_entry_point()
    {
        using var tree = new TempTree();
        var helper = tree.CompileAssembly("""
            public static class MissingPluginHelper
            {
                public static void Run() { }
            }
            """, "MissingPluginHelper");
        var plugin = tree.CompilePlugin("""
            using ZergRush.CodeGen;

            public static class PluginWithMissingDependency
            {
                [CodeGenPluginEntryPoint]
                public static void Configure(CodeGenPluginContext context) =>
                    MissingPluginHelper.Run();
            }
            """, "PluginWithMissingDependency", [helper]);
        File.Delete(helper);
        var session = new CodeGenSession([], tree.Root);

        var error = Assert.Throws<CodeGenPluginException>(() =>
            CodeGenPluginLoader.LoadAndConfigure([plugin], session, [], tree.Root, tree.Root));

        Assert.Equal(plugin, error.PluginPath);
        Assert.Equal("PluginWithMissingDependency.Configure", error.EntryPoint);
        Assert.IsType<FileNotFoundException>(error.InnerException);
    }

    [Fact]
    public void Multiple_plugins_register_generators_for_one_generation_batch()
    {
        using var tree = new TempTree();
        var first = tree.CompilePlugin("""
            using System.IO;
            using ZergRush.CodeGen;

            public static class FirstOutputPlugin
            {
                [CodeGenPluginEntryPoint]
                public static void Configure(CodeGenPluginContext context)
                {
                    context.AddGenerator(session =>
                        File.WriteAllText(Path.Combine(context.OutputDirectory, "first.txt"),
                            session.OutputPath));
                }
            }
            """, "FirstOutputPlugin");
        var second = tree.CompilePlugin("""
            using System;
            using System.IO;
            using ZergRush.CodeGen;

            public static class SecondOutputPlugin
            {
                [CodeGenPluginEntryPoint]
                public static void Configure(CodeGenPluginContext context)
                {
                    context.AddGenerator(session =>
                    {
                        var first = Path.Combine(context.OutputDirectory, "first.txt");
                        if (!File.Exists(first)) throw new InvalidOperationException("first output was cleaned");
                        File.WriteAllText(Path.Combine(context.OutputDirectory, "second.txt"),
                            session.OutputPath);
                    });
                }
            }
            """, "SecondOutputPlugin");
        var output = Path.Combine(tree.Root, "output");
        Directory.CreateDirectory(output);
        var session = new CodeGenSession([], output)
        {
            GenerateBuiltInTasks = false
        };

        CodeGenPluginLoader.LoadAndConfigure(
            [first, second], session, [], tree.Root, output);
        session.Generate();

        Assert.Equal(output, File.ReadAllText(Path.Combine(output, "first.txt")));
        Assert.Equal(output, File.ReadAllText(Path.Combine(output, "second.txt")));
    }

    sealed class TempTree : IDisposable
    {
        public string Root { get; } =
            Path.Combine(Path.GetTempPath(), $"ZergRushPluginTests_{Guid.NewGuid():N}");

        public TempTree() => Directory.CreateDirectory(Root);

        public string CompilePlugin(
            string source,
            string assemblyName = "TestPlugin",
            IReadOnlyList<string>? additionalReferences = null)
        {
            var references = new List<string>
            {
                typeof(CodeGenSession).Assembly.Location
            };
            if (additionalReferences != null) references.AddRange(additionalReferences);
            return CompileAssembly(source, assemblyName, references);
        }

        public string CompileAssembly(
            string source,
            string assemblyName,
            IReadOnlyList<string>? additionalReferences = null)
        {
            var output = Path.Combine(Root, assemblyName + ".dll");
            var trustedAssemblies = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
                .Split(Path.PathSeparator)
                .Select(path => MetadataReference.CreateFromFile(path));
            var references = trustedAssemblies.ToList();
            if (additionalReferences != null)
                references.AddRange(additionalReferences.Select(path => MetadataReference.CreateFromFile(path)));
            var compilation = CSharpCompilation.Create(
                assemblyName,
                [CSharpSyntaxTree.ParseText(source)],
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var result = compilation.Emit(output);
            Assert.True(result.Success,
                string.Join(Environment.NewLine, result.Diagnostics.Select(diagnostic => diagnostic.ToString())));
            return output;
        }

        public void Dispose()
        {
            try
            {
                if (Directory.Exists(Root)) Directory.Delete(Root, true);
            }
            catch (UnauthorizedAccessException)
            {
                // Plugin assemblies stay loaded until the test process exits.
            }
            catch (IOException)
            {
                // Windows may keep a loaded plugin assembly locked.
            }
        }
    }
}
