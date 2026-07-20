using System;
using Type = ZergRush.CodeGen.ZRType;
using System.Linq;
using System.Collections.Generic;
using ZergRush.Alive;

namespace ZergRush.CodeGen
{
    public static partial class CodeGen
    {
        public static string LivableEntryEnliveName = "Enlive";
        public static string LivableEntryMortifyName = "Mortify";

        public static string LivableGeneratedEnliveName = "Enlive";
        public static string LivableGeneratedMortifyName = "Mortify";

        public static string LivableGeneratedEnliveChildrenName = "EnliveChildren";
        public static string LivableGeneratedMortifyChildrenName = "MortifyChildren";

        public static string LivableCustomEnliveName = "EnliveSelf";
        public static string LivableCustomMortifyName = "MortifySelf";

        public static string LivableEnliveArgs = "";//"";
        public static string LivableEnliveCallArgs = "";//"";

        static bool IsLivableCustomType(this Type t)
        {
            return t.IsAssignableTo(typeof(Livable)) && !t.IsLivableContainer();
        }

        static bool IsLivableContainer(this Type t)
        {
            return t != null && t.IsConstructedGenericType && (t.IsLivableList() || t.IsLivableSlot());
        }
        static bool IsLivableGen(this Type t)
        {
            return ((t.ReadGenFlags() & GenTaskFlags.LifeSupport) != 0) || t.IsLivableContainer() || (t.IsLivableAncestor());
        }
        static bool IsLivableList(this Type t)
        {
            if (t == null) return false;
            return t.IsGenericOfType(typeof(LivableList<>)) ||
                   t.Name.StartsWith("LivableList", StringComparison.Ordinal) ||
                   t.GenericDefinition?.Name.StartsWith("LivableList", StringComparison.Ordinal) == true;
        }
        public static bool IsLivableSlot(this Type t)
        {
            if (t == null) return false;
            var tName = t.Name;
            return tName.StartsWith("LivableSlot");
        }
        static bool HasNestedLivableChildren(this Type t)
        {
            return t.GetMembersForCodeGen(GenTaskFlags.LifeSupport, true)
                .Any(member => member.IsReadOnly && member.MemberType.IsLivableCustomType() ||
                    member.MemberType.IsLivableList());
        }

        sealed class LivableAddressMember
        {
            public int id;
            public string idAccess = "";
            public string childAccess = "";
        }

        static bool IsLivableAddressMember(ZRMember member, ZRData info)
        {
            if (info.JustData) return false;

            var declaredType = member.DeclaredType ?? info.Type;
            if (declaredType.IsLivableList() || declaredType.IsLivableSlot()) return true;

            return member.WrapperTypes.Count == 0 && info.Type.IsLivableCustomType();
        }

        static int DeclaredLivableAddressMemberCount(Type type)
        {
            if (type == null || type == typeof(Livable)) return 0;
            return type.GetMembersForCodeGen(GenTaskFlags.LifeSupport)
                .Count(member => IsLivableAddressMember(member, member.ToData()));
        }

        static int BaseLivableAddressMemberCount(Type type)
        {
            var count = 0;
            var baseType = type.BaseType;
            while (baseType != null && baseType != typeof(Livable) && baseType != typeof(object))
            {
                count += DeclaredLivableAddressMemberCount(baseType);
                baseType = baseType.BaseType;
            }

            return count;
        }

        static List<LivableAddressMember> CollectLivableAddressMembers(Type carrierType,
            IEnumerable<ZRMember> members, string accessPrefix, int firstId)
        {
            var result = new List<LivableAddressMember>();
            var nextId = firstId;
            foreach (var member in members)
            {
                ProcessMember(carrierType, member, accessPrefix, GenTaskFlags.LifeSupport, false,
                    (processedMember, info, declaredAccess) =>
                    {
                        if (!IsLivableAddressMember(processedMember, info)) return;

                        if (!processedMember.IsReadOnly)
                        {
                            Error($"{declaredAccess} in type {carrierType} is part of the livable address hierarchy and must be readonly");
                        }

                        var declaredType = processedMember.DeclaredType ?? info.Type;
                        result.Add(new LivableAddressMember
                        {
                            id = nextId++,
                            idAccess = declaredAccess,
                            childAccess = declaredType.IsLivableSlot() ? info.Access : declaredAccess
                        });
                    });
            }

            return result;
        }

