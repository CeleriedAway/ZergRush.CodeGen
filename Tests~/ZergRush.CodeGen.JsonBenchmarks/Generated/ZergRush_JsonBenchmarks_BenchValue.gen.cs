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

    public partial class BenchValue : IUpdatableFrom<ZergRush.JsonBenchmarks.BenchValue>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.JsonBenchmarks.BenchValue>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public enum Types : ushort
        {
            BenchValue = 1,
            BenchChild = 2,
        }
        static Func<BenchValue> [] polymorphConstructors = new Func<BenchValue> [] {
            () => null, // 0
            () => new ZergRush.JsonBenchmarks.BenchValue(), // 1
            () => new ZergRush.JsonBenchmarks.BenchChild(), // 2
        };
        public static BenchValue CreatePolymorphic(ushort typeId) {
            if (typeId >= polymorphConstructors.Length) throw new ZergRushException("Unknown polymorphic class ID: " + typeId);
            return polymorphConstructors[typeId]() ?? throw new ZergRushException("Unknown polymorphic class ID: " + typeId);
        }
        public BenchValueType type => (BenchValueType) GetClassId();
        public virtual void UpdateFrom(ZergRush.JsonBenchmarks.BenchValue other, ZRUpdateFromHelper __helper) 
        {
            label = other.label;
            if (other.nested == null) {
                nested = null;
            }
            else { 
                var nestedClassId = other.nested.GetClassId();
                if (nested == null || nested.GetClassId() != nestedClassId) {
                    nested = (ZergRush.JsonBenchmarks.BenchValue)other.nested.NewInst();
                }
                nested.UpdateFrom(other.nested, __helper);
            }
            score = other.score;
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            label = reader.ReadString();
            if (!reader.ReadBoolean()) {
                nested = null;
            }
            else { 
                var nestedClassId = reader.ReadUInt16();
                if (nested == null || nested.GetClassId() != nestedClassId) {
                    nested = (ZergRush.JsonBenchmarks.BenchValue)ZergRush.JsonBenchmarks.BenchValue.CreatePolymorphic(nestedClassId);
                }
                nested.Deserialize(reader);
            }
            score = reader.ReadInt32();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            writer.Write(label);
            if (!(nested != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(nested.GetClassId());
                nested.Serialize(writer);
            }
            writer.Write(score);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)481725970;
            hash += hash << 11; hash ^= hash >> 7;
            hash += CodeGenImplTools.CalculateStringHash(label);
            hash += hash << 11; hash ^= hash >> 7;
            hash += nested != null ? nested.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)score;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  BenchValue() 
        {
            label = string.Empty;
        }
        public virtual void CompareCheck(ZergRush.JsonBenchmarks.BenchValue other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (label != other.label) CodeGenImplTools.LogCompError(__helper, "label", printer, other.label, label);
            if (CodeGenImplTools.CompareNull(__helper, "nested", printer, nested, other.nested)) {
                if (CodeGenImplTools.CompareClassId(__helper, "nested", printer, nested, other.nested)) {
                    __helper.Push("nested");
                    nested.CompareCheck(other.nested, __helper, printer);
                    __helper.Pop();
                }
            }
            if (score != other.score) CodeGenImplTools.LogCompError(__helper, "score", printer, other.score, score);
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "label":
                label = (string) reader.Value;
                break;
                case "nested":
                if (reader.TokenType == JsonToken.Null) {
                    nested = null;
                }
                else { 
                    var __nestedFrame = reader.BeginObject(true, false);
                    var nestedClassId = __nestedFrame.ClassId;
                    if (nested == null || nested.GetClassId() != nestedClassId) {
                        nested = (ZergRush.JsonBenchmarks.BenchValue)ZergRush.JsonBenchmarks.BenchValue.CreatePolymorphic(nestedClassId);
                    }
                    reader.ReadObject(nested, __nestedFrame);
                }
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
            if (!(nested != null))
            {
                writer.WritePropertyName("nested");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nested");
                nested.WriteJson(writer);
            }
            writer.WritePropertyName("score");
            writer.WriteValue(score);
        }
        public virtual ushort GetClassId() 
        {
        return (ushort)Types.BenchValue;
        }
        public virtual object NewInst() 
        {
        return new BenchValue();
        }
        public static ZergRush.JsonBenchmarks.BenchValue CreatePolymorphic(BenchValueType __classId) 
        {
        return (ZergRush.JsonBenchmarks.BenchValue)ZergRush.JsonBenchmarks.BenchValue.CreatePolymorphic((ushort) __classId);
        }
    }
}
#endif
