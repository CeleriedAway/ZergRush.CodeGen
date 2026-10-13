using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZergRush.Samples;

namespace ZergRush.CodeGen.Tests;

public sealed class JsonArrayFormatTests
{
    static string NodeId => ((ushort)JsonArrayNode.Types.JsonArrayNode).ToString();
    static string ChildId => ((ushort)JsonArrayNode.Types.JsonArrayChild).ToString();
    static string RefId => ((ushort)JsonPolyReference.Types.JsonPolyReferenceChild).ToString();

    public static JsonArrayHolder CreateFixture(int count = 3)
    {
        var result = new JsonArrayHolder { tail = 987 };
        for (int i = 0; i < count; i++)
            result.items.Add(i % 3 == 2 ? null! : new JsonArrayChild
            {
                score = i, label = "2026-10-13T14:15:16.123Z", date = new DateTime(638900000000000001),
                large = ulong.MaxValue, precise = 123456789.987654321m,
                nested = new JsonArrayNode { score = -i, label = "escaped \" / 雪\n" }
            });
        result.array = result.items.ToArray();
        foreach (var item in result.items) result.reactive.Add(item);
        for (int i = 0; i < result.items.Count; i++) result.dictionary.Add("k" + i, result.items[i]);
        result.item = count > 0 ? result.items[0] : null;
        result.cell.value = result.item;
        return result;
    }

    internal static void AssertFixture(JsonArrayHolder expected, JsonArrayHolder actual)
    {
        Assert.Equal(expected.WriteToJsonString(false), actual.WriteToJsonString(false));
        Assert.Equal(expected.items.Count, actual.items.Count);
        for (int i = 0; i < expected.items.Count; i++)
        {
            if (expected.items[i] == null) { Assert.Null(actual.items[i]); continue; }
            var child = Assert.IsType<JsonArrayChild>(actual.items[i]);
            Assert.Equal(i, child.score);
            Assert.Equal("2026-10-13T14:15:16.123Z", child.label);
            Assert.Equal(638900000000000001, child.date.Ticks);
            Assert.Equal(ulong.MaxValue, child.large);
            Assert.Equal(123456789.987654321m, child.precise);
            Assert.IsType<JsonArrayNode>(child.nested);
            Assert.Equal(-i, child.nested.score);
        }
        Assert.Equal(987, actual.tail);
    }

    [Theory]
    [InlineData(0)] [InlineData(1)] [InlineData(6)]
    public void Collections_fields_cells_and_nulls_use_individual_envelopes(int count)
    {
        var source = CreateFixture(count);
        string json = source.WriteToJsonString(false);
        Assert.DoesNotContain("__classId", json);
        var root = JObject.Parse(json);
        if (count > 0) Assert.Equal(2, Assert.IsType<JArray>(root["items"]![0]).Count);
        AssertFixture(source, json.ReadFromJson<JsonArrayHolder>());
    }

    [Fact]
    public void Reads_replace_collections_and_reuse_matching_instances()
    {
        var destination = CreateFixture(6);
        var original = destination.item;
        var source = CreateFixture(1);
        destination.ReadFromJson(source.WriteToJsonString());
        Assert.Same(original, destination.item);
        AssertFixture(source, destination);
        source.item = new JsonArrayNode { score = 83 };
        destination.ReadFromJson(source.WriteToJsonString());
        Assert.IsType<JsonArrayNode>(destination.item);
        Assert.Equal(83, destination.item.score);
        destination.ReadFromJson(CreateFixture(0).WriteToJsonString());
        AssertFixture(CreateFixture(0), destination);
    }

