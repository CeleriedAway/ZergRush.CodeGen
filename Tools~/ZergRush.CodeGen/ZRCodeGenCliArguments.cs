namespace ZergRush.CodeGen;

public enum ZRCodeGenInputKind
{
    File,
    Project,
    Solution
}

public enum ZRCodeGenSearchMode
{
    Direct,
    Up,
    Down
}

public sealed record ZRCodeGenInput(ZRCodeGenInputKind Kind, string Pattern);

/// <summary>Typed command-line inputs and deterministic filesystem discovery for the CodeGen CLI.</summary>
public sealed class ZRCodeGenCliArguments
{
    public const string Usage = """
        ZergRush.CodeGen.Cli

        Inputs (repeatable):
          -f, --file [path|pattern]       C# source file; defaults to *.cs
          -p, --project [path|pattern]    C# project; defaults to *.csproj
          -s, --solution [path|pattern]   Solution; defaults to *.sln

        Discovery:
              --search-up                Search the working directory and each parent
              --search-down              Search recursively below the working directory

        Generation:
              --generate <directory>     Generate code using this fallback output directory
              --preserve-target-folders  Honor parsed target folders (the default)
              --single-output-folder     Put every generated file in --generate directory
          -h, --help                     Show this help

        Examples:
          zrgen --search-up -p Assembly-CSharp.csproj
          zrgen --search-down -p
          zrgen --search-down -f *.cs
          zrgen -s Game.sln --generate Generated
        """;

    public string? GenerationOutput { get; private set; }
    public bool PreserveTargetFolders { get; private set; } = true;
    public bool ShowHelp { get; private set; }
    public ZRCodeGenSearchMode SearchMode { get; private set; }
    public List<ZRCodeGenInput> Inputs { get; } = [];

    public static ZRCodeGenCliArguments Parse(IReadOnlyList<string> args)
    {
        var result = new ZRCodeGenCliArguments();
        for (var i = 0; i < args.Count; i++)
        {
            switch (args[i])
            {
                case "-h":
                case "--help":
                    result.ShowHelp = true;
                    break;
                case "--search-up":
                    result.SetSearchMode(ZRCodeGenSearchMode.Up);
                    break;
                case "--search-down":
                    result.SetSearchMode(ZRCodeGenSearchMode.Down);
                    break;
                case "--preserve-target-folders":
                    result.PreserveTargetFolders = true;
                    break;
                case "--single-output-folder":
                    result.PreserveTargetFolders = false;
                    break;
                case "--generate":
                    result.GenerationOutput = Path.GetFullPath(RequiredValue(args, ref i, "--generate"));
                    break;
                case "-f":
                case "--file":
                    result.AddInput(ZRCodeGenInputKind.File, OptionalPattern(args, ref i, "*.cs"));
                    break;
                case "-p":
                case "--project":
                    result.AddInput(ZRCodeGenInputKind.Project, OptionalPattern(args, ref i, "*.csproj"));
                    break;
                case "-s":
                case "--solution":
                    result.AddInput(ZRCodeGenInputKind.Solution, OptionalPattern(args, ref i, "*.sln"));
                    break;
                default:
                    throw new ArgumentException($"Unknown argument '{args[i]}'. Inputs must use -f, -p, or -s.");
            }
        }

        if (!result.ShowHelp && result.Inputs.Count == 0)
            throw new ArgumentException("At least one -f, -p, or -s input is required.");

        return result;
    }

    public IReadOnlyList<string> ResolveInputs(string workingDirectory)
    {
        var root = Path.GetFullPath(workingDirectory);
        if (!Directory.Exists(root))
            throw new DirectoryNotFoundException($"CodeGen working directory does not exist: {root}");

        var resolved = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var input in Inputs)
        {
            var matches = SearchMode switch
            {
                ZRCodeGenSearchMode.Direct => ResolveDirect(root, input),
                ZRCodeGenSearchMode.Up => ResolveUp(root, input),
                ZRCodeGenSearchMode.Down => ResolveDown(root, input),
                _ => throw new ArgumentOutOfRangeException()
            };

            var matchList = matches.OrderBy(path => path, StringComparer.OrdinalIgnoreCase).ToList();
            if (matchList.Count == 0)
                throw new FileNotFoundException(
                    $"No {KindDescription(input.Kind)} matched '{input.Pattern}' using {SearchMode} search from '{root}'.");

            foreach (var match in matchList) resolved.Add(match);
        }

