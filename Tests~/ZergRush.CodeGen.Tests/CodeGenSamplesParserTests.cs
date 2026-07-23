using ZergRush.CodeGen;

namespace ZergRush.CodeGen.Tests;

public sealed class CodeGenSamplesParserTests
{
    [Fact]
    public void ZRData_stores_only_access_type_and_options()
    {
        var fields = typeof(ZRData).GetFields(
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic);

        Assert.Equal(3, fields.Length);
        Assert.Contains(fields, field => field.Name.Contains(nameof(ZRData.Access), StringComparison.Ordinal));
        Assert.Contains(fields, field => field.Name.Contains(nameof(ZRData.Type), StringComparison.Ordinal));
        Assert.Contains(fields, field => field.Name.Contains(nameof(ZRData.Options), StringComparison.Ordinal));

        var nullable = ZRType.FromSystemType(typeof(int?)).ToData("value");
        Assert.Equal("value", nullable.Access);
        Assert.Equal("int", nullable.Type.FullName);
        Assert.Equal(ZRDataOption.CanBeNull | ZRDataOption.IsNullable, nullable.Options);
        Assert.Equal("value.Value", nullable.ReadAccess);
        Assert.Equal("value.HasValue", nullable.HasValueExpression);
        Assert.Equal("value.HasValue ? (ulong)value.Value : 345093625", CodeGen.HashExpr(nullable));
        Assert.Equal("value.HasValue ? (ulong)value.Value : 345093625", CodeGen.UIdExpr(nullable));
    }

    [Fact]
    public void CodeGenSamples_parses_current_generator_input_model()
    {
        var types = ParseCodeGenSamples();

        Assert.Equal(20, types.Count);

        var sample = FindType(types, "ZergRush.Samples.CodeGenSamples");
        Assert.Equal(ZRTypeKind.Class, sample.Kind);
        Assert.Equal(GenTaskFlags.PolymorphicDataPack, sample.Flags);
        Assert.Equal(34, sample.DataMembers.Count);
        Assert.Contains(sample.ChildTypes, child => child.FullName == "ZergRush.Samples.Ancestor");
        Assert.NotNull(sample.TargetFolder);
        Assert.EndsWith(
            Path.Combine("Samples", "x_generated"),
            sample.TargetFolder!.Folder,
            StringComparison.OrdinalIgnoreCase);

        Assert.Null(FindMemberOrNull(sample, "stringPropWithoutTagNotIncluded"));
        Assert.Equal(GenTaskFlags.All, FindMember(sample, "someTempIgnoredField").IgnoreFlags);
        Assert.Equal(ZRDataOption.CanBeNull, FindMember(sample, "stringFieldThatCanBeNull").ToData().Options);

        AssertMember(sample, "reactiveValue", "ZergRush.Samples.OtherData", "reactiveValue.value", FieldWrapperType.Cell);
        AssertMember(sample, "reactiveNullablePrimitive", "int", "reactiveNullablePrimitive.value", FieldWrapperType.Cell, FieldWrapperType.Nullable);
        AssertMember(sample, "nullablePrimitive", "int", "nullablePrimitive", FieldWrapperType.Nullable);
        Assert.True(FindMember(sample, "reactiveNullablePrimitive").ToData().IsNullable);
        Assert.True(FindMember(sample, "nullablePrimitive").ToData().IsNullable);
        Assert.Equal("nullablePrimitive.Value", FindMember(sample, "nullablePrimitive").ToData().ReadAccess);
        Assert.Equal("nullablePrimitive.HasValue", FindMember(sample, "nullablePrimitive").ToData().HasValueExpression);
        AssertMember(
            sample,
            "nestedReactiveNullablePrimitive",
            "int",
            "nestedReactiveNullablePrimitive.value.value.value",
            FieldWrapperType.Cell,
            FieldWrapperType.Cell,
            FieldWrapperType.Cell,
            FieldWrapperType.Nullable);
    }