    [Fact]
    public void Concrete_and_factory_roots_validate_type_and_construct_once()
    {
        var source = new JsonArrayChild { score = 7, large = 42, precise = 1.25m };
        var json = source.WriteToJsonString(false);
        JsonArrayNode.Constructions = 0;
        var decoded = JsonPersistence.ReadPolymorphic(json, JsonArrayNode.CreatePolymorphic);
        Assert.Equal(1, JsonArrayNode.Constructions);
        Assert.IsType<JsonArrayChild>(decoded);
        Assert.Equal(7, decoded.score);
        Assert.Equal(json, decoded.WriteToJsonString(false));
        var same = new JsonArrayChild();
        JsonArrayNode.Constructions = 0;
        Assert.Same(same, same.ReadFromJson(json));
        Assert.Equal(0, JsonArrayNode.Constructions);
        Assert.Throws<JsonSerializationException>(() => new JsonArrayNode().ReadFromJson(json));
        Assert.Throws<JsonSerializationException>(() => json.ReadFromJson<JsonArrayNode>());
        Assert.IsType<JsonArrayChild>(json.ReadFromJson<JsonArrayChild>());
        Assert.IsType<JsonArrayChild>(JsonPersistence.ReadPolymorphic<IJsonSerializable>(json, JsonArrayNode.CreatePolymorphic));
    }

    [Fact]
    public void Generic_inheritance_round_trips_without_double_framing()
    {
        var source = CodeGenSamples.CreatePopulatedForTests();
        var json = source.WriteToJsonString(false);
        var result = JsonPersistence.ReadPolymorphic(json, CodeGenSamples.CreatePolymorphic);
        CodeGenSamples.AssertEquivalentForTests(source, result);
        Assert.DoesNotContain("__classId", json);
        Assert.Equal(2, JArray.Parse(json).Count);
    }

    public static JsonReferenceRoot CreateReferences(int backReferences = 12)
    {
        var root = new JsonReferenceRoot { tail = 517 };
        var child = new JsonPolyReferenceChild { score = 13, extra = 27, owner = root };
        var peer = new JsonPolyReference { score = 21, next = child, owner = root };
        child.next = peer;
        root.first = root.second = child;
        root.self = root;
        root.plainFirst = root.plainSecond = new JsonPlainReference { score = 99 };
        root.plainFirst.next = root.plainFirst;
        for (int i = 0; i < backReferences; i++) root.many.Add(child);
        return root;
    }

    internal static void AssertReferences(JsonReferenceRoot root)
    {
        Assert.Same(root, root.self);
        Assert.Same(root.first, root.second);
        Assert.Same(root.first, root.first.next.next);
        Assert.Same(root, root.first.owner);
        Assert.Same(root, root.first.next.owner);
        Assert.Same(root.plainFirst, root.plainSecond);
        Assert.Same(root.plainFirst, root.plainFirst.next);
        Assert.All(root.many, value => Assert.Same(root.first, value));
        Assert.Equal(27, Assert.IsType<JsonPolyReferenceChild>(root.first).extra);
        Assert.Equal(21, root.first.next.score);
        Assert.Equal(99, root.plainFirst.score);
        Assert.Equal(517, root.tail);
    }

    [Fact]
    public void Shared_children_cycles_and_root_back_references_do_not_construct_throwaways()
    {
        var source = CreateReferences(1000);
        string json = source.WriteToJsonString(false);
        JsonPolyReference.Constructions = JsonPlainReference.Constructions = 0;
        var result = JsonPersistence.Read<JsonReferenceRoot>(json);
        Assert.Equal(2, JsonPolyReference.Constructions);
        Assert.Equal(1, JsonPlainReference.Constructions);
        AssertReferences(result);
        var tree = JObject.Parse(json);
        Assert.Equal(4, Assert.IsType<JArray>(tree["first"]).Count);
        Assert.Equal(JTokenType.Null, tree["plainFirst"]![0]!.Type);
        Assert.Equal("1", tree["self"]![1]!.Value<string>());
        Assert.True(tree["self"]![2]!.Value<bool>());
        Assert.Equal(JTokenType.Null, tree["self"]![3]!.Type);
    }

