using ZergRush.CodeGen;
using ZergRush;
using ZergRush.Alive;

namespace ZergRush.CodeGen.Tests;

public sealed class UnityMigrationRegressionTests
{
    [Fact]
    public void Metadata_value_type_fields_survive_copy_and_serialization()
    {
        var source = new ExternalVectorHolder { position = new System.Numerics.Vector3(2, -3, 4) };
        var loaded = source.WriteToByteArray().Read<ExternalVectorHolder>();
        var copied = new ExternalVectorHolder();
        copied.UpdateFrom(source);
        Assert.Equal(source.position, loaded.position);
        Assert.Equal(source.position, copied.position);
    }

    [Fact]
    public void Project_defines_enable_conditional_generation_inputs()
    {
        WithSource("""
            #if UNITY_EDITOR
            public class EditorInput { public int value; }
            #endif
            """, (_, output) =>
        {
            var project = Path.Combine(Path.GetDirectoryName(output)!, "Input.csproj");
            File.WriteAllText(project, "<Project><PropertyGroup><DefineConstants>UNITY_EDITOR</DefineConstants></PropertyGroup><ItemGroup><Compile Include=\"Input.cs\" /></ItemGroup></Project>");
            Assert.Contains(new ZRCodeParser().ParseInputs(new[] { project }), type => type.FullName == "EditorInput");
        });
    }

    [Fact]
    public void Runtime_livable_contract_supports_generated_roots_and_round_trip()
    {
        var root = new SerializableLivableRoot();
        root.items.Add(new SerializableLivableLeaf { value = 42 });
        var loaded = root.WriteToByteArray().Read<SerializableLivableRoot>();
        loaded.__PropagateHierarchy();
        Assert.Equal(42, loaded.items[0].value);
        Assert.Same(loaded.items[0], loaded.GetLivableChild(root.items[0].GetLivableAddress()));
    }

    [Fact]
    public void Simple_list_round_trip_and_copy_preserve_count_and_contents()
    {
        var source = new SimpleListHolder();
        source.values.Add(12);
        source.values.Add(34);
        var loaded = source.WriteToByteArray().Read<SimpleListHolder>();
        var copied = new SimpleListHolder();
        copied.UpdateFrom(source);
        Assert.Equal(new[] { 12, 34 }, loaded.values.ToArray());
        Assert.Equal(new[] { 12, 34 }, copied.values.ToArray());
    }

    [Fact]
    public void Include_flags_do_not_serialize_computed_uid_properties()
    {
        WithSource("""
            using ZergRush.CodeGen;
            [GenTask(GenTaskFlags.SimpleDataPack | GenTaskFlags.UIDGen)] partial class Holder
            { [GenInclude(GenTaskFlags.UIDGen), UIDComponent] public int uid => 42; }
            """, (types, _) =>
        {
            var type = types.Single(t => t.FullName == "Holder");
            Assert.DoesNotContain(type.GetMembersForCodeGen(GenTaskFlags.Deserialize), m => m.Name == "uid");
            Assert.Contains(type.GetMembersForCodeGen(GenTaskFlags.UIDGen), m => m.Name == "uid");
        });
    }

    [Fact]
    public void Array_extension_update_emits_valid_local_identifiers()
    {
        WithSource("""
            using ZergRush.CodeGen;
            public struct Data { public int[] values; }
            [GenTask(GenTaskFlags.SimpleDataPack)] partial class Holder { public Data data; }
            """, (types, output) =>
        {
            new CodeGenSession(types, output).Generate();
            foreach (var file in Directory.GetFiles(output, "*.cs"))
                Assert.DoesNotContain(Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(File.ReadAllText(file)).GetDiagnostics(),
                    diagnostic => diagnostic.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error);
        });
    }

    [Fact]
    public void Array_custom_serializers_apply_to_the_member_type()
    {
        WithSource("""
            using ZergRush;
            using ZergRush.CodeGen;
            [GenTask(GenTaskFlags.SimpleDataPack)] partial class Holder { public int[] values; }
            static class Protocol { public static void Serialize(this int[] values, ZRBinaryWriter writer) {} }
            """, (types, _) =>
        {
            var array = types.Single(t => t.FullName == "Holder").Members.Single().MemberType;
            Assert.True((array.CustomImplementFlags & GenTaskFlags.Serialize) != 0);
        });
    }

    [Fact]
    public void Derived_just_data_attributes_survive_generic_specialization()
    {
        WithSource("""
            using ZergRush.CodeGen;
            class Arg : JustData { public Arg(int index) {} }
            class Arg1 : Arg { public Arg1() : base(1) {} }
            [GenTask(GenTaskFlags.SimpleDataPack)] partial class Model<T> { [Arg1] public T value; }
            class UsesModel { public Model<int> model; }
            """, (types, _) =>
        {
            foreach (var type in types.Where(t => t.FullName.StartsWith("Model<")))
                Assert.True(type.Members.Single(m => m.Name == "value").ToData().JustData);
        });
    }

    [Fact]
    public void Member_type_ignore_flags_are_honored_for_generic_events()
    {
        WithSource("""
            using ZergRush.CodeGen;
            [GenIgnore(GenTaskFlags.Serialization)] class Event<T> {}
            [GenTask(GenTaskFlags.SimpleDataPack)] partial class Model { public Event<int> changed; public int value; }
            """, (types, _) =>
        {
            var model = types.Single(t => t.FullName == "Model");
            Assert.DoesNotContain(model.GetMembersForCodeGen(GenTaskFlags.Serialize), m => m.Name == "changed");
            Assert.Contains(model.GetMembersForCodeGen(GenTaskFlags.DefaultConstructor), m => m.Name == "changed");
        });
    }

    [Fact]
    public void Abstract_polymorphic_leaf_can_be_used_as_a_nullable_reference()
    {
        WithSource("""
            using ZergRush.CodeGen;
            [GenTask(GenTaskFlags.PolymorphicDataPack)] abstract partial class AbstractLeaf {}
            [GenTask(GenTaskFlags.SimpleDataPack)] partial class Holder { [CanBeNull] public AbstractLeaf leaf; }
            """, (types, output) =>
        {
            new CodeGenSession(types, output).Generate();
            Assert.True(types.Single(t => t.FullName == "AbstractLeaf").CanBeAncestor());
            Assert.Contains("CreatePolymorphic", File.ReadAllText(Path.Combine(output, "Holder.gen.cs")));
        });
    }

    [Fact]
    public void Invalid_generation_throws_instead_of_reporting_success()
    {
        WithSource("""
            using ZergRush.CodeGen;
            using ZergRush.Alive;
            [GenTask(GenTaskFlags.LivableNodePack)] partial class InvalidOwner : Livable { public InvalidOwner child; }
            """, (types, output) =>
        {
            Assert.Throws<InvalidOperationException>(() => new CodeGenSession(types, output).Generate());
            Assert.False(File.Exists(Path.Combine(output, "InvalidOwner.gen.cs")));
        });
    }

    static void WithSource(string source, Action<IReadOnlyList<ZRType>, string> check)
    {
        var directory = Path.Combine(Path.GetTempPath(), "ZergRushMigrationTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            var file = Path.Combine(directory, "Input.cs");
            File.WriteAllText(file, source);
            var types = new ZRCodeParser().ParseFiles(new[] { file });
            var output = Path.Combine(directory, "Generated");
            foreach (var type in types) type.TargetFolder = new ZRTargetFolderInfo { Folder = output, Priority = 100 };
            check(types, output);
        }
        finally { Directory.Delete(directory, true); }
    }
}