    [Fact]
    public void CodeGenSamples_registers_constructed_generic_types_with_data_members()
    {
        var types = ParseCodeGenSamples();

        var generic = FindType(types, "ZergRush.Samples.TestGeneric<int>");

        Assert.True(generic.Options.HasFlag(ZRTypeOption.ConstructedGeneric));
        Assert.True(generic.Options.HasFlag(ZRTypeOption.External));
        Assert.Equal("ZergRush.Samples.TestGeneric<T>", generic.GenericDefinition?.FullName);
        Assert.Equal(4, generic.DataMembers.Count);

        AssertMember(generic, "value", "int", "value");
        AssertMember(generic, "values", "System.Collections.Generic.List<int>", "values");
        AssertMember(generic, "valuesByName", "System.Collections.Generic.Dictionary<string, int>", "valuesByName");
        AssertMember(generic, "reactiveValue", "int", "reactiveValue.value", FieldWrapperType.Cell);
    }

    [Fact]
    public void Generic_hierarchy_registers_automatic_and_attribute_instances()
    {
        var types = ParseCodeGenSamples();

        var definition = FindType(types, "ZergRush.Samples.TestGenericAncestor<T>");
        Assert.True(definition.Options.HasFlag(ZRTypeOption.GenericDefinition));
        Assert.False(definition.Options.HasFlag(ZRTypeOption.DoNotGen));

        var intInstance = FindType(types, "ZergRush.Samples.TestGenericAncestor<int>");
        Assert.Equal("ZergRush.Samples.TestGenericAncestor<T>", intInstance.GenericDefinition?.FullName);
        AssertMember(intInstance, "genericField", "int", "genericField");

        var sampleInstance = FindType(types, "ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>");
        Assert.Equal("ZergRush.Samples.TestGenericAncestor<T>", sampleInstance.GenericDefinition?.FullName);
        AssertMember(sampleInstance, "genericField", "ZergRush.Samples.CodeGenSamples", "genericField");

        Assert.Single(types, type => type.FullName == "ZergRush.Samples.TestGenericAncestor<int>");
    }

    [Fact]
    public void Closed_generic_base_inherits_custom_generation_tags_from_its_definition()
    {
        var expectedFlags = GenTaskFlags.ConfigData & ~GenTaskFlags.PolymorphicConstruction;
        var types = ParseSource("""
            using ZergRush.CodeGen;

            namespace ParserSamples;

            [GenTaskCustomImpl(GenTaskFlags.ConfigData & ~GenTaskFlags.PolymorphicConstruction)]
            public abstract partial class GenericRoot<T> where T : GenericRoot<T>, new()
            {
            }

            public partial class ConcreteRoot : GenericRoot<ConcreteRoot>
            {
                public int value;
            }
            """);

        var definition = FindType(types, "ParserSamples.GenericRoot<T>");
        var concrete = FindType(types, "ParserSamples.ConcreteRoot");

        Assert.Equal(expectedFlags, definition.Flags);
        Assert.True(concrete.BaseType!.IsConstructedGenericType);
        Assert.Same(definition, concrete.BaseType.GenericDefinition);
        Assert.Equal(expectedFlags, concrete.ReadGenFlags());

        Assert.NotNull(definition.GetCustomImplAttr());
        Assert.Null(concrete.GetCustomImplAttr());
    }

    [Fact]
    public void Closed_generic_base_inherits_generation_tags_from_external_definition()
    {
        var expectedFlags = GenTaskFlags.ConfigData & ~GenTaskFlags.PolymorphicConstruction;
        var types = ParseSource("""
            using ZergRush.Alive;

            namespace ParserSamples;

            public partial class ExternalConfig : GameConfigRoot<ExternalConfig>
            {
                public int value;
            }
            """);

        var concrete = FindType(types, "ParserSamples.ExternalConfig");

        Assert.True(concrete.BaseType!.IsConstructedGenericType);
        Assert.Equal("ZergRush.Alive.GameConfigRoot<T>", concrete.BaseType.GenericDefinition?.FullName);
        Assert.Equal(expectedFlags, concrete.ReadGenFlags());
        Assert.Null(concrete.GetCustomImplAttr());
    }

