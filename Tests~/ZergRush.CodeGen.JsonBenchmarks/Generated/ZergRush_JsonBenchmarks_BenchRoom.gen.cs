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

    public partial class BenchRoom : IUpdatableFrom<ZergRush.JsonBenchmarks.BenchRoom>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.JsonBenchmarks.BenchRoom>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.JsonBenchmarks.BenchRoom other, ZRUpdateFromHelper __helper) 
        {
            note = other.note;
            values.UpdateFrom(other.values, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            note = reader.ReadString();
            values.Deserialize(reader);
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            writer.Write(note);
            values.Serialize(writer);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)212886601;
            hash += hash << 11; hash ^= hash >> 7;
            hash += CodeGenImplTools.CalculateStringHash(note);
            hash += hash << 11; hash ^= hash >> 7;
            hash += values.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  BenchRoom() 
        {
            note = string.Empty;
            values = new System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue>();
        }
        public virtual void CompareCheck(ZergRush.JsonBenchmarks.BenchRoom other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (note != other.note) CodeGenImplTools.LogCompError(__helper, "note", printer, other.note, note);
            __helper.Push("values");
            values.CompareCheck(other.values, __helper, printer);
            __helper.Pop();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "note":
                note = (string) reader.Value;
                break;
                case "values":
                values.ReadFromJson(reader);
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("note");
            writer.WriteValue(note);
            writer.WritePropertyName("values");
            values.WriteJson(writer);
        }
    }
}
#endif
