using System;
using System.IO;

namespace ZergRush
{
    /// <summary>Versioned root persistence. Historical unversioned SimpleList capacity bytes are deliberately rejected.</summary>
    public static class BinaryPersistence
    {
        const int Magic = 0x3252425a; // ZBR2
        const int Version = 2;

        public static byte[] Write<T>(T value) where T : IBinarySerializable
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new ZRBinaryWriter(stream))
                {
                    writer.Write(Magic);
                    writer.Write(Version);
                    value.Serialize(writer);
                    return stream.ToArray();
                }
            }
        }

        public static T Read<T>(byte[] bytes, DeserializationBudget budget = null, int maxInputBytes = 64 * 1024 * 1024)
            where T : IBinaryDeserializable, new()
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            if (bytes.Length > maxInputBytes) throw new ZergRushCorruptedOrInvalidDataLayout();
            using (var reader = new ZRBinaryReader(bytes))
            {
                if (reader.ReadInt32() != Magic || reader.ReadInt32() != Version)
                    throw new InvalidDataException("Unsupported persistence schema. Unversioned SimpleList saves require application-specific migration with a validated count.");
                if (budget != null) reader.Budget = budget;
                var value = new T();
                value.Deserialize(reader);
                if (reader.BaseStream.Position != reader.BaseStream.Length) throw new InvalidDataException("Trailing binary data.");
                return value;
            }
        }
    }
}