    [Fact]
    public void Polymorphic_reference_root_registers_before_fields()
    {
        var source = new JsonPolyReferenceChild { score = 1, extra = 2 };
        source.next = source;
        var result = JsonPersistence.ReadPolymorphic(source.WriteToJsonString(), JsonPolyReference.CreatePolymorphic);
        Assert.Same(result, result.next);
        Assert.Equal(2, Assert.IsType<JsonPolyReferenceChild>(result).extra);
    }

    [Fact]
    public void Forward_references_construct_once_and_later_definitions_fill_the_same_instances()
    {
        string json = CreateReferences(100).WriteToJsonString(false);
        var root = JObject.Parse(json);
        // PostgreSQL JSONB orders keys by UTF-8 length, then bytes. Put all back-references
        // before the defining fields, including those inside the defining object's payload.
        Reorder(root);
        JsonPolyReference.Constructions = JsonPlainReference.Constructions = 0;
        var result = JsonPersistence.Read<JsonReferenceRoot>(root.ToString(Formatting.None));
        AssertReferences(result);
        Assert.Equal(2, JsonPolyReference.Constructions);
        Assert.Equal(1, JsonPlainReference.Constructions);

        static void Reorder(JToken token)
        {
            if (token is JObject obj)
            {
                var properties = obj.Properties().OrderBy(p => p.Name.Length).ThenBy(p => p.Name, StringComparer.Ordinal).ToArray();
                foreach (var property in properties) { property.Remove(); obj.Add(property); }
            }
            foreach (var child in token.Children()) Reorder(child);
        }
    }

    [Fact]
    public void Reused_root_can_split_previously_shared_references()
    {
        var destination = CreateReferences();
        var source = new JsonReferenceRoot
        {
            first = new JsonPolyReference { score = 7 }, second = new JsonPolyReference { score = 9 }
        };
        destination.ReadFromJson(source.WriteToJsonString(false));
        Assert.NotSame(destination.first, destination.second);
        Assert.Equal(7, destination.first.score);
        Assert.Equal(9, destination.second.score);
    }

    [Fact]
    public void Forward_reference_type_conflicts_and_missing_definitions_are_rejected()
    {
        string json = "{\"first\":[" + RefId + ",\"2\",true,null],\"second\":[" + (ushort)JsonPolyReference.Types.JsonPolyReference + ",\"2\",false,{}]}";
        Assert.Throws<JsonSerializationException>(() => JsonPersistence.Read<JsonReferenceRoot>(json));
        Assert.Throws<JsonSerializationException>(() => JsonPersistence.Read<JsonReferenceRoot>("{\"plainFirst\":[null,\"2\",true,null]}"));
    }

    public static IEnumerable<object[]> InvalidNodes()
    {
        foreach (var header in new[] { "-1", "65536", "99999999999999999999999", "1.0", "\"1\"", "null", "true", "[]", "{}", "0", "65535" })
            yield return new object[] { "[" + header + ",{}]" };
        foreach (var json in new[] { "[]", "[ID]", "[ID,null]", "[ID,[]]", "[ID,{},0]", "[ID,{},{}]",
                     "[ID,{\"__classId\":ID}]", "[ID,{\"refId\":\"2\"}]", "[ID,{\"isRef\":false}]",
                     "{\"__classId\":ID}", "[ID,{}] {}", "[ID,{}] true", "[ID,{}", "[ID,{" })
            yield return new object[] { json.Replace("ID", NodeId) };
    }

    [Theory, MemberData(nameof(InvalidNodes))]
    public void Malformed_polymorphic_frames_are_rejected(string json)
        => AssertRejected(() => JsonPersistence.ReadPolymorphic(json, JsonArrayNode.CreatePolymorphic));

