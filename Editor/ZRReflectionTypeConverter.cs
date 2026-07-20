using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ZergRush.CodeGen
{
    /// <summary>
    /// Converts already loaded CLR types into the parser-owned CodeGen model.
    /// Only explicitly supplied root types receive members and attributes; referenced
    /// framework types are represented as lightweight ZRType nodes.
    /// </summary>
    public sealed class ZRReflectionTypeConverter
    {
        readonly Dictionary<Type, ZRType> converted = new();
        readonly HashSet<Type> populated = new();

        public IReadOnlyDictionary<Type, ZRType> ConvertedTypes => converted;

        public ZRType Convert(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var result = Reference(type);
            Populate(type, result);
            LinkChildTypes();
            return result;
        }

        public IReadOnlyList<ZRType> Convert(IEnumerable<Type> types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            var roots = types.Where(type => type != null).Distinct().ToArray();
            foreach (var type in roots) Reference(type);
            foreach (var type in roots) Populate(type, converted[type]);
            LinkChildTypes();
            return roots.Select(type => converted[type]).ToArray();
        }

        public bool TryGet(Type type, out ZRType zrType) => converted.TryGetValue(type, out zrType!);

        ZRType Reference(Type type)
        {
            if (converted.TryGetValue(type, out var existing)) return existing;

            var template = ZRType.FromSystemType(type);
            var result = new ZRType
            {
                Name = template.Name,
                Namespace = template.Namespace,
                FullName = template.FullName,
                MetadataName = template.MetadataName,
                WrittenName = template.WrittenName,
                Kind = template.Kind,
                CommonConstruct = template.CommonConstruct,
                Options = template.Options,
                ArrayRank = template.ArrayRank,
                IsResolved = true,
                IsAbstract = type.IsAbstract,
                IsSealed = type.IsSealed,
                HasDeclaredConstructors = type.GetConstructors(DeclaredInstance).Length > 0,
                HasDeclaredParameterlessConstructor = type.GetConstructor(
                    DeclaredInstance, null, CallingConventions.Any, Type.EmptyTypes, null) != null,
                ReflectionType = type
            };
            converted[type] = result;

            if (type.IsArray)
            {
                result.ElementType = Reference(type.GetElementType()!);
                result.CommonConstructArgType = result.ElementType;
            }

            if (type.IsGenericType || type.IsGenericParameter)
            {
                result.GenericArguments = type.GetGenericArguments().Select(Reference).ToList();
                result.CommonConstructArgType = result.GenericArguments.FirstOrDefault();
                if (type.IsGenericType)
                    result.GenericDefinition = type.IsGenericTypeDefinition ? result : Reference(type.GetGenericTypeDefinition());
            }

            if (type.IsGenericParameter)
            {
                result.GenericParameters.Add(new ZRGenericParameter
                {
                    Name = type.Name,
                    Attributes = type.GenericParameterAttributes,
                    Constraints = type.GetGenericParameterConstraints().Select(Reference).ToList()
                });
            }
            else if (type.IsGenericTypeDefinition)
            {
                result.GenericParameters = type.GetGenericArguments().Select(parameter => new ZRGenericParameter
                {
                    Name = parameter.Name,
                    Attributes = parameter.GenericParameterAttributes,
                    Constraints = parameter.GetGenericParameterConstraints().Select(Reference).ToList()
                }).ToList();
            }

            if (type.BaseType != null && type.BaseType != typeof(object)) result.BaseType = Reference(type.BaseType);
            result.Interfaces = type.GetInterfaces().Select(Reference).ToList();
            if (type.IsEnum) result.EnumUnderlyingType = Reference(Enum.GetUnderlyingType(type));
            return result;
        }

        void Populate(Type systemType, ZRType type)
        {
            if (!populated.Add(systemType)) return;

            type.Attributes = ReadAttributes(systemType.CustomAttributes);
            ApplyTypeAttributes(systemType, type);
            type.Members = ReadMembers(systemType, type);
            type.DataMembers = type.Members.Select(member => member.ToData()).ToList();
            if ((type.Options & ZRTypeOption.DoNotSortFields) == 0)
                type.DataMembers = type.DataMembers.OrderBy(data => data.Access, StringComparer.Ordinal).ToList();
            type.Methods = ReadMethods(systemType, type);
        }

        List<ZRMember> ReadMembers(Type systemType, ZRType owner)
        {
            var result = new List<ZRMember>();
            foreach (var field in systemType.GetFields(DeclaredInstance)
                         .Where(field => !field.IsStatic && !field.IsDefined(typeof(CompilerGeneratedAttribute), false))
                         .OrderBy(field => field.MetadataToken))
            {
                result.Add(CreateMember(field, field.FieldType, ZRMemberKind.Field, field.IsInitOnly, owner));
            }

            foreach (var property in systemType.GetProperties(DeclaredInstance)
                         .Where(property => property.GetIndexParameters().Length == 0)
                         .Where(property => property.IsDefined(typeof(GenInclude), false))
                         .OrderBy(property => property.MetadataToken))
            {
                result.Add(CreateMember(property, property.PropertyType, ZRMemberKind.Property,
                    property.SetMethod == null, owner));
            }
            return result;
        }

        ZRMember CreateMember(MemberInfo info, Type declaredSystemType, ZRMemberKind kind, bool isReadOnly, ZRType owner)
        {
            var declaredType = Reference(declaredSystemType);
            var member = new ZRMember
            {
                Name = info.Name,
                Kind = kind,
                Visibility = Visibility(info),
                ParentType = owner,
                DeclaredType = declaredType,
                MemberType = Unwrap(declaredType, out var wrappers),
                WrapperTypes = wrappers,
                IsReadOnly = isReadOnly,
                IsResolved = true,
                Attributes = ReadAttributes(info.CustomAttributes)
            };
            ApplyMemberAttributes(member);
            return member;
        }

        List<ZRMethod> ReadMethods(Type systemType, ZRType owner)
        {
            return systemType.GetMethods(DeclaredInstance)
                .Where(method => !method.IsStatic && !method.IsSpecialName)
                .OrderBy(method => method.MetadataToken)
                .Select(method => new ZRMethod
                {
                    Name = method.Name,
                    DeclaringType = owner,
                    ReturnType = Reference(method.ReturnType),
                    IsAbstract = method.IsAbstract,
                    IsVirtual = method.IsVirtual,
                    IsStatic = method.IsStatic,
                    Visibility = Visibility(method),
                    Attributes = ReadAttributes(method.CustomAttributes),
                    ReflectionMethod = method,
                    Parameters = method.GetParameters().Select(parameter => new ZRParameter
                    {
                        Name = parameter.Name ?? $"arg{parameter.Position}",
                        ParameterType = Reference(parameter.ParameterType),
                        HasDefaultValue = parameter.HasDefaultValue,
                        DefaultValue = parameter.HasDefaultValue ? parameter.DefaultValue : null,
                        Attributes = ReadAttributes(parameter.CustomAttributes)
                    }).ToList()
                }).ToList();
        }

        List<ZRAttributeInfo> ReadAttributes(IEnumerable<CustomAttributeData> attributes)
        {
            return attributes.Select(attribute => new ZRAttributeInfo
            {
                Name = NormalizeAttributeName(attribute.AttributeType.Name),
                FullName = attribute.AttributeType.FullName ?? attribute.AttributeType.Name,
                SourceText = attribute.ToString(),
                ConstructorArguments = attribute.ConstructorArguments.Select(ConvertAttributeValue).ToList(),
                NamedArguments = attribute.NamedArguments.ToDictionary(
                    argument => argument.MemberName,
                    argument => ConvertAttributeValue(argument.TypedValue))
            }).ToList();
        }

        object? ConvertAttributeValue(CustomAttributeTypedArgument argument)
        {
            if (argument.ArgumentType.IsArray && argument.Value is IReadOnlyCollection<CustomAttributeTypedArgument> values)
                return values.Select(ConvertAttributeValue).ToArray();
            if (argument.Value is Type type) return Reference(type);
            return argument.Value;
        }

        void ApplyTypeAttributes(Type systemType, ZRType type)
        {
            foreach (var attribute in systemType.GetCustomAttributes(false).OfType<Attribute>())
            {
                switch (attribute)
                {
                    case GenTaskCustomImpl custom:
                        type.CustomImplementations.Add(new ZRCustomImplInfo
                        {
                            Flags = custom.flags,
                            GenerateBaseMethods = custom.genBaseMethods,
                            Inheritable = custom.inheritable
                        });
                        type.Flags |= custom.flags;
                        type.CustomImplementFlags |= custom.flags;
                        type.Options |= ZRTypeOption.HasCustomImplementation;
                        break;
                    case GenTask task:
                        type.Flags |= task.flags;
                        break;
                    case GenIgnore ignore:
                        type.IgnoreFlags |= ignore.flags;
                        type.Options |= ZRTypeOption.HasGenIgnore;
                        break;
                    case GenTargetFolder folder:
                        type.TargetFolder = new ZRTargetFolderInfo
                        {
                            Folder = folder.folder,
                            Inheritable = folder.inheritable,
                            Priority = folder.priority
                        };
                        type.Options |= ZRTypeOption.TargetFolder;
                        break;
                    case RootType root:
                        type.RootType = Reference(root.type);
                        type.Options |= ZRTypeOption.HasRootType;
                        break;
                    case ConfigRootType root:
                        type.ConfigRootType = Reference(root.type);
                        type.Options |= ZRTypeOption.HasConfigRootType;
                        break;
                }

                type.Options |= NormalizeAttributeName(attribute.GetType().Name) switch
                {
                    "GenDoNotSortFields" => ZRTypeOption.DoNotSortFields,
                    "GenDoNotInheritGenTags" => ZRTypeOption.DoNotInheritGenTags,
                    "GenMultipleRefs" => ZRTypeOption.MultipleRefs,
                    "GenModelRootSetup" => ZRTypeOption.ModelRootSetup,
                    "GenPolymorphicNode" => ZRTypeOption.PolymorphicNode,
                    "Immutable" => ZRTypeOption.Immutable,
                    "HasRefId" => ZRTypeOption.HasRefId,
                    "GenUpdatedEvent" => ZRTypeOption.UpdatedEvent,
                    "UIDUseClassNameHash" => ZRTypeOption.UidUseClassNameHash,
                    "DoNotGen" => ZRTypeOption.DoNotGen,
                    _ => ZRTypeOption.None
                };
            }
        }

        static void ApplyMemberAttributes(ZRMember member)
        {
            foreach (var attribute in member.Attributes)
            {
                switch (attribute.Name)
                {
                    case "CanBeNull": member.Options |= ZRMemberOption.CanBeNull; break;
                    case "Immutable": member.Options |= ZRMemberOption.Immutable; break;
                    case "JustData": member.Options |= ZRMemberOption.JustData; break;
                    case "CantBeAncestor": member.Options |= ZRMemberOption.CantBeAncestor; break;
                    case "UIDComponent": member.Options |= ZRMemberOption.UidComponent; break;
                    case "DefaultVal":
                        member.Options |= ZRMemberOption.HasDefaultValue;
                        member.DefaultValue = attribute.ConstructorArguments.FirstOrDefault();
                        break;
                    case "GenArrayLengthConstraint":
                        member.Options |= ZRMemberOption.HasArrayLengthConstraint;
                        member.ArrayLengthConstraint = System.Convert.ToInt32(attribute.ConstructorArguments.FirstOrDefault() ?? 100000);
                        break;
                    case "GenUnconstrainedArrayLength":
                        member.Options |= ZRMemberOption.UnconstrainedArrayLength;
                        member.ArrayLengthConstraint = -1;
                        break;
                    case "GenInclude":
                        member.Options |= ZRMemberOption.HasGenInclude;
                        member.IncludeFlags = AttributeFlags(attribute, GenTaskFlags.All);
                        break;
                    case "GenIgnore":
                        member.Options |= ZRMemberOption.HasGenIgnore;
                        member.IgnoreFlags = AttributeFlags(attribute, GenTaskFlags.All);
                        break;
                }
            }
        }

        static GenTaskFlags AttributeFlags(ZRAttributeInfo attribute, GenTaskFlags fallback)
        {
            var value = attribute.ConstructorArguments.FirstOrDefault();
            if (value is GenTaskFlags flags) return flags;
            if (value != null) return (GenTaskFlags)System.Convert.ToInt32(value);
            return fallback;
        }

        static ZRType Unwrap(ZRType declaredType, out List<FieldWrapperType> wrappers)
        {
            wrappers = new List<FieldWrapperType>();
            var current = declaredType;
            while (current.CommonConstruct is ZRCommonConstruct.Cell or ZRCommonConstruct.LivableSlot or ZRCommonConstruct.Nullable)
            {
                wrappers.Add(current.CommonConstruct switch
                {
                    ZRCommonConstruct.Cell => FieldWrapperType.Cell,
                    ZRCommonConstruct.LivableSlot => FieldWrapperType.LivableSlot,
                    _ => FieldWrapperType.Nullable
                });
                var inner = current.CommonConstructArgType ?? current.GenericArguments.FirstOrDefault();
                if (inner == null || ReferenceEquals(inner, current)) break;
                current = inner;
            }
            return current;
        }

        void LinkChildTypes()
        {
            foreach (var type in converted.Values) type.ChildTypes.Clear();
            foreach (var type in converted.Values)
            {
                if (type.BaseType != null && type.BaseType.ChildTypes.All(child => child != type))
                    type.BaseType.ChildTypes.Add(type);
            }
        }

        static ZRMemberVisibility Visibility(MemberInfo member)
        {
            return member switch
            {
                FieldInfo field => Visibility(field.IsPublic, field.IsFamily, field.IsAssembly,
                    field.IsFamilyOrAssembly, field.IsFamilyAndAssembly),
                MethodBase method => Visibility(method.IsPublic, method.IsFamily, method.IsAssembly,
                    method.IsFamilyOrAssembly, method.IsFamilyAndAssembly),
                PropertyInfo property => property.GetMethod != null
                    ? Visibility(property.GetMethod)
                    : property.SetMethod != null ? Visibility(property.SetMethod) : ZRMemberVisibility.Unknown,
                _ => ZRMemberVisibility.Unknown
            };
        }

        static ZRMemberVisibility Visibility(bool isPublic, bool isFamily, bool isAssembly,
            bool isFamilyOrAssembly, bool isFamilyAndAssembly)
        {
            if (isPublic) return ZRMemberVisibility.Public;
            if (isFamilyOrAssembly) return ZRMemberVisibility.ProtectedInternal;
            if (isFamilyAndAssembly) return ZRMemberVisibility.PrivateProtected;
            if (isFamily) return ZRMemberVisibility.Protected;
            if (isAssembly) return ZRMemberVisibility.Internal;
            return ZRMemberVisibility.Private;
        }

        static string NormalizeAttributeName(string name) =>
            name.EndsWith("Attribute", StringComparison.Ordinal) ? name[..^"Attribute".Length] : name;

        const BindingFlags DeclaredInstance = BindingFlags.Instance | BindingFlags.Public |
                                              BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
    }
}
