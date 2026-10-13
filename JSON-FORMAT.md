# JSON serialization schema 3

Schema 3 uses arrays for polymorphic and reference-tracked values. There is no legacy
object-metadata reader or writer option. Ordinary, non-polymorphic, untracked objects
retain their object shape, generated field dispatch and existing-instance reuse.
Collections, scalar encodings, class IDs and binary encodings retain their meanings.

## Wire contract

| Value at the declared call site | Encoding |
| --- | --- |
| Polymorphic value | `[classId, {payload fields}]` |
| Reference definition | `[classIdOrNull, "referenceId", false, {payload fields}]` |
| Reference use | `[classIdOrNull, "referenceId", true, null]` |
| Null value | `null` |
| Ordinary untracked object | `{fields}` |

Class IDs are integer UInt16 values from the existing generated registry; unknown
IDs are rejected. Reference IDs are positive Int64 values encoded as strings.
The first slot is null only for a non-polymorphic reference-tracked type. A reference
to a polymorphic value uses one four-element envelope, without an inner envelope.
Each polymorphic collection element has its own envelope. Payloads may not contain
`__classId`, `isRef` or `refId` metadata. Ordinary untracked application fields with
these names retain ordinary field semantics.

`WriteJson` at the root registers identity 1 before writing any fields. A polymorphic
root has a two-element envelope; an ordinary root is an object. Root back-references
use the four-element form with reference ID `"1"`. Nested reference definitions use
the same writer/reader identity context. `WriteObjectWithRef` is the nested-reference
API; do not use it as a substitute for the root `WriteJson` entry point.

## Reader contract and complexity

Generated code calls `BeginObject(polymorphic, referenceTracked)` with the current
token at StartArray (or StartObject for ordinary objects). It reads the fixed header
and leaves the payload StartObject or reference-use Null current. A generated direct
factory call selects the type. `ReadObject` and `ResolveReference` leave the matching
outer EndArray/EndObject current. The parent advances to its next token. Neither
helper consumes a sibling or the parent collection terminator.

Ordinary objects continue to use `ReadFromJson` field dispatch. Polymorphic values
reuse matching instances where the destination supports it, or replace them when
the encoded class ID changes. Reading an incompatible class ID into a concrete root
that cannot be replaced fails explicitly. Use `JsonPersistence.ReadPolymorphic(json,
Base.CreatePolymorphic)` or `reader.ReadPolymorphicRoot(Base.CreatePolymorphic)` for
factory-selected roots, including abstract bases and interface result types.

PostgreSQL JSONB can put reference uses before definitions by reordering payload
properties. Schema 3 therefore permits forward references. `TryGetReference` checks
the per-reader identity table before generated code creates an instance. If absent,
the first occurrence constructs the final instance once and registers it. Its later
definition fills that same instance. Definitions register before reading fields,
supporting cycles. Root completion rejects identities without definitions. Conflicting
types, duplicate definitions and incompatible destination types fail. There is no
deferred graph repair or second traversal. Each fresh read replaces the tracked child
identity graph; untracked matching objects retain their previous reuse behavior.

Reading is one linear token pass, with expected constant-time identity lookups.
There is no DOM, token replay, intermediate JSON string, reflection, captured factory
delegate or heap-allocated framing object on the ordinary decode path. Models,
collections and tokenizer strings/boxed values still allocate. The identity table
has one entry per distinct reference, and forward uses do not create throwaways.
`JsonObjectFrame` is a struct. Explicit migration callbacks are the only persistence
path that constructs a JSON tree.

All ZR JSON readers set `DateParseHandling.None`: ISO-shaped string fields remain
strings. Generated DateTime values still use integer ticks. Collection/storage and
input limits remain in force; `DeserializationBudget` additionally bounds reference
identities (including the root), default 100,000. Its three-argument constructor sets
that limit explicitly. Newtonsoft's per-reader MaxDepth bounds nesting.

## Compatibility

| Generated code | Runtime | Data | Supported |
| --- | --- | --- | --- |
| Schema 2 / preview.4 | preview.4 | Ordered legacy objects | Historical behavior |
| Schema 3 / preview.5 | preview.5 | Array polymorphism/references; ordinary objects | Yes |
| Schema 2 | preview.5 | Polymorphic/reference JSON | No; regenerate |
| Schema 3 | preview.4 | Any generated schema-3 calls | No; missing runtime helpers |
| Schema 3 | preview.5 | Legacy polymorphic/reference objects | No; explicit conversion outside this reader |

Replacing a NuGet runtime does not regenerate consumer code. All processes and tools
reading shared records must be upgraded together before schema-3 writes begin.
Old releases cannot read these arrays. SQL paths into polymorphic payloads now use
array index 1, or index 3 for reference definitions.