    [Fact]
    public void Generic_instance_attribute_resolves_CSharp_type_syntax_in_declaration_context()
    {
        var types = ParseSource("""
            using System.Collections.Generic;
            using ZergRush.CodeGen;

            namespace ParserSamples;

            public sealed class CustomArg {}

            [GenRegGenericInstance("int")]
            [GenRegGenericInstance("CustomArg")]
            [GenRegGenericInstance("List<int>")]
            [GenRegGenericInstance("int[]")]
            [GenRegGenericInstance("int?")]
            public class Generic<T>
            {
                public T value;
            }
            """);

        Assert.Contains(types, type => type.FullName == "ParserSamples.Generic<int>");
        Assert.Contains(types, type => type.FullName == "ParserSamples.Generic<ParserSamples.CustomArg>");
        Assert.Contains(types, type => type.FullName == "ParserSamples.Generic<System.Collections.Generic.List<int>>");
        Assert.Contains(types, type => type.FullName == "ParserSamples.Generic<int[]>");
        Assert.Contains(types, type => type.FullName == "ParserSamples.Generic<int?>");
    }

    [Fact]
    public void Constructed_generic_instances_are_discovered_recursively_from_method_signatures()
    {
        var types = ParseSource("""
            using System.Collections.Generic;

            namespace ParserSamples;

            public class Generic<T>
            {
                public T value;
            }

            public class Consumer
            {
                public List<Generic<int>> Convert(Generic<string>[] values) => null;
            }
            """);

        Assert.Contains(types, type => type.FullName == "ParserSamples.Generic<int>");
        Assert.Contains(types, type => type.FullName == "ParserSamples.Generic<string>");
    }

    [Fact]
    public void Constants_are_not_registered_as_instance_fields()
    {
        var types = ParseSource("""
            namespace ParserSamples;

            public class ConstantsOwner
            {
                public const int Constant = 1;
                public static int StaticValue;
                public readonly int ReadOnlyValue;
                public int Value;
            }
            """);

        var type = Assert.Single(types, type => type.FullName == "ParserSamples.ConstantsOwner");
        Assert.DoesNotContain(type.Members, member => member.Name == "Constant");
        Assert.DoesNotContain(type.Members, member => member.Name == "StaticValue");
        Assert.Contains(type.Members, member => member.Name == "ReadOnlyValue");
        Assert.Contains(type.Members, member => member.Name == "Value");
    }

    [Fact]
    public void Generic_instance_attribute_rejects_invalid_usage()
    {
        var misplaced = Assert.Throws<InvalidOperationException>(() => ParseSource("""
            using ZergRush.CodeGen;

            [GenRegGenericInstance("int")]
            public class NotGeneric {}
            """));
        Assert.Contains("exactly one type parameter", misplaced.Message);

        var unresolved = Assert.Throws<InvalidOperationException>(() => ParseSource("""
            using ZergRush.CodeGen;

            [GenRegGenericInstance("MissingType")]
            public class Generic<T> {}
            """));
        Assert.Contains("could not resolve", unresolved.Message);

        var open = Assert.Throws<InvalidOperationException>(() => ParseSource("""
            using ZergRush.CodeGen;

            [GenRegGenericInstance("T")]
            public class Generic<T> {}
            """));
        Assert.Contains("closed non-void type", open.Message);

        var constraint = Assert.Throws<InvalidOperationException>(() => ParseSource("""
            using ZergRush.CodeGen;

            [GenRegGenericInstance("string")]
            public class Generic<T> where T : struct {}
            """));
        Assert.Contains("does not satisfy the generic constraints", constraint.Message);
    }

