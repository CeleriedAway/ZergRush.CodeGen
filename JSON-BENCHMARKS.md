# Schema-3 JSON measurements

Measured 2026-10-13 on an Intel Core i9-12900K (24 logical processors), Windows
10.0.26200, .NET 10.0.8, Release builds. Baseline: published 0.1.0-preview.4,
CodeGen `5d9d644`; candidate: preview.5 source based on that commit. The same
`Tests~/ZergRush.CodeGen.JsonBenchmarks` handwritten models and harness were compiled
with each release's generated code and runtime. Inputs were precomputed. Each case
has a 500 ms warmup and five 300 ms samples; the table reports the median sample.
Full samples, throughput, GC counts and timestamps are in
`Tests~/ZergRush.CodeGen.JsonBenchmarks/results/`.

The room has a 2 KiB root string and 1/100/1,000 polymorphic children, each with a
nested polymorphic state. The reference case repeats one shared polymorphic object
with a 2 KiB string. This measures model/collection/string allocations too, not just
framing. No first-chance exceptions occurred in any measured case. Source inspection
confirms no DOM/replay/reparse path or allocated framing object in normal reads.

## Compact JSON decode

| Elements | preview.4 us/op | preview.5 us/op | Change | preview.4 B/op | preview.5 B/op | Old/new UTF-8 bytes |
| ---: | ---: | ---: | ---: | ---: | ---: | ---: |
| 1 | 2.94 | 3.11 | +5.8% | 19,856 | 20,032 | 2,191 / 2,171 |
| 100 | 61.59 | 67.45 | +9.5% | 76,273 | 68,529 | 14,635 / 12,635 |
| 1,000 | 609.16 | 643.83 | +5.7% | 594,689 | 514,944 | 131,638 / 111,638 |

The array reader remains linear. It adds an outer-array closing-token check and
stricter type/header validation. The measured compact-reader slowdown stays below
10% in this run; it is not a claim of identical performance. The smallest record has
a small context-allocation increase; larger records save property-name allocations.

## Serialization and shared references

| Elements | Old/new encode us/op | Old/new encode B/op | Old/new reference decode us/op | Old/new reference decode B/op |
| ---: | ---: | ---: | ---: | ---: |
| 1 | 2.79 / 2.70 | 13,624 / 13,800 | 3.63 / 3.78 | 19,616 / 19,552 |
| 100 | 30.49 / 30.94 | 62,888 / 59,104 | 23.08 / 19.50 | 37,561 / 24,032 |
| 1,000 | 403.22 / 376.43 | 537,976 / 449,976 | 210.58 / 164.79 | 195,971 / 60,041 |

At 1,000 uses, reference decoding is about 22% faster with 69% less allocated memory.
Constructor-count tests separately verify one construction per distinct reference
identity, including forward uses and cycles. Back-reference tokens themselves still
incur Newtonsoft string/boxing allocations; no framing object or throwaway model is
created for each use. GC counts are sample totals, not normalized counts per operation.

## Actual PostgreSQL output and end-to-end fetch

Native PostgreSQL 17.6 on loopback; Npgsql 9.0.4; parameterized
`SELECT @json::jsonb::text`. The decode-only case uses the returned string precomputed
outside timing. Fetch/decode includes the synchronous SQL round trip and returned
string allocation on one already-open connection.

| Elements | JSONB output bytes | Decode us/op | Decode B/op | Fetch + decode us/op | Fetch + decode B/op |
| ---: | ---: | ---: | ---: | ---: | ---: |
| 1 | 2,188 | 4.21 | 20,032 | 121.07 | 24,569 |
| 100 | 14,137 | 68.56 | 68,529 | 572.64 | 97,106 |
| 1,000 | 126,640 | 688.36 | 514,944 | 4,700.94 | 770,565 |

PostgreSQL adds separators/whitespace and reorders object fields. Its decode timing
is 688.36 us at 1,000 elements, about 13% above the old *compact* successful baseline;
this is a different input representation, and the 6.9% difference from candidate
compact decode should not be hidden inside database timing. Tiny-record results are
especially sensitive to JIT, scheduling and branch-order effects. These are local
microbenchmarks, not production latency guarantees. Legacy repair is intentionally
not implemented or benchmarked in this array-only release.

Binary SHA-256, model hashes and copied-model hashes match across both versions for
all three room sizes. Ordinary non-polymorphic generated object code is unchanged
apart from the schema comment; an in-place regression checks required, optional and
Cell-held object identity, existing list reuse, omitted fields, application `refId` /
`isRef` fields and zero new model constructions.

## Reproduce

```powershell
$reactive = '<Reactive checkout>/src/ZergRush.Reactive/ZergRush.Reactive.csproj'
dotnet build Tests~/ZergRush.CodeGen.JsonBenchmarks/ZergRush.CodeGen.JsonBenchmarks.csproj -c Release "-p:ZergRushReactiveProjectPath=$reactive"
$env:ZERGRUSH_JSONB_CONNECTION = 'Host=127.0.0.1;Port=55439;Database=postgres;Username=zr_jsonb;Pooling=false'
dotnet Tests~/ZergRush.CodeGen.JsonBenchmarks/bin~/Release/net10.0/ZergRush.CodeGen.JsonBenchmarks.dll candidate candidate.jsonl

# Copy the harness .cs/.csproj plus Directory.Build.props into a separate directory,
# then build it with the released runtime and its exact generator.
dotnet tool install ZergRush.CodeGen.Cli --version 0.1.0-preview.4 --tool-path ./baseline-cli
dotnet build <baseline-directory>/ZergRush.CodeGen.JsonBenchmarks.csproj -c Release -p:BaselineVersion=0.1.0-preview.4 "-p:CodeGenCliCommand=<absolute-path>/baseline-cli/zrgen.exe"
dotnet <baseline-directory>/bin~/Release/net10.0/ZergRush.CodeGen.JsonBenchmarks.dll baseline baseline.jsonl
```

Run the baseline and candidate sequentially on an otherwise idle machine. Keep output
paths outside `Generated/`. On non-Windows systems the tool command is `zrgen`.
