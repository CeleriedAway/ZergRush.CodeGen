# Upgrade to 0.1.0-preview.4

Update ZergRush.Reactive, ZergRush.CodeGen.Core, ZergRush.CodeGen and ZergRush.CodeGen.Cli together. Update the Unity wrapper and initialize its submodules too; backend NuGet updates do not change Unity source or previously generated files.

## Regeneration is required

Run the normal project generator (including any project plugins), then compile both client and server. New generated C# begins with `// ZergRush serialization schema: 2`. Check all generated collection helpers for that marker; unmarked files may still serialize SimpleList backing storage or append on read. Do not hand-edit generated methods. Preserve original save files before conversion.

`int[]` serialization/copy/hash/compare/JSON now belongs to `ZergRush.Int32ArraySerialization`. Generated calls explicitly name this non-extension runtime class. The Unity wrapper no longer emits global primitive helpers. Older consumer-owned extension helpers can remain during upgrade without conflicting with the wrapper; regenerate them to get the other fixes. Handwritten direct calls can use the runtime static class. Explicit custom array serializers still take precedence.

SDK project inputs are evaluated with the installed dotnet SDK (restore, ResolveReferences, GenerateGlobalUsings), including imported build properties, compile items, global/implicit imports, and resolved package/project references. Referenced projects can be built during reference resolution. File-only input requires explicit imports and references. Unresolved generated member types fail with the member name before existing output is replaced; unrelated missing generated partial methods do not prevent parsing.

## Legacy SimpleList saves

Old `{ "data": [...] }` storage contains capacity, not a recoverable logical count. Null/default trimming and assigning capacity to Count are unsafe: nulls, zeros and default structs may be valid, and tails may contain stale values. Default readers reject the legacy object shape. Raw historical binary capacity bytes are also ambiguous and must not be loaded as if their length were the live count.

Migrate with application evidence at the root level:

```csharp
var snapshot = JsonPersistence.Read<LeaderboardSnapshot>(originalJson,
    root => LegacySimpleListMigration.RankedEntries(
        root, "leaderboard", "idToPlace", "playerId"),
    new DeserializationBudget(1_000_000, 256L * 1024 * 1024),
    maxInputCharacters: 256 * 1024 * 1024);
var convertedJson = snapshot.WriteToJsonString();
var verified = JsonPersistence.Read<LeaderboardSnapshot>(convertedJson,
    budget: new DeserializationBudget(1_000_000, 256L * 1024 * 1024),
    maxInputCharacters: 256 * 1024 * 1024);
// Validate normalization and application invariants, then publish a new save.
// Keep originalJson / the original file as a backup until conversion is verified.
```

The ranked adapter requires a contiguous, unique rank map and exact ID matches for every live backing entry. It accepts empty maps and spare/stale tails; rejects gaps, duplicate ranks/IDs, null live entries, mismatched IDs and malformed storage. It never mutates the supplied tree. For other schemas, a root callback can use `FromValidatedCount` only after independently proving the count; that helper preserves legitimate null/default entries. Inputs without evidence must be retained for recovery, never replaced with an empty list. The migration API performs no disk writes.

For new binary persistence, use `BinaryPersistence.Write` / `Read<T>` with the versioned envelope. The reader rejects unversioned data, unsupported versions, truncated and trailing bytes. Historical binary migration needs the old schema plus external count evidence; there is deliberately no generic automatic conversion. Raw `Serialize` / `Deserialize` APIs remain unversioned for existing transport protocols, and cannot identify legacy capacity bytes.

## Reader semantics and limits

Generated list and dictionary reads replace populated destinations. Shape/count checks precede clearing; malformed element data can leave a partially replaced destination. Use `JsonPersistence.Read<T>` or `BinaryPersistence.Read<T>` for persistent loads: they create a fresh root and return it only on success. Swap the active root after validation. Do not retry a failed in-place read as if its old contents were intact.

Livable lists clear through their lifecycle API before entering update mode; removed live nodes are mortified/destroyed and notifications remain active. Update mode is restored in finally, including failed reads. Reactive collections use their normal Clear/Add notification paths.

Every generated JSON collection/external object and runtime root requires a closing token. Root reads reject trailing input, while nested collection reads stop at their own closing token. Arrays use List<T> geometric growth and one final ToArray, including reference/null/config/polymorphic elements.

Each reader has a `Budget` (default: 100,000 elements per collection, 64 MiB estimated aggregate collection storage). Negative counts and exhausted budgets fail before allocating binary collections. Dictionaries charge both keys and values. JSON charges incrementally. Storage bytes are an estimate of backing slots, not a complete managed-heap quota: element objects, strings and temporary builder capacity also consume memory. Persistence entry points additionally bound input bytes/characters; use process/request memory limits for fully untrusted data. Budgets are per reader/load and accumulate; create a new budget for another root. Writers are unbounded; large data requires an explicit matching reader budget, not a global relaxation of packet limits.

## Validation

The tests compile and execute freshly generated code: fresh/reused/empty/shorter/longer lists and dictionaries, reactive/livable replacement, null/default entries, spare capacity, all JSON truncation offsets, nested boundaries, migration/reload/rejection, linear array allocation growth, a million-element explicitly budgeted binary read, and SDK import/reference equivalence plus CLI failure/output preservation. Wrapper `Tests~/SerializationCompatibility` additionally builds separate assemblies and combined Core source with historical consumer helpers still present.