        static void EmitLivableAddressSwitch(MethodBuilder sink, IReadOnlyList<LivableAddressMember> members)
        {
            if (members.Count == 0) return;

            sink.content("switch (localChildId)");
            sink.openBrace();
            foreach (var member in members)
            {
                sink.content($"case {member.id}: return {member.childAccess};");
            }
            sink.closeBrace();
        }

        static void EmitLivableAddressAssignments(MethodBuilder sink, IReadOnlyList<LivableAddressMember> members)
        {
            foreach (var member in members)
            {
                sink.content($"{member.idAccess}.livableAddressId = {member.id};");
            }
        }

        static void GenerateLivableAddressMemberCode(Type type, MethodBuilder sink,
            Action<MethodBuilder, IReadOnlyList<LivableAddressMember>> emit)
        {
            var members = type.GetMembersForCodeGen(GenTaskFlags.LifeSupport).ToList();
            var genericMembers = type.IsGenericTypeDecl()
                ? members.Where(MemberDependsOnGenericParameter).ToList()
                : new List<ZRMember>();
            var ordinaryMembers = genericMembers.Count == 0
                ? members
                : members.Where(member => !MemberDependsOnGenericParameter(member)).ToList();

            var firstId = BaseLivableAddressMemberCount(type) + 1;
            var ordinary = CollectLivableAddressMembers(type, ordinaryMembers,
                type.AccessPrefixInGeneratedFunction(), firstId);
            emit(sink, ordinary);

            if (genericMembers.Count == 0) return;

            var genericFirstId = firstId + ordinary.Count;
            var instances = GenericInstancesFor(type);
            var genericParameter = type.GetGenericArguments()[0];
            for (var index = 0; index < instances.Count; ++index)
            {
                var instance = instances[index];
                var thisName = $"__livableAddressThis{index}";
                sink.content($"{(index == 0 ? "" : "else ")}if (typeof({genericParameter.Name}) == typeof({instance.FirstGenericArg().RealName(true)}))");
                sink.openBrace();
                sink.content($"var {thisName} = ({instance.RealName(true)})(object)this;");

                var specializedMembers = instance.GetMembersForCodeGen(GenTaskFlags.LifeSupport)
                    .ToDictionary(member => member.Name, StringComparer.Ordinal);
                var branchMembers = new List<ZRMember>();
                foreach (var genericMember in genericMembers)
                {
                    if (!specializedMembers.TryGetValue(genericMember.Name, out var specializedMember))
                    {
                        Error($"Could not find member {genericMember.Name} on registered generic instance {instance}.");
                        continue;
                    }
                    branchMembers.Add(specializedMember);
                }

                emit(sink, CollectLivableAddressMembers(type, branchMembers, thisName, genericFirstId));
                sink.closeBrace();
            }

            if (instances.Count > 0)
            {
                sink.content("else");
                sink.openBrace();
            }
            sink.content($"throw new System.NotSupportedException($\"Generic specialization '{{GetType()}}' is not registered for {type.RealName(true)}.\");");
            if (instances.Count > 0) sink.closeBrace();
        }

        static void GenerateLivableChildLookup(Type type)
        {
            var sink = MakeGenMethod(type, GenTaskFlags.OwnershipHierarchy, nameof(Livable.GetLivableChild),
                typeof(ILivable), "int localChildId");
            sink.type = MethodType.Override;
            sink.doNotCallBaseMethod = true;

            GenerateLivableAddressMemberCode(type, sink, EmitLivableAddressSwitch);
            sink.content("return base.GetLivableChild(localChildId);");
        }

        static void GenerateLivableAddressAssignments(Type type, MethodBuilder constructor)
        {
            GenerateLivableAddressMemberCode(type, constructor, EmitLivableAddressAssignments);
        }

