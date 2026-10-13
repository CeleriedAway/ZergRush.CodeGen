using Npgsql;
using NpgsqlTypes;
using Xunit;
using ZergRush.CodeGen.Tests;

namespace ZergRush.CodeGen.Jsonb.Tests;

// Intentionally separate from the fast solution. Requires an explicitly supplied local/disposable DB.
public sealed class JsonbIntegrationTests
{
    static NpgsqlConnection Open()
    {
        string connectionString = Environment.GetEnvironmentVariable("ZERGRUSH_JSONB_CONNECTION")
            ?? throw new InvalidOperationException("Set ZERGRUSH_JSONB_CONNECTION to a disposable PostgreSQL database.");
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }

    static string Roundtrip(NpgsqlConnection connection, string json)
    {
        using var command = new NpgsqlCommand("SELECT @json::jsonb::text", connection);
        command.Parameters.AddWithValue("json", NpgsqlDbType.Text, json);
        return (string)command.ExecuteScalar()!;
    }

    [Theory]
    [InlineData(0)] [InlineData(1)] [InlineData(100)] [InlineData(1000)]
    public void Generated_nested_polymorphic_collections_decode_directly_after_jsonb(int count)
    {
        using var connection = Open();
        var source = JsonArrayFormatTests.CreateFixture(count);
        string stored = Roundtrip(connection, source.WriteToJsonString(false));
        var decoded = JsonPersistence.Read<JsonArrayHolder>(stored);
        JsonArrayFormatTests.AssertFixture(source, decoded);
    }

    [Fact]
    public void Shared_reference_graph_survives_jsonb_moving_references_ahead_of_definitions()
    {
        using var connection = Open();
        var source = JsonArrayFormatTests.CreateReferences(1000);
        string stored = Roundtrip(connection, source.WriteToJsonString(false));
        Assert.True(stored.IndexOf("\"many\"", StringComparison.Ordinal) < stored.IndexOf("\"first\"", StringComparison.Ordinal));
        JsonPolyReference.Constructions = JsonPlainReference.Constructions = 0;
        var decoded = JsonPersistence.Read<JsonReferenceRoot>(stored);
        JsonArrayFormatTests.AssertReferences(decoded);
        Assert.Equal(2, JsonPolyReference.Constructions);
        Assert.Equal(1, JsonPlainReference.Constructions);
    }

    [Fact]
    public void Polymorphic_root_and_cycle_survive_jsonb()
    {
        using var connection = Open();
        var source = new JsonPolyReferenceChild { score = 13, extra = 29 };
        source.next = source;
        string stored = Roundtrip(connection, source.WriteToJsonString(false));
        var decoded = JsonPersistence.ReadPolymorphic(stored, JsonPolyReference.CreatePolymorphic);
        Assert.Equal(29, Assert.IsType<JsonPolyReferenceChild>(decoded).extra);
        Assert.Same(decoded, decoded.next);
    }
}
