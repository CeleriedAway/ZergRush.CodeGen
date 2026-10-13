using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Npgsql;
using NpgsqlTypes;
using ZergRush;
using ZergRush.JsonBenchmarks;

var label = args.Length > 0 ? args[0] : "candidate";
var output = args.Length > 1 ? args[1] : "benchmark-results.jsonl";
var rows = new List<object>();
rows.Add(new { label, runtime = RuntimeInformation.FrameworkDescription, os = RuntimeInformation.OSDescription,
    processors = Environment.ProcessorCount, revision = Environment.GetEnvironmentVariable("BENCHMARK_REVISION"),
    utc = DateTime.UtcNow, note = "Five 300ms samples after warmup, median time; bytes/GC from the median sample. Inputs precomputed." });
NpgsqlConnection? connection = null;
if (Environment.GetEnvironmentVariable("ZERGRUSH_JSONB_CONNECTION") is string connectionString)
{
    connection = new NpgsqlConnection(connectionString);
    connection.Open();
}

foreach (int count in new[] { 1, 100, 1000 })
{
    var room = new BenchRoom { note = new string('n', 2048) };
    for (int i = 0; i < count; i++) room.values.Add(new BenchChild
    {
        score = i, extra = i + 3, label = "record " + i,
        nested = new BenchValue { score = -i, label = "nested" }
    });
    var graph = new BenchGraph();
    var shared = new BenchReferenceChild { score = 42, extra = 7, label = new string('s', 2048) };
    for (int i = 0; i < count; i++) graph.values.Add(shared);
    var json = room.WriteToJsonString(false);
    var graphJson = graph.WriteToJsonString(false);
    var decoded = json.ReadFromJson<BenchRoom>();
    if (decoded.CalculateHash() != room.CalculateHash()) throw new Exception("Room hash mismatch");
    var decodedGraph = graphJson.ReadFromJson<BenchGraph>();
    if (decodedGraph.values.Any(value => !ReferenceEquals(decodedGraph.values[0], value))) throw new Exception("Graph identity mismatch");
    var copy = new BenchRoom();
    copy.UpdateFrom(room);
    if (copy.CalculateHash() != room.CalculateHash()) throw new Exception("Copy hash mismatch");
    rows.Add(new { kind = "integrity", label, count, binarySha256 = Convert.ToHexString(SHA256.HashData(room.WriteToByteArray())), hash = room.CalculateHash(), cloneHash = copy.CalculateHash() });
    if (label == "smoke") continue;
    Measure("decode", count, json, () => json.ReadFromJson<BenchRoom>());
    Measure("encode", count, json, () => room.WriteToJsonString(false));
    Measure("references", count, graphJson, () => graphJson.ReadFromJson<BenchGraph>());
    if (connection != null && label != "baseline")
    {
        using var command = new NpgsqlCommand("SELECT @json::jsonb::text", connection);
        command.Parameters.AddWithValue("json", NpgsqlDbType.Text, json);
        var stored = (string)command.ExecuteScalar()!;
        Measure("jsonb-decode", count, stored, () => stored.ReadFromJson<BenchRoom>());
        Measure("jsonb-fetch-decode", count, stored, () => ((string)command.ExecuteScalar()!).ReadFromJson<BenchRoom>());
    }
}
connection?.Dispose();
File.WriteAllLines(output, rows.Select(row => JsonSerializer.Serialize(row)));
Console.WriteLine("Results: " + Path.GetFullPath(output));

void Measure(string operation, int count, string json, Func<object> action)
{
    var warmup = Stopwatch.StartNew();
    while (warmup.ElapsedMilliseconds < 500) GC.KeepAlive(action());
    var samples = new List<(double microseconds, double bytes, int iterations, int gc0, int gc1, int gc2, int exceptions)>();
    for (int sample = 0; sample < 5; sample++)
    {
        GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();
        int exceptions = 0;
        EventHandler<System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs> handler = (_, _) => exceptions++;
        AppDomain.CurrentDomain.FirstChanceException += handler;
        int g0 = GC.CollectionCount(0), g1 = GC.CollectionCount(1), g2 = GC.CollectionCount(2);
        long bytes = GC.GetAllocatedBytesForCurrentThread();
        int iterations = 0;
        var timer = Stopwatch.StartNew();
        do { GC.KeepAlive(action()); iterations++; } while (timer.ElapsedMilliseconds < 300);
        timer.Stop();
        bytes = GC.GetAllocatedBytesForCurrentThread() - bytes;
        AppDomain.CurrentDomain.FirstChanceException -= handler;
        samples.Add((timer.Elapsed.TotalMicroseconds / iterations, (double)bytes / iterations, iterations,
            GC.CollectionCount(0) - g0, GC.CollectionCount(1) - g1, GC.CollectionCount(2) - g2, exceptions));
    }
    var median = samples.OrderBy(s => s.microseconds).ElementAt(2);
    if (samples.Any(s => s.exceptions != 0)) throw new Exception("Normal-path exceptions in " + operation);
    rows.Add(new { kind = "measurement", label, operation, count, payloadBytes = Encoding.UTF8.GetByteCount(json),
        microseconds = median.microseconds, bytesPerOperation = median.bytes, operationsPerSecond = 1_000_000 / median.microseconds,
        median.iterations, median.gc0, median.gc1, median.gc2, median.exceptions,
        sampleMicroseconds = samples.Select(s => s.microseconds).ToArray() });
    Console.WriteLine($"{label} {operation} {count}: {median.microseconds:F2} us, {median.bytes:F0} B/op");
}
