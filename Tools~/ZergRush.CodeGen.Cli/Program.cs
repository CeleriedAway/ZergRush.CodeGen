using ZergRush.CodeGen;

try
{
    var options = ZRCodeGenCliArguments.Parse(args);
    if (options.ShowHelp)
    {
        Console.WriteLine(ZRCodeGenCliArguments.Usage);
        return 0;
    }

    var inputs = options.ResolveInputs(Environment.CurrentDirectory);
    var parser = new ZRCodeParser();
    var types = parser.ParseInputs(inputs);

    if (options.GenerationOutput != null)
    {
        if (!options.PreserveTargetFolders)
        {
            foreach (var type in types)
            {
                type.TargetFolder = new ZRTargetFolderInfo
                {
                    Folder = options.GenerationOutput,
                    Inheritable = true,
                    Priority = type.TargetFolder?.Priority ?? 1
                };
            }
        }

        CodeGen.Gen(types, options.GenerationOutput);
        Console.WriteLine(options.PreserveTargetFolders
            ? $"Generated source using parsed target folders and {options.GenerationOutput} as fallback."
            : $"Generated source into {options.GenerationOutput}.");
        return 0;
    }

    Console.WriteLine($"Resolved inputs: {inputs.Count}");
    Console.WriteLine($"Parsed types: {types.Count}");
    foreach (var type in types)
    {
        Console.WriteLine($"{type.Kind} {type.FullName}");
        Console.WriteLine($"  Flags: {type.Flags}");
        Console.WriteLine($"  Options: {type.Options}");
        if (type.TargetFolder != null)
            Console.WriteLine($"  TargetFolder: {type.TargetFolder.Folder} priority={type.TargetFolder.Priority} inheritable={type.TargetFolder.Inheritable}");
        if (type.BaseType != null) Console.WriteLine($"  Base: {type.BaseType.FullName}");
        if (type.GenericDefinition != null) Console.WriteLine($"  GenericDefinition: {type.GenericDefinition.FullName}");
        if (type.ChildTypes.Count > 0)
            Console.WriteLine($"  ChildTypes: {string.Join(", ", type.ChildTypes.Select(child => child.FullName))}");
        Console.WriteLine($"  DataMembers: {type.DataMembers.Count}");
        foreach (var member in type.Members)
        {
            var data = member.ToData();
            var wrappers = member.WrapperTypes.Count == 0 ? "None" : string.Join(" -> ", member.WrapperTypes);
            Console.WriteLine($"    {member.Kind} {member.Name}: {data.Type.FullName} access={data.Access}");
            Console.WriteLine($"      Declared: {member.DeclaredType?.WrittenName ?? member.DeclaredType?.FullName ?? "<unknown>"} wrappers={wrappers}");
            Console.WriteLine($"      Include={member.IncludeFlags} Ignore={member.IgnoreFlags} Options={data.Options} ReadOnly={member.IsReadOnly}");
        }
    }
    return 0;
}
catch (Exception exception)
{
    Console.Error.WriteLine(exception.Message);
    Console.Error.WriteLine();
    Console.Error.WriteLine(ZRCodeGenCliArguments.Usage);
    return 2;
}