        static void GenerateLivable(Type type, string funcPrefix)
        {
            if (type.IsLivableGen() == false)
            {
                Error($"Type {type.RealName(true)} must be Livable ancestor to generate life support system");
                return;
            }
            
            var sinkEnlive = MakeGenMethod(type, GenTaskFlags.LifeSupport, funcPrefix + LivableGeneratedEnliveName, Void,
                LivableEnliveArgs);
            sinkEnlive.doNotCallBaseMethod = true;
            var sinkMortify = MakeGenMethod(type, GenTaskFlags.LifeSupport, funcPrefix + LivableGeneratedMortifyName, Void,
                "");
            sinkMortify.doNotCallBaseMethod = true;
            
            sinkEnlive.classBuilder.usingSink("ZergRush.Alive");
            sinkEnlive.classBuilder.usingSink("ZergRush");
            
            sinkEnlive.content($"{LivableCustomEnliveName}({LivableEnliveCallArgs});");
            sinkMortify.content($"{LivableCustomMortifyName}();");
            
            sinkEnlive.content($"{LivableGeneratedEnliveChildrenName}({LivableEnliveCallArgs});");
            sinkMortify.content($"{LivableGeneratedMortifyChildrenName}();");
            
            var sinkEnliveChildren = MakeGenMethod(type, GenTaskFlags.LifeSupport, funcPrefix + LivableGeneratedEnliveChildrenName, Void, LivableEnliveArgs);
            sinkEnliveChildren.access = MethodAccess.Protected;
            var sinkMortifyChildren = MakeGenMethod(type, GenTaskFlags.LifeSupport, funcPrefix + LivableGeneratedMortifyChildrenName, Void, "");
            sinkMortifyChildren.access = MethodAccess.Protected;
            
            type.ProcessMembers(GenTaskFlags.LifeSupport, false, (member, info, declaredAccess) =>
            {
                if (info.JustData) return;
                if ((info.Type.IsArray || info.Type.IsList()) && !info.Type.IsHierarchySupportContainer() && info.Type.FirstGenericArg().IsLivableGen())
                {
                    Error($"field {info.Access} in type {type} is list of livable values which is not allowed. " +
                          $"Use LivableList to store livable values");
                    return;
                }
                if (member.WrapperTypes.FirstOrDefault() == FieldWrapperType.Cell && info.Type.IsLivableGen())
                {
                    Error($"field {info.Access} in type {type} is cell of livable value which is not allowed. " +
                          $"Use LivableSlot to dynamically store livable value");
                    return;
                }
                if (!info.Type.IsLivableGen()) return;
                if (member.WrapperTypes.Count == 0 && info.Type.CanBeAncestor() && !info.CantBeAncestor)
                {
                    Error($"field {info.Access} in type {type} is polymorphic and readonly livable, " +
                          $"use [CantBeAscestor] tag to guarantee its type");
                    return;
                }
                
                sinkEnliveChildren.content($"{declaredAccess}.{LivableEntryEnliveName}();");
                sinkMortifyChildren.content($"{declaredAccess}.{LivableEntryMortifyName}();");
            }, GenericMembers(sinkEnliveChildren, sinkMortifyChildren));

            TraverseGenCustomType(new TraversStrategy
            {
                funcName = "VisitNode",
                funcArgs = "Action<object> action",
                interfaceType = null,
                needMembersGenRequest = false,
                memberPredicate = (_, info) => !info.JustData &&
                    (info.Type.IsLivableNode() || info.Type.IsLivableContainer() || info.Type.IsLivableList()),
                memberProcess = (sink, _, _, declaredAccess) =>
                    sink.content($"{declaredAccess}.VisitNode(action);"),
                elemProcess = (sink, info) => sink.content($"{info.Access}.VisitNode(action);"),
                flag = GenTaskFlags.OwnershipHierarchy
            }, type, funcPrefix);

            GenerateLivableChildLookup(type);

        }
        
        static string CreateLivableInRootFunc(this Type t)
        {
            return "Create" + t.UniqueName(false);
        }
    }
}
