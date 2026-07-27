using System.Reflection;
using System.Runtime.Loader;

namespace ZergRush.CodeGen;

public static class CodeGenPluginLoader
{
    public static IReadOnlyList<CodeGenPluginEntryPointInfo> LoadAndConfigure(
        IEnumerable<string> pluginPaths,
        CodeGenSession session,
        IReadOnlyList<string> inputPaths,
        string workingDirectory,
        string outputDirectory)
    {
        ArgumentNullException.ThrowIfNull(pluginPaths);
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(inputPaths);

        var plugins = pluginPaths.Select(LoadPlugin).ToList();
        var entryPoints = plugins
            .SelectMany(plugin => DiscoverEntryPoints(plugin.Path, plugin.Assembly)
                .Select(method => new LoadedEntryPoint(plugin.Path, method)))
            .ToList();

        foreach (var entryPoint in entryPoints)
        {
            var identity = EntryPointIdentity(entryPoint.Method);
            var context = new CodeGenPluginContext(
                session,
                inputPaths,
                workingDirectory,
                outputDirectory,
                entryPoint.PluginPath,
                identity);
            try
            {
                entryPoint.Method.Invoke(null, [context]);
            }
            catch (TargetInvocationException exception)
            {
                throw new CodeGenPluginException(
                    $"CodeGen plugin entry point '{identity}' from '{entryPoint.PluginPath}' failed.",
                    entryPoint.PluginPath,
                    identity,
                    exception.InnerException ?? exception);
            }
            catch (Exception exception)
            {
                throw new CodeGenPluginException(
                    $"Could not invoke CodeGen plugin entry point '{identity}' from '{entryPoint.PluginPath}'.",
                    entryPoint.PluginPath,
                    identity,
                    exception);
            }
        }

        return entryPoints
            .Select(entryPoint => new CodeGenPluginEntryPointInfo(
                entryPoint.PluginPath,
                EntryPointIdentity(entryPoint.Method)))
            .ToArray();
    }

    static LoadedPlugin LoadPlugin(string pluginPath)
    {
        var fullPath = Path.GetFullPath(pluginPath);
        if (!File.Exists(fullPath))
            throw new CodeGenPluginException(
                $"CodeGen plugin assembly does not exist: {fullPath}",
                fullPath);

        try
        {
            var loadContext = new CodeGenPluginLoadContext(fullPath);
            return new LoadedPlugin(fullPath, loadContext.LoadFromAssemblyPath(fullPath), loadContext);
        }
        catch (Exception exception)
        {
            throw new CodeGenPluginException(
                $"Could not load CodeGen plugin assembly '{fullPath}'.",
                fullPath,
                innerException: exception);
        }
    }

    static IReadOnlyList<MethodInfo> DiscoverEntryPoints(string pluginPath, Assembly assembly)
    {
        List<MethodInfo> methods;
        try
        {
            methods = assembly.DefinedTypes
                .SelectMany(type => type.DeclaredMethods)
                .Where(method => method.GetCustomAttribute<CodeGenPluginEntryPointAttribute>() != null)
                .OrderBy(method => method.DeclaringType?.FullName, StringComparer.Ordinal)
                .ThenBy(method => method.Name, StringComparer.Ordinal)
                .ToList();
        }
        catch (Exception exception)
        {
            throw new CodeGenPluginException(
                $"Could not inspect CodeGen plugin assembly '{pluginPath}'.",
                pluginPath,
                innerException: exception);
        }

        if (methods.Count == 0)
            throw new CodeGenPluginException(
                $"CodeGen plugin assembly '{pluginPath}' has no methods marked with [{nameof(CodeGenPluginEntryPointAttribute)}].",
                pluginPath);

        foreach (var method in methods)
        {
            if (!method.IsPublic ||
                !method.IsStatic ||
                method.IsGenericMethodDefinition ||
                method.ReturnType != typeof(void) ||
                method.GetParameters() is not [{ ParameterType: var parameterType }] ||
                parameterType != typeof(CodeGenPluginContext))
            {
                var identity = EntryPointIdentity(method);
                throw new CodeGenPluginException(
                    $"CodeGen plugin entry point '{identity}' from '{pluginPath}' must be a public, static, non-generic void method with exactly one {nameof(CodeGenPluginContext)} parameter.",
                    pluginPath,
                    identity);
            }
        }

        return methods;
    }

    static string EntryPointIdentity(MethodInfo method) =>
        $"{method.DeclaringType?.FullName ?? "<unknown>"}.{method.Name}";

    sealed record LoadedPlugin(string Path, Assembly Assembly, AssemblyLoadContext LoadContext);
    sealed record LoadedEntryPoint(string PluginPath, MethodInfo Method);

    sealed class CodeGenPluginLoadContext : AssemblyLoadContext
    {
        readonly AssemblyDependencyResolver resolver;
        readonly string pluginDirectory;

        public CodeGenPluginLoadContext(string pluginPath)
            : base($"CodeGenPlugin:{pluginPath}", isCollectible: false)
        {
            resolver = new AssemblyDependencyResolver(pluginPath);
            pluginDirectory = Path.GetDirectoryName(pluginPath)!;
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            if (assemblyName.Name?.StartsWith("ZergRush", StringComparison.Ordinal) == true)
            {
                var sharedAssembly = Default.Assemblies.FirstOrDefault(assembly =>
                    string.Equals(
                        assembly.GetName().Name,
                        assemblyName.Name,
                        StringComparison.OrdinalIgnoreCase));
                if (sharedAssembly != null) return sharedAssembly;

                try
                {
                    return Default.LoadFromAssemblyName(assemblyName);
                }
                catch (FileNotFoundException)
                {
                    // Fall through to plugin-private resolution for non-host ZergRush assemblies.
                }
            }

            var path = resolver.ResolveAssemblyToPath(assemblyName);
            if (path == null && assemblyName.Name != null)
            {
                var adjacentPath = Path.Combine(pluginDirectory, assemblyName.Name + ".dll");
                if (File.Exists(adjacentPath)) path = adjacentPath;
            }
            return path == null ? null : LoadFromAssemblyPath(path);
        }

        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            var path = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            return path == null ? nint.Zero : LoadUnmanagedDllFromPath(path);
        }
    }
}
