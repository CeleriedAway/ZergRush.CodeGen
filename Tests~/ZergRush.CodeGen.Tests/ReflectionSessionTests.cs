using System.Reflection;
using ZergRush.CodeGen;

namespace ZergRush.CodeGen.Tests;

public sealed class ReflectionSessionTests
{
    [Fact]
    public void Reflection_converter_builds_complete_ZRType_surface()
    {
        var converter = new ZRReflectionTypeConverter();
        var type = converter.Convert(typeof(ReflectionModel));

        Assert.Same(typeof(ReflectionModel), type.TryResolveSystemType());
        Assert.Equal(GenTaskFlags.Serialization, type.Flags);
        Assert.Same(typeof(ReflectionConfig), type.ConfigRootType!.TryResolveSystemType());
        Assert.Equal(nameof(ReflectionModel.Value), Assert.Single(type.Members).Name);

        var method = Assert.Single(type.Methods, method => method.Name == nameof(ReflectionModel.Execute));
        Assert.Equal(typeof(Task<int>), method.ReturnType.TryResolveSystemType());
        Assert.Equal("System.Threading.Tasks.Task<int>", method.ReturnType.RealName(true));
        var taskDefinition = ZRType.FromSystemType(typeof(Task<>));
        Assert.True(Assert.Single(taskDefinition.GenericArguments).IsGenericParameter);
        Assert.Equal("System.Threading.Tasks.Task<int>",
            taskDefinition.EnrichGeneric(ZRType.FromSystemType(typeof(int))).RealName(true));
        Assert.True(method.HasAttribute<ReflectionCommandAttribute>());
        Assert.Equal(typeof(string), Assert.Single(method.Parameters).ParameterType.TryResolveSystemType());
    }

    [Fact]
    public void Session_runs_explicit_generator_without_CodeGenExtension()
    {
        var output = Path.Combine(Path.GetTempPath(), "zr-session-" + Guid.NewGuid().ToString("N"));
        try
        {
            var session = CodeGenSession.FromReflection(new[] { typeof(ReflectionModel) }, output);
            session.GenerateBuiltInTasks = false;
            session.AddGenerator(activeSession =>
            {
                var module = activeSession.GetContext(activeSession.Types[0])
                    .createSharpCustomModule("ExplicitSession");
                module.content("public static class ExplicitSessionOutput {}");
            });

            session.Generate();

            var generated = Path.Combine(output, "ExplicitSession.gen.cs");
            Assert.True(File.Exists(generated));
            Assert.Contains("ExplicitSessionOutput", File.ReadAllText(generated));
        }
        finally
        {
            if (Directory.Exists(output)) Directory.Delete(output, true);
        }
    }

    [GenTask(GenTaskFlags.Serialization), ConfigRootType(typeof(ReflectionConfig))]
    sealed class ReflectionModel
    {
        public int Value;

        [ReflectionCommand]
        public Task<int> Execute(string input) => Task.FromResult(input.Length);
    }

    sealed class ReflectionConfig
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    sealed class ReflectionCommandAttribute : Attribute
    {
    }
}