        return resolved.OrderBy(path => path, StringComparer.OrdinalIgnoreCase).ToList();
    }

    void SetSearchMode(ZRCodeGenSearchMode mode)
    {
        if (SearchMode != ZRCodeGenSearchMode.Direct && SearchMode != mode)
            throw new ArgumentException("--search-up and --search-down cannot be combined.");
        SearchMode = mode;
    }

    void AddInput(ZRCodeGenInputKind kind, string pattern)
    {
        ValidateExtension(kind, pattern);
        Inputs.Add(new ZRCodeGenInput(kind, pattern));
    }

    static IEnumerable<string> ResolveDirect(string root, ZRCodeGenInput input)
    {
        var fullPattern = Path.GetFullPath(Path.Combine(root, input.Pattern));
        if (!HasWildcard(input.Pattern))
            return File.Exists(fullPattern) ? [fullPattern] : [];

        var directory = Path.GetDirectoryName(fullPattern) ?? root;
        var pattern = Path.GetFileName(fullPattern);
        return Directory.Exists(directory)
            ? Directory.EnumerateFiles(directory, pattern, SearchOption.TopDirectoryOnly)
            : [];
    }

    static IEnumerable<string> ResolveUp(string root, ZRCodeGenInput input)
    {
        for (var directory = new DirectoryInfo(root); directory != null; directory = directory.Parent)
        {
            var matches = ResolveDirect(directory.FullName, input).ToList();
            if (matches.Count > 0) return matches;
        }
        return [];
    }

    static IEnumerable<string> ResolveDown(string root, ZRCodeGenInput input)
    {
        var relativeDirectory = Path.GetDirectoryName(input.Pattern);
        var searchRoot = string.IsNullOrEmpty(relativeDirectory)
            ? root
            : Path.GetFullPath(Path.Combine(root, relativeDirectory));
        if (!Directory.Exists(searchRoot)) return [];

        var pattern = Path.GetFileName(input.Pattern);
        if (string.IsNullOrEmpty(pattern)) pattern = DefaultPattern(input.Kind);
        return Directory.EnumerateFiles(searchRoot, pattern, SearchOption.AllDirectories);
    }

    static string OptionalPattern(IReadOnlyList<string> args, ref int index, string defaultPattern)
    {
        if (index + 1 >= args.Count || IsOption(args[index + 1])) return defaultPattern;
        return args[++index];
    }

    static string RequiredValue(IReadOnlyList<string> args, ref int index, string option)
    {
        if (index + 1 >= args.Count || IsOption(args[index + 1]))
            throw new ArgumentException($"{option} requires a value.");
        return args[++index];
    }

    static bool IsOption(string value) => value.StartsWith("-", StringComparison.Ordinal);
    static bool HasWildcard(string value) => value.IndexOfAny(['*', '?']) >= 0;

    static void ValidateExtension(ZRCodeGenInputKind kind, string pattern)
    {
        var expected = DefaultPattern(kind)[1..];
        if (!pattern.EndsWith(expected, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException($"{KindDescription(kind)} input '{pattern}' must end with '{expected}'.");
    }

    static string DefaultPattern(ZRCodeGenInputKind kind) => kind switch
    {
        ZRCodeGenInputKind.File => "*.cs",
        ZRCodeGenInputKind.Project => "*.csproj",
        ZRCodeGenInputKind.Solution => "*.sln",
        _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
    };

    static string KindDescription(ZRCodeGenInputKind kind) => kind switch
    {
        ZRCodeGenInputKind.File => "C# file",
        ZRCodeGenInputKind.Project => "C# project",
        ZRCodeGenInputKind.Solution => "solution",
        _ => kind.ToString()
    };
}
