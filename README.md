# ZergRush.CodeGen

Source-first and reflection-driven code generation tooling for ZergRush.

`Runtime/` contains the Unity-compilable abstractions. `Tools~/` holds the parser, generator engine, and CLI source; Unity ignores that folder and the Unity wrapper builds the CLI locally for debugging. This repository is not published yet.

## CLI inputs

CLI inputs are explicit and repeatable: `-f`/`--file` for C# files, `-p`/`--project` for `.csproj` files, and `-s`/`--solution` for `.sln` files. Projects expand their compile items and solutions expand all referenced C# projects.

Use `--search-up` to resolve inputs from the working directory through its parents. This is useful from a Unity `Assets` directory:

```shell
zrgen --search-up -p Assembly-CSharp.csproj
```

Use `--search-down` for recursive discovery. Omitting the value selects every file of that input kind:

```shell
zrgen --search-down -p
zrgen --search-down -f "*.cs"
```

Pass `--generate <directory>` to generate source. Without it, the CLI parses and prints the resolved model, which is useful for validating a project or solution input before generation.

## Using CodeGen as a Unity editor library

Unity editor generators can build the same `ZRType` model used by the source parser and run explicit generators without `CodeGenExtension` discovery:

```csharp
var converter = new ZRReflectionTypeConverter();
var types = converter.Convert(typeof(MyModel).Assembly.GetTypes());

var session = new CodeGenSession(types, "Assets/ZergRushGenerated")
    .AddGenerator(activeSession => MyEditorGenerator.Generate(activeSession));

session.Generate();
```

Custom generators receive the session, inspect `session.Types`, and use session-scoped methods such as `GetContext`, `GetClass`, `CreateGeneratedMethod`, and `RequestGeneration`. Reflection methods, parameters, members, attributes, inheritance, generic types, and generation flags are represented by `ZRMethod`, `ZRParameter`, `ZRMember`, and `ZRType` before generator code runs.

For an additive custom generator that shares folders with source-generated files, set `GenerateBuiltInTasks` and `CleanOutputDirectories` to `false`. The session will overwrite only files emitted by that custom run.
