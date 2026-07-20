using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ZergRush.CodeGen
{
    public static class ReflectionCompatibilityExtensions
    {
        public static bool IsNullable(this Type type)
        {
            return type != null && Nullable.GetUnderlyingType(type) != null;
        }

        public static bool IsGenericOfType(this Type type, Type genericType)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }

        public static bool IsCell(this Type type)
        {
            return HasGenericBaseNamed(type, "Cell`1");
        }

        public static bool IsLivableSlot(this Type type)
        {
            return HasGenericBaseNamed(type, "LivableSlot`1");
        }

        public static bool IsConfig(this Type type)
        {
            return type != null && typeof(ZergRush.Alive.LoadableConfig).IsAssignableFrom(type);
        }

        public static bool IsDataNode(this Type type)
        {
            for (var current = type; current != null; current = current.BaseType)
            {
                if (current.Name == "DataNode") return true;
            }
            return false;
        }

        static bool HasGenericBaseNamed(Type type, string metadataName)
        {
            for (var current = type; current != null; current = current.BaseType)
            {
                if (current.IsGenericType && current.GetGenericTypeDefinition().Name == metadataName)
                    return true;
            }
            return false;
        }

        public static Type FirstGenericArg(this Type type)
        {
            if (type.IsArray) return type.GetElementType();
            if (type.IsGenericType) return type.GetGenericArguments()[0];
            return type.BaseType?.FirstGenericArg();
        }

        public static Type EnrichGeneric(this Type type, Type genericArgument)
        {
            if (!type.IsGenericType || type.IsConstructedGenericType) return type;
            return type.MakeGenericType(genericArgument);
        }

        public static bool HasInHierarchy(this Type type, Func<Type, bool> predicate)
        {
            return ParentWithPredicate(type, predicate) != null;
        }

        public static IEnumerable<Type> Parents(this Type type)
        {
            for (var current = type?.BaseType; current != null; current = current.BaseType)
                yield return current;
        }

        public static Type ParentWithTag<T>(this Type type) where T : Attribute
        {
            return ParentWithPredicate(type, current => current.IsDefined(typeof(T), false));
        }

        public static T FindTagInHierarchy<T>(this Type type) where T : Attribute
        {
            return ParentWithTag<T>(type)?.GetCustomAttribute<T>(false);
        }

        static Type ParentWithPredicate(Type type, Func<Type, bool> predicate)
        {
            while (type != null)
            {
                if (predicate(type)) return type;
                type = type.BaseType;
            }
            return null;
        }

        public static GenTaskFlags ReadGenFlags(this Type type)
        {
            var flags = GenTaskFlags.None;
            for (var current = type; current != null; current = current.BaseType)
            {
                flags = current.GetCustomAttributes<GenTask>(false)
                    .Aggregate(flags, (result, task) => result | task.flags);
            }

            var ignore = type.GetCustomAttribute<GenIgnore>(true);
            if (ignore != null) flags &= ~ignore.flags;
            return flags;
        }

        public static bool HasInHierarchy(this ZRType type, Func<Type, bool> predicate)
        {
            var systemType = type?.TryResolveSystemType();
            return systemType != null && systemType.HasInHierarchy(predicate);
        }

        public static bool IsDataNode(this ZRType type)
        {
            var current = type?.TryResolveSystemType();
            while (current != null)
            {
                if (current.Name == "DataNode") return true;
                current = current.BaseType;
            }
            return false;
        }
    }
}