    [Fact]
    public void Directory_inputs_ignore_existing_generated_sources()
    {
        var root = Path.Combine(Path.GetTempPath(), $"ZergRushParserDirectoryTests_{Guid.NewGuid():N}");
        var generated = Path.Combine(root, "x_generated");
        Directory.CreateDirectory(generated);
        File.WriteAllText(Path.Combine(root, "Source.cs"), "public class SourceType {}");
        File.WriteAllText(Path.Combine(root, "Old.gen.cs"), "public class OldGeneratedType {}");
        File.WriteAllText(Path.Combine(root, "Old.enum.cs"), "public enum OldGeneratedEnum {}");
        File.WriteAllText(Path.Combine(generated, "Nested.cs"), "public class NestedGeneratedType {}");

        try
        {
            var types = new ZRCodeParser().ParseInputs([root]);

            Assert.Contains(types, type => type.FullName == "SourceType");
            Assert.DoesNotContain(types, type => type.FullName.Contains("Generated", StringComparison.Ordinal));
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }

    [Fact]
    public void Config_storage_wrappers_are_classified_as_collections()
    {
        var types = ParseSource("""
            using ZergRush.Alive;

            namespace ParserSamples;

            public class StorageOwner
            {
                public ConfigStorageList<LoadableConfig> list = new();
                public ConfigStorageDict<string, LoadableConfig> dictionary = new();
                public ConfigStorageSlot<LoadableConfig> slot = new();
            }
            """);

        var owner = Assert.Single(types, type => type.FullName == "ParserSamples.StorageOwner");
        var list = FindMember(owner, "list").DeclaredType;
        var dictionary = FindMember(owner, "dictionary").DeclaredType;
        var slot = FindMember(owner, "slot").DeclaredType;

        Assert.True(list.IsList());
        Assert.True(dictionary.IsDictionary());
        Assert.True(slot.IsList());
        Assert.True(list.IsConfigStorage());
        Assert.True(dictionary.IsConfigStorage());
        Assert.True(slot.IsConfigStorage());
    }

    static IReadOnlyList<ZRType> ParseCodeGenSamples()
    {
        var parser = new ZRCodeParser();
        return parser.ParseInputs([CodeGenSamplesPath()]);
    }

    static IReadOnlyList<ZRType> ParseSource(string source)
    {
        var path = Path.Combine(Path.GetTempPath(), $"ZergRushParserTests_{Guid.NewGuid():N}.cs");
        File.WriteAllText(path, source);
        try
        {
            return new ZRCodeParser().ParseInputs([path]);
        }
        finally
        {
            File.Delete(path);
        }
    }

    static string CodeGenSamplesPath()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Samples", "CodeGenSamples.cs");
        if (File.Exists(path)) return path;

        throw new FileNotFoundException("Could not find the copied CodeGenSamples.cs test fixture.", path);
    }

    static ZRType FindType(IReadOnlyList<ZRType> types, string fullName)
    {
        var type = types.SingleOrDefault(t => t.FullName == fullName);
        Assert.NotNull(type);
        return type;
    }

    static ZRMember FindMember(ZRType type, string name)
    {
        var member = FindMemberOrNull(type, name);
        Assert.NotNull(member);
        return member;
    }

    static ZRMember? FindMemberOrNull(ZRType type, string name)
    {
        return type.Members.SingleOrDefault(member => member.Name == name);
    }

    static void AssertMember(ZRType type, string name, string fullTypeName, string access, params FieldWrapperType[] wrappers)
    {
        var member = FindMember(type, name);
        var data = member.ToData();
        Assert.Equal(fullTypeName, data.Type.FullName);
        Assert.Equal(access, data.Access);
        Assert.Equal(wrappers, member.WrapperTypes);

        var syntheticData = member.DeclaredType!.ToData("item");
        Assert.Equal(fullTypeName, syntheticData.Type.FullName);
        Assert.Equal("item" + access[name.Length..], syntheticData.Access);
        Assert.Equal(data.IsNullable, syntheticData.IsNullable);
    }
}