    public static IEnumerable<object[]> InvalidReferences()
    {
        foreach (var json in new[] { "[ID,2,false,{}]", "[ID,\"0\",false,{}]", "[ID,\"-1\",false,{}]",
                     "[ID,\"2\",0,{}]", "[ID,\"2\",\"false\",{}]", "[null,\"2\",false,{}]",
                     "[ID,\"2\",true,{}]", "[ID,\"2\",false,null]", "[ID,\"2\",false,{},0]",
                     "[ID,\"2\",false]", "[ID,\"2\",true,null]", "[ID,\"1\",true,null]",
                     "[ID,\"1\",false,{}]" })
            yield return new object[] { "{\"first\":" + json.Replace("ID", RefId) + "}" };
        yield return new object[] { "{\"first\":[" + RefId + ",\"2\",false,{}],\"second\":[" + RefId + ",\"2\",false,{}]}" };
        yield return new object[] { "{\"first\":[" + RefId + ",\"2\",false,{}],\"second\":[" + (ushort)JsonPolyReference.Types.JsonPolyReference + ",\"2\",true,null]}" };
        yield return new object[] { "{\"plainFirst\":[0,\"2\",false,{}]}" };
        yield return new object[] { "{\"first\":[" + RefId + ",\"2\",false,{}],\"plainFirst\":[null,\"2\",true,null]}" };
    }

    [Theory, MemberData(nameof(InvalidReferences))]
    public void Malformed_references_fail_without_returning_a_partial_root(string json)
        => AssertRejected(() => JsonPersistence.Read<JsonReferenceRoot>(json));

    static void AssertRejected(Action read)
    {
        var error = Record.Exception(read);
        Assert.True(error is JsonException or ZergRushException, error?.ToString() ?? "Malformed JSON was accepted.");
    }

    [Fact]
    public void Every_truncation_is_rejected_and_siblings_remain_readable()
    {
        var source = CreateFixture(3);
        string json = source.WriteToJsonString(false);
        for (int i = 0; i < json.Length; i++)
            AssertRejected(() => JsonPersistence.Read<JsonArrayHolder>(json[..i]));
        AssertFixture(source, JsonPersistence.Read<JsonArrayHolder>(json));
        string references = CreateReferences(2).WriteToJsonString(false);
        for (int i = 0; i < references.Length; i++)
            AssertRejected(() => JsonPersistence.Read<JsonReferenceRoot>(references[..i]));
    }

    [Fact]
    public void Unknown_fields_skip_nested_values_and_escaped_names_dispatch()
    {
        string json = "[" + ChildId + ", {\"unknown\":[{},[1,{\"score\":99}]],\"sc\\u006fre\":17,\"label\":\"雪\"}]";
        var result = JsonPersistence.ReadPolymorphic(json, JsonArrayNode.CreatePolymorphic);
        Assert.Equal(17, result.score);
        Assert.Equal("雪", result.label);
    }

    [Fact]
    public void Null_roots_and_all_entry_points_are_consistent()
    {
        Assert.Null("null".ReadFromJson<JsonArrayChild>());
        Assert.Null(JsonPersistence.Read<JsonArrayChild>("null"));
        Assert.Null(JsonPersistence.ReadPolymorphic("null", JsonArrayNode.CreatePolymorphic));
        using (var nullReader = new ZRJsonTextReader("null")) Assert.Null(nullReader.ReadAsJsonRoot<JsonArrayChild>());
        var source = new JsonArrayChild { label = "2026-10-13T14:15:16+03:00", date = new DateTime(1000000) };
        var json = source.WriteToJsonString(false);
        using (var reader = new ZRJsonTextReader(json)) Assert.Equal(source.label, reader.ReadAsJsonRoot<JsonArrayChild>().label);
        var path = Path.GetTempFileName();
        try
        {
            source.WriteToJsonFile(path, false);
            var result = path.ReadFromJsonFile<JsonArrayChild>();
            Assert.Equal(source.label, result.label);
            Assert.Equal(source.date, result.date);
        }
        finally { File.Delete(path); }
    }

