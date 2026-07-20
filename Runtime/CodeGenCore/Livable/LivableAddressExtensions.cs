using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ZergRush.Alive
{
    public static class LivableAddressExtensions
    {
        public static int[] GetLivableAddress(this ILivable target)
        {
            if (TryGetLivableAddress(target, out var address)) return address;
            throw new InvalidOperationException("The livable is detached, its hierarchy is cyclic, or its address no longer resolves to the same object.");
        }

        public static bool TryGetLivableAddress(this ILivable target, out int[] address)
        {
            address = Array.Empty<int>();
            if (target == null || !target.IsInHierarchy) return false;

            var reverseAddress = new List<int>();
            var visited = new HashSet<ILivable>(ReferenceComparer.Instance);
            var current = target;

            while (current is not LivableRoot)
            {
                if (!visited.Add(current)) return false;

                var parent = current.GetLivableAddressParent();
                if (parent == null) return false;

                reverseAddress.Add(current.livableAddressId);
                current = parent;
            }

            reverseAddress.Reverse();
            var candidate = reverseAddress.ToArray();
            if (!current.TryGetLivableChild(candidate, out var resolved) || !ReferenceEquals(resolved, target))
                return false;

            address = candidate;
            return true;
        }

        public static ILivable GetLivableChild(this ILivable root, IReadOnlyList<int> address)
        {
            if (TryGetLivableChild(root, address, out var child) && child != null) return child;
            throw new InvalidOperationException("The livable address is invalid, stale, or does not start from a LivableRoot.");
        }

        public static bool TryGetLivableChild(this ILivable root, IReadOnlyList<int> address, out ILivable? child)
        {
            child = null;
            if (root is not LivableRoot || address == null) return false;

            ILivable? current = root;
            for (var i = 0; i < address.Count; i++)
            {
                current = current.GetLivableChild(address[i]);
                if (current == null) return false;
            }

            child = current;
            return true;
        }

        sealed class ReferenceComparer : IEqualityComparer<ILivable>
        {
            public static readonly ReferenceComparer Instance = new ReferenceComparer();

            public bool Equals(ILivable? x, ILivable? y) => ReferenceEquals(x, y);
            public int GetHashCode(ILivable obj) => RuntimeHelpers.GetHashCode(obj);
        }
    }
}
