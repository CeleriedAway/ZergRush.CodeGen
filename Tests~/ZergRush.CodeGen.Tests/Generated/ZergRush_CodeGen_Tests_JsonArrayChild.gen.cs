// ZergRush serialization schema: 3 (array JSON envelopes, logical collections, replacement reads)
using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class JsonArrayChild : IUpdatableFrom<ZergRush.CodeGen.Tests.JsonArrayChild>, IUpdatableFrom<ZergRush.CodeGen.Tests.JsonArrayNode>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.JsonArrayNode>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public override void UpdateFrom(ZergRush.CodeGen.Tests.JsonArrayNode other, ZRUpdateFromHelper __helper) 
        {
            base.UpdateFrom(other,__helper);
            var otherConcrete = (ZergRush.CodeGen.Tests.JsonArrayChild)other;
            large = otherConcrete.large;
            precise = otherConcrete.precise;
        }
        public void UpdateFrom(ZergRush.CodeGen.Tests.JsonArrayChild other, ZRUpdateFromHelper __helper) 
        {
            this.UpdateFrom((ZergRush.CodeGen.Tests.JsonArrayNode)other, __helper);
        }
        public override void Deserialize(ZRBinaryReader reader) 
        {
            base.Deserialize(reader);
            large = reader.ReadUInt64();
            precise = reader.ReadDecimal();
        }
        public override void Serialize(ZRBinaryWriter writer) 
        {
            base.Serialize(writer);
            writer.Write(large);
            writer.Write(precise);
        }
        public override ulong CalculateHash(ZRHashHelper __helper) 
        {
            var baseVal = base.CalculateHash(__helper);
            ulong hash = baseVal;
            hash ^= (ulong)145311288;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)large;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)precise.GetHashCode();
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public override void CompareCheck(ZergRush.CodeGen.Tests.JsonArrayNode other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            base.CompareCheck(other,__helper,printer);
            var otherConcrete = (ZergRush.CodeGen.Tests.JsonArrayChild)other;
            if (large != otherConcrete.large) CodeGenImplTools.LogCompError(__helper, "large", printer, otherConcrete.large, large);
            if (precise != otherConcrete.precise) CodeGenImplTools.LogCompError(__helper, "precise", printer, otherConcrete.precise, precise);
        }
        public override bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            if (base.ReadFromJsonField(reader, __name)) return true;
            switch(__name)
            {
                case "large":
                large = ulong.Parse((string)reader.Value);
                break;
                case "precise":
                precise = Convert.ToDecimal(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                break;
                default: return false; break;
            }
            return true;
        }
        public override void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            base.WriteJsonFields(writer);
            writer.WritePropertyName("large");
            writer.WriteValue(large.ToString());
            writer.WritePropertyName("precise");
            writer.WriteValue(precise.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }
        public override ushort GetClassId() 
        {
        return (ushort)Types.JsonArrayChild;
        }
        public override object NewInst() 
        {
        return new JsonArrayChild();
        }
    }
}
#endif
