using System;
using System.Collections.Generic;
using System.Linq;

namespace ZergRush.CodeGen
{
    /// <summary>
    /// Explicit, reusable entry point for a single CodeGen run. A session owns its
    /// input model and custom generators; runs are serialized because the legacy
    /// low-level emitters still share internal caches.
    /// </summary>
    public sealed class CodeGenSession
    {
        static readonly object generationLock = new();
        readonly List<Action<CodeGenSession>> generators = new();
        readonly Dictionary<(ZRType Type, string Suffix), SharpClassBuilder> customClasses = new();
        bool active;

        public CodeGenSession(IEnumerable<ZRType> types, string outputPath)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (string.IsNullOrWhiteSpace(outputPath)) throw new ArgumentException("An output path is required.", nameof(outputPath));
            Types = types.Distinct().ToArray();
            OutputPath = outputPath;
        }

        public IReadOnlyList<ZRType> Types { get; }
        public string OutputPath { get; }
        public bool GenerateBuiltInTasks { get; set; } = true;
        public bool CleanOutputDirectories { get; set; } = true;

        public static CodeGenSession FromReflection(IEnumerable<Type> types, string outputPath,
            ZRReflectionTypeConverter? converter = null)
        {
            converter ??= new ZRReflectionTypeConverter();
            return new CodeGenSession(converter.Convert(types), outputPath);
        }

        public CodeGenSession AddGenerator(Action<CodeGenSession> generator)
        {
            generators.Add(generator ?? throw new ArgumentNullException(nameof(generator)));
            return this;
        }

        public void Generate()
        {
            lock (generationLock)
            {
                active = true;
                customClasses.Clear();
                try
                {
                    CodeGen.RawGen(Types, OutputPath, () =>
                    {
                        foreach (var generator in generators) generator(this);
                    }, GenerateBuiltInTasks, CleanOutputDirectories);
                }
                finally
                {
                    active = false;
                }
            }
        }

        public void RegisterTypeContext(ZRType type, ZRType? requester = null)
        {
            EnsureActive();
            CodeGen.RegisterTypeContext(type, requester);
        }

        public GeneratorContext GetContext(ZRType type)
        {
            EnsureActive();
            return CodeGen.GetContext(type);
        }

        public SharpClassBuilder GetClass(ZRType type, GeneratorContext? context = null)
        {
            EnsureActive();
            return CodeGen.GenClassSink(type, context);
        }

        public SharpClassBuilder GetPartialClass(ZRType type, string fileNameSuffix,
            GeneratorContext? context = null)
        {
            EnsureActive();
            var key = (type, fileNameSuffix);
            if (customClasses.TryGetValue(key, out var existing)) return existing;

            var targetContext = context ?? CodeGen.GetContext(type);
            var result = targetContext.createSharpClass(type.RealName(), type.FileName() + fileNameSuffix,
                type.Namespace, isPartial: true, isStruct: type.IsValueType, isSealed: false);
            result.usingSink("ZergRush.Alive");
            result.usingSink("ZergRush");
            result.context = targetContext;
            customClasses[key] = result;
            return result;
        }

        public MethodBuilder CreateGeneratedMethod(ZRType type, GenTaskFlags task, string name,
            ZRType returnType, string arguments, bool disableableFirstArgument = false)
        {
            EnsureActive();
            return CodeGen.MakeGenMethod(type, task, name, returnType, arguments, disableableFirstArgument);
        }

        public MethodBuilder CreateGeneratedMethod(SharpClassBuilder targetClass, ZRType type,
            GenTaskFlags task, string name, ZRType returnType, string arguments,
            bool disableableFirstArgument = false)
        {
            EnsureActive();
            return CodeGen.MakeGenMethod(targetClass, type, task, name, returnType, arguments,
                disableableFirstArgument);
        }

        public void RequestGeneration(ZRType type, ZRType? requester, GenTaskFlags flags, bool force = false)
        {
            EnsureActive();
            CodeGen.RequestGen(type, requester, flags, force);
        }

        public void WriteBinaryValue(MethodBuilder sink, ZRData data, string stream)
        {
            EnsureActive();
            CodeGen.GenWriteValueToStream(sink, data, stream);
        }

        public void ReadBinaryValue(MethodBuilder sink, ZRData data, ZRType declaredType,
            string declaredAccess, ZRType carrierType, string stream, bool pooled, bool declareVariable = false)
        {
            EnsureActive();
            CodeGen.GenReadValueFromStream(sink, data, declaredType, declaredAccess, carrierType,
                stream, pooled, declareVariable);
        }

        public void WriteJsonValue(MethodBuilder sink, ZRData data, bool inList, string? propertyName = null)
        {
            EnsureActive();
            CodeGen.WriteJsonValueStatement(sink, data, inList, propertyName);
        }

        public void ReadJsonValue(MethodBuilder sink, ZRData data, ZRType declaredType,
            string declaredAccess, ZRType carrierType, bool declareVariable, bool useTemporaryVariable = false)
        {
            EnsureActive();
            CodeGen.ReadJsonValueStatement(sink, data, declaredType, declaredAccess, carrierType,
                declareVariable, useTemporaryVariable);
        }

        void EnsureActive()
        {
            if (!active)
                throw new InvalidOperationException("Generation APIs can only be used from a generator registered on an active session.");
        }
    }
}