    [Fact]
    public void Depth_collection_reference_and_input_limits_are_enforced()
    {
        string json = CreateFixture(3).WriteToJsonString(false);
        Assert.ThrowsAny<Exception>(() => JsonPersistence.Read<JsonArrayHolder>(json, budget: new DeserializationBudget(1)));
        Assert.ThrowsAny<Exception>(() => JsonPersistence.Read<JsonArrayHolder>(json, budget: new DeserializationBudget(100, 1)));
        Assert.Throws<JsonSerializationException>(() => JsonPersistence.Read<JsonArrayHolder>(json, maxInputCharacters: 3));
        Assert.ThrowsAny<Exception>(() => JsonPersistence.Read<JsonReferenceRoot>(CreateReferences().WriteToJsonString(),
            budget: new DeserializationBudget(100000, 64 * 1024 * 1024, maxReferenceCount: 2)));
        using var reader = new ZRJsonTextReader(json) { MaxDepth = 3 };
        Assert.ThrowsAny<Exception>(() => reader.ReadAsJsonRoot<JsonArrayHolder>());
    }

    [Fact]
    public void Explicit_migrations_preserve_strings_and_validate_root_shape()
    {
        string json = new JsonArrayChild { label = "2026-10-13T14:15:16Z", score = 9 }.WriteToJsonString(false);
        var decoded = JsonPersistence.ReadPolymorphic(json, JsonArrayNode.CreatePolymorphic, migrate: token => token);
        Assert.Equal("2026-10-13T14:15:16Z", decoded.label);
        Assert.Equal(9, decoded.score);
        Assert.Throws<JsonSerializationException>(() => JsonPersistence.Read<JsonArrayChild>(json, root => root));
        Assert.Throws<JsonSerializationException>(() => JsonPersistence.ReadPolymorphic(json, JsonArrayNode.CreatePolymorphic, migrate: _ => null!));
    }

    [Fact]
    public void Stale_generated_readers_fail_with_a_regeneration_diagnostic()
    {
        using var reader = new ZRJsonTextReader("[1,{}]");
        reader.ReadRequired();
        var error = Assert.Throws<JsonSerializationException>(() => reader.ReadJsonClassId());
        Assert.Contains("Regenerate", error.Message);
    }

    [Fact]
    public void Existing_non_polymorphic_objects_keep_the_original_dispatch_and_identity()
    {
        var root = new JsonExistingPlainHolder { optional = new JsonExistingPlain() };
        var required = root.required;
        var optional = root.optional;
        var cell = root.cell.value;
        var list = required.values;
        list.Add(99);
        JsonExistingPlain.Constructions = 0;
        root.ReadFromJson("{\"required\":{\"values\":[3,4],\"score\":7,\"refId\":\"app-id\",\"isRef\":true},\"optional\":{\"score\":8},\"cell\":{\"score\":9}}");
        Assert.Same(required, root.required);
        Assert.Same(optional, root.optional);
        Assert.Same(cell, root.cell.value);
        Assert.Same(list, root.required.values);
        Assert.Equal(new[] { 3, 4 }, list);
        Assert.Equal(7, required.score);
        Assert.Equal(8, optional.score);
        Assert.Equal(9, cell.score);
        Assert.Equal("app-id", required.refId);
        Assert.True(required.isRef);
        Assert.Equal(0, JsonExistingPlain.Constructions);
        // Partial ordinary objects still leave omitted fields alone, and unknown values skip.
        root.ReadFromJson("{\"required\":{\"unknown\":[1,{}],\"score\":17}}");
        Assert.Same(required, root.required);
        Assert.Equal(new[] { 3, 4 }, required.values);
        Assert.Equal(17, required.score);
        Assert.Equal(0, JsonExistingPlain.Constructions);
        using var reader = new ZRJsonTextReader("{\"score\":23}");
        required.ReadRootFromJson(reader);
        Assert.Equal(23, required.score);
        Assert.Equal(0, JsonExistingPlain.Constructions);
        Assert.IsType<JObject>(JObject.Parse(root.WriteToJsonString())["required"]);
    }
}
