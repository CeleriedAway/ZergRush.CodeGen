using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZergRush;

namespace ZergRush.CodeGen.Tests;

public class SerializationSafetyTests
{
    static SafetySnapshot Snapshot(int count)
    {
        var value = new SafetySnapshot();
        for (int i = 0; i < count; i++)
        {
            value.leaderboard.Add(new SafetyEntry { playerId = 101 + i, score = 20 - i });
            value.idToPlace.Add(101 + i, i);
            value.list.Add(i);
            value.reactiveValues.Add(i);
            value.nullableEntries.Add(i, i == 0 ? null! : new SafetyEntry { playerId = i });
        }
        value.array = Enumerable.Range(0, count).Select(i => (long)i).ToArray();
        value.ints = Enumerable.Range(0, count).ToArray();
        return value;
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Reused_destinations_replace_all_collection_contents(bool binary)
    {
        var target = Snapshot(5);
        foreach (int count in new[] { 2, 2, 0, 1, 20, 0 })
        {
            var source = Snapshot(count);
            if (binary) source.WriteToByteArray().Read(target);
            else target.ReadFromJson(source.WriteToJsonString());
            Assert.Equal(count, target.leaderboard.Count);
            Assert.Equal(source.leaderboard.Select(x => x.playerId), target.leaderboard.Select(x => x.playerId));
            Assert.Equal(source.idToPlace, target.idToPlace);
            Assert.Equal(source.list, target.list);
            Assert.Equal(source.array, target.array);
            Assert.Equal(source.ints, target.ints);
            Assert.Equal(source.reactiveValues.ToArray(), target.reactiveValues.ToArray());
            Assert.Equal(source.nullableEntries.Count, target.nullableEntries.Count);
            if (count > 0) Assert.Null(target.nullableEntries[0]);
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Null_default_and_spare_capacity_are_preserved_logically(bool binary)
    {
        var source = Snapshot(2);
        source.leaderboard.Capacity = 20;
        source.leaderboard.Add(null!);
        source.list.Add(0);
        var loaded = binary ? source.WriteToByteArray().Read<SafetySnapshot>() : source.WriteToJsonString().ReadFromJson<SafetySnapshot>();
        Assert.Equal(3, loaded.leaderboard.Count);
        Assert.Null(loaded.leaderboard[2]);
        Assert.Equal(new[] { 0, 1, 0 }, loaded.list);
        Assert.Equal(3, ((JArray)JObject.Parse(source.WriteToJsonString())["leaderboard"]!).Count);
    }

    [Fact]
    public void Every_truncated_root_and_collection_is_rejected()
    {
        var json = Snapshot(2).WriteToJsonString();
        for (int i = 0; i < json.Length; i++)
            Assert.ThrowsAny<JsonException>(() => json[..i].ReadFromJson<SafetySnapshot>());
        const string array = "[{\"playerId\":101,\"score\":20}]";
        for (int i = 1; i < array.Length; i++)
        {
            using var reader = new ZRJsonTextReader(array[..i]);
            reader.Read();
            Assert.ThrowsAny<JsonException>(() => new SimpleList<SafetyEntry>().ReadFromJson(reader));
        }
        Assert.ThrowsAny<JsonException>(() => (json + " {}").ReadFromJson<SafetySnapshot>());
        Assert.ThrowsAny<JsonException>(() => "[]".ReadFromJson<SafetySnapshot>());
        Assert.Empty("{\"leaderboard\":[]}".ReadFromJson<SafetySnapshot>().leaderboard);
    }

    [Fact]
    public void Nested_collection_stops_at_its_own_closing_token()
    {
        using var reader = new ZRJsonTextReader("{\"items\":[1,2],\"next\":3}");
        reader.Read(); reader.Read(); reader.Read();
        var items = new SimpleList<int>();
        items.ReadFromJson(reader);
        Assert.Equal(new[] { 1, 2 }, items.ToArray());
        Assert.Equal(JsonToken.EndArray, reader.TokenType);
        reader.Read();
        Assert.Equal("next", reader.Value);
    }

    [Theory]
    [InlineData("[{\"value\":1,\"key\":2}]")]
    [InlineData("[{\"key\":1,\"value\":2")]
    [InlineData("[{\"key\":1,\"value\":2}]")]
    public void Dictionary_shape_and_repeated_reads(string json)
    {
        var map = new Dictionary<long, int> { [99] = 99 };
        using var reader = new ZRJsonTextReader(json);
        reader.Read();
        if (json == "[{\"key\":1,\"value\":2}]")
        {
            map.ReadFromJson(reader);
            Assert.Equal(new Dictionary<long, int> { [1] = 2 }, map);
        }
        else Assert.ThrowsAny<JsonException>(() => map.ReadFromJson(reader));
    }

    const string Legacy = """
        {"idToPlace":[{"key":101,"value":0},{"key":202,"value":1}],
         "leaderboard":{"data":[{"playerId":101,"score":20},{"playerId":202,"score":10},{"playerId":999,"score":99},null]}}
        """;

    static JObject Migrate(JObject root) => LegacySimpleListMigration.RankedEntries(root, "leaderboard", "idToPlace", "playerId");

    [Fact]
    public void Legacy_migration_uses_ranks_ignores_stale_tail_and_round_trips()
    {
        Assert.ThrowsAny<JsonException>(() => Legacy.ReadFromJson<SafetySnapshot>());
        var loaded = JsonPersistence.Read<SafetySnapshot>(Legacy, Migrate);
        Assert.Equal(new long[] { 101, 202 }, loaded.leaderboard.Select(x => x.playerId));
        var saved = loaded.WriteToJsonString();
        Assert.Equal(2, saved.ReadFromJson<SafetySnapshot>().leaderboard.Count);
        var empty = JObject.Parse(Legacy);
        empty["idToPlace"] = new JArray();
        Assert.Empty(JsonPersistence.Read<SafetySnapshot>(empty.ToString(), Migrate).leaderboard);
    }

    [Theory]
    [InlineData("[{\"key\":101,\"value\":1}]")]
    [InlineData("[{\"key\":999,\"value\":0}]")]
    [InlineData("[{\"key\":101,\"value\":0},{\"key\":202,\"value\":0}]")]
    [InlineData("[{\"key\":101,\"value\":0},{\"key\":101,\"value\":1}]")]
    public void Ambiguous_migrations_fail_without_mutating_source(string ranks)
    {
        var root = JObject.Parse(Legacy);
        root["idToPlace"] = JArray.Parse(ranks);
        var before = root.ToString();
        Assert.Throws<JsonSerializationException>(() => Migrate(root));
        Assert.Equal(before, root.ToString());
    }

    [Fact]
    public void Explicit_count_keeps_legitimate_nulls_and_defaults()
    {
        var legacy = JObject.Parse("{\"data\":[null,0,0,7]}");
        Assert.Equal("[null,0,0]", LegacySimpleListMigration.FromValidatedCount(legacy, 3).ToString(Formatting.None));
        Assert.Throws<JsonSerializationException>(() => LegacySimpleListMigration.FromValidatedCount(legacy, 5));
    }

    [Fact]
    public void Large_persistence_requires_explicit_budget_and_checks_negative_counts()
    {
        var source = new SimpleListHolder();
        for (int i = 0; i < 1000000; i++) source.values.Add(i);
        var bytes = source.WriteToByteArray();
        Assert.Throws<ZergRushCorruptedOrInvalidDataLayout>(() => bytes.Read<SimpleListHolder>());
        using var reader = new ZRBinaryReader(bytes) { Budget = new DeserializationBudget(1000000, 32 * 1024 * 1024) };
        var loaded = new SimpleListHolder();
        loaded.Deserialize(reader);
        Assert.Equal(1000000, loaded.values.Count);
        using var negative = new ZRBinaryReader(BitConverter.GetBytes(-1));
        Assert.Throws<ZergRushCorruptedOrInvalidDataLayout>(() => loaded.values.Deserialize(negative));
        Assert.Equal(1000000, loaded.values.Count);
        using var limited = new ZRBinaryReader(bytes) { Budget = new DeserializationBudget(1000000, 16) };
        Assert.Throws<ZergRushCorruptedOrInvalidDataLayout>(() => loaded.Deserialize(limited));
    }

    [Fact]
    public void Versioned_binary_persistence_rejects_legacy_truncation_and_trailing_bytes()
    {
        var source = Snapshot(2);
        var bytes = BinaryPersistence.Write(source);
        Assert.Equal(2, BinaryPersistence.Read<SafetySnapshot>(bytes).leaderboard.Count);
        Assert.Throws<System.IO.InvalidDataException>(() => BinaryPersistence.Read<SafetySnapshot>(source.WriteToByteArray()));
        for (int i = 0; i < bytes.Length; i++)
            Assert.ThrowsAny<System.IO.IOException>(() => BinaryPersistence.Read<SafetySnapshot>(bytes[..i]));
        Assert.Throws<System.IO.InvalidDataException>(() => BinaryPersistence.Read<SafetySnapshot>(bytes.Concat(new byte[] { 0 }).ToArray()));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Livable_replacement_destroys_removed_nodes_and_restores_update_mode_on_failure(bool binary)
    {
        var target = new SerializableLivableRoot();
        var removed = new SerializableLivableLeaf { value = 99 };
        target.items.Add(removed);
        var source = new SerializableLivableRoot();
        source.items.Add(new SerializableLivableLeaf { value = 42 });
        if (binary) source.WriteToByteArray().Read(target);
        else target.ReadFromJson(source.WriteToJsonString());
        Assert.False(removed.IsInHierarchy);
        Assert.Single(target.items);
        Assert.Equal(42, target.items[0].value);
        Assert.False(target.items.__update_mod);
        Assert.ThrowsAny<Exception>(() =>
        {
            if (binary) source.WriteToByteArray()[..^1].Read(target);
            else target.ReadFromJson(source.WriteToJsonString()[..^3]);
        });
        Assert.False(target.items.__update_mod);
    }

    [Fact]
    public void Json_array_allocations_grow_linearly()
    {
        static long Allocated(int count)
        {
            var json = "[" + string.Join(",", Enumerable.Repeat("1", count)) + "]";
            using var reader = new ZRJsonTextReader(json);
            reader.Read();
            var before = GC.GetAllocatedBytesForCurrentThread();
            var values = Array.Empty<long>().ReadFromJson(reader);
            var bytes = GC.GetAllocatedBytesForCurrentThread() - before;
            Assert.Equal(count, values.Length);
            return bytes;
        }
        Allocated(100);
        long small = Allocated(1000), large = Allocated(8000);
        Assert.True(large < small * 12, $"Allocation grew from {small} to {large}");
    }
}
