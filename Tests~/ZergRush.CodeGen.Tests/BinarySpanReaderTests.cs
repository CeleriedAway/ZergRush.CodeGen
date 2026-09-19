using System.Buffers.Binary;

namespace ZergRush.CodeGen.Tests;

public sealed class BinarySpanReaderTests
{
    [Fact]
    public void Array_slice_is_borrowed_without_copying()
    {
        var bytes = new byte[32];
        BinaryPrimitives.WriteInt32LittleEndian(bytes.AsSpan(7), 42);
        Probe.BeforeRead = reader =>
        {
            Assert.IsType<UnmanagedMemoryStream>(reader.BaseStream);
            Assert.Equal(4, reader.BaseStream.Length);
            // Deliberately violate the caller's immutability contract to detect a copy.
            BinaryPrimitives.WriteInt32LittleEndian(bytes.AsSpan(7), 123);
        };
        try { Assert.Equal(123, ((ReadOnlySpan<byte>)bytes.AsSpan(7, 4)).ReadFromSpan<Probe>().Value); }
        finally { Probe.BeforeRead = null; }
    }

    [Fact]
    public void Stack_buffer_and_empty_input_have_scoped_lifetimes()
    {
        Span<byte> bytes = stackalloc byte[12];
        BinaryPrimitives.WriteInt32LittleEndian(bytes.Slice(3), 456);
        Assert.Equal(456, ((ReadOnlySpan<byte>)bytes.Slice(3, 4)).ReadFromSpan<Probe>().Value);
        Assert.NotNull(ReadOnlySpan<byte>.Empty.ReadFromSpan<Empty>());
        Assert.Throws<EndOfStreamException>(() => ReadOnlySpan<byte>.Empty.ReadFromSpan<Probe>());
        Assert.Throws<EndOfStreamException>(() => new ReadOnlySpan<byte>(new byte[3]).ReadFromSpan<Probe>());
    }

    [Fact]
    public void Compaction_and_nested_deserialization_preserve_the_pinned_input()
    {
        var bytes = new byte[8192];
        BinaryPrimitives.WriteInt32LittleEndian(bytes.AsSpan(11), 789);
        Probe.BeforeRead = reader =>
        {
            for (var i = 0; i < 3; i++)
            {
                _ = new byte[64 * 1024];
                GC.Collect(2, GCCollectionMode.Forced, blocking: true, compacting: true);
            }
            Span<byte> nested = stackalloc byte[4];
            BinaryPrimitives.WriteInt32LittleEndian(nested, 321);
            Assert.Equal(321, ((ReadOnlySpan<byte>)nested).ReadFromSpan<PlainInt>().Value);
        };
        try { Assert.Equal(789, ((ReadOnlySpan<byte>)bytes.AsSpan(11, 4)).ReadFromSpan<Probe>().Value); }
        finally { Probe.BeforeRead = null; }
    }

    [Fact]
    public void Reader_and_stream_are_disposed_on_success_and_failure()
    {
        foreach (var fail in new[] { false, true })
        {
            ZRBinaryReader? escapedReader = null;
            Stream? escapedStream = null;
            Probe.BeforeRead = reader =>
            {
                escapedReader = reader;
                escapedStream = reader.BaseStream;
                if (fail) throw new InvalidDataException("deliberate decode failure");
            };
            try
            {
                if (fail) Assert.Throws<InvalidDataException>(() => new ReadOnlySpan<byte>(new byte[4]).ReadFromSpan<Probe>());
                else _ = new ReadOnlySpan<byte>(new byte[4]).ReadFromSpan<Probe>();
                Assert.NotNull(escapedReader);
                Assert.NotNull(escapedStream);
                Assert.False(escapedStream.CanRead);
                Assert.Throws<ObjectDisposedException>(() => escapedReader.ReadByte());
                Assert.Throws<ObjectDisposedException>(() => escapedStream.ReadByte());
            }
            finally { Probe.BeforeRead = null; }
        }
    }

    [Fact]
    public void Allocations_do_not_scale_with_input_size()
    {
        var small = new byte[16];
        var large = new byte[4 * 1024 * 1024];
        Measure(small); Measure(large); // Warm JIT, generic construction and reader buffers.
        var smallBytes = Measure(small);
        var largeBytes = Measure(large);
        Assert.True(largeBytes < 64 * 1024, $"Decoding allocated {largeBytes} bytes for borrowed inputs");
        Assert.InRange(largeBytes - smallBytes, -4096, 4096);

        static long Measure(byte[] bytes)
        {
            var start = GC.GetAllocatedBytesForCurrentThread();
            for (var i = 0; i < 32; i++) _ = ((ReadOnlySpan<byte>)bytes).ReadFromSpan<PlainInt>();
            return GC.GetAllocatedBytesForCurrentThread() - start;
        }
    }

    [Fact]
    public void Legacy_span_constructor_keeps_a_safe_owned_copy()
    {
        var bytes = BitConverter.GetBytes(42);
#pragma warning disable CS0618 // Verify the retained compatibility API cannot reintroduce the escaped-pointer bug.
        using var reader = new ZRBinaryReader((ReadOnlySpan<byte>)bytes);
#pragma warning restore CS0618
        Array.Clear(bytes);
        GC.Collect(2, GCCollectionMode.Forced, blocking: true, compacting: true);
        Assert.Equal(42, reader.ReadInt32());
    }

    [Fact]
    public void Parallel_decoders_do_not_share_reader_state()
    {
        Parallel.For(0, 4096, value =>
        {
            var bytes = BitConverter.GetBytes(value);
            Assert.Equal(value, ((ReadOnlySpan<byte>)bytes).ReadFromSpan<PlainInt>().Value);
        });
    }

    public sealed class Probe : IBinaryDeserializable
    {
        [ThreadStatic] public static Action<ZRBinaryReader>? BeforeRead;
        public int Value;
        public void Deserialize(ZRBinaryReader reader) { BeforeRead?.Invoke(reader); Value = reader.ReadInt32(); }
    }

    public sealed class PlainInt : IBinaryDeserializable
    {
        public int Value;
        public void Deserialize(ZRBinaryReader reader) => Value = reader.ReadInt32();
    }

    public sealed class Empty : IBinaryDeserializable
    {
        public void Deserialize(ZRBinaryReader reader) => Assert.Equal(0, reader.BaseStream.Length);
    }
}
