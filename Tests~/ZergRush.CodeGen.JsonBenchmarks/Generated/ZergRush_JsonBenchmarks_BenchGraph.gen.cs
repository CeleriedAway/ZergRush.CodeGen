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

    public partial class BenchGraph : IUpdatableFrom<ZergRush.JsonBenchmarks.BenchGraph>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.JsonBenchmarks.BenchGraph>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.JsonBenchmarks.BenchGraph other, ZRUpdateFromHelper __helper) 
        {
            values.UpdateFrom(other.values, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            values.Deserialize(reader);
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            values.Serialize(writer);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)271205541;
            hash += hash << 11; hash ^= hash >> 7;
            hash += values.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  BenchGraph() 
        {
            values = new System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference>();
        }
        public virtual void CompareCheck(ZergRush.JsonBenchmarks.BenchGraph other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            __helper.Push("values");
            values.CompareCheck(other.values, __helper, printer);
            __helper.Pop();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "values":
                values.ReadFromJson(reader);
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("values");
            values.WriteJson(writer);
        }
    }
}
#endif
