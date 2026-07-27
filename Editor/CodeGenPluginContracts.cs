using System;
using System.Collections.Generic;

namespace ZergRush.CodeGen
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CodeGenPluginEntryPointAttribute : Attribute
    {
    }

    public sealed class CodeGenPluginContext
    {
        readonly CodeGenSession session;
        readonly string pluginPath;
        readonly string entryPoint;

        internal CodeGenPluginContext(
            CodeGenSession session,
            IReadOnlyList<string> inputPaths,
            string workingDirectory,
            string outputDirectory,
            string pluginPath,
            string entryPoint)
        {
            this.session = session;
            this.pluginPath = pluginPath;
            this.entryPoint = entryPoint;
            InputPaths = inputPaths;
            WorkingDirectory = workingDirectory;
            OutputDirectory = outputDirectory;
        }

        public IReadOnlyList<ZRType> Types => session.Types;
        public IReadOnlyList<string> InputPaths { get; }
        public string WorkingDirectory { get; }
        public string OutputDirectory { get; }

        public void AddGenerator(Action<CodeGenSession> generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            session.AddGenerator(activeSession =>
            {
                try
                {
                    generator(activeSession);
                }
                catch (Exception exception)
                {
                    throw new CodeGenPluginException(
                        $"CodeGen plugin generator registered by '{entryPoint}' from '{pluginPath}' failed.",
                        pluginPath,
                        entryPoint,
                        exception);
                }
            });
        }
    }

    public sealed class CodeGenPluginException : Exception
    {
        public CodeGenPluginException(
            string message,
            string pluginPath,
            string entryPoint = null,
            Exception innerException = null)
            : base(message, innerException)
        {
            PluginPath = pluginPath;
            EntryPoint = entryPoint;
        }

        public string PluginPath { get; }
        public string EntryPoint { get; }
    }

    public sealed class CodeGenPluginEntryPointInfo
    {
        public CodeGenPluginEntryPointInfo(string pluginPath, string entryPoint)
        {
            PluginPath = pluginPath;
            EntryPoint = entryPoint;
        }

        public string PluginPath { get; }
        public string EntryPoint { get; }
    }
}
