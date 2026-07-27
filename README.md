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

Pass `--generate <directory>` to generate source. Generated types honor their parsed target folders by default, which places unannotated types in a local `x_generated` folder beside their source. The supplied directory is the fallback. Pass `--single-output-folder` to intentionally flatten every generated file into that fallback directory. Without `--generate`, the CLI parses and prints the resolved model, which is useful for validating a project or solution input before generation.

## CLI plugins

Pass one or more `--plugin <assembly.dll>` arguments to extend the same parsed generation batch:

```shell
zrgen -p Assembly-CSharp.csproj \
  --plugin Tools/PluginA.dll \
  --plugin Tools/PluginB.dll \
  --generate Generated
```

Plugin paths are resolved from the CLI working directory, deduplicated in argument order, and require `--generate`. Plugins run with full trust and must target a runtime compatible with the `net10.0` CLI.

Expose one or more public entry points from the plugin assembly:

```csharp
public static class MyCodeGenPlugin
{
    [CodeGenPluginEntryPoint]
    public static void Configure(CodeGenPluginContext context)
    {
        context.AddGenerator(MyGenerator.Generate);
    }
}
```

An entry point must be public, static, non-generic, return `void`, and accept exactly one `CodeGenPluginContext`. The context exposes the parsed types, resolved inputs, working directory, and output directory. All plugins configure one `CodeGenSession`; the CLI invokes `Generate()` once after every entry point has registered its generators.

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
