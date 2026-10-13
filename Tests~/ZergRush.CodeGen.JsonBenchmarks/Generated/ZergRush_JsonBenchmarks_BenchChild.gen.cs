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

    public partial class BenchChild : IUpdatableFrom<ZergRush.JsonBenchmarks.BenchChild>, IUpdatableFrom<ZergRush.JsonBenchmarks.BenchValue>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.JsonBenchmarks.BenchValue>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public override void UpdateFrom(ZergRush.JsonBenchmarks.BenchValue other, ZRUpdateFromHelper __helper) 
        {
            base.UpdateFrom(other,__helper);
            var otherConcrete = (ZergRush.JsonBenchmarks.BenchChild)other;
            extra = otherConcrete.extra;
        }
        public void UpdateFrom(ZergRush.JsonBenchmarks.BenchChild other, ZRUpdateFromHelper __helper) 
        {
            this.UpdateFrom((ZergRush.JsonBenchmarks.BenchValue)other, __helper);
        }
        public override void Deserialize(ZRBinaryReader reader) 
        {
            base.Deserialize(reader);
            extra = reader.ReadInt32();
        }
        public override void Serialize(ZRBinaryWriter writer) 
        {
            base.Serialize(writer);
            writer.Write(extra);
        }
        public override ulong CalculateHash(ZRHashHelper __helper) 
        {
            var baseVal = base.CalculateHash(__helper);
            ulong hash = baseVal;
            hash ^= (ulong)1040736210;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)extra;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  BenchChild() 
        {

        }
        public override void CompareCheck(ZergRush.JsonBenchmarks.BenchValue other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            base.CompareCheck(other,__helper,printer);
            var otherConcrete = (ZergRush.JsonBenchmarks.BenchChild)other;
            if (extra != otherConcrete.extra) CodeGenImplTools.LogCompError(__helper, "extra", printer, otherConcrete.extra, extra);
        }
        public override bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            if (base.ReadFromJsonField(reader, __name)) return true;
            switch(__name)
            {
                case "extra":
                extra = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public override void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            base.WriteJsonFields(writer);
            writer.WritePropertyName("extra");
            writer.WriteValue(extra);
        }
        public override ushort GetClassId() 
        {
        return (ushort)Types.BenchChild;
        }
        public override object NewInst() 
        {
        return new BenchChild();
        }
    }
}
#endif
