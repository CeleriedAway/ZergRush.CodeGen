# Upgrade to 0.1.0-preview.5

This release fixes PostgreSQL JSONB metadata reordering using array envelopes.
It deliberately removes legacy polymorphic/reference object-format support and
makes arrays the only format. See [JSON-FORMAT.md](JSON-FORMAT.md) for the protocol,
token boundaries, reference policy and generated-code/runtime matrix.

1. Update ZergRush.Reactive, ZergRush.CodeGen.Core, ZergRush.CodeGen and
   ZergRush.CodeGen.Cli to 0.1.0-preview.5. Update the Unity wrapper and its child
   repositories to the matching revisions. Runtime/core target remains netstandard2.1;
   generator/CLI/tests use .NET 10.
2. Regenerate every consumer with its normal project generator and plugins. Generated
   files must say `ZergRush serialization schema: 3`. Do not hand-edit generated C#.
3. Compile client, backend, tools and stress consumers; run persistence parity tests.
   Normal, non-polymorphic untracked objects retain their existing field shape and
   reuse already-created instances. This release does not change binary bytes, hashes
   or copy semantics for valid data.
4. Use a factory-aware root entry point when the encoded type can differ from T:

   ```csharp
   var room = JsonPersistence.ReadPolymorphic(json, RoomBase.CreatePolymorphic);
   var snapshot = JsonPersistence.Read<ConcreteSnapshot>(json);
   var saved = snapshot.WriteToJsonString(); // arrays are automatic
   ```

5. Coordinate all readers/writers of shared data. Convert required old polymorphic
   saves with an explicit application migration before selecting this release; no
   automatic legacy fallback is included. The existing object-root migration callback
   on `JsonPersistence.Read` remains available. `ReadPolymorphic` has a separately named
   token-root callback, enabling deliberate root migrations without ambiguous overloads.
   No JSON tree is built when a callback is absent.
6. Update SQL JSON paths that inspect polymorphic payloads. Validate actual JSONB
   output through the library directly before removing Genza's temporary JSON repair.
   Failed reads/migrations must preserve their source records; never save partial or
   empty substitutes. The library performs no database writes.

Forward references are now supported because JSONB can reorder a use ahead of its
definition. The first occurrence creates one correctly typed instance; the definition
fills it later. Missing definitions fail at root completion. Root ID 1, cycles, shared
children, strict metadata types and reference limits are covered by generated tests.

Rollback requires retaining/restoring records compatible with the old release or an
explicit reverse conversion. Downgrading packages while retaining array-format saves
does not work. Backend package upgrades do not update Unity source/generated files or
deploy any application. Genza integration and deployment remain separate work.

## Validation commands

```powershell
$reactive = '<Reactive checkout>/src/ZergRush.Reactive/ZergRush.Reactive.csproj'
dotnet test ZergRush.CodeGen.sln -c Release "-p:ZergRushReactiveProjectPath=$reactive"

# Separate integration suite; use a disposable/local PostgreSQL database.
$env:ZERGRUSH_JSONB_CONNECTION = 'Host=127.0.0.1;Port=55439;Database=postgres;Username=zr_jsonb'
dotnet test Tests~/ZergRush.CodeGen.Jsonb.Tests/ZergRush.CodeGen.Jsonb.Tests.csproj -c Release "-p:ZergRushReactiveProjectPath=$reactive"
```

The JSONB suite executes parameterized `SELECT @json::jsonb::text` on generated
records (0/1/100/1,000 polymorphic elements), shared references, cycles and roots.
It is separate from the fast solution and never accesses production records.
See [JSON-BENCHMARKS.md](JSON-BENCHMARKS.md) for the measurement harness and results.
