using System;
using System.Runtime.InteropServices;

namespace ZergRush
{
    /// <summary>Per-reader limits. Allocation bytes estimate collection storage, not the entire object graph.</summary>
    public sealed class DeserializationBudget
    {
        public int MaxCollectionCount { get; }
        public long MaxCollectionStorageBytes { get; }
        long reserved;

        public DeserializationBudget(int maxCollectionCount = 100000, long maxCollectionStorageBytes = 64 * 1024 * 1024)
        {
            if (maxCollectionCount < 0 || maxCollectionStorageBytes < 0) throw new ArgumentOutOfRangeException();
            MaxCollectionCount = maxCollectionCount;
            MaxCollectionStorageBytes = maxCollectionStorageBytes;
        }

        public void Reserve<T>(int count)
        {
            if (count < 0 || count > MaxCollectionCount) throw new ZergRushCorruptedOrInvalidDataLayout();
            long bytes = (long)count * Storage<T>.Size;
            if (bytes > MaxCollectionStorageBytes - reserved) throw new ZergRushCorruptedOrInvalidDataLayout();
            reserved += bytes;
        }

        public void ReserveElement<T>(int count)
        {
            if (count <= 0 || count > MaxCollectionCount) throw new ZergRushCorruptedOrInvalidDataLayout();
            Reserve<T>(1);
        }

        static class Storage<T>
        {
            public static readonly int Size = StorageSize<T>();
        }

        static int StorageSize<T>()
        {
            if (!typeof(T).IsValueType) return IntPtr.Size;
            try { return Math.Max(IntPtr.Size, Marshal.SizeOf(typeof(T))); }
            catch (ArgumentException) { return 64; }
        }
    }
}
