// ZergRush serialization schema: 3 (array JSON envelopes, logical collections, replacement reads)
using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.JsonBenchmarks {

    public partial class BenchReference : IUpdatableFrom<ZergRush.JsonBenchmarks.BenchReference>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.JsonBenchmarks.BenchReference>, IJsonSerializable, IsMultiRef, IPolymorphable, ICloneInst
    {
        public enum Types : ushort
        {
            BenchReference = 1,
            BenchReferenceChild = 2,
        }
        static Func<BenchReference> [] polymorphConstructors = new Func<BenchReference> [] {
            () => null, // 0
            () => new ZergRush.JsonBenchmarks.BenchReference(), // 1
            () => new ZergRush.JsonBenchmarks.BenchReferenceChild(), // 2
        };
        public static BenchReference CreatePolymorphic(ushort typeId) {
            if (typeId >= polymorphConstructors.Length) throw new ZergRushException("Unknown polymorphic class ID: " + typeId);
            return polymorphConstructors[typeId]() ?? throw new ZergRushException("Unknown polymorphic class ID: " + typeId);
        }
        public BenchReferenceType type => (BenchReferenceType) GetClassId();
        public virtual void UpdateFrom(ZergRush.JsonBenchmarks.BenchReference other, ZRUpdateFromHelper __helper) 
        {
            label = other.label;
            score = other.score;
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            label = reader.ReadString();
            score = reader.ReadInt32();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            writer.Write(label);
            writer.Write(score);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)709895871;
            hash += hash << 11; hash ^= hash >> 7;
            hash += CodeGenImplTools.CalculateStringHash(label);
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)score;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  BenchReference() 
        {
            label = string.Empty;
        }
        public virtual void CompareCheck(ZergRush.JsonBenchmarks.BenchReference other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (label != other.label) CodeGenImplTools.LogCompError(__helper, "label", printer, other.label, label);
            if (score != other.score) CodeGenImplTools.LogCompError(__helper, "score", printer, other.score, score);
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "label":
                label = (string) reader.Value;
                break;
                case "score":
                score = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("label");
            writer.WriteValue(label);
            writer.WritePropertyName("score");
            writer.WriteValue(score);
        }
        public virtual ushort GetClassId() 
        {
        return (ushort)Types.BenchReference;
        }
        public virtual object NewInst() 
        {
        return new BenchReference();
        }
        public static ZergRush.JsonBenchmarks.BenchReference CreatePolymorphic(BenchReferenceType __classId) 
        {
        return (ZergRush.JsonBenchmarks.BenchReference)ZergRush.JsonBenchmarks.BenchReference.CreatePolymorphic((ushort) __classId);
        }
    }
}
#endif
